namespace WinApp.Forms
{
    partial class AddPlayer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPlayer));
			this.AddPlayerTheme = new BadForm();
			this.btnCancel = new BadButton();
			this.btnAddNewPlayer = new BadButton();
			this.txtNewPlayerName = new BadTextBox();
			this.badLabel1 = new BadLabel();
			this.AddPlayerTheme.SuspendLayout();
			this.SuspendLayout();
			// 
			// AddPlayerTheme
			// 
			this.AddPlayerTheme.Controls.Add(this.btnCancel);
			this.AddPlayerTheme.Controls.Add(this.btnAddNewPlayer);
			this.AddPlayerTheme.Controls.Add(this.txtNewPlayerName);
			this.AddPlayerTheme.Controls.Add(this.badLabel1);
			this.AddPlayerTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AddPlayerTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.AddPlayerTheme.FormFooter = false;
			this.AddPlayerTheme.FormFooterHeight = 26;
			this.AddPlayerTheme.FormInnerBorder = 3;
			this.AddPlayerTheme.FormMargin = 0;
			this.AddPlayerTheme.Image = null;
			this.AddPlayerTheme.Location = new System.Drawing.Point(0, 0);
			this.AddPlayerTheme.MainArea = mainAreaClass1;
			this.AddPlayerTheme.Name = "AddPlayerTheme";
			this.AddPlayerTheme.Resizable = false;
			this.AddPlayerTheme.Size = new System.Drawing.Size(284, 130);
			this.AddPlayerTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("AddPlayerTheme.SystemExitImage")));
			this.AddPlayerTheme.SystemMaximizeImage = null;
			this.AddPlayerTheme.SystemMinimizeImage = null;
			this.AddPlayerTheme.TabIndex = 0;
			this.AddPlayerTheme.Text = "Add New player";
			this.AddPlayerTheme.TitleHeight = 26;
			// 
			// btnCancel
			// 
			this.btnCancel.Image = null;
			this.btnCancel.Location = new System.Drawing.Point(186, 88);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnAddNewPlayer
			// 
			this.btnAddNewPlayer.Image = null;
			this.btnAddNewPlayer.Location = new System.Drawing.Point(105, 88);
			this.btnAddNewPlayer.Name = "btnAddNewPlayer";
			this.btnAddNewPlayer.Size = new System.Drawing.Size(75, 23);
			this.btnAddNewPlayer.TabIndex = 3;
			this.btnAddNewPlayer.Text = "Save";
			this.btnAddNewPlayer.Click += new System.EventHandler(this.btnAddNewPlayer_Click);
			// 
			// txtNewPlayerName
			// 
			this.txtNewPlayerName.HasFocus = false;
			this.txtNewPlayerName.Image = null;
			this.txtNewPlayerName.Location = new System.Drawing.Point(105, 49);
			this.txtNewPlayerName.Name = "txtNewPlayerName";
			this.txtNewPlayerName.PasswordChar = '\0';
			this.txtNewPlayerName.Size = new System.Drawing.Size(156, 23);
			this.txtNewPlayerName.TabIndex = 2;
			this.txtNewPlayerName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(24, 49);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(75, 23);
			this.badLabel1.TabIndex = 1;
			this.badLabel1.TabStop = false;
			this.badLabel1.Text = "Player Name:";
			// 
			// AddPlayer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 130);
			this.Controls.Add(this.AddPlayerTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddPlayer";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add Player";
			this.AddPlayerTheme.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private BadForm AddPlayerTheme;
		private BadButton btnAddNewPlayer;
		private BadTextBox txtNewPlayerName;
		private BadLabel badLabel1;
		private BadButton btnCancel;
    }
}