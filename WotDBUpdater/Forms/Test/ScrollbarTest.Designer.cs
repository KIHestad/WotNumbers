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
			this.txtX = new BadTextBox();
			this.txtY = new BadTextBox();
			this.badScrollBar2 = new BadScrollBar();
			this.badScrollBar1 = new BadScrollBar();
			this.ScrollbarTestTheme.SuspendLayout();
			this.SuspendLayout();
			// 
			// ScrollbarTestTheme
			// 
			this.ScrollbarTestTheme.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.ScrollbarTestTheme.Controls.Add(this.txtX);
			this.ScrollbarTestTheme.Controls.Add(this.txtY);
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
			// txtX
			// 
			this.txtX.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.txtX.Image = null;
			this.txtX.Location = new System.Drawing.Point(13, 197);
			this.txtX.Name = "txtX";
			this.txtX.PasswordChar = '\0';
			this.txtX.Size = new System.Drawing.Size(56, 23);
			this.txtX.TabIndex = 3;
			// 
			// txtY
			// 
			this.txtY.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.txtY.Image = null;
			this.txtY.Location = new System.Drawing.Point(190, 32);
			this.txtY.Name = "txtY";
			this.txtY.PasswordChar = '\0';
			this.txtY.Size = new System.Drawing.Size(56, 23);
			this.txtY.TabIndex = 2;
			// 
			// badScrollBar2
			// 
			this.badScrollBar2.BackColor = System.Drawing.Color.Transparent;
			this.badScrollBar2.Image = null;
			this.badScrollBar2.Location = new System.Drawing.Point(12, 230);
			this.badScrollBar2.Name = "badScrollBar2";
			this.badScrollBar2.ScrollElementsTotals = 100;
			this.badScrollBar2.ScrollElementsVisible = 20;
			this.badScrollBar2.ScrollOrientation = System.Windows.Forms.ScrollOrientation.HorizontalScroll;
			this.badScrollBar2.ScrollPosition = 0;
			this.badScrollBar2.Size = new System.Drawing.Size(233, 17);
			this.badScrollBar2.TabIndex = 1;
			this.badScrollBar2.Text = "badScrollBar2";
			this.badScrollBar2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.badScrollBar2_MouseMove);
			// 
			// badScrollBar1
			// 
			this.badScrollBar1.BackColor = System.Drawing.Color.Transparent;
			this.badScrollBar1.Image = null;
			this.badScrollBar1.Location = new System.Drawing.Point(254, 32);
			this.badScrollBar1.Name = "badScrollBar1";
			this.badScrollBar1.ScrollElementsTotals = 100;
			this.badScrollBar1.ScrollElementsVisible = 20;
			this.badScrollBar1.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.badScrollBar1.ScrollPosition = 0;
			this.badScrollBar1.Size = new System.Drawing.Size(17, 188);
			this.badScrollBar1.TabIndex = 0;
			this.badScrollBar1.Text = "badScrollBar1";
			this.badScrollBar1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.badScrollBar1_MouseMove);
			// 
			// ScrollbarTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
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
		private BadScrollBar badScrollBar2;
		private BadTextBox txtX;
		private BadTextBox txtY;
	}
}