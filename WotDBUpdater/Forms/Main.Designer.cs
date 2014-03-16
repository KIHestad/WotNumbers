namespace WotDBUpdater.Forms
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAppSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDbSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDossier = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRunStopToggle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemManualRun = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemRunPrev = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRunPrevForceUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemReport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemReportView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemReportTable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTest = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTest_ShowCountry = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTest_AddCountry = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemTest_ListTanks = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemTest_ImportTank_Wn8exp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTest_ImportTurret = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTest_ImportGun = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTest_ImportRadio = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemTest_WotURL = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTest_ProgressBar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.panelTop = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.picNormalize = new System.Windows.Forms.PictureBox();
            this.picMinimize = new System.Windows.Forms.PictureBox();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.picResize = new System.Windows.Forms.PictureBox();
            this.panelMaster = new System.Windows.Forms.Panel();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblStatus2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.timerStatus2 = new System.Windows.Forms.Timer(this.components);
            this.menuItemTest_ViewRange = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNormalize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResize)).BeginInit();
            this.panelMaster.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.BackColor = System.Drawing.SystemColors.Menu;
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemDossier,
            this.menuItemReport,
            this.menuItemTest,
            this.menuItemHelp});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(475, 24);
            this.menuMain.TabIndex = 10;
            this.menuMain.Text = "menuMain";
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAppSettings,
            this.menuItemDbSettings,
            this.toolStripSeparator2,
            this.menuItemExit});
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(37, 20);
            this.menuItemFile.Text = "&File";
            // 
            // menuItemAppSettings
            // 
            this.menuItemAppSettings.Name = "menuItemAppSettings";
            this.menuItemAppSettings.Size = new System.Drawing.Size(179, 22);
            this.menuItemAppSettings.Text = "Application settings";
            this.menuItemAppSettings.Click += new System.EventHandler(this.menuItemAppSettings_Click);
            // 
            // menuItemDbSettings
            // 
            this.menuItemDbSettings.Name = "menuItemDbSettings";
            this.menuItemDbSettings.Size = new System.Drawing.Size(179, 22);
            this.menuItemDbSettings.Text = "Database Settings";
            this.menuItemDbSettings.Click += new System.EventHandler(this.menuItemDbSettings_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(176, 6);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(179, 22);
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItemDossier
            // 
            this.menuItemDossier.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemRunStopToggle,
            this.toolStripSeparator1,
            this.menuItemManualRun,
            this.toolStripSeparator6,
            this.menuItemRunPrev,
            this.menuItemRunPrevForceUpdate});
            this.menuItemDossier.Name = "menuItemDossier";
            this.menuItemDossier.Size = new System.Drawing.Size(57, 20);
            this.menuItemDossier.Text = "&Dossier";
            // 
            // menuItemRunStopToggle
            // 
            this.menuItemRunStopToggle.Name = "menuItemRunStopToggle";
            this.menuItemRunStopToggle.Size = new System.Drawing.Size(279, 22);
            this.menuItemRunStopToggle.Text = "Listen to dossier file";
            this.menuItemRunStopToggle.Click += new System.EventHandler(this.menuItemRunStopToggle_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(276, 6);
            // 
            // menuItemManualRun
            // 
            this.menuItemManualRun.Name = "menuItemManualRun";
            this.menuItemManualRun.Size = new System.Drawing.Size(279, 22);
            this.menuItemManualRun.Text = "Manual dossier file check";
            this.menuItemManualRun.Click += new System.EventHandler(this.menuItemManualRun_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(276, 6);
            // 
            // menuItemRunPrev
            // 
            this.menuItemRunPrev.Name = "menuItemRunPrev";
            this.menuItemRunPrev.Size = new System.Drawing.Size(279, 22);
            this.menuItemRunPrev.Text = "Normal check previous dossier file";
            this.menuItemRunPrev.Click += new System.EventHandler(this.menuItemRunPrev_Click);
            // 
            // menuItemRunPrevForceUpdate
            // 
            this.menuItemRunPrevForceUpdate.Name = "menuItemRunPrevForceUpdate";
            this.menuItemRunPrevForceUpdate.Size = new System.Drawing.Size(279, 22);
            this.menuItemRunPrevForceUpdate.Text = "Force update from previous dossier file";
            this.menuItemRunPrevForceUpdate.Click += new System.EventHandler(this.menuItemRunPrevForceUpdate_Click);
            // 
            // menuItemReport
            // 
            this.menuItemReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemReportView,
            this.menuItemReportTable});
            this.menuItemReport.Name = "menuItemReport";
            this.menuItemReport.Size = new System.Drawing.Size(59, 20);
            this.menuItemReport.Text = "&Reports";
            // 
            // menuItemReportView
            // 
            this.menuItemReportView.Name = "menuItemReportView";
            this.menuItemReportView.Size = new System.Drawing.Size(186, 22);
            this.menuItemReportView.Text = "Show Database View";
            this.menuItemReportView.Click += new System.EventHandler(this.menuItemReportView_Click);
            // 
            // menuItemReportTable
            // 
            this.menuItemReportTable.Name = "menuItemReportTable";
            this.menuItemReportTable.Size = new System.Drawing.Size(186, 22);
            this.menuItemReportTable.Text = "Show Database Table";
            this.menuItemReportTable.Click += new System.EventHandler(this.menuItemReportTable_Click);
            // 
            // menuItemTest
            // 
            this.menuItemTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTest_ShowCountry,
            this.menuItemTest_AddCountry,
            this.toolStripSeparator3,
            this.menuItemTest_ListTanks,
            this.toolStripSeparator4,
            this.menuItemTest_ImportTank_Wn8exp,
            this.menuItemTest_ImportTurret,
            this.menuItemTest_ImportGun,
            this.menuItemTest_ImportRadio,
            this.toolStripSeparator5,
            this.menuItemTest_WotURL,
            this.menuItemTest_ProgressBar,
            this.menuItemTest_ViewRange});
            this.menuItemTest.Name = "menuItemTest";
            this.menuItemTest.Size = new System.Drawing.Size(58, 20);
            this.menuItemTest.Text = "&Testing";
            // 
            // menuItemTest_ShowCountry
            // 
            this.menuItemTest_ShowCountry.Name = "menuItemTest_ShowCountry";
            this.menuItemTest_ShowCountry.Size = new System.Drawing.Size(213, 22);
            this.menuItemTest_ShowCountry.Text = "Show country table in grid";
            this.menuItemTest_ShowCountry.Click += new System.EventHandler(this.menuItemTest_ShowCountry_Click);
            // 
            // menuItemTest_AddCountry
            // 
            this.menuItemTest_AddCountry.Name = "menuItemTest_AddCountry";
            this.menuItemTest_AddCountry.Size = new System.Drawing.Size(213, 22);
            this.menuItemTest_AddCountry.Text = "Add country to table";
            this.menuItemTest_AddCountry.Click += new System.EventHandler(this.menuItemTest_AddCountry_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(210, 6);
            // 
            // menuItemTest_ListTanks
            // 
            this.menuItemTest_ListTanks.Name = "menuItemTest_ListTanks";
            this.menuItemTest_ListTanks.Size = new System.Drawing.Size(213, 22);
            this.menuItemTest_ListTanks.Text = "List Tanks";
            this.menuItemTest_ListTanks.Click += new System.EventHandler(this.menuItemTest_ListTanks_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(210, 6);
            // 
            // menuItemTest_ImportTank_Wn8exp
            // 
            this.menuItemTest_ImportTank_Wn8exp.Name = "menuItemTest_ImportTank_Wn8exp";
            this.menuItemTest_ImportTank_Wn8exp.Size = new System.Drawing.Size(213, 22);
            this.menuItemTest_ImportTank_Wn8exp.Text = "Import tank & wn8 exp";
            this.menuItemTest_ImportTank_Wn8exp.Click += new System.EventHandler(this.menuItemTest_ImportTank_Wn8exp_Click);
            // 
            // menuItemTest_ImportTurret
            // 
            this.menuItemTest_ImportTurret.Name = "menuItemTest_ImportTurret";
            this.menuItemTest_ImportTurret.Size = new System.Drawing.Size(213, 22);
            this.menuItemTest_ImportTurret.Text = "Import turrets";
            this.menuItemTest_ImportTurret.Click += new System.EventHandler(this.menuItemTest_ImportTurret_Click);
            // 
            // menuItemTest_ImportGun
            // 
            this.menuItemTest_ImportGun.Name = "menuItemTest_ImportGun";
            this.menuItemTest_ImportGun.Size = new System.Drawing.Size(213, 22);
            this.menuItemTest_ImportGun.Text = "Import guns";
            this.menuItemTest_ImportGun.Click += new System.EventHandler(this.menuItemTest_ImportGun_Click);
            // 
            // menuItemTest_ImportRadio
            // 
            this.menuItemTest_ImportRadio.Name = "menuItemTest_ImportRadio";
            this.menuItemTest_ImportRadio.Size = new System.Drawing.Size(213, 22);
            this.menuItemTest_ImportRadio.Text = "Import radios";
            this.menuItemTest_ImportRadio.Click += new System.EventHandler(this.menuItemTest_ImportRadio_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(210, 6);
            // 
            // menuItemTest_WotURL
            // 
            this.menuItemTest_WotURL.Name = "menuItemTest_WotURL";
            this.menuItemTest_WotURL.Size = new System.Drawing.Size(213, 22);
            this.menuItemTest_WotURL.Text = "Test URL";
            this.menuItemTest_WotURL.Click += new System.EventHandler(this.menuItemTest_WotURL_Click);
            // 
            // menuItemTest_ProgressBar
            // 
            this.menuItemTest_ProgressBar.Name = "menuItemTest_ProgressBar";
            this.menuItemTest_ProgressBar.Size = new System.Drawing.Size(213, 22);
            this.menuItemTest_ProgressBar.Text = "Test progress bar";
            this.menuItemTest_ProgressBar.Click += new System.EventHandler(this.menuItemTest_ProgressBar_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAbout});
            this.menuItemHelp.Name = "menuItemHelp";
            this.menuItemHelp.Size = new System.Drawing.Size(44, 20);
            this.menuItemHelp.Text = "&Help";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            this.menuItemAbout.Size = new System.Drawing.Size(107, 22);
            this.menuItemAbout.Text = "&About";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.picLogo);
            this.panelTop.Controls.Add(this.picNormalize);
            this.panelTop.Controls.Add(this.picMinimize);
            this.panelTop.Controls.Add(this.picClose);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Location = new System.Drawing.Point(12, 12);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(475, 26);
            this.panelTop.TabIndex = 14;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panelTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // picLogo
            // 
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(11, 4);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(35, 18);
            this.picLogo.TabIndex = 5;
            this.picLogo.TabStop = false;
            this.picLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.picLogo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.picLogo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // picNormalize
            // 
            this.picNormalize.Image = ((System.Drawing.Image)(resources.GetObject("picNormalize.Image")));
            this.picNormalize.Location = new System.Drawing.Point(397, 0);
            this.picNormalize.Name = "picNormalize";
            this.picNormalize.Size = new System.Drawing.Size(34, 26);
            this.picNormalize.TabIndex = 4;
            this.picNormalize.TabStop = false;
            this.picNormalize.Click += new System.EventHandler(this.picNormalize_Click);
            this.picNormalize.MouseLeave += new System.EventHandler(this.picNormalize_MouseLeave);
            this.picNormalize.MouseHover += new System.EventHandler(this.picNormalize_MouseHover);
            // 
            // picMinimize
            // 
            this.picMinimize.Image = ((System.Drawing.Image)(resources.GetObject("picMinimize.Image")));
            this.picMinimize.Location = new System.Drawing.Point(357, 0);
            this.picMinimize.Name = "picMinimize";
            this.picMinimize.Size = new System.Drawing.Size(34, 26);
            this.picMinimize.TabIndex = 3;
            this.picMinimize.TabStop = false;
            this.picMinimize.Click += new System.EventHandler(this.picMinimize_Click);
            this.picMinimize.MouseLeave += new System.EventHandler(this.picMinimize_MouseLeave);
            this.picMinimize.MouseHover += new System.EventHandler(this.picMinimize_MouseHover);
            // 
            // picClose
            // 
            this.picClose.Image = ((System.Drawing.Image)(resources.GetObject("picClose.Image")));
            this.picClose.Location = new System.Drawing.Point(437, 0);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(34, 26);
            this.picClose.TabIndex = 2;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            this.picClose.MouseLeave += new System.EventHandler(this.picClose_MouseLeave);
            this.picClose.MouseHover += new System.EventHandler(this.picClose_MouseHover);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblTitle.Location = new System.Drawing.Point(52, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(83, 17);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "WoT DBstats ";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.lblTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.menuMain);
            this.panelMain.Location = new System.Drawing.Point(12, 48);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(475, 274);
            this.panelMain.TabIndex = 15;
            // 
            // picResize
            // 
            this.picResize.Image = ((System.Drawing.Image)(resources.GetObject("picResize.Image")));
            this.picResize.Location = new System.Drawing.Point(426, 0);
            this.picResize.Name = "picResize";
            this.picResize.Size = new System.Drawing.Size(24, 24);
            this.picResize.TabIndex = 13;
            this.picResize.TabStop = false;
            this.picResize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picResize_MouseDown);
            this.picResize.MouseLeave += new System.EventHandler(this.picResize_MouseLeave);
            this.picResize.MouseHover += new System.EventHandler(this.picResize_MouseHover);
            this.picResize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picResize_MouseMove);
            this.picResize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picResize_MouseUp);
            // 
            // panelMaster
            // 
            this.panelMaster.Controls.Add(this.panelStatus);
            this.panelMaster.Controls.Add(this.panelTop);
            this.panelMaster.Controls.Add(this.panelMain);
            this.panelMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMaster.Location = new System.Drawing.Point(0, 0);
            this.panelMaster.Name = "panelMaster";
            this.panelMaster.Size = new System.Drawing.Size(503, 379);
            this.panelMaster.TabIndex = 16;
            this.panelMaster.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMaster_Paint);
            // 
            // panelStatus
            // 
            this.panelStatus.BackColor = System.Drawing.Color.Black;
            this.panelStatus.Controls.Add(this.picResize);
            this.panelStatus.Controls.Add(this.lblStatus2);
            this.panelStatus.Controls.Add(this.label1);
            this.panelStatus.Controls.Add(this.lblStatus1);
            this.panelStatus.Location = new System.Drawing.Point(12, 337);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(475, 24);
            this.panelStatus.TabIndex = 16;
            // 
            // lblStatus2
            // 
            this.lblStatus2.AutoSize = true;
            this.lblStatus2.ForeColor = System.Drawing.Color.DarkGray;
            this.lblStatus2.Location = new System.Drawing.Point(75, 5);
            this.lblStatus2.Name = "lblStatus2";
            this.lblStatus2.Size = new System.Drawing.Size(68, 13);
            this.lblStatus2.TabIndex = 16;
            this.lblStatus2.Text = "Last action...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(72, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 15;
            // 
            // lblStatus1
            // 
            this.lblStatus1.AutoSize = true;
            this.lblStatus1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblStatus1.Location = new System.Drawing.Point(8, 5);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(37, 13);
            this.lblStatus1.TabIndex = 14;
            this.lblStatus1.Text = "Status";
            // 
            // timerStatus2
            // 
            this.timerStatus2.Interval = 5000;
            this.timerStatus2.Tick += new System.EventHandler(this.timerStatus2_Tick);
            // 
            // menuItemTest_ViewRange
            // 
            this.menuItemTest_ViewRange.Name = "menuItemTest_ViewRange";
            this.menuItemTest_ViewRange.Size = new System.Drawing.Size(213, 22);
            this.menuItemTest_ViewRange.Text = "Test View Range";
            this.menuItemTest_ViewRange.Click += new System.EventHandler(this.menuItemTest_ViewRange_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(503, 379);
            this.Controls.Add(this.panelMaster);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.Name = "Main";
            this.Text = "WotDBUpdater";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNormalize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResize)).EndInit();
            this.panelMaster.ResumeLayout(false);
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem menuItemFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemAppSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private System.Windows.Forms.ToolStripMenuItem menuItemDbSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest_ShowCountry;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest_AddCountry;
        private System.Windows.Forms.ToolStripMenuItem menuItemReport;
        private System.Windows.Forms.ToolStripMenuItem menuItemReportTable;
        private System.Windows.Forms.ToolStripMenuItem menuItemReportView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest_ListTanks;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest_WotURL;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest_ImportTurret;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest_ImportGun;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest_ImportRadio;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest_ProgressBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest_ImportTank_Wn8exp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelMaster;
        private System.Windows.Forms.PictureBox picResize;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.PictureBox picNormalize;
        private System.Windows.Forms.PictureBox picMinimize;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblStatus1;
        private System.Windows.Forms.ToolStripMenuItem menuItemDossier;
        private System.Windows.Forms.ToolStripMenuItem menuItemRunStopToggle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemManualRun;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem menuItemRunPrev;
        private System.Windows.Forms.ToolStripMenuItem menuItemRunPrevForceUpdate;
        private System.Windows.Forms.Label lblStatus2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerStatus2;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest_ViewRange;
    }
}

