using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Gadget
{
    public partial class ucTotalStats : UserControl
    {
        private static object[] param = null;
        private static string battleMode { get; set; }
        private static string timespan { get; set; }
        private static int grids { get; set; }

        public ucTotalStats(object[] totalStatsParam)
        {
            InitializeComponent();
            param = totalStatsParam;
            // Get battle mode
            battleMode = (string)param[0];
            // Get timespan
            timespan = (string)param[1];
            // Get number of grids
            grids = (int)param[2];
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
		{
			// DataBind();
			// base.OnInvalidated(e);
		}

        private void ucTotalStats_Load(object sender, EventArgs e)
		{
			DataBind();
		}

        private void DataBind()
        {
            
            // Greate grids
            for (int i = 0; i < grids; i++)
            {
                // Create Grid now
                DataGridView newGrid = new DataGridView();
                newGrid.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(DataGridView_DataBindingComplete);
                newGrid.Name = "Grid" + i.ToString();
                GridHelper.StyleGadgetDataGrid(newGrid);
                newGrid.DataSource = GetGridData(i);
                // Place grid to user control
                panelMain.Controls.Add(newGrid);
            }
            ReziseNow();
        }

        private void DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            dgv.ClearSelection();
        }

        private void ReziseNow()
        {
            float gridWidth = (Width - (10 * (grids - 1))) / grids;
            int gridX = 0;
            for (int i = 0; i < grids; i++)
            {
                string gridName = "Grid" + i.ToString();
                Control[] gridControl = panelMain.Controls.Find(gridName, false);
                if (gridControl.Length > 0)
                {
                    DataGridView dgv = (DataGridView)gridControl[0];
                    dgv.Location = new Point(gridX, 0);
                    dgv.Width = Convert.ToInt32(gridWidth);
                    gridX += Convert.ToInt32(gridWidth) + 10;
                    dgv.Columns[0].Width = Convert.ToInt32(gridWidth * 0.20);
                    dgv.Columns[1].Width = Convert.ToInt32(gridWidth * 0.80);
                }
            }
        }

        private static DataTable GetGridData(int gridNumber)
        {
            string sql = "select top 5 id, name from tank";
            DataTable dt = DB.FetchData(sql, false);
            return dt;
        }

        private void ucTotalStats_Paint(object sender, PaintEventArgs e)
        {
            if (BackColor == ColorTheme.FormBackSelectedGadget)
                GadgetHelper.DrawBorderOnGadget(sender, e);
        }

        private void ucTotalStats_Resize(object sender, EventArgs e)
        {
            ReziseNow();
        }
    }
}
