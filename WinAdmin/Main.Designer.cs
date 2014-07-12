namespace WinAdmin
{
	partial class Main
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.toolStripMain = new System.Windows.Forms.ToolStrip();
			this.menuFile = new System.Windows.Forms.ToolStripDropDownButton();
			this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtAdminSQLiteDB = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.openFileDialogDQLiteADminDB = new System.Windows.Forms.OpenFileDialog();
			this.menuNewDB = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuSelectDB = new System.Windows.Forms.ToolStripMenuItem();
			this.menuData = new System.Windows.Forms.ToolStripDropDownButton();
			this.menuDataCreateTableStruct = new System.Windows.Forms.ToolStripMenuItem();
			this.menuDataGetTankDataFromAPI = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMain.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripMain
			// 
			this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuData});
			this.toolStripMain.Location = new System.Drawing.Point(0, 0);
			this.toolStripMain.Name = "toolStripMain";
			this.toolStripMain.ShowItemToolTips = false;
			this.toolStripMain.Size = new System.Drawing.Size(441, 25);
			this.toolStripMain.TabIndex = 0;
			this.toolStripMain.Text = "toolStrip1";
			// 
			// menuFile
			// 
			this.menuFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewDB,
            this.menuSelectDB,
            this.toolStripSeparator1,
            this.MenuExit});
			this.menuFile.Image = ((System.Drawing.Image)(resources.GetObject("menuFile.Image")));
			this.menuFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.menuFile.Name = "menuFile";
			this.menuFile.Size = new System.Drawing.Size(38, 22);
			this.menuFile.Text = "&File";
			// 
			// MenuExit
			// 
			this.MenuExit.Name = "MenuExit";
			this.MenuExit.Size = new System.Drawing.Size(201, 22);
			this.MenuExit.Text = "&Exit";
			this.MenuExit.Click += new System.EventHandler(this.MenuExit_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtAdminSQLiteDB);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(25, 44);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(395, 120);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Connection to Admin SQLite database";
			// 
			// txtAdminSQLiteDB
			// 
			this.txtAdminSQLiteDB.Enabled = false;
			this.txtAdminSQLiteDB.Location = new System.Drawing.Point(19, 45);
			this.txtAdminSQLiteDB.Multiline = true;
			this.txtAdminSQLiteDB.Name = "txtAdminSQLiteDB";
			this.txtAdminSQLiteDB.Size = new System.Drawing.Size(357, 56);
			this.txtAdminSQLiteDB.TabIndex = 1;
			this.txtAdminSQLiteDB.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Filename:";
			// 
			// openFileDialogDQLiteADminDB
			// 
			this.openFileDialogDQLiteADminDB.FileName = "openFileDialog1";
			// 
			// menuNewDB
			// 
			this.menuNewDB.Name = "menuNewDB";
			this.menuNewDB.Size = new System.Drawing.Size(201, 22);
			this.menuNewDB.Text = "Create New Admin DB...";
			this.menuNewDB.Click += new System.EventHandler(this.menuNewDB_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
			// 
			// menuSelectDB
			// 
			this.menuSelectDB.Name = "menuSelectDB";
			this.menuSelectDB.Size = new System.Drawing.Size(201, 22);
			this.menuSelectDB.Text = "Select Admin DB...";
			this.menuSelectDB.Click += new System.EventHandler(this.menuSelectDB_Click);
			// 
			// menuData
			// 
			this.menuData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.menuData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDataCreateTableStruct,
            this.menuDataGetTankDataFromAPI});
			this.menuData.Image = ((System.Drawing.Image)(resources.GetObject("menuData.Image")));
			this.menuData.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.menuData.Name = "menuData";
			this.menuData.Size = new System.Drawing.Size(44, 22);
			this.menuData.Text = "Data";
			// 
			// menuDataCreateTableStruct
			// 
			this.menuDataCreateTableStruct.Name = "menuDataCreateTableStruct";
			this.menuDataCreateTableStruct.Size = new System.Drawing.Size(203, 22);
			this.menuDataCreateTableStruct.Text = "Create table structure";
			this.menuDataCreateTableStruct.Click += new System.EventHandler(this.menuDataCreateTableStruct_Click);
			// 
			// menuDataGetTankDataFromAPI
			// 
			this.menuDataGetTankDataFromAPI.Name = "menuDataGetTankDataFromAPI";
			this.menuDataGetTankDataFromAPI.Size = new System.Drawing.Size(203, 22);
			this.menuDataGetTankDataFromAPI.Text = "Get tank data from API...";
			this.menuDataGetTankDataFromAPI.Click += new System.EventHandler(this.menuDataGetTankDataFromAPI_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(441, 188);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.toolStripMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Main";
			this.Text = "Wot Number Admin";
			this.Load += new System.EventHandler(this.Main_Load);
			this.toolStripMain.ResumeLayout(false);
			this.toolStripMain.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStripMain;
		private System.Windows.Forms.ToolStripDropDownButton menuFile;
		private System.Windows.Forms.ToolStripMenuItem MenuExit;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtAdminSQLiteDB;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog openFileDialogDQLiteADminDB;
		private System.Windows.Forms.ToolStripMenuItem menuNewDB;
		private System.Windows.Forms.ToolStripMenuItem menuSelectDB;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripDropDownButton menuData;
		private System.Windows.Forms.ToolStripMenuItem menuDataCreateTableStruct;
		private System.Windows.Forms.ToolStripMenuItem menuDataGetTankDataFromAPI;
	}
}

