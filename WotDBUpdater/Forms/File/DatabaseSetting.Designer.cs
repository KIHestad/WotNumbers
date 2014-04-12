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
			this.popupDatabaseType = new BadPopupBox();
			this.badLabel6 = new BadLabel();
			this.txtDatabaseFile = new BadTextBox();
			this.badLabel3 = new BadLabel();
			this.txtUID = new BadTextBox();
			this.txtPW = new BadTextBox();
			this.txtServerName = new BadTextBox();
			this.badLabel4 = new BadLabel();
			this.popupDbAuth = new BadPopupBox();
			this.lblUIDPW = new BadLabel();
			this.badLabel1 = new BadLabel();
			this.popupDatabase = new BadPopupBox();
			this.cmdSQLiteDatabaseFile = new BadButton();
			this.badSeperator1 = new BadSeperator();
			this.btnNewDb = new BadButton();
			this.btnSave = new BadButton();
			this.badSeperator2 = new BadSeperator();
			this.badLabel5 = new BadLabel();
			this.badGroupBox1 = new BadGroupBox();
			this.DatabaseSettingsTheme.SuspendLayout();
			this.SuspendLayout();
			// 
			// openFileDialogSQLite
			// 
			this.openFileDialogSQLite.FileName = "openFileDialog1";
			// 
			// DatabaseSettingsTheme
			// 
			this.DatabaseSettingsTheme.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.DatabaseSettingsTheme.Controls.Add(this.popupDatabaseType);
			this.DatabaseSettingsTheme.Controls.Add(this.badLabel6);
			this.DatabaseSettingsTheme.Controls.Add(this.txtDatabaseFile);
			this.DatabaseSettingsTheme.Controls.Add(this.badLabel3);
			this.DatabaseSettingsTheme.Controls.Add(this.txtUID);
			this.DatabaseSettingsTheme.Controls.Add(this.txtPW);
			this.DatabaseSettingsTheme.Controls.Add(this.txtServerName);
			this.DatabaseSettingsTheme.Controls.Add(this.badLabel4);
			this.DatabaseSettingsTheme.Controls.Add(this.popupDbAuth);
			this.DatabaseSettingsTheme.Controls.Add(this.lblUIDPW);
			this.DatabaseSettingsTheme.Controls.Add(this.badLabel1);
			this.DatabaseSettingsTheme.Controls.Add(this.popupDatabase);
			this.DatabaseSettingsTheme.Controls.Add(this.cmdSQLiteDatabaseFile);
			this.DatabaseSettingsTheme.Controls.Add(this.badSeperator1);
			this.DatabaseSettingsTheme.Controls.Add(this.btnNewDb);
			this.DatabaseSettingsTheme.Controls.Add(this.btnSave);
			this.DatabaseSettingsTheme.Controls.Add(this.badSeperator2);
			this.DatabaseSettingsTheme.Controls.Add(this.badLabel5);
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
			this.DatabaseSettingsTheme.Resizable = true;
			this.DatabaseSettingsTheme.Size = new System.Drawing.Size(378, 416);
			this.DatabaseSettingsTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("DatabaseSettingsTheme.SystemExitImage")));
			this.DatabaseSettingsTheme.SystemMaximizeImage = null;
			this.DatabaseSettingsTheme.SystemMinimizeImage = null;
			this.DatabaseSettingsTheme.TabIndex = 7;
			this.DatabaseSettingsTheme.Text = "Database Settings";
			this.DatabaseSettingsTheme.TitleHeight = 26;
			// 
			// popupDatabaseType
			// 
			this.popupDatabaseType.Image = ((System.Drawing.Image)(resources.GetObject("popupDatabaseType.Image")));
			this.popupDatabaseType.Location = new System.Drawing.Point(162, 69);
			this.popupDatabaseType.Name = "popupDatabaseType";
			this.popupDatabaseType.Size = new System.Drawing.Size(176, 23);
			this.popupDatabaseType.TabIndex = 29;
			this.popupDatabaseType.Click += new System.EventHandler(this.popupDatabaseType_Click);
			// 
			// badLabel6
			// 
			this.badLabel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel6.Dimmed = false;
			this.badLabel6.Image = null;
			this.badLabel6.Location = new System.Drawing.Point(36, 70);
			this.badLabel6.Name = "badLabel6";
			this.badLabel6.Size = new System.Drawing.Size(96, 23);
			this.badLabel6.TabIndex = 28;
			this.badLabel6.Text = "Database Type:";
			// 
			// txtDatabaseFile
			// 
			this.txtDatabaseFile.Image = null;
			this.txtDatabaseFile.Location = new System.Drawing.Point(36, 171);
			this.txtDatabaseFile.Name = "txtDatabaseFile";
			this.txtDatabaseFile.PasswordChar = '\0';
			this.txtDatabaseFile.Size = new System.Drawing.Size(275, 23);
			this.txtDatabaseFile.TabIndex = 26;
			// 
			// badLabel3
			// 
			this.badLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel3.Dimmed = false;
			this.badLabel3.Image = null;
			this.badLabel3.Location = new System.Drawing.Point(36, 234);
			this.badLabel3.Name = "badLabel3";
			this.badLabel3.Size = new System.Drawing.Size(75, 23);
			this.badLabel3.TabIndex = 24;
			this.badLabel3.Text = "Server Name:";
			// 
			// txtUID
			// 
			this.txtUID.Image = null;
			this.txtUID.Location = new System.Drawing.Point(162, 292);
			this.txtUID.Name = "txtUID";
			this.txtUID.PasswordChar = '\0';
			this.txtUID.Size = new System.Drawing.Size(85, 23);
			this.txtUID.TabIndex = 18;
			// 
			// txtPW
			// 
			this.txtPW.Image = null;
			this.txtPW.Location = new System.Drawing.Point(253, 292);
			this.txtPW.Name = "txtPW";
			this.txtPW.PasswordChar = '*';
			this.txtPW.Size = new System.Drawing.Size(85, 23);
			this.txtPW.TabIndex = 17;
			// 
			// txtServerName
			// 
			this.txtServerName.Image = null;
			this.txtServerName.Location = new System.Drawing.Point(162, 234);
			this.txtServerName.Name = "txtServerName";
			this.txtServerName.PasswordChar = '\0';
			this.txtServerName.Size = new System.Drawing.Size(176, 23);
			this.txtServerName.TabIndex = 23;
			// 
			// badLabel4
			// 
			this.badLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel4.Dimmed = false;
			this.badLabel4.Image = null;
			this.badLabel4.Location = new System.Drawing.Point(36, 263);
			this.badLabel4.Name = "badLabel4";
			this.badLabel4.Size = new System.Drawing.Size(96, 23);
			this.badLabel4.TabIndex = 22;
			this.badLabel4.Text = "Authentication:";
			// 
			// popupDbAuth
			// 
			this.popupDbAuth.Image = ((System.Drawing.Image)(resources.GetObject("popupDbAuth.Image")));
			this.popupDbAuth.Location = new System.Drawing.Point(162, 263);
			this.popupDbAuth.Name = "popupDbAuth";
			this.popupDbAuth.Size = new System.Drawing.Size(176, 23);
			this.popupDbAuth.TabIndex = 21;
			this.popupDbAuth.Click += new System.EventHandler(this.popupDbAuth_Click);
			// 
			// lblUIDPW
			// 
			this.lblUIDPW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblUIDPW.Dimmed = true;
			this.lblUIDPW.Image = null;
			this.lblUIDPW.Location = new System.Drawing.Point(36, 292);
			this.lblUIDPW.Name = "lblUIDPW";
			this.lblUIDPW.Size = new System.Drawing.Size(123, 23);
			this.lblUIDPW.TabIndex = 19;
			this.lblUIDPW.Text = "User Name / Password:";
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(36, 321);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(96, 23);
			this.badLabel1.TabIndex = 16;
			this.badLabel1.Text = "Select Database:";
			// 
			// popupDatabase
			// 
			this.popupDatabase.Image = ((System.Drawing.Image)(resources.GetObject("popupDatabase.Image")));
			this.popupDatabase.Location = new System.Drawing.Point(162, 321);
			this.popupDatabase.Name = "popupDatabase";
			this.popupDatabase.Size = new System.Drawing.Size(176, 23);
			this.popupDatabase.TabIndex = 15;
			this.popupDatabase.Click += new System.EventHandler(this.popupDatabase_Click);
			// 
			// cmdSQLiteDatabaseFile
			// 
			this.cmdSQLiteDatabaseFile.Image = null;
			this.cmdSQLiteDatabaseFile.Location = new System.Drawing.Point(317, 171);
			this.cmdSQLiteDatabaseFile.Name = "cmdSQLiteDatabaseFile";
			this.cmdSQLiteDatabaseFile.Size = new System.Drawing.Size(21, 23);
			this.cmdSQLiteDatabaseFile.TabIndex = 13;
			this.cmdSQLiteDatabaseFile.Text = "...";
			this.cmdSQLiteDatabaseFile.Click += new System.EventHandler(this.cmdSQLiteDatabaseFile_Click);
			// 
			// badSeperator1
			// 
			this.badSeperator1.BackColor = System.Drawing.Color.Transparent;
			this.badSeperator1.Direction = System.Windows.Forms.Orientation.Horizontal;
			this.badSeperator1.Image = null;
			this.badSeperator1.Location = new System.Drawing.Point(36, 202);
			this.badSeperator1.Name = "badSeperator1";
			this.badSeperator1.Size = new System.Drawing.Size(302, 26);
			this.badSeperator1.TabIndex = 12;
			this.badSeperator1.Text = "MS SQL Server";
			// 
			// btnNewDb
			// 
			this.btnNewDb.Image = null;
			this.btnNewDb.Location = new System.Drawing.Point(253, 103);
			this.btnNewDb.Name = "btnNewDb";
			this.btnNewDb.Size = new System.Drawing.Size(85, 23);
			this.btnNewDb.TabIndex = 11;
			this.btnNewDb.Text = "Create New";
			this.btnNewDb.Click += new System.EventHandler(this.btnNewDb_Click);
			// 
			// btnSave
			// 
			this.btnSave.Image = null;
			this.btnSave.Location = new System.Drawing.Point(281, 374);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 10;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
			// 
			// badSeperator2
			// 
			this.badSeperator2.BackColor = System.Drawing.Color.Transparent;
			this.badSeperator2.Direction = System.Windows.Forms.Orientation.Horizontal;
			this.badSeperator2.Image = null;
			this.badSeperator2.Location = new System.Drawing.Point(36, 127);
			this.badSeperator2.Name = "badSeperator2";
			this.badSeperator2.Size = new System.Drawing.Size(302, 23);
			this.badSeperator2.TabIndex = 9;
			this.badSeperator2.Text = "SQLite";
			// 
			// badLabel5
			// 
			this.badLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel5.Dimmed = false;
			this.badLabel5.Image = null;
			this.badLabel5.Location = new System.Drawing.Point(36, 151);
			this.badLabel5.Name = "badLabel5";
			this.badLabel5.Size = new System.Drawing.Size(75, 23);
			this.badLabel5.TabIndex = 27;
			this.badLabel5.Text = "Database File:";
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(12, 42);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(344, 320);
			this.badGroupBox1.TabIndex = 14;
			this.badGroupBox1.Text = "Settings";
			// 
			// DatabaseSetting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(378, 416);
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
    }
}