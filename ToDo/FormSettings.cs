using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDo.Lib;

namespace ToDo
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
            cbStartWithWindows.Checked = KabeLib.Registry.AutostartControl.IsSettedCorrectly("ToDo", "\"" + Application.ExecutablePath + "\"");
            try { cbTopMost.Checked = FormMain.Settings.GetSetting<bool>("TopMost"); }
            catch { }
        }

        private void cbTopMost_CheckedChanged(object sender, EventArgs e)
        {
            FormMain.Settings.StoreSetting("TopMost", cbTopMost.Checked);
        }

        private void cbStartWithWindows_CheckedChanged(object sender, EventArgs e)
        {
            KabeLib.Registry.AutostartControl.SetStartup(Application.ExecutablePath, "ToDo", cbStartWithWindows.Checked);
        }
    }
}
