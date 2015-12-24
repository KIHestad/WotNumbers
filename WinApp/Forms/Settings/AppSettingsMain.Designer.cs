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
            this.chkShowDBError = new BadCheckBox();
            this.badLabel2 = new BadLabel();
            this.btnCancel = new BadButton();
            this.cboSelectPlayer = new BadDropDownBox();
            this.btnSelectDossierFilePath = new BadButton();
            this.txtDossierFilePath = new BadTextBox();
            this.btnDbSetting = new BadButton();
            this.btnSave = new BadButton();
            this.lblDbSettings = new BadLabel();
            this.badGroupBox1 = new BadGroupBox();
            this.badGroupBox2 = new BadGroupBox();
            this.badGroupBox3 = new BadGroupBox();
            this.SuspendLayout();
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
            this.cboSelectPlayer.Location = new System.Drawing.Point(273, 215);
            this.cboSelectPlayer.Name = "cboSelectPlayer";
            this.cboSelectPlayer.Size = new System.Drawing.Size(155, 23);
            this.cboSelectPlayer.TabIndex = 26;
            this.cboSelectPlayer.TextChanged += new System.EventHandler(this.cboSelectPlayer_TextChanged);
            this.cboSelectPlayer.Click += new System.EventHandler(this.cboSelectPlayer_Click);
            // 
            // btnSelectDossierFilePath
            // 
            this.btnSelectDossierFilePath.BlackButton = false;
            this.btnSelectDossierFilePath.Checked = false;
            this.btnSelectDossierFilePath.Image = null;
            this.btnSelectDossierFilePath.Location = new System.Drawing.Point(340, 47);
            this.btnSelectDossierFilePath.Name = "btnSelectDossierFilePath";
            this.btnSelectDossierFilePath.Size = new System.Drawing.Size(88, 23);
            this.btnSelectDossierFilePath.TabIndex = 21;
            this.btnSelectDossierFilePath.Text = "Change Path";
            this.btnSelectDossierFilePath.ToolTipText = "";
            this.btnSelectDossierFilePath.Click += new System.EventHandler(this.btnSelectDossierFilePath_Click);
            // 
            // txtDossierFilePath
            // 
            this.txtDossierFilePath.HasFocus = false;
            this.txtDossierFilePath.Image = null;
            this.txtDossierFilePath.Location = new System.Drawing.Point(17, 25);
            this.txtDossierFilePath.MultilineAllow = true;
            this.txtDossierFilePath.Name = "txtDossierFilePath";
            this.txtDossierFilePath.PasswordChar = '\0';
            this.txtDossierFilePath.ReadOnly = false;
            this.txtDossierFilePath.Size = new System.Drawing.Size(314, 45);
            this.txtDossierFilePath.TabIndex = 20;
            this.txtDossierFilePath.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDossierFilePath.ToolTipText = "";
            this.txtDossierFilePath.TextChanged += new System.EventHandler(this.txtDossierFilePath_TextChanged);
            // 
            // btnDbSetting
            // 
            this.btnDbSetting.BlackButton = false;
            this.btnDbSetting.Checked = false;
            this.btnDbSetting.Image = null;
            this.btnDbSetting.Location = new System.Drawing.Point(340, 138);
            this.btnDbSetting.Name = "btnDbSetting";
            this.btnDbSetting.Size = new System.Drawing.Size(88, 23);
            this.btnDbSetting.TabIndex = 24;
            this.btnDbSetting.Text = "Settings";
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
            this.lblDbSettings.Location = new System.Drawing.Point(17, 125);
            this.lblDbSettings.Name = "lblDbSettings";
            this.lblDbSettings.Size = new System.Drawing.Size(317, 36);
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
            this.badGroupBox1.Size = new System.Drawing.Size(445, 86);
            this.badGroupBox1.TabIndex = 19;
            this.badGroupBox1.TabStop = false;
            this.badGroupBox1.Text = "Dossier File Path";
            // 
            // badGroupBox2
            // 
            this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox2.Image = null;
            this.badGroupBox2.Location = new System.Drawing.Point(1, 105);
            this.badGroupBox2.Name = "badGroupBox2";
            this.badGroupBox2.Size = new System.Drawing.Size(445, 71);
            this.badGroupBox2.TabIndex = 22;
            this.badGroupBox2.TabStop = false;
            this.badGroupBox2.Text = "Database";
            // 
            // badGroupBox3
            // 
            this.badGroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox3.Image = null;
            this.badGroupBox3.Location = new System.Drawing.Point(1, 192);
            this.badGroupBox3.Name = "badGroupBox3";
            this.badGroupBox3.Size = new System.Drawing.Size(445, 62);
            this.badGroupBox3.TabIndex = 25;
            this.badGroupBox3.TabStop = false;
            this.badGroupBox3.Text = "Player";
            // 
            // AppSettingsMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.chkShowDBError);
            this.Controls.Add(this.badLabel2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cboSelectPlayer);
            this.Controls.Add(this.btnSelectDossierFilePath);
            this.Controls.Add(this.txtDossierFilePath);
            this.Controls.Add(this.btnDbSetting);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblDbSettings);
            this.Controls.Add(this.badGroupBox1);
            this.Controls.Add(this.badGroupBox2);
            this.Controls.Add(this.badGroupBox3);
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
        private BadGroupBox badGroupBox3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogDossier;
    }
}
