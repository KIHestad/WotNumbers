namespace WinApp.Forms
{
	partial class BattleReview
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleReview));
            this.picIllustration = new System.Windows.Forms.PictureBox();
            this.dgvReviews = new System.Windows.Forms.DataGridView();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.panelComment = new System.Windows.Forms.Panel();
            this.panelMap = new System.Windows.Forms.Panel();
            this.picPaint = new System.Windows.Forms.PictureBox();
            this.toolStripPaint = new System.Windows.Forms.ToolStrip();
            this.mSave = new System.Windows.Forms.ToolStripButton();
            this.mClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mPenWhite = new System.Windows.Forms.ToolStripButton();
            this.mPenBlack = new System.Windows.Forms.ToolStripButton();
            this.mPenRed = new System.Windows.Forms.ToolStripButton();
            this.mPenOrange = new System.Windows.Forms.ToolStripButton();
            this.mPenYellow = new System.Windows.Forms.ToolStripButton();
            this.mPenGreen = new System.Windows.Forms.ToolStripButton();
            this.mPenBlue = new System.Windows.Forms.ToolStripButton();
            this.mPenPink = new System.Windows.Forms.ToolStripButton();
            this.mEraser = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mSizeSmall = new System.Windows.Forms.ToolStripButton();
            this.mSizeMedium = new System.Windows.Forms.ToolStripButton();
            this.mSizeLarge = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mShowInSeparateWindow = new System.Windows.Forms.ToolStripButton();
            this.imageListCursors = new System.Windows.Forms.ImageList(this.components);
            this.scrollComment = new BadScrollBar();
            this.txtComment = new BadTextBox();
            this.btnCommentClear = new BadButton();
            this.btnCommentCancel = new BadButton();
            this.btnCommentSave = new BadButton();
            this.scrollDgv = new BadScrollBar();
            this.chkTank = new BadCheckBox();
            this.chkBattleMode = new BadCheckBox();
            this.chkMap = new BadCheckBox();
            this.chkClan = new BadCheckBox();
            this.grpOtherBtlReviews = new BadGroupBox();
            this.lblMapDescription = new BadLabel();
            this.badGroupBox1 = new BadGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.picIllustration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).BeginInit();
            this.panelGrid.SuspendLayout();
            this.panelComment.SuspendLayout();
            this.panelMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPaint)).BeginInit();
            this.toolStripPaint.SuspendLayout();
            this.SuspendLayout();
            // 
            // picIllustration
            // 
            this.picIllustration.Location = new System.Drawing.Point(285, 0);
            this.picIllustration.Name = "picIllustration";
            this.picIllustration.Size = new System.Drawing.Size(153, 65);
            this.picIllustration.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIllustration.TabIndex = 7;
            this.picIllustration.TabStop = false;
            // 
            // dgvReviews
            // 
            this.dgvReviews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReviews.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReviews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReviews.Location = new System.Drawing.Point(19, 58);
            this.dgvReviews.Name = "dgvReviews";
            this.dgvReviews.Size = new System.Drawing.Size(388, 289);
            this.dgvReviews.TabIndex = 16;
            this.dgvReviews.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReviews_CellDoubleClick);
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvReviews);
            this.panelGrid.Controls.Add(this.scrollDgv);
            this.panelGrid.Controls.Add(this.chkTank);
            this.panelGrid.Controls.Add(this.chkBattleMode);
            this.panelGrid.Controls.Add(this.chkMap);
            this.panelGrid.Controls.Add(this.chkClan);
            this.panelGrid.Controls.Add(this.grpOtherBtlReviews);
            this.panelGrid.Location = new System.Drawing.Point(364, 92);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(438, 364);
            this.panelGrid.TabIndex = 18;
            // 
            // panelComment
            // 
            this.panelComment.Controls.Add(this.scrollComment);
            this.panelComment.Controls.Add(this.txtComment);
            this.panelComment.Controls.Add(this.btnCommentClear);
            this.panelComment.Controls.Add(this.picIllustration);
            this.panelComment.Controls.Add(this.btnCommentCancel);
            this.panelComment.Controls.Add(this.btnCommentSave);
            this.panelComment.Location = new System.Drawing.Point(364, 15);
            this.panelComment.Name = "panelComment";
            this.panelComment.Size = new System.Drawing.Size(438, 65);
            this.panelComment.TabIndex = 19;
            // 
            // panelMap
            // 
            this.panelMap.Controls.Add(this.picPaint);
            this.panelMap.Location = new System.Drawing.Point(15, 15);
            this.panelMap.Name = "panelMap";
            this.panelMap.Size = new System.Drawing.Size(315, 300);
            this.panelMap.TabIndex = 20;
            // 
            // picPaint
            // 
            this.picPaint.BackColor = System.Drawing.Color.Transparent;
            this.picPaint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picPaint.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picPaint.Location = new System.Drawing.Point(7, 0);
            this.picPaint.Name = "picPaint";
            this.picPaint.Size = new System.Drawing.Size(300, 300);
            this.picPaint.TabIndex = 0;
            this.picPaint.TabStop = false;
            this.picPaint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picPaint_MouseDown);
            this.picPaint.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picPaint_MouseMove);
            this.picPaint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picPaint_MouseUp);
            // 
            // toolStripPaint
            // 
            this.toolStripPaint.AutoSize = false;
            this.toolStripPaint.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripPaint.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripPaint.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSave,
            this.mClear,
            this.toolStripSeparator1,
            this.mPenWhite,
            this.mPenBlack,
            this.mPenRed,
            this.mPenOrange,
            this.mPenYellow,
            this.mPenGreen,
            this.mPenBlue,
            this.mPenPink,
            this.mEraser,
            this.toolStripSeparator2,
            this.mSizeSmall,
            this.mSizeMedium,
            this.mSizeLarge,
            this.toolStripSeparator3,
            this.mShowInSeparateWindow});
            this.toolStripPaint.Location = new System.Drawing.Point(15, 315);
            this.toolStripPaint.Name = "toolStripPaint";
            this.toolStripPaint.Size = new System.Drawing.Size(315, 25);
            this.toolStripPaint.TabIndex = 21;
            this.toolStripPaint.Tag = "1";
            this.toolStripPaint.Text = "toolStrip1";
            // 
            // mSave
            // 
            this.mSave.AutoSize = false;
            this.mSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mSave.Image = ((System.Drawing.Image)(resources.GetObject("mSave.Image")));
            this.mSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mSave.Name = "mSave";
            this.mSave.Size = new System.Drawing.Size(21, 22);
            this.mSave.Text = "toolStripButton1";
            this.mSave.ToolTipText = "Save";
            this.mSave.Click += new System.EventHandler(this.mSave_Click);
            // 
            // mClear
            // 
            this.mClear.AutoSize = false;
            this.mClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mClear.Image = ((System.Drawing.Image)(resources.GetObject("mClear.Image")));
            this.mClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mClear.Name = "mClear";
            this.mClear.Size = new System.Drawing.Size(21, 22);
            this.mClear.Text = "toolStripButton2";
            this.mClear.ToolTipText = "Clear";
            this.mClear.Click += new System.EventHandler(this.mClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // mPenWhite
            // 
            this.mPenWhite.AutoSize = false;
            this.mPenWhite.Checked = true;
            this.mPenWhite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mPenWhite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mPenWhite.Image = ((System.Drawing.Image)(resources.GetObject("mPenWhite.Image")));
            this.mPenWhite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mPenWhite.Name = "mPenWhite";
            this.mPenWhite.Size = new System.Drawing.Size(19, 22);
            this.mPenWhite.Tag = "FFFFFF";
            this.mPenWhite.Text = "toolStripButton4";
            this.mPenWhite.ToolTipText = "White pen color";
            this.mPenWhite.Click += new System.EventHandler(this.PaintingPenColor_Click);
            // 
            // mPenBlack
            // 
            this.mPenBlack.AutoSize = false;
            this.mPenBlack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mPenBlack.Image = ((System.Drawing.Image)(resources.GetObject("mPenBlack.Image")));
            this.mPenBlack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mPenBlack.Name = "mPenBlack";
            this.mPenBlack.Size = new System.Drawing.Size(19, 22);
            this.mPenBlack.Tag = "000000";
            this.mPenBlack.Text = "toolStripButton5";
            this.mPenBlack.ToolTipText = "Black pen color";
            this.mPenBlack.Click += new System.EventHandler(this.PaintingPenColor_Click);
            // 
            // mPenRed
            // 
            this.mPenRed.AutoSize = false;
            this.mPenRed.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mPenRed.Image = ((System.Drawing.Image)(resources.GetObject("mPenRed.Image")));
            this.mPenRed.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mPenRed.Name = "mPenRed";
            this.mPenRed.Size = new System.Drawing.Size(19, 22);
            this.mPenRed.Tag = "FF0000";
            this.mPenRed.Text = "toolStripButton6";
            this.mPenRed.ToolTipText = "Red pen color";
            this.mPenRed.Click += new System.EventHandler(this.PaintingPenColor_Click);
            // 
            // mPenOrange
            // 
            this.mPenOrange.AutoSize = false;
            this.mPenOrange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mPenOrange.Image = ((System.Drawing.Image)(resources.GetObject("mPenOrange.Image")));
            this.mPenOrange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mPenOrange.Name = "mPenOrange";
            this.mPenOrange.Size = new System.Drawing.Size(19, 22);
            this.mPenOrange.Tag = "FF6A00";
            this.mPenOrange.Text = "toolStripButton7";
            this.mPenOrange.ToolTipText = "Orange  pen color";
            this.mPenOrange.Click += new System.EventHandler(this.PaintingPenColor_Click);
            // 
            // mPenYellow
            // 
            this.mPenYellow.AutoSize = false;
            this.mPenYellow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mPenYellow.Image = ((System.Drawing.Image)(resources.GetObject("mPenYellow.Image")));
            this.mPenYellow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mPenYellow.Name = "mPenYellow";
            this.mPenYellow.Size = new System.Drawing.Size(19, 22);
            this.mPenYellow.Tag = "FFD800";
            this.mPenYellow.Text = "toolStripButton8";
            this.mPenYellow.ToolTipText = "Yellow pen color";
            this.mPenYellow.Click += new System.EventHandler(this.PaintingPenColor_Click);
            // 
            // mPenGreen
            // 
            this.mPenGreen.AutoSize = false;
            this.mPenGreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mPenGreen.Image = ((System.Drawing.Image)(resources.GetObject("mPenGreen.Image")));
            this.mPenGreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mPenGreen.Name = "mPenGreen";
            this.mPenGreen.Size = new System.Drawing.Size(19, 22);
            this.mPenGreen.Tag = "4CFF00";
            this.mPenGreen.Text = "toolStripButton9";
            this.mPenGreen.ToolTipText = "Green pen color";
            this.mPenGreen.Click += new System.EventHandler(this.PaintingPenColor_Click);
            // 
            // mPenBlue
            // 
            this.mPenBlue.AutoSize = false;
            this.mPenBlue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mPenBlue.Image = ((System.Drawing.Image)(resources.GetObject("mPenBlue.Image")));
            this.mPenBlue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mPenBlue.Name = "mPenBlue";
            this.mPenBlue.Size = new System.Drawing.Size(19, 22);
            this.mPenBlue.Tag = "0026FF";
            this.mPenBlue.Text = "toolStripButton10";
            this.mPenBlue.ToolTipText = "Blue pen color";
            this.mPenBlue.Click += new System.EventHandler(this.PaintingPenColor_Click);
            // 
            // mPenPink
            // 
            this.mPenPink.AutoSize = false;
            this.mPenPink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mPenPink.Image = ((System.Drawing.Image)(resources.GetObject("mPenPink.Image")));
            this.mPenPink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mPenPink.Name = "mPenPink";
            this.mPenPink.Size = new System.Drawing.Size(19, 22);
            this.mPenPink.Tag = "FF00DC";
            this.mPenPink.Text = "toolStripButton11";
            this.mPenPink.ToolTipText = "Pink pen color";
            this.mPenPink.Click += new System.EventHandler(this.PaintingPenColor_Click);
            // 
            // mEraser
            // 
            this.mEraser.AutoSize = false;
            this.mEraser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mEraser.Image = ((System.Drawing.Image)(resources.GetObject("mEraser.Image")));
            this.mEraser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mEraser.Name = "mEraser";
            this.mEraser.Size = new System.Drawing.Size(21, 22);
            this.mEraser.Tag = "FFFFFF";
            this.mEraser.Text = "toolStripButton3";
            this.mEraser.ToolTipText = "Eraser";
            this.mEraser.Click += new System.EventHandler(this.PaintingPenColor_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // mSizeSmall
            // 
            this.mSizeSmall.AutoSize = false;
            this.mSizeSmall.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mSizeSmall.Image = ((System.Drawing.Image)(resources.GetObject("mSizeSmall.Image")));
            this.mSizeSmall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mSizeSmall.Name = "mSizeSmall";
            this.mSizeSmall.Size = new System.Drawing.Size(19, 22);
            this.mSizeSmall.Tag = "1";
            this.mSizeSmall.Text = "toolStripButton12";
            this.mSizeSmall.ToolTipText = "Small pen size";
            this.mSizeSmall.Click += new System.EventHandler(this.PaintingPenSize_Click);
            // 
            // mSizeMedium
            // 
            this.mSizeMedium.AutoSize = false;
            this.mSizeMedium.Checked = true;
            this.mSizeMedium.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mSizeMedium.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mSizeMedium.Image = ((System.Drawing.Image)(resources.GetObject("mSizeMedium.Image")));
            this.mSizeMedium.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mSizeMedium.Name = "mSizeMedium";
            this.mSizeMedium.Size = new System.Drawing.Size(19, 22);
            this.mSizeMedium.Tag = "2";
            this.mSizeMedium.Text = "toolStripButton13";
            this.mSizeMedium.ToolTipText = "Medium pen size";
            this.mSizeMedium.Click += new System.EventHandler(this.PaintingPenSize_Click);
            // 
            // mSizeLarge
            // 
            this.mSizeLarge.AutoSize = false;
            this.mSizeLarge.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mSizeLarge.Image = ((System.Drawing.Image)(resources.GetObject("mSizeLarge.Image")));
            this.mSizeLarge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mSizeLarge.Name = "mSizeLarge";
            this.mSizeLarge.Size = new System.Drawing.Size(19, 20);
            this.mSizeLarge.Tag = "4";
            this.mSizeLarge.Text = "toolStripButton14";
            this.mSizeLarge.ToolTipText = "Large pen size";
            this.mSizeLarge.Click += new System.EventHandler(this.PaintingPenSize_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // mShowInSeparateWindow
            // 
            this.mShowInSeparateWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mShowInSeparateWindow.Image = ((System.Drawing.Image)(resources.GetObject("mShowInSeparateWindow.Image")));
            this.mShowInSeparateWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mShowInSeparateWindow.Name = "mShowInSeparateWindow";
            this.mShowInSeparateWindow.Size = new System.Drawing.Size(23, 22);
            this.mShowInSeparateWindow.Text = "toolStripButton1";
            this.mShowInSeparateWindow.ToolTipText = "Show bigger map in separate window...";
            this.mShowInSeparateWindow.Click += new System.EventHandler(this.mShowInSeparateWindow_Click);
            // 
            // imageListCursors
            // 
            this.imageListCursors.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCursors.ImageStream")));
            this.imageListCursors.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListCursors.Images.SetKeyName(0, "painting_5.png");
            this.imageListCursors.Images.SetKeyName(1, "painting_7.png");
            this.imageListCursors.Images.SetKeyName(2, "painting_9.png");
            this.imageListCursors.Images.SetKeyName(3, "erasor_10.png");
            this.imageListCursors.Images.SetKeyName(4, "erasor_20.png");
            this.imageListCursors.Images.SetKeyName(5, "erasor_40.png");
            // 
            // scrollComment
            // 
            this.scrollComment.Image = null;
            this.scrollComment.Location = new System.Drawing.Point(209, 0);
            this.scrollComment.Name = "scrollComment";
            this.scrollComment.ScrollElementsTotals = 100;
            this.scrollComment.ScrollElementsVisible = 20;
            this.scrollComment.ScrollHide = true;
            this.scrollComment.ScrollNecessary = true;
            this.scrollComment.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
            this.scrollComment.ScrollPosition = 0;
            this.scrollComment.Size = new System.Drawing.Size(17, 65);
            this.scrollComment.TabIndex = 6;
            this.scrollComment.Text = "badScrollBar1";
            // 
            // txtComment
            // 
            this.txtComment.HasFocus = false;
            this.txtComment.HideBorder = false;
            this.txtComment.Image = null;
            this.txtComment.Location = new System.Drawing.Point(0, 0);
            this.txtComment.Margin = new System.Windows.Forms.Padding(0);
            this.txtComment.MultilineAllow = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.PasswordChar = '\0';
            this.txtComment.ReadOnly = false;
            this.txtComment.Size = new System.Drawing.Size(210, 65);
            this.txtComment.TabIndex = 5;
            this.txtComment.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtComment.ToolTipText = "";
            // 
            // btnCommentClear
            // 
            this.btnCommentClear.BlackButton = false;
            this.btnCommentClear.Checked = false;
            this.btnCommentClear.Image = null;
            this.btnCommentClear.Location = new System.Drawing.Point(230, 0);
            this.btnCommentClear.Name = "btnCommentClear";
            this.btnCommentClear.Size = new System.Drawing.Size(49, 19);
            this.btnCommentClear.TabIndex = 10;
            this.btnCommentClear.Text = "Clear";
            this.btnCommentClear.ToolTipText = "";
            this.btnCommentClear.Click += new System.EventHandler(this.btnCommentClear_Click);
            // 
            // btnCommentCancel
            // 
            this.btnCommentCancel.BlackButton = false;
            this.btnCommentCancel.Checked = false;
            this.btnCommentCancel.Image = null;
            this.btnCommentCancel.Location = new System.Drawing.Point(230, 23);
            this.btnCommentCancel.Name = "btnCommentCancel";
            this.btnCommentCancel.Size = new System.Drawing.Size(49, 19);
            this.btnCommentCancel.TabIndex = 9;
            this.btnCommentCancel.Text = "Cancel";
            this.btnCommentCancel.ToolTipText = "";
            this.btnCommentCancel.Click += new System.EventHandler(this.btnCommentCancel_Click);
            // 
            // btnCommentSave
            // 
            this.btnCommentSave.BlackButton = false;
            this.btnCommentSave.Checked = false;
            this.btnCommentSave.Image = null;
            this.btnCommentSave.Location = new System.Drawing.Point(230, 46);
            this.btnCommentSave.Name = "btnCommentSave";
            this.btnCommentSave.Size = new System.Drawing.Size(49, 19);
            this.btnCommentSave.TabIndex = 8;
            this.btnCommentSave.Text = "Save";
            this.btnCommentSave.ToolTipText = "";
            this.btnCommentSave.Click += new System.EventHandler(this.btnCommentSave_Click);
            // 
            // scrollDgv
            // 
            this.scrollDgv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollDgv.Image = null;
            this.scrollDgv.Location = new System.Drawing.Point(407, 58);
            this.scrollDgv.Name = "scrollDgv";
            this.scrollDgv.ScrollElementsTotals = 100;
            this.scrollDgv.ScrollElementsVisible = 20;
            this.scrollDgv.ScrollHide = true;
            this.scrollDgv.ScrollNecessary = true;
            this.scrollDgv.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
            this.scrollDgv.ScrollPosition = 0;
            this.scrollDgv.Size = new System.Drawing.Size(17, 289);
            this.scrollDgv.TabIndex = 17;
            this.scrollDgv.Text = "badScrollBar1";
            // 
            // chkTank
            // 
            this.chkTank.BackColor = System.Drawing.Color.Transparent;
            this.chkTank.Checked = true;
            this.chkTank.Image = ((System.Drawing.Image)(resources.GetObject("chkTank.Image")));
            this.chkTank.Location = new System.Drawing.Point(16, 28);
            this.chkTank.Name = "chkTank";
            this.chkTank.Size = new System.Drawing.Size(82, 23);
            this.chkTank.TabIndex = 12;
            this.chkTank.Text = "This Tank";
            this.chkTank.Click += new System.EventHandler(this.dataGridViewFilterChanged);
            // 
            // chkBattleMode
            // 
            this.chkBattleMode.BackColor = System.Drawing.Color.Transparent;
            this.chkBattleMode.Checked = true;
            this.chkBattleMode.Image = ((System.Drawing.Image)(resources.GetObject("chkBattleMode.Image")));
            this.chkBattleMode.Location = new System.Drawing.Point(111, 29);
            this.chkBattleMode.Name = "chkBattleMode";
            this.chkBattleMode.Size = new System.Drawing.Size(115, 23);
            this.chkBattleMode.TabIndex = 15;
            this.chkBattleMode.Text = "This Battle Mode";
            this.chkBattleMode.Click += new System.EventHandler(this.dataGridViewFilterChanged);
            // 
            // chkMap
            // 
            this.chkMap.BackColor = System.Drawing.Color.Transparent;
            this.chkMap.Checked = true;
            this.chkMap.Image = ((System.Drawing.Image)(resources.GetObject("chkMap.Image")));
            this.chkMap.Location = new System.Drawing.Point(242, 28);
            this.chkMap.Name = "chkMap";
            this.chkMap.Size = new System.Drawing.Size(80, 23);
            this.chkMap.TabIndex = 13;
            this.chkMap.Text = "This Map";
            this.chkMap.Click += new System.EventHandler(this.dataGridViewFilterChanged);
            // 
            // chkClan
            // 
            this.chkClan.BackColor = System.Drawing.Color.Transparent;
            this.chkClan.Checked = true;
            this.chkClan.Image = ((System.Drawing.Image)(resources.GetObject("chkClan.Image")));
            this.chkClan.Location = new System.Drawing.Point(335, 28);
            this.chkClan.Name = "chkClan";
            this.chkClan.Size = new System.Drawing.Size(79, 23);
            this.chkClan.TabIndex = 14;
            this.chkClan.Text = "This Clan";
            this.chkClan.Click += new System.EventHandler(this.dataGridViewFilterChanged);
            // 
            // grpOtherBtlReviews
            // 
            this.grpOtherBtlReviews.BackColor = System.Drawing.Color.Transparent;
            this.grpOtherBtlReviews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOtherBtlReviews.Image = null;
            this.grpOtherBtlReviews.Location = new System.Drawing.Point(0, 0);
            this.grpOtherBtlReviews.Name = "grpOtherBtlReviews";
            this.grpOtherBtlReviews.Size = new System.Drawing.Size(438, 364);
            this.grpOtherBtlReviews.TabIndex = 11;
            this.grpOtherBtlReviews.Text = "Other Battle Reviews";
            // 
            // lblMapDescription
            // 
            this.lblMapDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMapDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblMapDescription.Dimmed = false;
            this.lblMapDescription.Image = null;
            this.lblMapDescription.Location = new System.Drawing.Point(30, 372);
            this.lblMapDescription.Name = "lblMapDescription";
            this.lblMapDescription.Size = new System.Drawing.Size(300, 73);
            this.lblMapDescription.TabIndex = 2;
            this.lblMapDescription.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(15, 352);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(328, 103);
            this.badGroupBox1.TabIndex = 3;
            this.badGroupBox1.Text = "Map Description";
            // 
            // BattleReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.toolStripPaint);
            this.Controls.Add(this.panelMap);
            this.Controls.Add(this.panelComment);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.lblMapDescription);
            this.Controls.Add(this.badGroupBox1);
            this.Name = "BattleReview";
            this.Size = new System.Drawing.Size(819, 469);
            this.Load += new System.EventHandler(this.BattleMapAndComment_Load);
            this.Resize += new System.EventHandler(this.BattleReview_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picIllustration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).EndInit();
            this.panelGrid.ResumeLayout(false);
            this.panelComment.ResumeLayout(false);
            this.panelMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPaint)).EndInit();
            this.toolStripPaint.ResumeLayout(false);
            this.toolStripPaint.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private BadLabel lblMapDescription;
		private BadGroupBox badGroupBox1;
		private BadTextBox txtComment;
		private BadScrollBar scrollComment;
		private System.Windows.Forms.PictureBox picIllustration;
		private BadButton btnCommentSave;
		private BadButton btnCommentCancel;
		private BadButton btnCommentClear;
		private BadGroupBox grpOtherBtlReviews;
		private BadCheckBox chkTank;
		private BadCheckBox chkMap;
		private BadCheckBox chkClan;
		private BadCheckBox chkBattleMode;
		private System.Windows.Forms.DataGridView dgvReviews;
		private BadScrollBar scrollDgv;
		private System.Windows.Forms.Panel panelGrid;
		private System.Windows.Forms.Panel panelComment;
		private System.Windows.Forms.Panel panelMap;
		private System.Windows.Forms.ToolStrip toolStripPaint;
		private System.Windows.Forms.ToolStripButton mSave;
		private System.Windows.Forms.ToolStripButton mClear;
		private System.Windows.Forms.ToolStripButton mEraser;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton mPenWhite;
		private System.Windows.Forms.ToolStripButton mPenBlack;
		private System.Windows.Forms.ToolStripButton mPenRed;
		private System.Windows.Forms.ToolStripButton mPenOrange;
		private System.Windows.Forms.ToolStripButton mPenYellow;
		private System.Windows.Forms.ToolStripButton mPenGreen;
		private System.Windows.Forms.ToolStripButton mPenBlue;
		private System.Windows.Forms.ToolStripButton mPenPink;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton mSizeSmall;
		private System.Windows.Forms.ToolStripButton mSizeMedium;
		private System.Windows.Forms.ToolStripButton mSizeLarge;
		private System.Windows.Forms.PictureBox picPaint;
		private System.Windows.Forms.ImageList imageListCursors;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton mShowInSeparateWindow;
    }
}
