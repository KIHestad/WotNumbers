using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
    public partial class TankSearch : Form
    {
        private DataTable dt { get; set; }

        #region Init

        public TankSearch()
        {
            InitializeComponent();
        }

        private void TankSearch_Load(object sender, EventArgs e)
        {
            // Style toolbar
            toolStripMain.Renderer = new StripRenderer();
            // Style datagrid
            GridHelper.StyleDataGrid(dataGridTanks, DataGridViewSelectionMode.CellSelect);
            dataGridTanks.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridTanks.RowTemplate.Height = 31;
            dataGridTanks.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            // Create table to hold data
            dt = new DataTable();
            dt.Columns.Add("LightID", typeof(Int32));
            dt.Columns.Add("LightName", typeof(string));
            dt.Columns.Add("Light", typeof(Image));
            
            dt.Columns.Add("MediumID", typeof(Int32));
            dt.Columns.Add("MediumName", typeof(string));
            dt.Columns.Add("Medium", typeof(Image));
            
            dt.Columns.Add("HeavyID", typeof(Int32));
            dt.Columns.Add("HeavyName", typeof(string));
            dt.Columns.Add("Heavy", typeof(Image));
            
            dt.Columns.Add("TDID", typeof(Int32));
            dt.Columns.Add("TDName", typeof(string));
            dt.Columns.Add("TD", typeof(Image));
            
            dt.Columns.Add("SPGID", typeof(Int32));
            dt.Columns.Add("SPGName", typeof(string));
            dt.Columns.Add("SPG", typeof(Image));
            // Mouse scrolling
            dataGridTanks.MouseWheel += new MouseEventHandler(dataGridTanks_MouseWheel);
            // Set result = cancel as default
            TankHelper.TankSearchResult = MsgBox.Button.Cancel;
            // Show data
            SearchNow();
            ResizeNow();
        }

        #endregion

        #region Grid Formatting

        private void FormatDataGrid()
        {
            dataGridTanks.Columns["LightID"].Visible = false;
            dataGridTanks.Columns["MediumID"].Visible = false;
            dataGridTanks.Columns["HeavyID"].Visible = false;
            dataGridTanks.Columns["TDID"].Visible = false;
            dataGridTanks.Columns["SPGID"].Visible = false;

            dataGridTanks.Columns["LightName"].Visible = false;
            dataGridTanks.Columns["MediumName"].Visible = false;
            dataGridTanks.Columns["HeavyName"].Visible = false;
            dataGridTanks.Columns["TDName"].Visible = false;
            dataGridTanks.Columns["SPGName"].Visible = false;

            dataGridTanks.Columns["TD"].HeaderText = "T D";
            dataGridTanks.Columns["SPG"].HeaderText = "S P G";
            foreach (DataGridViewColumn column in dataGridTanks.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewRow row in dataGridTanks.Rows)
            {
                float newSize = 7;
                if (row.Cells[0].Value != DBNull.Value && row.Cells[0].Value.ToString() == "-1")
                {
                    row.DefaultCellStyle.BackColor = ColorTheme.GridTotalsRow;
                    row.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    row.DefaultCellStyle.SelectionBackColor = row.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.Font = new Font(dataGridTanks.DefaultCellStyle.Font.FontFamily, newSize);
                    row.Height = 14;
                }
            }
            dataGridTanks.ClearSelection();
            scrollAllTanks.ScrollElementsTotals = dt.Rows.Count;
            scrollAllTanks.ScrollElementsVisible = dataGridTanks.DisplayedRowCount(false);
        }

        private void TankSearch_Shown(object sender, EventArgs e)
        {
            mTxtSearch.Focus();
        }

        #endregion

        private void TankSearch_Resize(object sender, EventArgs e)
        {
            ResizeNow();
        }

        private void ResizeNow()
        {
            try
            {
                int colWidth = (dataGridTanks.Width - 2) / 5;
                dataGridTanks.Columns[2].Width = colWidth;
                dataGridTanks.Columns[5].Width = colWidth;
                dataGridTanks.Columns[8].Width = colWidth;
                dataGridTanks.Columns[11].Width = colWidth;
                dataGridTanks.Columns[14].Width = (dataGridTanks.Width - 2) - (colWidth * 4);
                scrollAllTanks.ScrollElementsVisible = dataGridTanks.DisplayedRowCount(false);
            }
            catch (Exception)
            {
                // throw;
            }
        }

        private void mNationToggleAll_Click(object sender, EventArgs e)
        {
            if (mNationToggleAll.Text == "All")
            {
                mNationToggleAll.Text = "None";
                SelectAllNations(true);
            }
            else
            {
                mNationToggleAll.Text = "All";
                SelectAllNations(false);
            }
            SearchNow();
        }

        private void mNation_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = (ToolStripButton)sender;
            if (mNationSelectMode.Text == "Single" && !btn.Checked)
                SelectAllNations(false);
            btn.Checked = !btn.Checked;
            SearchNow();
        }

        private int GetNationCheckedCount()
        {
            int count = 0;
            for (int i = 0; i <= 7; i++)
            {
                ToolStripButton item = (ToolStripButton)toolStripMain.Items.Find("mNation" + i.ToString(), false)[0];
                if (item.Checked)
                    count++;
            }
            return count;
        }

        private string GetSelectedNations()
        {
            int count = 0;
            string countryId = "";
            for (int i = 0; i <= 7; i++)
            {
                ToolStripButton item = (ToolStripButton)toolStripMain.Items.Find("mNation" + i.ToString(), false)[0];
                if (item.Checked)
                {
                    count++;
                    countryId += i.ToString() + ",";
                }
            }
            string countryWhere = "";
            if (count == 1)
                countryWhere = "AND countryId = " + countryId.Substring(0, 1) + " ";
            else if (count > 1)
                countryWhere = "AND countryId IN (" + countryId.Substring(0, countryId.Length - 1) + ") ";
            return countryWhere;
        }

        private void SelectAllNations(bool select)
        {
            for (int i = 0; i <= 7; i++)
            {
                ToolStripButton item = (ToolStripButton)toolStripMain.Items.Find("mNation" + i.ToString(), false)[0];
                item.Checked = select;
            }
        }

        private void mTxtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchNow();
        }

        private void mNationSelectMode_Click(object sender, EventArgs e)
        {
            if (mNationSelectMode.Text == "Single")
            {
                mNationSelectMode.Text = "Multi";
                mNationToggleAll.Visible = true;
            }
            else
            {
                mNationSelectMode.Text = "Single";
                mNationToggleAll.Visible = false;
                mNationToggleAll.Text = "All";
                if (GetNationCheckedCount() > 1)
                    SelectAllNations(false);
            }
        }

        private void dataGridTanks_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int tankId = -1;
            if (dataGridTanks.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value != DBNull.Value)
                tankId = Convert.ToInt32(dataGridTanks.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value);
            if (tankId > 0)
            {
                TankHelper.TankSearchSelectedTankId = tankId;
                TankHelper.TankSearchResult = MsgBox.Button.OK;
                this.Close();
            }
        }

        private void SearchNow()
        {
            try
            {
                // Clear result datatable, get ready for retrieving tanks
                dt.Clear();
                // Search for tanks
                string freeTextSearch = "";
                if (mTxtSearch.Text.Trim() != "")
                    freeTextSearch = "AND tank.name like '" + mTxtSearch.Text.Trim() + "%' ";
                string nationFilter = GetSelectedNations();
                // Check if any selection is made
                if (freeTextSearch == "" && nationFilter == "")
                {
                    // No search to be performed, return empty result
                    AddTierHeading(11, 1);
                    dataGridTanks.DataSource = dt;
                    FormatDataGrid();
                    return;
                }
                // Get data from search / filter parameters
                string sql =
                    "select tank.id, tank.name, tank.tankTypeId, tank.countryId, tank.tier " +
                    "from tank inner join playerTank on tank.id = playerTank.tankId " +
                    "where playerTank.playerId = @playerId " +
                    nationFilter +
                    freeTextSearch +
                    "order by tank.tier desc, tank.tankTypeId, tank.name; ";
                DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
                DataTable dtSearchTanks = DB.FetchData(sql);

                // Populate table
                int currentTier = 11; // start one above max tier to force write first header
                int tierRows = 0; // Number of rows added per tier
                int[] tierRowsUsedPerTankType = new int[] { 0, 0, 0, 0, 0 }; // Number of rows userd per tier per tank type
                int lastRowNum = 0; // Currently the last row added to the final datatable holding all tanks
                foreach (DataRow dr in dtSearchTanks.Rows)
                {
                    // Check if tank tier is lower than current tier for adding header
                    int tankTier = Convert.ToInt32(dr["tier"]);
                    if (currentTier > tankTier)
                    {
                        AddTierHeading(currentTier, tankTier);
                        currentTier = tankTier;
                        tierRows = 0;
                        tierRowsUsedPerTankType = new int[] { 0, 0, 0, 0, 0 }; // 0 - 4
                    }
                    // Get new tank info
                    int tankType = Convert.ToInt32(dr["tankTypeId"]) -1; // tankTypeId = 1 - 5, subtract one to align with tierRowsUsedPerTankType
                    int tankId = Convert.ToInt32(dr["id"]);
                    string tankName = dr["name"].ToString();
                    tierRowsUsedPerTankType[tankType]++; // Add up one for needed rows to add this tank type to grid
                    // Before adding tank to grid, first check if needed to create a new row
                    if (tierRowsUsedPerTankType[tankType] > tierRows)
                    {
                        DataRow drTank = GetEmptyRow(); 
                        dt.Rows.Add(drTank);
                        dt.AcceptChanges();
                        tierRows++;
                        lastRowNum = dt.Rows.Count - 1;
                    }
                    // Now an available row exists, add tank to it
                    int tankTypeCol = tankType * 3; // 3 cols per tank type in grid, first for id (hidden) second for name (hidden) last image
                    dt.Rows[lastRowNum - tierRows + tierRowsUsedPerTankType[tankType]][tankTypeCol] = tankId;
                    dt.Rows[lastRowNum - tierRows + tierRowsUsedPerTankType[tankType]][tankTypeCol + 1] = tankName;
                    dt.Rows[lastRowNum - tierRows + tierRowsUsedPerTankType[tankType]][tankTypeCol + 2] = ImageHelper.GetTankImage(tankId,ImageHelper.TankImageType.SmallImage);
                }
                // Add remaining headers if any
                if (currentTier >= 1)
                    AddTierHeading(currentTier, 1, true);
                // Show Data
                dataGridTanks.DataSource = dt;
                FormatDataGrid();

            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        private void AddTierHeading(int fromTier, int toTier, bool forceEmptyRow = false)
        {
            fromTier--;
            for (int i = fromTier; i >= toTier; i--)
            {
                // Add tier header
                DataRow drHeader = GetEmptyRow();
                drHeader["LightID"] = -1; // Indicate Tier Header Row
                drHeader["Heavy"] = imageListTierIcons.Images[i - 1];
                dt.Rows.Add(drHeader);
                // Add tanks header if several rows in sequence
                if (i > toTier || forceEmptyRow)
                {
                    DataRow drTank = GetEmptyRow();
                    dt.Rows.Add(drTank);
                }
            }
            dt.AcceptChanges();
        }

        private DataRow GetEmptyRow()
        {
            Image img = new Bitmap(1, 1);
            DataRow drTank = dt.NewRow();
            drTank[2] = img;
            drTank[5] = img;
            drTank[8] = img;
            drTank[11] = img;
            drTank[14] = img;
            return drTank;
        }

        private void dataGridTanks_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Add tank name on top of tank image
            // Avoid header row
            if (e.RowIndex > -1)
            {
                // Avoid tier header rows
                if (dataGridTanks.Rows[e.RowIndex].Cells[0].Value == DBNull.Value || dataGridTanks.Rows[e.RowIndex].Cells[0].Value.ToString() != "-1")
                {
                    // Check for valid image cell, each 3rd cell
                    if ((e.ColumnIndex + 1) % 3 == 0)
                    {
                        // Check if image exists, check for name in column to left
                        if (dataGridTanks.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value != DBNull.Value)
                        {
                            // Now found cell with tank image, now add tank name as text - name exists in cell to left
                            string tankName = dataGridTanks.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString();
                            e.PaintBackground(e.ClipBounds, false); // no highlighting
                            e.PaintContent(e.ClipBounds);
                            SolidBrush fontColor = new SolidBrush(ColorTheme.ControlFont);
                            SolidBrush fontColorBack = new SolidBrush(Color.Black);
                            //SizeF stringSize = e.Graphics.MeasureString(tankName, dataGridTanks.DefaultCellStyle.Font, 200);
                            //int x = e.CellBounds.Right - Convert.ToInt32(stringSize.Width); // - right align
                            int x = e.CellBounds.Left + 60; // left align
                            int y = e.CellBounds.Bottom - 18;
                            e.Graphics.DrawString(tankName, dataGridTanks.DefaultCellStyle.Font, fontColorBack, x + 1, y + 1);
                            e.Graphics.DrawString(tankName, dataGridTanks.DefaultCellStyle.Font, fontColorBack, x - 1, y - 1);
                            e.Graphics.DrawString(tankName, dataGridTanks.DefaultCellStyle.Font, fontColor, x, y);
                            e.Handled = true; 

                        }
                    }
                }
            }
        }

        private bool scrollingAllTanks = false;
        private void scrollAllTanks_MouseDown(object sender, MouseEventArgs e)
        {
            if (dataGridTanks.RowCount > 0)
            {
                scrollingAllTanks = true;
                dataGridTanks.FirstDisplayedScrollingRowIndex = scrollAllTanks.ScrollPosition;
            }

        }

        private void scrollAllTanks_MouseMove(object sender, MouseEventArgs e)
        {
            if (dataGridTanks.RowCount > 0 && scrollingAllTanks)
            {
                int currentFirstRow = dataGridTanks.FirstDisplayedScrollingRowIndex;
                dataGridTanks.FirstDisplayedScrollingRowIndex = scrollAllTanks.ScrollPosition;
                if (currentFirstRow != dataGridTanks.FirstDisplayedScrollingRowIndex) Refresh();
            }

        }

        private void scrollAllTanks_MouseUp(object sender, MouseEventArgs e)
        {
            scrollingAllTanks = false;
        }

        // Enable mouse wheel scrolling for datagrid
        private void dataGridTanks_MouseWheel(object sender, MouseEventArgs e)
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
                MoveAllTanksScrollBar();
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex);
                // throw;
            }
        }

        private void MoveAllTanksScrollBar()
        {
            scrollAllTanks.ScrollPosition = dataGridTanks.FirstDisplayedScrollingRowIndex;
        }

        private void dataGridAllTanks_SelectionChanged(object sender, EventArgs e)
        {
            MoveAllTanksScrollBar();
        }

    }
}
