namespace WinApp.Forms
{
    partial class Replay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Replay));
            this.badForm1 = new BadForm();
            this.badGroupBox1 = new BadGroupBox();
            this.txtReplayFile = new BadTextBox();
            this.badForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // badForm1
            // 
            this.badForm1.Controls.Add(this.txtReplayFile);
            this.badForm1.Controls.Add(this.badGroupBox1);
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
            this.badForm1.Size = new System.Drawing.Size(513, 242);
            this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
            this.badForm1.SystemMaximizeImage = null;
            this.badForm1.SystemMinimizeImage = null;
            this.badForm1.TabIndex = 0;
            this.badForm1.Text = "Replay";
            this.badForm1.TitleHeight = 26;
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(21, 47);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(467, 122);
            this.badGroupBox1.TabIndex = 0;
            this.badGroupBox1.Text = "Search result for replay file";
            // 
            // txtReplayFile
            // 
            this.txtReplayFile.HasFocus = false;
            this.txtReplayFile.Image = null;
            this.txtReplayFile.Location = new System.Drawing.Point(39, 73);
            this.txtReplayFile.MultilineAllow = true;
            this.txtReplayFile.Name = "txtReplayFile";
            this.txtReplayFile.PasswordChar = '\0';
            this.txtReplayFile.ReadOnly = false;
            this.txtReplayFile.Size = new System.Drawing.Size(432, 54);
            this.txtReplayFile.TabIndex = 1;
            this.txtReplayFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtReplayFile.ToolTipText = "";
            // 
            // Replay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 242);
            this.Controls.Add(this.badForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Replay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Replay";
            this.Shown += new System.EventHandler(this.Replay_Shown);
            this.badForm1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BadForm badForm1;
        private BadGroupBox badGroupBox1;
        private BadTextBox txtReplayFile;
    }
}