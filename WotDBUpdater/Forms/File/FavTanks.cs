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
	public partial class FavTanks : Form
	{
		public FavTanks()
		{
			InitializeComponent();
		}

		#region Load and Style
		
		private void FavTanks_Load(object sender, EventArgs e)
		{
			// Style toolbar
			toolAllTanks.Renderer = new StripRenderer();
			toolAllTanks.BackColor = ColorTheme.FormBackTitle;
			toolAllTanks.ShowItemToolTips = false;
			toolSelectedTanks.Renderer = new StripRenderer();
			toolSelectedTanks.BackColor = ColorTheme.FormBackTitle;
			toolSelectedTanks.ShowItemToolTips = false;
			// Style datagrid
			StyleDataGrid(dataGridFavList);
			StyleDataGrid(dataGridAllTanks);
			StyleDataGrid(dataGridSelectedTanks);
			// Show content
			ShowFavList();
			ShowAllTanks();
			// Mouse scrolling
			dataGridAllTanks.MouseWheel += new MouseEventHandler(dataGridAllTanks_MouseWheel);
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

		#endregion

		#region Fav List

		private void ShowFavList()
		{
			DataTable dt = DB.FetchData("select position as 'Pos', name as 'Name' from favList order by COALESCE(position,99), name");
			dataGridFavList.DataSource = dt;
			dataGridFavList.Columns[0].Width = 50;
			dataGridFavList.Columns[1].Width = dataGridFavList.Width - 53;
			bool buttonsEnabled = (dt.Rows.Count > 0);
			btnFavListCancel.Enabled = buttonsEnabled;
			btnFavListSave.Enabled = buttonsEnabled;
			btnFavListDelete.Enabled = buttonsEnabled;
			
			SelectFavList();
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
				DataTable dt = DB.FetchData("select id from favList where name='" + newFavListName + "'");
				if (dt.Rows.Count > 0)
				{
					Code.MsgBox.Show("Cannot add new favourite tank list with this name, already in use.", "Cannot create favourite tank list");
				}
				else
				{
					AddFavList();
				}
			}
		}

		private void AddFavList()
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
			// Refresh Grid
			ShowFavList();
		}

		private void popupPosition_Click(object sender, EventArgs e)
		{
			string posList = "Not Visible,1,2,3,4,5,6,7,8,9,10";
			string newval = Code.PopupGrid.Show("Select Position", Code.PopupGrid.PopupGridType.List, posList);
			if (newval != "") popupPosition.Text = newval;
		}

		private void btnFavListDelete_Click(object sender, EventArgs e)
		{
			Code.MsgBox.Button answer = MsgBox.Show("Are you sure you want to delete favourite tank list: " + txtFavListName.Text,
				"Confirm deletion", MsgBoxType.OKCancel);
			if (answer == MsgBox.Button.OKButton)
			{
				string sql = "delete from favList where name='" + txtFavListName.Text + "'";
				DB.ExecuteNonQuery(sql);
				ShowFavList();
			}
		}

		private void dataGridFavList_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			SelectFavList();
		}

		private void SelectFavList()
		{
			int selectedRowCount = dataGridFavList.Rows.GetRowCount(DataGridViewElementStates.Selected);
			if (selectedRowCount > 0)
			{
				txtFavListName.Text = dataGridFavList.SelectedRows[0].Cells[1].Value.ToString();
				popupPosition.Text = dataGridFavList.SelectedRows[0].Cells[0].Value.ToString();
				if (popupPosition.Text == "") popupPosition.Text = "Not Visible";
			}
			else
			{
				txtFavListName.Text = "";
				popupPosition.Text = "Not Visible";
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
			string oldFavListName = dataGridFavList.SelectedRows[0].Cells[1].Value.ToString();
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
			sql += "update favList set position=@newFavListPos, name=@newFavListName where name =@oldFavListName; ";
			// Add parameters
			DB.AddWithValue(ref sql, "@newFavListPos", newFavListPos, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@newFavListName", newFavListName, DB.SqlDataType.VarChar);
			DB.AddWithValue(ref sql, "@oldFavListName", oldFavListName, DB.SqlDataType.VarChar);
			// Execute now
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
			btnSelectAll.Top = buttonsY - 60;
			btnSelectSelected.Top = buttonsY - 30;
			btnRemoveSelected.Top = buttonsY;
			btnRemoveAll.Top = buttonsY + 30;
			// Scroll 
			scrollAllTanks.ScrollElementsVisible = dataGridAllTanks.DisplayedRowCount(false);
		}

		private void FavTanks_ResizeEnd(object sender, EventArgs e)
		{
			ResizeTankAreaNow();
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

		#region All Tanks

		private bool allTanksColumnSetupDone = false;
		private void ShowAllTanks()
		{
			string sql =
				"SELECT   tank.tier AS Tier, tank.name AS Tank, tankType.shortname AS Type, country.name AS Nation, playerTank.lastBattleTime AS 'Last Battle' " +
				"FROM     country INNER JOIN " +
				"         tank ON country.id = tank.countryId INNER JOIN " +
				"         tankType ON tank.tankTypeId = tankType.id LEFT OUTER JOIN " +
				"         player INNER JOIN " +
				"         playerTank ON player.id = playerTank.playerId AND player.id = @playerid ON tank.id = playerTank.tankId ";
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
				dataGridAllTanks.Columns["Tier"].Width = 40;
				dataGridAllTanks.Columns["Type"].Width = 40;
				dataGridAllTanks.Columns["Nation"].Width = 60;

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

	}
}
