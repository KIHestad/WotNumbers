namespace WinApp.Forms
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
            BadThemeContainerControl.MainAreaClass mainAreaClass1 = new BadThemeContainerControl.MainAreaClass();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationSetting));
            this.folderBrowserDialogDossier = new System.Windows.Forms.FolderBrowserDialog();
            this.ApplicationSettingsTheme = new BadForm();
            this.chkShowDBError = new BadCheckBox();
            this.badLabel2 = new BadLabel();
            this.Cancel = new BadButton();
            this.cboSelectPlayer = new BadDropDownBox();
            this.btnSelectDossierFilePath = new BadButton();
            this.txtDossierFilePath = new BadTextBox();
            this.btnDbSetting = new BadButton();
            this.btnSave = new BadButton();
            this.lblDbSettings = new BadLabel();
            this.badGroupBox1 = new BadGroupBox();
            this.badGroupBox2 = new BadGroupBox();
            this.badGroupBox3 = new BadGroupBox();
            this.ApplicationSettingsTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // ApplicationSettingsTheme
            // 
            this.ApplicationSettingsTheme.Controls.Add(this.chkShowDBError);
            this.ApplicationSettingsTheme.Controls.Add(this.badLabel2);
            this.ApplicationSettingsTheme.Controls.Add(this.Cancel);
            this.ApplicationSettingsTheme.Controls.Add(this.cboSelectPlayer);
            this.ApplicationSettingsTheme.Controls.Add(this.btnSelectDossierFilePath);
            this.ApplicationSettingsTheme.Controls.Add(this.txtDossierFilePath);
            this.ApplicationSettingsTheme.Controls.Add(this.btnDbSetting);
            this.ApplicationSettingsTheme.Controls.Add(this.btnSave);
            this.ApplicationSettingsTheme.Controls.Add(this.lblDbSettings);
            this.ApplicationSettingsTheme.Controls.Add(this.badGroupBox1);
            this.ApplicationSettingsTheme.Controls.Add(this.badGroupBox2);
            this.ApplicationSettingsTheme.Controls.Add(this.badGroupBox3);
            this.ApplicationSettingsTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ApplicationSettingsTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ApplicationSettingsTheme.FormFooter = false;
            this.ApplicationSettingsTheme.FormFooterHeight = 26;
            this.ApplicationSettingsTheme.FormInnerBorder = 3;
            this.ApplicationSettingsTheme.FormMargin = 0;
            this.ApplicationSettingsTheme.Image = null;
            this.ApplicationSettingsTheme.Location = new System.Drawing.Point(0, 0);
            this.ApplicationSettingsTheme.MainArea = mainAreaClass1;
            this.ApplicationSettingsTheme.Name = "ApplicationSettingsTheme";
            this.ApplicationSettingsTheme.Resizable = false;
            this.ApplicationSettingsTheme.Size = new System.Drawing.Size(498, 379);
            this.ApplicationSettingsTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("ApplicationSettingsTheme.SystemExitImage")));
            this.ApplicationSettingsTheme.SystemMaximizeImage = null;
            this.ApplicationSettingsTheme.SystemMinimizeImage = null;
            this.ApplicationSettingsTheme.TabIndex = 0;
            this.ApplicationSettingsTheme.Text = "Application Settings";
            this.ApplicationSettingsTheme.TitleHeight = 26;
            // 
            // chkShowDBError
            // 
            this.chkShowDBError.BackColor = System.Drawing.Color.Transparent;
            this.chkShowDBError.Checked = false;
            this.chkShowDBError.Image = ((System.Drawing.Image)(resources.GetObject("chkShowDBError.Image")));
            this.chkShowDBError.Location = new System.Drawing.Point(25, 327);
            this.chkShowDBError.Name = "chkShowDBError";
            this.chkShowDBError.Size = new System.Drawing.Size(235, 23);
            this.chkShowDBError.TabIndex = 17;
            this.chkShowDBError.Text = "Show all database errors (debug mode)";
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
            this.badLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(44, 265);
            this.badLabel2.Name = "badLabel2";
            this.badLabel2.Size = new System.Drawing.Size(250, 23);
            this.badLabel2.TabIndex = 16;
            this.badLabel2.Text = "Select player if several players dossier files is read:";
            // 
            // Cancel
            // 
            this.Cancel.Image = null;
            this.Cancel.Location = new System.Drawing.Point(395, 327);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 12;
            this.Cancel.Text = "Cancel";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // cboSelectPlayer
            // 
            this.cboSelectPlayer.Image = null;
            this.cboSelectPlayer.Location = new System.Drawing.Point(300, 265);
            this.cboSelectPlayer.Name = "cboSelectPlayer";
            this.cboSelectPlayer.Size = new System.Drawing.Size(155, 23);
            this.cboSelectPlayer.TabIndex = 8;
            this.cboSelectPlayer.Click += new System.EventHandler(this.cboSelectPlayer_Click);
            // 
            // btnSelectDossierFilePath
            // 
            this.btnSelectDossierFilePath.Image = null;
            this.btnSelectDossierFilePath.Location = new System.Drawing.Point(367, 95);
            this.btnSelectDossierFilePath.Name = "btnSelectDossierFilePath";
            this.btnSelectDossierFilePath.Size = new System.Drawing.Size(88, 23);
            this.btnSelectDossierFilePath.TabIndex = 3;
            this.btnSelectDossierFilePath.Text = "Change Path";
            this.btnSelectDossierFilePath.Click += new System.EventHandler(this.btnSelectDossierFilePath_Click);
            // 
            // txtDossierFilePath
            // 
            this.txtDossierFilePath.HasFocus = false;
            this.txtDossierFilePath.Image = null;
            this.txtDossierFilePath.Location = new System.Drawing.Point(44, 73);
            this.txtDossierFilePath.MultilineAllow = true;
            this.txtDossierFilePath.Name = "txtDossierFilePath";
            this.txtDossierFilePath.PasswordChar = '\0';
            this.txtDossierFilePath.Size = new System.Drawing.Size(314, 45);
            this.txtDossierFilePath.TabIndex = 2;
            this.txtDossierFilePath.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnDbSetting
            // 
            this.btnDbSetting.Image = null;
            this.btnDbSetting.Location = new System.Drawing.Point(367, 186);
            this.btnDbSetting.Name = "btnDbSetting";
            this.btnDbSetting.Size = new System.Drawing.Size(88, 23);
            this.btnDbSetting.TabIndex = 6;
            this.btnDbSetting.Text = "Settings";
            this.btnDbSetting.Click += new System.EventHandler(this.btnDbSetting_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = null;
            this.btnSave.Location = new System.Drawing.Point(300, 327);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(77, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // lblDbSettings
            // 
            this.lblDbSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblDbSettings.Dimmed = false;
            this.lblDbSettings.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
            this.lblDbSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
            this.lblDbSettings.Image = null;
            this.lblDbSettings.Location = new System.Drawing.Point(44, 173);
            this.lblDbSettings.Name = "lblDbSettings";
            this.lblDbSettings.Size = new System.Drawing.Size(317, 36);
            this.lblDbSettings.TabIndex = 5;
            this.lblDbSettings.TabStop = false;
            this.lblDbSettings.Text = "Database:";
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(25, 49);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(447, 86);
            this.badGroupBox1.TabIndex = 1;
            this.badGroupBox1.TabStop = false;
            this.badGroupBox1.Text = "Dossier File Path";
            // 
            // badGroupBox2
            // 
            this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox2.Image = null;
            this.badGroupBox2.Location = new System.Drawing.Point(25, 153);
            this.badGroupBox2.Name = "badGroupBox2";
            this.badGroupBox2.Size = new System.Drawing.Size(447, 71);
            this.badGroupBox2.TabIndex = 4;
            this.badGroupBox2.TabStop = false;
            this.badGroupBox2.Text = "Database";
            // 
            // badGroupBox3
            // 
            this.badGroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox3.Image = null;
            this.badGroupBox3.Location = new System.Drawing.Point(25, 240);
            this.badGroupBox3.Name = "badGroupBox3";
            this.badGroupBox3.Size = new System.Drawing.Size(445, 64);
            this.badGroupBox3.TabIndex = 7;
            this.badGroupBox3.TabStop = false;
            this.badGroupBox3.Text = "Player";
            // 
            // ApplicationSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 379);
            this.Controls.Add(this.ApplicationSettingsTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApplicationSetting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Application settings";
            this.Load += new System.EventHandler(this.frmDossierFileSelect_Load);
            this.ApplicationSettingsTheme.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private BadForm ApplicationSettingsTheme;
		private BadGroupBox badGroupBox1;
		private BadButton btnDbSetting;
		private BadLabel lblDbSettings;
		private BadTextBox txtDossierFilePath;
		private BadButton btnSelectDossierFilePath;
		private BadButton btnSave;
		private BadDropDownBox cboSelectPlayer;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogDossier;
		private BadGroupBox badGroupBox2;
		private BadGroupBox badGroupBox3;
        private BadButton Cancel;
		private BadLabel badLabel2;
		private BadCheckBox chkShowDBError;
	}
}