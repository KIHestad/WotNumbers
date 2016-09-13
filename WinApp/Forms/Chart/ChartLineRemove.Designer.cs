namespace WinApp.Forms
{
    partial class ChartLineRemove
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartLineRemove));
            this.ChartLineRemoveTheme = new BadForm();
            this.scrollChartValues = new BadScrollBar();
            this.dataGridChartValues = new System.Windows.Forms.DataGridView();
            this.cmdCancel = new BadButton();
            this.cmdRemove = new BadButton();
            this.badGroupBox1 = new BadGroupBox();
            this.ChartLineRemoveTheme.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridChartValues)).BeginInit();
            this.SuspendLayout();
            // 
            // ChartLineRemoveTheme
            // 
            this.ChartLineRemoveTheme.Controls.Add(this.scrollChartValues);
            this.ChartLineRemoveTheme.Controls.Add(this.dataGridChartValues);
            this.ChartLineRemoveTheme.Controls.Add(this.cmdCancel);
            this.ChartLineRemoveTheme.Controls.Add(this.cmdRemove);
            this.ChartLineRemoveTheme.Controls.Add(this.badGroupBox1);
            this.ChartLineRemoveTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartLineRemoveTheme.FormBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ChartLineRemoveTheme.FormExitAsMinimize = false;
            this.ChartLineRemoveTheme.FormFooter = false;
            this.ChartLineRemoveTheme.FormFooterHeight = 26;
            this.ChartLineRemoveTheme.FormInnerBorder = 3;
            this.ChartLineRemoveTheme.FormMargin = 0;
            this.ChartLineRemoveTheme.Image = null;
            this.ChartLineRemoveTheme.Location = new System.Drawing.Point(0, 0);
            this.ChartLineRemoveTheme.MainArea = mainAreaClass1;
            this.ChartLineRemoveTheme.Name = "ChartLineRemoveTheme";
            this.ChartLineRemoveTheme.Resizable = true;
            this.ChartLineRemoveTheme.Size = new System.Drawing.Size(366, 378);
            this.ChartLineRemoveTheme.SystemExitImage = ((System.Drawing.Image)(resources.GetObject("ChartLineRemoveTheme.SystemExitImage")));
            this.ChartLineRemoveTheme.SystemMaximizeImage = null;
            this.ChartLineRemoveTheme.SystemMinimizeImage = null;
            this.ChartLineRemoveTheme.TabIndex = 0;
            this.ChartLineRemoveTheme.Text = "Remove Chart Values";
            this.ChartLineRemoveTheme.TitleHeight = 26;
            // 
            // scrollChartValues
            // 
            this.scrollChartValues.BackColor = System.Drawing.Color.Transparent;
            this.scrollChartValues.Image = null;
            this.scrollChartValues.Location = new System.Drawing.Point(312, 70);
            this.scrollChartValues.Name = "scrollChartValues";
            this.scrollChartValues.ScrollElementsTotals = 100;
            this.scrollChartValues.ScrollElementsVisible = 20;
            this.scrollChartValues.ScrollHide = false;
            this.scrollChartValues.ScrollNecessary = true;
            this.scrollChartValues.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
            this.scrollChartValues.ScrollPosition = 0;
            this.scrollChartValues.Size = new System.Drawing.Size(17, 229);
            this.scrollChartValues.TabIndex = 28;
            this.scrollChartValues.TabStop = false;
            this.scrollChartValues.Text = "badScrollBar2";
            this.scrollChartValues.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollChartValues_MouseDown);
            this.scrollChartValues.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollChartValues_MouseMove);
            this.scrollChartValues.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scrollChartValues_MouseUp);
            // 
            // dataGridChartValues
            // 
            this.dataGridChartValues.AllowUserToAddRows = false;
            this.dataGridChartValues.AllowUserToDeleteRows = false;
            this.dataGridChartValues.AllowUserToOrderColumns = true;
            this.dataGridChartValues.AllowUserToResizeRows = false;
            this.dataGridChartValues.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridChartValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridChartValues.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridChartValues.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridChartValues.Location = new System.Drawing.Point(36, 70);
            this.dataGridChartValues.Name = "dataGridChartValues";
            this.dataGridChartValues.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridChartValues.RowHeadersVisible = false;
            this.dataGridChartValues.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridChartValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridChartValues.Size = new System.Drawing.Size(276, 229);
            this.dataGridChartValues.TabIndex = 27;
            this.dataGridChartValues.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridChartValues_CellClick);
            this.dataGridChartValues.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridChartValues_CellFormatting);
            // 
            // cmdCancel
            // 
            this.cmdCancel.BlackButton = false;
            this.cmdCancel.Checked = false;
            this.cmdCancel.Image = null;
            this.cmdCancel.Location = new System.Drawing.Point(271, 336);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 26;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.ToolTipText = "";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdRemove
            // 
            this.cmdRemove.BlackButton = false;
            this.cmdRemove.Checked = false;
            this.cmdRemove.Image = null;
            this.cmdRemove.Location = new System.Drawing.Point(190, 336);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(75, 23);
            this.cmdRemove.TabIndex = 25;
            this.cmdRemove.Text = "Remove";
            this.cmdRemove.ToolTipText = "";
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // badGroupBox1
            // 
            this.badGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox1.Image = null;
            this.badGroupBox1.Location = new System.Drawing.Point(18, 44);
            this.badGroupBox1.Name = "badGroupBox1";
            this.badGroupBox1.Size = new System.Drawing.Size(328, 275);
            this.badGroupBox1.TabIndex = 24;
            this.badGroupBox1.Text = "Select Chart Values To Remove";
            // 
            // ChartLineRemove
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 378);
            this.Controls.Add(this.ChartLineRemoveTheme);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChartLineRemove";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChartLineRemove";
            this.Load += new System.EventHandler(this.ChartLineRemove_Load);
            this.ChartLineRemoveTheme.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridChartValues)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BadForm ChartLineRemoveTheme;
        private BadScrollBar scrollChartValues;
        private System.Windows.Forms.DataGridView dataGridChartValues;
        private BadButton cmdCancel;
        private BadButton cmdRemove;
        private BadGroupBox badGroupBox1;
    }
}