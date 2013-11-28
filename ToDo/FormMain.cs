﻿using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using ToDo.Lib;

namespace ToDo
{
    [Serializable]
    public partial class FormMain : Form
    {
        #region private fields and constants
        private string defaultDB;
        private string loadedDB;
        private DbManager dbm;
        private Settings settings;
        private string settingsPath;
        private List<string> recentFiles;
        private const string recentFilesKeyWord = "RecentFiles";
        private FormChanges formChanges;
        private static ToDoList todoList;
        

        #endregion

        /// <summary>
        /// Initialize the main form
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            this.Icon = ApplicationManager.GetAppIcon();
            InitObjectListView();
            
            ApplicationManager.Initialize();
        }

        public void InitObjectListView()
        {
            olvcPriority.GroupKeyGetter = PriorityGroupKeyGetter;
            olvcText.GroupKeyGetter = CategoryGroupKeyGetter;
            olvcDueDate.GroupKeyGetter = DueDateGroupKeyGetter;
            olvcEstimatedTime.GroupKeyGetter = EstimatedGroupKeyGetter;

            olvcText.FillsFreeSpace = true;
        }

        private object EstimatedGroupKeyGetter(object rowObject)
        {
            Task t = ((Task)rowObject);
            return t.EstimatedTime;
        }

        private object DueDateGroupKeyGetter(object rowObject)
        {
            Task t = ((Task)rowObject);
            return t.DueDate;
        }

        private object PriorityGroupKeyGetter(object rowObject)
        {
            Task t = ((Task)rowObject);
            return t.Priority;
        }

        private string CategoryGroupKeyGetter(object rowObject)
        {
            Task t = ((Task)rowObject);
            if (string.IsNullOrEmpty(t.Category))
            {
                return "No Category";
            }
            return t.Category;
        }

        public void InitializeTodo()
        {
            settingsPath =  ApplicationManager.GetAppPath() + "settings.dat";
            defaultDB = ApplicationManager.GetAppPath() +  "db.todo";
            dbm = new DbManager();
            LoadSettings();
            LoadDatabase(defaultDB);
            LoadRecentFiles();
            UpdateRecentFilesControl();
            UpdateList();
            ToDoList.ListChanged += todoList_ListChanged;
        }

        #region events

