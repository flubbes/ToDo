using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ToDo.Lib;

namespace ToDo
{
    [Serializable]
    public partial class FormMain : Form
    {
        FormTasks ft;
        string defaultDB = "db.dat";
        string loadedDB;
        DbManager dbm;
        TodoList todoList;

        public FormMain()
        {
            InitializeComponent();
            dbm = new DbManager();
            loadedDB = defaultDB;
            try
            {
                todoList = TodoList.DeserializeFromBinary(defaultDB);

            }
            catch
            {
                todoList = new TodoList();
            }

            UpdateList();
            ft = new FormTasks(ref todoList);
            todoList.ListChanged += todoList_ListChanged;
        }

        void todoList_ListChanged(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void addCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddCategory f = new FormAddCategory();
            f.ShowDialog();
            if (f.NewCat != null)
            {
                todoList.AddCategory(f.NewCat);
                UpdateList();
            }
        }

        private void UpdateList()
        {
            lvCategories.Items.Clear();
            foreach (Category c in todoList.Categories)
            {
                ListViewItem i = new ListViewItem(c.Name);
                i.SubItems.Add(c.TaskCount.ToString());
                i.SubItems.Add(c.CategoryPercentage.ToString());
                lvCategories.Items.Add(i);
            }
        }

        private void lvCategories_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvCategories_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            editToolStripMenuItem.PerformClick();
        }

        public void SaveDB()
        {
            TodoList.SerializeToXml(ref todoList, loadedDB);
            todoList.ListChanged += todoList_ListChanged;
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveDB();
        }

        private void DeleteSelection()
        {
            if (lvCategories.SelectedIndices.Count > 0)
            {
                if (lvCategories.SelectedIndices[0] != -1)
                {
                    Change c = new Change(Environment.UserName, ChangeType.Delete, lvCategories.SelectedIndices[0], null);
                    todoList.Categories.RemoveAt(lvCategories.SelectedIndices[0]);
                    todoList.OnListChanged(this, new TodoListChangedEventArgs(c));
                }
            }
        }

        private void lvCategories_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelection();
            }
        }

        private void loadTodoListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Xml-Files|*.xml";
            ofd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    todoList = TodoList.DeserializeFromBinary(ofd.FileName);
                    loadedDB = Path.GetFullPath(ofd.FileName);
                }
                catch (XmlException)
                {
                    MessageBox.Show("This database is corrupted!");
                }
                UpdateList();
            }
        }

        private void showChangesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormChanges fc = new FormChanges(todoList.Changes);
            fc.Show();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvCategories.SelectedIndices.Count >= 1)
            {
                if (ft.IsClosed)
                {
                    ft = new FormTasks(ref todoList);
                    ft.UpdateTasks(todoList.Categories[lvCategories.SelectedIndices[0]]);
                    ft.Show();
                }
                else
                {
                    ft.UpdateTasks(todoList.Categories[lvCategories.SelectedIndices[0]]);
                    ft.Show();
                }
                ft.TopMost = true;
                ft.TopMost = false;
            }
        }
    }
}
