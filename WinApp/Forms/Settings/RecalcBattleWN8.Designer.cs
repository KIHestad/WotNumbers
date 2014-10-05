namespace WinApp.Forms
{
	partial class RecalcBattleWN8
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecalcBattleWN8));
			this.RecalcBattleWN8Theme = new BadForm();
			this.btnStart = new BadButton();
			this.lblProgressStatus = new BadLabel();
			this.badProgressBar = new BadProgressBar();
			this.RecalcBattleWN8Theme.SuspendLayout();
			this.SuspendLayout();
			// 
			// RecalcBattleWN8Theme
			// 
			this.RecalcBattleWN8Theme.Controls.Add(this.btnStart);
			this.RecalcBattleWN8Theme.Controls.Add(this.lblProgressStatus);
			this.RecalcBattleWN8Theme.Controls.Add(this.badProgressBar);
			this.RecalcBattleWN8Theme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RecalcBattleWN8Theme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.RecalcBattleWN8Theme.FormFooter = false;
			this.RecalcBattleWN8Theme.FormFooterHeight = 26;
			this.RecalcBattleWN8Theme.FormInnerBorder = 3;
			this.RecalcBattleWN8Theme.FormMargin = 0;
			this.RecalcBattleWN8Theme.Image = null;
			this.RecalcBattleWN8Theme.Location = new System.Drawing.Point(0, 0);
			this.RecalcBattleWN8Theme.MainArea = mainAreaClass1;
			this.RecalcBattleWN8Theme.Name = "RecalcBattleWN8Theme";
			this.RecalcBattleWN8Theme.Resizable = false;
			this.RecalcBattleWN8Theme.Size = new System.Drawing.Size(382, 126);
			this.RecalcBattleWN8Theme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("RecalcBattleWN8Theme.SystemExitImage")));
			this.RecalcBattleWN8Theme.SystemMaximizeImage = null;
			this.RecalcBattleWN8Theme.SystemMinimizeImage = null;
			this.RecalcBattleWN8Theme.TabIndex = 0;
			this.RecalcBattleWN8Theme.Text = "Recalculate battle WN8";
			this.RecalcBattleWN8Theme.TitleHeight = 26;
			// 
			// btnStart
			// 
			this.btnStart.BlackButton = false;
			this.btnStart.Checked = false;
			this.btnStart.Image = null;
			this.btnStart.Location = new System.Drawing.Point(281, 83);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 2;
			this.btnStart.Text = "Start";
			this.btnStart.ToolTipText = "";
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
			// RecalcBattleWN8
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(382, 126);
			this.Controls.Add(this.RecalcBattleWN8Theme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "RecalcBattleWN8";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "UpdateFromApi";
			this.Shown += new System.EventHandler(this.UpdateFromApi_Shown);
			this.RecalcBattleWN8Theme.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm RecalcBattleWN8Theme;
		private BadButton btnStart;
		private BadLabel lblProgressStatus;
		private BadProgressBar badProgressBar;
	}
}