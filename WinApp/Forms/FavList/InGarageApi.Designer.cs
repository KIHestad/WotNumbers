namespace WinApp.Forms
{
	partial class InGarageApi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InGarageApi));
            this.badForm1 = new BadForm();
            this.pnlBrowser = new System.Windows.Forms.Panel();
            this.txtAddress = new BadTextBox();
            this.badForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // badForm1
            // 
            this.badForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badForm1.Controls.Add(this.pnlBrowser);
            this.badForm1.Controls.Add(this.txtAddress);
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
            this.badForm1.MinimumSize = new System.Drawing.Size(380, 500);
            this.badForm1.Name = "badForm1";
            this.badForm1.Resizable = true;
            this.badForm1.Size = new System.Drawing.Size(606, 678);
            this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
            this.badForm1.SystemMaximizeImage = null;
            this.badForm1.SystemMinimizeImage = null;
            this.badForm1.TabIndex = 0;
            this.badForm1.Text = "Login to Wargaming account to get \"In Garage\" Tanks";
            this.badForm1.TitleHeight = 26;
            // 
            // pnlBrowser
            // 
            this.pnlBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBrowser.BackColor = System.Drawing.Color.Transparent;
            this.pnlBrowser.Location = new System.Drawing.Point(13, 64);
            this.pnlBrowser.Name = "pnlBrowser";
            this.pnlBrowser.Size = new System.Drawing.Size(580, 598);
            this.pnlBrowser.TabIndex = 2;
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddress.HasFocus = false;
            this.txtAddress.Image = null;
            this.txtAddress.Location = new System.Drawing.Point(13, 35);
            this.txtAddress.MultilineAllow = false;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.PasswordChar = '\0';
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(580, 23);
            this.txtAddress.TabIndex = 1;
            this.txtAddress.Text = "Loading...";
            this.txtAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtAddress.ToolTipText = "Loading...";
            // 
            // InGarageApi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Fuchsia;
            this.ClientSize = new System.Drawing.Size(606, 678);
            this.Controls.Add(this.badForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(380, 500);
            this.Name = "InGarageApi";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GetInGarage";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.InGarageApi_Load);
            this.badForm1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private BadForm badForm1;
		private BadTextBox txtAddress;
        private System.Windows.Forms.Panel pnlBrowser;
	}
}