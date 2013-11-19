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
        private FormTasks taskForm;
        private string defaultDB;
        private string loadedDB;
        private DbManager dbm;
        private Settings settings;
        private string settingsPath;
        private List<string> recentFiles;
        private const string recentFilesKeyWord = "RecentFiles";
        private FormChanges formChanges;
        private static TodoList todoList;
        #endregion

        /// <summary>
        /// Initialize the main form
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            this.Icon = ApplicationManager.GetAppIcon();
            ApplicationManager.Initialize();
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
            taskForm = new FormTasks(todoList);
            TodoList.ListChanged += todoList_ListChanged;
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
        /// triggered when the add category button is clicked
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">The event data</param>
        private void addCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddCategory f = new FormAddCategory();
            f.ShowDialog();
            if (f.NewCat != null)
            {
                TodoList.AddCategory(f.NewCat);
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
        /// Is triggered when a key is down on the listView
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void lvCategories_KeyDown(object sender, KeyEventArgs e)
        {
            //if the key is the DEL key
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelection();
            }
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
            ofd.Filter = "Todo-Lists|*.todo";

            //set the initial directory to the path where the executeable is
            ofd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    TodoList = TodoList.DeserializeFromBinary(ofd.FileName);

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
                formChanges = new FormChanges(TodoList);
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
            if (lvCategories.SelectedIndices.Count >= 1)
            {
                if (taskForm.IsClosed)
                {
                    taskForm = new FormTasks(TodoList);
                    taskForm.UpdateTasks(TodoList.Categories[lvCategories.SelectedIndices[0]]);
                    taskForm.Show();
                }
                else
                {
                    taskForm.UpdateTasks(TodoList.Categories[lvCategories.SelectedIndices[0]]);
                    taskForm.Show();
                }
                taskForm.TopMost = true;
                taskForm.TopMost = false;
            }
        }

        /// <summary>
        /// Triggered when the import old xml file button is clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">the event data</param>
        private void importOldXMLFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            //set the filter to only .todo files
            ofd.Filter = "Todo-List|*.xml";

            //set the initial directory to the path where the executeable is
            ofd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    TodoList.FromXml(ofd.FileName);
                    loadedDB = Path.GetDirectoryName(ofd.FileName) + "imported" + DateTime.Now.ToShortDateString() + ".todo";
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

        public static TodoList TodoList
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
            TodoList = new TodoList();
            if (File.Exists(path))
            {
                try
                {
                    TodoList = TodoList.DeserializeFromBinary(defaultDB);
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
            lvCategories.Items.Clear();
            foreach (Category c in TodoList.Categories)
            {
                AddItemToListView(c.Name, c.TaskCount.ToString(), c.CategoryPercentage.ToString());
            }
            ResizeColumns();
        }

        private void ResizeColumns()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => lvCategories.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)));
                this.Invoke(new MethodInvoker(() => lvCategories.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize)));
                this.Invoke(new MethodInvoker(() => lvCategories.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize)));
            }
            else
            {
                lvCategories.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lvCategories.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
                lvCategories.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        /// <summary>
        /// Adds a new item to the listView
        /// </summary>
        /// <param name="categoryName">The name of the category</param>
        /// <param name="taskCount">The number of tasks in this category</param>
        /// <param name="categoryPercentage">The percentage of this category</param>
        private void AddItemToListView(string categoryName, string taskCount, string categoryPercentage)
        {
            
            ListViewItem i = new ListViewItem(categoryName);
            i.SubItems.Add(taskCount);
            i.SubItems.Add(categoryPercentage);
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => lvCategories.Items.Add(i)));
            }
            else
            {
                lvCategories.Items.Add(i);
            }
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
            TodoList.SerializeToBinary(TodoList, loadedDB);
        }

        /// <summary>
        /// Saves the settings
        /// </summary>
        private void SaveSettings()
        {
            settings.StoreSetting(recentFilesKeyWord, recentFiles);
            settings.Serialize(settingsPath);
        }

        /// <summary>
        /// Deletes the current selection
        /// </summary>
        private void DeleteSelection()
        {
            if (lvCategories.SelectedIndices.Count > 0)
            {
                //if the selection does not contain an invalid value
                if (lvCategories.SelectedIndices[0] != -1)
                {
                    Change c = new Change(Environment.UserName, ChangeType.Delete, TodoList.Categories[lvCategories.SelectedIndices[0]].Clone(), null);
                    TodoList.Categories.RemoveAt(lvCategories.SelectedIndices[0]);
                    TodoList.AddChange(c);
                }
            }
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
    }
}
