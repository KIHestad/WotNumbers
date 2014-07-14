using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class ColList : Form
	{
		// The current selected col list
		private int SelectedColListId = 0;

		#region Init

		public ColList()
		{
			InitializeComponent();
			if (MainSettings.View == GridView.Views.Tank)
			{
				ColListTheme.Text = "Column Setup - Tank View";
			}

			else if (MainSettings.View == GridView.Views.Battle)
			{
				ColListTheme.Text = "Column Setup - Battle View";
			}
		}

		private void ColumnSetup_Load(object sender, EventArgs e)
		{
			// Make sure borderless form do not cover task bar when maximized
			this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
			// Style toolbar
			toolAllColumns.Renderer = new StripRenderer();
			toolSelectedColumns.Renderer = new StripRenderer();
			toolColList.Renderer = new StripRenderer();
			// No tooltip
			toolAllColumns.ShowItemToolTips = false;
			toolSelectedColumns.ShowItemToolTips = false;
			toolColList.ShowItemToolTips = false;
			// Style datagrid
			StyleDataGrid(dataGridColumnList);
			StyleDataGrid(dataGridAllColumns);
			StyleDataGrid(dataGridSelectedColumns);
			// Show content
			SelectedColListId = MainSettings.GetCurrentGridFilter().ColListId;
			ShowColumnSetupList();
			ShowAllColumn();
			// Mouse scrolling
			dataGridAllColumns.MouseWheel += new MouseEventHandler(dataGridAllColumns_MouseWheel);
			dataGridSelectedColumns.MouseWheel += new MouseEventHandler(dataGridSelectedColumns_MouseWheel);
			// Default cursor
			ColListTheme.Cursor = Cursors.Default;
		}

		#endregion

		#region Resize

		private void ColumnList_Resize(object sender, EventArgs e)
		{
			ResizeNow();
		}

		private void ResizeNow()
		{
			// Resize elements X
			int gridX = (groupTanks.Width - ((toolAllColumns.Left - groupTanks.Left) * 2) - 60) / 2; // total resizeble area
			toolAllColumns.Width = gridX;
			toolSelectedColumns.Width = gridX;
			dataGridAllColumns.Width = gridX - scrollAllColumns.Width;
			dataGridSelectedColumns.Width = gridX - scrollSelectedColumns.Width;
			scrollAllColumns.Left = dataGridAllColumns.Left + dataGridAllColumns.Width;
			// Move middle - right section X
			int rightSectionX = toolAllColumns.Left + toolAllColumns.Width + 60;
			int middleSectionX = toolAllColumns.Left + toolAllColumns.Width + ((60 - btnSelectAll.Width) / 2);
			btnSelectAll.Left = middleSectionX;
			btnSelectSelected.Left = middleSectionX;
			btnRemoveAll.Left = middleSectionX;
			btnRemoveSelected.Left = middleSectionX;
			lblSelectedColumns.Left = rightSectionX;
			toolSelectedColumns.Left = rightSectionX;
			dataGridSelectedColumns.Left = rightSectionX;
			scrollSelectedColumns.Left = dataGridSelectedColumns.Left + dataGridSelectedColumns.Width;
			// Resize elements Y
			int gridY = groupTanks.Height - (toolAllColumns.Top + toolAllColumns.Height - groupTanks.Top) - 15;
			dataGridAllColumns.Height = gridY;
			dataGridSelectedColumns.Height = gridY;
			scrollAllColumns.Height = gridY;
			scrollSelectedColumns.Height = gridY;
			// Move buttons
			int buttonsY = groupTanks.Height / 2 + groupTanks.Top + 20;
			btnSelectSelected.Top = buttonsY - 60;
			btnSelectAll.Top = buttonsY - 30;
			btnRemoveAll.Top = buttonsY + 0;
			btnRemoveSelected.Top = buttonsY + 30;
			// Scroll 
			scrollAllColumns.ScrollElementsVisible = dataGridAllColumns.DisplayedRowCount(false);
			scrollSelectedColumns.ScrollElementsVisible = dataGridSelectedColumns.DisplayedRowCount(false);
		}

		private void ColumnList_ResizeEnd(object sender, EventArgs e)
		{
			ResizeNow();
		}

		#endregion

		#region Style

		class StripRenderer : ToolStripProfessionalRenderer
		{
			public StripRenderer()
				: base(new Code.StripLayout())
			{
				this.RoundedEdges = false;
			}

			protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
			{
				base.OnRenderItemText(e);
				e.Item.ForeColor = ColorTheme.ToolWhiteToolStrip;
			}
		}

		private void dataGrid_Paint(object sender, PaintEventArgs e)
		{
			DataGridView dgv = (DataGridView)sender;
			e.Graphics.DrawRectangle(new Pen(ColorTheme.ScrollbarBack), 0, 0, dgv.Width - 1, dgv.Height - 1);
		}

		private void StyleDataGrid(DataGridView dgv)
		{
			dgv.BorderStyle = BorderStyle.FixedSingle;
			dgv.BackgroundColor = ColorTheme.FormBack;
			dgv.GridColor = ColorTheme.GridBorders;
			dgv.EnableHeadersVisualStyles = false;
			dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			dgv.ColumnHeadersHeight = 26;
			dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTheme.GridHeaderBackLight;
			dgv.ColumnHeadersDefaultCellStyle.ForeColor = ColorTheme.ControlFont;
			dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
			dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = ColorTheme.GridSelectedHeaderColor;
			dgv.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			dgv.DefaultCellStyle.BackColor = ColorTheme.FormBack;
			dgv.DefaultCellStyle.ForeColor = ColorTheme.ControlFont;
			dgv.DefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
			dgv.DefaultCellStyle.SelectionBackColor = ColorTheme.GridSelectedCellColor;
		}

		private void toolItem_Checked_paint(object sender, PaintEventArgs e)
		{
			ToolStripMenuItem menu = (ToolStripMenuItem)sender;
			if (menu.Checked)
			{
				if (menu.Image == null)
				{
					// Default checkbox
					e.Graphics.DrawImage(imageListToolStrip.Images[0], 5, 3);
					e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, ColorTheme.ToolGrayCheckBorder)), 4, 2, 17, 17);
					e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, ColorTheme.ToolGrayCheckBorder)), 5, 3, 15, 15);
				}
				else
				{
					// Border around picture
					e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, ColorTheme.ToolGrayCheckBorder)), 3, 1, 19, 19);
					e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, ColorTheme.ToolGrayCheckBorder)), 4, 2, 17, 17);
				}

			}
		}

		#endregion

		#region ColumnList

		private void ShowColumnSetupList()
		{
			string sql = "select columnList.position as '#', columnList.name as 'Name', '' as 'Show', '' as 'Startup', '' as 'System', " +
				"favList.name as 'Fav Tank List', columnList.id, columnList.defaultFavListId, columnList.sysCol, columnList.colDefault " +
				"from columnList left join favList on columnList.defaultFavListId = favList.id " +
				"where columnList.colType=@colType " +
				"order by COALESCE(columnList.position,99), name; ";
			DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
			DataTable dtColumnList = DB.FetchData(sql);
			// Modify datatable by adding values to Show, Default and System columns
			foreach (DataRow row in dtColumnList.Rows)
			{
				var pos = row["#"];
				if (pos != DBNull.Value) row["Show"] = "X";
				int def = Convert.ToInt32(row["colDefault"]);
				if (def == 1) row["Startup"] = "X";
				int sys = Convert.ToInt32(row["sysCol"]);
				if (sys == 1) row["System"] = "X";
				int favList = Convert.ToInt32(row["defaultFavListId"]);
				if (favList == -1)
					row["Fav Tank List"] = "(Use Current)";
				else if (favList == -2)
					row["Fav Tank List"] = "(All Tanks)";
				row.AcceptChanges();
			}
			// Show in grid
			dataGridColumnList.DataSource = dtColumnList;
			// Format datagrid
			dataGridColumnList.Columns["#"].Width = 30;
			dataGridColumnList.Columns["Name"].Width = 120;
			dataGridColumnList.Columns["Show"].Width = 50;
			dataGridColumnList.Columns["Startup"].Width = 50;
			dataGridColumnList.Columns["System"].Width = 50;
			dataGridColumnList.Columns["Show"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridColumnList.Columns["Startup"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridColumnList.Columns["System"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridColumnList.Columns["Fav Tank List"].Width = 120;
			dataGridColumnList.Columns["sysCol"].Visible = false;
			dataGridColumnList.Columns["id"].Visible = false;
			dataGridColumnList.Columns["defaultFavListId"].Visible = false;
			dataGridColumnList.Columns["colDefault"].Visible = false;
			// Set selected item as selected in grid, and modify calculted values
			int rownum = 0;
			foreach (DataGridViewRow row in dataGridColumnList.Rows)
			{
				if (Convert.ToInt32(row.Cells["ID"].Value) == SelectedColListId) rownum = row.Index;
			}
			dataGridColumnList.Rows[rownum].Selected = true;
			SelectColumnList(SelectedColListId);
			// Connect to scrollbar
			scrollColumnList.ScrollElementsTotals = dtColumnList.Rows.Count;
			scrollColumnList.ScrollElementsVisible = dataGridColumnList.DisplayedRowCount(false);
			
		}

		private void dataGridColumnList_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			SelectedColListId = Convert.ToInt32(dataGridColumnList.SelectedRows[0].Cells["id"].Value);
			SelectColumnList(SelectedColListId);
		}

		private void SelectColumnList(int ColumnListId = 0)
		{
			// Set enabled when not sysColumn
			bool sysCol = Convert.ToBoolean(dataGridColumnList.SelectedRows[0].Cells["sysCol"].Value);
			toolColListDelete.Enabled = !sysCol;
			btnColumnListCancel.Enabled = !sysCol;
			btnColumnListSave.Enabled = !sysCol;
			// Move up/down
			//toolSelectedTanks_MoveUp.Enabled = !sysCol;
			//toolSelectedTanks_MoveDown.Enabled = !sysCol;
			// Add/remove buttons
			//btnRemoveAll.Enabled = !sysCol;
			//btnRemoveSelected.Enabled = !sysCol;
			//btnSelectAll.Enabled = !sysCol;
			//btnSelectSelected.Enabled = !sysCol;
			// Toggle show/hide
			bool isHidden = (dataGridColumnList.SelectedRows[0].Cells["#"].Value == DBNull.Value);
			string showButton = "Hide";
			if (isHidden) showButton = "Show";
			toolColListVisible.Text = showButton;
			// Diable Hide if it is the default column
			bool isDefault = (Convert.ToInt32(dataGridColumnList.SelectedRows[0].Cells["colDefault"].Value) == 1);
			toolColListVisible.Enabled = !isDefault;
			// Disable default if already default, or if col is hidden
			toolColListDefault.Enabled = (!isDefault && !isHidden);
			// Get cols for this Col list now
			GetSelectedColumnsFromColumnList();
			
		}

		private void scrollColumnList_MouseDown(object sender, MouseEventArgs e)
		{
			if (dataGridColumnList.RowCount > 0)
				dataGridColumnList.FirstDisplayedScrollingRowIndex = scrollColumnList.ScrollPosition;
		}

		private void scrollColumnList_MouseMove(object sender, MouseEventArgs e)
		{
			if (dataGridColumnList.RowCount > 0)
				dataGridColumnList.FirstDisplayedScrollingRowIndex = scrollColumnList.ScrollPosition;
		}

		private void btnSelectedColumnListCancel_Click(object sender, EventArgs e)
		{
			SelectColumnList();
		}

		private void btnSelectedColumnListSave_Click(object sender, EventArgs e)
		{
			string ColumnSetupListName = dataGridColumnList.SelectedRows[0].Cells[1].Value.ToString();
			string message = "You are about to save the selected columns to column setup list: " + ColumnSetupListName;
			Code.MsgBox.Button answer = MsgBox.Show(message, "Save selected columns to column setup list", MsgBoxType.OKCancel);
			if (answer == MsgBox.Button.OKButton)
			{
				SaveSelectedColumnList();
			}
		}

		private void SaveSelectedColumnList()
		{
			// Save Selected Tank List
			string sql = "delete from columnListSelection where columnListId=@columnListId; "; // Delete all old tanks
			// Loop through datagrid and add all new tanks
			foreach (DataGridViewRow dr in dataGridSelectedColumns.Rows)
			{
				string insertsql = "insert into columnListSelection (columnSelectionId, columnListId, sortorder, colWidth) " +
									"values (@columnSelectionId, @columnListId, @sortorder, @colWidth); ";
				DB.AddWithValue(ref insertsql, "@columnSelectionId", dr.Cells["columnSelectionId"].Value, DB.SqlDataType.Int);
				DB.AddWithValue(ref insertsql, "@sortorder", dr.Cells["#"].Value, DB.SqlDataType.Int);
				DB.AddWithValue(ref insertsql, "@colWidth", dr.Cells["colWidth"].Value, DB.SqlDataType.Int);
				sql += insertsql;
			}
			DB.AddWithValue(ref sql, "@columnListId", SelectedColListId, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql);
			// Refresh Grid
			ShowColumnSetupList();
		}


		#endregion

		#region All Columns

		private bool allTanksColumnSetupDone = false;
		private void ShowAllColumn()
		{
			// Get colGroups to show in toolbar
			string sql = "select colGroup from columnSelection WHERE colType=@colType AND colGroup IS NOT NULL order by position; "; // First get all sorted by position
			DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			// Now get unique values based
			List<string> colGroup = new List<string>();
			foreach (DataRow dr in dt.Rows)
			{
				if (!colGroup.Contains(dr[0].ToString())) colGroup.Add(dr[0].ToString());
			}
			int colGroupRow = -1; // Start on -1, first element will be -1 = All tanks, should be ignored, second = 0 -> first group button -> element [0] from select
			foreach (ToolStripButton button in toolAllColumns.Items)
			{
				if (colGroupRow >= 0 && colGroup.Count > colGroupRow)
				{
					button.Visible = true;
					button.Text = colGroup[colGroupRow];
				}
				else
				{
					if (colGroupRow >= 0) button.Visible = false;
				}
				colGroupRow++;
			}
			// Select All button
			toolAvailableCol_UnselectAll();
			toolAvailableCol_All.Checked = true;
			//toolAvaliableCol_All.Checked = true;
			// Show content now
			FilterAllColumn();
		}

		private void FilterAllColumn()
		{
			string sql = "SELECT name as 'Name', description as 'Description', id, colWidth FROM columnSelection WHERE colType=@colType ";
			// Check filter
			string colGroup = "All";
			foreach (ToolStripButton button in toolAllColumns.Items)
			{
				if (button.Checked) colGroup = button.Text;
			}
			if (colGroup != "All") sql += "AND colGroup=@colGroup ";
			sql += "ORDER BY position; ";
			DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@colGroup", colGroup, DB.SqlDataType.VarChar);
			DataTable dt = DB.FetchData(sql);
			dataGridAllColumns.DataSource = dt;
			if (!allTanksColumnSetupDone)
			{
				allTanksColumnSetupDone = true;
				dataGridAllColumns.Columns["description"].Width = 300;
				dataGridAllColumns.Columns["id"].Visible = false;
				dataGridAllColumns.Columns["colWidth"].Visible = false;
			}
			// Connect to scrollbar
			scrollAllColumns.ScrollElementsTotals = dt.Rows.Count;
			scrollAllColumns.ScrollElementsVisible = dataGridAllColumns.DisplayedRowCount(false);
		}

		private void toolAvaliableCol_All_Click(object sender, EventArgs e)
		{
			toolAvailableCol_UnselectAll();
			toolAvailableCol_All.Checked = true;
			FilterAllColumn();
		}

		private void toolAvaliableCol_Group_Click(object sender, EventArgs e)
		{
			toolAvailableCol_UnselectAll();
			ToolStripButton button = (ToolStripButton)sender;
			button.Checked = true;
			FilterAllColumn();
		}

		private void toolAvailableCol_UnselectAll()
		{
			foreach (ToolStripButton button in toolAllColumns.Items)
			{
				button.Checked = false;
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
			catch (Exception)
			{
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



		#endregion

		#region Selected Columns

		private DataTable dtSelectedColumns = new DataTable();
		private bool selectedColumnSetupDone = false;
		private void GetSelectedColumnsFromColumnList()
		{
			string sql =
				"SELECT columnListSelection.sortorder AS '#', columnSelection.name AS 'Name', description as 'Description', columnSelectionId, columnListId, columnListSelection.colWidth " +
				"FROM   columnListSelection INNER JOIN " +
				"		columnSelection ON columnListSelection.columnSelectionId = columnSelection.id " +
				"		AND columnListSelection.columnListId = @columnListId " +
				"ORDER BY sortorder ";
			DB.AddWithValue(ref sql, "@columnListId", SelectedColListId, DB.SqlDataType.Int);
			dtSelectedColumns = DB.FetchData(sql);
			ShowSelectedColumns();
			if (!selectedColumnSetupDone)
			{
				selectedColumnSetupDone = true;
				dataGridSelectedColumns.Columns["#"].Width = 20;
				dataGridSelectedColumns.Columns["#"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				dataGridSelectedColumns.Columns["Description"].Width = 300;
				dataGridSelectedColumns.Columns["columnSelectionId"].Visible = false;
				dataGridSelectedColumns.Columns["columnListId"].Visible = false;
				dataGridSelectedColumns.Columns["colWidth"].Visible = false;
			}
		}

		private void ShowSelectedColumns()
		{
			// Display datatable containing selected tanks in grid
			dataGridSelectedColumns.DataSource = dtSelectedColumns;
			// Connect to scrollbar
			scrollSelectedColumns.ScrollElementsTotals = dtSelectedColumns.Rows.Count;
			scrollSelectedColumns.ScrollElementsVisible = dataGridSelectedColumns.DisplayedRowCount(false);
			// No sorting for Selected Tanks Data Grid
			foreach (DataGridViewColumn col in dataGridSelectedColumns.Columns)
			{
				col.SortMode = DataGridViewColumnSortMode.NotSortable;
			}
		}

		private void AddSelectedColumn(bool All = false)
		{
			if (All) dataGridAllColumns.SelectAll(); // Select all rows in All Tank List
			int selectedRowCount = dataGridAllColumns.SelectedRows.Count;
			if (selectedRowCount > 0)
			{
				int lastcolumnSelectionId = 0; // Remember last tank ID to set focus at end
				int sortOrder = 1; // Get sort order start pos
				if (dataGridSelectedColumns.RowCount > 0)
				{
					int currentRowCount = dataGridSelectedColumns.SelectedRows.Count;
					if (currentRowCount > 0)
					{
						sortOrder = Convert.ToInt32(dataGridSelectedColumns.SelectedRows[0].Cells["#"].Value) + 1;
						lastcolumnSelectionId = Convert.ToInt32(dataGridSelectedColumns.SelectedRows[0].Cells["columnSelectionId"].Value);
						// Find last selected row if several
						foreach (DataGridViewRow dr in dataGridSelectedColumns.SelectedRows)
						{
							int newSort = Convert.ToInt32(dr.Cells["#"].Value);
							if (newSort > sortOrder)
							{
								sortOrder = newSort + 1;
								lastcolumnSelectionId = Convert.ToInt32(dr.Cells["columnSelectionId"].Value);
							}
						}

					}
				}
				// Move existing elements sort order to make room for new ones
				foreach (DataRow dr in dtSelectedColumns.Rows)
				{
					if (Convert.ToInt32(dr["#"]) >= sortOrder) dr["#"] = Convert.ToInt32(dr["#"]) + selectedRowCount;

				}
				// Insert new elements now
				for (int i = 0; i < dataGridAllColumns.Rows.Count; i++)
				{
					if (dataGridAllColumns.Rows[i].Selected)
					{
						// Check if this tank exist, if not add it
						DataRow[] drFind = dtSelectedColumns.Select("columnSelectionId=" + dataGridAllColumns.Rows[i].Cells["id"].Value);
						if (drFind.Length == 0)
						{
							DataRow dr = dtSelectedColumns.NewRow();
							lastcolumnSelectionId = Convert.ToInt32(dataGridAllColumns.Rows[i].Cells["id"].Value);
							dr["Name"] = dataGridAllColumns.Rows[i].Cells["Name"].Value; ;
							dr["Description"] = dataGridAllColumns.Rows[i].Cells["Description"].Value;
							dr["colWidth"] = dataGridAllColumns.Rows[i].Cells["colWidth"].Value;
							dr["columnSelectionId"] = lastcolumnSelectionId;
							dr["columnListId"] = SelectedColListId;
							dr["#"] = sortOrder;
							dtSelectedColumns.Rows.Add(dr);
							sortOrder++;
						}
					}
				}
				SortSelectedColum("#");
				// Select the last inserted tank
				if (lastcolumnSelectionId != 0)
				{
					int rownum = 0;
					dataGridSelectedColumns.ClearSelection();
					foreach (DataGridViewRow row in dataGridSelectedColumns.Rows)
					{
						if (Convert.ToInt32(row.Cells["columnSelectionId"].Value) == lastcolumnSelectionId) rownum = row.Index;
					}
					dataGridSelectedColumns.Rows[rownum].Selected = true;
				}
			}
		}

		private void MoveSelectedColumn(bool MoveDown) // true = move down, false = move up
		{
			int selectedRowCount = dataGridSelectedColumns.SelectedRows.Count;
			if (selectedRowCount > 0)
			{
				// Remember scroll pos
				int FirstVisibleRowInGrid = dataGridSelectedColumns.FirstDisplayedScrollingRowIndex;
				// Get ready
				List<int> selectedColumns = new List<int>();
				int lastRow = dataGridSelectedColumns.Rows.Count - 1;
				// Move direction up
				int fromRow = 0;     // loop from
				int toRow = lastRow; // loop to
				int move = -1;       // element move direction
				// Move direction down
				if (MoveDown)
				{
					fromRow = lastRow;
					toRow = 0;
					move = 1;
				}
				// Remember closest above/below row to change place with the moved one, -1 = not exists (yet)
				int notSelectedRowIndex = -1;
				// Loop through all rows in grid (oposit direction as moving elements)
				int currentPos = fromRow;
				while (currentPos >= 0 && currentPos <= lastRow)
				{
					DataGridViewRow currentRow = dataGridSelectedColumns.Rows[currentPos]; // Get current row
					if (currentRow.Selected)
					{
						// Selected row - move it
						selectedColumns.Add(Convert.ToInt32(currentRow.Cells["columnSelectionId"].Value)); // remember this tank to set selected area back after moving
						int currentRowSortPos = Convert.ToInt32(dtSelectedColumns.Rows[currentPos]["#"]); // current sort postition 
						// For each tank to be moved the above/below tank must change place with the moved one, if any exist
						if (notSelectedRowIndex != -1)
						{
							dtSelectedColumns.Rows[notSelectedRowIndex]["#"] = Convert.ToInt32(dtSelectedColumns.Rows[notSelectedRowIndex]["#"]) - move;
						}
						// move tank row now
						dtSelectedColumns.Rows[currentPos]["#"] = currentRowSortPos + move;
					}
					else
					{
						// Not selected row
						notSelectedRowIndex = currentPos;
					}
					currentPos -= move; // Move to next	position, in oposite direction as element movment					
				}
				// Save new sorted grid to datatable
				dtSelectedColumns.AcceptChanges();

				// Sort and show
				SortSelectedColum("#");
				// Set selected rows back to correct tanks
				dataGridSelectedColumns.ClearSelection();
				int selectedRowPos = 0;
				bool SelectedRowPosGet = true;
				for (int i = 0; i <= lastRow; i++)
				{
					if (selectedColumns.Contains(Convert.ToInt32(dataGridSelectedColumns.Rows[i].Cells["columnSelectionId"].Value)))
					{
						dataGridSelectedColumns.Rows[i].Selected = true;
						if (SelectedRowPosGet) selectedRowPos = i;
						if (!MoveDown) SelectedRowPosGet = false; // Get first one if move up
					}
				}
				// Return to scroll position
				dataGridSelectedColumns.FirstDisplayedScrollingRowIndex = FirstVisibleRowInGrid;
				// Check if outside
				int topGridRow = dataGridSelectedColumns.FirstDisplayedScrollingRowIndex;
				int bottomGridRow = topGridRow + dataGridSelectedColumns.DisplayedRowCount(false);
				if (selectedRowPos < topGridRow)
					dataGridSelectedColumns.FirstDisplayedScrollingRowIndex = FirstVisibleRowInGrid - 1;
				if (selectedRowPos >= bottomGridRow)
					dataGridSelectedColumns.FirstDisplayedScrollingRowIndex = FirstVisibleRowInGrid + 1;
				MoveSelectedColumnsScrollBar();
			}
		}

		private void RemoveSelectedColumn(bool All = false)
		{
			if (All)
				dtSelectedColumns.Clear(); // Remove all rows in Selected Tank List
			else
			{
				int selectedRowCount = dataGridSelectedColumns.SelectedRows.Count;
				if (selectedRowCount > 0)
				{
					foreach (DataGridViewRow dr in dataGridSelectedColumns.SelectedRows)
					{
						int columnId = Convert.ToInt32(dr.Cells["columnSelectionId"].Value);
						DataRow[] cols = dtSelectedColumns.Select("columnSelectionId = " + columnId.ToString());
						foreach (DataRow col in cols)
						{
							col.Delete();
						}
					}
				}
			}
			dtSelectedColumns.AcceptChanges(); // completely remove deleted rows
			ShowSelectedColumns();
		}

		private void SortSelectedColum(string Column, bool SortASC = true)
		{
			string sortDirection = " ASC";
			if (!SortASC) sortDirection = " DESC";
			dtSelectedColumns.DefaultView.Sort = Column + sortDirection;
			dtSelectedColumns = dtSelectedColumns.DefaultView.ToTable();
			int sortnum = 0;
			foreach (DataRow dr in dtSelectedColumns.Rows)
			{
				sortnum++;
				dr["#"] = sortnum;
			}
			ShowSelectedColumns();
		}

		private void btnRemoveSelected_Click(object sender, EventArgs e)
		{
			bool sysCol = Convert.ToBoolean(dataGridColumnList.SelectedRows[0].Cells["sysCol"].Value);
			if (sysCol)
				ShowSystemColListMessage();
			else
				RemoveSelectedColumn();
		}

		private void btnRemoveAll_Click(object sender, EventArgs e)
		{
			bool sysCol = Convert.ToBoolean(dataGridColumnList.SelectedRows[0].Cells["sysCol"].Value);
			if (sysCol)
				ShowSystemColListMessage();
			else
				RemoveSelectedColumn(true);
		}

		private void btnSelectSelected_Click(object sender, EventArgs e)
		{
			bool sysCol = Convert.ToBoolean(dataGridColumnList.SelectedRows[0].Cells["sysCol"].Value);
			if (sysCol)
				ShowSystemColListMessage();
			else
				AddSelectedColumn();
		}

		private void btnSelectAll_Click(object sender, EventArgs e)
		{
			bool sysCol = Convert.ToBoolean(dataGridColumnList.SelectedRows[0].Cells["sysCol"].Value);
			if (sysCol)
				ShowSystemColListMessage();
			else
				AddSelectedColumn(true);
		}

		private void toolSelectedColumns_MoveUp_Click(object sender, EventArgs e)
		{
			bool sysCol = Convert.ToBoolean(dataGridColumnList.SelectedRows[0].Cells["sysCol"].Value);
			if (sysCol)
				ShowSystemColListMessage();
			else
				MoveSelectedColumn(false);
		}

		private void toolSelectedColumns_MoveDown_Click(object sender, EventArgs e)
		{
			bool sysCol = Convert.ToBoolean(dataGridColumnList.SelectedRows[0].Cells["sysCol"].Value);
			if (sysCol)
				ShowSystemColListMessage();
			else
				MoveSelectedColumn(true);
		}

		private void ShowSystemColListMessage()
		{
			Code.MsgBox.Show("Cannot alter system list, add new list to modify columns.", "Cannot alter system list");
		}

		#endregion

		#region Selected Tanks Scrolling

		private bool scrollingSelectedColumns = false;
		private void scrollSelectedColumns_MouseDown(object sender, MouseEventArgs e)
		{
			if (dataGridSelectedColumns.RowCount > 0)
			{
				scrollingSelectedColumns = true;
				dataGridSelectedColumns.FirstDisplayedScrollingRowIndex = scrollSelectedColumns.ScrollPosition;
			}

		}

		private void scrollSelectedColumns_MouseMove(object sender, MouseEventArgs e)
		{
			if (dataGridSelectedColumns.RowCount > 0 && scrollingSelectedColumns)
			{
				int currentFirstRow = dataGridSelectedColumns.FirstDisplayedScrollingRowIndex;
				dataGridSelectedColumns.FirstDisplayedScrollingRowIndex = scrollSelectedColumns.ScrollPosition;
				if (currentFirstRow != dataGridSelectedColumns.FirstDisplayedScrollingRowIndex) Refresh();
			}

		}

		private void scrollSelectedColumns_MouseUp(object sender, MouseEventArgs e)
		{
			scrollingSelectedColumns = false;
		}

		// Enable mouse wheel scrolling for datagrid
		private void dataGridSelectedColumns_MouseWheel(object sender, MouseEventArgs e)
		{
			try
			{
				// scroll in grid from mouse wheel
				int currentIndex = this.dataGridSelectedColumns.FirstDisplayedScrollingRowIndex;
				int scrollLines = SystemInformation.MouseWheelScrollLines;
				if (e.Delta > 0)
				{
					this.dataGridSelectedColumns.FirstDisplayedScrollingRowIndex = Math.Max(0, currentIndex - scrollLines);
				}
				else if (e.Delta < 0)
				{
					this.dataGridSelectedColumns.FirstDisplayedScrollingRowIndex = currentIndex + scrollLines;
				}
				// move scrollbar
				MoveSelectedColumnsScrollBar();
			}
			catch (Exception)
			{
				// throw;
			}
		}

		private void MoveSelectedColumnsScrollBar()
		{
			scrollSelectedColumns.ScrollPosition = dataGridSelectedColumns.FirstDisplayedScrollingRowIndex;
		}

		private void dataGridSelectedColumns_SelectionChanged(object sender, EventArgs e)
		{
			MoveSelectedColumnsScrollBar();
		}

		#endregion

		private void toolColListDelete_Click(object sender, EventArgs e)
		{
			string ColListName = dataGridColumnList.SelectedRows[0].Cells["Name"].Value.ToString();
			Code.MsgBox.Button answer = MsgBox.Show("Are you sure you want to delete selected column list: " + ColListName,
				"Confirm deletion", MsgBoxType.OKCancel);
			if (answer == MsgBox.Button.OKButton)
			{

				string sql = "delete from columnListSelection where columnListId=@id; delete from columnList where id=@id;";
				DB.AddWithValue(ref sql, "@id", SelectedColListId, DB.SqlDataType.Int);
				DB.ExecuteNonQuery(sql);
				ShowColumnSetupList();
				//SelectColumnList();
			}
		}

		private void toolColListUp_Click(object sender, EventArgs e)
		{
			ColListMoveItem(-1);
		}

		private void toolColListDown_Click(object sender, EventArgs e)
		{
			ColListMoveItem(1);
		}

		private void ColListMoveItem(int move)
		{
			var ColListSelectedListPos = dataGridColumnList.SelectedRows[0].Cells["#"].Value;
			if (ColListSelectedListPos != DBNull.Value)
			{
				// Find item next to
				string sql = "";
				if (move == -1)
				{
					// up, find above
					sql = "select * from columnList where colType=@colType and position is not null and position < @position order by position desc";
				}
				else
				{
					// down, find below
					sql = "select * from columnList where colType=@colType and position is not null and position > @position order by position ";
				}

				DB.AddWithValue(ref sql, "@position", Convert.ToInt32(ColListSelectedListPos), DB.SqlDataType.Int);
				DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
				DataTable dt = DB.FetchData(sql);
				if (dt.Rows.Count > 0)
				{
					int rowNextToPos = Convert.ToInt32(dt.Rows[0]["position"]);
					int rowNextToId = Convert.ToInt32(dt.Rows[0]["id"]);
					sql = "update columnList set position=@rowNextToPos where id=@id; " +
						  "update columnList set position=@position where id=@rowNextToId;";
					DB.AddWithValue(ref sql, "@id", SelectedColListId, DB.SqlDataType.Int);
					DB.AddWithValue(ref sql, "@position",  Convert.ToInt32(ColListSelectedListPos), DB.SqlDataType.Int);
					DB.AddWithValue(ref sql, "@rowNextToId", rowNextToId, DB.SqlDataType.Int);
					DB.AddWithValue(ref sql, "@rowNextToPos", rowNextToPos, DB.SqlDataType.Int);
					DB.ExecuteNonQuery(sql);
				}
				ColListSort();
			}
		}

		private void ColListSort()
		{
			string sql = "select * from columnList where colType=@colType and position is not null order by position;";
			DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				sql = "";
				int pos = 1;
				foreach (DataRow dr in dt.Rows)
				{
					sql += "update columnList set position=@pos where id=@id; ";
					DB.AddWithValue(ref sql, "@id", Convert.ToInt32(dr["id"]), DB.SqlDataType.Int);
					DB.AddWithValue(ref sql, "@pos", pos, DB.SqlDataType.Int);
					pos++;
				}
				DB.ExecuteNonQuery(sql);
				ShowColumnSetupList();
			}
		}

		private void toolColListDefault_Click(object sender, EventArgs e)
		{
			string sql = "update columnList set colDefault=0 where colType=@colType; " +
						 "update columnList set colDefault=1 where colType=@colType and id=@id;";
			DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@id", SelectedColListId, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql);
			ShowColumnSetupList();
		}

		private void toolColListVisible_Click(object sender, EventArgs e)
		{
			string sql = "update columnList set position=99999 where id=@id";
			if (toolColListVisible.Text == "Hide")
				sql = "update columnList set position=NULL where id=@id";
			DB.AddWithValue(ref sql, "@id", SelectedColListId, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql);
			ColListSort();
		}

		public bool isHidden { get; set; }

		private void toolColListAdd_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.ColListNewEdit(0);
			frm.ShowDialog();
			ColListSort();
		}

		private void toolColListModify_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.ColListNewEdit(SelectedColListId);
			frm.ShowDialog();
			ShowColumnSetupList();
		}

		private void toolColListRefresh_Click(object sender, EventArgs e)
		{
			ShowColumnSetupList();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

	}
}
