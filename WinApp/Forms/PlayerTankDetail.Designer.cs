namespace WinApp.Forms
{
	partial class PlayerTankDetail
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerTankDetail));
			this.PlayerTankDetailsTheme = new BadForm();
			this.lblFooter = new System.Windows.Forms.Label();
			this.scrollTankDetails = new BadScrollBar();
			this.dataGridTankDetail = new System.Windows.Forms.DataGridView();
			this.picLarge = new System.Windows.Forms.PictureBox();
			this.PlayerTankDetailsTheme.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridTankDetail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picLarge)).BeginInit();
			this.SuspendLayout();
			// 
			// PlayerTankDetailsTheme
			// 
			this.PlayerTankDetailsTheme.Controls.Add(this.lblFooter);
			this.PlayerTankDetailsTheme.Controls.Add(this.scrollTankDetails);
			this.PlayerTankDetailsTheme.Controls.Add(this.dataGridTankDetail);
			this.PlayerTankDetailsTheme.Controls.Add(this.picLarge);
			this.PlayerTankDetailsTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PlayerTankDetailsTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.PlayerTankDetailsTheme.FormFooter = true;
			this.PlayerTankDetailsTheme.FormFooterHeight = 23;
			this.PlayerTankDetailsTheme.FormInnerBorder = 0;
			this.PlayerTankDetailsTheme.FormMargin = 0;
			this.PlayerTankDetailsTheme.Image = null;
			this.PlayerTankDetailsTheme.Location = new System.Drawing.Point(0, 0);
			this.PlayerTankDetailsTheme.MainArea = mainAreaClass1;
			this.PlayerTankDetailsTheme.Name = "PlayerTankDetailsTheme";
			this.PlayerTankDetailsTheme.Resizable = true;
			this.PlayerTankDetailsTheme.Size = new System.Drawing.Size(223, 572);
			this.PlayerTankDetailsTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("PlayerTankDetailsTheme.SystemExitImage")));
			this.PlayerTankDetailsTheme.SystemMaximizeImage = null;
			this.PlayerTankDetailsTheme.SystemMinimizeImage = ((System.Drawing.Image)(resources.GetObject("PlayerTankDetailsTheme.SystemMinimizeImage")));
			this.PlayerTankDetailsTheme.TabIndex = 0;
			this.PlayerTankDetailsTheme.Text = "Tank Details";
			this.PlayerTankDetailsTheme.TitleHeight = 26;
			// 
			// lblFooter
			// 
			this.lblFooter.AutoSize = true;
			this.lblFooter.BackColor = System.Drawing.Color.Transparent;
			this.lblFooter.ForeColor = System.Drawing.Color.DarkGray;
			this.lblFooter.Location = new System.Drawing.Point(5, 553);
			this.lblFooter.Name = "lblFooter";
			this.lblFooter.Size = new System.Drawing.Size(66, 13);
			this.lblFooter.TabIndex = 10;
			this.lblFooter.Text = "Footer text...";
			// 
			// scrollTankDetails
			// 
			this.scrollTankDetails.Image = null;
			this.scrollTankDetails.Location = new System.Drawing.Point(177, 127);
			this.scrollTankDetails.Name = "scrollTankDetails";
			this.scrollTankDetails.ScrollElementsTotals = 100;
			this.scrollTankDetails.ScrollElementsVisible = 20;
			this.scrollTankDetails.ScrollHide = true;
			this.scrollTankDetails.ScrollNecessary = true;
			this.scrollTankDetails.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.scrollTankDetails.ScrollPosition = 0;
			this.scrollTankDetails.Size = new System.Drawing.Size(17, 301);
			this.scrollTankDetails.TabIndex = 9;
			this.scrollTankDetails.Text = "badScrollBar1";
			this.scrollTankDetails.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollTankDetails_MouseDown);
			this.scrollTankDetails.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollTankDetails_MouseMove);
			this.scrollTankDetails.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scrollTankDetails_MouseUp);
			// 
			// dataGridTankDetail
			// 
			this.dataGridTankDetail.AllowUserToAddRows = false;
			this.dataGridTankDetail.AllowUserToDeleteRows = false;
			this.dataGridTankDetail.AllowUserToResizeRows = false;
			this.dataGridTankDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridTankDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridTankDetail.ColumnHeadersVisible = false;
			this.dataGridTankDetail.Cursor = System.Windows.Forms.Cursors.Default;
			this.dataGridTankDetail.Location = new System.Drawing.Point(1, 127);
			this.dataGridTankDetail.Name = "dataGridTankDetail";
			this.dataGridTankDetail.RowHeadersVisible = false;
			this.dataGridTankDetail.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dataGridTankDetail.Size = new System.Drawing.Size(160, 301);
			this.dataGridTankDetail.TabIndex = 8;
			this.dataGridTankDetail.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridTankDetail_CellEnter);
			this.dataGridTankDetail.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridTankDetail_CellFormatting);
			// 
			// picLarge
			// 
			this.picLarge.BackColor = System.Drawing.Color.Transparent;
			this.picLarge.Location = new System.Drawing.Point(32, 28);
			this.picLarge.Name = "picLarge";
			this.picLarge.Size = new System.Drawing.Size(160, 100);
			this.picLarge.TabIndex = 7;
			this.picLarge.TabStop = false;
			// 
			// PlayerTankDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Fuchsia;
			this.ClientSize = new System.Drawing.Size(223, 572);
			this.Controls.Add(this.PlayerTankDetailsTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(200, 300);
			this.Name = "PlayerTankDetail";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Tank Details";
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.Load += new System.EventHandler(this.PlayerTankDetails_Load);
			this.ResizeEnd += new System.EventHandler(this.PlayerTankDetails_ResizeEnd);
			this.Resize += new System.EventHandler(this.PlayerTankDetails_Resize);
			this.PlayerTankDetailsTheme.ResumeLayout(false);
			this.PlayerTankDetailsTheme.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridTankDetail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picLarge)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm PlayerTankDetailsTheme;
		private System.Windows.Forms.PictureBox picLarge;
		private System.Windows.Forms.DataGridView dataGridTankDetail;
		private BadScrollBar scrollTankDetails;
		private System.Windows.Forms.Label lblFooter;
	}
}