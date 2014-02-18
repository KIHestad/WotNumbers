namespace WotDBUpdater
{
    partial class frmMain
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
            this.fileSystemWatcherDossierFile = new System.IO.FileSystemWatcher();
            this.openFileDialogDossierFile = new System.Windows.Forms.OpenFileDialog();
            this.txtDossierFile = new System.Windows.Forms.TextBox();
            this.lblDossierFIle = new System.Windows.Forms.Label();
            this.btnOpenDossierFile = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherDossierFile)).BeginInit();
            this.SuspendLayout();
            // 
            // fileSystemWatcherDossierFile
            // 
            this.fileSystemWatcherDossierFile.EnableRaisingEvents = true;
            this.fileSystemWatcherDossierFile.NotifyFilter = System.IO.NotifyFilters.FileName;
            this.fileSystemWatcherDossierFile.SynchronizingObject = this;
            // 
            // openFileDialogDossierFile
            // 
            this.openFileDialogDossierFile.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // txtDossierFile
            // 
            this.txtDossierFile.Enabled = false;
            this.txtDossierFile.Location = new System.Drawing.Point(109, 14);
            this.txtDossierFile.Multiline = true;
            this.txtDossierFile.Name = "txtDossierFile";
            this.txtDossierFile.Size = new System.Drawing.Size(558, 52);
            this.txtDossierFile.TabIndex = 0;
            // 
            // lblDossierFIle
            // 
            this.lblDossierFIle.AutoSize = true;
            this.lblDossierFIle.Location = new System.Drawing.Point(13, 17);
            this.lblDossierFIle.Name = "lblDossierFIle";
            this.lblDossierFIle.Size = new System.Drawing.Size(90, 13);
            this.lblDossierFIle.TabIndex = 1;
            this.lblDossierFIle.Text = "WOT Dossier File";
            // 
            // btnOpenDossierFile
            // 
            this.btnOpenDossierFile.Location = new System.Drawing.Point(16, 42);
            this.btnOpenDossierFile.Name = "btnOpenDossierFile";
            this.btnOpenDossierFile.Size = new System.Drawing.Size(87, 24);
            this.btnOpenDossierFile.TabIndex = 2;
            this.btnOpenDossierFile.Text = "Select file";
            this.btnOpenDossierFile.UseVisualStyleBackColor = true;
            this.btnOpenDossierFile.Click += new System.EventHandler(this.btnOpenDossierFile_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(16, 91);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(87, 82);
            this.btnStartStop.TabIndex = 3;
            this.btnStartStop.Text = "Start / Stop";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(113, 91);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(553, 82);
            this.listBoxLog.TabIndex = 4;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 238);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.btnOpenDossierFile);
            this.Controls.Add(this.lblDossierFIle);
            this.Controls.Add(this.txtDossierFile);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "WotDBUpdater";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherDossierFile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.FileSystemWatcher fileSystemWatcherDossierFile;
        private System.Windows.Forms.OpenFileDialog openFileDialogDossierFile;
        private System.Windows.Forms.Button btnOpenDossierFile;
        private System.Windows.Forms.Label lblDossierFIle;
        private System.Windows.Forms.TextBox txtDossierFile;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.ListBox listBoxLog;
    }
}

