namespace WinApp.Forms.Settings
{
    partial class AppSettingsReplay
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
            this.components = new System.ComponentModel.Container();
            this.btnRemove = new BadButton();
            this.btnAdd = new BadButton();
            this.badGroupBox2 = new BadGroupBox();
            this.badLabel1 = new BadLabel();
            this.SuspendLayout();
            // 
            // btnRemove
            // 
            this.btnRemove.BlackButton = false;
            this.btnRemove.Checked = false;
            this.btnRemove.Image = null;
            this.btnRemove.Location = new System.Drawing.Point(375, 271);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(70, 23);
            this.btnRemove.TabIndex = 30;
            this.btnRemove.Text = "Remove";
            this.btnRemove.ToolTipText = "";
            // 
            // btnAdd
            // 
            this.btnAdd.BlackButton = false;
            this.btnAdd.Checked = false;
            this.btnAdd.Image = null;
            this.btnAdd.Location = new System.Drawing.Point(298, 271);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 23);
            this.btnAdd.TabIndex = 28;
            this.btnAdd.Text = "Add";
            this.btnAdd.ToolTipText = "";
            // 
            // badGroupBox2
            // 
            this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox2.Image = null;
            this.badGroupBox2.Location = new System.Drawing.Point(1, 1);
            this.badGroupBox2.Name = "badGroupBox2";
            this.badGroupBox2.Size = new System.Drawing.Size(445, 253);
            this.badGroupBox2.TabIndex = 29;
            this.badGroupBox2.Text = "Folders cointaing replay files";
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(159, 113);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(130, 23);
            this.badLabel1.TabIndex = 31;
            this.badLabel1.Text = "Not implemented yet";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AppSettingsReplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.badLabel1);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.badGroupBox2);
            this.Name = "AppSettingsReplay";
            this.Size = new System.Drawing.Size(457, 306);
            this.ResumeLayout(false);

        }

        #endregion

        private BadButton btnRemove;
        private BadButton btnAdd;
        private BadGroupBox badGroupBox2;
        private BadLabel badLabel1;
    }
}
