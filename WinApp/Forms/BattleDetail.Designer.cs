namespace WinApp.Forms
{
	partial class BattleDetail
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleDetail));
			this.BattleDetailTheme = new BadForm();
			this.btnEnemyTeam = new BadButton();
			this.btnOurTeam = new BadButton();
			this.btnTeams = new BadButton();
			this.btnPersonal = new BadButton();
			this.badGroupBox1 = new BadGroupBox();
			this.BattleDetailTheme.SuspendLayout();
			this.SuspendLayout();
			// 
			// BattleDetailTheme
			// 
			this.BattleDetailTheme.Controls.Add(this.btnEnemyTeam);
			this.BattleDetailTheme.Controls.Add(this.btnOurTeam);
			this.BattleDetailTheme.Controls.Add(this.btnTeams);
			this.BattleDetailTheme.Controls.Add(this.btnPersonal);
			this.BattleDetailTheme.Controls.Add(this.badGroupBox1);
			this.BattleDetailTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BattleDetailTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.BattleDetailTheme.FormFooter = false;
			this.BattleDetailTheme.FormFooterHeight = 26;
			this.BattleDetailTheme.FormInnerBorder = 3;
			this.BattleDetailTheme.FormMargin = 0;
			this.BattleDetailTheme.Image = null;
			this.BattleDetailTheme.Location = new System.Drawing.Point(0, 0);
			this.BattleDetailTheme.MainArea = mainAreaClass1;
			this.BattleDetailTheme.Name = "BattleDetailTheme";
			this.BattleDetailTheme.Resizable = true;
			this.BattleDetailTheme.Size = new System.Drawing.Size(701, 427);
			this.BattleDetailTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("BattleDetailTheme.SystemExitImage")));
			this.BattleDetailTheme.SystemMaximizeImage = null;
			this.BattleDetailTheme.SystemMinimizeImage = null;
			this.BattleDetailTheme.TabIndex = 0;
			this.BattleDetailTheme.Text = "Battle Details";
			this.BattleDetailTheme.TitleHeight = 26;
			// 
			// btnEnemyTeam
			// 
			this.btnEnemyTeam.BlackButton = false;
			this.btnEnemyTeam.Checked = false;
			this.btnEnemyTeam.Image = null;
			this.btnEnemyTeam.Location = new System.Drawing.Point(367, 40);
			this.btnEnemyTeam.Name = "btnEnemyTeam";
			this.btnEnemyTeam.Size = new System.Drawing.Size(108, 23);
			this.btnEnemyTeam.TabIndex = 5;
			this.btnEnemyTeam.Text = "Enemy Team";
			this.btnEnemyTeam.ToolTipText = "";
			this.btnEnemyTeam.Click += new System.EventHandler(this.btnTab_Click);
			// 
			// btnOurTeam
			// 
			this.btnOurTeam.BlackButton = false;
			this.btnOurTeam.Checked = false;
			this.btnOurTeam.Image = null;
			this.btnOurTeam.Location = new System.Drawing.Point(253, 40);
			this.btnOurTeam.Name = "btnOurTeam";
			this.btnOurTeam.Size = new System.Drawing.Size(108, 23);
			this.btnOurTeam.TabIndex = 4;
			this.btnOurTeam.Text = "Our Team";
			this.btnOurTeam.ToolTipText = "";
			this.btnOurTeam.Click += new System.EventHandler(this.btnTab_Click);
			// 
			// btnTeams
			// 
			this.btnTeams.BlackButton = false;
			this.btnTeams.Checked = false;
			this.btnTeams.Image = null;
			this.btnTeams.Location = new System.Drawing.Point(139, 40);
			this.btnTeams.Name = "btnTeams";
			this.btnTeams.Size = new System.Drawing.Size(108, 23);
			this.btnTeams.TabIndex = 2;
			this.btnTeams.Text = "Teams Overview";
			this.btnTeams.ToolTipText = "";
			this.btnTeams.Click += new System.EventHandler(this.btnTab_Click);
			// 
			// btnPersonal
			// 
			this.btnPersonal.BlackButton = false;
			this.btnPersonal.Checked = true;
			this.btnPersonal.Image = null;
			this.btnPersonal.Location = new System.Drawing.Point(25, 40);
			this.btnPersonal.Name = "btnPersonal";
			this.btnPersonal.Size = new System.Drawing.Size(108, 23);
			this.btnPersonal.TabIndex = 1;
			this.btnPersonal.Text = "My Result";
			this.btnPersonal.ToolTipText = "";
			this.btnPersonal.Click += new System.EventHandler(this.btnTab_Click);
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(25, 56);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(651, 342);
			this.badGroupBox1.TabIndex = 0;
			// 
			// BattleDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Fuchsia;
			this.ClientSize = new System.Drawing.Size(701, 427);
			this.Controls.Add(this.BattleDetailTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MinimumSize = new System.Drawing.Size(600, 300);
			this.Name = "BattleDetail";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "BattleDetail";
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.BattleDetailTheme.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm BattleDetailTheme;
		private BadButton btnEnemyTeam;
		private BadButton btnOurTeam;
		private BadButton btnTeams;
		private BadButton btnPersonal;
		private BadGroupBox badGroupBox1;
	}
}