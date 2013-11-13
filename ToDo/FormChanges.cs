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
    public partial class FormChanges : Form
    {
        TodoList todoList;
        public FormChanges(TodoList tl)
        {
            InitializeComponent();
            this.todoList = tl;
            UpdateListView();
            tl.ListChanged += tl_ListChanged;
        }

        void tl_ListChanged(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(UpdateListView));
            }
            else
            {
                UpdateListView();
            }
        }

        public string ColumnStringFromTask(Task t)
        {
            return string.Format("Done: {0} Text: {1}", t.IsDone, t.Text);
        }

        public string ColumnStringFromCategory(Category c)
        {
            return string.Format("Tasks: {0} Text: {1}", c.TaskCount, c.Name);
        }

        public void UpdateListView()
        {
            lvChanges.Items.Clear();
            List<Change> tempChanges = todoList.Changes.GetRange(todoList.Changes.Count - 101, 100);
            tempChanges.Reverse();
            foreach (Change c in tempChanges)
            {
                ListViewItem item = new ListViewItem(c.Type.ToString());
                item.SubItems.Add(c.Author);
                object before = c.Before;
                if(before != null && before.GetType() == typeof(Task))
                {
                    Task t = (Task)before;
                    item.SubItems.Add(ColumnStringFromTask(t));
                }
                else if(before != null && before.GetType() == typeof(Category))
                {
                    Category t = (Category)before;
                    item.SubItems.Add(ColumnStringFromCategory(t));
                }
                else
                {
                    item.SubItems.Add("");
                }

                object after = c.After;
                if (after != null && after.GetType() == typeof(Task))
                {
                    Task t = (Task)after;
                    item.SubItems.Add(ColumnStringFromTask(t));
                }
                else if (after != null && after.GetType() == typeof(Category))
                {
                    Category t = (Category)after;
                    item.SubItems.Add(ColumnStringFromCategory(t));
                }
                else
                {
                    item.SubItems.Add("");
                }
                lvChanges.Items.Add(item);
            }
        }

        private void lvChanges_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F5)
            {
                UpdateListView();
            }
        }
    }
}
