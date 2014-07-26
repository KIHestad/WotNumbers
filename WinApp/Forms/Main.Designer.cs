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
			this.toolStripEx1 = new WinApp.Code.ToolStripEx();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.mViewOverall = new System.Windows.Forms.ToolStripButton();
			this.mViewTankInfo = new System.Windows.Forms.ToolStripButton();
			this.mViewBattles = new System.Windows.Forms.ToolStripButton();
			this.mRefresh = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
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
			this.mTankFilter_All = new System.Windows.Forms.ToolStripMenuItem();
			this.mTankFilter_Country = new System.Windows.Forms.ToolStripMenuItem();
			this.mTankFilter_CountryChina = new System.Windows.Forms.ToolStripMenuItem();
			this.mTankFilter_CountryFrance = new System.Windows.Forms.ToolStripMenuItem();
			this.mTankFilter_CountryGermany = new System.Windows.Forms.ToolStripMenuItem();
			this.mTankFilter_CountryUK = new System.Windows.Forms.ToolStripMenuItem();
			this.mTankFilter_CountryUSA = new System.Windows.Forms.ToolStripMenuItem();
			this.mTankFilter_CountryUSSR = new System.Windows.Forms.ToolStripMenuItem();
			this.mTankFilter_CountryJapan = new System.Windows.Forms.ToolStripMenuItem();
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
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
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
			this.mTankFilter_EditFavList = new System.Windows.Forms.ToolStripMenuItem();
			this.mMode = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripMenuItem54 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
			this.mModeRandomCompanyClan = new System.Windows.Forms.ToolStripMenuItem();
			this.mModeTeam = new System.Windows.Forms.ToolStripMenuItem();
			this.mModeHistorical = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
			this.mModeRandom = new System.Windows.Forms.ToolStripMenuItem();
			this.mModeCompany = new System.Windows.Forms.ToolStripMenuItem();
			this.mModeClan = new System.Windows.Forms.ToolStripMenuItem();
			this.mModeSpecialInfo = new System.Windows.Forms.ToolStripMenuItem();
			this.mBattles = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripMenuItem62 = new System.Windows.Forms.ToolStripMenuItem();
			this.mBattlesYesterday = new System.Windows.Forms.ToolStripMenuItem();
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
			this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
			this.mSettings = new System.Windows.Forms.ToolStripDropDownButton();
			this.mSettingsRun = new System.Windows.Forms.ToolStripMenuItem();
			this.mSettingsDossierOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.mSettingsRunManual = new System.Windows.Forms.ToolStripMenuItem();
			this.mSettingsForceUpdateFromPrev = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
			this.mUpdateDataFromAPI = new System.Windows.Forms.ToolStripMenuItem();
			this.mImportBattlesFromWotStat = new System.Windows.Forms.ToolStripMenuItem();
			this.mShowDbTables = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
			this.mSettingsApp = new System.Windows.Forms.ToolStripMenuItem();
			this.mViewChart = new System.Windows.Forms.ToolStripButton();
			this.mHelp = new System.Windows.Forms.ToolStripButton();
			this.lblStatusRowCount = new System.Windows.Forms.Label();
			this.panelMainArea = new System.Windows.Forms.Panel();
			this.dataGridMain = new System.Windows.Forms.DataGridView();
			this.panelInfo = new System.Windows.Forms.Panel();
			this.lblOverView = new System.Windows.Forms.Label();
			this.picIS7 = new System.Windows.Forms.PictureBox();
			this.scrollY = new BadScrollBar();
			this.scrollCorner = new BadScrollBarCorner();
			this.scrollX = new BadScrollBar();
			this.toolMain = new System.Windows.Forms.ToolStrip();
			this.toolItemViewLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolItemViewOverall = new System.Windows.Forms.ToolStripButton();
			this.toolItemViewTankInfo = new System.Windows.Forms.ToolStripButton();
			this.toolItemViewBattles = new System.Windows.Forms.ToolStripButton();
			this.toolItemRefresh = new System.Windows.Forms.ToolStripButton();
			this.toolItemRefreshSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemColumnSelect = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolItemColumnSelect_01 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemColumnSelect_02 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemColumnSelect_03 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemColumnSelect_04 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemColumnSelect_05 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemColumnSelect_06 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemColumnSelect_07 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemColumnSelect_08 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemColumnSelect_09 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemColumnSelect_10 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemColumnSelect_11 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemColumnSelect_12 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemColumnSelect_13 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemColumnSelect_14 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemColumnSelect_15 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemColumnSelect_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolItemTankFilter_All = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Country = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_CountryChina = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_CountryFrance = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_CountryGermany = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_CountryUK = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_CountryUSA = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_CountryUSSR = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_CountryJapan = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Type = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_TypeLT = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_TypeMT = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_TypeHT = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_TypeTD = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_TypeSPG = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Tier = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Tier1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Tier2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Tier3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Tier4 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Tier5 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Tier6 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Tier7 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Tier8 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Tier9 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Tier10 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_FavSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemTankFilter_Fav01 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Fav02 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Fav03 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Fav04 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Fav05 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Fav06 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Fav07 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Fav08 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Fav09 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTankFilter_Fav10 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemTankFilter_EditFavList = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemMode = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolItemModeAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemModeRandomCompanyClan = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemModeTeam = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemModeHistorical = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemModeRandom = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemModeCompany = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemModeClan = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemModeSpecialInfo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemBattles = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolItemBattles1d = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemBattlesYesterday = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemBattles3d = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemBattles1w = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemBattles2w = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemBattles1m = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemBattles3m = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemBattles6m = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemBattles1y = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemBattles2y = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemBattlesAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemSettings = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolItemSettingsRun = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemSettingsDossierOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemSettingsRunManual = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemSettingsForceUpdateFromPrev = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemUpdateDataFromAPI = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemImportBattlesFromWotStat = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemShowDbTables = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemSettingsApp = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemViewChart = new System.Windows.Forms.ToolStripButton();
			this.toolItemHelp = new System.Windows.Forms.ToolStripButton();
			this.lblStatus2 = new System.Windows.Forms.Label();
			this.lblStatus1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherNewBattle)).BeginInit();
			this.MainTheme.SuspendLayout();
			this.toolStripEx1.SuspendLayout();
			this.panelMainArea.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridMain)).BeginInit();
			this.panelInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picIS7)).BeginInit();
			this.toolMain.SuspendLayout();
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
			// 
			// MainTheme
			// 
			this.MainTheme.BackColor = System.Drawing.Color.Fuchsia;
			this.MainTheme.Controls.Add(this.toolStripEx1);
			this.MainTheme.Controls.Add(this.lblStatusRowCount);
			this.MainTheme.Controls.Add(this.panelMainArea);
			this.MainTheme.Controls.Add(this.toolMain);
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
			this.MainTheme.Size = new System.Drawing.Size(812, 431);
			this.MainTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("MainTheme.SystemExitImage")));
			this.MainTheme.SystemMaximizeImage = ((System.Drawing.Image)(resources.GetObject("MainTheme.SystemMaximizeImage")));
			this.MainTheme.SystemMinimizeImage = ((System.Drawing.Image)(resources.GetObject("MainTheme.SystemMinimizeImage")));
			this.MainTheme.TabIndex = 18;
			this.MainTheme.Text = "Wot Numbers";
			this.MainTheme.TitleHeight = 53;
			// 
			// toolStripEx1
			// 
			this.toolStripEx1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStripEx1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.mViewOverall,
            this.mViewTankInfo,
            this.mViewBattles,
            this.mRefresh,
            this.toolStripSeparator1,
            this.mColumnSelect,
            this.mTankFilter,
            this.mMode,
            this.mBattles,
            this.toolStripSeparator23,
            this.mSettings,
            this.mViewChart,
            this.mHelp});
			this.toolStripEx1.Location = new System.Drawing.Point(47, 4);
			this.toolStripEx1.Name = "toolStripEx1";
			this.toolStripEx1.Size = new System.Drawing.Size(676, 25);
			this.toolStripEx1.Stretch = true;
			this.toolStripEx1.TabIndex = 18;
			this.toolStripEx1.Text = "toolStripEx1";
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(35, 22);
			this.toolStripLabel1.Text = "View:";
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
			// 
			// mViewTankInfo
			// 
			this.mViewTankInfo.Image = ((System.Drawing.Image)(resources.GetObject("mViewTankInfo.Image")));
			this.mViewTankInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mViewTankInfo.Name = "mViewTankInfo";
			this.mViewTankInfo.Size = new System.Drawing.Size(58, 22);
			this.mViewTankInfo.Text = "&Tanks";
			// 
			// mViewBattles
			// 
			this.mViewBattles.Image = ((System.Drawing.Image)(resources.GetObject("mViewBattles.Image")));
			this.mViewBattles.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mViewBattles.Name = "mViewBattles";
			this.mViewBattles.Size = new System.Drawing.Size(62, 22);
			this.mViewBattles.Text = "&Battles";
			// 
			// mRefresh
			// 
			this.mRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.mRefresh.Image = ((System.Drawing.Image)(resources.GetObject("mRefresh.Image")));
			this.mRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mRefresh.Name = "mRefresh";
			this.mRefresh.Size = new System.Drawing.Size(23, 22);
			this.mRefresh.Text = "Refresh grid";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
			this.mColumnSelect_01.Size = new System.Drawing.Size(182, 22);
			this.mColumnSelect_01.Text = "Col List #1";
			// 
			// mColumnSelect_02
			// 
			this.mColumnSelect_02.Name = "mColumnSelect_02";
			this.mColumnSelect_02.Size = new System.Drawing.Size(182, 22);
			this.mColumnSelect_02.Text = "Col List #2";
			// 
			// mColumnSelect_03
			// 
			this.mColumnSelect_03.Name = "mColumnSelect_03";
			this.mColumnSelect_03.Size = new System.Drawing.Size(182, 22);
			this.mColumnSelect_03.Text = "Col List #3";
			// 
			// mColumnSelect_04
			// 
			this.mColumnSelect_04.Name = "mColumnSelect_04";
			this.mColumnSelect_04.Size = new System.Drawing.Size(182, 22);
			this.mColumnSelect_04.Text = "Col List #4";
			this.mColumnSelect_04.Visible = false;
			// 
			// mColumnSelect_05
			// 
			this.mColumnSelect_05.Name = "mColumnSelect_05";
			this.mColumnSelect_05.Size = new System.Drawing.Size(182, 22);
			this.mColumnSelect_05.Text = "Col List #5";
			this.mColumnSelect_05.Visible = false;
			// 
			// mColumnSelect_06
			// 
			this.mColumnSelect_06.Name = "mColumnSelect_06";
			this.mColumnSelect_06.Size = new System.Drawing.Size(182, 22);
			this.mColumnSelect_06.Text = "Col List #6";
			this.mColumnSelect_06.Visible = false;
			// 
			// mColumnSelect_07
			// 
			this.mColumnSelect_07.Name = "mColumnSelect_07";
			this.mColumnSelect_07.Size = new System.Drawing.Size(182, 22);
			this.mColumnSelect_07.Text = "Col List #7";
			this.mColumnSelect_07.Visible = false;
			// 
			// mColumnSelect_08
			// 
			this.mColumnSelect_08.Name = "mColumnSelect_08";
			this.mColumnSelect_08.Size = new System.Drawing.Size(182, 22);
			this.mColumnSelect_08.Text = "Col List #8";
			this.mColumnSelect_08.Visible = false;
			// 
			// mColumnSelect_09
			// 
			this.mColumnSelect_09.Name = "mColumnSelect_09";
			this.mColumnSelect_09.Size = new System.Drawing.Size(182, 22);
			this.mColumnSelect_09.Text = "Col List #9";
			this.mColumnSelect_09.Visible = false;
			// 
			// mColumnSelect_10
			// 
			this.mColumnSelect_10.Name = "mColumnSelect_10";
			this.mColumnSelect_10.Size = new System.Drawing.Size(182, 22);
			this.mColumnSelect_10.Text = "Col List #10";
			this.mColumnSelect_10.Visible = false;
			// 
			// mColumnSelect_11
			// 
			this.mColumnSelect_11.Name = "mColumnSelect_11";
			this.mColumnSelect_11.Size = new System.Drawing.Size(182, 22);
			this.mColumnSelect_11.Text = "Col List #11";
			this.mColumnSelect_11.Visible = false;
			// 
			// mColumnSelect_12
			// 
			this.mColumnSelect_12.Name = "mColumnSelect_12";
			this.mColumnSelect_12.Size = new System.Drawing.Size(182, 22);
			this.mColumnSelect_12.Text = "Col List #12";
			this.mColumnSelect_12.Visible = false;
			// 
			// mColumnSelect_13
			// 
			this.mColumnSelect_13.Name = "mColumnSelect_13";
			this.mColumnSelect_13.Size = new System.Drawing.Size(182, 22);
			this.mColumnSelect_13.Text = "Col List #13";
			this.mColumnSelect_13.Visible = false;
			// 
			// mColumnSelect_14
			// 
			this.mColumnSelect_14.Name = "mColumnSelect_14";
			this.mColumnSelect_14.Size = new System.Drawing.Size(182, 22);
			this.mColumnSelect_14.Text = "Col List #14";
			// 
			// mColumnSelect_15
			// 
			this.mColumnSelect_15.Name = "mColumnSelect_15";
			this.mColumnSelect_15.Size = new System.Drawing.Size(182, 22);
			this.mColumnSelect_15.Text = "Col List #15";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(179, 6);
			// 
			// mColumnSelect_Edit
			// 
			this.mColumnSelect_Edit.Name = "mColumnSelect_Edit";
			this.mColumnSelect_Edit.Size = new System.Drawing.Size(182, 22);
			this.mColumnSelect_Edit.Text = "Edit Column Setup...";
			// 
			// mTankFilter
			// 
			this.mTankFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mTankFilter_All,
            this.mTankFilter_Country,
            this.mTankFilter_Type,
            this.mTankFilter_Tier,
            this.toolStripSeparator6,
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
            this.mTankFilter_EditFavList});
			this.mTankFilter.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter.Image")));
			this.mTankFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mTankFilter.Name = "mTankFilter";
			this.mTankFilter.ShowDropDownArrow = false;
			this.mTankFilter.Size = new System.Drawing.Size(75, 22);
			this.mTankFilter.Text = "All Tanks";
			// 
			// mTankFilter_All
			// 
			this.mTankFilter_All.BackColor = System.Drawing.Color.Transparent;
			this.mTankFilter_All.Checked = true;
			this.mTankFilter_All.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mTankFilter_All.Name = "mTankFilter_All";
			this.mTankFilter_All.Size = new System.Drawing.Size(205, 22);
			this.mTankFilter_All.Text = "All Tanks";
			// 
			// mTankFilter_Country
			// 
			this.mTankFilter_Country.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mTankFilter_CountryChina,
            this.mTankFilter_CountryFrance,
            this.mTankFilter_CountryGermany,
            this.mTankFilter_CountryUK,
            this.mTankFilter_CountryUSA,
            this.mTankFilter_CountryUSSR,
            this.mTankFilter_CountryJapan});
			this.mTankFilter_Country.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Country.Image")));
			this.mTankFilter_Country.Name = "mTankFilter_Country";
			this.mTankFilter_Country.Size = new System.Drawing.Size(205, 22);
			this.mTankFilter_Country.Text = "Nation";
			// 
			// mTankFilter_CountryChina
			// 
			this.mTankFilter_CountryChina.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryChina.Image")));
			this.mTankFilter_CountryChina.Name = "mTankFilter_CountryChina";
			this.mTankFilter_CountryChina.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_CountryChina.Text = "China";
			// 
			// mTankFilter_CountryFrance
			// 
			this.mTankFilter_CountryFrance.BackColor = System.Drawing.SystemColors.Control;
			this.mTankFilter_CountryFrance.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryFrance.Image")));
			this.mTankFilter_CountryFrance.Name = "mTankFilter_CountryFrance";
			this.mTankFilter_CountryFrance.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_CountryFrance.Text = "France";
			// 
			// mTankFilter_CountryGermany
			// 
			this.mTankFilter_CountryGermany.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryGermany.Image")));
			this.mTankFilter_CountryGermany.Name = "mTankFilter_CountryGermany";
			this.mTankFilter_CountryGermany.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_CountryGermany.Text = "Germany";
			// 
			// mTankFilter_CountryUK
			// 
			this.mTankFilter_CountryUK.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryUK.Image")));
			this.mTankFilter_CountryUK.Name = "mTankFilter_CountryUK";
			this.mTankFilter_CountryUK.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_CountryUK.Text = "U.K.";
			// 
			// mTankFilter_CountryUSA
			// 
			this.mTankFilter_CountryUSA.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryUSA.Image")));
			this.mTankFilter_CountryUSA.Name = "mTankFilter_CountryUSA";
			this.mTankFilter_CountryUSA.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_CountryUSA.Text = "U.S.A.";
			// 
			// mTankFilter_CountryUSSR
			// 
			this.mTankFilter_CountryUSSR.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryUSSR.Image")));
			this.mTankFilter_CountryUSSR.Name = "mTankFilter_CountryUSSR";
			this.mTankFilter_CountryUSSR.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_CountryUSSR.Text = "U.S.S.R.";
			// 
			// mTankFilter_CountryJapan
			// 
			this.mTankFilter_CountryJapan.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_CountryJapan.Image")));
			this.mTankFilter_CountryJapan.Name = "mTankFilter_CountryJapan";
			this.mTankFilter_CountryJapan.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_CountryJapan.Text = "Japan";
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
			this.mTankFilter_Type.Size = new System.Drawing.Size(205, 22);
			this.mTankFilter_Type.Text = "Tank Type";
			// 
			// mTankFilter_TypeLT
			// 
			this.mTankFilter_TypeLT.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_TypeLT.Image")));
			this.mTankFilter_TypeLT.Name = "mTankFilter_TypeLT";
			this.mTankFilter_TypeLT.Size = new System.Drawing.Size(158, 22);
			this.mTankFilter_TypeLT.Text = "Light Tanks";
			// 
			// mTankFilter_TypeMT
			// 
			this.mTankFilter_TypeMT.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_TypeMT.Image")));
			this.mTankFilter_TypeMT.Name = "mTankFilter_TypeMT";
			this.mTankFilter_TypeMT.Size = new System.Drawing.Size(158, 22);
			this.mTankFilter_TypeMT.Text = "Medium Tanks";
			// 
			// mTankFilter_TypeHT
			// 
			this.mTankFilter_TypeHT.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_TypeHT.Image")));
			this.mTankFilter_TypeHT.Name = "mTankFilter_TypeHT";
			this.mTankFilter_TypeHT.Size = new System.Drawing.Size(158, 22);
			this.mTankFilter_TypeHT.Text = "Heavy Tanks";
			// 
			// mTankFilter_TypeTD
			// 
			this.mTankFilter_TypeTD.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_TypeTD.Image")));
			this.mTankFilter_TypeTD.Name = "mTankFilter_TypeTD";
			this.mTankFilter_TypeTD.Size = new System.Drawing.Size(158, 22);
			this.mTankFilter_TypeTD.Text = "Tank Destroyers";
			// 
			// mTankFilter_TypeSPG
			// 
			this.mTankFilter_TypeSPG.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_TypeSPG.Image")));
			this.mTankFilter_TypeSPG.Name = "mTankFilter_TypeSPG";
			this.mTankFilter_TypeSPG.Size = new System.Drawing.Size(158, 22);
			this.mTankFilter_TypeSPG.Text = "SPGs";
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
			this.mTankFilter_Tier.Size = new System.Drawing.Size(205, 22);
			this.mTankFilter_Tier.Text = "Tier";
			// 
			// mTankFilter_Tier1
			// 
			this.mTankFilter_Tier1.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier1.Image")));
			this.mTankFilter_Tier1.Name = "mTankFilter_Tier1";
			this.mTankFilter_Tier1.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_Tier1.Text = "1";
			// 
			// mTankFilter_Tier2
			// 
			this.mTankFilter_Tier2.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier2.Image")));
			this.mTankFilter_Tier2.Name = "mTankFilter_Tier2";
			this.mTankFilter_Tier2.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_Tier2.Text = "2";
			// 
			// mTankFilter_Tier3
			// 
			this.mTankFilter_Tier3.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier3.Image")));
			this.mTankFilter_Tier3.Name = "mTankFilter_Tier3";
			this.mTankFilter_Tier3.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_Tier3.Text = "3";
			// 
			// mTankFilter_Tier4
			// 
			this.mTankFilter_Tier4.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier4.Image")));
			this.mTankFilter_Tier4.Name = "mTankFilter_Tier4";
			this.mTankFilter_Tier4.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_Tier4.Text = "4";
			// 
			// mTankFilter_Tier5
			// 
			this.mTankFilter_Tier5.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier5.Image")));
			this.mTankFilter_Tier5.Name = "mTankFilter_Tier5";
			this.mTankFilter_Tier5.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_Tier5.Text = "5";
			// 
			// mTankFilter_Tier6
			// 
			this.mTankFilter_Tier6.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier6.Image")));
			this.mTankFilter_Tier6.Name = "mTankFilter_Tier6";
			this.mTankFilter_Tier6.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_Tier6.Text = "6";
			// 
			// mTankFilter_Tier7
			// 
			this.mTankFilter_Tier7.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier7.Image")));
			this.mTankFilter_Tier7.Name = "mTankFilter_Tier7";
			this.mTankFilter_Tier7.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_Tier7.Text = "7";
			// 
			// mTankFilter_Tier8
			// 
			this.mTankFilter_Tier8.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier8.Image")));
			this.mTankFilter_Tier8.Name = "mTankFilter_Tier8";
			this.mTankFilter_Tier8.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_Tier8.Text = "8";
			// 
			// mTankFilter_Tier9
			// 
			this.mTankFilter_Tier9.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier9.Image")));
			this.mTankFilter_Tier9.Name = "mTankFilter_Tier9";
			this.mTankFilter_Tier9.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_Tier9.Text = "9";
			// 
			// mTankFilter_Tier10
			// 
			this.mTankFilter_Tier10.Image = ((System.Drawing.Image)(resources.GetObject("mTankFilter_Tier10.Image")));
			this.mTankFilter_Tier10.Name = "mTankFilter_Tier10";
			this.mTankFilter_Tier10.Size = new System.Drawing.Size(152, 22);
			this.mTankFilter_Tier10.Text = "10";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(202, 6);
			this.toolStripSeparator6.Visible = false;
			// 
			// mTankFilter_Fav01
			// 
			this.mTankFilter_Fav01.Name = "mTankFilter_Fav01";
			this.mTankFilter_Fav01.Size = new System.Drawing.Size(205, 22);
			this.mTankFilter_Fav01.Text = "Favourite item #1";
			this.mTankFilter_Fav01.Visible = false;
			// 
			// mTankFilter_Fav02
			// 
			this.mTankFilter_Fav02.Name = "mTankFilter_Fav02";
			this.mTankFilter_Fav02.Size = new System.Drawing.Size(205, 22);
			this.mTankFilter_Fav02.Text = "Favourite item #2";
			this.mTankFilter_Fav02.Visible = false;
			// 
			// mTankFilter_Fav03
			// 
			this.mTankFilter_Fav03.Name = "mTankFilter_Fav03";
			this.mTankFilter_Fav03.Size = new System.Drawing.Size(205, 22);
			this.mTankFilter_Fav03.Text = "Favourite item #3";
			this.mTankFilter_Fav03.Visible = false;
			// 
			// mTankFilter_Fav04
			// 
			this.mTankFilter_Fav04.Name = "mTankFilter_Fav04";
			this.mTankFilter_Fav04.Size = new System.Drawing.Size(205, 22);
			this.mTankFilter_Fav04.Text = "Favourite item #4";
			this.mTankFilter_Fav04.Visible = false;
			// 
			// mTankFilter_Fav05
			// 
			this.mTankFilter_Fav05.Name = "mTankFilter_Fav05";
			this.mTankFilter_Fav05.Size = new System.Drawing.Size(205, 22);
			this.mTankFilter_Fav05.Text = "Favourite item #5";
			this.mTankFilter_Fav05.Visible = false;
			// 
			// mTankFilter_Fav06
			// 
			this.mTankFilter_Fav06.Name = "mTankFilter_Fav06";
			this.mTankFilter_Fav06.Size = new System.Drawing.Size(205, 22);
			this.mTankFilter_Fav06.Text = "Favourite item #6";
			this.mTankFilter_Fav06.Visible = false;
			// 
			// mTankFilter_Fav07
			// 
			this.mTankFilter_Fav07.Name = "mTankFilter_Fav07";
			this.mTankFilter_Fav07.Size = new System.Drawing.Size(205, 22);
			this.mTankFilter_Fav07.Text = "Favourite item #7";
			this.mTankFilter_Fav07.Visible = false;
			// 
			// mTankFilter_Fav08
			// 
			this.mTankFilter_Fav08.Name = "mTankFilter_Fav08";
			this.mTankFilter_Fav08.Size = new System.Drawing.Size(205, 22);
			this.mTankFilter_Fav08.Text = "Favourite item #8";
			this.mTankFilter_Fav08.Visible = false;
			// 
			// mTankFilter_Fav09
			// 
			this.mTankFilter_Fav09.Name = "mTankFilter_Fav09";
			this.mTankFilter_Fav09.Size = new System.Drawing.Size(205, 22);
			this.mTankFilter_Fav09.Text = "Favourite item #9";
			this.mTankFilter_Fav09.Visible = false;
			// 
			// mTankFilter_Fav10
			// 
			this.mTankFilter_Fav10.Name = "mTankFilter_Fav10";
			this.mTankFilter_Fav10.Size = new System.Drawing.Size(205, 22);
			this.mTankFilter_Fav10.Text = "Favourite item #10";
			this.mTankFilter_Fav10.Visible = false;
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(202, 6);
			// 
			// mTankFilter_EditFavList
			// 
			this.mTankFilter_EditFavList.Name = "mTankFilter_EditFavList";
			this.mTankFilter_EditFavList.Size = new System.Drawing.Size(205, 22);
			this.mTankFilter_EditFavList.Text = "Edit Favourite Tank List...";
			// 
			// mMode
			// 
			this.mMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem54,
            this.toolStripSeparator17,
            this.mModeRandomCompanyClan,
            this.mModeTeam,
            this.mModeHistorical,
            this.toolStripSeparator18,
            this.mModeRandom,
            this.mModeCompany,
            this.mModeClan,
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
			this.toolStripMenuItem54.Size = new System.Drawing.Size(227, 22);
			this.toolStripMenuItem54.Tag = "All";
			this.toolStripMenuItem54.Text = "All modes";
			// 
			// toolStripSeparator17
			// 
			this.toolStripSeparator17.Name = "toolStripSeparator17";
			this.toolStripSeparator17.Size = new System.Drawing.Size(224, 6);
			// 
			// mModeRandomCompanyClan
			// 
			this.mModeRandomCompanyClan.Name = "mModeRandomCompanyClan";
			this.mModeRandomCompanyClan.Size = new System.Drawing.Size(227, 22);
			this.mModeRandomCompanyClan.Tag = "Mode15";
			this.mModeRandomCompanyClan.Text = "Random, Company and Clan";
			// 
			// mModeTeam
			// 
			this.mModeTeam.Name = "mModeTeam";
			this.mModeTeam.Size = new System.Drawing.Size(227, 22);
			this.mModeTeam.Tag = "Mode7";
			this.mModeTeam.Text = "Team Battles";
			// 
			// mModeHistorical
			// 
			this.mModeHistorical.Name = "mModeHistorical";
			this.mModeHistorical.Size = new System.Drawing.Size(227, 22);
			this.mModeHistorical.Tag = "Historical";
			this.mModeHistorical.Text = "Historical Battles";
			// 
			// toolStripSeparator18
			// 
			this.toolStripSeparator18.Name = "toolStripSeparator18";
			this.toolStripSeparator18.Size = new System.Drawing.Size(224, 6);
			// 
			// mModeRandom
			// 
			this.mModeRandom.Name = "mModeRandom";
			this.mModeRandom.Size = new System.Drawing.Size(227, 22);
			this.mModeRandom.Tag = "Random";
			this.mModeRandom.Text = "Random Battles (*)";
			// 
			// mModeCompany
			// 
			this.mModeCompany.Name = "mModeCompany";
			this.mModeCompany.Size = new System.Drawing.Size(227, 22);
			this.mModeCompany.Tag = "Company";
			this.mModeCompany.Text = "Tank Company Battles (*)";
			// 
			// mModeClan
			// 
			this.mModeClan.Name = "mModeClan";
			this.mModeClan.Size = new System.Drawing.Size(227, 22);
			this.mModeClan.Tag = "Clan";
			this.mModeClan.Text = "Clan War Battles (*)";
			// 
			// mModeSpecialInfo
			// 
			this.mModeSpecialInfo.Name = "mModeSpecialInfo";
			this.mModeSpecialInfo.Size = new System.Drawing.Size(227, 22);
			this.mModeSpecialInfo.Text = "* Stats Information";
			// 
			// mBattles
			// 
			this.mBattles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem62,
            this.mBattlesYesterday,
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
			// toolStripMenuItem62
			// 
			this.toolStripMenuItem62.Checked = true;
			this.toolStripMenuItem62.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolStripMenuItem62.Name = "toolStripMenuItem62";
			this.toolStripMenuItem62.Size = new System.Drawing.Size(181, 22);
			this.toolStripMenuItem62.Text = "Today\'s Battles";
			// 
			// mBattlesYesterday
			// 
			this.mBattlesYesterday.Name = "mBattlesYesterday";
			this.mBattlesYesterday.Size = new System.Drawing.Size(181, 22);
			this.mBattlesYesterday.Text = "Yesterday\'s Battles";
			// 
			// mBattles3d
			// 
			this.mBattles3d.Name = "mBattles3d";
			this.mBattles3d.Size = new System.Drawing.Size(181, 22);
			this.mBattles3d.Text = "Battles Last 3 Days";
			// 
			// toolStripSeparator19
			// 
			this.toolStripSeparator19.Name = "toolStripSeparator19";
			this.toolStripSeparator19.Size = new System.Drawing.Size(178, 6);
			// 
			// mBattles1w
			// 
			this.mBattles1w.Name = "mBattles1w";
			this.mBattles1w.Size = new System.Drawing.Size(181, 22);
			this.mBattles1w.Text = "Battles Last Week";
			// 
			// mBattles2w
			// 
			this.mBattles2w.Name = "mBattles2w";
			this.mBattles2w.Size = new System.Drawing.Size(181, 22);
			this.mBattles2w.Text = "Battles Last 2 Weeks";
			// 
			// toolStripSeparator20
			// 
			this.toolStripSeparator20.Name = "toolStripSeparator20";
			this.toolStripSeparator20.Size = new System.Drawing.Size(178, 6);
			// 
			// mBattles1m
			// 
			this.mBattles1m.Name = "mBattles1m";
			this.mBattles1m.Size = new System.Drawing.Size(181, 22);
			this.mBattles1m.Text = "Battles Last Month";
			// 
			// mBattles3m
			// 
			this.mBattles3m.Name = "mBattles3m";
			this.mBattles3m.Size = new System.Drawing.Size(181, 22);
			this.mBattles3m.Text = "Battles Last 3 Month";
			// 
			// mBattles6m
			// 
			this.mBattles6m.Name = "mBattles6m";
			this.mBattles6m.Size = new System.Drawing.Size(181, 22);
			this.mBattles6m.Text = "Battles Last 6 Month";
			// 
			// toolStripSeparator21
			// 
			this.toolStripSeparator21.Name = "toolStripSeparator21";
			this.toolStripSeparator21.Size = new System.Drawing.Size(178, 6);
			// 
			// mBattles1y
			// 
			this.mBattles1y.Name = "mBattles1y";
			this.mBattles1y.Size = new System.Drawing.Size(181, 22);
			this.mBattles1y.Text = "Battles Last Year";
			// 
			// mBattles2y
			// 
			this.mBattles2y.Name = "mBattles2y";
			this.mBattles2y.Size = new System.Drawing.Size(181, 22);
			this.mBattles2y.Text = "Battles Last 2 Years";
			// 
			// toolStripSeparator22
			// 
			this.toolStripSeparator22.Name = "toolStripSeparator22";
			this.toolStripSeparator22.Size = new System.Drawing.Size(178, 6);
			// 
			// mBattlesAll
			// 
			this.mBattlesAll.Name = "mBattlesAll";
			this.mBattlesAll.Size = new System.Drawing.Size(181, 22);
			this.mBattlesAll.Text = "All Battles";
			// 
			// toolStripSeparator23
			// 
			this.toolStripSeparator23.Name = "toolStripSeparator23";
			this.toolStripSeparator23.Size = new System.Drawing.Size(6, 25);
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
			this.mSettingsRun.Text = "Listen To Dossier File";
			// 
			// mSettingsDossierOptions
			// 
			this.mSettingsDossierOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSettingsRunManual,
            this.mSettingsForceUpdateFromPrev});
			this.mSettingsDossierOptions.Name = "mSettingsDossierOptions";
			this.mSettingsDossierOptions.Size = new System.Drawing.Size(263, 22);
			this.mSettingsDossierOptions.Text = "Dossier File Options";
			// 
			// mSettingsRunManual
			// 
			this.mSettingsRunManual.Name = "mSettingsRunManual";
			this.mSettingsRunManual.Size = new System.Drawing.Size(286, 22);
			this.mSettingsRunManual.Text = "Normal Dossier File Check";
			// 
			// mSettingsForceUpdateFromPrev
			// 
			this.mSettingsForceUpdateFromPrev.Name = "mSettingsForceUpdateFromPrev";
			this.mSettingsForceUpdateFromPrev.Size = new System.Drawing.Size(286, 22);
			this.mSettingsForceUpdateFromPrev.Text = "Force Update All Data Dossier File Check";
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
			// 
			// mImportBattlesFromWotStat
			// 
			this.mImportBattlesFromWotStat.Name = "mImportBattlesFromWotStat";
			this.mImportBattlesFromWotStat.Size = new System.Drawing.Size(263, 22);
			this.mImportBattlesFromWotStat.Text = "Import battles from WoT Statistics...";
			// 
			// mShowDbTables
			// 
			this.mShowDbTables.Name = "mShowDbTables";
			this.mShowDbTables.Size = new System.Drawing.Size(263, 22);
			this.mShowDbTables.Text = "Show Database Tables...";
			// 
			// toolStripSeparator25
			// 
			this.toolStripSeparator25.Name = "toolStripSeparator25";
			this.toolStripSeparator25.Size = new System.Drawing.Size(260, 6);
			// 
			// mSettingsApp
			// 
			this.mSettingsApp.Name = "mSettingsApp";
			this.mSettingsApp.Size = new System.Drawing.Size(263, 22);
			this.mSettingsApp.Text = "&Application Settings...";
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
			// mHelp
			// 
			this.mHelp.Image = ((System.Drawing.Image)(resources.GetObject("mHelp.Image")));
			this.mHelp.Name = "mHelp";
			this.mHelp.Size = new System.Drawing.Size(23, 22);
			// 
			// lblStatusRowCount
			// 
			this.lblStatusRowCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblStatusRowCount.BackColor = System.Drawing.Color.Transparent;
			this.lblStatusRowCount.ForeColor = System.Drawing.Color.DarkGray;
			this.lblStatusRowCount.Location = new System.Drawing.Point(723, 411);
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
			this.dataGridMain.AllowUserToOrderColumns = true;
			this.dataGridMain.BackgroundColor = System.Drawing.Color.Gray;
			this.dataGridMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridMain.CausesValidation = false;
			this.dataGridMain.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gainsboro;
			dataGridViewCellStyle1.NullValue = null;
			dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridMain.ColumnHeadersHeight = 36;
			this.dataGridMain.Cursor = System.Windows.Forms.Cursors.Arrow;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			dataGridViewCellStyle2.NullValue = null;
			dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridMain.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridMain.EnableHeadersVisualStyles = false;
			this.dataGridMain.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
			this.dataGridMain.Location = new System.Drawing.Point(14, 88);
			this.dataGridMain.Name = "dataGridMain";
			this.dataGridMain.ReadOnly = true;
			this.dataGridMain.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridMain.RowHeadersVisible = false;
			this.dataGridMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dataGridMain.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dataGridMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dataGridMain.ShowEditingIcon = false;
			this.dataGridMain.Size = new System.Drawing.Size(601, 204);
			this.dataGridMain.TabIndex = 11;
			this.dataGridMain.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridMain_CellFormatting);
			this.dataGridMain.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridMain_CellMouseDown);
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
			this.scrollX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollX_MouseDown);
			this.scrollX.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollX_MouseMove);
			this.scrollX.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scrollX_MouseUp);
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
            this.toolItemRefreshSeparator,
            this.toolItemColumnSelect,
            this.toolItemTankFilter,
            this.toolItemMode,
            this.toolItemBattles,
            this.toolStripSeparator8,
            this.toolItemSettings,
            this.toolItemViewChart,
            this.toolItemHelp});
			this.toolMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolMain.Location = new System.Drawing.Point(9, 29);
			this.toolMain.Name = "toolMain";
			this.toolMain.Size = new System.Drawing.Size(645, 25);
			this.toolMain.Stretch = true;
			this.toolMain.TabIndex = 13;
			this.toolMain.Text = "7";
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
			this.toolItemViewOverall.Image = ((System.Drawing.Image)(resources.GetObject("toolItemViewOverall.Image")));
			this.toolItemViewOverall.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemViewOverall.Name = "toolItemViewOverall";
			this.toolItemViewOverall.Size = new System.Drawing.Size(60, 22);
			this.toolItemViewOverall.Text = "&Home";
			this.toolItemViewOverall.ToolTipText = " ";
			this.toolItemViewOverall.Click += new System.EventHandler(this.toolItemViewOverall_Click);
			// 
			// toolItemViewTankInfo
			// 
			this.toolItemViewTankInfo.Image = ((System.Drawing.Image)(resources.GetObject("toolItemViewTankInfo.Image")));
			this.toolItemViewTankInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemViewTankInfo.Name = "toolItemViewTankInfo";
			this.toolItemViewTankInfo.Size = new System.Drawing.Size(58, 22);
			this.toolItemViewTankInfo.Text = "&Tanks";
			this.toolItemViewTankInfo.Click += new System.EventHandler(this.toolItemViewTankInfo_Click);
			// 
			// toolItemViewBattles
			// 
			this.toolItemViewBattles.Image = ((System.Drawing.Image)(resources.GetObject("toolItemViewBattles.Image")));
			this.toolItemViewBattles.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemViewBattles.Name = "toolItemViewBattles";
			this.toolItemViewBattles.Size = new System.Drawing.Size(62, 22);
			this.toolItemViewBattles.Text = "&Battles";
			this.toolItemViewBattles.Click += new System.EventHandler(this.toolItemViewBattles_Click);
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
			// toolItemRefreshSeparator
			// 
			this.toolItemRefreshSeparator.Name = "toolItemRefreshSeparator";
			this.toolItemRefreshSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// toolItemColumnSelect
			// 
			this.toolItemColumnSelect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemColumnSelect_01,
            this.toolItemColumnSelect_02,
            this.toolItemColumnSelect_03,
            this.toolItemColumnSelect_04,
            this.toolItemColumnSelect_05,
            this.toolItemColumnSelect_06,
            this.toolItemColumnSelect_07,
            this.toolItemColumnSelect_08,
            this.toolItemColumnSelect_09,
            this.toolItemColumnSelect_10,
            this.toolItemColumnSelect_11,
            this.toolItemColumnSelect_12,
            this.toolItemColumnSelect_13,
            this.toolItemColumnSelect_14,
            this.toolItemColumnSelect_15,
            this.toolStripSeparator3,
            this.toolItemColumnSelect_Edit});
			this.toolItemColumnSelect.Image = ((System.Drawing.Image)(resources.GetObject("toolItemColumnSelect.Image")));
			this.toolItemColumnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemColumnSelect.Name = "toolItemColumnSelect";
			this.toolItemColumnSelect.ShowDropDownArrow = false;
			this.toolItemColumnSelect.Size = new System.Drawing.Size(65, 22);
			this.toolItemColumnSelect.Text = "Default";
			this.toolItemColumnSelect.ToolTipText = "Default";
			// 
			// toolItemColumnSelect_01
			// 
			this.toolItemColumnSelect_01.Checked = true;
			this.toolItemColumnSelect_01.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolItemColumnSelect_01.Name = "toolItemColumnSelect_01";
			this.toolItemColumnSelect_01.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_01.Text = "Col List #1";
			this.toolItemColumnSelect_01.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_01.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_02
			// 
			this.toolItemColumnSelect_02.Name = "toolItemColumnSelect_02";
			this.toolItemColumnSelect_02.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_02.Text = "Col List #2";
			this.toolItemColumnSelect_02.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_02.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_03
			// 
			this.toolItemColumnSelect_03.Name = "toolItemColumnSelect_03";
			this.toolItemColumnSelect_03.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_03.Text = "Col List #3";
			this.toolItemColumnSelect_03.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_03.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_04
			// 
			this.toolItemColumnSelect_04.Name = "toolItemColumnSelect_04";
			this.toolItemColumnSelect_04.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_04.Text = "Col List #4";
			this.toolItemColumnSelect_04.Visible = false;
			this.toolItemColumnSelect_04.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_04.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_05
			// 
			this.toolItemColumnSelect_05.Name = "toolItemColumnSelect_05";
			this.toolItemColumnSelect_05.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_05.Text = "Col List #5";
			this.toolItemColumnSelect_05.Visible = false;
			this.toolItemColumnSelect_05.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_05.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_06
			// 
			this.toolItemColumnSelect_06.Name = "toolItemColumnSelect_06";
			this.toolItemColumnSelect_06.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_06.Text = "Col List #6";
			this.toolItemColumnSelect_06.Visible = false;
			this.toolItemColumnSelect_06.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_06.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_07
			// 
			this.toolItemColumnSelect_07.Name = "toolItemColumnSelect_07";
			this.toolItemColumnSelect_07.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_07.Text = "Col List #7";
			this.toolItemColumnSelect_07.Visible = false;
			this.toolItemColumnSelect_07.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_07.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_08
			// 
			this.toolItemColumnSelect_08.Name = "toolItemColumnSelect_08";
			this.toolItemColumnSelect_08.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_08.Text = "Col List #8";
			this.toolItemColumnSelect_08.Visible = false;
			this.toolItemColumnSelect_08.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_08.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_09
			// 
			this.toolItemColumnSelect_09.Name = "toolItemColumnSelect_09";
			this.toolItemColumnSelect_09.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_09.Text = "Col List #9";
			this.toolItemColumnSelect_09.Visible = false;
			this.toolItemColumnSelect_09.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_09.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_10
			// 
			this.toolItemColumnSelect_10.Name = "toolItemColumnSelect_10";
			this.toolItemColumnSelect_10.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_10.Text = "Col List #10";
			this.toolItemColumnSelect_10.Visible = false;
			this.toolItemColumnSelect_10.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_10.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_11
			// 
			this.toolItemColumnSelect_11.Name = "toolItemColumnSelect_11";
			this.toolItemColumnSelect_11.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_11.Text = "Col List #11";
			this.toolItemColumnSelect_11.Visible = false;
			this.toolItemColumnSelect_11.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_11.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_12
			// 
			this.toolItemColumnSelect_12.Name = "toolItemColumnSelect_12";
			this.toolItemColumnSelect_12.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_12.Text = "Col List #12";
			this.toolItemColumnSelect_12.Visible = false;
			this.toolItemColumnSelect_12.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_12.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_13
			// 
			this.toolItemColumnSelect_13.Name = "toolItemColumnSelect_13";
			this.toolItemColumnSelect_13.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_13.Text = "Col List #13";
			this.toolItemColumnSelect_13.Visible = false;
			this.toolItemColumnSelect_13.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_13.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_14
			// 
			this.toolItemColumnSelect_14.Name = "toolItemColumnSelect_14";
			this.toolItemColumnSelect_14.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_14.Text = "Col List #14";
			this.toolItemColumnSelect_14.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_14.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_15
			// 
			this.toolItemColumnSelect_15.Name = "toolItemColumnSelect_15";
			this.toolItemColumnSelect_15.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_15.Text = "Col List #15";
			this.toolItemColumnSelect_15.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_15.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(179, 6);
			// 
			// toolItemColumnSelect_Edit
			// 
			this.toolItemColumnSelect_Edit.Name = "toolItemColumnSelect_Edit";
			this.toolItemColumnSelect_Edit.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_Edit.Text = "Edit Column Setup...";
			this.toolItemColumnSelect_Edit.Click += new System.EventHandler(this.toolItemColumnSelect_Edit_Click);
			// 
			// toolItemTankFilter
			// 
			this.toolItemTankFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemTankFilter_All,
            this.toolItemTankFilter_Country,
            this.toolItemTankFilter_Type,
            this.toolItemTankFilter_Tier,
            this.toolItemTankFilter_FavSeparator,
            this.toolItemTankFilter_Fav01,
            this.toolItemTankFilter_Fav02,
            this.toolItemTankFilter_Fav03,
            this.toolItemTankFilter_Fav04,
            this.toolItemTankFilter_Fav05,
            this.toolItemTankFilter_Fav06,
            this.toolItemTankFilter_Fav07,
            this.toolItemTankFilter_Fav08,
            this.toolItemTankFilter_Fav09,
            this.toolItemTankFilter_Fav10,
            this.toolStripSeparator4,
            this.toolItemTankFilter_EditFavList});
			this.toolItemTankFilter.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter.Image")));
			this.toolItemTankFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemTankFilter.Name = "toolItemTankFilter";
			this.toolItemTankFilter.ShowDropDownArrow = false;
			this.toolItemTankFilter.Size = new System.Drawing.Size(75, 22);
			this.toolItemTankFilter.Text = "All Tanks";
			this.toolItemTankFilter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_MouseDown);
			// 
			// toolItemTankFilter_All
			// 
			this.toolItemTankFilter_All.BackColor = System.Drawing.Color.Transparent;
			this.toolItemTankFilter_All.Checked = true;
			this.toolItemTankFilter_All.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolItemTankFilter_All.Name = "toolItemTankFilter_All";
			this.toolItemTankFilter_All.Size = new System.Drawing.Size(205, 22);
			this.toolItemTankFilter_All.Text = "All Tanks";
			this.toolItemTankFilter_All.Click += new System.EventHandler(this.toolItemTankFilter_All_Click);
			this.toolItemTankFilter_All.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Country
			// 
			this.toolItemTankFilter_Country.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemTankFilter_CountryChina,
            this.toolItemTankFilter_CountryFrance,
            this.toolItemTankFilter_CountryGermany,
            this.toolItemTankFilter_CountryUK,
            this.toolItemTankFilter_CountryUSA,
            this.toolItemTankFilter_CountryUSSR,
            this.toolItemTankFilter_CountryJapan});
			this.toolItemTankFilter_Country.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Country.Image")));
			this.toolItemTankFilter_Country.Name = "toolItemTankFilter_Country";
			this.toolItemTankFilter_Country.Size = new System.Drawing.Size(205, 22);
			this.toolItemTankFilter_Country.Text = "Nation";
			// 
			// toolItemTankFilter_CountryChina
			// 
			this.toolItemTankFilter_CountryChina.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_CountryChina.Image")));
			this.toolItemTankFilter_CountryChina.Name = "toolItemTankFilter_CountryChina";
			this.toolItemTankFilter_CountryChina.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_CountryChina.Text = "China";
			this.toolItemTankFilter_CountryChina.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.toolItemTankFilter_CountryChina.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.toolItemTankFilter_CountryChina.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_CountryFrance
			// 
			this.toolItemTankFilter_CountryFrance.BackColor = System.Drawing.SystemColors.Control;
			this.toolItemTankFilter_CountryFrance.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_CountryFrance.Image")));
			this.toolItemTankFilter_CountryFrance.Name = "toolItemTankFilter_CountryFrance";
			this.toolItemTankFilter_CountryFrance.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_CountryFrance.Text = "France";
			this.toolItemTankFilter_CountryFrance.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.toolItemTankFilter_CountryFrance.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.toolItemTankFilter_CountryFrance.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_CountryGermany
			// 
			this.toolItemTankFilter_CountryGermany.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_CountryGermany.Image")));
			this.toolItemTankFilter_CountryGermany.Name = "toolItemTankFilter_CountryGermany";
			this.toolItemTankFilter_CountryGermany.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_CountryGermany.Text = "Germany";
			this.toolItemTankFilter_CountryGermany.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.toolItemTankFilter_CountryGermany.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.toolItemTankFilter_CountryGermany.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_CountryUK
			// 
			this.toolItemTankFilter_CountryUK.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_CountryUK.Image")));
			this.toolItemTankFilter_CountryUK.Name = "toolItemTankFilter_CountryUK";
			this.toolItemTankFilter_CountryUK.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_CountryUK.Text = "U.K.";
			this.toolItemTankFilter_CountryUK.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.toolItemTankFilter_CountryUK.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.toolItemTankFilter_CountryUK.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_CountryUSA
			// 
			this.toolItemTankFilter_CountryUSA.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_CountryUSA.Image")));
			this.toolItemTankFilter_CountryUSA.Name = "toolItemTankFilter_CountryUSA";
			this.toolItemTankFilter_CountryUSA.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_CountryUSA.Text = "U.S.A.";
			this.toolItemTankFilter_CountryUSA.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.toolItemTankFilter_CountryUSA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.toolItemTankFilter_CountryUSA.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_CountryUSSR
			// 
			this.toolItemTankFilter_CountryUSSR.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_CountryUSSR.Image")));
			this.toolItemTankFilter_CountryUSSR.Name = "toolItemTankFilter_CountryUSSR";
			this.toolItemTankFilter_CountryUSSR.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_CountryUSSR.Text = "U.S.S.R.";
			this.toolItemTankFilter_CountryUSSR.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.toolItemTankFilter_CountryUSSR.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.toolItemTankFilter_CountryUSSR.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_CountryJapan
			// 
			this.toolItemTankFilter_CountryJapan.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_CountryJapan.Image")));
			this.toolItemTankFilter_CountryJapan.Name = "toolItemTankFilter_CountryJapan";
			this.toolItemTankFilter_CountryJapan.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_CountryJapan.Text = "Japan";
			this.toolItemTankFilter_CountryJapan.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.toolItemTankFilter_CountryJapan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.toolItemTankFilter_CountryJapan.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Type
			// 
			this.toolItemTankFilter_Type.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemTankFilter_TypeLT,
            this.toolItemTankFilter_TypeMT,
            this.toolItemTankFilter_TypeHT,
            this.toolItemTankFilter_TypeTD,
            this.toolItemTankFilter_TypeSPG});
			this.toolItemTankFilter_Type.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Type.Image")));
			this.toolItemTankFilter_Type.Name = "toolItemTankFilter_Type";
			this.toolItemTankFilter_Type.Size = new System.Drawing.Size(205, 22);
			this.toolItemTankFilter_Type.Text = "Tank Type";
			// 
			// toolItemTankFilter_TypeLT
			// 
			this.toolItemTankFilter_TypeLT.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_TypeLT.Image")));
			this.toolItemTankFilter_TypeLT.Name = "toolItemTankFilter_TypeLT";
			this.toolItemTankFilter_TypeLT.Size = new System.Drawing.Size(158, 22);
			this.toolItemTankFilter_TypeLT.Text = "Light Tanks";
			this.toolItemTankFilter_TypeLT.Click += new System.EventHandler(this.toolItemTankFilter_Type_Click);
			this.toolItemTankFilter_TypeLT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Type_MouseDown);
			this.toolItemTankFilter_TypeLT.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_TypeMT
			// 
			this.toolItemTankFilter_TypeMT.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_TypeMT.Image")));
			this.toolItemTankFilter_TypeMT.Name = "toolItemTankFilter_TypeMT";
			this.toolItemTankFilter_TypeMT.Size = new System.Drawing.Size(158, 22);
			this.toolItemTankFilter_TypeMT.Text = "Medium Tanks";
			this.toolItemTankFilter_TypeMT.Click += new System.EventHandler(this.toolItemTankFilter_Type_Click);
			this.toolItemTankFilter_TypeMT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Type_MouseDown);
			this.toolItemTankFilter_TypeMT.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_TypeHT
			// 
			this.toolItemTankFilter_TypeHT.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_TypeHT.Image")));
			this.toolItemTankFilter_TypeHT.Name = "toolItemTankFilter_TypeHT";
			this.toolItemTankFilter_TypeHT.Size = new System.Drawing.Size(158, 22);
			this.toolItemTankFilter_TypeHT.Text = "Heavy Tanks";
			this.toolItemTankFilter_TypeHT.Click += new System.EventHandler(this.toolItemTankFilter_Type_Click);
			this.toolItemTankFilter_TypeHT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Type_MouseDown);
			this.toolItemTankFilter_TypeHT.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_TypeTD
			// 
			this.toolItemTankFilter_TypeTD.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_TypeTD.Image")));
			this.toolItemTankFilter_TypeTD.Name = "toolItemTankFilter_TypeTD";
			this.toolItemTankFilter_TypeTD.Size = new System.Drawing.Size(158, 22);
			this.toolItemTankFilter_TypeTD.Text = "Tank Destroyers";
			this.toolItemTankFilter_TypeTD.Click += new System.EventHandler(this.toolItemTankFilter_Type_Click);
			this.toolItemTankFilter_TypeTD.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Type_MouseDown);
			this.toolItemTankFilter_TypeTD.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_TypeSPG
			// 
			this.toolItemTankFilter_TypeSPG.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_TypeSPG.Image")));
			this.toolItemTankFilter_TypeSPG.Name = "toolItemTankFilter_TypeSPG";
			this.toolItemTankFilter_TypeSPG.Size = new System.Drawing.Size(158, 22);
			this.toolItemTankFilter_TypeSPG.Text = "SPGs";
			this.toolItemTankFilter_TypeSPG.Click += new System.EventHandler(this.toolItemTankFilter_Type_Click);
			this.toolItemTankFilter_TypeSPG.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Type_MouseDown);
			this.toolItemTankFilter_TypeSPG.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier
			// 
			this.toolItemTankFilter_Tier.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemTankFilter_Tier1,
            this.toolItemTankFilter_Tier2,
            this.toolItemTankFilter_Tier3,
            this.toolItemTankFilter_Tier4,
            this.toolItemTankFilter_Tier5,
            this.toolItemTankFilter_Tier6,
            this.toolItemTankFilter_Tier7,
            this.toolItemTankFilter_Tier8,
            this.toolItemTankFilter_Tier9,
            this.toolItemTankFilter_Tier10});
			this.toolItemTankFilter_Tier.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier.Image")));
			this.toolItemTankFilter_Tier.Name = "toolItemTankFilter_Tier";
			this.toolItemTankFilter_Tier.Size = new System.Drawing.Size(205, 22);
			this.toolItemTankFilter_Tier.Text = "Tier";
			// 
			// toolItemTankFilter_Tier1
			// 
			this.toolItemTankFilter_Tier1.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier1.Image")));
			this.toolItemTankFilter_Tier1.Name = "toolItemTankFilter_Tier1";
			this.toolItemTankFilter_Tier1.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_Tier1.Text = "1";
			this.toolItemTankFilter_Tier1.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier1.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier2
			// 
			this.toolItemTankFilter_Tier2.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier2.Image")));
			this.toolItemTankFilter_Tier2.Name = "toolItemTankFilter_Tier2";
			this.toolItemTankFilter_Tier2.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_Tier2.Text = "2";
			this.toolItemTankFilter_Tier2.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier2.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier3
			// 
			this.toolItemTankFilter_Tier3.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier3.Image")));
			this.toolItemTankFilter_Tier3.Name = "toolItemTankFilter_Tier3";
			this.toolItemTankFilter_Tier3.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_Tier3.Text = "3";
			this.toolItemTankFilter_Tier3.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier3.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier4
			// 
			this.toolItemTankFilter_Tier4.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier4.Image")));
			this.toolItemTankFilter_Tier4.Name = "toolItemTankFilter_Tier4";
			this.toolItemTankFilter_Tier4.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_Tier4.Text = "4";
			this.toolItemTankFilter_Tier4.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier4.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier5
			// 
			this.toolItemTankFilter_Tier5.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier5.Image")));
			this.toolItemTankFilter_Tier5.Name = "toolItemTankFilter_Tier5";
			this.toolItemTankFilter_Tier5.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_Tier5.Text = "5";
			this.toolItemTankFilter_Tier5.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier5.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier6
			// 
			this.toolItemTankFilter_Tier6.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier6.Image")));
			this.toolItemTankFilter_Tier6.Name = "toolItemTankFilter_Tier6";
			this.toolItemTankFilter_Tier6.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_Tier6.Text = "6";
			this.toolItemTankFilter_Tier6.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier6.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier7
			// 
			this.toolItemTankFilter_Tier7.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier7.Image")));
			this.toolItemTankFilter_Tier7.Name = "toolItemTankFilter_Tier7";
			this.toolItemTankFilter_Tier7.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_Tier7.Text = "7";
			this.toolItemTankFilter_Tier7.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier7.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier8
			// 
			this.toolItemTankFilter_Tier8.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier8.Image")));
			this.toolItemTankFilter_Tier8.Name = "toolItemTankFilter_Tier8";
			this.toolItemTankFilter_Tier8.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_Tier8.Text = "8";
			this.toolItemTankFilter_Tier8.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier8.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier9
			// 
			this.toolItemTankFilter_Tier9.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier9.Image")));
			this.toolItemTankFilter_Tier9.Name = "toolItemTankFilter_Tier9";
			this.toolItemTankFilter_Tier9.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_Tier9.Text = "9";
			this.toolItemTankFilter_Tier9.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier9.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier10
			// 
			this.toolItemTankFilter_Tier10.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier10.Image")));
			this.toolItemTankFilter_Tier10.Name = "toolItemTankFilter_Tier10";
			this.toolItemTankFilter_Tier10.Size = new System.Drawing.Size(152, 22);
			this.toolItemTankFilter_Tier10.Text = "10";
			this.toolItemTankFilter_Tier10.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier10.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier10.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_FavSeparator
			// 
			this.toolItemTankFilter_FavSeparator.Name = "toolItemTankFilter_FavSeparator";
			this.toolItemTankFilter_FavSeparator.Size = new System.Drawing.Size(202, 6);
			this.toolItemTankFilter_FavSeparator.Visible = false;
			// 
			// toolItemTankFilter_Fav01
			// 
			this.toolItemTankFilter_Fav01.Name = "toolItemTankFilter_Fav01";
			this.toolItemTankFilter_Fav01.Size = new System.Drawing.Size(205, 22);
			this.toolItemTankFilter_Fav01.Text = "Favourite item #1";
			this.toolItemTankFilter_Fav01.Visible = false;
			this.toolItemTankFilter_Fav01.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.toolItemTankFilter_Fav01.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Fav02
			// 
			this.toolItemTankFilter_Fav02.Name = "toolItemTankFilter_Fav02";
			this.toolItemTankFilter_Fav02.Size = new System.Drawing.Size(205, 22);
			this.toolItemTankFilter_Fav02.Text = "Favourite item #2";
			this.toolItemTankFilter_Fav02.Visible = false;
			this.toolItemTankFilter_Fav02.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.toolItemTankFilter_Fav02.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Fav03
			// 
			this.toolItemTankFilter_Fav03.Name = "toolItemTankFilter_Fav03";
			this.toolItemTankFilter_Fav03.Size = new System.Drawing.Size(205, 22);
			this.toolItemTankFilter_Fav03.Text = "Favourite item #3";
			this.toolItemTankFilter_Fav03.Visible = false;
			this.toolItemTankFilter_Fav03.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.toolItemTankFilter_Fav03.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Fav04
			// 
			this.toolItemTankFilter_Fav04.Name = "toolItemTankFilter_Fav04";
			this.toolItemTankFilter_Fav04.Size = new System.Drawing.Size(205, 22);
			this.toolItemTankFilter_Fav04.Text = "Favourite item #4";
			this.toolItemTankFilter_Fav04.Visible = false;
			this.toolItemTankFilter_Fav04.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.toolItemTankFilter_Fav04.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Fav05
			// 
			this.toolItemTankFilter_Fav05.Name = "toolItemTankFilter_Fav05";
			this.toolItemTankFilter_Fav05.Size = new System.Drawing.Size(205, 22);
			this.toolItemTankFilter_Fav05.Text = "Favourite item #5";
			this.toolItemTankFilter_Fav05.Visible = false;
			this.toolItemTankFilter_Fav05.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.toolItemTankFilter_Fav05.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Fav06
			// 
			this.toolItemTankFilter_Fav06.Name = "toolItemTankFilter_Fav06";
			this.toolItemTankFilter_Fav06.Size = new System.Drawing.Size(205, 22);
			this.toolItemTankFilter_Fav06.Text = "Favourite item #6";
			this.toolItemTankFilter_Fav06.Visible = false;
			this.toolItemTankFilter_Fav06.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.toolItemTankFilter_Fav06.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Fav07
			// 
			this.toolItemTankFilter_Fav07.Name = "toolItemTankFilter_Fav07";
			this.toolItemTankFilter_Fav07.Size = new System.Drawing.Size(205, 22);
			this.toolItemTankFilter_Fav07.Text = "Favourite item #7";
			this.toolItemTankFilter_Fav07.Visible = false;
			this.toolItemTankFilter_Fav07.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.toolItemTankFilter_Fav07.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Fav08
			// 
			this.toolItemTankFilter_Fav08.Name = "toolItemTankFilter_Fav08";
			this.toolItemTankFilter_Fav08.Size = new System.Drawing.Size(205, 22);
			this.toolItemTankFilter_Fav08.Text = "Favourite item #8";
			this.toolItemTankFilter_Fav08.Visible = false;
			this.toolItemTankFilter_Fav08.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.toolItemTankFilter_Fav08.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Fav09
			// 
			this.toolItemTankFilter_Fav09.Name = "toolItemTankFilter_Fav09";
			this.toolItemTankFilter_Fav09.Size = new System.Drawing.Size(205, 22);
			this.toolItemTankFilter_Fav09.Text = "Favourite item #9";
			this.toolItemTankFilter_Fav09.Visible = false;
			this.toolItemTankFilter_Fav09.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.toolItemTankFilter_Fav09.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Fav10
			// 
			this.toolItemTankFilter_Fav10.Name = "toolItemTankFilter_Fav10";
			this.toolItemTankFilter_Fav10.Size = new System.Drawing.Size(205, 22);
			this.toolItemTankFilter_Fav10.Text = "Favourite item #10";
			this.toolItemTankFilter_Fav10.Visible = false;
			this.toolItemTankFilter_Fav10.Click += new System.EventHandler(this.toolItem_Fav_Clicked);
			this.toolItemTankFilter_Fav10.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(202, 6);
			// 
			// toolItemTankFilter_EditFavList
			// 
			this.toolItemTankFilter_EditFavList.Name = "toolItemTankFilter_EditFavList";
			this.toolItemTankFilter_EditFavList.Size = new System.Drawing.Size(205, 22);
			this.toolItemTankFilter_EditFavList.Text = "Edit Favourite Tank List...";
			this.toolItemTankFilter_EditFavList.Click += new System.EventHandler(this.toolItemTankFilter_EditFavList_Click);
			// 
			// toolItemMode
			// 
			this.toolItemMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemModeAll,
            this.toolStripSeparator16,
            this.toolItemModeRandomCompanyClan,
            this.toolItemModeTeam,
            this.toolItemModeHistorical,
            this.toolStripSeparator15,
            this.toolItemModeRandom,
            this.toolItemModeCompany,
            this.toolItemModeClan,
            this.toolItemModeSpecialInfo});
			this.toolItemMode.Image = ((System.Drawing.Image)(resources.GetObject("toolItemMode.Image")));
			this.toolItemMode.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemMode.Name = "toolItemMode";
			this.toolItemMode.ShowDropDownArrow = false;
			this.toolItemMode.Size = new System.Drawing.Size(80, 22);
			this.toolItemMode.Text = "All modes";
			// 
			// toolItemModeAll
			// 
			this.toolItemModeAll.Name = "toolItemModeAll";
			this.toolItemModeAll.Size = new System.Drawing.Size(227, 22);
			this.toolItemModeAll.Tag = "All";
			this.toolItemModeAll.Text = "All modes";
			this.toolItemModeAll.Click += new System.EventHandler(this.toolItemMode_Click);
			this.toolItemModeAll.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator16
			// 
			this.toolStripSeparator16.Name = "toolStripSeparator16";
			this.toolStripSeparator16.Size = new System.Drawing.Size(224, 6);
			// 
			// toolItemModeRandomCompanyClan
			// 
			this.toolItemModeRandomCompanyClan.Name = "toolItemModeRandomCompanyClan";
			this.toolItemModeRandomCompanyClan.Size = new System.Drawing.Size(227, 22);
			this.toolItemModeRandomCompanyClan.Tag = "Mode15";
			this.toolItemModeRandomCompanyClan.Text = "Random, Company and Clan";
			this.toolItemModeRandomCompanyClan.Click += new System.EventHandler(this.toolItemMode_Click);
			this.toolItemModeRandomCompanyClan.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemModeTeam
			// 
			this.toolItemModeTeam.Name = "toolItemModeTeam";
			this.toolItemModeTeam.Size = new System.Drawing.Size(227, 22);
			this.toolItemModeTeam.Tag = "Mode7";
			this.toolItemModeTeam.Text = "Team Battles";
			this.toolItemModeTeam.Click += new System.EventHandler(this.toolItemMode_Click);
			this.toolItemModeTeam.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemModeHistorical
			// 
			this.toolItemModeHistorical.Name = "toolItemModeHistorical";
			this.toolItemModeHistorical.Size = new System.Drawing.Size(227, 22);
			this.toolItemModeHistorical.Tag = "Historical";
			this.toolItemModeHistorical.Text = "Historical Battles";
			this.toolItemModeHistorical.Click += new System.EventHandler(this.toolItemMode_Click);
			this.toolItemModeHistorical.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator15
			// 
			this.toolStripSeparator15.Name = "toolStripSeparator15";
			this.toolStripSeparator15.Size = new System.Drawing.Size(224, 6);
			// 
			// toolItemModeRandom
			// 
			this.toolItemModeRandom.Name = "toolItemModeRandom";
			this.toolItemModeRandom.Size = new System.Drawing.Size(227, 22);
			this.toolItemModeRandom.Tag = "Random";
			this.toolItemModeRandom.Text = "Random Battles (*)";
			this.toolItemModeRandom.Click += new System.EventHandler(this.toolItemMode_Click);
			this.toolItemModeRandom.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemModeCompany
			// 
			this.toolItemModeCompany.Name = "toolItemModeCompany";
			this.toolItemModeCompany.Size = new System.Drawing.Size(227, 22);
			this.toolItemModeCompany.Tag = "Company";
			this.toolItemModeCompany.Text = "Tank Company Battles (*)";
			this.toolItemModeCompany.Click += new System.EventHandler(this.toolItemMode_Click);
			this.toolItemModeCompany.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemModeClan
			// 
			this.toolItemModeClan.Name = "toolItemModeClan";
			this.toolItemModeClan.Size = new System.Drawing.Size(227, 22);
			this.toolItemModeClan.Tag = "Clan";
			this.toolItemModeClan.Text = "Clan War Battles (*)";
			this.toolItemModeClan.Click += new System.EventHandler(this.toolItemMode_Click);
			this.toolItemModeClan.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemModeSpecialInfo
			// 
			this.toolItemModeSpecialInfo.Name = "toolItemModeSpecialInfo";
			this.toolItemModeSpecialInfo.Size = new System.Drawing.Size(227, 22);
			this.toolItemModeSpecialInfo.Text = "* Stats Information";
			this.toolItemModeSpecialInfo.Click += new System.EventHandler(this.toolItemModeSpecialInfo_Click);
			// 
			// toolItemBattles
			// 
			this.toolItemBattles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemBattles1d,
            this.toolItemBattlesYesterday,
            this.toolItemBattles3d,
            this.toolStripSeparator10,
            this.toolItemBattles1w,
            this.toolItemBattles2w,
            this.toolStripSeparator7,
            this.toolItemBattles1m,
            this.toolItemBattles3m,
            this.toolItemBattles6m,
            this.toolStripSeparator9,
            this.toolItemBattles1y,
            this.toolItemBattles2y,
            this.toolStripSeparator14,
            this.toolItemBattlesAll});
			this.toolItemBattles.Image = ((System.Drawing.Image)(resources.GetObject("toolItemBattles.Image")));
			this.toolItemBattles.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemBattles.Name = "toolItemBattles";
			this.toolItemBattles.ShowDropDownArrow = false;
			this.toolItemBattles.Size = new System.Drawing.Size(106, 22);
			this.toolItemBattles.Text = "Today\'s Battles";
			// 
			// toolItemBattles1d
			// 
			this.toolItemBattles1d.Checked = true;
			this.toolItemBattles1d.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolItemBattles1d.Name = "toolItemBattles1d";
			this.toolItemBattles1d.Size = new System.Drawing.Size(181, 22);
			this.toolItemBattles1d.Text = "Today\'s Battles";
			this.toolItemBattles1d.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.toolItemBattles1d.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemBattlesYesterday
			// 
			this.toolItemBattlesYesterday.Name = "toolItemBattlesYesterday";
			this.toolItemBattlesYesterday.Size = new System.Drawing.Size(181, 22);
			this.toolItemBattlesYesterday.Text = "Yesterday\'s Battles";
			this.toolItemBattlesYesterday.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.toolItemBattlesYesterday.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemBattles3d
			// 
			this.toolItemBattles3d.Name = "toolItemBattles3d";
			this.toolItemBattles3d.Size = new System.Drawing.Size(181, 22);
			this.toolItemBattles3d.Text = "Battles Last 3 Days";
			this.toolItemBattles3d.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.toolItemBattles3d.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(178, 6);
			// 
			// toolItemBattles1w
			// 
			this.toolItemBattles1w.Name = "toolItemBattles1w";
			this.toolItemBattles1w.Size = new System.Drawing.Size(181, 22);
			this.toolItemBattles1w.Text = "Battles Last Week";
			this.toolItemBattles1w.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.toolItemBattles1w.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemBattles2w
			// 
			this.toolItemBattles2w.Name = "toolItemBattles2w";
			this.toolItemBattles2w.Size = new System.Drawing.Size(181, 22);
			this.toolItemBattles2w.Text = "Battles Last 2 Weeks";
			this.toolItemBattles2w.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.toolItemBattles2w.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(178, 6);
			// 
			// toolItemBattles1m
			// 
			this.toolItemBattles1m.Name = "toolItemBattles1m";
			this.toolItemBattles1m.Size = new System.Drawing.Size(181, 22);
			this.toolItemBattles1m.Text = "Battles Last Month";
			this.toolItemBattles1m.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.toolItemBattles1m.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemBattles3m
			// 
			this.toolItemBattles3m.Name = "toolItemBattles3m";
			this.toolItemBattles3m.Size = new System.Drawing.Size(181, 22);
			this.toolItemBattles3m.Text = "Battles Last 3 Month";
			this.toolItemBattles3m.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.toolItemBattles3m.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemBattles6m
			// 
			this.toolItemBattles6m.Name = "toolItemBattles6m";
			this.toolItemBattles6m.Size = new System.Drawing.Size(181, 22);
			this.toolItemBattles6m.Text = "Battles Last 6 Month";
			this.toolItemBattles6m.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.toolItemBattles6m.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(178, 6);
			// 
			// toolItemBattles1y
			// 
			this.toolItemBattles1y.Name = "toolItemBattles1y";
			this.toolItemBattles1y.Size = new System.Drawing.Size(181, 22);
			this.toolItemBattles1y.Text = "Battles Last Year";
			this.toolItemBattles1y.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.toolItemBattles1y.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemBattles2y
			// 
			this.toolItemBattles2y.Name = "toolItemBattles2y";
			this.toolItemBattles2y.Size = new System.Drawing.Size(181, 22);
			this.toolItemBattles2y.Text = "Battles Last 2 Years";
			this.toolItemBattles2y.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.toolItemBattles2y.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator14
			// 
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			this.toolStripSeparator14.Size = new System.Drawing.Size(178, 6);
			// 
			// toolItemBattlesAll
			// 
			this.toolItemBattlesAll.Name = "toolItemBattlesAll";
			this.toolItemBattlesAll.Size = new System.Drawing.Size(181, 22);
			this.toolItemBattlesAll.Text = "All Battles";
			this.toolItemBattlesAll.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.toolItemBattlesAll.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
			// 
			// toolItemSettings
			// 
			this.toolItemSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemSettingsRun,
            this.toolItemSettingsDossierOptions,
            this.toolStripSeparator13,
            this.toolItemUpdateDataFromAPI,
            this.toolItemImportBattlesFromWotStat,
            this.toolItemShowDbTables,
            this.toolStripSeparator2,
            this.toolItemSettingsApp});
			this.toolItemSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolItemSettings.Image")));
			this.toolItemSettings.Name = "toolItemSettings";
			this.toolItemSettings.ShowDropDownArrow = false;
			this.toolItemSettings.Size = new System.Drawing.Size(20, 22);
			this.toolItemSettings.Text = "Settings";
			// 
			// toolItemSettingsRun
			// 
			this.toolItemSettingsRun.Name = "toolItemSettingsRun";
			this.toolItemSettingsRun.Size = new System.Drawing.Size(263, 22);
			this.toolItemSettingsRun.Text = "Listen To Dossier File";
			this.toolItemSettingsRun.Click += new System.EventHandler(this.toolItemSettingsRun_Click);
			this.toolItemSettingsRun.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemSettingsDossierOptions
			// 
			this.toolItemSettingsDossierOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemSettingsRunManual,
            this.toolItemSettingsForceUpdateFromPrev});
			this.toolItemSettingsDossierOptions.Name = "toolItemSettingsDossierOptions";
			this.toolItemSettingsDossierOptions.Size = new System.Drawing.Size(263, 22);
			this.toolItemSettingsDossierOptions.Text = "Dossier File Options";
			// 
			// toolItemSettingsRunManual
			// 
			this.toolItemSettingsRunManual.Name = "toolItemSettingsRunManual";
			this.toolItemSettingsRunManual.Size = new System.Drawing.Size(286, 22);
			this.toolItemSettingsRunManual.Text = "Normal Dossier File Check";
			this.toolItemSettingsRunManual.Click += new System.EventHandler(this.toolItemSettingsRunManual_Click);
			// 
			// toolItemSettingsForceUpdateFromPrev
			// 
			this.toolItemSettingsForceUpdateFromPrev.Name = "toolItemSettingsForceUpdateFromPrev";
			this.toolItemSettingsForceUpdateFromPrev.Size = new System.Drawing.Size(286, 22);
			this.toolItemSettingsForceUpdateFromPrev.Text = "Force Update All Data Dossier File Check";
			this.toolItemSettingsForceUpdateFromPrev.Click += new System.EventHandler(this.toolItemSettingsForceUpdateFromPrev_Click);
			// 
			// toolStripSeparator13
			// 
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(260, 6);
			// 
			// toolItemUpdateDataFromAPI
			// 
			this.toolItemUpdateDataFromAPI.Name = "toolItemUpdateDataFromAPI";
			this.toolItemUpdateDataFromAPI.Size = new System.Drawing.Size(263, 22);
			this.toolItemUpdateDataFromAPI.Text = "Update Data from API...";
			this.toolItemUpdateDataFromAPI.Click += new System.EventHandler(this.toolItemUpdateDataFromAPI_Click);
			// 
			// toolItemImportBattlesFromWotStat
			// 
			this.toolItemImportBattlesFromWotStat.Name = "toolItemImportBattlesFromWotStat";
			this.toolItemImportBattlesFromWotStat.Size = new System.Drawing.Size(263, 22);
			this.toolItemImportBattlesFromWotStat.Text = "Import battles from WoT Statistics...";
			this.toolItemImportBattlesFromWotStat.Click += new System.EventHandler(this.toolItemImportBattlesFromWotStat_Click);
			// 
			// toolItemShowDbTables
			// 
			this.toolItemShowDbTables.Name = "toolItemShowDbTables";
			this.toolItemShowDbTables.Size = new System.Drawing.Size(263, 22);
			this.toolItemShowDbTables.Text = "Show Database Tables...";
			this.toolItemShowDbTables.Click += new System.EventHandler(this.toolItemShowDbTables_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(260, 6);
			// 
			// toolItemSettingsApp
			// 
			this.toolItemSettingsApp.Name = "toolItemSettingsApp";
			this.toolItemSettingsApp.Size = new System.Drawing.Size(263, 22);
			this.toolItemSettingsApp.Text = "&Application Settings...";
			this.toolItemSettingsApp.Click += new System.EventHandler(this.toolItemSettingsApp_Click);
			// 
			// toolItemViewChart
			// 
			this.toolItemViewChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemViewChart.Image = ((System.Drawing.Image)(resources.GetObject("toolItemViewChart.Image")));
			this.toolItemViewChart.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemViewChart.Name = "toolItemViewChart";
			this.toolItemViewChart.Size = new System.Drawing.Size(23, 22);
			this.toolItemViewChart.Text = "toolStripButton1";
			this.toolItemViewChart.Click += new System.EventHandler(this.toolItemViewChart_Click);
			// 
			// toolItemHelp
			// 
			this.toolItemHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolItemHelp.Image")));
			this.toolItemHelp.Name = "toolItemHelp";
			this.toolItemHelp.Size = new System.Drawing.Size(23, 22);
			this.toolItemHelp.Click += new System.EventHandler(this.toolItemHelp_Click);
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
			this.ClientSize = new System.Drawing.Size(812, 431);
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
			this.toolStripEx1.ResumeLayout(false);
			this.toolStripEx1.PerformLayout();
			this.panelMainArea.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridMain)).EndInit();
			this.panelInfo.ResumeLayout(false);
			this.panelInfo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picIS7)).EndInit();
			this.toolMain.ResumeLayout(false);
			this.toolMain.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer timerStatus2;
		private System.Windows.Forms.ToolStrip toolMain;
		private System.Windows.Forms.ToolStripButton toolItemRefresh;
		private System.Windows.Forms.ToolStripLabel toolItemViewLabel;
		private System.Windows.Forms.ToolStripButton toolItemViewOverall;
		private System.Windows.Forms.ToolStripButton toolItemViewTankInfo;
		private System.Windows.Forms.ToolStripButton toolItemViewBattles;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripSeparator toolItemRefreshSeparator;
		private System.Windows.Forms.ToolStripDropDownButton toolItemSettings;
		private System.Windows.Forms.ToolStripButton toolItemHelp;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.ToolStripMenuItem toolItemSettingsRun;
		private System.Windows.Forms.ToolStripMenuItem toolItemSettingsDossierOptions;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
		private System.Windows.Forms.ToolStripMenuItem toolItemSettingsApp;
		private System.Windows.Forms.ToolStripMenuItem toolItemSettingsRunManual;
		private System.Windows.Forms.ToolStripMenuItem toolItemSettingsForceUpdateFromPrev;
		private System.Windows.Forms.Label lblOverView;
		private System.Windows.Forms.PictureBox picIS7;
		private System.Windows.Forms.Timer timerPanelSlide;
		private System.Windows.Forms.ToolStripMenuItem toolItemShowDbTables;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem toolItemImportBattlesFromWotStat;
		private System.IO.FileSystemWatcher fileSystemWatcherNewBattle;
		private BadForm MainTheme;
		private System.Windows.Forms.Label lblStatus2;
		private System.Windows.Forms.Label lblStatus1;
		private System.Windows.Forms.ToolStripDropDownButton toolItemBattles;
		private System.Windows.Forms.ToolStripMenuItem toolItemBattles1d;
		private System.Windows.Forms.ToolStripMenuItem toolItemBattles3d;
		private System.Windows.Forms.ToolStripMenuItem toolItemBattles1w;
		private System.Windows.Forms.ToolStripMenuItem toolItemBattles1m;
		private System.Windows.Forms.ToolStripMenuItem toolItemBattles1y;
		private System.Windows.Forms.ToolStripMenuItem toolItemBattlesAll;
		private System.Windows.Forms.ToolStripDropDownButton toolItemTankFilter;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_All;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Tier;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Tier1;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Tier2;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Tier3;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Tier4;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Tier5;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Tier6;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Tier7;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Tier8;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Tier9;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Tier10;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Country;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_CountryChina;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_CountryFrance;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_CountryGermany;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_CountryUK;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_CountryUSA;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_CountryUSSR;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_CountryJapan;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Type;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_TypeLT;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_TypeMT;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_TypeHT;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_TypeTD;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_TypeSPG;
		private System.Windows.Forms.ToolStripSeparator toolItemTankFilter_FavSeparator;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Fav01;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Fav02;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Fav03;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Fav04;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Fav05;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Fav06;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Fav07;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Fav08;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Fav09;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_Fav10;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem toolItemTankFilter_EditFavList;
		private System.Windows.Forms.Panel panelMainArea;
		private System.Windows.Forms.Panel panelInfo;
		private System.Windows.Forms.ImageList imageListToolStrip;
		private System.Windows.Forms.Label lblStatusRowCount;
		private System.Windows.Forms.DataGridView dataGridMain;
		private BadScrollBar scrollY;
		private BadScrollBar scrollX;
		private BadScrollBarCorner scrollCorner;
		private System.Windows.Forms.ToolStripDropDownButton toolItemColumnSelect;
		private System.Windows.Forms.ToolStripMenuItem toolItemColumnSelect_01;
		private System.Windows.Forms.ToolStripMenuItem toolItemColumnSelect_04;
		private System.Windows.Forms.ToolStripMenuItem toolItemColumnSelect_05;
		private System.Windows.Forms.ToolStripMenuItem toolItemColumnSelect_13;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem toolItemColumnSelect_Edit;
		private System.Windows.Forms.ToolStripMenuItem toolItemColumnSelect_03;
		private System.Windows.Forms.ToolStripMenuItem toolItemColumnSelect_02;
		private System.Windows.Forms.ToolStripMenuItem toolItemColumnSelect_06;
		private System.Windows.Forms.ToolStripMenuItem toolItemColumnSelect_07;
		private System.Windows.Forms.ToolStripMenuItem toolItemColumnSelect_08;
		private System.Windows.Forms.ToolStripMenuItem toolItemColumnSelect_09;
		private System.Windows.Forms.ToolStripMenuItem toolItemColumnSelect_10;
		private System.Windows.Forms.ToolStripMenuItem toolItemColumnSelect_11;
		private System.Windows.Forms.ToolStripMenuItem toolItemColumnSelect_12;
		private System.Windows.Forms.ToolStripMenuItem toolItemBattlesYesterday;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		private System.Windows.Forms.ToolStripMenuItem toolItemBattles2w;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem toolItemBattles3m;
		private System.Windows.Forms.ToolStripMenuItem toolItemBattles6m;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem toolItemBattles2y;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
		private System.Windows.Forms.ToolStripMenuItem toolItemUpdateDataFromAPI;
		private System.Windows.Forms.ToolStripMenuItem toolItemColumnSelect_14;
		private System.Windows.Forms.ToolStripMenuItem toolItemColumnSelect_15;
		private System.Windows.Forms.ToolStripDropDownButton toolItemMode;
		private System.Windows.Forms.ToolStripMenuItem toolItemModeAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
		private System.Windows.Forms.ToolStripMenuItem toolItemModeRandom;
		private System.Windows.Forms.ToolStripMenuItem toolItemModeTeam;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
		private System.Windows.Forms.ToolStripMenuItem toolItemModeRandomCompanyClan;
		private System.Windows.Forms.ToolStripMenuItem toolItemModeClan;
		private System.Windows.Forms.ToolStripMenuItem toolItemModeCompany;
		private System.Windows.Forms.ToolStripMenuItem toolItemModeHistorical;
		private System.Windows.Forms.ToolStripMenuItem toolItemModeSpecialInfo;
		private System.Windows.Forms.ToolStripButton toolItemViewChart;
		private Code.ToolStripEx toolStripEx1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripButton mViewOverall;
		private System.Windows.Forms.ToolStripButton mViewTankInfo;
		private System.Windows.Forms.ToolStripButton mViewBattles;
		private System.Windows.Forms.ToolStripButton mRefresh;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
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
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
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
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem62;
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
		private System.Windows.Forms.ToolStripButton mHelp;
	}
}

