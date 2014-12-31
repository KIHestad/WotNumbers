namespace WinApp.Forms
{
	partial class BattleSummary
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleSummary));
			this.BattleSummaryTheme = new BadForm();
			this.SuspendLayout();
			// 
			// BattleSummaryTheme
			// 
			this.BattleSummaryTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BattleSummaryTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.BattleSummaryTheme.FormExitAsMinimize = false;
			this.BattleSummaryTheme.FormFooter = false;
			this.BattleSummaryTheme.FormFooterHeight = 26;
			this.BattleSummaryTheme.FormInnerBorder = 3;
			this.BattleSummaryTheme.FormMargin = 0;
			this.BattleSummaryTheme.Image = null;
			this.BattleSummaryTheme.Location = new System.Drawing.Point(0, 0);
			this.BattleSummaryTheme.MainArea = mainAreaClass1;
			this.BattleSummaryTheme.Name = "BattleSummaryTheme";
			this.BattleSummaryTheme.Resizable = true;
			this.BattleSummaryTheme.Size = new System.Drawing.Size(589, 361);
			this.BattleSummaryTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("BattleSummaryTheme.SystemExitImage")));
			this.BattleSummaryTheme.SystemMaximizeImage = null;
			this.BattleSummaryTheme.SystemMinimizeImage = null;
			this.BattleSummaryTheme.TabIndex = 0;
			this.BattleSummaryTheme.Text = "Battles Summary";
			this.BattleSummaryTheme.TitleHeight = 26;
			// 
			// BattleSummary
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(589, 361);
			this.Controls.Add(this.BattleSummaryTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "BattleSummary";
			this.Text = "BattleSummary";
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm BattleSummaryTheme;
	}
}