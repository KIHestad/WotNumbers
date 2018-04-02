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
            this.btn2 = new BadButton();
            this.btn1 = new BadButton();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lblStatusRowCount = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.MessageTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // MessageTheme
            // 
            this.MessageTheme.Controls.Add(this.lblStatus);
            this.MessageTheme.Controls.Add(this.lblStatusRowCount);
            this.MessageTheme.Controls.Add(this.btn2);
            this.MessageTheme.Controls.Add(this.btn1);
            this.MessageTheme.Controls.Add(this.txtMessage);
            this.MessageTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageTheme.FormBorderColor = System.Drawing.Color.Black;
            this.MessageTheme.FormExitAsMinimize = false;
            this.MessageTheme.FormFooter = true;
            this.MessageTheme.FormFooterHeight = 26;
            this.MessageTheme.FormInnerBorder = 0;
            this.MessageTheme.FormMargin = 0;
            this.MessageTheme.Image = null;
            this.MessageTheme.Location = new System.Drawing.Point(0, 0);
            this.MessageTheme.MainArea = mainAreaClass1;
            this.MessageTheme.Name = "MessageTheme";
            this.MessageTheme.Resizable = true;
            this.MessageTheme.Size = new System.Drawing.Size(390, 207);
            this.MessageTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("MessageTheme.SystemExitImage")));
            this.MessageTheme.SystemMaximizeImage = null;
            this.MessageTheme.SystemMinimizeImage = null;
            this.MessageTheme.TabIndex = 17;
            this.MessageTheme.Text = "Message";
            this.MessageTheme.TitleHeight = 26;
            this.MessageTheme.Resize += new System.EventHandler(this.MessageTheme_Resize);
            // 
            // btn2
            // 
            this.btn2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn2.BlackButton = false;
            this.btn2.Checked = false;
            this.btn2.Image = null;
            this.btn2.Location = new System.Drawing.Point(216, 138);
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
            this.btn1.Location = new System.Drawing.Point(297, 138);
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
            this.txtMessage.Margin = new System.Windows.Forms.Padding(1);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.Size = new System.Drawing.Size(350, 71);
            this.txtMessage.TabIndex = 16;
            this.txtMessage.TabStop = false;
            // 
            // lblStatusRowCount
            // 
            this.lblStatusRowCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusRowCount.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusRowCount.ForeColor = System.Drawing.Color.DarkGray;
            this.lblStatusRowCount.Location = new System.Drawing.Point(255, 187);
            this.lblStatusRowCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblStatusRowCount.Name = "lblStatusRowCount";
            this.lblStatusRowCount.Size = new System.Drawing.Size(130, 13);
            this.lblStatusRowCount.TabIndex = 21;
            this.lblStatusRowCount.Text = "Form needs resizing";
            this.lblStatusRowCount.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.ForeColor = System.Drawing.Color.DarkGray;
            this.lblStatus.Location = new System.Drawing.Point(6, 187);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(109, 13);
            this.lblStatus.TabIndex = 22;
            this.lblStatus.Text = "Copy text to clipboard";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_Click);
            // 
            // Message
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(390, 207);
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
		private BadButton btn2;
		private BadButton btn1;
        private System.Windows.Forms.Label lblStatusRowCount;
        private System.Windows.Forms.Label lblStatus;
    }
}