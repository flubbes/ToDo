using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo
{
    public partial class FormSplashScreen : Form
    {
        FormMain mainForm;

        public FormSplashScreen(FormMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.RunWorkerAsync();
            
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            mainForm.InitializeTodo();
            
        }
    }
}
