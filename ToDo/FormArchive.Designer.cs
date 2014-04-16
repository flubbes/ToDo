namespace ToDo
{
    partial class FormArchive
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.olvTasks = new BrightIdeasSoftware.ObjectListView();
            this.olvcText = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcPriority = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcDueDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcEstimatedTime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcArchivedAt = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvTasks)).BeginInit();
            this.SuspendLayout();
            // 
            // cms
            // 
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(95, 26);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // olvTasks
            // 
            this.olvTasks.AllColumns.Add(this.olvcText);
            this.olvTasks.AllColumns.Add(this.olvcPriority);
            this.olvTasks.AllColumns.Add(this.olvcDueDate);
            this.olvTasks.AllColumns.Add(this.olvcEstimatedTime);
            this.olvTasks.AllColumns.Add(this.olvcArchivedAt);
            this.olvTasks.AllowColumnReorder = true;
            this.olvTasks.BackColor = System.Drawing.SystemColors.Window;
            this.olvTasks.CheckBoxes = true;
            this.olvTasks.CheckedAspectName = "IsDone";
            this.olvTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcText,
            this.olvcPriority,
            this.olvcDueDate,
            this.olvcEstimatedTime,
            this.olvcArchivedAt});
            this.olvTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvTasks.FullRowSelect = true;
            this.olvTasks.Location = new System.Drawing.Point(0, 0);
            this.olvTasks.Name = "olvTasks";
            this.olvTasks.Size = new System.Drawing.Size(637, 276);
            this.olvTasks.TabIndex = 2;
            this.olvTasks.UseCompatibleStateImageBehavior = false;
            this.olvTasks.View = System.Windows.Forms.View.Details;
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
            // olvcArchivedAt
            // 
            this.olvcArchivedAt.AspectName = "ArchivedAt";
            this.olvcArchivedAt.CellPadding = null;
            this.olvcArchivedAt.Text = "Archived at";
            // 
            // FormArchive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 276);
            this.Controls.Add(this.olvTasks);
            this.Name = "FormArchive";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Archive";
            this.cms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvTasks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private BrightIdeasSoftware.ObjectListView olvTasks;
        private BrightIdeasSoftware.OLVColumn olvcText;
        private BrightIdeasSoftware.OLVColumn olvcPriority;
        private BrightIdeasSoftware.OLVColumn olvcDueDate;
        private BrightIdeasSoftware.OLVColumn olvcEstimatedTime;
        private BrightIdeasSoftware.OLVColumn olvcArchivedAt;
    }
}