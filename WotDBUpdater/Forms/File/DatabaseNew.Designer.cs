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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseNew));
			this.badForm1 = new BadForm();
			this.pbCreateDatabase = new System.Windows.Forms.ProgressBar();
			this.label3 = new System.Windows.Forms.Label();
			this.badSeperator1 = new BadSeperator();
			this.txtFileLocation = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnCreateDB = new BadButton();
			this.label1 = new System.Windows.Forms.Label();
			this.txtDatabasename = new System.Windows.Forms.TextBox();
			this.txtPlayerName = new System.Windows.Forms.TextBox();
			this.badForm1.SuspendLayout();
			this.SuspendLayout();
			// 
			// badForm1
			// 
			this.badForm1.Controls.Add(this.pbCreateDatabase);
			this.badForm1.Controls.Add(this.label3);
			this.badForm1.Controls.Add(this.badSeperator1);
			this.badForm1.Controls.Add(this.txtFileLocation);
			this.badForm1.Controls.Add(this.label2);
			this.badForm1.Controls.Add(this.btnCreateDB);
			this.badForm1.Controls.Add(this.label1);
			this.badForm1.Controls.Add(this.txtDatabasename);
			this.badForm1.Controls.Add(this.txtPlayerName);
			this.badForm1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.badForm1.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.badForm1.FormFooter = false;
			this.badForm1.FormFooterHeight = 26;
			this.badForm1.FormMargin = 0;
			this.badForm1.Image = null;
			this.badForm1.Location = new System.Drawing.Point(0, 0);
			this.badForm1.Name = "badForm1";
			this.badForm1.Resizable = false;
			this.badForm1.Size = new System.Drawing.Size(435, 222);
			this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
			this.badForm1.SystemMaximizeImage = null;
			this.badForm1.SystemMinimizeImage = null;
			this.badForm1.TabIndex = 14;
			this.badForm1.Text = "Create New Database";
			this.badForm1.TitleHeight = 26;
			// 
			// pbCreateDatabase
			// 
			this.pbCreateDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.pbCreateDatabase.Location = new System.Drawing.Point(27, 170);
			this.pbCreateDatabase.MarqueeAnimationSpeed = 1;
			this.pbCreateDatabase.Name = "pbCreateDatabase";
			this.pbCreateDatabase.Size = new System.Drawing.Size(264, 23);
			this.pbCreateDatabase.Step = 1;
			this.pbCreateDatabase.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pbCreateDatabase.TabIndex = 13;
			this.pbCreateDatabase.UseWaitCursor = true;
			this.pbCreateDatabase.Value = 1;
			this.pbCreateDatabase.Visible = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.label3.Location = new System.Drawing.Point(24, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "Database file location:";
			// 
			// badSeperator1
			// 
			this.badSeperator1.BackColor = System.Drawing.Color.Transparent;
			this.badSeperator1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.badSeperator1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.badSeperator1.Direction = System.Windows.Forms.Orientation.Horizontal;
			this.badSeperator1.Image = null;
			this.badSeperator1.Location = new System.Drawing.Point(27, 138);
			this.badSeperator1.Name = "badSeperator1";
			this.badSeperator1.Size = new System.Drawing.Size(380, 23);
			this.badSeperator1.TabIndex = 14;
			this.badSeperator1.Text = "gghjfgjh";
			// 
			// txtFileLocation
			// 
			this.txtFileLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.txtFileLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFileLocation.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.txtFileLocation.Location = new System.Drawing.Point(27, 104);
			this.txtFileLocation.Name = "txtFileLocation";
			this.txtFileLocation.Size = new System.Drawing.Size(380, 20);
			this.txtFileLocation.TabIndex = 13;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.label2.Location = new System.Drawing.Point(24, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Database name:";
			// 
			// btnCreateDB
			// 
			this.btnCreateDB.Image = null;
			this.btnCreateDB.Location = new System.Drawing.Point(301, 170);
			this.btnCreateDB.Name = "btnCreateDB";
			this.btnCreateDB.Size = new System.Drawing.Size(106, 23);
			this.btnCreateDB.TabIndex = 0;
			this.btnCreateDB.Text = "Create Database";
			this.btnCreateDB.Click += new System.EventHandler(this.btnCreateDB_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.label1.Location = new System.Drawing.Point(223, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Player name:";
			// 
			// txtDatabasename
			// 
			this.txtDatabasename.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.txtDatabasename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDatabasename.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.txtDatabasename.Location = new System.Drawing.Point(27, 59);
			this.txtDatabasename.Name = "txtDatabasename";
			this.txtDatabasename.Size = new System.Drawing.Size(181, 20);
			this.txtDatabasename.TabIndex = 11;
			// 
			// txtPlayerName
			// 
			this.txtPlayerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.txtPlayerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPlayerName.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.txtPlayerName.Location = new System.Drawing.Point(226, 59);
			this.txtPlayerName.Name = "txtPlayerName";
			this.txtPlayerName.Size = new System.Drawing.Size(181, 20);
			this.txtPlayerName.TabIndex = 9;
			// 
			// DatabaseNew
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(435, 222);
			this.Controls.Add(this.badForm1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DatabaseNew";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Create New Database";
			this.Load += new System.EventHandler(this.frmDatabaseNew_Load);
			this.badForm1.ResumeLayout(false);
			this.badForm1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.TextBox txtDatabasename;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFileLocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar pbCreateDatabase;
		private BadForm badForm1;
		private BadButton btnCreateDB;
		private BadSeperator badSeperator1;
    }
}