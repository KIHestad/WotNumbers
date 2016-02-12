namespace WinApp.Forms
{
	partial class BattleChart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleChart));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.BattleChartTheme = new BadForm();
            this.chkSpline = new BadCheckBox();
            this.ddMode = new BadDropDownBox();
            this.badLabel5 = new BadLabel();
            this.chkBullet = new BadCheckBox();
            this.lblFooter = new System.Windows.Forms.Label();
            this.ddPeriod = new BadDropDownBox();
            this.badLabel4 = new BadLabel();
            this.btnClearChart = new BadButton();
            this.ddXaxis = new BadDropDownBox();
            this.badLabel3 = new BadLabel();
            this.ddValue = new BadDropDownBox();
            this.badLabel2 = new BadLabel();
            this.btnAddChart = new BadButton();
            this.ddTank = new BadDropDownBox();
            this.badLabel1 = new BadLabel();
            this.ChartingMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.BattleChartTheme.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartingMain)).BeginInit();
            this.SuspendLayout();
            // 
            // BattleChartTheme
            // 
            this.BattleChartTheme.BackColor = System.Drawing.Color.Fuchsia;
            this.BattleChartTheme.Controls.Add(this.chkSpline);
            this.BattleChartTheme.Controls.Add(this.ddMode);
            this.BattleChartTheme.Controls.Add(this.badLabel5);
            this.BattleChartTheme.Controls.Add(this.chkBullet);
            this.BattleChartTheme.Controls.Add(this.lblFooter);
            this.BattleChartTheme.Controls.Add(this.ddPeriod);
            this.BattleChartTheme.Controls.Add(this.badLabel4);
            this.BattleChartTheme.Controls.Add(this.btnClearChart);
            this.BattleChartTheme.Controls.Add(this.ddXaxis);
            this.BattleChartTheme.Controls.Add(this.badLabel3);
            this.BattleChartTheme.Controls.Add(this.ddValue);
            this.BattleChartTheme.Controls.Add(this.badLabel2);
            this.BattleChartTheme.Controls.Add(this.btnAddChart);
            this.BattleChartTheme.Controls.Add(this.ddTank);
            this.BattleChartTheme.Controls.Add(this.badLabel1);
            this.BattleChartTheme.Controls.Add(this.ChartingMain);
            this.BattleChartTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BattleChartTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BattleChartTheme.FormExitAsMinimize = false;
            this.BattleChartTheme.FormFooter = true;
            this.BattleChartTheme.FormFooterHeight = 26;
            this.BattleChartTheme.FormInnerBorder = 0;
            this.BattleChartTheme.FormMargin = 0;
            this.BattleChartTheme.Image = null;
            this.BattleChartTheme.Location = new System.Drawing.Point(0, 0);
            this.BattleChartTheme.MainArea = mainAreaClass1;
            this.BattleChartTheme.Name = "BattleChartTheme";
            this.BattleChartTheme.Resizable = true;
            this.BattleChartTheme.Size = new System.Drawing.Size(948, 496);
            this.BattleChartTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("BattleChartTheme.SystemExitImage")));
            this.BattleChartTheme.SystemMaximizeImage = ((System.Drawing.Image)(resources.GetObject("BattleChartTheme.SystemMaximizeImage")));
            this.BattleChartTheme.SystemMinimizeImage = ((System.Drawing.Image)(resources.GetObject("BattleChartTheme.SystemMinimizeImage")));
            this.BattleChartTheme.TabIndex = 0;
            this.BattleChartTheme.Text = "Chart";
            this.BattleChartTheme.TitleHeight = 26;
            // 
            // chkSpline
            // 
            this.chkSpline.BackColor = System.Drawing.Color.Transparent;
            this.chkSpline.Checked = false;
            this.chkSpline.Image = ((System.Drawing.Image)(resources.GetObject("chkSpline.Image")));
            this.chkSpline.Location = new System.Drawing.Point(774, 38);
            this.chkSpline.Name = "chkSpline";
            this.chkSpline.Size = new System.Drawing.Size(66, 23);
            this.chkSpline.TabIndex = 21;
            this.chkSpline.Text = "Spline";
            this.chkSpline.Click += new System.EventHandler(this.chkSpline_Click);
            // 
            // ddMode
            // 
            this.ddMode.Image = null;
            this.ddMode.Location = new System.Drawing.Point(219, 38);
            this.ddMode.Name = "ddMode";
            this.ddMode.Size = new System.Drawing.Size(96, 23);
            this.ddMode.TabIndex = 19;
            this.ddMode.Text = "Random";
            this.ddMode.TextChanged += new System.EventHandler(this.ddMode_TextChanged);
            this.ddMode.Click += new System.EventHandler(this.ddMode_Click);
            // 
            // badLabel5
            // 
            this.badLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel5.Dimmed = false;
            this.badLabel5.Image = null;
            this.badLabel5.Location = new System.Drawing.Point(179, 38);
            this.badLabel5.Name = "badLabel5";
            this.badLabel5.Size = new System.Drawing.Size(39, 23);
            this.badLabel5.TabIndex = 20;
            this.badLabel5.Text = "Mode:";
            this.badLabel5.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // chkBullet
            // 
            this.chkBullet.BackColor = System.Drawing.Color.Transparent;
            this.chkBullet.Checked = false;
            this.chkBullet.Image = ((System.Drawing.Image)(resources.GetObject("chkBullet.Image")));
            this.chkBullet.Location = new System.Drawing.Point(719, 38);
            this.chkBullet.Name = "chkBullet";
            this.chkBullet.Size = new System.Drawing.Size(62, 23);
            this.chkBullet.TabIndex = 18;
            this.chkBullet.Text = "Bullet";
            this.chkBullet.Click += new System.EventHandler(this.chkBullet_Click);
            // 
            // lblFooter
            // 
            this.lblFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFooter.AutoSize = true;
            this.lblFooter.BackColor = System.Drawing.Color.Transparent;
            this.lblFooter.ForeColor = System.Drawing.Color.DarkGray;
            this.lblFooter.Location = new System.Drawing.Point(8, 476);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(204, 13);
            this.lblFooter.TabIndex = 17;
            this.lblFooter.Text = "No chart selected, please add a chart line";
            // 
            // ddPeriod
            // 
            this.ddPeriod.Image = null;
            this.ddPeriod.Location = new System.Drawing.Point(639, 38);
            this.ddPeriod.Name = "ddPeriod";
            this.ddPeriod.Size = new System.Drawing.Size(75, 23);
            this.ddPeriod.TabIndex = 16;
            this.ddPeriod.Text = "( All )";
            this.ddPeriod.TextChanged += new System.EventHandler(this.ddPeriod_TextChanged);
            this.ddPeriod.Click += new System.EventHandler(this.ddPeriod_Click);
            // 
            // badLabel4
            // 
            this.badLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel4.Dimmed = false;
            this.badLabel4.Image = null;
            this.badLabel4.Location = new System.Drawing.Point(598, 38);
            this.badLabel4.Name = "badLabel4";
            this.badLabel4.Size = new System.Drawing.Size(47, 23);
            this.badLabel4.TabIndex = 15;
            this.badLabel4.Text = "Period:";
            this.badLabel4.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // btnClearChart
            // 
            this.btnClearChart.BlackButton = false;
            this.btnClearChart.Checked = false;
            this.btnClearChart.Image = null;
            this.btnClearChart.Location = new System.Drawing.Point(891, 38);
            this.btnClearChart.Name = "btnClearChart";
            this.btnClearChart.Size = new System.Drawing.Size(42, 23);
            this.btnClearChart.TabIndex = 14;
            this.btnClearChart.Text = "Clear";
            this.btnClearChart.ToolTipText = "";
            this.btnClearChart.Click += new System.EventHandler(this.btnClearChart_Click);
            // 
            // ddXaxis
            // 
            this.ddXaxis.Image = null;
            this.ddXaxis.Location = new System.Drawing.Point(509, 38);
            this.ddXaxis.Name = "ddXaxis";
            this.ddXaxis.Size = new System.Drawing.Size(80, 23);
            this.ddXaxis.TabIndex = 13;
            this.ddXaxis.Text = "Date";
            this.ddXaxis.TextChanged += new System.EventHandler(this.ddXaxis_TextChanged);
            this.ddXaxis.Click += new System.EventHandler(this.ddXaxis_Click);
            // 
            // badLabel3
            // 
            this.badLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel3.Dimmed = false;
            this.badLabel3.Image = null;
            this.badLabel3.Location = new System.Drawing.Point(468, 38);
            this.badLabel3.Name = "badLabel3";
            this.badLabel3.Size = new System.Drawing.Size(47, 23);
            this.badLabel3.TabIndex = 12;
            this.badLabel3.Text = "X-Axis:";
            this.badLabel3.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // ddValue
            // 
            this.ddValue.Image = null;
            this.ddValue.Location = new System.Drawing.Point(359, 38);
            this.ddValue.Name = "ddValue";
            this.ddValue.Size = new System.Drawing.Size(102, 23);
            this.ddValue.TabIndex = 11;
            this.ddValue.TextChanged += new System.EventHandler(this.ddValue_TextChanged);
            this.ddValue.Click += new System.EventHandler(this.ddValue_Click);
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(321, 38);
            this.badLabel2.Name = "badLabel2";
            this.badLabel2.Size = new System.Drawing.Size(49, 23);
            this.badLabel2.TabIndex = 10;
            this.badLabel2.Text = "Value:";
            this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // btnAddChart
            // 
            this.btnAddChart.BlackButton = false;
            this.btnAddChart.Checked = false;
            this.btnAddChart.Image = null;
            this.btnAddChart.Location = new System.Drawing.Point(843, 38);
            this.btnAddChart.Name = "btnAddChart";
            this.btnAddChart.Size = new System.Drawing.Size(42, 23);
            this.btnAddChart.TabIndex = 8;
            this.btnAddChart.Text = "Add";
            this.btnAddChart.ToolTipText = "";
            this.btnAddChart.Click += new System.EventHandler(this.btnAddChart_Click);
            // 
            // ddTank
            // 
            this.ddTank.Image = null;
            this.ddTank.Location = new System.Drawing.Point(45, 38);
            this.ddTank.Name = "ddTank";
            this.ddTank.Size = new System.Drawing.Size(128, 23);
            this.ddTank.TabIndex = 3;
            this.ddTank.TextChanged += new System.EventHandler(this.ddTank_TextChanged);
            this.ddTank.Click += new System.EventHandler(this.ddTank_Click);
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(10, 38);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(41, 23);
            this.badLabel1.TabIndex = 7;
            this.badLabel1.Text = "Tank:";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // ChartingMain
            // 
            this.ChartingMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChartingMain.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY.MinorTickMark.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.Silver;
            chartArea1.AxisY2.LabelStyle.ForeColor = System.Drawing.Color.Silver;
            chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY2.MajorTickMark.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY2.MinorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY2.MinorTickMark.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY2.ScaleBreakStyle.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY2.ScaleBreakStyle.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.AxisY2.TitleForeColor = System.Drawing.Color.Silver;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea1.BorderColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            chartArea1.ShadowColor = System.Drawing.Color.Transparent;
            this.ChartingMain.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            legend1.ForeColor = System.Drawing.Color.Silver;
            legend1.IsDockedInsideChartArea = false;
            legend1.IsEquallySpacedItems = true;
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.ChartingMain.Legends.Add(legend1);
            this.ChartingMain.Location = new System.Drawing.Point(5, 67);
            this.ChartingMain.Name = "ChartingMain";
            this.ChartingMain.Size = new System.Drawing.Size(936, 397);
            this.ChartingMain.TabIndex = 0;
            this.ChartingMain.Text = "chart1";
            this.ChartingMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChartingMain_MouseMove);
            // 
            // BattleChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Fuchsia;
            this.ClientSize = new System.Drawing.Size(948, 496);
            this.Controls.Add(this.BattleChartTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "BattleChart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Battle Chart";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BattleChart_FormClosed);
            this.Load += new System.EventHandler(this.BattleChart_Load);
            this.Shown += new System.EventHandler(this.BattleChart_Shown);
            this.BattleChartTheme.ResumeLayout(false);
            this.BattleChartTheme.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartingMain)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private BadForm BattleChartTheme;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartingMain;
		private BadLabel badLabel1;
		private BadDropDownBox ddXaxis;
		private BadLabel badLabel3;
		private BadDropDownBox ddValue;
		private BadLabel badLabel2;
		private BadButton btnAddChart;
		private BadButton btnClearChart;
		private BadDropDownBox ddPeriod;
		private BadLabel badLabel4;
		private System.Windows.Forms.Label lblFooter;
		private BadCheckBox chkBullet;
        private BadDropDownBox ddMode;
        private BadLabel badLabel5;
        private BadCheckBox chkSpline;
        private BadDropDownBox ddTank;



	}
}