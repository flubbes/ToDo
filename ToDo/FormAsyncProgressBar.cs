using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo
{
    public partial class FormAsyncProgressBar : Form
    {
        BackgroundWorker backgroundWorker;
        Action<BackgroundWorker> toRun;

        public FormAsyncProgressBar(Action<BackgroundWorker> method, string taskName)
        {
            InitializeComponent();
            InitForm(method, taskName, ProgressBarStyle.Blocks);
        }

        public FormAsyncProgressBar(Action<BackgroundWorker> method, string taskName, ProgressBarStyle style)
        {
            InitializeComponent();
            InitForm(method, taskName, style);
        }

        private void InitForm(Action<BackgroundWorker> method, string taskName, ProgressBarStyle style)
        {
            progressBar.Style = style;
            this.Text = taskName;
            toRun = method;
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            if(style != ProgressBarStyle.Blocks)
            {
                lblProgress.Visible = false;
                lblProgressDesc.Visible = false;
            }
        }

        public void StartTheTask()
        {
            backgroundWorker.RunWorkerAsync();
        }

        void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Refresh();
            Application.DoEvents();
            this.Close();
        }

        void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => UpdateProgressBar(e.ProgressPercentage)));
            }
            else
            {
                UpdateProgressBar(e.ProgressPercentage);
            }
        }

        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            toRun.Invoke(backgroundWorker);
        }

        private void UpdateProgressBar(int val)
        {
            lblProgress.Text = val.ToString() + " %";
            progressBar.Value = val;
        }

        private void FormAsyncProgressBar_Shown(object sender, EventArgs e)
        {
            StartTheTask();
        }
    }
}
