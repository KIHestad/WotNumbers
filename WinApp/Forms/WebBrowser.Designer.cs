namespace WinApp.Forms
{
    partial class WebBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebBrowser));
            this.WebBrowserTheme = new BadForm();
            this.pnlBrowser = new System.Windows.Forms.Panel();
            this.WebBrowserTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // WebBrowserTheme
            // 
            this.WebBrowserTheme.BackColor = System.Drawing.Color.Magenta;
            this.WebBrowserTheme.Controls.Add(this.pnlBrowser);
            this.WebBrowserTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowserTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.WebBrowserTheme.FormExitAsMinimize = false;
            this.WebBrowserTheme.FormFooter = true;
            this.WebBrowserTheme.FormFooterHeight = 26;
            this.WebBrowserTheme.FormInnerBorder = 0;
            this.WebBrowserTheme.FormMargin = 0;
            this.WebBrowserTheme.Image = null;
            this.WebBrowserTheme.Location = new System.Drawing.Point(0, 0);
            this.WebBrowserTheme.MainArea = mainAreaClass1;
            this.WebBrowserTheme.Name = "WebBrowserTheme";
            this.WebBrowserTheme.Resizable = true;
            this.WebBrowserTheme.Size = new System.Drawing.Size(612, 572);
            this.WebBrowserTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("WebBrowserTheme.SystemExitImage")));
            this.WebBrowserTheme.SystemMaximizeImage = null;
            this.WebBrowserTheme.SystemMinimizeImage = null;
            this.WebBrowserTheme.TabIndex = 0;
            this.WebBrowserTheme.Text = "Wot Numbers Web Browser";
            this.WebBrowserTheme.TitleHeight = 26;
            // 
            // pnlBrowser
            // 
            this.pnlBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBrowser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pnlBrowser.Location = new System.Drawing.Point(12, 44);
            this.pnlBrowser.Name = "pnlBrowser";
            this.pnlBrowser.Size = new System.Drawing.Size(588, 485);
            this.pnlBrowser.TabIndex = 0;
            this.pnlBrowser.Visible = false;
            // 
            // WebBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(612, 572);
            this.Controls.Add(this.WebBrowserTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WebBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ad";
            this.TransparencyKey = System.Drawing.Color.Magenta;
            this.Load += new System.EventHandler(this.WebBrowser_Load);
            this.WebBrowserTheme.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BadForm WebBrowserTheme;
        private System.Windows.Forms.Panel pnlBrowser;
    }
}