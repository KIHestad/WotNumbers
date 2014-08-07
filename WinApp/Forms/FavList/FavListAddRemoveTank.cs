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
	public partial class FavListAddRemoveTank : Form
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
			if (_add) dataGridFavList.ClearSelection();
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
			if (MainSettings.GetCurrentGridFilter().FavListShow == GridFilter.FavListShowType.FavList && !_add)
			{
				int rownum = 0;
				foreach (DataGridViewRow row in dataGridFavList.Rows)
				{
					if (Convert.ToInt32(row.Cells["id"].Value) == MainSettings.GetCurrentGridFilter().FavListId)
						rownum = row.Index;
				}
			}
			// Connect to scrollbar
			//scrollFavList.ScrollElementsTotals = dt.Rows.Count;
			//scrollFavList.ScrollElementsVisible = dataGridFavList.DisplayedRowCount(false);
		}

		private void btnSave_Click(object sender, EventArgs e)
		{

		}
	}
}
