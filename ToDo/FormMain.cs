using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using ToDo.Lib;

namespace ToDo
{
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
        private Lib.Settings _settings;
        private List<string> _recentFiles;

        #endregion private fields and constants

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

        public static Settings Settings
        {
            get;
            set;
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
            settingsPath = ApplicationManager.GetAppPath() + "settings.dat";
            defaultDB = ApplicationManager.GetAppPath() + "db.todo";
            dbm = new DbManager();
            LoadSettings();
            LoadDatabase(defaultDB);
            LoadRecentFiles();
            UpdateRecentFilesControl();
            UpdateList();
            ToDoList.ListChanged += todoList_ListChanged;
        }

        /// <summary>
        /// Is triggered when the todo list changes
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">The EventArgs with the event data</param>
        private void todoList_ListChanged(object sender, EventArgs e)
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
            SaveDb();
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
        /// Is triggered when the edit button is clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = olvTasks.SelectedIndex;
            if (index >= ToDoList.Tasks.Count || index < 0)
            {
                return;
            }
            Task t = ToDoList.Tasks[index];
            FormTask ft = new FormTask("Edit a task", t);
            if (ft.ShowDialog() == DialogResult.OK)
            {
                ToDoList.ModifyTaskByIndex(index, t);
            }
        }

        /// <summary>
        /// Triggered when a recent file drop down item is clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void tsddi_Click(object sender, EventArgs e)
        {
            ToolStripDropDownItem item = (ToolStripDropDownItem)sender;
            LoadDatabase(item.Text);
        }

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

        private void SetSettings()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(SetSettings));
                return;
            }
            bool val = false;
            try
            {
                this.TopMost = Settings.GetSetting<bool>("TopMost");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Adds a new recent file to the recent files list
        /// </summary>
        /// <param name="path">The path to the recent used file</param>
        private void AddRecentFile(string path)
        {
            if (_recentFiles == null)
            {
                _recentFiles = new List<string>();
            }
            _recentFiles.Add(path);
            if (_recentFiles.Count > 5)
            {
                _recentFiles.RemoveAt(0);
            }
            UpdateRecentFilesControl();
        }

        /// <summary>
        /// Updates the recent files dropdown control
        /// </summary>
        private void UpdateRecentFilesControl()
        {
            if (recentFiles == null || recentFiles.Count <= 0)
            {
                recentFilesToolStripMenuItem.Visible = false;
                return;
            }
            recentFilesToolStripMenuItem.Visible = true;
            recentFilesToolStripMenuItem.DropDownItems.Clear();
            foreach (string path in _recentFiles)
            {
                recentFilesToolStripMenuItem.DropDownItems.Add(path);
            }
            foreach (ToolStripDropDownItem tsddi in recentFilesToolStripMenuItem.DropDownItems)
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
            _settings = new Settings();
            if (File.Exists(settingsPath))
            {
                try
                {
                    _settings.Deserialize(settingsPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            Settings = settings;
            SetSettings();
        }

        /// <summary>
        /// Loads the recentFiles Queue
        /// </summary>
        private void LoadRecentFiles()
        {
            _recentFiles = new List<string>();
            if (_settings.HasKey(recentFilesKeyWord))
            {
                _recentFiles = _settings.GetSetting<List<string>>(recentFilesKeyWord);
            }
        }

        /// <summary>
        /// Clears all items in the listView and adds all categories to it again
        /// </summary>
        private void UpdateList()
        {
            olvTasks.SetObjects(ToDoList.Tasks);
        }

        /// <summary>
        /// Stores the currently loaded db in a binary file
        /// </summary>
        public void SaveDb()
        {
            var fap = new FormAsyncProgressBar(SaveDbAsync, "Saving database", ProgressBarStyle.Marquee);
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
            _settings.StoreSetting(recentFilesKeyWord, _recentFiles);
            _settings.Serialize(settingsPath);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            var fss = new FormSplashScreen(this);
            fss.ShowDialog();
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ApplicationManager.Updater.HasUpdate())
                {
                    if (
                        MessageBox.Show("There is a new version available! Would you like to update now?", "New Update",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
            if (fat.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
            if (e.KeyCode == Keys.Enter)
            {
                addTaskToolStripMenuItem.PerformClick();
            }
            if (e.KeyCode == Keys.Delete)
            {
                deleteToolStripMenuItem.PerformClick();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            FormSettings settings = new FormSettings();
            settings.ShowDialog();
            SetSettings();
        }

        private void archiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToDoList.ArchiveTask(olvTasks.SelectedIndex);
        }

        private void showChangesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void archiveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormArchive fa = new FormArchive();
            fa.ShowDialog();
        }
    }
}