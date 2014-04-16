namespace WotDBUpdater.Forms.File
{
	partial class DatabaseSetting
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseSetting));
			this.openFileDialogSQLite = new System.Windows.Forms.OpenFileDialog();
			this.DatabaseSettingsTheme = new BadForm();
			this.panelSQLite = new System.Windows.Forms.Panel();
			this.txtDatabaseFile = new BadTextBox();
			this.badLabel5 = new BadLabel();
			this.badSeperator2 = new BadSeperator();
			this.cmdSQLiteDatabaseFile = new BadButton();
			this.panelMSSQL = new System.Windows.Forms.Panel();
			this.popupDbAuth = new BadPopupBox();
			this.badSeperator1 = new BadSeperator();
			this.popupDatabase = new BadPopupBox();
			this.badLabel1 = new BadLabel();
			this.badLabel3 = new BadLabel();
			this.lblUIDPW = new BadLabel();
			this.txtUID = new BadTextBox();
			this.badLabel4 = new BadLabel();
			this.txtPW = new BadTextBox();
			this.txtServerName = new BadTextBox();
			this.popupDatabaseType = new BadPopupBox();
			this.badLabel6 = new BadLabel();
			this.btnSave = new BadButton();
			this.btnNewDb = new BadButton();
			this.badGroupBox1 = new BadGroupBox();
			this.DatabaseSettingsTheme.SuspendLayout();
			this.panelSQLite.SuspendLayout();
			this.panelMSSQL.SuspendLayout();
			this.SuspendLayout();
			// 
			// openFileDialogSQLite
			// 
			this.openFileDialogSQLite.FileName = "openFileDialog1";
			// 
			// DatabaseSettingsTheme
			// 
			this.DatabaseSettingsTheme.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.DatabaseSettingsTheme.Controls.Add(this.panelSQLite);
			this.DatabaseSettingsTheme.Controls.Add(this.panelMSSQL);
			this.DatabaseSettingsTheme.Controls.Add(this.popupDatabaseType);
			this.DatabaseSettingsTheme.Controls.Add(this.badLabel6);
			this.DatabaseSettingsTheme.Controls.Add(this.btnSave);
			this.DatabaseSettingsTheme.Controls.Add(this.btnNewDb);
			this.DatabaseSettingsTheme.Controls.Add(this.badGroupBox1);
			this.DatabaseSettingsTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DatabaseSettingsTheme.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.DatabaseSettingsTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.DatabaseSettingsTheme.FormFooter = false;
			this.DatabaseSettingsTheme.FormFooterHeight = 26;
			this.DatabaseSettingsTheme.FormInnerBorder = 3;
			this.DatabaseSettingsTheme.FormMargin = 0;
			this.DatabaseSettingsTheme.Image = null;
			this.DatabaseSettingsTheme.Location = new System.Drawing.Point(0, 0);
			this.DatabaseSettingsTheme.MainArea = mainAreaClass1;
			this.DatabaseSettingsTheme.Name = "DatabaseSettingsTheme";
			this.DatabaseSettingsTheme.Resizable = false;
			this.DatabaseSettingsTheme.Size = new System.Drawing.Size(371, 517);
			this.DatabaseSettingsTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("DatabaseSettingsTheme.SystemExitImage")));
			this.DatabaseSettingsTheme.SystemMaximizeImage = null;
			this.DatabaseSettingsTheme.SystemMinimizeImage = null;
			this.DatabaseSettingsTheme.TabIndex = 7;
			this.DatabaseSettingsTheme.Text = "Database Settings";
			this.DatabaseSettingsTheme.TitleHeight = 26;
			// 
			// panelSQLite
			// 
			this.panelSQLite.Controls.Add(this.txtDatabaseFile);
			this.panelSQLite.Controls.Add(this.badLabel5);
			this.panelSQLite.Controls.Add(this.badSeperator2);
			this.panelSQLite.Controls.Add(this.cmdSQLiteDatabaseFile);
			this.panelSQLite.Location = new System.Drawing.Point(33, 350);
			this.panelSQLite.Name = "panelSQLite";
			this.panelSQLite.Size = new System.Drawing.Size(311, 140);
			this.panelSQLite.TabIndex = 31;
			// 
			// txtDatabaseFile
			// 
			this.txtDatabaseFile.Image = null;
			this.txtDatabaseFile.Location = new System.Drawing.Point(6, 48);
			this.txtDatabaseFile.Name = "txtDatabaseFile";
			this.txtDatabaseFile.PasswordChar = '\0';
			this.txtDatabaseFile.Size = new System.Drawing.Size(296, 45);
			this.txtDatabaseFile.TabIndex = 26;
			// 
			// badLabel5
			// 
			this.badLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel5.Dimmed = false;
			this.badLabel5.Image = null;
			this.badLabel5.Location = new System.Drawing.Point(4, 28);
			this.badLabel5.Name = "badLabel5";
			this.badLabel5.Size = new System.Drawing.Size(75, 23);
			this.badLabel5.TabIndex = 27;
			this.badLabel5.Text = "Database File:";
			// 
			// badSeperator2
			// 
			this.badSeperator2.BackColor = System.Drawing.Color.Transparent;
			this.badSeperator2.Direction = System.Windows.Forms.Orientation.Horizontal;
			this.badSeperator2.Image = null;
			this.badSeperator2.Location = new System.Drawing.Point(6, 3);
			this.badSeperator2.Name = "badSeperator2";
			this.badSeperator2.Size = new System.Drawing.Size(302, 23);
			this.badSeperator2.TabIndex = 9;
			this.badSeperator2.Text = "SQLite";
			// 
			// cmdSQLiteDatabaseFile
			// 
			this.cmdSQLiteDatabaseFile.Image = null;
			this.cmdSQLiteDatabaseFile.Location = new System.Drawing.Point(227, 99);
			this.cmdSQLiteDatabaseFile.Name = "cmdSQLiteDatabaseFile";
			this.cmdSQLiteDatabaseFile.Size = new System.Drawing.Size(75, 23);
			this.cmdSQLiteDatabaseFile.TabIndex = 13;
			this.cmdSQLiteDatabaseFile.Text = "Select File";
			this.cmdSQLiteDatabaseFile.Click += new System.EventHandler(this.cmdSQLiteDatabaseFile_Click);
			// 
			// panelMSSQL
			// 
			this.panelMSSQL.Controls.Add(this.popupDbAuth);
			this.panelMSSQL.Controls.Add(this.badSeperator1);
			this.panelMSSQL.Controls.Add(this.popupDatabase);
			this.panelMSSQL.Controls.Add(this.badLabel1);
			this.panelMSSQL.Controls.Add(this.badLabel3);
			this.panelMSSQL.Controls.Add(this.lblUIDPW);
			this.panelMSSQL.Controls.Add(this.txtUID);
			this.panelMSSQL.Controls.Add(this.badLabel4);
			this.panelMSSQL.Controls.Add(this.txtPW);
			this.panelMSSQL.Controls.Add(this.txtServerName);
			this.panelMSSQL.Location = new System.Drawing.Point(30, 127);
			this.panelMSSQL.Name = "panelMSSQL";
			this.panelMSSQL.Size = new System.Drawing.Size(311, 152);
			this.panelMSSQL.TabIndex = 30;
			// 
			// popupDbAuth
			// 
			this.popupDbAuth.Image = ((System.Drawing.Image)(resources.GetObject("popupDbAuth.Image")));
			this.popupDbAuth.Location = new System.Drawing.Point(129, 64);
			this.popupDbAuth.Name = "popupDbAuth";
			this.popupDbAuth.Size = new System.Drawing.Size(176, 23);
			this.popupDbAuth.TabIndex = 21;
			this.popupDbAuth.Text = null;
			this.popupDbAuth.Click += new System.EventHandler(this.popupDbAuth_Click);
			// 
			// badSeperator1
			// 
			this.badSeperator1.BackColor = System.Drawing.Color.Transparent;
			this.badSeperator1.Direction = System.Windows.Forms.Orientation.Horizontal;
			this.badSeperator1.Image = null;
			this.badSeperator1.Location = new System.Drawing.Point(3, 3);
			this.badSeperator1.Name = "badSeperator1";
			this.badSeperator1.Size = new System.Drawing.Size(302, 26);
			this.badSeperator1.TabIndex = 12;
			this.badSeperator1.Text = "MS SQL Server";
			// 
			// popupDatabase
			// 
			this.popupDatabase.Image = ((System.Drawing.Image)(resources.GetObject("popupDatabase.Image")));
			this.popupDatabase.Location = new System.Drawing.Point(129, 122);
			this.popupDatabase.Name = "popupDatabase";
			this.popupDatabase.Size = new System.Drawing.Size(176, 23);
			this.popupDatabase.TabIndex = 15;
			this.popupDatabase.Text = null;
			this.popupDatabase.Click += new System.EventHandler(this.popupDatabase_Click);
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(3, 122);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(96, 23);
			this.badLabel1.TabIndex = 16;
			this.badLabel1.Text = "Select Database:";
			// 
			// badLabel3
			// 
			this.badLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel3.Dimmed = false;
			this.badLabel3.Image = null;
			this.badLabel3.Location = new System.Drawing.Point(3, 35);
			this.badLabel3.Name = "badLabel3";
			this.badLabel3.Size = new System.Drawing.Size(75, 23);
			this.badLabel3.TabIndex = 24;
			this.badLabel3.Text = "Server Name:";
			// 
			// lblUIDPW
			// 
			this.lblUIDPW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblUIDPW.Dimmed = true;
			this.lblUIDPW.Image = null;
			this.lblUIDPW.Location = new System.Drawing.Point(3, 93);
			this.lblUIDPW.Name = "lblUIDPW";
			this.lblUIDPW.Size = new System.Drawing.Size(123, 23);
			this.lblUIDPW.TabIndex = 19;
			this.lblUIDPW.Text = "User Name / Password:";
			// 
			// txtUID
			// 
			this.txtUID.Image = null;
			this.txtUID.Location = new System.Drawing.Point(129, 93);
			this.txtUID.Name = "txtUID";
			this.txtUID.PasswordChar = '\0';
			this.txtUID.Size = new System.Drawing.Size(85, 23);
			this.txtUID.TabIndex = 18;
			// 
			// badLabel4
			// 
			this.badLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel4.Dimmed = false;
			this.badLabel4.Image = null;
			this.badLabel4.Location = new System.Drawing.Point(3, 64);
			this.badLabel4.Name = "badLabel4";
			this.badLabel4.Size = new System.Drawing.Size(96, 23);
			this.badLabel4.TabIndex = 22;
			this.badLabel4.Text = "Authentication:";
			// 
			// txtPW
			// 
			this.txtPW.Image = null;
			this.txtPW.Location = new System.Drawing.Point(220, 93);
			this.txtPW.Name = "txtPW";
			this.txtPW.PasswordChar = '*';
			this.txtPW.Size = new System.Drawing.Size(85, 23);
			this.txtPW.TabIndex = 17;
			// 
			// txtServerName
			// 
			this.txtServerName.Image = null;
			this.txtServerName.Location = new System.Drawing.Point(129, 35);
			this.txtServerName.Name = "txtServerName";
			this.txtServerName.PasswordChar = '\0';
			this.txtServerName.Size = new System.Drawing.Size(176, 23);
			this.txtServerName.TabIndex = 23;
			// 
			// popupDatabaseType
			// 
			this.popupDatabaseType.Image = ((System.Drawing.Image)(resources.GetObject("popupDatabaseType.Image")));
			this.popupDatabaseType.Location = new System.Drawing.Point(160, 69);
			this.popupDatabaseType.Name = "popupDatabaseType";
			this.popupDatabaseType.Size = new System.Drawing.Size(176, 23);
			this.popupDatabaseType.TabIndex = 29;
			this.popupDatabaseType.Text = null;
			this.popupDatabaseType.Click += new System.EventHandler(this.popupDatabaseType_Click);
			// 
			// badLabel6
			// 
			this.badLabel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel6.Dimmed = false;
			this.badLabel6.Image = null;
			this.badLabel6.Location = new System.Drawing.Point(34, 70);
			this.badLabel6.Name = "badLabel6";
			this.badLabel6.Size = new System.Drawing.Size(96, 23);
			this.badLabel6.TabIndex = 28;
			this.badLabel6.Text = "Database Type:";
			// 
			// btnSave
			// 
			this.btnSave.Image = null;
			this.btnSave.Location = new System.Drawing.Point(279, 306);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 10;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
			// 
			// btnNewDb
			// 
			this.btnNewDb.Image = null;
			this.btnNewDb.Location = new System.Drawing.Point(251, 103);
			this.btnNewDb.Name = "btnNewDb";
			this.btnNewDb.Size = new System.Drawing.Size(85, 23);
			this.btnNewDb.TabIndex = 11;
			this.btnNewDb.Text = "Create New";
			this.btnNewDb.Click += new System.EventHandler(this.btnNewDb_Click);
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(15, 42);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(341, 249);
			this.badGroupBox1.TabIndex = 14;
			this.badGroupBox1.Text = "Settings";
			// 
			// DatabaseSetting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(371, 517);
			this.Controls.Add(this.DatabaseSettingsTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DatabaseSetting";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Database Settings";
			this.Load += new System.EventHandler(this.DatabaseSetting_Load);
			this.DatabaseSettingsTheme.ResumeLayout(false);
			this.panelSQLite.ResumeLayout(false);
			this.panelMSSQL.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm DatabaseSettingsTheme;
		private BadButton btnSave;
		private BadSeperator badSeperator2;
		private BadButton btnNewDb;
		private BadButton cmdSQLiteDatabaseFile;
		private BadSeperator badSeperator1;
		private System.Windows.Forms.OpenFileDialog openFileDialogSQLite;
		private BadPopupBox popupDatabase;
		private BadGroupBox badGroupBox1;
		private BadLabel lblUIDPW;
		private BadTextBox txtUID;
		private BadTextBox txtPW;
		private BadLabel badLabel1;
		private BadLabel badLabel4;
		private BadPopupBox popupDbAuth;
		private BadTextBox txtDatabaseFile;
		private BadLabel badLabel3;
		private BadTextBox txtServerName;
		private BadPopupBox popupDatabaseType;
		private BadLabel badLabel6;
		private BadLabel badLabel5;
		private System.Windows.Forms.Panel panelSQLite;
		private System.Windows.Forms.Panel panelMSSQL;
	}
}