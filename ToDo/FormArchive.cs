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
    public partial class FormArchive : Form
    {

        public FormArchive()
        {
            InitializeComponent();
            this.Icon = ApplicationManager.GetAppIcon();
            olvcText.GroupKeyGetter = TextGroupKeyGetter;
            this.olvTasks.SetObjects(FormMain.ToDoList.ArchivedTasks);

            
        }

        private string TextGroupKeyGetter(object obj)
        {
            return ((Task)obj).Category;
        }
    }
}
