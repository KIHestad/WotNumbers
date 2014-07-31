namespace WinApp.Forms
{
	partial class DropDownGrid
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
			this.dataGridDropDown = new System.Windows.Forms.DataGridView();
			this.scrollY = new BadScrollBar();
			((System.ComponentModel.ISupportInitialize)(this.dataGridDropDown)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridDropDown
			// 
			this.dataGridDropDown.AllowUserToAddRows = false;
			this.dataGridDropDown.AllowUserToDeleteRows = false;
			this.dataGridDropDown.AllowUserToOrderColumns = true;
			this.dataGridDropDown.AllowUserToResizeColumns = false;
			this.dataGridDropDown.AllowUserToResizeRows = false;
			this.dataGridDropDown.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridDropDown.ColumnHeadersVisible = false;
			this.dataGridDropDown.Location = new System.Drawing.Point(12, 12);
			this.dataGridDropDown.Name = "dataGridDropDown";
			this.dataGridDropDown.ReadOnly = true;
			this.dataGridDropDown.RowHeadersVisible = false;
			this.dataGridDropDown.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dataGridDropDown.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridDropDown.Size = new System.Drawing.Size(152, 150);
			this.dataGridDropDown.TabIndex = 2;
			this.dataGridDropDown.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridDropDown_CellClick);
			this.dataGridDropDown.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridDropDown_CellMouseLeave);
			this.dataGridDropDown.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridDropDown_CellMouseMove);
			this.dataGridDropDown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridDropDown_KeyPress);
			this.dataGridDropDown.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dataGridDropDown_PreviewKeyDown);
			// 
			// scrollY
			// 
			this.scrollY.BackColor = System.Drawing.Color.Transparent;
			this.scrollY.Image = null;
			this.scrollY.Location = new System.Drawing.Point(171, 12);
			this.scrollY.Name = "scrollY";
			this.scrollY.ScrollElementsTotals = 100;
			this.scrollY.ScrollElementsVisible = 20;
			this.scrollY.ScrollHide = true;
			this.scrollY.ScrollNecessary = true;
			this.scrollY.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.scrollY.ScrollPosition = 0;
			this.scrollY.Size = new System.Drawing.Size(17, 150);
			this.scrollY.TabIndex = 3;
			this.scrollY.Text = "badScrollBar1";
			this.scrollY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollY_MouseDown);
			this.scrollY.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollY_MouseMove);
			// 
			// DropDownGrid
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
			this.ClientSize = new System.Drawing.Size(203, 179);
			this.ControlBox = false;
			this.Controls.Add(this.dataGridDropDown);
			this.Controls.Add(this.scrollY);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "DropDownGrid";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.Deactivate += new System.EventHandler(this.DropDownGrid_Deactivate);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DropDownGrid_FormClosed);
			this.Load += new System.EventHandler(this.DropDown_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.DropDownGrid_Paint);
			((System.ComponentModel.ISupportInitialize)(this.dataGridDropDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridDropDown;
		private BadScrollBar scrollY;
	}
}