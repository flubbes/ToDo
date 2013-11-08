namespace ToDo
{
    partial class FormMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.lvCategories = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tspbActionProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslCurrentAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTodoListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showChangesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvCategories
            // 
            this.lvCategories.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvCategories.ContextMenuStrip = this.cmsMenu;
            this.lvCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCategories.FullRowSelect = true;
            this.lvCategories.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvCategories.Location = new System.Drawing.Point(0, 24);
            this.lvCategories.MultiSelect = false;
            this.lvCategories.Name = "lvCategories";
            this.lvCategories.Size = new System.Drawing.Size(574, 245);
            this.lvCategories.TabIndex = 1;
            this.lvCategories.UseCompatibleStateImageBehavior = false;
            this.lvCategories.View = System.Windows.Forms.View.Details;
            this.lvCategories.SelectedIndexChanged += new System.EventHandler(this.lvCategories_SelectedIndexChanged);
            this.lvCategories.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvCategories_KeyDown);
            this.lvCategories.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvCategories_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Tasks";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Progress";
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(153, 48);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbActionProgress,
            this.tsslCurrentAction});
            this.statusStrip1.Location = new System.Drawing.Point(0, 269);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(574, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tspbActionProgress
            // 
            this.tspbActionProgress.Name = "tspbActionProgress";
            this.tspbActionProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // tsslCurrentAction
            // 
            this.tsslCurrentAction.Name = "tsslCurrentAction";
            this.tsslCurrentAction.Size = new System.Drawing.Size(82, 17);
            this.tsslCurrentAction.Text = "CurrentAction";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.addCategoryToolStripMenuItem,
            this.showChangesToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(574, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadTodoListToolStripMenuItem,
            this.recentFilesToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadTodoListToolStripMenuItem
            // 
            this.loadTodoListToolStripMenuItem.Name = "loadTodoListToolStripMenuItem";
            this.loadTodoListToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.loadTodoListToolStripMenuItem.Text = "Load TodoList";
            this.loadTodoListToolStripMenuItem.Click += new System.EventHandler(this.loadTodoListToolStripMenuItem_Click);
            // 
            // recentFilesToolStripMenuItem
            // 
            this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.recentFilesToolStripMenuItem.Text = "Recent Files";
            // 
            // addCategoryToolStripMenuItem
            // 
            this.addCategoryToolStripMenuItem.Name = "addCategoryToolStripMenuItem";
            this.addCategoryToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.addCategoryToolStripMenuItem.Text = "Add Category";
            this.addCategoryToolStripMenuItem.Click += new System.EventHandler(this.addCategoryToolStripMenuItem_Click);
            // 
            // showChangesToolStripMenuItem1
            // 
            this.showChangesToolStripMenuItem1.Name = "showChangesToolStripMenuItem1";
            this.showChangesToolStripMenuItem1.Size = new System.Drawing.Size(95, 20);
            this.showChangesToolStripMenuItem1.Text = "Show changes";
            this.showChangesToolStripMenuItem1.Click += new System.EventHandler(this.showChangesToolStripMenuItem1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 291);
            this.Controls.Add(this.lvCategories);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "ToDo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.cmsMenu.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvCategories;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar tspbActionProgress;
        private System.Windows.Forms.ToolStripStatusLabel tsslCurrentAction;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTodoListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showChangesToolStripMenuItem1;

    }
}

