using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WotDBUpdater.Code;

namespace WotDBUpdater.Forms.File
{
	public partial class FavTanks : Form
	{
		private int _showFavListId;
		public FavTanks(int showFavListId)
		{
			InitializeComponent();
			_showFavListId = showFavListId;
		}

		#region Load and Style
		
		private void FavTanks_Load(object sender, EventArgs e)
		{
			// Style toolbar
			toolAllTanks.Renderer = new StripRenderer();
			toolAllTanks.ShowItemToolTips = false;
			toolSelectedTanks.Renderer = new StripRenderer();
			toolSelectedTanks.ShowItemToolTips = false;
			// Style datagrid
			StyleDataGrid(dataGridFavList);
			StyleDataGrid(dataGridAllTanks);
			StyleDataGrid(dataGridSelectedTanks);
			// Show content
			ShowFavList(_showFavListId);
			ShowAllTanks();
			// Mouse scrolling
			dataGridAllTanks.MouseWheel += new MouseEventHandler(dataGridAllTanks_MouseWheel);
			dataGridSelectedTanks.MouseWheel += new MouseEventHandler(dataGridSelTanks_MouseWheel);
		}

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

		#region Fav List

		private void ShowFavList(int FavListId = 0, string FavListName = "")
		{
			DataTable dt = DB.FetchData("select position as 'Pos', name as 'Name', id as 'ID' from favList order by COALESCE(position,99), name");
			dataGridFavList.DataSource = dt;
			dataGridFavList.Columns[0].Width = 50;
			dataGridFavList.Columns[1].Width = dataGridFavList.Width - 53;
			dataGridFavList.Columns[2].Visible = false;
			bool buttonsEnabled = (dt.Rows.Count > 0);
			btnFavListCancel.Enabled = buttonsEnabled;
			btnFavListSave.Enabled = buttonsEnabled;
			btnFavListDelete.Enabled = buttonsEnabled;
			btnRemoveAll.Enabled = buttonsEnabled;
			btnRemoveSelected.Enabled = buttonsEnabled;
			btnSelectAll.Enabled = buttonsEnabled;
			btnSelectSelected.Enabled = buttonsEnabled;
			// Get favlist from name
			SelectFavList(FavListId);
			// Connect to scrollbar
			scrollFavList.ScrollElementsTotals = dt.Rows.Count;
			scrollFavList.ScrollElementsVisible = dataGridFavList.DisplayedRowCount(false);
		}
		
		private void btnFavListAdd_Click(object sender, EventArgs e)
		{
			string newFavListName = txtFavListName.Text.Trim();
			if (newFavListName.Length > 0)
			{
				// CheckBox if exists
				string sql = "select id from favList where name=@name;";
				DB.AddWithValue(ref sql, "@name", newFavListName, DB.SqlDataType.VarChar);
				DataTable dt = DB.FetchData(sql);
				if (dt.Rows.Count > 0)
				{
					Code.MsgBox.Show("Cannot add new favourite tank list with this name, already in use.", "Cannot create favourite tank list");
				}
				else
				{
					int copySelTanksFromFavListId = -1;
					if (dataGridSelectedTanks.Rows.Count > 0)
					{
						Code.MsgBox.Button answer = Code.MsgBox.Show("Do you want to create a new Favourite Tank List based on the current selected tanks?" +
							Environment.NewLine + Environment.NewLine +
							"Press 'OK' to include selected tanks into the new list." +
							Environment.NewLine + Environment.NewLine +
							"Press 'Cancel' to create an new empty list.", "Create new Favourite Tank List", MsgBoxType.OKCancel);
						if (answer == MsgBox.Button.OKButton) copySelTanksFromFavListId = SelectedFavListId;
					}
					AddFavList(copySelTanksFromFavListId);
				}
			}
			
		}

