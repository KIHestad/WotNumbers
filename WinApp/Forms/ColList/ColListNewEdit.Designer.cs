namespace WinApp.Forms
{
	partial class ColListNewEdit
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColListNewEdit));
			this.ColListNewEditTheme = new BadForm();
			this.badLabel2 = new BadLabel();
			this.ddDefaultTankFilter = new BadDropDownBox();
			this.txtName = new BadTextBox();
			this.badLabel1 = new BadLabel();
			this.badGroupBox1 = new BadGroupBox();
			this.btnCancel = new BadButton();
			this.btnSave = new BadButton();
			this.ColListNewEditTheme.SuspendLayout();
			this.SuspendLayout();
			// 
			// ColListNewEditTheme
			// 
			this.ColListNewEditTheme.Controls.Add(this.badLabel2);
			this.ColListNewEditTheme.Controls.Add(this.ddDefaultTankFilter);
			this.ColListNewEditTheme.Controls.Add(this.txtName);
			this.ColListNewEditTheme.Controls.Add(this.badLabel1);
			this.ColListNewEditTheme.Controls.Add(this.badGroupBox1);
			this.ColListNewEditTheme.Controls.Add(this.btnCancel);
			this.ColListNewEditTheme.Controls.Add(this.btnSave);
			this.ColListNewEditTheme.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
			this.ColListNewEditTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ColListNewEditTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.ColListNewEditTheme.FormFooter = false;
			this.ColListNewEditTheme.FormFooterHeight = 26;
			this.ColListNewEditTheme.FormInnerBorder = 3;
			this.ColListNewEditTheme.FormMargin = 0;
			this.ColListNewEditTheme.Image = null;
			this.ColListNewEditTheme.Location = new System.Drawing.Point(0, 0);
			this.ColListNewEditTheme.MainArea = mainAreaClass1;
			this.ColListNewEditTheme.Name = "ColListNewEditTheme";
			this.ColListNewEditTheme.Resizable = true;
			this.ColListNewEditTheme.Size = new System.Drawing.Size(370, 198);
			this.ColListNewEditTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("ColListNewEditTheme.SystemExitImage")));
			this.ColListNewEditTheme.SystemMaximizeImage = null;
			this.ColListNewEditTheme.SystemMinimizeImage = null;
			this.ColListNewEditTheme.TabIndex = 0;
			this.ColListNewEditTheme.Text = "New or Edit colList";
			this.ColListNewEditTheme.TitleHeight = 26;
			// 
			// badLabel2
			// 
			this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel2.Dimmed = false;
			this.badLabel2.Image = null;
			this.badLabel2.Location = new System.Drawing.Point(36, 97);
			this.badLabel2.Name = "badLabel2";
			this.badLabel2.Size = new System.Drawing.Size(106, 23);
			this.badLabel2.TabIndex = 3;
			this.badLabel2.Text = "Favourite Tank List:";
			// 
			// ddDefaultTankFilter
			// 
			this.ddDefaultTankFilter.Image = null;
			this.ddDefaultTankFilter.Location = new System.Drawing.Point(148, 96);
			this.ddDefaultTankFilter.Name = "ddDefaultTankFilter";
			this.ddDefaultTankFilter.Size = new System.Drawing.Size(184, 23);
			this.ddDefaultTankFilter.TabIndex = 2;
			this.ddDefaultTankFilter.Text = "(Use current)";
			this.ddDefaultTankFilter.Click += new System.EventHandler(this.ddDefaultTankFilter_Click);
			// 
			// txtName
			// 
			this.txtName.HasFocus = false;
			this.txtName.Image = null;
			this.txtName.Location = new System.Drawing.Point(148, 67);
			this.txtName.Name = "txtName";
			this.txtName.PasswordChar = '\0';
			this.txtName.Size = new System.Drawing.Size(184, 23);
			this.txtName.TabIndex = 1;
			this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(36, 67);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(106, 23);
			this.badLabel1.TabIndex = 0;
			this.badLabel1.Text = "Column List Name:";
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(21, 41);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(331, 100);
			this.badGroupBox1.TabIndex = 6;
			this.badGroupBox1.Text = "Column List Properties";
			// 
			// btnCancel
			// 
			this.btnCancel.Image = null;
			this.btnCancel.Location = new System.Drawing.Point(196, 157);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Image = null;
			this.btnSave.Location = new System.Drawing.Point(277, 157);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 4;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// ColListNewEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(370, 198);
			this.Controls.Add(this.ColListNewEditTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ColListNewEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ColListNewEdit";
			this.Load += new System.EventHandler(this.ColListNewEdit_Load);
			this.ColListNewEditTheme.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm ColListNewEditTheme;
		private BadTextBox txtName;
		private BadLabel badLabel1;
		private BadLabel badLabel2;
		private BadDropDownBox ddDefaultTankFilter;
		private BadGroupBox badGroupBox1;
		private BadButton btnCancel;
		private BadButton btnSave;
	}
}