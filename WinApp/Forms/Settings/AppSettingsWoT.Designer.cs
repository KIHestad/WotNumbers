namespace WinApp.Forms.Settings
{
    partial class AppSettingsWoT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppSettingsWoT));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblBRRStatus = new BadLabel();
            this.badLabel5 = new BadLabel();
            this.badLabel3 = new BadLabel();
            this.txtResModsSubFolder = new BadTextBox();
            this.badLabel2 = new BadLabel();
            this.cmdHelp = new BadButton();
            this.btnBrrInstall = new BadButton();
            this.chkBrrStarupCheck = new BadCheckBox();
            this.btnFile = new BadButton();
            this.txtBatchFile = new BadTextBox();
            this.badLabel4 = new BadLabel();
            this.chkAutoRun = new BadCheckBox();
            this.btnCancel = new BadButton();
            this.btnSave = new BadButton();
            this.chkCore7 = new BadCheckBox();
            this.chkCore6 = new BadCheckBox();
            this.chkCore5 = new BadCheckBox();
            this.chkCore4 = new BadCheckBox();
            this.chkOptimizeOn = new BadCheckBox();
            this.btnFolder = new BadButton();
            this.txtFolder = new BadTextBox();
            this.badLabel1 = new BadLabel();
            this.ddStartApp = new BadDropDownBox();
            this.badGroupBox1 = new BadGroupBox();
            this.chkCore3 = new BadCheckBox();
            this.chkCore2 = new BadCheckBox();
            this.chkCore1 = new BadCheckBox();
            this.chkCore0 = new BadCheckBox();
            this.badGroupBox2 = new BadGroupBox();
            this.badSeperator1 = new BadSeperator();
            this.badGroupBox3 = new BadGroupBox();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblBRRStatus
            // 
            this.lblBRRStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblBRRStatus.Dimmed = false;
            this.lblBRRStatus.Image = null;
            this.lblBRRStatus.Location = new System.Drawing.Point(19, 95);
            this.lblBRRStatus.Name = "lblBRRStatus";
            this.lblBRRStatus.Size = new System.Drawing.Size(109, 23);
            this.lblBRRStatus.TabIndex = 61;
            this.lblBRRStatus.Text = "BRR Status";
            this.lblBRRStatus.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badLabel5
            // 
            this.badLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel5.Dimmed = false;
            this.badLabel5.Image = null;
            this.badLabel5.Location = new System.Drawing.Point(55, 279);
            this.badLabel5.Name = "badLabel5";
            this.badLabel5.Size = new System.Drawing.Size(75, 23);
            this.badLabel5.TabIndex = 60;
            this.badLabel5.Text = "badLabel5";
            this.badLabel5.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badLabel3
            // 
            this.badLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel3.Dimmed = false;
            this.badLabel3.Image = null;
            this.badLabel3.Location = new System.Drawing.Point(18, 24);
            this.badLabel3.Name = "badLabel3";
            this.badLabel3.Size = new System.Drawing.Size(74, 23);
            this.badLabel3.TabIndex = 58;
            this.badLabel3.Text = "Install Folder:";
            this.badLabel3.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // txtResModsSubFolder
            // 
            this.txtResModsSubFolder.HasFocus = false;
            this.txtResModsSubFolder.HideBorder = false;
            this.txtResModsSubFolder.Image = null;
            this.txtResModsSubFolder.Location = new System.Drawing.Point(134, 53);
            this.txtResModsSubFolder.MultilineAllow = false;
            this.txtResModsSubFolder.Name = "txtResModsSubFolder";
            this.txtResModsSubFolder.PasswordChar = '\0';
            this.txtResModsSubFolder.ReadOnly = false;
            this.txtResModsSubFolder.Size = new System.Drawing.Size(73, 23);
            this.txtResModsSubFolder.TabIndex = 57;
            this.txtResModsSubFolder.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtResModsSubFolder.ToolTipText = "Current subfolder to res_mods-folder, normally the same as WoT version";
            this.txtResModsSubFolder.TextChanged += new System.EventHandler(this.EditChangesApply);
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(19, 53);
            this.badLabel2.Name = "badLabel2";
            this.badLabel2.Size = new System.Drawing.Size(116, 23);
            this.badLabel2.TabIndex = 56;
            this.badLabel2.Text = "res_mods - Subfolder:";
            this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // cmdHelp
            // 
            this.cmdHelp.BlackButton = false;
            this.cmdHelp.Checked = false;
            this.cmdHelp.Image = null;
            this.cmdHelp.Location = new System.Drawing.Point(220, 271);
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Size = new System.Drawing.Size(70, 23);
            this.cmdHelp.TabIndex = 55;
            this.cmdHelp.Text = "Help";
            this.cmdHelp.ToolTipText = "";
            this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
            // 
            // btnBrrInstall
            // 
            this.btnBrrInstall.BlackButton = false;
            this.btnBrrInstall.Checked = false;
            this.btnBrrInstall.Image = null;
            this.btnBrrInstall.Location = new System.Drawing.Point(134, 95);
            this.btnBrrInstall.Name = "btnBrrInstall";
            this.btnBrrInstall.Size = new System.Drawing.Size(73, 23);
            this.btnBrrInstall.TabIndex = 37;
            this.btnBrrInstall.Text = "Install";
            this.btnBrrInstall.ToolTipText = "";
            this.btnBrrInstall.Click += new System.EventHandler(this.btnBrrInstall_Click);
            // 
            // chkBrrStarupCheck
            // 
            this.chkBrrStarupCheck.BackColor = System.Drawing.Color.Transparent;
            this.chkBrrStarupCheck.Checked = false;
            this.chkBrrStarupCheck.Image = ((System.Drawing.Image)(resources.GetObject("chkBrrStarupCheck.Image")));
            this.chkBrrStarupCheck.Location = new System.Drawing.Point(277, 95);
            this.chkBrrStarupCheck.Name = "chkBrrStarupCheck";
            this.chkBrrStarupCheck.Size = new System.Drawing.Size(157, 23);
            this.chkBrrStarupCheck.TabIndex = 36;
            this.chkBrrStarupCheck.Text = "Check for BRR on Startup";
            this.chkBrrStarupCheck.Click += new System.EventHandler(this.EditChangesApply);
            // 
            // btnFile
            // 
            this.btnFile.BlackButton = false;
            this.btnFile.Checked = false;
            this.btnFile.Image = null;
            this.btnFile.Location = new System.Drawing.Point(247, 197);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(24, 23);
            this.btnFile.TabIndex = 41;
            this.btnFile.Text = "...";
            this.btnFile.ToolTipText = "";
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // txtBatchFile
            // 
            this.txtBatchFile.HasFocus = false;
            this.txtBatchFile.HideBorder = false;
            this.txtBatchFile.Image = null;
            this.txtBatchFile.Location = new System.Drawing.Point(69, 197);
            this.txtBatchFile.MultilineAllow = false;
            this.txtBatchFile.Name = "txtBatchFile";
            this.txtBatchFile.PasswordChar = '\0';
            this.txtBatchFile.ReadOnly = false;
            this.txtBatchFile.Size = new System.Drawing.Size(172, 23);
            this.txtBatchFile.TabIndex = 40;
            this.txtBatchFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBatchFile.ToolTipText = "Additional batch file or program to run";
            this.txtBatchFile.TextChanged += new System.EventHandler(this.EditChangesApply);
            // 
            // badLabel4
            // 
            this.badLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel4.Dimmed = false;
            this.badLabel4.Image = null;
            this.badLabel4.Location = new System.Drawing.Point(18, 197);
            this.badLabel4.Name = "badLabel4";
            this.badLabel4.Size = new System.Drawing.Size(43, 23);
            this.badLabel4.TabIndex = 54;
            this.badLabel4.Text = "Run";
            this.badLabel4.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // chkAutoRun
            // 
            this.chkAutoRun.BackColor = System.Drawing.Color.Transparent;
            this.chkAutoRun.Checked = false;
            this.chkAutoRun.Image = ((System.Drawing.Image)(resources.GetObject("chkAutoRun.Image")));
            this.chkAutoRun.Location = new System.Drawing.Point(16, 224);
            this.chkAutoRun.Name = "chkAutoRun";
            this.chkAutoRun.Size = new System.Drawing.Size(119, 23);
            this.chkAutoRun.TabIndex = 42;
            this.chkAutoRun.Text = "Auto WoT startup";
            this.chkAutoRun.Click += new System.EventHandler(this.EditChangesApply);
            // 
            // btnCancel
            // 
            this.btnCancel.BlackButton = false;
            this.btnCancel.Checked = false;
            this.btnCancel.Image = null;
            this.btnCancel.Location = new System.Drawing.Point(375, 271);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 23);
            this.btnCancel.TabIndex = 53;
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
            this.btnSave.TabIndex = 52;
            this.btnSave.Text = "Save";
            this.btnSave.ToolTipText = "";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkCore7
            // 
            this.chkCore7.BackColor = System.Drawing.Color.Transparent;
            this.chkCore7.Checked = false;
            this.chkCore7.Enabled = false;
            this.chkCore7.Image = ((System.Drawing.Image)(resources.GetObject("chkCore7.Image")));
            this.chkCore7.Location = new System.Drawing.Point(374, 222);
            this.chkCore7.Name = "chkCore7";
            this.chkCore7.Size = new System.Drawing.Size(65, 21);
            this.chkCore7.TabIndex = 51;
            this.chkCore7.Text = "CPU 7";
            this.chkCore7.Visible = false;
            this.chkCore7.Click += new System.EventHandler(this.EditChangesApply);
            // 
            // chkCore6
            // 
            this.chkCore6.BackColor = System.Drawing.Color.Transparent;
            this.chkCore6.Checked = false;
            this.chkCore6.Enabled = false;
            this.chkCore6.Image = ((System.Drawing.Image)(resources.GetObject("chkCore6.Image")));
            this.chkCore6.Location = new System.Drawing.Point(374, 203);
            this.chkCore6.Name = "chkCore6";
            this.chkCore6.Size = new System.Drawing.Size(65, 21);
            this.chkCore6.TabIndex = 50;
            this.chkCore6.Text = "CPU 6";
            this.chkCore6.Visible = false;
            this.chkCore6.Click += new System.EventHandler(this.EditChangesApply);
            // 
            // chkCore5
            // 
            this.chkCore5.BackColor = System.Drawing.Color.Transparent;
            this.chkCore5.Checked = false;
            this.chkCore5.Enabled = false;
            this.chkCore5.Image = ((System.Drawing.Image)(resources.GetObject("chkCore5.Image")));
            this.chkCore5.Location = new System.Drawing.Point(374, 183);
            this.chkCore5.Name = "chkCore5";
            this.chkCore5.Size = new System.Drawing.Size(65, 21);
            this.chkCore5.TabIndex = 49;
            this.chkCore5.Text = "CPU 5";
            this.chkCore5.Visible = false;
            this.chkCore5.Click += new System.EventHandler(this.EditChangesApply);
            // 
            // chkCore4
            // 
            this.chkCore4.BackColor = System.Drawing.Color.Transparent;
            this.chkCore4.Checked = false;
            this.chkCore4.Enabled = false;
            this.chkCore4.Image = ((System.Drawing.Image)(resources.GetObject("chkCore4.Image")));
            this.chkCore4.Location = new System.Drawing.Point(374, 164);
            this.chkCore4.Name = "chkCore4";
            this.chkCore4.Size = new System.Drawing.Size(65, 21);
            this.chkCore4.TabIndex = 48;
            this.chkCore4.Text = "CPU 4";
            this.chkCore4.Visible = false;
            this.chkCore4.Click += new System.EventHandler(this.EditChangesApply);
            // 
            // chkOptimizeOn
            // 
            this.chkOptimizeOn.BackColor = System.Drawing.Color.Transparent;
            this.chkOptimizeOn.Checked = false;
            this.chkOptimizeOn.Image = ((System.Drawing.Image)(resources.GetObject("chkOptimizeOn.Image")));
            this.chkOptimizeOn.Location = new System.Drawing.Point(134, 224);
            this.chkOptimizeOn.Name = "chkOptimizeOn";
            this.chkOptimizeOn.Size = new System.Drawing.Size(150, 23);
            this.chkOptimizeOn.TabIndex = 43;
            this.chkOptimizeOn.Text = "Use Optimization Mode";
            this.chkOptimizeOn.Click += new System.EventHandler(this.chkOptimizeOn_Click);
            // 
            // btnFolder
            // 
            this.btnFolder.BlackButton = false;
            this.btnFolder.Checked = false;
            this.btnFolder.Image = null;
            this.btnFolder.Location = new System.Drawing.Point(407, 24);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(24, 23);
            this.btnFolder.TabIndex = 32;
            this.btnFolder.Text = "...";
            this.btnFolder.ToolTipText = "";
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.HasFocus = false;
            this.txtFolder.HideBorder = false;
            this.txtFolder.Image = null;
            this.txtFolder.Location = new System.Drawing.Point(134, 24);
            this.txtFolder.MultilineAllow = false;
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.PasswordChar = '\0';
            this.txtFolder.ReadOnly = false;
            this.txtFolder.Size = new System.Drawing.Size(267, 23);
            this.txtFolder.TabIndex = 31;
            this.txtFolder.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtFolder.ToolTipText = "The folder where WoT game is installed";
            this.txtFolder.TextChanged += new System.EventHandler(this.EditChangesApply);
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(18, 169);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(43, 23);
            this.badLabel1.TabIndex = 34;
            this.badLabel1.Text = "Start";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // ddStartApp
            // 
            this.ddStartApp.Image = null;
            this.ddStartApp.Location = new System.Drawing.Point(69, 169);
            this.ddStartApp.Name = "ddStartApp";
            this.ddStartApp.Size = new System.Drawing.Size(202, 23);
            this.ddStartApp.TabIndex = 38;
            this.ddStartApp.Text = "Do not start WoT";
            this.ddStartApp.TextChanged += new System.EventHandler(this.ddStartApp_TextChanged);
            this.ddStartApp.Click += new System.EventHandler(this.ddStartApp_Click);
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(1, 143);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(291, 111);
            this.badGroupBox1.TabIndex = 30;
            this.badGroupBox1.TabStop = false;
            this.badGroupBox1.Text = "WoT Startup Settings";
            // 
            // chkCore3
            // 
            this.chkCore3.BackColor = System.Drawing.Color.Transparent;
            this.chkCore3.Checked = false;
            this.chkCore3.Enabled = false;
            this.chkCore3.Image = ((System.Drawing.Image)(resources.GetObject("chkCore3.Image")));
            this.chkCore3.Location = new System.Drawing.Point(309, 222);
            this.chkCore3.Name = "chkCore3";
            this.chkCore3.Size = new System.Drawing.Size(62, 21);
            this.chkCore3.TabIndex = 47;
            this.chkCore3.Text = "CPU 3";
            this.chkCore3.Visible = false;
            this.chkCore3.Click += new System.EventHandler(this.EditChangesApply);
            // 
            // chkCore2
            // 
            this.chkCore2.BackColor = System.Drawing.Color.Transparent;
            this.chkCore2.Checked = false;
            this.chkCore2.Enabled = false;
            this.chkCore2.Image = ((System.Drawing.Image)(resources.GetObject("chkCore2.Image")));
            this.chkCore2.Location = new System.Drawing.Point(309, 203);
            this.chkCore2.Name = "chkCore2";
            this.chkCore2.Size = new System.Drawing.Size(62, 21);
            this.chkCore2.TabIndex = 46;
            this.chkCore2.Text = "CPU 2";
            this.chkCore2.Visible = false;
            this.chkCore2.Click += new System.EventHandler(this.EditChangesApply);
            // 
            // chkCore1
            // 
            this.chkCore1.BackColor = System.Drawing.Color.Transparent;
            this.chkCore1.Checked = false;
            this.chkCore1.Enabled = false;
            this.chkCore1.Image = ((System.Drawing.Image)(resources.GetObject("chkCore1.Image")));
            this.chkCore1.Location = new System.Drawing.Point(309, 184);
            this.chkCore1.Name = "chkCore1";
            this.chkCore1.Size = new System.Drawing.Size(62, 21);
            this.chkCore1.TabIndex = 45;
            this.chkCore1.Text = "CPU 1";
            this.chkCore1.Visible = false;
            this.chkCore1.Click += new System.EventHandler(this.EditChangesApply);
            // 
            // chkCore0
            // 
            this.chkCore0.BackColor = System.Drawing.Color.Transparent;
            this.chkCore0.Checked = false;
            this.chkCore0.Enabled = false;
            this.chkCore0.Image = ((System.Drawing.Image)(resources.GetObject("chkCore0.Image")));
            this.chkCore0.Location = new System.Drawing.Point(309, 164);
            this.chkCore0.Name = "chkCore0";
            this.chkCore0.Size = new System.Drawing.Size(62, 21);
            this.chkCore0.TabIndex = 44;
            this.chkCore0.Text = "CPU 0";
            this.chkCore0.Visible = false;
            this.chkCore0.Click += new System.EventHandler(this.EditChangesApply);
            // 
            // badGroupBox2
            // 
            this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox2.Image = null;
            this.badGroupBox2.Location = new System.Drawing.Point(300, 143);
            this.badGroupBox2.Name = "badGroupBox2";
            this.badGroupBox2.Size = new System.Drawing.Size(146, 111);
            this.badGroupBox2.TabIndex = 39;
            this.badGroupBox2.TabStop = false;
            this.badGroupBox2.Text = "Optimization Settings";
            // 
            // badSeperator1
            // 
            this.badSeperator1.BackColor = System.Drawing.Color.Transparent;
            this.badSeperator1.Direction = System.Windows.Forms.Orientation.Horizontal;
            this.badSeperator1.Image = null;
            this.badSeperator1.Location = new System.Drawing.Point(19, 76);
            this.badSeperator1.Name = "badSeperator1";
            this.badSeperator1.Size = new System.Drawing.Size(412, 23);
            this.badSeperator1.TabIndex = 59;
            // 
            // badGroupBox3
            // 
            this.badGroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox3.Image = null;
            this.badGroupBox3.Location = new System.Drawing.Point(1, 1);
            this.badGroupBox3.Name = "badGroupBox3";
            this.badGroupBox3.Size = new System.Drawing.Size(445, 129);
            this.badGroupBox3.TabIndex = 29;
            this.badGroupBox3.TabStop = false;
            this.badGroupBox3.Text = "WoT folders and Battle Result Retriever (BRR)";
            // 
            // AppSettingsWoT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.lblBRRStatus);
            this.Controls.Add(this.badLabel5);
            this.Controls.Add(this.badLabel3);
            this.Controls.Add(this.txtResModsSubFolder);
            this.Controls.Add(this.badLabel2);
            this.Controls.Add(this.cmdHelp);
            this.Controls.Add(this.btnBrrInstall);
            this.Controls.Add(this.chkBrrStarupCheck);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.txtBatchFile);
            this.Controls.Add(this.badLabel4);
            this.Controls.Add(this.chkAutoRun);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkCore7);
            this.Controls.Add(this.chkCore6);
            this.Controls.Add(this.chkCore5);
            this.Controls.Add(this.chkCore4);
            this.Controls.Add(this.chkOptimizeOn);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.badLabel1);
            this.Controls.Add(this.ddStartApp);
            this.Controls.Add(this.badGroupBox1);
            this.Controls.Add(this.chkCore3);
            this.Controls.Add(this.chkCore2);
            this.Controls.Add(this.chkCore1);
            this.Controls.Add(this.chkCore0);
            this.Controls.Add(this.badGroupBox2);
            this.Controls.Add(this.badSeperator1);
            this.Controls.Add(this.badGroupBox3);
            this.Name = "AppSettingsWoT";
            this.Size = new System.Drawing.Size(457, 312);
            this.Load += new System.EventHandler(this.AppSettingsWoT_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BadButton btnBrrInstall;
        private BadCheckBox chkBrrStarupCheck;
        private BadButton btnFile;
        private BadTextBox txtBatchFile;
        private BadLabel badLabel4;
        private BadCheckBox chkAutoRun;
        private BadButton btnCancel;
        private BadButton btnSave;
        private BadCheckBox chkCore7;
        private BadCheckBox chkCore6;
        private BadCheckBox chkCore5;
        private BadCheckBox chkCore4;
        private BadCheckBox chkCore3;
        private BadCheckBox chkCore2;
        private BadCheckBox chkCore1;
        private BadCheckBox chkCore0;
        private BadCheckBox chkOptimizeOn;
        private BadGroupBox badGroupBox2;
        private BadButton btnFolder;
        private BadTextBox txtFolder;
        private BadLabel badLabel1;
        private BadDropDownBox ddStartApp;
        private BadGroupBox badGroupBox1;
        private BadGroupBox badGroupBox3;
        private BadButton cmdHelp;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private BadLabel badLabel2;
        private BadTextBox txtResModsSubFolder;
        private BadLabel badLabel3;
        private BadSeperator badSeperator1;
        private BadLabel badLabel5;
        private BadLabel lblBRRStatus;
    }
}
