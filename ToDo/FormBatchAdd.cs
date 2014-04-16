using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using ToDo.Lib;

namespace ToDo
{
    public partial class FormBatchAdd : Form
    {
        public FormBatchAdd()
        {
            InitializeComponent();
            NewTasks = new List<Task>();
        }

        public List<Task> NewTasks { get; private set; }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            NewTasks = new List<Task>();
            if (nupStart.Value > nupEnd.Value)
            {
                return;
            }
            var fap = new FormAsyncProgressBar(AddTheTasks, "Generating Tasks");
            fap.ShowDialog();
            Close();
        }

        private void AddTheTasks(BackgroundWorker worker)
        {
            var g = (int)nupEnd.Value;
            for (var i = (int)nupStart.Value; i <= (int)nupEnd.Value; i++)
            {
                var t = new Task { Text = tbString.Text };
                t.Text = t.Text.Replace("{0}", i.ToString(CultureInfo.InvariantCulture));
                NewTasks.Add(t);
                worker.ReportProgress(i * 100 / g);
            }
        }
    }
}