using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ToDo.Lib;

namespace ToDo
{
    public partial class FormChanges : Form
    {
        private readonly TodoList _todoList;

        public FormChanges(TodoList tl)
        {
            InitializeComponent();
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

        public string ColumnStringFromCategory(Category c)
        {
            return string.Format("Tasks: {0} Text: {1}", c.TaskCount, c.Name);
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
                else if (before != null && before.GetType() == typeof(Category))
                {
                    var t = (Category)before;
                    item.SubItems.Add(ColumnStringFromCategory(t));
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
                else if (after != null && after.GetType() == typeof(Category))
                {
                    var t = (Category)after;
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
            if (e.KeyCode == Keys.F5)
            {
                UpdateListView();
            }
        }
    }
}