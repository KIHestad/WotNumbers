namespace WotDBUpdater.Forms.File
{
	partial class DatabaseNew
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseNew));
			this.DatabaseNewTheme = new BadForm();
			this.cmdSelectFIle = new BadButton();
			this.pbCreateDatabase = new System.Windows.Forms.ProgressBar();
			this.btnCreateDB = new BadButton();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.badLabel1 = new BadLabel();
			this.txtDatabasename = new BadTextBox();
			this.badLabel2 = new BadLabel();
			this.txtPlayerName = new BadTextBox();
			this.badLabel3 = new BadLabel();
			this.txtFileLocation = new BadTextBox();
			this.badGroupBox1 = new BadGroupBox();
			this.DatabaseNewTheme.SuspendLayout();
			this.SuspendLayout();
			// 
			// DatabaseNewTheme
			// 
			this.DatabaseNewTheme.Controls.Add(this.cmdSelectFIle);
			this.DatabaseNewTheme.Controls.Add(this.txtFileLocation);
			this.DatabaseNewTheme.Controls.Add(this.badLabel3);
			this.DatabaseNewTheme.Controls.Add(this.txtPlayerName);
			this.DatabaseNewTheme.Controls.Add(this.badLabel2);
			this.DatabaseNewTheme.Controls.Add(this.txtDatabasename);
			this.DatabaseNewTheme.Controls.Add(this.badLabel1);
			this.DatabaseNewTheme.Controls.Add(this.pbCreateDatabase);
			this.DatabaseNewTheme.Controls.Add(this.btnCreateDB);
			this.DatabaseNewTheme.Controls.Add(this.badGroupBox1);
			this.DatabaseNewTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DatabaseNewTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.DatabaseNewTheme.FormFooter = false;
			this.DatabaseNewTheme.FormFooterHeight = 26;
			this.DatabaseNewTheme.FormInnerBorder = 3;
			this.DatabaseNewTheme.FormMargin = 0;
			this.DatabaseNewTheme.Image = null;
			this.DatabaseNewTheme.Location = new System.Drawing.Point(0, 0);
			this.DatabaseNewTheme.MainArea = mainAreaClass1;
			this.DatabaseNewTheme.Name = "DatabaseNewTheme";
			this.DatabaseNewTheme.Resizable = false;
			this.DatabaseNewTheme.Size = new System.Drawing.Size(467, 287);
			this.DatabaseNewTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("DatabaseNewTheme.SystemExitImage")));
			this.DatabaseNewTheme.SystemMaximizeImage = null;
			this.DatabaseNewTheme.SystemMinimizeImage = null;
			this.DatabaseNewTheme.TabIndex = 14;
			this.DatabaseNewTheme.Text = "Create New Database";
			this.DatabaseNewTheme.TitleHeight = 26;
			// 
			// cmdSelectFIle
			// 
			this.cmdSelectFIle.Image = null;
			this.cmdSelectFIle.Location = new System.Drawing.Point(339, 184);
			this.cmdSelectFIle.Name = "cmdSelectFIle";
			this.cmdSelectFIle.Size = new System.Drawing.Size(88, 23);
			this.cmdSelectFIle.TabIndex = 17;
			this.cmdSelectFIle.Text = "Select Path";
			this.cmdSelectFIle.Click += new System.EventHandler(this.cmdSelectFIle_Click);
			// 
			// pbCreateDatabase
			// 
			this.pbCreateDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.pbCreateDatabase.Location = new System.Drawing.Point(21, 235);
			this.pbCreateDatabase.MarqueeAnimationSpeed = 1;
			this.pbCreateDatabase.Name = "pbCreateDatabase";
			this.pbCreateDatabase.Size = new System.Drawing.Size(314, 23);
			this.pbCreateDatabase.Step = 1;
			this.pbCreateDatabase.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pbCreateDatabase.TabIndex = 13;
			this.pbCreateDatabase.UseWaitCursor = true;
			this.pbCreateDatabase.Value = 1;
			this.pbCreateDatabase.Visible = false;
			// 
			// btnCreateDB
			// 
			this.btnCreateDB.Image = null;
			this.btnCreateDB.Location = new System.Drawing.Point(341, 235);
			this.btnCreateDB.Name = "btnCreateDB";
			this.btnCreateDB.Size = new System.Drawing.Size(106, 23);
			this.btnCreateDB.TabIndex = 0;
			this.btnCreateDB.Text = "Create Database";
			this.btnCreateDB.Click += new System.EventHandler(this.btnCreateDB_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog1";
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(35, 62);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(119, 23);
			this.badLabel1.TabIndex = 18;
			this.badLabel1.Text = "Database Name:";
			// 
			// txtDatabasename
			// 
			this.txtDatabasename.Image = null;
			this.txtDatabasename.Location = new System.Drawing.Point(38, 81);
			this.txtDatabasename.Name = "txtDatabasename";
			this.txtDatabasename.PasswordChar = '\0';
			this.txtDatabasename.Size = new System.Drawing.Size(190, 23);
			this.txtDatabasename.TabIndex = 19;
			// 
			// badLabel2
			// 
			this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel2.Dimmed = false;
			this.badLabel2.Image = null;
			this.badLabel2.Location = new System.Drawing.Point(237, 61);
			this.badLabel2.Name = "badLabel2";
			this.badLabel2.Size = new System.Drawing.Size(130, 23);
			this.badLabel2.TabIndex = 20;
			this.badLabel2.Text = "Player Name:";
			// 
			// txtPlayerName
			// 
			this.txtPlayerName.Image = null;
			this.txtPlayerName.Location = new System.Drawing.Point(237, 81);
			this.txtPlayerName.Name = "txtPlayerName";
			this.txtPlayerName.PasswordChar = '\0';
			this.txtPlayerName.Size = new System.Drawing.Size(190, 23);
			this.txtPlayerName.TabIndex = 21;
			// 
			// badLabel3
			// 
			this.badLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel3.Dimmed = false;
			this.badLabel3.Image = null;
			this.badLabel3.Location = new System.Drawing.Point(35, 109);
			this.badLabel3.Name = "badLabel3";
			this.badLabel3.Size = new System.Drawing.Size(130, 23);
			this.badLabel3.TabIndex = 22;
			this.badLabel3.Text = "Database File Location:";
			// 
			// txtFileLocation
			// 
			this.txtFileLocation.Image = null;
			this.txtFileLocation.Location = new System.Drawing.Point(38, 130);
			this.txtFileLocation.Name = "txtFileLocation";
			this.txtFileLocation.PasswordChar = '\0';
			this.txtFileLocation.Size = new System.Drawing.Size(389, 43);
			this.txtFileLocation.TabIndex = 23;
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(21, 42);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(426, 177);
			this.badGroupBox1.TabIndex = 24;
			this.badGroupBox1.Text = "Database parameters";
			// 
			// DatabaseNew
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(467, 287);
			this.Controls.Add(this.DatabaseNewTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DatabaseNew";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Create New Database";
			this.Load += new System.EventHandler(this.frmDatabaseNew_Load);
			this.DatabaseNewTheme.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ProgressBar pbCreateDatabase;
		private BadForm DatabaseNewTheme;
		private BadButton btnCreateDB;
		private BadButton cmdSelectFIle;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private BadTextBox txtFileLocation;
		private BadLabel badLabel3;
		private BadTextBox txtPlayerName;
		private BadLabel badLabel2;
		private BadTextBox txtDatabasename;
		private BadLabel badLabel1;
		private BadGroupBox badGroupBox1;
	}
}