namespace WinApp.Forms.Settings
{
    partial class AppSettingsOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppSettingsOptions));
            this.folderBrowserDialogBackup = new System.Windows.Forms.FolderBrowserDialog();
            this.ddPeriod = new BadDropDownBox();
            this.badLabel6 = new BadLabel();
            this.badLabel5 = new BadLabel();
            this.chkTempBattleresultSave = new BadCheckBox();
            this.txtBackupFilePath = new BadTextBox();
            this.badLabel4 = new BadLabel();
            this.btnSelectBackupFilePath = new BadButton();
            this.btnRunBackup = new BadButton();
            this.badGroupBox3 = new BadGroupBox();
            this.badLabel3 = new BadLabel();
            this.ddHour = new BadDropDownBox();
            this.badLabel1 = new BadLabel();
            this.badLabel2 = new BadLabel();
            this.chkNotifyIconFormExitToMinimize = new BadCheckBox();
            this.chkNotifyIconUse = new BadCheckBox();
            this.badGroupBox2 = new BadGroupBox();
            this.badGroupBox1 = new BadGroupBox();
            this.btnCancel = new BadButton();
            this.btnSave = new BadButton();
            this.SuspendLayout();
            // 
            // ddPeriod
            // 
            this.ddPeriod.Image = null;
            this.ddPeriod.Location = new System.Drawing.Point(112, 143);
            this.ddPeriod.Name = "ddPeriod";
            this.ddPeriod.Size = new System.Drawing.Size(54, 26);
            this.ddPeriod.TabIndex = 38;
            this.ddPeriod.TextChanged += new System.EventHandler(this.ddPeriod_TextChanged);
            this.ddPeriod.Click += new System.EventHandler(this.ddPeriod_Click);
            // 
            // badLabel6
            // 
            this.badLabel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel6.Dimmed = false;
            this.badLabel6.Image = null;
            this.badLabel6.Location = new System.Drawing.Point(175, 145);
            this.badLabel6.Name = "badLabel6";
            this.badLabel6.Size = new System.Drawing.Size(259, 23);
            this.badLabel6.TabIndex = 37;
            this.badLabel6.Text = "( 0 = No automatic database backup)";
            this.badLabel6.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badLabel5
            // 
            this.badLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel5.Dimmed = false;
            this.badLabel5.Image = null;
            this.badLabel5.Location = new System.Drawing.Point(17, 145);
            this.badLabel5.Name = "badLabel5";
            this.badLabel5.Size = new System.Drawing.Size(81, 23);
            this.badLabel5.TabIndex = 36;
            this.badLabel5.Text = "Interval (days):";
            this.badLabel5.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // chkTempBattleresultSave
            // 
            this.chkTempBattleresultSave.BackColor = System.Drawing.Color.Transparent;
            this.chkTempBattleresultSave.Checked = false;
            this.chkTempBattleresultSave.Image = global::WinApp.Properties.Resources.checkboxcheck;
            this.chkTempBattleresultSave.Location = new System.Drawing.Point(-4, 271);
            this.chkTempBattleresultSave.Name = "chkTempBattleresultSave";
            this.chkTempBattleresultSave.Size = new System.Drawing.Size(288, 23);
            this.chkTempBattleresultSave.TabIndex = 35;
            this.chkTempBattleresultSave.Text = "Save battle result json file to AppData (until restart)";
            this.chkTempBattleresultSave.Click += new System.EventHandler(this.chkTempBattleresultSave_Click);
            // 
            // txtBackupFilePath
            // 
            this.txtBackupFilePath.HasFocus = false;
            this.txtBackupFilePath.HideBorder = false;
            this.txtBackupFilePath.Image = null;
            this.txtBackupFilePath.Location = new System.Drawing.Point(112, 114);
            this.txtBackupFilePath.MultilineAllow = false;
            this.txtBackupFilePath.Name = "txtBackupFilePath";
            this.txtBackupFilePath.PasswordChar = '\0';
            this.txtBackupFilePath.ReadOnly = false;
            this.txtBackupFilePath.Size = new System.Drawing.Size(228, 23);
            this.txtBackupFilePath.TabIndex = 32;
            this.txtBackupFilePath.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBackupFilePath.ToolTipText = "Leave blank for no backup";
            this.txtBackupFilePath.TextChanged += new System.EventHandler(this.txtBackupFilePath_TextChanged);
            // 
            // badLabel4
            // 
            this.badLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel4.Dimmed = false;
            this.badLabel4.Image = null;
            this.badLabel4.Location = new System.Drawing.Point(17, 114);
            this.badLabel4.Name = "badLabel4";
            this.badLabel4.Size = new System.Drawing.Size(96, 23);
            this.badLabel4.TabIndex = 34;
            this.badLabel4.Text = "Backup File Path:";
            this.badLabel4.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // btnSelectBackupFilePath
            // 
            this.btnSelectBackupFilePath.BlackButton = false;
            this.btnSelectBackupFilePath.Checked = false;
            this.btnSelectBackupFilePath.Image = null;
            this.btnSelectBackupFilePath.Location = new System.Drawing.Point(346, 114);
            this.btnSelectBackupFilePath.Name = "btnSelectBackupFilePath";
            this.btnSelectBackupFilePath.Size = new System.Drawing.Size(22, 23);
            this.btnSelectBackupFilePath.TabIndex = 33;
            this.btnSelectBackupFilePath.Text = "...";
            this.btnSelectBackupFilePath.ToolTipText = "";
            this.btnSelectBackupFilePath.Click += new System.EventHandler(this.btnSelectBackupFilePath_Click);
            // 
            // btnRunBackup
            // 
            this.btnRunBackup.BlackButton = false;
            this.btnRunBackup.Checked = false;
            this.btnRunBackup.Image = null;
            this.btnRunBackup.Location = new System.Drawing.Point(375, 114);
            this.btnRunBackup.Name = "btnRunBackup";
            this.btnRunBackup.Size = new System.Drawing.Size(59, 23);
            this.btnRunBackup.TabIndex = 28;
            this.btnRunBackup.Text = "Run";
            this.btnRunBackup.ToolTipText = "Perform Database Backup Now";
            this.btnRunBackup.Click += new System.EventHandler(this.btnRunBackup_Click);
            // 
            // badGroupBox3
            // 
            this.badGroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox3.Image = null;
            this.badGroupBox3.Location = new System.Drawing.Point(1, 94);
            this.badGroupBox3.Name = "badGroupBox3";
            this.badGroupBox3.Size = new System.Drawing.Size(445, 82);
            this.badGroupBox3.TabIndex = 27;
            this.badGroupBox3.Text = "Database Backup (Only for SQLite Database)";
            // 
            // badLabel3
            // 
            this.badLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel3.Dimmed = false;
            this.badLabel3.Image = null;
            this.badLabel3.Location = new System.Drawing.Point(17, 213);
            this.badLabel3.Name = "badLabel3";
            this.badLabel3.Size = new System.Drawing.Size(36, 23);
            this.badLabel3.TabIndex = 26;
            this.badLabel3.Text = "Hour";
            this.badLabel3.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // ddHour
            // 
            this.ddHour.Image = null;
            this.ddHour.Location = new System.Drawing.Point(59, 213);
            this.ddHour.Name = "ddHour";
            this.ddHour.Size = new System.Drawing.Size(54, 23);
            this.ddHour.TabIndex = 25;
            this.ddHour.TextChanged += new System.EventHandler(this.ddHour_TextChanged);
            this.ddHour.Click += new System.EventHandler(this.ddHour_Click);
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(126, 213);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(242, 23);
            this.badLabel1.TabIndex = 24;
            this.badLabel1.Text = "Normally set at WoT server reset";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(17, 47);
            this.badLabel2.Name = "badLabel2";
            this.badLabel2.Size = new System.Drawing.Size(242, 23);
            this.badLabel2.TabIndex = 23;
            this.badLabel2.Text = "Changes will apply for next application startup";
            this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // chkNotifyIconFormExitToMinimize
            // 
            this.chkNotifyIconFormExitToMinimize.BackColor = System.Drawing.Color.Transparent;
            this.chkNotifyIconFormExitToMinimize.Checked = false;
            this.chkNotifyIconFormExitToMinimize.Image = ((System.Drawing.Image)(resources.GetObject("chkNotifyIconFormExitToMinimize.Image")));
            this.chkNotifyIconFormExitToMinimize.Location = new System.Drawing.Point(163, 23);
            this.chkNotifyIconFormExitToMinimize.Name = "chkNotifyIconFormExitToMinimize";
            this.chkNotifyIconFormExitToMinimize.Size = new System.Drawing.Size(268, 23);
            this.chkNotifyIconFormExitToMinimize.TabIndex = 22;
            this.chkNotifyIconFormExitToMinimize.Text = "Minimize  when closing application";
            this.chkNotifyIconFormExitToMinimize.Click += new System.EventHandler(this.chkNotifyIconFormExitToMinimize_Click);
            // 
            // chkNotifyIconUse
            // 
            this.chkNotifyIconUse.BackColor = System.Drawing.Color.Transparent;
            this.chkNotifyIconUse.Checked = false;
            this.chkNotifyIconUse.Image = ((System.Drawing.Image)(resources.GetObject("chkNotifyIconUse.Image")));
            this.chkNotifyIconUse.Location = new System.Drawing.Point(17, 23);
            this.chkNotifyIconUse.Name = "chkNotifyIconUse";
            this.chkNotifyIconUse.Size = new System.Drawing.Size(140, 23);
            this.chkNotifyIconUse.TabIndex = 21;
            this.chkNotifyIconUse.Text = "Use system tray icon";
            this.chkNotifyIconUse.Click += new System.EventHandler(this.chkNotifyIconUse_Click);
            // 
            // badGroupBox2
            // 
            this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox2.Image = null;
            this.badGroupBox2.Location = new System.Drawing.Point(1, 1);
            this.badGroupBox2.Name = "badGroupBox2";
            this.badGroupBox2.Size = new System.Drawing.Size(445, 79);
            this.badGroupBox2.TabIndex = 20;
            this.badGroupBox2.Text = "Application Behavior";
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(1, 189);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(445, 66);
            this.badGroupBox1.TabIndex = 15;
            this.badGroupBox1.Text = "Set Hour for New Day Start";
            // 
            // btnCancel
            // 
            this.btnCancel.BlackButton = false;
            this.btnCancel.Checked = false;
            this.btnCancel.Image = null;
            this.btnCancel.Location = new System.Drawing.Point(375, 271);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTipText = "";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BlackButton = false;
            this.btnSave.Checked = false;
            this.btnSave.Image = null;
            this.btnSave.Location = new System.Drawing.Point(298, 271);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.ToolTipText = "";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AppSettingsOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.ddPeriod);
            this.Controls.Add(this.badLabel6);
            this.Controls.Add(this.badLabel5);
            this.Controls.Add(this.chkTempBattleresultSave);
            this.Controls.Add(this.txtBackupFilePath);
            this.Controls.Add(this.badLabel4);
            this.Controls.Add(this.btnSelectBackupFilePath);
            this.Controls.Add(this.btnRunBackup);
            this.Controls.Add(this.badGroupBox3);
            this.Controls.Add(this.badLabel3);
            this.Controls.Add(this.ddHour);
            this.Controls.Add(this.badLabel1);
            this.Controls.Add(this.badLabel2);
            this.Controls.Add(this.chkNotifyIconFormExitToMinimize);
            this.Controls.Add(this.chkNotifyIconUse);
            this.Controls.Add(this.badGroupBox2);
            this.Controls.Add(this.badGroupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Name = "AppSettingsOptions";
            this.Size = new System.Drawing.Size(460, 307);
            this.Load += new System.EventHandler(this.AppSettingsLayout_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BadLabel badLabel2;
        private BadCheckBox chkNotifyIconFormExitToMinimize;
        private BadCheckBox chkNotifyIconUse;
        private BadGroupBox badGroupBox2;
        private BadGroupBox badGroupBox1;
        private BadButton btnCancel;
        private BadButton btnSave;
        private BadLabel badLabel1;
        private BadDropDownBox ddHour;
        private BadLabel badLabel3;
        private BadGroupBox badGroupBox3;
        private BadButton btnRunBackup;
        private BadTextBox txtBackupFilePath;
        private BadLabel badLabel4;
        private BadButton btnSelectBackupFilePath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogBackup;
        private BadCheckBox chkTempBattleresultSave;
        private BadLabel badLabel5;
        private BadLabel badLabel6;
        private BadDropDownBox ddPeriod;
    }
}
