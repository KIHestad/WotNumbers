namespace WinApp.Forms
{
	partial class WoTGameClientSettings
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WoTGameClientSettings));
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.badForm1 = new BadForm();
			this.chkAutoRun = new BadCheckBox();
			this.btnCancel = new BadButton();
			this.btnSave = new BadButton();
			this.chkCore7 = new BadCheckBox();
			this.chkCore6 = new BadCheckBox();
			this.chkCore5 = new BadCheckBox();
			this.chkCore4 = new BadCheckBox();
			this.chkCore3 = new BadCheckBox();
			this.chkCore2 = new BadCheckBox();
			this.chkCore1 = new BadCheckBox();
			this.chkCore0 = new BadCheckBox();
			this.badLabel3 = new BadLabel();
			this.chkOptimizeOn = new BadCheckBox();
			this.badGroupBox2 = new BadGroupBox();
			this.btnFolder = new BadButton();
			this.txtFolder = new BadTextBox();
			this.badLabel2 = new BadLabel();
			this.badLabel1 = new BadLabel();
			this.ddStartApp = new BadDropDownBox();
			this.badGroupBox1 = new BadGroupBox();
			this.btnFile = new BadButton();
			this.txtBatchFile = new BadTextBox();
			this.badLabel4 = new BadLabel();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.badForm1.SuspendLayout();
			this.SuspendLayout();
			// 
			// badForm1
			// 
			this.badForm1.Controls.Add(this.btnFile);
			this.badForm1.Controls.Add(this.txtBatchFile);
			this.badForm1.Controls.Add(this.badLabel4);
			this.badForm1.Controls.Add(this.chkAutoRun);
			this.badForm1.Controls.Add(this.btnCancel);
			this.badForm1.Controls.Add(this.btnSave);
			this.badForm1.Controls.Add(this.chkCore7);
			this.badForm1.Controls.Add(this.chkCore6);
			this.badForm1.Controls.Add(this.chkCore5);
			this.badForm1.Controls.Add(this.chkCore4);
			this.badForm1.Controls.Add(this.chkCore3);
			this.badForm1.Controls.Add(this.chkCore2);
			this.badForm1.Controls.Add(this.chkCore1);
			this.badForm1.Controls.Add(this.chkCore0);
			this.badForm1.Controls.Add(this.badLabel3);
			this.badForm1.Controls.Add(this.chkOptimizeOn);
			this.badForm1.Controls.Add(this.badGroupBox2);
			this.badForm1.Controls.Add(this.btnFolder);
			this.badForm1.Controls.Add(this.txtFolder);
			this.badForm1.Controls.Add(this.badLabel2);
			this.badForm1.Controls.Add(this.badLabel1);
			this.badForm1.Controls.Add(this.ddStartApp);
			this.badForm1.Controls.Add(this.badGroupBox1);
			this.badForm1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.badForm1.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.badForm1.FormFooter = false;
			this.badForm1.FormFooterHeight = 26;
			this.badForm1.FormInnerBorder = 3;
			this.badForm1.FormMargin = 0;
			this.badForm1.Image = null;
			this.badForm1.Location = new System.Drawing.Point(0, 0);
			this.badForm1.MainArea = mainAreaClass1;
			this.badForm1.Name = "badForm1";
			this.badForm1.Resizable = false;
			this.badForm1.Size = new System.Drawing.Size(401, 460);
			this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
			this.badForm1.SystemMaximizeImage = null;
			this.badForm1.SystemMinimizeImage = null;
			this.badForm1.TabIndex = 0;
			this.badForm1.Text = "WoT Game Starting Settings";
			this.badForm1.TitleHeight = 26;
			// 
			// chkAutoRun
			// 
			this.chkAutoRun.BackColor = System.Drawing.Color.Transparent;
			this.chkAutoRun.Checked = false;
			this.chkAutoRun.Image = ((System.Drawing.Image)(resources.GetObject("chkAutoRun.Image")));
			this.chkAutoRun.Location = new System.Drawing.Point(39, 159);
			this.chkAutoRun.Name = "chkAutoRun";
			this.chkAutoRun.Size = new System.Drawing.Size(212, 23);
			this.chkAutoRun.TabIndex = 19;
			this.chkAutoRun.Text = "Auto run when Wot Numbers starts";
			// 
			// btnCancel
			// 
			this.btnCancel.BlackButton = false;
			this.btnCancel.Checked = false;
			this.btnCancel.Image = null;
			this.btnCancel.Location = new System.Drawing.Point(304, 417);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 18;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.ToolTipText = "";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.BlackButton = false;
			this.btnSave.Checked = false;
			this.btnSave.Image = null;
			this.btnSave.Location = new System.Drawing.Point(223, 417);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 17;
			this.btnSave.Text = "Save";
			this.btnSave.ToolTipText = "";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// chkCore7
			// 
			this.chkCore7.BackColor = System.Drawing.Color.Transparent;
			this.chkCore7.Checked = false;
			this.chkCore7.Enabled = false;
			this.chkCore7.Image = ((System.Drawing.Image)(resources.GetObject("chkCore7.Image")));
			this.chkCore7.Location = new System.Drawing.Point(165, 355);
			this.chkCore7.Name = "chkCore7";
			this.chkCore7.Size = new System.Drawing.Size(99, 23);
			this.chkCore7.TabIndex = 16;
			this.chkCore7.Text = "Use CPU 7";
			this.chkCore7.Visible = false;
			// 
			// chkCore6
			// 
			this.chkCore6.BackColor = System.Drawing.Color.Transparent;
			this.chkCore6.Checked = false;
			this.chkCore6.Enabled = false;
			this.chkCore6.Image = ((System.Drawing.Image)(resources.GetObject("chkCore6.Image")));
			this.chkCore6.Location = new System.Drawing.Point(165, 335);
			this.chkCore6.Name = "chkCore6";
			this.chkCore6.Size = new System.Drawing.Size(99, 23);
			this.chkCore6.TabIndex = 15;
			this.chkCore6.Text = "Use CPU 6";
			this.chkCore6.Visible = false;
			// 
			// chkCore5
			// 
			this.chkCore5.BackColor = System.Drawing.Color.Transparent;
			this.chkCore5.Checked = false;
			this.chkCore5.Enabled = false;
			this.chkCore5.Image = ((System.Drawing.Image)(resources.GetObject("chkCore5.Image")));
			this.chkCore5.Location = new System.Drawing.Point(165, 315);
			this.chkCore5.Name = "chkCore5";
			this.chkCore5.Size = new System.Drawing.Size(99, 23);
			this.chkCore5.TabIndex = 14;
			this.chkCore5.Text = "Use CPU 5";
			this.chkCore5.Visible = false;
			// 
			// chkCore4
			// 
			this.chkCore4.BackColor = System.Drawing.Color.Transparent;
			this.chkCore4.Checked = false;
			this.chkCore4.Enabled = false;
			this.chkCore4.Image = ((System.Drawing.Image)(resources.GetObject("chkCore4.Image")));
			this.chkCore4.Location = new System.Drawing.Point(165, 296);
			this.chkCore4.Name = "chkCore4";
			this.chkCore4.Size = new System.Drawing.Size(99, 23);
			this.chkCore4.TabIndex = 13;
			this.chkCore4.Text = "Use CPU 4";
			this.chkCore4.Visible = false;
			// 
			// chkCore3
			// 
			this.chkCore3.BackColor = System.Drawing.Color.Transparent;
			this.chkCore3.Checked = false;
			this.chkCore3.Enabled = false;
			this.chkCore3.Image = ((System.Drawing.Image)(resources.GetObject("chkCore3.Image")));
			this.chkCore3.Location = new System.Drawing.Point(60, 355);
			this.chkCore3.Name = "chkCore3";
			this.chkCore3.Size = new System.Drawing.Size(99, 23);
			this.chkCore3.TabIndex = 12;
			this.chkCore3.Text = "Use CPU 3";
			this.chkCore3.Visible = false;
			// 
			// chkCore2
			// 
			this.chkCore2.BackColor = System.Drawing.Color.Transparent;
			this.chkCore2.Checked = false;
			this.chkCore2.Enabled = false;
			this.chkCore2.Image = ((System.Drawing.Image)(resources.GetObject("chkCore2.Image")));
			this.chkCore2.Location = new System.Drawing.Point(60, 335);
			this.chkCore2.Name = "chkCore2";
			this.chkCore2.Size = new System.Drawing.Size(99, 23);
			this.chkCore2.TabIndex = 11;
			this.chkCore2.Text = "Use CPU 2";
			this.chkCore2.Visible = false;
			// 
			// chkCore1
			// 
			this.chkCore1.BackColor = System.Drawing.Color.Transparent;
			this.chkCore1.Checked = false;
			this.chkCore1.Enabled = false;
			this.chkCore1.Image = ((System.Drawing.Image)(resources.GetObject("chkCore1.Image")));
			this.chkCore1.Location = new System.Drawing.Point(60, 315);
			this.chkCore1.Name = "chkCore1";
			this.chkCore1.Size = new System.Drawing.Size(99, 23);
			this.chkCore1.TabIndex = 10;
			this.chkCore1.Text = "Use CPU 1";
			this.chkCore1.Visible = false;
			// 
			// chkCore0
			// 
			this.chkCore0.BackColor = System.Drawing.Color.Transparent;
			this.chkCore0.Checked = false;
			this.chkCore0.Enabled = false;
			this.chkCore0.Image = ((System.Drawing.Image)(resources.GetObject("chkCore0.Image")));
			this.chkCore0.Location = new System.Drawing.Point(60, 296);
			this.chkCore0.Name = "chkCore0";
			this.chkCore0.Size = new System.Drawing.Size(99, 23);
			this.chkCore0.TabIndex = 9;
			this.chkCore0.Text = "Use CPU 0";
			this.chkCore0.Visible = false;
			// 
			// badLabel3
			// 
			this.badLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel3.Dimmed = false;
			this.badLabel3.Image = null;
			this.badLabel3.Location = new System.Drawing.Point(39, 265);
			this.badLabel3.Name = "badLabel3";
			this.badLabel3.Size = new System.Drawing.Size(323, 25);
			this.badLabel3.TabIndex = 8;
			this.badLabel3.Text = "WoT runs best not using CPU 0";
			this.badLabel3.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// chkOptimizeOn
			// 
			this.chkOptimizeOn.BackColor = System.Drawing.Color.Transparent;
			this.chkOptimizeOn.Checked = false;
			this.chkOptimizeOn.Image = ((System.Drawing.Image)(resources.GetObject("chkOptimizeOn.Image")));
			this.chkOptimizeOn.Location = new System.Drawing.Point(39, 235);
			this.chkOptimizeOn.Name = "chkOptimizeOn";
			this.chkOptimizeOn.Size = new System.Drawing.Size(157, 23);
			this.chkOptimizeOn.TabIndex = 7;
			this.chkOptimizeOn.Text = "Use Optimization Mode";
			this.chkOptimizeOn.Click += new System.EventHandler(this.chkOptimizeOn_Click);
			// 
			// badGroupBox2
			// 
			this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox2.Image = null;
			this.badGroupBox2.Location = new System.Drawing.Point(20, 210);
			this.badGroupBox2.Name = "badGroupBox2";
			this.badGroupBox2.Size = new System.Drawing.Size(358, 187);
			this.badGroupBox2.TabIndex = 6;
			this.badGroupBox2.Text = "Optimization (Set Affinity and Priority High)";
			// 
			// btnFolder
			// 
			this.btnFolder.BlackButton = false;
			this.btnFolder.Checked = false;
			this.btnFolder.Image = null;
			this.btnFolder.Location = new System.Drawing.Point(338, 102);
			this.btnFolder.Name = "btnFolder";
			this.btnFolder.Size = new System.Drawing.Size(24, 23);
			this.btnFolder.TabIndex = 5;
			this.btnFolder.Text = "...";
			this.btnFolder.ToolTipText = "";
			this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
			// 
			// txtFolder
			// 
			this.txtFolder.HasFocus = false;
			this.txtFolder.Image = null;
			this.txtFolder.Location = new System.Drawing.Point(89, 102);
			this.txtFolder.MultilineAllow = false;
			this.txtFolder.Name = "txtFolder";
			this.txtFolder.PasswordChar = '\0';
			this.txtFolder.ReadOnly = false;
			this.txtFolder.Size = new System.Drawing.Size(243, 23);
			this.txtFolder.TabIndex = 4;
			this.txtFolder.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtFolder.ToolTipText = "The folder where WoT game is installed";
			// 
			// badLabel2
			// 
			this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel2.Dimmed = false;
			this.badLabel2.Image = null;
			this.badLabel2.Location = new System.Drawing.Point(39, 102);
			this.badLabel2.Name = "badLabel2";
			this.badLabel2.Size = new System.Drawing.Size(43, 23);
			this.badLabel2.TabIndex = 3;
			this.badLabel2.Text = "Folder";
			this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(39, 72);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(43, 23);
			this.badLabel1.TabIndex = 2;
			this.badLabel1.Text = "Start";
			this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// ddStartApp
			// 
			this.ddStartApp.Image = null;
			this.ddStartApp.Location = new System.Drawing.Point(88, 72);
			this.ddStartApp.Name = "ddStartApp";
			this.ddStartApp.Size = new System.Drawing.Size(143, 23);
			this.ddStartApp.TabIndex = 1;
			this.ddStartApp.Text = "Do not start WoT";
			this.ddStartApp.Click += new System.EventHandler(this.ddStartApp_Click);
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(20, 45);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(358, 150);
			this.badGroupBox1.TabIndex = 0;
			this.badGroupBox1.Text = "Settings";
			// 
			// btnFile
			// 
			this.btnFile.BlackButton = false;
			this.btnFile.Checked = false;
			this.btnFile.Image = null;
			this.btnFile.Location = new System.Drawing.Point(338, 131);
			this.btnFile.Name = "btnFile";
			this.btnFile.Size = new System.Drawing.Size(24, 23);
			this.btnFile.TabIndex = 22;
			this.btnFile.Text = "...";
			this.btnFile.ToolTipText = "";
			this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
			// 
			// txtBatchFile
			// 
			this.txtBatchFile.HasFocus = false;
			this.txtBatchFile.Image = null;
			this.txtBatchFile.Location = new System.Drawing.Point(89, 131);
			this.txtBatchFile.MultilineAllow = false;
			this.txtBatchFile.Name = "txtBatchFile";
			this.txtBatchFile.PasswordChar = '\0';
			this.txtBatchFile.ReadOnly = false;
			this.txtBatchFile.Size = new System.Drawing.Size(243, 23);
			this.txtBatchFile.TabIndex = 21;
			this.txtBatchFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtBatchFile.ToolTipText = "Optional batch file to run";
			// 
			// badLabel4
			// 
			this.badLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel4.Dimmed = false;
			this.badLabel4.Image = null;
			this.badLabel4.Location = new System.Drawing.Point(39, 131);
			this.badLabel4.Name = "badLabel4";
			this.badLabel4.Size = new System.Drawing.Size(43, 23);
			this.badLabel4.TabIndex = 20;
			this.badLabel4.Text = "Run";
			this.badLabel4.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// WoTGameClientSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(401, 460);
			this.Controls.Add(this.badForm1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "WoTGameClientSettings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "WoTGameClientSettings";
			this.Load += new System.EventHandler(this.WoTGameClientSettings_Load);
			this.badForm1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm badForm1;
		private BadDropDownBox ddStartApp;
		private BadGroupBox badGroupBox1;
		private BadLabel badLabel1;
		private BadButton btnCancel;
		private BadButton btnSave;
		private BadCheckBox chkCore7;
		private BadCheckBox chkCore6;
		private BadCheckBox chkCore5;
		private BadCheckBox chkCore4;
		private BadCheckBox chkCore3;
		private BadCheckBox chkCore2;
		private BadCheckBox chkCore1;
		private BadCheckBox chkCore0;
		private BadLabel badLabel3;
		private BadCheckBox chkOptimizeOn;
		private BadGroupBox badGroupBox2;
		private BadButton btnFolder;
		private BadTextBox txtFolder;
		private BadLabel badLabel2;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private BadCheckBox chkAutoRun;
		private BadButton btnFile;
		private BadTextBox txtBatchFile;
		private BadLabel badLabel4;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
	}
}