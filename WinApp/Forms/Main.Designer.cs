namespace WinApp.Forms
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
            BadThemeContainerControl.MainAreaClass mainAreaClass1 = new BadThemeContainerControl.MainAreaClass();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timerStatus2 = new System.Windows.Forms.Timer(this.components);
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.fileSystemWatcherNewBattle = new System.IO.FileSystemWatcher();
            this.imageListToolStrip = new System.Windows.Forms.ImageList(this.components);
            this.imageGrid = new System.Windows.Forms.ImageList(this.components);
            this.timerWoTAffnity = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.MainTheme = new BadForm();
            this.toolMain = new WinApp.Code.ToolStripEx(this.components);
            this.mWoT = new System.Windows.Forms.ToolStripButton();
            this.mViewLabel = new System.Windows.Forms.ToolStripLabel();
            this.mViewOverall = new System.Windows.Forms.ToolStripButton();
            this.mViewTankInfo = new System.Windows.Forms.ToolStripButton();
            this.mViewBattles = new System.Windows.Forms.ToolStripButton();
            this.mViewMaps = new System.Windows.Forms.ToolStripButton();
            this.mRefresh = new System.Windows.Forms.ToolStripButton();
            this.mRefreshSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mColumnSelect = new System.Windows.Forms.ToolStripDropDownButton();
            this.mColumnSelect_01 = new System.Windows.Forms.ToolStripMenuItem();
            this.mColumnSelect_02 = new System.Windows.Forms.ToolStripMenuItem();
            this.mColumnSelect_03 = new System.Windows.Forms.ToolStripMenuItem();
            this.mColumnSelect_04 = new System.Windows.Forms.ToolStripMenuItem();
            this.mColumnSelect_05 = new System.Windows.Forms.ToolStripMenuItem();
            this.mColumnSelect_06 = new System.Windows.Forms.ToolStripMenuItem();
            this.mColumnSelect_07 = new System.Windows.Forms.ToolStripMenuItem();
            this.mColumnSelect_08 = new System.Windows.Forms.ToolStripMenuItem();
            this.mColumnSelect_09 = new System.Windows.Forms.ToolStripMenuItem();
            this.mColumnSelect_10 = new System.Windows.Forms.ToolStripMenuItem();
            this.mColumnSelect_11 = new System.Windows.Forms.ToolStripMenuItem();
            this.mColumnSelect_12 = new System.Windows.Forms.ToolStripMenuItem();
            this.mColumnSelect_13 = new System.Windows.Forms.ToolStripMenuItem();
            this.mColumnSelect_14 = new System.Windows.Forms.ToolStripMenuItem();
            this.mColumnSelect_15 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mColumnSelect_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.mMapViewType = new System.Windows.Forms.ToolStripDropDownButton();
            this.mMapDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.mMapDescr = new System.Windows.Forms.ToolStripMenuItem();
            this.mMapDescrLarge = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.mMapShowAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mMapShowOld = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter = new System.Windows.Forms.ToolStripDropDownButton();
            this.mTankFilter_Country = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_CountryGermany = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_CountryUSSR = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_CountryUSA = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_CountryFrance = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_CountryUK = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_CountryChina = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_CountryJapan = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_CountryCzechoslovakia = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Type = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_TypeLT = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_TypeMT = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_TypeHT = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_TypeTD = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_TypeSPG = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Tier = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Tier1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Tier2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Tier3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Tier4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Tier5 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Tier6 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Tier7 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Tier8 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Tier9 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Tier10 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Search = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_FavSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mTankFilter_All = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_All_NotOwned = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Fav01 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Fav02 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Fav03 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Fav04 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Fav05 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Fav06 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Fav07 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Fav08 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Fav09 = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_Fav10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.mTankFilter_GetInGarage = new System.Windows.Forms.ToolStripMenuItem();
            this.mTankFilter_EditFavList = new System.Windows.Forms.ToolStripMenuItem();
            this.mMode = new System.Windows.Forms.ToolStripDropDownButton();
            this.mModeAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.mModeRandomTankCompany = new System.Windows.Forms.ToolStripMenuItem();
            this.mModeRandom = new System.Windows.Forms.ToolStripMenuItem();
            this.mModeRandomSoloPlatoon = new System.Windows.Forms.ToolStripMenuItem();
            this.mModeRandomSolo = new System.Windows.Forms.ToolStripMenuItem();
            this.mRandomPlatoon = new System.Windows.Forms.ToolStripMenuItem();
            this.mRandomPlatoon2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mRandomPlatoon3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mModeCompany = new System.Windows.Forms.ToolStripMenuItem();
            this.mModeClan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorForBattleView = new System.Windows.Forms.ToolStripSeparator();
            this.mModeHistorical = new System.Windows.Forms.ToolStripMenuItem();
            this.mModeTeam = new System.Windows.Forms.ToolStripMenuItem();
            this.mModeTeamRanked = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.mModeGlobalMap = new System.Windows.Forms.ToolStripMenuItem();
            this.mModeSkrimishes = new System.Windows.Forms.ToolStripMenuItem();
            this.mModeBattleForStronghold = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.mModeSpecial = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattles = new System.Windows.Forms.ToolStripDropDownButton();
            this.mBattles1d = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattlesYesterday = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mBattles2d = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattles3d = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.mBattles1w = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattles2w = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.mBattles1m = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattles3m = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattles6m = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.mBattles1y = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattles2y = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.mBattlesCustomUse = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattlesCustomChange = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.mBattlesAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattleGroup = new System.Windows.Forms.ToolStripDropDownButton();
            this.mBattleGroup_No = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mBattleGroup_TankAverage = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattleGroup_TankSum = new System.Windows.Forms.ToolStripMenuItem();
            this.mHomeView = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator31 = new System.Windows.Forms.ToolStripSeparator();
            this.mHomeViewDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.mHomeViewClassic = new System.Windows.Forms.ToolStripMenuItem();
            this.mHomeViewRecentSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mHomeViewRecent1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mHomeViewRecent2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mHomeViewRecent3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mHomeViewRecent4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mHomeViewRecent5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.mHomeViewFileLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mHomeViewRedraw = new System.Windows.Forms.ToolStripMenuItem();
            this.mHomeViewClearRecentList = new System.Windows.Forms.ToolStripMenuItem();
            this.mHomeViewEdit = new System.Windows.Forms.ToolStripDropDownButton();
            this.mHomeViewEditMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator32 = new System.Windows.Forms.ToolStripSeparator();
            this.mGadgetGauges = new System.Windows.Forms.ToolStripMenuItem();
            this.mGadgetWR = new System.Windows.Forms.ToolStripMenuItem();
            this.mGadgetRWR = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
            this.mGadgetWN9 = new System.Windows.Forms.ToolStripMenuItem();
            this.mGadgetWN8 = new System.Windows.Forms.ToolStripMenuItem();
            this.mGadgetWN7 = new System.Windows.Forms.ToolStripMenuItem();
            this.mGadgetEFF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.mGadgetKillDeath = new System.Windows.Forms.ToolStripMenuItem();
            this.mGadgetDamageCausedReceived = new System.Windows.Forms.ToolStripMenuItem();
            this.mGadgetCharts = new System.Windows.Forms.ToolStripMenuItem();
            this.mGadgetChartTier = new System.Windows.Forms.ToolStripMenuItem();
            this.mGadgetChartTankType = new System.Windows.Forms.ToolStripMenuItem();
            this.mGadgetChartNation = new System.Windows.Forms.ToolStripMenuItem();
            this.mGadgetGrids = new System.Windows.Forms.ToolStripMenuItem();
            this.mGadgetTotalStatsDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.mGadgetTotalStats = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
            this.mGadgetAddBattleModeStats = new System.Windows.Forms.ToolStripMenuItem();
            this.mGadgetAddTankTypeStats = new System.Windows.Forms.ToolStripMenuItem();
            this.mGadgetAddHeader = new System.Windows.Forms.ToolStripMenuItem();
            this.mGadgetAddImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator33 = new System.Windows.Forms.ToolStripSeparator();
            this.mHomeViewFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mHomeViewFileShowFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mGadgetRemoveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator34 = new System.Windows.Forms.ToolStripSeparator();
            this.mViewChart = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.mSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.mSettingsRun = new System.Windows.Forms.ToolStripMenuItem();
            this.mSettingsRunBattleCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.mUpdateDataFromAPI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.mRecalcTankStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.mRecalcBattleRatings = new System.Windows.Forms.ToolStripMenuItem();
            this.mRecalcBattleWN9 = new System.Windows.Forms.ToolStripMenuItem();
            this.mRecalcBattleWN8 = new System.Windows.Forms.ToolStripMenuItem();
            this.mRecalcBattleWN7 = new System.Windows.Forms.ToolStripMenuItem();
            this.mRecalcBattleEFF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.mRecalcBattleAllRatings = new System.Windows.Forms.ToolStripMenuItem();
            this.mRecalcBattleCreditsPerTank = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.mSettingsShowLogFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mShowDbTables = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mAppSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.mExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.mWotNumWebUserGuide = new System.Windows.Forms.ToolStripMenuItem();
            this.mWotNumWebForum = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.mHelpCheckVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.mHelpMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
            this.mVBaddict = new System.Windows.Forms.ToolStripButton();
            this.lblStatusRowCount = new System.Windows.Forms.Label();
            this.panelMainArea = new System.Windows.Forms.Panel();
            this.scrollCorner = new BadScrollBarCorner();
            this.scrollY = new BadScrollBar();
            this.dataGridMain = new System.Windows.Forms.DataGridView();
            this.scrollX = new BadScrollBar();
            this.lblStatus2 = new System.Windows.Forms.Label();
            this.lblStatus1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherNewBattle)).BeginInit();
            this.MainTheme.SuspendLayout();
            this.toolMain.SuspendLayout();
            this.panelMainArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMain)).BeginInit();
            this.SuspendLayout();
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
            // fileSystemWatcherNewBattle
            // 
            this.fileSystemWatcherNewBattle.EnableRaisingEvents = true;
            this.fileSystemWatcherNewBattle.SynchronizingObject = this;
            // 
            // imageListToolStrip
            // 
            this.imageListToolStrip.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListToolStrip.ImageStream")));
            this.imageListToolStrip.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListToolStrip.Images.SetKeyName(0, "check.png");
            this.imageListToolStrip.Images.SetKeyName(1, "iconTank.png");
            this.imageListToolStrip.Images.SetKeyName(2, "Chart_new1.png");
            this.imageListToolStrip.Images.SetKeyName(3, "xp.png");
            this.imageListToolStrip.Images.SetKeyName(4, "filter.png");
            this.imageListToolStrip.Images.SetKeyName(5, "tank_add.png");
            this.imageListToolStrip.Images.SetKeyName(6, "tank_remove.png");
            this.imageListToolStrip.Images.SetKeyName(7, "tank_new.png");
            this.imageListToolStrip.Images.SetKeyName(8, "delete.png");
            this.imageListToolStrip.Images.SetKeyName(9, "iconBattle.png");
            this.imageListToolStrip.Images.SetKeyName(10, "wn8_ver2.png");
            this.imageListToolStrip.Images.SetKeyName(11, "filter_clear2.png");
            this.imageListToolStrip.Images.SetKeyName(12, "iconBattleSummary2.png");
            this.imageListToolStrip.Images.SetKeyName(13, "copyRowToClipboard.png");
            this.imageListToolStrip.Images.SetKeyName(14, "replay2.png");
            this.imageListToolStrip.Images.SetKeyName(15, "vbAddictSmall.png");
            this.imageListToolStrip.Images.SetKeyName(16, "WoT_3.png");
            this.imageListToolStrip.Images.SetKeyName(17, "iconFavs2.png");
            // 
            // imageGrid
            // 
            this.imageGrid.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageGrid.ImageStream")));
            this.imageGrid.TransparentColor = System.Drawing.Color.Transparent;
            this.imageGrid.Images.SetKeyName(0, "grid.png");
            // 
            // timerWoTAffnity
            // 
            this.timerWoTAffnity.Interval = 6000;
            this.timerWoTAffnity.Tick += new System.EventHandler(this.timerWoTAffnity_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "New battle data fetched";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Wot Numbers";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainTheme
            // 
            this.MainTheme.BackColor = System.Drawing.Color.Fuchsia;
            this.MainTheme.Controls.Add(this.toolMain);
            this.MainTheme.Controls.Add(this.lblStatusRowCount);
            this.MainTheme.Controls.Add(this.panelMainArea);
            this.MainTheme.Controls.Add(this.lblStatus2);
            this.MainTheme.Controls.Add(this.lblStatus1);
            this.MainTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MainTheme.FormExitAsMinimize = false;
            this.MainTheme.FormFooter = true;
            this.MainTheme.FormFooterHeight = 26;
            this.MainTheme.FormInnerBorder = 0;
            this.MainTheme.FormMargin = 0;
            this.MainTheme.Image = ((System.Drawing.Image)(resources.GetObject("MainTheme.Image")));
            this.MainTheme.Location = new System.Drawing.Point(0, 0);
            this.MainTheme.MainArea = mainAreaClass1;
            this.MainTheme.Name = "MainTheme";
            this.MainTheme.Resizable = true;
            this.MainTheme.Size = new System.Drawing.Size(1119, 631);
            this.MainTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("MainTheme.SystemExitImage")));
            this.MainTheme.SystemMaximizeImage = ((System.Drawing.Image)(resources.GetObject("MainTheme.SystemMaximizeImage")));
            this.MainTheme.SystemMinimizeImage = ((System.Drawing.Image)(resources.GetObject("MainTheme.SystemMinimizeImage")));
            this.MainTheme.TabIndex = 18;
            this.MainTheme.Text = "Wot Numbers";
            this.MainTheme.TitleHeight = 53;
            // 
            // toolMain
            // 
            this.toolMain.AutoSize = false;
            this.toolMain.Dock = System.Windows.Forms.DockStyle.None;
            this.toolMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mWoT,
            this.mViewLabel,
            this.mViewOverall,
            this.mViewTankInfo,
            this.mViewBattles,
            this.mViewMaps,
            this.mRefresh,
            this.mRefreshSeparator,
            this.mColumnSelect,
            this.mMapViewType,
            this.mTankFilter,
            this.mMode,
            this.mBattles,
            this.mBattleGroup,
            this.mHomeView,
            this.mHomeViewEdit,
            this.toolStripSeparator34,
            this.mViewChart,
            this.toolStripSeparator16,
            this.mSettings,
            this.mHelp,
            this.toolStripSeparator30,
            this.mVBaddict});
            this.toolMain.Location = new System.Drawing.Point(9, 29);
            this.toolMain.Name = "toolMain";
            this.toolMain.Size = new System.Drawing.Size(1102, 25);
            this.toolMain.Stretch = true;
            this.toolMain.TabIndex = 18;
            this.toolMain.Text = "toolStripEx1";
            // 
            // mWoT
            // 
            this.mWoT.AutoSize = false;
            this.mWoT.BackColor = System.Drawing.Color.Transparent;
            this.mWoT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mWoT.Image = ((System.Drawing.Image)(resources.GetObject("mWoT.Image")));
            this.mWoT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mWoT.Name = "mWoT";
            this.mWoT.Size = new System.Drawing.Size(22, 22);
            this.mWoT.ToolTipText = "Start World of Tanks";
            this.mWoT.Click += new System.EventHandler(this.mWoT_Click);
            // 
            // mViewLabel
            // 
            this.mViewLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mViewLabel.Name = "mViewLabel";
            this.mViewLabel.Size = new System.Drawing.Size(35, 22);
            this.mViewLabel.Text = "View:";
            // 
            // mViewOverall
            // 
            this.mViewOverall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.mViewOverall.Checked = true;
            this.mViewOverall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mViewOverall.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mViewOverall.Image = ((System.Drawing.Image)(resources.GetObject("mViewOverall.Image")));
            this.mViewOverall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mViewOverall.Name = "mViewOverall";
            this.mViewOverall.Size = new System.Drawing.Size(60, 22);
            this.mViewOverall.Text = "&Home";
            this.mViewOverall.ToolTipText = " Home View";
            this.mViewOverall.Click += new System.EventHandler(this.mViewOverall_Click);
            // 
            // mViewTankInfo
            // 
            this.mViewTankInfo.BackColor = System.Drawing.Color.Fuchsia;
            this.mViewTankInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mViewTankInfo.Image = ((System.Drawing.Image)(resources.GetObject("mViewTankInfo.Image")));
            this.mViewTankInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mViewTankInfo.Name = "mViewTankInfo";
            this.mViewTankInfo.Size = new System.Drawing.Size(57, 22);
            this.mViewTankInfo.Text = "&Tanks";
            this.mViewTankInfo.ToolTipText = "Tank View";
            this.mViewTankInfo.Click += new System.EventHandler(this.mViewTankInfo_Click);
            // 
            // mViewBattles
            // 
            this.mViewBattles.BackColor = System.Drawing.Color.Fuchsia;
            this.mViewBattles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mViewBattles.Image = ((System.Drawing.Image)(resources.GetObject("mViewBattles.Image")));
            this.mViewBattles.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mViewBattles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mViewBattles.Name = "mViewBattles";
            this.mViewBattles.Size = new System.Drawing.Size(62, 22);
            this.mViewBattles.Text = "&Battles";
            this.mViewBattles.ToolTipText = "Battle View";
            this.mViewBattles.Click += new System.EventHandler(this.mViewBattles_Click);
            // 
            // mViewMaps
            // 
            this.mViewMaps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mViewMaps.Image = ((System.Drawing.Image)(resources.GetObject("mViewMaps.Image")));
            this.mViewMaps.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mViewMaps.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mViewMaps.Name = "mViewMaps";
            this.mViewMaps.Size = new System.Drawing.Size(51, 22);
            this.mViewMaps.Text = "Maps";
            this.mViewMaps.Click += new System.EventHandler(this.mViewMaps_Click);
            // 
            // mRefresh
            // 
            this.mRefresh.AutoSize = false;
            this.mRefresh.BackColor = System.Drawing.Color.Fuchsia;
            this.mRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mRefresh.Image = ((System.Drawing.Image)(resources.GetObject("mRefresh.Image")));
            this.mRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mRefresh.Name = "mRefresh";
            this.mRefresh.Size = new System.Drawing.Size(22, 22);
            this.mRefresh.Text = "Refresh";
            this.mRefresh.ToolTipText = "Refresh";
            this.mRefresh.Click += new System.EventHandler(this.toolItemRefresh_Click);
            // 
            // mRefreshSeparator
            // 
            this.mRefreshSeparator.Name = "mRefreshSeparator";
            this.mRefreshSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // mColumnSelect
            // 
            this.mColumnSelect.BackColor = System.Drawing.Color.Transparent;
            this.mColumnSelect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mColumnSelect_01,
            this.mColumnSelect_02,
            this.mColumnSelect_03,
            this.mColumnSelect_04,
            this.mColumnSelect_05,
            this.mColumnSelect_06,
            this.mColumnSelect_07,
            this.mColumnSelect_08,
            this.mColumnSelect_09,
            this.mColumnSelect_10,
            this.mColumnSelect_11,
            this.mColumnSelect_12,
            this.mColumnSelect_13,
            this.mColumnSelect_14,
            this.mColumnSelect_15,
            this.toolStripSeparator5,
            this.mColumnSelect_Edit});
            this.mColumnSelect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mColumnSelect.Image = ((System.Drawing.Image)(resources.GetObject("mColumnSelect.Image")));
            this.mColumnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mColumnSelect.Name = "mColumnSelect";
            this.mColumnSelect.ShowDropDownArrow = false;
            this.mColumnSelect.Size = new System.Drawing.Size(65, 22);
            this.mColumnSelect.Text = "Default";
            this.mColumnSelect.ToolTipText = "Select Tank/Battle View";
            this.mColumnSelect.Visible = false;
            // 
            // mColumnSelect_01
            // 
            this.mColumnSelect_01.Checked = true;
            this.mColumnSelect_01.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mColumnSelect_01.Name = "mColumnSelect_01";
            this.mColumnSelect_01.Size = new System.Drawing.Size(194, 22);
            this.mColumnSelect_01.Text = "Col List #1";
            this.mColumnSelect_01.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
            this.mColumnSelect_01.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mColumnSelect_02
            // 
            this.mColumnSelect_02.Name = "mColumnSelect_02";
            this.mColumnSelect_02.Size = new System.Drawing.Size(194, 22);
            this.mColumnSelect_02.Text = "Col List #2";
            this.mColumnSelect_02.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
            this.mColumnSelect_02.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mColumnSelect_03
            // 
            this.mColumnSelect_03.Name = "mColumnSelect_03";
            this.mColumnSelect_03.Size = new System.Drawing.Size(194, 22);
            this.mColumnSelect_03.Text = "Col List #3";
            this.mColumnSelect_03.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
            this.mColumnSelect_03.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mColumnSelect_04
            // 
            this.mColumnSelect_04.Name = "mColumnSelect_04";
            this.mColumnSelect_04.Size = new System.Drawing.Size(194, 22);
            this.mColumnSelect_04.Text = "Col List #4";
            this.mColumnSelect_04.Visible = false;
            this.mColumnSelect_04.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
            this.mColumnSelect_04.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mColumnSelect_05
            // 
            this.mColumnSelect_05.Name = "mColumnSelect_05";
            this.mColumnSelect_05.Size = new System.Drawing.Size(194, 22);
            this.mColumnSelect_05.Text = "Col List #5";
            this.mColumnSelect_05.Visible = false;
            this.mColumnSelect_05.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
            this.mColumnSelect_05.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mColumnSelect_06
            // 
            this.mColumnSelect_06.Name = "mColumnSelect_06";
            this.mColumnSelect_06.Size = new System.Drawing.Size(194, 22);
            this.mColumnSelect_06.Text = "Col List #6";
            this.mColumnSelect_06.Visible = false;
            this.mColumnSelect_06.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
            this.mColumnSelect_06.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mColumnSelect_07
            // 
            this.mColumnSelect_07.Name = "mColumnSelect_07";
            this.mColumnSelect_07.Size = new System.Drawing.Size(194, 22);
            this.mColumnSelect_07.Text = "Col List #7";
            this.mColumnSelect_07.Visible = false;
            this.mColumnSelect_07.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
            this.mColumnSelect_07.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mColumnSelect_08
            // 
            this.mColumnSelect_08.Name = "mColumnSelect_08";
            this.mColumnSelect_08.Size = new System.Drawing.Size(194, 22);
            this.mColumnSelect_08.Text = "Col List #8";
            this.mColumnSelect_08.Visible = false;
            this.mColumnSelect_08.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
            this.mColumnSelect_08.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mColumnSelect_09
            // 
            this.mColumnSelect_09.Name = "mColumnSelect_09";
            this.mColumnSelect_09.Size = new System.Drawing.Size(194, 22);
            this.mColumnSelect_09.Text = "Col List #9";
            this.mColumnSelect_09.Visible = false;
            this.mColumnSelect_09.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
            this.mColumnSelect_09.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mColumnSelect_10
            // 
            this.mColumnSelect_10.Name = "mColumnSelect_10";
            this.mColumnSelect_10.Size = new System.Drawing.Size(194, 22);
            this.mColumnSelect_10.Text = "Col List #10";
            this.mColumnSelect_10.Visible = false;
            this.mColumnSelect_10.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
            this.mColumnSelect_10.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mColumnSelect_11
            // 
            this.mColumnSelect_11.Name = "mColumnSelect_11";
            this.mColumnSelect_11.Size = new System.Drawing.Size(194, 22);
            this.mColumnSelect_11.Text = "Col List #11";
            this.mColumnSelect_11.Visible = false;
            this.mColumnSelect_11.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
            this.mColumnSelect_11.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mColumnSelect_12
            // 
            this.mColumnSelect_12.Name = "mColumnSelect_12";
            this.mColumnSelect_12.Size = new System.Drawing.Size(194, 22);
            this.mColumnSelect_12.Text = "Col List #12";
            this.mColumnSelect_12.Visible = false;
            this.mColumnSelect_12.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
            this.mColumnSelect_12.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mColumnSelect_13
            // 
            this.mColumnSelect_13.Name = "mColumnSelect_13";
            this.mColumnSelect_13.Size = new System.Drawing.Size(194, 22);
            this.mColumnSelect_13.Text = "Col List #13";
            this.mColumnSelect_13.Visible = false;
            this.mColumnSelect_13.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
            this.mColumnSelect_13.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mColumnSelect_14
            // 
            this.mColumnSelect_14.Name = "mColumnSelect_14";
            this.mColumnSelect_14.Size = new System.Drawing.Size(194, 22);
            this.mColumnSelect_14.Text = "Col List #14";
            this.mColumnSelect_14.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
            this.mColumnSelect_14.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mColumnSelect_15
            // 
            this.mColumnSelect_15.Name = "mColumnSelect_15";
            this.mColumnSelect_15.Size = new System.Drawing.Size(194, 22);
            this.mColumnSelect_15.Text = "Col List #15";
            this.mColumnSelect_15.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
            this.mColumnSelect_15.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(191, 6);
            // 
            // mColumnSelect_Edit
            // 
            this.mColumnSelect_Edit.Name = "mColumnSelect_Edit";
            this.mColumnSelect_Edit.Size = new System.Drawing.Size(194, 22);
            this.mColumnSelect_Edit.Text = "Edit Tank/Battle View...";
            this.mColumnSelect_Edit.Click += new System.EventHandler(this.toolItemColumnSelect_Edit_Click);
            // 
            // mMapViewType
            // 
            this.mMapViewType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMapDefault,
            this.mMapDescr,
            this.mMapDescrLarge,
            this.toolStripSeparator14,
            this.mMapShowAll,
            this.mMapShowOld});
            this.mMapViewType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mMapViewType.Image = ((System.Drawing.Image)(resources.GetObject("mMapViewType.Image")));
            this.mMapViewType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mMapViewType.Name = "mMapViewType";
            this.mMapViewType.ShowDropDownArrow = false;
            this.mMapViewType.Size = new System.Drawing.Size(65, 22);
            this.mMapViewType.Text = "Default";
            this.mMapViewType.ToolTipText = "Select Map View";
            this.mMapViewType.Visible = false;
            // 
            // mMapDefault
            // 
            this.mMapDefault.Checked = true;
            this.mMapDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mMapDefault.Name = "mMapDefault";
            this.mMapDefault.Size = new System.Drawing.Size(219, 22);
            this.mMapDefault.Text = "Default";
            this.mMapDefault.Click += new System.EventHandler(this.mMapViewType_Click);
            this.mMapDefault.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mMapDescr
            // 
            this.mMapDescr.Name = "mMapDescr";
            this.mMapDescr.Size = new System.Drawing.Size(219, 22);
            this.mMapDescr.Text = "Map Description";
            this.mMapDescr.Click += new System.EventHandler(this.mMapViewType_Click);
            this.mMapDescr.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mMapDescrLarge
            // 
            this.mMapDescrLarge.Name = "mMapDescrLarge";
            this.mMapDescrLarge.Size = new System.Drawing.Size(219, 22);
            this.mMapDescrLarge.Text = "Map Large Images";
            this.mMapDescrLarge.Click += new System.EventHandler(this.mMapViewType_Click);
            this.mMapDescrLarge.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(216, 6);
            // 
            // mMapShowAll
            // 
            this.mMapShowAll.Name = "mMapShowAll";
            this.mMapShowAll.Size = new System.Drawing.Size(219, 22);
            this.mMapShowAll.Text = "Show Maps Without Battles";
            this.mMapShowAll.Click += new System.EventHandler(this.mMapShowAll_Click);
            this.mMapShowAll.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mMapShowOld
            // 
            this.mMapShowOld.Checked = true;
            this.mMapShowOld.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mMapShowOld.Name = "mMapShowOld";
            this.mMapShowOld.Size = new System.Drawing.Size(219, 22);
            this.mMapShowOld.Text = "Show Old / Obsolete Maps";
            this.mMapShowOld.Click += new System.EventHandler(this.mMapShowOld_Click);
            this.mMapShowOld.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter
            // 
            this.mTankFilter.BackColor = System.Drawing.Color.Transparent;
            this.mTankFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mTankFilter_Country,
            this.mTankFilter_Type,
            this.mTankFilter_Tier,
            this.mTankFilter_Search,
            this.mTankFilter_Clear,
            this.mTankFilter_FavSeparator,
            this.mTankFilter_All,
            this.mTankFilter_All_NotOwned,
            this.mTankFilter_Fav01,
            this.mTankFilter_Fav02,
            this.mTankFilter_Fav03,
            this.mTankFilter_Fav04,
            this.mTankFilter_Fav05,
            this.mTankFilter_Fav06,
            this.mTankFilter_Fav07,
            this.mTankFilter_Fav08,
            this.mTankFilter_Fav09,
            this.mTankFilter_Fav10,
            this.toolStripSeparator12,
            this.mTankFilter_GetInGarage,
            this.mTankFilter_EditFavList});
            this.mTankFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mTankFilter.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter.Image")));
            this.mTankFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mTankFilter.Name = "mTankFilter";
            this.mTankFilter.ShowDropDownArrow = false;
            this.mTankFilter.Size = new System.Drawing.Size(77, 22);
            this.mTankFilter.Text = "My Tanks";
            this.mTankFilter.ToolTipText = "Select Favourite Tank List";
            this.mTankFilter.Visible = false;
            this.mTankFilter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_MouseDown);
            // 
            // mTankFilter_Country
            // 
            this.mTankFilter_Country.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mTankFilter_CountryGermany,
            this.mTankFilter_CountryUSSR,
            this.mTankFilter_CountryUSA,
            this.mTankFilter_CountryFrance,
            this.mTankFilter_CountryUK,
            this.mTankFilter_CountryChina,
            this.mTankFilter_CountryJapan,
            this.mTankFilter_CountryCzechoslovakia});
            this.mTankFilter_Country.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Country.Image")));
            this.mTankFilter_Country.Name = "mTankFilter_Country";
            this.mTankFilter_Country.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_Country.Text = "Nation";
            // 
            // mTankFilter_CountryGermany
            // 
            this.mTankFilter_CountryGermany.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryGermany.Image")));
            this.mTankFilter_CountryGermany.Name = "mTankFilter_CountryGermany";
            this.mTankFilter_CountryGermany.Size = new System.Drawing.Size(155, 22);
            this.mTankFilter_CountryGermany.Text = "Germany";
            this.mTankFilter_CountryGermany.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
            this.mTankFilter_CountryGermany.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
            this.mTankFilter_CountryGermany.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_CountryUSSR
            // 
            this.mTankFilter_CountryUSSR.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryUSSR.Image")));
            this.mTankFilter_CountryUSSR.Name = "mTankFilter_CountryUSSR";
            this.mTankFilter_CountryUSSR.Size = new System.Drawing.Size(155, 22);
            this.mTankFilter_CountryUSSR.Text = "U.S.S.R.";
            this.mTankFilter_CountryUSSR.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
            this.mTankFilter_CountryUSSR.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
            this.mTankFilter_CountryUSSR.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_CountryUSA
            // 
            this.mTankFilter_CountryUSA.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryUSA.Image")));
            this.mTankFilter_CountryUSA.Name = "mTankFilter_CountryUSA";
            this.mTankFilter_CountryUSA.Size = new System.Drawing.Size(155, 22);
            this.mTankFilter_CountryUSA.Text = "U.S.A.";
            this.mTankFilter_CountryUSA.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
            this.mTankFilter_CountryUSA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
            this.mTankFilter_CountryUSA.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_CountryFrance
            // 
            this.mTankFilter_CountryFrance.BackColor = System.Drawing.SystemColors.Control;
            this.mTankFilter_CountryFrance.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryFrance.Image")));
            this.mTankFilter_CountryFrance.Name = "mTankFilter_CountryFrance";
            this.mTankFilter_CountryFrance.Size = new System.Drawing.Size(155, 22);
            this.mTankFilter_CountryFrance.Text = "France";
            this.mTankFilter_CountryFrance.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
            this.mTankFilter_CountryFrance.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
            this.mTankFilter_CountryFrance.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_CountryUK
            // 
            this.mTankFilter_CountryUK.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryUK.Image")));
            this.mTankFilter_CountryUK.Name = "mTankFilter_CountryUK";
            this.mTankFilter_CountryUK.Size = new System.Drawing.Size(155, 22);
            this.mTankFilter_CountryUK.Text = "U.K.";
            this.mTankFilter_CountryUK.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
            this.mTankFilter_CountryUK.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
            this.mTankFilter_CountryUK.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_CountryChina
            // 
            this.mTankFilter_CountryChina.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryChina.Image")));
            this.mTankFilter_CountryChina.Name = "mTankFilter_CountryChina";
            this.mTankFilter_CountryChina.Size = new System.Drawing.Size(155, 22);
            this.mTankFilter_CountryChina.Text = "China";
            this.mTankFilter_CountryChina.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
            this.mTankFilter_CountryChina.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
            this.mTankFilter_CountryChina.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_CountryJapan
            // 
            this.mTankFilter_CountryJapan.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryJapan.Image")));
            this.mTankFilter_CountryJapan.Name = "mTankFilter_CountryJapan";
            this.mTankFilter_CountryJapan.Size = new System.Drawing.Size(155, 22);
            this.mTankFilter_CountryJapan.Text = "Japan";
            this.mTankFilter_CountryJapan.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
            this.mTankFilter_CountryJapan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
            this.mTankFilter_CountryJapan.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_CountryCzechoslovakia
            // 
            this.mTankFilter_CountryCzechoslovakia.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryCzechoslovakia.Image")));
            this.mTankFilter_CountryCzechoslovakia.Name = "mTankFilter_CountryCzechoslovakia";
            this.mTankFilter_CountryCzechoslovakia.Size = new System.Drawing.Size(155, 22);
            this.mTankFilter_CountryCzechoslovakia.Text = "Czechoslovakia";
            this.mTankFilter_CountryCzechoslovakia.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
            this.mTankFilter_CountryCzechoslovakia.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
            this.mTankFilter_CountryCzechoslovakia.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Type
            // 
            this.mTankFilter_Type.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mTankFilter_TypeLT,
            this.mTankFilter_TypeMT,
            this.mTankFilter_TypeHT,
            this.mTankFilter_TypeTD,
            this.mTankFilter_TypeSPG});
            this.mTankFilter_Type.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Type.Image")));
            this.mTankFilter_Type.Name = "mTankFilter_Type";
            this.mTankFilter_Type.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_Type.Text = "Tank Type";
            // 
            // mTankFilter_TypeLT
            // 
            this.mTankFilter_TypeLT.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_TypeLT.Image")));
            this.mTankFilter_TypeLT.Name = "mTankFilter_TypeLT";
            this.mTankFilter_TypeLT.Size = new System.Drawing.Size(157, 22);
            this.mTankFilter_TypeLT.Text = "Light Tanks";
            this.mTankFilter_TypeLT.Click += new System.EventHandler(this.toolItemTankFilter_Type_Click);
            this.mTankFilter_TypeLT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Type_MouseDown);
            this.mTankFilter_TypeLT.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_TypeMT
            // 
            this.mTankFilter_TypeMT.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_TypeMT.Image")));
            this.mTankFilter_TypeMT.Name = "mTankFilter_TypeMT";
            this.mTankFilter_TypeMT.Size = new System.Drawing.Size(157, 22);
            this.mTankFilter_TypeMT.Text = "Medium Tanks";
            this.mTankFilter_TypeMT.Click += new System.EventHandler(this.toolItemTankFilter_Type_Click);
            this.mTankFilter_TypeMT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Type_MouseDown);
            this.mTankFilter_TypeMT.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_TypeHT
            // 
            this.mTankFilter_TypeHT.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_TypeHT.Image")));
            this.mTankFilter_TypeHT.Name = "mTankFilter_TypeHT";
            this.mTankFilter_TypeHT.Size = new System.Drawing.Size(157, 22);
            this.mTankFilter_TypeHT.Text = "Heavy Tanks";
            this.mTankFilter_TypeHT.Click += new System.EventHandler(this.toolItemTankFilter_Type_Click);
            this.mTankFilter_TypeHT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Type_MouseDown);
            this.mTankFilter_TypeHT.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_TypeTD
            // 
            this.mTankFilter_TypeTD.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_TypeTD.Image")));
            this.mTankFilter_TypeTD.Name = "mTankFilter_TypeTD";
            this.mTankFilter_TypeTD.Size = new System.Drawing.Size(157, 22);
            this.mTankFilter_TypeTD.Text = "Tank Destroyers";
            this.mTankFilter_TypeTD.Click += new System.EventHandler(this.toolItemTankFilter_Type_Click);
            this.mTankFilter_TypeTD.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Type_MouseDown);
            this.mTankFilter_TypeTD.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_TypeSPG
            // 
            this.mTankFilter_TypeSPG.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_TypeSPG.Image")));
            this.mTankFilter_TypeSPG.Name = "mTankFilter_TypeSPG";
            this.mTankFilter_TypeSPG.Size = new System.Drawing.Size(157, 22);
            this.mTankFilter_TypeSPG.Text = "SPGs";
            this.mTankFilter_TypeSPG.Click += new System.EventHandler(this.toolItemTankFilter_Type_Click);
            this.mTankFilter_TypeSPG.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Type_MouseDown);
            this.mTankFilter_TypeSPG.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Tier
            // 
            this.mTankFilter_Tier.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mTankFilter_Tier1,
            this.mTankFilter_Tier2,
            this.mTankFilter_Tier3,
            this.mTankFilter_Tier4,
            this.mTankFilter_Tier5,
            this.mTankFilter_Tier6,
            this.mTankFilter_Tier7,
            this.mTankFilter_Tier8,
            this.mTankFilter_Tier9,
            this.mTankFilter_Tier10});
            this.mTankFilter_Tier.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier.Image")));
            this.mTankFilter_Tier.Name = "mTankFilter_Tier";
            this.mTankFilter_Tier.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_Tier.Text = "Tier";
            // 
            // mTankFilter_Tier1
            // 
            this.mTankFilter_Tier1.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier1.Image")));
            this.mTankFilter_Tier1.Name = "mTankFilter_Tier1";
            this.mTankFilter_Tier1.Size = new System.Drawing.Size(86, 22);
            this.mTankFilter_Tier1.Text = "1";
            this.mTankFilter_Tier1.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
            this.mTankFilter_Tier1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
            this.mTankFilter_Tier1.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Tier2
            // 
            this.mTankFilter_Tier2.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier2.Image")));
            this.mTankFilter_Tier2.Name = "mTankFilter_Tier2";
            this.mTankFilter_Tier2.Size = new System.Drawing.Size(86, 22);
            this.mTankFilter_Tier2.Text = "2";
            this.mTankFilter_Tier2.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
            this.mTankFilter_Tier2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
            this.mTankFilter_Tier2.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Tier3
            // 
            this.mTankFilter_Tier3.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier3.Image")));
            this.mTankFilter_Tier3.Name = "mTankFilter_Tier3";
            this.mTankFilter_Tier3.Size = new System.Drawing.Size(86, 22);
            this.mTankFilter_Tier3.Text = "3";
            this.mTankFilter_Tier3.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
            this.mTankFilter_Tier3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
            this.mTankFilter_Tier3.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Tier4
            // 
            this.mTankFilter_Tier4.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier4.Image")));
            this.mTankFilter_Tier4.Name = "mTankFilter_Tier4";
            this.mTankFilter_Tier4.Size = new System.Drawing.Size(86, 22);
            this.mTankFilter_Tier4.Text = "4";
            this.mTankFilter_Tier4.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
            this.mTankFilter_Tier4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
            this.mTankFilter_Tier4.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Tier5
            // 
            this.mTankFilter_Tier5.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier5.Image")));
            this.mTankFilter_Tier5.Name = "mTankFilter_Tier5";
            this.mTankFilter_Tier5.Size = new System.Drawing.Size(86, 22);
            this.mTankFilter_Tier5.Text = "5";
            this.mTankFilter_Tier5.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
            this.mTankFilter_Tier5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
            this.mTankFilter_Tier5.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Tier6
            // 
            this.mTankFilter_Tier6.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier6.Image")));
            this.mTankFilter_Tier6.Name = "mTankFilter_Tier6";
            this.mTankFilter_Tier6.Size = new System.Drawing.Size(86, 22);
            this.mTankFilter_Tier6.Text = "6";
            this.mTankFilter_Tier6.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
            this.mTankFilter_Tier6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
            this.mTankFilter_Tier6.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Tier7
            // 
            this.mTankFilter_Tier7.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier7.Image")));
            this.mTankFilter_Tier7.Name = "mTankFilter_Tier7";
            this.mTankFilter_Tier7.Size = new System.Drawing.Size(86, 22);
            this.mTankFilter_Tier7.Text = "7";
            this.mTankFilter_Tier7.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
            this.mTankFilter_Tier7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
            this.mTankFilter_Tier7.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Tier8
            // 
            this.mTankFilter_Tier8.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier8.Image")));
            this.mTankFilter_Tier8.Name = "mTankFilter_Tier8";
            this.mTankFilter_Tier8.Size = new System.Drawing.Size(86, 22);
            this.mTankFilter_Tier8.Text = "8";
            this.mTankFilter_Tier8.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
            this.mTankFilter_Tier8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
            this.mTankFilter_Tier8.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Tier9
            // 
            this.mTankFilter_Tier9.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier9.Image")));
            this.mTankFilter_Tier9.Name = "mTankFilter_Tier9";
            this.mTankFilter_Tier9.Size = new System.Drawing.Size(86, 22);
            this.mTankFilter_Tier9.Text = "9";
            this.mTankFilter_Tier9.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
            this.mTankFilter_Tier9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
            this.mTankFilter_Tier9.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Tier10
            // 
            this.mTankFilter_Tier10.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier10.Image")));
            this.mTankFilter_Tier10.Name = "mTankFilter_Tier10";
            this.mTankFilter_Tier10.Size = new System.Drawing.Size(86, 22);
            this.mTankFilter_Tier10.Text = "10";
            this.mTankFilter_Tier10.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
            this.mTankFilter_Tier10.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
            this.mTankFilter_Tier10.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Search
            // 
            this.mTankFilter_Search.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Search.Image")));
            this.mTankFilter_Search.Name = "mTankFilter_Search";
            this.mTankFilter_Search.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_Search.Text = "Search for Tank...";
            this.mTankFilter_Search.Click += new System.EventHandler(this.mTankFilter_Search_Click);
            // 
            // mTankFilter_Clear
            // 
            this.mTankFilter_Clear.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Clear.Image")));
            this.mTankFilter_Clear.Name = "mTankFilter_Clear";
            this.mTankFilter_Clear.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_Clear.Text = "Clear Tank Filter";
            this.mTankFilter_Clear.Click += new System.EventHandler(this.mTankFilter_Clear_Click);
            // 
            // mTankFilter_FavSeparator
            // 
            this.mTankFilter_FavSeparator.Name = "mTankFilter_FavSeparator";
            this.mTankFilter_FavSeparator.Size = new System.Drawing.Size(230, 6);
            // 
            // mTankFilter_All
            // 
            this.mTankFilter_All.BackColor = System.Drawing.Color.Transparent;
            this.mTankFilter_All.Checked = true;
            this.mTankFilter_All.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mTankFilter_All.Name = "mTankFilter_All";
            this.mTankFilter_All.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_All.Text = "My Tanks";
            this.mTankFilter_All.Click += new System.EventHandler(this.toolItemTankFilter_All_Click);
            this.mTankFilter_All.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_All_NotOwned
            // 
            this.mTankFilter_All_NotOwned.Name = "mTankFilter_All_NotOwned";
            this.mTankFilter_All_NotOwned.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_All_NotOwned.Text = "All Tanks";
            this.mTankFilter_All_NotOwned.Click += new System.EventHandler(this.mTankFilter_All_NotOwned_Click);
            this.mTankFilter_All_NotOwned.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Fav01
            // 
            this.mTankFilter_Fav01.Name = "mTankFilter_Fav01";
            this.mTankFilter_Fav01.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_Fav01.Text = "Favourite item #1";
            this.mTankFilter_Fav01.Visible = false;
            this.mTankFilter_Fav01.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
            this.mTankFilter_Fav01.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Fav02
            // 
            this.mTankFilter_Fav02.Name = "mTankFilter_Fav02";
            this.mTankFilter_Fav02.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_Fav02.Text = "Favourite item #2";
            this.mTankFilter_Fav02.Visible = false;
            this.mTankFilter_Fav02.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
            this.mTankFilter_Fav02.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Fav03
            // 
            this.mTankFilter_Fav03.Name = "mTankFilter_Fav03";
            this.mTankFilter_Fav03.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_Fav03.Text = "Favourite item #3";
            this.mTankFilter_Fav03.Visible = false;
            this.mTankFilter_Fav03.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
            this.mTankFilter_Fav03.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Fav04
            // 
            this.mTankFilter_Fav04.Name = "mTankFilter_Fav04";
            this.mTankFilter_Fav04.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_Fav04.Text = "Favourite item #4";
            this.mTankFilter_Fav04.Visible = false;
            this.mTankFilter_Fav04.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
            this.mTankFilter_Fav04.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Fav05
            // 
            this.mTankFilter_Fav05.Name = "mTankFilter_Fav05";
            this.mTankFilter_Fav05.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_Fav05.Text = "Favourite item #5";
            this.mTankFilter_Fav05.Visible = false;
            this.mTankFilter_Fav05.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
            this.mTankFilter_Fav05.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Fav06
            // 
            this.mTankFilter_Fav06.Name = "mTankFilter_Fav06";
            this.mTankFilter_Fav06.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_Fav06.Text = "Favourite item #6";
            this.mTankFilter_Fav06.Visible = false;
            this.mTankFilter_Fav06.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
            this.mTankFilter_Fav06.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Fav07
            // 
            this.mTankFilter_Fav07.Name = "mTankFilter_Fav07";
            this.mTankFilter_Fav07.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_Fav07.Text = "Favourite item #7";
            this.mTankFilter_Fav07.Visible = false;
            this.mTankFilter_Fav07.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
            this.mTankFilter_Fav07.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Fav08
            // 
            this.mTankFilter_Fav08.Name = "mTankFilter_Fav08";
            this.mTankFilter_Fav08.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_Fav08.Text = "Favourite item #8";
            this.mTankFilter_Fav08.Visible = false;
            this.mTankFilter_Fav08.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
            this.mTankFilter_Fav08.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Fav09
            // 
            this.mTankFilter_Fav09.Name = "mTankFilter_Fav09";
            this.mTankFilter_Fav09.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_Fav09.Text = "Favourite item #9";
            this.mTankFilter_Fav09.Visible = false;
            this.mTankFilter_Fav09.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
            this.mTankFilter_Fav09.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mTankFilter_Fav10
            // 
            this.mTankFilter_Fav10.Name = "mTankFilter_Fav10";
            this.mTankFilter_Fav10.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_Fav10.Text = "Favourite item #10";
            this.mTankFilter_Fav10.Visible = false;
            this.mTankFilter_Fav10.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
            this.mTankFilter_Fav10.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(230, 6);
            // 
            // mTankFilter_GetInGarage
            // 
            this.mTankFilter_GetInGarage.Name = "mTankFilter_GetInGarage";
            this.mTankFilter_GetInGarage.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_GetInGarage.Text = "Update \"In Garage\" Tank List...";
            this.mTankFilter_GetInGarage.Click += new System.EventHandler(this.mTankFilter_GetInGarage_Click);
            // 
            // mTankFilter_EditFavList
            // 
            this.mTankFilter_EditFavList.Name = "mTankFilter_EditFavList";
            this.mTankFilter_EditFavList.Size = new System.Drawing.Size(233, 22);
            this.mTankFilter_EditFavList.Text = "Edit Favourite Tank List...";
            this.mTankFilter_EditFavList.Click += new System.EventHandler(this.toolItemTankFilter_EditFavList_Click);
            // 
            // mMode
            // 
            this.mMode.BackColor = System.Drawing.Color.Transparent;
            this.mMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mModeAll,
            this.toolStripSeparator17,
            this.mModeRandomTankCompany,
            this.mModeRandom,
            this.mModeRandomSoloPlatoon,
            this.mModeCompany,
            this.mModeClan,
            this.toolStripSeparatorForBattleView,
            this.mModeHistorical,
            this.mModeTeam,
            this.mModeTeamRanked,
            this.toolStripSeparator13,
            this.mModeGlobalMap,
            this.mModeSkrimishes,
            this.mModeBattleForStronghold,
            this.toolStripSeparator15,
            this.mModeSpecial});
            this.mMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mMode.Image = ((System.Drawing.Image)(resources.GetObject("mMode.Image")));
            this.mMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mMode.Name = "mMode";
            this.mMode.ShowDropDownArrow = false;
            this.mMode.Size = new System.Drawing.Size(80, 22);
            this.mMode.Text = "All Modes";
            this.mMode.ToolTipText = "Select Battle Mode";
            this.mMode.Visible = false;
            // 
            // mModeAll
            // 
            this.mModeAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mModeAll.Name = "mModeAll";
            this.mModeAll.Size = new System.Drawing.Size(205, 22);
            this.mModeAll.Tag = "All";
            this.mModeAll.Text = "All Modes";
            this.mModeAll.Click += new System.EventHandler(this.toolItemMode_Click);
            this.mModeAll.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(202, 6);
            // 
            // mModeRandomTankCompany
            // 
            this.mModeRandomTankCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mModeRandomTankCompany.Name = "mModeRandomTankCompany";
            this.mModeRandomTankCompany.Size = new System.Drawing.Size(205, 22);
            this.mModeRandomTankCompany.Tag = "RandomAndTankCompany";
            this.mModeRandomTankCompany.Text = "Random, Tank Company";
            this.mModeRandomTankCompany.Click += new System.EventHandler(this.toolItemMode_Click);
            this.mModeRandomTankCompany.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mModeRandom
            // 
            this.mModeRandom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mModeRandom.Name = "mModeRandom";
            this.mModeRandom.Size = new System.Drawing.Size(205, 22);
            this.mModeRandom.Tag = "Random";
            this.mModeRandom.Text = "Random";
            this.mModeRandom.Visible = false;
            this.mModeRandom.Click += new System.EventHandler(this.toolItemMode_Click);
            this.mModeRandom.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mModeRandomSoloPlatoon
            // 
            this.mModeRandomSoloPlatoon.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mModeRandomSolo,
            this.mRandomPlatoon,
            this.mRandomPlatoon2,
            this.mRandomPlatoon3});
            this.mModeRandomSoloPlatoon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mModeRandomSoloPlatoon.Name = "mModeRandomSoloPlatoon";
            this.mModeRandomSoloPlatoon.Size = new System.Drawing.Size(205, 22);
            this.mModeRandomSoloPlatoon.Text = "Random - Solo / Platoon";
            this.mModeRandomSoloPlatoon.Visible = false;
            // 
            // mModeRandomSolo
            // 
            this.mModeRandomSolo.Name = "mModeRandomSolo";
            this.mModeRandomSolo.Size = new System.Drawing.Size(201, 22);
            this.mModeRandomSolo.Tag = "RandomSolo";
            this.mModeRandomSolo.Text = "Random - Solo";
            this.mModeRandomSolo.Click += new System.EventHandler(this.toolItemMode_Click);
            this.mModeRandomSolo.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mRandomPlatoon
            // 
            this.mRandomPlatoon.Name = "mRandomPlatoon";
            this.mRandomPlatoon.Size = new System.Drawing.Size(201, 22);
            this.mRandomPlatoon.Tag = "RandomPlatoon";
            this.mRandomPlatoon.Text = "Random - In Platoon";
            this.mRandomPlatoon.Click += new System.EventHandler(this.toolItemMode_Click);
            this.mRandomPlatoon.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mRandomPlatoon2
            // 
            this.mRandomPlatoon2.Name = "mRandomPlatoon2";
            this.mRandomPlatoon2.Size = new System.Drawing.Size(201, 22);
            this.mRandomPlatoon2.Tag = "RandomPlatoon2";
            this.mRandomPlatoon2.Text = "Random - In Platoon (2)";
            this.mRandomPlatoon2.Click += new System.EventHandler(this.toolItemMode_Click);
            this.mRandomPlatoon2.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mRandomPlatoon3
            // 
            this.mRandomPlatoon3.Name = "mRandomPlatoon3";
            this.mRandomPlatoon3.Size = new System.Drawing.Size(201, 22);
            this.mRandomPlatoon3.Tag = "RandomPlatoon3";
            this.mRandomPlatoon3.Text = "Random - In Platoon (3)";
            this.mRandomPlatoon3.Click += new System.EventHandler(this.toolItemMode_Click);
            this.mRandomPlatoon3.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mModeCompany
            // 
            this.mModeCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mModeCompany.Name = "mModeCompany";
            this.mModeCompany.Size = new System.Drawing.Size(205, 22);
            this.mModeCompany.Tag = "TankCompany";
            this.mModeCompany.Text = "Tank Company";
            this.mModeCompany.Visible = false;
            this.mModeCompany.Click += new System.EventHandler(this.toolItemMode_Click);
            this.mModeCompany.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mModeClan
            // 
            this.mModeClan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mModeClan.Name = "mModeClan";
            this.mModeClan.Size = new System.Drawing.Size(205, 22);
            this.mModeClan.Tag = "ClanWar";
            this.mModeClan.Text = "Clan War";
            this.mModeClan.Visible = false;
            this.mModeClan.Click += new System.EventHandler(this.toolItemMode_Click);
            this.mModeClan.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // toolStripSeparatorForBattleView
            // 
            this.toolStripSeparatorForBattleView.Name = "toolStripSeparatorForBattleView";
            this.toolStripSeparatorForBattleView.Size = new System.Drawing.Size(202, 6);
            // 
            // mModeHistorical
            // 
            this.mModeHistorical.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mModeHistorical.Name = "mModeHistorical";
            this.mModeHistorical.Size = new System.Drawing.Size(205, 22);
            this.mModeHistorical.Tag = "Historical";
            this.mModeHistorical.Text = "Historical Battle";
            this.mModeHistorical.Click += new System.EventHandler(this.toolItemMode_Click);
            this.mModeHistorical.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mModeTeam
            // 
            this.mModeTeam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mModeTeam.Name = "mModeTeam";
            this.mModeTeam.Size = new System.Drawing.Size(205, 22);
            this.mModeTeam.Tag = "Team";
            this.mModeTeam.Text = "Team: Unranked";
            this.mModeTeam.Click += new System.EventHandler(this.toolItemMode_Click);
            this.mModeTeam.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mModeTeamRanked
            // 
            this.mModeTeamRanked.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mModeTeamRanked.Name = "mModeTeamRanked";
            this.mModeTeamRanked.Size = new System.Drawing.Size(205, 22);
            this.mModeTeamRanked.Tag = "TeamRanked";
            this.mModeTeamRanked.Text = "Team: Ranked";
            this.mModeTeamRanked.Click += new System.EventHandler(this.toolItemMode_Click);
            this.mModeTeamRanked.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(202, 6);
            // 
            // mModeGlobalMap
            // 
            this.mModeGlobalMap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mModeGlobalMap.Name = "mModeGlobalMap";
            this.mModeGlobalMap.Size = new System.Drawing.Size(205, 22);
            this.mModeGlobalMap.Tag = "GlobalMap";
            this.mModeGlobalMap.Text = "Global Map";
            this.mModeGlobalMap.Click += new System.EventHandler(this.toolItemMode_Click);
            this.mModeGlobalMap.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mModeSkrimishes
            // 
            this.mModeSkrimishes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mModeSkrimishes.Name = "mModeSkrimishes";
            this.mModeSkrimishes.Size = new System.Drawing.Size(205, 22);
            this.mModeSkrimishes.Tag = "Skirmishes";
            this.mModeSkrimishes.Text = "Skirmishes";
            this.mModeSkrimishes.Click += new System.EventHandler(this.toolItemMode_Click);
            this.mModeSkrimishes.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mModeBattleForStronghold
            // 
            this.mModeBattleForStronghold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mModeBattleForStronghold.Name = "mModeBattleForStronghold";
            this.mModeBattleForStronghold.Size = new System.Drawing.Size(205, 22);
            this.mModeBattleForStronghold.Tag = "Stronghold";
            this.mModeBattleForStronghold.Text = "Battle for Stronghold";
            this.mModeBattleForStronghold.Click += new System.EventHandler(this.toolItemMode_Click);
            this.mModeBattleForStronghold.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(202, 6);
            // 
            // mModeSpecial
            // 
            this.mModeSpecial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mModeSpecial.Name = "mModeSpecial";
            this.mModeSpecial.Size = new System.Drawing.Size(205, 22);
            this.mModeSpecial.Tag = "Special";
            this.mModeSpecial.Text = "Special Events";
            this.mModeSpecial.Click += new System.EventHandler(this.toolItemMode_Click);
            this.mModeSpecial.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mBattles
            // 
            this.mBattles.BackColor = System.Drawing.Color.Transparent;
            this.mBattles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mBattles1d,
            this.mBattlesYesterday,
            this.toolStripSeparator3,
            this.mBattles2d,
            this.mBattles3d,
            this.toolStripSeparator19,
            this.mBattles1w,
            this.mBattles2w,
            this.toolStripSeparator20,
            this.mBattles1m,
            this.mBattles3m,
            this.mBattles6m,
            this.toolStripSeparator21,
            this.mBattles1y,
            this.mBattles2y,
            this.toolStripSeparator22,
            this.mBattlesCustomUse,
            this.mBattlesCustomChange,
            this.toolStripSeparator10,
            this.mBattlesAll});
            this.mBattles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattles.Image = ((System.Drawing.Image)(resources.GetObject("mBattles.Image")));
            this.mBattles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mBattles.Name = "mBattles";
            this.mBattles.ShowDropDownArrow = false;
            this.mBattles.Size = new System.Drawing.Size(105, 22);
            this.mBattles.Text = "Today\'s Battles";
            this.mBattles.ToolTipText = "Select Battle Time";
            this.mBattles.Visible = false;
            this.mBattles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mBattles_MouseDown);
            // 
            // mBattles1d
            // 
            this.mBattles1d.Checked = true;
            this.mBattles1d.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mBattles1d.Name = "mBattles1d";
            this.mBattles1d.Size = new System.Drawing.Size(198, 22);
            this.mBattles1d.Text = "Today\'s Battles";
            this.mBattles1d.Click += new System.EventHandler(this.mBattleTime_Click);
            this.mBattles1d.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mBattlesYesterday
            // 
            this.mBattlesYesterday.Name = "mBattlesYesterday";
            this.mBattlesYesterday.Size = new System.Drawing.Size(198, 22);
            this.mBattlesYesterday.Text = "Yesterday\'s Battles";
            this.mBattlesYesterday.Click += new System.EventHandler(this.mBattleTime_Click);
            this.mBattlesYesterday.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(195, 6);
            // 
            // mBattles2d
            // 
            this.mBattles2d.Name = "mBattles2d";
            this.mBattles2d.Size = new System.Drawing.Size(198, 22);
            this.mBattles2d.Text = "Battles Last 2 Days";
            this.mBattles2d.ToolTipText = "Battles today and yesterday";
            this.mBattles2d.Click += new System.EventHandler(this.mBattleTime_Click);
            this.mBattles2d.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mBattles3d
            // 
            this.mBattles3d.Name = "mBattles3d";
            this.mBattles3d.Size = new System.Drawing.Size(198, 22);
            this.mBattles3d.Text = "Battles Last 3 Days";
            this.mBattles3d.ToolTipText = "Battles today and the two previous days";
            this.mBattles3d.Click += new System.EventHandler(this.mBattleTime_Click);
            this.mBattles3d.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(195, 6);
            // 
            // mBattles1w
            // 
            this.mBattles1w.Name = "mBattles1w";
            this.mBattles1w.Size = new System.Drawing.Size(198, 22);
            this.mBattles1w.Text = "Battles Last Week";
            this.mBattles1w.Click += new System.EventHandler(this.mBattleTime_Click);
            this.mBattles1w.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mBattles2w
            // 
            this.mBattles2w.Name = "mBattles2w";
            this.mBattles2w.Size = new System.Drawing.Size(198, 22);
            this.mBattles2w.Text = "Battles Last 2 Weeks";
            this.mBattles2w.Click += new System.EventHandler(this.mBattleTime_Click);
            this.mBattles2w.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(195, 6);
            // 
            // mBattles1m
            // 
            this.mBattles1m.Name = "mBattles1m";
            this.mBattles1m.Size = new System.Drawing.Size(198, 22);
            this.mBattles1m.Text = "Battles Last Month";
            this.mBattles1m.Click += new System.EventHandler(this.mBattleTime_Click);
            this.mBattles1m.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mBattles3m
            // 
            this.mBattles3m.Name = "mBattles3m";
            this.mBattles3m.Size = new System.Drawing.Size(198, 22);
            this.mBattles3m.Text = "Battles Last 3 Months";
            this.mBattles3m.Click += new System.EventHandler(this.mBattleTime_Click);
            this.mBattles3m.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mBattles6m
            // 
            this.mBattles6m.Name = "mBattles6m";
            this.mBattles6m.Size = new System.Drawing.Size(198, 22);
            this.mBattles6m.Text = "Battles Last 6 Months";
            this.mBattles6m.Click += new System.EventHandler(this.mBattleTime_Click);
            this.mBattles6m.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(195, 6);
            // 
            // mBattles1y
            // 
            this.mBattles1y.Name = "mBattles1y";
            this.mBattles1y.Size = new System.Drawing.Size(198, 22);
            this.mBattles1y.Text = "Battles Last Year";
            this.mBattles1y.Click += new System.EventHandler(this.mBattleTime_Click);
            this.mBattles1y.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mBattles2y
            // 
            this.mBattles2y.Name = "mBattles2y";
            this.mBattles2y.Size = new System.Drawing.Size(198, 22);
            this.mBattles2y.Text = "Battles Last 2 Years";
            this.mBattles2y.Click += new System.EventHandler(this.mBattleTime_Click);
            this.mBattles2y.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(195, 6);
            // 
            // mBattlesCustomUse
            // 
            this.mBattlesCustomUse.Name = "mBattlesCustomUse";
            this.mBattlesCustomUse.Size = new System.Drawing.Size(198, 22);
            this.mBattlesCustomUse.Tag = "Custom Filter";
            this.mBattlesCustomUse.Text = "Use Custom Filter";
            this.mBattlesCustomUse.Click += new System.EventHandler(this.mBattleTime_Click);
            this.mBattlesCustomUse.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mBattlesCustomChange
            // 
            this.mBattlesCustomChange.Name = "mBattlesCustomChange";
            this.mBattlesCustomChange.Size = new System.Drawing.Size(198, 22);
            this.mBattlesCustomChange.Text = "Change Custom Filter...";
            this.mBattlesCustomChange.Click += new System.EventHandler(this.mBattleTimeCustomChange_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(195, 6);
            // 
            // mBattlesAll
            // 
            this.mBattlesAll.Name = "mBattlesAll";
            this.mBattlesAll.Size = new System.Drawing.Size(198, 22);
            this.mBattlesAll.Text = "All Battles";
            this.mBattlesAll.Click += new System.EventHandler(this.mBattleTime_Click);
            // 
            // mBattleGroup
            // 
            this.mBattleGroup.BackColor = System.Drawing.Color.Transparent;
            this.mBattleGroup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mBattleGroup_No,
            this.toolStripSeparator6,
            this.mBattleGroup_TankAverage,
            this.mBattleGroup_TankSum});
            this.mBattleGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleGroup.Image = ((System.Drawing.Image)(resources.GetObject("mBattleGroup.Image")));
            this.mBattleGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mBattleGroup.Name = "mBattleGroup";
            this.mBattleGroup.ShowDropDownArrow = false;
            this.mBattleGroup.Size = new System.Drawing.Size(96, 22);
            this.mBattleGroup.Text = "No Grouping";
            this.mBattleGroup.ToolTipText = "Select Battle Grouping";
            this.mBattleGroup.Visible = false;
            // 
            // mBattleGroup_No
            // 
            this.mBattleGroup_No.Checked = true;
            this.mBattleGroup_No.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mBattleGroup_No.Name = "mBattleGroup_No";
            this.mBattleGroup_No.Size = new System.Drawing.Size(205, 22);
            this.mBattleGroup_No.Text = "No Grouping";
            this.mBattleGroup_No.Click += new System.EventHandler(this.toolItemGroupingSelected_Click);
            this.mBattleGroup_No.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(202, 6);
            // 
            // mBattleGroup_TankAverage
            // 
            this.mBattleGroup_TankAverage.Name = "mBattleGroup_TankAverage";
            this.mBattleGroup_TankAverage.Size = new System.Drawing.Size(205, 22);
            this.mBattleGroup_TankAverage.Text = "Group by Tank - Average";
            this.mBattleGroup_TankAverage.Click += new System.EventHandler(this.toolItemGroupingSelected_Click);
            this.mBattleGroup_TankAverage.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mBattleGroup_TankSum
            // 
            this.mBattleGroup_TankSum.Name = "mBattleGroup_TankSum";
            this.mBattleGroup_TankSum.Size = new System.Drawing.Size(205, 22);
            this.mBattleGroup_TankSum.Text = "Group by Tank - Sum";
            this.mBattleGroup_TankSum.Click += new System.EventHandler(this.toolItemGroupingSelected_Click);
            this.mBattleGroup_TankSum.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mHomeView
            // 
            this.mHomeView.BackColor = System.Drawing.Color.Transparent;
            this.mHomeView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator31,
            this.mHomeViewDefault,
            this.mHomeViewClassic,
            this.mHomeViewRecentSeparator,
            this.mHomeViewRecent1,
            this.mHomeViewRecent2,
            this.mHomeViewRecent3,
            this.mHomeViewRecent4,
            this.mHomeViewRecent5,
            this.toolStripSeparator28,
            this.mHomeViewFileLoad,
            this.toolStripSeparator8,
            this.mHomeViewRedraw,
            this.mHomeViewClearRecentList});
            this.mHomeView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mHomeView.Image = ((System.Drawing.Image)(resources.GetObject("mHomeView.Image")));
            this.mHomeView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mHomeView.Name = "mHomeView";
            this.mHomeView.ShowDropDownArrow = false;
            this.mHomeView.Size = new System.Drawing.Size(65, 22);
            this.mHomeView.Text = "Default";
            this.mHomeView.ToolTipText = "Select Home VIew";
            // 
            // toolStripSeparator31
            // 
            this.toolStripSeparator31.Name = "toolStripSeparator31";
            this.toolStripSeparator31.Size = new System.Drawing.Size(174, 6);
            // 
            // mHomeViewDefault
            // 
            this.mHomeViewDefault.Name = "mHomeViewDefault";
            this.mHomeViewDefault.Size = new System.Drawing.Size(177, 22);
            this.mHomeViewDefault.Tag = "New_Default_Setup.json";
            this.mHomeViewDefault.Text = "Default";
            this.mHomeViewDefault.Click += new System.EventHandler(this.mHomeViewDefaults_Click);
            this.mHomeViewDefault.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mHomeViewClassic
            // 
            this.mHomeViewClassic.Name = "mHomeViewClassic";
            this.mHomeViewClassic.Size = new System.Drawing.Size(177, 22);
            this.mHomeViewClassic.Tag = "Old_Default_Setup.json";
            this.mHomeViewClassic.Text = "Classic";
            this.mHomeViewClassic.Click += new System.EventHandler(this.mHomeViewDefaults_Click);
            this.mHomeViewClassic.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mHomeViewRecentSeparator
            // 
            this.mHomeViewRecentSeparator.Name = "mHomeViewRecentSeparator";
            this.mHomeViewRecentSeparator.Size = new System.Drawing.Size(174, 6);
            // 
            // mHomeViewRecent1
            // 
            this.mHomeViewRecent1.Name = "mHomeViewRecent1";
            this.mHomeViewRecent1.Size = new System.Drawing.Size(177, 22);
            this.mHomeViewRecent1.Text = "Recent1";
            this.mHomeViewRecent1.Click += new System.EventHandler(this.mHomeViewRecent_Click);
            this.mHomeViewRecent1.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mHomeViewRecent2
            // 
            this.mHomeViewRecent2.Name = "mHomeViewRecent2";
            this.mHomeViewRecent2.Size = new System.Drawing.Size(177, 22);
            this.mHomeViewRecent2.Text = "Recent2";
            this.mHomeViewRecent2.Click += new System.EventHandler(this.mHomeViewRecent_Click);
            this.mHomeViewRecent2.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mHomeViewRecent3
            // 
            this.mHomeViewRecent3.Name = "mHomeViewRecent3";
            this.mHomeViewRecent3.Size = new System.Drawing.Size(177, 22);
            this.mHomeViewRecent3.Text = "Recent3";
            this.mHomeViewRecent3.Click += new System.EventHandler(this.mHomeViewRecent_Click);
            this.mHomeViewRecent3.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mHomeViewRecent4
            // 
            this.mHomeViewRecent4.Name = "mHomeViewRecent4";
            this.mHomeViewRecent4.Size = new System.Drawing.Size(177, 22);
            this.mHomeViewRecent4.Text = "Recent4";
            this.mHomeViewRecent4.Click += new System.EventHandler(this.mHomeViewRecent_Click);
            this.mHomeViewRecent4.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mHomeViewRecent5
            // 
            this.mHomeViewRecent5.Name = "mHomeViewRecent5";
            this.mHomeViewRecent5.Size = new System.Drawing.Size(177, 22);
            this.mHomeViewRecent5.Text = "Recent5";
            this.mHomeViewRecent5.Click += new System.EventHandler(this.mHomeViewRecent_Click);
            this.mHomeViewRecent5.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new System.Drawing.Size(174, 6);
            // 
            // mHomeViewFileLoad
            // 
            this.mHomeViewFileLoad.Name = "mHomeViewFileLoad";
            this.mHomeViewFileLoad.Size = new System.Drawing.Size(177, 22);
            this.mHomeViewFileLoad.Text = "Load from File...";
            this.mHomeViewFileLoad.Click += new System.EventHandler(this.mHomeViewFileLoad_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(174, 6);
            // 
            // mHomeViewRedraw
            // 
            this.mHomeViewRedraw.Name = "mHomeViewRedraw";
            this.mHomeViewRedraw.Size = new System.Drawing.Size(177, 22);
            this.mHomeViewRedraw.Text = "Refresh Home View";
            this.mHomeViewRedraw.Click += new System.EventHandler(this.mHomeViewRefresh_Click);
            // 
            // mHomeViewClearRecentList
            // 
            this.mHomeViewClearRecentList.Name = "mHomeViewClearRecentList";
            this.mHomeViewClearRecentList.Size = new System.Drawing.Size(177, 22);
            this.mHomeViewClearRecentList.Text = "Clear Recent List";
            this.mHomeViewClearRecentList.Click += new System.EventHandler(this.mHomeViewClearRecentList_Click);
            // 
            // mHomeViewEdit
            // 
            this.mHomeViewEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mHomeViewEditMode,
            this.toolStripSeparator32,
            this.mGadgetGauges,
            this.mGadgetCharts,
            this.mGadgetGrids,
            this.mGadgetAddHeader,
            this.mGadgetAddImage,
            this.toolStripSeparator33,
            this.mHomeViewFileSave,
            this.mHomeViewFileShowFolder,
            this.toolStripSeparator4,
            this.mGadgetRemoveAll});
            this.mHomeViewEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mHomeViewEdit.Image = ((System.Drawing.Image)(resources.GetObject("mHomeViewEdit.Image")));
            this.mHomeViewEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mHomeViewEdit.Name = "mHomeViewEdit";
            this.mHomeViewEdit.ShowDropDownArrow = false;
            this.mHomeViewEdit.Size = new System.Drawing.Size(70, 22);
            this.mHomeViewEdit.Text = "Gadgets";
            this.mHomeViewEdit.ToolTipText = "Edit Home View";
            // 
            // mHomeViewEditMode
            // 
            this.mHomeViewEditMode.Name = "mHomeViewEditMode";
            this.mHomeViewEditMode.Size = new System.Drawing.Size(180, 22);
            this.mHomeViewEditMode.Text = "Edit Mode";
            this.mHomeViewEditMode.Click += new System.EventHandler(this.mHomeEdit_Click);
            this.mHomeViewEditMode.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // toolStripSeparator32
            // 
            this.toolStripSeparator32.Name = "toolStripSeparator32";
            this.toolStripSeparator32.Size = new System.Drawing.Size(177, 6);
            // 
            // mGadgetGauges
            // 
            this.mGadgetGauges.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mGadgetWR,
            this.mGadgetRWR,
            this.toolStripSeparator27,
            this.mGadgetWN9,
            this.mGadgetWN8,
            this.mGadgetWN7,
            this.mGadgetEFF,
            this.toolStripSeparator9,
            this.mGadgetKillDeath,
            this.mGadgetDamageCausedReceived});
            this.mGadgetGauges.Name = "mGadgetGauges";
            this.mGadgetGauges.Size = new System.Drawing.Size(180, 22);
            this.mGadgetGauges.Text = "Gauges";
            // 
            // mGadgetWR
            // 
            this.mGadgetWR.Name = "mGadgetWR";
            this.mGadgetWR.Size = new System.Drawing.Size(218, 22);
            this.mGadgetWR.Tag = "ucGaugeWinRate";
            this.mGadgetWR.Text = "Win Rate";
            this.mGadgetWR.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // mGadgetRWR
            // 
            this.mGadgetRWR.Name = "mGadgetRWR";
            this.mGadgetRWR.Size = new System.Drawing.Size(218, 22);
            this.mGadgetRWR.Tag = "ucGaugeRWR";
            this.mGadgetRWR.Text = "RWR";
            this.mGadgetRWR.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // toolStripSeparator27
            // 
            this.toolStripSeparator27.Name = "toolStripSeparator27";
            this.toolStripSeparator27.Size = new System.Drawing.Size(215, 6);
            // 
            // mGadgetWN9
            // 
            this.mGadgetWN9.Name = "mGadgetWN9";
            this.mGadgetWN9.Size = new System.Drawing.Size(218, 22);
            this.mGadgetWN9.Tag = "ucGaugeWN9";
            this.mGadgetWN9.Text = "WN9";
            this.mGadgetWN9.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // mGadgetWN8
            // 
            this.mGadgetWN8.Name = "mGadgetWN8";
            this.mGadgetWN8.Size = new System.Drawing.Size(218, 22);
            this.mGadgetWN8.Tag = "ucGaugeWN8";
            this.mGadgetWN8.Text = "WN8";
            this.mGadgetWN8.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // mGadgetWN7
            // 
            this.mGadgetWN7.Name = "mGadgetWN7";
            this.mGadgetWN7.Size = new System.Drawing.Size(218, 22);
            this.mGadgetWN7.Tag = "ucGaugeWN7";
            this.mGadgetWN7.Text = "WN7";
            this.mGadgetWN7.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // mGadgetEFF
            // 
            this.mGadgetEFF.Name = "mGadgetEFF";
            this.mGadgetEFF.Size = new System.Drawing.Size(218, 22);
            this.mGadgetEFF.Tag = "ucGaugeEFF";
            this.mGadgetEFF.Text = "Efficiency";
            this.mGadgetEFF.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(215, 6);
            // 
            // mGadgetKillDeath
            // 
            this.mGadgetKillDeath.Name = "mGadgetKillDeath";
            this.mGadgetKillDeath.Size = new System.Drawing.Size(218, 22);
            this.mGadgetKillDeath.Tag = "ucGaugeKillDeath";
            this.mGadgetKillDeath.Text = "Kill / Death Ratio";
            this.mGadgetKillDeath.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // mGadgetDamageCausedReceived
            // 
            this.mGadgetDamageCausedReceived.Name = "mGadgetDamageCausedReceived";
            this.mGadgetDamageCausedReceived.Size = new System.Drawing.Size(218, 22);
            this.mGadgetDamageCausedReceived.Tag = "ucGaugeDmgCausedReceived";
            this.mGadgetDamageCausedReceived.Text = "Damage Caused / Received";
            this.mGadgetDamageCausedReceived.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // mGadgetCharts
            // 
            this.mGadgetCharts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mGadgetChartTier,
            this.mGadgetChartTankType,
            this.mGadgetChartNation});
            this.mGadgetCharts.Name = "mGadgetCharts";
            this.mGadgetCharts.Size = new System.Drawing.Size(180, 22);
            this.mGadgetCharts.Text = "Charts";
            // 
            // mGadgetChartTier
            // 
            this.mGadgetChartTier.Name = "mGadgetChartTier";
            this.mGadgetChartTier.Size = new System.Drawing.Size(216, 22);
            this.mGadgetChartTier.Tag = "ucChartTier";
            this.mGadgetChartTier.Text = "Battle Count per Tier";
            this.mGadgetChartTier.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // mGadgetChartTankType
            // 
            this.mGadgetChartTankType.Name = "mGadgetChartTankType";
            this.mGadgetChartTankType.Size = new System.Drawing.Size(216, 22);
            this.mGadgetChartTankType.Tag = "ucChartTankType";
            this.mGadgetChartTankType.Text = "Battle Count per Tank Type";
            this.mGadgetChartTankType.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // mGadgetChartNation
            // 
            this.mGadgetChartNation.Name = "mGadgetChartNation";
            this.mGadgetChartNation.Size = new System.Drawing.Size(216, 22);
            this.mGadgetChartNation.Tag = "ucChartNation";
            this.mGadgetChartNation.Text = "Battle Count per Nation";
            this.mGadgetChartNation.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // mGadgetGrids
            // 
            this.mGadgetGrids.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mGadgetTotalStatsDefault,
            this.mGadgetTotalStats,
            this.toolStripSeparator26,
            this.mGadgetAddBattleModeStats,
            this.mGadgetAddTankTypeStats});
            this.mGadgetGrids.Name = "mGadgetGrids";
            this.mGadgetGrids.Size = new System.Drawing.Size(180, 22);
            this.mGadgetGrids.Text = "Grids";
            // 
            // mGadgetTotalStatsDefault
            // 
            this.mGadgetTotalStatsDefault.Name = "mGadgetTotalStatsDefault";
            this.mGadgetTotalStatsDefault.Size = new System.Drawing.Size(261, 22);
            this.mGadgetTotalStatsDefault.Tag = "ucTotalStatsDefault";
            this.mGadgetTotalStatsDefault.Text = "Total Stats - Using Default Columns";
            this.mGadgetTotalStatsDefault.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // mGadgetTotalStats
            // 
            this.mGadgetTotalStats.Name = "mGadgetTotalStats";
            this.mGadgetTotalStats.Size = new System.Drawing.Size(261, 22);
            this.mGadgetTotalStats.Tag = "ucTotalStats";
            this.mGadgetTotalStats.Text = "Total Stats - Empty";
            this.mGadgetTotalStats.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // toolStripSeparator26
            // 
            this.toolStripSeparator26.Name = "toolStripSeparator26";
            this.toolStripSeparator26.Size = new System.Drawing.Size(258, 6);
            // 
            // mGadgetAddBattleModeStats
            // 
            this.mGadgetAddBattleModeStats.Name = "mGadgetAddBattleModeStats";
            this.mGadgetAddBattleModeStats.Size = new System.Drawing.Size(261, 22);
            this.mGadgetAddBattleModeStats.Tag = "ucBattleTypes";
            this.mGadgetAddBattleModeStats.Text = "Battle Mode Stats";
            this.mGadgetAddBattleModeStats.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // mGadgetAddTankTypeStats
            // 
            this.mGadgetAddTankTypeStats.Name = "mGadgetAddTankTypeStats";
            this.mGadgetAddTankTypeStats.Size = new System.Drawing.Size(261, 22);
            this.mGadgetAddTankTypeStats.Tag = "ucTotalTanks";
            this.mGadgetAddTankTypeStats.Text = "Tank Type Stats";
            this.mGadgetAddTankTypeStats.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // mGadgetAddHeader
            // 
            this.mGadgetAddHeader.Name = "mGadgetAddHeader";
            this.mGadgetAddHeader.Size = new System.Drawing.Size(180, 22);
            this.mGadgetAddHeader.Tag = "ucHeading";
            this.mGadgetAddHeader.Text = "Header";
            this.mGadgetAddHeader.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // mGadgetAddImage
            // 
            this.mGadgetAddImage.Name = "mGadgetAddImage";
            this.mGadgetAddImage.Size = new System.Drawing.Size(180, 22);
            this.mGadgetAddImage.Tag = "ucBattleListLargeImages";
            this.mGadgetAddImage.Text = "Recent Battles";
            this.mGadgetAddImage.Click += new System.EventHandler(this.mGadgetAdd);
            // 
            // toolStripSeparator33
            // 
            this.toolStripSeparator33.Name = "toolStripSeparator33";
            this.toolStripSeparator33.Size = new System.Drawing.Size(177, 6);
            // 
            // mHomeViewFileSave
            // 
            this.mHomeViewFileSave.Name = "mHomeViewFileSave";
            this.mHomeViewFileSave.Size = new System.Drawing.Size(180, 22);
            this.mHomeViewFileSave.Text = "Save to File...";
            this.mHomeViewFileSave.Click += new System.EventHandler(this.mHomeViewFileSave_Click);
            // 
            // mHomeViewFileShowFolder
            // 
            this.mHomeViewFileShowFolder.Name = "mHomeViewFileShowFolder";
            this.mHomeViewFileShowFolder.Size = new System.Drawing.Size(180, 22);
            this.mHomeViewFileShowFolder.Text = "Show Files...";
            this.mHomeViewFileShowFolder.Click += new System.EventHandler(this.mHomeViewFileShowFolder_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
            // 
            // mGadgetRemoveAll
            // 
            this.mGadgetRemoveAll.Name = "mGadgetRemoveAll";
            this.mGadgetRemoveAll.Size = new System.Drawing.Size(180, 22);
            this.mGadgetRemoveAll.Text = "Remove All Gadgets";
            this.mGadgetRemoveAll.Click += new System.EventHandler(this.mGadgetRemoveAll_Click);
            // 
            // toolStripSeparator34
            // 
            this.toolStripSeparator34.Name = "toolStripSeparator34";
            this.toolStripSeparator34.Size = new System.Drawing.Size(6, 25);
            // 
            // mViewChart
            // 
            this.mViewChart.AutoSize = false;
            this.mViewChart.BackColor = System.Drawing.Color.Fuchsia;
            this.mViewChart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mViewChart.Image = ((System.Drawing.Image)(resources.GetObject("mViewChart.Image")));
            this.mViewChart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mViewChart.Name = "mViewChart";
            this.mViewChart.Size = new System.Drawing.Size(61, 22);
            this.mViewChart.Text = "Charts";
            this.mViewChart.ToolTipText = "Show Charts";
            this.mViewChart.Click += new System.EventHandler(this.toolItemViewChart_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(6, 25);
            // 
            // mSettings
            // 
            this.mSettings.AutoSize = false;
            this.mSettings.BackColor = System.Drawing.Color.Transparent;
            this.mSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSettingsRun,
            this.mSettingsRunBattleCheck,
            this.toolStripSeparator24,
            this.mUpdateDataFromAPI,
            this.toolStripSeparator18,
            this.mRecalcTankStatistics,
            this.mRecalcBattleRatings,
            this.mRecalcBattleCreditsPerTank,
            this.toolStripSeparator25,
            this.mSettingsShowLogFiles,
            this.mShowDbTables,
            this.toolStripSeparator1,
            this.mAppSettings,
            this.toolStripSeparator7,
            this.mExit});
            this.mSettings.Image = ((System.Drawing.Image)(resources.GetObject("mSettings.Image")));
            this.mSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mSettings.Name = "mSettings";
            this.mSettings.ShowDropDownArrow = false;
            this.mSettings.Size = new System.Drawing.Size(22, 22);
            this.mSettings.Text = "Settings";
            // 
            // mSettingsRun
            // 
            this.mSettingsRun.Enabled = false;
            this.mSettingsRun.Name = "mSettingsRun";
            this.mSettingsRun.Size = new System.Drawing.Size(264, 22);
            this.mSettingsRun.Text = "Automatically Fetch New Battles";
            this.mSettingsRun.Click += new System.EventHandler(this.toolItemSettingsRun_Click);
            this.mSettingsRun.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
            // 
            // mSettingsRunBattleCheck
            // 
            this.mSettingsRunBattleCheck.Enabled = false;
            this.mSettingsRunBattleCheck.Name = "mSettingsRunBattleCheck";
            this.mSettingsRunBattleCheck.Size = new System.Drawing.Size(264, 22);
            this.mSettingsRunBattleCheck.Text = "Check for New Battle";
            this.mSettingsRunBattleCheck.Click += new System.EventHandler(this.mSettingsRunBattleCheck_Click);
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(261, 6);
            // 
            // mUpdateDataFromAPI
            // 
            this.mUpdateDataFromAPI.Enabled = false;
            this.mUpdateDataFromAPI.Name = "mUpdateDataFromAPI";
            this.mUpdateDataFromAPI.Size = new System.Drawing.Size(264, 22);
            this.mUpdateDataFromAPI.Text = "Download and Update Tanks...";
            this.mUpdateDataFromAPI.Click += new System.EventHandler(this.toolItemUpdateDataFromAPI_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(261, 6);
            // 
            // mRecalcTankStatistics
            // 
            this.mRecalcTankStatistics.Enabled = false;
            this.mRecalcTankStatistics.Name = "mRecalcTankStatistics";
            this.mRecalcTankStatistics.Size = new System.Drawing.Size(264, 22);
            this.mRecalcTankStatistics.Text = "Recalculate Tank Statistics";
            this.mRecalcTankStatistics.Click += new System.EventHandler(this.mRecalcTankStatistics_Click);
            // 
            // mRecalcBattleRatings
            // 
            this.mRecalcBattleRatings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mRecalcBattleWN9,
            this.mRecalcBattleWN8,
            this.mRecalcBattleWN7,
            this.mRecalcBattleEFF,
            this.toolStripSeparator29,
            this.mRecalcBattleAllRatings});
            this.mRecalcBattleRatings.Enabled = false;
            this.mRecalcBattleRatings.Name = "mRecalcBattleRatings";
            this.mRecalcBattleRatings.Size = new System.Drawing.Size(264, 22);
            this.mRecalcBattleRatings.Text = "Recalculate Battle Ratings";
            // 
            // mRecalcBattleWN9
            // 
            this.mRecalcBattleWN9.Enabled = false;
            this.mRecalcBattleWN9.Name = "mRecalcBattleWN9";
            this.mRecalcBattleWN9.Size = new System.Drawing.Size(235, 22);
            this.mRecalcBattleWN9.Tag = "WN9";
            this.mRecalcBattleWN9.Text = "Recalculate Battle WN9...";
            this.mRecalcBattleWN9.Click += new System.EventHandler(this.mRecalcBattleRatings_Click);
            // 
            // mRecalcBattleWN8
            // 
            this.mRecalcBattleWN8.Enabled = false;
            this.mRecalcBattleWN8.Name = "mRecalcBattleWN8";
            this.mRecalcBattleWN8.Size = new System.Drawing.Size(235, 22);
            this.mRecalcBattleWN8.Tag = "WN8";
            this.mRecalcBattleWN8.Text = "Recalculate Battle WN8...";
            this.mRecalcBattleWN8.Click += new System.EventHandler(this.mRecalcBattleRatings_Click);
            // 
            // mRecalcBattleWN7
            // 
            this.mRecalcBattleWN7.Enabled = false;
            this.mRecalcBattleWN7.Name = "mRecalcBattleWN7";
            this.mRecalcBattleWN7.Size = new System.Drawing.Size(235, 22);
            this.mRecalcBattleWN7.Tag = "WN7";
            this.mRecalcBattleWN7.Text = "Recalculate Battle WN7...";
            this.mRecalcBattleWN7.Click += new System.EventHandler(this.mRecalcBattleRatings_Click);
            // 
            // mRecalcBattleEFF
            // 
            this.mRecalcBattleEFF.Enabled = false;
            this.mRecalcBattleEFF.Name = "mRecalcBattleEFF";
            this.mRecalcBattleEFF.Size = new System.Drawing.Size(235, 22);
            this.mRecalcBattleEFF.Tag = "EFF";
            this.mRecalcBattleEFF.Text = "Recalculate Battle EFF...";
            this.mRecalcBattleEFF.Click += new System.EventHandler(this.mRecalcBattleRatings_Click);
            // 
            // toolStripSeparator29
            // 
            this.toolStripSeparator29.Name = "toolStripSeparator29";
            this.toolStripSeparator29.Size = new System.Drawing.Size(232, 6);
            this.toolStripSeparator29.Click += new System.EventHandler(this.mRecalcBattleRatings_Click);
            // 
            // mRecalcBattleAllRatings
            // 
            this.mRecalcBattleAllRatings.Enabled = false;
            this.mRecalcBattleAllRatings.Name = "mRecalcBattleAllRatings";
            this.mRecalcBattleAllRatings.Size = new System.Drawing.Size(235, 22);
            this.mRecalcBattleAllRatings.Tag = "ALL";
            this.mRecalcBattleAllRatings.Text = "Recalculate All Battle Ratings...";
            this.mRecalcBattleAllRatings.Click += new System.EventHandler(this.mRecalcBattleRatings_Click);
            // 
            // mRecalcBattleCreditsPerTank
            // 
            this.mRecalcBattleCreditsPerTank.Enabled = false;
            this.mRecalcBattleCreditsPerTank.Name = "mRecalcBattleCreditsPerTank";
            this.mRecalcBattleCreditsPerTank.Size = new System.Drawing.Size(264, 22);
            this.mRecalcBattleCreditsPerTank.Text = "Recalculate Battle Credits per Tank...";
            this.mRecalcBattleCreditsPerTank.Click += new System.EventHandler(this.mRecalcBattleCreditsPerTank_Click);
            // 
            // toolStripSeparator25
            // 
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            this.toolStripSeparator25.Size = new System.Drawing.Size(261, 6);
            // 
            // mSettingsShowLogFiles
            // 
            this.mSettingsShowLogFiles.Name = "mSettingsShowLogFiles";
            this.mSettingsShowLogFiles.Size = new System.Drawing.Size(264, 22);
            this.mSettingsShowLogFiles.Text = "Show Log Files...";
            this.mSettingsShowLogFiles.Click += new System.EventHandler(this.mSettingsShowLogFiles_Click);
            // 
            // mShowDbTables
            // 
            this.mShowDbTables.Name = "mShowDbTables";
            this.mShowDbTables.Size = new System.Drawing.Size(264, 22);
            this.mShowDbTables.Text = "Show Database Tables...";
            this.mShowDbTables.Click += new System.EventHandler(this.toolItemShowDbTables_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(261, 6);
            // 
            // mAppSettings
            // 
            this.mAppSettings.Enabled = false;
            this.mAppSettings.Name = "mAppSettings";
            this.mAppSettings.Size = new System.Drawing.Size(264, 22);
            this.mAppSettings.Text = "Application Settings...";
            this.mAppSettings.Click += new System.EventHandler(this.mAppSettings_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(261, 6);
            // 
            // mExit
            // 
            this.mExit.Name = "mExit";
            this.mExit.Size = new System.Drawing.Size(264, 22);
            this.mExit.Text = "Exit";
            this.mExit.Click += new System.EventHandler(this.mExit_Click);
            // 
            // mHelp
            // 
            this.mHelp.AutoSize = false;
            this.mHelp.BackColor = System.Drawing.Color.Transparent;
            this.mHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mWotNumWebUserGuide,
            this.mWotNumWebForum,
            this.toolStripSeparator23,
            this.mHelpCheckVersion,
            this.mHelpMessage,
            this.toolStripSeparator2,
            this.mHelpAbout});
            this.mHelp.Image = ((System.Drawing.Image)(resources.GetObject("mHelp.Image")));
            this.mHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mHelp.Name = "mHelp";
            this.mHelp.ShowDropDownArrow = false;
            this.mHelp.Size = new System.Drawing.Size(22, 22);
            this.mHelp.Text = "toolStripDropDownButton1";
            this.mHelp.ToolTipText = "Help";
            // 
            // mWotNumWebUserGuide
            // 
            this.mWotNumWebUserGuide.Name = "mWotNumWebUserGuide";
            this.mWotNumWebUserGuide.Size = new System.Drawing.Size(286, 22);
            this.mWotNumWebUserGuide.Text = "User Guide...";
            this.mWotNumWebUserGuide.Click += new System.EventHandler(this.mWotNumWebUserGuide_Click);
            // 
            // mWotNumWebForum
            // 
            this.mWotNumWebForum.Name = "mWotNumWebForum";
            this.mWotNumWebForum.Size = new System.Drawing.Size(286, 22);
            this.mWotNumWebForum.Text = "Forum (Q&&A)...";
            this.mWotNumWebForum.Click += new System.EventHandler(this.mWotNumWebForum_Click);
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(283, 6);
            // 
            // mHelpCheckVersion
            // 
            this.mHelpCheckVersion.Name = "mHelpCheckVersion";
            this.mHelpCheckVersion.Size = new System.Drawing.Size(286, 22);
            this.mHelpCheckVersion.Text = "Check for new version";
            this.mHelpCheckVersion.Click += new System.EventHandler(this.mHelpCheckVersion_Click);
            // 
            // mHelpMessage
            // 
            this.mHelpMessage.Name = "mHelpMessage";
            this.mHelpMessage.Size = new System.Drawing.Size(286, 22);
            this.mHelpMessage.Text = "View message from Wot Numbers Team";
            this.mHelpMessage.Click += new System.EventHandler(this.mHelpMessage_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(283, 6);
            // 
            // mHelpAbout
            // 
            this.mHelpAbout.Name = "mHelpAbout";
            this.mHelpAbout.Size = new System.Drawing.Size(286, 22);
            this.mHelpAbout.Text = "About Wot Numbers...";
            this.mHelpAbout.Click += new System.EventHandler(this.mHelpAbout_Click);
            // 
            // toolStripSeparator30
            // 
            this.toolStripSeparator30.Name = "toolStripSeparator30";
            this.toolStripSeparator30.Size = new System.Drawing.Size(6, 25);
            // 
            // mVBaddict
            // 
            this.mVBaddict.BackColor = System.Drawing.Color.Transparent;
            this.mVBaddict.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mVBaddict.Image = ((System.Drawing.Image)(resources.GetObject("mVBaddict.Image")));
            this.mVBaddict.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mVBaddict.Name = "mVBaddict";
            this.mVBaddict.Size = new System.Drawing.Size(23, 22);
            this.mVBaddict.Text = "toolStripButton1";
            this.mVBaddict.ToolTipText = "Go to vBAddict Player Profile";
            this.mVBaddict.Visible = false;
            this.mVBaddict.Click += new System.EventHandler(this.mVBaddict_Click);
            // 
            // lblStatusRowCount
            // 
            this.lblStatusRowCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusRowCount.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusRowCount.ForeColor = System.Drawing.Color.DarkGray;
            this.lblStatusRowCount.Location = new System.Drawing.Point(1030, 611);
            this.lblStatusRowCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblStatusRowCount.Name = "lblStatusRowCount";
            this.lblStatusRowCount.Size = new System.Drawing.Size(77, 13);
            this.lblStatusRowCount.TabIndex = 19;
            this.lblStatusRowCount.Text = "Rows";
            this.lblStatusRowCount.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // panelMainArea
            // 
            this.panelMainArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelMainArea.Controls.Add(this.scrollCorner);
            this.panelMainArea.Controls.Add(this.scrollY);
            this.panelMainArea.Controls.Add(this.dataGridMain);
            this.panelMainArea.Controls.Add(this.scrollX);
            this.panelMainArea.Location = new System.Drawing.Point(9, 57);
            this.panelMainArea.Name = "panelMainArea";
            this.panelMainArea.Size = new System.Drawing.Size(649, 336);
            this.panelMainArea.TabIndex = 18;
            // 
            // scrollCorner
            // 
            this.scrollCorner.Image = null;
            this.scrollCorner.Location = new System.Drawing.Point(621, 298);
            this.scrollCorner.Name = "scrollCorner";
            this.scrollCorner.Size = new System.Drawing.Size(17, 17);
            this.scrollCorner.TabIndex = 19;
            this.scrollCorner.Text = "badScrollBarCorner1";
            this.scrollCorner.Visible = false;
            // 
            // scrollY
            // 
            this.scrollY.BackColor = System.Drawing.Color.Transparent;
            this.scrollY.Image = null;
            this.scrollY.Location = new System.Drawing.Point(621, 16);
            this.scrollY.Name = "scrollY";
            this.scrollY.ScrollElementsTotals = 100;
            this.scrollY.ScrollElementsVisible = 0;
            this.scrollY.ScrollHide = true;
            this.scrollY.ScrollNecessary = true;
            this.scrollY.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
            this.scrollY.ScrollPosition = 0;
            this.scrollY.Size = new System.Drawing.Size(17, 276);
            this.scrollY.TabIndex = 21;
            this.scrollY.Text = "badScrollBar2";
            this.scrollY.Visible = false;
            this.scrollY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollY_MouseDown);
            this.scrollY.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollY_MouseMove);
            this.scrollY.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scrollX_MouseUp);
            // 
            // dataGridMain
            // 
            this.dataGridMain.AllowUserToAddRows = false;
            this.dataGridMain.AllowUserToDeleteRows = false;
            this.dataGridMain.AllowUserToResizeRows = false;
            this.dataGridMain.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dataGridMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridMain.CausesValidation = false;
            this.dataGridMain.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.NullValue = null;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2, 2, 0, 2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridMain.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridMain.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridMain.EnableHeadersVisualStyles = false;
            this.dataGridMain.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.dataGridMain.Location = new System.Drawing.Point(14, 16);
            this.dataGridMain.Name = "dataGridMain";
            this.dataGridMain.ReadOnly = true;
            this.dataGridMain.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridMain.RowHeadersWidth = 25;
            this.dataGridMain.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridMain.ShowEditingIcon = false;
            this.dataGridMain.Size = new System.Drawing.Size(601, 276);
            this.dataGridMain.TabIndex = 11;
            this.dataGridMain.Visible = false;
            this.dataGridMain.RowHeadersWidthChanged += new System.EventHandler(this.dataGridMain_RowHeadersWidthChanged);
            this.dataGridMain.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridMain_CellFormatting);
            this.dataGridMain.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridMain_CellMouseDown);
            this.dataGridMain.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridMain_CellPainting);
            this.dataGridMain.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridMain_ColumnHeaderMouseClick);
            this.dataGridMain.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridMain_ColumnWidthChanged);
            this.dataGridMain.SelectionChanged += new System.EventHandler(this.dataGridMain_SelectionChanged);
            // 
            // scrollX
            // 
            this.scrollX.BackColor = System.Drawing.Color.Transparent;
            this.scrollX.Image = null;
            this.scrollX.Location = new System.Drawing.Point(14, 298);
            this.scrollX.Name = "scrollX";
            this.scrollX.ScrollElementsTotals = 100;
            this.scrollX.ScrollElementsVisible = 0;
            this.scrollX.ScrollHide = true;
            this.scrollX.ScrollNecessary = true;
            this.scrollX.ScrollOrientation = System.Windows.Forms.ScrollOrientation.HorizontalScroll;
            this.scrollX.ScrollPosition = 0;
            this.scrollX.Size = new System.Drawing.Size(601, 17);
            this.scrollX.TabIndex = 20;
            this.scrollX.Text = "badScrollBar1";
            this.scrollX.Visible = false;
            this.scrollX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollX_MouseDown);
            this.scrollX.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollX_MouseMove);
            this.scrollX.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scrollX_MouseUp);
            // 
            // lblStatus2
            // 
            this.lblStatus2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus2.AutoSize = true;
            this.lblStatus2.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus2.ForeColor = System.Drawing.Color.DarkGray;
            this.lblStatus2.Location = new System.Drawing.Point(69, 611);
            this.lblStatus2.Margin = new System.Windows.Forms.Padding(0);
            this.lblStatus2.Name = "lblStatus2";
            this.lblStatus2.Size = new System.Drawing.Size(82, 13);
            this.lblStatus2.TabIndex = 16;
            this.lblStatus2.Text = "Action message";
            this.lblStatus2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblStatus1
            // 
            this.lblStatus1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus1.AutoSize = true;
            this.lblStatus1.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblStatus1.Location = new System.Drawing.Point(13, 611);
            this.lblStatus1.Margin = new System.Windows.Forms.Padding(0);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(37, 13);
            this.lblStatus1.TabIndex = 14;
            this.lblStatus1.Text = "Status";
            this.lblStatus1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Fuchsia;
            this.ClientSize = new System.Drawing.Size(1119, 631);
            this.Controls.Add(this.MainTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(450, 250);
            this.Name = "Main";
            this.ShowInTaskbar = false;
            this.Text = "Wot Numbers";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.ResizeEnd += new System.EventHandler(this.Main_ResizeEnd);
            this.LocationChanged += new System.EventHandler(this.Main_LocationChanged);
            this.Resize += new System.EventHandler(this.Main_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherNewBattle)).EndInit();
            this.MainTheme.ResumeLayout(false);
            this.MainTheme.PerformLayout();
            this.toolMain.ResumeLayout(false);
            this.toolMain.PerformLayout();
            this.panelMainArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMain)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer timerStatus2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.IO.FileSystemWatcher fileSystemWatcherNewBattle;
		private BadForm MainTheme;
		private System.Windows.Forms.Label lblStatus2;
		private System.Windows.Forms.Label lblStatus1;
		private System.Windows.Forms.Panel panelMainArea;
		private System.Windows.Forms.ImageList imageListToolStrip;
		private System.Windows.Forms.Label lblStatusRowCount;
		private System.Windows.Forms.DataGridView dataGridMain;
		private BadScrollBar scrollY;
		private BadScrollBar scrollX;
		private BadScrollBarCorner scrollCorner;
		private Code.ToolStripEx toolMain;
		private System.Windows.Forms.ToolStripLabel mViewLabel;
		private System.Windows.Forms.ToolStripButton mViewOverall;
		private System.Windows.Forms.ToolStripButton mViewTankInfo;
		private System.Windows.Forms.ToolStripButton mViewBattles;
		private System.Windows.Forms.ToolStripButton mRefresh;
		private System.Windows.Forms.ToolStripSeparator mRefreshSeparator;
		private System.Windows.Forms.ToolStripDropDownButton mColumnSelect;
		private System.Windows.Forms.ToolStripMenuItem mColumnSelect_01;
		private System.Windows.Forms.ToolStripMenuItem mColumnSelect_02;
		private System.Windows.Forms.ToolStripMenuItem mColumnSelect_03;
		private System.Windows.Forms.ToolStripMenuItem mColumnSelect_04;
		private System.Windows.Forms.ToolStripMenuItem mColumnSelect_05;
		private System.Windows.Forms.ToolStripMenuItem mColumnSelect_06;
		private System.Windows.Forms.ToolStripMenuItem mColumnSelect_07;
		private System.Windows.Forms.ToolStripMenuItem mColumnSelect_08;
		private System.Windows.Forms.ToolStripMenuItem mColumnSelect_09;
		private System.Windows.Forms.ToolStripMenuItem mColumnSelect_10;
		private System.Windows.Forms.ToolStripMenuItem mColumnSelect_11;
		private System.Windows.Forms.ToolStripMenuItem mColumnSelect_12;
		private System.Windows.Forms.ToolStripMenuItem mColumnSelect_13;
		private System.Windows.Forms.ToolStripMenuItem mColumnSelect_14;
		private System.Windows.Forms.ToolStripMenuItem mColumnSelect_15;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem mColumnSelect_Edit;
		private System.Windows.Forms.ToolStripDropDownButton mTankFilter;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_All;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Country;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_CountryChina;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_CountryFrance;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_CountryGermany;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_CountryUK;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_CountryUSA;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_CountryUSSR;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_CountryJapan;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Type;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_TypeLT;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_TypeMT;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_TypeHT;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_TypeTD;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_TypeSPG;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Tier;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Tier1;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Tier2;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Tier3;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Tier4;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Tier5;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Tier6;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Tier7;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Tier8;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Tier9;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Tier10;
		private System.Windows.Forms.ToolStripSeparator mTankFilter_FavSeparator;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Fav01;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Fav02;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Fav03;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Fav04;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Fav05;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Fav06;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Fav07;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Fav08;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Fav09;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Fav10;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_EditFavList;
		private System.Windows.Forms.ToolStripDropDownButton mMode;
		private System.Windows.Forms.ToolStripMenuItem mModeAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
		private System.Windows.Forms.ToolStripMenuItem mModeRandomTankCompany;
		private System.Windows.Forms.ToolStripMenuItem mModeTeam;
		private System.Windows.Forms.ToolStripMenuItem mModeHistorical;
		private System.Windows.Forms.ToolStripMenuItem mModeRandom;
		private System.Windows.Forms.ToolStripMenuItem mModeCompany;
		private System.Windows.Forms.ToolStripMenuItem mModeClan;
		private System.Windows.Forms.ToolStripDropDownButton mBattles;
		private System.Windows.Forms.ToolStripMenuItem mBattles1d;
		private System.Windows.Forms.ToolStripMenuItem mBattlesYesterday;
		private System.Windows.Forms.ToolStripMenuItem mBattles3d;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
		private System.Windows.Forms.ToolStripMenuItem mBattles1w;
		private System.Windows.Forms.ToolStripMenuItem mBattles2w;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
		private System.Windows.Forms.ToolStripMenuItem mBattles1m;
		private System.Windows.Forms.ToolStripMenuItem mBattles3m;
		private System.Windows.Forms.ToolStripMenuItem mBattles6m;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
		private System.Windows.Forms.ToolStripMenuItem mBattles1y;
		private System.Windows.Forms.ToolStripMenuItem mBattles2y;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
        private System.Windows.Forms.ToolStripMenuItem mBattlesAll;
		private System.Windows.Forms.ToolStripDropDownButton mSettings;
		private System.Windows.Forms.ToolStripMenuItem mSettingsRun;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
        private System.Windows.Forms.ToolStripMenuItem mUpdateDataFromAPI;
		private System.Windows.Forms.ToolStripMenuItem mShowDbTables;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator25;
		private System.Windows.Forms.ToolStripButton mViewChart;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_GetInGarage;
		private System.Windows.Forms.ToolStripDropDownButton mHelp;
		private System.Windows.Forms.ToolStripMenuItem mHelpCheckVersion;
		private System.Windows.Forms.ToolStripMenuItem mHelpMessage;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem mHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem mTankFilter_Clear;
		private System.Windows.Forms.ToolStripMenuItem mModeSkrimishes;
		private System.Windows.Forms.ToolStripMenuItem mBattles2d;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton mHomeView;
        private System.Windows.Forms.ToolStripMenuItem mHomeViewClassic;
		private System.Windows.Forms.ToolStripDropDownButton mBattleGroup;
		private System.Windows.Forms.ToolStripMenuItem mBattleGroup_No;
		private System.Windows.Forms.ToolStripMenuItem mBattleGroup_TankAverage;
		private System.Windows.Forms.ToolStripMenuItem mBattleGroup_TankSum;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem mHomeViewRedraw;
		private System.Windows.Forms.ImageList imageGrid;
		private System.Windows.Forms.ToolStripMenuItem mModeRandomSoloPlatoon;
		private System.Windows.Forms.ToolStripMenuItem mModeRandomSolo;
		private System.Windows.Forms.ToolStripMenuItem mRandomPlatoon;
		private System.Windows.Forms.ToolStripMenuItem mRandomPlatoon2;
		private System.Windows.Forms.ToolStripMenuItem mRandomPlatoon3;
		private System.Windows.Forms.ToolStripButton mWoT;
        private System.Windows.Forms.Timer timerWoTAffnity;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem mExit;
		private System.Windows.Forms.ToolStripMenuItem mBattlesCustomUse;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		private System.Windows.Forms.ToolStripMenuItem mBattlesCustomChange;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_All_NotOwned;
        private System.Windows.Forms.ToolStripMenuItem mSettingsRunBattleCheck;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorForBattleView;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripMenuItem mModeSpecial;
		private System.Windows.Forms.ToolStripMenuItem mSettingsShowLogFiles;
		private System.Windows.Forms.ToolStripMenuItem mModeBattleForStronghold;
		private System.Windows.Forms.ToolStripMenuItem mModeTeamRanked;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
		private System.Windows.Forms.ToolStripMenuItem mModeGlobalMap;
        private System.Windows.Forms.ToolStripMenuItem mRecalcBattleCreditsPerTank;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripMenuItem mAppSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mTankFilter_CountryCzechoslovakia;
        private System.Windows.Forms.ToolStripButton mViewMaps;
        private System.Windows.Forms.ToolStripDropDownButton mMapViewType;
        private System.Windows.Forms.ToolStripMenuItem mMapDefault;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripMenuItem mMapShowAll;
        private System.Windows.Forms.ToolStripMenuItem mMapDescr;
        private System.Windows.Forms.ToolStripMenuItem mMapDescrLarge;
        private System.Windows.Forms.ToolStripMenuItem mMapShowOld;
        private System.Windows.Forms.ToolStripButton mVBaddict;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator28;
        private System.Windows.Forms.ToolStripMenuItem mHomeViewFileLoad;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem mRecalcBattleRatings;
        private System.Windows.Forms.ToolStripMenuItem mRecalcBattleWN8;
        private System.Windows.Forms.ToolStripMenuItem mRecalcBattleWN7;
        private System.Windows.Forms.ToolStripMenuItem mRecalcBattleEFF;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator29;
        private System.Windows.Forms.ToolStripMenuItem mRecalcBattleAllRatings;
        private System.Windows.Forms.ToolStripMenuItem mRecalcTankStatistics;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.ToolStripMenuItem mHomeViewDefault;
        private System.Windows.Forms.ToolStripMenuItem mTankFilter_Search;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator30;
        private System.Windows.Forms.ToolStripMenuItem mWotNumWebForum;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
        private System.Windows.Forms.ToolStripMenuItem mWotNumWebUserGuide;
        private System.Windows.Forms.ToolStripMenuItem mRecalcBattleWN9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator31;
        private System.Windows.Forms.ToolStripMenuItem mHomeViewRecent1;
        private System.Windows.Forms.ToolStripMenuItem mHomeViewRecent5;
        private System.Windows.Forms.ToolStripMenuItem mHomeViewRecent4;
        private System.Windows.Forms.ToolStripMenuItem mHomeViewRecent3;
        private System.Windows.Forms.ToolStripMenuItem mHomeViewRecent2;
        private System.Windows.Forms.ToolStripDropDownButton mHomeViewEdit;
        private System.Windows.Forms.ToolStripMenuItem mHomeViewEditMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator32;
        private System.Windows.Forms.ToolStripMenuItem mGadgetGauges;
        private System.Windows.Forms.ToolStripMenuItem mGadgetWR;
        private System.Windows.Forms.ToolStripMenuItem mGadgetRWR;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator27;
        private System.Windows.Forms.ToolStripMenuItem mGadgetWN9;
        private System.Windows.Forms.ToolStripMenuItem mGadgetWN8;
        private System.Windows.Forms.ToolStripMenuItem mGadgetWN7;
        private System.Windows.Forms.ToolStripMenuItem mGadgetEFF;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem mGadgetKillDeath;
        private System.Windows.Forms.ToolStripMenuItem mGadgetDamageCausedReceived;
        private System.Windows.Forms.ToolStripMenuItem mGadgetCharts;
        private System.Windows.Forms.ToolStripMenuItem mGadgetChartTier;
        private System.Windows.Forms.ToolStripMenuItem mGadgetChartTankType;
        private System.Windows.Forms.ToolStripMenuItem mGadgetChartNation;
        private System.Windows.Forms.ToolStripMenuItem mGadgetGrids;
        private System.Windows.Forms.ToolStripMenuItem mGadgetTotalStatsDefault;
        private System.Windows.Forms.ToolStripMenuItem mGadgetTotalStats;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator26;
        private System.Windows.Forms.ToolStripMenuItem mGadgetAddBattleModeStats;
        private System.Windows.Forms.ToolStripMenuItem mGadgetAddTankTypeStats;
        private System.Windows.Forms.ToolStripMenuItem mGadgetAddHeader;
        private System.Windows.Forms.ToolStripMenuItem mGadgetAddImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator33;
        private System.Windows.Forms.ToolStripMenuItem mHomeViewFileSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mGadgetRemoveAll;
        private System.Windows.Forms.ToolStripMenuItem mHomeViewFileShowFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator34;
        private System.Windows.Forms.ToolStripSeparator mHomeViewRecentSeparator;
        private System.Windows.Forms.ToolStripMenuItem mHomeViewClearRecentList;
    }
}

