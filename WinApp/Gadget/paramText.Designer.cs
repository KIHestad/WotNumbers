namespace WinApp.Gadget
{
	partial class paramText
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
            this.badLabel1 = new BadLabel();
            this.txtText = new BadTextBox();
            this.badForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // badForm1
            // 
            this.badForm1.Controls.Add(this.txtText);
            this.badForm1.Controls.Add(this.btnCancel);
            this.badForm1.Controls.Add(this.btnSelect);
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
            this.badForm1.Resizable = false;
            this.badForm1.Size = new System.Drawing.Size(260, 137);
            this.badForm1.SystemExitImage = null;
            this.badForm1.SystemMaximizeImage = null;
            this.badForm1.SystemMinimizeImage = null;
            this.badForm1.TabIndex = 0;
            this.badForm1.Text = "Parameter";
            this.badForm1.TitleHeight = 26;
            // 
            // btnCancel
            // 
            this.btnCancel.BlackButton = false;
            this.btnCancel.Checked = false;
            this.btnCancel.Image = null;
            this.btnCancel.Location = new System.Drawing.Point(173, 90);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTipText = "";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.BlackButton = false;
            this.btnSelect.Checked = false;
            this.btnSelect.Image = null;
            this.btnSelect.Location = new System.Drawing.Point(103, 90);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(64, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Select";
            this.btnSelect.ToolTipText = "";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(25, 48);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(39, 23);
            this.badLabel1.TabIndex = 0;
            this.badLabel1.Text = "Text:";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // txtText
            // 
            this.txtText.HasFocus = false;
            this.txtText.Image = null;
            this.txtText.Location = new System.Drawing.Point(62, 48);
            this.txtText.MultilineAllow = false;
            this.txtText.Name = "txtText";
            this.txtText.PasswordChar = '\0';
            this.txtText.ReadOnly = false;
            this.txtText.Size = new System.Drawing.Size(175, 23);
            this.txtText.TabIndex = 4;
            this.txtText.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtText.ToolTipText = "";
            // 
            // paramText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 137);
            this.Controls.Add(this.badForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "paramText";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ucGaugeWinRateParameter";
            this.Load += new System.EventHandler(this.paramText_Load);
            this.badForm1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private BadForm badForm1;
        private BadButton btnSelect;
		private BadLabel badLabel1;
        private BadButton btnCancel;
        private BadTextBox txtText;
	}
}