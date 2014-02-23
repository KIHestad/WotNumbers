namespace WotDBUpdater.Forms
{
    partial class frmImportTank
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
            this.btnImportTanks = new System.Windows.Forms.Button();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnImportTanks
            // 
            this.btnImportTanks.Location = new System.Drawing.Point(12, 12);
            this.btnImportTanks.Name = "btnImportTanks";
            this.btnImportTanks.Size = new System.Drawing.Size(83, 23);
            this.btnImportTanks.TabIndex = 0;
            this.btnImportTanks.Text = "Import tanks";
            this.btnImportTanks.UseVisualStyleBackColor = true;
            this.btnImportTanks.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(12, 41);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(260, 212);
            this.listBoxLog.TabIndex = 1;
            // 
            // frmImportTank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.btnImportTanks);
            this.Name = "frmImportTank";
            this.Text = "frmImportTank";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImportTanks;
        private System.Windows.Forms.ListBox listBoxLog;
    }
}