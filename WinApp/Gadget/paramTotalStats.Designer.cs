namespace WinApp.Gadget
{
	partial class paramTotalStats
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(paramTotalStats));
            this.paramTotalStatsTheme = new BadForm();
            this.toolSelectedColumns = new System.Windows.Forms.ToolStrip();
            this.toolSelectedTanks_MoveUp = new System.Windows.Forms.ToolStripButton();
            this.toolSelectedTanks_MoveDown = new System.Windows.Forms.ToolStripButton();
            this.dataGridSelectedColumns = new System.Windows.Forms.DataGridView();
            this.lblSelectedColumns = new BadLabel();
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
            this.toolAvailableCol_10 = new System.Windows.Forms.ToolStripButton();
            this.toolAvailableCol_9 = new System.Windows.Forms.ToolStripButton();
            this.scrollAllColumns = new BadScrollBar();
            this.dataGridAllColumns = new System.Windows.Forms.DataGridView();
            this.btnDefault = new BadButton();
            this.ddGridCount = new BadDropDownBox();
            this.badLabel3 = new BadLabel();
            this.ddTimeSpan = new BadDropDownBox();
            this.badLabel2 = new BadLabel();
            this.btnCancel = new BadButton();
            this.btnSelect = new BadButton();
            this.ddBattleMode = new BadDropDownBox();
            this.badLabel1 = new BadLabel();
            this.badGroupBox1 = new BadGroupBox();
            this.lblAllColumns = new BadLabel();
            this.groupRows = new BadGroupBox();
            this.paramTotalStatsTheme.SuspendLayout();
            this.toolSelectedColumns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSelectedColumns)).BeginInit();
            this.toolAllColumns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAllColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // paramTotalStatsTheme
            // 
            this.paramTotalStatsTheme.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.paramTotalStatsTheme.Controls.Add(this.toolSelectedColumns);
            this.paramTotalStatsTheme.Controls.Add(this.dataGridSelectedColumns);
            this.paramTotalStatsTheme.Controls.Add(this.lblSelectedColumns);
            this.paramTotalStatsTheme.Controls.Add(this.toolAllColumns);
            this.paramTotalStatsTheme.Controls.Add(this.scrollAllColumns);
            this.paramTotalStatsTheme.Controls.Add(this.dataGridAllColumns);
            this.paramTotalStatsTheme.Controls.Add(this.btnDefault);
            this.paramTotalStatsTheme.Controls.Add(this.ddGridCount);
            this.paramTotalStatsTheme.Controls.Add(this.badLabel3);
            this.paramTotalStatsTheme.Controls.Add(this.ddTimeSpan);
            this.paramTotalStatsTheme.Controls.Add(this.badLabel2);
            this.paramTotalStatsTheme.Controls.Add(this.btnCancel);
            this.paramTotalStatsTheme.Controls.Add(this.btnSelect);
            this.paramTotalStatsTheme.Controls.Add(this.ddBattleMode);
            this.paramTotalStatsTheme.Controls.Add(this.badLabel1);
            this.paramTotalStatsTheme.Controls.Add(this.badGroupBox1);
            this.paramTotalStatsTheme.Controls.Add(this.lblAllColumns);
            this.paramTotalStatsTheme.Controls.Add(this.groupRows);
            this.paramTotalStatsTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paramTotalStatsTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.paramTotalStatsTheme.FormExitAsMinimize = false;
            this.paramTotalStatsTheme.FormFooter = false;
            this.paramTotalStatsTheme.FormFooterHeight = 26;
            this.paramTotalStatsTheme.FormInnerBorder = 3;
            this.paramTotalStatsTheme.FormMargin = 0;
            this.paramTotalStatsTheme.Image = null;
            this.paramTotalStatsTheme.Location = new System.Drawing.Point(0, 0);
            this.paramTotalStatsTheme.MainArea = mainAreaClass1;
            this.paramTotalStatsTheme.Name = "paramTotalStatsTheme";
            this.paramTotalStatsTheme.Resizable = true;
            this.paramTotalStatsTheme.Size = new System.Drawing.Size(713, 482);
            this.paramTotalStatsTheme.SystemExitImage = null;
            this.paramTotalStatsTheme.SystemMaximizeImage = null;
            this.paramTotalStatsTheme.SystemMinimizeImage = null;
            this.paramTotalStatsTheme.TabIndex = 0;
            this.paramTotalStatsTheme.Text = "Total Stats Parameters";
            this.paramTotalStatsTheme.TitleHeight = 26;
            this.paramTotalStatsTheme.Resize += new System.EventHandler(this.badForm1_Resize);
            // 
            // toolSelectedColumns
            // 
            this.toolSelectedColumns.AutoSize = false;
            this.toolSelectedColumns.Dock = System.Windows.Forms.DockStyle.None;
            this.toolSelectedColumns.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolSelectedColumns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSelectedTanks_MoveUp,
            this.toolSelectedTanks_MoveDown});
            this.toolSelectedColumns.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolSelectedColumns.Location = new System.Drawing.Point(304, 173);
            this.toolSelectedColumns.Name = "toolSelectedColumns";
            this.toolSelectedColumns.Size = new System.Drawing.Size(358, 25);
            this.toolSelectedColumns.Stretch = true;
            this.toolSelectedColumns.TabIndex = 22;
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
            // 
            // dataGridSelectedColumns
            // 
            this.dataGridSelectedColumns.AllowUserToAddRows = false;
            this.dataGridSelectedColumns.AllowUserToDeleteRows = false;
            this.dataGridSelectedColumns.AllowUserToOrderColumns = true;
            this.dataGridSelectedColumns.AllowUserToResizeRows = false;
            this.dataGridSelectedColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridSelectedColumns.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridSelectedColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSelectedColumns.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridSelectedColumns.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridSelectedColumns.Location = new System.Drawing.Point(304, 198);
            this.dataGridSelectedColumns.Name = "dataGridSelectedColumns";
            this.dataGridSelectedColumns.ReadOnly = true;
            this.dataGridSelectedColumns.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridSelectedColumns.RowHeadersVisible = false;
            this.dataGridSelectedColumns.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridSelectedColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridSelectedColumns.Size = new System.Drawing.Size(358, 202);
            this.dataGridSelectedColumns.TabIndex = 23;
            // 
            // lblSelectedColumns
            // 
            this.lblSelectedColumns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblSelectedColumns.Dimmed = false;
            this.lblSelectedColumns.Image = null;
            this.lblSelectedColumns.Location = new System.Drawing.Point(304, 151);
            this.lblSelectedColumns.Name = "lblSelectedColumns";
            this.lblSelectedColumns.Size = new System.Drawing.Size(149, 23);
            this.lblSelectedColumns.TabIndex = 21;
            this.lblSelectedColumns.TabStop = false;
            this.lblSelectedColumns.Text = "Selected Columns:";
            this.lblSelectedColumns.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
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
            this.toolAvailableCol_10,
            this.toolAvailableCol_9});
            this.toolAllColumns.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolAllColumns.Location = new System.Drawing.Point(39, 173);
            this.toolAllColumns.Name = "toolAllColumns";
            this.toolAllColumns.Size = new System.Drawing.Size(234, 25);
            this.toolAllColumns.Stretch = true;
            this.toolAllColumns.TabIndex = 17;
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
            // scrollAllColumns
            // 
            this.scrollAllColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.scrollAllColumns.BackColor = System.Drawing.Color.Transparent;
            this.scrollAllColumns.Image = null;
            this.scrollAllColumns.Location = new System.Drawing.Point(256, 198);
            this.scrollAllColumns.Name = "scrollAllColumns";
            this.scrollAllColumns.ScrollElementsTotals = 100;
            this.scrollAllColumns.ScrollElementsVisible = 20;
            this.scrollAllColumns.ScrollHide = false;
            this.scrollAllColumns.ScrollNecessary = true;
            this.scrollAllColumns.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
            this.scrollAllColumns.ScrollPosition = 0;
            this.scrollAllColumns.Size = new System.Drawing.Size(17, 202);
            this.scrollAllColumns.TabIndex = 20;
            this.scrollAllColumns.TabStop = false;
            this.scrollAllColumns.Text = "badScrollBar1";
            // 
            // dataGridAllColumns
            // 
            this.dataGridAllColumns.AllowUserToAddRows = false;
            this.dataGridAllColumns.AllowUserToDeleteRows = false;
            this.dataGridAllColumns.AllowUserToOrderColumns = true;
            this.dataGridAllColumns.AllowUserToResizeRows = false;
            this.dataGridAllColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridAllColumns.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridAllColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAllColumns.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridAllColumns.Location = new System.Drawing.Point(39, 198);
            this.dataGridAllColumns.Name = "dataGridAllColumns";
            this.dataGridAllColumns.ReadOnly = true;
            this.dataGridAllColumns.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridAllColumns.RowHeadersVisible = false;
            this.dataGridAllColumns.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridAllColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridAllColumns.Size = new System.Drawing.Size(217, 202);
            this.dataGridAllColumns.TabIndex = 19;
            // 
            // btnDefault
            // 
            this.btnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefault.BlackButton = false;
            this.btnDefault.Checked = false;
            this.btnDefault.Image = null;
            this.btnDefault.Location = new System.Drawing.Point(482, 436);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(64, 23);
            this.btnDefault.TabIndex = 11;
            this.btnDefault.Text = "Default";
            this.btnDefault.ToolTipText = "";
            // 
            // ddGridCount
            // 
            this.ddGridCount.Image = null;
            this.ddGridCount.Location = new System.Drawing.Point(610, 71);
            this.ddGridCount.Name = "ddGridCount";
            this.ddGridCount.Size = new System.Drawing.Size(52, 23);
            this.ddGridCount.TabIndex = 7;
            this.ddGridCount.Text = "3";
            this.ddGridCount.TextChanged += new System.EventHandler(this.ddGridCount_TextChanged);
            this.ddGridCount.Click += new System.EventHandler(this.ddGridCount_Click);
            // 
            // badLabel3
            // 
            this.badLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel3.Dimmed = false;
            this.badLabel3.Image = null;
            this.badLabel3.Location = new System.Drawing.Point(553, 71);
            this.badLabel3.Name = "badLabel3";
            this.badLabel3.Size = new System.Drawing.Size(62, 23);
            this.badLabel3.TabIndex = 6;
            this.badLabel3.Text = "Columns:";
            this.badLabel3.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // ddTimeSpan
            // 
            this.ddTimeSpan.Image = null;
            this.ddTimeSpan.Location = new System.Drawing.Point(372, 71);
            this.ddTimeSpan.Name = "ddTimeSpan";
            this.ddTimeSpan.Size = new System.Drawing.Size(134, 23);
            this.ddTimeSpan.TabIndex = 5;
            this.ddTimeSpan.Click += new System.EventHandler(this.ddTimeSpan_Click);
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(304, 71);
            this.badLabel2.Name = "badLabel2";
            this.badLabel2.Size = new System.Drawing.Size(62, 23);
            this.badLabel2.TabIndex = 4;
            this.badLabel2.Text = "Timespan:";
            this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BlackButton = false;
            this.btnCancel.Checked = false;
            this.btnCancel.Image = null;
            this.btnCancel.Location = new System.Drawing.Point(622, 436);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTipText = "";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.BlackButton = false;
            this.btnSelect.Checked = false;
            this.btnSelect.Image = null;
            this.btnSelect.Location = new System.Drawing.Point(552, 436);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(64, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Save";
            this.btnSelect.ToolTipText = "";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // ddBattleMode
            // 
            this.ddBattleMode.Image = null;
            this.ddBattleMode.Location = new System.Drawing.Point(125, 71);
            this.ddBattleMode.Name = "ddBattleMode";
            this.ddBattleMode.Size = new System.Drawing.Size(134, 23);
            this.ddBattleMode.TabIndex = 1;
            this.ddBattleMode.Click += new System.EventHandler(this.ddBattleMode_Click);
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(47, 71);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(76, 23);
            this.badLabel1.TabIndex = 0;
            this.badLabel1.Text = "Battle Mode:";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(22, 45);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(664, 69);
            this.badGroupBox1.TabIndex = 8;
            this.badGroupBox1.Text = "Settings";
            // 
            // lblAllColumns
            // 
            this.lblAllColumns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblAllColumns.Dimmed = false;
            this.lblAllColumns.Image = null;
            this.lblAllColumns.Location = new System.Drawing.Point(39, 151);
            this.lblAllColumns.Name = "lblAllColumns";
            this.lblAllColumns.Size = new System.Drawing.Size(152, 23);
            this.lblAllColumns.TabIndex = 18;
            this.lblAllColumns.TabStop = false;
            this.lblAllColumns.Text = "Available Columns:";
            this.lblAllColumns.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // groupRows
            // 
            this.groupRows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupRows.BackColor = System.Drawing.Color.Transparent;
            this.groupRows.Image = null;
            this.groupRows.Location = new System.Drawing.Point(22, 131);
            this.groupRows.Name = "groupRows";
            this.groupRows.Size = new System.Drawing.Size(664, 289);
            this.groupRows.TabIndex = 16;
            this.groupRows.TabStop = false;
            this.groupRows.Text = "Rows";
            // 
            // paramTotalStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(713, 482);
            this.Controls.Add(this.paramTotalStatsTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(713, 482);
            this.Name = "paramTotalStats";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ucGaugeWinRateParameter";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.paramTotalStats_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.paramBattleMode_Paint);
            this.paramTotalStatsTheme.ResumeLayout(false);
            this.toolSelectedColumns.ResumeLayout(false);
            this.toolSelectedColumns.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSelectedColumns)).EndInit();
            this.toolAllColumns.ResumeLayout(false);
            this.toolAllColumns.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAllColumns)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private BadForm paramTotalStatsTheme;
		private BadButton btnSelect;
		private BadDropDownBox ddBattleMode;
		private BadLabel badLabel1;
		private BadButton btnCancel;
        private BadDropDownBox ddTimeSpan;
        private BadLabel badLabel2;
        private BadDropDownBox ddGridCount;
        private BadLabel badLabel3;
        private BadButton btnDefault;
        private BadGroupBox badGroupBox1;
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
        private BadScrollBar scrollAllColumns;
        private System.Windows.Forms.DataGridView dataGridAllColumns;
        private BadLabel lblAllColumns;
        private BadGroupBox groupRows;
        private System.Windows.Forms.ToolStrip toolSelectedColumns;
        private System.Windows.Forms.ToolStripButton toolSelectedTanks_MoveUp;
        private System.Windows.Forms.ToolStripButton toolSelectedTanks_MoveDown;
        private System.Windows.Forms.DataGridView dataGridSelectedColumns;
        private BadLabel lblSelectedColumns;
	}
}