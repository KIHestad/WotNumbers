namespace WotDBUpdater.Forms.File
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
			this.openFileDialogDossierFile = new System.Windows.Forms.OpenFileDialog();
			this.ApplicationSettingsTheme = new BadForm();
			this.btnSelectDossierFilePath = new BadButton();
			this.btnRemovePlayer = new BadButton();
			this.btnAddPlayer = new BadButton();
			this.txtDossierFilePath = new BadTextBox();
			this.badLabel4 = new BadLabel();
			this.btnDbSetting = new BadButton();
			this.cboSelectPlayer = new BadPopupBox();
			this.badSeperator2 = new BadSeperator();
			this.badSeperator1 = new BadSeperator();
			this.btnSave = new BadButton();
			this.lblPlayer = new BadLabel();
			this.lblDbSettings = new BadLabel();
			this.badGroupBox1 = new BadGroupBox();
			this.ApplicationSettingsTheme.SuspendLayout();
			this.SuspendLayout();
			// 
			// ApplicationSettingsTheme
			// 
			this.ApplicationSettingsTheme.Controls.Add(this.btnSelectDossierFilePath);
			this.ApplicationSettingsTheme.Controls.Add(this.btnRemovePlayer);
			this.ApplicationSettingsTheme.Controls.Add(this.btnAddPlayer);
			this.ApplicationSettingsTheme.Controls.Add(this.txtDossierFilePath);
			this.ApplicationSettingsTheme.Controls.Add(this.badLabel4);
			this.ApplicationSettingsTheme.Controls.Add(this.btnDbSetting);
			this.ApplicationSettingsTheme.Controls.Add(this.cboSelectPlayer);
			this.ApplicationSettingsTheme.Controls.Add(this.badSeperator2);
			this.ApplicationSettingsTheme.Controls.Add(this.badSeperator1);
			this.ApplicationSettingsTheme.Controls.Add(this.btnSave);
			this.ApplicationSettingsTheme.Controls.Add(this.lblPlayer);
			this.ApplicationSettingsTheme.Controls.Add(this.lblDbSettings);
			this.ApplicationSettingsTheme.Controls.Add(this.badGroupBox1);
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
			this.ApplicationSettingsTheme.Size = new System.Drawing.Size(487, 331);
			this.ApplicationSettingsTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("ApplicationSettingsTheme.SystemExitImage")));
			this.ApplicationSettingsTheme.SystemMaximizeImage = null;
			this.ApplicationSettingsTheme.SystemMinimizeImage = null;
			this.ApplicationSettingsTheme.TabIndex = 11;
			this.ApplicationSettingsTheme.Text = "Application Settings";
			this.ApplicationSettingsTheme.TitleHeight = 26;
			// 
			// btnSelectDossierFilePath
			// 
			this.btnSelectDossierFilePath.Image = null;
			this.btnSelectDossierFilePath.Location = new System.Drawing.Point(363, 237);
			this.btnSelectDossierFilePath.Name = "btnSelectDossierFilePath";
			this.btnSelectDossierFilePath.Size = new System.Drawing.Size(88, 23);
			this.btnSelectDossierFilePath.TabIndex = 34;
			this.btnSelectDossierFilePath.Text = "Select Path";
			this.btnSelectDossierFilePath.Click += new System.EventHandler(this.btnSelectDossierFilePath_Click);
			// 
			// btnRemovePlayer
			// 
			this.btnRemovePlayer.Image = null;
			this.btnRemovePlayer.Location = new System.Drawing.Point(373, 118);
			this.btnRemovePlayer.Name = "btnRemovePlayer";
			this.btnRemovePlayer.Size = new System.Drawing.Size(78, 23);
			this.btnRemovePlayer.TabIndex = 31;
			this.btnRemovePlayer.Text = "Remove";
			this.btnRemovePlayer.Click += new System.EventHandler(this.btnRemovePlayer_Click_1);
			// 
			// btnAddPlayer
			// 
			this.btnAddPlayer.Image = null;
			this.btnAddPlayer.Location = new System.Drawing.Point(286, 118);
			this.btnAddPlayer.Name = "btnAddPlayer";
			this.btnAddPlayer.Size = new System.Drawing.Size(81, 23);
			this.btnAddPlayer.TabIndex = 30;
			this.btnAddPlayer.Text = "Add";
			this.btnAddPlayer.Click += new System.EventHandler(this.btmAddPlayer_Click);
			// 
			// txtDossierFilePath
			// 
			this.txtDossierFilePath.Image = null;
			this.txtDossierFilePath.Location = new System.Drawing.Point(40, 181);
			this.txtDossierFilePath.Name = "txtDossierFilePath";
			this.txtDossierFilePath.PasswordChar = '\0';
			this.txtDossierFilePath.Size = new System.Drawing.Size(411, 45);
			this.txtDossierFilePath.TabIndex = 32;
			// 
			// badLabel4
			// 
			this.badLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel4.Dimmed = false;
			this.badLabel4.Image = null;
			this.badLabel4.Location = new System.Drawing.Point(40, 160);
			this.badLabel4.Name = "badLabel4";
			this.badLabel4.Size = new System.Drawing.Size(100, 23);
			this.badLabel4.TabIndex = 33;
			this.badLabel4.Text = "Dossier File Path:";
			// 
			// btnDbSetting
			// 
			this.btnDbSetting.Image = null;
			this.btnDbSetting.Location = new System.Drawing.Point(373, 65);
			this.btnDbSetting.Name = "btnDbSetting";
			this.btnDbSetting.Size = new System.Drawing.Size(78, 23);
			this.btnDbSetting.TabIndex = 24;
			this.btnDbSetting.Text = "Settings";
			this.btnDbSetting.Click += new System.EventHandler(this.btnDbSetting_Click);
			// 
			// cboSelectPlayer
			// 
			this.cboSelectPlayer.Image = ((System.Drawing.Image)(resources.GetObject("cboSelectPlayer.Image")));
			this.cboSelectPlayer.Location = new System.Drawing.Point(91, 118);
			this.cboSelectPlayer.Name = "cboSelectPlayer";
			this.cboSelectPlayer.Size = new System.Drawing.Size(189, 23);
			this.cboSelectPlayer.TabIndex = 38;
			this.cboSelectPlayer.Text = "badComboBox1";
			this.cboSelectPlayer.Click += new System.EventHandler(this.cboSelectPlayer_Click);
			// 
			// badSeperator2
			// 
			this.badSeperator2.BackColor = System.Drawing.Color.Transparent;
			this.badSeperator2.Direction = System.Windows.Forms.Orientation.Horizontal;
			this.badSeperator2.Image = null;
			this.badSeperator2.Location = new System.Drawing.Point(40, 147);
			this.badSeperator2.Name = "badSeperator2";
			this.badSeperator2.Size = new System.Drawing.Size(411, 18);
			this.badSeperator2.TabIndex = 37;
			// 
			// badSeperator1
			// 
			this.badSeperator1.BackColor = System.Drawing.Color.Transparent;
			this.badSeperator1.Direction = System.Windows.Forms.Orientation.Horizontal;
			this.badSeperator1.Image = null;
			this.badSeperator1.Location = new System.Drawing.Point(40, 94);
			this.badSeperator1.Name = "badSeperator1";
			this.badSeperator1.Size = new System.Drawing.Size(411, 18);
			this.badSeperator1.TabIndex = 36;
			// 
			// btnSave
			// 
			this.btnSave.Image = null;
			this.btnSave.Location = new System.Drawing.Point(378, 290);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(88, 23);
			this.btnSave.TabIndex = 35;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
			// 
			// lblPlayer
			// 
			this.lblPlayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblPlayer.Dimmed = false;
			this.lblPlayer.Image = null;
			this.lblPlayer.Location = new System.Drawing.Point(40, 118);
			this.lblPlayer.Name = "lblPlayer";
			this.lblPlayer.Size = new System.Drawing.Size(45, 23);
			this.lblPlayer.TabIndex = 29;
			this.lblPlayer.Text = "Player:";
			// 
			// lblDbSettings
			// 
			this.lblDbSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblDbSettings.Dimmed = false;
			this.lblDbSettings.Image = null;
			this.lblDbSettings.Location = new System.Drawing.Point(40, 60);
			this.lblDbSettings.Name = "lblDbSettings";
			this.lblDbSettings.Size = new System.Drawing.Size(317, 36);
			this.lblDbSettings.TabIndex = 28;
			this.lblDbSettings.Text = "Database:";
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(19, 41);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(447, 234);
			this.badGroupBox1.TabIndex = 20;
			this.badGroupBox1.Text = "Settings";
			// 
			// ApplicationSetting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(487, 331);
			this.Controls.Add(this.ApplicationSettingsTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ApplicationSetting";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Application settings";
			this.Activated += new System.EventHandler(this.ApplicationSetting_Activated);
			this.Load += new System.EventHandler(this.frmDossierFileSelect_Load);
			this.ApplicationSettingsTheme.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog openFileDialogDossierFile;
		private BadForm ApplicationSettingsTheme;
		private BadGroupBox badGroupBox1;
		private BadButton btnDbSetting;
		private BadLabel lblDbSettings;
		private BadLabel lblPlayer;
		private BadButton btnAddPlayer;
		private BadButton btnRemovePlayer;
		private BadTextBox txtDossierFilePath;
		private BadLabel badLabel4;
		private BadButton btnSelectDossierFilePath;
		private BadButton btnSave;
		private BadSeperator badSeperator2;
		private BadSeperator badSeperator1;
		private BadPopupBox cboSelectPlayer;
	}
}