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
			this.label3 = new System.Windows.Forms.Label();
			this.txtServerName = new System.Windows.Forms.TextBox();
			this.badForm1 = new BadForm();
			this.cmdSQLiteDatabaseFile = new BadButton();
			this.badSeperator1 = new BadSeperator();
			this.txtSQLiteDatabaseFile = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnNewDb = new BadButton();
			this.btnSave = new BadButton();
			this.badSeperator2 = new BadSeperator();
			this.txtPwd = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cboDatabaseName = new System.Windows.Forms.ComboBox();
			this.lblPwd = new System.Windows.Forms.Label();
			this.rbSqlAuth = new System.Windows.Forms.RadioButton();
			this.txtUid = new System.Windows.Forms.TextBox();
			this.rbWinAuth = new System.Windows.Forms.RadioButton();
			this.lblUid = new System.Windows.Forms.Label();
			this.openFileDialogSQLite = new System.Windows.Forms.OpenFileDialog();
			this.badForm1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.label3.Location = new System.Drawing.Point(22, 170);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(195, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "MS SQL Server Database Sever Name:";
			// 
			// txtServerName
			// 
			this.txtServerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.txtServerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtServerName.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.txtServerName.Location = new System.Drawing.Point(25, 186);
			this.txtServerName.Name = "txtServerName";
			this.txtServerName.Size = new System.Drawing.Size(272, 20);
			this.txtServerName.TabIndex = 3;
			this.txtServerName.TextChanged += new System.EventHandler(this.txtServerName_TextChanged);
			// 
			// badForm1
			// 
			this.badForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badForm1.Controls.Add(this.cmdSQLiteDatabaseFile);
			this.badForm1.Controls.Add(this.badSeperator1);
			this.badForm1.Controls.Add(this.txtSQLiteDatabaseFile);
			this.badForm1.Controls.Add(this.label1);
			this.badForm1.Controls.Add(this.btnNewDb);
			this.badForm1.Controls.Add(this.btnSave);
			this.badForm1.Controls.Add(this.badSeperator2);
			this.badForm1.Controls.Add(this.txtPwd);
			this.badForm1.Controls.Add(this.label6);
			this.badForm1.Controls.Add(this.cboDatabaseName);
			this.badForm1.Controls.Add(this.lblPwd);
			this.badForm1.Controls.Add(this.rbSqlAuth);
			this.badForm1.Controls.Add(this.txtUid);
			this.badForm1.Controls.Add(this.rbWinAuth);
			this.badForm1.Controls.Add(this.lblUid);
			this.badForm1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.badForm1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.badForm1.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.badForm1.FormFooter = false;
			this.badForm1.FormFooterHeight = 26;
			this.badForm1.FormMargin = 0;
			this.badForm1.Image = null;
			this.badForm1.Location = new System.Drawing.Point(0, 0);
			this.badForm1.MainArea = mainAreaClass1;
			this.badForm1.Name = "badForm1";
			this.badForm1.Resizable = true;
			this.badForm1.Size = new System.Drawing.Size(327, 470);
			this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
			this.badForm1.SystemMaximizeImage = null;
			this.badForm1.SystemMinimizeImage = null;
			this.badForm1.TabIndex = 7;
			this.badForm1.Text = "Database Settings";
			this.badForm1.TitleHeight = 26;
			// 
			// cmdSQLiteDatabaseFile
			// 
			this.cmdSQLiteDatabaseFile.Image = null;
			this.cmdSQLiteDatabaseFile.Location = new System.Drawing.Point(219, 95);
			this.cmdSQLiteDatabaseFile.Name = "cmdSQLiteDatabaseFile";
			this.cmdSQLiteDatabaseFile.Size = new System.Drawing.Size(75, 23);
			this.cmdSQLiteDatabaseFile.TabIndex = 13;
			this.cmdSQLiteDatabaseFile.Text = "Select file";
			this.cmdSQLiteDatabaseFile.Click += new System.EventHandler(this.cmdSQLiteDatabaseFile_Click);
			// 
			// badSeperator1
			// 
			this.badSeperator1.BackColor = System.Drawing.Color.Transparent;
			this.badSeperator1.Direction = System.Windows.Forms.Orientation.Horizontal;
			this.badSeperator1.Image = null;
			this.badSeperator1.Location = new System.Drawing.Point(24, 124);
			this.badSeperator1.Name = "badSeperator1";
			this.badSeperator1.Size = new System.Drawing.Size(270, 23);
			this.badSeperator1.TabIndex = 12;
			this.badSeperator1.Text = "badSeperator1";
			// 
			// txtSQLiteDatabaseFile
			// 
			this.txtSQLiteDatabaseFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.txtSQLiteDatabaseFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSQLiteDatabaseFile.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.txtSQLiteDatabaseFile.Location = new System.Drawing.Point(24, 67);
			this.txtSQLiteDatabaseFile.Name = "txtSQLiteDatabaseFile";
			this.txtSQLiteDatabaseFile.Size = new System.Drawing.Size(272, 20);
			this.txtSQLiteDatabaseFile.TabIndex = 8;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.label1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.label1.Location = new System.Drawing.Point(23, 47);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(110, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "SQLite Database File:";
			// 
			// btnNewDb
			// 
			this.btnNewDb.Image = null;
			this.btnNewDb.Location = new System.Drawing.Point(204, 368);
			this.btnNewDb.Name = "btnNewDb";
			this.btnNewDb.Size = new System.Drawing.Size(93, 23);
			this.btnNewDb.TabIndex = 11;
			this.btnNewDb.Text = "Create New";
			this.btnNewDb.Click += new System.EventHandler(this.btnNewDb_Click);
			// 
			// btnSave
			// 
			this.btnSave.Image = null;
			this.btnSave.Location = new System.Drawing.Point(222, 426);
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
			this.badSeperator2.Location = new System.Drawing.Point(24, 397);
			this.badSeperator2.Name = "badSeperator2";
			this.badSeperator2.Size = new System.Drawing.Size(273, 23);
			this.badSeperator2.TabIndex = 9;
			this.badSeperator2.Text = "badSeperator2";
			// 
			// txtPwd
			// 
			this.txtPwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPwd.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.txtPwd.Location = new System.Drawing.Point(118, 297);
			this.txtPwd.Name = "txtPwd";
			this.txtPwd.PasswordChar = '*';
			this.txtPwd.Size = new System.Drawing.Size(179, 20);
			this.txtPwd.TabIndex = 5;
			this.txtPwd.TextChanged += new System.EventHandler(this.txtPwd_TextChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(22, 332);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(87, 13);
			this.label6.TabIndex = 1;
			this.label6.Text = "Select database:";
			// 
			// cboDatabaseName
			// 
			this.cboDatabaseName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.cboDatabaseName.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.cboDatabaseName.FormattingEnabled = true;
			this.cboDatabaseName.Location = new System.Drawing.Point(118, 329);
			this.cboDatabaseName.Name = "cboDatabaseName";
			this.cboDatabaseName.Size = new System.Drawing.Size(179, 21);
			this.cboDatabaseName.TabIndex = 0;
			this.cboDatabaseName.Enter += new System.EventHandler(this.cboDatabaseName_Enter);
			// 
			// lblPwd
			// 
			this.lblPwd.AutoSize = true;
			this.lblPwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblPwd.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.lblPwd.Location = new System.Drawing.Point(46, 301);
			this.lblPwd.Name = "lblPwd";
			this.lblPwd.Size = new System.Drawing.Size(56, 13);
			this.lblPwd.TabIndex = 4;
			this.lblPwd.Text = "Password:";
			// 
			// rbSqlAuth
			// 
			this.rbSqlAuth.AutoSize = true;
			this.rbSqlAuth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.rbSqlAuth.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.rbSqlAuth.Location = new System.Drawing.Point(25, 238);
			this.rbSqlAuth.Name = "rbSqlAuth";
			this.rbSqlAuth.Size = new System.Drawing.Size(173, 17);
			this.rbSqlAuth.TabIndex = 1;
			this.rbSqlAuth.TabStop = true;
			this.rbSqlAuth.Text = "Use SQL Server Authentication";
			this.rbSqlAuth.UseVisualStyleBackColor = false;
			this.rbSqlAuth.CheckedChanged += new System.EventHandler(this.rbSqlAuth_CheckedChanged);
			// 
			// txtUid
			// 
			this.txtUid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.txtUid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtUid.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.txtUid.Location = new System.Drawing.Point(118, 271);
			this.txtUid.Name = "txtUid";
			this.txtUid.Size = new System.Drawing.Size(179, 20);
			this.txtUid.TabIndex = 3;
			this.txtUid.TextChanged += new System.EventHandler(this.txtUid_TextChanged);
			// 
			// rbWinAuth
			// 
			this.rbWinAuth.AutoSize = true;
			this.rbWinAuth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.rbWinAuth.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.rbWinAuth.Location = new System.Drawing.Point(25, 215);
			this.rbWinAuth.Name = "rbWinAuth";
			this.rbWinAuth.Size = new System.Drawing.Size(162, 17);
			this.rbWinAuth.TabIndex = 0;
			this.rbWinAuth.TabStop = true;
			this.rbWinAuth.Text = "Use Windows Authentication";
			this.rbWinAuth.UseVisualStyleBackColor = false;
			this.rbWinAuth.CheckedChanged += new System.EventHandler(this.rbWinAuth_CheckedChanged);
			// 
			// lblUid
			// 
			this.lblUid.AutoSize = true;
			this.lblUid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblUid.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.lblUid.Location = new System.Drawing.Point(46, 275);
			this.lblUid.Name = "lblUid";
			this.lblUid.Size = new System.Drawing.Size(61, 13);
			this.lblUid.TabIndex = 2;
			this.lblUid.Text = "User name:";
			// 
			// openFileDialogSQLite
			// 
			this.openFileDialogSQLite.FileName = "openFileDialog1";
			// 
			// DatabaseSetting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(327, 470);
			this.Controls.Add(this.txtServerName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.badForm1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DatabaseSetting";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Database Settings";
			this.Load += new System.EventHandler(this.frmDatabaseSetting_Load);
			this.badForm1.ResumeLayout(false);
			this.badForm1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.TextBox txtUid;
        private System.Windows.Forms.Label lblUid;
        private System.Windows.Forms.RadioButton rbSqlAuth;
		private System.Windows.Forms.RadioButton rbWinAuth;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboDatabaseName;
		private BadForm badForm1;
		private BadButton btnSave;
		private BadSeperator badSeperator2;
		private BadButton btnNewDb;
		private System.Windows.Forms.TextBox txtSQLiteDatabaseFile;
		private System.Windows.Forms.Label label1;
		private BadButton cmdSQLiteDatabaseFile;
		private BadSeperator badSeperator1;
		private System.Windows.Forms.OpenFileDialog openFileDialogSQLite;
    }
}