namespace WinApp.Forms
{
	partial class UpdateFromApi
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateFromApi));
			this.UpdateFromApiTheme = new BadForm();
			this.btnStart = new BadButton();
			this.lblProgressStatus = new BadLabel();
			this.badProgressBar = new BadProgressBar();
			this.UpdateFromApiTheme.SuspendLayout();
			this.SuspendLayout();
			// 
			// UpdateFromApiTheme
			// 
			this.UpdateFromApiTheme.Controls.Add(this.btnStart);
			this.UpdateFromApiTheme.Controls.Add(this.lblProgressStatus);
			this.UpdateFromApiTheme.Controls.Add(this.badProgressBar);
			this.UpdateFromApiTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.UpdateFromApiTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.UpdateFromApiTheme.FormFooter = false;
			this.UpdateFromApiTheme.FormFooterHeight = 26;
			this.UpdateFromApiTheme.FormInnerBorder = 3;
			this.UpdateFromApiTheme.FormMargin = 0;
			this.UpdateFromApiTheme.Image = null;
			this.UpdateFromApiTheme.Location = new System.Drawing.Point(0, 0);
			this.UpdateFromApiTheme.MainArea = mainAreaClass1;
			this.UpdateFromApiTheme.Name = "UpdateFromApiTheme";
			this.UpdateFromApiTheme.Resizable = false;
			this.UpdateFromApiTheme.Size = new System.Drawing.Size(382, 126);
			this.UpdateFromApiTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("UpdateFromApiTheme.SystemExitImage")));
			this.UpdateFromApiTheme.SystemMaximizeImage = null;
			this.UpdateFromApiTheme.SystemMinimizeImage = null;
			this.UpdateFromApiTheme.TabIndex = 0;
			this.UpdateFromApiTheme.Text = "Update Tank Data from API";
			this.UpdateFromApiTheme.TitleHeight = 26;
			// 
			// btnStart
			// 
			this.btnStart.Image = null;
			this.btnStart.Location = new System.Drawing.Point(281, 83);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 2;
			this.btnStart.Text = "Start";
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// lblProgressStatus
			// 
			this.lblProgressStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblProgressStatus.Dimmed = false;
			this.lblProgressStatus.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.lblProgressStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.lblProgressStatus.Image = null;
			this.lblProgressStatus.Location = new System.Drawing.Point(26, 83);
			this.lblProgressStatus.Name = "lblProgressStatus";
			this.lblProgressStatus.Size = new System.Drawing.Size(249, 23);
			this.lblProgressStatus.TabIndex = 1;
			// 
			// badProgressBar
			// 
			this.badProgressBar.BackColor = System.Drawing.Color.Transparent;
			this.badProgressBar.Image = null;
			this.badProgressBar.Location = new System.Drawing.Point(25, 48);
			this.badProgressBar.Name = "badProgressBar";
			this.badProgressBar.ProgressBarColorMode = false;
			this.badProgressBar.ProgressBarMargins = 2;
			this.badProgressBar.ProgressBarShowPercentage = false;
			this.badProgressBar.Size = new System.Drawing.Size(330, 23);
			this.badProgressBar.TabIndex = 0;
			this.badProgressBar.Text = "badProgressBar1";
			this.badProgressBar.Value = 0D;
			this.badProgressBar.ValueMax = 100D;
			this.badProgressBar.ValueMin = 0D;
			// 
			// UpdateFromApi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(382, 126);
			this.Controls.Add(this.UpdateFromApiTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "UpdateFromApi";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "UpdateFromApi";
			this.Shown += new System.EventHandler(this.UpdateFromApi_Shown);
			this.UpdateFromApiTheme.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm UpdateFromApiTheme;
		private BadButton btnStart;
		private BadLabel lblProgressStatus;
		private BadProgressBar badProgressBar;
	}
}