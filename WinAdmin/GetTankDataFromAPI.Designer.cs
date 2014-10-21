namespace WinAdmin
{
	partial class GetTankDataFromAPI
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
			this.pbStatus = new System.Windows.Forms.ProgressBar();
			this.lblStatus = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pbStatus
			// 
			this.pbStatus.Location = new System.Drawing.Point(23, 21);
			this.pbStatus.Name = "pbStatus";
			this.pbStatus.Size = new System.Drawing.Size(375, 23);
			this.pbStatus.TabIndex = 0;
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(20, 58);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(113, 13);
			this.lblStatus.TabIndex = 1;
			this.lblStatus.Text = "Ready to start import...";
			// 
			// GetTankDataFromAPI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(421, 93);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.pbStatus);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GetTankDataFromAPI";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Get Tank Data From API";
			this.Load += new System.EventHandler(this.GetTankDataFromAPI_Load);
			this.Shown += new System.EventHandler(this.GetTankDataFromAPI_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar pbStatus;
		private System.Windows.Forms.Label lblStatus;
	}
}