        /// <summary>
        /// Is triggered when the todo list changes
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">The EventArgs with the event data</param>
        void todoList_ListChanged(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(UpdateList));
            }
            else
            {
                UpdateList();
            }
            
        }


        /// <summary>
        /// Is triggered when an item got doubleclicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void lvCategories_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            editToolStripMenuItem.PerformClick();
        }

        /// <summary>
        /// Is triggered when the form is closed
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
            SaveDB();
        }

        /// <summary>
        /// triggered when the load todolist button is clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void loadTodoListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            //set the filter to only .todo files
            ofd.Filter = "Todo-Lists|*.todo|Old Xml TodoList|*.xml";

            //set the initial directory to the path where the executeable is
            ofd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    ToDoList = DbVersionManager.ReadToDoList(ofd.FileName);

                    loadedDB = Path.GetFullPath(ofd.FileName);
                    AddRecentFile(loadedDB);
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("This database is corrupted!");
                }
                UpdateList();
            }
        }

        /// <summary>
        /// Is triggered when the show changes button is clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void showChangesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //create a new changes form
            if (formChanges == null || formChanges.IsDisposed)
            {
                formChanges = new FormChanges(ToDoList);
            }
            
            //show the form
            formChanges.Show();

            formChanges.TopMost = true;
            formChanges.TopMost = false;
        }

        /// <summary>
        /// Is triggered when the edit button is clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = olvTasks.SelectedIndex;
            Task t = ToDoList.Tasks[index]; 
            FormTask ft = new FormTask("Edit a task", t);
            if(ft.ShowDialog() == DialogResult.OK)
            {
                ToDoList.ModifyTaskByIndex(index, t);
            }
        }


        /// <summary>
        /// Triggered when a recent file drop down item is clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        void tsddi_Click(object sender, EventArgs e)
        {
            ToolStripDropDownItem item = (ToolStripDropDownItem)sender;
            LoadDatabase(item.Text);
        }
        #endregion

        public static ToDoList ToDoList
        {
            get
            {
                return todoList;
            }
            private set
            {
                todoList = value;
            }
        }

        /// <summary>
        /// Adds a new recent file to the recent files list
        /// </summary>
        /// <param name="path">The path to the recent used file</param>
        private void AddRecentFile(string path)
        {
            if(recentFiles == null)
            {
                recentFiles = new List<string>();
            }
            recentFiles.Add(path);
            if(recentFiles.Count > 5)
            {
                recentFiles.RemoveAt(0);
            }
            UpdateRecentFilesControl();
        }

        /// <summary>
        /// Updates the recent files dropdown control
        /// </summary>
        private void UpdateRecentFilesControl()
        {
            if(recentFiles == null || recentFiles.Count <= 0)
            {
                recentFilesToolStripMenuItem.Visible = false;
                return;
            }
            recentFilesToolStripMenuItem.Visible = true;
            recentFilesToolStripMenuItem.DropDownItems.Clear();
            foreach(string path in recentFiles)
            {
                recentFilesToolStripMenuItem.DropDownItems.Add(path);
            }
            foreach(ToolStripDropDownItem tsddi in recentFilesToolStripMenuItem.DropDownItems)
            {
                tsddi.Click += tsddi_Click;
            }
        }

        /// <summary>
        /// Load a database from the given path
        /// </summary>
        /// <param name="path">The from where you want to load the database</param>
        private void LoadDatabase(string path)
        {
            loadedDB = path;
            ToDoList = new ToDoList();
            if (File.Exists(path))
            {
                
                try
                {
                    ToDoList = DbVersionManager.ReadToDoList(path);
                }
                catch
                {
                    MessageBox.Show("Database corrupted");
                }
            }
        }

        /// <summary>
        /// Loads the settings
        /// </summary>
        private void LoadSettings()
        {
            settings = new Settings();
            if (File.Exists(settingsPath))
            {
                try
                {
                    settings.Deserialize(settingsPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Loads the recentFiles Queue
        /// </summary>
        private void LoadRecentFiles()
        {
            recentFiles = new List<string>();
            if (settings.HasKey(recentFilesKeyWord))
            {
                recentFiles = settings.GetSetting<List<string>>(recentFilesKeyWord);
            }
        }

        /// <summary>
        /// Clears all items in the listView and adds all categories to it again
        /// </summary>
        private void UpdateList()
        {
            olvTasks.SetObjects(ToDoList.Tasks);
        }

        private void ResizeColumns()
        {
        }


        /// <summary>
        /// Stores the currently loaded db in a binary file
        /// </summary>
        public void SaveDB()
        {
            FormAsyncProgressBar fap = new FormAsyncProgressBar(new Action<BackgroundWorker>(SaveDbAsync), "Saving database", ProgressBarStyle.Marquee);
            fap.ShowDialog();
        }

        private void SaveDbAsync(BackgroundWorker worker)
        {
            ToDoList.Serialize(ToDoList, loadedDB);
        }

        /// <summary>
        /// Saves the settings
        /// </summary>
        private void SaveSettings()
        {
            settings.StoreSetting(recentFilesKeyWord, recentFiles);
            settings.Serialize(settingsPath);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            FormSplashScreen fss = new FormSplashScreen(this);
            fss.ShowDialog();
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ApplicationManager.Updater.HasUpdate())
                {
                    if (MessageBox.Show("There is a new version available! Would you like to update now?", "New Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        ApplicationManager.Updater.DownloadUpdate();
                    }
                }
                else
                {
                    MessageBox.Show("There is no update");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog();
        }

        private void addTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTask fat = new FormTask("Add a new Task");
            if(fat.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ToDoList.AddTask(fat.Result);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToDoList.RemoveAtIndex(olvTasks.SelectedIndex);
        }

        private void olvTasks_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                addTaskToolStripMenuItem.PerformClick();
            }
            if(e.KeyCode == Keys.Delete)
            {
                deleteToolStripMenuItem.PerformClick();
            }
        }
    }
}
