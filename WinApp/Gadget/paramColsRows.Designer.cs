namespace WinApp.Gadget
{
	partial class paramColsRows
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
            this.badForm1 = new BadForm();
            this.btnCancel = new BadButton();
            this.btnSelect = new BadButton();
            this.txtRows = new BadTextBox();
            this.txtCols = new BadTextBox();
            this.badLabel2 = new BadLabel();
            this.badLabel1 = new BadLabel();
            this.badForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // badForm1
            // 
            this.badForm1.Controls.Add(this.btnCancel);
            this.badForm1.Controls.Add(this.btnSelect);
            this.badForm1.Controls.Add(this.txtRows);
            this.badForm1.Controls.Add(this.txtCols);
            this.badForm1.Controls.Add(this.badLabel2);
            this.badForm1.Controls.Add(this.badLabel1);
            this.badForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.badForm1.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.badForm1.FormExitAsMinimize = false;
            this.badForm1.FormFooter = false;
            this.badForm1.FormFooterHeight = 26;
            this.badForm1.FormInnerBorder = 3;
            this.badForm1.FormMargin = 0;
            this.badForm1.Image = null;
            this.badForm1.Location = new System.Drawing.Point(0, 0);
            this.badForm1.MainArea = mainAreaClass1;
            this.badForm1.Name = "badForm1";
            this.badForm1.Resizable = true;
            this.badForm1.Size = new System.Drawing.Size(199, 161);
            this.badForm1.SystemExitImage = null;
            this.badForm1.SystemMaximizeImage = null;
            this.badForm1.SystemMinimizeImage = null;
            this.badForm1.TabIndex = 0;
            this.badForm1.Text = "Select Size";
            this.badForm1.TitleHeight = 26;
            // 
            // btnCancel
            // 
            this.btnCancel.BlackButton = false;
            this.btnCancel.Checked = false;
            this.btnCancel.Image = null;
            this.btnCancel.Location = new System.Drawing.Point(105, 114);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTipText = "";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.BlackButton = false;
            this.btnSelect.Checked = false;
            this.btnSelect.Image = null;
            this.btnSelect.Location = new System.Drawing.Point(35, 114);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(64, 23);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "Save";
            this.btnSelect.ToolTipText = "";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtRows
            // 
            this.txtRows.HasFocus = false;
            this.txtRows.Image = null;
            this.txtRows.Location = new System.Drawing.Point(105, 74);
            this.txtRows.MultilineAllow = false;
            this.txtRows.Name = "txtRows";
            this.txtRows.PasswordChar = '\0';
            this.txtRows.ReadOnly = false;
            this.txtRows.Size = new System.Drawing.Size(64, 23);
            this.txtRows.TabIndex = 3;
            this.txtRows.Text = "2";
            this.txtRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRows.ToolTipText = "";
            // 
            // txtCols
            // 
            this.txtCols.HasFocus = false;
            this.txtCols.Image = null;
            this.txtCols.Location = new System.Drawing.Point(105, 45);
            this.txtCols.MultilineAllow = false;
            this.txtCols.Name = "txtCols";
            this.txtCols.PasswordChar = '\0';
            this.txtCols.ReadOnly = false;
            this.txtCols.Size = new System.Drawing.Size(64, 23);
            this.txtCols.TabIndex = 2;
            this.txtCols.Text = "5";
            this.txtCols.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCols.ToolTipText = "";
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(35, 74);
            this.badLabel2.Name = "badLabel2";
            this.badLabel2.Size = new System.Drawing.Size(53, 23);
            this.badLabel2.TabIndex = 1;
            this.badLabel2.Text = "Rows";
            this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(35, 44);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(53, 23);
            this.badLabel1.TabIndex = 0;
            this.badLabel1.Text = "Columns";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // paramColsRows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 161);
            this.Controls.Add(this.badForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "paramColsRows";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "paramColsRows";
            this.Load += new System.EventHandler(this.paramColsRows_Load);
            this.badForm1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private BadForm badForm1;
		private BadTextBox txtRows;
		private BadTextBox txtCols;
		private BadLabel badLabel2;
		private BadLabel badLabel1;
		private BadButton btnCancel;
		private BadButton btnSelect;
	}
}