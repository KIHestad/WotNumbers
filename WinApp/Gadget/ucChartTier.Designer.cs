namespace WinApp.Gadget
{
	partial class ucChartTier
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblChartType = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnToday = new BadButton();
            this.btnWeek = new BadButton();
            this.btnMonth = new BadButton();
            this.btnMonth3 = new BadButton();
            this.btnTotal = new BadButton();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            this.chart1.BorderlineColor = System.Drawing.Color.Maroon;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.LabelStyle.Enabled = false;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.White;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.BorderColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 100F;
            chartArea1.Position.Width = 98F;
            chartArea1.Position.X = 1F;
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(15, 3);
            this.chart1.Margin = new System.Windows.Forms.Padding(0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            series1.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.No;
            series1.SmartLabelStyle.CalloutLineColor = System.Drawing.Color.SaddleBrown;
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(249, 131);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // lblChartType
            // 
            this.lblChartType.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblChartType.BackColor = System.Drawing.Color.Transparent;
            this.lblChartType.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChartType.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblChartType.Location = new System.Drawing.Point(58, 134);
            this.lblChartType.Name = "lblChartType";
            this.lblChartType.Size = new System.Drawing.Size(186, 22);
            this.lblChartType.TabIndex = 16;
            this.lblChartType.Text = "Chart Type";
            this.lblChartType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnToday
            // 
            this.btnToday.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnToday.BlackButton = true;
            this.btnToday.Checked = false;
            this.btnToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToday.Image = null;
            this.btnToday.Location = new System.Drawing.Point(210, 157);
            this.btnToday.Margin = new System.Windows.Forms.Padding(0);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(34, 16);
            this.btnToday.TabIndex = 15;
            this.btnToday.Tag = "Today";
            this.btnToday.Text = "Today";
            this.btnToday.ToolTipText = "";
            this.btnToday.Click += new System.EventHandler(this.btnSelection_Click);
            // 
            // btnWeek
            // 
            this.btnWeek.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnWeek.BlackButton = true;
            this.btnWeek.Checked = false;
            this.btnWeek.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeek.Image = null;
            this.btnWeek.Location = new System.Drawing.Point(172, 157);
            this.btnWeek.Margin = new System.Windows.Forms.Padding(0);
            this.btnWeek.Name = "btnWeek";
            this.btnWeek.Size = new System.Drawing.Size(34, 16);
            this.btnWeek.TabIndex = 14;
            this.btnWeek.Tag = "Week";
            this.btnWeek.Text = "Week";
            this.btnWeek.ToolTipText = "";
            this.btnWeek.Click += new System.EventHandler(this.btnSelection_Click);
            // 
            // btnMonth
            // 
            this.btnMonth.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMonth.BlackButton = true;
            this.btnMonth.Checked = false;
            this.btnMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonth.Image = null;
            this.btnMonth.Location = new System.Drawing.Point(134, 157);
            this.btnMonth.Margin = new System.Windows.Forms.Padding(0);
            this.btnMonth.Name = "btnMonth";
            this.btnMonth.Size = new System.Drawing.Size(34, 16);
            this.btnMonth.TabIndex = 13;
            this.btnMonth.Tag = "Month";
            this.btnMonth.Text = "Month";
            this.btnMonth.ToolTipText = "";
            this.btnMonth.Click += new System.EventHandler(this.btnSelection_Click);
            // 
            // btnMonth3
            // 
            this.btnMonth3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMonth3.BlackButton = true;
            this.btnMonth3.Checked = false;
            this.btnMonth3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonth3.Image = null;
            this.btnMonth3.Location = new System.Drawing.Point(96, 157);
            this.btnMonth3.Margin = new System.Windows.Forms.Padding(0);
            this.btnMonth3.Name = "btnMonth3";
            this.btnMonth3.Size = new System.Drawing.Size(34, 16);
            this.btnMonth3.TabIndex = 12;
            this.btnMonth3.Tag = "3 Months";
            this.btnMonth3.Text = "3 Mth";
            this.btnMonth3.ToolTipText = "";
            this.btnMonth3.Click += new System.EventHandler(this.btnSelection_Click);
            // 
            // btnTotal
            // 
            this.btnTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnTotal.BlackButton = true;
            this.btnTotal.Checked = false;
            this.btnTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTotal.Image = null;
            this.btnTotal.Location = new System.Drawing.Point(58, 157);
            this.btnTotal.Margin = new System.Windows.Forms.Padding(0);
            this.btnTotal.Name = "btnTotal";
            this.btnTotal.Size = new System.Drawing.Size(34, 16);
            this.btnTotal.TabIndex = 11;
            this.btnTotal.Tag = "Total";
            this.btnTotal.Text = "Total";
            this.btnTotal.ToolTipText = "";
            this.btnTotal.Click += new System.EventHandler(this.btnSelection_Click);
            // 
            // ucChartTier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.btnToday);
            this.Controls.Add(this.btnWeek);
            this.Controls.Add(this.btnMonth);
            this.Controls.Add(this.btnMonth3);
            this.Controls.Add(this.btnTotal);
            this.Controls.Add(this.lblChartType);
            this.Controls.Add(this.chart1);
            this.Name = "ucChartTier";
            this.Size = new System.Drawing.Size(300, 180);
            this.Load += new System.EventHandler(this.ucChart_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucChart_Paint);
            this.Resize += new System.EventHandler(this.ucChart_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
		private BadButton btnToday;
		private BadButton btnWeek;
		private BadButton btnMonth;
		private BadButton btnMonth3;
		private BadButton btnTotal;
		private System.Windows.Forms.Label lblChartType;
		private System.Windows.Forms.Timer timer1;
	}
}
