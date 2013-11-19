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
        TodoList todoList;
        List<Task> batchAddTaskList;

        public FormTasks(TodoList todoList)
        {
            InitializeComponent();
            this.Icon = ApplicationManager.GetAppIcon();
            this.todoList = todoList;
        }

        public void UpdateTasks()
        {
            if (curCat == null)
            {
                return;
            }
            UpdateTasks(curCat);
        }

        public void UpdateTasks(Category c)
        {
            curCat = c;
            FormAsyncProgressBar fap = new FormAsyncProgressBar(new Action<BackgroundWorker>(RefreshListView), "Refreshing form");
            fap.ShowDialog();
        }

        private void RefreshListView(BackgroundWorker worker)
        {
            this.Text = "Tasks : " + curCat.Name;
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(clbTasks.Items.Clear));
            }
            else
            {
                clbTasks.Items.Clear();
            }
            int counter = 0;
            int g = curCat.Tasks.Count;
            foreach (Task t in curCat.Tasks)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(() => clbTasks.Items.Add(t.Text, t.IsDone)));
                }
                else
                {
                    clbTasks.Items.Add(t.Text, t.IsDone);
                }
                counter++;
                worker.ReportProgress(counter * 100 / g);
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

        private Task GetSelectedTask()
        {
            if (clbTasks.SelectedIndex != -1)
            {
                return curCat.Tasks[clbTasks.SelectedIndex];
            }
            return null;
        }

        private void DeleteSelection()
        {
            if (clbTasks.SelectedIndex != -1)
            {
                Change c = new Change(Environment.UserName, ChangeType.Delete, curCat.Tasks[clbTasks.SelectedIndex].Clone(), null);
                curCat.Tasks.RemoveAt(clbTasks.SelectedIndex);
                UpdateTasks();
                todoList.AddChange(c);
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
            curCat.Tasks[e.Index].SetIsDone(e.NewValue.Equals(CheckState.Checked));
        }

        private void addTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddTask f = new FormAddTask();
            f.ShowDialog();
            if (f.NewTask != null)
            {
                Change c = new Change(Environment.UserName, ChangeType.Add, null, f.NewTask.Clone());
                curCat.AddTask(f.NewTask);
                UpdateTasks();
                todoList.AddChange(c);
            }
        }

        private void AddTheTasks(BackgroundWorker worker)
        {
            int w = 0;
            int g = batchAddTaskList.Count;
            int p = 0;
            for (int i = 0; i < batchAddTaskList.Count; i++)
            {
                Task t = batchAddTaskList[i];
                Change c = new Change(Environment.UserName, ChangeType.Add, null, t.Clone());
                curCat.AddTask(t);
                todoList.AddChangeWithoutEventTriggering(c);

                w = i + 1;
                p = w * 100 / g;
                worker.ReportProgress(p);
            }
            todoList.TriggerChangeEvent();
        }

        private void batchAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBatchAdd f = new FormBatchAdd();
            f.ShowDialog();
            if (f.NewTasks != null)
            {
                batchAddTaskList = f.NewTasks;
                FormAsyncProgressBar fap = new FormAsyncProgressBar(new Action<BackgroundWorker>(AddTheTasks), "Adding the tasks");
                fap.ShowDialog();
                UpdateTasks();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task toEdit = GetSelectedTask();
            if(toEdit != null)
            {
                FormEditTask fet = new FormEditTask(toEdit.Text);
                if(fet.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Task old = (Task)toEdit.Clone();
                    toEdit.Text = fet.ResultText;
                    UpdateTasks();
                    Change c = new Change(Environment.UserName, ChangeType.Edit, old, toEdit);
                    todoList.AddChange(c);
                }
            }
        }
    }
}
