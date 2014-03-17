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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Message));
            this.picClose = new System.Windows.Forms.PictureBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.picResize = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResize)).BeginInit();
            this.SuspendLayout();
            // 
            // picClose
            // 
            this.picClose.Image = ((System.Drawing.Image)(resources.GetObject("picClose.Image")));
            this.picClose.Location = new System.Drawing.Point(251, 0);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(34, 26);
            this.picClose.TabIndex = 2;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            this.picClose.MouseLeave += new System.EventHandler(this.picClose_MouseLeave);
            this.picClose.MouseHover += new System.EventHandler(this.picClose_MouseHover);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.panelTop.Controls.Add(this.picClose);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Location = new System.Drawing.Point(10, 6);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(286, 26);
            this.panelTop.TabIndex = 15;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panelTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(156)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(58, 17);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Message";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.lblTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.ForeColor = System.Drawing.Color.Silver;
            this.txtMessage.Location = new System.Drawing.Point(25, 41);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.Size = new System.Drawing.Size(253, 76);
            this.txtMessage.TabIndex = 16;
            this.txtMessage.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(224, 151);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(72, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Visible = false;
            // 
            // picResize
            // 
            this.picResize.Image = ((System.Drawing.Image)(resources.GetObject("picResize.Image")));
            this.picResize.Location = new System.Drawing.Point(267, 72);
            this.picResize.Name = "picResize";
            this.picResize.Size = new System.Drawing.Size(28, 26);
            this.picResize.TabIndex = 17;
            this.picResize.TabStop = false;
            this.picResize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picResize_MouseDown);
            this.picResize.MouseLeave += new System.EventHandler(this.picResize_MouseLeave);
            this.picResize.MouseHover += new System.EventHandler(this.picResize_MouseHover);
            this.picResize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picResize_MouseMove);
            this.picResize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picResize_MouseUp);
            // 
            // Message
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(308, 140);
            this.Controls.Add(this.picResize);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Message";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MessageBox";
            this.Load += new System.EventHandler(this.Message_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MessageBox_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox picResize;
    }
}