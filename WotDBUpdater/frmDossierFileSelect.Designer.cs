namespace WotDBUpdater
{
    partial class frmDossierFileSelect
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
            this.btnOpenDossierFile = new System.Windows.Forms.Button();
            this.lblDossierFIle = new System.Windows.Forms.Label();
            this.txtDossierFilePath = new System.Windows.Forms.TextBox();
            this.openFileDialogDossierFile = new System.Windows.Forms.OpenFileDialog();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenDossierFile
            // 
            this.btnOpenDossierFile.Location = new System.Drawing.Point(332, 69);
            this.btnOpenDossierFile.Name = "btnOpenDossierFile";
            this.btnOpenDossierFile.Size = new System.Drawing.Size(110, 25);
            this.btnOpenDossierFile.TabIndex = 5;
            this.btnOpenDossierFile.Text = "Select dossier file";
            this.btnOpenDossierFile.UseVisualStyleBackColor = true;
            this.btnOpenDossierFile.Click += new System.EventHandler(this.btnOpenDossierFile_Click);
            // 
            // lblDossierFIle
            // 
            this.lblDossierFIle.AutoSize = true;
            this.lblDossierFIle.Location = new System.Drawing.Point(12, 9);
            this.lblDossierFIle.Name = "lblDossierFIle";
            this.lblDossierFIle.Size = new System.Drawing.Size(76, 13);
            this.lblDossierFIle.TabIndex = 4;
            this.lblDossierFIle.Text = "Selected path:";
            // 
            // txtDossierFilePath
            // 
            this.txtDossierFilePath.Location = new System.Drawing.Point(18, 27);
            this.txtDossierFilePath.Multiline = true;
            this.txtDossierFilePath.Name = "txtDossierFilePath";
            this.txtDossierFilePath.Size = new System.Drawing.Size(497, 36);
            this.txtDossierFilePath.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(448, 69);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(67, 25);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmDossierFileSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 102);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtDossierFilePath);
            this.Controls.Add(this.btnOpenDossierFile);
            this.Controls.Add(this.lblDossierFIle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDossierFileSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Dossier Filepath";
            this.Load += new System.EventHandler(this.frmDossierFileSelect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenDossierFile;
        private System.Windows.Forms.Label lblDossierFIle;
        private System.Windows.Forms.OpenFileDialog openFileDialogDossierFile;
        private System.Windows.Forms.TextBox txtDossierFilePath;
        private System.Windows.Forms.Button btnSave;
    }
}