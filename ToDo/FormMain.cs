﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ToDo.Lib;

namespace ToDo
{
    public partial class FormMain : Form
    {
        FormTasks ft;
        string defaultDB = "db.xml";
        string loadedDB;

        public FormMain()
        {
            InitializeComponent();
            CategoryManager.Init();
            try
            {
                CategoryManager.FromXml(defaultDB);
                loadedDB = defaultDB;
            }
            catch (XmlException ex)
            {
                MessageBox.Show("The old database is corrupted!");
            }
            UpdateList();
            ft = new FormTasks();
            CategoryManager.ListChanged += CategoryManager_ListChanged;
        }

        void CategoryManager_ListChanged(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void addCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddCategory f = new FormAddCategory();
            f.ShowDialog();
            if (f.NewCat != null)
            {
                CategoryManager.AddCategory(f.NewCat);
                UpdateList();
            }
        }

        private void UpdateList()
        {
            lvCategories.Items.Clear();
            foreach (Category c in CategoryManager.Categories)
            {
                ListViewItem i = new ListViewItem(c.Name);
                i.SubItems.Add(c.TaskCount.ToString());
                i.SubItems.Add(c.CategoryPercentage.ToString());
                lvCategories.Items.Add(i);
            }
        }

        private void lvCategories_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvCategories_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ft.IsClosed)
            {
                ft = new FormTasks();
                ft.UpdateTasks(CategoryManager.Categories[lvCategories.SelectedIndices[0]]);
                ft.Show();
            }
            else
            {
                ft.UpdateTasks(CategoryManager.Categories[lvCategories.SelectedIndices[0]]);
                ft.Show();
            }
            ft.TopMost = true;
            ft.TopMost = false;
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            CategoryManager.ToXml(loadedDB);
        }

        private void DeleteSelection()
        {
            if (lvCategories.SelectedIndices.Count > 0)
            {
                if (lvCategories.SelectedIndices[0] != -1)
                {
                    CategoryManager.Categories.RemoveAt(lvCategories.SelectedIndices[0]);
                }
            }
        }

        private void lvCategories_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelection();
                CategoryManager.OnListChanged(this, e);
            }
        }

        private void loadTodoListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Xml-Files|*.xml";
            ofd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    CategoryManager.FromXml(ofd.FileName);
                    loadedDB = Path.GetFullPath(ofd.FileName);
                }
                catch (XmlException ex)
                {
                    MessageBox.Show("This database is corrupted!");
                }
                UpdateList();
            }
        }
    }
}
