namespace WotDBUpdater.Forms.Test
{
    partial class TestShowImage
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestShowImage));
			this.TestShowImageTheme = new BadForm();
			this.SuspendLayout();
			// 
			// TestShowImageTheme
			// 
			this.TestShowImageTheme.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
			this.TestShowImageTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TestShowImageTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.TestShowImageTheme.FormFooter = false;
			this.TestShowImageTheme.FormFooterHeight = 26;
			this.TestShowImageTheme.FormInnerBorder = 3;
			this.TestShowImageTheme.FormMargin = 0;
			this.TestShowImageTheme.Image = null;
			this.TestShowImageTheme.Location = new System.Drawing.Point(0, 0);
			this.TestShowImageTheme.MainArea = mainAreaClass1;
			this.TestShowImageTheme.Name = "TestShowImageTheme";
			this.TestShowImageTheme.Resizable = true;
			this.TestShowImageTheme.Size = new System.Drawing.Size(318, 286);
			this.TestShowImageTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("TestShowImageTheme.SystemExitImage")));
			this.TestShowImageTheme.SystemMaximizeImage = null;
			this.TestShowImageTheme.SystemMinimizeImage = null;
			this.TestShowImageTheme.TabIndex = 0;
			this.TestShowImageTheme.Text = "Tank Image Test";
			this.TestShowImageTheme.TitleHeight = 26;
			// 
			// TestShowImage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(318, 286);
			this.Controls.Add(this.TestShowImageTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "TestShowImage";
			this.Text = "TestShowImage";
			this.Load += new System.EventHandler(this.TestShowImage_Load);
			this.ResumeLayout(false);

        }

        #endregion

		private BadForm TestShowImageTheme;



    }
}