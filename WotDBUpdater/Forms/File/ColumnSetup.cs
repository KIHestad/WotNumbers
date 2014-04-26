using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WotDBUpdater.Code;

namespace WotDBUpdater.Forms.File
{
	public partial class ColumnSetup : Form
	{
		
		#region Init
		
		public enum ColumnSetupType
		{
			TankView = 1,
			BattleView = 2
		}
		
		private string colTypeText = "";
		int colType = 0;
		public ColumnSetup(ColumnSetupType colSelectedSetupType)
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

		private void ColumnSetup_Load(object sender, EventArgs e)
		{
			// Style toolbar
			toolAllColumns.Renderer = new StripRenderer();
			toolAllColumns.BackColor = ColorTheme.FormBackTitle;
			toolAllColumns.ShowItemToolTips = false;
			toolSelectedColumns.Renderer = new StripRenderer();
			toolSelectedColumns.BackColor = ColorTheme.FormBackTitle;
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
			string sql = "select position as 'Pos', name as 'Name', id as 'ID', colDefault, sysCol from columnList where colType=@colType order by position; ";
			DB.AddWithValue(ref sql, "@colType", colType, DB.SqlDataType.Int);
			dtColumnList = DB.FetchData(sql);
			dataGridColumnList.DataSource = dtColumnList;
			dataGridColumnList.Columns[0].Width = 50;
			dataGridColumnList.Columns[1].Width = dataGridColumnList.Width - 53;
			dataGridColumnList.Columns[2].Visible = false;
			dataGridColumnList.Columns[3].Visible = false;
			dataGridColumnList.Columns[4].Visible = false;
			bool buttonsEnabled = (dtColumnList.Rows.Count > 0);
			btnColumnListCancel.Enabled = false;
			btnColumnListSave.Enabled = false;
			btnColumnListDelete.Enabled = false;
			btnRemoveAll.Enabled = false;
			btnRemoveSelected.Enabled = false;
			btnSelectAll.Enabled = false;
			btnSelectSelected.Enabled = false;
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
				string defaultText = "Not used as default colum setup for " + colTypeText;
				Color defaultTextColor = ColorTheme.ControlFont;
				if (Convert.ToBoolean(dataGridColumnList.SelectedRows[0].Cells["colDefault"].Value))
				{
					defaultText = "Used as default colum setup for " + colTypeText;
					defaultTextColor = Color.ForestGreen;
				}
				lblDefaultColumnSetup.Text = defaultText;
				lblDefaultColumnSetup.ForeColor = defaultTextColor;
				bool sysCol = Convert.ToBoolean(dataGridColumnList.SelectedRows[0].Cells["sysCol"].Value) ;
				btnColumnListDelete.Enabled = !sysCol;
				btnColumnListCancel.Enabled = !sysCol;
				btnColumnListSave.Enabled = !sysCol;
				btnRemoveAll.Enabled = !sysCol;
				btnRemoveSelected.Enabled = !sysCol;
				btnSelectAll.Enabled = !sysCol;
				btnSelectSelected.Enabled = !sysCol;
				if (popupPosition.Text == "") popupPosition.Text = "Not Visible";
				popupPosition.Text = dataGridColumnList.SelectedRows[0].Cells["Pos"].Value.ToString();
				//GetSelectedTanksFromFavList(); // Get tanks for this fav list now
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
			string newval = Code.PopupGrid.Show("Select Position", Code.PopupGrid.PopupGridType.List, posList);
			if (newval != "") popupPosition.Text = newval;
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
			//toolAvaliableCol_All.Checked = true;
			// Show content now
			FilterAllColumn();
		}

		private void FilterAllColumn()
		{
			string sql = "SELECT position as '#', name as 'Name', description as 'Description', id FROM columnSelection WHERE colType=@colType ";
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
				dataGridAllColumns.Columns["#"].Width = 20;
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
			//toolAvaliableCol_All.Checked = true;
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





	}
}
