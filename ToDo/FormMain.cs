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

        private const string SettingsPath = "settings.dat";
        private const string RecentFilesKeyWord = "RecentFiles";
        private DbManager _dbm;
        private const string DefaultDb = "db.todo";
        private FormChanges _formChanges;
        private string _loadedDb;
        private List<string> _recentFiles;
        private Settings _settings;
        private FormTasks _taskForm;
        private TodoList _todoList;

        #endregion private fields and constants

        /// <summary>
        ///     Initialize the main form
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            ApplicationManager.Initialize();
        }

        public void InitializeTodo()
        {
            _dbm = new DbManager();
            LoadSettings();
            LoadDatabase(DefaultDb);
            LoadRecentFiles();
            UpdateRecentFilesControl();
            UpdateList();
            _taskForm = new FormTasks(ref _todoList);
            _todoList.ListChanged += todoList_ListChanged;
        }

        /// <summary>
        ///     Adds a new recent file to the recent files list
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
        ///     Updates the recent files dropdown control
        /// </summary>
        private void UpdateRecentFilesControl()
        {
            if (_recentFiles == null)
            {
                return;
            }
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
        ///     Load a database from the given path
        /// </summary>
        /// <param name="path">The from where you want to load the database</param>
        private void LoadDatabase(string path)
        {
            _loadedDb = path;
            _todoList = new TodoList();
            if (File.Exists(path))
            {
                try
                {
                    _todoList = TodoList.DeserializeFromBinary(DefaultDb);
                }
                catch
                {
                    MessageBox.Show("Database corrupted");
                }
            }
        }

        /// <summary>
        ///     Loads the settings
        /// </summary>
        private void LoadSettings()
        {
            _settings = new Settings();
            if (File.Exists(SettingsPath))
            {
                try
                {
                    _settings.Deserialize(SettingsPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        ///     Loads the recentFiles Queue
        /// </summary>
        private void LoadRecentFiles()
        {
            _recentFiles = new List<string>();
            if (_settings.HasKey(RecentFilesKeyWord))
            {
                _recentFiles = _settings.GetSetting<List<string>>(RecentFilesKeyWord);
            }
        }

        /// <summary>
        ///     Clears all items in the listView and adds all categories to it again
        /// </summary>
        private void UpdateList()
        {
            lvCategories.Items.Clear();
            foreach (Category c in _todoList.Categories)
            {
                AddItemToListView(c.Name, c.TaskCount.ToString(CultureInfo.InvariantCulture), c.CategoryPercentage.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        ///     Adds a new item to the listView
        /// </summary>
        /// <param name="categoryName">The name of the category</param>
        /// <param name="taskCount">The number of tasks in this category</param>
        /// <param name="categoryPercentage">The percentage of this category</param>
        private void AddItemToListView(string categoryName, string taskCount, string categoryPercentage)
        {
            var i = new ListViewItem(categoryName);
            i.SubItems.Add(taskCount);
            i.SubItems.Add(categoryPercentage);
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => lvCategories.Items.Add(i)));
            }
            else
            {
                lvCategories.Items.Add(i);
            }
        }

        /// <summary>
        ///     Stores the currently loaded db in a binary file
        /// </summary>
        public void SaveDb()
        {
            var fap = new FormAsyncProgressBar(SaveDbAsync, "Saving database", ProgressBarStyle.Marquee);
            fap.ShowDialog();
        }

        private void SaveDbAsync(BackgroundWorker worker)
        {
            TodoList.SerializeToBinary(ref _todoList, _loadedDb);
        }

        /// <summary>
        ///     Saves the settings
        /// </summary>
        private void SaveSettings()
        {
            _settings.StoreSetting(RecentFilesKeyWord, _recentFiles);
            _settings.Serialize(SettingsPath);
        }

        /// <summary>
        ///     Deletes the current selection
        /// </summary>
        private void DeleteSelection()
        {
            if (lvCategories.SelectedIndices.Count > 0)
            {
                //if the selection does not contain an invalid value
                if (lvCategories.SelectedIndices[0] != -1)
                {
                    var c = new Change(Environment.UserName, ChangeType.Delete,
                        _todoList.Categories[lvCategories.SelectedIndices[0]].Clone(), null);
                    _todoList.Categories.RemoveAt(lvCategories.SelectedIndices[0]);
                    _todoList.AddChange(c);
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            var fss = new FormSplashScreen(this);
            fss.ShowDialog();
        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
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

        #region events

        /// <summary>
        ///     Is triggered when the todo list changes
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">The EventArgs with the event data</param>
        private void todoList_ListChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(UpdateList));
            }
            else
            {
                UpdateList();
            }
        }

        /// <summary>
        ///     triggered when the add category button is clicked
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">The event data</param>
        private void addCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FormAddCategory();
            f.ShowDialog();
            if (f.NewCat != null)
            {
                _todoList.AddCategory(f.NewCat);
                UpdateList();
            }
        }

        /// <summary>
        ///     Is triggered when an item got doubleclicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void lvCategories_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            editToolStripMenuItem.PerformClick();
        }

        /// <summary>
        ///     Is triggered when the form is closed
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
            SaveDb();
        }

        /// <summary>
        ///     Is triggered when a key is down on the listView
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
        ///     triggered when the load todolist button is clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void loadTodoListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Todo-Lists|*.todo",
                InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath),
                Multiselect = false
            };

            //set the filter to only .todo files

            //set the initial directory to the path where the executeable is
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _todoList = TodoList.DeserializeFromBinary(ofd.FileName);

                    _loadedDb = Path.GetFullPath(ofd.FileName);
                    AddRecentFile(_loadedDb);
                }
                catch (Exception)
                {
                    MessageBox.Show("This database is corrupted!");
                }
                UpdateList();
            }
        }

        /// <summary>
        ///     Is triggered when the show changes button is clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void showChangesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //create a new changes form
            if (_formChanges == null || _formChanges.IsDisposed)
            {
                _formChanges = new FormChanges(_todoList);
            }

            //show the form
            _formChanges.Show();

            _formChanges.TopMost = true;
            _formChanges.TopMost = false;
        }

        /// <summary>
        ///     Is triggered when the edit button is clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvCategories.SelectedIndices.Count >= 1)
            {
                if (_taskForm.IsClosed)
                {
                    _taskForm = new FormTasks(ref _todoList);
                    _taskForm.UpdateTasks(_todoList.Categories[lvCategories.SelectedIndices[0]]);
                    _taskForm.Show();
                }
                else
                {
                    _taskForm.UpdateTasks(_todoList.Categories[lvCategories.SelectedIndices[0]]);
                    _taskForm.Show();
                }
                _taskForm.TopMost = true;
                _taskForm.TopMost = false;
            }
        }

        /// <summary>
        ///     Triggered when the import old xml file button is clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">the event data</param>
        private void importOldXMLFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Todo-List|*.xml",
                InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath),
                Multiselect = false
            };

            //set the filter to only .todo files

            //set the initial directory to the path where the executeable is
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _todoList.FromXml(ofd.FileName);
                    _loadedDb = Path.GetDirectoryName(ofd.FileName) + "imported" + DateTime.Now.ToShortDateString() +
                               ".todo";
                    AddRecentFile(_loadedDb);
                }
                catch (Exception)
                {
                    MessageBox.Show("This database is corrupted!");
                }
                UpdateList();
            }
        }

        /// <summary>
        ///     Triggered when a recent file drop down item is clicked
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event data</param>
        private void tsddi_Click(object sender, EventArgs e)
        {
            var item = (ToolStripDropDownItem)sender;
            LoadDatabase(item.Text);
        }

        #endregion events
    }
}