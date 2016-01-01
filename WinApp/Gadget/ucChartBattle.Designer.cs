namespace WinApp.Gadget
{
	partial class ucChartBattle
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
            this.btnToday = new BadButton();
            this.btnWeek = new BadButton();
            this.btnMonth = new BadButton();
            this.btn1000 = new BadButton();
            this.btnTotal = new BadButton();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            this.chart1.BorderlineColor = System.Drawing.Color.Maroon;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisY.LabelStyle.Enabled = false;
            chartArea1.AxisY.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.BorderColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(22, 23);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(153, 104);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // btnToday
            // 
            this.btnToday.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnToday.BlackButton = true;
            this.btnToday.Checked = false;
            this.btnToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToday.Image = null;
            this.btnToday.Location = new System.Drawing.Point(160, 147);
            this.btnToday.Margin = new System.Windows.Forms.Padding(0);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(34, 16);
            this.btnToday.TabIndex = 15;
            this.btnToday.Text = "Today";
            this.btnToday.ToolTipText = "";
            // 
            // btnWeek
            // 
            this.btnWeek.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnWeek.BlackButton = true;
            this.btnWeek.Checked = false;
            this.btnWeek.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeek.Image = null;
            this.btnWeek.Location = new System.Drawing.Point(122, 147);
            this.btnWeek.Margin = new System.Windows.Forms.Padding(0);
            this.btnWeek.Name = "btnWeek";
            this.btnWeek.Size = new System.Drawing.Size(34, 16);
            this.btnWeek.TabIndex = 14;
            this.btnWeek.Text = "Week";
            this.btnWeek.ToolTipText = "";
            // 
            // btnMonth
            // 
            this.btnMonth.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMonth.BlackButton = true;
            this.btnMonth.Checked = false;
            this.btnMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonth.Image = null;
            this.btnMonth.Location = new System.Drawing.Point(84, 147);
            this.btnMonth.Margin = new System.Windows.Forms.Padding(0);
            this.btnMonth.Name = "btnMonth";
            this.btnMonth.Size = new System.Drawing.Size(34, 16);
            this.btnMonth.TabIndex = 13;
            this.btnMonth.Text = "Month";
            this.btnMonth.ToolTipText = "";
            // 
            // btn1000
            // 
            this.btn1000.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn1000.BlackButton = true;
            this.btn1000.Checked = false;
            this.btn1000.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn1000.Image = null;
            this.btn1000.Location = new System.Drawing.Point(46, 147);
            this.btn1000.Margin = new System.Windows.Forms.Padding(0);
            this.btn1000.Name = "btn1000";
            this.btn1000.Size = new System.Drawing.Size(34, 16);
            this.btn1000.TabIndex = 12;
            this.btn1000.Text = "1000";
            this.btn1000.ToolTipText = "";
            // 
            // btnTotal
            // 
            this.btnTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnTotal.BlackButton = true;
            this.btnTotal.Checked = true;
            this.btnTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTotal.Image = null;
            this.btnTotal.Location = new System.Drawing.Point(8, 147);
            this.btnTotal.Margin = new System.Windows.Forms.Padding(0);
            this.btnTotal.Name = "btnTotal";
            this.btnTotal.Size = new System.Drawing.Size(34, 16);
            this.btnTotal.TabIndex = 11;
            this.btnTotal.Text = "Total";
            this.btnTotal.ToolTipText = "";
            // 
            // ucChartBattle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.btnToday);
            this.Controls.Add(this.btnWeek);
            this.Controls.Add(this.btnMonth);
            this.Controls.Add(this.btn1000);
            this.Controls.Add(this.btnTotal);
            this.Controls.Add(this.chart1);
            this.MinimumSize = new System.Drawing.Size(100, 40);
            this.Name = "ucChartBattle";
            this.Size = new System.Drawing.Size(200, 170);
            this.Load += new System.EventHandler(this.ucChartBattle_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucChartBattle_Paint);
            this.Resize += new System.EventHandler(this.ucChartBattle_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
		private BadButton btnToday;
		private BadButton btnWeek;
		private BadButton btnMonth;
		private BadButton btn1000;
		private BadButton btnTotal;
	}
}
