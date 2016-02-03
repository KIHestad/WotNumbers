namespace WinApp.Forms.Settings
{
    partial class AppSettingsOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppSettingsOptions));
            this.badLabel3 = new BadLabel();
            this.ddHour = new BadDropDownBox();
            this.badLabel1 = new BadLabel();
            this.badLabel2 = new BadLabel();
            this.chkNotifyIconFormExitToMinimize = new BadCheckBox();
            this.chkNotifyIconUse = new BadCheckBox();
            this.badGroupBox2 = new BadGroupBox();
            this.badGroupBox1 = new BadGroupBox();
            this.btnCancel = new BadButton();
            this.btnSave = new BadButton();
            this.SuspendLayout();
            // 
            // badLabel3
            // 
            this.badLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel3.Dimmed = false;
            this.badLabel3.Image = null;
            this.badLabel3.Location = new System.Drawing.Point(17, 208);
            this.badLabel3.Name = "badLabel3";
            this.badLabel3.Size = new System.Drawing.Size(36, 23);
            this.badLabel3.TabIndex = 26;
            this.badLabel3.Text = "Hour";
            this.badLabel3.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // ddHour
            // 
            this.ddHour.Image = null;
            this.ddHour.Location = new System.Drawing.Point(59, 208);
            this.ddHour.Name = "ddHour";
            this.ddHour.Size = new System.Drawing.Size(54, 23);
            this.ddHour.TabIndex = 25;
            this.ddHour.TextChanged += new System.EventHandler(this.ddHour_TextChanged);
            this.ddHour.Click += new System.EventHandler(this.ddHour_Click);
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(17, 175);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(242, 23);
            this.badLabel1.TabIndex = 24;
            this.badLabel1.Text = "Normally set at WoT server reset";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(17, 26);
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
            this.chkNotifyIconFormExitToMinimize.Location = new System.Drawing.Point(17, 84);
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
            this.chkNotifyIconUse.Location = new System.Drawing.Point(17, 55);
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
            this.badGroupBox2.Size = new System.Drawing.Size(445, 124);
            this.badGroupBox2.TabIndex = 20;
            this.badGroupBox2.Text = "Application Behavior";
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(1, 148);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(445, 106);
            this.badGroupBox1.TabIndex = 15;
            this.badGroupBox1.Text = "Set Hour for New Day Start";
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
            // AppSettingsOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.badLabel3);
            this.Controls.Add(this.ddHour);
            this.Controls.Add(this.badLabel1);
            this.Controls.Add(this.badLabel2);
            this.Controls.Add(this.chkNotifyIconFormExitToMinimize);
            this.Controls.Add(this.chkNotifyIconUse);
            this.Controls.Add(this.badGroupBox2);
            this.Controls.Add(this.badGroupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Name = "AppSettingsOptions";
            this.Size = new System.Drawing.Size(460, 307);
            this.Load += new System.EventHandler(this.AppSettingsLayout_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BadLabel badLabel2;
        private BadCheckBox chkNotifyIconFormExitToMinimize;
        private BadCheckBox chkNotifyIconUse;
        private BadGroupBox badGroupBox2;
        private BadGroupBox badGroupBox1;
        private BadButton btnCancel;
        private BadButton btnSave;
        private BadLabel badLabel1;
        private BadDropDownBox ddHour;
        private BadLabel badLabel3;
    }
}
