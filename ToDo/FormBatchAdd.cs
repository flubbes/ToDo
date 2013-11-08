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
    public partial class FormBatchAdd : Form
    {
        public FormBatchAdd()
        {
            InitializeComponent();
            NewTasks = new List<Task>();
        }

        public List<Task> NewTasks
        {
            get;
            private set;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            NewTasks = new List<Task>();
            if (nupStart.Value > nupEnd.Value)
            {
                return;
            }
            for (decimal i = nupStart.Value; i <= nupEnd.Value; i++)
            {
                Task t = new Task();
                t.Text = tbString.Text;
                t.Text = t.Text.Replace("{0}", i.ToString());
                NewTasks.Add(t);
            }
            this.Close();
        }
    }
}
