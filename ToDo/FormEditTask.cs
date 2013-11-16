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
    public partial class FormEditTask : Form
    {
        public FormEditTask(string editString)
        {
            InitializeComponent();
            tbText.Text = editString;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        public string ResultText
        {
            get;
            private set;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ResultText = tbText.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void tbText_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                btnSave.PerformClick();
            }
        }
    }
}
