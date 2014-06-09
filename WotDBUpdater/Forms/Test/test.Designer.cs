namespace WotDBUpdater.Forms
{
	partial class test
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(test));
			BadThemeContainerControl.MainAreaClass mainAreaClass1 = new BadThemeContainerControl.MainAreaClass();
			this.badForm1 = new BadForm();
			this.label1 = new System.Windows.Forms.Label();
			this.badButton2 = new BadButton();
			this.badButton1 = new BadButton();
			this.badForm1.SuspendLayout();
			this.SuspendLayout();
			// 
			// badForm1
			// 
			this.badForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badForm1.Controls.Add(this.label1);
			this.badForm1.Controls.Add(this.badButton2);
			this.badForm1.Controls.Add(this.badButton1);
			this.badForm1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.badForm1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(156)))));
			this.badForm1.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.badForm1.FormFooter = true;
			this.badForm1.FormFooterHeight = 24;
			this.badForm1.FormInnerBorder = 3;
			this.badForm1.FormMargin = 0;
			this.badForm1.Image = ((System.Drawing.Image)(resources.GetObject("badForm1.Image")));
			this.badForm1.Location = new System.Drawing.Point(0, 0);
			this.badForm1.MainArea = mainAreaClass1;
			this.badForm1.Name = "badForm1";
			this.badForm1.Resizable = true;
			this.badForm1.Size = new System.Drawing.Size(366, 234);
			this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
			this.badForm1.SystemMaximizeImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemMaximizeImage")));
			this.badForm1.SystemMinimizeImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemMinimizeImage")));
			this.badForm1.TabIndex = 0;
			this.badForm1.Text = "Test Form";
			this.badForm1.TitleHeight = 26;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(10, 211);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(109, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Footertext blah blah...";
			// 
			// badButton2
			// 
			this.badButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.badButton2.Image = null;
			this.badButton2.Location = new System.Drawing.Point(153, 173);
			this.badButton2.Name = "badButton2";
			this.badButton2.Size = new System.Drawing.Size(137, 23);
			this.badButton2.TabIndex = 1;
			this.badButton2.Text = "Change Frame Border";
			this.badButton2.Click += new System.EventHandler(this.badButton2_Click);
			// 
			// badButton1
			// 
			this.badButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.badButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
			this.badButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
			this.badButton1.Image = null;
			this.badButton1.Location = new System.Drawing.Point(298, 173);
			this.badButton1.Name = "badButton1";
			this.badButton1.Size = new System.Drawing.Size(56, 23);
			this.badButton1.TabIndex = 0;
			this.badButton1.Text = "Close";
			this.badButton1.Click += new System.EventHandler(this.badButton1_Click);
			// 
			// test
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.ClientSize = new System.Drawing.Size(366, 234);
			this.Controls.Add(this.badForm1);
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MinimumSize = new System.Drawing.Size(300, 150);
			this.Name = "test";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "test";
			this.Resize += new System.EventHandler(this.test_Resize);
			this.badForm1.ResumeLayout(false);
			this.badForm1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private BadButton badButton1;
		private BadForm badForm1;
		private BadButton badButton2;
		private System.Windows.Forms.Label label1;
















	}
}