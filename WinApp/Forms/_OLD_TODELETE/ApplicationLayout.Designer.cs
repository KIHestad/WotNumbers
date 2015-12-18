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
			this.components = new System.ComponentModel.Container();
			BadThemeContainerControl.MainAreaClass mainAreaClass1 = new BadThemeContainerControl.MainAreaClass();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationLayout));
			this.badForm1 = new BadForm();
			this.badLabel2 = new BadLabel();
			this.chkNotifyIconFormExitToMinimize = new BadCheckBox();
			this.chkNotifyIconUse = new BadCheckBox();
			this.badGroupBox2 = new BadGroupBox();
			this.chkSmallMasteryBadgeIcons = new BadCheckBox();
			this.ddFontSize = new BadDropDownBox();
			this.badLabel1 = new BadLabel();
			this.chkBattleTotalsPosition = new BadCheckBox();
			this.badGroupBox1 = new BadGroupBox();
			this.btnCancel = new BadButton();
			this.btnSave = new BadButton();
			this.badForm1.SuspendLayout();
			this.SuspendLayout();
			// 
			// badForm1
			// 
			this.badForm1.Controls.Add(this.badLabel2);
			this.badForm1.Controls.Add(this.chkNotifyIconFormExitToMinimize);
			this.badForm1.Controls.Add(this.chkNotifyIconUse);
			this.badForm1.Controls.Add(this.badGroupBox2);
			this.badForm1.Controls.Add(this.chkSmallMasteryBadgeIcons);
			this.badForm1.Controls.Add(this.ddFontSize);
			this.badForm1.Controls.Add(this.badLabel1);
			this.badForm1.Controls.Add(this.chkBattleTotalsPosition);
			this.badForm1.Controls.Add(this.badGroupBox1);
			this.badForm1.Controls.Add(this.btnCancel);
			this.badForm1.Controls.Add(this.btnSave);
			this.badForm1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.badForm1.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.badForm1.FormExitAsMinimize = false;
			this.badForm1.FormFooter = false;
			this.badForm1.FormFooterHeight = 26;
			this.badForm1.FormInnerBorder = 3;
			this.badForm1.FormMargin = 0;
			this.badForm1.Image = null;
			this.badForm1.Location = new System.Drawing.Point(0, 0);
			this.badForm1.MainArea = mainAreaClass1;
			this.badForm1.Name = "badForm1";
			this.badForm1.Resizable = false;
			this.badForm1.Size = new System.Drawing.Size(353, 360);
			this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
			this.badForm1.SystemMaximizeImage = null;
			this.badForm1.SystemMinimizeImage = null;
			this.badForm1.TabIndex = 0;
			this.badForm1.Text = "Application Layout";
			this.badForm1.TitleHeight = 26;
			// 
			// badLabel2
			// 
			this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel2.Dimmed = false;
			this.badLabel2.Image = null;
			this.badLabel2.Location = new System.Drawing.Point(41, 66);
			this.badLabel2.Name = "badLabel2";
			this.badLabel2.Size = new System.Drawing.Size(242, 23);
			this.badLabel2.TabIndex = 12;
			this.badLabel2.Text = "Changes will apply for next application startup";
			this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// chkNotifyIconFormExitToMinimize
			// 
			this.chkNotifyIconFormExitToMinimize.BackColor = System.Drawing.Color.Transparent;
			this.chkNotifyIconFormExitToMinimize.Checked = false;
			this.chkNotifyIconFormExitToMinimize.Image = ((System.Drawing.Image)(resources.GetObject("chkNotifyIconFormExitToMinimize.Image")));
			this.chkNotifyIconFormExitToMinimize.Location = new System.Drawing.Point(41, 124);
			this.chkNotifyIconFormExitToMinimize.Name = "chkNotifyIconFormExitToMinimize";
			this.chkNotifyIconFormExitToMinimize.Size = new System.Drawing.Size(268, 23);
			this.chkNotifyIconFormExitToMinimize.TabIndex = 11;
			this.chkNotifyIconFormExitToMinimize.Text = "Minimize  when closing application";
			// 
			// chkNotifyIconUse
			// 
			this.chkNotifyIconUse.BackColor = System.Drawing.Color.Transparent;
			this.chkNotifyIconUse.Checked = false;
			this.chkNotifyIconUse.Image = ((System.Drawing.Image)(resources.GetObject("chkNotifyIconUse.Image")));
			this.chkNotifyIconUse.Location = new System.Drawing.Point(41, 95);
			this.chkNotifyIconUse.Name = "chkNotifyIconUse";
			this.chkNotifyIconUse.Size = new System.Drawing.Size(140, 23);
			this.chkNotifyIconUse.TabIndex = 10;
			this.chkNotifyIconUse.Text = "Use system tray icon";
			this.chkNotifyIconUse.Click += new System.EventHandler(this.chkNotifyIconUse_Click);
			// 
			// badGroupBox2
			// 
			this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox2.Image = null;
			this.badGroupBox2.Location = new System.Drawing.Point(22, 45);
			this.badGroupBox2.Name = "badGroupBox2";
			this.badGroupBox2.Size = new System.Drawing.Size(308, 115);
			this.badGroupBox2.TabIndex = 9;
			this.badGroupBox2.Text = "Application Behavior";
			// 
			// chkSmallMasteryBadgeIcons
			// 
			this.chkSmallMasteryBadgeIcons.BackColor = System.Drawing.Color.Transparent;
			this.chkSmallMasteryBadgeIcons.Checked = false;
			this.chkSmallMasteryBadgeIcons.Image = ((System.Drawing.Image)(resources.GetObject("chkSmallMasteryBadgeIcons.Image")));
			this.chkSmallMasteryBadgeIcons.Location = new System.Drawing.Point(41, 261);
			this.chkSmallMasteryBadgeIcons.Name = "chkSmallMasteryBadgeIcons";
			this.chkSmallMasteryBadgeIcons.Size = new System.Drawing.Size(193, 23);
			this.chkSmallMasteryBadgeIcons.TabIndex = 8;
			this.chkSmallMasteryBadgeIcons.Text = "Use small mastery badge icons ";
			// 
			// ddFontSize
			// 
			this.ddFontSize.Image = null;
			this.ddFontSize.Location = new System.Drawing.Point(129, 199);
			this.ddFontSize.Name = "ddFontSize";
			this.ddFontSize.Size = new System.Drawing.Size(86, 23);
			this.ddFontSize.TabIndex = 5;
			this.ddFontSize.Click += new System.EventHandler(this.ddFontSize_Click);
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(41, 199);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(81, 23);
			this.badLabel1.TabIndex = 4;
			this.badLabel1.Text = "Grid font size";
			this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// chkBattleTotalsPosition
			// 
			this.chkBattleTotalsPosition.BackColor = System.Drawing.Color.Transparent;
			this.chkBattleTotalsPosition.Checked = false;
			this.chkBattleTotalsPosition.Image = ((System.Drawing.Image)(resources.GetObject("chkBattleTotalsPosition.Image")));
			this.chkBattleTotalsPosition.Location = new System.Drawing.Point(41, 232);
			this.chkBattleTotalsPosition.Name = "chkBattleTotalsPosition";
			this.chkBattleTotalsPosition.Size = new System.Drawing.Size(268, 23);
			this.chkBattleTotalsPosition.TabIndex = 3;
			this.chkBattleTotalsPosition.Text = "Show battle average and totals at grid top";
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(22, 176);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(308, 123);
			this.badGroupBox1.TabIndex = 2;
			this.badGroupBox1.Text = "Grid Settings";
			// 
			// btnCancel
			// 
			this.btnCancel.BlackButton = false;
			this.btnCancel.Checked = false;
			this.btnCancel.Image = null;
			this.btnCancel.Location = new System.Drawing.Point(260, 318);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(70, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Close";
			this.btnCancel.ToolTipText = "";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.BlackButton = false;
			this.btnSave.Checked = false;
			this.btnSave.Image = null;
			this.btnSave.Location = new System.Drawing.Point(184, 318);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(70, 23);
			this.btnSave.TabIndex = 0;
			this.btnSave.Text = "Save";
			this.btnSave.ToolTipText = "";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// ApplicationLayout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(353, 360);
			this.Controls.Add(this.badForm1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ApplicationLayout";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ApplicationLayout";
			this.Load += new System.EventHandler(this.ApplicationLayout_Load);
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
		private BadCheckBox chkSmallMasteryBadgeIcons;
		private BadCheckBox chkNotifyIconFormExitToMinimize;
		private BadCheckBox chkNotifyIconUse;
		private BadGroupBox badGroupBox2;
		private BadLabel badLabel2;
	}
}