namespace WinApp.Forms.Settings
{
    partial class AppSettingsLayout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppSettingsLayout));
            this.badLabel2 = new BadLabel();
            this.chkNotifyIconFormExitToMinimize = new BadCheckBox();
            this.chkNotifyIconUse = new BadCheckBox();
            this.badGroupBox2 = new BadGroupBox();
            this.chkSmallMasteryBadgeIcons = new BadCheckBox();
            this.ddFontSize = new BadDropDownBox();
            this.badLabel1 = new BadLabel();
            this.chkBattleTotalsPosition = new BadCheckBox();
            this.badGroupBox1 = new BadGroupBox();
            this.btnCancel = new BadButton();
            this.btnSave = new BadButton();
            this.SuspendLayout();
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(17, 22);
            this.badLabel2.Name = "badLabel2";
            this.badLabel2.Size = new System.Drawing.Size(242, 23);
            this.badLabel2.TabIndex = 23;
            this.badLabel2.Text = "Changes will apply for next application startup";
            this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // chkNotifyIconFormExitToMinimize
            // 
            this.chkNotifyIconFormExitToMinimize.BackColor = System.Drawing.Color.Transparent;
            this.chkNotifyIconFormExitToMinimize.Checked = false;
            this.chkNotifyIconFormExitToMinimize.Image = ((System.Drawing.Image)(resources.GetObject("chkNotifyIconFormExitToMinimize.Image")));
            this.chkNotifyIconFormExitToMinimize.Location = new System.Drawing.Point(17, 80);
            this.chkNotifyIconFormExitToMinimize.Name = "chkNotifyIconFormExitToMinimize";
            this.chkNotifyIconFormExitToMinimize.Size = new System.Drawing.Size(268, 23);
            this.chkNotifyIconFormExitToMinimize.TabIndex = 22;
            this.chkNotifyIconFormExitToMinimize.Text = "Minimize  when closing application";
            this.chkNotifyIconFormExitToMinimize.Click += new System.EventHandler(this.chkNotifyIconFormExitToMinimize_Click);
            // 
            // chkNotifyIconUse
            // 
            this.chkNotifyIconUse.BackColor = System.Drawing.Color.Transparent;
            this.chkNotifyIconUse.Checked = false;
            this.chkNotifyIconUse.Image = ((System.Drawing.Image)(resources.GetObject("chkNotifyIconUse.Image")));
            this.chkNotifyIconUse.Location = new System.Drawing.Point(17, 51);
            this.chkNotifyIconUse.Name = "chkNotifyIconUse";
            this.chkNotifyIconUse.Size = new System.Drawing.Size(140, 23);
            this.chkNotifyIconUse.TabIndex = 21;
            this.chkNotifyIconUse.Text = "Use system tray icon";
            this.chkNotifyIconUse.Click += new System.EventHandler(this.chkNotifyIconUse_Click);
            // 
            // badGroupBox2
            // 
            this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox2.Image = null;
            this.badGroupBox2.Location = new System.Drawing.Point(1, 1);
            this.badGroupBox2.Name = "badGroupBox2";
            this.badGroupBox2.Size = new System.Drawing.Size(445, 115);
            this.badGroupBox2.TabIndex = 20;
            this.badGroupBox2.Text = "Application Behavior";
            // 
            // chkSmallMasteryBadgeIcons
            // 
            this.chkSmallMasteryBadgeIcons.BackColor = System.Drawing.Color.Transparent;
            this.chkSmallMasteryBadgeIcons.Checked = false;
            this.chkSmallMasteryBadgeIcons.Image = ((System.Drawing.Image)(resources.GetObject("chkSmallMasteryBadgeIcons.Image")));
            this.chkSmallMasteryBadgeIcons.Location = new System.Drawing.Point(17, 217);
            this.chkSmallMasteryBadgeIcons.Name = "chkSmallMasteryBadgeIcons";
            this.chkSmallMasteryBadgeIcons.Size = new System.Drawing.Size(193, 23);
            this.chkSmallMasteryBadgeIcons.TabIndex = 19;
            this.chkSmallMasteryBadgeIcons.Text = "Use small mastery badge icons ";
            this.chkSmallMasteryBadgeIcons.Click += new System.EventHandler(this.chkSmallMasteryBadgeIcons_Click);
            // 
            // ddFontSize
            // 
            this.ddFontSize.Image = null;
            this.ddFontSize.Location = new System.Drawing.Point(108, 155);
            this.ddFontSize.Name = "ddFontSize";
            this.ddFontSize.Size = new System.Drawing.Size(86, 23);
            this.ddFontSize.TabIndex = 18;
            this.ddFontSize.TextChanged += new System.EventHandler(this.ddFontSize_TextChanged);
            this.ddFontSize.Click += new System.EventHandler(this.ddFontSize_Click);
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(17, 155);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(81, 23);
            this.badLabel1.TabIndex = 17;
            this.badLabel1.Text = "Grid font size";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // chkBattleTotalsPosition
            // 
            this.chkBattleTotalsPosition.BackColor = System.Drawing.Color.Transparent;
            this.chkBattleTotalsPosition.Checked = false;
            this.chkBattleTotalsPosition.Image = ((System.Drawing.Image)(resources.GetObject("chkBattleTotalsPosition.Image")));
            this.chkBattleTotalsPosition.Location = new System.Drawing.Point(17, 188);
            this.chkBattleTotalsPosition.Name = "chkBattleTotalsPosition";
            this.chkBattleTotalsPosition.Size = new System.Drawing.Size(268, 23);
            this.chkBattleTotalsPosition.TabIndex = 16;
            this.chkBattleTotalsPosition.Text = "Show battle average and totals at grid top";
            this.chkBattleTotalsPosition.Click += new System.EventHandler(this.chkBattleTotalsPosition_Click);
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(1, 132);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(445, 122);
            this.badGroupBox1.TabIndex = 15;
            this.badGroupBox1.Text = "Grid Settings";
            // 
            // btnCancel
            // 
            this.btnCancel.BlackButton = false;
            this.btnCancel.Checked = false;
            this.btnCancel.Image = null;
            this.btnCancel.Location = new System.Drawing.Point(375, 271);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTipText = "";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BlackButton = false;
            this.btnSave.Checked = false;
            this.btnSave.Image = null;
            this.btnSave.Location = new System.Drawing.Point(298, 271);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.ToolTipText = "";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AppSettingsLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.badLabel2);
            this.Controls.Add(this.chkNotifyIconFormExitToMinimize);
            this.Controls.Add(this.chkNotifyIconUse);
            this.Controls.Add(this.badGroupBox2);
            this.Controls.Add(this.chkSmallMasteryBadgeIcons);
            this.Controls.Add(this.ddFontSize);
            this.Controls.Add(this.badLabel1);
            this.Controls.Add(this.chkBattleTotalsPosition);
            this.Controls.Add(this.badGroupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Name = "AppSettingsLayout";
            this.Size = new System.Drawing.Size(460, 307);
            this.Load += new System.EventHandler(this.AppSettingsLayout_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BadLabel badLabel2;
        private BadCheckBox chkNotifyIconFormExitToMinimize;
        private BadCheckBox chkNotifyIconUse;
        private BadGroupBox badGroupBox2;
        private BadCheckBox chkSmallMasteryBadgeIcons;
        private BadDropDownBox ddFontSize;
        private BadLabel badLabel1;
        private BadCheckBox chkBattleTotalsPosition;
        private BadGroupBox badGroupBox1;
        private BadButton btnCancel;
        private BadButton btnSave;
    }
}
