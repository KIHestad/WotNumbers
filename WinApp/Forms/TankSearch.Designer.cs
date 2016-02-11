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
            BadThemeContainerControl.MainAreaClass mainAreaClass1 = new BadThemeContainerControl.MainAreaClass();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TankSearch));
            this.TankSearchTheme = new BadForm();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.mLabel = new System.Windows.Forms.ToolStripLabel();
            this.mTxtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.mNation0 = new System.Windows.Forms.ToolStripButton();
            this.mNation1 = new System.Windows.Forms.ToolStripButton();
            this.mNation2 = new System.Windows.Forms.ToolStripButton();
            this.mNation3 = new System.Windows.Forms.ToolStripButton();
            this.mNation4 = new System.Windows.Forms.ToolStripButton();
            this.mNation5 = new System.Windows.Forms.ToolStripButton();
            this.mNation6 = new System.Windows.Forms.ToolStripButton();
            this.mNation7 = new System.Windows.Forms.ToolStripButton();
            this.mNationSelectMode = new System.Windows.Forms.ToolStripButton();
            this.mNationToggleAll = new System.Windows.Forms.ToolStripButton();
            this.TankSearchTheme.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // TankSearchTheme
            // 
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
            this.TankSearchTheme.Size = new System.Drawing.Size(819, 411);
            this.TankSearchTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("TankSearchTheme.SystemExitImage")));
            this.TankSearchTheme.SystemMaximizeImage = null;
            this.TankSearchTheme.SystemMinimizeImage = null;
            this.TankSearchTheme.TabIndex = 0;
            this.TankSearchTheme.Text = "Search for tank";
            this.TankSearchTheme.TitleHeight = 26;
            // 
            // toolStripMain
            // 
            this.toolStripMain.AutoSize = false;
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mLabel,
            this.mTxtSearch,
            this.mNation0,
            this.mNation1,
            this.mNation2,
            this.mNation3,
            this.mNation4,
            this.mNation5,
            this.mNation6,
            this.mNation7,
            this.mNationSelectMode,
            this.mNationToggleAll});
            this.toolStripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripMain.Location = new System.Drawing.Point(2, 27);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(808, 25);
            this.toolStripMain.Stretch = true;
            this.toolStripMain.TabIndex = 7;
            this.toolStripMain.Text = "toolStrip1";
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
            this.mTxtSearch.ForeColor = System.Drawing.SystemColors.Window;
            this.mTxtSearch.Name = "mTxtSearch";
            this.mTxtSearch.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.mTxtSearch.Size = new System.Drawing.Size(100, 25);
            this.mTxtSearch.TextChanged += new System.EventHandler(this.mTxtSearch_TextChanged);
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
            // mNationSelectMode
            // 
            this.mNationSelectMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mNationSelectMode.Image = ((System.Drawing.Image)(resources.GetObject("mNationSelectMode.Image")));
            this.mNationSelectMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mNationSelectMode.Name = "mNationSelectMode";
            this.mNationSelectMode.Size = new System.Drawing.Size(43, 22);
            this.mNationSelectMode.Text = "Single";
            this.mNationSelectMode.ToolTipText = "Toggle selection mode";
            this.mNationSelectMode.Click += new System.EventHandler(this.mNationSelectMode_Click);
            // 
            // mNationToggleAll
            // 
            this.mNationToggleAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mNationToggleAll.Image = ((System.Drawing.Image)(resources.GetObject("mNationToggleAll.Image")));
            this.mNationToggleAll.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.mNationToggleAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mNationToggleAll.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.mNationToggleAll.Name = "mNationToggleAll";
            this.mNationToggleAll.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mNationToggleAll.Size = new System.Drawing.Size(33, 21);
            this.mNationToggleAll.Text = "All";
            this.mNationToggleAll.ToolTipText = "Select or deselect all Nations";
            this.mNationToggleAll.Visible = false;
            this.mNationToggleAll.Click += new System.EventHandler(this.mNationToggleAll_Click);
            // 
            // TankSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 411);
            this.Controls.Add(this.TankSearchTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TankSearch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TankSearch";
            this.Load += new System.EventHandler(this.TankSearch_Load);
            this.Resize += new System.EventHandler(this.TankSearch_Resize);
            this.TankSearchTheme.ResumeLayout(false);
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
    }
}