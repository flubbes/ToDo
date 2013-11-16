using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDo.Lib;

namespace ToDo
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            //lblVersion.Text = "Version: " + ApplicationManager.Updater.LocalVersion.ToString();

            Stream stream = GetType().Assembly.GetManifestResourceStream("ToDo.Changelog.txt");
            byte[] bytes = new byte[(int)stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            rtbChangelog.Text = Encoding.UTF8.GetString(bytes);
        }
    }
}
