using System.ComponentModel;
using System.Windows.Forms;

namespace ToDo
{
    public partial class FormSplashScreen : Form
    {
        private readonly FormMain _mainForm;

        public FormSplashScreen(FormMain mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            var bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.RunWorkerAsync();
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            _mainForm.InitializeTodo();
        }
    }
}