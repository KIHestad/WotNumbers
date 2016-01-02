namespace WinApp.Gadget
{
    partial class ucTotalStats
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
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblTotalStats = new System.Windows.Forms.Label();
            this.lblBattleMode = new System.Windows.Forms.Label();
            this.btnToday = new BadButton();
            this.btnWeek = new BadButton();
            this.btnMonth = new BadButton();
            this.btnMonth3 = new BadButton();
            this.btnTotal = new BadButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeColumns = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(1, 1);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGrid.ShowEditingIcon = false;
            this.dataGrid.Size = new System.Drawing.Size(458, 177);
            this.dataGrid.TabIndex = 0;
            // 
            // panelFooter
            // 
            this.panelFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.panelFooter.Controls.Add(this.btnToday);
            this.panelFooter.Controls.Add(this.btnWeek);
            this.panelFooter.Controls.Add(this.btnMonth);
            this.panelFooter.Controls.Add(this.btnMonth3);
            this.panelFooter.Controls.Add(this.btnTotal);
            this.panelFooter.Controls.Add(this.lblBattleMode);
            this.panelFooter.Controls.Add(this.lblTotalStats);
            this.panelFooter.Location = new System.Drawing.Point(1, 177);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(458, 22);
            this.panelFooter.TabIndex = 21;
            // 
            // lblTotalStats
            // 
            this.lblTotalStats.AutoSize = true;
            this.lblTotalStats.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalStats.ForeColor = System.Drawing.Color.Silver;
            this.lblTotalStats.Location = new System.Drawing.Point(3, 5);
            this.lblTotalStats.Name = "lblTotalStats";
            this.lblTotalStats.Size = new System.Drawing.Size(61, 13);
            this.lblTotalStats.TabIndex = 26;
            this.lblTotalStats.Text = "Footer Text";
            // 
            // lblBattleMode
            // 
            this.lblBattleMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBattleMode.BackColor = System.Drawing.Color.Transparent;
            this.lblBattleMode.ForeColor = System.Drawing.Color.Silver;
            this.lblBattleMode.Location = new System.Drawing.Point(331, 4);
            this.lblBattleMode.Name = "lblBattleMode";
            this.lblBattleMode.Size = new System.Drawing.Size(124, 14);
            this.lblBattleMode.TabIndex = 27;
            this.lblBattleMode.Text = "Battle mode";
            this.lblBattleMode.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnToday
            // 
            this.btnToday.BlackButton = true;
            this.btnToday.Checked = false;
            this.btnToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToday.Image = null;
            this.btnToday.Location = new System.Drawing.Point(285, 3);
            this.btnToday.Margin = new System.Windows.Forms.Padding(0);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(34, 16);
            this.btnToday.TabIndex = 25;
            this.btnToday.Text = "Today";
            this.btnToday.ToolTipText = "";
            this.btnToday.Click += new System.EventHandler(this.btnTimeSpan_Click);
            // 
            // btnWeek
            // 
            this.btnWeek.BlackButton = true;
            this.btnWeek.Checked = false;
            this.btnWeek.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeek.Image = null;
            this.btnWeek.Location = new System.Drawing.Point(247, 3);
            this.btnWeek.Margin = new System.Windows.Forms.Padding(0);
            this.btnWeek.Name = "btnWeek";
            this.btnWeek.Size = new System.Drawing.Size(34, 16);
            this.btnWeek.TabIndex = 24;
            this.btnWeek.Text = "Week";
            this.btnWeek.ToolTipText = "";
            this.btnWeek.Click += new System.EventHandler(this.btnTimeSpan_Click);
            // 
            // btnMonth
            // 
            this.btnMonth.BlackButton = true;
            this.btnMonth.Checked = false;
            this.btnMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonth.Image = null;
            this.btnMonth.Location = new System.Drawing.Point(209, 3);
            this.btnMonth.Margin = new System.Windows.Forms.Padding(0);
            this.btnMonth.Name = "btnMonth";
            this.btnMonth.Size = new System.Drawing.Size(34, 16);
            this.btnMonth.TabIndex = 23;
            this.btnMonth.Text = "Month";
            this.btnMonth.ToolTipText = "";
            this.btnMonth.Click += new System.EventHandler(this.btnTimeSpan_Click);
            // 
            // btnMonth3
            // 
            this.btnMonth3.BlackButton = true;
            this.btnMonth3.Checked = false;
            this.btnMonth3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonth3.Image = null;
            this.btnMonth3.Location = new System.Drawing.Point(171, 3);
            this.btnMonth3.Margin = new System.Windows.Forms.Padding(0);
            this.btnMonth3.Name = "btnMonth3";
            this.btnMonth3.Size = new System.Drawing.Size(34, 16);
            this.btnMonth3.TabIndex = 22;
            this.btnMonth3.Text = "3 Mth";
            this.btnMonth3.ToolTipText = "";
            this.btnMonth3.Click += new System.EventHandler(this.btnTimeSpan_Click);
            // 
            // btnTotal
            // 
            this.btnTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnTotal.BlackButton = true;
            this.btnTotal.Checked = false;
            this.btnTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTotal.Image = null;
            this.btnTotal.Location = new System.Drawing.Point(133, 3);
            this.btnTotal.Margin = new System.Windows.Forms.Padding(0);
            this.btnTotal.Name = "btnTotal";
            this.btnTotal.Size = new System.Drawing.Size(34, 16);
            this.btnTotal.TabIndex = 21;
            this.btnTotal.Text = "Total";
            this.btnTotal.ToolTipText = "";
            this.btnTotal.Click += new System.EventHandler(this.btnTimeSpan_Click);
            // 
            // ucTotalStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.dataGrid);
            this.MinimumSize = new System.Drawing.Size(100, 40);
            this.Name = "ucTotalStats";
            this.Size = new System.Drawing.Size(460, 200);
            this.Load += new System.EventHandler(this.ucTotalStats_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucTotalStats_Paint);
            this.Resize += new System.EventHandler(this.ucTotalStats_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Panel panelFooter;
        private BadButton btnToday;
        private BadButton btnWeek;
        private BadButton btnMonth;
        private BadButton btnMonth3;
        private BadButton btnTotal;
        private System.Windows.Forms.Label lblTotalStats;
        private System.Windows.Forms.Label lblBattleMode;

    }
}
