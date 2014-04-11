﻿namespace WotDBUpdater.Forms.File
{
    partial class ApplicationSetting
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
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnDatabaseSettings = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.cboDatabaseType = new System.Windows.Forms.ComboBox();
			this.btnRemovePlayer = new System.Windows.Forms.Button();
			this.btnAddPlayer = new System.Windows.Forms.Button();
			this.cboPlayer = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOpenDossierFile
			// 
			this.btnOpenDossierFile.Location = new System.Drawing.Point(331, 172);
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
			this.lblDossierFIle.Location = new System.Drawing.Point(15, 114);
			this.lblDossierFIle.Name = "lblDossierFIle";
			this.lblDossierFIle.Size = new System.Drawing.Size(69, 13);
			this.lblDossierFIle.TabIndex = 4;
			this.lblDossierFIle.Text = "Dossier path:";
			// 
			// txtDossierFilePath
			// 
			this.txtDossierFilePath.Location = new System.Drawing.Point(18, 130);
			this.txtDossierFilePath.Multiline = true;
			this.txtDossierFilePath.Name = "txtDossierFilePath";
			this.txtDossierFilePath.Size = new System.Drawing.Size(423, 36);
			this.txtDossierFilePath.TabIndex = 6;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(401, 257);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(67, 25);
			this.btnSave.TabIndex = 7;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 65);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Player name:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnDatabaseSettings);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.cboDatabaseType);
			this.groupBox1.Controls.Add(this.btnRemovePlayer);
			this.groupBox1.Controls.Add(this.btnAddPlayer);
			this.groupBox1.Controls.Add(this.cboPlayer);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtDossierFilePath);
			this.groupBox1.Controls.Add(this.btnOpenDossierFile);
			this.groupBox1.Controls.Add(this.lblDossierFIle);
			this.groupBox1.Location = new System.Drawing.Point(12, 27);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(456, 214);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Settings";
			// 
			// btnDatabaseSettings
			// 
			this.btnDatabaseSettings.Location = new System.Drawing.Point(261, 21);
			this.btnDatabaseSettings.Name = "btnDatabaseSettings";
			this.btnDatabaseSettings.Size = new System.Drawing.Size(87, 25);
			this.btnDatabaseSettings.TabIndex = 15;
			this.btnDatabaseSettings.Text = "Settings";
			this.btnDatabaseSettings.UseVisualStyleBackColor = true;
			this.btnDatabaseSettings.Click += new System.EventHandler(this.btnDatabaseSettings_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "Database type:";
			// 
			// cboDatabaseType
			// 
			this.cboDatabaseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDatabaseType.FormattingEnabled = true;
			this.cboDatabaseType.Items.AddRange(new object[] {
            "MS SQL Server",
            "SQLite"});
			this.cboDatabaseType.Location = new System.Drawing.Point(109, 23);
			this.cboDatabaseType.Name = "cboDatabaseType";
			this.cboDatabaseType.Size = new System.Drawing.Size(146, 21);
			this.cboDatabaseType.TabIndex = 13;
			this.cboDatabaseType.SelectedIndexChanged += new System.EventHandler(this.cboDatabaseType_SelectedIndexChanged);
			// 
			// btnRemovePlayer
			// 
			this.btnRemovePlayer.Location = new System.Drawing.Point(354, 80);
			this.btnRemovePlayer.Name = "btnRemovePlayer";
			this.btnRemovePlayer.Size = new System.Drawing.Size(87, 23);
			this.btnRemovePlayer.TabIndex = 12;
			this.btnRemovePlayer.Text = "Remove player";
			this.btnRemovePlayer.UseVisualStyleBackColor = true;
			this.btnRemovePlayer.Click += new System.EventHandler(this.btnRemovePlayer_Click);
			// 
			// btnAddPlayer
			// 
			this.btnAddPlayer.Location = new System.Drawing.Point(261, 80);
			this.btnAddPlayer.Name = "btnAddPlayer";
			this.btnAddPlayer.Size = new System.Drawing.Size(87, 23);
			this.btnAddPlayer.TabIndex = 11;
			this.btnAddPlayer.Text = "Add player";
			this.btnAddPlayer.UseVisualStyleBackColor = true;
			this.btnAddPlayer.Click += new System.EventHandler(this.btnAddPlayer_Click);
			// 
			// cboPlayer
			// 
			this.cboPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPlayer.FormattingEnabled = true;
			this.cboPlayer.Location = new System.Drawing.Point(18, 81);
			this.cboPlayer.Name = "cboPlayer";
			this.cboPlayer.Size = new System.Drawing.Size(237, 21);
			this.cboPlayer.TabIndex = 10;
			// 
			// ApplicationSetting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(487, 311);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ApplicationSetting";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Application settings";
			this.Load += new System.EventHandler(this.frmDossierFileSelect_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenDossierFile;
        private System.Windows.Forms.Label lblDossierFIle;
        private System.Windows.Forms.OpenFileDialog openFileDialogDossierFile;
        private System.Windows.Forms.TextBox txtDossierFilePath;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboPlayer;
        private System.Windows.Forms.Button btnAddPlayer;
        private System.Windows.Forms.Button btnRemovePlayer;
		private System.Windows.Forms.Button btnDatabaseSettings;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboDatabaseType;
    }
}