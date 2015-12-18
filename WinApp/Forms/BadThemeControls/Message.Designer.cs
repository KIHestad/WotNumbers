namespace WinApp.Forms
{
    partial class Message
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
			BadThemeContainerControl.MainAreaClass mainAreaClass1 = new BadThemeContainerControl.MainAreaClass();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Message));
			this.MessageTheme = new BadForm();
			this.btnClose = new BadButton();
			this.btnCancel = new BadButton();
			this.btnOK = new BadButton();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.MessageTheme.SuspendLayout();
			this.SuspendLayout();
			// 
			// MessageTheme
			// 
			this.MessageTheme.Controls.Add(this.btnClose);
			this.MessageTheme.Controls.Add(this.btnCancel);
			this.MessageTheme.Controls.Add(this.btnOK);
			this.MessageTheme.Controls.Add(this.txtMessage);
			this.MessageTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MessageTheme.FormBorderColor = System.Drawing.Color.Black;
			this.MessageTheme.FormFooter = false;
			this.MessageTheme.FormFooterHeight = 26;
			this.MessageTheme.FormInnerBorder = 3;
			this.MessageTheme.FormMargin = 0;
			this.MessageTheme.Image = null;
			this.MessageTheme.Location = new System.Drawing.Point(0, 0);
			this.MessageTheme.MainArea = mainAreaClass1;
			this.MessageTheme.Name = "MessageTheme";
			this.MessageTheme.Resizable = true;
			this.MessageTheme.Size = new System.Drawing.Size(321, 172);
			this.MessageTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("MessageTheme.SystemExitImage")));
			this.MessageTheme.SystemMaximizeImage = null;
			this.MessageTheme.SystemMinimizeImage = null;
			this.MessageTheme.TabIndex = 17;
			this.MessageTheme.Text = "Message";
			this.MessageTheme.TitleHeight = 26;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnClose.Image = null;
			this.btnClose.Location = new System.Drawing.Point(125, 128);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 17;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnCancel.Image = null;
			this.btnCancel.Location = new System.Drawing.Point(173, 128);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 19;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Visible = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnOK.Image = null;
			this.btnOK.Location = new System.Drawing.Point(77, 128);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 18;
			this.btnOK.Text = "OK";
			this.btnOK.Visible = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// txtMessage
			// 
			this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtMessage.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMessage.ForeColor = System.Drawing.Color.Silver;
			this.txtMessage.Location = new System.Drawing.Point(23, 48);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.ReadOnly = true;
			this.txtMessage.Size = new System.Drawing.Size(274, 74);
			this.txtMessage.TabIndex = 16;
			this.txtMessage.TabStop = false;
			// 
			// Message
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.ClientSize = new System.Drawing.Size(321, 172);
			this.Controls.Add(this.MessageTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(304, 141);
			this.Name = "Message";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MessageBox";
			this.Load += new System.EventHandler(this.Message_Load);
			this.MessageTheme.ResumeLayout(false);
			this.MessageTheme.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.TextBox txtMessage;
		private BadForm MessageTheme;
		private BadButton btnClose;
		private BadButton btnCancel;
		private BadButton btnOK;
    }
}