using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ToDo.Lib;

namespace ToDo
{
    public partial class FormTasks : Form
    {
        private readonly TodoList _todoList;
        private List<Task> _batchAddTaskList;
        private Category _curCat;

        public FormTasks(ref TodoList todoList)
        {
            InitializeComponent();
            _todoList = todoList;
        }

        public bool IsClosed { get; private set; }

        public void UpdateTasks()
        {
            if (_curCat == null)
            {
                return;
            }
            UpdateTasks(_curCat);
        }

        public void UpdateTasks(Category c)
        {
            _curCat = c;
            var fap = new FormAsyncProgressBar(RefreshListView, "Refreshing form");
            fap.ShowDialog();
        }

        private void RefreshListView(BackgroundWorker worker)
        {
            Text = "Tasks : " + _curCat.Name;
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(clbTasks.Items.Clear));
            }
            else
            {
                clbTasks.Items.Clear();
            }
            int counter = 0;
            int g = _curCat.Tasks.Count;
            foreach (Task t in _curCat.Tasks)
            {
                if (InvokeRequired)
                {
                    Task t1 = t;
                    Invoke(new MethodInvoker(() => clbTasks.Items.Add(t1.Text, t1.IsDone)));
                }
                else
                {
                    clbTasks.Items.Add(t.Text, t.IsDone);
                }
                counter++;
                worker.ReportProgress(counter * 100 / g);
            }
        }

        private void FormTasks_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsClosed = true;
        }

        private void DeleteSelection()
        {
            if (clbTasks.SelectedIndex != -1)
            {
                var c = new Change(Environment.UserName, ChangeType.Delete, _curCat.Tasks[clbTasks.SelectedIndex].Clone(),
                    null);
                _curCat.Tasks.RemoveAt(clbTasks.SelectedIndex);
                UpdateTasks();
                _todoList.AddChange(c);
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
            var old = (Task)_curCat.Tasks[e.Index].Clone();
            _curCat.Tasks[e.Index].IsDone = e.NewValue.Equals(CheckState.Checked);
            var c = new Change(Environment.UserName, ChangeType.Edit, old, _curCat.Tasks[e.Index].Clone());
            _todoList.AddChange(c);
        }

        private void addTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FormAddTask();
            f.ShowDialog();
            if (f.NewTask != null)
            {
                var c = new Change(Environment.UserName, ChangeType.Add, null, f.NewTask.Clone());
                _curCat.AddTask(f.NewTask);
                UpdateTasks();
                _todoList.AddChange(c);
            }
        }

        private void AddTheTasks(BackgroundWorker worker)
        {
            int g = _batchAddTaskList.Count;
            for (int i = 0; i < _batchAddTaskList.Count; i++)
            {
                Task t = _batchAddTaskList[i];
                var c = new Change(Environment.UserName, ChangeType.Add, null, t.Clone());
                _curCat.AddTask(t);
                _todoList.AddChangeWithoutEventTriggering(c);

                int w = i + 1;
                int p = w * 100 / g;
                worker.ReportProgress(p);
            }
            _todoList.TriggerChangeEvent();
        }

        private void batchAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FormBatchAdd();
            f.ShowDialog();
            if (f.NewTasks != null)
            {
                _batchAddTaskList = f.NewTasks;
                var fap = new FormAsyncProgressBar(AddTheTasks, "Adding the tasks");
                fap.ShowDialog();
                UpdateTasks();
            }
        }
    }
}