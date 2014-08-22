namespace WinApp.Gadget
{
	partial class ucPlayerInfo
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtPlayerName = new BadTextBox();
			this.badGroupBox1 = new BadGroupBox();
			this.SuspendLayout();
			// 
			// txtPlayerName
			// 
			this.txtPlayerName.HasFocus = false;
			this.txtPlayerName.Image = null;
			this.txtPlayerName.Location = new System.Drawing.Point(20, 39);
			this.txtPlayerName.MultilineAllow = false;
			this.txtPlayerName.Name = "txtPlayerName";
			this.txtPlayerName.PasswordChar = '\0';
			this.txtPlayerName.Size = new System.Drawing.Size(131, 23);
			this.txtPlayerName.TabIndex = 1;
			this.txtPlayerName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(3, 3);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(164, 90);
			this.badGroupBox1.TabIndex = 2;
			this.badGroupBox1.Text = "Player Name";
			// 
			// ucPlayerInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.txtPlayerName);
			this.Controls.Add(this.badGroupBox1);
			this.Name = "ucPlayerInfo";
			this.Size = new System.Drawing.Size(170, 110);
			this.Load += new System.EventHandler(this.ucPlayerInfo_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private BadTextBox txtPlayerName;
		private BadGroupBox badGroupBox1;
	}
}
