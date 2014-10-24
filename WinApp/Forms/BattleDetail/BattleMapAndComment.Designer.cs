namespace WinApp.Forms
{
	partial class BattleMapAndComment
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
			this.picMap = new System.Windows.Forms.PictureBox();
			this.lblMapDescription = new BadLabel();
			((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
			this.SuspendLayout();
			// 
			// picMap
			// 
			this.picMap.Location = new System.Drawing.Point(15, 15);
			this.picMap.Name = "picMap";
			this.picMap.Size = new System.Drawing.Size(300, 300);
			this.picMap.TabIndex = 1;
			this.picMap.TabStop = false;
			// 
			// lblMapDescription
			// 
			this.lblMapDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblMapDescription.Dimmed = false;
			this.lblMapDescription.Image = null;
			this.lblMapDescription.Location = new System.Drawing.Point(15, 331);
			this.lblMapDescription.Name = "lblMapDescription";
			this.lblMapDescription.Size = new System.Drawing.Size(300, 106);
			this.lblMapDescription.TabIndex = 2;
			this.lblMapDescription.TxtAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// BattleMapAndComment
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.Controls.Add(this.lblMapDescription);
			this.Controls.Add(this.picMap);
			this.Name = "BattleMapAndComment";
			this.Size = new System.Drawing.Size(850, 450);
			this.Load += new System.EventHandler(this.BattleMapAndComment_Load);
			((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox picMap;
		private BadLabel lblMapDescription;
	}
}
