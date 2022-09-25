namespace WinApp.Forms
{
	partial class RecalcBattleMinTier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecalcBattleMinTier));
            this.RecalcBattleMinTierTheme = new BadForm();
            this.btnStart = new BadButton();
            this.lblProgressStatus = new BadLabel();
            this.badProgressBar = new BadProgressBar();
            this.RecalcBattleMinTierTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // RecalcBattleMinTierTheme
            // 
            this.RecalcBattleMinTierTheme.Controls.Add(this.btnStart);
            this.RecalcBattleMinTierTheme.Controls.Add(this.lblProgressStatus);
            this.RecalcBattleMinTierTheme.Controls.Add(this.badProgressBar);
            this.RecalcBattleMinTierTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RecalcBattleMinTierTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RecalcBattleMinTierTheme.FormExitAsMinimize = false;
            this.RecalcBattleMinTierTheme.FormFooter = false;
            this.RecalcBattleMinTierTheme.FormFooterHeight = 26;
            this.RecalcBattleMinTierTheme.FormInnerBorder = 3;
            this.RecalcBattleMinTierTheme.FormMargin = 0;
            this.RecalcBattleMinTierTheme.Image = null;
            this.RecalcBattleMinTierTheme.Location = new System.Drawing.Point(0, 0);
            this.RecalcBattleMinTierTheme.MainArea = mainAreaClass1;
            this.RecalcBattleMinTierTheme.Name = "RecalcBattleMinTierTheme";
            this.RecalcBattleMinTierTheme.Resizable = false;
            this.RecalcBattleMinTierTheme.Size = new System.Drawing.Size(382, 126);
            this.RecalcBattleMinTierTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("RecalcBattleMinTierTheme.SystemExitImage")));
            this.RecalcBattleMinTierTheme.SystemMaximizeImage = null;
            this.RecalcBattleMinTierTheme.SystemMinimizeImage = null;
            this.RecalcBattleMinTierTheme.TabIndex = 0;
            this.RecalcBattleMinTierTheme.Text = "Recalculate battle min tier";
            this.RecalcBattleMinTierTheme.TitleHeight = 26;
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
            this.lblProgressStatus.Image = null;
            this.lblProgressStatus.Location = new System.Drawing.Point(26, 83);
            this.lblProgressStatus.Name = "lblProgressStatus";
            this.lblProgressStatus.Size = new System.Drawing.Size(249, 23);
            this.lblProgressStatus.TabIndex = 1;
            this.lblProgressStatus.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
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
            // RecalcBattleMinTier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 126);
            this.Controls.Add(this.RecalcBattleMinTierTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RecalcBattleMinTier";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UpdateFromApi";
            this.Shown += new System.EventHandler(this.RecalcBattleMinTier_Shown);
            this.RecalcBattleMinTierTheme.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private BadForm RecalcBattleMinTierTheme;
		private BadButton btnStart;
		private BadLabel lblProgressStatus;
		private BadProgressBar badProgressBar;
	}
}