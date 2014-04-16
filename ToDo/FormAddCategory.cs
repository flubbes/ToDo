using System;
using System.Windows.Forms;
using ToDo.Lib;

namespace ToDo
{
    public partial class FormAddCategory : Form
    {
        public FormAddCategory()
        {
            InitializeComponent();
        }

        public Category NewCat { get; private set; }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Please fill in all required information");
                return;
            }
            NewCat = new Category(tbName.Text);
            Close();
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