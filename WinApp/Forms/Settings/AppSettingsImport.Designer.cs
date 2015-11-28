namespace WinApp.Forms.Settings
{
    partial class AppSettingsImport
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.openFileWotStatDbFile = new System.Windows.Forms.OpenFileDialog();
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
            this.SuspendLayout();
            // 
            // openFileWotStatDbFile
            // 
            this.openFileWotStatDbFile.FileName = "*.db";
            // 
            // btnRemove
            // 
            this.btnRemove.BlackButton = false;
            this.btnRemove.Checked = false;
            this.btnRemove.Image = null;
            this.btnRemove.Location = new System.Drawing.Point(375, 271);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(70, 23);
            this.btnRemove.TabIndex = 21;
            this.btnRemove.Text = "Remove";
            this.btnRemove.ToolTipText = "";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblResult.Dimmed = false;
            this.lblResult.Image = null;
            this.lblResult.Location = new System.Drawing.Point(0, 271);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(280, 23);
            this.lblResult.TabIndex = 19;
            this.lblResult.TabStop = false;
            this.lblResult.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // txtToDate
            // 
            this.txtToDate.HasFocus = false;
            this.txtToDate.Image = null;
            this.txtToDate.Location = new System.Drawing.Point(314, 23);
            this.txtToDate.MultilineAllow = false;
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.PasswordChar = '\0';
            this.txtToDate.ReadOnly = false;
            this.txtToDate.Size = new System.Drawing.Size(114, 23);
            this.txtToDate.TabIndex = 14;
            this.txtToDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtToDate.ToolTipText = "";
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(17, 23);
            this.badLabel2.Name = "badLabel2";
            this.badLabel2.Size = new System.Drawing.Size(290, 23);
            this.badLabel2.TabIndex = 13;
            this.badLabel2.TabStop = false;
            this.badLabel2.Text = "Import battles before date (dd.mm.yyyy), blank = import all:";
            this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // progressBarImport
            // 
            this.progressBarImport.BackColor = System.Drawing.Color.Transparent;
            this.progressBarImport.Image = null;
            this.progressBarImport.Location = new System.Drawing.Point(0, 230);
            this.progressBarImport.Name = "progressBarImport";
            this.progressBarImport.ProgressBarColorMode = false;
            this.progressBarImport.ProgressBarMargins = 2;
            this.progressBarImport.ProgressBarShowPercentage = false;
            this.progressBarImport.Size = new System.Drawing.Size(445, 23);
            this.progressBarImport.TabIndex = 18;
            this.progressBarImport.TabStop = false;
            this.progressBarImport.Text = "badProgressBar1";
            this.progressBarImport.Value = 0D;
            this.progressBarImport.ValueMax = 100D;
            this.progressBarImport.ValueMin = 0D;
            // 
            // btnStartImport
            // 
            this.btnStartImport.BlackButton = false;
            this.btnStartImport.Checked = false;
            this.btnStartImport.Image = null;
            this.btnStartImport.Location = new System.Drawing.Point(286, 271);
            this.btnStartImport.Name = "btnStartImport";
            this.btnStartImport.Size = new System.Drawing.Size(82, 23);
            this.btnStartImport.TabIndex = 20;
            this.btnStartImport.Text = "Start Import";
            this.btnStartImport.ToolTipText = "";
            this.btnStartImport.Click += new System.EventHandler(this.btnStartImport_Click);
            // 
            // btnOpenWotStatDbFile
            // 
            this.btnOpenWotStatDbFile.BlackButton = false;
            this.btnOpenWotStatDbFile.Checked = false;
            this.btnOpenWotStatDbFile.Image = null;
            this.btnOpenWotStatDbFile.Location = new System.Drawing.Point(349, 170);
            this.btnOpenWotStatDbFile.Name = "btnOpenWotStatDbFile";
            this.btnOpenWotStatDbFile.Size = new System.Drawing.Size(79, 23);
            this.btnOpenWotStatDbFile.TabIndex = 17;
            this.btnOpenWotStatDbFile.Text = "Select File";
            this.btnOpenWotStatDbFile.ToolTipText = "";
            this.btnOpenWotStatDbFile.Click += new System.EventHandler(this.btnOpenWotStatDbFile_Click);
            // 
            // txtWotStatDb
            // 
            this.txtWotStatDb.HasFocus = false;
            this.txtWotStatDb.Image = null;
            this.txtWotStatDb.Location = new System.Drawing.Point(17, 81);
            this.txtWotStatDb.MultilineAllow = true;
            this.txtWotStatDb.Name = "txtWotStatDb";
            this.txtWotStatDb.PasswordChar = '\0';
            this.txtWotStatDb.ReadOnly = false;
            this.txtWotStatDb.Size = new System.Drawing.Size(411, 71);
            this.txtWotStatDb.TabIndex = 16;
            this.txtWotStatDb.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtWotStatDb.ToolTipText = "";
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(17, 57);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(175, 23);
            this.badLabel1.TabIndex = 15;
            this.badLabel1.TabStop = false;
            this.badLabel1.Text = "Wot Statistics Database File:";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(1, 1);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(445, 210);
            this.badGroupBox1.TabIndex = 12;
            this.badGroupBox1.TabStop = false;
            this.badGroupBox1.Text = "Import Parameters";
            // 
            // AppSettingsImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.txtToDate);
            this.Controls.Add(this.badLabel2);
            this.Controls.Add(this.progressBarImport);
            this.Controls.Add(this.btnStartImport);
            this.Controls.Add(this.btnOpenWotStatDbFile);
            this.Controls.Add(this.txtWotStatDb);
            this.Controls.Add(this.badLabel1);
            this.Controls.Add(this.badGroupBox1);
            this.Name = "AppSettingsImport";
            this.Size = new System.Drawing.Size(458, 308);
            this.ResumeLayout(false);

        }

        #endregion

        private BadButton btnRemove;
        private BadLabel lblResult;
        private BadTextBox txtToDate;
        private BadLabel badLabel2;
        private BadProgressBar progressBarImport;
        private BadButton btnStartImport;
        private BadButton btnOpenWotStatDbFile;
        private BadTextBox txtWotStatDb;
        private BadLabel badLabel1;
        private BadGroupBox badGroupBox1;
        private System.Windows.Forms.OpenFileDialog openFileWotStatDbFile;
    }
}
