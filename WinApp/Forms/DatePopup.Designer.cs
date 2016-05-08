namespace WinApp.Forms
{
	partial class DatePopup
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
            BadThemeContainerControl.MainAreaClass mainAreaClass2 = new BadThemeContainerControl.MainAreaClass();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatePopup));
            this.DatePopupTheme = new BadForm();
            this.calendar = new BadMonthCalendar();
            this.btnCancel = new BadButton();
            this.btnOK = new BadButton();
            this.DatePopupTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // DatePopupTheme
            // 
            this.DatePopupTheme.Controls.Add(this.calendar);
            this.DatePopupTheme.Controls.Add(this.btnCancel);
            this.DatePopupTheme.Controls.Add(this.btnOK);
            this.DatePopupTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DatePopupTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DatePopupTheme.FormExitAsMinimize = false;
            this.DatePopupTheme.FormFooter = false;
            this.DatePopupTheme.FormFooterHeight = 26;
            this.DatePopupTheme.FormInnerBorder = 3;
            this.DatePopupTheme.FormMargin = 0;
            this.DatePopupTheme.Image = null;
            this.DatePopupTheme.Location = new System.Drawing.Point(0, 0);
            this.DatePopupTheme.MainArea = mainAreaClass2;
            this.DatePopupTheme.Name = "DatePopupTheme";
            this.DatePopupTheme.Resizable = false;
            this.DatePopupTheme.Size = new System.Drawing.Size(237, 241);
            this.DatePopupTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("DatePopupTheme.SystemExitImage")));
            this.DatePopupTheme.SystemMaximizeImage = null;
            this.DatePopupTheme.SystemMinimizeImage = null;
            this.DatePopupTheme.TabIndex = 0;
            this.DatePopupTheme.Text = "Select Date";
            this.DatePopupTheme.TitleHeight = 26;
            // 
            // calendar
            // 
            this.calendar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.calendar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
            this.calendar.Location = new System.Drawing.Point(18, 34);
            this.calendar.MaxSelectionCount = 1;
            this.calendar.Name = "calendar";
            this.calendar.ShowToday = false;
            this.calendar.ShowTodayCircle = false;
            this.calendar.TabIndex = 13;
            this.calendar.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.calendar.TitleForeColor = System.Drawing.Color.Black;
            this.calendar.TrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(89)))));
            this.calendar.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BlackButton = false;
            this.btnCancel.Checked = false;
            this.btnCancel.Image = null;
            this.btnCancel.Location = new System.Drawing.Point(122, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 23);
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
            this.btnOK.Location = new System.Drawing.Point(18, 200);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(95, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.ToolTipText = "";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // DatePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 241);
            this.Controls.Add(this.DatePopupTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DatePopup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BattleTimeFilterCustom";
            this.Load += new System.EventHandler(this.DatePopup_Load);
            this.Shown += new System.EventHandler(this.DatePopup_Shown);
            this.DatePopupTheme.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private BadForm DatePopupTheme;
		private BadButton btnCancel;
        private BadButton btnOK;
        private BadMonthCalendar calendar;
	}
}