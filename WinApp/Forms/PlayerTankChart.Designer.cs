namespace WinApp.Forms
{
    partial class PlayerTankChart
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerTankChart));
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			this.PlayerTankChartTheme = new BadForm();
			this.ddTank = new BadDropDownBox();
			this.badLabel1 = new BadLabel();
			this.ChartingBattleCount = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.PlayerTankChartTheme.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ChartingBattleCount)).BeginInit();
			this.SuspendLayout();
			// 
			// PlayerTankChartTheme
			// 
			this.PlayerTankChartTheme.BackColor = System.Drawing.Color.Fuchsia;
			this.PlayerTankChartTheme.Controls.Add(this.ddTank);
			this.PlayerTankChartTheme.Controls.Add(this.badLabel1);
			this.PlayerTankChartTheme.Controls.Add(this.ChartingBattleCount);
			this.PlayerTankChartTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PlayerTankChartTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.PlayerTankChartTheme.FormFooter = false;
			this.PlayerTankChartTheme.FormFooterHeight = 26;
			this.PlayerTankChartTheme.FormInnerBorder = 3;
			this.PlayerTankChartTheme.FormMargin = 0;
			this.PlayerTankChartTheme.Image = null;
			this.PlayerTankChartTheme.Location = new System.Drawing.Point(0, 0);
			this.PlayerTankChartTheme.MainArea = mainAreaClass1;
			this.PlayerTankChartTheme.Name = "PlayerTankChartTheme";
			this.PlayerTankChartTheme.Resizable = true;
			this.PlayerTankChartTheme.Size = new System.Drawing.Size(755, 428);
			this.PlayerTankChartTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("PlayerTankChartTheme.SystemExitImage")));
			this.PlayerTankChartTheme.SystemMaximizeImage = null;
			this.PlayerTankChartTheme.SystemMinimizeImage = null;
			this.PlayerTankChartTheme.TabIndex = 0;
			this.PlayerTankChartTheme.Text = "Tank Chart";
			this.PlayerTankChartTheme.TitleHeight = 26;
			// 
			// ddTank
			// 
			this.ddTank.Image = null;
			this.ddTank.Location = new System.Drawing.Point(98, 40);
			this.ddTank.Name = "ddTank";
			this.ddTank.Size = new System.Drawing.Size(160, 22);
			this.ddTank.TabIndex = 3;
			this.ddTank.TextChanged += new System.EventHandler(this.ddTank_TextChanged);
			this.ddTank.Click += new System.EventHandler(this.ddTank_Click);
			// 
			// badLabel1
			// 
			this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.badLabel1.Dimmed = false;
			this.badLabel1.Image = null;
			this.badLabel1.Location = new System.Drawing.Point(17, 39);
			this.badLabel1.Name = "badLabel1";
			this.badLabel1.Size = new System.Drawing.Size(75, 23);
			this.badLabel1.TabIndex = 7;
			this.badLabel1.Text = "Select Tank:";
			// 
			// ChartingBattleCount
			// 
			this.ChartingBattleCount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ChartingBattleCount.BackColor = System.Drawing.Color.Transparent;
			chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
			chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Silver;
			chartArea1.AxisX.LineColor = System.Drawing.Color.Silver;
			chartArea1.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.DimGray;
			chartArea1.AxisX.TitleForeColor = System.Drawing.Color.Silver;
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
			this.ChartingBattleCount.ChartAreas.Add(chartArea1);
			legend1.BackColor = System.Drawing.Color.Transparent;
			legend1.ForeColor = System.Drawing.Color.Silver;
			legend1.Name = "Legend1";
			this.ChartingBattleCount.Legends.Add(legend1);
			this.ChartingBattleCount.Location = new System.Drawing.Point(12, 72);
			this.ChartingBattleCount.Name = "ChartingBattleCount";
			this.ChartingBattleCount.Size = new System.Drawing.Size(730, 341);
			this.ChartingBattleCount.TabIndex = 0;
			this.ChartingBattleCount.Text = "chart1";
			// 
			// PlayerTankChart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Fuchsia;
			this.ClientSize = new System.Drawing.Size(755, 428);
			this.Controls.Add(this.PlayerTankChartTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "PlayerTankChart";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "TestShowImage";
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.Load += new System.EventHandler(this.TestShowImage_Load);
			this.PlayerTankChartTheme.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ChartingBattleCount)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private BadForm PlayerTankChartTheme;
		private System.Windows.Forms.DataVisualization.Charting.Chart ChartingBattleCount;
		private BadDropDownBox ddTank;
		private BadLabel badLabel1;



    }
}