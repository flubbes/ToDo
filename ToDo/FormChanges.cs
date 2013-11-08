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
        List<Change> changes;
        public FormChanges(List<Change> changes)
        {
            InitializeComponent();
            this.changes = changes;
            UdpateListView();
        }

        public string ColumnStringFromTask(Task t)
        {
            return string.Format("Done: {0} Text: {1}", t.IsDone, t.Text);
        }

        public string ColumnStringFromCategory(Category c)
        {
            return string.Format("Tasks: {0} Text: {1}", c.TaskCount, c.Name);
        }

        public void UdpateListView()
        {
            lvChanges.Items.Clear();
            foreach(Change c in changes)
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
    }
}
