namespace WinApp.Forms.Chart
{
    partial class FavouriteEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FavouriteEdit));
            this.FavouriteEditTheme = new BadForm();
            this.cmdDelete = new BadButton();
            this.badLabel1 = new BadLabel();
            this.txtUpdateFavName = new BadTextBox();
            this.cmdCancel = new BadButton();
            this.cmdSave = new BadButton();
            this.badGroupBox1 = new BadGroupBox();
            this.FavouriteEditTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // FavouriteEditTheme
            // 
            this.FavouriteEditTheme.Controls.Add(this.cmdDelete);
            this.FavouriteEditTheme.Controls.Add(this.badLabel1);
            this.FavouriteEditTheme.Controls.Add(this.txtUpdateFavName);
            this.FavouriteEditTheme.Controls.Add(this.cmdCancel);
            this.FavouriteEditTheme.Controls.Add(this.cmdSave);
            this.FavouriteEditTheme.Controls.Add(this.badGroupBox1);
            this.FavouriteEditTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FavouriteEditTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FavouriteEditTheme.FormExitAsMinimize = false;
            this.FavouriteEditTheme.FormFooter = false;
            this.FavouriteEditTheme.FormFooterHeight = 26;
            this.FavouriteEditTheme.FormInnerBorder = 3;
            this.FavouriteEditTheme.FormMargin = 0;
            this.FavouriteEditTheme.Image = null;
            this.FavouriteEditTheme.Location = new System.Drawing.Point(0, 0);
            this.FavouriteEditTheme.MainArea = mainAreaClass1;
            this.FavouriteEditTheme.Name = "FavouriteEditTheme";
            this.FavouriteEditTheme.Resizable = true;
            this.FavouriteEditTheme.Size = new System.Drawing.Size(357, 163);
            this.FavouriteEditTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("FavouriteEditTheme.SystemExitImage")));
            this.FavouriteEditTheme.SystemMaximizeImage = null;
            this.FavouriteEditTheme.SystemMinimizeImage = null;
            this.FavouriteEditTheme.TabIndex = 0;
            this.FavouriteEditTheme.Text = "Edit or Delete Favourite";
            this.FavouriteEditTheme.TitleHeight = 26;
            // 
            // cmdDelete
            // 
            this.cmdDelete.BlackButton = false;
            this.cmdDelete.Checked = false;
            this.cmdDelete.Image = null;
            this.cmdDelete.Location = new System.Drawing.Point(186, 124);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(75, 23);
            this.cmdDelete.TabIndex = 12;
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.ToolTipText = "";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(37, 65);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(89, 23);
            this.badLabel1.TabIndex = 11;
            this.badLabel1.Text = "Favourite name:";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // txtUpdateFavName
            // 
            this.txtUpdateFavName.HasFocus = false;
            this.txtUpdateFavName.HideBorder = false;
            this.txtUpdateFavName.Image = null;
            this.txtUpdateFavName.Location = new System.Drawing.Point(132, 65);
            this.txtUpdateFavName.MultilineAllow = false;
            this.txtUpdateFavName.Name = "txtUpdateFavName";
            this.txtUpdateFavName.PasswordChar = '\0';
            this.txtUpdateFavName.ReadOnly = false;
            this.txtUpdateFavName.Size = new System.Drawing.Size(193, 23);
            this.txtUpdateFavName.TabIndex = 10;
            this.txtUpdateFavName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtUpdateFavName.ToolTipText = "";
            // 
            // cmdCancel
            // 
            this.cmdCancel.BlackButton = false;
            this.cmdCancel.Checked = false;
            this.cmdCancel.Image = null;
            this.cmdCancel.Location = new System.Drawing.Point(267, 124);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 9;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.ToolTipText = "";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.BlackButton = false;
            this.cmdSave.Checked = false;
            this.cmdSave.Image = null;
            this.cmdSave.Location = new System.Drawing.Point(105, 124);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 8;
            this.cmdSave.Text = "Save";
            this.cmdSave.ToolTipText = "";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(19, 40);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(323, 69);
            this.badGroupBox1.TabIndex = 7;
            this.badGroupBox1.Text = "Edit Options";
            // 
            // FavouriteEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 163);
            this.Controls.Add(this.FavouriteEditTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FavouriteEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FavouriteEdit";
            this.FavouriteEditTheme.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BadForm FavouriteEditTheme;
        private BadButton cmdDelete;
        private BadLabel badLabel1;
        private BadTextBox txtUpdateFavName;
        private BadButton cmdCancel;
        private BadButton cmdSave;
        private BadGroupBox badGroupBox1;
    }
}