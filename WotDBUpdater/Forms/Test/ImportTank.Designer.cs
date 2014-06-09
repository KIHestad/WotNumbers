namespace WinApp.Forms
{
    partial class ImportTank
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
            this.btnUpdateWN8 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnImportTanks
            // 
            this.btnImportTanks.Location = new System.Drawing.Point(284, 317);
            this.btnImportTanks.Name = "btnImportTanks";
            this.btnImportTanks.Size = new System.Drawing.Size(83, 23);
            this.btnImportTanks.TabIndex = 0;
            this.btnImportTanks.Text = "Import tanks";
            this.btnImportTanks.UseVisualStyleBackColor = true;
            this.btnImportTanks.Click += new System.EventHandler(this.btnImportTanks_Click);
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(12, 12);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(355, 290);
            this.listBoxLog.TabIndex = 1;
            // 
            // btnUpdateWN8
            // 
            this.btnUpdateWN8.Location = new System.Drawing.Point(161, 317);
            this.btnUpdateWN8.Name = "btnUpdateWN8";
            this.btnUpdateWN8.Size = new System.Drawing.Size(117, 23);
            this.btnUpdateWN8.TabIndex = 2;
            this.btnUpdateWN8.Text = "Update WN8 values";
            this.btnUpdateWN8.UseVisualStyleBackColor = true;
            this.btnUpdateWN8.Click += new System.EventHandler(this.btnUpdateWN8_Click);
            // 
            // frmImportTank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 352);
            this.Controls.Add(this.btnUpdateWN8);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.btnImportTanks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImportTank";
            this.ShowInTaskbar = false;
            this.Text = "Import Tank";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImportTanks;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Button btnUpdateWN8;
    }
}