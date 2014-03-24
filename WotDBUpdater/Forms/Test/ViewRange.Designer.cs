namespace WotDBUpdater.Forms.Test
{
    partial class ViewRange
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
            this.cbBino = new System.Windows.Forms.CheckBox();
            this.cbOptics = new System.Windows.Forms.CheckBox();
            this.cbVent = new System.Windows.Forms.CheckBox();
            this.labelAwareness = new System.Windows.Forms.Label();
            this.labelRecon = new System.Windows.Forms.Label();
            this.cbCons = new System.Windows.Forms.CheckBox();
            this.textBoxBaseVR = new System.Windows.Forms.TextBox();
            this.labelBaseVR = new System.Windows.Forms.Label();
            this.cbBIA = new System.Windows.Forms.CheckBox();
            this.textBoxAwareness = new System.Windows.Forms.TextBox();
            this.textBoxRecon = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPrimarySkill = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbBino
            // 
            this.cbBino.AutoSize = true;
            this.cbBino.Location = new System.Drawing.Point(22, 201);
            this.cbBino.Name = "cbBino";
            this.cbBino.Size = new System.Drawing.Size(75, 17);
            this.cbBino.TabIndex = 4;
            this.cbBino.Text = "Binoculars";
            this.cbBino.UseVisualStyleBackColor = true;
            this.cbBino.CheckedChanged += new System.EventHandler(this.PropertiesChanged);
            // 
            // cbOptics
            // 
            this.cbOptics.AutoSize = true;
            this.cbOptics.Location = new System.Drawing.Point(22, 228);
            this.cbOptics.Name = "cbOptics";
            this.cbOptics.Size = new System.Drawing.Size(91, 17);
            this.cbOptics.TabIndex = 5;
            this.cbOptics.Text = "Coated optics";
            this.cbOptics.UseVisualStyleBackColor = true;
            this.cbOptics.CheckedChanged += new System.EventHandler(this.PropertiesChanged);
            // 
            // cbVent
            // 
            this.cbVent.AutoSize = true;
            this.cbVent.Location = new System.Drawing.Point(22, 174);
            this.cbVent.Name = "cbVent";
            this.cbVent.Size = new System.Drawing.Size(75, 17);
            this.cbVent.TabIndex = 3;
            this.cbVent.Text = "Ventilation";
            this.cbVent.UseVisualStyleBackColor = true;
            this.cbVent.CheckedChanged += new System.EventHandler(this.PropertiesChanged);
            // 
            // labelAwareness
            // 
            this.labelAwareness.AutoSize = true;
            this.labelAwareness.Location = new System.Drawing.Point(186, 201);
            this.labelAwareness.Name = "labelAwareness";
            this.labelAwareness.Size = new System.Drawing.Size(59, 13);
            this.labelAwareness.TabIndex = 0;
            this.labelAwareness.Text = "Awareness";
            // 
            // labelRecon
            // 
            this.labelRecon.AutoSize = true;
            this.labelRecon.Location = new System.Drawing.Point(186, 229);
            this.labelRecon.Name = "labelRecon";
            this.labelRecon.Size = new System.Drawing.Size(39, 13);
            this.labelRecon.TabIndex = 0;
            this.labelRecon.Text = "Recon";
            // 
            // cbCons
            // 
            this.cbCons.AutoSize = true;
            this.cbCons.Location = new System.Drawing.Point(291, 173);
            this.cbCons.Name = "cbCons";
            this.cbCons.Size = new System.Drawing.Size(89, 17);
            this.cbCons.TabIndex = 9;
            this.cbCons.Text = "Consumables";
            this.cbCons.UseVisualStyleBackColor = true;
            this.cbCons.CheckedChanged += new System.EventHandler(this.PropertiesChanged);
            // 
            // textBoxBaseVR
            // 
            this.textBoxBaseVR.Location = new System.Drawing.Point(22, 89);
            this.textBoxBaseVR.MaxLength = 3;
            this.textBoxBaseVR.Name = "textBoxBaseVR";
            this.textBoxBaseVR.Size = new System.Drawing.Size(28, 20);
            this.textBoxBaseVR.TabIndex = 1;
            this.textBoxBaseVR.Leave += new System.EventHandler(this.PropertiesChanged);
            // 
            // labelBaseVR
            // 
            this.labelBaseVR.AutoSize = true;
            this.labelBaseVR.Location = new System.Drawing.Point(342, 296);
            this.labelBaseVR.Name = "labelBaseVR";
            this.labelBaseVR.Size = new System.Drawing.Size(13, 13);
            this.labelBaseVR.TabIndex = 10;
            this.labelBaseVR.Text = "0";
            // 
            // cbBIA
            // 
            this.cbBIA.AutoSize = true;
            this.cbBIA.Location = new System.Drawing.Point(167, 173);
            this.cbBIA.Name = "cbBIA";
            this.cbBIA.Size = new System.Drawing.Size(46, 17);
            this.cbBIA.TabIndex = 6;
            this.cbBIA.Text = " BIA";
            this.cbBIA.UseVisualStyleBackColor = true;
            this.cbBIA.CheckedChanged += new System.EventHandler(this.PropertiesChanged);
            // 
            // textBoxAwareness
            // 
            this.textBoxAwareness.Location = new System.Drawing.Point(152, 199);
            this.textBoxAwareness.MaxLength = 3;
            this.textBoxAwareness.Name = "textBoxAwareness";
            this.textBoxAwareness.Size = new System.Drawing.Size(28, 20);
            this.textBoxAwareness.TabIndex = 7;
            this.textBoxAwareness.Leave += new System.EventHandler(this.PropertiesChanged);
            // 
            // textBoxRecon
            // 
            this.textBoxRecon.Location = new System.Drawing.Point(152, 226);
            this.textBoxRecon.MaxLength = 3;
            this.textBoxRecon.Name = "textBoxRecon";
            this.textBoxRecon.Size = new System.Drawing.Size(28, 20);
            this.textBoxRecon.TabIndex = 8;
            this.textBoxRecon.Leave += new System.EventHandler(this.PropertiesChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Base view range";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Primary skill";
            // 
            // textBoxPrimarySkill
            // 
            this.textBoxPrimarySkill.Location = new System.Drawing.Point(22, 115);
            this.textBoxPrimarySkill.MaxLength = 3;
            this.textBoxPrimarySkill.Name = "textBoxPrimarySkill";
            this.textBoxPrimarySkill.Size = new System.Drawing.Size(28, 20);
            this.textBoxPrimarySkill.TabIndex = 2;
            this.textBoxPrimarySkill.Text = "100";
            this.textBoxPrimarySkill.Leave += new System.EventHandler(this.PropertiesChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(229, 296);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Effective view range:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "View range calculator";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(22, 291);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(43, 23);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ViewRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 340);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxPrimarySkill);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxRecon);
            this.Controls.Add(this.textBoxAwareness);
            this.Controls.Add(this.cbBIA);
            this.Controls.Add(this.labelBaseVR);
            this.Controls.Add(this.textBoxBaseVR);
            this.Controls.Add(this.cbCons);
            this.Controls.Add(this.labelRecon);
            this.Controls.Add(this.labelAwareness);
            this.Controls.Add(this.cbVent);
            this.Controls.Add(this.cbOptics);
            this.Controls.Add(this.cbBino);
            this.MinimizeBox = false;
            this.Name = "ViewRange";
            this.Text = "ViewRange";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbBino;
        private System.Windows.Forms.CheckBox cbOptics;
        private System.Windows.Forms.CheckBox cbVent;
        private System.Windows.Forms.Label labelAwareness;
        private System.Windows.Forms.Label labelRecon;
        private System.Windows.Forms.CheckBox cbCons;
        private System.Windows.Forms.TextBox textBoxBaseVR;
        private System.Windows.Forms.Label labelBaseVR;
        private System.Windows.Forms.CheckBox cbBIA;
        private System.Windows.Forms.TextBox textBoxAwareness;
        private System.Windows.Forms.TextBox textBoxRecon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPrimarySkill;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClear;
    }
}