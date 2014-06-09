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
	public partial class ColSetup : Form
	{
		
		#region Init
		
		public enum ColumnSetupType
		{
			TankView = 1,
			BattleView = 2
		}
		
		int colType = 0;
		public ColSetup(ColumnSetupType colSelectedSetupType)
		{
			InitializeComponent();
			if (colSelectedSetupType == ColumnSetupType.TankView)
			{
				popupColumnListType.Text = "Tank View";
				colType = 1;
			}

			else if (colSelectedSetupType == ColumnSetupType.BattleView)
			{
				popupColumnListType.Text = "Battle View";
				colType = 2;
			}
		}

		private string favList = "Use Current,All Tanks";
		private void ColumnSetup_Load(object sender, EventArgs e)
		{
			// Get favList
			string sql = "select name from favList order by COALESCE(position,99), name";
			DataTable dt = DB.FetchData(sql);
			foreach (DataRow row in dt.Rows)
			{
				favList += "," + row["name"].ToString();
			}
			// Style toolbar
			toolAllColumns.Renderer = new StripRenderer();
			toolAllColumns.ShowItemToolTips = false;
			toolSelectedColumns.Renderer = new StripRenderer();
			
			toolSelectedColumns.ShowItemToolTips = false;
			// Style datagrid
			StyleDataGrid(dataGridColumnList);
			StyleDataGrid(dataGridAllColumns);
			StyleDataGrid(dataGridSelectedColumns);
			// Show content
			ShowColumnSetupList();
			ShowAllColumn();
			// Mouse scrolling
			dataGridAllColumns.MouseWheel += new MouseEventHandler(dataGridAllColumns_MouseWheel);
			dataGridSelectedColumns.MouseWheel += new MouseEventHandler(dataGridSelectedColumns_MouseWheel);
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

		private void popupColumnSetupType_Click(object sender, EventArgs e)
		{
			Code.DropDownGrid.Show(popupColumnListType, Code.DropDownGrid.DropDownGridType.List, "Tank View,Battle View");
		}

		private void popupColumnListType_TextChanged(object sender, EventArgs e)
			{
				if (popupColumnListType.Text == "Tank View")
					colType = 1;
				else if (popupColumnListType.Text == "Battle View")
					colType = 2;
				ShowColumnSetupList();
				ShowAllColumn();
			}

		private DataTable dtColumnList = new DataTable();
		private void ShowColumnSetupList(int ColumListId = 0)
		{
			string sql = "select position as 'Pos', name as 'Name', id as 'ID', colDefault, sysCol, defaultFavListId from columnList where colType=@colType order by COALESCE(position,99), name; ";
			DB.AddWithValue(ref sql, "@colType", colType, DB.SqlDataType.Int);
			dtColumnList = DB.FetchData(sql);
			dataGridColumnList.DataSource = dtColumnList;
			dataGridColumnList.Columns[0].Width = 50;
			dataGridColumnList.Columns[1].Width = dataGridColumnList.Width - 53;
			dataGridColumnList.Columns[2].Visible = false;
			dataGridColumnList.Columns[3].Visible = false;
			dataGridColumnList.Columns[4].Visible = false;
			dataGridColumnList.Columns[5].Visible = false;
			bool buttonsEnabled = (dtColumnList.Rows.Count > 0);
			btnColumnListCancel.Enabled = false;
			btnColumnListSave.Enabled = false;
			btnColumnListDelete.Enabled = false;
			btnRemoveAll.Enabled = false;
			btnRemoveSelected.Enabled = false;
			btnSelectAll.Enabled = false;
			btnSelectSelected.Enabled = false;
			toolSelectedTanks_MoveUp.Enabled = false;
			toolSelectedTanks_MoveDown.Enabled = false;
			SelectColumnList(ColumListId);
			// Connect to scrollbar
			scrollColumnList.ScrollElementsTotals = dtColumnList.Rows.Count;
			scrollColumnList.ScrollElementsVisible = dataGridColumnList.DisplayedRowCount(false);
		}

		private void dataGridColumnList_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			SelectColumnList();
		}

		private int SelectedColumnListId = 0;
		private void SelectColumnList(int ColumnListId = 0)
		{
			int selectedRowCount = dataGridColumnList.Rows.GetRowCount(DataGridViewElementStates.Selected);
			if (selectedRowCount > 0)
			{
				// If spesific favList is selected, find it in grid and select it
				if (ColumnListId > 0)
				{
					int rownum = 0;
					foreach (DataGridViewRow row in dataGridColumnList.Rows)
					{
						if (Convert.ToInt32(row.Cells["ID"].Value) == ColumnListId) rownum = row.Index;
					}
					dataGridColumnList.Rows[rownum].Selected = true;
				}
				SelectedColumnListId = Convert.ToInt32(dataGridColumnList.SelectedRows[0].Cells["id"].Value);
				txtColumnListName.Text = dataGridColumnList.SelectedRows[0].Cells["Name"].Value.ToString();
				// Set if default column list
				string defaultText = "Not used as default colum setup for " + popupColumnListType.Text;
				btnSetAsDefaultColumnList.Enabled = true;
				Color defaultTextColor = ColorTheme.ControlFont;
				if (Convert.ToBoolean(dataGridColumnList.SelectedRows[0].Cells["colDefault"].Value))
				{
					defaultText = "Used as default colum setup for " + popupColumnListType.Text;
					defaultTextColor = Color.ForestGreen;
					btnSetAsDefaultColumnList.Enabled = false;
				}
				lblDefaultColumnSetup.Text = defaultText;
				lblDefaultColumnSetup.ForeColor = defaultTextColor;
				// Find default fav list
				defaultTankFilterSave = false; // avoid saving on changed value
				int selectedDefaultFavList = Convert.ToInt32(dataGridColumnList.SelectedRows[0].Cells["defaultFavListId"].Value);
				if (selectedDefaultFavList == -1)
					ddDefaultTankFilter.Text = "Use Current";
				else if (selectedDefaultFavList == -2)
					ddDefaultTankFilter.Text = "All Tanks";
				else 
				{
					string sql = "select name from favList where id=@id";
					DB.AddWithValue (ref sql, "@id", selectedDefaultFavList, DB.SqlDataType.Int);
					DataTable dt = DB.FetchData(sql);
					if (dt.Rows.Count > 0)
						ddDefaultTankFilter.Text = dt.Rows[0]["name"].ToString();
				}
				// Other values
				bool sysCol = Convert.ToBoolean(dataGridColumnList.SelectedRows[0].Cells["sysCol"].Value) ;
				btnColumnListDelete.Enabled = !sysCol;
				btnColumnListCancel.Enabled = !sysCol;
				btnColumnListSave.Enabled = !sysCol;
				btnRemoveAll.Enabled = !sysCol;
				btnRemoveSelected.Enabled = !sysCol;
				btnSelectAll.Enabled = !sysCol;
				btnSelectSelected.Enabled = !sysCol;
				toolSelectedTanks_MoveUp.Enabled = !sysCol;
				toolSelectedTanks_MoveDown.Enabled = !sysCol;
			
				popupPosition.Text = dataGridColumnList.SelectedRows[0].Cells["Pos"].Value.ToString();
				if (popupPosition.Text == "") popupPosition.Text = "Not Visible";
				GetSelectedColumnsFromColumnList(); // Get tanks for this fav list now
			}
			else
			{
				txtColumnListName.Text = "";
				popupPosition.Text = "Not Visible";
				dtColumnList.Clear();
				dataGridColumnList.DataSource = dtColumnList; // empty list
			}
		}

		private void popupPosition_Click(object sender, EventArgs e)
		{
			string posList = "Not Visible,4,5,6,7,8,9,10,11,12,13";
			Code.DropDownGrid.Show(popupPosition, Code.DropDownGrid.DropDownGridType.List, posList);
		}

		private void btnColumnListAdd_Click(object sender, EventArgs e)
		{
			string newColListName = txtColumnListName.Text.Trim();
			if (newColListName.Length > 0)
			{
				// CheckBox if exists
				if (Convert.ToInt32(popupPosition.Text) < 4)
				{
					Code.MsgBox.Show("Cannot add new Column Setup List with position 1, 2 or 3 - these are reserved." +
						Environment.NewLine + Environment.NewLine + "Select another position.", "Cannot create Column Setup List ");
				}
				else
				{
					string sql = "select id from columnList where name=@name;";
					DB.AddWithValue(ref sql, "@name", newColListName, DB.SqlDataType.VarChar);
					DataTable dt = DB.FetchData(sql);
					if (dt.Rows.Count > 0)
					{
						Code.MsgBox.Show("Cannot add new Column Setup List with this name, already in use.", "Cannot create Column Setup List ");
					}
					else
					{
						int copySelTanksFromFavListId = -1;
						if (dataGridSelectedColumns.Rows.Count > 0)
						{
							Code.MsgBox.Button answer = Code.MsgBox.Show("Do you want to create a new Column Setup List  based on the current selected columns?" +
								Environment.NewLine + Environment.NewLine +
								"Press 'OK' to include selected columns into the new list." +
								Environment.NewLine + Environment.NewLine +
								"Press 'Cancel' to create an new empty list.", "Create new Column Setup List ", MsgBoxType.OKCancel);
							if (answer == MsgBox.Button.OKButton) copySelTanksFromFavListId = SelectedColumnListId;
						}
						AddColumnList(copySelTanksFromFavListId);
					}
				}
			}
		}

		private void AddColumnList(int CopySelColumnsFromColumnListId = -1)
		{
			string newColumnListName = txtColumnListName.Text.Trim();
			string newColumnListPos = popupPosition.Text;
			if (newColumnListPos == "Not Visible") newColumnListPos = "NULL";
			// Change position on existing if already used
			string sql = "select * from columnList where position = @newColumnListPos";
			DB.AddWithValue(ref sql, "@newColumnListPos", newColumnListPos, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			sql = "";
			if (dt.Rows.Count == 1)
			{
				// Move existing favlist on this pos or below one step
				sql = "update columnList set position = position + 1 where position >= @newColumnListPos; ";
				// Remove positions above 10
				sql += "update columnList set position = NULL where position > 10; ";
			}
			// Add new favlist
			sql += "insert into columnList (colType, position, name) values (@colType, @newColumnListPos, @newFavListName); ";
			// Add parameters
			DB.AddWithValue(ref sql, "@colType", colType, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@newColumnListPos", newColumnListPos, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@newFavListName", newColumnListName, DB.SqlDataType.VarChar);
			// Execute now
			DB.ExecuteNonQuery(sql);
			// Get ID for new tank list
			sql = "select id from columnList where name=@name";
			DB.AddWithValue(ref sql, "@name", newColumnListName, DB.SqlDataType.VarChar);
			dt = DB.FetchData(sql);
			SelectedColumnListId = Convert.ToInt32(dt.Rows[0]["id"]);
			// Copy favListTanks if selected
			if (CopySelColumnsFromColumnListId != -1)
			{
				sql = "insert into columnListSelection (columnSelectionId, columnListId, sortorder) select columnSelectionId, @copyToColumnListId, sortorder " +
																			"   from columnListSelection " +
																			"   where ColumnListId=@copyFromColumnListId; ";
				DB.AddWithValue(ref sql, "@copyToColumnListId", SelectedColumnListId, DB.SqlDataType.Int);
				DB.AddWithValue(ref sql, "@copyFromColumnListId", CopySelColumnsFromColumnListId, DB.SqlDataType.Int);
				DB.ExecuteNonQuery(sql);
			}
			// Refresh Grid
			ShowColumnSetupList(SelectedColumnListId);
		}

		private void btnSetAsDefaultColumnList_Click(object sender, EventArgs e)
		{
			// todo: check for unsaved changes first
			string sql = "update ColumnList set colDefault=0 where colType=@colType; " + 
						 "update ColumnList set colDefault=1 where colType=@colType and id=@id;";
			DB.AddWithValue(ref sql, "@colType", colType, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@id", SelectedColumnListId, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql);
			ShowColumnSetupList(SelectedColumnListId);
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

		private void btnSelectedColumnListDelete_Click(object sender, EventArgs e)
		{
			Code.MsgBox.Button answer = MsgBox.Show("Are you sure you want to delete selected column list: " + txtColumnListName.Text,
				"Confirm deletion", MsgBoxType.OKCancel);
			if (answer == MsgBox.Button.OKButton)
			{

				string sql = "delete from columnListSelection where columnListId=@id; delete from columnList where id=@id;";
				DB.AddWithValue(ref sql, "@id", SelectedColumnListId, DB.SqlDataType.Int);
				DB.ExecuteNonQuery(sql);
				ShowColumnSetupList();
				//SelectColumnList();
			}
		}

		private void btnSelectedColumnListCancel_Click(object sender, EventArgs e)
		{
			SelectColumnList();
		}

		private void btnSelectedColumnListSave_Click(object sender, EventArgs e)
		{
			string oldColumnSetupListName = dataGridColumnList.SelectedRows[0].Cells[1].Value.ToString();
			string message = "You are about to save column setup list: " + txtColumnListName.Text;
			if (txtColumnListName.Text != oldColumnSetupListName)
				message = "You are about to save and rename column setup list: " + oldColumnSetupListName + " to new name: " + txtColumnListName.Text;
			Code.MsgBox.Button answer = MsgBox.Show(message, "Save existing column setup list", MsgBoxType.OKCancel);
			if (answer == MsgBox.Button.OKButton)
			{
				SaveSelectedColumnList();
			}
		}

		private void SaveSelectedColumnList()
		{
			string newColumnSelectedListName = txtColumnListName.Text.Trim();
			string newColumnSelectedListPos = popupPosition.Text;
			if (newColumnSelectedListPos == "Not Visible") newColumnSelectedListPos = "NULL";
			// Change position on existing if already used
			string sql = "select * from columnList where position = @position";
			DB.AddWithValue(ref sql, "@position", newColumnSelectedListPos, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			sql = "";
			if (dt.Rows.Count == 1)
			{
				sql = "update columnList set position = position + 1 where position >= @position; ";
				// Remove positions above 10
				sql += "update columnList set position = NULL where position > 10; ";
			}
			sql += "update columnList set position=@position, name=@name where id=@id; ";
			// Add parameters
			DB.AddWithValue(ref sql, "@position", newColumnSelectedListPos, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@name", newColumnSelectedListName, DB.SqlDataType.VarChar);
			DB.AddWithValue(ref sql, "@id", SelectedColumnListId, DB.SqlDataType.Int);
			// Save Fav List
			DB.ExecuteNonQuery(sql);
			// Save Selected Tank List
			sql = "delete from columnListSelection where columnListId=@columnListId; "; // Delete all old tanks
			// Loop through datagrid and add all new tanks
			foreach (DataGridViewRow dr in dataGridSelectedColumns.Rows)
			{
				string insertsql = "insert into columnListSelection (columnSelectionId, columnListId, sortorder) values (@columnSelectionId, @columnListId, @sortorder); ";
				DB.AddWithValue(ref insertsql, "@columnSelectionId", dr.Cells["columnSelectionId"].Value, DB.SqlDataType.Int);
				DB.AddWithValue(ref insertsql, "@sortorder", dr.Cells["#"].Value, DB.SqlDataType.Int);
				sql += insertsql;
			}
			DB.AddWithValue(ref sql, "@columnListId", SelectedColumnListId, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql);

			// Refresh Grid
			ShowColumnSetupList(SelectedColumnListId);
		}


		#endregion

		#region All Columns

		private bool allTanksColumnSetupDone = false;
		private void ShowAllColumn()
		{
			// Get colGroups to show in toolbar
			string sql = "select colGroup from columnSelection WHERE colType=@colType AND colGroup IS NOT NULL order by position; "; // First get all sorted by position
			DB.AddWithValue(ref sql, "@colType", colType, DB.SqlDataType.Int);
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
			string sql = "SELECT name as 'Name', description as 'Description', id FROM columnSelection WHERE colType=@colType ";
			// Check filter
			string colGroup = "All";
			foreach (ToolStripButton button in toolAllColumns.Items)
			{
				if (button.Checked) colGroup=button.Text;
			}
			if (colGroup != "All") sql += "AND colGroup=@colGroup ";
			sql += "ORDER BY position; ";
			DB.AddWithValue(ref sql, "@colType", colType, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@colGroup", colGroup, DB.SqlDataType.VarChar);
			DataTable dt = DB.FetchData(sql);
			dataGridAllColumns.DataSource = dt;
			if (!allTanksColumnSetupDone)
			{
				allTanksColumnSetupDone = true;
				dataGridAllColumns.Columns["description"].Width = 300;
				dataGridAllColumns.Columns["id"].Visible = false;
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
				"SELECT columnListSelection.sortorder AS '#', columnSelection.name AS 'Name', description as 'Description', columnSelectionId, columnListId " +
				"FROM   columnListSelection INNER JOIN " +
				"		columnSelection ON columnListSelection.columnSelectionId = columnSelection.id " +
				"		AND columnListSelection.columnListId = @columnListId " +
				"ORDER BY sortorder ";
			DB.AddWithValue(ref sql, "@columnListId", SelectedColumnListId, DB.SqlDataType.Int);
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
							dr["Description"] = dataGridAllColumns.Rows[i].Cells["Description"].Value; ;
							dr["columnSelectionId"] = lastcolumnSelectionId;
							dr["columnListId"] = SelectedColumnListId;
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
			RemoveSelectedColumn();
		}

		private void btnRemoveAll_Click(object sender, EventArgs e)
		{
			RemoveSelectedColumn(true);
		}

		private void btnSelectSelected_Click(object sender, EventArgs e)
		{
			AddSelectedColumn();
		}

		private void btnSelectAll_Click(object sender, EventArgs e)
		{
			AddSelectedColumn(true);
		}

		private void toolSelectedColumns_MoveUp_Click(object sender, EventArgs e)
		{
			MoveSelectedColumn(false);
		}

		private void toolSelectedColumns_MoveDown_Click(object sender, EventArgs e)
		{
			MoveSelectedColumn(true);
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

		bool defaultTankFilterSave = false;
		private void ddDefaultTankFilter_Click(object sender, EventArgs e)
		{
			defaultTankFilterSave = true;
			string selectedTankFilter = Code.DropDownGrid.Show(ddDefaultTankFilter, Code.DropDownGrid.DropDownGridType.List, favList);
		}

		private void ddDefaultTankFilter_TextChanged(object sender, EventArgs e)
		{
			if (defaultTankFilterSave)
			{
				// Update favlist
				string selectedfavListName = ddDefaultTankFilter.Text;
				int defaultFavListId = -1; // Use current
				if (selectedfavListName == "Use Current")
					defaultFavListId = -1;
				else if (selectedfavListName == "All Tanks")
					defaultFavListId = -2;
				else
				{
					// Find favListId
					string sql = "select id from favList where name=@name";
					DB.AddWithValue(ref sql, "@name", selectedfavListName, DB.SqlDataType.VarChar);
					DataTable dtFavList = DB.FetchData(sql);
					if (dtFavList.Rows.Count > 0)
						defaultFavListId = Convert.ToInt32(dtFavList.Rows[0][0]);
				}
				// Save now
				string updateSql = "update columnList set defaultFavListId=@defaultFavListId where id=@id";
				DB.AddWithValue(ref updateSql, "@defaultFavListId", defaultFavListId, DB.SqlDataType.Int);
				DB.AddWithValue(ref updateSql, "@id", SelectedColumnListId, DB.SqlDataType.Int);
				DB.ExecuteNonQuery(updateSql);
				defaultTankFilterSave = false;
				// Also update grid
				dataGridColumnList.SelectedRows[0].Cells["defaultFavListId"].Value = defaultFavListId;
			}
		}

	}
}
