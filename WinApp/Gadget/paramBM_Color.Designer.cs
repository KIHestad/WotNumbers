﻿namespace WinApp.Gadget
{
	partial class paramBM_Color
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
            this.badForm1 = new BadForm();
            this.ddTimeSpan = new BadDropDownBox();
            this.badLabel3 = new BadLabel();
            this.btnColorPicker = new BadButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.badLabel2 = new BadLabel();
            this.btnCancel = new BadButton();
            this.btnSelect = new BadButton();
            this.ddBattleMode = new BadDropDownBox();
            this.badLabel1 = new BadLabel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.badForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // badForm1
            // 
            this.badForm1.Controls.Add(this.ddTimeSpan);
            this.badForm1.Controls.Add(this.badLabel3);
            this.badForm1.Controls.Add(this.btnColorPicker);
            this.badForm1.Controls.Add(this.panel1);
            this.badForm1.Controls.Add(this.badLabel2);
            this.badForm1.Controls.Add(this.btnCancel);
            this.badForm1.Controls.Add(this.btnSelect);
            this.badForm1.Controls.Add(this.ddBattleMode);
            this.badForm1.Controls.Add(this.badLabel1);
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
            this.badForm1.Resizable = false;
            this.badForm1.Size = new System.Drawing.Size(260, 191);
            this.badForm1.SystemExitImage = null;
            this.badForm1.SystemMaximizeImage = null;
            this.badForm1.SystemMinimizeImage = null;
            this.badForm1.TabIndex = 0;
            this.badForm1.Text = "Parameters";
            this.badForm1.TitleHeight = 26;
            // 
            // ddTimeSpan
            // 
            this.ddTimeSpan.Image = null;
            this.ddTimeSpan.Location = new System.Drawing.Point(103, 77);
            this.ddTimeSpan.Name = "ddTimeSpan";
            this.ddTimeSpan.Size = new System.Drawing.Size(134, 23);
            this.ddTimeSpan.TabIndex = 8;
            this.ddTimeSpan.Click += new System.EventHandler(this.ddTimeSpan_Click);
            // 
            // badLabel3
            // 
            this.badLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel3.Dimmed = false;
            this.badLabel3.Image = null;
            this.badLabel3.Location = new System.Drawing.Point(25, 77);
            this.badLabel3.Name = "badLabel3";
            this.badLabel3.Size = new System.Drawing.Size(76, 23);
            this.badLabel3.TabIndex = 7;
            this.badLabel3.Text = "Timespan:";
            this.badLabel3.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // btnColorPicker
            // 
            this.btnColorPicker.BlackButton = false;
            this.btnColorPicker.Checked = false;
            this.btnColorPicker.Image = null;
            this.btnColorPicker.Location = new System.Drawing.Point(212, 106);
            this.btnColorPicker.Name = "btnColorPicker";
            this.btnColorPicker.Size = new System.Drawing.Size(25, 23);
            this.btnColorPicker.TabIndex = 6;
            this.btnColorPicker.Text = "...";
            this.btnColorPicker.ToolTipText = "";
            this.btnColorPicker.Click += new System.EventHandler(this.btnColorPicker_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Location = new System.Drawing.Point(103, 106);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(103, 23);
            this.panel1.TabIndex = 5;
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(25, 106);
            this.badLabel2.Name = "badLabel2";
            this.badLabel2.Size = new System.Drawing.Size(75, 23);
            this.badLabel2.TabIndex = 4;
            this.badLabel2.Text = "Bar Color:";
            this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.BlackButton = false;
            this.btnCancel.Checked = false;
            this.btnCancel.Image = null;
            this.btnCancel.Location = new System.Drawing.Point(173, 148);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTipText = "";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.BlackButton = false;
            this.btnSelect.Checked = false;
            this.btnSelect.Image = null;
            this.btnSelect.Location = new System.Drawing.Point(103, 148);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(64, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Select";
            this.btnSelect.ToolTipText = "";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // ddBattleMode
            // 
            this.ddBattleMode.Image = null;
            this.ddBattleMode.Location = new System.Drawing.Point(103, 48);
            this.ddBattleMode.Name = "ddBattleMode";
            this.ddBattleMode.Size = new System.Drawing.Size(134, 23);
            this.ddBattleMode.TabIndex = 1;
            this.ddBattleMode.Click += new System.EventHandler(this.ddBattleMode_Click);
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(25, 48);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(76, 23);
            this.badLabel1.TabIndex = 0;
            this.badLabel1.Text = "Battle Mode:";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // paramBM_Color
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 191);
            this.Controls.Add(this.badForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "paramBM_Color";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ucGaugeWinRateParameter";
            this.Load += new System.EventHandler(this.param_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.paramBattleMode_Paint);
            this.badForm1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private BadForm badForm1;
		private BadButton btnSelect;
		private BadDropDownBox ddBattleMode;
		private BadLabel badLabel1;
		private BadButton btnCancel;
		private BadButton btnColorPicker;
		private System.Windows.Forms.Panel panel1;
		private BadLabel badLabel2;
		private System.Windows.Forms.ColorDialog colorDialog1;
        private BadDropDownBox ddTimeSpan;
        private BadLabel badLabel3;
	}
}