namespace WinApp.Gadget
{
	partial class ucImage
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucImage));
			this.picture = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
			this.SuspendLayout();
			// 
			// picture
			// 
			this.picture.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.picture.Image = ((System.Drawing.Image)(resources.GetObject("picture.Image")));
			this.picture.Location = new System.Drawing.Point(10, 10);
			this.picture.Name = "picture";
			this.picture.Size = new System.Drawing.Size(280, 180);
			this.picture.TabIndex = 0;
			this.picture.TabStop = false;
			// 
			// ucImage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.picture);
			this.Name = "ucImage";
			this.Size = new System.Drawing.Size(300, 200);
			this.Load += new System.EventHandler(this.ucImage_Load);
			((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox picture;
	}
}
