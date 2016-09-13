namespace WinApp.Forms
{
    partial class ChartLineAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartLineAdd));
            this.ChartLineAddTheme = new BadForm();
            this.scrollChartTypes = new BadScrollBar();
            this.dataGridChartTypes = new System.Windows.Forms.DataGridView();
            this.toolStripEx1 = new WinApp.Code.ToolStripEx(this.components);
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.mTxtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.mTankClearFilter = new System.Windows.Forms.ToolStripButton();
            this.mTankShowSelected = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mTankSearchAndSelect = new System.Windows.Forms.ToolStripButton();
            this.mTankUnselect = new System.Windows.Forms.ToolStripButton();
            this.scrollTanks = new BadScrollBar();
            this.dataGridTanks = new System.Windows.Forms.DataGridView();
            this.chkAllTanks = new BadCheckBox();
            this.cmdCancel = new BadButton();
            this.cmdSelect = new BadButton();
            this.badGroupBox2 = new BadGroupBox();
            this.badGroupBox1 = new BadGroupBox();
            this.ChartLineAddTheme.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridChartTypes)).BeginInit();
            this.toolStripEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTanks)).BeginInit();
            this.SuspendLayout();
            // 
            // ChartLineAddTheme
            // 
            this.ChartLineAddTheme.Controls.Add(this.scrollChartTypes);
            this.ChartLineAddTheme.Controls.Add(this.dataGridChartTypes);
            this.ChartLineAddTheme.Controls.Add(this.toolStripEx1);
            this.ChartLineAddTheme.Controls.Add(this.scrollTanks);
            this.ChartLineAddTheme.Controls.Add(this.dataGridTanks);
            this.ChartLineAddTheme.Controls.Add(this.chkAllTanks);
            this.ChartLineAddTheme.Controls.Add(this.cmdCancel);
            this.ChartLineAddTheme.Controls.Add(this.cmdSelect);
            this.ChartLineAddTheme.Controls.Add(this.badGroupBox2);
            this.ChartLineAddTheme.Controls.Add(this.badGroupBox1);
            this.ChartLineAddTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartLineAddTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ChartLineAddTheme.FormExitAsMinimize = false;
            this.ChartLineAddTheme.FormFooter = false;
            this.ChartLineAddTheme.FormFooterHeight = 26;
            this.ChartLineAddTheme.FormInnerBorder = 3;
            this.ChartLineAddTheme.FormMargin = 0;
            this.ChartLineAddTheme.Image = null;
            this.ChartLineAddTheme.Location = new System.Drawing.Point(0, 0);
            this.ChartLineAddTheme.MainArea = mainAreaClass1;
            this.ChartLineAddTheme.Name = "ChartLineAddTheme";
            this.ChartLineAddTheme.Resizable = true;
            this.ChartLineAddTheme.Size = new System.Drawing.Size(700, 417);
            this.ChartLineAddTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("ChartLineAddTheme.SystemExitImage")));
            this.ChartLineAddTheme.SystemMaximizeImage = null;
            this.ChartLineAddTheme.SystemMinimizeImage = null;
            this.ChartLineAddTheme.TabIndex = 0;
            this.ChartLineAddTheme.Text = "Add Chart Value";
            this.ChartLineAddTheme.TitleHeight = 26;
            // 
            // scrollChartTypes
            // 
            this.scrollChartTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollChartTypes.BackColor = System.Drawing.Color.Transparent;
            this.scrollChartTypes.Image = null;
            this.scrollChartTypes.Location = new System.Drawing.Point(645, 68);
            this.scrollChartTypes.Name = "scrollChartTypes";
            this.scrollChartTypes.ScrollElementsTotals = 100;
            this.scrollChartTypes.ScrollElementsVisible = 20;
            this.scrollChartTypes.ScrollHide = false;
            this.scrollChartTypes.ScrollNecessary = true;
            this.scrollChartTypes.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
            this.scrollChartTypes.ScrollPosition = 0;
            this.scrollChartTypes.Size = new System.Drawing.Size(17, 269);
            this.scrollChartTypes.TabIndex = 26;
            this.scrollChartTypes.TabStop = false;
            this.scrollChartTypes.Text = "badScrollBar2";
            this.scrollChartTypes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollChartTypes_MouseDown);
            this.scrollChartTypes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollChartTypes_MouseMove);
            this.scrollChartTypes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scrollChartTypes_MouseUp);
            // 
            // dataGridChartTypes
            // 
            this.dataGridChartTypes.AllowUserToAddRows = false;
            this.dataGridChartTypes.AllowUserToDeleteRows = false;
            this.dataGridChartTypes.AllowUserToOrderColumns = true;
            this.dataGridChartTypes.AllowUserToResizeRows = false;
            this.dataGridChartTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridChartTypes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridChartTypes.ColumnHeadersHeight = 22;
            this.dataGridChartTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridChartTypes.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridChartTypes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridChartTypes.Location = new System.Drawing.Point(442, 68);
            this.dataGridChartTypes.Name = "dataGridChartTypes";
            this.dataGridChartTypes.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridChartTypes.RowHeadersVisible = false;
            this.dataGridChartTypes.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridChartTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridChartTypes.Size = new System.Drawing.Size(205, 269);
            this.dataGridChartTypes.TabIndex = 25;
            this.dataGridChartTypes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridChartTypes_CellClick);
            this.dataGridChartTypes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridChartTypes_CellFormatting);
            // 
            // toolStripEx1
            // 
            this.toolStripEx1.AutoSize = false;
            this.toolStripEx1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.mTxtSearch,
            this.mTankClearFilter,
            this.mTankShowSelected,
            this.toolStripSeparator1,
            this.mTankSearchAndSelect,
            this.mTankUnselect});
            this.toolStripEx1.Location = new System.Drawing.Point(39, 68);
            this.toolStripEx1.Name = "toolStripEx1";
            this.toolStripEx1.Size = new System.Drawing.Size(353, 25);
            this.toolStripEx1.TabIndex = 24;
            this.toolStripEx1.Text = "toolStripEx1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(70, 22);
            this.toolStripLabel1.Text = "Tank Name:";
            // 
            // mTxtSearch
            // 
            this.mTxtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.mTxtSearch.ForeColor = System.Drawing.SystemColors.Window;
            this.mTxtSearch.Name = "mTxtSearch";
            this.mTxtSearch.Size = new System.Drawing.Size(120, 25);
            this.mTxtSearch.TextChanged += new System.EventHandler(this.mTxtSearch_TextChanged);
            // 
            // mTankClearFilter
            // 
            this.mTankClearFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mTankClearFilter.Image = ((System.Drawing.Image)(resources.GetObject("mTankClearFilter.Image")));
            this.mTankClearFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mTankClearFilter.Name = "mTankClearFilter";
            this.mTankClearFilter.Size = new System.Drawing.Size(23, 22);
            this.mTankClearFilter.Text = "toolStripButton1";
            this.mTankClearFilter.ToolTipText = "Clear Filter";
            this.mTankClearFilter.Click += new System.EventHandler(this.mTankClearFilter_Click);
            // 
            // mTankShowSelected
            // 
            this.mTankShowSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mTankShowSelected.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.mTankShowSelected.Image = ((System.Drawing.Image)(resources.GetObject("mTankShowSelected.Image")));
            this.mTankShowSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mTankShowSelected.Name = "mTankShowSelected";
            this.mTankShowSelected.Size = new System.Drawing.Size(23, 22);
            this.mTankShowSelected.ToolTipText = "Show Only Selected";
            this.mTankShowSelected.Click += new System.EventHandler(this.mTankShowSelected_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // mTankSearchAndSelect
            // 
            this.mTankSearchAndSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mTankSearchAndSelect.Image = ((System.Drawing.Image)(resources.GetObject("mTankSearchAndSelect.Image")));
            this.mTankSearchAndSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mTankSearchAndSelect.Name = "mTankSearchAndSelect";
            this.mTankSearchAndSelect.Size = new System.Drawing.Size(23, 22);
            this.mTankSearchAndSelect.Text = "toolStripButton2";
            this.mTankSearchAndSelect.ToolTipText = "Search and Select Tank";
            this.mTankSearchAndSelect.Click += new System.EventHandler(this.mTankSearchAndSelect_Click);
            // 
            // mTankUnselect
            // 
            this.mTankUnselect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mTankUnselect.Image = ((System.Drawing.Image)(resources.GetObject("mTankUnselect.Image")));
            this.mTankUnselect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mTankUnselect.Name = "mTankUnselect";
            this.mTankUnselect.Size = new System.Drawing.Size(23, 22);
            this.mTankUnselect.Text = "toolStripButton1";
            this.mTankUnselect.ToolTipText = "Unselect All Tanks";
            this.mTankUnselect.Click += new System.EventHandler(this.mTankUnselect_Click);
            // 
            // scrollTanks
            // 
            this.scrollTanks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.scrollTanks.BackColor = System.Drawing.Color.Transparent;
            this.scrollTanks.Image = null;
            this.scrollTanks.Location = new System.Drawing.Point(375, 93);
            this.scrollTanks.Name = "scrollTanks";
            this.scrollTanks.ScrollElementsTotals = 100;
            this.scrollTanks.ScrollElementsVisible = 20;
            this.scrollTanks.ScrollHide = false;
            this.scrollTanks.ScrollNecessary = true;
            this.scrollTanks.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
            this.scrollTanks.ScrollPosition = 0;
            this.scrollTanks.Size = new System.Drawing.Size(17, 244);
            this.scrollTanks.TabIndex = 23;
            this.scrollTanks.TabStop = false;
            this.scrollTanks.Text = "badScrollBar2";
            this.scrollTanks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollTank_MouseDown);
            this.scrollTanks.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollTank_MouseMove);
            this.scrollTanks.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scrollTank_MouseUp);
            // 
            // dataGridTanks
            // 
            this.dataGridTanks.AllowUserToAddRows = false;
            this.dataGridTanks.AllowUserToDeleteRows = false;
            this.dataGridTanks.AllowUserToOrderColumns = true;
            this.dataGridTanks.AllowUserToResizeRows = false;
            this.dataGridTanks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridTanks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridTanks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTanks.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridTanks.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridTanks.Location = new System.Drawing.Point(39, 93);
            this.dataGridTanks.Name = "dataGridTanks";
            this.dataGridTanks.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridTanks.RowHeadersVisible = false;
            this.dataGridTanks.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridTanks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridTanks.Size = new System.Drawing.Size(338, 244);
            this.dataGridTanks.TabIndex = 22;
            this.dataGridTanks.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridTanks_CellClick);
            this.dataGridTanks.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridTanks_CellFormatting);
            this.dataGridTanks.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridTanks_CellPainting);
            // 
            // chkAllTanks
            // 
            this.chkAllTanks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAllTanks.BackColor = System.Drawing.Color.Transparent;
            this.chkAllTanks.Checked = false;
            this.chkAllTanks.Image = global::WinApp.Properties.Resources.checkboxcheck;
            this.chkAllTanks.Location = new System.Drawing.Point(21, 374);
            this.chkAllTanks.Name = "chkAllTanks";
            this.chkAllTanks.Size = new System.Drawing.Size(179, 23);
            this.chkAllTanks.TabIndex = 16;
            this.chkAllTanks.Text = "Add Chart Value for All Tanks";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.BlackButton = false;
            this.cmdCancel.Checked = false;
            this.cmdCancel.Image = null;
            this.cmdCancel.Location = new System.Drawing.Point(605, 374);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.ToolTipText = "";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSelect
            // 
            this.cmdSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelect.BlackButton = false;
            this.cmdSelect.Checked = false;
            this.cmdSelect.Image = null;
            this.cmdSelect.Location = new System.Drawing.Point(524, 374);
            this.cmdSelect.Name = "cmdSelect";
            this.cmdSelect.Size = new System.Drawing.Size(75, 23);
            this.cmdSelect.TabIndex = 2;
            this.cmdSelect.Text = "Select";
            this.cmdSelect.ToolTipText = "";
            this.cmdSelect.Click += new System.EventHandler(this.cmdSelect_Click);
            // 
            // badGroupBox2
            // 
            this.badGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox2.Image = null;
            this.badGroupBox2.Location = new System.Drawing.Point(423, 44);
            this.badGroupBox2.Name = "badGroupBox2";
            this.badGroupBox2.Size = new System.Drawing.Size(257, 313);
            this.badGroupBox2.TabIndex = 1;
            this.badGroupBox2.Text = "Select Chart Type";
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(21, 44);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(390, 313);
            this.badGroupBox1.TabIndex = 0;
            this.badGroupBox1.Text = "Select Tank";
            // 
            // ChartLineAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 417);
            this.Controls.Add(this.ChartLineAddTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChartLineAdd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChartLineAdd";
            this.Load += new System.EventHandler(this.ChartLineAdd_Load);
            this.Shown += new System.EventHandler(this.ChartLineAdd_Shown);
            this.ChartLineAddTheme.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridChartTypes)).EndInit();
            this.toolStripEx1.ResumeLayout(false);
            this.toolStripEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTanks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BadForm ChartLineAddTheme;
        private BadButton cmdCancel;
        private BadButton cmdSelect;
        private BadGroupBox badGroupBox2;
        private BadGroupBox badGroupBox1;
        private BadCheckBox chkAllTanks;
        private BadScrollBar scrollTanks;
        private System.Windows.Forms.DataGridView dataGridTanks;
        private Code.ToolStripEx toolStripEx1;
        private System.Windows.Forms.ToolStripButton mTankSearchAndSelect;
        private System.Windows.Forms.ToolStripButton mTankShowSelected;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton mTankUnselect;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton mTankClearFilter;
        private System.Windows.Forms.ToolStripTextBox mTxtSearch;
        private BadScrollBar scrollChartTypes;
        private System.Windows.Forms.DataGridView dataGridChartTypes;
    }
}