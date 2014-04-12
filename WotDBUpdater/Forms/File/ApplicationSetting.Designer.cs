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
			this.cboSelectPlayer = new BadComboBox();
			this.badSeperator2 = new BadSeperator();
			this.badSeperator1 = new BadSeperator();
			this.btnSave = new BadButton();
			this.btnSelectDossierFilePath = new BadButton();
			this.txtDossierFilePath = new BadTextBox();
			this.badLabel4 = new BadLabel();
			this.btnRemovePlayer = new BadButton();
			this.btmAddPlayer = new BadButton();
			this.badLabel3 = new BadLabel();
			this.badLabel2 = new BadLabel();
			this.btnDbSetting = new BadButton();
			this.cboDatabaseType = new System.Windows.Forms.ComboBox();
			this.badGroupBox1 = new BadGroupBox();
			this.ApplicationSettingsTheme.SuspendLayout();
			this.SuspendLayout();
			// 
			// ApplicationSettingsTheme
			// 
			this.ApplicationSettingsTheme.Controls.Add(this.cboSelectPlayer);
			this.ApplicationSettingsTheme.Controls.Add(this.badSeperator2);
			this.ApplicationSettingsTheme.Controls.Add(this.badSeperator1);
			this.ApplicationSettingsTheme.Controls.Add(this.btnSave);
			this.ApplicationSettingsTheme.Controls.Add(this.btnSelectDossierFilePath);
			this.ApplicationSettingsTheme.Controls.Add(this.txtDossierFilePath);
			this.ApplicationSettingsTheme.Controls.Add(this.badLabel4);
			this.ApplicationSettingsTheme.Controls.Add(this.btnRemovePlayer);
			this.ApplicationSettingsTheme.Controls.Add(this.btmAddPlayer);
			this.ApplicationSettingsTheme.Controls.Add(this.badLabel3);
			this.ApplicationSettingsTheme.Controls.Add(this.badLabel2);
			this.ApplicationSettingsTheme.Controls.Add(this.btnDbSetting);
			this.ApplicationSettingsTheme.Controls.Add(this.cboDatabaseType);
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
			this.ApplicationSettingsTheme.Resizable = true;
			this.ApplicationSettingsTheme.Size = new System.Drawing.Size(487, 357);
			this.ApplicationSettingsTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("ApplicationSettingsTheme.SystemExitImage")));
			this.ApplicationSettingsTheme.SystemMaximizeImage = null;
			this.ApplicationSettingsTheme.SystemMinimizeImage = null;
			this.ApplicationSettingsTheme.TabIndex = 11;
			this.ApplicationSettingsTheme.Text = "Application Settings";
			this.ApplicationSettingsTheme.TitleHeight = 26;
			// 
			// cboSelectPlayer
			// 
			this.cboSelectPlayer.Image = ((System.Drawing.Image)(resources.GetObject("cboSelectPlayer.Image")));
			this.cboSelectPlayer.Location = new System.Drawing.Point(139, 126);
			this.cboSelectPlayer.Name = "cboSelectPlayer";
			this.cboSelectPlayer.Size = new System.Drawing.Size(141, 23);
			this.cboSelectPlayer.TabIndex = 38;
			this.cboSelectPlayer.Text = "badComboBox1";
			this.cboSelectPlayer.Click += new System.EventHandler(this.cboSelectPlayer_Click);
			// 
			// badSeperator2
			// 
			this.badSeperator2.BackColor = System.Drawing.Color.Transparent;
			this.badSeperator2.Direction = System.Windows.Forms.Orientation.Horizontal;
			this.badSeperator2.Image = null;
			this.badSeperator2.Location = new System.Drawing.Point(40, 156);
			this.badSeperator2.Name = "badSeperator2";
			this.badSeperator2.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
			this.badSeperator2.Size = new System.Drawing.Size(411, 23);
			this.badSeperator2.TabIndex = 37;
			// 
			// badSeperator1
			// 
			this.badSeperator1.BackColor = System.Drawing.Color.Transparent;
			this.badSeperator1.Direction = System.Windows.Forms.Orientation.Horizontal;
			this.badSeperator1.Image = null;
			this.badSeperator1.Location = new System.Drawing.Point(40, 94);
			this.badSeperator1.Name = "badSeperator1";
			this.badSeperator1.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
			this.badSeperator1.Size = new System.Drawing.Size(411, 26);
			this.badSeperator1.TabIndex = 36;
			// 
			// btnSave
			// 
			this.btnSave.Image = null;
			this.btnSave.Location = new System.Drawing.Point(378, 314);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(88, 23);
			this.btnSave.TabIndex = 35;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
			// 
			// btnSelectDossierFilePath
			// 
			this.btnSelectDossierFilePath.Image = null;
			this.btnSelectDossierFilePath.Location = new System.Drawing.Point(363, 255);
			this.btnSelectDossierFilePath.Name = "btnSelectDossierFilePath";
			this.btnSelectDossierFilePath.Size = new System.Drawing.Size(88, 23);
			this.btnSelectDossierFilePath.TabIndex = 34;
			this.btnSelectDossierFilePath.Text = "Select Path";
			this.btnSelectDossierFilePath.Click += new System.EventHandler(this.btnSelectDossierFilePath_Click);
			// 
			// txtDossierFilePath
			// 
			this.txtDossierFilePath.Image = null;
			this.txtDossierFilePath.Location = new System.Drawing.Point(40, 202);
			this.txtDossierFilePath.Name = "txtDossierFilePath";
			this.txtDossierFilePath.Size = new System.Drawing.Size(411, 45);
			this.txtDossierFilePath.TabIndex = 32;
			// 
			// badLabel4
			// 
			this.badLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel4.Image = null;
			this.badLabel4.Location = new System.Drawing.Point(40, 182);
			this.badLabel4.Name = "badLabel4";
			this.badLabel4.Size = new System.Drawing.Size(176, 23);
			this.badLabel4.TabIndex = 33;
			this.badLabel4.Text = "Dossier File Path:";
			// 
			// btnRemovePlayer
			// 
			this.btnRemovePlayer.Image = null;
			this.btnRemovePlayer.Location = new System.Drawing.Point(373, 126);
			this.btnRemovePlayer.Name = "btnRemovePlayer";
			this.btnRemovePlayer.Size = new System.Drawing.Size(78, 23);
			this.btnRemovePlayer.TabIndex = 31;
			this.btnRemovePlayer.Text = "Remove";
			this.btnRemovePlayer.Click += new System.EventHandler(this.btnRemovePlayer_Click_1);
			// 
			// btmAddPlayer
			// 
			this.btmAddPlayer.Image = null;
			this.btmAddPlayer.Location = new System.Drawing.Point(286, 126);
			this.btmAddPlayer.Name = "btmAddPlayer";
			this.btmAddPlayer.Size = new System.Drawing.Size(81, 23);
			this.btmAddPlayer.TabIndex = 30;
			this.btmAddPlayer.Text = "Add";
			this.btmAddPlayer.Click += new System.EventHandler(this.btmAddPlayer_Click);
			// 
			// badLabel3
			// 
			this.badLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel3.Image = null;
			this.badLabel3.Location = new System.Drawing.Point(40, 126);
			this.badLabel3.Name = "badLabel3";
			this.badLabel3.Size = new System.Drawing.Size(45, 23);
			this.badLabel3.TabIndex = 29;
			this.badLabel3.Text = "Player:";
			// 
			// badLabel2
			// 
			this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel2.Image = null;
			this.badLabel2.Location = new System.Drawing.Point(40, 63);
			this.badLabel2.Name = "badLabel2";
			this.badLabel2.Size = new System.Drawing.Size(93, 23);
			this.badLabel2.TabIndex = 28;
			this.badLabel2.Text = "Database Type:";
			// 
			// btnDbSetting
			// 
			this.btnDbSetting.Image = null;
			this.btnDbSetting.Location = new System.Drawing.Point(373, 65);
			this.btnDbSetting.Name = "btnDbSetting";
			this.btnDbSetting.Size = new System.Drawing.Size(78, 23);
			this.btnDbSetting.TabIndex = 24;
			this.btnDbSetting.Text = "Database";
			this.btnDbSetting.Click += new System.EventHandler(this.btnDbSetting_Click);
			// 
			// cboDatabaseType
			// 
			this.cboDatabaseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDatabaseType.FormattingEnabled = true;
			this.cboDatabaseType.Items.AddRange(new object[] {
            "MS SQL Server",
            "SQLite"});
			this.cboDatabaseType.Location = new System.Drawing.Point(139, 65);
			this.cboDatabaseType.Name = "cboDatabaseType";
			this.cboDatabaseType.Size = new System.Drawing.Size(144, 21);
			this.cboDatabaseType.TabIndex = 13;
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(19, 41);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(447, 257);
			this.badGroupBox1.TabIndex = 20;
			this.badGroupBox1.Text = "Settings";
			// 
			// ApplicationSetting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(487, 357);
			this.Controls.Add(this.ApplicationSettingsTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ApplicationSetting";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Application settings";
			this.Load += new System.EventHandler(this.frmDossierFileSelect_Load);
			this.ApplicationSettingsTheme.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog openFileDialogDossierFile;
		private System.Windows.Forms.ComboBox cboDatabaseType;
		private BadForm ApplicationSettingsTheme;
		private BadGroupBox badGroupBox1;
		private BadButton btnDbSetting;
		private BadLabel badLabel2;
		private BadLabel badLabel3;
		private BadButton btmAddPlayer;
		private BadButton btnRemovePlayer;
		private BadTextBox txtDossierFilePath;
		private BadLabel badLabel4;
		private BadButton btnSelectDossierFilePath;
		private BadButton btnSave;
		private BadSeperator badSeperator2;
		private BadSeperator badSeperator1;
		private BadComboBox cboSelectPlayer;
	}
}