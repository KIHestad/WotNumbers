namespace WinApp.Forms
{
	partial class ApplicationLayout
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationLayout));
			this.badForm1 = new BadForm();
			this.btnSave = new BadButton();
			this.btnCancel = new BadButton();
			this.badGroupBox1 = new BadGroupBox();
			this.chkBattleTotalsPosition = new BadCheckBox();
			this.badLabel1 = new BadLabel();
			this.ddFontSize = new BadDropDownBox();
			this.badForm1.SuspendLayout();
			this.SuspendLayout();
			// 
			// badForm1
			// 
			this.badForm1.Controls.Add(this.ddFontSize);
			this.badForm1.Controls.Add(this.badLabel1);
			this.badForm1.Controls.Add(this.chkBattleTotalsPosition);
			this.badForm1.Controls.Add(this.badGroupBox1);
			this.badForm1.Controls.Add(this.btnCancel);
			this.badForm1.Controls.Add(this.btnSave);
			this.badForm1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.badForm1.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.badForm1.FormFooter = false;
			this.badForm1.FormFooterHeight = 26;
			this.badForm1.FormInnerBorder = 3;
			this.badForm1.FormMargin = 0;
			this.badForm1.Image = null;
			this.badForm1.Location = new System.Drawing.Point(0, 0);
			this.badForm1.MainArea = mainAreaClass1;
			this.badForm1.Name = "badForm1";
			this.badForm1.Resizable = false;
			this.badForm1.Size = new System.Drawing.Size(352, 197);
			this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
			this.badForm1.SystemMaximizeImage = null;
			this.badForm1.SystemMinimizeImage = null;
			this.badForm1.TabIndex = 0;
			this.badForm1.Text = "Application Layout";
			this.badForm1.TitleHeight = 26;
			// 
			// btnSave
			// 
			this.btnSave.Image = null;
			this.btnSave.Location = new System.Drawing.Point(181, 153);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(70, 23);
			this.btnSave.TabIndex = 0;
			this.btnSave.Text = "Save";
			// 
			// btnCancel
			// 
			this.btnCancel.Image = null;
			this.btnCancel.Location = new System.Drawing.Point(257, 153);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(70, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Close";
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(22, 46);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(308, 91);
			this.badGroupBox1.TabIndex = 2;
			this.badGroupBox1.Text = "Settings";
			// 
			// chkBattleTotalsPosition
			// 
			this.chkBattleTotalsPosition.BackColor = System.Drawing.Color.Transparent;
			this.chkBattleTotalsPosition.Checked = false;
			this.chkBattleTotalsPosition.Image = null;
			this.chkBattleTotalsPosition.Location = new System.Drawing.Point(41, 98);
			this.chkBattleTotalsPosition.Name = "chkBattleTotalsPosition";
			this.chkBattleTotalsPosition.Size = new System.Drawing.Size(268, 23);
			this.chkBattleTotalsPosition.TabIndex = 3;
			this.chkBattleTotalsPosition.Text = "Show battle average and totals at grid top";
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.badLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(41, 69);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(81, 23);
			this.badLabel1.TabIndex = 4;
			this.badLabel1.Text = "Grid Font Size";
			// 
			// ddFontSize
			// 
			this.ddFontSize.Image = null;
			this.ddFontSize.Location = new System.Drawing.Point(129, 69);
			this.ddFontSize.Name = "ddFontSize";
			this.ddFontSize.Size = new System.Drawing.Size(86, 23);
			this.ddFontSize.TabIndex = 5;
			this.ddFontSize.Click += new System.EventHandler(this.ddFontSize_Click);
			// 
			// ApplicationLayout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(352, 197);
			this.Controls.Add(this.badForm1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ApplicationLayout";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ApplicationLayout";
			this.badForm1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm badForm1;
		private BadGroupBox badGroupBox1;
		private BadButton btnCancel;
		private BadButton btnSave;
		private BadDropDownBox ddFontSize;
		private BadLabel badLabel1;
		private BadCheckBox chkBattleTotalsPosition;
	}
}