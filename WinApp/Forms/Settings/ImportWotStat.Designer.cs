namespace WinApp.Forms
{
	partial class ImportWotStat
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportWotStat));
			this.openFileWotStatDbFile = new System.Windows.Forms.OpenFileDialog();
			this.ImportWotStatTheme = new BadForm();
			this.btnClose = new BadButton();
			this.btnRemove = new BadButton();
			this.lblResult = new BadLabel();
			this.txtToDate = new BadTextBox();
			this.badLabel2 = new BadLabel();
			this.progressBarImport = new BadProgressBar();
			this.btnStartImport = new BadButton();
			this.btnOpenWotStatDbFile = new BadButton();
			this.txtWotStatDb = new BadTextBox();
			this.badLabel1 = new BadLabel();
			this.badGroupBox1 = new BadGroupBox();
			this.ImportWotStatTheme.SuspendLayout();
			this.SuspendLayout();
			// 
			// openFileWotStatDbFile
			// 
			this.openFileWotStatDbFile.FileName = "*.db";
			// 
			// ImportWotStatTheme
			// 
			this.ImportWotStatTheme.Controls.Add(this.btnClose);
			this.ImportWotStatTheme.Controls.Add(this.btnRemove);
			this.ImportWotStatTheme.Controls.Add(this.lblResult);
			this.ImportWotStatTheme.Controls.Add(this.txtToDate);
			this.ImportWotStatTheme.Controls.Add(this.badLabel2);
			this.ImportWotStatTheme.Controls.Add(this.progressBarImport);
			this.ImportWotStatTheme.Controls.Add(this.btnStartImport);
			this.ImportWotStatTheme.Controls.Add(this.btnOpenWotStatDbFile);
			this.ImportWotStatTheme.Controls.Add(this.txtWotStatDb);
			this.ImportWotStatTheme.Controls.Add(this.badLabel1);
			this.ImportWotStatTheme.Controls.Add(this.badGroupBox1);
			this.ImportWotStatTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ImportWotStatTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.ImportWotStatTheme.FormFooter = false;
			this.ImportWotStatTheme.FormFooterHeight = 26;
			this.ImportWotStatTheme.FormInnerBorder = 3;
			this.ImportWotStatTheme.FormMargin = 0;
			this.ImportWotStatTheme.Image = null;
			this.ImportWotStatTheme.Location = new System.Drawing.Point(0, 0);
			this.ImportWotStatTheme.MainArea = mainAreaClass1;
			this.ImportWotStatTheme.Name = "ImportWotStatTheme";
			this.ImportWotStatTheme.Resizable = false;
			this.ImportWotStatTheme.Size = new System.Drawing.Size(488, 290);
			this.ImportWotStatTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("ImportWotStatTheme.SystemExitImage")));
			this.ImportWotStatTheme.SystemMaximizeImage = null;
			this.ImportWotStatTheme.SystemMinimizeImage = null;
			this.ImportWotStatTheme.TabIndex = 0;
			this.ImportWotStatTheme.Text = "Import Battles From WoT Statistics";
			this.ImportWotStatTheme.TitleHeight = 26;
			// 
			// btnClose
			// 
			this.btnClose.Image = null;
			this.btnClose.Location = new System.Drawing.Point(387, 239);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 11;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Image = null;
			this.btnRemove.Location = new System.Drawing.Point(306, 239);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(75, 23);
			this.btnRemove.TabIndex = 10;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// lblResult
			// 
			this.lblResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblResult.Dimmed = false;
			this.lblResult.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.lblResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.lblResult.Image = null;
			this.lblResult.Location = new System.Drawing.Point(25, 239);
			this.lblResult.Name = "lblResult";
			this.lblResult.Size = new System.Drawing.Size(181, 23);
			this.lblResult.TabIndex = 8;
			this.lblResult.TabStop = false;
			// 
			// txtToDate
			// 
			this.txtToDate.HasFocus = false;
			this.txtToDate.Image = null;
			this.txtToDate.Location = new System.Drawing.Point(338, 70);
			this.txtToDate.MultilineAllow = false;
			this.txtToDate.Name = "txtToDate";
			this.txtToDate.PasswordChar = '\0';
			this.txtToDate.Size = new System.Drawing.Size(109, 23);
			this.txtToDate.TabIndex = 3;
			this.txtToDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			// 
			// badLabel2
			// 
			this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel2.Dimmed = false;
			this.badLabel2.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.badLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.badLabel2.Image = null;
			this.badLabel2.Location = new System.Drawing.Point(42, 70);
			this.badLabel2.Name = "badLabel2";
			this.badLabel2.Size = new System.Drawing.Size(290, 23);
			this.badLabel2.TabIndex = 2;
			this.badLabel2.TabStop = false;
			this.badLabel2.Text = "Import battles before date (dd.mm.yyyy), blank = import all:";
			// 
			// progressBarImport
			// 
			this.progressBarImport.BackColor = System.Drawing.Color.Transparent;
			this.progressBarImport.Image = null;
			this.progressBarImport.Location = new System.Drawing.Point(25, 208);
			this.progressBarImport.Name = "progressBarImport";
			this.progressBarImport.ProgressBarColorMode = false;
			this.progressBarImport.ProgressBarMargins = 2;
			this.progressBarImport.ProgressBarShowPercentage = false;
			this.progressBarImport.Size = new System.Drawing.Size(437, 23);
			this.progressBarImport.TabIndex = 7;
			this.progressBarImport.TabStop = false;
			this.progressBarImport.Text = "badProgressBar1";
			this.progressBarImport.Value = 0D;
			this.progressBarImport.ValueMax = 100D;
			this.progressBarImport.ValueMin = 0D;
			this.progressBarImport.Visible = false;
			// 
			// btnStartImport
			// 
			this.btnStartImport.Image = null;
			this.btnStartImport.Location = new System.Drawing.Point(212, 239);
			this.btnStartImport.Name = "btnStartImport";
			this.btnStartImport.Size = new System.Drawing.Size(88, 23);
			this.btnStartImport.TabIndex = 9;
			this.btnStartImport.Text = "Start Import";
			this.btnStartImport.Click += new System.EventHandler(this.btnStartImport_Click);
			// 
			// btnOpenWotStatDbFile
			// 
			this.btnOpenWotStatDbFile.Image = null;
			this.btnOpenWotStatDbFile.Location = new System.Drawing.Point(372, 159);
			this.btnOpenWotStatDbFile.Name = "btnOpenWotStatDbFile";
			this.btnOpenWotStatDbFile.Size = new System.Drawing.Size(75, 23);
			this.btnOpenWotStatDbFile.TabIndex = 6;
			this.btnOpenWotStatDbFile.Text = "Select File";
			this.btnOpenWotStatDbFile.Click += new System.EventHandler(this.btnOpenWotStatDbFile_Click);
			// 
			// txtWotStatDb
			// 
			this.txtWotStatDb.HasFocus = false;
			this.txtWotStatDb.Image = null;
			this.txtWotStatDb.Location = new System.Drawing.Point(42, 114);
			this.txtWotStatDb.MultilineAllow = true;
			this.txtWotStatDb.Name = "txtWotStatDb";
			this.txtWotStatDb.PasswordChar = '\0';
			this.txtWotStatDb.Size = new System.Drawing.Size(405, 39);
			this.txtWotStatDb.TabIndex = 5;
			this.txtWotStatDb.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.badLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(42, 94);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(175, 23);
			this.badLabel1.TabIndex = 4;
			this.badLabel1.TabStop = false;
			this.badLabel1.Text = "Wot Statistics Database File:";
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(25, 48);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(437, 144);
			this.badGroupBox1.TabIndex = 1;
			this.badGroupBox1.TabStop = false;
			this.badGroupBox1.Text = "Settings";
			// 
			// ImportWotStat
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(488, 290);
			this.Controls.Add(this.ImportWotStatTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImportWotStat";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Import Battles from WoT Statistics";
			this.Load += new System.EventHandler(this.ImportWotStat_Load);
			this.ImportWotStatTheme.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog openFileWotStatDbFile;
		private BadForm ImportWotStatTheme;
		private BadTextBox txtWotStatDb;
		private BadLabel badLabel1;
		private BadGroupBox badGroupBox1;
		private BadProgressBar progressBarImport;
		private BadButton btnStartImport;
		private BadButton btnOpenWotStatDbFile;
		private BadTextBox txtToDate;
		private BadLabel badLabel2;
		private BadLabel lblResult;
		private BadButton btnRemove;
		private BadButton btnClose;
	}
}