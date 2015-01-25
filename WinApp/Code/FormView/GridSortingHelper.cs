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
			public string lastColumn = ""; // Column selected in header for sorting
			public string lastSortColumn = ""; // Actual column name for sorting, normally the same as lastColumn, exept for Images
			public bool lastSortDirectionAsc = false;
		}

		public static Sorting GetSorting(GridFilter.Settings currentGridFilter)
		{
			Sorting sorting = new Sorting();
			string sql = "select * from columnList where id=@id;";
			DB.AddWithValue(ref sql, "@id", currentGridFilter.ColListId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				if (dt.Rows[0]["lastSortColumn"] != DBNull.Value)
				{
					sorting.lastColumn = dt.Rows[0]["lastSortColumn"].ToString();
					ColListHelper.ColListClass clc = ColListHelper.GetColListItem(sorting.lastColumn, MainSettings.View);
					// Check if found column
					if (clc.name == "")
						sorting.lastColumn = ""; // not found
					else
					{
						// found sorting column, get values
						sorting.lastSortColumn = clc.colName;
						// special sort defined for columns
						if (clc.colNameSort != "")
							sorting.lastSortColumn = clc.colNameSort;
					}
					// special sort for image columns
					//if (clc.colDataType == "Image")
					//{
					//	if (clc.name == "Mastery Badge")
					//		sorting.lastSortColumn = "mb_id";
					//	else
					//		sorting.lastSortColumn = "tank_name";
					//}
				}
				sorting.lastSortDirectionAsc = Convert.ToBoolean(dt.Rows[0]["lastSortDirectionAsc"]);
			}
			return sorting;
		}

		public static void SaveSorting(int colListId, Sorting sorting)
		{
			string sql = "update columnList set lastSortColumn=@lastSortColumn, lastSortDirectionAsc=@lastSortDirectionAsc where id=@id;";
			DB.AddWithValue(ref sql, "@id", colListId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@lastSortColumn", sorting.lastColumn, DB.SqlDataType.VarChar);
			DB.AddWithValue(ref sql, "@lastSortDirectionAsc", sorting.lastSortDirectionAsc, DB.SqlDataType.Boolean);
			DB.ExecuteNonQuery(sql);
		}

	}
}
