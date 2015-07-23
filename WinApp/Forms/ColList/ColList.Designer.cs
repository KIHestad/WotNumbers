namespace WinApp.Forms
{
	partial class ColList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColList));
			BadThemeContainerControl.MainAreaClass mainAreaClass1 = new BadThemeContainerControl.MainAreaClass();
			this.imageListToolStrip = new System.Windows.Forms.ImageList(this.components);
			this.ColListTheme = new BadForm();
			this.btnReset = new BadButton();
			this.btnClose = new BadButton();
			this.toolColList = new System.Windows.Forms.ToolStrip();
			this.toolColListUp = new System.Windows.Forms.ToolStripButton();
			this.toolColListDown = new System.Windows.Forms.ToolStripButton();
			this.toolColListVisible = new System.Windows.Forms.ToolStripButton();
			this.toolColListDefault = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolColListAdd = new System.Windows.Forms.ToolStripButton();
			this.toolColListModify = new System.Windows.Forms.ToolStripButton();
			this.toolColListDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolColListRefresh = new System.Windows.Forms.ToolStripButton();
			this.btnRemoveAll = new BadButton();
			this.btnRemoveSelected = new BadButton();
			this.btnSelectSelected = new BadButton();
			this.btnSelectAll = new BadButton();
			this.btnColumnListCancel = new BadButton();
			this.btnColumnListSave = new BadButton();
			this.toolSelectedColumns = new System.Windows.Forms.ToolStrip();
			this.toolSelectedTanks_MoveUp = new System.Windows.Forms.ToolStripButton();
			this.toolSelectedTanks_MoveDown = new System.Windows.Forms.ToolStripButton();
			this.toolSelectedTanks_Separator = new System.Windows.Forms.ToolStripButton();
			this.toolSelectedTanks_Sep = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolSelectedTanks_Sep_02 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolSelectedTanks_Sep_03 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolSelectedTanks_Sep_04 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolSelectedTanks_Sep_05 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolSelectedTanks_Sep_06 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolSelectedTanks_Sep_08 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolSelectedTanks_Sep_10 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolSelectedTanks_Sep_12 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolSelectedTanks_Sep_15 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAllColumns = new System.Windows.Forms.ToolStrip();
			this.toolAvailableCol_All = new System.Windows.Forms.ToolStripButton();
			this.toolAvailableCol_1 = new System.Windows.Forms.ToolStripButton();
			this.toolAvailableCol_2 = new System.Windows.Forms.ToolStripButton();
			this.toolAvailableCol_3 = new System.Windows.Forms.ToolStripButton();
			this.toolAvailableCol_4 = new System.Windows.Forms.ToolStripButton();
			this.toolAvailableCol_5 = new System.Windows.Forms.ToolStripButton();
			this.toolAvailableCol_6 = new System.Windows.Forms.ToolStripButton();
			this.toolAvailableCol_7 = new System.Windows.Forms.ToolStripButton();
			this.toolAvailableCol_8 = new System.Windows.Forms.ToolStripButton();
			this.toolAvailableCol_9 = new System.Windows.Forms.ToolStripButton();
			this.toolAvailableCol_10 = new System.Windows.Forms.ToolStripButton();
			this.scrollSelectedColumns = new BadScrollBar();
			this.dataGridSelectedColumns = new System.Windows.Forms.DataGridView();
			this.scrollAllColumns = new BadScrollBar();
			this.dataGridAllColumns = new System.Windows.Forms.DataGridView();
			this.scrollColumnList = new BadScrollBar();
			this.dataGridColumnList = new System.Windows.Forms.DataGridView();
			this.lblAllColumns = new BadLabel();
			this.lblSelectedColumns = new BadLabel();
			this.groupTanks = new BadGroupBox();
			this.badGroupBox2 = new BadGroupBox();
			this.badLabel1 = new BadLabel();
			this.ColListTheme.SuspendLayout();
			this.toolColList.SuspendLayout();
			this.toolSelectedColumns.SuspendLayout();
			this.toolAllColumns.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridSelectedColumns)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridAllColumns)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridColumnList)).BeginInit();
			this.SuspendLayout();
			// 
			// imageListToolStrip
			// 
			this.imageListToolStrip.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListToolStrip.ImageStream")));
			this.imageListToolStrip.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListToolStrip.Images.SetKeyName(0, "check.png");
			// 
			// ColListTheme
			// 
			this.ColListTheme.Controls.Add(this.btnReset);
			this.ColListTheme.Controls.Add(this.btnClose);
			this.ColListTheme.Controls.Add(this.toolColList);
			this.ColListTheme.Controls.Add(this.btnRemoveAll);
			this.ColListTheme.Controls.Add(this.btnRemoveSelected);
			this.ColListTheme.Controls.Add(this.btnSelectSelected);
			this.ColListTheme.Controls.Add(this.btnSelectAll);
			this.ColListTheme.Controls.Add(this.btnColumnListCancel);
			this.ColListTheme.Controls.Add(this.btnColumnListSave);
			this.ColListTheme.Controls.Add(this.toolSelectedColumns);
			this.ColListTheme.Controls.Add(this.toolAllColumns);
			this.ColListTheme.Controls.Add(this.scrollSelectedColumns);
			this.ColListTheme.Controls.Add(this.dataGridSelectedColumns);
			this.ColListTheme.Controls.Add(this.scrollAllColumns);
			this.ColListTheme.Controls.Add(this.dataGridAllColumns);
			this.ColListTheme.Controls.Add(this.scrollColumnList);
			this.ColListTheme.Controls.Add(this.dataGridColumnList);
			this.ColListTheme.Controls.Add(this.lblAllColumns);
			this.ColListTheme.Controls.Add(this.lblSelectedColumns);
			this.ColListTheme.Controls.Add(this.groupTanks);
			this.ColListTheme.Controls.Add(this.badGroupBox2);
			this.ColListTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ColListTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.ColListTheme.FormExitAsMinimize = false;
			this.ColListTheme.FormFooter = false;
			this.ColListTheme.FormFooterHeight = 26;
			this.ColListTheme.FormInnerBorder = 3;
			this.ColListTheme.FormMargin = 0;
			this.ColListTheme.Image = null;
			this.ColListTheme.Location = new System.Drawing.Point(0, 0);
			this.ColListTheme.MainArea = mainAreaClass1;
			this.ColListTheme.Name = "ColListTheme";
			this.ColListTheme.Resizable = true;
			this.ColListTheme.Size = new System.Drawing.Size(624, 633);
			this.ColListTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("ColListTheme.SystemExitImage")));
			this.ColListTheme.SystemMaximizeImage = ((System.Drawing.Image)(resources.GetObject("ColListTheme.SystemMaximizeImage")));
			this.ColListTheme.SystemMinimizeImage = null;
			this.ColListTheme.TabIndex = 0;
			this.ColListTheme.Text = "Edit Tank/Battle View";
			this.ColListTheme.TitleHeight = 26;
			// 
			// btnReset
			// 
			this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnReset.BlackButton = false;
			this.btnReset.Checked = false;
			this.btnReset.Image = null;
			this.btnReset.Location = new System.Drawing.Point(25, 590);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(75, 23);
			this.btnReset.TabIndex = 22;
			this.btnReset.Text = "Reset";
			this.btnReset.ToolTipText = "Reset all system views";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.BlackButton = false;
			this.btnClose.Checked = false;
			this.btnClose.Image = null;
			this.btnClose.Location = new System.Drawing.Point(523, 590);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 21;
			this.btnClose.Text = "Close";
			this.btnClose.ToolTipText = "";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// toolColList
			// 
			this.toolColList.AutoSize = false;
			this.toolColList.Dock = System.Windows.Forms.DockStyle.None;
			this.toolColList.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolColList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolColListUp,
            this.toolColListDown,
            this.toolColListVisible,
            this.toolColListDefault,
            this.toolStripSeparator1,
            this.toolColListAdd,
            this.toolColListModify,
            this.toolColListDelete,
            this.toolStripSeparator2,
            this.toolColListRefresh});
			this.toolColList.Location = new System.Drawing.Point(42, 77);
			this.toolColList.Name = "toolColList";
			this.toolColList.Size = new System.Drawing.Size(537, 25);
			this.toolColList.TabIndex = 1;
			this.toolColList.TabStop = true;
			this.toolColList.Text = "toolStrip1";
			// 
			// toolColListUp
			// 
			this.toolColListUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolColListUp.Image = ((System.Drawing.Image)(resources.GetObject("toolColListUp.Image")));
			this.toolColListUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolColListUp.Name = "toolColListUp";
			this.toolColListUp.Size = new System.Drawing.Size(23, 22);
			this.toolColListUp.Text = "toolStripButton1";
			this.toolColListUp.ToolTipText = "Move selected view up";
			this.toolColListUp.Click += new System.EventHandler(this.toolColListUp_Click);
			// 
			// toolColListDown
			// 
			this.toolColListDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolColListDown.Image = ((System.Drawing.Image)(resources.GetObject("toolColListDown.Image")));
			this.toolColListDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolColListDown.Name = "toolColListDown";
			this.toolColListDown.Size = new System.Drawing.Size(23, 22);
			this.toolColListDown.Text = "toolStripButton2";
			this.toolColListDown.ToolTipText = "Move selected view down";
			this.toolColListDown.Click += new System.EventHandler(this.toolColListDown_Click);
			// 
			// toolColListVisible
			// 
			this.toolColListVisible.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolColListVisible.Image = ((System.Drawing.Image)(resources.GetObject("toolColListVisible.Image")));
			this.toolColListVisible.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolColListVisible.Name = "toolColListVisible";
			this.toolColListVisible.Size = new System.Drawing.Size(36, 22);
			this.toolColListVisible.Text = "Hide";
			this.toolColListVisible.ToolTipText = "Hide selected view from menu";
			this.toolColListVisible.Click += new System.EventHandler(this.toolColListVisible_Click);
			// 
			// toolColListDefault
			// 
			this.toolColListDefault.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolColListDefault.Image = ((System.Drawing.Image)(resources.GetObject("toolColListDefault.Image")));
			this.toolColListDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolColListDefault.Name = "toolColListDefault";
			this.toolColListDefault.Size = new System.Drawing.Size(49, 22);
			this.toolColListDefault.Text = "Startup";
			this.toolColListDefault.ToolTipText = "Use as default view when application starts";
			this.toolColListDefault.Click += new System.EventHandler(this.toolColListDefault_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolColListAdd
			// 
			this.toolColListAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolColListAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolColListAdd.Image")));
			this.toolColListAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolColListAdd.Name = "toolColListAdd";
			this.toolColListAdd.Size = new System.Drawing.Size(33, 22);
			this.toolColListAdd.Text = "Add";
			this.toolColListAdd.ToolTipText = "Add new view";
			this.toolColListAdd.Click += new System.EventHandler(this.toolColListAdd_Click);
			// 
			// toolColListModify
			// 
			this.toolColListModify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolColListModify.Image = ((System.Drawing.Image)(resources.GetObject("toolColListModify.Image")));
			this.toolColListModify.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolColListModify.Name = "toolColListModify";
			this.toolColListModify.Size = new System.Drawing.Size(49, 22);
			this.toolColListModify.Text = "Modify";
			this.toolColListModify.ToolTipText = "Modify selected view";
			this.toolColListModify.Click += new System.EventHandler(this.toolColListModify_Click);
			// 
			// toolColListDelete
			// 
			this.toolColListDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolColListDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolColListDelete.Image")));
			this.toolColListDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolColListDelete.Name = "toolColListDelete";
			this.toolColListDelete.Size = new System.Drawing.Size(44, 22);
			this.toolColListDelete.Text = "Delete";
			this.toolColListDelete.ToolTipText = "Delete selected view";
			this.toolColListDelete.Click += new System.EventHandler(this.toolColListDelete_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolColListRefresh
			// 
			this.toolColListRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolColListRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolColListRefresh.Image")));
			this.toolColListRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolColListRefresh.Name = "toolColListRefresh";
			this.toolColListRefresh.Size = new System.Drawing.Size(23, 22);
			this.toolColListRefresh.Text = "toolStripButton1";
			this.toolColListRefresh.ToolTipText = "Refresh";
			this.toolColListRefresh.Click += new System.EventHandler(this.toolColListRefresh_Click);
			// 
			// btnRemoveAll
			// 
			this.btnRemoveAll.BlackButton = false;
			this.btnRemoveAll.Checked = false;
			this.btnRemoveAll.Image = null;
			this.btnRemoveAll.Location = new System.Drawing.Point(295, 456);
			this.btnRemoveAll.Name = "btnRemoveAll";
			this.btnRemoveAll.Size = new System.Drawing.Size(29, 23);
			this.btnRemoveAll.TabIndex = 14;
			this.btnRemoveAll.Text = "<<";
			this.btnRemoveAll.ToolTipText = "Remove all columns from current view";
			this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
			// 
			// btnRemoveSelected
			// 
			this.btnRemoveSelected.BlackButton = false;
			this.btnRemoveSelected.Checked = false;
			this.btnRemoveSelected.Image = null;
			this.btnRemoveSelected.Location = new System.Drawing.Point(295, 485);
			this.btnRemoveSelected.Name = "btnRemoveSelected";
			this.btnRemoveSelected.Size = new System.Drawing.Size(29, 23);
			this.btnRemoveSelected.TabIndex = 15;
			this.btnRemoveSelected.Text = "<";
			this.btnRemoveSelected.ToolTipText = "Remove selected column(s) from current view";
			this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
			// 
			// btnSelectSelected
			// 
			this.btnSelectSelected.BlackButton = false;
			this.btnSelectSelected.Checked = false;
			this.btnSelectSelected.Image = null;
			this.btnSelectSelected.Location = new System.Drawing.Point(295, 398);
			this.btnSelectSelected.Name = "btnSelectSelected";
			this.btnSelectSelected.Size = new System.Drawing.Size(29, 23);
			this.btnSelectSelected.TabIndex = 12;
			this.btnSelectSelected.Text = ">";
			this.btnSelectSelected.ToolTipText = "Add selected column(s) to current view";
			this.btnSelectSelected.Click += new System.EventHandler(this.btnSelectSelected_Click);
			// 
			// btnSelectAll
			// 
			this.btnSelectAll.BlackButton = false;
			this.btnSelectAll.Checked = false;
			this.btnSelectAll.Image = null;
			this.btnSelectAll.Location = new System.Drawing.Point(295, 427);
			this.btnSelectAll.Name = "btnSelectAll";
			this.btnSelectAll.Size = new System.Drawing.Size(29, 23);
			this.btnSelectAll.TabIndex = 13;
			this.btnSelectAll.Text = ">>";
			this.btnSelectAll.ToolTipText = "Add all columns to current view";
			this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
			// 
			// btnColumnListCancel
			// 
			this.btnColumnListCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnColumnListCancel.BlackButton = false;
			this.btnColumnListCancel.Checked = false;
			this.btnColumnListCancel.Enabled = false;
			this.btnColumnListCancel.Image = null;
			this.btnColumnListCancel.Location = new System.Drawing.Point(437, 590);
			this.btnColumnListCancel.Name = "btnColumnListCancel";
			this.btnColumnListCancel.Size = new System.Drawing.Size(80, 23);
			this.btnColumnListCancel.TabIndex = 20;
			this.btnColumnListCancel.Text = "Revert";
			this.btnColumnListCancel.ToolTipText = "";
			this.btnColumnListCancel.Click += new System.EventHandler(this.btnSelectedColumnListCancel_Click);
			// 
			// btnColumnListSave
			// 
			this.btnColumnListSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnColumnListSave.BlackButton = false;
			this.btnColumnListSave.Checked = false;
			this.btnColumnListSave.Enabled = false;
			this.btnColumnListSave.Image = null;
			this.btnColumnListSave.Location = new System.Drawing.Point(351, 590);
			this.btnColumnListSave.Name = "btnColumnListSave";
			this.btnColumnListSave.Size = new System.Drawing.Size(80, 23);
			this.btnColumnListSave.TabIndex = 19;
			this.btnColumnListSave.Text = "Save";
			this.btnColumnListSave.ToolTipText = "";
			this.btnColumnListSave.Click += new System.EventHandler(this.btnSelectedColumnListSave_Click);
			// 
			// toolSelectedColumns
			// 
			this.toolSelectedColumns.AutoSize = false;
			this.toolSelectedColumns.Dock = System.Windows.Forms.DockStyle.None;
			this.toolSelectedColumns.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolSelectedColumns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSelectedTanks_MoveUp,
            this.toolSelectedTanks_MoveDown,
            this.toolSelectedTanks_Separator,
            this.toolSelectedTanks_Sep});
			this.toolSelectedColumns.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolSelectedColumns.Location = new System.Drawing.Point(345, 326);
			this.toolSelectedColumns.Name = "toolSelectedColumns";
			this.toolSelectedColumns.Size = new System.Drawing.Size(234, 25);
			this.toolSelectedColumns.Stretch = true;
			this.toolSelectedColumns.TabIndex = 9;
			this.toolSelectedColumns.TabStop = true;
			this.toolSelectedColumns.Text = "toolStrip1";
			// 
			// toolSelectedTanks_MoveUp
			// 
			this.toolSelectedTanks_MoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolSelectedTanks_MoveUp.Image = ((System.Drawing.Image)(resources.GetObject("toolSelectedTanks_MoveUp.Image")));
			this.toolSelectedTanks_MoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolSelectedTanks_MoveUp.Name = "toolSelectedTanks_MoveUp";
			this.toolSelectedTanks_MoveUp.Size = new System.Drawing.Size(23, 22);
			this.toolSelectedTanks_MoveUp.Text = "toolStripButton1";
			this.toolSelectedTanks_MoveUp.ToolTipText = "Move selected column(s) up";
			this.toolSelectedTanks_MoveUp.Click += new System.EventHandler(this.toolSelectedColumns_MoveUp_Click);
			// 
			// toolSelectedTanks_MoveDown
			// 
			this.toolSelectedTanks_MoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolSelectedTanks_MoveDown.Image = ((System.Drawing.Image)(resources.GetObject("toolSelectedTanks_MoveDown.Image")));
			this.toolSelectedTanks_MoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolSelectedTanks_MoveDown.Name = "toolSelectedTanks_MoveDown";
			this.toolSelectedTanks_MoveDown.Size = new System.Drawing.Size(23, 22);
			this.toolSelectedTanks_MoveDown.Text = "toolStripButton2";
			this.toolSelectedTanks_MoveDown.ToolTipText = "Move selected column(s) down";
			this.toolSelectedTanks_MoveDown.Click += new System.EventHandler(this.toolSelectedColumns_MoveDown_Click);
			// 
			// toolSelectedTanks_Separator
			// 
			this.toolSelectedTanks_Separator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolSelectedTanks_Separator.Image = ((System.Drawing.Image)(resources.GetObject("toolSelectedTanks_Separator.Image")));
			this.toolSelectedTanks_Separator.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolSelectedTanks_Separator.Name = "toolSelectedTanks_Separator";
			this.toolSelectedTanks_Separator.Size = new System.Drawing.Size(23, 22);
			this.toolSelectedTanks_Separator.Text = "Separator";
			this.toolSelectedTanks_Separator.ToolTipText = "Add separator";
			this.toolSelectedTanks_Separator.Click += new System.EventHandler(this.toolSelectedTanks_Separator_Click);
			// 
			// toolSelectedTanks_Sep
			// 
			this.toolSelectedTanks_Sep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolSelectedTanks_Sep.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSelectedTanks_Sep_02,
            this.toolSelectedTanks_Sep_03,
            this.toolSelectedTanks_Sep_04,
            this.toolStripSeparator3,
            this.toolSelectedTanks_Sep_05,
            this.toolSelectedTanks_Sep_06,
            this.toolSelectedTanks_Sep_08,
            this.toolStripSeparator4,
            this.toolSelectedTanks_Sep_10,
            this.toolSelectedTanks_Sep_12,
            this.toolSelectedTanks_Sep_15});
			this.toolSelectedTanks_Sep.Image = ((System.Drawing.Image)(resources.GetObject("toolSelectedTanks_Sep.Image")));
			this.toolSelectedTanks_Sep.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolSelectedTanks_Sep.Name = "toolSelectedTanks_Sep";
			this.toolSelectedTanks_Sep.ShowDropDownArrow = false;
			this.toolSelectedTanks_Sep.Size = new System.Drawing.Size(20, 22);
			this.toolSelectedTanks_Sep.Text = "toolStripDropDownButton1";
			this.toolSelectedTanks_Sep.ToolTipText = "Set width of separators";
			// 
			// toolSelectedTanks_Sep_02
			// 
			this.toolSelectedTanks_Sep_02.Name = "toolSelectedTanks_Sep_02";
			this.toolSelectedTanks_Sep_02.Size = new System.Drawing.Size(247, 22);
			this.toolSelectedTanks_Sep_02.Text = "Set separator size: 2px (Narrow)";
			this.toolSelectedTanks_Sep_02.Click += new System.EventHandler(this.toolSelectedTanks_Sep_Size_Click);
			// 
			// toolSelectedTanks_Sep_03
			// 
			this.toolSelectedTanks_Sep_03.Name = "toolSelectedTanks_Sep_03";
			this.toolSelectedTanks_Sep_03.Size = new System.Drawing.Size(247, 22);
			this.toolSelectedTanks_Sep_03.Text = "Set separator size: 3px (Default)";
			this.toolSelectedTanks_Sep_03.Click += new System.EventHandler(this.toolSelectedTanks_Sep_Size_Click);
			// 
			// toolSelectedTanks_Sep_04
			// 
			this.toolSelectedTanks_Sep_04.Name = "toolSelectedTanks_Sep_04";
			this.toolSelectedTanks_Sep_04.Size = new System.Drawing.Size(247, 22);
			this.toolSelectedTanks_Sep_04.Text = "Set separator size: 4px";
			this.toolSelectedTanks_Sep_04.Click += new System.EventHandler(this.toolSelectedTanks_Sep_Size_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(244, 6);
			// 
			// toolSelectedTanks_Sep_05
			// 
			this.toolSelectedTanks_Sep_05.Name = "toolSelectedTanks_Sep_05";
			this.toolSelectedTanks_Sep_05.Size = new System.Drawing.Size(247, 22);
			this.toolSelectedTanks_Sep_05.Text = "Set separator size: 5px";
			this.toolSelectedTanks_Sep_05.Click += new System.EventHandler(this.toolSelectedTanks_Sep_Size_Click);
			// 
			// toolSelectedTanks_Sep_06
			// 
			this.toolSelectedTanks_Sep_06.Name = "toolSelectedTanks_Sep_06";
			this.toolSelectedTanks_Sep_06.Size = new System.Drawing.Size(247, 22);
			this.toolSelectedTanks_Sep_06.Text = "Set separator size: 6 px (Medium)";
			this.toolSelectedTanks_Sep_06.Click += new System.EventHandler(this.toolSelectedTanks_Sep_Size_Click);
			// 
			// toolSelectedTanks_Sep_08
			// 
			this.toolSelectedTanks_Sep_08.Name = "toolSelectedTanks_Sep_08";
			this.toolSelectedTanks_Sep_08.Size = new System.Drawing.Size(247, 22);
			this.toolSelectedTanks_Sep_08.Text = "Set separator size: 8 px";
			this.toolSelectedTanks_Sep_08.Click += new System.EventHandler(this.toolSelectedTanks_Sep_Size_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(244, 6);
			// 
			// toolSelectedTanks_Sep_10
			// 
			this.toolSelectedTanks_Sep_10.Name = "toolSelectedTanks_Sep_10";
			this.toolSelectedTanks_Sep_10.Size = new System.Drawing.Size(247, 22);
			this.toolSelectedTanks_Sep_10.Text = "Set separator size: 10 px";
			this.toolSelectedTanks_Sep_10.Click += new System.EventHandler(this.toolSelectedTanks_Sep_Size_Click);
			// 
			// toolSelectedTanks_Sep_12
			// 
			this.toolSelectedTanks_Sep_12.Name = "toolSelectedTanks_Sep_12";
			this.toolSelectedTanks_Sep_12.Size = new System.Drawing.Size(247, 22);
			this.toolSelectedTanks_Sep_12.Text = "Set separator size: 12 px";
			this.toolSelectedTanks_Sep_12.Click += new System.EventHandler(this.toolSelectedTanks_Sep_Size_Click);
			// 
			// toolSelectedTanks_Sep_15
			// 
			this.toolSelectedTanks_Sep_15.Name = "toolSelectedTanks_Sep_15";
			this.toolSelectedTanks_Sep_15.Size = new System.Drawing.Size(247, 22);
			this.toolSelectedTanks_Sep_15.Text = "Set separator size: 15 px (Wide)";
			this.toolSelectedTanks_Sep_15.Click += new System.EventHandler(this.toolSelectedTanks_Sep_Size_Click);
			// 
			// toolAllColumns
			// 
			this.toolAllColumns.AutoSize = false;
			this.toolAllColumns.Dock = System.Windows.Forms.DockStyle.None;
			this.toolAllColumns.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolAllColumns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAvailableCol_All,
            this.toolAvailableCol_1,
            this.toolAvailableCol_2,
            this.toolAvailableCol_3,
            this.toolAvailableCol_4,
            this.toolAvailableCol_5,
            this.toolAvailableCol_6,
            this.toolAvailableCol_7,
            this.toolAvailableCol_8,
            this.toolAvailableCol_9,
            this.toolAvailableCol_10});
			this.toolAllColumns.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolAllColumns.Location = new System.Drawing.Point(42, 326);
			this.toolAllColumns.Name = "toolAllColumns";
			this.toolAllColumns.Size = new System.Drawing.Size(234, 25);
			this.toolAllColumns.Stretch = true;
			this.toolAllColumns.TabIndex = 6;
			this.toolAllColumns.TabStop = true;
			this.toolAllColumns.Text = "toolStrip1";
			// 
			// toolAvailableCol_All
			// 
			this.toolAvailableCol_All.Checked = true;
			this.toolAvailableCol_All.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolAvailableCol_All.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolAvailableCol_All.Image = ((System.Drawing.Image)(resources.GetObject("toolAvailableCol_All.Image")));
			this.toolAvailableCol_All.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolAvailableCol_All.Name = "toolAvailableCol_All";
			this.toolAvailableCol_All.Size = new System.Drawing.Size(25, 22);
			this.toolAvailableCol_All.Text = "All";
			this.toolAvailableCol_All.Click += new System.EventHandler(this.toolAvaliableCol_All_Click);
			// 
			// toolAvailableCol_1
			// 
			this.toolAvailableCol_1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolAvailableCol_1.Image = ((System.Drawing.Image)(resources.GetObject("toolAvailableCol_1.Image")));
			this.toolAvailableCol_1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolAvailableCol_1.Name = "toolAvailableCol_1";
			this.toolAvailableCol_1.Size = new System.Drawing.Size(23, 22);
			this.toolAvailableCol_1.Text = "X";
			this.toolAvailableCol_1.Visible = false;
			this.toolAvailableCol_1.Click += new System.EventHandler(this.toolAvaliableCol_Group_Click);
			// 
			// toolAvailableCol_2
			// 
			this.toolAvailableCol_2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolAvailableCol_2.Image = ((System.Drawing.Image)(resources.GetObject("toolAvailableCol_2.Image")));
			this.toolAvailableCol_2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolAvailableCol_2.Name = "toolAvailableCol_2";
			this.toolAvailableCol_2.Size = new System.Drawing.Size(23, 22);
			this.toolAvailableCol_2.Text = "X";
			this.toolAvailableCol_2.Visible = false;
			this.toolAvailableCol_2.Click += new System.EventHandler(this.toolAvaliableCol_Group_Click);
			// 
			// toolAvailableCol_3
			// 
			this.toolAvailableCol_3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolAvailableCol_3.Image = ((System.Drawing.Image)(resources.GetObject("toolAvailableCol_3.Image")));
			this.toolAvailableCol_3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolAvailableCol_3.Name = "toolAvailableCol_3";
			this.toolAvailableCol_3.Size = new System.Drawing.Size(23, 22);
			this.toolAvailableCol_3.Text = "X";
			this.toolAvailableCol_3.Visible = false;
			this.toolAvailableCol_3.Click += new System.EventHandler(this.toolAvaliableCol_Group_Click);
			// 
			// toolAvailableCol_4
			// 
			this.toolAvailableCol_4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolAvailableCol_4.Image = ((System.Drawing.Image)(resources.GetObject("toolAvailableCol_4.Image")));
			this.toolAvailableCol_4.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolAvailableCol_4.Name = "toolAvailableCol_4";
			this.toolAvailableCol_4.Size = new System.Drawing.Size(23, 22);
			this.toolAvailableCol_4.Text = "X";
			this.toolAvailableCol_4.Visible = false;
			this.toolAvailableCol_4.Click += new System.EventHandler(this.toolAvaliableCol_Group_Click);
			// 
			// toolAvailableCol_5
			// 
			this.toolAvailableCol_5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolAvailableCol_5.Image = ((System.Drawing.Image)(resources.GetObject("toolAvailableCol_5.Image")));
			this.toolAvailableCol_5.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolAvailableCol_5.Name = "toolAvailableCol_5";
			this.toolAvailableCol_5.Size = new System.Drawing.Size(23, 22);
			this.toolAvailableCol_5.Text = "X";
			this.toolAvailableCol_5.Visible = false;
			this.toolAvailableCol_5.Click += new System.EventHandler(this.toolAvaliableCol_Group_Click);
			// 
			// toolAvailableCol_6
			// 
			this.toolAvailableCol_6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolAvailableCol_6.Image = ((System.Drawing.Image)(resources.GetObject("toolAvailableCol_6.Image")));
			this.toolAvailableCol_6.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolAvailableCol_6.Name = "toolAvailableCol_6";
			this.toolAvailableCol_6.Size = new System.Drawing.Size(23, 22);
			this.toolAvailableCol_6.Text = "X";
			this.toolAvailableCol_6.Visible = false;
			this.toolAvailableCol_6.Click += new System.EventHandler(this.toolAvaliableCol_Group_Click);
			// 
			// toolAvailableCol_7
			// 
			this.toolAvailableCol_7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolAvailableCol_7.Image = ((System.Drawing.Image)(resources.GetObject("toolAvailableCol_7.Image")));
			this.toolAvailableCol_7.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolAvailableCol_7.Name = "toolAvailableCol_7";
			this.toolAvailableCol_7.Size = new System.Drawing.Size(23, 22);
			this.toolAvailableCol_7.Text = "X";
			this.toolAvailableCol_7.Visible = false;
			this.toolAvailableCol_7.Click += new System.EventHandler(this.toolAvaliableCol_Group_Click);
			// 
			// toolAvailableCol_8
			// 
			this.toolAvailableCol_8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolAvailableCol_8.Image = ((System.Drawing.Image)(resources.GetObject("toolAvailableCol_8.Image")));
			this.toolAvailableCol_8.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolAvailableCol_8.Name = "toolAvailableCol_8";
			this.toolAvailableCol_8.Size = new System.Drawing.Size(23, 22);
			this.toolAvailableCol_8.Text = "X";
			this.toolAvailableCol_8.Visible = false;
			this.toolAvailableCol_8.Click += new System.EventHandler(this.toolAvaliableCol_Group_Click);
			// 
			// toolAvailableCol_9
			// 
			this.toolAvailableCol_9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolAvailableCol_9.Image = ((System.Drawing.Image)(resources.GetObject("toolAvailableCol_9.Image")));
			this.toolAvailableCol_9.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolAvailableCol_9.Name = "toolAvailableCol_9";
			this.toolAvailableCol_9.Size = new System.Drawing.Size(23, 22);
			this.toolAvailableCol_9.Text = "X";
			this.toolAvailableCol_9.Visible = false;
			this.toolAvailableCol_9.Click += new System.EventHandler(this.toolAvaliableCol_Group_Click);
			// 
			// toolAvailableCol_10
			// 
			this.toolAvailableCol_10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolAvailableCol_10.Image = ((System.Drawing.Image)(resources.GetObject("toolAvailableCol_10.Image")));
			this.toolAvailableCol_10.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolAvailableCol_10.Name = "toolAvailableCol_10";
			this.toolAvailableCol_10.Size = new System.Drawing.Size(23, 22);
			this.toolAvailableCol_10.Text = "X";
			this.toolAvailableCol_10.Visible = false;
			this.toolAvailableCol_10.Click += new System.EventHandler(this.toolAvaliableCol_Group_Click);
			// 
			// scrollSelectedColumns
			// 
			this.scrollSelectedColumns.BackColor = System.Drawing.Color.Transparent;
			this.scrollSelectedColumns.Image = null;
			this.scrollSelectedColumns.Location = new System.Drawing.Point(562, 351);
			this.scrollSelectedColumns.Name = "scrollSelectedColumns";
			this.scrollSelectedColumns.ScrollElementsTotals = 100;
			this.scrollSelectedColumns.ScrollElementsVisible = 20;
			this.scrollSelectedColumns.ScrollHide = false;
			this.scrollSelectedColumns.ScrollNecessary = true;
			this.scrollSelectedColumns.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.scrollSelectedColumns.ScrollPosition = 0;
			this.scrollSelectedColumns.Size = new System.Drawing.Size(17, 202);
			this.scrollSelectedColumns.TabIndex = 18;
			this.scrollSelectedColumns.TabStop = false;
			this.scrollSelectedColumns.Text = "badScrollBar2";
			this.scrollSelectedColumns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollSelectedColumns_MouseDown);
			this.scrollSelectedColumns.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollSelectedColumns_MouseMove);
			this.scrollSelectedColumns.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scrollSelectedColumns_MouseUp);
			// 
			// dataGridSelectedColumns
			// 
			this.dataGridSelectedColumns.AllowUserToAddRows = false;
			this.dataGridSelectedColumns.AllowUserToDeleteRows = false;
			this.dataGridSelectedColumns.AllowUserToOrderColumns = true;
			this.dataGridSelectedColumns.AllowUserToResizeRows = false;
			this.dataGridSelectedColumns.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridSelectedColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridSelectedColumns.Cursor = System.Windows.Forms.Cursors.Default;
			this.dataGridSelectedColumns.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridSelectedColumns.Location = new System.Drawing.Point(345, 351);
			this.dataGridSelectedColumns.Name = "dataGridSelectedColumns";
			this.dataGridSelectedColumns.ReadOnly = true;
			this.dataGridSelectedColumns.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dataGridSelectedColumns.RowHeadersVisible = false;
			this.dataGridSelectedColumns.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dataGridSelectedColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridSelectedColumns.Size = new System.Drawing.Size(217, 202);
			this.dataGridSelectedColumns.TabIndex = 17;
			this.dataGridSelectedColumns.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridSelectedColumns_CellContentDoubleClick);
			this.dataGridSelectedColumns.SelectionChanged += new System.EventHandler(this.dataGridAllTanks_SelectionChanged);
			this.dataGridSelectedColumns.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGrid_Paint);
			// 
			// scrollAllColumns
			// 
			this.scrollAllColumns.BackColor = System.Drawing.Color.Transparent;
			this.scrollAllColumns.Image = null;
			this.scrollAllColumns.Location = new System.Drawing.Point(259, 351);
			this.scrollAllColumns.Name = "scrollAllColumns";
			this.scrollAllColumns.ScrollElementsTotals = 100;
			this.scrollAllColumns.ScrollElementsVisible = 20;
			this.scrollAllColumns.ScrollHide = false;
			this.scrollAllColumns.ScrollNecessary = true;
			this.scrollAllColumns.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.scrollAllColumns.ScrollPosition = 0;
			this.scrollAllColumns.Size = new System.Drawing.Size(17, 202);
			this.scrollAllColumns.TabIndex = 11;
			this.scrollAllColumns.TabStop = false;
			this.scrollAllColumns.Text = "badScrollBar1";
			this.scrollAllColumns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollAllColumns_MouseDown);
			this.scrollAllColumns.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollAllColumns_MouseMove);
			this.scrollAllColumns.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scrollAllColumns_MouseUp);
			// 
			// dataGridAllColumns
			// 
			this.dataGridAllColumns.AllowUserToAddRows = false;
			this.dataGridAllColumns.AllowUserToDeleteRows = false;
			this.dataGridAllColumns.AllowUserToOrderColumns = true;
			this.dataGridAllColumns.AllowUserToResizeRows = false;
			this.dataGridAllColumns.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridAllColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridAllColumns.Cursor = System.Windows.Forms.Cursors.Default;
			this.dataGridAllColumns.Location = new System.Drawing.Point(42, 351);
			this.dataGridAllColumns.Name = "dataGridAllColumns";
			this.dataGridAllColumns.ReadOnly = true;
			this.dataGridAllColumns.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dataGridAllColumns.RowHeadersVisible = false;
			this.dataGridAllColumns.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dataGridAllColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridAllColumns.Size = new System.Drawing.Size(217, 202);
			this.dataGridAllColumns.TabIndex = 10;
			this.dataGridAllColumns.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridAllColumns_CellContentDoubleClick);
			this.dataGridAllColumns.SelectionChanged += new System.EventHandler(this.dataGridAllTanks_SelectionChanged);
			this.dataGridAllColumns.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGrid_Paint);
			// 
			// scrollColumnList
			// 
			this.scrollColumnList.BackColor = System.Drawing.Color.Transparent;
			this.scrollColumnList.Image = null;
			this.scrollColumnList.Location = new System.Drawing.Point(562, 102);
			this.scrollColumnList.Name = "scrollColumnList";
			this.scrollColumnList.ScrollElementsTotals = 100;
			this.scrollColumnList.ScrollElementsVisible = 20;
			this.scrollColumnList.ScrollHide = false;
			this.scrollColumnList.ScrollNecessary = true;
			this.scrollColumnList.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.scrollColumnList.ScrollPosition = 0;
			this.scrollColumnList.Size = new System.Drawing.Size(17, 153);
			this.scrollColumnList.TabIndex = 4;
			this.scrollColumnList.TabStop = false;
			this.scrollColumnList.Text = "badScrollBar1";
			this.scrollColumnList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollColumnList_MouseDown);
			this.scrollColumnList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollColumnList_MouseMove);
			// 
			// dataGridColumnList
			// 
			this.dataGridColumnList.AllowUserToAddRows = false;
			this.dataGridColumnList.AllowUserToDeleteRows = false;
			this.dataGridColumnList.AllowUserToOrderColumns = true;
			this.dataGridColumnList.AllowUserToResizeRows = false;
			this.dataGridColumnList.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridColumnList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridColumnList.Cursor = System.Windows.Forms.Cursors.Default;
			this.dataGridColumnList.Location = new System.Drawing.Point(42, 102);
			this.dataGridColumnList.MultiSelect = false;
			this.dataGridColumnList.Name = "dataGridColumnList";
			this.dataGridColumnList.ReadOnly = true;
			this.dataGridColumnList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dataGridColumnList.RowHeadersVisible = false;
			this.dataGridColumnList.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dataGridColumnList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridColumnList.Size = new System.Drawing.Size(520, 153);
			this.dataGridColumnList.TabIndex = 3;
			this.dataGridColumnList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridColumnList_CellClick);
			this.dataGridColumnList.SelectionChanged += new System.EventHandler(this.dataGridAllTanks_SelectionChanged);
			this.dataGridColumnList.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGrid_Paint);
			// 
			// lblAllColumns
			// 
			this.lblAllColumns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblAllColumns.Dimmed = false;
			this.lblAllColumns.Image = null;
			this.lblAllColumns.Location = new System.Drawing.Point(42, 304);
			this.lblAllColumns.Name = "lblAllColumns";
			this.lblAllColumns.Size = new System.Drawing.Size(152, 23);
			this.lblAllColumns.TabIndex = 7;
			this.lblAllColumns.TabStop = false;
			this.lblAllColumns.Text = "Available Columns:";
			this.lblAllColumns.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// lblSelectedColumns
			// 
			this.lblSelectedColumns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblSelectedColumns.Dimmed = false;
			this.lblSelectedColumns.Image = null;
			this.lblSelectedColumns.Location = new System.Drawing.Point(345, 304);
			this.lblSelectedColumns.Name = "lblSelectedColumns";
			this.lblSelectedColumns.Size = new System.Drawing.Size(149, 23);
			this.lblSelectedColumns.TabIndex = 8;
			this.lblSelectedColumns.TabStop = false;
			this.lblSelectedColumns.Text = "Selected Columns:";
			this.lblSelectedColumns.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// groupTanks
			// 
			this.groupTanks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupTanks.BackColor = System.Drawing.Color.Transparent;
			this.groupTanks.Image = null;
			this.groupTanks.Location = new System.Drawing.Point(25, 284);
			this.groupTanks.Name = "groupTanks";
			this.groupTanks.Size = new System.Drawing.Size(573, 289);
			this.groupTanks.TabIndex = 5;
			this.groupTanks.TabStop = false;
			this.groupTanks.Text = "Columns";
			// 
			// badGroupBox2
			// 
			this.badGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox2.Image = null;
			this.badGroupBox2.Location = new System.Drawing.Point(25, 48);
			this.badGroupBox2.Name = "badGroupBox2";
			this.badGroupBox2.Size = new System.Drawing.Size(572, 225);
			this.badGroupBox2.TabIndex = 1;
			this.badGroupBox2.TabStop = false;
			this.badGroupBox2.Text = "Select View";
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(34, 61);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(39, 23);
			this.badLabel1.TabIndex = 73;
			this.badLabel1.Text = "Type:";
			this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// ColList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 633);
			this.Controls.Add(this.ColListTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MinimumSize = new System.Drawing.Size(607, 591);
			this.Name = "ColList";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ColumnSetup";
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ColList_FormClosing);
			this.Load += new System.EventHandler(this.ColumnSetup_Load);
			this.ResizeEnd += new System.EventHandler(this.ColumnList_ResizeEnd);
			this.LocationChanged += new System.EventHandler(this.ColList_LocationChanged);
			this.Resize += new System.EventHandler(this.ColumnList_Resize);
			this.ColListTheme.ResumeLayout(false);
			this.toolColList.ResumeLayout(false);
			this.toolColList.PerformLayout();
			this.toolSelectedColumns.ResumeLayout(false);
			this.toolSelectedColumns.PerformLayout();
			this.toolAllColumns.ResumeLayout(false);
			this.toolAllColumns.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridSelectedColumns)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridAllColumns)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridColumnList)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm ColListTheme;
		private System.Windows.Forms.ToolStrip toolColList;
		private System.Windows.Forms.ToolStripButton toolColListUp;
		private System.Windows.Forms.ToolStripButton toolColListDown;
		private System.Windows.Forms.ToolStripButton toolColListVisible;
		private System.Windows.Forms.ToolStripButton toolColListDefault;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolColListAdd;
		private System.Windows.Forms.ToolStripButton toolColListModify;
		private System.Windows.Forms.ToolStripButton toolColListDelete;
		private BadButton btnRemoveAll;
		private BadButton btnRemoveSelected;
		private BadButton btnSelectSelected;
		private BadButton btnSelectAll;
		private BadButton btnColumnListCancel;
		private BadButton btnColumnListSave;
		private System.Windows.Forms.ToolStrip toolSelectedColumns;
		private System.Windows.Forms.ToolStripButton toolSelectedTanks_MoveUp;
		private System.Windows.Forms.ToolStripButton toolSelectedTanks_MoveDown;
		private System.Windows.Forms.ToolStrip toolAllColumns;
		private System.Windows.Forms.ToolStripButton toolAvailableCol_All;
		private System.Windows.Forms.ToolStripButton toolAvailableCol_1;
		private System.Windows.Forms.ToolStripButton toolAvailableCol_2;
		private System.Windows.Forms.ToolStripButton toolAvailableCol_3;
		private System.Windows.Forms.ToolStripButton toolAvailableCol_4;
		private System.Windows.Forms.ToolStripButton toolAvailableCol_5;
		private System.Windows.Forms.ToolStripButton toolAvailableCol_6;
		private System.Windows.Forms.ToolStripButton toolAvailableCol_7;
		private System.Windows.Forms.ToolStripButton toolAvailableCol_8;
		private System.Windows.Forms.ToolStripButton toolAvailableCol_9;
		private System.Windows.Forms.ToolStripButton toolAvailableCol_10;
		private BadScrollBar scrollSelectedColumns;
		private System.Windows.Forms.DataGridView dataGridSelectedColumns;
		private BadScrollBar scrollAllColumns;
		private System.Windows.Forms.DataGridView dataGridAllColumns;
		private BadScrollBar scrollColumnList;
		private System.Windows.Forms.DataGridView dataGridColumnList;
		private BadLabel lblAllColumns;
		private BadLabel lblSelectedColumns;
		private BadGroupBox groupTanks;
		private BadGroupBox badGroupBox2;
		private System.Windows.Forms.ImageList imageListToolStrip;
		private BadLabel badLabel1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolColListRefresh;
		private BadButton btnClose;
		private System.Windows.Forms.ToolStripButton toolSelectedTanks_Separator;
		private System.Windows.Forms.ToolStripDropDownButton toolSelectedTanks_Sep;
		private System.Windows.Forms.ToolStripMenuItem toolSelectedTanks_Sep_02;
		private System.Windows.Forms.ToolStripMenuItem toolSelectedTanks_Sep_03;
		private System.Windows.Forms.ToolStripMenuItem toolSelectedTanks_Sep_04;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem toolSelectedTanks_Sep_06;
		private System.Windows.Forms.ToolStripMenuItem toolSelectedTanks_Sep_08;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem toolSelectedTanks_Sep_10;
		private System.Windows.Forms.ToolStripMenuItem toolSelectedTanks_Sep_12;
		private System.Windows.Forms.ToolStripMenuItem toolSelectedTanks_Sep_15;
		private System.Windows.Forms.ToolStripMenuItem toolSelectedTanks_Sep_05;
		private BadButton btnReset;




	}
}