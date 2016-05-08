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
            this.cmdStart = new System.Windows.Forms.Button();
            this.chkFetchNewTanks = new System.Windows.Forms.CheckBox();
            this.rbEU = new System.Windows.Forms.RadioButton();
            this.rbNA = new System.Windows.Forms.RadioButton();
            this.chkKeepExistingImg = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(25, 78);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(375, 23);
            this.pbStatus.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(22, 116);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(113, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Ready to start import...";
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(325, 111);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(75, 23);
            this.cmdStart.TabIndex = 2;
            this.cmdStart.Text = "Start";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // chkFetchNewTanks
            // 
            this.chkFetchNewTanks.AutoSize = true;
            this.chkFetchNewTanks.Checked = true;
            this.chkFetchNewTanks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFetchNewTanks.Location = new System.Drawing.Point(25, 12);
            this.chkFetchNewTanks.Name = "chkFetchNewTanks";
            this.chkFetchNewTanks.Size = new System.Drawing.Size(105, 17);
            this.chkFetchNewTanks.TabIndex = 3;
            this.chkFetchNewTanks.Text = "Fetch new tanks";
            this.chkFetchNewTanks.UseVisualStyleBackColor = true;
            // 
            // rbEU
            // 
            this.rbEU.AutoSize = true;
            this.rbEU.Checked = true;
            this.rbEU.Location = new System.Drawing.Point(325, 11);
            this.rbEU.Name = "rbEU";
            this.rbEU.Size = new System.Drawing.Size(72, 17);
            this.rbEU.TabIndex = 4;
            this.rbEU.TabStop = true;
            this.rbEU.Text = "EU server";
            this.rbEU.UseVisualStyleBackColor = true;
            // 
            // rbNA
            // 
            this.rbNA.AutoSize = true;
            this.rbNA.Location = new System.Drawing.Point(325, 35);
            this.rbNA.Name = "rbNA";
            this.rbNA.Size = new System.Drawing.Size(72, 17);
            this.rbNA.TabIndex = 5;
            this.rbNA.Text = "NA server";
            this.rbNA.UseVisualStyleBackColor = true;
            // 
            // chkKeepExistingImg
            // 
            this.chkKeepExistingImg.AutoSize = true;
            this.chkKeepExistingImg.Checked = true;
            this.chkKeepExistingImg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKeepExistingImg.Location = new System.Drawing.Point(25, 36);
            this.chkKeepExistingImg.Name = "chkKeepExistingImg";
            this.chkKeepExistingImg.Size = new System.Drawing.Size(125, 17);
            this.chkKeepExistingImg.TabIndex = 6;
            this.chkKeepExistingImg.Text = "Keep existing images";
            this.chkKeepExistingImg.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(244, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Start OLD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GetTankDataFromAPI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 149);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkKeepExistingImg);
            this.Controls.Add(this.rbNA);
            this.Controls.Add(this.rbEU);
            this.Controls.Add(this.chkFetchNewTanks);
            this.Controls.Add(this.cmdStart);
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
		private System.Windows.Forms.Button cmdStart;
		private System.Windows.Forms.CheckBox chkFetchNewTanks;
		private System.Windows.Forms.RadioButton rbEU;
		private System.Windows.Forms.RadioButton rbNA;
		private System.Windows.Forms.CheckBox chkKeepExistingImg;
        private System.Windows.Forms.Button button1;
	}
}