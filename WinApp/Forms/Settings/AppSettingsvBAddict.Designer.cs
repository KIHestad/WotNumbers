namespace WinApp.Forms.Settings
{
    partial class AppSettingsvBAddict
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppSettingsvBAddict));
            this.linkVbAddict = new System.Windows.Forms.LinkLabel();
            this.toolTipShowvBAddictIcon = new System.Windows.Forms.ToolTip(this.components);
            this.chkShowvbAddictIcon = new BadCheckBox();
            this.chkActivateAutoReplayUpload = new BadCheckBox();
            this.btnCancel = new BadButton();
            this.badLabel2 = new BadLabel();
            this.txtToken = new BadTextBox();
            this.badLabel1 = new BadLabel();
            this.chkActivateAutoUpload = new BadCheckBox();
            this.btnUploadDossier = new BadButton();
            this.btnTestConnection = new BadButton();
            this.btnSaveSettings = new BadButton();
            this.group_vBAddict_Settings = new BadGroupBox();
            this.badGroupBox2 = new BadGroupBox();
            this.SuspendLayout();
            // 
            // linkVbAddict
            // 
            this.linkVbAddict.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.linkVbAddict.AutoSize = true;
            this.linkVbAddict.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.linkVbAddict.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkVbAddict.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.linkVbAddict.Location = new System.Drawing.Point(267, 154);
            this.linkVbAddict.Name = "linkVbAddict";
            this.linkVbAddict.Size = new System.Drawing.Size(161, 13);
            this.linkVbAddict.TabIndex = 23;
            this.linkVbAddict.TabStop = true;
            this.linkVbAddict.Text = "Create and view your token here";
            this.linkVbAddict.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.linkVbAddict.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkVbAddict_LinkClicked);
            // 
            // chkShowvbAddictIcon
            // 
            this.chkShowvbAddictIcon.BackColor = System.Drawing.Color.Transparent;
            this.chkShowvbAddictIcon.Checked = false;
            this.chkShowvbAddictIcon.Image = ((System.Drawing.Image)(resources.GetObject("chkShowvbAddictIcon.Image")));
            this.chkShowvbAddictIcon.Location = new System.Drawing.Point(-4, 271);
            this.chkShowvbAddictIcon.Name = "chkShowvbAddictIcon";
            this.chkShowvbAddictIcon.Size = new System.Drawing.Size(213, 23);
            this.chkShowvbAddictIcon.TabIndex = 28;
            this.chkShowvbAddictIcon.Text = "Show vbAddict icon on main toolbar";
            this.chkShowvbAddictIcon.Click += new System.EventHandler(this.chkShowvbAddictIcon_Click);
            // 
            // chkActivateAutoReplayUpload
            // 
            this.chkActivateAutoReplayUpload.BackColor = System.Drawing.Color.Transparent;
            this.chkActivateAutoReplayUpload.Checked = false;
            this.chkActivateAutoReplayUpload.Image = ((System.Drawing.Image)(resources.GetObject("chkActivateAutoReplayUpload.Image")));
            this.chkActivateAutoReplayUpload.Location = new System.Drawing.Point(12, 221);
            this.chkActivateAutoReplayUpload.Name = "chkActivateAutoReplayUpload";
            this.chkActivateAutoReplayUpload.Size = new System.Drawing.Size(411, 23);
            this.chkActivateAutoReplayUpload.TabIndex = 27;
            this.chkActivateAutoReplayUpload.Text = "Auto upload replays to vBAddict (replay files)";
            this.chkActivateAutoReplayUpload.Click += new System.EventHandler(this.chkActivateAutoReplayUpload_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BlackButton = false;
            this.btnCancel.Checked = false;
            this.btnCancel.Enabled = false;
            this.btnCancel.Image = null;
            this.btnCancel.Location = new System.Drawing.Point(375, 271);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 23);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTipText = "";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(17, 24);
            this.badLabel2.Name = "badLabel2";
            this.badLabel2.Size = new System.Drawing.Size(428, 48);
            this.badLabel2.TabIndex = 25;
            this.badLabel2.Text = "Please test connection and dossier upload to vBAddict. A message box will display" +
    " the result. If you have enabled secure access to vBAddict, add token below.";
            this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // txtToken
            // 
            this.txtToken.HasFocus = false;
            this.txtToken.HideBorder = false;
            this.txtToken.Image = null;
            this.txtToken.Location = new System.Drawing.Point(17, 172);
            this.txtToken.MultilineAllow = false;
            this.txtToken.Name = "txtToken";
            this.txtToken.PasswordChar = '\0';
            this.txtToken.ReadOnly = false;
            this.txtToken.Size = new System.Drawing.Size(406, 23);
            this.txtToken.TabIndex = 22;
            this.txtToken.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtToken.ToolTipText = "";
            this.txtToken.TextChanged += new System.EventHandler(this.txtToken_TextChanged);
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(17, 149);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(66, 23);
            this.badLabel1.TabIndex = 21;
            this.badLabel1.Text = "Token:";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // chkActivateAutoUpload
            // 
            this.chkActivateAutoUpload.BackColor = System.Drawing.Color.Transparent;
            this.chkActivateAutoUpload.Checked = false;
            this.chkActivateAutoUpload.Image = ((System.Drawing.Image)(resources.GetObject("chkActivateAutoUpload.Image")));
            this.chkActivateAutoUpload.Location = new System.Drawing.Point(12, 200);
            this.chkActivateAutoUpload.Name = "chkActivateAutoUpload";
            this.chkActivateAutoUpload.Size = new System.Drawing.Size(411, 23);
            this.chkActivateAutoUpload.TabIndex = 20;
            this.chkActivateAutoUpload.Text = "Auto upload battle result to vBAddict (dossier and battle files)";
            this.chkActivateAutoUpload.Click += new System.EventHandler(this.chkActivateAutoUpload_Click);
            // 
            // btnUploadDossier
            // 
            this.btnUploadDossier.BlackButton = false;
            this.btnUploadDossier.Checked = false;
            this.btnUploadDossier.Image = null;
            this.btnUploadDossier.Location = new System.Drawing.Point(325, 71);
            this.btnUploadDossier.Name = "btnUploadDossier";
            this.btnUploadDossier.Size = new System.Drawing.Size(103, 23);
            this.btnUploadDossier.TabIndex = 19;
            this.btnUploadDossier.Text = "Upload Dossier";
            this.btnUploadDossier.ToolTipText = "";
            this.btnUploadDossier.Click += new System.EventHandler(this.btnUploadDossier_Click);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.BlackButton = false;
            this.btnTestConnection.Checked = false;
            this.btnTestConnection.Image = null;
            this.btnTestConnection.Location = new System.Drawing.Point(210, 71);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(109, 23);
            this.btnTestConnection.TabIndex = 18;
            this.btnTestConnection.Text = "Connection Test";
            this.btnTestConnection.ToolTipText = "";
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.BlackButton = false;
            this.btnSaveSettings.Checked = false;
            this.btnSaveSettings.Enabled = false;
            this.btnSaveSettings.Image = null;
            this.btnSaveSettings.Location = new System.Drawing.Point(298, 271);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(70, 23);
            this.btnSaveSettings.TabIndex = 17;
            this.btnSaveSettings.Text = "Save";
            this.btnSaveSettings.ToolTipText = "";
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // group_vBAddict_Settings
            // 
            this.group_vBAddict_Settings.BackColor = System.Drawing.Color.Transparent;
            this.group_vBAddict_Settings.Image = null;
            this.group_vBAddict_Settings.Location = new System.Drawing.Point(1, 125);
            this.group_vBAddict_Settings.Name = "group_vBAddict_Settings";
            this.group_vBAddict_Settings.Size = new System.Drawing.Size(445, 129);
            this.group_vBAddict_Settings.TabIndex = 16;
            this.group_vBAddict_Settings.Text = "Settings for current player";
            // 
            // badGroupBox2
            // 
            this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox2.Image = null;
            this.badGroupBox2.Location = new System.Drawing.Point(1, 1);
            this.badGroupBox2.Name = "badGroupBox2";
            this.badGroupBox2.Size = new System.Drawing.Size(445, 108);
            this.badGroupBox2.TabIndex = 24;
            this.badGroupBox2.Text = "Test connection / upload";
            // 
            // AppSettingsvBAddict
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.chkShowvbAddictIcon);
            this.Controls.Add(this.chkActivateAutoReplayUpload);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.badLabel2);
            this.Controls.Add(this.linkVbAddict);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(this.badLabel1);
            this.Controls.Add(this.chkActivateAutoUpload);
            this.Controls.Add(this.btnUploadDossier);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.btnSaveSettings);
            this.Controls.Add(this.group_vBAddict_Settings);
            this.Controls.Add(this.badGroupBox2);
            this.Name = "AppSettingsvBAddict";
            this.Size = new System.Drawing.Size(456, 307);
            this.Load += new System.EventHandler(this.vBAddict_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BadLabel badLabel2;
        private System.Windows.Forms.LinkLabel linkVbAddict;
        private BadTextBox txtToken;
        private BadLabel badLabel1;
        private BadCheckBox chkActivateAutoUpload;
        private BadButton btnUploadDossier;
        private BadButton btnTestConnection;
        private BadButton btnSaveSettings;
        private BadGroupBox group_vBAddict_Settings;
        private BadGroupBox badGroupBox2;
        private BadButton btnCancel;
        private BadCheckBox chkActivateAutoReplayUpload;
        private BadCheckBox chkShowvbAddictIcon;
        private System.Windows.Forms.ToolTip toolTipShowvBAddictIcon;
    }
}
