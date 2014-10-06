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
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.picMB = new System.Windows.Forms.PictureBox();
			this.lblTankName = new System.Windows.Forms.Label();
			this.lblDuration = new System.Windows.Forms.Label();
			this.lblTime = new System.Windows.Forms.Label();
			this.lblDate = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lblResult = new System.Windows.Forms.Label();
			this.lblMap = new System.Windows.Forms.Label();
			this.lblSurvival = new System.Windows.Forms.Label();
			this.lblBattleMode = new System.Windows.Forms.Label();
			this.picTank = new System.Windows.Forms.PictureBox();
			this.btnEnemyTeam = new BadButton();
			this.btnOurTeam = new BadButton();
			this.btnTeams = new BadButton();
			this.btnPersonal = new BadButton();
			this.badGroupBox1 = new BadGroupBox();
			this.BattleDetailTheme.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picMB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picTank)).BeginInit();
			this.SuspendLayout();
			// 
			// BattleDetailTheme
			// 
			this.BattleDetailTheme.Controls.Add(this.panel1);
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
			this.BattleDetailTheme.Size = new System.Drawing.Size(678, 521);
			this.BattleDetailTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("BattleDetailTheme.SystemExitImage")));
			this.BattleDetailTheme.SystemMaximizeImage = null;
			this.BattleDetailTheme.SystemMinimizeImage = null;
			this.BattleDetailTheme.TabIndex = 0;
			this.BattleDetailTheme.Text = "Battle Details";
			this.BattleDetailTheme.TitleHeight = 26;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Location = new System.Drawing.Point(26, 64);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(626, 426);
			this.panel1.TabIndex = 6;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
			this.panel2.Controls.Add(this.picMB);
			this.panel2.Controls.Add(this.lblTankName);
			this.panel2.Controls.Add(this.lblDuration);
			this.panel2.Controls.Add(this.lblTime);
			this.panel2.Controls.Add(this.lblDate);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.lblResult);
			this.panel2.Controls.Add(this.lblMap);
			this.panel2.Controls.Add(this.lblSurvival);
			this.panel2.Controls.Add(this.lblBattleMode);
			this.panel2.Controls.Add(this.picTank);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(626, 100);
			this.panel2.TabIndex = 6;
			// 
			// picMB
			// 
			this.picMB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.picMB.Location = new System.Drawing.Point(342, 15);
			this.picMB.Name = "picMB";
			this.picMB.Size = new System.Drawing.Size(67, 71);
			this.picMB.TabIndex = 14;
			this.picMB.TabStop = false;
			// 
			// lblTankName
			// 
			this.lblTankName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTankName.BackColor = System.Drawing.Color.Transparent;
			this.lblTankName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(187)))));
			this.lblTankName.Location = new System.Drawing.Point(407, 11);
			this.lblTankName.Name = "lblTankName";
			this.lblTankName.Size = new System.Drawing.Size(98, 24);
			this.lblTankName.TabIndex = 7;
			this.lblTankName.Text = "Tank Name";
			this.lblTankName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblDuration
			// 
			this.lblDuration.AutoSize = true;
			this.lblDuration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(187)))));
			this.lblDuration.Location = new System.Drawing.Point(229, 76);
			this.lblDuration.Name = "lblDuration";
			this.lblDuration.Size = new System.Drawing.Size(42, 13);
			this.lblDuration.TabIndex = 13;
			this.lblDuration.Text = "MM:SS";
			this.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTime
			// 
			this.lblTime.AutoSize = true;
			this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(187)))));
			this.lblTime.Location = new System.Drawing.Point(229, 58);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(44, 13);
			this.lblTime.TabIndex = 12;
			this.lblTime.Text = "HH:MM";
			this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblDate
			// 
			this.lblDate.AutoSize = true;
			this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(187)))));
			this.lblDate.Location = new System.Drawing.Point(229, 40);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(75, 13);
			this.lblDate.TabIndex = 11;
			this.lblDate.Text = "DD.MM.YYYY";
			this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(187)))));
			this.label3.Location = new System.Drawing.Point(178, 76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(50, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Duration:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(187)))));
			this.label2.Location = new System.Drawing.Point(178, 58);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Time:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(187)))));
			this.label1.Location = new System.Drawing.Point(178, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Date:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblResult
			// 
			this.lblResult.AutoSize = true;
			this.lblResult.BackColor = System.Drawing.Color.Transparent;
			this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblResult.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.lblResult.Location = new System.Drawing.Point(12, 11);
			this.lblResult.Name = "lblResult";
			this.lblResult.Size = new System.Drawing.Size(112, 24);
			this.lblResult.TabIndex = 2;
			this.lblResult.Text = "Battle Result";
			this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMap
			// 
			this.lblMap.AutoSize = true;
			this.lblMap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(187)))));
			this.lblMap.Location = new System.Drawing.Point(13, 76);
			this.lblMap.Name = "lblMap";
			this.lblMap.Size = new System.Drawing.Size(57, 13);
			this.lblMap.TabIndex = 6;
			this.lblMap.Text = "Map name";
			this.lblMap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSurvival
			// 
			this.lblSurvival.AutoSize = true;
			this.lblSurvival.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(187)))));
			this.lblSurvival.Location = new System.Drawing.Point(13, 40);
			this.lblSurvival.Name = "lblSurvival";
			this.lblSurvival.Size = new System.Drawing.Size(119, 13);
			this.lblSurvival.TabIndex = 4;
			this.lblSurvival.Text = "Survival / Extermination";
			this.lblSurvival.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblBattleMode
			// 
			this.lblBattleMode.AutoSize = true;
			this.lblBattleMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(187)))));
			this.lblBattleMode.Location = new System.Drawing.Point(13, 58);
			this.lblBattleMode.Name = "lblBattleMode";
			this.lblBattleMode.Size = new System.Drawing.Size(64, 13);
			this.lblBattleMode.TabIndex = 5;
			this.lblBattleMode.Text = "Battle Mode";
			this.lblBattleMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// picTank
			// 
			this.picTank.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.picTank.Location = new System.Drawing.Point(456, 0);
			this.picTank.Name = "picTank";
			this.picTank.Size = new System.Drawing.Size(170, 100);
			this.picTank.TabIndex = 1;
			this.picTank.TabStop = false;
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
			this.badGroupBox1.Size = new System.Drawing.Size(628, 436);
			this.badGroupBox1.TabIndex = 0;
			// 
			// BattleDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Fuchsia;
			this.ClientSize = new System.Drawing.Size(678, 521);
			this.Controls.Add(this.BattleDetailTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MinimumSize = new System.Drawing.Size(678, 300);
			this.Name = "BattleDetail";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "BattleDetail";
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.Load += new System.EventHandler(this.BattleDetail_Load);
			this.ResizeEnd += new System.EventHandler(this.BattleDetail_Resize);
			this.Resize += new System.EventHandler(this.BattleDetail_Resize);
			this.BattleDetailTheme.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picMB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picTank)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm BattleDetailTheme;
		private BadButton btnEnemyTeam;
		private BadButton btnOurTeam;
		private BadButton btnTeams;
		private BadButton btnPersonal;
		private BadGroupBox badGroupBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox picTank;
		private System.Windows.Forms.Label lblResult;
		private System.Windows.Forms.Label lblSurvival;
		private System.Windows.Forms.Label lblBattleMode;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label lblMap;
		private System.Windows.Forms.Label lblTankName;
		private System.Windows.Forms.Label lblDuration;
		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.Label lblDate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox picMB;
	}
}