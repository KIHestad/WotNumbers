namespace WinApp.Forms
{
	partial class ManualCheckNewBattles
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualCheckNewBattles));
			this.ManualCheckNewBattlesTheme = new BadForm();
			this.btnCancel = new BadButton();
			this.chkRun = new BadButton();
			this.badLabel1 = new BadLabel();
			this.chkForceUpdateAll = new BadCheckBox();
			this.badGroupBox1 = new BadGroupBox();
			this.ManualCheckNewBattlesTheme.SuspendLayout();
			this.SuspendLayout();
			// 
			// ManualCheckNewBattlesTheme
			// 
			this.ManualCheckNewBattlesTheme.Controls.Add(this.btnCancel);
			this.ManualCheckNewBattlesTheme.Controls.Add(this.chkRun);
			this.ManualCheckNewBattlesTheme.Controls.Add(this.badLabel1);
			this.ManualCheckNewBattlesTheme.Controls.Add(this.chkForceUpdateAll);
			this.ManualCheckNewBattlesTheme.Controls.Add(this.badGroupBox1);
			this.ManualCheckNewBattlesTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ManualCheckNewBattlesTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.ManualCheckNewBattlesTheme.FormExitAsMinimize = false;
			this.ManualCheckNewBattlesTheme.FormFooter = false;
			this.ManualCheckNewBattlesTheme.FormFooterHeight = 26;
			this.ManualCheckNewBattlesTheme.FormInnerBorder = 3;
			this.ManualCheckNewBattlesTheme.FormMargin = 0;
			this.ManualCheckNewBattlesTheme.Image = null;
			this.ManualCheckNewBattlesTheme.Location = new System.Drawing.Point(0, 0);
			this.ManualCheckNewBattlesTheme.MainArea = mainAreaClass1;
			this.ManualCheckNewBattlesTheme.Name = "ManualCheckNewBattlesTheme";
			this.ManualCheckNewBattlesTheme.Resizable = false;
			this.ManualCheckNewBattlesTheme.Size = new System.Drawing.Size(308, 211);
			this.ManualCheckNewBattlesTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("ManualCheckNewBattlesTheme.SystemExitImage")));
			this.ManualCheckNewBattlesTheme.SystemMaximizeImage = null;
			this.ManualCheckNewBattlesTheme.SystemMinimizeImage = null;
			this.ManualCheckNewBattlesTheme.TabIndex = 0;
			this.ManualCheckNewBattlesTheme.Text = "Run Battle Check";
			this.ManualCheckNewBattlesTheme.TitleHeight = 26;
			// 
			// btnCancel
			// 
			this.btnCancel.BlackButton = false;
			this.btnCancel.Checked = false;
			this.btnCancel.Image = null;
			this.btnCancel.Location = new System.Drawing.Point(207, 169);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.ToolTipText = "";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// chkRun
			// 
			this.chkRun.BlackButton = false;
			this.chkRun.Checked = false;
			this.chkRun.Image = null;
			this.chkRun.Location = new System.Drawing.Point(126, 169);
			this.chkRun.Name = "chkRun";
			this.chkRun.Size = new System.Drawing.Size(75, 23);
			this.chkRun.TabIndex = 3;
			this.chkRun.Text = "Run";
			this.chkRun.ToolTipText = "";
			this.chkRun.Click += new System.EventHandler(this.chkRun_Click);
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(41, 72);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(229, 39);
			this.badLabel1.TabIndex = 2;
			this.badLabel1.Text = "Updates battle statistics for all tanks, also for tanks where no new battles are " +
    "found.";
			this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// chkForceUpdateAll
			// 
			this.chkForceUpdateAll.BackColor = System.Drawing.Color.Transparent;
			this.chkForceUpdateAll.Checked = false;
			this.chkForceUpdateAll.Image = ((System.Drawing.Image)(resources.GetObject("chkForceUpdateAll.Image")));
			this.chkForceUpdateAll.Location = new System.Drawing.Point(41, 117);
			this.chkForceUpdateAll.Name = "chkForceUpdateAll";
			this.chkForceUpdateAll.Size = new System.Drawing.Size(166, 23);
			this.chkForceUpdateAll.TabIndex = 1;
			this.chkForceUpdateAll.Text = "Force Update All Data";
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(25, 45);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(257, 106);
			this.badGroupBox1.TabIndex = 0;
			this.badGroupBox1.Text = "Optional Selection";
			// 
			// ManualCheckNewBattles
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(308, 211);
			this.Controls.Add(this.ManualCheckNewBattlesTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ManualCheckNewBattles";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ManualCheckNewBattles";
			this.ManualCheckNewBattlesTheme.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm ManualCheckNewBattlesTheme;
		private BadCheckBox chkForceUpdateAll;
		private BadGroupBox badGroupBox1;
		private BadButton btnCancel;
		private BadButton chkRun;
		private BadLabel badLabel1;
	}
}