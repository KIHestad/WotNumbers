namespace WinApp.Forms.Settings
{
    partial class AppSettingsMain
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppSettingsMain));
            this.folderBrowserDialogDossier = new System.Windows.Forms.FolderBrowserDialog();
            this.txtDownloadFilePath = new BadTextBox();
            this.chkCreateDownloadSubFolders = new BadCheckBox();
            this.badLabel3 = new BadLabel();
            this.btnSelectDownloadFilePath = new BadButton();
            this.txtDossierFilePath = new BadTextBox();
            this.badLabel1 = new BadLabel();
            this.chkShowDBError = new BadCheckBox();
            this.badLabel2 = new BadLabel();
            this.btnCancel = new BadButton();
            this.cboSelectPlayer = new BadDropDownBox();
            this.btnSelectDossierFilePath = new BadButton();
            this.btnDbSetting = new BadButton();
            this.btnSave = new BadButton();
            this.lblDbSettings = new BadLabel();
            this.badGroupBox1 = new BadGroupBox();
            this.badGroupBox2 = new BadGroupBox();
            this.SuspendLayout();
            // 
            // txtDownloadFilePath
            // 
            this.txtDownloadFilePath.HasFocus = false;
            this.txtDownloadFilePath.Image = null;
            this.txtDownloadFilePath.Location = new System.Drawing.Point(17, 100);
            this.txtDownloadFilePath.MultilineAllow = false;
            this.txtDownloadFilePath.Name = "txtDownloadFilePath";
            this.txtDownloadFilePath.PasswordChar = '\0';
            this.txtDownloadFilePath.ReadOnly = false;
            this.txtDownloadFilePath.Size = new System.Drawing.Size(383, 23);
            this.txtDownloadFilePath.TabIndex = 32;
            this.txtDownloadFilePath.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDownloadFilePath.ToolTipText = "";
            this.txtDownloadFilePath.TextChanged += new System.EventHandler(this.txtDownloadFilePath_TextChanged);
            // 
            // chkCreateDownloadSubFolders
            // 
            this.chkCreateDownloadSubFolders.BackColor = System.Drawing.Color.Transparent;
            this.chkCreateDownloadSubFolders.Checked = false;
            this.chkCreateDownloadSubFolders.Image = ((System.Drawing.Image)(resources.GetObject("chkCreateDownloadSubFolders.Image")));
            this.chkCreateDownloadSubFolders.Location = new System.Drawing.Point(125, 78);
            this.chkCreateDownloadSubFolders.Name = "chkCreateDownloadSubFolders";
            this.chkCreateDownloadSubFolders.Size = new System.Drawing.Size(263, 23);
            this.chkCreateDownloadSubFolders.TabIndex = 35;
            this.chkCreateDownloadSubFolders.Text = "Create Subfolder per version";
            this.chkCreateDownloadSubFolders.Click += new System.EventHandler(this.chkCreateDownloadSubFolders_Click);
            // 
            // badLabel3
            // 
            this.badLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel3.Dimmed = false;
            this.badLabel3.Image = null;
            this.badLabel3.Location = new System.Drawing.Point(17, 77);
            this.badLabel3.Name = "badLabel3";
            this.badLabel3.Size = new System.Drawing.Size(113, 23);
            this.badLabel3.TabIndex = 34;
            this.badLabel3.Text = "Download File Path:";
            this.badLabel3.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // btnSelectDownloadFilePath
            // 
            this.btnSelectDownloadFilePath.BlackButton = false;
            this.btnSelectDownloadFilePath.Checked = false;
            this.btnSelectDownloadFilePath.Image = null;
            this.btnSelectDownloadFilePath.Location = new System.Drawing.Point(406, 100);
            this.btnSelectDownloadFilePath.Name = "btnSelectDownloadFilePath";
            this.btnSelectDownloadFilePath.Size = new System.Drawing.Size(22, 23);
            this.btnSelectDownloadFilePath.TabIndex = 33;
            this.btnSelectDownloadFilePath.Text = "...";
            this.btnSelectDownloadFilePath.ToolTipText = "";
            this.btnSelectDownloadFilePath.Click += new System.EventHandler(this.btnSelectDownloadFilePath_Click);
            // 
            // txtDossierFilePath
            // 
            this.txtDossierFilePath.HasFocus = false;
            this.txtDossierFilePath.Image = null;
            this.txtDossierFilePath.Location = new System.Drawing.Point(17, 44);
            this.txtDossierFilePath.MultilineAllow = false;
            this.txtDossierFilePath.Name = "txtDossierFilePath";
            this.txtDossierFilePath.PasswordChar = '\0';
            this.txtDossierFilePath.ReadOnly = false;
            this.txtDossierFilePath.Size = new System.Drawing.Size(383, 23);
            this.txtDossierFilePath.TabIndex = 20;
            this.txtDossierFilePath.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDossierFilePath.ToolTipText = "";
            this.txtDossierFilePath.TextChanged += new System.EventHandler(this.txtDossierFilePath_TextChanged);
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(17, 21);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(126, 23);
            this.badLabel1.TabIndex = 31;
            this.badLabel1.Text = "Dossier File Path:";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // chkShowDBError
            // 
            this.chkShowDBError.BackColor = System.Drawing.Color.Transparent;
            this.chkShowDBError.Checked = false;
            this.chkShowDBError.Image = ((System.Drawing.Image)(resources.GetObject("chkShowDBError.Image")));
            this.chkShowDBError.Location = new System.Drawing.Point(-4, 271);
            this.chkShowDBError.Name = "chkShowDBError";
            this.chkShowDBError.Size = new System.Drawing.Size(277, 23);
            this.chkShowDBError.TabIndex = 30;
            this.chkShowDBError.Text = "Debug mode (show all errors / extended logging)";
            this.chkShowDBError.Click += new System.EventHandler(this.chkShowDBError_Click);
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(17, 215);
            this.badLabel2.Name = "badLabel2";
            this.badLabel2.Size = new System.Drawing.Size(250, 23);
            this.badLabel2.TabIndex = 29;
            this.badLabel2.Text = "Select player:";
            this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.BlackButton = false;
            this.btnCancel.Checked = false;
            this.btnCancel.Image = null;
            this.btnCancel.Location = new System.Drawing.Point(375, 271);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 23);
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTipText = "";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cboSelectPlayer
            // 
            this.cboSelectPlayer.Image = null;
            this.cboSelectPlayer.Location = new System.Drawing.Point(286, 215);
            this.cboSelectPlayer.Name = "cboSelectPlayer";
            this.cboSelectPlayer.Size = new System.Drawing.Size(142, 23);
            this.cboSelectPlayer.TabIndex = 26;
            this.cboSelectPlayer.TextChanged += new System.EventHandler(this.cboSelectPlayer_TextChanged);
            this.cboSelectPlayer.Click += new System.EventHandler(this.cboSelectPlayer_Click);
            // 
            // btnSelectDossierFilePath
            // 
            this.btnSelectDossierFilePath.BlackButton = false;
            this.btnSelectDossierFilePath.Checked = false;
            this.btnSelectDossierFilePath.Image = null;
            this.btnSelectDossierFilePath.Location = new System.Drawing.Point(406, 44);
            this.btnSelectDossierFilePath.Name = "btnSelectDossierFilePath";
            this.btnSelectDossierFilePath.Size = new System.Drawing.Size(22, 23);
            this.btnSelectDossierFilePath.TabIndex = 21;
            this.btnSelectDossierFilePath.Text = "...";
            this.btnSelectDossierFilePath.ToolTipText = "";
            this.btnSelectDossierFilePath.Click += new System.EventHandler(this.btnSelectDossierFilePath_Click);
            // 
            // btnDbSetting
            // 
            this.btnDbSetting.BlackButton = false;
            this.btnDbSetting.Checked = false;
            this.btnDbSetting.Image = null;
            this.btnDbSetting.Location = new System.Drawing.Point(286, 182);
            this.btnDbSetting.Name = "btnDbSetting";
            this.btnDbSetting.Size = new System.Drawing.Size(142, 23);
            this.btnDbSetting.TabIndex = 24;
            this.btnDbSetting.Text = "Database Settings";
            this.btnDbSetting.ToolTipText = "";
            this.btnDbSetting.Click += new System.EventHandler(this.btnDbSetting_Click);
            // 
            // btnSave
            // 
            this.btnSave.BlackButton = false;
            this.btnSave.Checked = false;
            this.btnSave.Image = null;
            this.btnSave.Location = new System.Drawing.Point(298, 271);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 23);
            this.btnSave.TabIndex = 27;
            this.btnSave.Text = "Save";
            this.btnSave.ToolTipText = "";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // lblDbSettings
            // 
            this.lblDbSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblDbSettings.Dimmed = false;
            this.lblDbSettings.Image = null;
            this.lblDbSettings.Location = new System.Drawing.Point(17, 175);
            this.lblDbSettings.Name = "lblDbSettings";
            this.lblDbSettings.Size = new System.Drawing.Size(263, 36);
            this.lblDbSettings.TabIndex = 23;
            this.lblDbSettings.TabStop = false;
            this.lblDbSettings.Text = "Database:";
            this.lblDbSettings.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(1, 1);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(445, 137);
            this.badGroupBox1.TabIndex = 19;
            this.badGroupBox1.TabStop = false;
            this.badGroupBox1.Text = "File Paths";
            // 
            // badGroupBox2
            // 
            this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox2.Image = null;
            this.badGroupBox2.Location = new System.Drawing.Point(1, 154);
            this.badGroupBox2.Name = "badGroupBox2";
            this.badGroupBox2.Size = new System.Drawing.Size(445, 100);
            this.badGroupBox2.TabIndex = 22;
            this.badGroupBox2.TabStop = false;
            this.badGroupBox2.Text = "Database and Player";
            // 
            // AppSettingsMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.txtDownloadFilePath);
            this.Controls.Add(this.chkCreateDownloadSubFolders);
            this.Controls.Add(this.badLabel3);
            this.Controls.Add(this.btnSelectDownloadFilePath);
            this.Controls.Add(this.txtDossierFilePath);
            this.Controls.Add(this.badLabel1);
            this.Controls.Add(this.chkShowDBError);
            this.Controls.Add(this.badLabel2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cboSelectPlayer);
            this.Controls.Add(this.btnSelectDossierFilePath);
            this.Controls.Add(this.btnDbSetting);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblDbSettings);
            this.Controls.Add(this.badGroupBox1);
            this.Controls.Add(this.badGroupBox2);
            this.Name = "AppSettingsMain";
            this.Size = new System.Drawing.Size(460, 307);
            this.Load += new System.EventHandler(this.AppSettingsMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BadCheckBox chkShowDBError;
        private BadLabel badLabel2;
        private BadButton btnCancel;
        private BadDropDownBox cboSelectPlayer;
        private BadButton btnSelectDossierFilePath;
        private BadTextBox txtDossierFilePath;
        private BadButton btnDbSetting;
        private BadButton btnSave;
        private BadLabel lblDbSettings;
        private BadGroupBox badGroupBox1;
        private BadGroupBox badGroupBox2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogDossier;
        private BadLabel badLabel1;
        private BadTextBox txtDownloadFilePath;
        private BadLabel badLabel3;
        private BadButton btnSelectDownloadFilePath;
        private BadCheckBox chkCreateDownloadSubFolders;
    }
}
