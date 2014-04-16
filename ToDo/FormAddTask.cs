using System;
using System.Windows.Forms;
using ToDo.Lib;

namespace ToDo
{
    public partial class FormAddTask : Form
    {
        public FormAddTask()
        {
            InitializeComponent();
            NewTask = null;
        }

        public Task NewTask { get; private set; }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            NewTask = new Task { Text = tbTaskText.Text.Trim() };
            Close();
        }

        private void tbTaskText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddTask.PerformClick();
            }
        }

        private void FormAddTask_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}