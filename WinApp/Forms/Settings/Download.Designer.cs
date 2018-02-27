namespace WinApp.Forms
{
	partial class Download
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Download));
            this.DownloadTheme = new BadForm();
            this.badLabel4 = new BadLabel();
            this.lblNewVer = new BadLabel();
            this.badLabel2 = new BadLabel();
            this.badLabel1 = new BadLabel();
            this.lblCurrVer = new BadLabel();
            this.btnGoToDownloadPage = new BadButton();
            this.DownloadTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // DownloadTheme
            // 
            this.DownloadTheme.Controls.Add(this.btnGoToDownloadPage);
            this.DownloadTheme.Controls.Add(this.badLabel4);
            this.DownloadTheme.Controls.Add(this.lblNewVer);
            this.DownloadTheme.Controls.Add(this.badLabel2);
            this.DownloadTheme.Controls.Add(this.badLabel1);
            this.DownloadTheme.Controls.Add(this.lblCurrVer);
            this.DownloadTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DownloadTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DownloadTheme.FormExitAsMinimize = false;
            this.DownloadTheme.FormFooter = false;
            this.DownloadTheme.FormFooterHeight = 26;
            this.DownloadTheme.FormInnerBorder = 3;
            this.DownloadTheme.FormMargin = 0;
            this.DownloadTheme.Image = null;
            this.DownloadTheme.Location = new System.Drawing.Point(0, 0);
            this.DownloadTheme.MainArea = mainAreaClass1;
            this.DownloadTheme.Name = "DownloadTheme";
            this.DownloadTheme.Resizable = false;
            this.DownloadTheme.Size = new System.Drawing.Size(403, 210);
            this.DownloadTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("DownloadTheme.SystemExitImage")));
            this.DownloadTheme.SystemMaximizeImage = null;
            this.DownloadTheme.SystemMinimizeImage = null;
            this.DownloadTheme.TabIndex = 0;
            this.DownloadTheme.Text = "New version available";
            this.DownloadTheme.TitleHeight = 26;
            // 
            // badLabel4
            // 
            this.badLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel4.Dimmed = false;
            this.badLabel4.Image = null;
            this.badLabel4.Location = new System.Drawing.Point(20, 45);
            this.badLabel4.Name = "badLabel4";
            this.badLabel4.Size = new System.Drawing.Size(366, 23);
            this.badLabel4.TabIndex = 18;
            this.badLabel4.Text = "A new Wot Numbers version is available. Please go to the download page.";
            this.badLabel4.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // lblNewVer
            // 
            this.lblNewVer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblNewVer.Dimmed = false;
            this.lblNewVer.Image = null;
            this.lblNewVer.Location = new System.Drawing.Point(210, 107);
            this.lblNewVer.Name = "lblNewVer";
            this.lblNewVer.Size = new System.Drawing.Size(66, 23);
            this.lblNewVer.TabIndex = 17;
            this.lblNewVer.Text = "Loading...";
            this.lblNewVer.TxtAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(125, 107);
            this.badLabel2.Name = "badLabel2";
            this.badLabel2.Size = new System.Drawing.Size(90, 26);
            this.badLabel2.TabIndex = 16;
            this.badLabel2.Text = "New version:";
            this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(125, 85);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(90, 23);
            this.badLabel1.TabIndex = 15;
            this.badLabel1.Text = "Current version:";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // lblCurrVer
            // 
            this.lblCurrVer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblCurrVer.Dimmed = false;
            this.lblCurrVer.Image = null;
            this.lblCurrVer.Location = new System.Drawing.Point(210, 85);
            this.lblCurrVer.Name = "lblCurrVer";
            this.lblCurrVer.Size = new System.Drawing.Size(66, 23);
            this.lblCurrVer.TabIndex = 14;
            this.lblCurrVer.Text = "Loading...";
            this.lblCurrVer.TxtAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnGoToDownloadPage
            // 
            this.btnGoToDownloadPage.BlackButton = false;
            this.btnGoToDownloadPage.Checked = false;
            this.btnGoToDownloadPage.Image = null;
            this.btnGoToDownloadPage.Location = new System.Drawing.Point(68, 148);
            this.btnGoToDownloadPage.Name = "btnGoToDownloadPage";
            this.btnGoToDownloadPage.Size = new System.Drawing.Size(262, 23);
            this.btnGoToDownloadPage.TabIndex = 19;
            this.btnGoToDownloadPage.Text = "Go To Download Page";
            this.btnGoToDownloadPage.ToolTipText = "";
            this.btnGoToDownloadPage.Click += new System.EventHandler(this.btnGoToDownloadPage_Click);
            // 
            // Download
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 210);
            this.Controls.Add(this.DownloadTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Download";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Download";
            this.Load += new System.EventHandler(this.Download_Load);
            this.DownloadTheme.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private BadForm DownloadTheme;
        private BadButton btnGoToDownloadPage;
        private BadLabel badLabel4;
        private BadLabel lblNewVer;
        private BadLabel badLabel2;
        private BadLabel badLabel1;
        private BadLabel lblCurrVer;
    }
}