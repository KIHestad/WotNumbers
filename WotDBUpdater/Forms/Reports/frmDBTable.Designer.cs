namespace WotDBUpdater.Forms.Reports
{
    partial class frmDBTable
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
            this.dataGridViewShowTable = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.ddSelectTable = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShowTable)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewShowTable
            // 
            this.dataGridViewShowTable.AllowUserToAddRows = false;
            this.dataGridViewShowTable.AllowUserToDeleteRows = false;
            this.dataGridViewShowTable.AllowUserToOrderColumns = true;
            this.dataGridViewShowTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewShowTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewShowTable.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewShowTable.Name = "dataGridViewShowTable";
            this.dataGridViewShowTable.ReadOnly = true;
            this.dataGridViewShowTable.Size = new System.Drawing.Size(815, 402);
            this.dataGridViewShowTable.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Table:";
            // 
            // ddSelectTable
            // 
            this.ddSelectTable.FormattingEnabled = true;
            this.ddSelectTable.Location = new System.Drawing.Point(83, 5);
            this.ddSelectTable.Name = "ddSelectTable";
            this.ddSelectTable.Size = new System.Drawing.Size(226, 21);
            this.ddSelectTable.TabIndex = 3;
            this.ddSelectTable.SelectedValueChanged += new System.EventHandler(this.ddSelectTable_SelectedValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.ddSelectTable);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(815, 31);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.dataGridViewShowTable);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(815, 402);
            this.panel2.TabIndex = 5;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(315, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmDBTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 433);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmDBTable";
            this.ShowInTaskbar = false;
            this.Text = "Show Database Table";
            this.Load += new System.EventHandler(this.frmDBTable_Load);
            this.SizeChanged += new System.EventHandler(this.frmDBTable_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShowTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewShowTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddSelectTable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRefresh;

    }
}