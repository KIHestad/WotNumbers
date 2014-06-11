using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
	public class GridFilter
	{
		public enum FavListShowType
		{
			UseCurrent = -1,
			AllTanks = -2,
			FavList = 0,
		}
		
		public class Settings
		{
			public int ColListId = 0;
			public string ColListName = "";
			public FavListShowType FavListShow = FavListShowType.UseCurrent;
			public int FavListId = 0;
			public string FavListName = "";
		}

		public static Settings SetDefault(GridView.Views gridView)
		{
			// Gets default settings for tank and battle filter
			Settings defaultGridFilter = new Settings();
			string sql = "SELECT columnList.id AS colListId, columnList.name AS colListName, columnList.defaultFavListId AS colListDefaultFavList, " +
						 "       favList.id AS favListId, favList.name AS favListName " +
						 "FROM columnList LEFT OUTER JOIN " +
						 "     favList ON columnList.defaultFavListId = favList.id " +
						 "WHERE (columnList.colDefault=1) AND (columnList.colType=@colType); ";
			DB.AddWithValue(ref sql, "@colType", (int)gridView, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count == 0)
			{
				// No default is selected, choose first sys col
				switch (gridView)
				{
					case GridView.Views.Overall:
						break;
					case GridView.Views.Tank:
						defaultGridFilter.ColListId = 1; // 1-3 default col list (sys col) - for tank view
						break;
					case GridView.Views.Battle:
						defaultGridFilter.ColListId = 4; // 4-6 default col list (sys col) - for battle view
						break;
					default:
						break;
				}
				defaultGridFilter.ColListName = "Default";
			}
			else
			{
				// Use defaults from DB
				DataRow dr = dt.Rows[0];
				defaultGridFilter.ColListId = Convert.ToInt32(dr["colListId"]);
				defaultGridFilter.ColListName = dr["colListName"].ToString();
				int defaultFavListId = Convert.ToInt32(dr["colListDefaultFavList"]);
				if (defaultFavListId > 0)
				{
					defaultGridFilter.FavListShow = FavListShowType.FavList;
					defaultGridFilter.FavListId = defaultFavListId;
					defaultGridFilter.FavListName = dr["favListName"].ToString();
				}
				else
					defaultGridFilter.FavListShow = (FavListShowType)defaultFavListId;
			}
			return defaultGridFilter;
		}
	}
}
