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
			this.roolItemFile = new System.Windows.Forms.ToolStripDropDownButton();
			this.roolItemFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnCreate = new System.Windows.Forms.Button();
			this.btnSelect = new System.Windows.Forms.Button();
			this.txtAdminSQLiteDB = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.openFileDialogDQLiteADminDB = new System.Windows.Forms.OpenFileDialog();
			this.toolStripMain.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripMain
			// 
			this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roolItemFile});
			this.toolStripMain.Location = new System.Drawing.Point(0, 0);
			this.toolStripMain.Name = "toolStripMain";
			this.toolStripMain.ShowItemToolTips = false;
			this.toolStripMain.Size = new System.Drawing.Size(441, 25);
			this.toolStripMain.TabIndex = 0;
			this.toolStripMain.Text = "toolStrip1";
			// 
			// roolItemFile
			// 
			this.roolItemFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.roolItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roolItemFileExit});
			this.roolItemFile.Image = ((System.Drawing.Image)(resources.GetObject("roolItemFile.Image")));
			this.roolItemFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.roolItemFile.Name = "roolItemFile";
			this.roolItemFile.Size = new System.Drawing.Size(38, 22);
			this.roolItemFile.Text = "&File";
			// 
			// roolItemFileExit
			// 
			this.roolItemFileExit.Name = "roolItemFileExit";
			this.roolItemFileExit.Size = new System.Drawing.Size(92, 22);
			this.roolItemFileExit.Text = "&Exit";
			this.roolItemFileExit.Click += new System.EventHandler(this.roolItemFileExit_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnCreate);
			this.groupBox1.Controls.Add(this.btnSelect);
			this.groupBox1.Controls.Add(this.txtAdminSQLiteDB);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(25, 44);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(395, 153);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Connection to Admin SQLite database";
			// 
			// btnCreate
			// 
			this.btnCreate.Location = new System.Drawing.Point(220, 114);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size(75, 23);
			this.btnCreate.TabIndex = 3;
			this.btnCreate.Text = "Create new";
			this.btnCreate.UseVisualStyleBackColor = true;
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			// 
			// btnSelect
			// 
			this.btnSelect.Location = new System.Drawing.Point(301, 114);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(75, 23);
			this.btnSelect.TabIndex = 2;
			this.btnSelect.Text = "Select";
			this.btnSelect.UseVisualStyleBackColor = true;
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
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
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(441, 222);
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
		private System.Windows.Forms.ToolStripDropDownButton roolItemFile;
		private System.Windows.Forms.ToolStripMenuItem roolItemFileExit;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnCreate;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.TextBox txtAdminSQLiteDB;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog openFileDialogDQLiteADminDB;
	}
}

