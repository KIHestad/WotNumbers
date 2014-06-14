namespace WinApp.Forms
{
    partial class ChartBattleCount
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartBattleCount));
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			this.ChartBattleCountTheme = new BadForm();
			this.ddTank = new BadDropDownBox();
			this.badLabel1 = new BadLabel();
			this.picIcon = new System.Windows.Forms.PictureBox();
			this.picSmall = new System.Windows.Forms.PictureBox();
			this.picLarge = new System.Windows.Forms.PictureBox();
			this.ChartingBattleCount = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.ChartBattleCountTheme.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picSmall)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picLarge)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ChartingBattleCount)).BeginInit();
			this.SuspendLayout();
			// 
			// ChartBattleCountTheme
			// 
			this.ChartBattleCountTheme.BackColor = System.Drawing.Color.Fuchsia;
			this.ChartBattleCountTheme.Controls.Add(this.ddTank);
			this.ChartBattleCountTheme.Controls.Add(this.badLabel1);
			this.ChartBattleCountTheme.Controls.Add(this.picIcon);
			this.ChartBattleCountTheme.Controls.Add(this.picSmall);
			this.ChartBattleCountTheme.Controls.Add(this.picLarge);
			this.ChartBattleCountTheme.Controls.Add(this.ChartingBattleCount);
			this.ChartBattleCountTheme.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ChartBattleCountTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.ChartBattleCountTheme.FormFooter = false;
			this.ChartBattleCountTheme.FormFooterHeight = 26;
			this.ChartBattleCountTheme.FormInnerBorder = 3;
			this.ChartBattleCountTheme.FormMargin = 0;
			this.ChartBattleCountTheme.Image = null;
			this.ChartBattleCountTheme.Location = new System.Drawing.Point(0, 0);
			this.ChartBattleCountTheme.MainArea = mainAreaClass1;
			this.ChartBattleCountTheme.Name = "ChartBattleCountTheme";
			this.ChartBattleCountTheme.Resizable = true;
			this.ChartBattleCountTheme.Size = new System.Drawing.Size(908, 529);
			this.ChartBattleCountTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("ChartBattleCountTheme.SystemExitImage")));
			this.ChartBattleCountTheme.SystemMaximizeImage = null;
			this.ChartBattleCountTheme.SystemMinimizeImage = null;
			this.ChartBattleCountTheme.TabIndex = 0;
			this.ChartBattleCountTheme.Text = "Battle Chart - Battle Count";
			this.ChartBattleCountTheme.TitleHeight = 26;
			// 
			// ddTank
			// 
			this.ddTank.Image = null;
			this.ddTank.Location = new System.Drawing.Point(17, 61);
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
			// picIcon
			// 
			this.picIcon.BackColor = System.Drawing.Color.Transparent;
			this.picIcon.Location = new System.Drawing.Point(65, 232);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size(65, 24);
			this.picIcon.TabIndex = 6;
			this.picIcon.TabStop = false;
			// 
			// picSmall
			// 
			this.picSmall.BackColor = System.Drawing.Color.Transparent;
			this.picSmall.Location = new System.Drawing.Point(52, 195);
			this.picSmall.Name = "picSmall";
			this.picSmall.Size = new System.Drawing.Size(124, 31);
			this.picSmall.TabIndex = 5;
			this.picSmall.TabStop = false;
			// 
			// picLarge
			// 
			this.picLarge.BackColor = System.Drawing.Color.Transparent;
			this.picLarge.Location = new System.Drawing.Point(17, 89);
			this.picLarge.Name = "picLarge";
			this.picLarge.Size = new System.Drawing.Size(160, 100);
			this.picLarge.TabIndex = 4;
			this.picLarge.TabStop = false;
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
			this.ChartingBattleCount.Location = new System.Drawing.Point(195, 39);
			this.ChartingBattleCount.Name = "ChartingBattleCount";
			this.ChartingBattleCount.Size = new System.Drawing.Size(696, 473);
			this.ChartingBattleCount.TabIndex = 0;
			this.ChartingBattleCount.Text = "chart1";
			// 
			// ChartBattleCount
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Fuchsia;
			this.ClientSize = new System.Drawing.Size(908, 529);
			this.Controls.Add(this.ChartBattleCountTheme);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ChartBattleCount";
			this.ShowInTaskbar = false;
			this.Text = "TestShowImage";
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.Load += new System.EventHandler(this.TestShowImage_Load);
			this.ChartBattleCountTheme.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picSmall)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picLarge)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ChartingBattleCount)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private BadForm ChartBattleCountTheme;
		private System.Windows.Forms.DataVisualization.Charting.Chart ChartingBattleCount;
		private BadDropDownBox ddTank;
		private System.Windows.Forms.PictureBox picLarge;
		private System.Windows.Forms.PictureBox picIcon;
		private System.Windows.Forms.PictureBox picSmall;
		private BadLabel badLabel1;



    }
}