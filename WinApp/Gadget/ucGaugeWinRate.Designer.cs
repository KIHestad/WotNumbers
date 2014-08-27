namespace WinApp.Gadget
{
	partial class ucGaugeWinRate
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
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.aGauge1 = new AGaugeApp.AGauge();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Interval = 10;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// aGauge1
			// 
			this.aGauge1.BaseArcColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.aGauge1.BaseArcRadius = 70;
			this.aGauge1.BaseArcStart = 150;
			this.aGauge1.BaseArcSweep = 240;
			this.aGauge1.BaseArcWidth = 1;
			this.aGauge1.Cap_Idx = ((byte)(1));
			this.aGauge1.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))))};
			this.aGauge1.CapPosition = new System.Drawing.Point(10, 10);
			this.aGauge1.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(128, 90),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
			this.aGauge1.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
			this.aGauge1.CapText = "";
			this.aGauge1.Center = new System.Drawing.Point(95, 90);
			this.aGauge1.CenterSubText = "";
			this.aGauge1.CenterText = "";
			this.aGauge1.CenterTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.aGauge1.CenterTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.aGauge1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
			this.aGauge1.Location = new System.Drawing.Point(10, 10);
			this.aGauge1.Name = "aGauge1";
			this.aGauge1.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.WotNumbers;
			this.aGauge1.NeedleColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.aGauge1.NeedleRadius = 58;
			this.aGauge1.NeedleType = 0;
			this.aGauge1.NeedleWidth = 2;
			this.aGauge1.Range_Idx = ((byte)(0));
			this.aGauge1.RangeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(96)))), ((int)(((byte)(127)))));
			this.aGauge1.RangeEnabled = false;
			this.aGauge1.RangeEndValue = 0F;
			this.aGauge1.RangeInnerRadius = 70;
			this.aGauge1.RangeOuterRadius = 80;
			this.aGauge1.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(96)))), ((int)(((byte)(127))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))),
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
			this.aGauge1.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
			this.aGauge1.RangesEndValue = new float[] {
        0F,
        0F,
        0F,
        0F,
        0F};
			this.aGauge1.RangesInnerRadius = new int[] {
        70,
        70,
        70,
        70,
        70};
			this.aGauge1.RangesOuterRadius = new int[] {
        80,
        80,
        80,
        80,
        80};
			this.aGauge1.RangesStartValue = new float[] {
        0F,
        0F,
        0F,
        0F,
        0F};
			this.aGauge1.RangeStartValue = 0F;
			this.aGauge1.ScaleLinesInterColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.aGauge1.ScaleLinesInterInnerRadius = 63;
			this.aGauge1.ScaleLinesInterOuterRadius = 70;
			this.aGauge1.ScaleLinesInterWidth = 1;
			this.aGauge1.ScaleLinesMajorColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.aGauge1.ScaleLinesMajorInnerRadius = 60;
			this.aGauge1.ScaleLinesMajorOuterRadius = 70;
			this.aGauge1.ScaleLinesMajorWidth = 2;
			this.aGauge1.ScaleLinesMinorColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(89)))));
			this.aGauge1.ScaleLinesMinorInnerRadius = 65;
			this.aGauge1.ScaleLinesMinorOuterRadius = 70;
			this.aGauge1.ScaleLinesMinorWidth = 1;
			this.aGauge1.ScaleNumbersColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
			this.aGauge1.ScaleNumbersFormat = null;
			this.aGauge1.ScaleNumbersRadius = 85;
			this.aGauge1.ScaleNumbersRotation = 0;
			this.aGauge1.ScaleNumbersStartScaleLine = 0;
			this.aGauge1.ScaleNumbersStepScaleLines = 1;
			this.aGauge1.Size = new System.Drawing.Size(190, 140);
			this.aGauge1.TabIndex = 0;
			this.aGauge1.Text = "aGauge1";
			this.aGauge1.Value = 18F;
			this.aGauge1.ValueMax = 80F;
			this.aGauge1.ValueMin = 20F;
			this.aGauge1.ValueScaleLinesMajorStepValue = 5F;
			this.aGauge1.ValueScaleLinesMinorNumOf = 9;
			// 
			// ucGaugeWinRate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.Controls.Add(this.aGauge1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "ucGaugeWinRate";
			this.Size = new System.Drawing.Size(200, 150);
			this.Load += new System.EventHandler(this.ucGaugeWinRate_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private AGaugeApp.AGauge aGauge1;
		private System.Windows.Forms.Timer timer1;

















	}
}
