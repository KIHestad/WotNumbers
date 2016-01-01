namespace WinApp.Gadget
{
	partial class paramTotalStats
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
            this.badForm1 = new BadForm();
            this.btnDefault = new BadButton();
            this.badGroupBox3 = new BadGroupBox();
            this.badGroupBox2 = new BadGroupBox();
            this.ddGridCount = new BadDropDownBox();
            this.badLabel3 = new BadLabel();
            this.ddTimeSpan = new BadDropDownBox();
            this.badLabel2 = new BadLabel();
            this.btnCancel = new BadButton();
            this.btnSelect = new BadButton();
            this.ddBattleMode = new BadDropDownBox();
            this.badLabel1 = new BadLabel();
            this.badGroupBox1 = new BadGroupBox();
            this.badForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // badForm1
            // 
            this.badForm1.Controls.Add(this.btnDefault);
            this.badForm1.Controls.Add(this.badGroupBox3);
            this.badForm1.Controls.Add(this.badGroupBox2);
            this.badForm1.Controls.Add(this.ddGridCount);
            this.badForm1.Controls.Add(this.badLabel3);
            this.badForm1.Controls.Add(this.ddTimeSpan);
            this.badForm1.Controls.Add(this.badLabel2);
            this.badForm1.Controls.Add(this.btnCancel);
            this.badForm1.Controls.Add(this.btnSelect);
            this.badForm1.Controls.Add(this.ddBattleMode);
            this.badForm1.Controls.Add(this.badLabel1);
            this.badForm1.Controls.Add(this.badGroupBox1);
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
            this.badForm1.Resizable = true;
            this.badForm1.Size = new System.Drawing.Size(713, 482);
            this.badForm1.SystemExitImage = null;
            this.badForm1.SystemMaximizeImage = null;
            this.badForm1.SystemMinimizeImage = null;
            this.badForm1.TabIndex = 0;
            this.badForm1.Text = "Total Stats Parameters";
            this.badForm1.TitleHeight = 26;
            // 
            // btnDefault
            // 
            this.btnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefault.BlackButton = false;
            this.btnDefault.Checked = false;
            this.btnDefault.Image = null;
            this.btnDefault.Location = new System.Drawing.Point(482, 436);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(64, 23);
            this.btnDefault.TabIndex = 11;
            this.btnDefault.Text = "Default";
            this.btnDefault.ToolTipText = "";
            // 
            // badGroupBox3
            // 
            this.badGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.badGroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox3.Image = null;
            this.badGroupBox3.Location = new System.Drawing.Point(290, 129);
            this.badGroupBox3.Name = "badGroupBox3";
            this.badGroupBox3.Size = new System.Drawing.Size(396, 288);
            this.badGroupBox3.TabIndex = 10;
            this.badGroupBox3.Text = "Selected rows";
            // 
            // badGroupBox2
            // 
            this.badGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox2.Image = null;
            this.badGroupBox2.Location = new System.Drawing.Point(22, 129);
            this.badGroupBox2.Name = "badGroupBox2";
            this.badGroupBox2.Size = new System.Drawing.Size(249, 288);
            this.badGroupBox2.TabIndex = 9;
            this.badGroupBox2.Text = "Available rows";
            // 
            // ddGridCount
            // 
            this.ddGridCount.Image = null;
            this.ddGridCount.Location = new System.Drawing.Point(610, 71);
            this.ddGridCount.Name = "ddGridCount";
            this.ddGridCount.Size = new System.Drawing.Size(52, 23);
            this.ddGridCount.TabIndex = 7;
            this.ddGridCount.Text = "3";
            this.ddGridCount.Click += new System.EventHandler(this.ddGridCount_Click);
            // 
            // badLabel3
            // 
            this.badLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel3.Dimmed = false;
            this.badLabel3.Image = null;
            this.badLabel3.Location = new System.Drawing.Point(553, 71);
            this.badLabel3.Name = "badLabel3";
            this.badLabel3.Size = new System.Drawing.Size(62, 23);
            this.badLabel3.TabIndex = 6;
            this.badLabel3.Text = "Columns:";
            this.badLabel3.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // ddTimeSpan
            // 
            this.ddTimeSpan.Image = null;
            this.ddTimeSpan.Location = new System.Drawing.Point(372, 71);
            this.ddTimeSpan.Name = "ddTimeSpan";
            this.ddTimeSpan.Size = new System.Drawing.Size(134, 23);
            this.ddTimeSpan.TabIndex = 5;
            this.ddTimeSpan.Click += new System.EventHandler(this.ddTimeSpan_Click);
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(304, 71);
            this.badLabel2.Name = "badLabel2";
            this.badLabel2.Size = new System.Drawing.Size(62, 23);
            this.badLabel2.TabIndex = 4;
            this.badLabel2.Text = "Timespan:";
            this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BlackButton = false;
            this.btnCancel.Checked = false;
            this.btnCancel.Image = null;
            this.btnCancel.Location = new System.Drawing.Point(622, 436);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTipText = "";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.BlackButton = false;
            this.btnSelect.Checked = false;
            this.btnSelect.Image = null;
            this.btnSelect.Location = new System.Drawing.Point(552, 436);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(64, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Save";
            this.btnSelect.ToolTipText = "";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // ddBattleMode
            // 
            this.ddBattleMode.Image = null;
            this.ddBattleMode.Location = new System.Drawing.Point(125, 71);
            this.ddBattleMode.Name = "ddBattleMode";
            this.ddBattleMode.Size = new System.Drawing.Size(134, 23);
            this.ddBattleMode.TabIndex = 1;
            this.ddBattleMode.Click += new System.EventHandler(this.ddBattleMode_Click);
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(47, 71);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(76, 23);
            this.badLabel1.TabIndex = 0;
            this.badLabel1.Text = "Battle Mode:";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(22, 45);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(664, 69);
            this.badGroupBox1.TabIndex = 8;
            this.badGroupBox1.Text = "Settings";
            // 
            // paramTotalStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 482);
            this.Controls.Add(this.badForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(713, 482);
            this.Name = "paramTotalStats";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ucGaugeWinRateParameter";
            this.Load += new System.EventHandler(this.paramTotalStats_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.paramBattleMode_Paint);
            this.badForm1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private BadForm badForm1;
		private BadButton btnSelect;
		private BadDropDownBox ddBattleMode;
		private BadLabel badLabel1;
		private BadButton btnCancel;
        private BadDropDownBox ddTimeSpan;
        private BadLabel badLabel2;
        private BadDropDownBox ddGridCount;
        private BadLabel badLabel3;
        private BadButton btnDefault;
        private BadGroupBox badGroupBox3;
        private BadGroupBox badGroupBox2;
        private BadGroupBox badGroupBox1;
	}
}