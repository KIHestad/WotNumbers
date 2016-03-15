namespace WinApp.Forms
{
    partial class Ad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ad));
            this.badForm1 = new BadForm();
            this.btnGoTo = new BadButton();
            this.btnNextAd = new BadButton();
            this.btnClose = new BadButton();
            this.pnlBrowser = new System.Windows.Forms.Panel();
            this.badForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // badForm1
            // 
            this.badForm1.BackColor = System.Drawing.Color.Magenta;
            this.badForm1.Controls.Add(this.btnGoTo);
            this.badForm1.Controls.Add(this.btnNextAd);
            this.badForm1.Controls.Add(this.btnClose);
            this.badForm1.Controls.Add(this.pnlBrowser);
            this.badForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.badForm1.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.badForm1.FormExitAsMinimize = false;
            this.badForm1.FormFooter = false;
            this.badForm1.FormFooterHeight = 26;
            this.badForm1.FormInnerBorder = 3;
            this.badForm1.FormMargin = 0;
            this.badForm1.Image = null;
            this.badForm1.Location = new System.Drawing.Point(0, 0);
            this.badForm1.MainArea = mainAreaClass1;
            this.badForm1.Name = "badForm1";
            this.badForm1.Resizable = true;
            this.badForm1.Size = new System.Drawing.Size(627, 507);
            this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
            this.badForm1.SystemMaximizeImage = null;
            this.badForm1.SystemMinimizeImage = null;
            this.badForm1.TabIndex = 0;
            this.badForm1.Text = "Thank you for supporting Wot Numbers Team by watching this ad";
            this.badForm1.TitleHeight = 26;
            // 
            // btnGoTo
            // 
            this.btnGoTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoTo.BlackButton = false;
            this.btnGoTo.Checked = false;
            this.btnGoTo.Enabled = false;
            this.btnGoTo.Image = null;
            this.btnGoTo.Location = new System.Drawing.Point(101, 465);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(78, 23);
            this.btnGoTo.TabIndex = 3;
            this.btnGoTo.Text = "Go To";
            this.btnGoTo.ToolTipText = "";
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnNextAd
            // 
            this.btnNextAd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextAd.BlackButton = false;
            this.btnNextAd.Checked = false;
            this.btnNextAd.Image = null;
            this.btnNextAd.Location = new System.Drawing.Point(17, 465);
            this.btnNextAd.Name = "btnNextAd";
            this.btnNextAd.Size = new System.Drawing.Size(78, 23);
            this.btnNextAd.TabIndex = 2;
            this.btnNextAd.Text = "Next Ad";
            this.btnNextAd.ToolTipText = "";
            this.btnNextAd.Click += new System.EventHandler(this.btnNextAd_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BlackButton = false;
            this.btnClose.Checked = false;
            this.btnClose.Image = null;
            this.btnClose.Location = new System.Drawing.Point(531, 465);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.ToolTipText = "";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlBrowser
            // 
            this.pnlBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBrowser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pnlBrowser.Location = new System.Drawing.Point(17, 42);
            this.pnlBrowser.Name = "pnlBrowser";
            this.pnlBrowser.Size = new System.Drawing.Size(592, 406);
            this.pnlBrowser.TabIndex = 0;
            this.pnlBrowser.Visible = false;
            // 
            // Ad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(627, 507);
            this.Controls.Add(this.badForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Ad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ad";
            this.TransparencyKey = System.Drawing.Color.Magenta;
            this.Load += new System.EventHandler(this.Ad_Load);
            this.badForm1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BadForm badForm1;
        private BadButton btnClose;
        private System.Windows.Forms.Panel pnlBrowser;
        private BadButton btnNextAd;
        private BadButton btnGoTo;
    }
}