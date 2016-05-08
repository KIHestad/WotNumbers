namespace WinApp.Forms
{
	partial class BattleTimeFilterCustom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleTimeFilterCustom));
            this.BattleTimeFilterCustomTheme = new BadForm();
            this.btnQuickSet1 = new BadButton();
            this.btnQuickSet2 = new BadButton();
            this.chkUseTo = new BadCheckBox();
            this.calendarFrom = new BadMonthCalendar();
            this.calendarTo = new BadMonthCalendar();
            this.txtTimeTo = new BadTextBox();
            this.lblTimeTo = new BadLabel();
            this.txtTimeFrom = new BadTextBox();
            this.lblTimeFrom = new BadLabel();
            this.chkUseFrom = new BadCheckBox();
            this.btnCancel = new BadButton();
            this.btnOK = new BadButton();
            this.badGroupBox1 = new BadGroupBox();
            this.BattleTimeFilterCustomTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // BattleTimeFilterCustomTheme
            // 
            this.BattleTimeFilterCustomTheme.Controls.Add(this.btnQuickSet1);
            this.BattleTimeFilterCustomTheme.Controls.Add(this.btnQuickSet2);
            this.BattleTimeFilterCustomTheme.Controls.Add(this.chkUseTo);
            this.BattleTimeFilterCustomTheme.Controls.Add(this.calendarFrom);
            this.BattleTimeFilterCustomTheme.Controls.Add(this.calendarTo);
            this.BattleTimeFilterCustomTheme.Controls.Add(this.txtTimeTo);
            this.BattleTimeFilterCustomTheme.Controls.Add(this.lblTimeTo);
            this.BattleTimeFilterCustomTheme.Controls.Add(this.txtTimeFrom);
            this.BattleTimeFilterCustomTheme.Controls.Add(this.lblTimeFrom);
            this.BattleTimeFilterCustomTheme.Controls.Add(this.chkUseFrom);
            this.BattleTimeFilterCustomTheme.Controls.Add(this.btnCancel);
            this.BattleTimeFilterCustomTheme.Controls.Add(this.btnOK);
            this.BattleTimeFilterCustomTheme.Controls.Add(this.badGroupBox1);
            this.BattleTimeFilterCustomTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BattleTimeFilterCustomTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BattleTimeFilterCustomTheme.FormExitAsMinimize = false;
            this.BattleTimeFilterCustomTheme.FormFooter = false;
            this.BattleTimeFilterCustomTheme.FormFooterHeight = 26;
            this.BattleTimeFilterCustomTheme.FormInnerBorder = 3;
            this.BattleTimeFilterCustomTheme.FormMargin = 0;
            this.BattleTimeFilterCustomTheme.Image = null;
            this.BattleTimeFilterCustomTheme.Location = new System.Drawing.Point(0, 0);
            this.BattleTimeFilterCustomTheme.MainArea = mainAreaClass1;
            this.BattleTimeFilterCustomTheme.Name = "BattleTimeFilterCustomTheme";
            this.BattleTimeFilterCustomTheme.Resizable = false;
            this.BattleTimeFilterCustomTheme.Size = new System.Drawing.Size(499, 324);
            this.BattleTimeFilterCustomTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("BattleTimeFilterCustomTheme.SystemExitImage")));
            this.BattleTimeFilterCustomTheme.SystemMaximizeImage = null;
            this.BattleTimeFilterCustomTheme.SystemMinimizeImage = null;
            this.BattleTimeFilterCustomTheme.TabIndex = 0;
            this.BattleTimeFilterCustomTheme.Text = "Custom Battle Time Filter";
            this.BattleTimeFilterCustomTheme.TitleHeight = 26;
            // 
            // btnQuickSet1
            // 
            this.btnQuickSet1.BlackButton = false;
            this.btnQuickSet1.Checked = false;
            this.btnQuickSet1.Image = null;
            this.btnQuickSet1.Location = new System.Drawing.Point(22, 279);
            this.btnQuickSet1.Name = "btnQuickSet1";
            this.btnQuickSet1.Size = new System.Drawing.Size(75, 23);
            this.btnQuickSet1.TabIndex = 19;
            this.btnQuickSet1.Text = "Today";
            this.btnQuickSet1.ToolTipText = "Show battles for Today";
            this.btnQuickSet1.Click += new System.EventHandler(this.btnQuickSet1_Click);
            // 
            // btnQuickSet2
            // 
            this.btnQuickSet2.BlackButton = false;
            this.btnQuickSet2.Checked = false;
            this.btnQuickSet2.Image = null;
            this.btnQuickSet2.Location = new System.Drawing.Point(103, 279);
            this.btnQuickSet2.Name = "btnQuickSet2";
            this.btnQuickSet2.Size = new System.Drawing.Size(75, 23);
            this.btnQuickSet2.TabIndex = 18;
            this.btnQuickSet2.Text = "From now";
            this.btnQuickSet2.ToolTipText = "Show battle from current time";
            this.btnQuickSet2.Click += new System.EventHandler(this.btnQuickSet2_Click);
            // 
            // chkUseTo
            // 
            this.chkUseTo.BackColor = System.Drawing.Color.Transparent;
            this.chkUseTo.Checked = true;
            this.chkUseTo.Image = ((System.Drawing.Image)(resources.GetObject("chkUseTo.Image")));
            this.chkUseTo.Location = new System.Drawing.Point(259, 224);
            this.chkUseTo.Name = "chkUseTo";
            this.chkUseTo.Size = new System.Drawing.Size(72, 23);
            this.chkUseTo.TabIndex = 15;
            this.chkUseTo.Text = "Use";
            this.chkUseTo.Click += new System.EventHandler(this.chkUseTo_Click);
            // 
            // calendarFrom
            // 
            this.calendarFrom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.calendarFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
            this.calendarFrom.Location = new System.Drawing.Point(42, 61);
            this.calendarFrom.MaxSelectionCount = 1;
            this.calendarFrom.Name = "calendarFrom";
            this.calendarFrom.ShowToday = false;
            this.calendarFrom.ShowTodayCircle = false;
            this.calendarFrom.TabIndex = 13;
            this.calendarFrom.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.calendarFrom.TitleForeColor = System.Drawing.Color.Black;
            this.calendarFrom.TrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(89)))));
            this.calendarFrom.Visible = false;
            this.calendarFrom.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.calendarFrom_DateChanged);
            // 
            // calendarTo
            // 
            this.calendarTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.calendarTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
            this.calendarTo.Location = new System.Drawing.Point(259, 61);
            this.calendarTo.MaxSelectionCount = 1;
            this.calendarTo.Name = "calendarTo";
            this.calendarTo.ShowToday = false;
            this.calendarTo.ShowTodayCircle = false;
            this.calendarTo.TabIndex = 12;
            this.calendarTo.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.calendarTo.TitleForeColor = System.Drawing.Color.Black;
            this.calendarTo.TrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(89)))));
            this.calendarTo.Visible = false;
            this.calendarTo.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.calendarTo_DateChanged);
            // 
            // txtTimeTo
            // 
            this.txtTimeTo.HasFocus = false;
            this.txtTimeTo.Image = null;
            this.txtTimeTo.Location = new System.Drawing.Point(412, 224);
            this.txtTimeTo.MultilineAllow = false;
            this.txtTimeTo.Name = "txtTimeTo";
            this.txtTimeTo.PasswordChar = '\0';
            this.txtTimeTo.ReadOnly = false;
            this.txtTimeTo.Size = new System.Drawing.Size(40, 23);
            this.txtTimeTo.TabIndex = 10;
            this.txtTimeTo.Text = "07:00";
            this.txtTimeTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTimeTo.ToolTipText = "";
            this.txtTimeTo.Click += new System.EventHandler(this.txtTimeTo_Click);
            // 
            // lblTimeTo
            // 
            this.lblTimeTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblTimeTo.Dimmed = false;
            this.lblTimeTo.Image = null;
            this.lblTimeTo.Location = new System.Drawing.Point(375, 224);
            this.lblTimeTo.Name = "lblTimeTo";
            this.lblTimeTo.Size = new System.Drawing.Size(41, 23);
            this.lblTimeTo.TabIndex = 9;
            this.lblTimeTo.Text = "Time:";
            this.lblTimeTo.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // txtTimeFrom
            // 
            this.txtTimeFrom.HasFocus = false;
            this.txtTimeFrom.Image = null;
            this.txtTimeFrom.Location = new System.Drawing.Point(201, 224);
            this.txtTimeFrom.MultilineAllow = false;
            this.txtTimeFrom.Name = "txtTimeFrom";
            this.txtTimeFrom.PasswordChar = '\0';
            this.txtTimeFrom.ReadOnly = false;
            this.txtTimeFrom.Size = new System.Drawing.Size(40, 23);
            this.txtTimeFrom.TabIndex = 8;
            this.txtTimeFrom.Text = "07:00";
            this.txtTimeFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTimeFrom.ToolTipText = "";
            this.txtTimeFrom.Click += new System.EventHandler(this.txtTimeFrom_Click);
            // 
            // lblTimeFrom
            // 
            this.lblTimeFrom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblTimeFrom.Dimmed = false;
            this.lblTimeFrom.Image = null;
            this.lblTimeFrom.Location = new System.Drawing.Point(164, 224);
            this.lblTimeFrom.Name = "lblTimeFrom";
            this.lblTimeFrom.Size = new System.Drawing.Size(36, 23);
            this.lblTimeFrom.TabIndex = 7;
            this.lblTimeFrom.Text = "Time:";
            this.lblTimeFrom.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // chkUseFrom
            // 
            this.chkUseFrom.BackColor = System.Drawing.Color.Transparent;
            this.chkUseFrom.Checked = true;
            this.chkUseFrom.Image = ((System.Drawing.Image)(resources.GetObject("chkUseFrom.Image")));
            this.chkUseFrom.Location = new System.Drawing.Point(42, 224);
            this.chkUseFrom.Name = "chkUseFrom";
            this.chkUseFrom.Size = new System.Drawing.Size(72, 23);
            this.chkUseFrom.TabIndex = 3;
            this.chkUseFrom.Text = "Use";
            this.chkUseFrom.Click += new System.EventHandler(this.chkUseFrom_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BlackButton = false;
            this.btnCancel.Checked = false;
            this.btnCancel.Image = null;
            this.btnCancel.Location = new System.Drawing.Point(402, 279);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTipText = "";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BlackButton = false;
            this.btnOK.Checked = false;
            this.btnOK.Image = null;
            this.btnOK.Location = new System.Drawing.Point(321, 279);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.ToolTipText = "";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(22, 42);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(455, 221);
            this.badGroupBox1.TabIndex = 0;
            this.badGroupBox1.Text = "Date and Time Selection: From - To";
            // 
            // BattleTimeFilterCustom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 324);
            this.Controls.Add(this.BattleTimeFilterCustomTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BattleTimeFilterCustom";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BattleTimeFilterCustom";
            this.Load += new System.EventHandler(this.BattleTimeFilterCustom_Load);
            this.Shown += new System.EventHandler(this.BattleTimeFilterCustom_Shown);
            this.BattleTimeFilterCustomTheme.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private BadForm BattleTimeFilterCustomTheme;
		private BadCheckBox chkUseFrom;
		private BadButton btnCancel;
		private BadButton btnOK;
		private BadGroupBox badGroupBox1;
		private BadTextBox txtTimeTo;
		private BadLabel lblTimeTo;
		private BadTextBox txtTimeFrom;
		private BadLabel lblTimeFrom;
		private BadMonthCalendar calendarTo;
		private BadMonthCalendar calendarFrom;
		private BadCheckBox chkUseTo;
		private BadButton btnQuickSet2;
		private BadButton btnQuickSet1;
	}
}