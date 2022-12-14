namespace WinApp.Forms
{
	partial class FixBattleTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FixBattleTable));
            this.FixBattleTableTheme = new BadForm();
            this.btnStart = new BadButton();
            this.lblProgressStatus = new BadLabel();
            this.badProgressBar = new BadProgressBar();
            this.FixBattleTableTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // FixBattleTableTheme
            // 
            this.FixBattleTableTheme.Controls.Add(this.btnStart);
            this.FixBattleTableTheme.Controls.Add(this.lblProgressStatus);
            this.FixBattleTableTheme.Controls.Add(this.badProgressBar);
            this.FixBattleTableTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FixBattleTableTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FixBattleTableTheme.FormExitAsMinimize = false;
            this.FixBattleTableTheme.FormFooter = false;
            this.FixBattleTableTheme.FormFooterHeight = 26;
            this.FixBattleTableTheme.FormInnerBorder = 3;
            this.FixBattleTableTheme.FormMargin = 0;
            this.FixBattleTableTheme.Image = null;
            this.FixBattleTableTheme.Location = new System.Drawing.Point(0, 0);
            this.FixBattleTableTheme.MainArea = mainAreaClass1;
            this.FixBattleTableTheme.Name = "FixBattleTableTheme";
            this.FixBattleTableTheme.Resizable = false;
            this.FixBattleTableTheme.Size = new System.Drawing.Size(382, 126);
            this.FixBattleTableTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("FixBattleTableTheme.  ")));
            this.FixBattleTableTheme.SystemMaximizeImage = null;
            this.FixBattleTableTheme.SystemMinimizeImage = null;
            this.FixBattleTableTheme.TabIndex = 0;
            this.FixBattleTableTheme.Text = "Recalculate battle table entries";
            this.FixBattleTableTheme.TitleHeight = 26;
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
            // FixBattleTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 126);
            this.Controls.Add(this.FixBattleTableTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FixBattleTable";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UpdateFromApi";
            this.Shown += new System.EventHandler(this.FixBattleTable_Shown);
            this.FixBattleTableTheme.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private BadForm FixBattleTableTheme;
		private BadButton btnStart;
		private BadLabel lblProgressStatus;
		private BadProgressBar badProgressBar;
	}
}