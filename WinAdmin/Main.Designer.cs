namespace WinAdmin
{
	partial class Main
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.toolStripMain = new System.Windows.Forms.ToolStrip();
			this.roolItemFile = new System.Windows.Forms.ToolStripDropDownButton();
			this.roolItemFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripMain
			// 
			this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roolItemFile});
			this.toolStripMain.Location = new System.Drawing.Point(0, 0);
			this.toolStripMain.Name = "toolStripMain";
			this.toolStripMain.ShowItemToolTips = false;
			this.toolStripMain.Size = new System.Drawing.Size(453, 25);
			this.toolStripMain.TabIndex = 0;
			this.toolStripMain.Text = "toolStrip1";
			// 
			// roolItemFile
			// 
			this.roolItemFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.roolItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roolItemFileExit});
			this.roolItemFile.Image = ((System.Drawing.Image)(resources.GetObject("roolItemFile.Image")));
			this.roolItemFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.roolItemFile.Name = "roolItemFile";
			this.roolItemFile.Size = new System.Drawing.Size(38, 22);
			this.roolItemFile.Text = "&File";
			// 
			// roolItemFileExit
			// 
			this.roolItemFileExit.Name = "roolItemFileExit";
			this.roolItemFileExit.Size = new System.Drawing.Size(152, 22);
			this.roolItemFileExit.Text = "&Exit";
			this.roolItemFileExit.Click += new System.EventHandler(this.roolItemFileExit_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(453, 256);
			this.Controls.Add(this.toolStripMain);
			this.Name = "Main";
			this.Text = "Wot Number Admin";
			this.toolStripMain.ResumeLayout(false);
			this.toolStripMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStripMain;
		private System.Windows.Forms.ToolStripDropDownButton roolItemFile;
		private System.Windows.Forms.ToolStripMenuItem roolItemFileExit;
	}
}

