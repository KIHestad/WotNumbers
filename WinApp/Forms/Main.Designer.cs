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
			this.timerPanelSlide = new System.Windows.Forms.Timer(this.components);
			this.fileSystemWatcherNewBattle = new System.IO.FileSystemWatcher();
			this.imageListToolStrip = new System.Windows.Forms.ImageList(this.components);
			this.MainTheme = new BadForm();
			this.toolMain = new WinApp.Code.ToolStripEx();
			this.mViewLabel = new System.Windows.Forms.ToolStripLabel();
			this.mViewOverall = new System.Windows.Forms.ToolStripButton();
			this.mViewTankInfo = new System.Windows.Forms.ToolStripButton();
			this.mViewBattles = new System.Windows.Forms.ToolStripButton();
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
			this.mTankFilter = new System.Windows.Forms.ToolStripDropDownButton();
			this.mTankFilter_Country = new System.Windows.Forms.ToolStripMenuItem();
			this.mTankFilter_CountryChina = new System.Windows.Forms.ToolStripMenuItem();
			this.mTankFilter_CountryFrance = new System.Windows.Forms.ToolStripMenuItem();
			this.mTankFilter_CountryGermany = new System.Windows.Forms.ToolStripMenuItem();
			this.mTankFilter_CountryUK = new System.Windows.Forms.ToolStripMenuItem();
			this.mTankFilter_CountryUSA = new System.Windows.Forms.ToolStripMenuItem();
			this.mTankFilter_CountryJapan = new System.Windows.Forms.ToolStripMenuItem();
			this.mTankFilter_CountryUSSR = new System.Windows.Forms.ToolStripMenuItem();
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
			this.mTankFilter_Clear = new System.Windows.Forms.ToolStripMenuItem();
			this.mTankFilter_FavSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.mTankFilter_All = new System.Windows.Forms.ToolStripMenuItem();
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
			this.toolStripMenuItem54 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
			this.mModeRandomCompanyClan = new System.Windows.Forms.ToolStripMenuItem();
			this.mModeHistorical = new System.Windows.Forms.ToolStripMenuItem();
			this.mModeTeam = new System.Windows.Forms.ToolStripMenuItem();
			this.mModeSkrimishes = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
			this.mModeRandom = new System.Windows.Forms.ToolStripMenuItem();
			this.mModeCompany = new System.Windows.Forms.ToolStripMenuItem();
			this.mModeClan = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.mModeSpecialInfo = new System.Windows.Forms.ToolStripMenuItem();
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
			this.mBattlesAll = new System.Windows.Forms.ToolStripMenuItem();
			this.mBattleGroup = new System.Windows.Forms.ToolStripDropDownButton();
			this.mBattleGroup_No = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.mBattleGroup_TankAverage = new System.Windows.Forms.ToolStripMenuItem();
			this.mBattleGroup_TankSum = new System.Windows.Forms.ToolStripMenuItem();
			this.mGadget = new System.Windows.Forms.ToolStripDropDownButton();
			this.gaugesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.winRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.wN8RatingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.wN7RatingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.efficiencyRatingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mGadgetAddBattleModeStats = new System.Windows.Forms.ToolStripMenuItem();
			this.mGadgetAddTankTypeStats = new System.Windows.Forms.ToolStripMenuItem();
			this.mGadgetAddImage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.mGadgetEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mHomeEdit = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
			this.mViewChart = new System.Windows.Forms.ToolStripButton();
			this.mSettings = new System.Windows.Forms.ToolStripDropDownButton();
			this.mSettingsRun = new System.Windows.Forms.ToolStripMenuItem();
			this.mSettingsDossierOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.mSettingsRunManual = new System.Windows.Forms.ToolStripMenuItem();
			this.mSettingsForceUpdateFromPrev = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mSettingsTestAddBattleResult = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
			this.mUpdateDataFromAPI = new System.Windows.Forms.ToolStripMenuItem();
			this.mImportBattlesFromWotStat = new System.Windows.Forms.ToolStripMenuItem();
			this.mShowDbTables = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
			this.mSettingsAppLayout = new System.Windows.Forms.ToolStripMenuItem();
			this.mSettingsApp = new System.Windows.Forms.ToolStripMenuItem();
			this.mHelp = new System.Windows.Forms.ToolStripDropDownButton();
			this.mHelpCheckVersion = new System.Windows.Forms.ToolStripMenuItem();
			this.mHelpMessage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.mHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.lblStatusRowCount = new System.Windows.Forms.Label();
			this.panelMainArea = new System.Windows.Forms.Panel();
			this.dataGridMain = new System.Windows.Forms.DataGridView();
			this.panelInfo = new System.Windows.Forms.Panel();
			this.lblOverView = new System.Windows.Forms.Label();
			this.picIS7 = new System.Windows.Forms.PictureBox();
			this.scrollY = new BadScrollBar();
			this.scrollCorner = new BadScrollBarCorner();
			this.scrollX = new BadScrollBar();
			this.lblStatus2 = new System.Windows.Forms.Label();
			this.lblStatus1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherNewBattle)).BeginInit();
			this.MainTheme.SuspendLayout();
			this.toolMain.SuspendLayout();
			this.panelMainArea.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridMain)).BeginInit();
			this.panelInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picIS7)).BeginInit();
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
			// timerPanelSlide
			// 
			this.timerPanelSlide.Interval = 5;
			this.timerPanelSlide.Tick += new System.EventHandler(this.timerPanelSlide_Tick);
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
			this.imageListToolStrip.Images.SetKeyName(1, "tank_detail.png");
			this.imageListToolStrip.Images.SetKeyName(2, "iconCharts_v2b.png");
			this.imageListToolStrip.Images.SetKeyName(3, "xp.png");
			this.imageListToolStrip.Images.SetKeyName(4, "filter.png");
			this.imageListToolStrip.Images.SetKeyName(5, "tank_add.png");
			this.imageListToolStrip.Images.SetKeyName(6, "tank_remove.png");
			this.imageListToolStrip.Images.SetKeyName(7, "tank_new.png");
			this.imageListToolStrip.Images.SetKeyName(8, "delete.png");
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
			this.MainTheme.FormFooter = true;
			this.MainTheme.FormFooterHeight = 26;
			this.MainTheme.FormInnerBorder = 0;
			this.MainTheme.FormMargin = 0;
			this.MainTheme.Image = ((System.Drawing.Image)(resources.GetObject("MainTheme.Image")));
			this.MainTheme.Location = new System.Drawing.Point(0, 0);
			this.MainTheme.MainArea = mainAreaClass1;
			this.MainTheme.Name = "MainTheme";
			this.MainTheme.Resizable = true;
			this.MainTheme.Size = new System.Drawing.Size(872, 431);
			this.MainTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("MainTheme.SystemExitImage")));
			this.MainTheme.SystemMaximizeImage = ((System.Drawing.Image)(resources.GetObject("MainTheme.SystemMaximizeImage")));
			this.MainTheme.SystemMinimizeImage = ((System.Drawing.Image)(resources.GetObject("MainTheme.SystemMinimizeImage")));
			this.MainTheme.TabIndex = 18;
			this.MainTheme.Text = "Wot Numbers";
			this.MainTheme.TitleHeight = 53;
			// 
			// toolMain
			// 
			this.toolMain.Dock = System.Windows.Forms.DockStyle.None;
			this.toolMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mViewLabel,
            this.mViewOverall,
            this.mViewTankInfo,
            this.mViewBattles,
            this.mRefresh,
            this.mRefreshSeparator,
            this.mColumnSelect,
            this.mTankFilter,
            this.mMode,
            this.mBattles,
            this.mBattleGroup,
            this.mGadget,
            this.mHomeEdit,
            this.toolStripSeparator23,
            this.mViewChart,
            this.mSettings,
            this.mHelp});
			this.toolMain.Location = new System.Drawing.Point(9, 29);
			this.toolMain.Name = "toolMain";
			this.toolMain.Size = new System.Drawing.Size(862, 25);
			this.toolMain.Stretch = true;
			this.toolMain.TabIndex = 18;
			this.toolMain.Text = "toolStripEx1";
			// 
			// mViewLabel
			// 
			this.mViewLabel.Name = "mViewLabel";
			this.mViewLabel.Size = new System.Drawing.Size(35, 22);
			this.mViewLabel.Text = "View:";
			// 
			// mViewOverall
			// 
			this.mViewOverall.Checked = true;
			this.mViewOverall.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mViewOverall.Image = ((System.Drawing.Image)(resources.GetObject("mViewOverall.Image")));
			this.mViewOverall.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mViewOverall.Name = "mViewOverall";
			this.mViewOverall.Size = new System.Drawing.Size(60, 22);
			this.mViewOverall.Text = "&Home";
			this.mViewOverall.ToolTipText = " ";
			this.mViewOverall.Click += new System.EventHandler(this.toolItemViewOverall_Click);
			// 
			// mViewTankInfo
			// 
			this.mViewTankInfo.Image = ((System.Drawing.Image)(resources.GetObject("mViewTankInfo.Image")));
			this.mViewTankInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mViewTankInfo.Name = "mViewTankInfo";
			this.mViewTankInfo.Size = new System.Drawing.Size(58, 22);
			this.mViewTankInfo.Text = "&Tanks";
			this.mViewTankInfo.Click += new System.EventHandler(this.toolItemViewTankInfo_Click);
			// 
			// mViewBattles
			// 
			this.mViewBattles.Image = ((System.Drawing.Image)(resources.GetObject("mViewBattles.Image")));
			this.mViewBattles.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mViewBattles.Name = "mViewBattles";
			this.mViewBattles.Size = new System.Drawing.Size(62, 22);
			this.mViewBattles.Text = "&Battles";
			this.mViewBattles.Click += new System.EventHandler(this.toolItemViewBattles_Click);
			// 
			// mRefresh
			// 
			this.mRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.mRefresh.Image = ((System.Drawing.Image)(resources.GetObject("mRefresh.Image")));
			this.mRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mRefresh.Name = "mRefresh";
			this.mRefresh.Size = new System.Drawing.Size(23, 22);
			this.mRefresh.Text = "Refresh grid";
			this.mRefresh.Click += new System.EventHandler(this.toolItemRefresh_Click);
			// 
			// mRefreshSeparator
			// 
			this.mRefreshSeparator.Name = "mRefreshSeparator";
			this.mRefreshSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// mColumnSelect
			// 
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
			this.mColumnSelect.Image = ((System.Drawing.Image)(resources.GetObject("mColumnSelect.Image")));
			this.mColumnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mColumnSelect.Name = "mColumnSelect";
			this.mColumnSelect.ShowDropDownArrow = false;
			this.mColumnSelect.Size = new System.Drawing.Size(65, 22);
			this.mColumnSelect.Text = "Default";
			this.mColumnSelect.ToolTipText = "Default";
			// 
			// mColumnSelect_01
			// 
			this.mColumnSelect_01.Checked = true;
			this.mColumnSelect_01.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mColumnSelect_01.Name = "mColumnSelect_01";
			this.mColumnSelect_01.Size = new System.Drawing.Size(195, 22);
			this.mColumnSelect_01.Text = "Col List #1";
			this.mColumnSelect_01.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.mColumnSelect_01.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mColumnSelect_02
			// 
			this.mColumnSelect_02.Name = "mColumnSelect_02";
			this.mColumnSelect_02.Size = new System.Drawing.Size(195, 22);
			this.mColumnSelect_02.Text = "Col List #2";
			this.mColumnSelect_02.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.mColumnSelect_02.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mColumnSelect_03
			// 
			this.mColumnSelect_03.Name = "mColumnSelect_03";
			this.mColumnSelect_03.Size = new System.Drawing.Size(195, 22);
			this.mColumnSelect_03.Text = "Col List #3";
			this.mColumnSelect_03.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.mColumnSelect_03.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mColumnSelect_04
			// 
			this.mColumnSelect_04.Name = "mColumnSelect_04";
			this.mColumnSelect_04.Size = new System.Drawing.Size(195, 22);
			this.mColumnSelect_04.Text = "Col List #4";
			this.mColumnSelect_04.Visible = false;
			this.mColumnSelect_04.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.mColumnSelect_04.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mColumnSelect_05
			// 
			this.mColumnSelect_05.Name = "mColumnSelect_05";
			this.mColumnSelect_05.Size = new System.Drawing.Size(195, 22);
			this.mColumnSelect_05.Text = "Col List #5";
			this.mColumnSelect_05.Visible = false;
			this.mColumnSelect_05.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.mColumnSelect_05.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mColumnSelect_06
			// 
			this.mColumnSelect_06.Name = "mColumnSelect_06";
			this.mColumnSelect_06.Size = new System.Drawing.Size(195, 22);
			this.mColumnSelect_06.Text = "Col List #6";
			this.mColumnSelect_06.Visible = false;
			this.mColumnSelect_06.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.mColumnSelect_06.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mColumnSelect_07
			// 
			this.mColumnSelect_07.Name = "mColumnSelect_07";
			this.mColumnSelect_07.Size = new System.Drawing.Size(195, 22);
			this.mColumnSelect_07.Text = "Col List #7";
			this.mColumnSelect_07.Visible = false;
			this.mColumnSelect_07.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.mColumnSelect_07.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mColumnSelect_08
			// 
			this.mColumnSelect_08.Name = "mColumnSelect_08";
			this.mColumnSelect_08.Size = new System.Drawing.Size(195, 22);
			this.mColumnSelect_08.Text = "Col List #8";
			this.mColumnSelect_08.Visible = false;
			this.mColumnSelect_08.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.mColumnSelect_08.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mColumnSelect_09
			// 
			this.mColumnSelect_09.Name = "mColumnSelect_09";
			this.mColumnSelect_09.Size = new System.Drawing.Size(195, 22);
			this.mColumnSelect_09.Text = "Col List #9";
			this.mColumnSelect_09.Visible = false;
			this.mColumnSelect_09.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.mColumnSelect_09.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mColumnSelect_10
			// 
			this.mColumnSelect_10.Name = "mColumnSelect_10";
			this.mColumnSelect_10.Size = new System.Drawing.Size(195, 22);
			this.mColumnSelect_10.Text = "Col List #10";
			this.mColumnSelect_10.Visible = false;
			this.mColumnSelect_10.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.mColumnSelect_10.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mColumnSelect_11
			// 
			this.mColumnSelect_11.Name = "mColumnSelect_11";
			this.mColumnSelect_11.Size = new System.Drawing.Size(195, 22);
			this.mColumnSelect_11.Text = "Col List #11";
			this.mColumnSelect_11.Visible = false;
			this.mColumnSelect_11.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.mColumnSelect_11.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mColumnSelect_12
			// 
			this.mColumnSelect_12.Name = "mColumnSelect_12";
			this.mColumnSelect_12.Size = new System.Drawing.Size(195, 22);
			this.mColumnSelect_12.Text = "Col List #12";
			this.mColumnSelect_12.Visible = false;
			this.mColumnSelect_12.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.mColumnSelect_12.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mColumnSelect_13
			// 
			this.mColumnSelect_13.Name = "mColumnSelect_13";
			this.mColumnSelect_13.Size = new System.Drawing.Size(195, 22);
			this.mColumnSelect_13.Text = "Col List #13";
			this.mColumnSelect_13.Visible = false;
			this.mColumnSelect_13.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.mColumnSelect_13.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mColumnSelect_14
			// 
			this.mColumnSelect_14.Name = "mColumnSelect_14";
			this.mColumnSelect_14.Size = new System.Drawing.Size(195, 22);
			this.mColumnSelect_14.Text = "Col List #14";
			this.mColumnSelect_14.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.mColumnSelect_14.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mColumnSelect_15
			// 
			this.mColumnSelect_15.Name = "mColumnSelect_15";
			this.mColumnSelect_15.Size = new System.Drawing.Size(195, 22);
			this.mColumnSelect_15.Text = "Col List #15";
			this.mColumnSelect_15.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.mColumnSelect_15.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(192, 6);
			// 
			// mColumnSelect_Edit
			// 
			this.mColumnSelect_Edit.Name = "mColumnSelect_Edit";
			this.mColumnSelect_Edit.Size = new System.Drawing.Size(195, 22);
			this.mColumnSelect_Edit.Text = "Edit Tank/Battle View...";
			this.mColumnSelect_Edit.Click += new System.EventHandler(this.toolItemColumnSelect_Edit_Click);
			// 
			// mTankFilter
			// 
			this.mTankFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mTankFilter_Country,
            this.mTankFilter_Type,
            this.mTankFilter_Tier,
            this.mTankFilter_Clear,
            this.mTankFilter_FavSeparator,
            this.mTankFilter_All,
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
			this.mTankFilter.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter.Image")));
			this.mTankFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mTankFilter.Name = "mTankFilter";
			this.mTankFilter.ShowDropDownArrow = false;
			this.mTankFilter.Size = new System.Drawing.Size(75, 22);
			this.mTankFilter.Text = "All Tanks";
			this.mTankFilter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_MouseDown);
			// 
			// mTankFilter_Country
			// 
			this.mTankFilter_Country.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mTankFilter_CountryChina,
            this.mTankFilter_CountryFrance,
            this.mTankFilter_CountryGermany,
            this.mTankFilter_CountryUK,
            this.mTankFilter_CountryUSA,
            this.mTankFilter_CountryJapan,
            this.mTankFilter_CountryUSSR});
			this.mTankFilter_Country.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Country.Image")));
			this.mTankFilter_Country.Name = "mTankFilter_Country";
			this.mTankFilter_Country.Size = new System.Drawing.Size(234, 22);
			this.mTankFilter_Country.Text = "Nation";
			// 
			// mTankFilter_CountryChina
			// 
			this.mTankFilter_CountryChina.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryChina.Image")));
			this.mTankFilter_CountryChina.Name = "mTankFilter_CountryChina";
			this.mTankFilter_CountryChina.Size = new System.Drawing.Size(122, 22);
			this.mTankFilter_CountryChina.Text = "China";
			this.mTankFilter_CountryChina.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.mTankFilter_CountryChina.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.mTankFilter_CountryChina.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_CountryFrance
			// 
			this.mTankFilter_CountryFrance.BackColor = System.Drawing.SystemColors.Control;
			this.mTankFilter_CountryFrance.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryFrance.Image")));
			this.mTankFilter_CountryFrance.Name = "mTankFilter_CountryFrance";
			this.mTankFilter_CountryFrance.Size = new System.Drawing.Size(122, 22);
			this.mTankFilter_CountryFrance.Text = "France";
			this.mTankFilter_CountryFrance.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.mTankFilter_CountryFrance.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.mTankFilter_CountryFrance.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_CountryGermany
			// 
			this.mTankFilter_CountryGermany.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryGermany.Image")));
			this.mTankFilter_CountryGermany.Name = "mTankFilter_CountryGermany";
			this.mTankFilter_CountryGermany.Size = new System.Drawing.Size(122, 22);
			this.mTankFilter_CountryGermany.Text = "Germany";
			this.mTankFilter_CountryGermany.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.mTankFilter_CountryGermany.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.mTankFilter_CountryGermany.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_CountryUK
			// 
			this.mTankFilter_CountryUK.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryUK.Image")));
			this.mTankFilter_CountryUK.Name = "mTankFilter_CountryUK";
			this.mTankFilter_CountryUK.Size = new System.Drawing.Size(122, 22);
			this.mTankFilter_CountryUK.Text = "U.K.";
			this.mTankFilter_CountryUK.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.mTankFilter_CountryUK.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.mTankFilter_CountryUK.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_CountryUSA
			// 
			this.mTankFilter_CountryUSA.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryUSA.Image")));
			this.mTankFilter_CountryUSA.Name = "mTankFilter_CountryUSA";
			this.mTankFilter_CountryUSA.Size = new System.Drawing.Size(122, 22);
			this.mTankFilter_CountryUSA.Text = "U.S.A.";
			this.mTankFilter_CountryUSA.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.mTankFilter_CountryUSA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.mTankFilter_CountryUSA.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_CountryJapan
			// 
			this.mTankFilter_CountryJapan.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryJapan.Image")));
			this.mTankFilter_CountryJapan.Name = "mTankFilter_CountryJapan";
			this.mTankFilter_CountryJapan.Size = new System.Drawing.Size(122, 22);
			this.mTankFilter_CountryJapan.Text = "Japan";
			this.mTankFilter_CountryJapan.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.mTankFilter_CountryJapan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.mTankFilter_CountryJapan.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_CountryUSSR
			// 
			this.mTankFilter_CountryUSSR.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryUSSR.Image")));
			this.mTankFilter_CountryUSSR.Name = "mTankFilter_CountryUSSR";
			this.mTankFilter_CountryUSSR.Size = new System.Drawing.Size(122, 22);
			this.mTankFilter_CountryUSSR.Text = "U.S.S.R.";
			this.mTankFilter_CountryUSSR.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.mTankFilter_CountryUSSR.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.mTankFilter_CountryUSSR.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
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
			this.mTankFilter_Type.Size = new System.Drawing.Size(234, 22);
			this.mTankFilter_Type.Text = "Tank Type";
			// 
			// mTankFilter_TypeLT
			// 
			this.mTankFilter_TypeLT.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_TypeLT.Image")));
			this.mTankFilter_TypeLT.Name = "mTankFilter_TypeLT";
			this.mTankFilter_TypeLT.Size = new System.Drawing.Size(158, 22);
			this.mTankFilter_TypeLT.Text = "Light Tanks";
			this.mTankFilter_TypeLT.Click += new System.EventHandler(this.toolItemTankFilter_Type_Click);
			this.mTankFilter_TypeLT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Type_MouseDown);
			this.mTankFilter_TypeLT.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_TypeMT
			// 
			this.mTankFilter_TypeMT.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_TypeMT.Image")));
			this.mTankFilter_TypeMT.Name = "mTankFilter_TypeMT";
			this.mTankFilter_TypeMT.Size = new System.Drawing.Size(158, 22);
			this.mTankFilter_TypeMT.Text = "Medium Tanks";
			this.mTankFilter_TypeMT.Click += new System.EventHandler(this.toolItemTankFilter_Type_Click);
			this.mTankFilter_TypeMT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Type_MouseDown);
			this.mTankFilter_TypeMT.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_TypeHT
			// 
			this.mTankFilter_TypeHT.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_TypeHT.Image")));
			this.mTankFilter_TypeHT.Name = "mTankFilter_TypeHT";
			this.mTankFilter_TypeHT.Size = new System.Drawing.Size(158, 22);
			this.mTankFilter_TypeHT.Text = "Heavy Tanks";
			this.mTankFilter_TypeHT.Click += new System.EventHandler(this.toolItemTankFilter_Type_Click);
			this.mTankFilter_TypeHT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Type_MouseDown);
			this.mTankFilter_TypeHT.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_TypeTD
			// 
			this.mTankFilter_TypeTD.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_TypeTD.Image")));
			this.mTankFilter_TypeTD.Name = "mTankFilter_TypeTD";
			this.mTankFilter_TypeTD.Size = new System.Drawing.Size(158, 22);
			this.mTankFilter_TypeTD.Text = "Tank Destroyers";
			this.mTankFilter_TypeTD.Click += new System.EventHandler(this.toolItemTankFilter_Type_Click);
			this.mTankFilter_TypeTD.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Type_MouseDown);
			this.mTankFilter_TypeTD.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_TypeSPG
			// 
			this.mTankFilter_TypeSPG.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_TypeSPG.Image")));
			this.mTankFilter_TypeSPG.Name = "mTankFilter_TypeSPG";
			this.mTankFilter_TypeSPG.Size = new System.Drawing.Size(158, 22);
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
			this.mTankFilter_Tier.Size = new System.Drawing.Size(234, 22);
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
			// mTankFilter_Clear
			// 
			this.mTankFilter_Clear.Name = "mTankFilter_Clear";
			this.mTankFilter_Clear.Size = new System.Drawing.Size(234, 22);
			this.mTankFilter_Clear.Text = "Clear Tank Filter";
			this.mTankFilter_Clear.Click += new System.EventHandler(this.mTankFilter_Clear_Click);
			// 
			// mTankFilter_FavSeparator
			// 
			this.mTankFilter_FavSeparator.Name = "mTankFilter_FavSeparator";
			this.mTankFilter_FavSeparator.Size = new System.Drawing.Size(231, 6);
			// 
			// mTankFilter_All
			// 
			this.mTankFilter_All.BackColor = System.Drawing.Color.Transparent;
			this.mTankFilter_All.Checked = true;
			this.mTankFilter_All.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mTankFilter_All.Name = "mTankFilter_All";
			this.mTankFilter_All.Size = new System.Drawing.Size(234, 22);
			this.mTankFilter_All.Text = "All Tanks";
			this.mTankFilter_All.Click += new System.EventHandler(this.toolItemTankFilter_All_Click);
			this.mTankFilter_All.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_Fav01
			// 
			this.mTankFilter_Fav01.Name = "mTankFilter_Fav01";
			this.mTankFilter_Fav01.Size = new System.Drawing.Size(234, 22);
			this.mTankFilter_Fav01.Text = "Favourite item #1";
			this.mTankFilter_Fav01.Visible = false;
			this.mTankFilter_Fav01.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.mTankFilter_Fav01.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_Fav02
			// 
			this.mTankFilter_Fav02.Name = "mTankFilter_Fav02";
			this.mTankFilter_Fav02.Size = new System.Drawing.Size(234, 22);
			this.mTankFilter_Fav02.Text = "Favourite item #2";
			this.mTankFilter_Fav02.Visible = false;
			this.mTankFilter_Fav02.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.mTankFilter_Fav02.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_Fav03
			// 
			this.mTankFilter_Fav03.Name = "mTankFilter_Fav03";
			this.mTankFilter_Fav03.Size = new System.Drawing.Size(234, 22);
			this.mTankFilter_Fav03.Text = "Favourite item #3";
			this.mTankFilter_Fav03.Visible = false;
			this.mTankFilter_Fav03.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.mTankFilter_Fav03.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_Fav04
			// 
			this.mTankFilter_Fav04.Name = "mTankFilter_Fav04";
			this.mTankFilter_Fav04.Size = new System.Drawing.Size(234, 22);
			this.mTankFilter_Fav04.Text = "Favourite item #4";
			this.mTankFilter_Fav04.Visible = false;
			this.mTankFilter_Fav04.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.mTankFilter_Fav04.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_Fav05
			// 
			this.mTankFilter_Fav05.Name = "mTankFilter_Fav05";
			this.mTankFilter_Fav05.Size = new System.Drawing.Size(234, 22);
			this.mTankFilter_Fav05.Text = "Favourite item #5";
			this.mTankFilter_Fav05.Visible = false;
			this.mTankFilter_Fav05.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.mTankFilter_Fav05.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_Fav06
			// 
			this.mTankFilter_Fav06.Name = "mTankFilter_Fav06";
			this.mTankFilter_Fav06.Size = new System.Drawing.Size(234, 22);
			this.mTankFilter_Fav06.Text = "Favourite item #6";
			this.mTankFilter_Fav06.Visible = false;
			this.mTankFilter_Fav06.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.mTankFilter_Fav06.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_Fav07
			// 
			this.mTankFilter_Fav07.Name = "mTankFilter_Fav07";
			this.mTankFilter_Fav07.Size = new System.Drawing.Size(234, 22);
			this.mTankFilter_Fav07.Text = "Favourite item #7";
			this.mTankFilter_Fav07.Visible = false;
			this.mTankFilter_Fav07.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.mTankFilter_Fav07.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_Fav08
			// 
			this.mTankFilter_Fav08.Name = "mTankFilter_Fav08";
			this.mTankFilter_Fav08.Size = new System.Drawing.Size(234, 22);
			this.mTankFilter_Fav08.Text = "Favourite item #8";
			this.mTankFilter_Fav08.Visible = false;
			this.mTankFilter_Fav08.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.mTankFilter_Fav08.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_Fav09
			// 
			this.mTankFilter_Fav09.Name = "mTankFilter_Fav09";
			this.mTankFilter_Fav09.Size = new System.Drawing.Size(234, 22);
			this.mTankFilter_Fav09.Text = "Favourite item #9";
			this.mTankFilter_Fav09.Visible = false;
			this.mTankFilter_Fav09.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.mTankFilter_Fav09.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mTankFilter_Fav10
			// 
			this.mTankFilter_Fav10.Name = "mTankFilter_Fav10";
			this.mTankFilter_Fav10.Size = new System.Drawing.Size(234, 22);
			this.mTankFilter_Fav10.Text = "Favourite item #10";
			this.mTankFilter_Fav10.Visible = false;
			this.mTankFilter_Fav10.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.mTankFilter_Fav10.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(231, 6);
			// 
			// mTankFilter_GetInGarage
			// 
			this.mTankFilter_GetInGarage.Name = "mTankFilter_GetInGarage";
			this.mTankFilter_GetInGarage.Size = new System.Drawing.Size(234, 22);
			this.mTankFilter_GetInGarage.Text = "Update \"In Garage\" Tank List...";
			this.mTankFilter_GetInGarage.Click += new System.EventHandler(this.mTankFilter_GetInGarage_Click);
			// 
			// mTankFilter_EditFavList
			// 
			this.mTankFilter_EditFavList.Name = "mTankFilter_EditFavList";
			this.mTankFilter_EditFavList.Size = new System.Drawing.Size(234, 22);
			this.mTankFilter_EditFavList.Text = "Edit Favourite Tank List...";
			this.mTankFilter_EditFavList.Click += new System.EventHandler(this.toolItemTankFilter_EditFavList_Click);
			// 
			// mMode
			// 
			this.mMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem54,
            this.toolStripSeparator17,
            this.mModeRandomCompanyClan,
            this.mModeHistorical,
            this.mModeTeam,
            this.mModeSkrimishes,
            this.toolStripSeparator18,
            this.mModeRandom,
            this.mModeCompany,
            this.mModeClan,
            this.toolStripSeparator7,
            this.mModeSpecialInfo});
			this.mMode.Image = ((System.Drawing.Image)(resources.GetObject("mMode.Image")));
			this.mMode.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mMode.Name = "mMode";
			this.mMode.ShowDropDownArrow = false;
			this.mMode.Size = new System.Drawing.Size(80, 22);
			this.mMode.Text = "All modes";
			// 
			// toolStripMenuItem54
			// 
			this.toolStripMenuItem54.Name = "toolStripMenuItem54";
			this.toolStripMenuItem54.Size = new System.Drawing.Size(206, 22);
			this.toolStripMenuItem54.Tag = "All";
			this.toolStripMenuItem54.Text = "All modes";
			this.toolStripMenuItem54.Click += new System.EventHandler(this.toolItemMode_Click);
			this.toolStripMenuItem54.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator17
			// 
			this.toolStripSeparator17.Name = "toolStripSeparator17";
			this.toolStripSeparator17.Size = new System.Drawing.Size(203, 6);
			// 
			// mModeRandomCompanyClan
			// 
			this.mModeRandomCompanyClan.Name = "mModeRandomCompanyClan";
			this.mModeRandomCompanyClan.Size = new System.Drawing.Size(206, 22);
			this.mModeRandomCompanyClan.Tag = "Mode15";
			this.mModeRandomCompanyClan.Text = "Random, Tank Company";
			this.mModeRandomCompanyClan.Click += new System.EventHandler(this.toolItemMode_Click);
			this.mModeRandomCompanyClan.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mModeHistorical
			// 
			this.mModeHistorical.Name = "mModeHistorical";
			this.mModeHistorical.Size = new System.Drawing.Size(206, 22);
			this.mModeHistorical.Tag = "Historical";
			this.mModeHistorical.Text = "Historical";
			this.mModeHistorical.Click += new System.EventHandler(this.toolItemMode_Click);
			this.mModeHistorical.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mModeTeam
			// 
			this.mModeTeam.Name = "mModeTeam";
			this.mModeTeam.Size = new System.Drawing.Size(206, 22);
			this.mModeTeam.Tag = "Mode7";
			this.mModeTeam.Text = "Team";
			this.mModeTeam.Click += new System.EventHandler(this.toolItemMode_Click);
			this.mModeTeam.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mModeSkrimishes
			// 
			this.mModeSkrimishes.Name = "mModeSkrimishes";
			this.mModeSkrimishes.Size = new System.Drawing.Size(206, 22);
			this.mModeSkrimishes.Tag = "Skirmishes";
			this.mModeSkrimishes.Text = "Skirmishes";
			this.mModeSkrimishes.Click += new System.EventHandler(this.toolItemMode_Click);
			this.mModeSkrimishes.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator18
			// 
			this.toolStripSeparator18.Name = "toolStripSeparator18";
			this.toolStripSeparator18.Size = new System.Drawing.Size(203, 6);
			this.toolStripSeparator18.Visible = false;
			// 
			// mModeRandom
			// 
			this.mModeRandom.Name = "mModeRandom";
			this.mModeRandom.Size = new System.Drawing.Size(206, 22);
			this.mModeRandom.Tag = "Random";
			this.mModeRandom.Text = "Random *";
			this.mModeRandom.Visible = false;
			this.mModeRandom.Click += new System.EventHandler(this.toolItemMode_Click);
			this.mModeRandom.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mModeCompany
			// 
			this.mModeCompany.Name = "mModeCompany";
			this.mModeCompany.Size = new System.Drawing.Size(206, 22);
			this.mModeCompany.Tag = "Company";
			this.mModeCompany.Text = "Tank Company *";
			this.mModeCompany.Visible = false;
			this.mModeCompany.Click += new System.EventHandler(this.toolItemMode_Click);
			this.mModeCompany.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mModeClan
			// 
			this.mModeClan.Name = "mModeClan";
			this.mModeClan.Size = new System.Drawing.Size(206, 22);
			this.mModeClan.Tag = "Clan";
			this.mModeClan.Text = "Clan War *";
			this.mModeClan.Visible = false;
			this.mModeClan.Click += new System.EventHandler(this.toolItemMode_Click);
			this.mModeClan.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(203, 6);
			this.toolStripSeparator7.Visible = false;
			// 
			// mModeSpecialInfo
			// 
			this.mModeSpecialInfo.Name = "mModeSpecialInfo";
			this.mModeSpecialInfo.Size = new System.Drawing.Size(206, 22);
			this.mModeSpecialInfo.Text = "* Information";
			this.mModeSpecialInfo.Visible = false;
			this.mModeSpecialInfo.Click += new System.EventHandler(this.toolItemModeSpecialInfo_Click);
			// 
			// mBattles
			// 
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
            this.mBattlesAll});
			this.mBattles.Image = ((System.Drawing.Image)(resources.GetObject("mBattles.Image")));
			this.mBattles.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mBattles.Name = "mBattles";
			this.mBattles.ShowDropDownArrow = false;
			this.mBattles.Size = new System.Drawing.Size(106, 22);
			this.mBattles.Text = "Today\'s Battles";
			// 
			// mBattles1d
			// 
			this.mBattles1d.Checked = true;
			this.mBattles1d.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mBattles1d.Name = "mBattles1d";
			this.mBattles1d.Size = new System.Drawing.Size(186, 22);
			this.mBattles1d.Text = "Today\'s Battles";
			this.mBattles1d.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.mBattles1d.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mBattlesYesterday
			// 
			this.mBattlesYesterday.Name = "mBattlesYesterday";
			this.mBattlesYesterday.Size = new System.Drawing.Size(186, 22);
			this.mBattlesYesterday.Text = "Yesterday\'s Battles";
			this.mBattlesYesterday.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.mBattlesYesterday.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(183, 6);
			// 
			// mBattles2d
			// 
			this.mBattles2d.Name = "mBattles2d";
			this.mBattles2d.Size = new System.Drawing.Size(186, 22);
			this.mBattles2d.Text = "Battles Last 2 Days";
			this.mBattles2d.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.mBattles2d.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mBattles3d
			// 
			this.mBattles3d.Name = "mBattles3d";
			this.mBattles3d.Size = new System.Drawing.Size(186, 22);
			this.mBattles3d.Text = "Battles Last 3 Days";
			this.mBattles3d.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.mBattles3d.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator19
			// 
			this.toolStripSeparator19.Name = "toolStripSeparator19";
			this.toolStripSeparator19.Size = new System.Drawing.Size(183, 6);
			// 
			// mBattles1w
			// 
			this.mBattles1w.Name = "mBattles1w";
			this.mBattles1w.Size = new System.Drawing.Size(186, 22);
			this.mBattles1w.Text = "Battles Last Week";
			this.mBattles1w.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.mBattles1w.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mBattles2w
			// 
			this.mBattles2w.Name = "mBattles2w";
			this.mBattles2w.Size = new System.Drawing.Size(186, 22);
			this.mBattles2w.Text = "Battles Last 2 Weeks";
			this.mBattles2w.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.mBattles2w.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator20
			// 
			this.toolStripSeparator20.Name = "toolStripSeparator20";
			this.toolStripSeparator20.Size = new System.Drawing.Size(183, 6);
			// 
			// mBattles1m
			// 
			this.mBattles1m.Name = "mBattles1m";
			this.mBattles1m.Size = new System.Drawing.Size(186, 22);
			this.mBattles1m.Text = "Battles Last Month";
			this.mBattles1m.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.mBattles1m.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mBattles3m
			// 
			this.mBattles3m.Name = "mBattles3m";
			this.mBattles3m.Size = new System.Drawing.Size(186, 22);
			this.mBattles3m.Text = "Battles Last 3 Months";
			this.mBattles3m.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.mBattles3m.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mBattles6m
			// 
			this.mBattles6m.Name = "mBattles6m";
			this.mBattles6m.Size = new System.Drawing.Size(186, 22);
			this.mBattles6m.Text = "Battles Last 6 Months";
			this.mBattles6m.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.mBattles6m.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator21
			// 
			this.toolStripSeparator21.Name = "toolStripSeparator21";
			this.toolStripSeparator21.Size = new System.Drawing.Size(183, 6);
			// 
			// mBattles1y
			// 
			this.mBattles1y.Name = "mBattles1y";
			this.mBattles1y.Size = new System.Drawing.Size(186, 22);
			this.mBattles1y.Text = "Battles Last Year";
			this.mBattles1y.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.mBattles1y.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mBattles2y
			// 
			this.mBattles2y.Name = "mBattles2y";
			this.mBattles2y.Size = new System.Drawing.Size(186, 22);
			this.mBattles2y.Text = "Battles Last 2 Years";
			this.mBattles2y.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.mBattles2y.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator22
			// 
			this.toolStripSeparator22.Name = "toolStripSeparator22";
			this.toolStripSeparator22.Size = new System.Drawing.Size(183, 6);
			// 
			// mBattlesAll
			// 
			this.mBattlesAll.Name = "mBattlesAll";
			this.mBattlesAll.Size = new System.Drawing.Size(186, 22);
			this.mBattlesAll.Text = "All Battles";
			this.mBattlesAll.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			// 
			// mBattleGroup
			// 
			this.mBattleGroup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mBattleGroup_No,
            this.toolStripSeparator6,
            this.mBattleGroup_TankAverage,
            this.mBattleGroup_TankSum});
			this.mBattleGroup.Image = ((System.Drawing.Image)(resources.GetObject("mBattleGroup.Image")));
			this.mBattleGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mBattleGroup.Name = "mBattleGroup";
			this.mBattleGroup.ShowDropDownArrow = false;
			this.mBattleGroup.Size = new System.Drawing.Size(96, 22);
			this.mBattleGroup.Text = "No Grouping";
			// 
			// mBattleGroup_No
			// 
			this.mBattleGroup_No.Checked = true;
			this.mBattleGroup_No.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mBattleGroup_No.Name = "mBattleGroup_No";
			this.mBattleGroup_No.Size = new System.Drawing.Size(206, 22);
			this.mBattleGroup_No.Text = "No Grouping";
			this.mBattleGroup_No.Click += new System.EventHandler(this.toolItemGroupingSelected_Click);
			this.mBattleGroup_No.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(203, 6);
			// 
			// mBattleGroup_TankAverage
			// 
			this.mBattleGroup_TankAverage.Name = "mBattleGroup_TankAverage";
			this.mBattleGroup_TankAverage.Size = new System.Drawing.Size(206, 22);
			this.mBattleGroup_TankAverage.Text = "Group by Tank - Average";
			this.mBattleGroup_TankAverage.Click += new System.EventHandler(this.toolItemGroupingSelected_Click);
			this.mBattleGroup_TankAverage.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mBattleGroup_TankSum
			// 
			this.mBattleGroup_TankSum.Name = "mBattleGroup_TankSum";
			this.mBattleGroup_TankSum.Size = new System.Drawing.Size(206, 22);
			this.mBattleGroup_TankSum.Text = "Group by Tank - Sum";
			this.mBattleGroup_TankSum.Click += new System.EventHandler(this.toolItemGroupingSelected_Click);
			this.mBattleGroup_TankSum.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mGadget
			// 
			this.mGadget.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gaugesToolStripMenuItem,
            this.mGadgetAddBattleModeStats,
            this.mGadgetAddTankTypeStats,
            this.mGadgetAddImage,
            this.toolStripSeparator4,
            this.mGadgetEdit});
			this.mGadget.Image = ((System.Drawing.Image)(resources.GetObject("mGadget.Image")));
			this.mGadget.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mGadget.Name = "mGadget";
			this.mGadget.ShowDropDownArrow = false;
			this.mGadget.Size = new System.Drawing.Size(70, 22);
			this.mGadget.Text = "Gadgets";
			this.mGadget.ToolTipText = "Gadget";
			// 
			// gaugesToolStripMenuItem
			// 
			this.gaugesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.winRateToolStripMenuItem,
            this.wN8RatingToolStripMenuItem,
            this.wN7RatingToolStripMenuItem,
            this.efficiencyRatingToolStripMenuItem});
			this.gaugesToolStripMenuItem.Name = "gaugesToolStripMenuItem";
			this.gaugesToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
			this.gaugesToolStripMenuItem.Text = "Gauges";
			this.gaugesToolStripMenuItem.Click += new System.EventHandler(this.mGadgetNotImplemented);
			// 
			// winRateToolStripMenuItem
			// 
			this.winRateToolStripMenuItem.Name = "winRateToolStripMenuItem";
			this.winRateToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.winRateToolStripMenuItem.Text = "Win Rate";
			this.winRateToolStripMenuItem.Click += new System.EventHandler(this.mGadgetNotImplemented);
			// 
			// wN8RatingToolStripMenuItem
			// 
			this.wN8RatingToolStripMenuItem.Name = "wN8RatingToolStripMenuItem";
			this.wN8RatingToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.wN8RatingToolStripMenuItem.Text = "WN8 Rating";
			this.wN8RatingToolStripMenuItem.Click += new System.EventHandler(this.mGadgetNotImplemented);
			// 
			// wN7RatingToolStripMenuItem
			// 
			this.wN7RatingToolStripMenuItem.Name = "wN7RatingToolStripMenuItem";
			this.wN7RatingToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.wN7RatingToolStripMenuItem.Text = "WN7 Rating";
			this.wN7RatingToolStripMenuItem.Click += new System.EventHandler(this.mGadgetNotImplemented);
			// 
			// efficiencyRatingToolStripMenuItem
			// 
			this.efficiencyRatingToolStripMenuItem.Name = "efficiencyRatingToolStripMenuItem";
			this.efficiencyRatingToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.efficiencyRatingToolStripMenuItem.Text = "Efficiency Rating";
			this.efficiencyRatingToolStripMenuItem.Click += new System.EventHandler(this.mGadgetNotImplemented);
			// 
			// mGadgetAddBattleModeStats
			// 
			this.mGadgetAddBattleModeStats.Name = "mGadgetAddBattleModeStats";
			this.mGadgetAddBattleModeStats.Size = new System.Drawing.Size(236, 22);
			this.mGadgetAddBattleModeStats.Text = "Battle Mode Stats";
			this.mGadgetAddBattleModeStats.Click += new System.EventHandler(this.mGadgetNotImplemented);
			// 
			// mGadgetAddTankTypeStats
			// 
			this.mGadgetAddTankTypeStats.Name = "mGadgetAddTankTypeStats";
			this.mGadgetAddTankTypeStats.Size = new System.Drawing.Size(236, 22);
			this.mGadgetAddTankTypeStats.Text = "Tank Type Stats";
			this.mGadgetAddTankTypeStats.Click += new System.EventHandler(this.mGadgetNotImplemented);
			// 
			// mGadgetAddImage
			// 
			this.mGadgetAddImage.Name = "mGadgetAddImage";
			this.mGadgetAddImage.Size = new System.Drawing.Size(236, 22);
			this.mGadgetAddImage.Text = "Last Battle Result Large Images";
			this.mGadgetAddImage.Click += new System.EventHandler(this.mGadgetNotImplemented);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(233, 6);
			// 
			// mGadgetEdit
			// 
			this.mGadgetEdit.Name = "mGadgetEdit";
			this.mGadgetEdit.Size = new System.Drawing.Size(236, 22);
			this.mGadgetEdit.Text = "Edit Home View Gadgets...";
			this.mGadgetEdit.Click += new System.EventHandler(this.mGadgetNotImplemented);
			// 
			// mHomeEdit
			// 
			this.mHomeEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.mHomeEdit.Image = ((System.Drawing.Image)(resources.GetObject("mHomeEdit.Image")));
			this.mHomeEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mHomeEdit.Name = "mHomeEdit";
			this.mHomeEdit.Size = new System.Drawing.Size(23, 22);
			this.mHomeEdit.Text = "toolStripButton1";
			this.mHomeEdit.Click += new System.EventHandler(this.mHomeEdit_Click);
			// 
			// toolStripSeparator23
			// 
			this.toolStripSeparator23.Name = "toolStripSeparator23";
			this.toolStripSeparator23.Size = new System.Drawing.Size(6, 25);
			// 
			// mViewChart
			// 
			this.mViewChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.mViewChart.Image = ((System.Drawing.Image)(resources.GetObject("mViewChart.Image")));
			this.mViewChart.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mViewChart.Name = "mViewChart";
			this.mViewChart.Size = new System.Drawing.Size(23, 22);
			this.mViewChart.Text = "toolStripButton1";
			this.mViewChart.Click += new System.EventHandler(this.toolItemViewChart_Click);
			// 
			// mSettings
			// 
			this.mSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.mSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSettingsRun,
            this.mSettingsDossierOptions,
            this.toolStripSeparator24,
            this.mUpdateDataFromAPI,
            this.mImportBattlesFromWotStat,
            this.mShowDbTables,
            this.toolStripSeparator25,
            this.mSettingsAppLayout,
            this.mSettingsApp});
			this.mSettings.Image = ((System.Drawing.Image)(resources.GetObject("mSettings.Image")));
			this.mSettings.Name = "mSettings";
			this.mSettings.ShowDropDownArrow = false;
			this.mSettings.Size = new System.Drawing.Size(20, 22);
			this.mSettings.Text = "Settings";
			// 
			// mSettingsRun
			// 
			this.mSettingsRun.Name = "mSettingsRun";
			this.mSettingsRun.Size = new System.Drawing.Size(263, 22);
			this.mSettingsRun.Text = "Automatically Fetch New Battles";
			this.mSettingsRun.Click += new System.EventHandler(this.toolItemSettingsRun_Click);
			this.mSettingsRun.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// mSettingsDossierOptions
			// 
			this.mSettingsDossierOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSettingsRunManual,
            this.mSettingsForceUpdateFromPrev,
            this.toolStripSeparator1,
            this.mSettingsTestAddBattleResult});
			this.mSettingsDossierOptions.Name = "mSettingsDossierOptions";
			this.mSettingsDossierOptions.Size = new System.Drawing.Size(263, 22);
			this.mSettingsDossierOptions.Text = "Manual Check for New Battles";
			// 
			// mSettingsRunManual
			// 
			this.mSettingsRunManual.Name = "mSettingsRunManual";
			this.mSettingsRunManual.Size = new System.Drawing.Size(286, 22);
			this.mSettingsRunManual.Text = "Normal Dossier File Check";
			this.mSettingsRunManual.Click += new System.EventHandler(this.toolItemSettingsRunManual_Click);
			// 
			// mSettingsForceUpdateFromPrev
			// 
			this.mSettingsForceUpdateFromPrev.Name = "mSettingsForceUpdateFromPrev";
			this.mSettingsForceUpdateFromPrev.Size = new System.Drawing.Size(286, 22);
			this.mSettingsForceUpdateFromPrev.Text = "Force Update All Data Dossier File Check";
			this.mSettingsForceUpdateFromPrev.Click += new System.EventHandler(this.toolItemSettingsForceUpdateFromPrev_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(283, 6);
			// 
			// mSettingsTestAddBattleResult
			// 
			this.mSettingsTestAddBattleResult.Name = "mSettingsTestAddBattleResult";
			this.mSettingsTestAddBattleResult.Size = new System.Drawing.Size(286, 22);
			this.mSettingsTestAddBattleResult.Text = "Check for Battle Result";
			this.mSettingsTestAddBattleResult.Click += new System.EventHandler(this.mSettingsTestAddBattleResult_Click);
			// 
			// toolStripSeparator24
			// 
			this.toolStripSeparator24.Name = "toolStripSeparator24";
			this.toolStripSeparator24.Size = new System.Drawing.Size(260, 6);
			// 
			// mUpdateDataFromAPI
			// 
			this.mUpdateDataFromAPI.Name = "mUpdateDataFromAPI";
			this.mUpdateDataFromAPI.Size = new System.Drawing.Size(263, 22);
			this.mUpdateDataFromAPI.Text = "Update Data from API...";
			this.mUpdateDataFromAPI.Click += new System.EventHandler(this.toolItemUpdateDataFromAPI_Click);
			// 
			// mImportBattlesFromWotStat
			// 
			this.mImportBattlesFromWotStat.Name = "mImportBattlesFromWotStat";
			this.mImportBattlesFromWotStat.Size = new System.Drawing.Size(263, 22);
			this.mImportBattlesFromWotStat.Text = "Import battles from WoT Statistics...";
			this.mImportBattlesFromWotStat.Click += new System.EventHandler(this.toolItemImportBattlesFromWotStat_Click);
			// 
			// mShowDbTables
			// 
			this.mShowDbTables.Name = "mShowDbTables";
			this.mShowDbTables.Size = new System.Drawing.Size(263, 22);
			this.mShowDbTables.Text = "Show Database Tables...";
			this.mShowDbTables.Click += new System.EventHandler(this.toolItemShowDbTables_Click);
			// 
			// toolStripSeparator25
			// 
			this.toolStripSeparator25.Name = "toolStripSeparator25";
			this.toolStripSeparator25.Size = new System.Drawing.Size(260, 6);
			// 
			// mSettingsAppLayout
			// 
			this.mSettingsAppLayout.Name = "mSettingsAppLayout";
			this.mSettingsAppLayout.Size = new System.Drawing.Size(263, 22);
			this.mSettingsAppLayout.Text = "Application &Layout...";
			this.mSettingsAppLayout.Click += new System.EventHandler(this.mSettingsAppLayout_Click);
			// 
			// mSettingsApp
			// 
			this.mSettingsApp.Name = "mSettingsApp";
			this.mSettingsApp.Size = new System.Drawing.Size(263, 22);
			this.mSettingsApp.Text = "Application &Settings...";
			this.mSettingsApp.Click += new System.EventHandler(this.toolItemSettingsApp_Click);
			// 
			// mHelp
			// 
			this.mHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.mHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mHelpCheckVersion,
            this.mHelpMessage,
            this.toolStripSeparator2,
            this.mHelpAbout});
			this.mHelp.Image = ((System.Drawing.Image)(resources.GetObject("mHelp.Image")));
			this.mHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mHelp.Name = "mHelp";
			this.mHelp.ShowDropDownArrow = false;
			this.mHelp.Size = new System.Drawing.Size(20, 22);
			this.mHelp.Text = "toolStripDropDownButton1";
			// 
			// mHelpCheckVersion
			// 
			this.mHelpCheckVersion.Name = "mHelpCheckVersion";
			this.mHelpCheckVersion.Size = new System.Drawing.Size(287, 22);
			this.mHelpCheckVersion.Text = "Check for new version";
			this.mHelpCheckVersion.Click += new System.EventHandler(this.mHelpCheckVersion_Click);
			// 
			// mHelpMessage
			// 
			this.mHelpMessage.Name = "mHelpMessage";
			this.mHelpMessage.Size = new System.Drawing.Size(287, 22);
			this.mHelpMessage.Text = "View message from Wot Numbers Team";
			this.mHelpMessage.Click += new System.EventHandler(this.mHelpMessage_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(284, 6);
			// 
			// mHelpAbout
			// 
			this.mHelpAbout.Name = "mHelpAbout";
			this.mHelpAbout.Size = new System.Drawing.Size(287, 22);
			this.mHelpAbout.Text = "About Wot Numbers...";
			this.mHelpAbout.Click += new System.EventHandler(this.mHelpAbout_Click);
			// 
			// lblStatusRowCount
			// 
			this.lblStatusRowCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblStatusRowCount.BackColor = System.Drawing.Color.Transparent;
			this.lblStatusRowCount.ForeColor = System.Drawing.Color.DarkGray;
			this.lblStatusRowCount.Location = new System.Drawing.Point(783, 411);
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
			this.panelMainArea.Controls.Add(this.dataGridMain);
			this.panelMainArea.Controls.Add(this.panelInfo);
			this.panelMainArea.Controls.Add(this.scrollY);
			this.panelMainArea.Controls.Add(this.scrollCorner);
			this.panelMainArea.Controls.Add(this.scrollX);
			this.panelMainArea.Location = new System.Drawing.Point(9, 57);
			this.panelMainArea.Name = "panelMainArea";
			this.panelMainArea.Size = new System.Drawing.Size(649, 336);
			this.panelMainArea.TabIndex = 18;
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
			this.dataGridMain.Location = new System.Drawing.Point(14, 88);
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
			this.dataGridMain.Size = new System.Drawing.Size(601, 204);
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
			// panelInfo
			// 
			this.panelInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
			this.panelInfo.Controls.Add(this.lblOverView);
			this.panelInfo.Controls.Add(this.picIS7);
			this.panelInfo.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.panelInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelInfo.Location = new System.Drawing.Point(0, 0);
			this.panelInfo.Name = "panelInfo";
			this.panelInfo.Size = new System.Drawing.Size(649, 72);
			this.panelInfo.TabIndex = 18;
			this.panelInfo.Visible = false;
			// 
			// lblOverView
			// 
			this.lblOverView.AutoSize = true;
			this.lblOverView.BackColor = System.Drawing.Color.Transparent;
			this.lblOverView.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.lblOverView.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblOverView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(96)))), ((int)(((byte)(127)))));
			this.lblOverView.Location = new System.Drawing.Point(16, 34);
			this.lblOverView.Name = "lblOverView";
			this.lblOverView.Size = new System.Drawing.Size(123, 29);
			this.lblOverView.TabIndex = 0;
			this.lblOverView.Text = "Welcome...";
			// 
			// picIS7
			// 
			this.picIS7.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.picIS7.Dock = System.Windows.Forms.DockStyle.Right;
			this.picIS7.Image = ((System.Drawing.Image)(resources.GetObject("picIS7.Image")));
			this.picIS7.Location = new System.Drawing.Point(193, 0);
			this.picIS7.Name = "picIS7";
			this.picIS7.Size = new System.Drawing.Size(456, 72);
			this.picIS7.TabIndex = 17;
			this.picIS7.TabStop = false;
			// 
			// scrollY
			// 
			this.scrollY.BackColor = System.Drawing.Color.Transparent;
			this.scrollY.Image = null;
			this.scrollY.Location = new System.Drawing.Point(621, 88);
			this.scrollY.Name = "scrollY";
			this.scrollY.ScrollElementsTotals = 100;
			this.scrollY.ScrollElementsVisible = 0;
			this.scrollY.ScrollHide = true;
			this.scrollY.ScrollNecessary = true;
			this.scrollY.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.scrollY.ScrollPosition = 0;
			this.scrollY.Size = new System.Drawing.Size(17, 204);
			this.scrollY.TabIndex = 21;
			this.scrollY.Text = "badScrollBar2";
			this.scrollY.Visible = false;
			this.scrollY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollY_MouseDown);
			this.scrollY.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollY_MouseMove);
			this.scrollY.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scrollX_MouseUp);
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
			this.lblStatus2.Location = new System.Drawing.Point(69, 411);
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
			this.lblStatus1.Location = new System.Drawing.Point(13, 411);
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
			this.ClientSize = new System.Drawing.Size(872, 431);
			this.Controls.Add(this.MainTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(450, 250);
			this.Name = "Main";
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
			this.panelInfo.ResumeLayout(false);
			this.panelInfo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picIS7)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer timerStatus2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.Label lblOverView;
		private System.Windows.Forms.PictureBox picIS7;
		private System.Windows.Forms.Timer timerPanelSlide;
		private System.IO.FileSystemWatcher fileSystemWatcherNewBattle;
		private BadForm MainTheme;
		private System.Windows.Forms.Label lblStatus2;
		private System.Windows.Forms.Label lblStatus1;
		private System.Windows.Forms.Panel panelMainArea;
		private System.Windows.Forms.Panel panelInfo;
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
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem54;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
		private System.Windows.Forms.ToolStripMenuItem mModeRandomCompanyClan;
		private System.Windows.Forms.ToolStripMenuItem mModeTeam;
		private System.Windows.Forms.ToolStripMenuItem mModeHistorical;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
		private System.Windows.Forms.ToolStripMenuItem mModeRandom;
		private System.Windows.Forms.ToolStripMenuItem mModeCompany;
		private System.Windows.Forms.ToolStripMenuItem mModeClan;
		private System.Windows.Forms.ToolStripMenuItem mModeSpecialInfo;
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
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
		private System.Windows.Forms.ToolStripDropDownButton mSettings;
		private System.Windows.Forms.ToolStripMenuItem mSettingsRun;
		private System.Windows.Forms.ToolStripMenuItem mSettingsDossierOptions;
		private System.Windows.Forms.ToolStripMenuItem mSettingsRunManual;
		private System.Windows.Forms.ToolStripMenuItem mSettingsForceUpdateFromPrev;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
		private System.Windows.Forms.ToolStripMenuItem mUpdateDataFromAPI;
		private System.Windows.Forms.ToolStripMenuItem mImportBattlesFromWotStat;
		private System.Windows.Forms.ToolStripMenuItem mShowDbTables;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator25;
		private System.Windows.Forms.ToolStripMenuItem mSettingsApp;
		private System.Windows.Forms.ToolStripButton mViewChart;
		private System.Windows.Forms.ToolStripMenuItem mSettingsTestAddBattleResult;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_GetInGarage;
		private System.Windows.Forms.ToolStripDropDownButton mHelp;
		private System.Windows.Forms.ToolStripMenuItem mHelpCheckVersion;
		private System.Windows.Forms.ToolStripMenuItem mHelpMessage;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem mHelpAbout;
		private System.Windows.Forms.ToolStripMenuItem mTankFilter_Clear;
		private System.Windows.Forms.ToolStripMenuItem mSettingsAppLayout;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem mModeSkrimishes;
		private System.Windows.Forms.ToolStripMenuItem mBattles2d;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripDropDownButton mGadget;
		private System.Windows.Forms.ToolStripMenuItem mGadgetAddBattleModeStats;
		private System.Windows.Forms.ToolStripMenuItem mGadgetAddImage;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem mGadgetEdit;
		private System.Windows.Forms.ToolStripMenuItem mGadgetAddTankTypeStats;
		private System.Windows.Forms.ToolStripDropDownButton mBattleGroup;
		private System.Windows.Forms.ToolStripMenuItem mBattleGroup_No;
		private System.Windows.Forms.ToolStripMenuItem mBattleGroup_TankAverage;
		private System.Windows.Forms.ToolStripMenuItem mBattleGroup_TankSum;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem gaugesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem winRateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem wN8RatingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem wN7RatingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem efficiencyRatingToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton mHomeEdit;
	}
}

