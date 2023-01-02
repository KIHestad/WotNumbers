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
	public partial class FavListNewEdit : FormCloseOnEsc
    {
		string _copyFromDD = "";
		int _favListId = 0;
		string _prevName = "";
		int _tankId = 0;
		public FavListNewEdit(int selectedFavListId = 0, string newFavListName = "", int addTankId = 0)
		{
			InitializeComponent();
			if (selectedFavListId == 0)
			{
				FavListNewEditTheme.Text = "Add new Favourite Tank List";
				txtName.Text = newFavListName;
			}
			else
			{
				FavListNewEditTheme.Text = "Modify Favourite Tank List";
				ddCopyFrom.Visible = false;
				lblCopyFrom.Visible = false;
			}
			_favListId = selectedFavListId;
			_tankId = addTankId;
		}

		private async void FavListNewEdit_Load(object sender, EventArgs e)
		{
			if (_favListId > 0)
			{
				// Show selected favList
				string sql = "select * from favList where id=@id; ";
				DB.AddWithValue(ref sql, "@id", _favListId, DB.SqlDataType.Int);
                DataTable dt = await DB.FetchData(sql);
                DataRow dr = dt.Rows[0];
				txtName.Text = dr["name"].ToString();
				_prevName = dr["name"].ToString();
			}
			else 
			{
				// Populate Copy From DD
				_copyFromDD = "(None)";
				string copyFromSql = "select * from favList order by COALESCE(position,99), name";
				DataTable dtcopyFrom = await DB.FetchData(copyFromSql);
				if (dtcopyFrom.Rows.Count > 0)
				{
					foreach (DataRow dr2 in dtcopyFrom.Rows)
					{
						_copyFromDD += "," + dr2["name"].ToString();
					}
				}
			}
		}
		private async void btnSave_Click(object sender, EventArgs e)
		{
			string newName = txtName.Text.Trim();
			if (newName.Length == 0)
			{
				Code.MsgBox.Show("Plese select a name for your Favourite Tank List", "Name missing", this);
			}
			else
			{
				// Check if name not already exists
				string sql = "select id from favList where name=@name; ";
				DB.AddWithValue(ref sql, "@name", newName, DB.SqlDataType.VarChar);
				DataTable dtExists = await DB.FetchData(sql);
				if (newName != _prevName && dtExists.Rows.Count > 0)
				{
					Code.MsgBox.Show("This name is already in use, select a different name for your Favourite Tank List", "Name already in use", this);
				}
				else
				{
					if (_favListId > 0)
						sql = "update favList set name=@name where id=@id; ";
					else
						sql = "insert into favList (name, position) values (@name, 99999); ";
					// Avoid saving if name is not changed
					if (_favListId > 0 && newName == _prevName) return;
					// Save now
					DB.AddWithValue(ref sql, "@name", newName, DB.SqlDataType.VarChar);
					DB.AddWithValue(ref sql, "@id", _favListId, DB.SqlDataType.Int);
                    await DB.ExecuteNonQuery(sql);
					// Add tanks if new colList and seleced colList in copy to DD
					if (_favListId == 0 && ddCopyFrom.Text != "(None)")
					{
						// Get the id for copy from
						sql = "select id from favList where name=@name; ";
						DB.AddWithValue(ref sql, "@name", ddCopyFrom.Text, DB.SqlDataType.VarChar);
						DataTable dtCopyFrom = await DB.FetchData(sql);
						int copyFromId = Convert.ToInt32(dtCopyFrom.Rows[0]["id"]);
						// Get the id for copy to
						sql = "select id from favList where name=@name; ";
						DB.AddWithValue(ref sql, "@name", newName, DB.SqlDataType.VarChar);
						DataTable dtCopyTo = await DB.FetchData(sql);
						int copyToId = Convert.ToInt32(dtCopyTo.Rows[0]["id"]);
						// Copy now
						sql = "insert into favListTank (favListId, tankId, sortorder) select @copyToFavListId, tankId, sortorder " +
																					"   from favListTank " +
																					"   where favListId=@copyFromFavListId; ";
						DB.AddWithValue(ref sql, "@copyToFavListId", copyToId, DB.SqlDataType.Int);
						DB.AddWithValue(ref sql, "@copyFromFavListId", copyFromId, DB.SqlDataType.Int);
                        await DB.ExecuteNonQuery(sql);
					}
					// Set as default for right click menu
					// find last favlist
					sql = "select max(id) from favList; ";
					DataTable dtAddTank = await DB.FetchData(sql);
					int favListId = Convert.ToInt32(dtAddTank.Rows[0][0]);
					FavListHelper.lastAddFavListFromPopup = favListId;
					// Add tank to new favList if selected
					if (_tankId > 0)
					{
						// Check if tank already exists
						sql = "select favListId from favListTank where favListId=@favListId; ";
						DB.AddWithValue(ref sql, "@favListId", favListId, DB.SqlDataType.Int);
                        // Add tank if not exsits
                        DataTable dt = await DB.FetchData(sql);
                        if (dt.Rows.Count == 0)
						{
							sql = "insert into favListTank (favListId, tankId, sortorder) values (@favListId, @tankId, 999999) ";
							DB.AddWithValue(ref sql, "@favListId", favListId, DB.SqlDataType.Int);
							DB.AddWithValue(ref sql, "@tankId", _tankId, DB.SqlDataType.Int);
                            await DB.ExecuteNonQuery(sql);
                            await FavListHelper.TankSort(favListId);
						}
					}
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    await FavListHelper.FavListSort();
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private async void ddCopyFrom_Click(object sender, EventArgs e)
		{
            await Code.DropDownGrid.Show(ddCopyFrom, Code.DropDownGrid.DropDownGridType.List, _copyFromDD);
		}


	}
}
