namespace WotDBUpdater.Forms.File
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtWotStatDb = new System.Windows.Forms.TextBox();
			this.btnOpenWotStatDbFile = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lblResult = new System.Windows.Forms.Label();
			this.btnStartImport = new System.Windows.Forms.Button();
			this.progressBarImport = new System.Windows.Forms.ProgressBar();
			this.openFileWotStatDbFile = new System.Windows.Forms.OpenFileDialog();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(147, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "WoT Statisctics Databasefile:";
			// 
			// txtWotStatDb
			// 
			this.txtWotStatDb.Location = new System.Drawing.Point(21, 42);
			this.txtWotStatDb.Name = "txtWotStatDb";
			this.txtWotStatDb.Size = new System.Drawing.Size(378, 20);
			this.txtWotStatDb.TabIndex = 1;
			// 
			// btnOpenWotStatDbFile
			// 
			this.btnOpenWotStatDbFile.Location = new System.Drawing.Point(405, 40);
			this.btnOpenWotStatDbFile.Name = "btnOpenWotStatDbFile";
			this.btnOpenWotStatDbFile.Size = new System.Drawing.Size(29, 23);
			this.btnOpenWotStatDbFile.TabIndex = 2;
			this.btnOpenWotStatDbFile.Text = "...";
			this.btnOpenWotStatDbFile.UseVisualStyleBackColor = true;
			this.btnOpenWotStatDbFile.Click += new System.EventHandler(this.btnOpenWotStatDbFile_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtWotStatDb);
			this.groupBox1.Controls.Add(this.btnOpenWotStatDbFile);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(452, 80);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Parameters";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lblResult);
			this.groupBox2.Controls.Add(this.btnStartImport);
			this.groupBox2.Controls.Add(this.progressBarImport);
			this.groupBox2.Location = new System.Drawing.Point(13, 110);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(451, 88);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Import";
			// 
			// lblResult
			// 
			this.lblResult.AutoSize = true;
			this.lblResult.Location = new System.Drawing.Point(17, 58);
			this.lblResult.Name = "lblResult";
			this.lblResult.Size = new System.Drawing.Size(47, 13);
			this.lblResult.TabIndex = 2;
			this.lblResult.Text = "Ready...";
			// 
			// btnStartImport
			// 
			this.btnStartImport.Location = new System.Drawing.Point(358, 53);
			this.btnStartImport.Name = "btnStartImport";
			this.btnStartImport.Size = new System.Drawing.Size(75, 23);
			this.btnStartImport.TabIndex = 1;
			this.btnStartImport.Text = "Start";
			this.btnStartImport.UseVisualStyleBackColor = true;
			this.btnStartImport.Click += new System.EventHandler(this.btnStartImport_Click);
			// 
			// progressBarImport
			// 
			this.progressBarImport.Location = new System.Drawing.Point(20, 20);
			this.progressBarImport.Name = "progressBarImport";
			this.progressBarImport.Size = new System.Drawing.Size(413, 23);
			this.progressBarImport.TabIndex = 0;
			// 
			// openFileWotStatDbFile
			// 
			this.openFileWotStatDbFile.FileName = "*.db";
			// 
			// ImportWotStat
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(479, 214);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImportWotStat";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Import Battles from WoT Statistics";
			this.Load += new System.EventHandler(this.ImportWotStat_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtWotStatDb;
		private System.Windows.Forms.Button btnOpenWotStatDbFile;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnStartImport;
		private System.Windows.Forms.ProgressBar progressBarImport;
		private System.Windows.Forms.OpenFileDialog openFileWotStatDbFile;
		private System.Windows.Forms.Label lblResult;
	}
}