namespace WinApp.Forms
{
	partial class FavListNewEdit
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FavListNewEdit));
			this.FavListNewEditTheme = new BadForm();
			this.ddCopyFrom = new BadDropDownBox();
			this.lblCopyFrom = new BadLabel();
			this.txtName = new BadTextBox();
			this.badLabel1 = new BadLabel();
			this.badGroupBox1 = new BadGroupBox();
			this.btnCancel = new BadButton();
			this.btnSave = new BadButton();
			this.FavListNewEditTheme.SuspendLayout();
			this.SuspendLayout();
			// 
			// FavListNewEditTheme
			// 
			this.FavListNewEditTheme.Controls.Add(this.ddCopyFrom);
			this.FavListNewEditTheme.Controls.Add(this.lblCopyFrom);
			this.FavListNewEditTheme.Controls.Add(this.txtName);
			this.FavListNewEditTheme.Controls.Add(this.badLabel1);
			this.FavListNewEditTheme.Controls.Add(this.badGroupBox1);
			this.FavListNewEditTheme.Controls.Add(this.btnCancel);
			this.FavListNewEditTheme.Controls.Add(this.btnSave);
			this.FavListNewEditTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FavListNewEditTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.FavListNewEditTheme.FormFooter = false;
			this.FavListNewEditTheme.FormFooterHeight = 26;
			this.FavListNewEditTheme.FormInnerBorder = 3;
			this.FavListNewEditTheme.FormMargin = 0;
			this.FavListNewEditTheme.Image = null;
			this.FavListNewEditTheme.Location = new System.Drawing.Point(0, 0);
			this.FavListNewEditTheme.MainArea = mainAreaClass1;
			this.FavListNewEditTheme.Name = "FavListNewEditTheme";
			this.FavListNewEditTheme.Resizable = false;
			this.FavListNewEditTheme.Size = new System.Drawing.Size(381, 203);
			this.FavListNewEditTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("FavListNewEditTheme.SystemExitImage")));
			this.FavListNewEditTheme.SystemMaximizeImage = null;
			this.FavListNewEditTheme.SystemMinimizeImage = null;
			this.FavListNewEditTheme.TabIndex = 0;
			this.FavListNewEditTheme.Text = "New or Edit favList";
			this.FavListNewEditTheme.TitleHeight = 26;
			// 
			// ddCopyFrom
			// 
			this.ddCopyFrom.Image = null;
			this.ddCopyFrom.Location = new System.Drawing.Point(152, 105);
			this.ddCopyFrom.Name = "ddCopyFrom";
			this.ddCopyFrom.Size = new System.Drawing.Size(184, 23);
			this.ddCopyFrom.TabIndex = 5;
			this.ddCopyFrom.Text = "(None)";
			this.ddCopyFrom.Click += new System.EventHandler(this.ddCopyFrom_Click);
			// 
			// lblCopyFrom
			// 
			this.lblCopyFrom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblCopyFrom.Dimmed = false;
			this.lblCopyFrom.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.lblCopyFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.lblCopyFrom.Image = null;
			this.lblCopyFrom.Location = new System.Drawing.Point(40, 105);
			this.lblCopyFrom.Name = "lblCopyFrom";
			this.lblCopyFrom.Size = new System.Drawing.Size(106, 23);
			this.lblCopyFrom.TabIndex = 4;
			this.lblCopyFrom.TabStop = false;
			this.lblCopyFrom.Text = "Copy from existing:";
			this.lblCopyFrom.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// txtName
			// 
			this.txtName.HasFocus = false;
			this.txtName.Image = null;
			this.txtName.Location = new System.Drawing.Point(152, 74);
			this.txtName.MultilineAllow = false;
			this.txtName.Name = "txtName";
			this.txtName.PasswordChar = '\0';
			this.txtName.ReadOnly = false;
			this.txtName.Size = new System.Drawing.Size(184, 23);
			this.txtName.TabIndex = 3;
			this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtName.ToolTipText = "";
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.badLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(40, 74);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(106, 23);
			this.badLabel1.TabIndex = 2;
			this.badLabel1.TabStop = false;
			this.badLabel1.Text = "Fav Tank List Name:";
			this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(25, 48);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(331, 99);
			this.badGroupBox1.TabIndex = 1;
			this.badGroupBox1.TabStop = false;
			this.badGroupBox1.Text = "Favourite Tank List Properties";
			// 
			// btnCancel
			// 
			this.btnCancel.BlackButton = false;
			this.btnCancel.Checked = false;
			this.btnCancel.Image = null;
			this.btnCancel.Location = new System.Drawing.Point(281, 161);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.ToolTipText = "";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.BlackButton = false;
			this.btnSave.Checked = false;
			this.btnSave.Image = null;
			this.btnSave.Location = new System.Drawing.Point(200, 161);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 6;
			this.btnSave.Text = "Save";
			this.btnSave.ToolTipText = "";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// FavListNewEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(381, 203);
			this.Controls.Add(this.FavListNewEditTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FavListNewEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FavListNewEdit";
			this.Load += new System.EventHandler(this.FavListNewEdit_Load);
			this.FavListNewEditTheme.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm FavListNewEditTheme;
		private BadDropDownBox ddCopyFrom;
		private BadLabel lblCopyFrom;
		private BadTextBox txtName;
		private BadLabel badLabel1;
		private BadGroupBox badGroupBox1;
		private BadButton btnCancel;
		private BadButton btnSave;
	}
}