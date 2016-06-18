using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
	public static class TankHelper
	{
        public static void CreateUnknownTank(int tankId, string tankName)
        {
            int tier = 0;
            int countryId = -1; // Unknown nation
            int premium = 0;
            int tankTypeId = -1; // Unknown tank type
            // Special tank
            if (tankName == "unknown_1_234")
            {
                tankName = "Karl";
                tankTypeId = 5;
            }
            // Add tank
            string insertSql = "INSERT INTO tank (id, tankTypeId, countryId, name, tier, premium) VALUES (@id, @tankTypeId, @countryId, @name, @tier, @premium); ";
            DB.AddWithValue(ref insertSql, "@id", tankId, DB.SqlDataType.Int);
            DB.AddWithValue(ref insertSql, "@tankTypeId", tankTypeId, DB.SqlDataType.Int);
            DB.AddWithValue(ref insertSql, "@countryId", countryId, DB.SqlDataType.Int);
            DB.AddWithValue(ref insertSql, "@name", tankName, DB.SqlDataType.VarChar);
            DB.AddWithValue(ref insertSql, "@tier", tier, DB.SqlDataType.Int);
            DB.AddWithValue(ref insertSql, "@premium", premium, DB.SqlDataType.Int);
            DB.ExecuteNonQuery(insertSql);
        }
        
        #region DatabaseLookup

		public static void GetAllLists()
		{
			Log.CheckLogFileSize();
			GetTankList();
			GetJson2dbMappingFromDB();
			GetAchList();
			GetPlayerTankAchList();
			GetPlayerTankFragList();
			Battle2json.GetExistingBattleFiles();
		}

		public static DataTable achList = new DataTable();

		public static void GetAchList()
		{
			achList.Dispose();
			achList.Clear();
			achList = DB.FetchData("SELECT id, name FROM ach");
		}

		public static DataTable playerTankAchList = new DataTable();

		public static void GetPlayerTankAchList()
		{
			playerTankAchList.Dispose();
			playerTankAchList.Clear();
			playerTankAchList = new DataTable();
			playerTankAchList = DB.FetchData("SELECT playerTankAch.* " +
											"FROM playerTankAch INNER JOIN playerTank ON playerTankAch.playerTankId=playerTank.Id " +
											"WHERE playerId=" + Config.Settings.playerId.ToString());
		}

		public static void ClearPlayerTankAchList()
		{
			playerTankAchList.Dispose();
			playerTankAchList.Clear();
		}

		public static DataTable playerTankFragList = new DataTable();

		public static void GetPlayerTankFragList()
		{
			playerTankFragList.Dispose();
			playerTankFragList.Clear();
			playerTankFragList = new DataTable();
			string sql = "SELECT playerTank.id AS playerTankId, playerTank.tankId as PlayerTankTankId, playerTankFrag.* " +
	  					"FROM playerTank INNER JOIN playerTankFrag ON playerTank.id=playerTankFrag.playerTankId " +
						"WHERE playerId=" + Config.Settings.playerId.ToString();
			playerTankFragList = DB.FetchData(sql);
		}

		public static void ClearPlayerTankFragList()
		{
			playerTankFragList.Dispose();
			playerTankFragList.Clear();
		}

		public static DataTable tankList = new DataTable();

		public static void GetTankList()
		{
			tankList.Dispose();
			tankList.Clear();
			tankList = DB.FetchData("SELECT * FROM tank");
			foreach (DataRow dr in tankList.Rows)
			{
				// Replace WoT API tank name with Phalynx Dossier tank name
				string tankName = dr["name"].ToString();
				//tankName = tankName.Replace("ö", "o");
				//tankName = tankName.Replace("ä", "a");
				//tankName = tankName.Replace("â", "a");
				//tankName = tankName.Replace("ß", "s");
				dr["name"] = tankName;
				dr.AcceptChanges();
			}
			tankList.AcceptChanges();
		}

		public static DataTable GetPlayerTank(int tankId)
		{
			string sql = "SELECT * FROM playerTank WHERE playerId=@playerId AND tankId=@tankId; ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@tankId", tankId, DB.SqlDataType.Int);
			return DB.FetchData(sql);
		}

        public static DataTable GetPlayerTankBattle(int playerTankId, BattleMode.TypeEnum dossierBattleMode, bool CreateNewIfNotExists)
		{
            BattleMode.Item battleMode = BattleMode.GetItemFromType(dossierBattleMode);
			string sql = "SELECT * FROM playerTankBattle WHERE playerTankId=@playerId AND battleMode=@battleMode; ";
			DB.AddWithValue(ref sql, "@playerId", playerTankId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battleMode", battleMode.SqlName, DB.SqlDataType.VarChar);
			DataTable dt = DB.FetchData(sql);
			if (CreateNewIfNotExists && dt.Rows.Count == 0) // No battle recorded for this tank in this mode, create now and fetch once more
			{
				AddPlayerTankBattle(playerTankId, battleMode.SqlName);
				dt = DB.FetchData(sql);
			}
			return dt;
		}

        public static int GetPlayerTankBattleCount(int playerTankId, BattleMode.TypeEnum dossierBattleMode, out int wins, out int xp)
		{
            BattleMode.Item battleMode = BattleMode.GetItemFromType(dossierBattleMode);
			string sql = "SELECT battles, wins, xp FROM playerTankBattle WHERE playerTankId=@playerId AND battleMode=@battleMode; ";
			DB.AddWithValue(ref sql, "@playerId", playerTankId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battleMode", battleMode.SqlName, DB.SqlDataType.VarChar);
			DataTable dt = DB.FetchData(sql);
			int battles = 0;
			xp = 0;
			wins = 0;
			if (dt.Rows.Count > 0) // No battle recorded for this tank in this mode, create now and fetch once more
			{
				battles = Convert.ToInt32(dt.Rows[0]["battles"]);
				wins = Convert.ToInt32(dt.Rows[0]["wins"]);
				xp = Convert.ToInt32(dt.Rows[0]["xp"]);
			}
			dt.Dispose();
			dt.Clear();
			return battles;
		}
		

		private static void AddPlayerTankBattle(int playerTankId, string battleMode)
		{
			string sql = "INSERT INTO PlayerTankBattle (playerTankId, battleMode, battles) VALUES (@playerTankId, @battleMode, 0); ";
			DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
			DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql);
		}

		public static int GetPlayerTankCount()
		{
			string sql = "SELECT count(id) AS count FROM playerTank WHERE playerId=@playerId; ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			int count = 0;
			if (dt.Rows.Count > 0) count = Convert.ToInt32(dt.Rows[0]["count"]);
			dt.Dispose();
			dt.Clear();
			return count;
		}

		public static int ConvertWs2TankId(int wsTankId, int wsCountryId)
		{
			string sql = "SELECT tankId FROM wsTankId WHERE wsTankId=@wsTankId AND wsCountryId=@wsCountryId; ";
			DB.AddWithValue(ref sql, "@wsTankId", wsTankId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@wsCountryId", wsCountryId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			int lookupTankId = 0;
			if (dt.Rows.Count > 0) lookupTankId = Convert.ToInt32(dt.Rows[0]["tankId"]);
			dt.Dispose();
			dt.Clear();
			return lookupTankId;
		}


		public static int GetPlayerTankId(int tankId)
		{
			string sql = "SELECT playerTank.id " +
						 "FROM playerTank INNER JOIN tank ON playerTank.tankid = tank.id " +
						 "WHERE tank.id=@id and playerTank.playerId=@playerId; ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@id", tankId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			int lookupTankId = 0;
			if (dt.Rows.Count > 0) lookupTankId = Convert.ToInt32(dt.Rows[0][0]);
			dt.Dispose();
			dt.Clear();
			return lookupTankId;
		}

		public static int GetPlayerTankId(string tankName)
		{
			string sql = "SELECT playerTank.id " +
						 "FROM playerTank INNER JOIN tank ON playerTank.tankid = tank.id " +
						 "WHERE tank.name=@name and playerTank.playerId=@playerId; ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@name", tankName, DB.SqlDataType.VarChar);
			DataTable dt = DB.FetchData(sql);
			int lookupTankId = 0;
			if (dt.Rows.Count > 0) lookupTankId = Convert.ToInt32(dt.Rows[0][0]);
			dt.Dispose();
			dt.Clear();
			return lookupTankId;
		}

		public static DataTable GetBattle(int battleId)
		{
			string sql = "SELECT * FROM battle WHERE id=@id; ";
			DB.AddWithValue(ref sql, "@id", battleId, DB.SqlDataType.Int);
			return DB.FetchData(sql);
		}

		public static int GetBattleIdForImportedWsBattle(int wsId)
		{
			string sql = "SELECT Id FROM battle WHERE wsId=@wsId; ";
			DB.AddWithValue(ref sql, "@wsId", wsId, DB.SqlDataType.Int); 
			DataTable dt = DB.FetchData(sql);
			int lookupBattle = 0;
			if (dt.Rows.Count > 0) lookupBattle = Convert.ToInt32(dt.Rows[0]["Id"]);
			dt.Dispose();
			dt.Clear();
			return (lookupBattle);
		}

		public static DataTable json2dbMapping = new DataTable();
		
		public static void GetJson2dbMappingFromDB()
		{
			json2dbMapping = DB.FetchData("SELECT * FROM json2dbMapping ORDER BY jsonMainSubProperty; ");
		}

        public static DataTable GetTankData2BattleMapping(BattleMode.TypeEnum dossierBattleMode)
		{
            BattleMode.Item battleMode = BattleMode.GetItemFromType(dossierBattleMode);
			string sql =
				"SELECT  dbDataType, dbPlayerTank, dbPlayerTankMode, dbBattle " +
				"FROM    json2dbMapping " +
				"WHERE   (dbBattle IS NOT NULL) AND (dbPlayerTankMode IS NULL OR dbPlayerTankMode=@dbPlayerTankMode) " +
				"GROUP BY dbDataType, dbPlayerTank, dbBattle, dbPlayerTankMode ";
			DB.AddWithValue(ref sql, "@dbPlayerTankMode", battleMode.SqlName, DB.SqlDataType.VarChar);
			return DB.FetchData(sql);
		}

		#endregion

		#region LookupData

		// TODO: just for testing
		public static string ListTanks()
		{
			string s = "";
			foreach (DataRow dr in tankList.Rows)
			{
				s += dr["id"] + ":" + dr["name"] + ", ";
			}
			return s;
		}

		public static string GetTankName(int TankId)
		{
			string sql = "SELECT name FROM tank WHERE tank.Id=@tankId; ";
			DB.AddWithValue(ref sql, "@tankId", TankId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			string tankName = "";
			if (dt.Rows.Count > 0) tankName = dt.Rows[0]["name"].ToString();
			dt.Dispose();
			dt.Clear();
			return tankName;
		}

		public static int GetTankID(string TankName)
		{
			int tankID = 0;
			string expression = "name = '" + TankName + "'";
			DataRow[] foundRows = tankList.Select(expression);
			if (foundRows.Length > 0) // If tank exist in Tank table 
			{
				tankID = Convert.ToInt32(foundRows[0]["id"]);
			}
			return tankID;
		}

		public static int GetTankTier(string TankName)
		{
			int tankTier = 0;
			string sql = "SELECT tier FROM tank WHERE name=@name; ";
			DB.AddWithValue(ref sql, "@name", TankName, DB.SqlDataType.VarChar);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0) tankTier = Convert.ToInt32(dt.Rows[0]["tier"]);
			dt.Dispose();
			dt.Clear();
			return tankTier;
		}

		public static int GetTankTier(int TankId)
		{
			int tankTier = 0;
			string sql = "SELECT tier FROM tank WHERE id=@id; ";
			DB.AddWithValue(ref sql, "@id", TankId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0) tankTier = Convert.ToInt32(dt.Rows[0]["tier"]);
			dt.Dispose();
			dt.Clear();
			return tankTier;
		}

		public static int GetTankID(string TankName, out int TankTier)
		{
			int tankID = 0;
			TankTier = 0;
			string expression = "name = '" + TankName + "'";
			DataRow[] foundRows = tankList.Select(expression);
			if (foundRows.Length > 0) // If tank exist in Tank table 
			{
				tankID = Convert.ToInt32(foundRows[0]["id"]);
				TankTier = Convert.ToInt32(foundRows[0]["tier"]);
			}
			return tankID;
		}

		public static int GetTankID(int PlayerTankId)
		{
			string sql = "SELECT tank.id " +
						 "FROM playerTank INNER JOIN tank ON playerTank.tankid = tank.id " +
						 "WHERE playerTank.Id=@PlayerTankId AND playerTank.playerId=@playerId; ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@PlayerTankId", PlayerTankId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			int tankId = 0;
			if (dt.Rows.Count > 0) tankId = Convert.ToInt32(dt.Rows[0]["id"]);
			dt.Dispose();
			dt.Clear();
			return tankId;
		}

		public static bool GetAchievmentExist(string achName)
		{
			bool exists = false;
			string sql = "SELECT ach.id FROM ach WHERE name=@name; ";
			DB.AddWithValue(ref sql, "@name", achName, DB.SqlDataType.VarChar);
			DataTable dt = DB.FetchData(sql);
			exists = (dt.Rows.Count > 0);
			dt.Dispose();
			dt.Clear();
			return exists;
		}

		public static bool TankExists(int tankId)
		{
			string expression = "id = " + tankId.ToString();
			DataRow[] foundRows = tankList.Select(expression);
			return (foundRows.Length > 0);
		}

		public static bool PlayerTankExists(int tankId)
		{
			string sql = "SELECT id FROM playerTank WHERE tankId=@tankId; ";
			DB.AddWithValue(ref sql, "@tankId", tankId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			bool exists = (dt.Rows.Count > 0);
			dt.Dispose();
			dt.Clear();
			return exists;
		}

		public static DataRow TankInfo(int tankId)
		{
			string expression = "id = " + tankId.ToString();
			DataRow[] foundRows = tankList.Select(expression);
			if (foundRows.Length > 0)
				return foundRows[0];
			else
				return null;
		}

        #endregion
	   
	}
}
