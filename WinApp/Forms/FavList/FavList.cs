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
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class FavList2 : Form
	{
		private int SelectedFavListId;
		public FavList2(int showFavListId)
		{
			InitializeComponent();
			SelectedFavListId = showFavListId;
		}

		#region Load and Style
		
		private void FavList_Load(object sender, EventArgs e)
		{
			// Make sure borderless form do not cover task bar when maximized
			Screen screen = Screen.FromControl(this);
			this.MaximumSize = screen.WorkingArea.Size;
			// Style toolbar
			toolAllTanks.Renderer = new StripRenderer();
			toolAllTanks.ShowItemToolTips = false;
			toolSelectedTanks.Renderer = new StripRenderer();
			toolSelectedTanks.ShowItemToolTips = false;
			toolColList.Renderer = new StripRenderer();
			toolColList.ShowItemToolTips = false;

			// Style datagrid
			StyleDataGrid(dataGridFavList);
			StyleDataGrid(dataGridAllTanks);
			StyleDataGrid(dataGridSelectedTanks);
			// Show content
			ShowFavList();
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

		private void ShowFavList(bool selectNewestFavList = false)
		{
			DataTable dt = DB.FetchData("select position as '#', name as 'Name', '' as 'Show', id from favList order by COALESCE(position,99), name");
			// Modify datatable by adding values to Show
			foreach (DataRow row in dt.Rows)
			{
				var pos = row["#"];
				if (pos != DBNull.Value) row["Show"] = "X";
				row.AcceptChanges();
			}
			// Show in grid
			dataGridFavList.DataSource = dt;
			// Format
			dataGridFavList.Columns["#"].Width = 50;
			dataGridFavList.Columns["Name"].Width = 150;
			dataGridFavList.Columns["Show"].Width = 50;
			dataGridFavList.Columns["id"].Visible = false;
			// Enable buttons
			bool buttonsEnabled = (dt.Rows.Count > 0);
			btnFavListCancel.Enabled = buttonsEnabled;
			btnFavListSave.Enabled = buttonsEnabled;
			btnRemoveAll.Enabled = buttonsEnabled;
			btnRemoveSelected.Enabled = buttonsEnabled;
			btnSelectAll.Enabled = buttonsEnabled;
			btnSelectSelected.Enabled = buttonsEnabled;
			// Set selected item as selected in grid, and modify calculted values
			int rownum = 0;
			int highestId = 0;
			foreach (DataGridViewRow row in dataGridFavList.Rows)
			{
				if (selectNewestFavList)
				{
					if (Convert.ToInt32(row.Cells["ID"].Value) > highestId)
					{
						highestId = Convert.ToInt32(row.Cells["ID"].Value);
						SelectedFavListId = highestId;
						rownum = row.Index;
					}
				}
				else
				{
					if (Convert.ToInt32(row.Cells["id"].Value) == SelectedFavListId)
						rownum = row.Index;
				}
			}
			if (dataGridFavList.Rows.Count > 0)
				dataGridFavList.Rows[rownum].Selected = true;
			SelectFavList();
			// Connect to scrollbar
			scrollFavList.ScrollElementsTotals = dt.Rows.Count;
			scrollFavList.ScrollElementsVisible = dataGridFavList.DisplayedRowCount(false);
		}
		
				
		private void dataGridFavList_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			SelectedFavListId = Convert.ToInt32(dataGridFavList.SelectedRows[0].Cells["id"].Value);
			SelectFavList();
		}

		private void SelectFavList(int FavListId = 0)
		{
			// Toggle show/hide
			if (dataGridFavList.SelectedRows.Count > 0)
			{
				bool isHidden = (dataGridFavList.SelectedRows[0].Cells["#"].Value == DBNull.Value);
				string showButton = "Hide";
				if (isHidden) showButton = "Show";
				toolFavListVisible.Text = showButton;
				// Get tanks for this fav list now
				GetSelectedTanksFromFavList();
			}
		}

		private void btnFavListCancel_Click(object sender, EventArgs e)
		{
			SelectFavList();
		}

		private void btnFavListSave_Click(object sender, EventArgs e)
		{
			string FavListName = dataGridFavList.SelectedRows[0].Cells["Name"].Value.ToString();
			string message = "You are about to save selected tanks to favourite tank list: " + FavListName;
			Code.MsgBox.Button answer = MsgBox.Show(message, "Save selected tanks to favourite tank list", MsgBoxType.OKCancel, this);
			if (answer == MsgBox.Button.OKButton)
			{
				SaveFavList();
			}
		}


		private void SaveFavList()
		{
			// Save Selected Tank List
			string sql = "delete from favListTank where favListId=@favListId; "; // Delete all old tanks
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
			ShowFavList();
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
				"WHERE  (favList.id = @id) " +
				"ORDER BY sortorder ";
			DB.AddWithValue(ref sql, "@id", SelectedFavListId, DB.SqlDataType.Int);
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
			// Update scrollbar
			MoveSelTanksScrollBar();
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
					dataGridSelectedTanks.ClearSelection();
					foreach (DataGridViewRow row in dataGridSelectedTanks.Rows)
					{
						if (Convert.ToInt32(row.Cells["ID"].Value) == lastTankID) rownum = row.Index;
					}
					dataGridSelectedTanks.Rows[rownum].Selected = true;
					// scroll down to show it
					if (rownum > 3)
						dataGridSelectedTanks.FirstDisplayedScrollingRowIndex = rownum - 3;
					else
						dataGridSelectedTanks.FirstDisplayedScrollingRowIndex = 0;
					// Acjust scrollbar
					MoveSelTanksScrollBar();
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

		private void toolFavListUp_Click(object sender, EventArgs e)
		{
			FavListMoveItem(-1);
		}

		private void toolFavListDown_Click(object sender, EventArgs e)
		{
			FavListMoveItem(1);
		}

		private void FavListMoveItem(int move)
		{
			var FavListSelectedListPos = dataGridFavList.SelectedRows[0].Cells["#"].Value;
			if (FavListSelectedListPos != DBNull.Value)
			{
				// Find item next to
				string sql = "";
				if (move == -1)
				{
					// up, find above
					sql = "select * from favList where position is not null and position < @position order by position desc";
				}
				else
				{
					// down, find below
					sql = "select * from favList where position is not null and position > @position order by position ";
				}

				DB.AddWithValue(ref sql, "@position", Convert.ToInt32(FavListSelectedListPos), DB.SqlDataType.Int);
				DataTable dt = DB.FetchData(sql);
				if (dt.Rows.Count > 0)
				{
					int rowNextToPos = Convert.ToInt32(dt.Rows[0]["position"]);
					int rowNextToId = Convert.ToInt32(dt.Rows[0]["id"]);
					sql = "update favList set position=@rowNextToPos where id=@id; " +
						  "update favList set position=@position where id=@rowNextToId;";
					DB.AddWithValue(ref sql, "@id", SelectedFavListId, DB.SqlDataType.Int);
					DB.AddWithValue(ref sql, "@position", Convert.ToInt32(FavListSelectedListPos), DB.SqlDataType.Int);
					DB.AddWithValue(ref sql, "@rowNextToId", rowNextToId, DB.SqlDataType.Int);
					DB.AddWithValue(ref sql, "@rowNextToPos", rowNextToPos, DB.SqlDataType.Int);
					DB.ExecuteNonQuery(sql);
				}
				FavListSort();
			}
		}

		private void FavListSort(bool selectNewestFavList = false)
		{
			string sql = "select * from favList where position is not null order by position;";
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				sql = "";
				int pos = 1;
				foreach (DataRow dr in dt.Rows)
				{
					sql += "update favList set position=@pos where id=@id; ";
					DB.AddWithValue(ref sql, "@id", Convert.ToInt32(dr["id"]), DB.SqlDataType.Int);
					DB.AddWithValue(ref sql, "@pos", pos, DB.SqlDataType.Int);
					pos++;
				}
				DB.ExecuteNonQuery(sql);
				ShowFavList(selectNewestFavList);
			}
		}

		private void toolFavListVisible_Click(object sender, EventArgs e)
		{
			string sql = "update favList set position=99999 where id=@id";
			if (toolFavListVisible.Text == "Hide")
				sql = "update favList set position=NULL where id=@id";
			DB.AddWithValue(ref sql, "@id", SelectedFavListId, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql);
			FavListSort();
		}

		private void toolFavListAdd_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.FavListNewEdit(0);
			frm.ShowDialog();
			FavListSort(true);
		}

		private void toolFavListModify_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.FavListNewEdit(SelectedFavListId);
			frm.ShowDialog();
			FavListSort();
		}

		private void toolFavListDelete_Click(object sender, EventArgs e)
		{
			string FavListName = dataGridFavList.SelectedRows[0].Cells["Name"].Value.ToString();
			Code.MsgBox.Button answer = MsgBox.Show("Are you sure you want to delete favourite tank list: " + FavListName,
				"Confirm deletion", MsgBoxType.OKCancel, this);
			if (answer == MsgBox.Button.OKButton)
			{

				string sql = "delete from favListTank where favListId=@id; delete from favList where id=@id;";
				DB.AddWithValue(ref sql, "@id", SelectedFavListId, DB.SqlDataType.Int);
				DB.ExecuteNonQuery(sql);
				SelectedFavListId = 0;
				if (dataGridFavList.RowCount > 0)
					SelectedFavListId = Convert.ToInt32(dataGridFavList.Rows[0].Cells["id"].Value);
				ShowFavList();
			}
		}

		private void toolFavListRefresh_Click(object sender, EventArgs e)
		{
			ShowFavList();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void FavList2_LocationChanged(object sender, EventArgs e)
		{
			Screen screen = Screen.FromControl(this);
			this.MaximumSize = screen.WorkingArea.Size;

		}

		private void FavList2_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				GridFilter.Settings gf = MainSettings.GetCurrentGridFilter();
				gf.FavListId = SelectedFavListId;
				gf.FavListName = dataGridFavList.SelectedRows[0].Cells["Name"].Value.ToString();
				gf.FavListShow = GridFilter.FavListShowType.FavList;
				MainSettings.UpdateCurrentGridFilter(gf);
			}
			catch (Exception)
			{
				GridFilter.Settings gf = MainSettings.GetCurrentGridFilter();
				gf.FavListId = 0;
				gf.FavListName = "";
				gf.FavListShow = GridFilter.FavListShowType.AllTanks;
				MainSettings.UpdateCurrentGridFilter(gf);
			}
			
		}

	}
}
