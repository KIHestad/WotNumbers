using System;
using System.Data;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class FavListAddRemoveTank : FormCloseOnEsc
    {
		private bool _add = true;
		private int _tankId = 0;
		
        public FavListAddRemoveTank(int tankId, bool add = true)
		{
			InitializeComponent();
			_add = add;
			_tankId = tankId;
		}

		private void FavListAddRemoveTank_Load(object sender, EventArgs e)
		{
			if (_add)
			{
				FavListAddRemoveTankTheme.Text = "Add To Favourite Tank List";
				btnSave.Text = "Add";
			}
			else
			{
				FavListAddRemoveTankTheme.Text = "Remove From Favourite Tank List";
				btnSave.Text = "Remove";
			}
			// Style datagrid
			GridHelper.StyleDataGrid(dataGridFavList);

            // Populate grid with data
            ShowFavList();

		}

		private void ShowFavList()
		{
			string sql = "select position as '#', name as 'Name', '' as 'Show', id from favList ";
			if (_add)
				sql += "where id not in (select favListId from favListTank where tankId=@tankId) ";
			else
				sql += "where id in (select favListId from favListTank where tankId=@tankId) ";
			sql += " order by COALESCE(position,99), name";
			DB.AddWithValue(ref sql, "@tankId", _tankId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
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
			dataGridFavList.Columns["#"].Width = 30;
			dataGridFavList.Columns["Name"].Width = 135;
			dataGridFavList.Columns["Show"].Width = 35;
			dataGridFavList.Columns["Show"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridFavList.Columns["id"].Visible = false;
			// Set current favList as selected in grid if remove
			int selectRowIndex = -1;
			if (MainSettings.GetCurrentGridFilter().FavListShow == GridFilter.FavListShowType.FavList && !_add)
			{
				foreach (DataGridViewRow row in dataGridFavList.Rows)
				{
					if (Convert.ToInt32(row.Cells["id"].Value) == MainSettings.GetCurrentGridFilter().FavListId)
						selectRowIndex = row.Index;
				}
			}
			else if (_add)
			{
				// if only one favList, select it
				if (dataGridFavList.Rows.Count == 1)
					selectRowIndex = 0;
				// Use last selected
				else if (FavListHelper.lastAddFavListFromPopup != 0)
				{
					foreach (DataGridViewRow row in dataGridFavList.Rows)
					{
						if (Convert.ToInt32(row.Cells["id"].Value) == FavListHelper.lastAddFavListFromPopup)
							selectRowIndex = row.Index;
					}
				}
			}
			dataGridFavList.ClearSelection();
			if (selectRowIndex >= 0)
				dataGridFavList.Rows[selectRowIndex].Selected = true;
			// Connect to scrollbar
			scrollY.ScrollElementsTotals = dt.Rows.Count;
			scrollY.ScrollElementsVisible = dataGridFavList.DisplayedRowCount(false);
		}

		private async void btnSave_Click(object sender, EventArgs e)
		{
			FavListHelper.refreshGridAfterAddRemove = false;
			string sql = ""; 
			// Loop through datagrid and add tank to favLists
			foreach (DataGridViewRow dr in dataGridFavList.SelectedRows)
			{
				if (_add)
				{
					sql += "insert into favListTank (favListId, tankId, sortorder) values (@favListId, @tankId, 9998); ";
				}
				else
				{
					sql += "delete from favlistTank where favListId=@favListId and tankId=@tankId; ";
					FavListHelper.refreshGridAfterAddRemove = true;
				}
				DB.AddWithValue(ref sql, "@favListId", dr.Cells["id"].Value, DB.SqlDataType.Int);
				FavListHelper.lastAddFavListFromPopup = Convert.ToInt32(dr.Cells["id"].Value);
			}
			DB.AddWithValue(ref sql, "@tankId", _tankId, DB.SqlDataType.Int);
            await DB.ExecuteNonQueryAsync(sql, Config.Settings.showDBErrors, true);
			//Sort
			if (_add)
				foreach (DataGridViewRow dr in dataGridFavList.SelectedRows)
				{
					if (_add)
                        await FavListHelper.TankSort(Convert.ToInt32(dr.Cells["id"].Value));
				}
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void scrollY_MouseDown(object sender, MouseEventArgs e)
		{
			if (dataGridFavList.RowCount > 0)
				dataGridFavList.FirstDisplayedScrollingRowIndex = scrollY.ScrollPosition;
		}

		private void scrollY_MouseMove(object sender, MouseEventArgs e)
		{
			if (dataGridFavList.RowCount > 0)
				dataGridFavList.FirstDisplayedScrollingRowIndex = scrollY.ScrollPosition;
		}
	}
}
