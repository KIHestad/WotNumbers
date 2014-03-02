namespace WotDBUpdater
{
    partial class frmDatabaseSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtConnStr = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.lblPwd = new System.Windows.Forms.Label();
            this.txtUid = new System.Windows.Forms.TextBox();
            this.lblUid = new System.Windows.Forms.Label();
            this.rbSqlAuth = new System.Windows.Forms.RadioButton();
            this.rbWinAuth = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.txtServerType = new System.Windows.Forms.TextBox();
            this.btnChangeServerType = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnNewDatabase = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cboDatabaseName = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 383);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Database Connection String:";
            // 
            // txtConnStr
            // 
            this.txtConnStr.Enabled = false;
            this.txtConnStr.Location = new System.Drawing.Point(9, 402);
            this.txtConnStr.Multiline = true;
            this.txtConnStr.Name = "txtConnStr";
            this.txtConnStr.Size = new System.Drawing.Size(313, 39);
            this.txtConnStr.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(251, 343);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 25);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPwd);
            this.groupBox1.Controls.Add(this.lblPwd);
            this.groupBox1.Controls.Add(this.txtUid);
            this.groupBox1.Controls.Add(this.lblUid);
            this.groupBox1.Controls.Add(this.rbSqlAuth);
            this.groupBox1.Controls.Add(this.rbWinAuth);
            this.groupBox1.Location = new System.Drawing.Point(15, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 139);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database source and login";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(112, 105);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(179, 20);
            this.txtPwd.TabIndex = 5;
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.Location = new System.Drawing.Point(40, 109);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(56, 13);
            this.lblPwd.TabIndex = 4;
            this.lblPwd.Text = "Password:";
            // 
            // txtUid
            // 
            this.txtUid.Location = new System.Drawing.Point(112, 79);
            this.txtUid.Name = "txtUid";
            this.txtUid.Size = new System.Drawing.Size(179, 20);
            this.txtUid.TabIndex = 3;
            // 
            // lblUid
            // 
            this.lblUid.AutoSize = true;
            this.lblUid.Location = new System.Drawing.Point(40, 83);
            this.lblUid.Name = "lblUid";
            this.lblUid.Size = new System.Drawing.Size(61, 13);
            this.lblUid.TabIndex = 2;
            this.lblUid.Text = "User name:";
            // 
            // rbSqlAuth
            // 
            this.rbSqlAuth.AutoSize = true;
            this.rbSqlAuth.Location = new System.Drawing.Point(19, 46);
            this.rbSqlAuth.Name = "rbSqlAuth";
            this.rbSqlAuth.Size = new System.Drawing.Size(173, 17);
            this.rbSqlAuth.TabIndex = 1;
            this.rbSqlAuth.TabStop = true;
            this.rbSqlAuth.Text = "Use SQL Server Authentication";
            this.rbSqlAuth.UseVisualStyleBackColor = true;
            this.rbSqlAuth.CheckedChanged += new System.EventHandler(this.rbSqlAuth_CheckedChanged);
            // 
            // rbWinAuth
            // 
            this.rbWinAuth.AutoSize = true;
            this.rbWinAuth.Location = new System.Drawing.Point(19, 23);
            this.rbWinAuth.Name = "rbWinAuth";
            this.rbWinAuth.Size = new System.Drawing.Size(162, 17);
            this.rbWinAuth.TabIndex = 0;
            this.rbWinAuth.TabStop = true;
            this.rbWinAuth.Text = "Use Windows Authentication";
            this.rbWinAuth.UseVisualStyleBackColor = true;
            this.rbWinAuth.CheckedChanged += new System.EventHandler(this.rbWinAuth_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database server type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Database sever name:";
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(15, 72);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(310, 20);
            this.txtServerName.TabIndex = 3;
            // 
            // txtServerType
            // 
            this.txtServerType.BackColor = System.Drawing.SystemColors.Window;
            this.txtServerType.Enabled = false;
            this.txtServerType.Location = new System.Drawing.Point(15, 27);
            this.txtServerType.Name = "txtServerType";
            this.txtServerType.Size = new System.Drawing.Size(245, 20);
            this.txtServerType.TabIndex = 4;
            this.txtServerType.Text = "Microsoft SQL Sever (SqlClient)";
            // 
            // btnChangeServerType
            // 
            this.btnChangeServerType.Enabled = false;
            this.btnChangeServerType.Location = new System.Drawing.Point(266, 25);
            this.btnChangeServerType.Name = "btnChangeServerType";
            this.btnChangeServerType.Size = new System.Drawing.Size(59, 23);
            this.btnChangeServerType.TabIndex = 5;
            this.btnChangeServerType.Text = "Change";
            this.btnChangeServerType.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnNewDatabase);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cboDatabaseName);
            this.groupBox2.Location = new System.Drawing.Point(15, 243);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 90);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database";
            // 
            // btnNewDatabase
            // 
            this.btnNewDatabase.Location = new System.Drawing.Point(209, 50);
            this.btnNewDatabase.Name = "btnNewDatabase";
            this.btnNewDatabase.Size = new System.Drawing.Size(82, 25);
            this.btnNewDatabase.TabIndex = 7;
            this.btnNewDatabase.Text = "Create new";
            this.btnNewDatabase.UseVisualStyleBackColor = true;
            this.btnNewDatabase.Click += new System.EventHandler(this.btnNewDatabase_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Select database:";
            // 
            // cboDatabaseName
            // 
            this.cboDatabaseName.FormattingEnabled = true;
            this.cboDatabaseName.Location = new System.Drawing.Point(112, 23);
            this.cboDatabaseName.Name = "cboDatabaseName";
            this.cboDatabaseName.Size = new System.Drawing.Size(179, 21);
            this.cboDatabaseName.TabIndex = 0;
            this.cboDatabaseName.SelectedIndexChanged += new System.EventHandler(this.cboDatabaseName_SelectedIndexChanged);
            this.cboDatabaseName.Click += new System.EventHandler(this.cboDatabaseName_Click);
            // 
            // frmDatabaseSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 454);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnChangeServerType);
            this.Controls.Add(this.txtServerType);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConnStr);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDatabaseSetting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database Settings";
            this.Load += new System.EventHandler(this.frmDatabaseSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConnStr;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
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
        private System.Windows.Forms.Button btnChangeServerType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnNewDatabase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboDatabaseName;
    }
}