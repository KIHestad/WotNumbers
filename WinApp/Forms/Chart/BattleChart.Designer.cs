namespace WinApp.Forms
{
	partial class BattleChart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleChart));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.BattleChartTheme = new BadForm();
            this.mMain = new WinApp.Code.ToolStripEx(this.components);
            this.mFavourites = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mChartAdd = new System.Windows.Forms.ToolStripButton();
            this.mChartRemove = new System.Windows.Forms.ToolStripButton();
            this.mChartClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.mRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mBattleModes = new System.Windows.Forms.ToolStripDropDownButton();
            this.mBattleModesAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mBattleModesRandom = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattleModesHistorical = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattleModesTeamUnranked = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattleModesTeamRanked = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.mBattleModesGlobalMap = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattleModesSkirmishes = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattleModesStronghold = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.mBattleModesSpecial = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattleTimeFilter = new System.Windows.Forms.ToolStripDropDownButton();
            this.mBattleTimeAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.battlesLast2YearsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattleTime1Y = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mBattleTime6M = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattleTime3M = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattleTime1M = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mBattleTime2W = new System.Windows.Forms.ToolStripMenuItem();
            this.mBattleTime1W = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.mXaxisDate = new System.Windows.Forms.ToolStripButton();
            this.mXaxisBattle = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.mBullet = new System.Windows.Forms.ToolStripButton();
            this.mSpline = new System.Windows.Forms.ToolStripButton();
            this.lblFooter = new System.Windows.Forms.Label();
            this.ChartingMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.BattleChartTheme.SuspendLayout();
            this.mMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartingMain)).BeginInit();
            this.SuspendLayout();
            // 
            // BattleChartTheme
            // 
            this.BattleChartTheme.BackColor = System.Drawing.Color.Fuchsia;
            this.BattleChartTheme.Controls.Add(this.mMain);
            this.BattleChartTheme.Controls.Add(this.lblFooter);
            this.BattleChartTheme.Controls.Add(this.ChartingMain);
            this.BattleChartTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BattleChartTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BattleChartTheme.FormExitAsMinimize = false;
            this.BattleChartTheme.FormFooter = true;
            this.BattleChartTheme.FormFooterHeight = 26;
            this.BattleChartTheme.FormInnerBorder = 0;
            this.BattleChartTheme.FormMargin = 0;
            this.BattleChartTheme.Image = ((System.Drawing.Image)(resources.GetObject("BattleChartTheme.Image")));
            this.BattleChartTheme.Location = new System.Drawing.Point(0, 0);
            this.BattleChartTheme.MainArea = mainAreaClass1;
            this.BattleChartTheme.Name = "BattleChartTheme";
            this.BattleChartTheme.Resizable = true;
            this.BattleChartTheme.Size = new System.Drawing.Size(948, 496);
            this.BattleChartTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("BattleChartTheme.SystemExitImage")));
            this.BattleChartTheme.SystemMaximizeImage = ((System.Drawing.Image)(resources.GetObject("BattleChartTheme.SystemMaximizeImage")));
            this.BattleChartTheme.SystemMinimizeImage = ((System.Drawing.Image)(resources.GetObject("BattleChartTheme.SystemMinimizeImage")));
            this.BattleChartTheme.TabIndex = 0;
            this.BattleChartTheme.Text = "Chart";
            this.BattleChartTheme.TitleHeight = 26;
            // 
            // mMain
            // 
            this.mMain.AutoSize = false;
            this.mMain.Dock = System.Windows.Forms.DockStyle.None;
            this.mMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFavourites,
            this.toolStripSeparator6,
            this.mChartAdd,
            this.mChartRemove,
            this.mChartClear,
            this.toolStripSeparator11,
            this.mRefresh,
            this.toolStripSeparator1,
            this.mBattleModes,
            this.mBattleTimeFilter,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.mXaxisDate,
            this.mXaxisBattle,
            this.toolStripSeparator7,
            this.mBullet,
            this.mSpline});
            this.mMain.Location = new System.Drawing.Point(1, 26);
            this.mMain.Name = "mMain";
            this.mMain.Padding = new System.Windows.Forms.Padding(8, 0, 1, 0);
            this.mMain.Size = new System.Drawing.Size(869, 25);
            this.mMain.TabIndex = 22;
            this.mMain.Text = "toolStripEx1";
            // 
            // mFavourites
            // 
            this.mFavourites.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mFavourites.Image = ((System.Drawing.Image)(resources.GetObject("mFavourites.Image")));
            this.mFavourites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mFavourites.Name = "mFavourites";
            this.mFavourites.ShowDropDownArrow = false;
            this.mFavourites.Size = new System.Drawing.Size(81, 22);
            this.mFavourites.Text = "Favourites";
            this.mFavourites.ToolTipText = "Select Chart from Favourites";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // mChartAdd
            // 
            this.mChartAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mChartAdd.Image = ((System.Drawing.Image)(resources.GetObject("mChartAdd.Image")));
            this.mChartAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mChartAdd.Name = "mChartAdd";
            this.mChartAdd.Size = new System.Drawing.Size(49, 22);
            this.mChartAdd.Text = "Add";
            this.mChartAdd.ToolTipText = "Add new chart line";
            this.mChartAdd.Click += new System.EventHandler(this.mChartAdd_Click);
            // 
            // mChartRemove
            // 
            this.mChartRemove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mChartRemove.Image = ((System.Drawing.Image)(resources.GetObject("mChartRemove.Image")));
            this.mChartRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mChartRemove.Name = "mChartRemove";
            this.mChartRemove.Size = new System.Drawing.Size(70, 22);
            this.mChartRemove.Text = "Remove";
            this.mChartRemove.ToolTipText = "Remove chart line";
            // 
            // mChartClear
            // 
            this.mChartClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mChartClear.Image = ((System.Drawing.Image)(resources.GetObject("mChartClear.Image")));
            this.mChartClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mChartClear.Name = "mChartClear";
            this.mChartClear.Size = new System.Drawing.Size(54, 22);
            this.mChartClear.Text = "Clear";
            this.mChartClear.ToolTipText = "Clear chart by removing all chart lines";
            this.mChartClear.Click += new System.EventHandler(this.mChartClear_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // mRefresh
            // 
            this.mRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mRefresh.Image = ((System.Drawing.Image)(resources.GetObject("mRefresh.Image")));
            this.mRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mRefresh.Name = "mRefresh";
            this.mRefresh.Size = new System.Drawing.Size(23, 22);
            this.mRefresh.Text = "Refresh";
            this.mRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.mRefresh.Click += new System.EventHandler(this.mRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // mBattleModes
            // 
            this.mBattleModes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mBattleModesAll,
            this.toolStripSeparator8,
            this.mBattleModesRandom,
            this.mBattleModesHistorical,
            this.mBattleModesTeamUnranked,
            this.mBattleModesTeamRanked,
            this.toolStripSeparator9,
            this.mBattleModesGlobalMap,
            this.mBattleModesSkirmishes,
            this.mBattleModesStronghold,
            this.toolStripSeparator10,
            this.mBattleModesSpecial});
            this.mBattleModes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleModes.Image = ((System.Drawing.Image)(resources.GetObject("mBattleModes.Image")));
            this.mBattleModes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mBattleModes.Name = "mBattleModes";
            this.mBattleModes.ShowDropDownArrow = false;
            this.mBattleModes.Size = new System.Drawing.Size(80, 22);
            this.mBattleModes.Text = "All Modes";
            this.mBattleModes.ToolTipText = "Select battle modes";
            // 
            // mBattleModesAll
            // 
            this.mBattleModesAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleModesAll.Name = "mBattleModesAll";
            this.mBattleModesAll.Size = new System.Drawing.Size(160, 22);
            this.mBattleModesAll.Text = "All Modes";
            this.mBattleModesAll.Click += new System.EventHandler(this.mBattleModesChanged_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(157, 6);
            // 
            // mBattleModesRandom
            // 
            this.mBattleModesRandom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleModesRandom.Name = "mBattleModesRandom";
            this.mBattleModesRandom.Size = new System.Drawing.Size(160, 22);
            this.mBattleModesRandom.Tag = "15";
            this.mBattleModesRandom.Text = "Random";
            this.mBattleModesRandom.Click += new System.EventHandler(this.mBattleModesChanged_Click);
            // 
            // mBattleModesHistorical
            // 
            this.mBattleModesHistorical.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleModesHistorical.Name = "mBattleModesHistorical";
            this.mBattleModesHistorical.Size = new System.Drawing.Size(160, 22);
            this.mBattleModesHistorical.Tag = "Historical";
            this.mBattleModesHistorical.Text = "Historical";
            this.mBattleModesHistorical.Click += new System.EventHandler(this.mBattleModesChanged_Click);
            // 
            // mBattleModesTeamUnranked
            // 
            this.mBattleModesTeamUnranked.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleModesTeamUnranked.Name = "mBattleModesTeamUnranked";
            this.mBattleModesTeamUnranked.Size = new System.Drawing.Size(160, 22);
            this.mBattleModesTeamUnranked.Tag = "7";
            this.mBattleModesTeamUnranked.Text = "Team: Unranked";
            this.mBattleModesTeamUnranked.Click += new System.EventHandler(this.mBattleModesChanged_Click);
            // 
            // mBattleModesTeamRanked
            // 
            this.mBattleModesTeamRanked.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleModesTeamRanked.Name = "mBattleModesTeamRanked";
            this.mBattleModesTeamRanked.Size = new System.Drawing.Size(160, 22);
            this.mBattleModesTeamRanked.Tag = "7Ranked";
            this.mBattleModesTeamRanked.Text = "Team: Ranked";
            this.mBattleModesTeamRanked.Click += new System.EventHandler(this.mBattleModesChanged_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(157, 6);
            // 
            // mBattleModesGlobalMap
            // 
            this.mBattleModesGlobalMap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleModesGlobalMap.Name = "mBattleModesGlobalMap";
            this.mBattleModesGlobalMap.Size = new System.Drawing.Size(160, 22);
            this.mBattleModesGlobalMap.Tag = "GlobalMap";
            this.mBattleModesGlobalMap.Text = "Global Map";
            this.mBattleModesGlobalMap.Click += new System.EventHandler(this.mBattleModesChanged_Click);
            // 
            // mBattleModesSkirmishes
            // 
            this.mBattleModesSkirmishes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleModesSkirmishes.Name = "mBattleModesSkirmishes";
            this.mBattleModesSkirmishes.Size = new System.Drawing.Size(160, 22);
            this.mBattleModesSkirmishes.Tag = "Skirmishes";
            this.mBattleModesSkirmishes.Text = "Skirmishes";
            this.mBattleModesSkirmishes.Click += new System.EventHandler(this.mBattleModesChanged_Click);
            // 
            // mBattleModesStronghold
            // 
            this.mBattleModesStronghold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleModesStronghold.Name = "mBattleModesStronghold";
            this.mBattleModesStronghold.Size = new System.Drawing.Size(160, 22);
            this.mBattleModesStronghold.Tag = "Stronghold";
            this.mBattleModesStronghold.Text = "Stronghold";
            this.mBattleModesStronghold.Click += new System.EventHandler(this.mBattleModesChanged_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(157, 6);
            // 
            // mBattleModesSpecial
            // 
            this.mBattleModesSpecial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleModesSpecial.Name = "mBattleModesSpecial";
            this.mBattleModesSpecial.Size = new System.Drawing.Size(160, 22);
            this.mBattleModesSpecial.Tag = "Special";
            this.mBattleModesSpecial.Text = "Special Events";
            this.mBattleModesSpecial.Click += new System.EventHandler(this.mBattleModesChanged_Click);
            // 
            // mBattleTimeFilter
            // 
            this.mBattleTimeFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mBattleTimeAll,
            this.toolStripSeparator3,
            this.battlesLast2YearsToolStripMenuItem,
            this.mBattleTime1Y,
            this.toolStripSeparator5,
            this.mBattleTime6M,
            this.mBattleTime3M,
            this.mBattleTime1M,
            this.toolStripSeparator4,
            this.mBattleTime2W,
            this.mBattleTime1W});
            this.mBattleTimeFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleTimeFilter.Image = ((System.Drawing.Image)(resources.GetObject("mBattleTimeFilter.Image")));
            this.mBattleTimeFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mBattleTimeFilter.Name = "mBattleTimeFilter";
            this.mBattleTimeFilter.ShowDropDownArrow = false;
            this.mBattleTimeFilter.Size = new System.Drawing.Size(79, 22);
            this.mBattleTimeFilter.Text = "All Battles";
            this.mBattleTimeFilter.ToolTipText = "Select battle time filter";
            // 
            // mBattleTimeAll
            // 
            this.mBattleTimeAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleTimeAll.Name = "mBattleTimeAll";
            this.mBattleTimeAll.Size = new System.Drawing.Size(186, 22);
            this.mBattleTimeAll.Tag = "ALL";
            this.mBattleTimeAll.Text = "All Battles";
            this.mBattleTimeAll.Click += new System.EventHandler(this.mBattleTimeChanged_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(183, 6);
            // 
            // battlesLast2YearsToolStripMenuItem
            // 
            this.battlesLast2YearsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.battlesLast2YearsToolStripMenuItem.Name = "battlesLast2YearsToolStripMenuItem";
            this.battlesLast2YearsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.battlesLast2YearsToolStripMenuItem.Tag = "Y2";
            this.battlesLast2YearsToolStripMenuItem.Text = "Battles Last 2 Years";
            this.battlesLast2YearsToolStripMenuItem.Click += new System.EventHandler(this.mBattleTimeChanged_Click);
            // 
            // mBattleTime1Y
            // 
            this.mBattleTime1Y.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleTime1Y.Name = "mBattleTime1Y";
            this.mBattleTime1Y.Size = new System.Drawing.Size(186, 22);
            this.mBattleTime1Y.Tag = "Y1";
            this.mBattleTime1Y.Text = "Battles Last Year";
            this.mBattleTime1Y.Click += new System.EventHandler(this.mBattleTimeChanged_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(183, 6);
            // 
            // mBattleTime6M
            // 
            this.mBattleTime6M.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleTime6M.Name = "mBattleTime6M";
            this.mBattleTime6M.Size = new System.Drawing.Size(186, 22);
            this.mBattleTime6M.Tag = "M6";
            this.mBattleTime6M.Text = "Battles Last 6 Months";
            this.mBattleTime6M.Click += new System.EventHandler(this.mBattleTimeChanged_Click);
            // 
            // mBattleTime3M
            // 
            this.mBattleTime3M.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleTime3M.Name = "mBattleTime3M";
            this.mBattleTime3M.Size = new System.Drawing.Size(186, 22);
            this.mBattleTime3M.Tag = "M3";
            this.mBattleTime3M.Text = "Battles Last 3 Months";
            this.mBattleTime3M.Click += new System.EventHandler(this.mBattleTimeChanged_Click);
            // 
            // mBattleTime1M
            // 
            this.mBattleTime1M.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleTime1M.Name = "mBattleTime1M";
            this.mBattleTime1M.Size = new System.Drawing.Size(186, 22);
            this.mBattleTime1M.Tag = "M1";
            this.mBattleTime1M.Text = "Battles Last Month";
            this.mBattleTime1M.Click += new System.EventHandler(this.mBattleTimeChanged_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(183, 6);
            // 
            // mBattleTime2W
            // 
            this.mBattleTime2W.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleTime2W.Name = "mBattleTime2W";
            this.mBattleTime2W.Size = new System.Drawing.Size(186, 22);
            this.mBattleTime2W.Tag = "W2";
            this.mBattleTime2W.Text = "Battles Last 2 Weeks";
            this.mBattleTime2W.Click += new System.EventHandler(this.mBattleTimeChanged_Click);
            // 
            // mBattleTime1W
            // 
            this.mBattleTime1W.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBattleTime1W.Name = "mBattleTime1W";
            this.mBattleTime1W.Size = new System.Drawing.Size(186, 22);
            this.mBattleTime1W.Tag = "W1";
            this.mBattleTime1W.Text = "Battles Last Week";
            this.mBattleTime1W.Click += new System.EventHandler(this.mBattleTimeChanged_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(43, 22);
            this.toolStripLabel1.Text = "X-Axis:";
            // 
            // mXaxisDate
            // 
            this.mXaxisDate.Checked = true;
            this.mXaxisDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mXaxisDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mXaxisDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mXaxisDate.Image = ((System.Drawing.Image)(resources.GetObject("mXaxisDate.Image")));
            this.mXaxisDate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mXaxisDate.Name = "mXaxisDate";
            this.mXaxisDate.Size = new System.Drawing.Size(35, 22);
            this.mXaxisDate.Text = "Date";
            this.mXaxisDate.ToolTipText = "Show X-axis per date";
            this.mXaxisDate.Click += new System.EventHandler(this.mXaxis_Click);
            // 
            // mXaxisBattle
            // 
            this.mXaxisBattle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mXaxisBattle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mXaxisBattle.Image = ((System.Drawing.Image)(resources.GetObject("mXaxisBattle.Image")));
            this.mXaxisBattle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mXaxisBattle.Name = "mXaxisBattle";
            this.mXaxisBattle.Size = new System.Drawing.Size(41, 22);
            this.mXaxisBattle.Text = "Battle";
            this.mXaxisBattle.ToolTipText = "Show X-axis per battle";
            this.mXaxisBattle.Click += new System.EventHandler(this.mXaxis_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // mBullet
            // 
            this.mBullet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mBullet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mBullet.Image = ((System.Drawing.Image)(resources.GetObject("mBullet.Image")));
            this.mBullet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mBullet.Name = "mBullet";
            this.mBullet.Size = new System.Drawing.Size(46, 22);
            this.mBullet.Text = "Bullets";
            this.mBullet.ToolTipText = "Show bullets";
            this.mBullet.Click += new System.EventHandler(this.mBullet_Click);
            // 
            // mSpline
            // 
            this.mSpline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mSpline.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mSpline.Image = ((System.Drawing.Image)(resources.GetObject("mSpline.Image")));
            this.mSpline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mSpline.Name = "mSpline";
            this.mSpline.Size = new System.Drawing.Size(43, 22);
            this.mSpline.Text = "Spline";
            this.mSpline.ToolTipText = "Show line as curved";
            this.mSpline.Click += new System.EventHandler(this.mSpline_Click);
            // 
            // lblFooter
            // 
            this.lblFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFooter.AutoSize = true;
            this.lblFooter.BackColor = System.Drawing.Color.Transparent;
            this.lblFooter.ForeColor = System.Drawing.Color.DarkGray;
            this.lblFooter.Location = new System.Drawing.Point(8, 476);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(204, 13);
            this.lblFooter.TabIndex = 17;
            this.lblFooter.Text = "No chart selected, please add a chart line";
            // 
            // ChartingMain
            // 
            this.ChartingMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChartingMain.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY.MinorTickMark.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.Silver;
            chartArea1.AxisY2.LabelStyle.ForeColor = System.Drawing.Color.Silver;
            chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY2.MajorTickMark.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY2.MinorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY2.MinorTickMark.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY2.ScaleBreakStyle.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY2.ScaleBreakStyle.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.AxisY2.TitleForeColor = System.Drawing.Color.Silver;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea1.BorderColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            chartArea1.ShadowColor = System.Drawing.Color.Transparent;
            this.ChartingMain.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            legend1.ForeColor = System.Drawing.Color.Silver;
            legend1.IsDockedInsideChartArea = false;
            legend1.IsEquallySpacedItems = true;
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.ChartingMain.Legends.Add(legend1);
            this.ChartingMain.Location = new System.Drawing.Point(5, 54);
            this.ChartingMain.Name = "ChartingMain";
            this.ChartingMain.Size = new System.Drawing.Size(936, 410);
            this.ChartingMain.TabIndex = 0;
            this.ChartingMain.Text = "chart1";
            this.ChartingMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChartingMain_MouseMove);
            // 
            // BattleChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Fuchsia;
            this.ClientSize = new System.Drawing.Size(948, 496);
            this.Controls.Add(this.BattleChartTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "BattleChart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Battle Chart";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BattleChart_FormClosed);
            this.Load += new System.EventHandler(this.BattleChart_Load);
            this.Shown += new System.EventHandler(this.BattleChart_Shown);
            this.BattleChartTheme.ResumeLayout(false);
            this.BattleChartTheme.PerformLayout();
            this.mMain.ResumeLayout(false);
            this.mMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartingMain)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private BadForm BattleChartTheme;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartingMain;
		private System.Windows.Forms.Label lblFooter;
        private Code.ToolStripEx mMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton mXaxisDate;
        private System.Windows.Forms.ToolStripButton mXaxisBattle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton mBattleTimeFilter;
        private System.Windows.Forms.ToolStripMenuItem mBattleTimeAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mBattleTime1Y;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mBattleTime6M;
        private System.Windows.Forms.ToolStripMenuItem mBattleTime3M;
        private System.Windows.Forms.ToolStripMenuItem mBattleTime1M;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mBattleTime2W;
        private System.Windows.Forms.ToolStripMenuItem mBattleTime1W;
        private System.Windows.Forms.ToolStripDropDownButton mFavourites;
        private System.Windows.Forms.ToolStripButton mRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton mSpline;
        private System.Windows.Forms.ToolStripButton mBullet;
        private System.Windows.Forms.ToolStripDropDownButton mBattleModes;
        private System.Windows.Forms.ToolStripMenuItem battlesLast2YearsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mBattleModesAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem mBattleModesRandom;
        private System.Windows.Forms.ToolStripMenuItem mBattleModesHistorical;
        private System.Windows.Forms.ToolStripMenuItem mBattleModesTeamUnranked;
        private System.Windows.Forms.ToolStripMenuItem mBattleModesTeamRanked;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem mBattleModesGlobalMap;
        private System.Windows.Forms.ToolStripMenuItem mBattleModesSkirmishes;
        private System.Windows.Forms.ToolStripMenuItem mBattleModesStronghold;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem mBattleModesSpecial;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton mChartAdd;
        private System.Windows.Forms.ToolStripButton mChartRemove;
        private System.Windows.Forms.ToolStripButton mChartClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
    }
}