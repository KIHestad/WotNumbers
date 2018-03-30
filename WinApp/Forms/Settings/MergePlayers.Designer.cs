namespace WinApp.Forms.Settings
{
    partial class MergePlayers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MergePlayers));
            this.FormTheme = new BadForm();
            this.badLabel3 = new BadLabel();
            this.badGroupBox1 = new BadGroupBox();
            this.badLabel1 = new BadLabel();
            this.ddPlayerMergeTo = new BadDropDownBox();
            this.badLabel2 = new BadLabel();
            this.ddPlayerMergeFrom = new BadDropDownBox();
            this.grpPlayers = new BadGroupBox();
            this.btnStart = new BadButton();
            this.lblProgressStatus = new BadLabel();
            this.badProgressBar = new BadProgressBar();
            this.FormTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // FormTheme
            // 
            this.FormTheme.Controls.Add(this.badLabel3);
            this.FormTheme.Controls.Add(this.badGroupBox1);
            this.FormTheme.Controls.Add(this.badLabel1);
            this.FormTheme.Controls.Add(this.ddPlayerMergeTo);
            this.FormTheme.Controls.Add(this.badLabel2);
            this.FormTheme.Controls.Add(this.ddPlayerMergeFrom);
            this.FormTheme.Controls.Add(this.grpPlayers);
            this.FormTheme.Controls.Add(this.btnStart);
            this.FormTheme.Controls.Add(this.lblProgressStatus);
            this.FormTheme.Controls.Add(this.badProgressBar);
            this.FormTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FormTheme.FormExitAsMinimize = false;
            this.FormTheme.FormFooter = false;
            this.FormTheme.FormFooterHeight = 26;
            this.FormTheme.FormInnerBorder = 3;
            this.FormTheme.FormMargin = 0;
            this.FormTheme.Image = null;
            this.FormTheme.Location = new System.Drawing.Point(0, 0);
            this.FormTheme.MainArea = mainAreaClass1;
            this.FormTheme.Name = "FormTheme";
            this.FormTheme.Resizable = false;
            this.FormTheme.Size = new System.Drawing.Size(371, 307);
            this.FormTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("FormTheme.SystemExitImage")));
            this.FormTheme.SystemMaximizeImage = null;
            this.FormTheme.SystemMinimizeImage = null;
            this.FormTheme.TabIndex = 0;
            this.FormTheme.Text = "Merge Players";
            this.FormTheme.TitleHeight = 26;
            // 
            // badLabel3
            // 
            this.badLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel3.Dimmed = false;
            this.badLabel3.Image = null;
            this.badLabel3.Location = new System.Drawing.Point(37, 66);
            this.badLabel3.Name = "badLabel3";
            this.badLabel3.Size = new System.Drawing.Size(300, 33);
            this.badLabel3.TabIndex = 35;
            this.badLabel3.Text = "Please backup the database before merging data.";
            this.badLabel3.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(22, 40);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(330, 65);
            this.badGroupBox1.TabIndex = 34;
            this.badGroupBox1.Text = "Information";
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(39, 176);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(134, 23);
            this.badLabel1.TabIndex = 33;
            this.badLabel1.Text = "Player to copy data to:";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // ddPlayerMergeTo
            // 
            this.ddPlayerMergeTo.Image = null;
            this.ddPlayerMergeTo.Location = new System.Drawing.Point(197, 176);
            this.ddPlayerMergeTo.Name = "ddPlayerMergeTo";
            this.ddPlayerMergeTo.Size = new System.Drawing.Size(142, 23);
            this.ddPlayerMergeTo.TabIndex = 32;
            this.ddPlayerMergeTo.Click += new System.EventHandler(this.ddPlayerMergeTo_Click);
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(39, 147);
            this.badLabel2.Name = "badLabel2";
            this.badLabel2.Size = new System.Drawing.Size(134, 23);
            this.badLabel2.TabIndex = 31;
            this.badLabel2.Text = "Player to copy data from:";
            this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // ddPlayerMergeFrom
            // 
            this.ddPlayerMergeFrom.Image = null;
            this.ddPlayerMergeFrom.Location = new System.Drawing.Point(197, 147);
            this.ddPlayerMergeFrom.Name = "ddPlayerMergeFrom";
            this.ddPlayerMergeFrom.Size = new System.Drawing.Size(142, 23);
            this.ddPlayerMergeFrom.TabIndex = 30;
            this.ddPlayerMergeFrom.TextChanged += new System.EventHandler(this.ddPlayerMergeFrom_TextChanged);
            this.ddPlayerMergeFrom.Click += new System.EventHandler(this.ddPlayerMergeFrom_Click);
            // 
            // grpPlayers
            // 
            this.grpPlayers.BackColor = System.Drawing.Color.Transparent;
            this.grpPlayers.Image = null;
            this.grpPlayers.Location = new System.Drawing.Point(22, 120);
            this.grpPlayers.Name = "grpPlayers";
            this.grpPlayers.Size = new System.Drawing.Size(330, 91);
            this.grpPlayers.TabIndex = 6;
            this.grpPlayers.Text = "Select Players for merging battle data";
            // 
            // btnStart
            // 
            this.btnStart.BlackButton = false;
            this.btnStart.Checked = false;
            this.btnStart.Image = null;
            this.btnStart.Location = new System.Drawing.Point(284, 265);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(70, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.ToolTipText = "";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblProgressStatus
            // 
            this.lblProgressStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblProgressStatus.Dimmed = false;
            this.lblProgressStatus.Image = null;
            this.lblProgressStatus.Location = new System.Drawing.Point(24, 265);
            this.lblProgressStatus.Name = "lblProgressStatus";
            this.lblProgressStatus.Size = new System.Drawing.Size(254, 23);
            this.lblProgressStatus.TabIndex = 4;
            this.lblProgressStatus.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badProgressBar
            // 
            this.badProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.badProgressBar.Image = null;
            this.badProgressBar.Location = new System.Drawing.Point(23, 230);
            this.badProgressBar.Name = "badProgressBar";
            this.badProgressBar.ProgressBarColorMode = false;
            this.badProgressBar.ProgressBarMargins = 2;
            this.badProgressBar.ProgressBarShowPercentage = false;
            this.badProgressBar.Size = new System.Drawing.Size(330, 23);
            this.badProgressBar.TabIndex = 3;
            this.badProgressBar.Text = "badProgressBar1";
            this.badProgressBar.Value = 0D;
            this.badProgressBar.ValueMax = 100D;
            this.badProgressBar.ValueMin = 0D;
            // 
            // MergePlayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 307);
            this.Controls.Add(this.FormTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MergePlayers";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MergePlayers";
            this.Load += new System.EventHandler(this.MergePlayers_Load);
            this.FormTheme.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BadForm FormTheme;
        private BadGroupBox grpPlayers;
        private BadButton btnStart;
        private BadLabel lblProgressStatus;
        private BadProgressBar badProgressBar;
        private BadLabel badLabel1;
        private BadDropDownBox ddPlayerMergeTo;
        private BadLabel badLabel2;
        private BadDropDownBox ddPlayerMergeFrom;
        private BadLabel badLabel3;
        private BadGroupBox badGroupBox1;
    }
}