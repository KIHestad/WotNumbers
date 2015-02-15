namespace WinApp.Forms
{
	partial class UploadTovBAddict
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadTovBAddict));
			this.badForm1 = new BadForm();
			this.badLabel2 = new BadLabel();
			this.linkVbAddict = new System.Windows.Forms.LinkLabel();
			this.txtToken = new BadTextBox();
			this.badLabel1 = new BadLabel();
			this.chkActivateAutoUpload = new BadCheckBox();
			this.btnUploadDossier = new BadButton();
			this.btnTestConnection = new BadButton();
			this.btnSaveSettings = new BadButton();
			this.badGroupBox1 = new BadGroupBox();
			this.badGroupBox2 = new BadGroupBox();
			this.badForm1.SuspendLayout();
			this.SuspendLayout();
			// 
			// badForm1
			// 
			this.badForm1.Controls.Add(this.badLabel2);
			this.badForm1.Controls.Add(this.linkVbAddict);
			this.badForm1.Controls.Add(this.txtToken);
			this.badForm1.Controls.Add(this.badLabel1);
			this.badForm1.Controls.Add(this.chkActivateAutoUpload);
			this.badForm1.Controls.Add(this.btnUploadDossier);
			this.badForm1.Controls.Add(this.btnTestConnection);
			this.badForm1.Controls.Add(this.btnSaveSettings);
			this.badForm1.Controls.Add(this.badGroupBox1);
			this.badForm1.Controls.Add(this.badGroupBox2);
			this.badForm1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.badForm1.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.badForm1.FormExitAsMinimize = false;
			this.badForm1.FormFooter = false;
			this.badForm1.FormFooterHeight = 26;
			this.badForm1.FormInnerBorder = 3;
			this.badForm1.FormMargin = 0;
			this.badForm1.Image = null;
			this.badForm1.Location = new System.Drawing.Point(0, 0);
			this.badForm1.MainArea = mainAreaClass1;
			this.badForm1.Name = "badForm1";
			this.badForm1.Resizable = false;
			this.badForm1.Size = new System.Drawing.Size(375, 319);
			this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
			this.badForm1.SystemMaximizeImage = null;
			this.badForm1.SystemMinimizeImage = null;
			this.badForm1.TabIndex = 0;
			this.badForm1.Text = "Upload to vBAddict Settings";
			this.badForm1.TitleHeight = 26;
			// 
			// badLabel2
			// 
			this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel2.Dimmed = false;
			this.badLabel2.Image = null;
			this.badLabel2.Location = new System.Drawing.Point(155, 217);
			this.badLabel2.Name = "badLabel2";
			this.badLabel2.Size = new System.Drawing.Size(180, 64);
			this.badLabel2.TabIndex = 15;
			this.badLabel2.Text = "For testing connection and upload to vBAddict. Returns an XML-formatted result to" +
    " verify correct token and connection/upload result.";
			this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// linkVbAddict
			// 
			this.linkVbAddict.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.linkVbAddict.AutoSize = true;
			this.linkVbAddict.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.linkVbAddict.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkVbAddict.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.linkVbAddict.Location = new System.Drawing.Point(170, 80);
			this.linkVbAddict.Name = "linkVbAddict";
			this.linkVbAddict.Size = new System.Drawing.Size(161, 13);
			this.linkVbAddict.TabIndex = 13;
			this.linkVbAddict.TabStop = true;
			this.linkVbAddict.Text = "Create and view your token here";
			this.linkVbAddict.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.linkVbAddict.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkVbAddict_LinkClicked);
			// 
			// txtToken
			// 
			this.txtToken.HasFocus = false;
			this.txtToken.Image = null;
			this.txtToken.Location = new System.Drawing.Point(47, 96);
			this.txtToken.MultilineAllow = false;
			this.txtToken.Name = "txtToken";
			this.txtToken.PasswordChar = '\0';
			this.txtToken.ReadOnly = false;
			this.txtToken.Size = new System.Drawing.Size(284, 23);
			this.txtToken.TabIndex = 6;
			this.txtToken.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtToken.ToolTipText = "";
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(45, 75);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(66, 23);
			this.badLabel1.TabIndex = 5;
			this.badLabel1.Text = "Token:";
			this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// chkActivateAutoUpload
			// 
			this.chkActivateAutoUpload.BackColor = System.Drawing.Color.Transparent;
			this.chkActivateAutoUpload.Checked = false;
			this.chkActivateAutoUpload.Image = ((System.Drawing.Image)(resources.GetObject("chkActivateAutoUpload.Image")));
			this.chkActivateAutoUpload.Location = new System.Drawing.Point(42, 132);
			this.chkActivateAutoUpload.Name = "chkActivateAutoUpload";
			this.chkActivateAutoUpload.Size = new System.Drawing.Size(200, 23);
			this.chkActivateAutoUpload.TabIndex = 4;
			this.chkActivateAutoUpload.Text = "Activate Auto Upload to vBAddict";
			// 
			// btnUploadDossier
			// 
			this.btnUploadDossier.BlackButton = false;
			this.btnUploadDossier.Checked = false;
			this.btnUploadDossier.Image = null;
			this.btnUploadDossier.Location = new System.Drawing.Point(45, 253);
			this.btnUploadDossier.Name = "btnUploadDossier";
			this.btnUploadDossier.Size = new System.Drawing.Size(97, 23);
			this.btnUploadDossier.TabIndex = 3;
			this.btnUploadDossier.Text = "Upload Dossier";
			this.btnUploadDossier.ToolTipText = "";
			this.btnUploadDossier.Click += new System.EventHandler(this.btnUploadDossier_Click);
			// 
			// btnTestConnection
			// 
			this.btnTestConnection.BlackButton = false;
			this.btnTestConnection.Checked = false;
			this.btnTestConnection.Image = null;
			this.btnTestConnection.Location = new System.Drawing.Point(45, 222);
			this.btnTestConnection.Name = "btnTestConnection";
			this.btnTestConnection.Size = new System.Drawing.Size(97, 23);
			this.btnTestConnection.TabIndex = 2;
			this.btnTestConnection.Text = "Connection Test";
			this.btnTestConnection.ToolTipText = "";
			this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
			// 
			// btnSaveSettings
			// 
			this.btnSaveSettings.BlackButton = false;
			this.btnSaveSettings.Checked = false;
			this.btnSaveSettings.Image = null;
			this.btnSaveSettings.Location = new System.Drawing.Point(262, 132);
			this.btnSaveSettings.Name = "btnSaveSettings";
			this.btnSaveSettings.Size = new System.Drawing.Size(69, 23);
			this.btnSaveSettings.TabIndex = 1;
			this.btnSaveSettings.Text = "Save";
			this.btnSaveSettings.ToolTipText = "";
			this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(27, 49);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(322, 125);
			this.badGroupBox1.TabIndex = 0;
			this.badGroupBox1.Text = "Auto Upload Settings";
			// 
			// badGroupBox2
			// 
			this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox2.Image = null;
			this.badGroupBox2.Location = new System.Drawing.Point(27, 192);
			this.badGroupBox2.Name = "badGroupBox2";
			this.badGroupBox2.Size = new System.Drawing.Size(322, 102);
			this.badGroupBox2.TabIndex = 14;
			this.badGroupBox2.Text = "Test connection / upload";
			// 
			// UploadTovBAddict
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(375, 319);
			this.Controls.Add(this.badForm1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "UploadTovBAddict";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Upload to vBAddict Settings";
			this.Load += new System.EventHandler(this.UploadTovBAddict_Load);
			this.badForm1.ResumeLayout(false);
			this.badForm1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm badForm1;
		private BadButton btnTestConnection;
		private BadButton btnSaveSettings;
		private BadGroupBox badGroupBox1;
		private BadTextBox txtToken;
		private BadLabel badLabel1;
		private BadCheckBox chkActivateAutoUpload;
		private BadButton btnUploadDossier;
		private System.Windows.Forms.LinkLabel linkVbAddict;
		private BadLabel badLabel2;
		private BadGroupBox badGroupBox2;
	}
}