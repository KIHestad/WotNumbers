namespace WinApp.Forms
{
    partial class TankInfoEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TankInfoEdit));
            this.badForm1 = new BadForm();
            this.btnReset = new BadButton();
            this.ddTankType = new BadDropDownBox();
            this.ddNation = new BadDropDownBox();
            this.txtTier = new BadTextBox();
            this.badLabel6 = new BadLabel();
            this.badLabel5 = new BadLabel();
            this.badLabel4 = new BadLabel();
            this.txtShortName = new BadTextBox();
            this.badLabel3 = new BadLabel();
            this.txtName = new BadTextBox();
            this.badLabel2 = new BadLabel();
            this.btnClose = new BadButton();
            this.txtID = new BadTextBox();
            this.badLabel1 = new BadLabel();
            this.btnSave = new BadButton();
            this.badGroupBox1 = new BadGroupBox();
            this.badForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // badForm1
            // 
            this.badForm1.Controls.Add(this.btnReset);
            this.badForm1.Controls.Add(this.ddTankType);
            this.badForm1.Controls.Add(this.ddNation);
            this.badForm1.Controls.Add(this.txtTier);
            this.badForm1.Controls.Add(this.badLabel6);
            this.badForm1.Controls.Add(this.badLabel5);
            this.badForm1.Controls.Add(this.badLabel4);
            this.badForm1.Controls.Add(this.txtShortName);
            this.badForm1.Controls.Add(this.badLabel3);
            this.badForm1.Controls.Add(this.txtName);
            this.badForm1.Controls.Add(this.badLabel2);
            this.badForm1.Controls.Add(this.btnClose);
            this.badForm1.Controls.Add(this.txtID);
            this.badForm1.Controls.Add(this.badLabel1);
            this.badForm1.Controls.Add(this.btnSave);
            this.badForm1.Controls.Add(this.badGroupBox1);
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
            this.badForm1.Resizable = true;
            this.badForm1.Size = new System.Drawing.Size(327, 310);
            this.badForm1.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("badForm1.SystemExitImage")));
            this.badForm1.SystemMaximizeImage = null;
            this.badForm1.SystemMinimizeImage = null;
            this.badForm1.TabIndex = 0;
            this.badForm1.Text = "Edit Tank Info";
            this.badForm1.TitleHeight = 26;
            // 
            // btnReset
            // 
            this.btnReset.BlackButton = false;
            this.btnReset.Checked = false;
            this.btnReset.Image = null;
            this.btnReset.Location = new System.Drawing.Point(17, 268);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(109, 23);
            this.btnReset.TabIndex = 15;
            this.btnReset.Text = "Get Default Values";
            this.btnReset.ToolTipText = "Lookup default values using Wargaming API";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // ddTankType
            // 
            this.ddTankType.Image = null;
            this.ddTankType.Location = new System.Drawing.Point(132, 183);
            this.ddTankType.Name = "ddTankType";
            this.ddTankType.Size = new System.Drawing.Size(156, 23);
            this.ddTankType.TabIndex = 10;
            this.ddTankType.Click += new System.EventHandler(this.ddTankType_Click);
            // 
            // ddNation
            // 
            this.ddNation.Image = null;
            this.ddNation.Location = new System.Drawing.Point(132, 154);
            this.ddNation.Name = "ddNation";
            this.ddNation.Size = new System.Drawing.Size(156, 23);
            this.ddNation.TabIndex = 8;
            this.ddNation.Click += new System.EventHandler(this.ddNation_Click);
            // 
            // txtTier
            // 
            this.txtTier.HasFocus = false;
            this.txtTier.Image = null;
            this.txtTier.Location = new System.Drawing.Point(132, 212);
            this.txtTier.MultilineAllow = false;
            this.txtTier.Name = "txtTier";
            this.txtTier.PasswordChar = '\0';
            this.txtTier.ReadOnly = false;
            this.txtTier.Size = new System.Drawing.Size(57, 23);
            this.txtTier.TabIndex = 12;
            this.txtTier.TabStop = false;
            this.txtTier.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTier.ToolTipText = "";
            // 
            // badLabel6
            // 
            this.badLabel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel6.Dimmed = false;
            this.badLabel6.Image = null;
            this.badLabel6.Location = new System.Drawing.Point(34, 212);
            this.badLabel6.Name = "badLabel6";
            this.badLabel6.Size = new System.Drawing.Size(69, 23);
            this.badLabel6.TabIndex = 11;
            this.badLabel6.TabStop = false;
            this.badLabel6.Text = "Tier";
            this.badLabel6.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badLabel5
            // 
            this.badLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel5.Dimmed = false;
            this.badLabel5.Image = null;
            this.badLabel5.Location = new System.Drawing.Point(34, 183);
            this.badLabel5.Name = "badLabel5";
            this.badLabel5.Size = new System.Drawing.Size(69, 23);
            this.badLabel5.TabIndex = 9;
            this.badLabel5.TabStop = false;
            this.badLabel5.Text = "Tank Type";
            this.badLabel5.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // badLabel4
            // 
            this.badLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel4.Dimmed = false;
            this.badLabel4.Image = null;
            this.badLabel4.Location = new System.Drawing.Point(34, 154);
            this.badLabel4.Name = "badLabel4";
            this.badLabel4.Size = new System.Drawing.Size(69, 23);
            this.badLabel4.TabIndex = 7;
            this.badLabel4.TabStop = false;
            this.badLabel4.Text = "Nation";
            this.badLabel4.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // txtShortName
            // 
            this.txtShortName.HasFocus = false;
            this.txtShortName.Image = null;
            this.txtShortName.Location = new System.Drawing.Point(132, 125);
            this.txtShortName.MultilineAllow = false;
            this.txtShortName.Name = "txtShortName";
            this.txtShortName.PasswordChar = '\0';
            this.txtShortName.ReadOnly = false;
            this.txtShortName.Size = new System.Drawing.Size(156, 23);
            this.txtShortName.TabIndex = 6;
            this.txtShortName.TabStop = false;
            this.txtShortName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtShortName.ToolTipText = "";
            // 
            // badLabel3
            // 
            this.badLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel3.Dimmed = false;
            this.badLabel3.Image = null;
            this.badLabel3.Location = new System.Drawing.Point(34, 125);
            this.badLabel3.Name = "badLabel3";
            this.badLabel3.Size = new System.Drawing.Size(92, 23);
            this.badLabel3.TabIndex = 5;
            this.badLabel3.TabStop = false;
            this.badLabel3.Text = "Tank Short Name";
            this.badLabel3.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // txtName
            // 
            this.txtName.HasFocus = false;
            this.txtName.Image = null;
            this.txtName.Location = new System.Drawing.Point(132, 96);
            this.txtName.MultilineAllow = false;
            this.txtName.Name = "txtName";
            this.txtName.PasswordChar = '\0';
            this.txtName.ReadOnly = false;
            this.txtName.Size = new System.Drawing.Size(156, 23);
            this.txtName.TabIndex = 4;
            this.txtName.TabStop = false;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtName.ToolTipText = "";
            // 
            // badLabel2
            // 
            this.badLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel2.Dimmed = false;
            this.badLabel2.Image = null;
            this.badLabel2.Location = new System.Drawing.Point(34, 96);
            this.badLabel2.Name = "badLabel2";
            this.badLabel2.Size = new System.Drawing.Size(69, 23);
            this.badLabel2.TabIndex = 3;
            this.badLabel2.TabStop = false;
            this.badLabel2.Text = "Tank Name";
            this.badLabel2.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // btnClose
            // 
            this.btnClose.BlackButton = false;
            this.btnClose.Checked = false;
            this.btnClose.Image = null;
            this.btnClose.Location = new System.Drawing.Point(233, 268);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Close";
            this.btnClose.ToolTipText = "";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.HasFocus = false;
            this.txtID.Image = null;
            this.txtID.Location = new System.Drawing.Point(132, 67);
            this.txtID.MultilineAllow = false;
            this.txtID.Name = "txtID";
            this.txtID.PasswordChar = '\0';
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(57, 23);
            this.txtID.TabIndex = 2;
            this.txtID.TabStop = false;
            this.txtID.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtID.ToolTipText = "";
            // 
            // badLabel1
            // 
            this.badLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.badLabel1.Dimmed = false;
            this.badLabel1.Image = null;
            this.badLabel1.Location = new System.Drawing.Point(34, 67);
            this.badLabel1.Name = "badLabel1";
            this.badLabel1.Size = new System.Drawing.Size(69, 23);
            this.badLabel1.TabIndex = 1;
            this.badLabel1.TabStop = false;
            this.badLabel1.Text = "ID";
            this.badLabel1.TxtAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // btnSave
            // 
            this.btnSave.BlackButton = false;
            this.btnSave.Checked = false;
            this.btnSave.Image = null;
            this.btnSave.Location = new System.Drawing.Point(152, 268);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.ToolTipText = "";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(17, 44);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(291, 208);
            this.badGroupBox1.TabIndex = 0;
            this.badGroupBox1.Text = "Tank Details";
            // 
            // TankInfoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 310);
            this.Controls.Add(this.badForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TankInfoEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TankInfoEdit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TankInfoEdit_FormClosing);
            this.Load += new System.EventHandler(this.TankInfoEdit_Load);
            this.badForm1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BadForm badForm1;
        private BadDropDownBox ddTankType;
        private BadDropDownBox ddNation;
        private BadTextBox txtTier;
        private BadLabel badLabel6;
        private BadLabel badLabel5;
        private BadLabel badLabel4;
        private BadTextBox txtShortName;
        private BadLabel badLabel3;
        private BadTextBox txtName;
        private BadLabel badLabel2;
        private BadButton btnClose;
        private BadTextBox txtID;
        private BadLabel badLabel1;
        private BadButton btnSave;
        private BadGroupBox badGroupBox1;
        private BadButton btnReset;
    }
}