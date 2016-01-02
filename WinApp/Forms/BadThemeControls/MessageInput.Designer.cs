namespace WinApp.Forms
{
    partial class MessageInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageInput));
            this.MessageTheme = new BadForm();
            this.txtText = new BadTextBox();
            this.lblText = new BadLabel();
            this.btnCancel = new BadButton();
            this.btnOK = new BadButton();
            this.MessageTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // MessageTheme
            // 
            this.MessageTheme.Controls.Add(this.txtText);
            this.MessageTheme.Controls.Add(this.lblText);
            this.MessageTheme.Controls.Add(this.btnCancel);
            this.MessageTheme.Controls.Add(this.btnOK);
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
            this.MessageTheme.Size = new System.Drawing.Size(321, 172);
            this.MessageTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("MessageTheme.SystemExitImage")));
            this.MessageTheme.SystemMaximizeImage = null;
            this.MessageTheme.SystemMinimizeImage = null;
            this.MessageTheme.TabIndex = 17;
            this.MessageTheme.TabStop = false;
            this.MessageTheme.Text = "Message";
            this.MessageTheme.TitleHeight = 26;
            // 
            // txtText
            // 
            this.txtText.HasFocus = true;
            this.txtText.Image = null;
            this.txtText.Location = new System.Drawing.Point(24, 69);
            this.txtText.MultilineAllow = false;
            this.txtText.Name = "txtText";
            this.txtText.PasswordChar = '\0';
            this.txtText.ReadOnly = false;
            this.txtText.Size = new System.Drawing.Size(272, 26);
            this.txtText.TabIndex = 0;
            this.txtText.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtText.ToolTipText = "";
            // 
            // lblText
            // 
            this.lblText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblText.Dimmed = false;
            this.lblText.Image = null;
            this.lblText.Location = new System.Drawing.Point(22, 47);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(274, 23);
            this.lblText.TabIndex = 21;
            this.lblText.TabStop = false;
            this.lblText.Text = "Input text:";
            this.lblText.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.BlackButton = false;
            this.btnCancel.Checked = false;
            this.btnCancel.Image = null;
            this.btnCancel.Location = new System.Drawing.Point(173, 121);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTipText = "";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.BlackButton = false;
            this.btnOK.Checked = false;
            this.btnOK.Image = null;
            this.btnOK.Location = new System.Drawing.Point(77, 121);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.ToolTipText = "";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // MessageInput
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
            this.Name = "MessageInput";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MessageBox";
            this.Load += new System.EventHandler(this.Message_Load);
            this.Shown += new System.EventHandler(this.MessageInput_Shown);
            this.MessageTheme.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BadForm MessageTheme;
		private BadButton btnOK;
        private BadTextBox txtText;
        private BadLabel lblText;
        private BadButton btnCancel;
    }
}