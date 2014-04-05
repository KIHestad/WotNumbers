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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseSetting));
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtServerName = new System.Windows.Forms.TextBox();
			this.txtServerType = new System.Windows.Forms.TextBox();
			this.badForm1 = new BadForm();
			this.btnNewDb = new BadButton();
			this.btnSave = new BadButton();
			this.badSeperator2 = new BadSeperator();
			this.badSeperator1 = new BadSeperator();
			this.txtPwd = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cboDatabaseName = new System.Windows.Forms.ComboBox();
			this.lblPwd = new System.Windows.Forms.Label();
			this.rbSqlAuth = new System.Windows.Forms.RadioButton();
			this.txtUid = new System.Windows.Forms.TextBox();
			this.rbWinAuth = new System.Windows.Forms.RadioButton();
			this.lblUid = new System.Windows.Forms.Label();
			this.badForm1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.label2.Location = new System.Drawing.Point(22, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(111, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Database server type:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.label3.Location = new System.Drawing.Point(22, 90);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(114, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Database sever name:";
			// 
			// txtServerName
			// 
			this.txtServerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.txtServerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtServerName.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.txtServerName.Location = new System.Drawing.Point(25, 106);
			this.txtServerName.Name = "txtServerName";
			this.txtServerName.Size = new System.Drawing.Size(272, 20);
			this.txtServerName.TabIndex = 3;
			this.txtServerName.TextChanged += new System.EventHandler(this.txtServerName_TextChanged);
			// 
			// txtServerType
			// 
			this.txtServerType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.txtServerType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtServerType.Enabled = false;
			this.txtServerType.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.txtServerType.Location = new System.Drawing.Point(25, 61);
			this.txtServerType.Name = "txtServerType";
			this.txtServerType.Size = new System.Drawing.Size(272, 20);
			this.txtServerType.TabIndex = 4;
			this.txtServerType.Text = "Microsoft SQL Sever (SqlClient)";
			// 
			// badForm1
			// 
			this.badForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badForm1.Controls.Add(this.btnNewDb);
			this.badForm1.Controls.Add(this.btnSave);
			this.badForm1.Controls.Add(this.badSeperator2);
			this.badForm1.Controls.Add(this.badSeperator1);
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
			this.badForm1.Name = "badForm1";
			this.badForm1.Resizable = true;
			this.badForm1.Size = new System.Drawing.Size(327, 426);
			this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
			this.badForm1.SystemMaximizeImage = null;
			this.badForm1.SystemMinimizeImage = null;
			this.badForm1.TabIndex = 7;
			this.badForm1.Text = "Database Settings";
			this.badForm1.TitleHeight = 26;
			// 
			// btnNewDb
			// 
			this.btnNewDb.Image = null;
			this.btnNewDb.Location = new System.Drawing.Point(204, 318);
			this.btnNewDb.Name = "btnNewDb";
			this.btnNewDb.Size = new System.Drawing.Size(93, 23);
			this.btnNewDb.TabIndex = 11;
			this.btnNewDb.Text = "Create New";
			this.btnNewDb.Click += new System.EventHandler(this.btnNewDb_Click);
			// 
			// btnSave
			// 
			this.btnSave.Image = null;
			this.btnSave.Location = new System.Drawing.Point(222, 376);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 10;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
			// 
			// badSeperator2
			// 
			this.badSeperator2.BackColor = System.Drawing.Color.Transparent;
			this.badSeperator2.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.badSeperator2.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.badSeperator2.Direction = System.Windows.Forms.Orientation.Horizontal;
			this.badSeperator2.Image = null;
			this.badSeperator2.Location = new System.Drawing.Point(24, 347);
			this.badSeperator2.Name = "badSeperator2";
			this.badSeperator2.Size = new System.Drawing.Size(273, 23);
			this.badSeperator2.TabIndex = 9;
			this.badSeperator2.Text = "badSeperator2";
			// 
			// badSeperator1
			// 
			this.badSeperator1.BackColor = System.Drawing.Color.Transparent;
			this.badSeperator1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.badSeperator1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.badSeperator1.Direction = System.Windows.Forms.Orientation.Horizontal;
			this.badSeperator1.Image = null;
			this.badSeperator1.Location = new System.Drawing.Point(24, 251);
			this.badSeperator1.Name = "badSeperator1";
			this.badSeperator1.Size = new System.Drawing.Size(273, 25);
			this.badSeperator1.TabIndex = 8;
			this.badSeperator1.Text = "badSeperator1";
			// 
			// txtPwd
			// 
			this.txtPwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPwd.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.txtPwd.Location = new System.Drawing.Point(118, 225);
			this.txtPwd.Name = "txtPwd";
			this.txtPwd.PasswordChar = '*';
			this.txtPwd.Size = new System.Drawing.Size(179, 20);
			this.txtPwd.TabIndex = 5;
			this.txtPwd.TextChanged += new System.EventHandler(this.txtPwd_TextChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(22, 282);
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
			this.cboDatabaseName.Location = new System.Drawing.Point(118, 279);
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
			this.lblPwd.Location = new System.Drawing.Point(46, 229);
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
			this.rbSqlAuth.Location = new System.Drawing.Point(25, 166);
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
			this.txtUid.Location = new System.Drawing.Point(118, 199);
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
			this.rbWinAuth.Location = new System.Drawing.Point(25, 143);
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
			this.lblUid.Location = new System.Drawing.Point(46, 203);
			this.lblUid.Name = "lblUid";
			this.lblUid.Size = new System.Drawing.Size(61, 13);
			this.lblUid.TabIndex = 2;
			this.lblUid.Text = "User name:";
			// 
			// DatabaseSetting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(327, 426);
			this.Controls.Add(this.txtServerType);
			this.Controls.Add(this.txtServerName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServerName;
		private System.Windows.Forms.TextBox txtServerType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboDatabaseName;
		private BadForm badForm1;
		private BadButton btnSave;
		private BadSeperator badSeperator2;
		private BadSeperator badSeperator1;
		private BadButton btnNewDb;
    }
}