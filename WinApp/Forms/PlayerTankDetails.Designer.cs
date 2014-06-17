namespace WinApp.Forms
{
	partial class PlayerTankDetails
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerTankDetails));
			this.PlayerTankDetailsTheme = new BadForm();
			this.picIcon = new System.Windows.Forms.PictureBox();
			this.picSmall = new System.Windows.Forms.PictureBox();
			this.picLarge = new System.Windows.Forms.PictureBox();
			this.PlayerTankDetailsTheme.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picSmall)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picLarge)).BeginInit();
			this.SuspendLayout();
			// 
			// PlayerTankDetailsTheme
			// 
			this.PlayerTankDetailsTheme.Controls.Add(this.picIcon);
			this.PlayerTankDetailsTheme.Controls.Add(this.picSmall);
			this.PlayerTankDetailsTheme.Controls.Add(this.picLarge);
			this.PlayerTankDetailsTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PlayerTankDetailsTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.PlayerTankDetailsTheme.FormFooter = false;
			this.PlayerTankDetailsTheme.FormFooterHeight = 26;
			this.PlayerTankDetailsTheme.FormInnerBorder = 3;
			this.PlayerTankDetailsTheme.FormMargin = 0;
			this.PlayerTankDetailsTheme.Image = null;
			this.PlayerTankDetailsTheme.Location = new System.Drawing.Point(0, 0);
			this.PlayerTankDetailsTheme.MainArea = mainAreaClass1;
			this.PlayerTankDetailsTheme.Name = "PlayerTankDetailsTheme";
			this.PlayerTankDetailsTheme.Resizable = true;
			this.PlayerTankDetailsTheme.Size = new System.Drawing.Size(222, 262);
			this.PlayerTankDetailsTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("PlayerTankDetailsTheme.SystemExitImage")));
			this.PlayerTankDetailsTheme.SystemMaximizeImage = null;
			this.PlayerTankDetailsTheme.SystemMinimizeImage = null;
			this.PlayerTankDetailsTheme.TabIndex = 0;
			this.PlayerTankDetailsTheme.Text = "Tank Details";
			this.PlayerTankDetailsTheme.TitleHeight = 26;
			// 
			// picIcon
			// 
			this.picIcon.BackColor = System.Drawing.Color.Transparent;
			this.picIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picIcon.Location = new System.Drawing.Point(81, 212);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size(65, 24);
			this.picIcon.TabIndex = 9;
			this.picIcon.TabStop = false;
			// 
			// picSmall
			// 
			this.picSmall.BackColor = System.Drawing.Color.Transparent;
			this.picSmall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picSmall.Location = new System.Drawing.Point(51, 165);
			this.picSmall.Name = "picSmall";
			this.picSmall.Size = new System.Drawing.Size(124, 31);
			this.picSmall.TabIndex = 8;
			this.picSmall.TabStop = false;
			// 
			// picLarge
			// 
			this.picLarge.BackColor = System.Drawing.Color.Transparent;
			this.picLarge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picLarge.Location = new System.Drawing.Point(33, 50);
			this.picLarge.Name = "picLarge";
			this.picLarge.Size = new System.Drawing.Size(160, 100);
			this.picLarge.TabIndex = 7;
			this.picLarge.TabStop = false;
			// 
			// PlayerTankDetails
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(222, 262);
			this.Controls.Add(this.PlayerTankDetailsTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PlayerTankDetails";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Tank Details";
			this.Load += new System.EventHandler(this.PlayerTankDetails_Load);
			this.PlayerTankDetailsTheme.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picSmall)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picLarge)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm PlayerTankDetailsTheme;
		private System.Windows.Forms.PictureBox picIcon;
		private System.Windows.Forms.PictureBox picSmall;
		private System.Windows.Forms.PictureBox picLarge;
	}
}