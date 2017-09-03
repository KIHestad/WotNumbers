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
			MyTanks = -2,
			FavList = 0,
			AllTanksNotOwned = -3,
		}
		
		public enum BattleModeType
		{
			All = 1,
			RandomAndTankCompany = 2,
			Team = 3,
			Random = 4,
			ClanWar = 5,
			TankCompany = 6,
			Historical = 7,
			Skirmishes = 8,
			RandomSolo = 9,
			RandomPlatoon = 10,
			RandomPlatoon2 = 11,
			RandomPlatoon3 = 12,
			Special = 13,
			Stronghold = 14,
			TeamRanked = 15,
			GlobalMap = 16,
            Grand = 17,
		}

		public class Settings
		{
			public int ColListId = 0;
			public string ColListName = ""; // Col showing in grid
			public FavListShowType FavListShow = FavListShowType.MyTanks;
			public int FavListId = 0;
			public string FavListName = "";
			public int TankId = -1; // Filter on explicit tank
			public BattleModeType BattleMode = BattleModeType.All;
		}

		public static bool BattleTimeFilterCustomApply = false; // Used for form: BattleTimeFilterCustom

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
				{
					defaultGridFilter.FavListShow = (FavListShowType)defaultFavListId;
				}
				// If used "Use Current" as startup fav list, override with appropiate fav list type
				if (defaultGridFilter.FavListShow == FavListShowType.UseCurrent)
					defaultGridFilter.FavListShow = FavListShowType.MyTanks;
			}
			if (gridView == GridView.Views.Tank)
				defaultGridFilter.BattleMode = BattleModeType.RandomAndTankCompany;
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
