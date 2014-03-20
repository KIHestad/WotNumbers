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
            this.comboAwareness = new System.Windows.Forms.ComboBox();
            this.labelAwareness = new System.Windows.Forms.Label();
            this.comboRecon = new System.Windows.Forms.ComboBox();
            this.labelRecon = new System.Windows.Forms.Label();
            this.cbCons = new System.Windows.Forms.CheckBox();
            this.buttonCalcViewRange = new System.Windows.Forms.Button();
            this.textBoxBaseVR = new System.Windows.Forms.TextBox();
            this.labelBaseVR = new System.Windows.Forms.Label();
            this.cbBIA = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbBino
            // 
            this.cbBino.AutoSize = true;
            this.cbBino.Location = new System.Drawing.Point(19, 150);
            this.cbBino.Name = "cbBino";
            this.cbBino.Size = new System.Drawing.Size(75, 17);
            this.cbBino.TabIndex = 2;
            this.cbBino.Text = "Binoculars";
            this.cbBino.UseVisualStyleBackColor = true;
            this.cbBino.CheckedChanged += new System.EventHandler(this.PropertiesChanged);
            // 
            // cbOptics
            // 
            this.cbOptics.AutoSize = true;
            this.cbOptics.Location = new System.Drawing.Point(19, 177);
            this.cbOptics.Name = "cbOptics";
            this.cbOptics.Size = new System.Drawing.Size(91, 17);
            this.cbOptics.TabIndex = 3;
            this.cbOptics.Text = "Coated optics";
            this.cbOptics.UseVisualStyleBackColor = true;
            this.cbOptics.CheckedChanged += new System.EventHandler(this.PropertiesChanged);
            // 
            // cbVent
            // 
            this.cbVent.AutoSize = true;
            this.cbVent.Location = new System.Drawing.Point(19, 123);
            this.cbVent.Name = "cbVent";
            this.cbVent.Size = new System.Drawing.Size(75, 17);
            this.cbVent.TabIndex = 1;
            this.cbVent.Text = "Ventilation";
            this.cbVent.UseVisualStyleBackColor = true;
            this.cbVent.CheckedChanged += new System.EventHandler(this.PropertiesChanged);
            // 
            // comboAwareness
            // 
            this.comboAwareness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAwareness.FormattingEnabled = true;
            this.comboAwareness.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100"});
            this.comboAwareness.Location = new System.Drawing.Point(149, 148);
            this.comboAwareness.Name = "comboAwareness";
            this.comboAwareness.Size = new System.Drawing.Size(43, 21);
            this.comboAwareness.TabIndex = 5;
            this.comboAwareness.SelectedIndexChanged += new System.EventHandler(this.PropertiesChanged);
            // 
            // labelAwareness
            // 
            this.labelAwareness.AutoSize = true;
            this.labelAwareness.Location = new System.Drawing.Point(198, 152);
            this.labelAwareness.Name = "labelAwareness";
            this.labelAwareness.Size = new System.Drawing.Size(59, 13);
            this.labelAwareness.TabIndex = 0;
            this.labelAwareness.Text = "Awareness";
            // 
            // comboRecon
            // 
            this.comboRecon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRecon.FormattingEnabled = true;
            this.comboRecon.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100"});
            this.comboRecon.Location = new System.Drawing.Point(149, 175);
            this.comboRecon.Name = "comboRecon";
            this.comboRecon.Size = new System.Drawing.Size(43, 21);
            this.comboRecon.TabIndex = 6;
            this.comboRecon.SelectedIndexChanged += new System.EventHandler(this.PropertiesChanged);
            // 
            // labelRecon
            // 
            this.labelRecon.AutoSize = true;
            this.labelRecon.Location = new System.Drawing.Point(199, 177);
            this.labelRecon.Name = "labelRecon";
            this.labelRecon.Size = new System.Drawing.Size(39, 13);
            this.labelRecon.TabIndex = 0;
            this.labelRecon.Text = "Recon";
            // 
            // cbCons
            // 
            this.cbCons.AutoSize = true;
            this.cbCons.Location = new System.Drawing.Point(298, 123);
            this.cbCons.Name = "cbCons";
            this.cbCons.Size = new System.Drawing.Size(89, 17);
            this.cbCons.TabIndex = 7;
            this.cbCons.Text = "Consumables";
            this.cbCons.UseVisualStyleBackColor = true;
            this.cbCons.CheckedChanged += new System.EventHandler(this.PropertiesChanged);
            // 
            // buttonCalcViewRange
            // 
            this.buttonCalcViewRange.Location = new System.Drawing.Point(257, 245);
            this.buttonCalcViewRange.Name = "buttonCalcViewRange";
            this.buttonCalcViewRange.Size = new System.Drawing.Size(130, 23);
            this.buttonCalcViewRange.TabIndex = 8;
            this.buttonCalcViewRange.Text = "Calculate view range";
            this.buttonCalcViewRange.UseVisualStyleBackColor = true;
            // 
            // textBoxBaseVR
            // 
            this.textBoxBaseVR.Location = new System.Drawing.Point(19, 64);
            this.textBoxBaseVR.Name = "textBoxBaseVR";
            this.textBoxBaseVR.Size = new System.Drawing.Size(100, 20);
            this.textBoxBaseVR.TabIndex = 9;
            // 
            // labelBaseVR
            // 
            this.labelBaseVR.AutoSize = true;
            this.labelBaseVR.Location = new System.Drawing.Point(16, 250);
            this.labelBaseVR.Name = "labelBaseVR";
            this.labelBaseVR.Size = new System.Drawing.Size(12, 13);
            this.labelBaseVR.TabIndex = 10;
            this.labelBaseVR.Text = "x";
            // 
            // cbBIA
            // 
            this.cbBIA.AutoSize = true;
            this.cbBIA.Location = new System.Drawing.Point(149, 123);
            this.cbBIA.Name = "cbBIA";
            this.cbBIA.Size = new System.Drawing.Size(43, 17);
            this.cbBIA.TabIndex = 11;
            this.cbBIA.Text = "BIA";
            this.cbBIA.UseVisualStyleBackColor = true;
            this.cbBIA.CheckedChanged += new System.EventHandler(this.PropertiesChanged);
            // 
            // ViewRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 340);
            this.Controls.Add(this.cbBIA);
            this.Controls.Add(this.labelBaseVR);
            this.Controls.Add(this.textBoxBaseVR);
            this.Controls.Add(this.buttonCalcViewRange);
            this.Controls.Add(this.cbCons);
            this.Controls.Add(this.labelRecon);
            this.Controls.Add(this.comboRecon);
            this.Controls.Add(this.labelAwareness);
            this.Controls.Add(this.comboAwareness);
            this.Controls.Add(this.cbVent);
            this.Controls.Add(this.cbOptics);
            this.Controls.Add(this.cbBino);
            this.Name = "ViewRange";
            this.Text = "ViewRange";
            this.Load += new System.EventHandler(this.ViewRange_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbBino;
        private System.Windows.Forms.CheckBox cbOptics;
        private System.Windows.Forms.CheckBox cbVent;
        private System.Windows.Forms.ComboBox comboAwareness;
        private System.Windows.Forms.Label labelAwareness;
        private System.Windows.Forms.ComboBox comboRecon;
        private System.Windows.Forms.Label labelRecon;
        private System.Windows.Forms.CheckBox cbCons;
        private System.Windows.Forms.Button buttonCalcViewRange;
        private System.Windows.Forms.TextBox textBoxBaseVR;
        private System.Windows.Forms.Label labelBaseVR;
        private System.Windows.Forms.CheckBox cbBIA;
    }
}