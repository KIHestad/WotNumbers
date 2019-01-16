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
            this.btnPlayReplay = new BadButton();
            this.btnShowFolder = new BadButton();
            this.txtFile = new BadTextBox();
            this.txtPath = new BadTextBox();
            this.lblFile = new BadLabel();
            this.lblFolder = new BadLabel();
            this.lblMessage = new BadLabel();
            this.badGroupBox1 = new BadGroupBox();
            this.badForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // badForm1
            // 
            this.badForm1.Controls.Add(this.btnPlayReplay);
            this.badForm1.Controls.Add(this.btnShowFolder);
            this.badForm1.Controls.Add(this.txtFile);
            this.badForm1.Controls.Add(this.txtPath);
            this.badForm1.Controls.Add(this.lblFile);
            this.badForm1.Controls.Add(this.lblFolder);
            this.badForm1.Controls.Add(this.lblMessage);
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
            this.badForm1.Size = new System.Drawing.Size(535, 231);
            this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
            this.badForm1.SystemMaximizeImage = null;
            this.badForm1.SystemMinimizeImage = null;
            this.badForm1.TabIndex = 0;
            this.badForm1.Text = "Replay";
            this.badForm1.TitleHeight = 26;
            // 
            // btnPlayReplay
            // 
            this.btnPlayReplay.BlackButton = false;
            this.btnPlayReplay.Checked = false;
            this.btnPlayReplay.Image = null;
            this.btnPlayReplay.Location = new System.Drawing.Point(345, 189);
            this.btnPlayReplay.Name = "btnPlayReplay";
            this.btnPlayReplay.Size = new System.Drawing.Size(81, 23);
            this.btnPlayReplay.TabIndex = 8;
            this.btnPlayReplay.Text = "Play Replay";
            this.btnPlayReplay.ToolTipText = "Play replay file using World of Tanks game client";
            this.btnPlayReplay.Click += new System.EventHandler(this.btnPlayReplay_Click);
            // 
            // btnShowFolder
            // 
            this.btnShowFolder.BlackButton = false;
            this.btnShowFolder.Checked = false;
            this.btnShowFolder.Image = null;
            this.btnShowFolder.Location = new System.Drawing.Point(432, 189);
            this.btnShowFolder.Name = "btnShowFolder";
            this.btnShowFolder.Size = new System.Drawing.Size(80, 23);
            this.btnShowFolder.TabIndex = 7;
            this.btnShowFolder.Text = "Open Folder";
            this.btnShowFolder.ToolTipText = "Open Windows Explorer at folder location and select the file";
            this.btnShowFolder.Click += new System.EventHandler(this.btnShowFolder_Click);
            // 
            // txtFile
            // 
            this.txtFile.HasFocus = false;
            this.txtFile.HideBorder = false;
            this.txtFile.Image = null;
            this.txtFile.Location = new System.Drawing.Point(90, 130);
            this.txtFile.MultilineAllow = false;
            this.txtFile.Name = "txtFile";
            this.txtFile.PasswordChar = '\0';
            this.txtFile.ReadOnly = false;
            this.txtFile.Size = new System.Drawing.Size(401, 23);
            this.txtFile.TabIndex = 5;
            this.txtFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtFile.ToolTipText = "";
            // 
            // txtPath
            // 
            this.txtPath.HasFocus = false;
            this.txtPath.HideBorder = false;
            this.txtPath.Image = null;
            this.txtPath.Location = new System.Drawing.Point(90, 100);
            this.txtPath.MultilineAllow = false;
            this.txtPath.Name = "txtPath";
            this.txtPath.PasswordChar = '\0';
            this.txtPath.ReadOnly = false;
            this.txtPath.Size = new System.Drawing.Size(401, 23);
            this.txtPath.TabIndex = 4;
            this.txtPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPath.ToolTipText = "";
            // 
            // lblFile
            // 
            this.lblFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblFile.Dimmed = false;
            this.lblFile.Image = null;
            this.lblFile.Location = new System.Drawing.Point(40, 130);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(44, 23);
            this.lblFile.TabIndex = 3;
            this.lblFile.Text = "File";
            this.lblFile.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // lblFolder
            // 
            this.lblFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblFolder.Dimmed = false;
            this.lblFolder.Image = null;
            this.lblFolder.Location = new System.Drawing.Point(40, 101);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(44, 23);
            this.lblFolder.TabIndex = 2;
            this.lblFolder.Text = "Folder";
            this.lblFolder.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblMessage.Dimmed = false;
            this.lblMessage.Image = null;
            this.lblMessage.Location = new System.Drawing.Point(40, 67);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(451, 23);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(21, 43);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(491, 130);
            this.badGroupBox1.TabIndex = 0;
            this.badGroupBox1.Text = "Search result for replay file";
            // 
            // Replay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 231);
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
        private BadButton btnShowFolder;
        private BadTextBox txtFile;
        private BadTextBox txtPath;
        private BadLabel lblFile;
        private BadLabel lblFolder;
        private BadLabel lblMessage;
        private BadButton btnPlayReplay;
    }
}