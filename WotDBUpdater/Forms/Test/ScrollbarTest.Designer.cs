namespace WotDBUpdater.Forms.Test
{
	partial class ScrollbarTest
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScrollbarTest));
			this.ScrollbarTestTheme = new BadForm();
			this.badScrollBar4 = new BadScrollBar();
			this.badScrollBar3 = new BadScrollBar();
			this.badScrollBar2 = new BadScrollBar();
			this.badScrollBar1 = new BadScrollBar();
			this.ScrollbarTestTheme.SuspendLayout();
			this.SuspendLayout();
			// 
			// ScrollbarTestTheme
			// 
			this.ScrollbarTestTheme.Controls.Add(this.badScrollBar4);
			this.ScrollbarTestTheme.Controls.Add(this.badScrollBar3);
			this.ScrollbarTestTheme.Controls.Add(this.badScrollBar2);
			this.ScrollbarTestTheme.Controls.Add(this.badScrollBar1);
			this.ScrollbarTestTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ScrollbarTestTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.ScrollbarTestTheme.FormFooter = false;
			this.ScrollbarTestTheme.FormFooterHeight = 26;
			this.ScrollbarTestTheme.FormInnerBorder = 6;
			this.ScrollbarTestTheme.FormMargin = 0;
			this.ScrollbarTestTheme.Image = null;
			this.ScrollbarTestTheme.Location = new System.Drawing.Point(0, 0);
			this.ScrollbarTestTheme.MainArea = mainAreaClass1;
			this.ScrollbarTestTheme.Name = "ScrollbarTestTheme";
			this.ScrollbarTestTheme.Resizable = true;
			this.ScrollbarTestTheme.Size = new System.Drawing.Size(284, 262);
			this.ScrollbarTestTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("ScrollbarTestTheme.SystemExitImage")));
			this.ScrollbarTestTheme.SystemMaximizeImage = null;
			this.ScrollbarTestTheme.SystemMinimizeImage = null;
			this.ScrollbarTestTheme.TabIndex = 0;
			this.ScrollbarTestTheme.Text = "Scrollbar Test";
			this.ScrollbarTestTheme.TitleHeight = 26;
			// 
			// badScrollBar4
			// 
			this.badScrollBar4.BackColor = System.Drawing.Color.Transparent;
			this.badScrollBar4.Image = null;
			this.badScrollBar4.Location = new System.Drawing.Point(26, 215);
			this.badScrollBar4.Name = "badScrollBar4";
			this.badScrollBar4.ScrollElementsTotals = 100;
			this.badScrollBar4.ScrollElementsVisible = 20;
			this.badScrollBar4.ScrollMarginPixels = 4;
			this.badScrollBar4.ScrollOrientation = System.Windows.Forms.ScrollOrientation.HorizontalScroll;
			this.badScrollBar4.ScrollPosition = 90;
			this.badScrollBar4.Size = new System.Drawing.Size(223, 21);
			this.badScrollBar4.TabIndex = 3;
			this.badScrollBar4.Text = "badScrollBar4";
			// 
			// badScrollBar3
			// 
			this.badScrollBar3.BackColor = System.Drawing.Color.Transparent;
			this.badScrollBar3.Image = null;
			this.badScrollBar3.Location = new System.Drawing.Point(13, 211);
			this.badScrollBar3.Name = "badScrollBar3";
			this.badScrollBar3.ScrollElementsTotals = 0;
			this.badScrollBar3.ScrollElementsVisible = 0;
			this.badScrollBar3.ScrollMarginPixels = 4;
			this.badScrollBar3.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.badScrollBar3.ScrollPosition = 0;
			this.badScrollBar3.Size = new System.Drawing.Size(75, 23);
			this.badScrollBar3.TabIndex = 2;
			this.badScrollBar3.Text = "badScrollBar3";
			// 
			// badScrollBar2
			// 
			this.badScrollBar2.BackColor = System.Drawing.Color.Transparent;
			this.badScrollBar2.Image = null;
			this.badScrollBar2.Location = new System.Drawing.Point(32, 203);
			this.badScrollBar2.Name = "badScrollBar2";
			this.badScrollBar2.ScrollElementsTotals = 0;
			this.badScrollBar2.ScrollElementsVisible = 0;
			this.badScrollBar2.ScrollMarginPixels = 4;
			this.badScrollBar2.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.badScrollBar2.ScrollPosition = 0;
			this.badScrollBar2.Size = new System.Drawing.Size(217, 23);
			this.badScrollBar2.TabIndex = 1;
			this.badScrollBar2.Text = "badScrollBar2";
			// 
			// badScrollBar1
			// 
			this.badScrollBar1.BackColor = System.Drawing.Color.Transparent;
			this.badScrollBar1.Image = null;
			this.badScrollBar1.Location = new System.Drawing.Point(231, 39);
			this.badScrollBar1.Name = "badScrollBar1";
			this.badScrollBar1.ScrollElementsTotals = 100;
			this.badScrollBar1.ScrollElementsVisible = 20;
			this.badScrollBar1.ScrollMarginPixels = 4;
			this.badScrollBar1.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.badScrollBar1.ScrollPosition = 0;
			this.badScrollBar1.Size = new System.Drawing.Size(18, 139);
			this.badScrollBar1.TabIndex = 0;
			this.badScrollBar1.Text = "badScrollBar1";
			// 
			// ScrollbarTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.ScrollbarTestTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ScrollbarTest";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ScrollbarTest";
			this.Resize += new System.EventHandler(this.ScrollbarTest_Resize);
			this.ScrollbarTestTheme.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm ScrollbarTestTheme;
		private BadScrollBar badScrollBar1;
		private BadScrollBar badScrollBar3;
		private BadScrollBar badScrollBar2;
		private BadScrollBar badScrollBar4;
	}
}