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
            this.components = new System.ComponentModel.Container();
            BadThemeContainerControl.MainAreaClass mainAreaClass1 = new BadThemeContainerControl.MainAreaClass();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Message));
            this.MessageTheme = new BadForm();
            this.btnCopyText = new BadButton();
            this.btn2 = new BadButton();
            this.btn1 = new BadButton();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.MessageTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // MessageTheme
            // 
            this.MessageTheme.Controls.Add(this.btnCopyText);
            this.MessageTheme.Controls.Add(this.btn2);
            this.MessageTheme.Controls.Add(this.btn1);
            this.MessageTheme.Controls.Add(this.txtMessage);
            this.MessageTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageTheme.FormBorderColor = System.Drawing.Color.Black;
            this.MessageTheme.FormExitAsMinimize = false;
            this.MessageTheme.FormFooter = false;
            this.MessageTheme.FormFooterHeight = 26;
            this.MessageTheme.FormInnerBorder = 3;
            this.MessageTheme.FormMargin = 0;
            this.MessageTheme.Image = null;
            this.MessageTheme.Location = new System.Drawing.Point(0, 0);
            this.MessageTheme.MainArea = mainAreaClass1;
            this.MessageTheme.Name = "MessageTheme";
            this.MessageTheme.Resizable = true;
            this.MessageTheme.Size = new System.Drawing.Size(390, 186);
            this.MessageTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("MessageTheme.SystemExitImage")));
            this.MessageTheme.SystemMaximizeImage = null;
            this.MessageTheme.SystemMinimizeImage = null;
            this.MessageTheme.TabIndex = 17;
            this.MessageTheme.Text = "Message";
            this.MessageTheme.TitleHeight = 26;
            // 
            // btnCopyText
            // 
            this.btnCopyText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyText.BlackButton = false;
            this.btnCopyText.Checked = false;
            this.btnCopyText.Image = null;
            this.btnCopyText.Location = new System.Drawing.Point(23, 142);
            this.btnCopyText.Name = "btnCopyText";
            this.btnCopyText.Size = new System.Drawing.Size(75, 23);
            this.btnCopyText.TabIndex = 17;
            this.btnCopyText.Text = "Copy Text";
            this.btnCopyText.ToolTipText = "Copy Message Text to Clipboard";
            this.btnCopyText.Click += new System.EventHandler(this.btnCopyText_Click);
            // 
            // btn2
            // 
            this.btn2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn2.BlackButton = false;
            this.btn2.Checked = false;
            this.btn2.Image = null;
            this.btn2.Location = new System.Drawing.Point(210, 142);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(75, 23);
            this.btn2.TabIndex = 19;
            this.btn2.Text = "Button 2";
            this.btn2.ToolTipText = "";
            this.btn2.Visible = false;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // btn1
            // 
            this.btn1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn1.BlackButton = false;
            this.btn1.Checked = false;
            this.btn1.Image = null;
            this.btn1.Location = new System.Drawing.Point(291, 142);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(75, 23);
            this.btn1.TabIndex = 1;
            this.btn1.Text = "Close";
            this.btn1.ToolTipText = "";
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
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
            this.txtMessage.Size = new System.Drawing.Size(343, 78);
            this.txtMessage.TabIndex = 16;
            this.txtMessage.TabStop = false;
            // 
            // Message
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(390, 186);
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
		private BadButton btnCopyText;
		private BadButton btn2;
		private BadButton btn1;
    }
}