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
		
		public enum BattleModeType
		{
			All = 1,
			Mode15 = 2,
			Mode7 = 3,
			Random = 4,
			Clan = 5,
			Company = 6,
			Historical = 7,
		}

		public class Settings
		{
			public int ColListId = 0;
			public string ColListName = "";
			public FavListShowType FavListShow = FavListShowType.AllTanks;
			public int FavListId = 0;
			public string FavListName = "";
			public int TankId = -1; // Filter on explicit tank
			public BattleModeType BattleMode = BattleModeType.All;
		}

		public static Settings GetDefault(GridView.Views gridView)
		{
			// Gets default settings for tank and battle filter
			Settings defaultGridFilter = new Settings();
			string sql = "SELECT columnList.id AS colListId, columnList.name AS colListName, columnList.defaultFavListId AS colListDefaultFavList, " +
						 "       favList.id AS favListId, favList.name AS favListName " +
						 "FROM columnList LEFT OUTER JOIN " +
						 "     favList ON columnList.defaultFavListId = favList.id " +
						 "WHERE (columnList.colDefault=1) AND (columnList.colType=@colType); ";
			DB.AddWithValue(ref sql, "@colType", (int)gridView, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql, false);
			if (dt.Rows.Count == 0)
			{
				// No default is selected, choose first colList
				string colListName = "";
				defaultGridFilter.ColListId = GetFirstColListId(gridView, out colListName);
				defaultGridFilter.ColListName = colListName;
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

		private static int GetFirstColListId(GridView.Views gridView, out string ColListName)
		{
			string sql = "SELECT columnList.id AS colListId, columnList.name AS colListName, columnList.defaultFavListId AS colListDefaultFavList, " +
						 "       favList.id AS favListId, favList.name AS favListName " +
						 "FROM columnList LEFT OUTER JOIN " +
						 "     favList ON columnList.defaultFavListId = favList.id " +
						 "WHERE (columnList.colType=@colType); ";
			DB.AddWithValue(ref sql, "@colType", (int)gridView, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql, false);
			int colListId = 0;
			ColListName = "";
			if (dt.Rows.Count > 0)
			{
				colListId = Convert.ToInt32(dt.Rows[0]["colListId"]);
				ColListName = dt.Rows[0]["colListName"].ToString();
			}
			return colListId;

		}
	}
}
