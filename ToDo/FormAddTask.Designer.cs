namespace ToDo
{
    partial class FormAddTask
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
            this.btnAddTask = new System.Windows.Forms.Button();
            this.tbTaskText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnAddTask
            // 
            this.btnAddTask.Location = new System.Drawing.Point(215, 38);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(75, 23);
            this.btnAddTask.TabIndex = 1;
            this.btnAddTask.Text = "Add";
            this.btnAddTask.UseVisualStyleBackColor = true;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // tbTaskText
            // 
            this.tbTaskText.Location = new System.Drawing.Point(12, 12);
            this.tbTaskText.Name = "tbTaskText";
            this.tbTaskText.Size = new System.Drawing.Size(278, 20);
            this.tbTaskText.TabIndex = 0;
            this.tbTaskText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTaskText_KeyDown);
            // 
            // FormAddTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 72);
            this.Controls.Add(this.btnAddTask);
            this.Controls.Add(this.tbTaskText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormAddTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormAddTask";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAddTask_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.TextBox tbTaskText;
    }
}