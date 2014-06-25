namespace WinApp.Forms
{
	partial class GrindingParameter
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GrindingParameter));
			this.GrindingParameterTheme = new BadForm();
			this.chkAutoLoad = new BadCheckBox();
			this.ddEveryBattle = new BadDropDownBox();
			this.badLabel2 = new BadLabel();
			this.ddFirstBattle = new BadDropDownBox();
			this.badLabel1 = new BadLabel();
			this.cmdCancel = new BadButton();
			this.btnSave = new BadButton();
			this.badGroupBox1 = new BadGroupBox();
			this.GrindingParameterTheme.SuspendLayout();
			this.SuspendLayout();
			// 
			// GrindingParameterTheme
			// 
			this.GrindingParameterTheme.Controls.Add(this.chkAutoLoad);
			this.GrindingParameterTheme.Controls.Add(this.ddEveryBattle);
			this.GrindingParameterTheme.Controls.Add(this.badLabel2);
			this.GrindingParameterTheme.Controls.Add(this.ddFirstBattle);
			this.GrindingParameterTheme.Controls.Add(this.badLabel1);
			this.GrindingParameterTheme.Controls.Add(this.cmdCancel);
			this.GrindingParameterTheme.Controls.Add(this.btnSave);
			this.GrindingParameterTheme.Controls.Add(this.badGroupBox1);
			this.GrindingParameterTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GrindingParameterTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.GrindingParameterTheme.FormFooter = false;
			this.GrindingParameterTheme.FormFooterHeight = 26;
			this.GrindingParameterTheme.FormInnerBorder = 3;
			this.GrindingParameterTheme.FormMargin = 0;
			this.GrindingParameterTheme.Image = null;
			this.GrindingParameterTheme.Location = new System.Drawing.Point(0, 0);
			this.GrindingParameterTheme.MainArea = mainAreaClass1;
			this.GrindingParameterTheme.Name = "GrindingParameterTheme";
			this.GrindingParameterTheme.Resizable = false;
			this.GrindingParameterTheme.Size = new System.Drawing.Size(316, 247);
			this.GrindingParameterTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("GrindingParameterTheme.SystemExitImage")));
			this.GrindingParameterTheme.SystemMaximizeImage = null;
			this.GrindingParameterTheme.SystemMinimizeImage = null;
			this.GrindingParameterTheme.TabIndex = 0;
			this.GrindingParameterTheme.Text = "Grinding Parameters";
			this.GrindingParameterTheme.TitleHeight = 26;
			// 
			// chkAutoLoad
			// 
			this.chkAutoLoad.BackColor = System.Drawing.Color.Transparent;
			this.chkAutoLoad.Checked = false;
			this.chkAutoLoad.Image = ((System.Drawing.Image)(resources.GetObject("chkAutoLoad.Image")));
			this.chkAutoLoad.Location = new System.Drawing.Point(45, 147);
			this.chkAutoLoad.Name = "chkAutoLoad";
			this.chkAutoLoad.Size = new System.Drawing.Size(176, 23);
			this.chkAutoLoad.TabIndex = 6;
			this.chkAutoLoad.Text = "Display this form on startup";
			// 
			// ddEveryBattle
			// 
			this.ddEveryBattle.Image = null;
			this.ddEveryBattle.Location = new System.Drawing.Point(214, 105);
			this.ddEveryBattle.Name = "ddEveryBattle";
			this.ddEveryBattle.Size = new System.Drawing.Size(58, 23);
			this.ddEveryBattle.TabIndex = 5;
			this.ddEveryBattle.Text = "None";
			this.ddEveryBattle.Click += new System.EventHandler(this.ddEveryBattle_Click);
			// 
			// badLabel2
			// 
			this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel2.Dimmed = false;
			this.badLabel2.Image = null;
			this.badLabel2.Location = new System.Drawing.Point(45, 105);
			this.badLabel2.Name = "badLabel2";
			this.badLabel2.Size = new System.Drawing.Size(126, 23);
			this.badLabel2.TabIndex = 4;
			this.badLabel2.TabStop = false;
			this.badLabel2.Text = "Bonus per victory:";
			// 
			// ddFirstBattle
			// 
			this.ddFirstBattle.Image = null;
			this.ddFirstBattle.Location = new System.Drawing.Point(214, 76);
			this.ddFirstBattle.Name = "ddFirstBattle";
			this.ddFirstBattle.Size = new System.Drawing.Size(58, 23);
			this.ddFirstBattle.TabIndex = 3;
			this.ddFirstBattle.Text = "2X";
			this.ddFirstBattle.Click += new System.EventHandler(this.ddFirstBattle_Click);
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(45, 76);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(163, 23);
			this.badLabel1.TabIndex = 2;
			this.badLabel1.TabStop = false;
			this.badLabel1.Text = "Bonus on first victory every day:";
			// 
			// cmdCancel
			// 
			this.cmdCancel.Image = null;
			this.cmdCancel.Location = new System.Drawing.Point(214, 201);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 8;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Image = null;
			this.btnSave.Location = new System.Drawing.Point(133, 201);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 7;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(25, 48);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(264, 137);
			this.badGroupBox1.TabIndex = 1;
			this.badGroupBox1.TabStop = false;
			this.badGroupBox1.Text = "Parameters";
			// 
			// GrindingParameter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(316, 247);
			this.Controls.Add(this.GrindingParameterTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "GrindingParameter";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "GrindingParameter";
			this.Load += new System.EventHandler(this.GrindingParameter_Load);
			this.GrindingParameterTheme.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm GrindingParameterTheme;
		private BadButton cmdCancel;
		private BadButton btnSave;
		private BadGroupBox badGroupBox1;
		private BadCheckBox chkAutoLoad;
		private BadDropDownBox ddEveryBattle;
		private BadLabel badLabel2;
		private BadDropDownBox ddFirstBattle;
		private BadLabel badLabel1;
	}
}