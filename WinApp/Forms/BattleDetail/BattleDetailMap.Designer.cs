namespace WinApp.Forms
{
    partial class BattleDetailMap
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
            BadThemeContainerControl.MainAreaClass mainAreaClass3 = new BadThemeContainerControl.MainAreaClass();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleDetailMap));
            this.BattleDetailMapTheme = new BadForm();
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
            this.imageListCursors = new System.Windows.Forms.ImageList(this.components);
            this.BattleDetailMapTheme.SuspendLayout();
            this.panelMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPaint)).BeginInit();
            this.toolStripPaint.SuspendLayout();
            this.SuspendLayout();
            // 
            // BattleDetailMapTheme
            // 
            this.BattleDetailMapTheme.Controls.Add(this.panelMap);
            this.BattleDetailMapTheme.Controls.Add(this.toolStripPaint);
            this.BattleDetailMapTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BattleDetailMapTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BattleDetailMapTheme.FormExitAsMinimize = false;
            this.BattleDetailMapTheme.FormFooter = false;
            this.BattleDetailMapTheme.FormFooterHeight = 26;
            this.BattleDetailMapTheme.FormInnerBorder = 3;
            this.BattleDetailMapTheme.FormMargin = 0;
            this.BattleDetailMapTheme.Image = null;
            this.BattleDetailMapTheme.Location = new System.Drawing.Point(0, 0);
            this.BattleDetailMapTheme.MainArea = mainAreaClass3;
            this.BattleDetailMapTheme.Name = "BattleDetailMapTheme";
            this.BattleDetailMapTheme.Resizable = true;
            this.BattleDetailMapTheme.Size = new System.Drawing.Size(624, 681);
            this.BattleDetailMapTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("BattleDetailMapTheme.SystemExitImage")));
            this.BattleDetailMapTheme.SystemMaximizeImage = ((System.Drawing.Image)(resources.GetObject("BattleDetailMapTheme.SystemMaximizeImage")));
            this.BattleDetailMapTheme.SystemMinimizeImage = ((System.Drawing.Image)(resources.GetObject("BattleDetailMapTheme.SystemMinimizeImage")));
            this.BattleDetailMapTheme.TabIndex = 0;
            this.BattleDetailMapTheme.Text = "Battle Details Map";
            this.BattleDetailMapTheme.TitleHeight = 26;
            // 
            // panelMap
            // 
            this.panelMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMap.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelMap.Controls.Add(this.picPaint);
            this.panelMap.Location = new System.Drawing.Point(12, 40);
            this.panelMap.Name = "panelMap";
            this.panelMap.Size = new System.Drawing.Size(600, 600);
            this.panelMap.TabIndex = 24;
            // 
            // picPaint
            // 
            this.picPaint.BackColor = System.Drawing.Color.Transparent;
            this.picPaint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picPaint.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picPaint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPaint.Location = new System.Drawing.Point(0, 0);
            this.picPaint.Margin = new System.Windows.Forms.Padding(0);
            this.picPaint.Name = "picPaint";
            this.picPaint.Size = new System.Drawing.Size(600, 600);
            this.picPaint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPaint.TabIndex = 0;
            this.picPaint.TabStop = false;
            this.picPaint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picPaint_MouseDown);
            this.picPaint.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picPaint_MouseMove);
            this.picPaint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picPaint_MouseUp);
            // 
            // toolStripPaint
            // 
            this.toolStripPaint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.toolStripSeparator3});
            this.toolStripPaint.Location = new System.Drawing.Point(3, 653);
            this.toolStripPaint.Name = "toolStripPaint";
            this.toolStripPaint.Size = new System.Drawing.Size(407, 25);
            this.toolStripPaint.TabIndex = 23;
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
            // BattleDetailMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Fuchsia;
            this.ClientSize = new System.Drawing.Size(624, 681);
            this.Controls.Add(this.BattleDetailMapTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BattleDetailMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BattleDetailMap_FormClosed);
            this.Load += new System.EventHandler(this.BattleDetailMap_Load);
            this.ResizeEnd += new System.EventHandler(this.BattleDetailMap_Resize);
            this.Resize += new System.EventHandler(this.BattleDetailMap_Resize);
            this.BattleDetailMapTheme.ResumeLayout(false);
            this.panelMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPaint)).EndInit();
            this.toolStripPaint.ResumeLayout(false);
            this.toolStripPaint.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BadForm BattleDetailMapTheme;
        private System.Windows.Forms.ToolStrip toolStripPaint;
        private System.Windows.Forms.ToolStripButton mSave;
        private System.Windows.Forms.ToolStripButton mClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton mPenWhite;
        private System.Windows.Forms.ToolStripButton mPenBlack;
        private System.Windows.Forms.ToolStripButton mPenRed;
        private System.Windows.Forms.ToolStripButton mPenOrange;
        private System.Windows.Forms.ToolStripButton mPenYellow;
        private System.Windows.Forms.ToolStripButton mPenGreen;
        private System.Windows.Forms.ToolStripButton mPenBlue;
        private System.Windows.Forms.ToolStripButton mPenPink;
        private System.Windows.Forms.ToolStripButton mEraser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton mSizeSmall;
        private System.Windows.Forms.ToolStripButton mSizeMedium;
        private System.Windows.Forms.ToolStripButton mSizeLarge;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Panel panelMap;
        private System.Windows.Forms.PictureBox picPaint;
        private System.Windows.Forms.ImageList imageListCursors;
    }
}