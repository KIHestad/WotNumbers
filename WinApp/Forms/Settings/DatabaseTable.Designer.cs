namespace WinApp.Forms
{
	partial class DatabaseTable
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseTable));
			BadThemeContainerControl.MainAreaClass mainAreaClass1 = new BadThemeContainerControl.MainAreaClass();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.DatabaseTableTheme = new BadForm();
			this.popupSelectTable = new BadDropDownBox();
			this.dataGridViewShowTable = new System.Windows.Forms.DataGridView();
			this.btnRefresh = new BadButton();
			this.badLabel1 = new BadLabel();
			this.scrollY = new BadScrollBar();
			this.scrollX = new BadScrollBar();
			this.scrollCorner = new BadScrollBarCorner();
			this.DatabaseTableTheme.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewShowTable)).BeginInit();
			this.SuspendLayout();
			// 
			// DatabaseTableTheme
			// 
			this.DatabaseTableTheme.BackColor = System.Drawing.Color.Fuchsia;
			this.DatabaseTableTheme.Controls.Add(this.popupSelectTable);
			this.DatabaseTableTheme.Controls.Add(this.dataGridViewShowTable);
			this.DatabaseTableTheme.Controls.Add(this.btnRefresh);
			this.DatabaseTableTheme.Controls.Add(this.badLabel1);
			this.DatabaseTableTheme.Controls.Add(this.scrollY);
			this.DatabaseTableTheme.Controls.Add(this.scrollX);
			this.DatabaseTableTheme.Controls.Add(this.scrollCorner);
			this.DatabaseTableTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DatabaseTableTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.DatabaseTableTheme.FormFooter = false;
			this.DatabaseTableTheme.FormFooterHeight = 26;
			this.DatabaseTableTheme.FormInnerBorder = 0;
			this.DatabaseTableTheme.FormMargin = 0;
			this.DatabaseTableTheme.Image = ((System.Drawing.Image)(resources.GetObject("DatabaseTableTheme.Image")));
			this.DatabaseTableTheme.Location = new System.Drawing.Point(0, 0);
			this.DatabaseTableTheme.MainArea = mainAreaClass1;
			this.DatabaseTableTheme.Name = "DatabaseTableTheme";
			this.DatabaseTableTheme.Resizable = true;
			this.DatabaseTableTheme.Size = new System.Drawing.Size(448, 302);
			this.DatabaseTableTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("DatabaseTableTheme.SystemExitImage")));
			this.DatabaseTableTheme.SystemMaximizeImage = ((System.Drawing.Image)(resources.GetObject("DatabaseTableTheme.SystemMaximizeImage")));
			this.DatabaseTableTheme.SystemMinimizeImage = ((System.Drawing.Image)(resources.GetObject("DatabaseTableTheme.SystemMinimizeImage")));
			this.DatabaseTableTheme.TabIndex = 0;
			this.DatabaseTableTheme.Text = "Database Tables";
			this.DatabaseTableTheme.TitleHeight = 26;
			// 
			// popupSelectTable
			// 
			this.popupSelectTable.Image = null;
			this.popupSelectTable.Location = new System.Drawing.Point(97, 36);
			this.popupSelectTable.Name = "popupSelectTable";
			this.popupSelectTable.Size = new System.Drawing.Size(247, 23);
			this.popupSelectTable.TabIndex = 2;
			this.popupSelectTable.TextChanged += new System.EventHandler(this.popupSelectTable_TextChanged);
			this.popupSelectTable.Click += new System.EventHandler(this.popupSelectTable_Click);
			// 
			// dataGridViewShowTable
			// 
			this.dataGridViewShowTable.AllowUserToAddRows = false;
			this.dataGridViewShowTable.AllowUserToDeleteRows = false;
			this.dataGridViewShowTable.AllowUserToOrderColumns = true;
			this.dataGridViewShowTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridViewShowTable.CausesValidation = false;
			this.dataGridViewShowTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.Red;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewShowTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridViewShowTable.EnableHeadersVisualStyles = false;
			this.dataGridViewShowTable.Location = new System.Drawing.Point(15, 78);
			this.dataGridViewShowTable.Name = "dataGridViewShowTable";
			this.dataGridViewShowTable.ReadOnly = true;
			this.dataGridViewShowTable.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dataGridViewShowTable.Size = new System.Drawing.Size(387, 183);
			this.dataGridViewShowTable.TabIndex = 4;
			this.dataGridViewShowTable.SelectionChanged += new System.EventHandler(this.dataGridViewShowTable_SelectionChanged);
			// 
			// btnRefresh
			// 
			this.btnRefresh.Image = null;
			this.btnRefresh.Location = new System.Drawing.Point(350, 37);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(75, 23);
			this.btnRefresh.TabIndex = 3;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.badLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(15, 37);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(75, 23);
			this.badLabel1.TabIndex = 1;
			this.badLabel1.TabStop = false;
			this.badLabel1.Text = "Select Table:";
			// 
			// scrollY
			// 
			this.scrollY.BackColor = System.Drawing.Color.Transparent;
			this.scrollY.Image = null;
			this.scrollY.Location = new System.Drawing.Point(408, 78);
			this.scrollY.Name = "scrollY";
			this.scrollY.ScrollElementsTotals = 100;
			this.scrollY.ScrollElementsVisible = 20;
			this.scrollY.ScrollHide = true;
			this.scrollY.ScrollNecessary = true;
			this.scrollY.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.scrollY.ScrollPosition = 0;
			this.scrollY.Size = new System.Drawing.Size(17, 183);
			this.scrollY.TabIndex = 5;
			this.scrollY.TabStop = false;
			this.scrollY.Text = "badScrollBar1";
			this.scrollY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollY_MouseDown);
			this.scrollY.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollY_MouseMove);
			this.scrollY.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scrollY_MouseUp);
			// 
			// scrollX
			// 
			this.scrollX.BackColor = System.Drawing.Color.Transparent;
			this.scrollX.Image = null;
			this.scrollX.Location = new System.Drawing.Point(15, 267);
			this.scrollX.Name = "scrollX";
			this.scrollX.ScrollElementsTotals = 100;
			this.scrollX.ScrollElementsVisible = 20;
			this.scrollX.ScrollHide = true;
			this.scrollX.ScrollNecessary = true;
			this.scrollX.ScrollOrientation = System.Windows.Forms.ScrollOrientation.HorizontalScroll;
			this.scrollX.ScrollPosition = 0;
			this.scrollX.Size = new System.Drawing.Size(387, 17);
			this.scrollX.TabIndex = 6;
			this.scrollX.TabStop = false;
			this.scrollX.Text = "badScrollBar2";
			this.scrollX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollX_MouseDown);
			this.scrollX.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollX_MouseMove);
			this.scrollX.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scrollX_MouseUp);
			// 
			// scrollCorner
			// 
			this.scrollCorner.Image = null;
			this.scrollCorner.Location = new System.Drawing.Point(408, 267);
			this.scrollCorner.Name = "scrollCorner";
			this.scrollCorner.Size = new System.Drawing.Size(17, 17);
			this.scrollCorner.TabIndex = 7;
			this.scrollCorner.TabStop = false;
			this.scrollCorner.Text = "badScrollBarCorner1";
			// 
			// DatabaseTable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Fuchsia;
			this.ClientSize = new System.Drawing.Size(448, 302);
			this.Controls.Add(this.DatabaseTableTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(448, 302);
			this.Name = "DatabaseTable";
			this.Text = "DatabaseTable";
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.Load += new System.EventHandler(this.DatabaseTable_Load);
			this.ResizeEnd += new System.EventHandler(this.DatabaseTable_ResizeEnd);
			this.LocationChanged += new System.EventHandler(this.DatabaseTable_LocationChanged);
			this.Resize += new System.EventHandler(this.DatabaseTable_Resize);
			this.DatabaseTableTheme.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewShowTable)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private BadForm DatabaseTableTheme;
		private BadButton btnRefresh;
		private BadLabel badLabel1;
		private System.Windows.Forms.DataGridView dataGridViewShowTable;
		private BadScrollBar scrollX;
		private BadScrollBar scrollY;
		private BadScrollBarCorner scrollCorner;
		private BadDropDownBox popupSelectTable;
	}
}