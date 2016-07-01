namespace WinApp.Gadget
{
	partial class ucGaugeKillDeath
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
            this.lblLeft = new System.Windows.Forms.Label();
            this.lblRight = new System.Windows.Forms.Label();
            this.lblCenter = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.lblBattleMode = new System.Windows.Forms.Label();
            this.btnToday = new BadButton();
            this.btnWeek = new BadButton();
            this.btnMonth = new BadButton();
            this.btn3M = new BadButton();
            this.btnTotal = new BadButton();
            this.aGauge1 = new AGaugeApp.AGauge();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblLeft
            // 
            this.lblLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
            this.lblLeft.Location = new System.Drawing.Point(10, 108);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(48, 17);
            this.lblLeft.TabIndex = 6;
            this.lblLeft.Text = "label1";
            // 
            // lblRight
            // 
            this.lblRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
            this.lblRight.Location = new System.Drawing.Point(143, 108);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(48, 17);
            this.lblRight.TabIndex = 7;
            this.lblRight.Text = "label2";
            this.lblRight.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCenter
            // 
            this.lblCenter.BackColor = System.Drawing.Color.Transparent;
            this.lblCenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCenter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
            this.lblCenter.Location = new System.Drawing.Point(81, 70);
            this.lblCenter.Name = "lblCenter";
            this.lblCenter.Size = new System.Drawing.Size(38, 17);
            this.lblCenter.TabIndex = 8;
            this.lblCenter.Text = "label3";
            this.lblCenter.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSub
            // 
            this.lblSub.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
            this.lblSub.Location = new System.Drawing.Point(63, 107);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(77, 15);
            this.lblSub.TabIndex = 9;
            this.lblSub.Text = "Kill / Death Ratio";
            this.lblSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBattleMode
            // 
            this.lblBattleMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBattleMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
            this.lblBattleMode.Location = new System.Drawing.Point(52, 93);
            this.lblBattleMode.Name = "lblBattleMode";
            this.lblBattleMode.Size = new System.Drawing.Size(97, 15);
            this.lblBattleMode.TabIndex = 10;
            this.lblBattleMode.Text = "Battle Mode";
            this.lblBattleMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnToday
            // 
            this.btnToday.BlackButton = true;
            this.btnToday.Checked = false;
            this.btnToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToday.Image = null;
            this.btnToday.Location = new System.Drawing.Point(159, 128);
            this.btnToday.Margin = new System.Windows.Forms.Padding(0);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(34, 16);
            this.btnToday.TabIndex = 5;
            this.btnToday.Text = "Today";
            this.btnToday.ToolTipText = "";
            this.btnToday.Click += new System.EventHandler(this.btnTime_Click);
            // 
            // btnWeek
            // 
            this.btnWeek.BlackButton = true;
            this.btnWeek.Checked = false;
            this.btnWeek.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeek.Image = null;
            this.btnWeek.Location = new System.Drawing.Point(121, 128);
            this.btnWeek.Margin = new System.Windows.Forms.Padding(0);
            this.btnWeek.Name = "btnWeek";
            this.btnWeek.Size = new System.Drawing.Size(34, 16);
            this.btnWeek.TabIndex = 4;
            this.btnWeek.Text = "Week";
            this.btnWeek.ToolTipText = "";
            this.btnWeek.Click += new System.EventHandler(this.btnTime_Click);
            // 
            // btnMonth
            // 
            this.btnMonth.BlackButton = true;
            this.btnMonth.Checked = false;
            this.btnMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonth.Image = null;
            this.btnMonth.Location = new System.Drawing.Point(83, 128);
            this.btnMonth.Margin = new System.Windows.Forms.Padding(0);
            this.btnMonth.Name = "btnMonth";
            this.btnMonth.Size = new System.Drawing.Size(34, 16);
            this.btnMonth.TabIndex = 3;
            this.btnMonth.Text = "Month";
            this.btnMonth.ToolTipText = "";
            this.btnMonth.Click += new System.EventHandler(this.btnTime_Click);
            // 
            // btn3M
            // 
            this.btn3M.BlackButton = true;
            this.btn3M.Checked = false;
            this.btn3M.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn3M.Image = null;
            this.btn3M.Location = new System.Drawing.Point(45, 128);
            this.btn3M.Margin = new System.Windows.Forms.Padding(0);
            this.btn3M.Name = "btn3M";
            this.btn3M.Size = new System.Drawing.Size(34, 16);
            this.btn3M.TabIndex = 2;
            this.btn3M.Text = "3 Mth";
            this.btn3M.ToolTipText = "";
            this.btn3M.Click += new System.EventHandler(this.btnTime_Click);
            // 
            // btnTotal
            // 
            this.btnTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnTotal.BlackButton = true;
            this.btnTotal.Checked = false;
            this.btnTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTotal.Image = null;
            this.btnTotal.Location = new System.Drawing.Point(7, 128);
            this.btnTotal.Margin = new System.Windows.Forms.Padding(0);
            this.btnTotal.Name = "btnTotal";
            this.btnTotal.Size = new System.Drawing.Size(34, 16);
            this.btnTotal.TabIndex = 1;
            this.btnTotal.Text = "Total";
            this.btnTotal.ToolTipText = "";
            this.btnTotal.Click += new System.EventHandler(this.btnTime_Click);
            // 
            // aGauge1
            // 
            this.aGauge1.BaseArcColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
            this.aGauge1.BaseArcRadius = 70;
            this.aGauge1.BaseArcStart = 180;
            this.aGauge1.BaseArcSweep = 180;
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
            this.aGauge1.Center = new System.Drawing.Point(100, 90);
            this.aGauge1.CenterSubText = "";
            this.aGauge1.CenterText = "";
            this.aGauge1.CenterTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
            this.aGauge1.CenterTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.aGauge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.aGauge1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.aGauge1.Location = new System.Drawing.Point(0, 0);
            this.aGauge1.Name = "aGauge1";
            this.aGauge1.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.WotNumbers;
            this.aGauge1.NeedleColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(206)))));
            this.aGauge1.NeedleRadius = 58;
            this.aGauge1.NeedleType = 2;
            this.aGauge1.NeedleWidth = 2;
            this.aGauge1.Range_Idx = ((byte)(0));
            this.aGauge1.RangeEnabled = false;
            this.aGauge1.RangeEndValue = 0F;
            this.aGauge1.RangeInnerRadius = 70;
            this.aGauge1.RangeOuterRadius = 72;
            this.aGauge1.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false,
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
        0F,
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
        70,
        70,
        70,
        70,
        70,
        70};
            this.aGauge1.RangesOuterRadius = new int[] {
        72,
        72,
        72,
        72,
        72,
        72,
        72,
        72,
        72,
        72};
            this.aGauge1.RangesStartValue = new float[] {
        0F,
        0F,
        0F,
        0F,
        0F,
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
            this.aGauge1.Size = new System.Drawing.Size(200, 102);
            this.aGauge1.TabIndex = 0;
            this.aGauge1.Text = "aGauge1";
            this.aGauge1.Value = 0F;
            this.aGauge1.ValueMax = 2F;
            this.aGauge1.ValueMin = 0F;
            this.aGauge1.ValueScaleLinesMajorStepValue = 0.5F;
            this.aGauge1.ValueScaleLinesMinorNumOf = 9;
            // 
            // ucGaugeKillDeath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.lblBattleMode);
            this.Controls.Add(this.lblSub);
            this.Controls.Add(this.lblCenter);
            this.Controls.Add(this.lblRight);
            this.Controls.Add(this.lblLeft);
            this.Controls.Add(this.btnToday);
            this.Controls.Add(this.btnWeek);
            this.Controls.Add(this.btnMonth);
            this.Controls.Add(this.btn3M);
            this.Controls.Add(this.btnTotal);
            this.Controls.Add(this.aGauge1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucGaugeKillDeath";
            this.Size = new System.Drawing.Size(200, 150);
            this.Load += new System.EventHandler(this.ucGauge_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucGauge_Paint);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer timer1;
		private AGaugeApp.AGauge aGauge1;
		private BadButton btnTotal;
		private BadButton btn3M;
		private BadButton btnMonth;
		private BadButton btnWeek;
		private BadButton btnToday;
		private System.Windows.Forms.Label lblLeft;
		private System.Windows.Forms.Label lblRight;
		private System.Windows.Forms.Label lblCenter;
		private System.Windows.Forms.Label lblSub;
		private System.Windows.Forms.Label lblBattleMode;

















	}
}
