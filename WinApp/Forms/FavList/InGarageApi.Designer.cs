namespace WinApp.Forms
{
	partial class InGarageApi
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InGarageApi));
			this.badForm1 = new BadForm();
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.badForm1.SuspendLayout();
			this.SuspendLayout();
			// 
			// badForm1
			// 
			this.badForm1.Controls.Add(this.webBrowser1);
			this.badForm1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.badForm1.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.badForm1.FormFooter = false;
			this.badForm1.FormFooterHeight = 26;
			this.badForm1.FormInnerBorder = 3;
			this.badForm1.FormMargin = 0;
			this.badForm1.Image = null;
			this.badForm1.Location = new System.Drawing.Point(0, 0);
			this.badForm1.MainArea = mainAreaClass1;
			this.badForm1.Name = "badForm1";
			this.badForm1.Resizable = true;
			this.badForm1.Size = new System.Drawing.Size(537, 862);
			this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
			this.badForm1.SystemMaximizeImage = null;
			this.badForm1.SystemMinimizeImage = null;
			this.badForm1.TabIndex = 0;
			this.badForm1.Text = "Login to Wargaming account to get \"In Garage\" Tanks";
			this.badForm1.TitleHeight = 26;
			// 
			// webBrowser1
			// 
			this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowser1.Location = new System.Drawing.Point(12, 38);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new System.Drawing.Size(512, 812);
			this.webBrowser1.TabIndex = 0;
			this.webBrowser1.Url = new System.Uri("http://wotnum.hestad.no/Api/WotAuthenticate.aspx?uid=1", System.UriKind.Absolute);
			this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
			// 
			// InGarageApi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Fuchsia;
			this.ClientSize = new System.Drawing.Size(537, 862);
			this.Controls.Add(this.badForm1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "InGarageApi";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "GetInGarage";
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.badForm1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm badForm1;
		private System.Windows.Forms.WebBrowser webBrowser1;
	}
}