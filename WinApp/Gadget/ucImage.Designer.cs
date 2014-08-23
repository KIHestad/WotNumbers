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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
			this.SuspendLayout();
			// 
			// picture
			// 
			this.picture.Image = ((System.Drawing.Image)(resources.GetObject("picture.Image")));
			this.picture.Location = new System.Drawing.Point(10, 10);
			this.picture.Name = "picture";
			this.picture.Size = new System.Drawing.Size(160, 100);
			this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picture.TabIndex = 0;
			this.picture.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(204)))));
			this.label1.Location = new System.Drawing.Point(10, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "label1";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
			this.label2.Location = new System.Drawing.Point(10, 27);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "label2";
			// 
			// ucImage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.picture);
			this.Name = "ucImage";
			this.Size = new System.Drawing.Size(170, 110);
			this.Load += new System.EventHandler(this.ucImage_Load);
			((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox picture;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}
