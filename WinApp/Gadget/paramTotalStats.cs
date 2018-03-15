using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Gadget
{
	public partial class paramTotalStats : FormCloseOnEsc
    {
        bool _saveOk = false;
        int _gadgetId = -1;
        bool _useDefault = false;

        string _lastSavedGridCount {  get; set; }
        object[] currentParameters = new object[20];
        string[] headers = new string[10];
        int fixedParams = 5; // Number of fixed parameters before grid rows params starts

        public paramTotalStats(int gadgetId = -1, bool useDefault = false)
		{
			InitializeComponent();
			_gadgetId = gadgetId;
            _useDefault = useDefault;
		}

        private void paramTotalStats_Load(object sender, EventArgs e)
		{
            // All colums section
            // Style toolbar and set buttons
            toolAllColumns.Renderer = new StripRenderer();
            //toolAllColumns.BackColor = ColorTheme.FormBack;
            toolAllColumns = ColListHelper.SetToolStripColType(toolAllColumns, GridView.Views.Tank, true);
            toolAvailableCol_All.Checked = true;
            // Mouse scrolling
            dataGridAllColumns.MouseWheel += new MouseEventHandler(dataGridAllColumns_MouseWheel);
            // Style datagrid
            GridHelper.StyleDataGrid(dataGridAllColumns);
            // Get all columns data source
            SetAllColumnsDataGrid();

            // Selected columns section
            // Style toolbar 
            toolSelectedColumns.Renderer = new StripRenderer();
            // Style datagrid
            GridHelper.StyleDataGrid(dataGridSelectedColumns, DataGridViewSelectionMode.CellSelect);

            // Check if new or edit
            if (_gadgetId > -1)
			{
                // Edit total stats gadget
                // Lookup value for current gadget
				string sql = "select * from gadgetParameter where gadgetId=@gadgetId order by paramNum;";
				DB.AddWithValue(ref sql, "@gadgetId", _gadgetId, DB.SqlDataType.Int);
				DataTable dt = DB.FetchData(sql, Config.Settings.showDBErrors);
				foreach (DataRow dr in dt.Rows)
				{
		 			object paramValue = dr["value"];
					int paramNum = Convert.ToInt32(dr["paramNum"]);
					currentParameters[paramNum] = paramValue;
				}
                // Settings values
                if (currentParameters[0] != null)
                    ddBattleMode.Text = BattleMode.GetItemFromSqlName(currentParameters[0].ToString()).Name;
                if (currentParameters[1] != null)
                    ddTimeSpan.Text = GadgetHelper.GetTimeItemFromName(currentParameters[1].ToString()).Name;
                if (currentParameters[2] != null)
                {
                    _lastSavedGridCount = currentParameters[2].ToString();
                    ddGridCount.Text = _lastSavedGridCount;
                }
                if (currentParameters[3] != null)
                {
                    string headerList = currentParameters[3].ToString();
                    headers = headerList.Split(new string[] { ";" }, StringSplitOptions.None);
                }
                if (currentParameters[4] != null)
                {
                    txtHeader.Text = currentParameters[4].ToString();
                }
                // Get selected columns data source
                SetSelectedColumnsDataGrid();
                ResizeNow();
            } 
            else
            {
                // New total stats gadget
                btnRevert.Visible = false;
                if (_useDefault)
                {
                    _lastSavedGridCount = "4"; // Should be the same as for default 
                    UseDefault();
                }
                else
                {
                    _lastSavedGridCount = "3";
                    ClearSelectedColumnsDataGrid();
                    dataGridSelectedColumns.Rows.Add();
                    ResizeNow();
                }
            }
		}

        private void SetAllColumnsDataGrid()
        {
            dataGridAllColumns.DataSource = ColListHelper.GetDataGridColums(toolAllColumns, GridView.Views.Tank, true);
            dataGridAllColumns.Columns["id"].Visible = false;
            dataGridAllColumns.Columns["colWidth"].Visible = false;
            dataGridAllColumns.Columns["Description"].Width = 300;
            // Connect to scrollbar
            scrollAllColumns.ScrollElementsTotals = dataGridAllColumns.RowCount;
            scrollAllColumns.ScrollElementsVisible = dataGridAllColumns.DisplayedRowCount(false);
        }

        private void ClearSelectedColumnsDataGrid()
        {
            // Add columns to datagrid
            int cols = Convert.ToInt32(ddGridCount.Text);
            dataGridSelectedColumns.Rows.Clear();
            dataGridSelectedColumns.Columns.Clear();
            for (int i = 1; i < (cols + 1); i++)
            {
                dataGridSelectedColumns.Columns.Add("id" + i.ToString(), "id" + i.ToString());
                dataGridSelectedColumns.Columns.Add("Column " + i.ToString(), "Column " + i.ToString());
            }
            // No sorting
            for (int i = 0; i < dataGridSelectedColumns.Columns.Count; i++)
            {
                dataGridSelectedColumns.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Hide ID cols
            FormatSelectedColumnDataGrid();
        }

        private void FormatSelectedColumnDataGrid()
        {
            // Hide id cols
            int colCount = 0;
            int visibleColCount = 0;
            foreach (DataGridViewColumn col in dataGridSelectedColumns.Columns)
            {
                colCount++;
                if (colCount == 1)
                {
                    // col 1 should not be visible, holds ID as hiddel col
                    col.Visible = false; 
                }
                else
                {
                    // col 2 should be visible, holds col name
                    col.Visible = true;
                    // Check if header exists and add header name
                    if (visibleColCount < headers.Length && headers[visibleColCount] != null & headers[visibleColCount] != "")
                        col.HeaderText = headers[visibleColCount];
                    else
                        col.HeaderText = "Column " + (visibleColCount + 1).ToString();
                        
                    // Get ready for next iteration
                    visibleColCount++;
                    colCount = 0;
                }
            }
        }

        private void SetSelectedColumnsDataGrid()
        {
            ClearSelectedColumnsDataGrid();
            if (currentParameters.Length > fixedParams)
            {
                for (int i = fixedParams; i < currentParameters.Length; i++)
                {
                    if (currentParameters[i] != null)
                    {
                        ReadRowIntoSelectedColumnsDataGrid(currentParameters[i].ToString());
                    }
                }
            }
            // Add blank rows to datagrid
            dataGridSelectedColumns.Rows.Add();
        }

        private void ReadRowIntoSelectedColumnsDataGrid(string row)
        {
            string[] rowItems = row.Split(new string[] { ";" }, StringSplitOptions.None);
            int rowId = dataGridSelectedColumns.Rows.Add();
            DataGridViewRow dgvr = dataGridSelectedColumns.Rows[rowId];
            int col = 0;
            foreach (DataGridViewCell dgvc in dgvr.Cells)
            {
                if (col <= rowItems.Length - 1)
                {
                    dgvc.Value = rowItems[col];
                    col++;
                }
            }
        }

        private void toolAvaliableCol_Group_Click(object sender, EventArgs e)
        {
            foreach (ToolStripButton button in toolAllColumns.Items)
			{
				button.Checked = false;
			}
            ToolStripButton selectedButton = (ToolStripButton)sender;
            selectedButton.Checked = true;
            SetAllColumnsDataGrid();
        }

		private void ddBattleMode_Click(object sender, EventArgs e)
		{
			DropDownGrid.Show(ddBattleMode, DropDownGrid.DropDownGridType.List, BattleMode.GetDropDownList(true));
		}

		private async void btnSelect_Click(object sender, EventArgs e)
		{
            string headerList = "";
            bool headerListOK = true;
            for (int i = 1; i < dataGridSelectedColumns.ColumnCount; i = i + 2)
            {
                string colHeader = dataGridSelectedColumns.Columns[i].HeaderText;
                headerList += colHeader + ";";
                if (colHeader == "")
                    headerListOK = false;
            }
            if (!headerListOK)
            {
                MsgBox.Show("Please enter a column header for all columns", "Missing column header");
            }
            if (txtHeader.Text.Trim() == "")
            {
                MsgBox.Show("Please select a header text", "Missing header text");
            }
            if (ddBattleMode.Text == "")
            {
                MsgBox.Show("Please select a battle mode", "Missing battle mode");
            }
            else if (ddTimeSpan.Text == "")
            {
                MsgBox.Show("Please select a time span", "Missing time span");
            }
			else
			{
				string paramBattleMode = "";
                BattleMode.Item battleMode = BattleMode.GetItemFromName(ddBattleMode.Text);
                if (battleMode != null)
                    paramBattleMode = battleMode.SqlName;
                string paramTimeSpan = "";
                GadgetHelper.TimeItem ti = GadgetHelper.GetTimeItemFromName(ddTimeSpan.Text);
                if (ti != null)
                    paramTimeSpan = ti.Name;
                // Create new param according to number of rows in datagrid
                int paramCount = fixedParams + dataGridSelectedColumns.RowCount;
                GadgetHelper.newParameters = new object[paramCount];
                // Add settings
				GadgetHelper.newParameters[0] = paramBattleMode;
                GadgetHelper.newParameters[1] = paramTimeSpan;
                GadgetHelper.newParameters[2] = Convert.ToInt32(ddGridCount.Text);
                GadgetHelper.newParameters[3] = headerList;
                GadgetHelper.newParameters[4] = txtHeader.Text;
                // Add colums
                int paramId = fixedParams;
                foreach (DataGridViewRow dgvr in dataGridSelectedColumns.Rows)
                {
                    string paramValue = "";
                    foreach (DataGridViewColumn col in dataGridSelectedColumns.Columns)
                    {
                        paramValue += dgvr.Cells[col.Index].Value + ";";
                    }
                    if (paramValue.Replace(";", "").Length > 0)
                    {
                        GadgetHelper.newParameters[paramId] = paramValue;
                        paramId++;
                    }
                }
				GadgetHelper.newParametersOK = true;
                await GadgetHelper.DeleteGadgetParameter(_gadgetId);
                _saveOk = true;
				this.Close();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void paramBattleMode_Paint(object sender, PaintEventArgs e)
		{
			if (BackColor == ColorTheme.FormBackSelectedGadget)
				GadgetHelper.DrawBorderOnGadget(sender, e);
		}

        private void ddTimeSpan_Click(object sender, EventArgs e)
        {
            DropDownGrid.Show(ddTimeSpan, DropDownGrid.DropDownGridType.List, GadgetHelper.GetTimeDropDownList());
        }

        private void ddGridCount_Click(object sender, EventArgs e)
        {
            DropDownGrid.Show(ddGridCount, DropDownGrid.DropDownGridType.List, "1,2,3,4,5,6,7,8,9,10");
        }

        private void ddGridCount_TextChanged(object sender, EventArgs e)
        {
            int currentCols = dataGridSelectedColumns.DisplayedColumnCount(true);
            int newCols = Convert.ToInt32(ddGridCount.Text);
            if (newCols < currentCols)
            {
                // Remove Cols
                for (int i = currentCols; i > newCols; i--)
                {
                    dataGridSelectedColumns.Columns.RemoveAt((i * 2) - 1);
                    dataGridSelectedColumns.Columns.RemoveAt((i * 2) - 2);
                }
            }
            else if (newCols > currentCols)
            {
                // Add Cols
                for (int i = currentCols; i < newCols; i++)
                {
                    string newColNum = (i + 1).ToString();
                    dataGridSelectedColumns.Columns.Add("id" + newColNum, "id" + newColNum);
                    dataGridSelectedColumns.Columns.Add("Column " + newColNum, "Column " + newColNum);
                }
            }
            FormatSelectedColumnDataGrid();
            ResizeNow();
        }

        private void badForm1_Resize(object sender, EventArgs e)
        {
            ResizeNow();
        }

        private void ResizeNow()
        {
            // Size dataGridAllColumns
            int totalWidth = groupRows.Width - 60;
            int dataGridAllColumnsWidht = Convert.ToInt32(totalWidth * 0.3);
            toolAllColumns.Width = dataGridAllColumnsWidht;
            dataGridAllColumns.Width = dataGridAllColumnsWidht - scrollAllColumns.Width;
            scrollAllColumns.Left = dataGridAllColumns.Left + dataGridAllColumns.Width;
            // Size and position dataGridSelectedColumns
            int pos = toolAllColumns.Left + toolAllColumns.Width + 20;
            int dataGridSelectedColumnsWidht = Convert.ToInt32(totalWidth * 0.7);
            lblSelectedColumns.Left = pos;
            toolSelectedColumns.Left = pos;
            dataGridSelectedColumns.Left = pos;
            toolSelectedColumns.Width = dataGridSelectedColumnsWidht;
            dataGridSelectedColumns.Width = dataGridSelectedColumnsWidht;
            // Resize dataGridSelectedColumns col width
            if (dataGridSelectedColumns.Columns.Count > 0)
            {
                int avgWidth = Convert.ToInt32((dataGridSelectedColumns.Width - 3) / Convert.ToDouble(ddGridCount.Text));
                int totWidth = 0;
                foreach (DataGridViewColumn col in dataGridSelectedColumns.Columns)
                {
                    if (col.Visible)
                    {
                        col.Width = avgWidth;
                        totWidth += avgWidth;
                    }
                }
                if (totWidth != (dataGridSelectedColumns.Width - 3))
                {
                    dataGridSelectedColumns.Columns[dataGridSelectedColumns.Columns.Count - 1].Width = dataGridSelectedColumns.Width - 3 - (avgWidth * (Convert.ToInt32(ddGridCount.Text) - 1));
                }

            }

        }


        

        private bool scrollingAllColumns = false;
        private void scrollAllColumns_MouseDown(object sender, MouseEventArgs e)
        {
            if (dataGridAllColumns.RowCount > 0)
            {
                scrollingAllColumns = true;
                dataGridAllColumns.FirstDisplayedScrollingRowIndex = scrollAllColumns.ScrollPosition;
            }
        }

        private void scrollAllColumns_MouseMove(object sender, MouseEventArgs e)
        {
            if (dataGridAllColumns.RowCount > 0 && scrollingAllColumns)
            {
                int currentFirstRow = dataGridAllColumns.FirstDisplayedScrollingRowIndex;
                dataGridAllColumns.FirstDisplayedScrollingRowIndex = scrollAllColumns.ScrollPosition;
                if (currentFirstRow != dataGridAllColumns.FirstDisplayedScrollingRowIndex) Refresh();
            }
        }

        private void scrollAllColumns_MouseUp(object sender, MouseEventArgs e)
        {
            scrollingAllColumns = false;
        }

        // Enable mouse wheel scrolling for datagrid
        private void dataGridAllColumns_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                // scroll in grid from mouse wheel
                int currentIndex = this.dataGridAllColumns.FirstDisplayedScrollingRowIndex;
                int scrollLines = SystemInformation.MouseWheelScrollLines;
                if (e.Delta > 0)
                {
                    this.dataGridAllColumns.FirstDisplayedScrollingRowIndex = Math.Max(0, currentIndex - scrollLines);
                }
                else if (e.Delta < 0)
                {
                    this.dataGridAllColumns.FirstDisplayedScrollingRowIndex = currentIndex + scrollLines;
                }
                // move scrollbar
                MoveAllColumnsScrollBar();
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex);
                // throw;
            }
        }

        private void MoveAllColumnsScrollBar()
        {
            scrollAllColumns.ScrollPosition = dataGridAllColumns.FirstDisplayedScrollingRowIndex;
        }

        private void dataGridAllTanks_SelectionChanged(object sender, EventArgs e)
        {
            MoveAllColumnsScrollBar();
        }

        #region drag and drop items to selected colums

        private class DragDropValue
        {
            public string id {get; set;}
            public string name {get; set;}
            public DataGridViewCell cellDragFrom { get; set; }
        }

        private static DragDropEffects ddEffect = DragDropEffects.Copy;

        private void dataGridAllColumns_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info = dataGridAllColumns.HitTest(e.X, e.Y);
            if (info.RowIndex >= 0)
            {
                if (info.RowIndex >= 0 && info.ColumnIndex >= 0)
                {
                    dataGridAllColumns.Rows[info.RowIndex].Selected = true;
                    DataGridViewRow selectedRow = dataGridAllColumns.Rows[info.RowIndex];
                    if (selectedRow != null)
                    {
                        DragDropValue ddv = new DragDropValue()
                        {
                            id = selectedRow.Cells["id"].Value.ToString(),
                            name = selectedRow.Cells["Name"].Value.ToString(),
                            cellDragFrom = selectedRow.Cells[info.ColumnIndex]
                        };
                        ddEffect = DragDropEffects.Copy;
                        dataGridAllColumns.DoDragDrop(ddv, ddEffect);
                    }
                }
            }
        }

        private void dataGridSelectedColumns_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info = dataGridSelectedColumns.HitTest(e.X, e.Y);
            if (info.RowIndex >= 0)
            {
                if (info.RowIndex >= 0 && info.ColumnIndex >= 0)
                {
                    dataGridSelectedColumns.Rows[info.RowIndex].Selected = true;
                    DataGridViewRow selectedRow = dataGridSelectedColumns.Rows[info.RowIndex];
                    if (selectedRow != null)
                    {
                        if (selectedRow.Cells[info.ColumnIndex].Value != null && selectedRow.Cells[info.ColumnIndex].Value.ToString() != "")
                        {
                            DragDropValue ddv = new DragDropValue()
                            {
                                id = selectedRow.Cells[info.ColumnIndex - 1].Value.ToString(),
                                name = selectedRow.Cells[info.ColumnIndex].Value.ToString(),
                                cellDragFrom = selectedRow.Cells[info.ColumnIndex]
                            };
                            ddEffect = DragDropEffects.Move;
                            dataGridSelectedColumns.DoDragDrop(ddv, ddEffect);
                        }
                    }
                }
            }
        }

        private static DataGridViewCell DataGridSelectedColumnsSetSelected = null;
        private void dataGridSelectedColumns_MouseMove(object sender, MouseEventArgs e)
        {
            if (DataGridSelectedColumnsSetSelected != null)
            {
                DataGridSelectedColumnsSetSelected.Selected = true;
                DataGridSelectedColumnsSetSelected = null;
            }
                
        }

        private void dataGridSelectedColumns_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void dataGridAllColumns_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = ddEffect;
        }

        private void dataGridSelectedColumns_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = ddEffect;
        }

        private void groupRows_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = ddEffect;
        }

        private void scrollAllColumns_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = ddEffect;
        }

        
        private void dataGridSelectedColumns_DragDrop(object sender, DragEventArgs e)
        {
            DragDropValue ddv = e.Data.GetData(typeof(DragDropValue)) as DragDropValue;
            if (ddv != null)
            {
                Point cursorLocation = this.PointToClient(new Point(e.X, e.Y));
                Point gridLocation = new Point(cursorLocation.X - dataGridSelectedColumns.Left, cursorLocation.Y - dataGridSelectedColumns.Top);

                System.Windows.Forms.DataGridView.HitTestInfo hittest = dataGridSelectedColumns.HitTest(gridLocation.X, gridLocation.Y);
                // Check if valid cell is selected
                if (hittest.ColumnIndex != -1 && hittest.RowIndex != -1)
                {
                    // Get cell dropped at
                    DataGridViewCell cellDrop = dataGridSelectedColumns[hittest.ColumnIndex, hittest.RowIndex];
                    // Check that not moved to same cell
                    if (ddEffect == DragDropEffects.Move && ddv.cellDragFrom != null && ddv.cellDragFrom == cellDrop)
                      return;
                    // Check if value exists
                    if (ddv.cellDragFrom.Value == null)
                        return;
                    // Add new row at bottom if new value dropped to last row or last row is not empty
                    int lastRow = dataGridSelectedColumns.RowCount - 1;
                    if (hittest.RowIndex == lastRow || dataGridSelectedColumns.Rows[lastRow].Cells[hittest.ColumnIndex].Value != null)
                        dataGridSelectedColumns.Rows.Add();
                    lastRow = dataGridSelectedColumns.RowCount - 1;
                    // Get ready to move current values down, default move all down if copy mode
                    int lastRowToMoveDown = lastRow;
                    bool moveup = false;
                    // If move mode, and moved within same column, only move limited area
                    if (ddEffect == DragDropEffects.Move && cellDrop.ColumnIndex == ddv.cellDragFrom.ColumnIndex)
                    {
                        if (cellDrop.RowIndex < ddv.cellDragFrom.RowIndex)
                        {
                            // move up
                            moveup = true;
                            lastRowToMoveDown = ddv.cellDragFrom.RowIndex;
                        }
                    }
                    // Move now
                    for (int i = lastRowToMoveDown; i > hittest.RowIndex; i--)
                    {
                        dataGridSelectedColumns[hittest.ColumnIndex - 1, i].Value = dataGridSelectedColumns[hittest.ColumnIndex - 1, i - 1].Value; // id
                        dataGridSelectedColumns[hittest.ColumnIndex, i].Value = dataGridSelectedColumns[hittest.ColumnIndex, i - 1].Value; // name
                    }
                    // Check if move mode and not moved up within same column, then remove old value
                    if (ddEffect == DragDropEffects.Move && !moveup)
                        RemoveCellFromDataGridSelectedColumns(ddv.cellDragFrom);
                    // Insert at position 
                    dataGridSelectedColumns[hittest.ColumnIndex - 1, hittest.RowIndex].Value = ddv.id;
                    dataGridSelectedColumns[hittest.ColumnIndex, hittest.RowIndex].Value = ddv.name;
                    // Check if last row if empty to make room for new values
                    if (dataGridSelectedColumns.Rows[lastRow].Cells[hittest.ColumnIndex].Value != null)
                        dataGridSelectedColumns.Rows.Add();
                    // Set as selected
                    DataGridSelectedColumnsSetSelected = cellDrop;
                }
            }
        }

        #endregion 

        private void tool_MoveUp_Click(object sender, EventArgs e)
        {
            // Get selecected cell
            int selectedCellCount = dataGridSelectedColumns.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                DataGridViewCell currenCell = dataGridSelectedColumns.SelectedCells[0];
                DataGridViewCell currenCellID = dataGridSelectedColumns.Rows[currenCell.RowIndex].Cells[currenCell.ColumnIndex - 1];
                var currentCellValue = currenCell.Value;
                var currentCellIDValue = currenCellID.Value;
                if (currenCell.RowIndex > 0)
                {
                    DataGridViewCell cellAbove = dataGridSelectedColumns.Rows[currenCell.RowIndex - 1].Cells[currenCell.ColumnIndex];
                    DataGridViewCell cellAboveID = dataGridSelectedColumns.Rows[currenCell.RowIndex - 1].Cells[currenCell.ColumnIndex - 1];
                    currenCell.Value = cellAbove.Value;
                    currenCellID.Value = cellAboveID.Value;
                    cellAbove.Value = currentCellValue;
                    cellAboveID.Value = currentCellIDValue;
                    cellAbove.Selected = true;
                }
            }
        }

        private void tool_MoveDown_Click(object sender, EventArgs e)
        {
            // Get selecected cell
            int selectedCellCount = dataGridSelectedColumns.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                DataGridViewCell currenCell = dataGridSelectedColumns.SelectedCells[0];
                DataGridViewCell currenCellID = dataGridSelectedColumns.Rows[currenCell.RowIndex].Cells[currenCell.ColumnIndex - 1];
                var currentCellValue = currenCell.Value;
                var currentCellIDValue = currenCellID.Value;
                if (currenCell.RowIndex < dataGridSelectedColumns.RowCount - 1)
                {
                    DataGridViewCell cellBelow = dataGridSelectedColumns.Rows[currenCell.RowIndex + 1].Cells[currenCell.ColumnIndex];
                    DataGridViewCell cellBelowID = dataGridSelectedColumns.Rows[currenCell.RowIndex + 1].Cells[currenCell.ColumnIndex - 1];
                    currenCell.Value = cellBelow.Value;
                    currenCellID.Value = cellBelowID.Value;
                    cellBelow.Value = currentCellValue;
                    cellBelowID.Value = currentCellIDValue;
                    cellBelow.Selected = true;
                }
            }
        }

        private void tool_Clear_Click(object sender, EventArgs e)
        {
            // Get selecected cell
            int selectedCellCount = dataGridSelectedColumns.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                DataGridViewCell currenCell = dataGridSelectedColumns.SelectedCells[0];
                DataGridViewCell currenCellID = dataGridSelectedColumns.Rows[currenCell.RowIndex].Cells[currenCell.ColumnIndex - 1];
                currenCell.Value = null;
                currenCellID.Value = null;
            }
        }

        private void tool_AddRow_Click(object sender, EventArgs e)
        {
            // Get selecected cell's row
            int selectedCellCount = dataGridSelectedColumns.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                int rowNum = dataGridSelectedColumns.SelectedCells[0].RowIndex;
                // Add blank rows to datagrid
                dataGridSelectedColumns.Rows.Insert(rowNum);
            }
            else
            {
                dataGridSelectedColumns.Rows.Add();
            }
        }

        private void tool_RemoveRow_Click(object sender, EventArgs e)
        {
            // Get selecected cell's row
            int selectedCellCount = dataGridSelectedColumns.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                int rowNum = dataGridSelectedColumns.SelectedCells[0].RowIndex;
                // Remove row
                dataGridSelectedColumns.Rows.RemoveAt(rowNum);
            }

        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            UseDefault();
        }

        private void UseDefault()
        {
            // From table : SELECT * FROM gadgetParameter WHERE gadgetid = 1344

            //15
            //Total
            //4
            //Battle count;Performance;Damage;XP / Credits;
            //50;Battles;131;Frags;128;Dmg;156;XP Max;
            //95;Win Rate;97;Killed;132;Received Damage;534;Avg Cred Income;
            //86;Victory;219;K/D Ratio;220;Dmg C/R;535;Avg Cred Cost;
            //91;Draw;98;Survival Rate;188;Avg Dmg;536;Avg Cred Result;
            //92;Defeat;155;Frags Max;212;Avg Dmg Assist;537;Max Cred Income;

            ClearSelectedColumnsDataGrid();
            txtHeader.Text = "Total Statistics";
            ddBattleMode.Text = "Random";
            ddTimeSpan.Text = "Total";
            ddGridCount.Text = "4";
            string[] rows = new string[5];
            rows[0] = "50;Battles;131;Frags;128;Dmg;156;XP Max;";
            rows[1] = "95;Win Rate;97;Killed;132;Received Damage;534;Avg Cred Income;";
            rows[2] = "86;Victory;219;K/D Ratio;220;Dmg C/R;535;Avg Cred Cost;";
            rows[3] = "91;Draw;98;Survival Rate;188;Avg Dmg;536;Avg Cred Result;";
            rows[4] = "92;Defeat;155;Frags Max;212;Avg Dmg Assist;537;Max Cred Income;";
            foreach (string row in rows)
            {
                ReadRowIntoSelectedColumnsDataGrid(row);
            }
            dataGridSelectedColumns.Rows.Add();
            dataGridSelectedColumns.Columns[1].HeaderText = "Battle count";
            dataGridSelectedColumns.Columns[3].HeaderText = "Performance";
            dataGridSelectedColumns.Columns[5].HeaderText = "Damage";
            dataGridSelectedColumns.Columns[7].HeaderText = "XP / Credits";
            ResizeNow();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            ClearSelectedColumnsDataGrid();
            dataGridSelectedColumns.Rows.Add();
            ResizeNow();
        }

        private void tool_AddRowBottom_Click(object sender, EventArgs e)
        {
            // Add blank rows to datagrid
            dataGridSelectedColumns.Rows.Add();
        }

        private void tool_InsertCell_Click(object sender, EventArgs e)
        {
            // Get selecected cell
            int selectedCellCount = dataGridSelectedColumns.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                int currentRow = dataGridSelectedColumns.SelectedCells[0].RowIndex;
                int currentColumn = dataGridSelectedColumns.SelectedCells[0].ColumnIndex;
                // Check if available space at end, if not add row
                if (dataGridSelectedColumns.Rows[dataGridSelectedColumns.RowCount - 1].Cells[currentColumn].Value != null)
                    dataGridSelectedColumns.Rows.Add();
                // Move down
                for (int i = dataGridSelectedColumns.RowCount - 1; i > currentRow; i--)
                {
                    dataGridSelectedColumns.Rows[i].Cells[currentColumn].Value = dataGridSelectedColumns.Rows[i - 1].Cells[currentColumn].Value;
                    dataGridSelectedColumns.Rows[i].Cells[currentColumn - 1].Value = dataGridSelectedColumns.Rows[i - 1].Cells[currentColumn - 1].Value;
                }
                dataGridSelectedColumns.Rows[currentRow].Cells[currentColumn].Value = null;
                dataGridSelectedColumns.Rows[currentRow].Cells[currentColumn - 1].Value = null;
            }
        }

        private void tool_removeCell_Click(object sender, EventArgs e)
        {
            RemoveCellFromDataGridSelectedColumns();
        }

        private void RemoveCellFromDataGridSelectedColumns(DataGridViewCell DataGridSelectedColumns_Cell = null)
        {
            DataGridViewCell cellToRemove = null;
            if (DataGridSelectedColumns_Cell == null)
            {
                // Get current selecected cell if exists
                if (dataGridSelectedColumns.GetCellCount(DataGridViewElementStates.Selected) > 0)
                    cellToRemove = dataGridSelectedColumns.SelectedCells[0];
            }
            else
            {
                // Use spesified cell
                cellToRemove = DataGridSelectedColumns_Cell;
            }
            // If any cell process
            if (cellToRemove != null)
            {
                int currentRow = cellToRemove.RowIndex;
                int currentColumn = cellToRemove.ColumnIndex;
                for (int i = currentRow; i < dataGridSelectedColumns.RowCount - 1; i++)
                {
                    dataGridSelectedColumns.Rows[i].Cells[currentColumn].Value = dataGridSelectedColumns.Rows[i + 1].Cells[currentColumn].Value;
                    dataGridSelectedColumns.Rows[i].Cells[currentColumn - 1].Value = dataGridSelectedColumns.Rows[i + 1].Cells[currentColumn - 1].Value;
                }
                dataGridSelectedColumns.Rows[dataGridSelectedColumns.RowCount - 1].Cells[currentColumn].Value = null;
                dataGridSelectedColumns.Rows[dataGridSelectedColumns.RowCount - 1].Cells[currentColumn - 1].Value = null;
            }
        }

        private void btnRevert_Click(object sender, EventArgs e)
        {
            ddGridCount.Text = _lastSavedGridCount;
            SetSelectedColumnsDataGrid();
            ResizeNow();
        }

        private void tool_HeaderName_Click(object sender, EventArgs e)
        {
            // Get selecected cell
            int selectedCellCount = dataGridSelectedColumns.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                int currentColumn = dataGridSelectedColumns.SelectedCells[0].ColumnIndex;
                string defaultText = dataGridSelectedColumns.Columns[currentColumn].HeaderText;
                InputBox.ResultClass result = Code.InputBox.Show(title: "Column Header Name", defaultText: defaultText, owner: this);
                if (result.Button == InputBox.InputButton.OK)
                {
                    dataGridSelectedColumns.Columns[currentColumn].HeaderText = result.InputText;
                }
            }

        }

        private void paramTotalStats_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_saveOk)
            {
                // Cancel saving
                GadgetHelper.newParameters = new object[] { null, null, null, null, null };
                GadgetHelper.newParametersOK = false;
            }
        }
    }
}
