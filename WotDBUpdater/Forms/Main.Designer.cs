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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menuItemTest = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTest_ImportTank_Wn8exp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTest_ImportTurret = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTest_ImportGun = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTest_ImportRadio = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemTest_WotURL = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTest_ProgressBar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTest_ViewRange = new System.Windows.Forms.ToolStripMenuItem();
            this.panelTop = new System.Windows.Forms.Panel();
            this.picNormalize = new System.Windows.Forms.PictureBox();
            this.picMinimize = new System.Windows.Forms.PictureBox();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelScrollArea = new System.Windows.Forms.Panel();
            this.panelScrollbar = new System.Windows.Forms.Panel();
            this.dataGridMain = new System.Windows.Forms.DataGridView();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lblOverView = new System.Windows.Forms.Label();
            this.picIS7 = new System.Windows.Forms.PictureBox();
            this.toolMain = new System.Windows.Forms.ToolStrip();
            this.toolItemViewLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolItemViewOverall = new System.Windows.Forms.ToolStripButton();
            this.toolItemViewTankInfo = new System.Windows.Forms.ToolStripButton();
            this.toolItemViewBattles = new System.Windows.Forms.ToolStripButton();
            this.toolItemRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolItemSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolItemSettingsRun = new System.Windows.Forms.ToolStripMenuItem();
            this.toolItemSettingsDossierOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolItemSettingsRunManual = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.toolItemSettingsUpdateFromPrev = new System.Windows.Forms.ToolStripMenuItem();
            this.toolItemSettingsForceUpdateFromPrev = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.toolItemSettingsApp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolItemSettingsDb = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolItemShowDbTables = new System.Windows.Forms.ToolStripMenuItem();
            this.toolItemHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.panelStrip = new System.Windows.Forms.Panel();
            this.picResize = new System.Windows.Forms.PictureBox();
            this.panelMaster = new System.Windows.Forms.Panel();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblStatus2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.timerStatus2 = new System.Windows.Forms.Timer(this.components);
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.timerPanelSlide = new System.Windows.Forms.Timer(this.components);
            this.menuMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNormalize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.panelMain.SuspendLayout();
            this.panelScrollArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMain)).BeginInit();
            this.panelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIS7)).BeginInit();
            this.toolMain.SuspendLayout();
            this.panelStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResize)).BeginInit();
            this.panelMaster.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.BackColor = System.Drawing.SystemColors.Menu;
            this.menuMain.Dock = System.Windows.Forms.DockStyle.Right;
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTest});
            this.menuMain.Location = new System.Drawing.Point(868, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(47, 27);
            this.menuMain.TabIndex = 10;
            this.menuMain.Text = "menuMain";
            // 
            // menuItemTest
            // 
            this.menuItemTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTest_ImportTank_Wn8exp,
            this.menuItemTest_ImportTurret,
            this.menuItemTest_ImportGun,
            this.menuItemTest_ImportRadio,
            this.toolStripSeparator5,
            this.menuItemTest_WotURL,
            this.menuItemTest_ProgressBar,
            this.menuItemTest_ViewRange});
            this.menuItemTest.Name = "menuItemTest";
            this.menuItemTest.Size = new System.Drawing.Size(34, 19);
            this.menuItemTest.Text = "Test";
            // 
            // menuItemTest_ImportTank_Wn8exp
            // 
            this.menuItemTest_ImportTank_Wn8exp.Name = "menuItemTest_ImportTank_Wn8exp";
            this.menuItemTest_ImportTank_Wn8exp.Size = new System.Drawing.Size(185, 22);
            this.menuItemTest_ImportTank_Wn8exp.Text = "Import tank & wn8 exp";
            this.menuItemTest_ImportTank_Wn8exp.Click += new System.EventHandler(this.menuItemTest_ImportTank_Wn8exp_Click);
            // 
            // menuItemTest_ImportTurret
            // 
            this.menuItemTest_ImportTurret.Name = "menuItemTest_ImportTurret";
            this.menuItemTest_ImportTurret.Size = new System.Drawing.Size(185, 22);
            this.menuItemTest_ImportTurret.Text = "Import turrets";
            this.menuItemTest_ImportTurret.Click += new System.EventHandler(this.menuItemTest_ImportTurret_Click);
            // 
            // menuItemTest_ImportGun
            // 
            this.menuItemTest_ImportGun.Name = "menuItemTest_ImportGun";
            this.menuItemTest_ImportGun.Size = new System.Drawing.Size(185, 22);
            this.menuItemTest_ImportGun.Text = "Import guns";
            this.menuItemTest_ImportGun.Click += new System.EventHandler(this.menuItemTest_ImportGun_Click);
            // 
            // menuItemTest_ImportRadio
            // 
            this.menuItemTest_ImportRadio.Name = "menuItemTest_ImportRadio";
            this.menuItemTest_ImportRadio.Size = new System.Drawing.Size(185, 22);
            this.menuItemTest_ImportRadio.Text = "Import radios";
            this.menuItemTest_ImportRadio.Click += new System.EventHandler(this.menuItemTest_ImportRadio_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(182, 6);
            // 
            // menuItemTest_WotURL
            // 
            this.menuItemTest_WotURL.Name = "menuItemTest_WotURL";
            this.menuItemTest_WotURL.Size = new System.Drawing.Size(185, 22);
            this.menuItemTest_WotURL.Text = "Test URL";
            this.menuItemTest_WotURL.Click += new System.EventHandler(this.menuItemTest_WotURL_Click);
            // 
            // menuItemTest_ProgressBar
            // 
            this.menuItemTest_ProgressBar.Name = "menuItemTest_ProgressBar";
            this.menuItemTest_ProgressBar.Size = new System.Drawing.Size(185, 22);
            this.menuItemTest_ProgressBar.Text = "Test progress bar";
            this.menuItemTest_ProgressBar.Click += new System.EventHandler(this.menuItemTest_ProgressBar_Click);
            // 
            // menuItemTest_ViewRange
            // 
            this.menuItemTest_ViewRange.Name = "menuItemTest_ViewRange";
            this.menuItemTest_ViewRange.Size = new System.Drawing.Size(185, 22);
            this.menuItemTest_ViewRange.Text = "Test View Range";
            this.menuItemTest_ViewRange.Click += new System.EventHandler(this.menuItemTest_ViewRange_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.Transparent;
            this.panelTop.Controls.Add(this.picNormalize);
            this.panelTop.Controls.Add(this.picMinimize);
            this.panelTop.Controls.Add(this.picClose);
            this.panelTop.Controls.Add(this.picLogo);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Location = new System.Drawing.Point(12, 12);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(915, 30);
            this.panelTop.TabIndex = 14;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panelTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // picNormalize
            // 
            this.picNormalize.Image = ((System.Drawing.Image)(resources.GetObject("picNormalize.Image")));
            this.picNormalize.Location = new System.Drawing.Point(819, 0);
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
            this.picMinimize.Location = new System.Drawing.Point(779, 0);
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
            this.picClose.Location = new System.Drawing.Point(859, 0);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(34, 26);
            this.picClose.TabIndex = 2;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            this.picClose.MouseLeave += new System.EventHandler(this.picClose_MouseLeave);
            this.picClose.MouseHover += new System.EventHandler(this.picClose_MouseHover);
            // 
            // picLogo
            // 
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(11, 8);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(35, 18);
            this.picLogo.TabIndex = 5;
            this.picLogo.TabStop = false;
            this.picLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.picLogo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.picLogo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(156)))));
            this.lblTitle.Location = new System.Drawing.Point(52, 9);
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
            this.panelMain.Controls.Add(this.panelScrollArea);
            this.panelMain.Controls.Add(this.dataGridMain);
            this.panelMain.Location = new System.Drawing.Point(9, 172);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(918, 178);
            this.panelMain.TabIndex = 15;
            // 
            // panelScrollArea
            // 
            this.panelScrollArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.panelScrollArea.Controls.Add(this.panelScrollbar);
            this.panelScrollArea.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelScrollArea.Location = new System.Drawing.Point(900, 0);
            this.panelScrollArea.Name = "panelScrollArea";
            this.panelScrollArea.Size = new System.Drawing.Size(18, 178);
            this.panelScrollArea.TabIndex = 16;
            // 
            // panelScrollbar
            // 
            this.panelScrollbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(106)))));
            this.panelScrollbar.Location = new System.Drawing.Point(4, 4);
            this.panelScrollbar.Name = "panelScrollbar";
            this.panelScrollbar.Size = new System.Drawing.Size(10, 30);
            this.panelScrollbar.TabIndex = 12;
            this.panelScrollbar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlScrollbar_MouseDown);
            this.panelScrollbar.MouseLeave += new System.EventHandler(this.pnlScrollbar_MouseLeave);
            this.panelScrollbar.MouseHover += new System.EventHandler(this.pnlScrollbar_MouseHover);
            this.panelScrollbar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlScrollbar_MouseMove);
            this.panelScrollbar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlScrollbar_MouseUp);
            // 
            // dataGridMain
            // 
            this.dataGridMain.AllowUserToAddRows = false;
            this.dataGridMain.AllowUserToDeleteRows = false;
            this.dataGridMain.AllowUserToOrderColumns = true;
            this.dataGridMain.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.dataGridMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridMain.CausesValidation = false;
            this.dataGridMain.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle15.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridMain.ColumnHeadersHeight = 36;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle16.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridMain.DefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridMain.EnableHeadersVisualStyles = false;
            this.dataGridMain.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.dataGridMain.Location = new System.Drawing.Point(0, 0);
            this.dataGridMain.Name = "dataGridMain";
            this.dataGridMain.ReadOnly = true;
            this.dataGridMain.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridMain.RowHeadersVisible = false;
            this.dataGridMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridMain.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridMain.Size = new System.Drawing.Size(455, 178);
            this.dataGridMain.TabIndex = 11;
            this.dataGridMain.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridMain_CellFormatting);
            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.panelInfo.Controls.Add(this.lblOverView);
            this.panelInfo.Controls.Add(this.picIS7);
            this.panelInfo.Location = new System.Drawing.Point(9, 81);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(918, 72);
            this.panelInfo.TabIndex = 15;
            // 
            // lblOverView
            // 
            this.lblOverView.AutoSize = true;
            this.lblOverView.BackColor = System.Drawing.Color.Transparent;
            this.lblOverView.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(96)))), ((int)(((byte)(127)))));
            this.lblOverView.Location = new System.Drawing.Point(3, 40);
            this.lblOverView.Name = "lblOverView";
            this.lblOverView.Size = new System.Drawing.Size(123, 29);
            this.lblOverView.TabIndex = 0;
            this.lblOverView.Text = "Welcome...";
            // 
            // picIS7
            // 
            this.picIS7.Dock = System.Windows.Forms.DockStyle.Right;
            this.picIS7.Image = ((System.Drawing.Image)(resources.GetObject("picIS7.Image")));
            this.picIS7.Location = new System.Drawing.Point(450, 0);
            this.picIS7.Name = "picIS7";
            this.picIS7.Size = new System.Drawing.Size(468, 72);
            this.picIS7.TabIndex = 17;
            this.picIS7.TabStop = false;
            // 
            // toolMain
            // 
            this.toolMain.Dock = System.Windows.Forms.DockStyle.None;
            this.toolMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemViewLabel,
            this.toolItemViewOverall,
            this.toolItemViewTankInfo,
            this.toolItemViewBattles,
            this.toolItemRefresh,
            this.toolStripSeparator8,
            this.toolItemSettings,
            this.toolItemHelp,
            this.toolStripSeparator9});
            this.toolMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolMain.Location = new System.Drawing.Point(13, 0);
            this.toolMain.Name = "toolMain";
            this.toolMain.Size = new System.Drawing.Size(344, 25);
            this.toolMain.Stretch = true;
            this.toolMain.TabIndex = 13;
            this.toolMain.Text = "toolStrip1";
            // 
            // toolItemViewLabel
            // 
            this.toolItemViewLabel.Name = "toolItemViewLabel";
            this.toolItemViewLabel.Size = new System.Drawing.Size(35, 22);
            this.toolItemViewLabel.Text = "View:";
            // 
            // toolItemViewOverall
            // 
            this.toolItemViewOverall.Checked = true;
            this.toolItemViewOverall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolItemViewOverall.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolItemViewOverall.Image = ((System.Drawing.Image)(resources.GetObject("toolItemViewOverall.Image")));
            this.toolItemViewOverall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItemViewOverall.Name = "toolItemViewOverall";
            this.toolItemViewOverall.Size = new System.Drawing.Size(60, 22);
            this.toolItemViewOverall.Text = "&Overview";
            this.toolItemViewOverall.ToolTipText = " ";
            this.toolItemViewOverall.Click += new System.EventHandler(this.toolItemViewOverall_Click);
            // 
            // toolItemViewTankInfo
            // 
            this.toolItemViewTankInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolItemViewTankInfo.Image = ((System.Drawing.Image)(resources.GetObject("toolItemViewTankInfo.Image")));
            this.toolItemViewTankInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItemViewTankInfo.Name = "toolItemViewTankInfo";
            this.toolItemViewTankInfo.Size = new System.Drawing.Size(42, 22);
            this.toolItemViewTankInfo.Text = "&Tanks";
            this.toolItemViewTankInfo.Click += new System.EventHandler(this.toolItemViewTankInfo_Click);
            // 
            // toolItemViewBattles
            // 
            this.toolItemViewBattles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolItemViewBattles.Image = ((System.Drawing.Image)(resources.GetObject("toolItemViewBattles.Image")));
            this.toolItemViewBattles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItemViewBattles.Name = "toolItemViewBattles";
            this.toolItemViewBattles.Size = new System.Drawing.Size(46, 22);
            this.toolItemViewBattles.Text = "&Battles";
            this.toolItemViewBattles.Click += new System.EventHandler(this.toolItenViewBattles_Click);
            // 
            // toolItemRefresh
            // 
            this.toolItemRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolItemRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolItemRefresh.Image")));
            this.toolItemRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItemRefresh.Name = "toolItemRefresh";
            this.toolItemRefresh.Size = new System.Drawing.Size(23, 22);
            this.toolItemRefresh.Text = "Refresh grid";
            this.toolItemRefresh.Click += new System.EventHandler(this.toolItemRefresh_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolItemSettings
            // 
            this.toolItemSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemSettingsRun,
            this.toolItemSettingsDossierOptions,
            this.toolStripSeparator13,
            this.toolItemSettingsApp,
            this.toolItemSettingsDb,
            this.toolStripSeparator1,
            this.toolItemShowDbTables});
            this.toolItemSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolItemSettings.Image")));
            this.toolItemSettings.Name = "toolItemSettings";
            this.toolItemSettings.ShowDropDownArrow = false;
            this.toolItemSettings.Size = new System.Drawing.Size(69, 22);
            this.toolItemSettings.Text = "Settings";
            // 
            // toolItemSettingsRun
            // 
            this.toolItemSettingsRun.Name = "toolItemSettingsRun";
            this.toolItemSettingsRun.Size = new System.Drawing.Size(191, 22);
            this.toolItemSettingsRun.Text = "&Listen To Dossier File";
            this.toolItemSettingsRun.Click += new System.EventHandler(this.toolItemSettingsRun_Click);
            // 
            // toolItemSettingsDossierOptions
            // 
            this.toolItemSettingsDossierOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemSettingsRunManual,
            this.toolStripSeparator12,
            this.toolItemSettingsUpdateFromPrev,
            this.toolItemSettingsForceUpdateFromPrev});
            this.toolItemSettingsDossierOptions.Name = "toolItemSettingsDossierOptions";
            this.toolItemSettingsDossierOptions.Size = new System.Drawing.Size(191, 22);
            this.toolItemSettingsDossierOptions.Text = "D&ossier File Options";
            // 
            // toolItemSettingsRunManual
            // 
            this.toolItemSettingsRunManual.Name = "toolItemSettingsRunManual";
            this.toolItemSettingsRunManual.Size = new System.Drawing.Size(285, 22);
            this.toolItemSettingsRunManual.Text = "&Manual Dossier File Check";
            this.toolItemSettingsRunManual.Click += new System.EventHandler(this.toolItemSettingsRunManual_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(282, 6);
            // 
            // toolItemSettingsUpdateFromPrev
            // 
            this.toolItemSettingsUpdateFromPrev.Name = "toolItemSettingsUpdateFromPrev";
            this.toolItemSettingsUpdateFromPrev.Size = new System.Drawing.Size(285, 22);
            this.toolItemSettingsUpdateFromPrev.Text = "&Normal Check Previous Dossier File";
            this.toolItemSettingsUpdateFromPrev.Click += new System.EventHandler(this.toolItemSettingsUpdateFromPrev_Click);
            // 
            // toolItemSettingsForceUpdateFromPrev
            // 
            this.toolItemSettingsForceUpdateFromPrev.Name = "toolItemSettingsForceUpdateFromPrev";
            this.toolItemSettingsForceUpdateFromPrev.Size = new System.Drawing.Size(285, 22);
            this.toolItemSettingsForceUpdateFromPrev.Text = "&Force Update From Previous Dossier File";
            this.toolItemSettingsForceUpdateFromPrev.Click += new System.EventHandler(this.toolItemSettingsForceUpdateFromPrev_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(188, 6);
            // 
            // toolItemSettingsApp
            // 
            this.toolItemSettingsApp.Name = "toolItemSettingsApp";
            this.toolItemSettingsApp.Size = new System.Drawing.Size(191, 22);
            this.toolItemSettingsApp.Text = "&Application Settings";
            this.toolItemSettingsApp.Click += new System.EventHandler(this.toolItemSettingsApp_Click);
            // 
            // toolItemSettingsDb
            // 
            this.toolItemSettingsDb.Name = "toolItemSettingsDb";
            this.toolItemSettingsDb.Size = new System.Drawing.Size(191, 22);
            this.toolItemSettingsDb.Text = "&Database Settings";
            this.toolItemSettingsDb.Click += new System.EventHandler(this.toolItemSettingsDb_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(188, 6);
            // 
            // toolItemShowDbTables
            // 
            this.toolItemShowDbTables.Name = "toolItemShowDbTables";
            this.toolItemShowDbTables.Size = new System.Drawing.Size(191, 22);
            this.toolItemShowDbTables.Text = "Show Database &Tables";
            this.toolItemShowDbTables.Click += new System.EventHandler(this.toolItemShowDbTables_Click);
            // 
            // toolItemHelp
            // 
            this.toolItemHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolItemHelp.Image")));
            this.toolItemHelp.Name = "toolItemHelp";
            this.toolItemHelp.Size = new System.Drawing.Size(23, 22);
            this.toolItemHelp.Click += new System.EventHandler(this.toolItemHelp_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // panelStrip
            // 
            this.panelStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.panelStrip.Controls.Add(this.menuMain);
            this.panelStrip.Controls.Add(this.toolMain);
            this.panelStrip.Location = new System.Drawing.Point(12, 48);
            this.panelStrip.Name = "panelStrip";
            this.panelStrip.Size = new System.Drawing.Size(915, 27);
            this.panelStrip.TabIndex = 14;
            // 
            // picResize
            // 
            this.picResize.Image = ((System.Drawing.Image)(resources.GetObject("picResize.Image")));
            this.picResize.Location = new System.Drawing.Point(869, 0);
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
            this.panelMaster.Controls.Add(this.panelInfo);
            this.panelMaster.Controls.Add(this.panelTop);
            this.panelMaster.Controls.Add(this.panelStrip);
            this.panelMaster.Controls.Add(this.panelMain);
            this.panelMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMaster.Location = new System.Drawing.Point(0, 0);
            this.panelMaster.Name = "panelMaster";
            this.panelMaster.Size = new System.Drawing.Size(936, 414);
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
            this.panelStatus.Location = new System.Drawing.Point(12, 362);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(915, 24);
            this.panelStatus.TabIndex = 16;
            // 
            // lblStatus2
            // 
            this.lblStatus2.AutoSize = true;
            this.lblStatus2.ForeColor = System.Drawing.Color.DarkGray;
            this.lblStatus2.Location = new System.Drawing.Point(67, 5);
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
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 6);
            // 
            // timerPanelSlide
            // 
            this.timerPanelSlide.Interval = 5;
            this.timerPanelSlide.Tick += new System.EventHandler(this.timerPanelSlide_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(936, 414);
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
            ((System.ComponentModel.ISupportInitialize)(this.picNormalize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelScrollArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMain)).EndInit();
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIS7)).EndInit();
            this.toolMain.ResumeLayout(false);
            this.toolMain.PerformLayout();
            this.panelStrip.ResumeLayout(false);
            this.panelStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResize)).EndInit();
            this.panelMaster.ResumeLayout(false);
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest_WotURL;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest_ImportTurret;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest_ImportGun;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest_ImportRadio;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest_ProgressBar;
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
        private System.Windows.Forms.Label lblStatus2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerStatus2;
        private System.Windows.Forms.ToolStripMenuItem menuItemTest_ViewRange;
        private System.Windows.Forms.DataGridView dataGridMain;
        private System.Windows.Forms.Panel panelScrollbar;
        private System.Windows.Forms.ToolStrip toolMain;
        private System.Windows.Forms.ToolStripButton toolItemRefresh;
        private System.Windows.Forms.Panel panelStrip;
        private System.Windows.Forms.ToolStripLabel toolItemViewLabel;
        private System.Windows.Forms.ToolStripButton toolItemViewOverall;
        private System.Windows.Forms.ToolStripButton toolItemViewTankInfo;
        private System.Windows.Forms.ToolStripButton toolItemViewBattles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripDropDownButton toolItemSettings;
        private System.Windows.Forms.ToolStripButton toolItemHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem toolItemSettingsRun;
        private System.Windows.Forms.ToolStripMenuItem toolItemSettingsDossierOptions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem toolItemSettingsApp;
        private System.Windows.Forms.ToolStripMenuItem toolItemSettingsDb;
        private System.Windows.Forms.ToolStripMenuItem toolItemSettingsRunManual;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem toolItemSettingsUpdateFromPrev;
        private System.Windows.Forms.ToolStripMenuItem toolItemSettingsForceUpdateFromPrev;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label lblOverView;
        private System.Windows.Forms.PictureBox picIS7;
        private System.Windows.Forms.Timer timerPanelSlide;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolItemShowDbTables;
        private System.Windows.Forms.Panel panelScrollArea;
    }
}

