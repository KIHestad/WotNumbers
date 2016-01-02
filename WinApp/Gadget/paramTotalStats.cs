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
	public partial class paramTotalStats : Form
	{
		int _gadgetId = -1;
        object[] currentParameters = new object[20];

        public paramTotalStats(int gadgetId = -1)
		{
			InitializeComponent();
			_gadgetId = gadgetId;
		}

        private void paramTotalStats_Load(object sender, EventArgs e)
		{
			if (_gadgetId > -1)
			{
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
                    ddGridCount.Text = currentParameters[2].ToString();

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
                // Get all columns data source
                SetSelectedColumnsDataGrid();

                // Resize
                ReziseNow();
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

        private void SetSelectedColumnsDataGrid()
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
            int datarows = currentParameters.Length - 3;
            if (datarows > 0)
            {
                for (int i = 3; i < datarows; i++)
                {
                    if (currentParameters[i] != null)
                    {
                        string row = currentParameters[i].ToString();
                        string[] rowItems = row.Split(new string[] { ";" }, StringSplitOptions.None);
                        int rowId = dataGridSelectedColumns.Rows.Add();
                        DataGridViewRow dgvr = dataGridSelectedColumns.Rows[rowId];
                        int col = 0;
                        foreach (DataGridViewCell dgvc in dgvr.Cells)
                        {
                            if (col <= rowItems.Length -1)
                            {
                                dgvc.Value = rowItems[col];
                                col++;
                            }
                        }
                    }
                }
            }
            // Add blank rows to datagrid
            dataGridSelectedColumns.Rows.Add();

            // Hide id cols
            int colCount = 0;
            foreach (DataGridViewColumn col in dataGridSelectedColumns.Columns)
            {
                colCount ++;
                if (colCount == 1)
                {
                    col.Visible = false;
                }
                else
                    colCount = 0;

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

		private void btnSelect_Click(object sender, EventArgs e)
		{
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
                int paramCount = 3 + dataGridSelectedColumns.RowCount;
                GadgetHelper.newParameters = new object[paramCount];
                // Add settings
				GadgetHelper.newParameters[0] = paramBattleMode;
                GadgetHelper.newParameters[1] = paramTimeSpan;
                GadgetHelper.newParameters[2] = Convert.ToInt32(ddGridCount.Text);
                // Add colums
                int paramId = 3;
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
                GadgetHelper.DeleteGadgetParameter(_gadgetId);
				this.Close();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			GadgetHelper.newParameters = new object[] { null, null, null, null, null };
			GadgetHelper.newParametersOK = false;
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
            DropDownGrid.Show(ddGridCount, DropDownGrid.DropDownGridType.List, "1,2,3,4,5");
        }

        private void badForm1_Resize(object sender, EventArgs e)
        {
            ReziseNow();
        }

        private void ReziseNow()
        {
            // Size dataGridAllColumns
            int totalWidth = groupRows.Width - 60;
            int dataGridAllColumnsWidht = Convert.ToInt32(totalWidth * 0.40);
            toolAllColumns.Width = dataGridAllColumnsWidht;
            dataGridAllColumns.Width = dataGridAllColumnsWidht - scrollAllColumns.Width;
            scrollAllColumns.Left = dataGridAllColumns.Left + dataGridAllColumns.Width;
            // Size and position dataGridSelectedColumns
            int pos = toolAllColumns.Left + toolAllColumns.Width + 20;
            int dataGridSelectedColumnsWidht = Convert.ToInt32(totalWidth * 0.60);
            lblSelectedColumns.Left = pos;
            toolSelectedColumns.Left = pos;
            dataGridSelectedColumns.Left = pos;
            toolSelectedColumns.Width = dataGridSelectedColumnsWidht;
            dataGridSelectedColumns.Width = dataGridSelectedColumnsWidht;
            // Resize dataGridSelectedColumns col width
            foreach (DataGridViewColumn col in dataGridSelectedColumns.Columns)
            {
                col.Width = Convert.ToInt32(dataGridSelectedColumns.Width / Convert.ToDouble(ddGridCount.Text));
            }
        }


        private void ddGridCount_TextChanged(object sender, EventArgs e)
        {
            SetSelectedColumnsDataGrid();
            ReziseNow();
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
                        dataGridAllColumns.DoDragDrop(selectedRow, DragDropEffects.Copy);
                }
            }
        }

        private void dataGridAllColumns_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        #endregion 

        

        private void groupRows_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void scrollAllColumns_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void dataGridSelectedColumns_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void dataGridSelectedColumns_DragDrop(object sender, DragEventArgs e)
        {
            DataGridViewRow dgvr = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
            string id = dgvr.Cells["id"].Value.ToString();
            string name = dgvr.Cells["Name"].Value.ToString();
            Point cursorLocation = this.PointToClient(new Point(e.X, e.Y));
            Point gridLocation = new Point(cursorLocation.X - dataGridSelectedColumns.Left, cursorLocation.Y - dataGridSelectedColumns.Top);

            System.Windows.Forms.DataGridView.HitTestInfo hittest = dataGridSelectedColumns.HitTest(gridLocation.X, gridLocation.Y);
            // Check if valid cell is selected
            if (hittest.ColumnIndex != -1 && hittest.RowIndex != -1)
            {
                // Check if new rows should be added if new value added to last row - to make room for new value
                int lastRow = dataGridSelectedColumns.RowCount -1;
                if (hittest.RowIndex == lastRow)
                   dataGridSelectedColumns.Rows.Add();
                // Check if value exists
                if (dataGridSelectedColumns[hittest.ColumnIndex, hittest.RowIndex].Value != null)
                {
                    // Check if last row if empty to make room for moving other values down
                    if (dataGridSelectedColumns.Rows[lastRow].Cells[hittest.ColumnIndex].Value != null)
                        dataGridSelectedColumns.Rows.Add();
                    // Move current values down
                    for (int i = lastRow; i > hittest.RowIndex; i--)
                    {
                        dataGridSelectedColumns[hittest.ColumnIndex - 1, i].Value = dataGridSelectedColumns[hittest.ColumnIndex - 1, i - 1].Value; // id
                        dataGridSelectedColumns[hittest.ColumnIndex, i].Value = dataGridSelectedColumns[hittest.ColumnIndex, i - 1].Value; // name
                    }
                    // Check if last row if empty to make room for new values
                    if (dataGridSelectedColumns.Rows[lastRow].Cells[hittest.ColumnIndex].Value != null)
                        dataGridSelectedColumns.Rows.Add();
                }
                // Insert at position
                dataGridSelectedColumns[hittest.ColumnIndex - 1, hittest.RowIndex].Value = id;
                dataGridSelectedColumns[hittest.ColumnIndex, hittest.RowIndex].Value = name;
                
            }
                
        }



        


        

        

        
    }
}
