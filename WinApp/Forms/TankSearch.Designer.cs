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
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtSearchText = new System.Windows.Forms.ToolStripTextBox();
            this.toolAllTanks_Nation0 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAllTanks_Nation1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAllTanks_Nation2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAllTanks_Nation3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAllTanks_Nation4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAllTanks_Nation5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAllTanks_Nation6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAllTanks_Nation7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAllTanks_Toggle = new System.Windows.Forms.ToolStripButton();
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
            this.TankSearchTheme.Image = null;
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
            this.toolStripLabel1,
            this.txtSearchText,
            this.toolAllTanks_Nation0,
            this.toolAllTanks_Nation1,
            this.toolAllTanks_Nation2,
            this.toolAllTanks_Nation3,
            this.toolAllTanks_Nation4,
            this.toolAllTanks_Nation5,
            this.toolAllTanks_Nation6,
            this.toolAllTanks_Nation7,
            this.toolAllTanks_Toggle});
            this.toolStripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripMain.Location = new System.Drawing.Point(2, 29);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(808, 25);
            this.toolStripMain.Stretch = true;
            this.toolStripMain.TabIndex = 7;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(73, 22);
            this.toolStripLabel1.Text = " Tank Name:";
            // 
            // txtSearchText
            // 
            this.txtSearchText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.txtSearchText.ForeColor = System.Drawing.SystemColors.Window;
            this.txtSearchText.Name = "txtSearchText";
            this.txtSearchText.Size = new System.Drawing.Size(100, 25);
            // 
            // toolAllTanks_Nation0
            // 
            this.toolAllTanks_Nation0.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Nation0.Image")));
            this.toolAllTanks_Nation0.Name = "toolAllTanks_Nation0";
            this.toolAllTanks_Nation0.Size = new System.Drawing.Size(74, 25);
            this.toolAllTanks_Nation0.Text = "U.S.S.R.";
            // 
            // toolAllTanks_Nation1
            // 
            this.toolAllTanks_Nation1.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Nation1.Image")));
            this.toolAllTanks_Nation1.Name = "toolAllTanks_Nation1";
            this.toolAllTanks_Nation1.Size = new System.Drawing.Size(83, 25);
            this.toolAllTanks_Nation1.Text = "Germany";
            // 
            // toolAllTanks_Nation2
            // 
            this.toolAllTanks_Nation2.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Nation2.Image")));
            this.toolAllTanks_Nation2.Name = "toolAllTanks_Nation2";
            this.toolAllTanks_Nation2.Size = new System.Drawing.Size(66, 25);
            this.toolAllTanks_Nation2.Text = "U.S.A.";
            // 
            // toolAllTanks_Nation3
            // 
            this.toolAllTanks_Nation3.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Nation3.Image")));
            this.toolAllTanks_Nation3.Name = "toolAllTanks_Nation3";
            this.toolAllTanks_Nation3.Size = new System.Drawing.Size(66, 25);
            this.toolAllTanks_Nation3.Text = "China";
            // 
            // toolAllTanks_Nation4
            // 
            this.toolAllTanks_Nation4.BackColor = System.Drawing.SystemColors.Control;
            this.toolAllTanks_Nation4.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Nation4.Image")));
            this.toolAllTanks_Nation4.Name = "toolAllTanks_Nation4";
            this.toolAllTanks_Nation4.Size = new System.Drawing.Size(70, 25);
            this.toolAllTanks_Nation4.Text = "France";
            // 
            // toolAllTanks_Nation5
            // 
            this.toolAllTanks_Nation5.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Nation5.Image")));
            this.toolAllTanks_Nation5.Name = "toolAllTanks_Nation5";
            this.toolAllTanks_Nation5.Size = new System.Drawing.Size(56, 25);
            this.toolAllTanks_Nation5.Text = "U.K.";
            // 
            // toolAllTanks_Nation6
            // 
            this.toolAllTanks_Nation6.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Nation6.Image")));
            this.toolAllTanks_Nation6.Name = "toolAllTanks_Nation6";
            this.toolAllTanks_Nation6.Size = new System.Drawing.Size(65, 25);
            this.toolAllTanks_Nation6.Text = "Japan";
            // 
            // toolAllTanks_Nation7
            // 
            this.toolAllTanks_Nation7.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Nation7.Image")));
            this.toolAllTanks_Nation7.Name = "toolAllTanks_Nation7";
            this.toolAllTanks_Nation7.Size = new System.Drawing.Size(67, 25);
            this.toolAllTanks_Nation7.Text = "Czech";
            // 
            // toolAllTanks_Toggle
            // 
            this.toolAllTanks_Toggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolAllTanks_Toggle.Image = ((System.Drawing.Image)(resources.GetObject("toolAllTanks_Toggle.Image")));
            this.toolAllTanks_Toggle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAllTanks_Toggle.Name = "toolAllTanks_Toggle";
            this.toolAllTanks_Toggle.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.toolAllTanks_Toggle.Size = new System.Drawing.Size(33, 22);
            this.toolAllTanks_Toggle.Text = "All";
            this.toolAllTanks_Toggle.ToolTipText = "Select All Nations";
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
            this.TankSearchTheme.ResumeLayout(false);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BadForm TankSearchTheme;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Nation0;
        private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Nation1;
        private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Nation2;
        private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Nation3;
        private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Nation4;
        private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Nation5;
        private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Nation6;
        private System.Windows.Forms.ToolStripMenuItem toolAllTanks_Nation7;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtSearchText;
        private System.Windows.Forms.ToolStripButton toolAllTanks_Toggle;
    }
}