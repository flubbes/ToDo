namespace ToDo
{
    partial class FormSettings
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
            this.cbStartWithWindows = new System.Windows.Forms.CheckBox();
            this.gbEstimatedTimes = new System.Windows.Forms.GroupBox();
            this.cbTopMost = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbStartWithWindows
            // 
            this.cbStartWithWindows.AutoSize = true;
            this.cbStartWithWindows.Location = new System.Drawing.Point(12, 12);
            this.cbStartWithWindows.Name = "cbStartWithWindows";
            this.cbStartWithWindows.Size = new System.Drawing.Size(120, 17);
            this.cbStartWithWindows.TabIndex = 0;
            this.cbStartWithWindows.Text = "Start with windows?";
            this.cbStartWithWindows.UseVisualStyleBackColor = true;
            this.cbStartWithWindows.CheckedChanged += new System.EventHandler(this.cbStartWithWindows_CheckedChanged);
            // 
            // gbEstimatedTimes
            // 
            this.gbEstimatedTimes.Location = new System.Drawing.Point(138, 12);
            this.gbEstimatedTimes.Name = "gbEstimatedTimes";
            this.gbEstimatedTimes.Size = new System.Drawing.Size(376, 210);
            this.gbEstimatedTimes.TabIndex = 1;
            this.gbEstimatedTimes.TabStop = false;
            this.gbEstimatedTimes.Text = "Estimated time values";
            // 
            // cbTopMost
            // 
            this.cbTopMost.AutoSize = true;
            this.cbTopMost.Location = new System.Drawing.Point(12, 46);
            this.cbTopMost.Name = "cbTopMost";
            this.cbTopMost.Size = new System.Drawing.Size(98, 17);
            this.cbTopMost.TabIndex = 2;
            this.cbTopMost.Text = "Always on top?";
            this.cbTopMost.UseVisualStyleBackColor = true;
            this.cbTopMost.CheckedChanged += new System.EventHandler(this.cbTopMost_CheckedChanged);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 234);
            this.Controls.Add(this.cbTopMost);
            this.Controls.Add(this.gbEstimatedTimes);
            this.Controls.Add(this.cbStartWithWindows);
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbStartWithWindows;
        private System.Windows.Forms.GroupBox gbEstimatedTimes;
        private System.Windows.Forms.CheckBox cbTopMost;
    }
}