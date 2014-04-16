using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ToDo.Lib;
using Task = ToDo.Lib.Task;

namespace ToDo
{
    public partial class FormChanges : Form
    {
        private readonly ToDoList _todoList;

        public FormChanges(ToDoList tl)
        {
            InitializeComponent();
            Icon = ApplicationManager.GetAppIcon();
            _todoList = tl;
            UpdateListView();
            tl.ListChanged += tl_ListChanged;
        }

        private void tl_ListChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(UpdateListView));
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
            List<Change> tempChanges = _todoList.Changes.Count <= 100 ? _todoList.Changes : _todoList.Changes.GetRange(_todoList.Changes.Count - 101, 100);
            tempChanges.Reverse();
            foreach (Change c in tempChanges)
            {
                var item = new ListViewItem(c.Type.ToString());
                item.SubItems.Add(c.Author);
                object before = c.Before;
                if (before != null && before.GetType() == typeof(Task))
                {
                    var t = (Task)before;
                    item.SubItems.Add(ColumnStringFromTask(t));
                }
                else
                {
                    item.SubItems.Add("");
                }

                object after = c.After;
                if (after != null && after.GetType() == typeof(Task))
                {
                    var t = (Task)after;
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
            if (e.KeyCode == Keys.F5)
            {
                UpdateListView();
            }
        }
    }
}