using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ToDo.Lib;

namespace ToDo
{
    public partial class FormTasks : Form
    {
        Category curCat;
        public FormTasks()
        {
            InitializeComponent();
        }

        public void UpdateTasks()
        {
            if (curCat == null)
            {
                return;
            }
            clbTasks.Items.Clear();
            foreach (Task t in curCat.Tasks)
            {
                clbTasks.Items.Add(t.Text, t.IsDone);
            }
        }

        public void UpdateTasks(Category c)
        {
            curCat = c;
            this.Text = "Tasks : " + c.Name;
            clbTasks.Items.Clear();
            foreach (Task t in curCat.Tasks)
            {
                clbTasks.Items.Add(t.Text, t.IsDone);
            }
        }

        public bool IsClosed
        {
            get;
            private set;
        }

        private void FormTasks_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsClosed = true;
        }

        private void DeleteSelection()
        {
            if (clbTasks.SelectedIndex != -1)
            {
                curCat.Tasks.RemoveAt(clbTasks.SelectedIndex);
                UpdateTasks();
                CategoryManager.OnListChanged(this, new EventArgs());
            }
        }

        private void clbTasks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addTaskToolStripMenuItem.PerformClick();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DeleteSelection();
            }
        }

        private void clbTasks_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            curCat.Tasks[e.Index].IsDone = e.NewValue.Equals(CheckState.Checked);
            CategoryManager.OnListChanged(this, new EventArgs());
        }

        private void addTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddTask f = new FormAddTask();
            f.ShowDialog();
            if (f.NewTask != null)
            {
                curCat.AddTask(f.NewTask);
                UpdateTasks();
                CategoryManager.OnListChanged(this, new EventArgs());
            }
        }

        private void batchAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBatchAdd f = new FormBatchAdd();
            f.ShowDialog();
            if (f.NewTasks != null)
            {
                foreach (Task t in f.NewTasks)
                {
                    curCat.AddTask(t);
                }
                UpdateTasks();
                CategoryManager.OnListChanged(this, new EventArgs());
            }
        }
    }
}
