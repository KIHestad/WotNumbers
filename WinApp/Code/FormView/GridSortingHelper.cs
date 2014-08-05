using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WinApp.Code
{
	class GridSortingHelper
	{
		public class Sorting
		{
			public string lastSortColumn = "";
			public bool lastSortDirectionAsc = false;
		}
		
		public static Sorting GetSorting(int colListId)
		{
			Sorting sorting = new Sorting();
			string sql = "select * from columnList where id=@id;";
			DB.AddWithValue(ref sql, "@id", colListId, DB.SqlDataType.Image);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				if (dt.Rows[0]["lastSortColumn"] != DBNull.Value)
					sorting.lastSortColumn = dt.Rows[0]["lastSortColumn"].ToString();
				sorting.lastSortDirectionAsc = Convert.ToBoolean(dt.Rows[0]["lastSortDirectionAsc"]);
			}
			return sorting;
		}

		public static void SaveSorting(int colListId, Sorting sorting)
		{
			string sql = "update columnList set lastSortColumn=@lastSortColumn, lastSortDirectionAsc=@lastSortDirectionAsc where id=@id;";
			DB.AddWithValue(ref sql, "@id", colListId, DB.SqlDataType.Image);
			DB.AddWithValue(ref sql, "@lastSortColumn", sorting.lastSortColumn, DB.SqlDataType.VarChar);
			DB.AddWithValue(ref sql, "@lastSortDirectionAsc", sorting.lastSortDirectionAsc, DB.SqlDataType.Boolean);
			DB.ExecuteNonQuery(sql);
		}

	}
}
