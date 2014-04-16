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
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.olvTasks = new BrightIdeasSoftware.ObjectListView();
            this.olvcText = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcPriority = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcDueDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcEstimatedTime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tspbActionProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslCurrentAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTodoListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showChangesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showChangesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archiveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvTasks)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.archiveToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(115, 70);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.archiveToolStripMenuItem.Text = "Archive";
            this.archiveToolStripMenuItem.Click += new System.EventHandler(this.archiveToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // olvTasks
            // 
            this.olvTasks.AllColumns.Add(this.olvcText);
            this.olvTasks.AllColumns.Add(this.olvcPriority);
            this.olvTasks.AllColumns.Add(this.olvcDueDate);
            this.olvTasks.AllColumns.Add(this.olvcEstimatedTime);
            this.olvTasks.AllowColumnReorder = true;
            this.olvTasks.BackColor = System.Drawing.SystemColors.Window;
            this.olvTasks.CheckBoxes = true;
            this.olvTasks.CheckedAspectName = "IsDone";
            this.olvTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcText,
            this.olvcPriority,
            this.olvcDueDate,
            this.olvcEstimatedTime});
            this.olvTasks.ContextMenuStrip = this.cmsMenu;
            this.olvTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvTasks.FullRowSelect = true;
            this.olvTasks.Location = new System.Drawing.Point(0, 24);
            this.olvTasks.Name = "olvTasks";
            this.olvTasks.Size = new System.Drawing.Size(573, 248);
            this.olvTasks.TabIndex = 1;
            this.olvTasks.UseCompatibleStateImageBehavior = false;
            this.olvTasks.View = System.Windows.Forms.View.Details;
            this.olvTasks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.olvTasks_KeyDown);
            // 
            // olvcText
            // 
            this.olvcText.AspectName = "Text";
            this.olvcText.CellPadding = null;
            this.olvcText.Text = "Text";
            // 
            // olvcPriority
            // 
            this.olvcPriority.AspectName = "Priority";
            this.olvcPriority.CellPadding = null;
            this.olvcPriority.Text = "Priority";
            // 
            // olvcDueDate
            // 
            this.olvcDueDate.AspectName = "DueDate";
            this.olvcDueDate.CellPadding = null;
            this.olvcDueDate.Text = "Due date";
            // 
            // olvcEstimatedTime
            // 
            this.olvcEstimatedTime.AspectName = "EstimatedTime";
            this.olvcEstimatedTime.CellPadding = null;
            this.olvcEstimatedTime.Text = "Estimated time";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbActionProgress,
            this.tsslCurrentAction});
            this.statusStrip1.Location = new System.Drawing.Point(0, 272);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(573, 22);
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
            this.addTaskToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.showChangesToolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(573, 24);
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
            // addTaskToolStripMenuItem
            // 
            this.addTaskToolStripMenuItem.Name = "addTaskToolStripMenuItem";
            this.addTaskToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.addTaskToolStripMenuItem.Text = "Add task [Return]";
            this.addTaskToolStripMenuItem.Click += new System.EventHandler(this.addTaskToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // showChangesToolStripMenuItem1
            // 
            this.showChangesToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showChangesToolStripMenuItem,
            this.archiveToolStripMenuItem1});
            this.showChangesToolStripMenuItem1.Name = "showChangesToolStripMenuItem1";
            this.showChangesToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.showChangesToolStripMenuItem1.Text = "View";
            // 
            // showChangesToolStripMenuItem
            // 
            this.showChangesToolStripMenuItem.Name = "showChangesToolStripMenuItem";
            this.showChangesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showChangesToolStripMenuItem.Text = "Changes";
            this.showChangesToolStripMenuItem.Click += new System.EventHandler(this.showChangesToolStripMenuItem_Click);
            // 
            // archiveToolStripMenuItem1
            // 
            this.archiveToolStripMenuItem1.Name = "archiveToolStripMenuItem1";
            this.archiveToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.archiveToolStripMenuItem1.Text = "Archive";
            this.archiveToolStripMenuItem1.Click += new System.EventHandler(this.archiveToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 294);
            this.Controls.Add(this.olvTasks);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(235, 250);
            this.Name = "FormMain";
            this.Text = "ToDo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.cmsMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvTasks)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar tspbActionProgress;
        private System.Windows.Forms.ToolStripStatusLabel tsslCurrentAction;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTodoListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showChangesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private BrightIdeasSoftware.ObjectListView olvTasks;
        private BrightIdeasSoftware.OLVColumn olvcText;
        private System.Windows.Forms.ToolStripMenuItem addTaskToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn olvcPriority;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn olvcDueDate;
        private BrightIdeasSoftware.OLVColumn olvcEstimatedTime;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showChangesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem1;

    }
}

