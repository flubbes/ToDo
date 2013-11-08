namespace ToDo
{
    partial class FormTasks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTasks));
            this.clbTasks = new System.Windows.Forms.CheckedListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.addTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // clbTasks
            // 
            this.clbTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbTasks.FormattingEnabled = true;
            this.clbTasks.Location = new System.Drawing.Point(0, 24);
            this.clbTasks.Name = "clbTasks";
            this.clbTasks.Size = new System.Drawing.Size(637, 252);
            this.clbTasks.TabIndex = 0;
            this.clbTasks.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbTasks_ItemCheck);
            this.clbTasks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.clbTasks_KeyDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTaskToolStripMenuItem,
            this.batchAddToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(637, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // addTaskToolStripMenuItem
            // 
            this.addTaskToolStripMenuItem.Name = "addTaskToolStripMenuItem";
            this.addTaskToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.addTaskToolStripMenuItem.Text = "Add Task [Enter]";
            this.addTaskToolStripMenuItem.Click += new System.EventHandler(this.addTaskToolStripMenuItem_Click);
            // 
            // batchAddToolStripMenuItem
            // 
            this.batchAddToolStripMenuItem.Name = "batchAddToolStripMenuItem";
            this.batchAddToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.batchAddToolStripMenuItem.Text = "Batch Add";
            this.batchAddToolStripMenuItem.Click += new System.EventHandler(this.batchAddToolStripMenuItem_Click);
            // 
            // FormTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 276);
            this.Controls.Add(this.clbTasks);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormTasks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormTasks";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormTasks_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbTasks;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batchAddToolStripMenuItem;
    }
}