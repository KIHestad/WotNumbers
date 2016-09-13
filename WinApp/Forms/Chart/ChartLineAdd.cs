using System;
using System.Windows.Forms;
using WinApp.Code.FormView;
using WinApp.Code;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace WinApp.Forms
{
    public partial class ChartLineAdd : FormCloseOnEsc
    {
        private class TankItem
        {
            public bool Select { get; set;}
            public int Tier { get; set; }
            public Image Tank { get; set; }
            public DateTime? LastBattle { get; set; }
            public string TankName { get; set; }
            public string TankNameSearch { get; set; }
            public string TankNameShortSearch { get; set; }
            public int Id { get; set; }
        }

        private List<TankItem> TankList = new List<TankItem>();

        private class ChartTypeItem
        {
            public bool Select { get; set; }
            public string ChartTypeName { get; set; }
        }

        private List<ChartTypeItem> ChartTypeList = new List<ChartTypeItem>();

        int _tankId = 0; // If selected to show chart for tank, open add chart item on startup

        #region Init

        public ChartLineAdd(int tankId = 0)
        {
            _tankId = tankId;
            BattleChartHelper.NewChartItem = new List<BattleChartHelper.BattleChartItem>();
            InitializeComponent();
        }

        private void ChartLineAdd_Load(object sender, EventArgs e)
        {
            // Tank GridStyle and handle scrolling for Tank Grid
            GridHelper.StyleDataGrid(dataGridTanks, DataGridViewSelectionMode.CellSelect);
            dataGridTanks.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridTanks.RowTemplate.Height = 31;
            dataGridTanks.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridTanks.MouseWheel += new MouseEventHandler(dataGridTank_MouseWheel);
            // ChartType GridStyle and handle scrolling for ChartType Grid
            GridHelper.StyleDataGrid(dataGridChartTypes, DataGridViewSelectionMode.CellSelect);
            dataGridChartTypes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridChartTypes.RowTemplate.Height = 27;
            dataGridChartTypes.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridChartTypes.MouseWheel += new MouseEventHandler(dataGridChartTypes_MouseWheel);

            try
            {
                // Get all tanks
                string sql =
                    "select tank.id, tank.tier, tank.short_name, tank.name, playerTank.lastBattleTime " +
                    "from tank inner join playerTank on tank.id = playerTank.tankId " +
                    "where playerTank.playerId = @playerId " +
                    "order by playerTank.lastBattleTime DESC; ";
                DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
                DataTable dtTank = DB.FetchData(sql);
                foreach (DataRow dr in dtTank.Rows)
                {
                    // Use ImageHelper to add tank image
                    Image img = ImageHelper.GetTankImage(Convert.ToInt32(dr["id"]), ImageHelper.TankImageType.SmallImage);
                    DateTime? lastBattle = null;
                    if (dr["lastBattleTime"] != DBNull.Value)
                        lastBattle = Convert.ToDateTime(dr["lastBattleTime"]);
                    string tankShortName = dr["short_name"].ToString();
                    string tankFullName = dr["name"].ToString();
                    string tankNameSearch = tankFullName.Replace(" ", "").Replace("/", "").Replace("-", "").Replace(".", "").Replace("Š", "S").ToLower();
                    string tankNameShortSearch = tankShortName.Replace(" ", "").Replace("/", "").Replace("-", "").Replace(".", "").Replace("Š", "S").ToLower();
                    TankList.Add(new TankItem()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Select = false,
                        Tier = Convert.ToInt32(dr["Tier"]),
                        TankName = tankShortName,
                        TankNameSearch = tankNameSearch,
                        TankNameShortSearch = tankNameShortSearch,
                        LastBattle = lastBattle,
                        Tank = img
                    });
                }

            }
            catch (Exception ex)
            {
                Log.LogToFile(ex);
            }
            // Spesific tank added, select it
            if (_tankId != 0)
            {
                List<TankItem> selectedTank = TankList.Where(t => t.Id == _tankId).ToList();
                foreach (TankItem item in selectedTank)
                {
                    item.Select = true;
                }
                mTankShowSelected.Checked = true;
            }
            // Show tanks now
            TankListShow();
            TanksFormatDataGrid();

            // Get chart type list
            List<BattleChartHelper.ChartType> chartTypeList = BattleChartHelper.GetChartTypeList();
            foreach (BattleChartHelper.ChartType c in chartTypeList)
            {
                ChartTypeList.Add(new ChartTypeItem()
                {
                    Select = false,
                    ChartTypeName = c.name
                });
            }
            // Show charts now
            ChartListShow();
            ChartTypesFormatDataGrid();          
            
        }

        private void ChartLineAdd_Shown(object sender, EventArgs e)
        {
            mTxtSearch.Focus();
        }

        #endregion

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSelect_Click(object sender, EventArgs e)
        {
            // Check if any chart types is selected
            List<ChartTypeItem> selectedChartTypes = ChartTypeList.Where(t => t.Select == true).ToList();
            if (selectedChartTypes.Count == 0)
            {
                MsgBox.Show("Please select at least one chart type", "Missing Chart Value", this);
                return;
            }
            // Check if any tank selected
            List<TankItem> selectedTank = TankList.Where(t => t.Select == true).ToList();
            if (selectedTank.Count == 0 && !chkAllTanks.Checked)
            {
                MsgBox.Show("Please select at least one tank", "No tanks selected", this);
                return;
            }
            // Add all charts for all tanks
            foreach (ChartTypeItem chartTypeItem in selectedChartTypes)
            {
                // Add All tanks chart item selected
                if (chkAllTanks.Checked)
                {
                    BattleChartHelper.BattleChartItem newChartItemAllTanks = new BattleChartHelper.BattleChartItem();
                    newChartItemAllTanks.tankId = 0;
                    newChartItemAllTanks.tankName = "All Tanks";
                    newChartItemAllTanks.chartTypeName = chartTypeItem.ChartTypeName;
                    BattleChartHelper.NewChartItem.Add(newChartItemAllTanks);
                }
                // Check if tanks any selected

                foreach (TankItem tankItem in selectedTank)
                {
                    BattleChartHelper.BattleChartItem newChartItem = new BattleChartHelper.BattleChartItem();
                    newChartItem.tankId = tankItem.Id;
                    newChartItem.tankName = tankItem.TankName;
                    newChartItem.chartTypeName = chartTypeItem.ChartTypeName;
                    BattleChartHelper.NewChartItem.Add(newChartItem);
                }
            }
            // OK, close now and continue
            this.Close();
        }

        private void mTankSearchAndSelect_Click(object sender, EventArgs e)
        {
            TankSearchHelper.OpenTankSearch(this);
            if (TankSearchHelper.Result == MsgBox.Button.OK && TankSearchHelper.SelectedTankId > 0)
            {
                _tankId = TankSearchHelper.SelectedTankId;
                List<TankItem> selectedTank = TankList.Where(t => t.Id == _tankId).ToList();
                foreach (TankItem item in selectedTank)
                {
                    item.Select = true;
                }
                dataGridTanks.Refresh();
                dataGridTanks.ClearSelection();
            }
        }

        private void mTankClearFilter_Click(object sender, EventArgs e)
        {
            mTxtSearch.Text = "";
        }

        private void mTankShowSelected_Click(object sender, EventArgs e)
        {
            mTankShowSelected.Checked = !mTankShowSelected.Checked;
            TankListShow();
        }

        #region datagrid scrolling

        // Enable mouse wheel scrolling for datagrid
        private void dataGridTank_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                // scroll in grid from mouse wheel
                int currentIndex = this.dataGridTanks.FirstDisplayedScrollingRowIndex;
                int scrollLines = SystemInformation.MouseWheelScrollLines;
                if (e.Delta > 0)
                {
                    this.dataGridTanks.FirstDisplayedScrollingRowIndex = Math.Max(0, currentIndex - scrollLines);
                }
                else if (e.Delta < 0)
                {
                    this.dataGridTanks.FirstDisplayedScrollingRowIndex = currentIndex + scrollLines;
                }
                // move scrollbar
                MoveScrollTanks();
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex);
                // throw;
            }
        }

        private void dataGridChartTypes_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                // scroll in grid from mouse wheel
                int currentIndex = this.dataGridChartTypes.FirstDisplayedScrollingRowIndex;
                int scrollLines = SystemInformation.MouseWheelScrollLines;
                if (e.Delta > 0)
                {
                    this.dataGridChartTypes.FirstDisplayedScrollingRowIndex = Math.Max(0, currentIndex - scrollLines);
                }
                else if (e.Delta < 0)
                {
                    this.dataGridChartTypes.FirstDisplayedScrollingRowIndex = currentIndex + scrollLines;
                }
                // move scrollbar
                MoveScrollChartTypes();
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex);
                // throw;
            }
        }

        

        private void MoveScrollTanks()
        {
            scrollTanks.ScrollPosition = dataGridTanks.FirstDisplayedScrollingRowIndex;
        }

        private void MoveScrollChartTypes()
        {
            scrollChartTypes.ScrollPosition = dataGridChartTypes.FirstDisplayedScrollingRowIndex;
        }

        private bool scrollingTanks = false;
        private void scrollTank_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (dataGridTanks.RowCount > 0)
                {
                    scrollingTanks = true;
                    dataGridTanks.FirstDisplayedScrollingRowIndex = scrollTanks.ScrollPosition;
                }
            }
            catch (Exception)
            {
                // throw;
            }
        }

        private void scrollTank_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (dataGridTanks.RowCount > 0 && scrollingTanks)
                {
                    int currentFirstRow = dataGridTanks.FirstDisplayedScrollingRowIndex;
                    dataGridTanks.FirstDisplayedScrollingRowIndex = scrollTanks.ScrollPosition;
                    if (currentFirstRow != dataGridTanks.FirstDisplayedScrollingRowIndex) Refresh();
                }
            }
            catch (Exception)
            {
                // throw;
            }
        }

        private void scrollTank_MouseUp(object sender, MouseEventArgs e)
        {
            scrollingTanks = false;
        }

        private bool scrollingChartTypes = false;
        private void scrollChartTypes_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (dataGridTanks.RowCount > 0)
                {
                    scrollingChartTypes = true;
                    dataGridChartTypes.FirstDisplayedScrollingRowIndex = scrollChartTypes.ScrollPosition;
                }
            }
            catch (Exception)
            {
                // throw;
            }
        }

        private void scrollChartTypes_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (dataGridChartTypes.RowCount > 0 && scrollingChartTypes)
                {
                    int currentFirstRow = dataGridChartTypes.FirstDisplayedScrollingRowIndex;
                    dataGridChartTypes.FirstDisplayedScrollingRowIndex = scrollChartTypes.ScrollPosition;
                    if (currentFirstRow != dataGridChartTypes.FirstDisplayedScrollingRowIndex) Refresh();
                }
            }
            catch (Exception)
            {
                // throw;
            }
        }

        private void scrollChartTypes_MouseUp(object sender, MouseEventArgs e)
        {
            scrollingChartTypes = false;
        }

        #endregion

        private void mTxtSearch_TextChanged(object sender, EventArgs e)
        {
            mTankShowSelected.Checked = false;
            TankListShow();
        }

        private void TankListShow()
        {
            try
            {
                // Show Data
                List<TankItem> ShowTankList;
                bool textSearchExists = (mTxtSearch.Text.Trim() != "");
                string textSearch = mTxtSearch.Text.Trim().Replace(" ", "").Replace("/", "").Replace("-", "").Replace(".", "").Replace("Š", "S").ToLower();
                // Show according to text search and options
                if (mTankShowSelected.Checked && textSearchExists)
                    ShowTankList = TankList.Where(t => t.Select == true && (t.TankNameSearch.Contains(textSearch) || t.TankNameShortSearch.Contains(textSearch))).ToList();
                else if (textSearchExists)
                    ShowTankList = TankList.Where(t => t.TankNameSearch.Contains(textSearch) || t.TankNameShortSearch.Contains(textSearch)).ToList();
                else if (mTankShowSelected.Checked)
                    ShowTankList = TankList.Where(t => t.Select).ToList();
                else
                    ShowTankList = TankList;

                dataGridTanks.DataSource = ShowTankList;
                scrollTanks.ScrollElementsVisible = dataGridTanks.DisplayedRowCount(false);
                scrollTanks.ScrollElementsTotals = ShowTankList.Count;
                MoveScrollTanks();
                dataGridTanks.ClearSelection();

            }
            catch (Exception ex)
            {
                MsgBox.Show("Error occured searching for tank: " + ex.Message, "Error");
                //throw;
            }
        }

        private void ChartListShow()
        {
            try
            {
                // Show Data
                dataGridChartTypes.DataSource = ChartTypeList;
                scrollChartTypes.ScrollElementsVisible = dataGridChartTypes.DisplayedRowCount(false);
                scrollChartTypes.ScrollElementsTotals = ChartTypeList.Count;
                MoveScrollChartTypes();
                dataGridTanks.ClearSelection();

            }
            catch (Exception ex)
            {
                MsgBox.Show("Error occured searching for tank: " + ex.Message, "Error");
                //throw;
            }
        }

        private void TanksFormatDataGrid()
        {
            dataGridTanks.Columns["Select"].DefaultCellStyle.Padding = new Padding(12, 0, 0, 0);
            dataGridTanks.Columns["Select"].Width = 40;
            dataGridTanks.Columns["Select"].ReadOnly = false;

            dataGridTanks.Columns["Tier"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridTanks.Columns["Tier"].Width = 34;
            dataGridTanks.Columns["Tier"].ReadOnly = true;

            dataGridTanks.Columns["Tank"].Width = 159;
            dataGridTanks.Columns["Tank"].ReadOnly = true;

            dataGridTanks.Columns["LastBattle"].HeaderText = "Last Battle";
            dataGridTanks.Columns["LastBattle"].ReadOnly = true;
            
            dataGridTanks.Columns["ID"].Visible = false;
            dataGridTanks.Columns["TankName"].Visible = false;
            dataGridTanks.Columns["TankNameSearch"].Visible = false;
            dataGridTanks.Columns["TankNameShortSearch"].Visible = false;
        }

        private void ChartTypesFormatDataGrid()
        {
            dataGridChartTypes.Columns["Select"].DefaultCellStyle.Padding = new Padding(12, 0, 0, 0);
            dataGridChartTypes.Columns["Select"].Width = 40;
            dataGridChartTypes.Columns["Select"].ReadOnly = false;

            dataGridChartTypes.Columns["ChartTypeName"].Width = 94;
            dataGridChartTypes.Columns["ChartTypeName"].ReadOnly = true;
            dataGridChartTypes.Columns["ChartTypeName"].HeaderText = "Chart Type";
        }

        private void dataGridTanks_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Add tank name on top of tank image
            // Avoid header row and only if advanced main mode
            if (e.RowIndex > -1)
            {
                // Locate image column
                if (e.ColumnIndex == 2)
                {
                    // Now found cell with tank image, now add tank name as text
                    string tankName = dataGridTanks.Rows[e.RowIndex].Cells["TankName"].Value.ToString();
                    e.PaintBackground(e.ClipBounds, false); // no highlighting
                    e.PaintContent(e.ClipBounds);
                    SolidBrush fontColor = new SolidBrush(ColorTheme.ControlFont);
                    SolidBrush fontColorBack = new SolidBrush(Color.Black);
                    int x = e.CellBounds.Left + 60; // left align
                    int y = e.CellBounds.Bottom - 18;
                    e.Graphics.DrawString(tankName, dataGridTanks.DefaultCellStyle.Font, fontColorBack, x + 1, y + 1);
                    e.Graphics.DrawString(tankName, dataGridTanks.DefaultCellStyle.Font, fontColorBack, x - 1, y - 1);
                    e.Graphics.DrawString(tankName, dataGridTanks.DefaultCellStyle.Font, fontColor, x, y);
                    e.Handled = true;
                }
            }
        }

        private void dataGridTanks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check for select deselect tank
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                // Change selected status for tank
                //int tankId = Convert.ToInt32(dataGridTanks.Rows[e.RowIndex].Cells["ID"].Value);
                bool select = Convert.ToBoolean(dataGridTanks.Rows[e.RowIndex].Cells["Select"].Value);
                select = !select;
                dataGridTanks.Rows[e.RowIndex].Cells["Select"].Value = select;
                
            }
        }

        private void dataGridTanks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            bool selected = Convert.ToBoolean(dataGridTanks.Rows[e.RowIndex].Cells["Select"].Value);
            DataGridViewCell cell = dataGridTanks[e.ColumnIndex, e.RowIndex];
            if (selected)
                cell.Style.BackColor = ColorTheme.GridSelectedCellColor;
            else
                cell.Style.BackColor = ColorTheme.FormBack;
        }

        private void dataGridChartTypes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check for select deselect tank
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                // Change selected status for tank
                //string chartTypeName = dataGridChartTypes.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                bool select = Convert.ToBoolean(dataGridChartTypes.Rows[e.RowIndex].Cells["Select"].Value);
                select = !select;
                dataGridChartTypes.Rows[e.RowIndex].Cells["Select"].Value = select;

            }
        }

        private void dataGridChartTypes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            bool selected = Convert.ToBoolean(dataGridChartTypes.Rows[e.RowIndex].Cells["Select"].Value);
            DataGridViewCell cell = dataGridChartTypes[e.ColumnIndex, e.RowIndex];
            if (selected)
                cell.Style.BackColor = ColorTheme.GridSelectedCellColor;
            else
                cell.Style.BackColor = ColorTheme.FormBack;
        }

        private void mTankUnselect_Click(object sender, EventArgs e)
        {
            List<TankItem> selectedTank = TankList.Where(t => t.Select == true).ToList();
            foreach (TankItem item in selectedTank)
            {
                item.Select = false;
            }
            dataGridTanks.Refresh();
            dataGridTanks.ClearSelection();
        }

        
    }
}
