namespace WinApp.Forms
{
	partial class FavListAddRemoveTank
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FavListAddRemoveTank));
			this.FavListAddRemoveTankTheme = new BadForm();
			this.scrollY = new BadScrollBar();
			this.dataGridFavList = new System.Windows.Forms.DataGridView();
			this.btnSave = new BadButton();
			this.btnCancel = new BadButton();
			this.FavListAddRemoveTankTheme.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridFavList)).BeginInit();
			this.SuspendLayout();
			// 
			// FavListAddRemoveTankTheme
			// 
			this.FavListAddRemoveTankTheme.Controls.Add(this.scrollY);
			this.FavListAddRemoveTankTheme.Controls.Add(this.dataGridFavList);
			this.FavListAddRemoveTankTheme.Controls.Add(this.btnSave);
			this.FavListAddRemoveTankTheme.Controls.Add(this.btnCancel);
			this.FavListAddRemoveTankTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FavListAddRemoveTankTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.FavListAddRemoveTankTheme.FormFooter = false;
			this.FavListAddRemoveTankTheme.FormFooterHeight = 26;
			this.FavListAddRemoveTankTheme.FormInnerBorder = 3;
			this.FavListAddRemoveTankTheme.FormMargin = 0;
			this.FavListAddRemoveTankTheme.Image = null;
			this.FavListAddRemoveTankTheme.Location = new System.Drawing.Point(0, 0);
			this.FavListAddRemoveTankTheme.MainArea = mainAreaClass1;
			this.FavListAddRemoveTankTheme.Name = "FavListAddRemoveTankTheme";
			this.FavListAddRemoveTankTheme.Resizable = false;
			this.FavListAddRemoveTankTheme.Size = new System.Drawing.Size(247, 283);
			this.FavListAddRemoveTankTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("FavListAddRemoveTankTheme.SystemExitImage")));
			this.FavListAddRemoveTankTheme.SystemMaximizeImage = null;
			this.FavListAddRemoveTankTheme.SystemMinimizeImage = null;
			this.FavListAddRemoveTankTheme.TabIndex = 0;
			this.FavListAddRemoveTankTheme.Text = "Fav List Add Remove Tank";
			this.FavListAddRemoveTankTheme.TitleHeight = 26;
			// 
			// scrollY
			// 
			this.scrollY.Image = null;
			this.scrollY.Location = new System.Drawing.Point(216, 40);
			this.scrollY.Name = "scrollY";
			this.scrollY.ScrollElementsTotals = 100;
			this.scrollY.ScrollElementsVisible = 20;
			this.scrollY.ScrollHide = true;
			this.scrollY.ScrollNecessary = true;
			this.scrollY.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.scrollY.ScrollPosition = 0;
			this.scrollY.Size = new System.Drawing.Size(17, 193);
			this.scrollY.TabIndex = 2;
			this.scrollY.Text = "badScrollBar1";
			// 
			// dataGridFavList
			// 
			this.dataGridFavList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridFavList.Location = new System.Drawing.Point(14, 40);
			this.dataGridFavList.Name = "dataGridFavList";
			this.dataGridFavList.Size = new System.Drawing.Size(203, 193);
			this.dataGridFavList.TabIndex = 1;
			// 
			// btnSave
			// 
			this.btnSave.Image = null;
			this.btnSave.Location = new System.Drawing.Point(91, 246);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(68, 23);
			this.btnSave.TabIndex = 3;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Image = null;
			this.btnCancel.Location = new System.Drawing.Point(165, 246);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(68, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			// 
			// FavListAddRemoveTank
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(247, 283);
			this.Controls.Add(this.FavListAddRemoveTankTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FavListAddRemoveTank";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FavListAddRemoveTank";
			this.Load += new System.EventHandler(this.FavListAddRemoveTank_Load);
			this.FavListAddRemoveTankTheme.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridFavList)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm FavListAddRemoveTankTheme;
		private BadScrollBar scrollY;
		private System.Windows.Forms.DataGridView dataGridFavList;
		private BadButton btnSave;
		private BadButton btnCancel;
	}
}