namespace ToDo
{
    partial class FormTask
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
            this.cbCategories = new System.Windows.Forms.ComboBox();
            this.lblTextDesc = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCategoryDesc = new System.Windows.Forms.Label();
            this.nudPriority = new System.Windows.Forms.NumericUpDown();
            this.lblPriorityDesc = new System.Windows.Forms.Label();
            this.lblEstimatedTimeDesc = new System.Windows.Forms.Label();
            this.cbEstimatedTimes = new System.Windows.Forms.ComboBox();
            this.lblDueDateDesc = new System.Windows.Forms.Label();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.nudPriority)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddTask
            // 
            this.btnAddTask.Location = new System.Drawing.Point(210, 178);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(75, 23);
            this.btnAddTask.TabIndex = 5;
            this.btnAddTask.Text = "OK";
            this.btnAddTask.UseVisualStyleBackColor = true;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // tbTaskText
            // 
            this.tbTaskText.Location = new System.Drawing.Point(64, 9);
            this.tbTaskText.Name = "tbTaskText";
            this.tbTaskText.Size = new System.Drawing.Size(221, 20);
            this.tbTaskText.TabIndex = 0;
            this.tbTaskText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTaskText_KeyDown);
            // 
            // cbCategories
            // 
            this.cbCategories.FormattingEnabled = true;
            this.cbCategories.Location = new System.Drawing.Point(64, 40);
            this.cbCategories.Name = "cbCategories";
            this.cbCategories.Size = new System.Drawing.Size(121, 21);
            this.cbCategories.TabIndex = 1;
            // 
            // lblTextDesc
            // 
            this.lblTextDesc.AutoSize = true;
            this.lblTextDesc.Location = new System.Drawing.Point(6, 12);
            this.lblTextDesc.Name = "lblTextDesc";
            this.lblTextDesc.Size = new System.Drawing.Size(31, 13);
            this.lblTextDesc.TabIndex = 12;
            this.lblTextDesc.Text = "Text:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 178);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblCategoryDesc
            // 
            this.lblCategoryDesc.AutoSize = true;
            this.lblCategoryDesc.Location = new System.Drawing.Point(6, 43);
            this.lblCategoryDesc.Name = "lblCategoryDesc";
            this.lblCategoryDesc.Size = new System.Drawing.Size(52, 13);
            this.lblCategoryDesc.TabIndex = 11;
            this.lblCategoryDesc.Text = "Category:";
            // 
            // nudPriority
            // 
            this.nudPriority.Location = new System.Drawing.Point(64, 70);
            this.nudPriority.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudPriority.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.nudPriority.Name = "nudPriority";
            this.nudPriority.Size = new System.Drawing.Size(54, 20);
            this.nudPriority.TabIndex = 2;
            // 
            // lblPriorityDesc
            // 
            this.lblPriorityDesc.AutoSize = true;
            this.lblPriorityDesc.Location = new System.Drawing.Point(6, 72);
            this.lblPriorityDesc.Name = "lblPriorityDesc";
            this.lblPriorityDesc.Size = new System.Drawing.Size(41, 13);
            this.lblPriorityDesc.TabIndex = 10;
            this.lblPriorityDesc.Text = "Priority:";
            // 
            // lblEstimatedTimeDesc
            // 
            this.lblEstimatedTimeDesc.AutoSize = true;
            this.lblEstimatedTimeDesc.Location = new System.Drawing.Point(6, 103);
            this.lblEstimatedTimeDesc.Name = "lblEstimatedTimeDesc";
            this.lblEstimatedTimeDesc.Size = new System.Drawing.Size(78, 13);
            this.lblEstimatedTimeDesc.TabIndex = 9;
            this.lblEstimatedTimeDesc.Text = "Estimated time:";
            // 
            // cbEstimatedTimes
            // 
            this.cbEstimatedTimes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstimatedTimes.FormattingEnabled = true;
            this.cbEstimatedTimes.Location = new System.Drawing.Point(90, 100);
            this.cbEstimatedTimes.Name = "cbEstimatedTimes";
            this.cbEstimatedTimes.Size = new System.Drawing.Size(195, 21);
            this.cbEstimatedTimes.TabIndex = 3;
            // 
            // lblDueDateDesc
            // 
            this.lblDueDateDesc.AutoSize = true;
            this.lblDueDateDesc.Location = new System.Drawing.Point(6, 133);
            this.lblDueDateDesc.Name = "lblDueDateDesc";
            this.lblDueDateDesc.Size = new System.Drawing.Size(54, 13);
            this.lblDueDateDesc.TabIndex = 8;
            this.lblDueDateDesc.Text = "Due date:";
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.Location = new System.Drawing.Point(90, 127);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(195, 20);
            this.dtpDueDate.TabIndex = 4;
            // 
            // FormTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 213);
            this.Controls.Add(this.dtpDueDate);
            this.Controls.Add(this.lblDueDateDesc);
            this.Controls.Add(this.cbEstimatedTimes);
            this.Controls.Add(this.lblEstimatedTimeDesc);
            this.Controls.Add(this.lblPriorityDesc);
            this.Controls.Add(this.nudPriority);
            this.Controls.Add(this.lblCategoryDesc);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblTextDesc);
            this.Controls.Add(this.cbCategories);
            this.Controls.Add(this.btnAddTask);
            this.Controls.Add(this.tbTaskText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add a new Task";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAddTask_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudPriority)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.TextBox tbTaskText;
        private System.Windows.Forms.ComboBox cbCategories;
        private System.Windows.Forms.Label lblTextDesc;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCategoryDesc;
        private System.Windows.Forms.NumericUpDown nudPriority;
        private System.Windows.Forms.Label lblPriorityDesc;
        private System.Windows.Forms.Label lblEstimatedTimeDesc;
        private System.Windows.Forms.ComboBox cbEstimatedTimes;
        private System.Windows.Forms.Label lblDueDateDesc;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
    }
}