using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace ToDo
{
    public partial class FormAsyncProgressBar : Form
    {
        private BackgroundWorker _backgroundWorker;
        private Action<BackgroundWorker> _toRun;

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
            Text = taskName;
            _toRun = method;
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWork;
            _backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            if (style != ProgressBarStyle.Blocks)
            {
                lblProgress.Visible = false;
                lblProgressDesc.Visible = false;
            }
        }

        public void StartTheTask()
        {
            _backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Refresh();
            Application.DoEvents();
            Close();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => UpdateProgressBar(e.ProgressPercentage)));
            }
            else
            {
                UpdateProgressBar(e.ProgressPercentage);
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _toRun.Invoke(_backgroundWorker);
        }

        private void UpdateProgressBar(int val)
        {
            lblProgress.Text = val.ToString(CultureInfo.InvariantCulture) + " %";
            progressBar.Value = val;
        }

        private void FormAsyncProgressBar_Shown(object sender, EventArgs e)
        {
            StartTheTask();
        }
    }
}