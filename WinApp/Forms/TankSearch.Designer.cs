namespace WinApp.Forms
{
    partial class TankSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TankSearch));
            BadThemeContainerControl.MainAreaClass mainAreaClass1 = new BadThemeContainerControl.MainAreaClass();
            this.imageListTierIcons = new System.Windows.Forms.ImageList(this.components);
            this.imageListSelectionMode = new System.Windows.Forms.ImageList(this.components);
            this.imageListMainMode = new System.Windows.Forms.ImageList(this.components);
            this.TankSearchTheme = new BadForm();
            this.scrollAllTanks = new BadScrollBar();
            this.dataGridTanks = new System.Windows.Forms.DataGridView();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.mMainMode = new System.Windows.Forms.ToolStripButton();
            this.mSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mLabel = new System.Windows.Forms.ToolStripLabel();
            this.mTxtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.mResetSearch = new System.Windows.Forms.ToolStripButton();
            this.mSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mNationSelectMode = new System.Windows.Forms.ToolStripButton();
            this.mNation0 = new System.Windows.Forms.ToolStripButton();
            this.mNation1 = new System.Windows.Forms.ToolStripButton();
            this.mNation2 = new System.Windows.Forms.ToolStripButton();
            this.mNation3 = new System.Windows.Forms.ToolStripButton();
            this.mNation4 = new System.Windows.Forms.ToolStripButton();
            this.mNation5 = new System.Windows.Forms.ToolStripButton();
            this.mNation6 = new System.Windows.Forms.ToolStripButton();
            this.mNation7 = new System.Windows.Forms.ToolStripButton();
            this.mNationToggleAll = new System.Windows.Forms.ToolStripButton();
            this.TankSearchTheme.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTanks)).BeginInit();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageListTierIcons
            // 
            this.imageListTierIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTierIcons.ImageStream")));
            this.imageListTierIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTierIcons.Images.SetKeyName(0, "1.png");
            this.imageListTierIcons.Images.SetKeyName(1, "2.png");
            this.imageListTierIcons.Images.SetKeyName(2, "3.png");
            this.imageListTierIcons.Images.SetKeyName(3, "4.png");
            this.imageListTierIcons.Images.SetKeyName(4, "5.png");
            this.imageListTierIcons.Images.SetKeyName(5, "6.png");
            this.imageListTierIcons.Images.SetKeyName(6, "7.png");
            this.imageListTierIcons.Images.SetKeyName(7, "8.png");
            this.imageListTierIcons.Images.SetKeyName(8, "9.png");
            this.imageListTierIcons.Images.SetKeyName(9, "10.png");
            // 
            // imageListSelectionMode
            // 
            this.imageListSelectionMode.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSelectionMode.ImageStream")));
            this.imageListSelectionMode.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSelectionMode.Images.SetKeyName(0, "modeSingle.png");
            this.imageListSelectionMode.Images.SetKeyName(1, "modeMulti.png");
            // 
            // imageListMainMode
            // 
            this.imageListMainMode.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMainMode.ImageStream")));
            this.imageListMainMode.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListMainMode.Images.SetKeyName(0, "mode_grid.png");
            this.imageListMainMode.Images.SetKeyName(1, "mode_list.png");
            // 
            // TankSearchTheme
            // 
            this.TankSearchTheme.BackColor = System.Drawing.Color.Fuchsia;
            this.TankSearchTheme.Controls.Add(this.scrollAllTanks);
            this.TankSearchTheme.Controls.Add(this.dataGridTanks);
            this.TankSearchTheme.Controls.Add(this.toolStripMain);
            this.TankSearchTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TankSearchTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TankSearchTheme.FormExitAsMinimize = false;
            this.TankSearchTheme.FormFooter = false;
            this.TankSearchTheme.FormFooterHeight = 26;
            this.TankSearchTheme.FormInnerBorder = 3;
            this.TankSearchTheme.FormMargin = 0;
            this.TankSearchTheme.Image = ((System.Drawing.Image)(resources.GetObject("TankSearchTheme.Image")));
            this.TankSearchTheme.Location = new System.Drawing.Point(0, 0);
            this.TankSearchTheme.MainArea = mainAreaClass1;
            this.TankSearchTheme.Name = "TankSearchTheme";
            this.TankSearchTheme.Resizable = true;
            this.TankSearchTheme.Size = new System.Drawing.Size(855, 635);
            this.TankSearchTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("TankSearchTheme.SystemExitImage")));
            this.TankSearchTheme.SystemMaximizeImage = null;
            this.TankSearchTheme.SystemMinimizeImage = null;
            this.TankSearchTheme.TabIndex = 0;
            this.TankSearchTheme.Text = "Search for Tank";
            this.TankSearchTheme.TitleHeight = 26;
            // 
            // scrollAllTanks
            // 
            this.scrollAllTanks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollAllTanks.BackColor = System.Drawing.Color.Transparent;
            this.scrollAllTanks.Image = null;
            this.scrollAllTanks.Location = new System.Drawing.Point(826, 63);
            this.scrollAllTanks.Name = "scrollAllTanks";
            this.scrollAllTanks.ScrollElementsTotals = 100;
            this.scrollAllTanks.ScrollElementsVisible = 20;
            this.scrollAllTanks.ScrollHide = false;
            this.scrollAllTanks.ScrollNecessary = true;
            this.scrollAllTanks.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
            this.scrollAllTanks.ScrollPosition = 0;
            this.scrollAllTanks.Size = new System.Drawing.Size(17, 558);
            this.scrollAllTanks.TabIndex = 9;
            this.scrollAllTanks.Text = "badScrollBar1";
            this.scrollAllTanks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollAllTanks_MouseDown);
            this.scrollAllTanks.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollAllTanks_MouseMove);
            this.scrollAllTanks.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scrollAllTanks_MouseUp);
            // 
            // dataGridTanks
            // 
            this.dataGridTanks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridTanks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridTanks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTanks.Location = new System.Drawing.Point(12, 63);
            this.dataGridTanks.Name = "dataGridTanks";
            this.dataGridTanks.Size = new System.Drawing.Size(814, 558);
            this.dataGridTanks.TabIndex = 8;
            this.dataGridTanks.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridTanks_CellContentDoubleClick);
            this.dataGridTanks.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridTanks_CellPainting);
            this.dataGridTanks.SelectionChanged += new System.EventHandler(this.dataGridAllTanks_SelectionChanged);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStripMain.AutoSize = false;
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMainMode,
            this.mSeparator1,
            this.mLabel,
            this.mTxtSearch,
            this.mResetSearch,
            this.mSeparator2,
            this.mNationSelectMode,
            this.mNation0,
            this.mNation1,
            this.mNation2,
            this.mNation3,
            this.mNation4,
            this.mNation5,
            this.mNation6,
            this.mNation7,
            this.mNationToggleAll});
            this.toolStripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripMain.Location = new System.Drawing.Point(12, 38);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(831, 25);
            this.toolStripMain.Stretch = true;
            this.toolStripMain.TabIndex = 7;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // mMainMode
            // 
            this.mMainMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mMainMode.Image = ((System.Drawing.Image)(resources.GetObject("mMainMode.Image")));
            this.mMainMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mMainMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mMainMode.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.mMainMode.Name = "mMainMode";
            this.mMainMode.Size = new System.Drawing.Size(23, 22);
            this.mMainMode.Text = "toolStripButton1";
            this.mMainMode.ToolTipText = "Select advanced or simple mode";
            this.mMainMode.Click += new System.EventHandler(this.mMainMode_Click);
            // 
            // mSeparator1
            // 
            this.mSeparator1.Name = "mSeparator1";
            this.mSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // mLabel
            // 
            this.mLabel.Name = "mLabel";
            this.mLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.mLabel.Size = new System.Drawing.Size(76, 22);
            this.mLabel.Text = "  Tank Name:";
            // 
            // mTxtSearch
            // 
            this.mTxtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.mTxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mTxtSearch.ForeColor = System.Drawing.SystemColors.Window;
            this.mTxtSearch.Name = "mTxtSearch";
            this.mTxtSearch.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.mTxtSearch.Size = new System.Drawing.Size(100, 25);
            this.mTxtSearch.TextChanged += new System.EventHandler(this.mTxtSearch_TextChanged);
            // 
            // mResetSearch
            // 
            this.mResetSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mResetSearch.Image = ((System.Drawing.Image)(resources.GetObject("mResetSearch.Image")));
            this.mResetSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mResetSearch.Name = "mResetSearch";
            this.mResetSearch.Size = new System.Drawing.Size(23, 22);
            this.mResetSearch.Text = "toolStripButton1";
            this.mResetSearch.ToolTipText = "Reset search";
            this.mResetSearch.Click += new System.EventHandler(this.mResetSearch_Click);
            // 
            // mSeparator2
            // 
            this.mSeparator2.Name = "mSeparator2";
            this.mSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // mNationSelectMode
            // 
            this.mNationSelectMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mNationSelectMode.Image = ((System.Drawing.Image)(resources.GetObject("mNationSelectMode.Image")));
            this.mNationSelectMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mNationSelectMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mNationSelectMode.Name = "mNationSelectMode";
            this.mNationSelectMode.Size = new System.Drawing.Size(35, 22);
            this.mNationSelectMode.Text = "Single";
            this.mNationSelectMode.ToolTipText = "Toggle selection mode (Single / Multi)";
            this.mNationSelectMode.Click += new System.EventHandler(this.mNationSelectMode_Click);
            // 
            // mNation0
            // 
            this.mNation0.Image = ((System.Drawing.Image)(resources.GetObject("mNation0.Image")));
            this.mNation0.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mNation0.Name = "mNation0";
            this.mNation0.Size = new System.Drawing.Size(66, 22);
            this.mNation0.Text = "U.S.S.R.";
            this.mNation0.Click += new System.EventHandler(this.mNation_Click);
            // 
            // mNation1
            // 
            this.mNation1.Image = ((System.Drawing.Image)(resources.GetObject("mNation1.Image")));
            this.mNation1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mNation1.Name = "mNation1";
            this.mNation1.Size = new System.Drawing.Size(75, 22);
            this.mNation1.Text = "Germany";
            this.mNation1.Click += new System.EventHandler(this.mNation_Click);
            // 
            // mNation2
            // 
            this.mNation2.Image = ((System.Drawing.Image)(resources.GetObject("mNation2.Image")));
            this.mNation2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mNation2.Name = "mNation2";
            this.mNation2.Size = new System.Drawing.Size(58, 22);
            this.mNation2.Text = "U.S.A.";
            this.mNation2.Click += new System.EventHandler(this.mNation_Click);
            // 
            // mNation3
            // 
            this.mNation3.Image = ((System.Drawing.Image)(resources.GetObject("mNation3.Image")));
            this.mNation3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mNation3.Name = "mNation3";
            this.mNation3.Size = new System.Drawing.Size(58, 22);
            this.mNation3.Text = "China";
            this.mNation3.Click += new System.EventHandler(this.mNation_Click);
            // 
            // mNation4
            // 
            this.mNation4.Image = ((System.Drawing.Image)(resources.GetObject("mNation4.Image")));
            this.mNation4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mNation4.Name = "mNation4";
            this.mNation4.Size = new System.Drawing.Size(62, 22);
            this.mNation4.Text = "France";
            this.mNation4.Click += new System.EventHandler(this.mNation_Click);
            // 
            // mNation5
            // 
            this.mNation5.Image = ((System.Drawing.Image)(resources.GetObject("mNation5.Image")));
            this.mNation5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mNation5.Name = "mNation5";
            this.mNation5.Size = new System.Drawing.Size(48, 22);
            this.mNation5.Text = "U.K.";
            this.mNation5.Click += new System.EventHandler(this.mNation_Click);
            // 
            // mNation6
            // 
            this.mNation6.Image = ((System.Drawing.Image)(resources.GetObject("mNation6.Image")));
            this.mNation6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mNation6.Name = "mNation6";
            this.mNation6.Size = new System.Drawing.Size(57, 22);
            this.mNation6.Text = "Japan";
            this.mNation6.Click += new System.EventHandler(this.mNation_Click);
            // 
            // mNation7
            // 
            this.mNation7.Image = ((System.Drawing.Image)(resources.GetObject("mNation7.Image")));
            this.mNation7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mNation7.Name = "mNation7";
            this.mNation7.Size = new System.Drawing.Size(59, 22);
            this.mNation7.Text = "Czech";
            this.mNation7.Click += new System.EventHandler(this.mNation_Click);
            // 
            // mNationToggleAll
            // 
            this.mNationToggleAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mNationToggleAll.Image = ((System.Drawing.Image)(resources.GetObject("mNationToggleAll.Image")));
            this.mNationToggleAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mNationToggleAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mNationToggleAll.Name = "mNationToggleAll";
            this.mNationToggleAll.Size = new System.Drawing.Size(35, 22);
            this.mNationToggleAll.Text = "All";
            this.mNationToggleAll.ToolTipText = "Select or deselect all Nations";
            this.mNationToggleAll.Visible = false;
            this.mNationToggleAll.Click += new System.EventHandler(this.mNationToggleAll_Click);
            // 
            // TankSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Fuchsia;
            this.ClientSize = new System.Drawing.Size(855, 635);
            this.Controls.Add(this.TankSearchTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TankSearch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TankSearch";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TankSearch_FormClosing);
            this.Load += new System.EventHandler(this.TankSearch_Load);
            this.Shown += new System.EventHandler(this.TankSearch_Shown);
            this.Resize += new System.EventHandler(this.TankSearch_Resize);
            this.TankSearchTheme.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTanks)).EndInit();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BadForm TankSearchTheme;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripLabel mLabel;
        private System.Windows.Forms.ToolStripTextBox mTxtSearch;
        private System.Windows.Forms.ToolStripButton mNationToggleAll;
        private System.Windows.Forms.ToolStripButton mNation7;
        private System.Windows.Forms.ToolStripButton mNation0;
        private System.Windows.Forms.ToolStripButton mNation1;
        private System.Windows.Forms.ToolStripButton mNation2;
        private System.Windows.Forms.ToolStripButton mNation3;
        private System.Windows.Forms.ToolStripButton mNation4;
        private System.Windows.Forms.ToolStripButton mNation5;
        private System.Windows.Forms.ToolStripButton mNation6;
        private System.Windows.Forms.ToolStripButton mNationSelectMode;
        private BadScrollBar scrollAllTanks;
        private System.Windows.Forms.DataGridView dataGridTanks;
        private System.Windows.Forms.ImageList imageListTierIcons;
        private System.Windows.Forms.ToolStripSeparator mSeparator1;
        private System.Windows.Forms.ToolStripButton mResetSearch;
        private System.Windows.Forms.ImageList imageListSelectionMode;
        private System.Windows.Forms.ToolStripButton mMainMode;
        private System.Windows.Forms.ToolStripSeparator mSeparator2;
        private System.Windows.Forms.ImageList imageListMainMode;
    }
}