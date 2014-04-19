namespace WotDBUpdater.Forms.Reports
{
	partial class DBTable
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBTable));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.DBTableTheme = new BadForm();
			this.btnRefresh = new BadButton();
			this.popupSelectTable = new BadPopupBox();
			this.badLabel1 = new BadLabel();
			this.dataGridViewShowTable = new System.Windows.Forms.DataGridView();
			this.DBTableTheme.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewShowTable)).BeginInit();
			this.SuspendLayout();
			// 
			// DBTableTheme
			// 
			this.DBTableTheme.Controls.Add(this.btnRefresh);
			this.DBTableTheme.Controls.Add(this.popupSelectTable);
			this.DBTableTheme.Controls.Add(this.badLabel1);
			this.DBTableTheme.Controls.Add(this.dataGridViewShowTable);
			this.DBTableTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DBTableTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.DBTableTheme.FormFooter = false;
			this.DBTableTheme.FormFooterHeight = 26;
			this.DBTableTheme.FormInnerBorder = 3;
			this.DBTableTheme.FormMargin = 0;
			this.DBTableTheme.Image = ((System.Drawing.Image)(resources.GetObject("DBTableTheme.Image")));
			this.DBTableTheme.Location = new System.Drawing.Point(0, 0);
			this.DBTableTheme.MainArea = mainAreaClass1;
			this.DBTableTheme.Name = "DBTableTheme";
			this.DBTableTheme.Resizable = true;
			this.DBTableTheme.Size = new System.Drawing.Size(438, 320);
			this.DBTableTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("DBTableTheme.SystemExitImage")));
			this.DBTableTheme.SystemMaximizeImage = ((System.Drawing.Image)(resources.GetObject("DBTableTheme.SystemMaximizeImage")));
			this.DBTableTheme.SystemMinimizeImage = ((System.Drawing.Image)(resources.GetObject("DBTableTheme.SystemMinimizeImage")));
			this.DBTableTheme.TabIndex = 5;
			this.DBTableTheme.Text = "Database Tables";
			this.DBTableTheme.TitleHeight = 26;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Image = null;
			this.btnRefresh.Location = new System.Drawing.Point(347, 38);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(75, 23);
			this.btnRefresh.TabIndex = 7;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// popupSelectTable
			// 
			this.popupSelectTable.Image = ((System.Drawing.Image)(resources.GetObject("popupSelectTable.Image")));
			this.popupSelectTable.Location = new System.Drawing.Point(93, 38);
			this.popupSelectTable.Name = "popupSelectTable";
			this.popupSelectTable.Size = new System.Drawing.Size(248, 23);
			this.popupSelectTable.TabIndex = 6;
			this.popupSelectTable.Text = null;
			this.popupSelectTable.Click += new System.EventHandler(this.popupSelectTable_Click);
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(12, 38);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(75, 23);
			this.badLabel1.TabIndex = 5;
			this.badLabel1.Text = "Select Table:";
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
			this.dataGridViewShowTable.Location = new System.Drawing.Point(12, 78);
			this.dataGridViewShowTable.Name = "dataGridViewShowTable";
			this.dataGridViewShowTable.ReadOnly = true;
			this.dataGridViewShowTable.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dataGridViewShowTable.Size = new System.Drawing.Size(378, 191);
			this.dataGridViewShowTable.TabIndex = 0;
			// 
			// DBTable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(438, 320);
			this.Controls.Add(this.DBTableTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(438, 320);
			this.Name = "DBTable";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Show Database Table";
			this.Load += new System.EventHandler(this.frmDBTable_Load);
			this.SizeChanged += new System.EventHandler(this.frmDBTable_SizeChanged);
			this.DBTableTheme.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewShowTable)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewShowTable;
		private BadForm DBTableTheme;
		private BadButton btnRefresh;
		private BadPopupBox popupSelectTable;
		private BadLabel badLabel1;

	}
}