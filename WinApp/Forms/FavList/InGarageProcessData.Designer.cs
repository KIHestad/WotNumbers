namespace WinApp.Forms
{
	partial class InGarageProcessData
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InGarageProcessData));
			this.badForm1 = new BadForm();
			this.ddFavList = new BadDropDownBox();
			this.badLabel3 = new BadLabel();
			this.btnCreateFavList = new BadButton();
			this.btnCancel = new BadButton();
			this.btnSaveTanksToFavList = new BadButton();
			this.badGroupBox2 = new BadGroupBox();
			this.txtTanksInGarage = new BadTextBox();
			this.badLabel2 = new BadLabel();
			this.txtNickname = new BadTextBox();
			this.badLabel1 = new BadLabel();
			this.badGroupBox1 = new BadGroupBox();
			this.badForm1.SuspendLayout();
			this.SuspendLayout();
			// 
			// badForm1
			// 
			this.badForm1.Controls.Add(this.ddFavList);
			this.badForm1.Controls.Add(this.badLabel3);
			this.badForm1.Controls.Add(this.btnCreateFavList);
			this.badForm1.Controls.Add(this.btnCancel);
			this.badForm1.Controls.Add(this.btnSaveTanksToFavList);
			this.badForm1.Controls.Add(this.badGroupBox2);
			this.badForm1.Controls.Add(this.txtTanksInGarage);
			this.badForm1.Controls.Add(this.badLabel2);
			this.badForm1.Controls.Add(this.txtNickname);
			this.badForm1.Controls.Add(this.badLabel1);
			this.badForm1.Controls.Add(this.badGroupBox1);
			this.badForm1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.badForm1.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.badForm1.FormFooter = false;
			this.badForm1.FormFooterHeight = 26;
			this.badForm1.FormInnerBorder = 3;
			this.badForm1.FormMargin = 0;
			this.badForm1.Image = null;
			this.badForm1.Location = new System.Drawing.Point(0, 0);
			this.badForm1.MainArea = mainAreaClass1;
			this.badForm1.Name = "badForm1";
			this.badForm1.Resizable = false;
			this.badForm1.Size = new System.Drawing.Size(291, 323);
			this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
			this.badForm1.SystemMaximizeImage = null;
			this.badForm1.SystemMinimizeImage = null;
			this.badForm1.TabIndex = 0;
			this.badForm1.Text = "Save \"In garage\" Favourite Tank List";
			this.badForm1.TitleHeight = 26;
			// 
			// ddFavList
			// 
			this.ddFavList.Image = null;
			this.ddFavList.Location = new System.Drawing.Point(125, 187);
			this.ddFavList.Name = "ddFavList";
			this.ddFavList.Size = new System.Drawing.Size(130, 23);
			this.ddFavList.TabIndex = 10;
			this.ddFavList.Click += new System.EventHandler(this.ddFavList_Click);
			// 
			// badLabel3
			// 
			this.badLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel3.Dimmed = false;
			this.badLabel3.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.badLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.badLabel3.Image = null;
			this.badLabel3.Location = new System.Drawing.Point(37, 187);
			this.badLabel3.Name = "badLabel3";
			this.badLabel3.Size = new System.Drawing.Size(85, 23);
			this.badLabel3.TabIndex = 9;
			this.badLabel3.Text = "Fav Tank List:";
			// 
			// btnCreateFavList
			// 
			this.btnCreateFavList.Image = null;
			this.btnCreateFavList.Location = new System.Drawing.Point(175, 225);
			this.btnCreateFavList.Name = "btnCreateFavList";
			this.btnCreateFavList.Size = new System.Drawing.Size(80, 22);
			this.btnCreateFavList.TabIndex = 8;
			this.btnCreateFavList.Text = "Create new";
			this.btnCreateFavList.Click += new System.EventHandler(this.btnCreateFavList_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Image = null;
			this.btnCancel.Location = new System.Drawing.Point(195, 277);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSaveTanksToFavList
			// 
			this.btnSaveTanksToFavList.Enabled = false;
			this.btnSaveTanksToFavList.Image = null;
			this.btnSaveTanksToFavList.Location = new System.Drawing.Point(113, 277);
			this.btnSaveTanksToFavList.Name = "btnSaveTanksToFavList";
			this.btnSaveTanksToFavList.Size = new System.Drawing.Size(75, 23);
			this.btnSaveTanksToFavList.TabIndex = 6;
			this.btnSaveTanksToFavList.Text = "Save";
			this.btnSaveTanksToFavList.Click += new System.EventHandler(this.btnSaveTanksToFavList_Click);
			// 
			// badGroupBox2
			// 
			this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox2.Image = null;
			this.badGroupBox2.Location = new System.Drawing.Point(17, 162);
			this.badGroupBox2.Name = "badGroupBox2";
			this.badGroupBox2.Size = new System.Drawing.Size(253, 99);
			this.badGroupBox2.TabIndex = 5;
			this.badGroupBox2.Text = "Select or Create Favourite Tank List";
			// 
			// txtTanksInGarage
			// 
			this.txtTanksInGarage.Enabled = false;
			this.txtTanksInGarage.HasFocus = false;
			this.txtTanksInGarage.Image = null;
			this.txtTanksInGarage.Location = new System.Drawing.Point(125, 104);
			this.txtTanksInGarage.MultilineAllow = false;
			this.txtTanksInGarage.Name = "txtTanksInGarage";
			this.txtTanksInGarage.PasswordChar = '\0';
			this.txtTanksInGarage.Size = new System.Drawing.Size(130, 23);
			this.txtTanksInGarage.TabIndex = 4;
			this.txtTanksInGarage.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			// 
			// badLabel2
			// 
			this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel2.Dimmed = false;
			this.badLabel2.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.badLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.badLabel2.Image = null;
			this.badLabel2.Location = new System.Drawing.Point(33, 104);
			this.badLabel2.Name = "badLabel2";
			this.badLabel2.Size = new System.Drawing.Size(89, 23);
			this.badLabel2.TabIndex = 3;
			this.badLabel2.Text = "Tanks in garage:";
			// 
			// txtNickname
			// 
			this.txtNickname.Enabled = false;
			this.txtNickname.HasFocus = false;
			this.txtNickname.Image = null;
			this.txtNickname.Location = new System.Drawing.Point(125, 75);
			this.txtNickname.MultilineAllow = false;
			this.txtNickname.Name = "txtNickname";
			this.txtNickname.PasswordChar = '\0';
			this.txtNickname.Size = new System.Drawing.Size(130, 23);
			this.txtNickname.TabIndex = 2;
			this.txtNickname.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.badLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(33, 75);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(75, 23);
			this.badLabel1.TabIndex = 1;
			this.badLabel1.Text = "Player name:";
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(17, 46);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(253, 100);
			this.badGroupBox1.TabIndex = 0;
			this.badGroupBox1.Text = "Retrieved Data From WoT API";
			// 
			// InGarageProcessData
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Fuchsia;
			this.ClientSize = new System.Drawing.Size(291, 323);
			this.Controls.Add(this.badForm1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "InGarageProcessData";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "InGarageProcessData";
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.Load += new System.EventHandler(this.InGarageProcessData_Load);
			this.badForm1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm badForm1;
		private BadDropDownBox ddFavList;
		private BadLabel badLabel3;
		private BadButton btnCreateFavList;
		private BadButton btnCancel;
		private BadButton btnSaveTanksToFavList;
		private BadGroupBox badGroupBox2;
		private BadTextBox txtTanksInGarage;
		private BadLabel badLabel2;
		private BadTextBox txtNickname;
		private BadLabel badLabel1;
		private BadGroupBox badGroupBox1;
	}
}