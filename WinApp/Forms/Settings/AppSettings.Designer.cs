namespace WinApp.Forms.Settings
{
    partial class AppSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppSettings));
            this.AppSettingsTheme = new BadForm();
            this.btnTab7 = new BadButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnTab6 = new BadButton();
            this.btnTab2 = new BadButton();
            this.btnTab5 = new BadButton();
            this.btnTab4 = new BadButton();
            this.btnTab3 = new BadButton();
            this.btnTab1 = new BadButton();
            this.grpMain = new BadGroupBox();
            this.AppSettingsTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // AppSettingsTheme
            // 
            this.AppSettingsTheme.Controls.Add(this.btnTab7);
            this.AppSettingsTheme.Controls.Add(this.pnlMain);
            this.AppSettingsTheme.Controls.Add(this.btnTab6);
            this.AppSettingsTheme.Controls.Add(this.btnTab2);
            this.AppSettingsTheme.Controls.Add(this.btnTab5);
            this.AppSettingsTheme.Controls.Add(this.btnTab4);
            this.AppSettingsTheme.Controls.Add(this.btnTab3);
            this.AppSettingsTheme.Controls.Add(this.btnTab1);
            this.AppSettingsTheme.Controls.Add(this.grpMain);
            this.AppSettingsTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AppSettingsTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AppSettingsTheme.FormExitAsMinimize = false;
            this.AppSettingsTheme.FormFooter = false;
            this.AppSettingsTheme.FormFooterHeight = 26;
            this.AppSettingsTheme.FormInnerBorder = 3;
            this.AppSettingsTheme.FormMargin = 0;
            this.AppSettingsTheme.Image = null;
            this.AppSettingsTheme.Location = new System.Drawing.Point(0, 0);
            this.AppSettingsTheme.MainArea = mainAreaClass1;
            this.AppSettingsTheme.Name = "AppSettingsTheme";
            this.AppSettingsTheme.Resizable = false;
            this.AppSettingsTheme.Size = new System.Drawing.Size(525, 426);
            this.AppSettingsTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("AppSettingsTheme.SystemExitImage")));
            this.AppSettingsTheme.SystemMaximizeImage = null;
            this.AppSettingsTheme.SystemMinimizeImage = null;
            this.AppSettingsTheme.TabIndex = 0;
            this.AppSettingsTheme.Text = "Application Settings";
            this.AppSettingsTheme.TitleHeight = 26;
            // 
            // btnTab7
            // 
            this.btnTab7.BlackButton = false;
            this.btnTab7.Checked = false;
            this.btnTab7.Image = null;
            this.btnTab7.Location = new System.Drawing.Point(84, 47);
            this.btnTab7.Name = "btnTab7";
            this.btnTab7.Size = new System.Drawing.Size(60, 23);
            this.btnTab7.TabIndex = 17;
            this.btnTab7.Tag = "7";
            this.btnTab7.Text = "Options";
            this.btnTab7.ToolTipText = "Import from WOT Statistics";
            this.btnTab7.Click += new System.EventHandler(this.SelectTab_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pnlMain.Location = new System.Drawing.Point(39, 90);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(462, 307);
            this.pnlMain.TabIndex = 16;
            // 
            // btnTab6
            // 
            this.btnTab6.BlackButton = false;
            this.btnTab6.Checked = false;
            this.btnTab6.Image = null;
            this.btnTab6.Location = new System.Drawing.Point(214, 47);
            this.btnTab6.Name = "btnTab6";
            this.btnTab6.Size = new System.Drawing.Size(60, 23);
            this.btnTab6.TabIndex = 15;
            this.btnTab6.Tag = "6";
            this.btnTab6.Text = "Replays";
            this.btnTab6.ToolTipText = "Replay Settings";
            this.btnTab6.Click += new System.EventHandler(this.SelectTab_Click);
            // 
            // btnTab2
            // 
            this.btnTab2.BlackButton = false;
            this.btnTab2.Checked = false;
            this.btnTab2.Image = null;
            this.btnTab2.Location = new System.Drawing.Point(149, 47);
            this.btnTab2.Name = "btnTab2";
            this.btnTab2.Size = new System.Drawing.Size(60, 23);
            this.btnTab2.TabIndex = 13;
            this.btnTab2.Tag = "2";
            this.btnTab2.Text = "Layout";
            this.btnTab2.ToolTipText = "Layout Settings";
            this.btnTab2.Click += new System.EventHandler(this.SelectTab_Click);
            // 
            // btnTab5
            // 
            this.btnTab5.BlackButton = false;
            this.btnTab5.Checked = false;
            this.btnTab5.Image = null;
            this.btnTab5.Location = new System.Drawing.Point(409, 47);
            this.btnTab5.Name = "btnTab5";
            this.btnTab5.Size = new System.Drawing.Size(60, 23);
            this.btnTab5.TabIndex = 12;
            this.btnTab5.Tag = "5";
            this.btnTab5.Text = "Import";
            this.btnTab5.ToolTipText = "Import from WOT Statistics";
            this.btnTab5.Visible = false;
            this.btnTab5.Click += new System.EventHandler(this.SelectTab_Click);
            // 
            // btnTab4
            // 
            this.btnTab4.BlackButton = false;
            this.btnTab4.Checked = false;
            this.btnTab4.Image = null;
            this.btnTab4.Location = new System.Drawing.Point(344, 47);
            this.btnTab4.Name = "btnTab4";
            this.btnTab4.Size = new System.Drawing.Size(60, 23);
            this.btnTab4.TabIndex = 11;
            this.btnTab4.Tag = "4";
            this.btnTab4.Text = "vBAddict";
            this.btnTab4.ToolTipText = "vBAddict uploads";
            this.btnTab4.Click += new System.EventHandler(this.SelectTab_Click);
            // 
            // btnTab3
            // 
            this.btnTab3.BlackButton = false;
            this.btnTab3.Checked = false;
            this.btnTab3.Image = null;
            this.btnTab3.Location = new System.Drawing.Point(279, 47);
            this.btnTab3.Name = "btnTab3";
            this.btnTab3.Size = new System.Drawing.Size(60, 23);
            this.btnTab3.TabIndex = 10;
            this.btnTab3.Tag = "3";
            this.btnTab3.Text = "WoT";
            this.btnTab3.ToolTipText = "World of Tanks Game Settings";
            this.btnTab3.Click += new System.EventHandler(this.SelectTab_Click);
            // 
            // btnTab1
            // 
            this.btnTab1.BlackButton = false;
            this.btnTab1.Checked = true;
            this.btnTab1.Image = null;
            this.btnTab1.Location = new System.Drawing.Point(19, 47);
            this.btnTab1.Name = "btnTab1";
            this.btnTab1.Size = new System.Drawing.Size(60, 23);
            this.btnTab1.TabIndex = 9;
            this.btnTab1.Tag = "1";
            this.btnTab1.Text = "Main";
            this.btnTab1.ToolTipText = "Main Settings";
            this.btnTab1.Click += new System.EventHandler(this.SelectTab_Click);
            // 
            // grpMain
            // 
            this.grpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMain.BackColor = System.Drawing.Color.Transparent;
            this.grpMain.Image = null;
            this.grpMain.Location = new System.Drawing.Point(19, 63);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(486, 340);
            this.grpMain.TabIndex = 8;
            // 
            // AppSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 426);
            this.Controls.Add(this.AppSettingsTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "AppSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AppSettings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AppSettings_FormClosing);
            this.Load += new System.EventHandler(this.AppSettings_Load);
            this.AppSettingsTheme.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BadForm AppSettingsTheme;
        private BadButton btnTab2;
        private BadButton btnTab5;
        private BadButton btnTab4;
        private BadButton btnTab3;
        private BadButton btnTab1;
        private BadGroupBox grpMain;
        private BadButton btnTab6;
        private System.Windows.Forms.Panel pnlMain;
        private BadButton btnTab7;

    }
}