namespace WinApp.Forms
{
    partial class ChartLineAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartLineAdd));
            this.badForm1 = new BadForm();
            this.ddChartType = new BadDropDownBox();
            this.badLabel2 = new BadLabel();
            this.cmdCancel = new BadButton();
            this.cmdSelect = new BadButton();
            this.ddTank = new BadDropDownBox();
            this.badLabel1 = new BadLabel();
            this.badGroupBox2 = new BadGroupBox();
            this.badGroupBox1 = new BadGroupBox();
            this.badForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // badForm1
            // 
            this.badForm1.Controls.Add(this.ddChartType);
            this.badForm1.Controls.Add(this.badLabel2);
            this.badForm1.Controls.Add(this.cmdCancel);
            this.badForm1.Controls.Add(this.cmdSelect);
            this.badForm1.Controls.Add(this.ddTank);
            this.badForm1.Controls.Add(this.badLabel1);
            this.badForm1.Controls.Add(this.badGroupBox2);
            this.badForm1.Controls.Add(this.badGroupBox1);
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
            this.badForm1.Size = new System.Drawing.Size(652, 384);
            this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
            this.badForm1.SystemMaximizeImage = null;
            this.badForm1.SystemMinimizeImage = null;
            this.badForm1.TabIndex = 0;
            this.badForm1.Text = "Add Chart Line";
            this.badForm1.TitleHeight = 26;
            // 
            // ddChartType
            // 
            this.ddChartType.Image = null;
            this.ddChartType.Location = new System.Drawing.Point(391, 73);
            this.ddChartType.Name = "ddChartType";
            this.ddChartType.Size = new System.Drawing.Size(102, 23);
            this.ddChartType.TabIndex = 15;
            this.ddChartType.Click += new System.EventHandler(this.ddChartType_Click);
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(353, 73);
            this.badLabel2.Name = "badLabel2";
            this.badLabel2.Size = new System.Drawing.Size(49, 23);
            this.badLabel2.TabIndex = 14;
            this.badLabel2.Text = "Value:";
            this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // cmdCancel
            // 
            this.cmdCancel.BlackButton = false;
            this.cmdCancel.Checked = false;
            this.cmdCancel.Image = null;
            this.cmdCancel.Location = new System.Drawing.Point(553, 342);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.ToolTipText = "";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSelect
            // 
            this.cmdSelect.BlackButton = false;
            this.cmdSelect.Checked = false;
            this.cmdSelect.Image = null;
            this.cmdSelect.Location = new System.Drawing.Point(472, 342);
            this.cmdSelect.Name = "cmdSelect";
            this.cmdSelect.Size = new System.Drawing.Size(75, 23);
            this.cmdSelect.TabIndex = 2;
            this.cmdSelect.Text = "Select";
            this.cmdSelect.ToolTipText = "";
            this.cmdSelect.Click += new System.EventHandler(this.cmdSelect_Click);
            // 
            // ddTank
            // 
            this.ddTank.Image = null;
            this.ddTank.Location = new System.Drawing.Point(75, 73);
            this.ddTank.Name = "ddTank";
            this.ddTank.Size = new System.Drawing.Size(128, 23);
            this.ddTank.TabIndex = 12;
            this.ddTank.TextChanged += new System.EventHandler(this.ddTank_TextChanged);
            this.ddTank.Click += new System.EventHandler(this.ddTank_Click);
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(40, 73);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(41, 23);
            this.badLabel1.TabIndex = 13;
            this.badLabel1.Text = "Tank:";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badGroupBox2
            // 
            this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox2.Image = null;
            this.badGroupBox2.Location = new System.Drawing.Point(336, 44);
            this.badGroupBox2.Name = "badGroupBox2";
            this.badGroupBox2.Size = new System.Drawing.Size(292, 282);
            this.badGroupBox2.TabIndex = 1;
            this.badGroupBox2.Text = "Select Chart Type";
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(21, 44);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(292, 282);
            this.badGroupBox1.TabIndex = 0;
            this.badGroupBox1.Text = "Select Tank";
            // 
            // ChartLineAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 384);
            this.Controls.Add(this.badForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChartLineAdd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChartLineAdd";
            this.Load += new System.EventHandler(this.ChartLineAdd_Load);
            this.badForm1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BadForm badForm1;
        private BadButton cmdCancel;
        private BadButton cmdSelect;
        private BadGroupBox badGroupBox2;
        private BadGroupBox badGroupBox1;
        private BadDropDownBox ddChartType;
        private BadLabel badLabel2;
        private BadDropDownBox ddTank;
        private BadLabel badLabel1;
    }
}