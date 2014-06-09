namespace WinApp.Forms
{
	partial class PopupGrid
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupGrid));
			this.PopupGridTheme = new BadForm();
			this.dataGridPopup = new System.Windows.Forms.DataGridView();
			this.scrollY = new BadScrollBar();
			this.PopupGridTheme.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridPopup)).BeginInit();
			this.SuspendLayout();
			// 
			// PopupGridTheme
			// 
			this.PopupGridTheme.Controls.Add(this.dataGridPopup);
			this.PopupGridTheme.Controls.Add(this.scrollY);
			this.PopupGridTheme.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
			this.PopupGridTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PopupGridTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.PopupGridTheme.FormFooter = false;
			this.PopupGridTheme.FormFooterHeight = 26;
			this.PopupGridTheme.FormInnerBorder = 3;
			this.PopupGridTheme.FormMargin = 0;
			this.PopupGridTheme.Image = null;
			this.PopupGridTheme.Location = new System.Drawing.Point(0, 0);
			this.PopupGridTheme.MainArea = mainAreaClass1;
			this.PopupGridTheme.Name = "PopupGridTheme";
			this.PopupGridTheme.Resizable = true;
			this.PopupGridTheme.Size = new System.Drawing.Size(208, 200);
			this.PopupGridTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("PopupGridTheme.SystemExitImage")));
			this.PopupGridTheme.SystemMaximizeImage = null;
			this.PopupGridTheme.SystemMinimizeImage = null;
			this.PopupGridTheme.TabIndex = 0;
			this.PopupGridTheme.Text = "Popup";
			this.PopupGridTheme.TitleHeight = 26;
			// 
			// dataGridPopup
			// 
			this.dataGridPopup.AllowUserToAddRows = false;
			this.dataGridPopup.AllowUserToDeleteRows = false;
			this.dataGridPopup.AllowUserToOrderColumns = true;
			this.dataGridPopup.AllowUserToResizeColumns = false;
			this.dataGridPopup.AllowUserToResizeRows = false;
			this.dataGridPopup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridPopup.ColumnHeadersVisible = false;
			this.dataGridPopup.Location = new System.Drawing.Point(12, 38);
			this.dataGridPopup.Name = "dataGridPopup";
			this.dataGridPopup.ReadOnly = true;
			this.dataGridPopup.RowHeadersVisible = false;
			this.dataGridPopup.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dataGridPopup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridPopup.Size = new System.Drawing.Size(152, 150);
			this.dataGridPopup.TabIndex = 0;
			this.dataGridPopup.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridPopup_CellClick);
			this.dataGridPopup.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridPopup_CellMouseLeave);
			this.dataGridPopup.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridPopup_CellMouseMove);
			// 
			// scrollY
			// 
			this.scrollY.BackColor = System.Drawing.Color.Transparent;
			this.scrollY.Image = null;
			this.scrollY.Location = new System.Drawing.Point(171, 38);
			this.scrollY.Name = "scrollY";
			this.scrollY.ScrollElementsTotals = 100;
			this.scrollY.ScrollElementsVisible = 20;
			this.scrollY.ScrollHide = true;
			this.scrollY.ScrollNecessary = true;
			this.scrollY.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.scrollY.ScrollPosition = 0;
			this.scrollY.Size = new System.Drawing.Size(17, 150);
			this.scrollY.TabIndex = 1;
			this.scrollY.Text = "badScrollBar1";
			this.scrollY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollY_MouseDown);
			this.scrollY.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollY_MouseMove);
			// 
			// PopupGrid
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.ClientSize = new System.Drawing.Size(208, 200);
			this.Controls.Add(this.PopupGridTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MinimumSize = new System.Drawing.Size(200, 100);
			this.Name = "PopupGrid";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "popup";
			this.Load += new System.EventHandler(this.Popup_Load);
			this.Shown += new System.EventHandler(this.PopupGrid_Shown);
			this.Leave += new System.EventHandler(this.PopupGrid_Leave);
			this.Resize += new System.EventHandler(this.Popup_Resize);
			this.PopupGridTheme.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridPopup)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm PopupGridTheme;
		private System.Windows.Forms.DataGridView dataGridPopup;
		private BadScrollBar scrollY;
	}
}