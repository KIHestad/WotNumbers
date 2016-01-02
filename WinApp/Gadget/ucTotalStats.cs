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
        private object[] currentParameters = null;
        private string battleMode { get; set; }
        private GadgetHelper.TimeRangeEnum _battleTimeSpan { get; set; }
        private int gridColums { get; set; }
        private string[] gridHeaders { get; set; }

        public ucTotalStats(object[] totalStatsParam)
        {
            InitializeComponent();
            currentParameters = totalStatsParam;
            // Get battle mode
            battleMode = (string)currentParameters[0];
            lblBattleMode.Text = "Battle Mode: " + BattleMode.GetItemFromSqlName(battleMode).Name;
            lblTotalStats.Text = "Total Statistics";
            // Get timespan
            _battleTimeSpan = GadgetHelper.GetTimeItemFromName((string)currentParameters[1]).TimeRange;
            // Get number of grids
            gridColums = (int)currentParameters[2];
            // Get headers
            string headerList = currentParameters[3].ToString();
            gridHeaders = headerList.Split(new string[] { ";" }, StringSplitOptions.None);
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
		{
			DataBind();
			base.OnInvalidated(e);
		}

        private void ucTotalStats_Load(object sender, EventArgs e)
		{
			DataBind();
		}

        private void DataBind()
        {
            
            // Greate grid
            GridHelper.StyleGadgetDataGrid(dataGrid, DataGridViewSelectionMode.CellSelect);
            GetGridData();
            // show correct timespan button as selected
            switch (_battleTimeSpan)
            {
                case GadgetHelper.TimeRangeEnum.Total:
                    btnTotal.Checked = true;
                    break;
                case GadgetHelper.TimeRangeEnum.TimeMonth3:
                    btnMonth3.Checked = true;
                    break;
                case GadgetHelper.TimeRangeEnum.TimeMonth:
                    btnMonth.Checked = true;
                    break;
                case GadgetHelper.TimeRangeEnum.TimeWeek:
                    btnWeek.Checked = true;
                    break;
                case GadgetHelper.TimeRangeEnum.TimeToday:
                    btnToday.Checked = true;
                    break;
            }
            // Place grid to user control
            ReziseNow();
        }

        private void GetGridData()
        {
            ClearSelectedColumnsDataGrid();
            // Check if any data rows
            if (currentParameters.Length > 4)
            {
                // Loop through each row
                for (int i = 4; i < currentParameters.Length; i++)
                {
                    // Check if data exists
                    if (currentParameters[i] != null)
                    {
                        // Get parameter data as splitted string array
                        string row = currentParameters[i].ToString();
                        string[] rowItems = row.Split(new string[] { ";" }, StringSplitOptions.None);
                        // Add row to datagrid
                        int rowId = dataGrid.Rows.Add();
                        DataGridViewRow dgvr = dataGrid.Rows[rowId];
                        // Add values for each columd for the new row
                        for (int sectionCol = 0; sectionCol < gridColums; sectionCol++)
                        {
                            dgvr.Cells["Data" + sectionCol].Value = rowItems[(sectionCol * 2) + 1];
                            dgvr.Cells["Value" + sectionCol].Value = rowItems[(sectionCol * 2)];
                            if (_battleTimeSpan != GadgetHelper.TimeRangeEnum.Total)
                                dgvr.Cells["Trend" + sectionCol].Value = 0;
                        }
                    }
                }
            }
            dataGrid.ClearSelection();
        }

        private void ClearSelectedColumnsDataGrid()
        {
            // Add columns to datagrid
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();
            for (int i = 0; i < gridColums; i++)
            {
                dataGrid.Columns.Add("Data" + i.ToString(), gridHeaders[i]);
                dataGrid.Columns.Add("Value" + i.ToString(), "Value");
                if (_battleTimeSpan != GadgetHelper.TimeRangeEnum.Total)
                    dataGrid.Columns.Add("Trend" + i.ToString(), "Trend");
                dataGrid.Columns.Add("Separator" + i.ToString(), "");
            }
            // No sorting and format
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
                dataGrid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGrid.Columns[i].Resizable = DataGridViewTriState.False;
            }
            // Format
            for (int i = 0; i < gridColums; i++)
            {
                dataGrid.Columns["Value" + i.ToString()].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGrid.Columns["Value" + i.ToString()].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (_battleTimeSpan != GadgetHelper.TimeRangeEnum.Total)
                    dataGrid.Columns["Trend" + i.ToString()].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGrid.Columns["Separator" + i.ToString()].HeaderCell.Style.BackColor = ColorTheme.FormBack;
            }
        }

        private void ReziseNow()
        {
            if (dataGrid.ColumnCount > 0)
            {
                int col1Width = Convert.ToInt32(80); // Value
                int col2Width = Convert.ToInt32(00); // Trend
                if (_battleTimeSpan != GadgetHelper.TimeRangeEnum.Total)
                {
                    col1Width = Convert.ToInt32(60); // Value
                    col2Width = Convert.ToInt32(40); // Trend
                }
                int col3Width = Convert.ToInt32(10); // Separator
                float sectionWidth = (dataGrid.Width + col3Width) / gridColums;
                int col0Width = Convert.ToInt32(sectionWidth - col1Width - col2Width - col3Width); // Data
                int step = 3;
                if (_battleTimeSpan != GadgetHelper.TimeRangeEnum.Total)
                    step = 4;
                for (int i = 0; i < gridColums * step; i = i + step)
                {
                    dataGrid.Columns[i + 0].Width = col0Width; // Data
                    dataGrid.Columns[i + 1].Width = col1Width; // Value
                    if (_battleTimeSpan != GadgetHelper.TimeRangeEnum.Total)
                    {
                        dataGrid.Columns[i + 2].Width = col2Width; // Trend
                        dataGrid.Columns[i + 3].Width = col3Width; // Separator
                    }
                    else
                        dataGrid.Columns[i + 2].Width = col3Width; // Separator
                }
            }
            // buttons in footer
            int middleButtonX = (panelFooter.Width / 2) - (btnMonth.Width / 2);
            int distance = btnMonth.Width + 5;
            btnTotal.Left = middleButtonX - (distance * 2);
            btnMonth3.Left = middleButtonX - distance;
            btnMonth.Left = middleButtonX;
            btnWeek.Left = middleButtonX + distance;
            btnToday.Left = middleButtonX + (distance * 2);
            // Hide labels if small
            bool showLabels = (panelFooter.Width > (distance * 5) + 100);
            lblBattleMode.Visible = showLabels;
            lblTotalStats.Visible = showLabels;
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

        private void btnTimeSpan_Click(object sender, EventArgs e)
        {
            BadButton b = (BadButton)sender;
            switch (b.Name)
            {
                case "btnTotal": _battleTimeSpan = GadgetHelper.TimeRangeEnum.Total; break;
                case "btnMonth3": _battleTimeSpan = GadgetHelper.TimeRangeEnum.TimeMonth3; break;
                case "btnMonth": _battleTimeSpan = GadgetHelper.TimeRangeEnum.TimeMonth; break;
                case "btnWeek": _battleTimeSpan = GadgetHelper.TimeRangeEnum.TimeWeek; break;
                case "btnToday": _battleTimeSpan = GadgetHelper.TimeRangeEnum.TimeToday; break;
            }
            SelectTimeRangeButton();
            DataBind();
        }

        private void SelectTimeRangeButton()
        {
            btnTotal.Checked = false;
            btnMonth3.Checked = false;
            btnMonth.Checked = false;
            btnWeek.Checked = false;
            btnToday.Checked = false;
            switch (_battleTimeSpan)
            {
                case GadgetHelper.TimeRangeEnum.Total: btnTotal.Checked = true; break;
                case GadgetHelper.TimeRangeEnum.TimeMonth3: btnMonth3.Checked = true; break;
                case GadgetHelper.TimeRangeEnum.TimeMonth: btnMonth.Checked = true; break;
                case GadgetHelper.TimeRangeEnum.TimeWeek: btnWeek.Checked = true; break;
                case GadgetHelper.TimeRangeEnum.TimeToday: btnToday.Checked = true; break;
            }
        }


    }
}
