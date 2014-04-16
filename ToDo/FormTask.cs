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
    public partial class FormTask : Form
    {
        Task curTask;
        public FormTask(string formText, Task t): this(formText)
        {
            cbCategories.Text = t.Category;
            dtpDueDate.Value = t.DueDate;
            tbTaskText.Text = t.Text;
            nudPriority.Value = t.Priority;
            curTask = t;
        }

        public FormTask(string formText)
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.None;
            Result = null;
            FillCategoryComboBox(FormMain.ToDoList.ParseCategories());
            dtpDueDate.Value = DateTime.Now;
        }

        private void FillCategoryComboBox(string[] categories)
        {
            cbCategories.Items.Clear();
            foreach(string cat in categories)
            {
                cbCategories.Items.Add(cat);
            }
        }

        public Task Result
        {
            get;
            private set;
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            if (curTask != null)
            {
                Result = curTask;
            }
            else
            {
                Result = new Task();
            }
            Result.Text = tbTaskText.Text.Trim();
            Result.DueDate = dtpDueDate.Value;
            Result.Priority = (int)nudPriority.Value;
            Result.EstimatedTime = cbEstimatedTimes.SelectedText;
            Result.Category = cbCategories.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
