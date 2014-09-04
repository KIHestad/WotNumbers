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
			this.DownloadTheme = new BadForm();
			this.btnCancel = new BadButton();
			this.progressDownload = new BadProgressBar();
			this.DownloadTheme.SuspendLayout();
			this.SuspendLayout();
			// 
			// DownloadTheme
			// 
			this.DownloadTheme.Controls.Add(this.btnCancel);
			this.DownloadTheme.Controls.Add(this.progressDownload);
			this.DownloadTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DownloadTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.DownloadTheme.FormFooter = false;
			this.DownloadTheme.FormFooterHeight = 26;
			this.DownloadTheme.FormInnerBorder = 3;
			this.DownloadTheme.FormMargin = 0;
			this.DownloadTheme.Image = null;
			this.DownloadTheme.Location = new System.Drawing.Point(0, 0);
			this.DownloadTheme.MainArea = mainAreaClass1;
			this.DownloadTheme.Name = "DownloadTheme";
			this.DownloadTheme.Resizable = false;
			this.DownloadTheme.Size = new System.Drawing.Size(381, 127);
			this.DownloadTheme.SystemExitImage = null;
			this.DownloadTheme.SystemMaximizeImage = null;
			this.DownloadTheme.SystemMinimizeImage = null;
			this.DownloadTheme.TabIndex = 0;
			this.DownloadTheme.Text = "Download Progress";
			this.DownloadTheme.TitleHeight = 26;
			// 
			// btnCancel
			// 
			this.btnCancel.BlackButton = false;
			this.btnCancel.Checked = false;
			this.btnCancel.Image = null;
			this.btnCancel.Location = new System.Drawing.Point(279, 87);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.ToolTipText = "";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// progressDownload
			// 
			this.progressDownload.BackColor = System.Drawing.Color.Transparent;
			this.progressDownload.Image = null;
			this.progressDownload.Location = new System.Drawing.Point(24, 49);
			this.progressDownload.Name = "progressDownload";
			this.progressDownload.ProgressBarColorMode = false;
			this.progressDownload.ProgressBarMargins = 2;
			this.progressDownload.ProgressBarShowPercentage = false;
			this.progressDownload.Size = new System.Drawing.Size(330, 23);
			this.progressDownload.TabIndex = 0;
			this.progressDownload.Text = "badProgressBar1";
			this.progressDownload.Value = 0D;
			this.progressDownload.ValueMax = 100D;
			this.progressDownload.ValueMin = 0D;
			// 
			// Download
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(381, 127);
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
		private BadProgressBar progressDownload;
		private BadButton btnCancel;
	}
}