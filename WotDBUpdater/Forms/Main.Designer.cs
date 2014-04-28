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
			BadThemeContainerControl.MainAreaClass mainAreaClass1 = new BadThemeContainerControl.MainAreaClass();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.timerStatus2 = new System.Windows.Forms.Timer(this.components);
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.timerPanelSlide = new System.Windows.Forms.Timer(this.components);
			this.fileSystemWatcherNewBattle = new System.IO.FileSystemWatcher();
			this.imageListToolStrip = new System.Windows.Forms.ImageList(this.components);
			this.MainTheme = new BadForm();
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
			this.toolItemColumnSelectSep = new System.Windows.Forms.ToolStripSeparator();
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
			this.toolItemBattles = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolItemBattles1d = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemBattles3d = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemBattles1w = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemBattles1m = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemBattles1y = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemBattlesAll = new System.Windows.Forms.ToolStripMenuItem();
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
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemShowDbTables = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemImportBattlesFromWotStat = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemHelp = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemTest = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolItemTest_ImportTankWn8 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTest_ProgressBar = new System.Windows.Forms.ToolStripMenuItem();
			this.toolItemTest_ViewRange = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemTest_ScrollBar = new System.Windows.Forms.ToolStripMenuItem();
			this.importDossierHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importWsDossierHistoryToDbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.testNewTankImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.testNewTurretImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lblStatus2 = new System.Windows.Forms.Label();
			this.lblStatus1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherNewBattle)).BeginInit();
			this.MainTheme.SuspendLayout();
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
			this.MainTheme.Controls.Add(this.lblStatusRowCount);
			this.MainTheme.Controls.Add(this.panelMainArea);
			this.MainTheme.Controls.Add(this.toolMain);
			this.MainTheme.Controls.Add(this.lblStatus2);
			this.MainTheme.Controls.Add(this.lblStatus1);
			this.MainTheme.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
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
			this.MainTheme.Size = new System.Drawing.Size(670, 431);
			this.MainTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("MainTheme.SystemExitImage")));
			this.MainTheme.SystemMaximizeImage = ((System.Drawing.Image)(resources.GetObject("MainTheme.SystemMaximizeImage")));
			this.MainTheme.SystemMinimizeImage = ((System.Drawing.Image)(resources.GetObject("MainTheme.SystemMinimizeImage")));
			this.MainTheme.TabIndex = 18;
			this.MainTheme.Text = "Argus - World of Tanks Statistics";
			this.MainTheme.TitleHeight = 53;
			// 
			// lblStatusRowCount
			// 
			this.lblStatusRowCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblStatusRowCount.BackColor = System.Drawing.Color.Transparent;
			this.lblStatusRowCount.ForeColor = System.Drawing.Color.DarkGray;
			this.lblStatusRowCount.Location = new System.Drawing.Point(581, 411);
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
			this.toolItemBattles,
			this.toolStripSeparator8,
			this.toolItemSettings,
			this.toolItemHelp,
			this.toolStripSeparator5,
			this.toolItemTest});
			this.toolMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolMain.Location = new System.Drawing.Point(9, 29);
			this.toolMain.Name = "toolMain";
			this.toolMain.Size = new System.Drawing.Size(515, 25);
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
			this.toolItemViewOverall.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolItemViewOverall.Image = ((System.Drawing.Image)(resources.GetObject("toolItemViewOverall.Image")));
			this.toolItemViewOverall.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemViewOverall.Name = "toolItemViewOverall";
			this.toolItemViewOverall.Size = new System.Drawing.Size(60, 22);
			this.toolItemViewOverall.Text = "&Overview";
			this.toolItemViewOverall.ToolTipText = " ";
			this.toolItemViewOverall.Click += new System.EventHandler(this.toolItemViewSelected_Click);
			// 
			// toolItemViewTankInfo
			// 
			this.toolItemViewTankInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolItemViewTankInfo.Image = ((System.Drawing.Image)(resources.GetObject("toolItemViewTankInfo.Image")));
			this.toolItemViewTankInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemViewTankInfo.Name = "toolItemViewTankInfo";
			this.toolItemViewTankInfo.Size = new System.Drawing.Size(42, 22);
			this.toolItemViewTankInfo.Text = "&Tanks";
			this.toolItemViewTankInfo.Click += new System.EventHandler(this.toolItemViewSelected_Click);
			// 
			// toolItemViewBattles
			// 
			this.toolItemViewBattles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolItemViewBattles.Image = ((System.Drawing.Image)(resources.GetObject("toolItemViewBattles.Image")));
			this.toolItemViewBattles.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemViewBattles.Name = "toolItemViewBattles";
			this.toolItemViewBattles.Size = new System.Drawing.Size(46, 22);
			this.toolItemViewBattles.Text = "&Battles";
			this.toolItemViewBattles.Click += new System.EventHandler(this.toolItemViewSelected_Click);
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
			this.toolItemColumnSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemColumnSelect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolItemColumnSelect_01,
			this.toolItemColumnSelect_02,
			this.toolItemColumnSelect_03,
			this.toolItemColumnSelectSep,
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
			this.toolStripSeparator3,
			this.toolItemColumnSelect_Edit});
			this.toolItemColumnSelect.Image = ((System.Drawing.Image)(resources.GetObject("toolItemColumnSelect.Image")));
			this.toolItemColumnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemColumnSelect.Name = "toolItemColumnSelect";
			this.toolItemColumnSelect.ShowDropDownArrow = false;
			this.toolItemColumnSelect.Size = new System.Drawing.Size(20, 22);
			this.toolItemColumnSelect.Text = "toolStripDropDownButton1";
			// 
			// toolItemColumnSelect_01
			// 
			this.toolItemColumnSelect_01.Checked = true;
			this.toolItemColumnSelect_01.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolItemColumnSelect_01.Name = "toolItemColumnSelect_01";
			this.toolItemColumnSelect_01.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_01.Text = "Pre Defined #1";
			this.toolItemColumnSelect_01.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_01.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_02
			// 
			this.toolItemColumnSelect_02.Name = "toolItemColumnSelect_02";
			this.toolItemColumnSelect_02.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_02.Text = "Pre Defined #2";
			this.toolItemColumnSelect_02.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_02.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_03
			// 
			this.toolItemColumnSelect_03.Name = "toolItemColumnSelect_03";
			this.toolItemColumnSelect_03.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_03.Text = "Pre Defined #3";
			this.toolItemColumnSelect_03.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_03.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelectSep
			// 
			this.toolItemColumnSelectSep.Name = "toolItemColumnSelectSep";
			this.toolItemColumnSelectSep.Size = new System.Drawing.Size(179, 6);
			// 
			// toolItemColumnSelect_04
			// 
			this.toolItemColumnSelect_04.Name = "toolItemColumnSelect_04";
			this.toolItemColumnSelect_04.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_04.Text = "User Defined #4";
			this.toolItemColumnSelect_04.Visible = false;
			this.toolItemColumnSelect_04.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_04.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_05
			// 
			this.toolItemColumnSelect_05.Name = "toolItemColumnSelect_05";
			this.toolItemColumnSelect_05.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_05.Text = "User Defined #5";
			this.toolItemColumnSelect_05.Visible = false;
			this.toolItemColumnSelect_05.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_05.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemColumnSelect_06
			// 
			this.toolItemColumnSelect_06.Name = "toolItemColumnSelect_06";
			this.toolItemColumnSelect_06.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_06.Text = "User Defined #6";
			this.toolItemColumnSelect_06.Visible = false;
			// 
			// toolItemColumnSelect_07
			// 
			this.toolItemColumnSelect_07.Name = "toolItemColumnSelect_07";
			this.toolItemColumnSelect_07.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_07.Text = "User Defined #7";
			this.toolItemColumnSelect_07.Visible = false;
			// 
			// toolItemColumnSelect_08
			// 
			this.toolItemColumnSelect_08.Name = "toolItemColumnSelect_08";
			this.toolItemColumnSelect_08.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_08.Text = "User Defined #8";
			this.toolItemColumnSelect_08.Visible = false;
			// 
			// toolItemColumnSelect_09
			// 
			this.toolItemColumnSelect_09.Name = "toolItemColumnSelect_09";
			this.toolItemColumnSelect_09.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_09.Text = "User Defined #9";
			this.toolItemColumnSelect_09.Visible = false;
			// 
			// toolItemColumnSelect_10
			// 
			this.toolItemColumnSelect_10.Name = "toolItemColumnSelect_10";
			this.toolItemColumnSelect_10.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_10.Text = "User Defined #10";
			this.toolItemColumnSelect_10.Visible = false;
			// 
			// toolItemColumnSelect_11
			// 
			this.toolItemColumnSelect_11.Name = "toolItemColumnSelect_11";
			this.toolItemColumnSelect_11.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_11.Text = "User Defined #11";
			this.toolItemColumnSelect_11.Visible = false;
			// 
			// toolItemColumnSelect_12
			// 
			this.toolItemColumnSelect_12.Name = "toolItemColumnSelect_12";
			this.toolItemColumnSelect_12.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_12.Text = "User Defined #12";
			this.toolItemColumnSelect_12.Visible = false;
			// 
			// toolItemColumnSelect_13
			// 
			this.toolItemColumnSelect_13.Name = "toolItemColumnSelect_13";
			this.toolItemColumnSelect_13.Size = new System.Drawing.Size(182, 22);
			this.toolItemColumnSelect_13.Text = "User Defined #13";
			this.toolItemColumnSelect_13.Visible = false;
			this.toolItemColumnSelect_13.Click += new System.EventHandler(this.toolItemColumnSelect_Click);
			this.toolItemColumnSelect_13.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
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
			this.toolItemTankFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
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
			this.toolItemTankFilter.Size = new System.Drawing.Size(59, 22);
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
			this.toolItemTankFilter_CountryChina.Size = new System.Drawing.Size(122, 22);
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
			this.toolItemTankFilter_CountryFrance.Size = new System.Drawing.Size(122, 22);
			this.toolItemTankFilter_CountryFrance.Text = "France";
			this.toolItemTankFilter_CountryFrance.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.toolItemTankFilter_CountryFrance.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.toolItemTankFilter_CountryFrance.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_CountryGermany
			// 
			this.toolItemTankFilter_CountryGermany.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_CountryGermany.Image")));
			this.toolItemTankFilter_CountryGermany.Name = "toolItemTankFilter_CountryGermany";
			this.toolItemTankFilter_CountryGermany.Size = new System.Drawing.Size(122, 22);
			this.toolItemTankFilter_CountryGermany.Text = "Germany";
			this.toolItemTankFilter_CountryGermany.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.toolItemTankFilter_CountryGermany.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.toolItemTankFilter_CountryGermany.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_CountryUK
			// 
			this.toolItemTankFilter_CountryUK.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_CountryUK.Image")));
			this.toolItemTankFilter_CountryUK.Name = "toolItemTankFilter_CountryUK";
			this.toolItemTankFilter_CountryUK.Size = new System.Drawing.Size(122, 22);
			this.toolItemTankFilter_CountryUK.Text = "U.K.";
			this.toolItemTankFilter_CountryUK.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.toolItemTankFilter_CountryUK.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.toolItemTankFilter_CountryUK.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_CountryUSA
			// 
			this.toolItemTankFilter_CountryUSA.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_CountryUSA.Image")));
			this.toolItemTankFilter_CountryUSA.Name = "toolItemTankFilter_CountryUSA";
			this.toolItemTankFilter_CountryUSA.Size = new System.Drawing.Size(122, 22);
			this.toolItemTankFilter_CountryUSA.Text = "U.S.A.";
			this.toolItemTankFilter_CountryUSA.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.toolItemTankFilter_CountryUSA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.toolItemTankFilter_CountryUSA.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_CountryUSSR
			// 
			this.toolItemTankFilter_CountryUSSR.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_CountryUSSR.Image")));
			this.toolItemTankFilter_CountryUSSR.Name = "toolItemTankFilter_CountryUSSR";
			this.toolItemTankFilter_CountryUSSR.Size = new System.Drawing.Size(122, 22);
			this.toolItemTankFilter_CountryUSSR.Text = "U.S.S.R.";
			this.toolItemTankFilter_CountryUSSR.Click += new System.EventHandler(this.toolItemTankFilter_Country_Click);
			this.toolItemTankFilter_CountryUSSR.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Country_MouseDown);
			this.toolItemTankFilter_CountryUSSR.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_CountryJapan
			// 
			this.toolItemTankFilter_CountryJapan.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_CountryJapan.Image")));
			this.toolItemTankFilter_CountryJapan.Name = "toolItemTankFilter_CountryJapan";
			this.toolItemTankFilter_CountryJapan.Size = new System.Drawing.Size(122, 22);
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
			this.toolItemTankFilter_Tier1.Size = new System.Drawing.Size(86, 22);
			this.toolItemTankFilter_Tier1.Text = "1";
			this.toolItemTankFilter_Tier1.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier1.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier2
			// 
			this.toolItemTankFilter_Tier2.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier2.Image")));
			this.toolItemTankFilter_Tier2.Name = "toolItemTankFilter_Tier2";
			this.toolItemTankFilter_Tier2.Size = new System.Drawing.Size(86, 22);
			this.toolItemTankFilter_Tier2.Text = "2";
			this.toolItemTankFilter_Tier2.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier2.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier3
			// 
			this.toolItemTankFilter_Tier3.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier3.Image")));
			this.toolItemTankFilter_Tier3.Name = "toolItemTankFilter_Tier3";
			this.toolItemTankFilter_Tier3.Size = new System.Drawing.Size(86, 22);
			this.toolItemTankFilter_Tier3.Text = "3";
			this.toolItemTankFilter_Tier3.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier3.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier4
			// 
			this.toolItemTankFilter_Tier4.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier4.Image")));
			this.toolItemTankFilter_Tier4.Name = "toolItemTankFilter_Tier4";
			this.toolItemTankFilter_Tier4.Size = new System.Drawing.Size(86, 22);
			this.toolItemTankFilter_Tier4.Text = "4";
			this.toolItemTankFilter_Tier4.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier4.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier5
			// 
			this.toolItemTankFilter_Tier5.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier5.Image")));
			this.toolItemTankFilter_Tier5.Name = "toolItemTankFilter_Tier5";
			this.toolItemTankFilter_Tier5.Size = new System.Drawing.Size(86, 22);
			this.toolItemTankFilter_Tier5.Text = "5";
			this.toolItemTankFilter_Tier5.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier5.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier6
			// 
			this.toolItemTankFilter_Tier6.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier6.Image")));
			this.toolItemTankFilter_Tier6.Name = "toolItemTankFilter_Tier6";
			this.toolItemTankFilter_Tier6.Size = new System.Drawing.Size(86, 22);
			this.toolItemTankFilter_Tier6.Text = "6";
			this.toolItemTankFilter_Tier6.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier6.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier7
			// 
			this.toolItemTankFilter_Tier7.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier7.Image")));
			this.toolItemTankFilter_Tier7.Name = "toolItemTankFilter_Tier7";
			this.toolItemTankFilter_Tier7.Size = new System.Drawing.Size(86, 22);
			this.toolItemTankFilter_Tier7.Text = "7";
			this.toolItemTankFilter_Tier7.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier7.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier8
			// 
			this.toolItemTankFilter_Tier8.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier8.Image")));
			this.toolItemTankFilter_Tier8.Name = "toolItemTankFilter_Tier8";
			this.toolItemTankFilter_Tier8.Size = new System.Drawing.Size(86, 22);
			this.toolItemTankFilter_Tier8.Text = "8";
			this.toolItemTankFilter_Tier8.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier8.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier9
			// 
			this.toolItemTankFilter_Tier9.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier9.Image")));
			this.toolItemTankFilter_Tier9.Name = "toolItemTankFilter_Tier9";
			this.toolItemTankFilter_Tier9.Size = new System.Drawing.Size(86, 22);
			this.toolItemTankFilter_Tier9.Text = "9";
			this.toolItemTankFilter_Tier9.Click += new System.EventHandler(this.toolItemTankFilter_Tier_Click);
			this.toolItemTankFilter_Tier9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemTankFilter_Tier_MouseDown);
			this.toolItemTankFilter_Tier9.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemTankFilter_Tier10
			// 
			this.toolItemTankFilter_Tier10.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTankFilter_Tier10.Image")));
			this.toolItemTankFilter_Tier10.Name = "toolItemTankFilter_Tier10";
			this.toolItemTankFilter_Tier10.Size = new System.Drawing.Size(86, 22);
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
			// toolItemBattles
			// 
			this.toolItemBattles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolItemBattles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolItemBattles1d,
			this.toolItemBattles3d,
			this.toolItemBattles1w,
			this.toolItemBattles1m,
			this.toolItemBattles1y,
			this.toolItemBattlesAll});
			this.toolItemBattles.Image = ((System.Drawing.Image)(resources.GetObject("toolItemBattles.Image")));
			this.toolItemBattles.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemBattles.Name = "toolItemBattles";
			this.toolItemBattles.ShowDropDownArrow = false;
			this.toolItemBattles.Size = new System.Drawing.Size(90, 22);
			this.toolItemBattles.Text = "Today\'s Battles";
			this.toolItemBattles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolItemBattleFilter_MouseDown);
			// 
			// toolItemBattles1d
			// 
			this.toolItemBattles1d.Checked = true;
			this.toolItemBattles1d.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolItemBattles1d.Name = "toolItemBattles1d";
			this.toolItemBattles1d.Size = new System.Drawing.Size(172, 22);
			this.toolItemBattles1d.Text = "Today\'s Battles";
			this.toolItemBattles1d.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.toolItemBattles1d.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemBattles3d
			// 
			this.toolItemBattles3d.Name = "toolItemBattles3d";
			this.toolItemBattles3d.Size = new System.Drawing.Size(172, 22);
			this.toolItemBattles3d.Text = "Battles Last 3 Days";
			this.toolItemBattles3d.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.toolItemBattles3d.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemBattles1w
			// 
			this.toolItemBattles1w.Name = "toolItemBattles1w";
			this.toolItemBattles1w.Size = new System.Drawing.Size(172, 22);
			this.toolItemBattles1w.Text = "Battles Last Week";
			this.toolItemBattles1w.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.toolItemBattles1w.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemBattles1m
			// 
			this.toolItemBattles1m.Name = "toolItemBattles1m";
			this.toolItemBattles1m.Size = new System.Drawing.Size(172, 22);
			this.toolItemBattles1m.Text = "Battles Last Month";
			this.toolItemBattles1m.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.toolItemBattles1m.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemBattles1y
			// 
			this.toolItemBattles1y.Name = "toolItemBattles1y";
			this.toolItemBattles1y.Size = new System.Drawing.Size(172, 22);
			this.toolItemBattles1y.Text = "Battles Last Year";
			this.toolItemBattles1y.Click += new System.EventHandler(this.toolItemBattlesSelected_Click);
			this.toolItemBattles1y.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolItemBattlesAll
			// 
			this.toolItemBattlesAll.Name = "toolItemBattlesAll";
			this.toolItemBattlesAll.Size = new System.Drawing.Size(172, 22);
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
			this.toolItemSettingsApp,
			this.toolStripSeparator1,
			this.toolItemShowDbTables,
			this.toolStripSeparator2,
			this.toolItemImportBattlesFromWotStat});
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
			this.toolStripSeparator12,
			this.toolItemSettingsUpdateFromPrev,
			this.toolItemSettingsForceUpdateFromPrev});
			this.toolItemSettingsDossierOptions.Name = "toolItemSettingsDossierOptions";
			this.toolItemSettingsDossierOptions.Size = new System.Drawing.Size(263, 22);
			this.toolItemSettingsDossierOptions.Text = "Dossier File Options";
			// 
			// toolItemSettingsRunManual
			// 
			this.toolItemSettingsRunManual.Name = "toolItemSettingsRunManual";
			this.toolItemSettingsRunManual.Size = new System.Drawing.Size(285, 22);
			this.toolItemSettingsRunManual.Text = "Manual Dossier File Check";
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
			this.toolItemSettingsUpdateFromPrev.Text = "Normal Check Previous Dossier File";
			this.toolItemSettingsUpdateFromPrev.Click += new System.EventHandler(this.toolItemSettingsUpdateFromPrev_Click);
			// 
			// toolItemSettingsForceUpdateFromPrev
			// 
			this.toolItemSettingsForceUpdateFromPrev.Name = "toolItemSettingsForceUpdateFromPrev";
			this.toolItemSettingsForceUpdateFromPrev.Size = new System.Drawing.Size(285, 22);
			this.toolItemSettingsForceUpdateFromPrev.Text = "Force Update From Previous Dossier File";
			this.toolItemSettingsForceUpdateFromPrev.Click += new System.EventHandler(this.toolItemSettingsForceUpdateFromPrev_Click);
			// 
			// toolStripSeparator13
			// 
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(260, 6);
			// 
			// toolItemSettingsApp
			// 
			this.toolItemSettingsApp.Name = "toolItemSettingsApp";
			this.toolItemSettingsApp.Size = new System.Drawing.Size(263, 22);
			this.toolItemSettingsApp.Text = "&Application Settings...";
			this.toolItemSettingsApp.Click += new System.EventHandler(this.toolItemSettingsApp_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(260, 6);
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
			// toolItemImportBattlesFromWotStat
			// 
			this.toolItemImportBattlesFromWotStat.Name = "toolItemImportBattlesFromWotStat";
			this.toolItemImportBattlesFromWotStat.Size = new System.Drawing.Size(263, 22);
			this.toolItemImportBattlesFromWotStat.Text = "Import battles from WoT Statistics...";
			this.toolItemImportBattlesFromWotStat.Click += new System.EventHandler(this.toolItemImportBattlesFromWotStat_Click);
			// 
			// toolItemHelp
			// 
			this.toolItemHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolItemHelp.Image")));
			this.toolItemHelp.Name = "toolItemHelp";
			this.toolItemHelp.Size = new System.Drawing.Size(23, 22);
			this.toolItemHelp.Click += new System.EventHandler(this.toolItemHelp_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// toolItemTest
			// 
			this.toolItemTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolItemTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolItemTest_ImportTankWn8,
			this.toolItemTest_ProgressBar,
			this.toolItemTest_ViewRange,
			this.toolStripSeparator6,
			this.toolItemTest_ScrollBar,
			this.importDossierHistoryToolStripMenuItem,
			this.importWsDossierHistoryToDbToolStripMenuItem,
			this.testNewTankImportToolStripMenuItem,
			this.testNewTurretImportToolStripMenuItem});
			this.toolItemTest.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemTest.Name = "toolItemTest";
			this.toolItemTest.ShowDropDownArrow = false;
			this.toolItemTest.Size = new System.Drawing.Size(76, 22);
			this.toolItemTest.Text = "Admin tools";
			// 
			// toolItemTest_ImportTankWn8
			// 
			this.toolItemTest_ImportTankWn8.Name = "toolItemTest_ImportTankWn8";
			this.toolItemTest_ImportTankWn8.Size = new System.Drawing.Size(273, 22);
			this.toolItemTest_ImportTankWn8.Text = "Import Tank and WN8...";
			this.toolItemTest_ImportTankWn8.Click += new System.EventHandler(this.toolItemTest_ImportTankWn8_Click);
			// 
			// toolItemTest_ProgressBar
			// 
			this.toolItemTest_ProgressBar.Name = "toolItemTest_ProgressBar";
			this.toolItemTest_ProgressBar.Size = new System.Drawing.Size(273, 22);
			this.toolItemTest_ProgressBar.Text = "Progress Bar...";
			this.toolItemTest_ProgressBar.Click += new System.EventHandler(this.toolItemTest_ProgressBar_Click);
			// 
			// toolItemTest_ViewRange
			// 
			this.toolItemTest_ViewRange.Name = "toolItemTest_ViewRange";
			this.toolItemTest_ViewRange.Size = new System.Drawing.Size(273, 22);
			this.toolItemTest_ViewRange.Text = "View Range...";
			this.toolItemTest_ViewRange.Click += new System.EventHandler(this.toolItemTest_ViewRange_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(270, 6);
			// 
			// toolItemTest_ScrollBar
			// 
			this.toolItemTest_ScrollBar.Name = "toolItemTest_ScrollBar";
			this.toolItemTest_ScrollBar.Size = new System.Drawing.Size(273, 22);
			this.toolItemTest_ScrollBar.Text = "Scrollbar Test...";
			this.toolItemTest_ScrollBar.Click += new System.EventHandler(this.toolItemTest_ScrollBar_Click);
			// 
			// importDossierHistoryToolStripMenuItem
			// 
			this.importDossierHistoryToolStripMenuItem.Name = "importDossierHistoryToolStripMenuItem";
			this.importDossierHistoryToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
			this.importDossierHistoryToolStripMenuItem.Text = "Import dossier history";
			this.importDossierHistoryToolStripMenuItem.Click += new System.EventHandler(this.importDossierHistoryToolStripMenuItem_Click);
			// 
			// importWsDossierHistoryToDbToolStripMenuItem
			// 
			this.importWsDossierHistoryToDbToolStripMenuItem.Name = "importWsDossierHistoryToDbToolStripMenuItem";
			this.importWsDossierHistoryToDbToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
			this.importWsDossierHistoryToDbToolStripMenuItem.Text = "Import battles from ws dossier history";
			this.importWsDossierHistoryToDbToolStripMenuItem.Click += new System.EventHandler(this.importWsDossierHistoryToDbToolStripMenuItem_Click);
			// 
			// testNewTankImportToolStripMenuItem
			// 
			this.testNewTankImportToolStripMenuItem.Name = "testNewTankImportToolStripMenuItem";
			this.testNewTankImportToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
			this.testNewTankImportToolStripMenuItem.Text = "Test new tankImport";
			this.testNewTankImportToolStripMenuItem.Click += new System.EventHandler(this.testNewTankImportToolStripMenuItem_Click);
			// 
			// testNewTurretImportToolStripMenuItem
			// 
			this.testNewTurretImportToolStripMenuItem.Name = "testNewTurretImportToolStripMenuItem";
			this.testNewTurretImportToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
			this.testNewTurretImportToolStripMenuItem.Text = "Test new turretImport";
			this.testNewTurretImportToolStripMenuItem.Click += new System.EventHandler(this.testNewTurretImportToolStripMenuItem_Click);
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
			this.BackColor = System.Drawing.Color.MediumPurple;
			this.ClientSize = new System.Drawing.Size(670, 431);
			this.Controls.Add(this.MainTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(450, 250);
			this.Name = "Main";
			this.Text = "WotDBUpdater";
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.Load += new System.EventHandler(this.Main_Load);
			this.Shown += new System.EventHandler(this.Main_Shown);
			this.ResizeEnd += new System.EventHandler(this.Main_ResizeEnd);
			this.Resize += new System.EventHandler(this.Main_Resize);
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherNewBattle)).EndInit();
			this.MainTheme.ResumeLayout(false);
			this.MainTheme.PerformLayout();
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
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
		private System.Windows.Forms.ToolStripMenuItem toolItemSettingsUpdateFromPrev;
		private System.Windows.Forms.ToolStripMenuItem toolItemSettingsForceUpdateFromPrev;
		private System.Windows.Forms.Label lblOverView;
		private System.Windows.Forms.PictureBox picIS7;
		private System.Windows.Forms.Timer timerPanelSlide;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
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
		private System.Windows.Forms.ToolStripDropDownButton toolItemTest;
		private System.Windows.Forms.ToolStripMenuItem toolItemTest_ImportTankWn8;
		private System.Windows.Forms.ToolStripMenuItem toolItemTest_ProgressBar;
		private System.Windows.Forms.ToolStripMenuItem toolItemTest_ViewRange;
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
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.Panel panelMainArea;
		private System.Windows.Forms.Panel panelInfo;
		private System.Windows.Forms.ImageList imageListToolStrip;
		private System.Windows.Forms.Label lblStatusRowCount;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem toolItemTest_ScrollBar;
		private System.Windows.Forms.ToolStripMenuItem importDossierHistoryToolStripMenuItem;
		private System.Windows.Forms.DataGridView dataGridMain;
		private BadScrollBar scrollY;
		private BadScrollBar scrollX;
		private BadScrollBarCorner scrollCorner;
		private System.Windows.Forms.ToolStripMenuItem importWsDossierHistoryToDbToolStripMenuItem;
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
		private System.Windows.Forms.ToolStripMenuItem testNewTankImportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem testNewTurretImportToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolItemColumnSelectSep;
	}
}