		private void AddFavList(int CopySelTanksFromFavListId = -1)
		{
			string newFavListName = txtFavListName.Text.Trim();
			string newFavListPos = popupPosition.Text;
			if (newFavListPos == "Not Visible") newFavListPos = "NULL";
			// Change position on existing if already used
			string sql = "select * from favList where position = @newFavListPos";
			DB.AddWithValue(ref sql, "@newFavListPos", newFavListPos, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			sql = "";
			if (dt.Rows.Count == 1)
			{
				// Move existing favlist on this pos or below one step
				sql = "update favlist set position = position + 1 where position >= @newFavListPos; ";
				// Remove positions above 10
				sql += "update favlist set position = NULL where position > 10; ";
			}
			// Add new favlist
			sql += "insert into favList (position, name) values (@newFavListPos, @newFavListName); ";
			// Add parameters
			DB.AddWithValue(ref sql, "@newFavListPos", newFavListPos, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@newFavListName", newFavListName, DB.SqlDataType.VarChar);
			// Execute now
			DB.ExecuteNonQuery(sql);
			// Get ID for new tank list
			sql = "select id from favList where name=@name";
			DB.AddWithValue(ref sql, "@name", newFavListName, DB.SqlDataType.VarChar);
			dt = DB.FetchData(sql);
			SelectedFavListId = Convert.ToInt32(dt.Rows[0]["id"]);
			// Copy favListTanks if selected
			if (CopySelTanksFromFavListId != -1)
			{
				sql = "insert into favListTank (favListId, tankId, sortorder) select @copyToFavListId, tankId, sortorder " +
																			"   from favListTank " +
																			"   where favListId=@copyFromFavListId; ";
				DB.AddWithValue(ref sql, "@copyToFavListId", SelectedFavListId, DB.SqlDataType.Int);
				DB.AddWithValue(ref sql, "@copyFromFavListId", CopySelTanksFromFavListId, DB.SqlDataType.Int);
				DB.ExecuteNonQuery(sql);
			}
			// Refresh Grid
			ShowFavList(SelectedFavListId);
		}

		private void popupPosition_Click(object sender, EventArgs e)
		{
			string posList = "Not Visible,1,2,3,4,5,6,7,8,9,10";
			Code.DropDownGrid.Show(popupPosition, Code.DropDownGrid.DropDownGridType.List, posList);
		}

		private void btnFavListDelete_Click(object sender, EventArgs e)
		{
			Code.MsgBox.Button answer = MsgBox.Show("Are you sure you want to delete favourite tank list: " + txtFavListName.Text,
				"Confirm deletion", MsgBoxType.OKCancel);
			if (answer == MsgBox.Button.OKButton)
			{
				
				string sql = "delete from favListTank where favListId=@id; delete from favList where id=@id;";
				DB.AddWithValue(ref sql, "@id", SelectedFavListId, DB.SqlDataType.Int);
				DB.ExecuteNonQuery(sql);
				ShowFavList();
			}
		}

		private void dataGridFavList_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			SelectFavList();
		}

		private int SelectedFavListId = 0;
		private void SelectFavList(int FavListId = 0)
		{
			int selectedRowCount = dataGridFavList.Rows.GetRowCount(DataGridViewElementStates.Selected);
			if (selectedRowCount > 0)
			{
				// If spesific favList is selected, find it in grid and select it
				if (FavListId > 0)
				{
					int rownum = 0;
					foreach (DataGridViewRow row in dataGridFavList.Rows)
					{
						if (Convert.ToInt32(row.Cells["ID"].Value) == FavListId) rownum = row.Index;
					}
					dataGridFavList.Rows[rownum].Selected = true;
				}
				SelectedFavListId = Convert.ToInt32(dataGridFavList.SelectedRows[0].Cells["id"].Value);
				txtFavListName.Text = dataGridFavList.SelectedRows[0].Cells["Name"].Value.ToString();
				popupPosition.Text = dataGridFavList.SelectedRows[0].Cells["Pos"].Value.ToString();
				if (popupPosition.Text == "") popupPosition.Text = "Not Visible";
				GetSelectedTanksFromFavList(); // Get tanks for this fav list now
			}
			else
			{
				txtFavListName.Text = "";
				popupPosition.Text = "Not Visible";
				dtFavListTank.Clear();
				dataGridSelectedTanks.DataSource = dtFavListTank; // empty list
			}
		}

		private void btnFavListCancel_Click(object sender, EventArgs e)
		{
			SelectFavList();
		}

		private void btnFavListSave_Click(object sender, EventArgs e)
		{
			string oldFavListName = dataGridFavList.SelectedRows[0].Cells[1].Value.ToString();
			string message = "You are about to save favourite tank list: " +  txtFavListName.Text;
			if (txtFavListName.Text != oldFavListName)
				message = "You are about to save and rename favourite tank list: " + oldFavListName + " to new name: " + txtFavListName.Text;
			Code.MsgBox.Button answer = MsgBox.Show(message,"Save existing favourite tank list", MsgBoxType.OKCancel);
			if (answer == MsgBox.Button.OKButton)
			{
				SaveFavList();
			}
		}

		private void SaveFavList()
		{
			string newFavListName = txtFavListName.Text.Trim();
			string newFavListPos = popupPosition.Text;
			if (newFavListPos == "Not Visible") newFavListPos = "NULL";
			// Change position on existing if already used
			string sql = "select * from favList where position = @newFavListPos";
			DB.AddWithValue(ref sql, "@newFavListPos", newFavListPos, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			sql = "";
			if (dt.Rows.Count == 1)
			{
				sql = "update favlist set position = position + 1 where position >= @newFavListPos; ";
				// Remove positions above 10
				sql += "update favlist set position = NULL where position > 10; ";
			}
			// Add new favlist
			sql += "update favList set position=@newFavListPos, name=@newFavListName where id=@id; ";
			// Add parameters
			DB.AddWithValue(ref sql, "@newFavListPos", newFavListPos, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@newFavListName", newFavListName, DB.SqlDataType.VarChar);
			DB.AddWithValue(ref sql, "@id", SelectedFavListId, DB.SqlDataType.Int);
			// Save Fav List
			DB.ExecuteNonQuery(sql);
			// Save Selected Tank List
			sql = "delete from favListTank where favListId=@favListId; "; // Delete all old tanks
			// Loop through datagrid and add all new tanks
			foreach (DataGridViewRow dr in dataGridSelectedTanks.Rows)
			{
				string insertsql = "insert into favListTank (favListId, tankId, sortorder) values (@favListId, @tankId, @sortorder); ";
				DB.AddWithValue(ref insertsql, "@tankId", dr.Cells["ID"].Value, DB.SqlDataType.Int);
				DB.AddWithValue(ref insertsql, "@sortorder", dr.Cells["#"].Value, DB.SqlDataType.Int);
				sql += insertsql;
			}
			DB.AddWithValue(ref sql, "@favListId", SelectedFavListId, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql);

			// Refresh Grid
			ShowFavList(SelectedFavListId);
		}

		private void scrollFavList_MouseDown(object sender, MouseEventArgs e)
		{
			if (dataGridFavList.RowCount > 0)
				dataGridFavList.FirstDisplayedScrollingRowIndex = scrollFavList.ScrollPosition;
		}

		private void scrollFavList_MouseMove(object sender, MouseEventArgs e)
		{
			if (dataGridFavList.RowCount > 0)
				dataGridFavList.FirstDisplayedScrollingRowIndex = scrollFavList.ScrollPosition;
		}

		#endregion

		#region Resize

		private void FavTanks_Resize(object sender, EventArgs e)
		{
			ResizeTankAreaNow();
		}

		private void ResizeTankAreaNow()
		{
			// Resize elements X
			int gridX = (groupTanks.Width - ((toolAllTanks.Left - groupTanks.Left) * 2) - 60) / 2; // total resizeble area
			toolAllTanks.Width = gridX;
			toolSelectedTanks.Width = gridX;
			dataGridAllTanks.Width = gridX - scrollAllTanks.Width;
			dataGridSelectedTanks.Width = gridX - scrollSelectedTanks.Width;
			scrollAllTanks.Left = dataGridAllTanks.Left + dataGridAllTanks.Width;
			// Move middle - right section X
			int rightSectionX = toolAllTanks.Left + toolAllTanks.Width + 60;
			int middleSectionX = toolAllTanks.Left + toolAllTanks.Width + ((60 - btnSelectAll.Width) / 2);
			btnSelectAll.Left = middleSectionX;
			btnSelectSelected.Left = middleSectionX;
			btnRemoveAll.Left = middleSectionX;
			btnRemoveSelected.Left = middleSectionX;
			lblSelectedTanks.Left = rightSectionX;
			toolSelectedTanks.Left = rightSectionX;
			dataGridSelectedTanks.Left = rightSectionX;
			scrollSelectedTanks.Left = dataGridSelectedTanks.Left + dataGridSelectedTanks.Width;
			// Resize elements Y
			int gridY = groupTanks.Height - (toolAllTanks.Top + toolAllTanks.Height - groupTanks.Top) - 15;
			dataGridAllTanks.Height = gridY;
			dataGridSelectedTanks.Height = gridY;
			scrollAllTanks.Height = gridY;
			scrollSelectedTanks.Height = gridY;
			// Move buttons
			int buttonsY = groupTanks.Height / 2 + groupTanks.Top + 20;
			btnSelectSelected.Top = buttonsY - 60;
			btnSelectAll.Top = buttonsY - 30;
			btnRemoveAll.Top = buttonsY + 0;
			btnRemoveSelected.Top = buttonsY + 30;
			// Scroll 
			scrollAllTanks.ScrollElementsVisible = dataGridAllTanks.DisplayedRowCount(false);
			scrollSelectedTanks.ScrollElementsVisible = dataGridSelectedTanks.DisplayedRowCount(false);
		}

		private void FavTanks_ResizeEnd(object sender, EventArgs e)
		{
			ResizeTankAreaNow();
		}

		#endregion
		
		#region Selected Tanks

		private DataTable dtFavListTank = new DataTable();
		private bool selectedTanksColumnSetupDone = false;
		private void GetSelectedTanksFromFavList()
		{
			string sql =
				"SELECT favListTank.sortorder AS '#', tank.tier AS Tier, tank.name AS Tank, tankType.shortname AS Type, country.name AS Nation, tank.id as ID " +
				"FROM   favListTank INNER JOIN " +
				"		tank ON favListTank.tankId = tank.id INNER JOIN " +
				"		country ON tank.countryId = country.id INNER JOIN " +
				"		tankType ON tank.tankTypeId = tankType.id INNER JOIN " +
				"		favList ON favListTank.favListId = favList.id " +
				"WHERE  (favList.name = @favListName) " +
				"ORDER BY sortorder ";
			DB.AddWithValue(ref sql, "@favListName", txtFavListName.Text, DB.SqlDataType.VarChar);
			dtFavListTank = DB.FetchData(sql);
			ShowSelectedTanks();
			if (!selectedTanksColumnSetupDone)
			{
				selectedTanksColumnSetupDone = true;
				dataGridSelectedTanks.Columns["#"].Width = 20;
				dataGridSelectedTanks.Columns["#"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				dataGridSelectedTanks.Columns["Tier"].Width = 30;
				dataGridSelectedTanks.Columns["Tier"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				dataGridSelectedTanks.Columns["Type"].Width = 40;
				dataGridSelectedTanks.Columns["Nation"].Width = 60;
				dataGridSelectedTanks.Columns["ID"].Visible = false;
			}
		}

		private void ShowSelectedTanks()
		{
			// Display datatable containing selected tanks in grid
			dataGridSelectedTanks.DataSource = dtFavListTank;
			// Connect to scrollbar
			scrollSelectedTanks.ScrollElementsTotals = dtFavListTank.Rows.Count;
			scrollSelectedTanks.ScrollElementsVisible = dataGridSelectedTanks.DisplayedRowCount(false);
			// No sorting for Selected Tanks Data Grid
			foreach (DataGridViewColumn col in dataGridSelectedTanks.Columns)
			{
				col.SortMode = DataGridViewColumnSortMode.NotSortable;
			}
		}

		private void AddTankToFavList(bool All = false)
		{
			if (All) dataGridAllTanks.SelectAll(); // Select all rows in All Tank List
			int selectedRowCount = dataGridAllTanks.SelectedRows.Count;
			if (selectedRowCount > 0)
			{
				int lastTankID = 0; // Remember last tank ID to set focus at end
				int sortOrder = 1; // Get sort order start pos
				if (dataGridSelectedTanks.RowCount > 0)
				{
					int currentRowCount = dataGridSelectedTanks.SelectedRows.Count;
					if (currentRowCount > 0) 
					{
						sortOrder = Convert.ToInt32(dataGridSelectedTanks.SelectedRows[0].Cells["#"].Value) + 1;
						lastTankID = Convert.ToInt32(dataGridSelectedTanks.SelectedRows[0].Cells["ID"].Value);
						// Find last selected row if several
						foreach (DataGridViewRow dr in dataGridSelectedTanks.SelectedRows)
						{
							int newSort = Convert.ToInt32(dr.Cells["#"].Value);
							if (newSort > sortOrder)
							{
								sortOrder = newSort + 1;
								lastTankID = Convert.ToInt32(dr.Cells["ID"].Value);
							}
						}
						
					}
				}
				// Move existing elements sort order to make room for new ones
				foreach (DataRow dr in dtFavListTank.Rows)
				{
					if (Convert.ToInt32(dr["#"]) >= sortOrder) dr["#"] = Convert.ToInt32(dr["#"]) + selectedRowCount;

				}
				// Insert new elements now
				for (int i = 0; i < dataGridAllTanks.Rows.Count; i++)
				{
					if (dataGridAllTanks.Rows[i].Selected)
					{
						// Check if this tank exist, if not add it
						DataRow[] drFind = dtFavListTank.Select("ID=" + dataGridAllTanks.Rows[i].Cells["ID"].Value);
						if (drFind.Length == 0)
						{
							DataRow dr = dtFavListTank.NewRow();
							lastTankID = Convert.ToInt32(dataGridAllTanks.Rows[i].Cells["ID"].Value);
							dr["ID"] = lastTankID;
							dr["Tier"] = Convert.ToInt32(dataGridAllTanks.Rows[i].Cells["Tier"].Value);
							dr["Tank"] = dataGridAllTanks.Rows[i].Cells["Tank"].Value;
							dr["Type"] = dataGridAllTanks.Rows[i].Cells["Type"].Value;
							dr["Nation"] = dataGridAllTanks.Rows[i].Cells["Nation"].Value;
							dr["#"] = sortOrder;
							dtFavListTank.Rows.Add(dr);
							sortOrder++;
						}
					}
				}
				SortFavList("#");
				// Select the last inserted tank
				dataGridSelectedTanks.ClearSelection();
				if (lastTankID !=0)
				{
					int rownum = 0;
					foreach (DataGridViewRow row in dataGridSelectedTanks.Rows)
					{
						if (Convert.ToInt32(row.Cells["ID"].Value) == lastTankID) rownum = row.Index;
					}
					dataGridSelectedTanks.Rows[rownum].Selected = true;
				}
			}
		}

		private void MoveSelectedTanks(bool MoveDown) // true = move down, false = move up
		{
			int selectedRowCount = dataGridSelectedTanks.SelectedRows.Count;
			if (selectedRowCount > 0)
			{
				// Remember scroll pos
				int FirstVisibleRowInGrid = dataGridSelectedTanks.FirstDisplayedScrollingRowIndex;
				// Get ready
				List<int> selectedTanks = new List<int>();
				int lastRow = dataGridSelectedTanks.Rows.Count - 1;
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
					DataGridViewRow currentRow = dataGridSelectedTanks.Rows[currentPos]; // Get current row
					if (currentRow.Selected)
					{
						// Selected row - move it
						selectedTanks.Add(Convert.ToInt32(currentRow.Cells["ID"].Value)); // remember this tank to set selected area back after moving
						int currentRowSortPos = Convert.ToInt32(dtFavListTank.Rows[currentPos]["#"]); // current sort postition 
						// For each tank to be moved the above/below tank must change place with the moved one, if any exist
						if (notSelectedRowIndex != -1)
						{
							dtFavListTank.Rows[notSelectedRowIndex]["#"] = Convert.ToInt32(dtFavListTank.Rows[notSelectedRowIndex]["#"]) - move;
						}
						// move tank row now
						dtFavListTank.Rows[currentPos]["#"] = currentRowSortPos + move;
					}
					else
					{
						// Not selected row
						notSelectedRowIndex = currentPos;
					}
					currentPos -= move; // Move to next	position, in oposite direction as element movment					
				}
				// Save new sorted grid to datatable
				dtFavListTank.AcceptChanges();
				
				// Sort and show
				SortFavList("#");
				// Set selected rows back to correct tanks
				dataGridSelectedTanks.ClearSelection();
				int selectedRowPos = 0;
				bool SelectedRowPosGet = true;
				for (int i = 0; i <= lastRow; i++)
				{
					if (selectedTanks.Contains(Convert.ToInt32(dataGridSelectedTanks.Rows[i].Cells["ID"].Value)))
					{
						dataGridSelectedTanks.Rows[i].Selected = true;
						if (SelectedRowPosGet) selectedRowPos = i;
						if (!MoveDown) SelectedRowPosGet = false; // Get first one if move up
					}
				}
				// Return to scroll position
				dataGridSelectedTanks.FirstDisplayedScrollingRowIndex = FirstVisibleRowInGrid;
				// Check if outside
				int topGridRow = dataGridSelectedTanks.FirstDisplayedScrollingRowIndex;
				int bottomGridRow = topGridRow + dataGridSelectedTanks.DisplayedRowCount(false);
				if (selectedRowPos < topGridRow)
					dataGridSelectedTanks.FirstDisplayedScrollingRowIndex = FirstVisibleRowInGrid - 1;
				if (selectedRowPos >= bottomGridRow)
					dataGridSelectedTanks.FirstDisplayedScrollingRowIndex = FirstVisibleRowInGrid + 1;
				MoveSelTanksScrollBar();
			}
		}

		private void RemoveTankFromFavList(bool All = false)
		{
			if (All)
				dtFavListTank.Clear(); // Remove all rows in Selected Tank List
			else
			{
				int selectedRowCount = dataGridSelectedTanks.SelectedRows.Count;
				if (selectedRowCount > 0)
				{
					foreach (DataGridViewRow dr in dataGridSelectedTanks.SelectedRows)
					{
						int tankID = Convert.ToInt32(dr.Cells["ID"].Value);
						DataRow[] tanks = dtFavListTank.Select("ID = " + tankID.ToString());
						foreach (DataRow tank in tanks)
						{
							tank.Delete();
						}
					}
				}
			}
			dtFavListTank.AcceptChanges(); // completely remove deleted rows
			ShowSelectedTanks();
		}

		private void SortFavList(string Column, bool SortASC = true)
		{
			string sortDirection = " ASC";
			if (!SortASC) sortDirection = " DESC";
			dtFavListTank.DefaultView.Sort = Column + sortDirection;
			dtFavListTank = dtFavListTank.DefaultView.ToTable();
			int sortnum = 0;
			foreach (DataRow dr in dtFavListTank.Rows)
			{
				sortnum++;
				dr["#"] = sortnum;
			}
			ShowSelectedTanks();
		}

		private bool sortNationASC = false;
		private void toolSelectedTanks_SortNation_Click(object sender, EventArgs e)
		{
			sortNationASC = !sortNationASC;
			SortFavList("Nation", sortNationASC);
		}

		private bool sortTypeASC = false;
		private void toolSelectedTanks_SortType_Click(object sender, EventArgs e)
		{
			sortTypeASC = !sortTypeASC;
			SortFavList("Type", sortTypeASC);
		}

		private bool sortTierASC = false;
		private void toolSelectedTanks_SortTier_Click(object sender, EventArgs e)
		{
			sortTierASC = !sortTierASC;
			SortFavList("Tier", sortTierASC);
		}

		private void btnRemoveSelected_Click(object sender, EventArgs e)
		{
			RemoveTankFromFavList();
		}

		private void btnRemoveAll_Click(object sender, EventArgs e)
		{
			RemoveTankFromFavList(true);
		}

		private void btnSelectSelected_Click(object sender, EventArgs e)
		{
			AddTankToFavList();
		}

		private void btnSelectAll_Click(object sender, EventArgs e)
		{
			AddTankToFavList(true);
		}

		private void toolSelectedTanks_MoveUp_Click(object sender, EventArgs e)
		{
			MoveSelectedTanks(false);
		}

		private void toolSelectedTanks_MoveDown_Click(object sender, EventArgs e)
		{
			MoveSelectedTanks(true);
		}

		#endregion

		#region Selected Tanks Scrolling

		private bool scrollingSelTanks = false;
		private void scrollSelTanks_MouseDown(object sender, MouseEventArgs e)
		{
			if (dataGridSelectedTanks.RowCount > 0)
			{
				scrollingSelTanks = true;
				dataGridSelectedTanks.FirstDisplayedScrollingRowIndex = scrollSelectedTanks.ScrollPosition;
			}

		}

		private void scrollSelTanks_MouseMove(object sender, MouseEventArgs e)
		{
			if (dataGridSelectedTanks.RowCount > 0 && scrollingSelTanks)
			{
				int currentFirstRow = dataGridSelectedTanks.FirstDisplayedScrollingRowIndex;
				dataGridSelectedTanks.FirstDisplayedScrollingRowIndex = scrollSelectedTanks.ScrollPosition;
				if (currentFirstRow != dataGridSelectedTanks.FirstDisplayedScrollingRowIndex) Refresh();
			}

		}

		private void scrollSelTanks_MouseUp(object sender, MouseEventArgs e)
		{
			scrollingSelTanks = false;
		}

		// Enable mouse wheel scrolling for datagrid
		private void dataGridSelTanks_MouseWheel(object sender, MouseEventArgs e)
		{
			try
			{
				// scroll in grid from mouse wheel
				int currentIndex = this.dataGridSelectedTanks.FirstDisplayedScrollingRowIndex;
				int scrollLines = SystemInformation.MouseWheelScrollLines;
				if (e.Delta > 0)
				{
					this.dataGridSelectedTanks.FirstDisplayedScrollingRowIndex = Math.Max(0, currentIndex - scrollLines);
				}
				else if (e.Delta < 0)
				{
					this.dataGridSelectedTanks.FirstDisplayedScrollingRowIndex = currentIndex + scrollLines;
				}
				// move scrollbar
				MoveSelTanksScrollBar();
			}
			catch (Exception)
			{
				// throw;
			}
		}

		private void MoveSelTanksScrollBar()
		{
			scrollSelectedTanks.ScrollPosition = dataGridSelectedTanks.FirstDisplayedScrollingRowIndex;
		}

		private void dataGridSelTanks_SelectionChanged(object sender, EventArgs e)
		{
			MoveSelTanksScrollBar();
		}

		#endregion

		#region All Tanks

		private bool allTanksColumnSetupDone = false;
		private void ShowAllTanks()
		{
			string sql =
				"SELECT   tank.tier AS Tier, tank.name AS Tank, tankType.shortname AS Type, country.name AS Nation, playerTank.lastBattleTime AS 'Last Battle', tank.id AS ID " +
				"FROM     country INNER JOIN " +
				"		tank ON country.id = tank.countryId INNER JOIN " +
				"		tankType ON tank.tankTypeId = tankType.id LEFT OUTER JOIN " +
				"		playerTank ON tank.id = playerTank.tankId AND playerTank.playerId=@playerid";
			DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
			// Check filter
			string nationFilter = "";
			foreach (ToolStripMenuItem menu in toolAllTanks_Nation.DropDown.Items)
				if (menu.Checked) nationFilter += menu.Name.Substring(menu.Name.Length - 1, 1) + ",";
			string tierFilter = "";
			foreach (ToolStripMenuItem menu in toolAllTanks_Tier.DropDown.Items)
				if (menu.Checked) tierFilter += menu.Name.Substring(menu.Name.Length - 2, 2) + ",";
			string typeFilter = "";
			foreach (ToolStripMenuItem menu in toolAllTanks_Type.DropDown.Items)
				if (menu.Checked) typeFilter += menu.Name.Substring(menu.Name.Length - 1, 1) + ",";
			string filter = "";
			if (nationFilter.Length > 0)
				filter = AddAndToWhere(filter,"tank.countryId IN (" + nationFilter.Substring(0,nationFilter.Length - 1) + ")");
			if (tierFilter.Length > 0)
				filter = AddAndToWhere(filter,"tank.tier IN (" + tierFilter.Substring(0,tierFilter.Length - 1) + ")"); 
			if (typeFilter.Length > 0)
				filter = AddAndToWhere(filter,"tank.tankTypeId IN (" + typeFilter.Substring(0,typeFilter.Length - 1) + ")");
			if (toolAllTanks_Used.Checked)
				filter = AddAndToWhere(filter,"playerTank.lastBattleTime is not null");
			if (filter.Length > 0)
				filter = " WHERE " + filter;
			DataTable dt = DB.FetchData(sql + filter);
			dataGridAllTanks.DataSource = dt;
			if (!allTanksColumnSetupDone)
			{
				allTanksColumnSetupDone = true;
				dataGridAllTanks.Columns["Tier"].Width = 30;
				dataGridAllTanks.Columns["Tier"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				dataGridAllTanks.Columns["Type"].Width = 40;
				dataGridAllTanks.Columns["Nation"].Width = 60;
				dataGridAllTanks.Columns["ID"].Visible = false;
			}
			// Connect to scrollbar
			scrollAllTanks.ScrollElementsTotals = dt.Rows.Count;
			scrollAllTanks.ScrollElementsVisible = dataGridAllTanks.DisplayedRowCount(false);
		}

		private string AddAndToWhere(string WherePart, string AddNewAndPart)
		{
			string s = "";
			if (WherePart.Length > 0) s = WherePart + " AND ";
			return s + AddNewAndPart;
		}

		private void toolAllTanks_Nation_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menu = (ToolStripMenuItem)sender;
			menu.Checked = !menu.Checked;
			ShowAllTanks();
		}

		private void toolAllTanks_Type_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menu = (ToolStripMenuItem)sender;
			menu.Checked = !menu.Checked;
			ShowAllTanks();
		}

		private void toolAllTanks_Tier_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menu = (ToolStripMenuItem)sender;
			menu.Checked = !menu.Checked;
			ShowAllTanks();
		}

		private void toolAllTanks_Used_Click(object sender, EventArgs e)
		{
			ToolStripButton menu = (ToolStripButton)sender;
			menu.Checked = !menu.Checked; 
			ShowAllTanks();
		}

		private void toolAllTanks_All_Click(object sender, EventArgs e)
		{
			foreach (ToolStripMenuItem menu in toolAllTanks_Nation.DropDown.Items)
				menu.Checked = false;
			foreach (ToolStripMenuItem menu in toolAllTanks_Tier.DropDown.Items)
				menu.Checked = false;
			foreach (ToolStripMenuItem menu in toolAllTanks_Type.DropDown.Items)
				menu.Checked = false;
			toolAllTanks_Used.Checked = false;
			ShowAllTanks();
		}

		

		#endregion

		#region All Tanks Scrolling
		
		private bool scrollingAllTanks = false;
		private void scrollAllTanks_MouseDown(object sender, MouseEventArgs e)
		{
			if (dataGridAllTanks.RowCount > 0)
			{
				scrollingAllTanks = true;
				dataGridAllTanks.FirstDisplayedScrollingRowIndex = scrollAllTanks.ScrollPosition;
			}
				
		}

		private void scrollAllTanks_MouseMove(object sender, MouseEventArgs e)
		{
			if (dataGridAllTanks.RowCount > 0 && scrollingAllTanks)
			{
				int currentFirstRow = dataGridAllTanks.FirstDisplayedScrollingRowIndex;
				dataGridAllTanks.FirstDisplayedScrollingRowIndex = scrollAllTanks.ScrollPosition;
				if(currentFirstRow != dataGridAllTanks.FirstDisplayedScrollingRowIndex) Refresh();
			}
				
		}

		private void scrollAllTanks_MouseUp(object sender, MouseEventArgs e)
		{
			scrollingAllTanks = false;
		}

		// Enable mouse wheel scrolling for datagrid
		private void dataGridAllTanks_MouseWheel(object sender, MouseEventArgs e)
		{
			try
			{
				// scroll in grid from mouse wheel
				int currentIndex = this.dataGridAllTanks.FirstDisplayedScrollingRowIndex;
				int scrollLines = SystemInformation.MouseWheelScrollLines;
				if (e.Delta > 0)
				{
					this.dataGridAllTanks.FirstDisplayedScrollingRowIndex = Math.Max(0, currentIndex - scrollLines);
				}
				else if (e.Delta < 0)
				{
					this.dataGridAllTanks.FirstDisplayedScrollingRowIndex = currentIndex + scrollLines;
				}
				// move scrollbar
				MoveAllTanksScrollBar();
			}
			catch (Exception)
			{
				// throw;
			}
		}

		private void MoveAllTanksScrollBar()
		{
			scrollAllTanks.ScrollPosition = dataGridAllTanks.FirstDisplayedScrollingRowIndex;
		}

		private void dataGridAllTanks_SelectionChanged(object sender, EventArgs e)
		{
			MoveAllTanksScrollBar();
		}

		#endregion


	}
}
