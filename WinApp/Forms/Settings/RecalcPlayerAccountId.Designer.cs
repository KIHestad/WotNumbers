namespace WinApp.Forms
{
	partial class RecalcPlayerAccountId
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecalcPlayerAccountId));
            this.RecalcPlayerAccountIdTheme = new BadForm();
            this.btnStart = new BadButton();
            this.lblProgressStatus = new BadLabel();
            this.badProgressBar = new BadProgressBar();
            this.RecalcPlayerAccountIdTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // RecalcPlayerAccountIdTheme
            // 
            this.RecalcPlayerAccountIdTheme.Controls.Add(this.btnStart);
            this.RecalcPlayerAccountIdTheme.Controls.Add(this.lblProgressStatus);
            this.RecalcPlayerAccountIdTheme.Controls.Add(this.badProgressBar);
            this.RecalcPlayerAccountIdTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RecalcPlayerAccountIdTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RecalcPlayerAccountIdTheme.FormExitAsMinimize = false;
            this.RecalcPlayerAccountIdTheme.FormFooter = false;
            this.RecalcPlayerAccountIdTheme.FormFooterHeight = 26;
            this.RecalcPlayerAccountIdTheme.FormInnerBorder = 3;
            this.RecalcPlayerAccountIdTheme.FormMargin = 0;
            this.RecalcPlayerAccountIdTheme.Image = null;
            this.RecalcPlayerAccountIdTheme.Location = new System.Drawing.Point(0, 0);
            this.RecalcPlayerAccountIdTheme.MainArea = mainAreaClass1;
            this.RecalcPlayerAccountIdTheme.Name = "RecalcPlayerAccountIdTheme";
            this.RecalcPlayerAccountIdTheme.Resizable = false;
            this.RecalcPlayerAccountIdTheme.Size = new System.Drawing.Size(382, 126);
            this.RecalcPlayerAccountIdTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("RecalcPlayerAccountIdTheme.SystemExitImage")));
            this.RecalcPlayerAccountIdTheme.SystemMaximizeImage = null;
            this.RecalcPlayerAccountIdTheme.SystemMinimizeImage = null;
            this.RecalcPlayerAccountIdTheme.TabIndex = 0;
            this.RecalcPlayerAccountIdTheme.Text = "Recalculate player account id";
            this.RecalcPlayerAccountIdTheme.TitleHeight = 26;
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
            // RecalcPlayerAccountId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 126);
            this.Controls.Add(this.RecalcPlayerAccountIdTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RecalcPlayerAccountId";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UpdateFromApi";
            this.Shown += new System.EventHandler(this.RecalcPlayerAccountId_Shown);
            this.RecalcPlayerAccountIdTheme.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private BadForm RecalcPlayerAccountIdTheme;
		private BadButton btnStart;
		private BadLabel lblProgressStatus;
		private BadProgressBar badProgressBar;
	}
}