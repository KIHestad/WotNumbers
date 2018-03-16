namespace WinApp.Forms.Chart
{
    partial class FavouriteSave
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FavouriteSave));
            this.FavouriteSaveTheme = new BadForm();
            this.txtUpdateFavName = new BadTextBox();
            this.cmdCancel = new BadButton();
            this.cmdSave = new BadButton();
            this.txtNewFavName = new BadTextBox();
            this.chkNewFav = new BadCheckBox();
            this.chkUpdateFav = new BadCheckBox();
            this.badGroupBox1 = new BadGroupBox();
            this.FavouriteSaveTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // FavouriteSaveTheme
            // 
            this.FavouriteSaveTheme.Controls.Add(this.txtUpdateFavName);
            this.FavouriteSaveTheme.Controls.Add(this.cmdCancel);
            this.FavouriteSaveTheme.Controls.Add(this.cmdSave);
            this.FavouriteSaveTheme.Controls.Add(this.txtNewFavName);
            this.FavouriteSaveTheme.Controls.Add(this.chkNewFav);
            this.FavouriteSaveTheme.Controls.Add(this.chkUpdateFav);
            this.FavouriteSaveTheme.Controls.Add(this.badGroupBox1);
            this.FavouriteSaveTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FavouriteSaveTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FavouriteSaveTheme.FormExitAsMinimize = false;
            this.FavouriteSaveTheme.FormFooter = false;
            this.FavouriteSaveTheme.FormFooterHeight = 26;
            this.FavouriteSaveTheme.FormInnerBorder = 3;
            this.FavouriteSaveTheme.FormMargin = 0;
            this.FavouriteSaveTheme.Image = null;
            this.FavouriteSaveTheme.Location = new System.Drawing.Point(0, 0);
            this.FavouriteSaveTheme.MainArea = mainAreaClass1;
            this.FavouriteSaveTheme.Name = "FavouriteSaveTheme";
            this.FavouriteSaveTheme.Resizable = true;
            this.FavouriteSaveTheme.Size = new System.Drawing.Size(418, 196);
            this.FavouriteSaveTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("FavouriteSaveTheme.SystemExitImage")));
            this.FavouriteSaveTheme.SystemMaximizeImage = null;
            this.FavouriteSaveTheme.SystemMinimizeImage = null;
            this.FavouriteSaveTheme.TabIndex = 0;
            this.FavouriteSaveTheme.Text = "Save Favourite";
            this.FavouriteSaveTheme.TitleHeight = 26;
            // 
            // txtUpdateFavName
            // 
            this.txtUpdateFavName.HasFocus = false;
            this.txtUpdateFavName.HideBorder = false;
            this.txtUpdateFavName.Image = null;
            this.txtUpdateFavName.Location = new System.Drawing.Point(193, 67);
            this.txtUpdateFavName.MultilineAllow = false;
            this.txtUpdateFavName.Name = "txtUpdateFavName";
            this.txtUpdateFavName.PasswordChar = '\0';
            this.txtUpdateFavName.ReadOnly = false;
            this.txtUpdateFavName.Size = new System.Drawing.Size(193, 23);
            this.txtUpdateFavName.TabIndex = 6;
            this.txtUpdateFavName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtUpdateFavName.ToolTipText = "";
            // 
            // cmdCancel
            // 
            this.cmdCancel.BlackButton = false;
            this.cmdCancel.Checked = false;
            this.cmdCancel.Image = null;
            this.cmdCancel.Location = new System.Drawing.Point(325, 153);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.ToolTipText = "";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.BlackButton = false;
            this.cmdSave.Checked = false;
            this.cmdSave.Image = null;
            this.cmdSave.Location = new System.Drawing.Point(244, 153);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 4;
            this.cmdSave.Text = "Save";
            this.cmdSave.ToolTipText = "";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // txtNewFavName
            // 
            this.txtNewFavName.HasFocus = false;
            this.txtNewFavName.HideBorder = false;
            this.txtNewFavName.Image = null;
            this.txtNewFavName.Location = new System.Drawing.Point(193, 96);
            this.txtNewFavName.MultilineAllow = false;
            this.txtNewFavName.Name = "txtNewFavName";
            this.txtNewFavName.PasswordChar = '\0';
            this.txtNewFavName.ReadOnly = false;
            this.txtNewFavName.Size = new System.Drawing.Size(193, 23);
            this.txtNewFavName.TabIndex = 3;
            this.txtNewFavName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNewFavName.ToolTipText = "";
            // 
            // chkNewFav
            // 
            this.chkNewFav.BackColor = System.Drawing.Color.Transparent;
            this.chkNewFav.Checked = false;
            this.chkNewFav.Image = global::WinApp.Properties.Resources.checkboxcheck;
            this.chkNewFav.Location = new System.Drawing.Point(32, 96);
            this.chkNewFav.Name = "chkNewFav";
            this.chkNewFav.Size = new System.Drawing.Size(152, 23);
            this.chkNewFav.TabIndex = 2;
            this.chkNewFav.Text = "Save as new favourite:";
            this.chkNewFav.Click += new System.EventHandler(this.chkNewFav_Click);
            // 
            // chkUpdateFav
            // 
            this.chkUpdateFav.BackColor = System.Drawing.Color.Transparent;
            this.chkUpdateFav.Checked = false;
            this.chkUpdateFav.Image = global::WinApp.Properties.Resources.checkboxcheck;
            this.chkUpdateFav.Location = new System.Drawing.Point(32, 67);
            this.chkUpdateFav.Name = "chkUpdateFav";
            this.chkUpdateFav.Size = new System.Drawing.Size(155, 23);
            this.chkUpdateFav.TabIndex = 1;
            this.chkUpdateFav.Text = "Update current favourite:";
            this.chkUpdateFav.Click += new System.EventHandler(this.chkUpdateFav_Click);
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(17, 41);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(383, 95);
            this.badGroupBox1.TabIndex = 0;
            this.badGroupBox1.Text = "Save Options";
            // 
            // FavouriteSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 196);
            this.Controls.Add(this.FavouriteSaveTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FavouriteSave";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FavouriteSave";
            this.FavouriteSaveTheme.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BadForm FavouriteSaveTheme;
        private BadGroupBox badGroupBox1;
        private BadTextBox txtUpdateFavName;
        private BadButton cmdCancel;
        private BadButton cmdSave;
        private BadTextBox txtNewFavName;
        private BadCheckBox chkNewFav;
        private BadCheckBox chkUpdateFav;
    }
}