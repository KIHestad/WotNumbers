namespace WinApp.Forms
{
	partial class FavList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FavList));
			BadThemeContainerControl.MainAreaClass mainAreaClass1 = new BadThemeContainerControl.MainAreaClass();
			this.imageListToolStrip = new System.Windows.Forms.ImageList(this.components);
			this.FavTanksTheme = new BadForm();
			this.btnClose = new BadButton();
			this.toolColList = new System.Windows.Forms.ToolStrip();
			this.toolFavListUp = new System.Windows.Forms.ToolStripButton();
			this.toolFavListDown = new System.Windows.Forms.ToolStripButton();
			this.toolFavListVisible = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolFavListAdd = new System.Windows.Forms.ToolStripButton();
			this.toolFavListModify = new System.Windows.Forms.ToolStripButton();
			this.toolFavListDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolFavListRefresh = new System.Windows.Forms.ToolStripButton();
			this.btnRemoveAll = new BadButton();
			this.btnRemoveSelected = new BadButton();
			this.btnSelectSelected = new BadButton();
			this.btnSelectAll = new BadButton();
			this.btnFavListCancel = new BadButton();
			this.btnFavListSave = new BadButton();
			this.toolSelectedTanks = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.toolSelectedTanks_SortNation = new System.Windows.Forms.ToolStripButton();
			this.toolSelectedTanks_SortType = new System.Windows.Forms.ToolStripButton();
			this.toolSelectedTanks_SortTier = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolSelectedTanks_MoveUp = new System.Windows.Forms.ToolStripButton();
			this.toolSelectedTanks_MoveDown = new System.Windows.Forms.ToolStripButton();
			this.toolAllTanks = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.toolAllTanks_Nation = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Nation3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Nation4 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Nation1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Nation5 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Nation2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Nation0 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Nation6 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Type = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Type1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Type2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Type3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Type4 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Type5 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Tier = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Tier01 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Tier02 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Tier03 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Tier04 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Tier05 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Tier06 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Tier07 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Tier08 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Tier09 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Tier10 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllTanks_Used = new System.Windows.Forms.ToolStripButton();
			this.toolAllTanks_All = new System.Windows.Forms.ToolStripButton();
			this.scrollSelectedTanks = new BadScrollBar();
			this.dataGridSelectedTanks = new System.Windows.Forms.DataGridView();
			this.scrollAllTanks = new BadScrollBar();
			this.dataGridAllTanks = new System.Windows.Forms.DataGridView();
			this.scrollFavList = new BadScrollBar();
			this.dataGridFavList = new System.Windows.Forms.DataGridView();
			this.badGroupBox2 = new BadGroupBox();
			this.lblSelectedTanks = new BadLabel();
			this.lblAllTanks = new BadLabel();
			this.groupTanks = new BadGroupBox();
			this.FavTanksTheme.SuspendLayout();
			this.toolColList.SuspendLayout();
			this.toolSelectedTanks.SuspendLayout();
			this.toolAllTanks.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridSelectedTanks)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridAllTanks)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridFavList)).BeginInit();
			this.SuspendLayout();
			// 
			// imageListToolStrip
			// 
			this.imageListToolStrip.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListToolStrip.ImageStream")));
			this.imageListToolStrip.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListToolStrip.Images.SetKeyName(0, "check.png");
			// 
			// FavTanksTheme
			// 
			this.FavTanksTheme.BackColor = System.Drawing.Color.Fuchsia;
			this.FavTanksTheme.Controls.Add(this.btnClose);
			this.FavTanksTheme.Controls.Add(this.toolColList);
			this.FavTanksTheme.Controls.Add(this.btnRemoveAll);
			this.FavTanksTheme.Controls.Add(this.btnRemoveSelected);
			this.FavTanksTheme.Controls.Add(this.btnSelectSelected);
			this.FavTanksTheme.Controls.Add(this.btnSelectAll);
			this.FavTanksTheme.Controls.Add(this.btnFavListCancel);
			this.FavTanksTheme.Controls.Add(this.btnFavListSave);
			this.FavTanksTheme.Controls.Add(this.toolSelectedTanks);
			this.FavTanksTheme.Controls.Add(this.toolAllTanks);
			this.FavTanksTheme.Controls.Add(this.scrollSelectedTanks);
			this.FavTanksTheme.Controls.Add(this.dataGridSelectedTanks);
			this.FavTanksTheme.Controls.Add(this.scrollAllTanks);
			this.FavTanksTheme.Controls.Add(this.dataGridAllTanks);
			this.FavTanksTheme.Controls.Add(this.scrollFavList);
			this.FavTanksTheme.Controls.Add(this.dataGridFavList);
			this.FavTanksTheme.Controls.Add(this.badGroupBox2);
			this.FavTanksTheme.Controls.Add(this.lblSelectedTanks);
			this.FavTanksTheme.Controls.Add(this.lblAllTanks);
			this.FavTanksTheme.Controls.Add(this.groupTanks);
			this.FavTanksTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FavTanksTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.FavTanksTheme.FormFooter = false;
			this.FavTanksTheme.FormFooterHeight = 26;
			this.FavTanksTheme.FormInnerBorder = 3;
			this.FavTanksTheme.FormMargin = 0;
			this.FavTanksTheme.Image = null;
			this.FavTanksTheme.Location = new System.Drawing.Point(0, 0);
			this.FavTanksTheme.MainArea = mainAreaClass1;
			this.FavTanksTheme.Name = "FavTanksTheme";
			this.FavTanksTheme.Resizable = true;
			this.FavTanksTheme.Size = new System.Drawing.Size(766, 610);
			this.FavTanksTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("FavTanksTheme.SystemExitImage")));
			this.FavTanksTheme.SystemMaximizeImage = ((System.Drawing.Image)(resources.GetObject("FavTanksTheme.SystemMaximizeImage")));
			this.FavTanksTheme.SystemMinimizeImage = null;
			this.FavTanksTheme.TabIndex = 0;
			this.FavTanksTheme.Text = "Favourite Tank List";
			this.FavTanksTheme.TitleHeight = 26;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Image = null;
			this.btnClose.Location = new System.Drawing.Point(665, 563);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 19;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// toolColList
			// 
			this.toolColList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.toolColList.AutoSize = false;
			this.toolColList.Dock = System.Windows.Forms.DockStyle.None;
			this.toolColList.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolColList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolFavListUp,
			this.toolFavListDown,
			this.toolFavListVisible,
			this.toolStripSeparator2,
			this.toolFavListAdd,
			this.toolFavListModify,
			this.toolFavListDelete,
			this.toolStripSeparator3,
			this.toolFavListRefresh});
			this.toolColList.Location = new System.Drawing.Point(42, 72);
			this.toolColList.Name = "toolColList";
			this.toolColList.Size = new System.Drawing.Size(678, 25);
			this.toolColList.TabIndex = 2;
			this.toolColList.Text = "toolStrip1";
			// 
			// toolFavListUp
			// 
			this.toolFavListUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolFavListUp.Image = ((System.Drawing.Image)(resources.GetObject("toolFavListUp.Image")));
			this.toolFavListUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolFavListUp.Name = "toolFavListUp";
			this.toolFavListUp.Size = new System.Drawing.Size(23, 22);
			this.toolFavListUp.Text = "toolStripButton1";
			this.toolFavListUp.Click += new System.EventHandler(this.toolFavListUp_Click);
			// 
			// toolFavListDown
			// 
			this.toolFavListDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolFavListDown.Image = ((System.Drawing.Image)(resources.GetObject("toolFavListDown.Image")));
			this.toolFavListDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolFavListDown.Name = "toolFavListDown";
			this.toolFavListDown.Size = new System.Drawing.Size(23, 22);
			this.toolFavListDown.Text = "toolStripButton2";
			this.toolFavListDown.Click += new System.EventHandler(this.toolFavListDown_Click);
			// 
			// toolFavListVisible
			// 
			this.toolFavListVisible.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolFavListVisible.Image = ((System.Drawing.Image)(resources.GetObject("toolFavListVisible.Image")));
			this.toolFavListVisible.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolFavListVisible.Name = "toolFavListVisible";
			this.toolFavListVisible.Size = new System.Drawing.Size(36, 22);
			this.toolFavListVisible.Text = "Hide";
			this.toolFavListVisible.ToolTipText = "Hide";
			this.toolFavListVisible.Click += new System.EventHandler(this.toolFavListVisible_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolFavListAdd
			// 
			this.toolFavListAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolFavListAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolFavListAdd.Image")));
			this.toolFavListAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolFavListAdd.Name = "toolFavListAdd";
			this.toolFavListAdd.Size = new System.Drawing.Size(33, 22);
			this.toolFavListAdd.Text = "Add";
			this.toolFavListAdd.Click += new System.EventHandler(this.toolFavListAdd_Click);
			// 
			// toolFavListModify
			// 
			this.toolFavListModify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolFavListModify.Image = ((System.Drawing.Image)(resources.GetObject("toolFavListModify.Image")));
			this.toolFavListModify.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolFavListModify.Name = "toolFavListModify";
			this.toolFavListModify.Size = new System.Drawing.Size(49, 22);
			this.toolFavListModify.Text = "Modify";
			this.toolFavListModify.Click += new System.EventHandler(this.toolFavListModify_Click);
			// 
			// toolFavListDelete
			// 
			this.toolFavListDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolFavListDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolFavListDelete.Image")));
			this.toolFavListDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolFavListDelete.Name = "toolFavListDelete";
			this.toolFavListDelete.Size = new System.Drawing.Size(44, 22);
			this.toolFavListDelete.Text = "Delete";
			this.toolFavListDelete.Click += new System.EventHandler(this.toolFavListDelete_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// toolFavListRefresh
			// 
			this.toolFavListRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolFavListRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolFavListRefresh.Image")));
			this.toolFavListRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolFavListRefresh.Name = "toolFavListRefresh";
			this.toolFavListRefresh.Size = new System.Drawing.Size(23, 22);
			this.toolFavListRefresh.Text = "toolStripButton1";
			this.toolFavListRefresh.Click += new System.EventHandler(this.toolFavListRefresh_Click);
			// 
			// btnRemoveAll
			// 
			this.btnRemoveAll.Image = null;
			this.btnRemoveAll.Location = new System.Drawing.Point(410, 417);
			this.btnRemoveAll.Name = "btnRemoveAll";
			this.btnRemoveAll.Size = new System.Drawing.Size(29, 23);
			this.btnRemoveAll.TabIndex = 11;
			this.btnRemoveAll.Text = "<<";
			this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
			// 
			// btnRemoveSelected
			// 
			this.btnRemoveSelected.Image = null;
			this.btnRemoveSelected.Location = new System.Drawing.Point(410, 446);
			this.btnRemoveSelected.Name = "btnRemoveSelected";
			this.btnRemoveSelected.Size = new System.Drawing.Size(29, 23);
			this.btnRemoveSelected.TabIndex = 12;
			this.btnRemoveSelected.Text = "<";
			this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
			// 
			// btnSelectSelected
			// 
			this.btnSelectSelected.Image = null;
			this.btnSelectSelected.Location = new System.Drawing.Point(410, 359);
			this.btnSelectSelected.Name = "btnSelectSelected";
			this.btnSelectSelected.Size = new System.Drawing.Size(29, 23);
			this.btnSelectSelected.TabIndex = 9;
			this.btnSelectSelected.Text = ">";
			this.btnSelectSelected.Click += new System.EventHandler(this.btnSelectSelected_Click);
			// 
			// btnSelectAll
			// 
			this.btnSelectAll.Image = null;
			this.btnSelectAll.Location = new System.Drawing.Point(410, 388);
			this.btnSelectAll.Name = "btnSelectAll";
			this.btnSelectAll.Size = new System.Drawing.Size(29, 23);
			this.btnSelectAll.TabIndex = 10;
			this.btnSelectAll.Text = ">>";
			this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
			// 
			// btnFavListCancel
			// 
			this.btnFavListCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFavListCancel.Enabled = false;
			this.btnFavListCancel.Image = null;
			this.btnFavListCancel.Location = new System.Drawing.Point(579, 563);
			this.btnFavListCancel.Name = "btnFavListCancel";
			this.btnFavListCancel.Size = new System.Drawing.Size(80, 23);
			this.btnFavListCancel.TabIndex = 18;
			this.btnFavListCancel.Text = "Revert";
			this.btnFavListCancel.Click += new System.EventHandler(this.btnFavListCancel_Click);
			// 
			// btnFavListSave
			// 
			this.btnFavListSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFavListSave.Enabled = false;
			this.btnFavListSave.Image = null;
			this.btnFavListSave.Location = new System.Drawing.Point(493, 563);
			this.btnFavListSave.Name = "btnFavListSave";
			this.btnFavListSave.Size = new System.Drawing.Size(80, 23);
			this.btnFavListSave.TabIndex = 17;
			this.btnFavListSave.Text = "Save";
			this.btnFavListSave.Click += new System.EventHandler(this.btnFavListSave_Click);
			// 
			// toolSelectedTanks
			// 
			this.toolSelectedTanks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.toolSelectedTanks.AutoSize = false;
			this.toolSelectedTanks.Dock = System.Windows.Forms.DockStyle.None;
			this.toolSelectedTanks.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolSelectedTanks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripLabel2,
			this.toolSelectedTanks_SortNation,
			this.toolSelectedTanks_SortType,
			this.toolSelectedTanks_SortTier,
			this.toolStripSeparator1,
			this.toolSelectedTanks_MoveUp,
			this.toolSelectedTanks_MoveDown});
			this.toolSelectedTanks.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolSelectedTanks.Location = new System.Drawing.Point(453, 298);
			this.toolSelectedTanks.Name = "toolSelectedTanks";
			this.toolSelectedTanks.Size = new System.Drawing.Size(267, 25);
			this.toolSelectedTanks.Stretch = true;
			this.toolSelectedTanks.TabIndex = 14;
			this.toolSelectedTanks.Text = "toolStrip1";
			// 
			// toolStripLabel2
			// 
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(37, 22);
			this.toolStripLabel2.Text = "  Sort:";
			// 
			// toolSelectedTanks_SortNation
			// 
			this.toolSelectedTanks_SortNation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolSelectedTanks_SortNation.Image = ((System.Drawing.Image)(resources.GetObject("toolSelectedTanks_SortNation.Image")));
			this.toolSelectedTanks_SortNation.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolSelectedTanks_SortNation.Name = "toolSelectedTanks_SortNation";
			this.toolSelectedTanks_SortNation.Size = new System.Drawing.Size(23, 22);
			this.toolSelectedTanks_SortNation.Text = "toolStripButton1";
			this.toolSelectedTanks_SortNation.Click += new System.EventHandler(this.toolSelectedTanks_SortNation_Click);
			// 
			// toolSelectedTanks_SortType
			// 
			this.toolSelectedTanks_SortType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolSelectedTanks_SortType.Image = ((System.Drawing.Image)(resources.GetObject("toolSelectedTanks_SortType.Image")));
			this.toolSelectedTanks_SortType.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolSelectedTanks_SortType.Name = "toolSelectedTanks_SortType";
			this.toolSelectedTanks_SortType.Size = new System.Drawing.Size(23, 22);
			this.toolSelectedTanks_SortType.Text = "toolStripButton2";
			this.toolSelectedTanks_SortType.Click += new System.EventHandler(this.toolSelectedTanks_SortType_Click);
			// 
			// toolSelectedTanks_SortTier
			// 
			this.toolSelectedTanks_SortTier.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolSelectedTanks_SortTier.Image = ((System.Drawing.Image)(resources.GetObject("toolSelectedTanks_SortTier.Image")));
			this.toolSelectedTanks_SortTier.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolSelectedTanks_SortTier.Name = "toolSelectedTanks_SortTier";
			this.toolSelectedTanks_SortTier.Size = new System.Drawing.Size(23, 22);
			this.toolSelectedTanks_SortTier.Text = "toolStripButton3";
			this.toolSelectedTanks_SortTier.Click += new System.EventHandler(this.toolSelectedTanks_SortTier_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolSelectedTanks_MoveUp
			// 
			this.toolSelectedTanks_MoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolSelectedTanks_MoveUp.Image = ((System.Drawing.Image)(resources.GetObject("toolSelectedTanks_MoveUp.Image")));
			this.toolSelectedTanks_MoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolSelectedTanks_MoveUp.Name = "toolSelectedTanks_MoveUp";
			this.toolSelectedTanks_MoveUp.Size = new System.Drawing.Size(23, 22);
			this.toolSelectedTanks_MoveUp.Text = "toolStripButton1";
			this.toolSelectedTanks_MoveUp.Click += new System.EventHandler(this.toolSelectedTanks_MoveUp_Click);
			// 
			// toolSelectedTanks_MoveDown
			// 
			this.toolSelectedTanks_MoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolSelectedTanks_MoveDown.Image = ((System.Drawing.Image)(resources.GetObject("toolSelectedTanks_MoveDown.Image")));
			this.toolSelectedTanks_MoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolSelectedTanks_MoveDown.Name = "toolSelectedTanks_MoveDown";
			this.toolSelectedTanks_MoveDown.Size = new System.Drawing.Size(23, 22);
			this.toolSelectedTanks_MoveDown.Text = "toolStripButton2";
			this.toolSelectedTanks_MoveDown.Click += new System.EventHandler(this.toolSelectedTanks_MoveDown_Click);
			// 
			// toolAllTanks
			// 
			this.toolAllTanks.AutoSize = false;
			this.toolAllTanks.Dock = System.Windows.Forms.DockStyle.None;
			this.toolAllTanks.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolAllTanks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripLabel1,
			this.toolAllTanks_Nation,
			this.toolAllTanks_Type,
			this.toolAllTanks_Tier,
			this.toolAllTanks_Used,
			this.toolAllTanks_All});
			this.toolAllTanks.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolAllTanks.Location = new System.Drawing.Point(42, 298);
			this.toolAllTanks.Name = "toolAllTanks";
			this.toolAllTanks.Size = new System.Drawing.Size(352, 25);
			this.toolAllTanks.Stretch = true;
			this.toolAllTanks.TabIndex = 6;
			this.toolAllTanks.Text = "toolStrip1";
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(42, 22);
			this.toolStripLabel1.Text = "  Filter:";
			// 
			// toolAllTanks_Nation
			// 
			this.toolAllTanks_Nation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolAllTanks_Nation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolAllTanks_Nation3,
			this.toolAllTanks_Nation4,
			this.toolAllTanks_Nation1,
			this.toolAllTanks_Nation5,
			this.toolAllTanks_Nation2,
			this.toolAllTanks_Nation0,
			this.toolAllTanks_Nation6});
			this.toolAllTanks_Nation.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Nation.Image")));
			this.toolAllTanks_Nation.Name = "toolAllTanks_Nation";
			this.toolAllTanks_Nation.Size = new System.Drawing.Size(28, 25);
			this.toolAllTanks_Nation.Text = "Nation";
			// 
			// toolAllTanks_Nation3
			// 
			this.toolAllTanks_Nation3.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Nation3.Image")));
			this.toolAllTanks_Nation3.Name = "toolAllTanks_Nation3";
			this.toolAllTanks_Nation3.Size = new System.Drawing.Size(122, 22);
			this.toolAllTanks_Nation3.Text = "China";
			this.toolAllTanks_Nation3.Click += new System.EventHandler(this.toolAllTanks_Nation_Click);
			this.toolAllTanks_Nation3.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Nation4
			// 
			this.toolAllTanks_Nation4.BackColor = System.Drawing.SystemColors.Control;
			this.toolAllTanks_Nation4.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Nation4.Image")));
			this.toolAllTanks_Nation4.Name = "toolAllTanks_Nation4";
			this.toolAllTanks_Nation4.Size = new System.Drawing.Size(122, 22);
			this.toolAllTanks_Nation4.Text = "France";
			this.toolAllTanks_Nation4.Click += new System.EventHandler(this.toolAllTanks_Nation_Click);
			this.toolAllTanks_Nation4.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Nation1
			// 
			this.toolAllTanks_Nation1.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Nation1.Image")));
			this.toolAllTanks_Nation1.Name = "toolAllTanks_Nation1";
			this.toolAllTanks_Nation1.Size = new System.Drawing.Size(122, 22);
			this.toolAllTanks_Nation1.Text = "Germany";
			this.toolAllTanks_Nation1.Click += new System.EventHandler(this.toolAllTanks_Nation_Click);
			this.toolAllTanks_Nation1.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Nation5
			// 
			this.toolAllTanks_Nation5.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Nation5.Image")));
			this.toolAllTanks_Nation5.Name = "toolAllTanks_Nation5";
			this.toolAllTanks_Nation5.Size = new System.Drawing.Size(122, 22);
			this.toolAllTanks_Nation5.Text = "U.K.";
			this.toolAllTanks_Nation5.Click += new System.EventHandler(this.toolAllTanks_Nation_Click);
			this.toolAllTanks_Nation5.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Nation2
			// 
			this.toolAllTanks_Nation2.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Nation2.Image")));
			this.toolAllTanks_Nation2.Name = "toolAllTanks_Nation2";
			this.toolAllTanks_Nation2.Size = new System.Drawing.Size(122, 22);
			this.toolAllTanks_Nation2.Text = "U.S.A.";
			this.toolAllTanks_Nation2.Click += new System.EventHandler(this.toolAllTanks_Nation_Click);
			this.toolAllTanks_Nation2.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Nation0
			// 
			this.toolAllTanks_Nation0.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Nation0.Image")));
			this.toolAllTanks_Nation0.Name = "toolAllTanks_Nation0";
			this.toolAllTanks_Nation0.Size = new System.Drawing.Size(122, 22);
			this.toolAllTanks_Nation0.Text = "U.S.S.R.";
			this.toolAllTanks_Nation0.Click += new System.EventHandler(this.toolAllTanks_Nation_Click);
			this.toolAllTanks_Nation0.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Nation6
			// 
			this.toolAllTanks_Nation6.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Nation6.Image")));
			this.toolAllTanks_Nation6.Name = "toolAllTanks_Nation6";
			this.toolAllTanks_Nation6.Size = new System.Drawing.Size(122, 22);
			this.toolAllTanks_Nation6.Text = "Japan";
			this.toolAllTanks_Nation6.Click += new System.EventHandler(this.toolAllTanks_Nation_Click);
			this.toolAllTanks_Nation6.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Type
			// 
			this.toolAllTanks_Type.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolAllTanks_Type.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolAllTanks_Type1,
			this.toolAllTanks_Type2,
			this.toolAllTanks_Type3,
			this.toolAllTanks_Type4,
			this.toolAllTanks_Type5});
			this.toolAllTanks_Type.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Type.Image")));
			this.toolAllTanks_Type.Name = "toolAllTanks_Type";
			this.toolAllTanks_Type.Size = new System.Drawing.Size(28, 25);
			this.toolAllTanks_Type.Text = "Tank Type";
			// 
			// toolAllTanks_Type1
			// 
			this.toolAllTanks_Type1.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Type1.Image")));
			this.toolAllTanks_Type1.Name = "toolAllTanks_Type1";
			this.toolAllTanks_Type1.Size = new System.Drawing.Size(158, 22);
			this.toolAllTanks_Type1.Text = "Light Tanks";
			this.toolAllTanks_Type1.Click += new System.EventHandler(this.toolAllTanks_Type_Click);
			this.toolAllTanks_Type1.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Type2
			// 
			this.toolAllTanks_Type2.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Type2.Image")));
			this.toolAllTanks_Type2.Name = "toolAllTanks_Type2";
			this.toolAllTanks_Type2.Size = new System.Drawing.Size(158, 22);
			this.toolAllTanks_Type2.Text = "Medium Tanks";
			this.toolAllTanks_Type2.Click += new System.EventHandler(this.toolAllTanks_Type_Click);
			this.toolAllTanks_Type2.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Type3
			// 
			this.toolAllTanks_Type3.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Type3.Image")));
			this.toolAllTanks_Type3.Name = "toolAllTanks_Type3";
			this.toolAllTanks_Type3.Size = new System.Drawing.Size(158, 22);
			this.toolAllTanks_Type3.Text = "Heavy Tanks";
			this.toolAllTanks_Type3.Click += new System.EventHandler(this.toolAllTanks_Type_Click);
			this.toolAllTanks_Type3.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Type4
			// 
			this.toolAllTanks_Type4.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Type4.Image")));
			this.toolAllTanks_Type4.Name = "toolAllTanks_Type4";
			this.toolAllTanks_Type4.Size = new System.Drawing.Size(158, 22);
			this.toolAllTanks_Type4.Text = "Tank Destroyers";
			this.toolAllTanks_Type4.Click += new System.EventHandler(this.toolAllTanks_Type_Click);
			this.toolAllTanks_Type4.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Type5
			// 
			this.toolAllTanks_Type5.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Type5.Image")));
			this.toolAllTanks_Type5.Name = "toolAllTanks_Type5";
			this.toolAllTanks_Type5.Size = new System.Drawing.Size(158, 22);
			this.toolAllTanks_Type5.Text = "SPGs";
			this.toolAllTanks_Type5.Click += new System.EventHandler(this.toolAllTanks_Type_Click);
			this.toolAllTanks_Type5.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Tier
			// 
			this.toolAllTanks_Tier.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolAllTanks_Tier.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolAllTanks_Tier01,
			this.toolAllTanks_Tier02,
			this.toolAllTanks_Tier03,
			this.toolAllTanks_Tier04,
			this.toolAllTanks_Tier05,
			this.toolAllTanks_Tier06,
			this.toolAllTanks_Tier07,
			this.toolAllTanks_Tier08,
			this.toolAllTanks_Tier09,
			this.toolAllTanks_Tier10});
			this.toolAllTanks_Tier.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Tier.Image")));
			this.toolAllTanks_Tier.Name = "toolAllTanks_Tier";
			this.toolAllTanks_Tier.Size = new System.Drawing.Size(28, 25);
			this.toolAllTanks_Tier.Text = "Tier";
			// 
			// toolAllTanks_Tier01
			// 
			this.toolAllTanks_Tier01.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Tier01.Image")));
			this.toolAllTanks_Tier01.Name = "toolAllTanks_Tier01";
			this.toolAllTanks_Tier01.Size = new System.Drawing.Size(86, 22);
			this.toolAllTanks_Tier01.Text = "1";
			this.toolAllTanks_Tier01.Click += new System.EventHandler(this.toolAllTanks_Tier_Click);
			this.toolAllTanks_Tier01.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Tier02
			// 
			this.toolAllTanks_Tier02.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Tier02.Image")));
			this.toolAllTanks_Tier02.Name = "toolAllTanks_Tier02";
			this.toolAllTanks_Tier02.Size = new System.Drawing.Size(86, 22);
			this.toolAllTanks_Tier02.Text = "2";
			this.toolAllTanks_Tier02.Click += new System.EventHandler(this.toolAllTanks_Tier_Click);
			this.toolAllTanks_Tier02.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Tier03
			// 
			this.toolAllTanks_Tier03.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Tier03.Image")));
			this.toolAllTanks_Tier03.Name = "toolAllTanks_Tier03";
			this.toolAllTanks_Tier03.Size = new System.Drawing.Size(86, 22);
			this.toolAllTanks_Tier03.Text = "3";
			this.toolAllTanks_Tier03.Click += new System.EventHandler(this.toolAllTanks_Tier_Click);
			this.toolAllTanks_Tier03.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Tier04
			// 
			this.toolAllTanks_Tier04.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Tier04.Image")));
			this.toolAllTanks_Tier04.Name = "toolAllTanks_Tier04";
			this.toolAllTanks_Tier04.Size = new System.Drawing.Size(86, 22);
			this.toolAllTanks_Tier04.Text = "4";
			this.toolAllTanks_Tier04.Click += new System.EventHandler(this.toolAllTanks_Tier_Click);
			this.toolAllTanks_Tier04.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Tier05
			// 
			this.toolAllTanks_Tier05.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Tier05.Image")));
			this.toolAllTanks_Tier05.Name = "toolAllTanks_Tier05";
			this.toolAllTanks_Tier05.Size = new System.Drawing.Size(86, 22);
			this.toolAllTanks_Tier05.Text = "5";
			this.toolAllTanks_Tier05.Click += new System.EventHandler(this.toolAllTanks_Tier_Click);
			this.toolAllTanks_Tier05.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Tier06
			// 
			this.toolAllTanks_Tier06.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Tier06.Image")));
			this.toolAllTanks_Tier06.Name = "toolAllTanks_Tier06";
			this.toolAllTanks_Tier06.Size = new System.Drawing.Size(86, 22);
			this.toolAllTanks_Tier06.Text = "6";
			this.toolAllTanks_Tier06.Click += new System.EventHandler(this.toolAllTanks_Tier_Click);
			this.toolAllTanks_Tier06.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Tier07
			// 
			this.toolAllTanks_Tier07.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Tier07.Image")));
			this.toolAllTanks_Tier07.Name = "toolAllTanks_Tier07";
			this.toolAllTanks_Tier07.Size = new System.Drawing.Size(86, 22);
			this.toolAllTanks_Tier07.Text = "7";
			this.toolAllTanks_Tier07.Click += new System.EventHandler(this.toolAllTanks_Tier_Click);
			this.toolAllTanks_Tier07.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Tier08
			// 
			this.toolAllTanks_Tier08.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Tier08.Image")));
			this.toolAllTanks_Tier08.Name = "toolAllTanks_Tier08";
			this.toolAllTanks_Tier08.Size = new System.Drawing.Size(86, 22);
			this.toolAllTanks_Tier08.Text = "8";
			this.toolAllTanks_Tier08.Click += new System.EventHandler(this.toolAllTanks_Tier_Click);
			this.toolAllTanks_Tier08.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Tier09
			// 
			this.toolAllTanks_Tier09.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Tier09.Image")));
			this.toolAllTanks_Tier09.Name = "toolAllTanks_Tier09";
			this.toolAllTanks_Tier09.Size = new System.Drawing.Size(86, 22);
			this.toolAllTanks_Tier09.Text = "9";
			this.toolAllTanks_Tier09.Click += new System.EventHandler(this.toolAllTanks_Tier_Click);
			this.toolAllTanks_Tier09.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Tier10
			// 
			this.toolAllTanks_Tier10.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Tier10.Image")));
			this.toolAllTanks_Tier10.Name = "toolAllTanks_Tier10";
			this.toolAllTanks_Tier10.Size = new System.Drawing.Size(86, 22);
			this.toolAllTanks_Tier10.Text = "10";
			this.toolAllTanks_Tier10.Click += new System.EventHandler(this.toolAllTanks_Tier_Click);
			this.toolAllTanks_Tier10.Paint += new System.Windows.Forms.PaintEventHandler(this.toolItem_Checked_paint);
			// 
			// toolAllTanks_Used
			// 
			this.toolAllTanks_Used.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolAllTanks_Used.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Used.Image")));
			this.toolAllTanks_Used.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolAllTanks_Used.Name = "toolAllTanks_Used";
			this.toolAllTanks_Used.Size = new System.Drawing.Size(37, 22);
			this.toolAllTanks_Used.Text = "Used";
			this.toolAllTanks_Used.Click += new System.EventHandler(this.toolAllTanks_Used_Click);
			// 
			// toolAllTanks_All
			// 
			this.toolAllTanks_All.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolAllTanks_All.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_All.Image")));
			this.toolAllTanks_All.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolAllTanks_All.Name = "toolAllTanks_All";
			this.toolAllTanks_All.Size = new System.Drawing.Size(25, 22);
			this.toolAllTanks_All.Text = "All";
			this.toolAllTanks_All.Click += new System.EventHandler(this.toolAllTanks_All_Click);
			// 
			// scrollSelectedTanks
			// 
			this.scrollSelectedTanks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.scrollSelectedTanks.BackColor = System.Drawing.Color.Transparent;
			this.scrollSelectedTanks.Image = null;
			this.scrollSelectedTanks.Location = new System.Drawing.Point(703, 323);
			this.scrollSelectedTanks.Name = "scrollSelectedTanks";
			this.scrollSelectedTanks.ScrollElementsTotals = 100;
			this.scrollSelectedTanks.ScrollElementsVisible = 20;
			this.scrollSelectedTanks.ScrollHide = false;
			this.scrollSelectedTanks.ScrollNecessary = true;
			this.scrollSelectedTanks.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.scrollSelectedTanks.ScrollPosition = 0;
			this.scrollSelectedTanks.Size = new System.Drawing.Size(17, 202);
			this.scrollSelectedTanks.TabIndex = 16;
			this.scrollSelectedTanks.TabStop = false;
			this.scrollSelectedTanks.Text = "badScrollBar2";
			this.scrollSelectedTanks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollSelTanks_MouseDown);
			this.scrollSelectedTanks.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollSelTanks_MouseMove);
			this.scrollSelectedTanks.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scrollSelTanks_MouseUp);
			// 
			// dataGridSelectedTanks
			// 
			this.dataGridSelectedTanks.AllowUserToAddRows = false;
			this.dataGridSelectedTanks.AllowUserToDeleteRows = false;
			this.dataGridSelectedTanks.AllowUserToOrderColumns = true;
			this.dataGridSelectedTanks.AllowUserToResizeRows = false;
			this.dataGridSelectedTanks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridSelectedTanks.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridSelectedTanks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridSelectedTanks.Cursor = System.Windows.Forms.Cursors.Default;
			this.dataGridSelectedTanks.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridSelectedTanks.Location = new System.Drawing.Point(453, 323);
			this.dataGridSelectedTanks.Name = "dataGridSelectedTanks";
			this.dataGridSelectedTanks.ReadOnly = true;
			this.dataGridSelectedTanks.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dataGridSelectedTanks.RowHeadersVisible = false;
			this.dataGridSelectedTanks.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dataGridSelectedTanks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridSelectedTanks.Size = new System.Drawing.Size(250, 202);
			this.dataGridSelectedTanks.TabIndex = 15;
			this.dataGridSelectedTanks.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridSelectedTanks_CellContentDoubleClick);
			this.dataGridSelectedTanks.SelectionChanged += new System.EventHandler(this.dataGridSelTanks_SelectionChanged);
			this.dataGridSelectedTanks.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGrid_Paint);
			// 
			// scrollAllTanks
			// 
			this.scrollAllTanks.BackColor = System.Drawing.Color.Transparent;
			this.scrollAllTanks.Image = null;
			this.scrollAllTanks.Location = new System.Drawing.Point(377, 323);
			this.scrollAllTanks.Name = "scrollAllTanks";
			this.scrollAllTanks.ScrollElementsTotals = 100;
			this.scrollAllTanks.ScrollElementsVisible = 20;
			this.scrollAllTanks.ScrollHide = false;
			this.scrollAllTanks.ScrollNecessary = true;
			this.scrollAllTanks.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.scrollAllTanks.ScrollPosition = 0;
			this.scrollAllTanks.Size = new System.Drawing.Size(17, 202);
			this.scrollAllTanks.TabIndex = 8;
			this.scrollAllTanks.TabStop = false;
			this.scrollAllTanks.Text = "badScrollBar1";
			this.scrollAllTanks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollAllTanks_MouseDown);
			this.scrollAllTanks.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollAllTanks_MouseMove);
			this.scrollAllTanks.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scrollAllTanks_MouseUp);
			// 
			// dataGridAllTanks
			// 
			this.dataGridAllTanks.AllowUserToAddRows = false;
			this.dataGridAllTanks.AllowUserToDeleteRows = false;
			this.dataGridAllTanks.AllowUserToOrderColumns = true;
			this.dataGridAllTanks.AllowUserToResizeRows = false;
			this.dataGridAllTanks.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridAllTanks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridAllTanks.Cursor = System.Windows.Forms.Cursors.Default;
			this.dataGridAllTanks.Location = new System.Drawing.Point(42, 323);
			this.dataGridAllTanks.Name = "dataGridAllTanks";
			this.dataGridAllTanks.ReadOnly = true;
			this.dataGridAllTanks.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dataGridAllTanks.RowHeadersVisible = false;
			this.dataGridAllTanks.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dataGridAllTanks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridAllTanks.Size = new System.Drawing.Size(335, 202);
			this.dataGridAllTanks.TabIndex = 7;
			this.dataGridAllTanks.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridAllTanks_CellContentDoubleClick);
			this.dataGridAllTanks.SelectionChanged += new System.EventHandler(this.dataGridAllTanks_SelectionChanged);
			this.dataGridAllTanks.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGrid_Paint);
			this.dataGridAllTanks.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridAllTanks_KeyPress);
			this.dataGridAllTanks.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dataGridAllTanks_PreviewKeyDown);
			// 
			// scrollFavList
			// 
			this.scrollFavList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.scrollFavList.BackColor = System.Drawing.Color.Transparent;
			this.scrollFavList.Image = null;
			this.scrollFavList.Location = new System.Drawing.Point(703, 97);
			this.scrollFavList.Name = "scrollFavList";
			this.scrollFavList.ScrollElementsTotals = 100;
			this.scrollFavList.ScrollElementsVisible = 20;
			this.scrollFavList.ScrollHide = false;
			this.scrollFavList.ScrollNecessary = true;
			this.scrollFavList.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.scrollFavList.ScrollPosition = 0;
			this.scrollFavList.Size = new System.Drawing.Size(17, 126);
			this.scrollFavList.TabIndex = 4;
			this.scrollFavList.Text = "badScrollBar1";
			this.scrollFavList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollFavList_MouseDown);
			this.scrollFavList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollFavList_MouseMove);
			// 
			// dataGridFavList
			// 
			this.dataGridFavList.AllowUserToAddRows = false;
			this.dataGridFavList.AllowUserToDeleteRows = false;
			this.dataGridFavList.AllowUserToOrderColumns = true;
			this.dataGridFavList.AllowUserToResizeRows = false;
			this.dataGridFavList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridFavList.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridFavList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridFavList.Cursor = System.Windows.Forms.Cursors.Default;
			this.dataGridFavList.Location = new System.Drawing.Point(42, 97);
			this.dataGridFavList.MultiSelect = false;
			this.dataGridFavList.Name = "dataGridFavList";
			this.dataGridFavList.ReadOnly = true;
			this.dataGridFavList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dataGridFavList.RowHeadersVisible = false;
			this.dataGridFavList.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dataGridFavList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridFavList.Size = new System.Drawing.Size(661, 126);
			this.dataGridFavList.TabIndex = 3;
			this.dataGridFavList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridFavList_CellClick);
			this.dataGridFavList.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGrid_Paint);
			// 
			// badGroupBox2
			// 
			this.badGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox2.Image = null;
			this.badGroupBox2.Location = new System.Drawing.Point(25, 48);
			this.badGroupBox2.Name = "badGroupBox2";
			this.badGroupBox2.Size = new System.Drawing.Size(715, 195);
			this.badGroupBox2.TabIndex = 1;
			this.badGroupBox2.TabStop = false;
			this.badGroupBox2.Text = "Favourite Tank Lists";
			this.badGroupBox2.UseWaitCursor = true;
			// 
			// lblSelectedTanks
			// 
			this.lblSelectedTanks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblSelectedTanks.Dimmed = false;
			this.lblSelectedTanks.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.lblSelectedTanks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.lblSelectedTanks.Image = null;
			this.lblSelectedTanks.Location = new System.Drawing.Point(453, 276);
			this.lblSelectedTanks.Name = "lblSelectedTanks";
			this.lblSelectedTanks.Size = new System.Drawing.Size(182, 23);
			this.lblSelectedTanks.TabIndex = 13;
			this.lblSelectedTanks.TabStop = false;
			this.lblSelectedTanks.Text = "Selected Tanks:";
			// 
			// lblAllTanks
			// 
			this.lblAllTanks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblAllTanks.Dimmed = false;
			this.lblAllTanks.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.lblAllTanks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.lblAllTanks.Image = null;
			this.lblAllTanks.Location = new System.Drawing.Point(42, 276);
			this.lblAllTanks.Name = "lblAllTanks";
			this.lblAllTanks.Size = new System.Drawing.Size(152, 23);
			this.lblAllTanks.TabIndex = 5;
			this.lblAllTanks.TabStop = false;
			this.lblAllTanks.Text = "Available Tanks:";
			// 
			// groupTanks
			// 
			this.groupTanks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.groupTanks.BackColor = System.Drawing.Color.Transparent;
			this.groupTanks.Image = null;
			this.groupTanks.Location = new System.Drawing.Point(25, 260);
			this.groupTanks.Name = "groupTanks";
			this.groupTanks.Size = new System.Drawing.Size(715, 285);
			this.groupTanks.TabIndex = 12;
			this.groupTanks.TabStop = false;
			this.groupTanks.Text = "Tanks";
			// 
			// FavList2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Fuchsia;
			this.ClientSize = new System.Drawing.Size(766, 610);
			this.Controls.Add(this.FavTanksTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MinimumSize = new System.Drawing.Size(607, 591);
			this.Name = "FavList2";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FavTanks";
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FavList2_FormClosing);
			this.Load += new System.EventHandler(this.FavList_Load);
			this.ResizeEnd += new System.EventHandler(this.FavTanks_ResizeEnd);
			this.LocationChanged += new System.EventHandler(this.FavList2_LocationChanged);
			this.Resize += new System.EventHandler(this.FavTanks_Resize);
			this.FavTanksTheme.ResumeLayout(false);
			this.toolColList.ResumeLayout(false);
			this.toolColList.PerformLayout();
			this.toolSelectedTanks.ResumeLayout(false);
			this.toolSelectedTanks.PerformLayout();
			this.toolAllTanks.ResumeLayout(false);
			this.toolAllTanks.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridSelectedTanks)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridAllTanks)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridFavList)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm FavTanksTheme;
		private BadScrollBar scrollFavList;
		private System.Windows.Forms.DataGridView dataGridFavList;
		private BadGroupBox badGroupBox2;
		private BadScrollBar scrollSelectedTanks;
		private System.Windows.Forms.DataGridView dataGridSelectedTanks;
		private BadScrollBar scrollAllTanks;
		private System.Windows.Forms.DataGridView dataGridAllTanks;
		private BadLabel lblSelectedTanks;
		private BadLabel lblAllTanks;
		private BadGroupBox groupTanks;
		private BadButton btnRemoveAll;
		private BadButton btnRemoveSelected;
		private BadButton btnSelectSelected;
		private BadButton btnSelectAll;
		private BadButton btnFavListCancel;
		private BadButton btnFavListSave;
		private System.Windows.Forms.ToolStrip toolAllTanks;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Nation;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Nation3;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Nation4;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Nation1;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Nation5;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Nation2;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Nation0;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Nation6;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Type;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Type1;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Type2;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Type3;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Type4;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Type5;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Tier;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Tier01;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Tier02;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Tier03;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Tier04;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Tier05;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Tier06;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Tier07;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Tier08;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Tier09;
		private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Tier10;
		private System.Windows.Forms.ToolStrip toolSelectedTanks;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripButton toolSelectedTanks_SortNation;
		private System.Windows.Forms.ToolStripButton toolSelectedTanks_SortType;
		private System.Windows.Forms.ToolStripButton toolSelectedTanks_SortTier;
		private System.Windows.Forms.ToolStripButton toolAllTanks_Used;
		private System.Windows.Forms.ToolStripButton toolAllTanks_All;
		private System.Windows.Forms.ImageList imageListToolStrip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolSelectedTanks_MoveUp;
		private System.Windows.Forms.ToolStripButton toolSelectedTanks_MoveDown;
		private System.Windows.Forms.ToolStrip toolColList;
		private System.Windows.Forms.ToolStripButton toolFavListUp;
		private System.Windows.Forms.ToolStripButton toolFavListDown;
		private System.Windows.Forms.ToolStripButton toolFavListVisible;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolFavListAdd;
		private System.Windows.Forms.ToolStripButton toolFavListModify;
		private System.Windows.Forms.ToolStripButton toolFavListDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton toolFavListRefresh;
		private BadButton btnClose;
	}
}