using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
	class FavListHelper
	{
		public static int GetId(string FavListName)
		{
			int favListId = 0;
			string sql = "select id from favList where name=@name;";
			DB.AddWithValue(ref sql, "@name", FavListName, DB.SqlDataType.VarChar);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				favListId = Convert.ToInt32(dt.Rows[0]["id"]);
			}
			return favListId;
		}

		public static void FavListSort()
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
			}
		}
	}
}
