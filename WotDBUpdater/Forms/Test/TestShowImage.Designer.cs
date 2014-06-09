namespace WotDBUpdater.Forms
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.TestShowImageTheme = new BadForm();
            this.chartBtn2 = new BadButton();
            this.chartBtn1 = new BadButton();
            this.testChart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.TestShowImageTheme.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.testChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // TestShowImageTheme
            // 
            this.TestShowImageTheme.Controls.Add(this.chartBtn2);
            this.TestShowImageTheme.Controls.Add(this.chartBtn1);
            this.TestShowImageTheme.Controls.Add(this.testChart1);
            this.TestShowImageTheme.Cursor = System.Windows.Forms.Cursors.Default;
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
            this.TestShowImageTheme.Size = new System.Drawing.Size(944, 569);
            this.TestShowImageTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("TestShowImageTheme.SystemExitImage")));
            this.TestShowImageTheme.SystemMaximizeImage = null;
            this.TestShowImageTheme.SystemMinimizeImage = null;
            this.TestShowImageTheme.TabIndex = 0;
            this.TestShowImageTheme.Text = "Tank Image Test";
            this.TestShowImageTheme.TitleHeight = 26;
            // 
            // chartBtn2
            // 
            this.chartBtn2.Image = null;
            this.chartBtn2.Location = new System.Drawing.Point(221, 523);
            this.chartBtn2.Name = "chartBtn2";
            this.chartBtn2.Size = new System.Drawing.Size(75, 23);
            this.chartBtn2.TabIndex = 2;
            this.chartBtn2.Text = "(ikke i bruk)";
            this.chartBtn2.Click += new System.EventHandler(this.chartBtn1_Click);
            // 
            // chartBtn1
            // 
            this.chartBtn1.Image = null;
            this.chartBtn1.Location = new System.Drawing.Point(221, 485);
            this.chartBtn1.Name = "chartBtn1";
            this.chartBtn1.Size = new System.Drawing.Size(75, 23);
            this.chartBtn1.TabIndex = 1;
            this.chartBtn1.Text = "Draw!";
            this.chartBtn1.Click += new System.EventHandler(this.chartBtn_Click);
            // 
            // testChart1
            // 
            this.testChart1.BackColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            chartArea1.BackColor = System.Drawing.Color.LightGray;
            chartArea1.Name = "ChartArea1";
            this.testChart1.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.LightGray;
            legend1.Name = "Legend1";
            this.testChart1.Legends.Add(legend1);
            this.testChart1.Location = new System.Drawing.Point(221, 40);
            this.testChart1.Name = "testChart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Color = System.Drawing.Color.DarkOrange;
            series1.Legend = "Legend1";
            series1.Name = "Battles";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Color = System.Drawing.Color.Red;
            series2.Legend = "Legend1";
            series2.Name = "Spot";
            this.testChart1.Series.Add(series1);
            this.testChart1.Series.Add(series2);
            this.testChart1.Size = new System.Drawing.Size(689, 423);
            this.testChart1.TabIndex = 0;
            this.testChart1.Text = "chart1";
            // 
            // TestShowImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 569);
            this.Controls.Add(this.TestShowImageTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TestShowImage";
            this.Text = "TestShowImage";
            this.Load += new System.EventHandler(this.TestShowImage_Load);
            this.TestShowImageTheme.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.testChart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

		private BadForm TestShowImageTheme;
        private System.Windows.Forms.DataVisualization.Charting.Chart testChart1;
        private BadButton chartBtn1;
        private BadButton chartBtn2;



    }
}