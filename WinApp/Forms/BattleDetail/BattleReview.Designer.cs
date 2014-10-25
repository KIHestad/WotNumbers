﻿namespace WinApp.Forms
{
	partial class BattleReview
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleReview));
			this.picMap = new System.Windows.Forms.PictureBox();
			this.picIllustration = new System.Windows.Forms.PictureBox();
			this.dgvReviews = new System.Windows.Forms.DataGridView();
			this.panelGrid = new System.Windows.Forms.Panel();
			this.panelComment = new System.Windows.Forms.Panel();
			this.scrollComment = new BadScrollBar();
			this.txtComment = new BadTextBox();
			this.btnCommentClear = new BadButton();
			this.btnCommentCancel = new BadButton();
			this.btnCommentSave = new BadButton();
			this.scrollDgv = new BadScrollBar();
			this.chkTank = new BadCheckBox();
			this.chkBattleMode = new BadCheckBox();
			this.chkMap = new BadCheckBox();
			this.chkClan = new BadCheckBox();
			this.grpOtherBtlReviews = new BadGroupBox();
			this.lblMapDescription = new BadLabel();
			this.badGroupBox1 = new BadGroupBox();
			((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picIllustration)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).BeginInit();
			this.panelGrid.SuspendLayout();
			this.panelComment.SuspendLayout();
			this.SuspendLayout();
			// 
			// picMap
			// 
			this.picMap.Location = new System.Drawing.Point(15, 15);
			this.picMap.Name = "picMap";
			this.picMap.Size = new System.Drawing.Size(300, 300);
			this.picMap.TabIndex = 1;
			this.picMap.TabStop = false;
			// 
			// picIllustration
			// 
			this.picIllustration.Location = new System.Drawing.Point(285, 0);
			this.picIllustration.Name = "picIllustration";
			this.picIllustration.Size = new System.Drawing.Size(153, 65);
			this.picIllustration.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picIllustration.TabIndex = 7;
			this.picIllustration.TabStop = false;
			// 
			// dgvReviews
			// 
			this.dgvReviews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvReviews.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvReviews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvReviews.Location = new System.Drawing.Point(19, 58);
			this.dgvReviews.Name = "dgvReviews";
			this.dgvReviews.Size = new System.Drawing.Size(388, 289);
			this.dgvReviews.TabIndex = 16;
			// 
			// panelGrid
			// 
			this.panelGrid.Controls.Add(this.dgvReviews);
			this.panelGrid.Controls.Add(this.scrollDgv);
			this.panelGrid.Controls.Add(this.chkTank);
			this.panelGrid.Controls.Add(this.chkBattleMode);
			this.panelGrid.Controls.Add(this.chkMap);
			this.panelGrid.Controls.Add(this.chkClan);
			this.panelGrid.Controls.Add(this.grpOtherBtlReviews);
			this.panelGrid.Location = new System.Drawing.Point(334, 92);
			this.panelGrid.Name = "panelGrid";
			this.panelGrid.Size = new System.Drawing.Size(438, 364);
			this.panelGrid.TabIndex = 18;
			// 
			// panelComment
			// 
			this.panelComment.Controls.Add(this.scrollComment);
			this.panelComment.Controls.Add(this.txtComment);
			this.panelComment.Controls.Add(this.btnCommentClear);
			this.panelComment.Controls.Add(this.picIllustration);
			this.panelComment.Controls.Add(this.btnCommentCancel);
			this.panelComment.Controls.Add(this.btnCommentSave);
			this.panelComment.Location = new System.Drawing.Point(334, 15);
			this.panelComment.Name = "panelComment";
			this.panelComment.Size = new System.Drawing.Size(438, 65);
			this.panelComment.TabIndex = 19;
			// 
			// scrollComment
			// 
			this.scrollComment.Image = null;
			this.scrollComment.Location = new System.Drawing.Point(209, 0);
			this.scrollComment.Name = "scrollComment";
			this.scrollComment.ScrollElementsTotals = 100;
			this.scrollComment.ScrollElementsVisible = 20;
			this.scrollComment.ScrollHide = true;
			this.scrollComment.ScrollNecessary = true;
			this.scrollComment.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.scrollComment.ScrollPosition = 0;
			this.scrollComment.Size = new System.Drawing.Size(17, 65);
			this.scrollComment.TabIndex = 6;
			this.scrollComment.Text = "badScrollBar1";
			// 
			// txtComment
			// 
			this.txtComment.HasFocus = false;
			this.txtComment.Image = null;
			this.txtComment.Location = new System.Drawing.Point(0, 0);
			this.txtComment.Margin = new System.Windows.Forms.Padding(0);
			this.txtComment.MultilineAllow = true;
			this.txtComment.Name = "txtComment";
			this.txtComment.PasswordChar = '\0';
			this.txtComment.ReadOnly = false;
			this.txtComment.Size = new System.Drawing.Size(210, 65);
			this.txtComment.TabIndex = 5;
			this.txtComment.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtComment.ToolTipText = "";
			// 
			// btnCommentClear
			// 
			this.btnCommentClear.BlackButton = false;
			this.btnCommentClear.Checked = false;
			this.btnCommentClear.Image = null;
			this.btnCommentClear.Location = new System.Drawing.Point(230, 0);
			this.btnCommentClear.Name = "btnCommentClear";
			this.btnCommentClear.Size = new System.Drawing.Size(49, 19);
			this.btnCommentClear.TabIndex = 10;
			this.btnCommentClear.Text = "Clear";
			this.btnCommentClear.ToolTipText = "";
			this.btnCommentClear.Click += new System.EventHandler(this.btnCommentClear_Click);
			// 
			// btnCommentCancel
			// 
			this.btnCommentCancel.BlackButton = false;
			this.btnCommentCancel.Checked = false;
			this.btnCommentCancel.Image = null;
			this.btnCommentCancel.Location = new System.Drawing.Point(230, 23);
			this.btnCommentCancel.Name = "btnCommentCancel";
			this.btnCommentCancel.Size = new System.Drawing.Size(49, 19);
			this.btnCommentCancel.TabIndex = 9;
			this.btnCommentCancel.Text = "Cancel";
			this.btnCommentCancel.ToolTipText = "";
			this.btnCommentCancel.Click += new System.EventHandler(this.btnCommentCancel_Click);
			// 
			// btnCommentSave
			// 
			this.btnCommentSave.BlackButton = false;
			this.btnCommentSave.Checked = false;
			this.btnCommentSave.Image = null;
			this.btnCommentSave.Location = new System.Drawing.Point(230, 46);
			this.btnCommentSave.Name = "btnCommentSave";
			this.btnCommentSave.Size = new System.Drawing.Size(49, 19);
			this.btnCommentSave.TabIndex = 8;
			this.btnCommentSave.Text = "Save";
			this.btnCommentSave.ToolTipText = "";
			this.btnCommentSave.Click += new System.EventHandler(this.btnCommentSave_Click);
			// 
			// scrollDgv
			// 
			this.scrollDgv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.scrollDgv.Image = null;
			this.scrollDgv.Location = new System.Drawing.Point(407, 58);
			this.scrollDgv.Name = "scrollDgv";
			this.scrollDgv.ScrollElementsTotals = 100;
			this.scrollDgv.ScrollElementsVisible = 20;
			this.scrollDgv.ScrollHide = true;
			this.scrollDgv.ScrollNecessary = true;
			this.scrollDgv.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
			this.scrollDgv.ScrollPosition = 0;
			this.scrollDgv.Size = new System.Drawing.Size(17, 289);
			this.scrollDgv.TabIndex = 17;
			this.scrollDgv.Text = "badScrollBar1";
			// 
			// chkTank
			// 
			this.chkTank.BackColor = System.Drawing.Color.Transparent;
			this.chkTank.Checked = true;
			this.chkTank.Image = ((System.Drawing.Image)(resources.GetObject("chkTank.Image")));
			this.chkTank.Location = new System.Drawing.Point(16, 28);
			this.chkTank.Name = "chkTank";
			this.chkTank.Size = new System.Drawing.Size(82, 23);
			this.chkTank.TabIndex = 12;
			this.chkTank.Text = "This Tank";
			this.chkTank.Click += new System.EventHandler(this.dataGridViewFilterChanged);
			// 
			// chkBattleMode
			// 
			this.chkBattleMode.BackColor = System.Drawing.Color.Transparent;
			this.chkBattleMode.Checked = true;
			this.chkBattleMode.Image = ((System.Drawing.Image)(resources.GetObject("chkBattleMode.Image")));
			this.chkBattleMode.Location = new System.Drawing.Point(111, 29);
			this.chkBattleMode.Name = "chkBattleMode";
			this.chkBattleMode.Size = new System.Drawing.Size(115, 23);
			this.chkBattleMode.TabIndex = 15;
			this.chkBattleMode.Text = "This Battle Mode";
			this.chkBattleMode.Click += new System.EventHandler(this.dataGridViewFilterChanged);
			// 
			// chkMap
			// 
			this.chkMap.BackColor = System.Drawing.Color.Transparent;
			this.chkMap.Checked = true;
			this.chkMap.Image = ((System.Drawing.Image)(resources.GetObject("chkMap.Image")));
			this.chkMap.Location = new System.Drawing.Point(242, 28);
			this.chkMap.Name = "chkMap";
			this.chkMap.Size = new System.Drawing.Size(80, 23);
			this.chkMap.TabIndex = 13;
			this.chkMap.Text = "This Map";
			this.chkMap.Click += new System.EventHandler(this.dataGridViewFilterChanged);
			// 
			// chkClan
			// 
			this.chkClan.BackColor = System.Drawing.Color.Transparent;
			this.chkClan.Checked = true;
			this.chkClan.Image = ((System.Drawing.Image)(resources.GetObject("chkClan.Image")));
			this.chkClan.Location = new System.Drawing.Point(335, 28);
			this.chkClan.Name = "chkClan";
			this.chkClan.Size = new System.Drawing.Size(79, 23);
			this.chkClan.TabIndex = 14;
			this.chkClan.Text = "This Clan";
			this.chkClan.Click += new System.EventHandler(this.dataGridViewFilterChanged);
			// 
			// grpOtherBtlReviews
			// 
			this.grpOtherBtlReviews.BackColor = System.Drawing.Color.Transparent;
			this.grpOtherBtlReviews.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpOtherBtlReviews.Image = null;
			this.grpOtherBtlReviews.Location = new System.Drawing.Point(0, 0);
			this.grpOtherBtlReviews.Name = "grpOtherBtlReviews";
			this.grpOtherBtlReviews.Size = new System.Drawing.Size(438, 364);
			this.grpOtherBtlReviews.TabIndex = 11;
			this.grpOtherBtlReviews.Text = "Other Battle Reviews";
			// 
			// lblMapDescription
			// 
			this.lblMapDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblMapDescription.Dimmed = false;
			this.lblMapDescription.Image = null;
			this.lblMapDescription.Location = new System.Drawing.Point(30, 349);
			this.lblMapDescription.Name = "lblMapDescription";
			this.lblMapDescription.Size = new System.Drawing.Size(273, 96);
			this.lblMapDescription.TabIndex = 2;
			this.lblMapDescription.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// badGroupBox1
			// 
			this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.badGroupBox1.Image = null;
			this.badGroupBox1.Location = new System.Drawing.Point(15, 325);
			this.badGroupBox1.Name = "badGroupBox1";
			this.badGroupBox1.Size = new System.Drawing.Size(300, 131);
			this.badGroupBox1.TabIndex = 3;
			this.badGroupBox1.Text = "Map Description";
			// 
			// BattleReview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.Controls.Add(this.panelComment);
			this.Controls.Add(this.panelGrid);
			this.Controls.Add(this.lblMapDescription);
			this.Controls.Add(this.badGroupBox1);
			this.Controls.Add(this.picMap);
			this.Name = "BattleReview";
			this.Size = new System.Drawing.Size(787, 469);
			this.Load += new System.EventHandler(this.BattleMapAndComment_Load);
			this.Resize += new System.EventHandler(this.BattleReview_Resize);
			((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picIllustration)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).EndInit();
			this.panelGrid.ResumeLayout(false);
			this.panelComment.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox picMap;
		private BadLabel lblMapDescription;
		private BadGroupBox badGroupBox1;
		private BadTextBox txtComment;
		private BadScrollBar scrollComment;
		private System.Windows.Forms.PictureBox picIllustration;
		private BadButton btnCommentSave;
		private BadButton btnCommentCancel;
		private BadButton btnCommentClear;
		private BadGroupBox grpOtherBtlReviews;
		private BadCheckBox chkTank;
		private BadCheckBox chkMap;
		private BadCheckBox chkClan;
		private BadCheckBox chkBattleMode;
		private System.Windows.Forms.DataGridView dgvReviews;
		private BadScrollBar scrollDgv;
		private System.Windows.Forms.Panel panelGrid;
		private System.Windows.Forms.Panel panelComment;
	}
}
