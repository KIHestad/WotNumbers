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
	public partial class FavListNewEdit : Form
	{
		string copyFromDD = "";
		int favListId = 0;
		string prevName = "";
		public FavListNewEdit(int selectedFavListId = 0, string newFavListName = "")
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
			favListId = selectedFavListId;
		}

		private void FavListNewEdit_Load(object sender, EventArgs e)
		{
			if (favListId > 0)
			{
				// Show selected favList
				string sql = "select * from favList where id=@id; ";
				DB.AddWithValue(ref sql, "@id", favListId, DB.SqlDataType.Int);
				DataRow dr = DB.FetchData(sql).Rows[0];
				txtName.Text = dr["name"].ToString();
				prevName = dr["name"].ToString();
			}
			else 
			{
				// Populate Copy From DD
				copyFromDD = "(None)";
				string copyFromSql = "select * from favList order by COALESCE(position,99), name";
				DataTable dtcopyFrom = DB.FetchData(copyFromSql);
				if (dtcopyFrom.Rows.Count > 0)
				{
					foreach (DataRow dr2 in dtcopyFrom.Rows)
					{
						copyFromDD += "," + dr2["name"].ToString();
					}
				}
			}
		}
		private void btnSave_Click(object sender, EventArgs e)
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
				DataTable dtExists = DB.FetchData(sql);
				if (newName != prevName && dtExists.Rows.Count > 0)
				{
					Code.MsgBox.Show("This name is already in use, select a different name for your Favourite Tank List", "Name already in use", this);
				}
				else
				{
					if (favListId > 0)
						sql = "update favList set name=@name where id=@id; ";
					else
						sql = "insert into favList (name, position) values (@name, 99999); ";
					// Avoid saving if name is not changed
					if (favListId > 0 && newName == prevName) return;
					// Save now
					DB.AddWithValue(ref sql, "@name", newName, DB.SqlDataType.VarChar);
					DB.AddWithValue(ref sql, "@id", favListId, DB.SqlDataType.Int);
					DB.ExecuteNonQuery(sql);
					// Add tanks if new colList and seleced colList in copy to DD
					if (favListId == 0 && ddCopyFrom.Text != "(None)")
					{
						// Get the id for copy from
						sql = "select id from favList where name=@name; ";
						DB.AddWithValue(ref sql, "@name", ddCopyFrom.Text, DB.SqlDataType.VarChar);
						DataTable dtCopyFrom = DB.FetchData(sql);
						int copyFromId = Convert.ToInt32(dtCopyFrom.Rows[0]["id"]);
						// Get the id for copy to
						sql = "select id from favList where name=@name; ";
						DB.AddWithValue(ref sql, "@name", newName, DB.SqlDataType.VarChar);
						DataTable dtCopyTo = DB.FetchData(sql);
						int copyToId = Convert.ToInt32(dtCopyTo.Rows[0]["id"]);
						// Copy now
						sql = "insert into favListTank (favListId, tankId, sortorder) select @copyToFavListId, tankId, sortorder " +
																					"   from favListTank " +
																					"   where favListId=@copyFromFavListId; ";
						DB.AddWithValue(ref sql, "@copyToFavListId", copyToId, DB.SqlDataType.Int);
						DB.AddWithValue(ref sql, "@copyFromFavListId", copyFromId, DB.SqlDataType.Int);
						DB.ExecuteNonQuery(sql);
					}
					this.Close();
					FavListHelper.FavListSort();
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ddCopyFrom_Click(object sender, EventArgs e)
		{
			Code.DropDownGrid.Show(ddCopyFrom, Code.DropDownGrid.DropDownGridType.List, copyFromDD);
		}


	}
}
