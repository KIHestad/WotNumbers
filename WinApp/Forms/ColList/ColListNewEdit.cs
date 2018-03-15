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
	public partial class ColListNewEdit : FormCloseOnEsc
    {
		string favListDD = "";
		string copyFromDD = "";
		int colListId = 0;
		string prevName = "";
		public ColListNewEdit(int selectedColListId = 0)
		{
			InitializeComponent();
			if (selectedColListId == 0)
				ColListNewEditTheme.Text = "Add new Column List";
			else
			{
				ColListNewEditTheme.Text = "Modify Column List";
				ddCopyFrom.Visible = false;
				lblCopyFrom.Visible = false;
			}
			colListId = selectedColListId;
		}

		private void ColListNewEdit_Load(object sender, EventArgs e)
		{
			if (colListId > 0)
			{
				// Show selected colList
				string sql = "select columnList.name as colListName, columnList.defaultFavListId, favList.name as favListname " +
							"from columnList left join favList on columnList.defaultFavListId=favList.id " +
							"where columnList.id=@id";
				DB.AddWithValue(ref sql, "@id", colListId, DB.SqlDataType.Int);
				DataRow dr = DB.FetchData(sql).Rows[0];
				txtName.Text = dr["colListName"].ToString();
				prevName = dr["colListName"].ToString();
				if (dr["favListname"] == DBNull.Value)
				{
					int favListId = Convert.ToInt32(dr["defaultFavListId"]);
					string favListName = "(Use Current)";
					if (favListId == -2) favListName = "(My Tanks)";
					ddDefaultTankFilter.Text = favListName;
				}
				else
					ddDefaultTankFilter.Text = dr["favListname"].ToString();
			}
			favListDD = "(Use Current),(My Tanks)";
			string favListSql = "select * from favList order by COALESCE(position,99), name";
			DataTable dtFavList = DB.FetchData(favListSql);
			if (dtFavList.Rows.Count > 0)
			{
				foreach (DataRow dr in dtFavList.Rows)
				{
					favListDD += "," + dr["name"].ToString();
				}
			}
			// Populate Copy From DD
			copyFromDD = "(None)";
			string copyFromSql = "select * from columnList where colType=@colType order by COALESCE(position,99), name";
			DB.AddWithValue(ref copyFromSql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
			DataTable dtcopyFrom = DB.FetchData(copyFromSql);
			if (dtcopyFrom.Rows.Count > 0)
			{
				foreach (DataRow dr2 in dtcopyFrom.Rows)
				{
					copyFromDD += "," + dr2["name"].ToString();
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ddDefaultTankFilter_Click(object sender, EventArgs e)
		{
			Code.DropDownGrid.Show(ddDefaultTankFilter, Code.DropDownGrid.DropDownGridType.List, favListDD);
		}

		private async void btnSave_Click(object sender, EventArgs e)
		{
			string newName = txtName.Text.Trim();
			if (newName.Length == 0)
			{
				Code.MsgBox.Show("Plese select a name for the column list", "Name missing", this);
			}
			else
			{
				// Check if name not already exists
				string sql = "select id from columnList where name=@name and colType=@colType; ";
				DB.AddWithValue(ref sql, "@name", newName, DB.SqlDataType.VarChar);
				DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
				DataTable dtExists = DB.FetchData(sql);
				if (dtExists.Rows.Count > 0 && newName != prevName)
				{
					Code.MsgBox.Show("This name is already in use, select a different name for the column list", "Name already in use", this);
				}
				else
				{
					// Find favlist ID
					string selectedfavListName = ddDefaultTankFilter.Text;
					int defaultFavListId = -1; // Use current
					if (selectedfavListName == "(Use Current)")
						defaultFavListId = -1;
					else if (selectedfavListName == "(My Tanks)")
						defaultFavListId = -2;
					else
					{
						// Find favListId
						sql = "select id from favList where name=@name";
						DB.AddWithValue(ref sql, "@name", selectedfavListName, DB.SqlDataType.VarChar);
						DataTable dtFavList = DB.FetchData(sql);
						if (dtFavList.Rows.Count > 0)
							defaultFavListId = Convert.ToInt32(dtFavList.Rows[0][0]);
					}
					// Save now
					if (colListId > 0)
						sql = "update columnList set defaultFavListId=@defaultFavListId, name=@name where id=@id";
					else
						sql = "insert into columnList (defaultFavListId, name, colType, position) values (@defaultFavListId, @name, @colType, 99999)";
					DB.AddWithValue(ref sql, "@defaultFavListId", defaultFavListId, DB.SqlDataType.Int);
					DB.AddWithValue(ref sql, "@name", newName, DB.SqlDataType.VarChar);
					DB.AddWithValue(ref sql, "@id", colListId, DB.SqlDataType.Int);
					DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
					await DB.ExecuteNonQueryAsync(sql);
					// Add tanks if new colList and seleced colList in copy to DD
					if (colListId == 0 && ddCopyFrom.Text != "(None)")
					{
						// Get the id for copy from
						sql = "select id from columnList where name=@name and colType=@colType; ";
						DB.AddWithValue(ref sql, "@name", ddCopyFrom.Text, DB.SqlDataType.VarChar);
						DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
						DataTable dtCopyFrom = DB.FetchData(sql);
						int copyFromId = Convert.ToInt32(dtCopyFrom.Rows[0]["id"]);
						// Get the id for copy to
						sql = "select id from columnList where name=@name and colType=@colType; ";
						DB.AddWithValue(ref sql, "@name", newName, DB.SqlDataType.VarChar);
						DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
						DataTable dtCopyTo = DB.FetchData(sql);
						int copyToId = Convert.ToInt32(dtCopyTo.Rows[0]["id"]);
						// Copy now
						sql =	"insert into columnListSelection (columnSelectionId, columnListId, sortorder, colWidth) " +
								"   select columnSelectionId, @copyToColumnListId, sortorder, colWidth " +
								"   from columnListSelection " +
								"   where ColumnListId=@copyFromColumnListId; ";
						DB.AddWithValue(ref sql, "@copyToColumnListId", copyToId, DB.SqlDataType.Int);
						DB.AddWithValue(ref sql, "@copyFromColumnListId", copyFromId, DB.SqlDataType.Int);
						await DB.ExecuteNonQueryAsync(sql);
					
					}
					this.Close();
				}
			}
		}

		private void ddCopyFrom_Click(object sender, EventArgs e)
		{
			Code.DropDownGrid.Show(ddCopyFrom, Code.DropDownGrid.DropDownGridType.List, copyFromDD);
		}

		
	}
}
