using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
	class GridSortingHelper
	{
		public class Sorting
		{
			public string ColumnHeader = ""; // Column selected in header for sorting
			public string ColumnName = ""; // Actual column name for sorting, normally the same as lastColumn, exept for Images
			public bool SortDirectionAsc = false;
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
					sorting.ColumnHeader = dt.Rows[0]["lastSortColumn"].ToString();
					ColListHelper.ColListClass clc = ColListHelper.GetColListItem(sorting.ColumnHeader, MainSettings.View);
					// Check if found column
					if (clc.name == "")
						sorting.ColumnHeader = ""; // not found
					else
					{
						// found sorting column, get values
						sorting.ColumnName = clc.colName;
						// special sort defined for columns
						if (clc.colNameSort != "")
							sorting.ColumnName = clc.colNameSort;
					}
				}
				sorting.SortDirectionAsc = Convert.ToBoolean(dt.Rows[0]["lastSortDirectionAsc"]);
			}
			return sorting;
		}

		public async static Task SaveSorting(int colListId, Sorting sorting)
		{
			string sql = "update columnList set lastSortColumn=@lastSortColumn, lastSortDirectionAsc=@lastSortDirectionAsc where id=@id;";
			DB.AddWithValue(ref sql, "@id", colListId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@lastSortColumn", sorting.ColumnHeader, DB.SqlDataType.VarChar);
			DB.AddWithValue(ref sql, "@lastSortDirectionAsc", sorting.SortDirectionAsc, DB.SqlDataType.Boolean);
            await DB.ExecuteNonQueryAsync(sql);
		}

	}
}
