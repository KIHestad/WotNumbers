using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;
using WinApp.Code.FormView;

namespace WinApp.Forms
{
    public partial class ChartLineRemove : Form
    {
        private class ChartValueItem
        {
            public bool Select { get; set; }
            public int TankId { get; set; }
            public string TankName { get; set; }
            public string ChartTypeName { get; set; }
        }

        private List<ChartValueItem> chartValueList = new List<ChartValueItem>();

        public ChartLineRemove()
        {
            InitializeComponent();
        }

        private void ChartLineRemove_Load(object sender, EventArgs e)
        {
            // Init no removes yet
            BattleChartHelper.RemovedChartValues = 0;
            
            // ChartType GridStyle and handle scrolling for ChartType Grid
            GridHelper.StyleDataGrid(dataGridChartValues, DataGridViewSelectionMode.CellSelect);
            dataGridChartValues.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridChartValues.RowTemplate.Height = 27;
            dataGridChartValues.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridChartValues.MouseWheel += new MouseEventHandler(dataGridChartValues_MouseWheel);

            // Get chart type list
            foreach (BattleChartHelper.BattleChartItem item in BattleChartHelper.CurrentChartView)
            {
                chartValueList.Add(new ChartValueItem()
                {
                    Select = false,
                    TankId = item.TankId,
                    TankName = item.TankName,
                    ChartTypeName = item.ChartTypeName
                });
            }
            // Show charts now
            ChartValuehow();
            ChartValuesFormatDataGrid();
        }

        private void ChartValuehow()
        {
            try
            {
                // Show Data
                dataGridChartValues.DataSource = chartValueList;
                scrollChartValues.ScrollElementsVisible = dataGridChartValues.DisplayedRowCount(false);
                scrollChartValues.ScrollElementsTotals = chartValueList.Count;
                MoveScrollChartValues();
                dataGridChartValues.ClearSelection();

            }
            catch (Exception ex)
            {
                MsgBox.Show("Error showing chart value items: " + ex.Message, "Error");
                //throw;
            }
        }

        private void ChartValuesFormatDataGrid()
        {
            dataGridChartValues.Columns["Select"].DefaultCellStyle.Padding = new Padding(12, 0, 0, 0);
            dataGridChartValues.Columns["Select"].Width = 40;
            dataGridChartValues.Columns["Select"].ReadOnly = false;

            dataGridChartValues.Columns["TankName"].Width = 120;
            dataGridChartValues.Columns["TankName"].ReadOnly = true;
            dataGridChartValues.Columns["TankName"].HeaderText = "Tank";

            dataGridChartValues.Columns["ChartTypeName"].Width = 113;
            dataGridChartValues.Columns["ChartTypeName"].ReadOnly = true;
            dataGridChartValues.Columns["ChartTypeName"].HeaderText = "Chart Type";

            dataGridChartValues.Columns["TankId"].Visible = false;
        }

        private void MoveScrollChartValues()
        {
            scrollChartValues.ScrollPosition = dataGridChartValues.FirstDisplayedScrollingRowIndex;
        }

        private async void dataGridChartValues_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                // scroll in grid from mouse wheel
                int currentIndex = this.dataGridChartValues.FirstDisplayedScrollingRowIndex;
                int scrollLines = SystemInformation.MouseWheelScrollLines;
                if (e.Delta > 0)
                {
                    this.dataGridChartValues.FirstDisplayedScrollingRowIndex = Math.Max(0, currentIndex - scrollLines);
                }
                else if (e.Delta < 0)
                {
                    this.dataGridChartValues.FirstDisplayedScrollingRowIndex = currentIndex + scrollLines;
                }
                // move scrollbar
                MoveScrollChartValues();
            }
            catch (Exception ex)
            {
                await Log.LogToFile(ex);
                // throw;
            }
        }

        private bool scrollingChartValues = false;
        private void scrollChartValues_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (dataGridChartValues.RowCount > 0)
                {
                    scrollingChartValues = true;
                    dataGridChartValues.FirstDisplayedScrollingRowIndex = scrollChartValues.ScrollPosition;
                }
            }
            catch (Exception)
            {
                // throw;
            }
        }

        private void scrollChartValues_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (dataGridChartValues.RowCount > 0 && scrollingChartValues)
                {
                    int currentFirstRow = dataGridChartValues.FirstDisplayedScrollingRowIndex;
                    dataGridChartValues.FirstDisplayedScrollingRowIndex = scrollChartValues.ScrollPosition;
                    if (currentFirstRow != dataGridChartValues.FirstDisplayedScrollingRowIndex) Refresh();
                }
            }
            catch (Exception)
            {
                // throw;
            }
        }

        private void scrollChartValues_MouseUp(object sender, MouseEventArgs e)
        {
            scrollingChartValues = false;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            // Check if any chart types is selected
            List<ChartValueItem> selectedChartValues = chartValueList.Where(t => t.Select == true).ToList();
            if (selectedChartValues.Count == 0)
            {
                MsgBox.Show("Please select at least one chart value to remove", "None chart values selected", this);
                return;
            }
            int before = BattleChartHelper.CurrentChartView.Count();
            foreach (ChartValueItem chartValueItem in selectedChartValues)
            {
                // Remove Now
                BattleChartHelper.BattleChartItem itemToRemove = BattleChartHelper.CurrentChartView.Find
                    (x => x.ChartTypeName == chartValueItem.ChartTypeName &&
                        x.TankId == chartValueItem.TankId &&
                        x.TankName == chartValueItem.TankName
                    );
                BattleChartHelper.CurrentChartView.Remove(itemToRemove);
            }
            int after = BattleChartHelper.CurrentChartView.Count();
            BattleChartHelper.RemovedChartValues = before - after;
            this.Close();
        }

        private void dataGridChartValues_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            bool selected = Convert.ToBoolean(dataGridChartValues.Rows[e.RowIndex].Cells["Select"].Value);
            DataGridViewCell cell = dataGridChartValues[e.ColumnIndex, e.RowIndex];
            if (selected)
                cell.Style.BackColor = ColorTheme.GridSelectedCellColor;
            else
                cell.Style.BackColor = ColorTheme.FormBack;
        }

        private void dataGridChartValues_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check for select deselect tank
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                // Change selected status for tank
                //string chartTypeName = dataGridChartTypes.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                bool select = Convert.ToBoolean(dataGridChartValues.Rows[e.RowIndex].Cells["Select"].Value);
                select = !select;
                dataGridChartValues.Rows[e.RowIndex].Cells["Select"].Value = select;

            }
        }
    }

    
}
