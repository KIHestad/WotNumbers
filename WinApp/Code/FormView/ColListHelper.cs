using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
	class ColListHelper
	{
		public static int GetId(string ColListName)
		{
			int colListId = 0;
			string sql = "select columnList.id as id " +
						 "from columnList  " +
						 "where columnList.colType=@colType and columnList.name=@name";
			DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@name", ColListName, DB.SqlDataType.VarChar);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				colListId = Convert.ToInt32(dt.Rows[0]["id"]);
			}
			return colListId;
		}

		public static GridFilter.Settings GetSettingsForColList(int colListId)
		{
			GridFilter.Settings dfl = new GridFilter.Settings();
			string sql = "SELECT columnList.id AS colListId, columnList.name AS colListName, columnList.defaultFavListId AS colListDefaultFavList, " +
						 "       favList.id AS favListId, favList.name AS favListName " +
						 "FROM columnList LEFT OUTER JOIN " +
						 "     favList ON columnList.defaultFavListId = favList.id " +
						 "WHERE columnList.id=@id;";
			DB.AddWithValue(ref sql, "@id", colListId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				dfl.ColListId = colListId;
				dfl.ColListName = dt.Rows[0]["colListName"].ToString();
				int defaultFavListID = Convert.ToInt32(dt.Rows[0]["colListDefaultFavList"]);
				if (defaultFavListID > 0)
				{
					dfl.FavListShow = GridFilter.FavListShowType.FavList;
					dfl.FavListId = defaultFavListID;
					dfl.FavListName = dt.Rows[0]["favListName"].ToString();
				}
				else
				{
					dfl.FavListShow = (GridFilter.FavListShowType)defaultFavListID;
				}
			}
			return dfl;
		}
	}
}
