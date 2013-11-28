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
        ToDoList todoList;
        public FormChanges(ToDoList tl)
        {
            InitializeComponent();
            this.Icon = ApplicationManager.GetAppIcon();
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

        public void UpdateListView()
        {
            lvChanges.Items.Clear();
            List<Change> tempChanges;
            if (todoList.Changes.Count <= 100)
            {
                tempChanges = todoList.Changes;
            }
            else
            {
                tempChanges = todoList.Changes.GetRange(todoList.Changes.Count - 101, 100);
            }
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
