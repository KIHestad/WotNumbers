namespace WotDBUpdater
{
    partial class frmMain
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
            this.btnStartStop = new System.Windows.Forms.Button();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.btnManualRun = new System.Windows.Forms.Button();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectDossierFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.importTanksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDatabaseViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDatabaseTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showCountryTableInGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCountryToTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.listTanksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testTankStats2DBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTestPrev = new System.Windows.Forms.Button();
            this.btntestForce = new System.Windows.Forms.Button();
            this.testReadModuleDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlStatus.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(18, 80);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(89, 34);
            this.btnStartStop.TabIndex = 3;
            this.btnStartStop.Text = "Start / Stop";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(117, 41);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(336, 199);
            this.listBoxLog.TabIndex = 4;
            this.listBoxLog.DoubleClick += new System.EventHandler(this.listBoxLog_DoubleClick);
            // 
            // btnManualRun
            // 
            this.btnManualRun.Location = new System.Drawing.Point(18, 120);
            this.btnManualRun.Name = "btnManualRun";
            this.btnManualRun.Size = new System.Drawing.Size(89, 34);
            this.btnManualRun.TabIndex = 5;
            this.btnManualRun.Text = "Manual run";
            this.btnManualRun.UseVisualStyleBackColor = true;
            this.btnManualRun.Click += new System.EventHandler(this.btnManualRun_Click);
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.Gray;
            this.pnlStatus.Controls.Add(this.lblStatus);
            this.pnlStatus.Location = new System.Drawing.Point(18, 41);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(89, 33);
            this.pnlStatus.TabIndex = 8;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(13, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(61, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "WAITING";
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.testingToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(469, 24);
            this.menuMain.TabIndex = 10;
            this.menuMain.Text = "menuMain";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectDossierFileToolStripMenuItem,
            this.databaseSettingsToolStripMenuItem,
            this.toolStripSeparator2,
            this.importTanksToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // selectDossierFileToolStripMenuItem
            // 
            this.selectDossierFileToolStripMenuItem.Name = "selectDossierFileToolStripMenuItem";
            this.selectDossierFileToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.selectDossierFileToolStripMenuItem.Text = "Application settings";
            this.selectDossierFileToolStripMenuItem.Click += new System.EventHandler(this.selectApplicationSetting_Click);
            // 
            // databaseSettingsToolStripMenuItem
            // 
            this.databaseSettingsToolStripMenuItem.Name = "databaseSettingsToolStripMenuItem";
            this.databaseSettingsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.databaseSettingsToolStripMenuItem.Text = "Database Settings";
            this.databaseSettingsToolStripMenuItem.Click += new System.EventHandler(this.databaseSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(176, 6);
            // 
            // importTanksToolStripMenuItem
            // 
            this.importTanksToolStripMenuItem.Name = "importTanksToolStripMenuItem";
            this.importTanksToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.importTanksToolStripMenuItem.Text = "Import tanks";
            this.importTanksToolStripMenuItem.Click += new System.EventHandler(this.importTanksToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDatabaseViewToolStripMenuItem,
            this.showDatabaseTableToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reportsToolStripMenuItem.Text = "&Reports";
            // 
            // showDatabaseViewToolStripMenuItem
            // 
            this.showDatabaseViewToolStripMenuItem.Name = "showDatabaseViewToolStripMenuItem";
            this.showDatabaseViewToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.showDatabaseViewToolStripMenuItem.Text = "Show Database View";
            this.showDatabaseViewToolStripMenuItem.Click += new System.EventHandler(this.showDatabaseViewToolStripMenuItem_Click);
            // 
            // showDatabaseTableToolStripMenuItem
            // 
            this.showDatabaseTableToolStripMenuItem.Name = "showDatabaseTableToolStripMenuItem";
            this.showDatabaseTableToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.showDatabaseTableToolStripMenuItem.Text = "Show Database Table";
            this.showDatabaseTableToolStripMenuItem.Click += new System.EventHandler(this.showDatabaseTableToolStripMenuItem_Click);
            // 
            // testingToolStripMenuItem
            // 
            this.testingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showCountryTableInGridToolStripMenuItem,
            this.addCountryToTableToolStripMenuItem,
            this.toolStripSeparator3,
            this.listTanksToolStripMenuItem,
            this.testURLToolStripMenuItem,
            this.testTankStats2DBToolStripMenuItem,
            this.testReadModuleDataToolStripMenuItem});
            this.testingToolStripMenuItem.Name = "testingToolStripMenuItem";
            this.testingToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.testingToolStripMenuItem.Text = "&Testing";
            // 
            // showCountryTableInGridToolStripMenuItem
            // 
            this.showCountryTableInGridToolStripMenuItem.Name = "showCountryTableInGridToolStripMenuItem";
            this.showCountryTableInGridToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.showCountryTableInGridToolStripMenuItem.Text = "Show country table in grid";
            this.showCountryTableInGridToolStripMenuItem.Click += new System.EventHandler(this.showTankTableInGridToolStripMenuItem_Click);
            // 
            // addCountryToTableToolStripMenuItem
            // 
            this.addCountryToTableToolStripMenuItem.Name = "addCountryToTableToolStripMenuItem";
            this.addCountryToTableToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.addCountryToTableToolStripMenuItem.Text = "Add country to table";
            this.addCountryToTableToolStripMenuItem.Click += new System.EventHandler(this.addCountryToTableToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(210, 6);
            // 
            // listTanksToolStripMenuItem
            // 
            this.listTanksToolStripMenuItem.Name = "listTanksToolStripMenuItem";
            this.listTanksToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.listTanksToolStripMenuItem.Text = "List Tanks";
            this.listTanksToolStripMenuItem.Click += new System.EventHandler(this.listTanksToolStripMenuItem_Click);
            // 
            // testURLToolStripMenuItem
            // 
            this.testURLToolStripMenuItem.Name = "testURLToolStripMenuItem";
            this.testURLToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.testURLToolStripMenuItem.Text = "Test URL";
            this.testURLToolStripMenuItem.Click += new System.EventHandler(this.testURLToolStripMenuItem_Click);
            // 
            // testTankStats2DBToolStripMenuItem
            // 
            this.testTankStats2DBToolStripMenuItem.Name = "testTankStats2DBToolStripMenuItem";
            this.testTankStats2DBToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.testTankStats2DBToolStripMenuItem.Text = "Test tankStats2DB";
            this.testTankStats2DBToolStripMenuItem.Click += new System.EventHandler(this.testTankStats2DBToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // btnTestPrev
            // 
            this.btnTestPrev.Location = new System.Drawing.Point(18, 160);
            this.btnTestPrev.Name = "btnTestPrev";
            this.btnTestPrev.Size = new System.Drawing.Size(89, 34);
            this.btnTestPrev.TabIndex = 11;
            this.btnTestPrev.Text = "Test normal";
            this.btnTestPrev.UseVisualStyleBackColor = true;
            this.btnTestPrev.Click += new System.EventHandler(this.btnTestPrev_Click);
            // 
            // btntestForce
            // 
            this.btntestForce.Location = new System.Drawing.Point(18, 200);
            this.btntestForce.Name = "btntestForce";
            this.btntestForce.Size = new System.Drawing.Size(89, 34);
            this.btntestForce.TabIndex = 12;
            this.btntestForce.Text = "Test force";
            this.btntestForce.UseVisualStyleBackColor = true;
            this.btntestForce.Click += new System.EventHandler(this.btntestForce_Click);
            // 
            // testReadModuleDataToolStripMenuItem
            // 
            this.testReadModuleDataToolStripMenuItem.Name = "testReadModuleDataToolStripMenuItem";
            this.testReadModuleDataToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.testReadModuleDataToolStripMenuItem.Text = "Test read module data";
            this.testReadModuleDataToolStripMenuItem.Click += new System.EventHandler(this.testReadModuleDataToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 249);
            this.Controls.Add(this.btntestForce);
            this.Controls.Add(this.btnTestPrev);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.btnManualRun);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.btnStartStop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "WotDBUpdater";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Button btnManualRun;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectDossierFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showCountryTableInGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCountryToTableToolStripMenuItem;
        private System.Windows.Forms.Button btnTestPrev;
        private System.Windows.Forms.ToolStripMenuItem importTanksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDatabaseTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDatabaseViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem listTanksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testURLToolStripMenuItem;
        private System.Windows.Forms.Button btntestForce;
        private System.Windows.Forms.ToolStripMenuItem testTankStats2DBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testReadModuleDataToolStripMenuItem;
    }
}

