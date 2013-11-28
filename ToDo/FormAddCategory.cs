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
using ToDo.Lib.FormatPre04;

namespace ToDo
{
    public partial class FormAddCategory : Form
    {
        public FormAddCategory()
        {
            InitializeComponent();
        }

        public Category NewCat
        {
            get;
            private set;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Please fill in all required information");
                return;
            }
            NewCat = new Category(tbName.Text);
            this.Close();
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.PerformClick();
            }
        }
    }
}
