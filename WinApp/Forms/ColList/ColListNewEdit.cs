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
	public partial class ColListNewEdit : Form
	{
		string favListDD = "";
		int colListId = 0;
		public ColListNewEdit(int selectedColListId = 0)
		{
			InitializeComponent();
			if (selectedColListId == 0)
				ColListNewEditTheme.Text = "Add new Column List";
			else
				ColListNewEditTheme.Text = "Modify Column List";
			colListId = selectedColListId;
		}

		private void ColListNewEdit_Load(object sender, EventArgs e)
		{
			if (colListId > 0)
			{
				string sql = "select columnList.name as colListName, columnList.defaultFavListId, favList.name as favListname " +
							"from columnList left join favList on columnList.defaultFavListId=favList.id " +
							"where columnList.id=@id";
				DB.AddWithValue(ref sql, "@id", colListId, DB.SqlDataType.Int);
				DataRow dr = DB.FetchData(sql).Rows[0];
				txtName.Text = dr["colListName"].ToString();
				if (dr["favListname"] == DBNull.Value)
				{
					int favListId = Convert.ToInt32(dr["defaultFavListId"]);
					string favListName = "(Use Current)";
					if (favListId == -2) favListName = "(All Tanks)";
					ddDefaultTankFilter.Text = favListName;
				}
				else
					ddDefaultTankFilter.Text = dr["favListname"].ToString();

			}
			favListDD = "(Use Current),(All Tanks)";
			string favListSql = "select * from favList order by position";
			DataTable dtFavList = DB.FetchData(favListSql);
			if (dtFavList.Rows.Count > 0)
			{
				foreach (DataRow dr in dtFavList.Rows)
				{
					favListDD += "," + dr["name"].ToString();
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

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (txtName.Text.Length > 0)
			{
				string sql = "";
				// Update favlist
				string selectedfavListName = ddDefaultTankFilter.Text;
				int defaultFavListId = -1; // Use current
				if (selectedfavListName == "(Use Current)")
					defaultFavListId = -1;
				else if (selectedfavListName == "(All Tanks)")
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
				DB.AddWithValue(ref sql, "@name", txtName.Text, DB.SqlDataType.VarChar);
				DB.AddWithValue(ref sql, "@id", colListId, DB.SqlDataType.Int);
				DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
				DB.ExecuteNonQuery(sql);
				this.Close();
			}
		}

		
	}
}
