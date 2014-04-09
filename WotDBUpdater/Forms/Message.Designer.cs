namespace WotDBUpdater.Forms
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
			this.badForm1 = new BadForm();
			this.cmdClose = new BadButton();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.badForm1.SuspendLayout();
			this.SuspendLayout();
			// 
			// badForm1
			// 
			this.badForm1.Controls.Add(this.cmdClose);
			this.badForm1.Controls.Add(this.txtMessage);
			this.badForm1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.badForm1.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.badForm1.FormFooter = false;
			this.badForm1.FormFooterHeight = 26;
			this.badForm1.FormMargin = 0;
			this.badForm1.Image = null;
			this.badForm1.Location = new System.Drawing.Point(0, 0);
			this.badForm1.MainArea = mainAreaClass1;
			this.badForm1.Name = "badForm1";
			this.badForm1.Resizable = true;
			this.badForm1.Size = new System.Drawing.Size(304, 141);
			this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
			this.badForm1.SystemMaximizeImage = null;
			this.badForm1.SystemMinimizeImage = null;
			this.badForm1.TabIndex = 17;
			this.badForm1.Text = "Message";
			this.badForm1.TitleHeight = 26;
			this.badForm1.Resize += new System.EventHandler(this.badForm1_Resize);
			// 
			// cmdClose
			// 
			this.cmdClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.cmdClose.Image = null;
			this.cmdClose.Location = new System.Drawing.Point(121, 106);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(75, 23);
			this.cmdClose.TabIndex = 17;
			this.cmdClose.Text = "Close";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// txtMessage
			// 
			this.txtMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtMessage.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMessage.ForeColor = System.Drawing.Color.Silver;
			this.txtMessage.Location = new System.Drawing.Point(25, 43);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.ReadOnly = true;
			this.txtMessage.Size = new System.Drawing.Size(253, 76);
			this.txtMessage.TabIndex = 16;
			this.txtMessage.TabStop = false;
			// 
			// Message
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.ClientSize = new System.Drawing.Size(304, 141);
			this.Controls.Add(this.badForm1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(304, 141);
			this.Name = "Message";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MessageBox";
			this.Load += new System.EventHandler(this.Message_Load);
			this.badForm1.ResumeLayout(false);
			this.badForm1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.TextBox txtMessage;
		private BadForm badForm1;
		private BadButton cmdClose;
    }
}