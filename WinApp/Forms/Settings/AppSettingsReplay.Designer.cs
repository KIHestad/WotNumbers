namespace WinApp.Forms.Settings
{
    partial class AppSettingsReplay
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
            this.dataGridReplayFolder = new System.Windows.Forms.DataGridView();
            this.folderBrowserDialogDBPath = new System.Windows.Forms.FolderBrowserDialog();
            this.scrollY = new BadScrollBar();
            this.btnRemove = new BadButton();
            this.btnAdd = new BadButton();
            this.badGroupBox2 = new BadGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReplayFolder)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridReplayFolder
            // 
            this.dataGridReplayFolder.AllowUserToAddRows = false;
            this.dataGridReplayFolder.AllowUserToDeleteRows = false;
            this.dataGridReplayFolder.AllowUserToOrderColumns = true;
            this.dataGridReplayFolder.AllowUserToResizeRows = false;
            this.dataGridReplayFolder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridReplayFolder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridReplayFolder.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridReplayFolder.Location = new System.Drawing.Point(19, 26);
            this.dataGridReplayFolder.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridReplayFolder.Name = "dataGridReplayFolder";
            this.dataGridReplayFolder.ReadOnly = true;
            this.dataGridReplayFolder.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridReplayFolder.RowHeadersVisible = false;
            this.dataGridReplayFolder.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridReplayFolder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridReplayFolder.Size = new System.Drawing.Size(393, 209);
            this.dataGridReplayFolder.TabIndex = 31;
            // 
            // scrollY
            // 
            this.scrollY.BackColor = System.Drawing.Color.Transparent;
            this.scrollY.Image = null;
            this.scrollY.Location = new System.Drawing.Point(412, 26);
            this.scrollY.Margin = new System.Windows.Forms.Padding(0);
            this.scrollY.Name = "scrollY";
            this.scrollY.ScrollElementsTotals = 100;
            this.scrollY.ScrollElementsVisible = 20;
            this.scrollY.ScrollHide = false;
            this.scrollY.ScrollNecessary = true;
            this.scrollY.ScrollOrientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
            this.scrollY.ScrollPosition = 0;
            this.scrollY.Size = new System.Drawing.Size(17, 209);
            this.scrollY.TabIndex = 32;
            this.scrollY.Text = "badScrollBar1";
            this.scrollY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrollY_MouseDown);
            this.scrollY.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scrollY_MouseMove);
            // 
            // btnRemove
            // 
            this.btnRemove.BlackButton = false;
            this.btnRemove.Checked = false;
            this.btnRemove.Image = null;
            this.btnRemove.Location = new System.Drawing.Point(375, 271);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(70, 23);
            this.btnRemove.TabIndex = 30;
            this.btnRemove.Text = "Remove";
            this.btnRemove.ToolTipText = "";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BlackButton = false;
            this.btnAdd.Checked = false;
            this.btnAdd.Image = null;
            this.btnAdd.Location = new System.Drawing.Point(298, 271);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 23);
            this.btnAdd.TabIndex = 28;
            this.btnAdd.Text = "Add";
            this.btnAdd.ToolTipText = "";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // badGroupBox2
            // 
            this.badGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.badGroupBox2.Image = null;
            this.badGroupBox2.Location = new System.Drawing.Point(1, 1);
            this.badGroupBox2.Name = "badGroupBox2";
            this.badGroupBox2.Size = new System.Drawing.Size(445, 253);
            this.badGroupBox2.TabIndex = 29;
            this.badGroupBox2.Text = "Folders cointaing replay files";
            // 
            // AppSettingsReplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.scrollY);
            this.Controls.Add(this.dataGridReplayFolder);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.badGroupBox2);
            this.Name = "AppSettingsReplay";
            this.Size = new System.Drawing.Size(454, 304);
            this.Load += new System.EventHandler(this.AppSettingsReplay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReplayFolder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BadButton btnRemove;
        private BadButton btnAdd;
        private BadGroupBox badGroupBox2;
        private System.Windows.Forms.DataGridView dataGridReplayFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogDBPath;
        private BadScrollBar scrollY;
    }
}
