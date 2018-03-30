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
        public class BasicTankInfo
        {
            public string name { get; set; }
            public string short_name { get; set; }
            public string tier { get; set; }
            public string nation { get; set; }
            public string tankType { get; set; }
            public bool customTankInfo { get; set; }
            public string message { get; set; }
        }
                
        public async static Task CreateUnknownTank(int tankId, string tankName)
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
            await DB.ExecuteNonQuery(insertSql);
        }
        
        #region DatabaseLookup

		public async static Task GetAllLists()
		{
			await Log.CheckLogFileSize();
			await GetTankList();
            await GetJson2dbMappingFromDB();
            await GetAchList();
            await GetPlayerTankAchList();
            await GetPlayerTankFragList();
			Battle2json.GetExistingBattleFiles();
		}

		public static DataTable achList = new DataTable();

		public async static Task GetAchList()
		{
			achList.Dispose();
			achList.Clear();
			achList = await DB.FetchData("SELECT id, name FROM ach");
		}

		public static DataTable playerTankAchList = new DataTable();

		public async static Task GetPlayerTankAchList()
		{
			playerTankAchList.Dispose();
			playerTankAchList.Clear();
			playerTankAchList = new DataTable();
			playerTankAchList = await DB.FetchData("SELECT playerTankAch.* " +
											"FROM playerTankAch INNER JOIN playerTank ON playerTankAch.playerTankId=playerTank.Id " +
											"WHERE playerId=" + Config.Settings.playerId.ToString());
		}

		public static void ClearPlayerTankAchList()
		{
			playerTankAchList.Dispose();
			playerTankAchList.Clear();
		}

		public static DataTable playerTankFragList = new DataTable();

		public async static Task GetPlayerTankFragList()
		{
			playerTankFragList.Dispose();
			playerTankFragList.Clear();
			playerTankFragList = new DataTable();
			string sql = "SELECT playerTank.id AS playerTankId, playerTank.tankId as PlayerTankTankId, playerTankFrag.* " +
	  					"FROM playerTank INNER JOIN playerTankFrag ON playerTank.id=playerTankFrag.playerTankId " +
						"WHERE playerId=" + Config.Settings.playerId.ToString();
			playerTankFragList = await DB.FetchData(sql);
		}

		public static void ClearPlayerTankFragList()
		{
			playerTankFragList.Dispose();
			playerTankFragList.Clear();
		}

		public static DataTable tankList = new DataTable();

		public async static Task GetTankList()
		{
			tankList.Dispose();
			tankList.Clear();
			tankList = await DB.FetchData("SELECT * FROM tank");
			tankList.AcceptChanges();
		}

		public async static Task<DataTable> GetPlayerTank(int tankId)
		{
			string sql = "SELECT * FROM playerTank WHERE playerId=@playerId AND tankId=@tankId; ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@tankId", tankId, DB.SqlDataType.Int);
			return await DB.FetchData(sql);
		}

        public async static Task<DataTable> GetPlayerTankBattle(int playerTankId, BattleMode.TypeEnum dossierBattleMode, bool CreateNewIfNotExists)
		{
            BattleMode.Item battleMode = BattleMode.GetItemFromType(dossierBattleMode);
			string sql = "SELECT * FROM playerTankBattle WHERE playerTankId=@playerId AND battleMode=@battleMode; ";
			DB.AddWithValue(ref sql, "@playerId", playerTankId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battleMode", battleMode.SqlName, DB.SqlDataType.VarChar);
			DataTable dt = await DB.FetchData(sql);
			if (CreateNewIfNotExists && dt.Rows.Count == 0) // No battle recorded for this tank in this mode, create now and fetch once more
			{
                await AddPlayerTankBattle(playerTankId, battleMode.SqlName);
				dt = await DB.FetchData(sql);
			}
			return dt;
		}

        public class PlayerTankBattleCountResult
        {
            public int Battles { get; set; }
            public int Wins { get; set; }
            public int Xp { get; set; }
        }

        public async static Task<PlayerTankBattleCountResult> GetPlayerTankBattleCount(int playerTankId, BattleMode.TypeEnum dossierBattleMode)
		{
            PlayerTankBattleCountResult result = new PlayerTankBattleCountResult();
            BattleMode.Item battleMode = BattleMode.GetItemFromType(dossierBattleMode);
			string sql = "SELECT battles, wins, xp FROM playerTankBattle WHERE playerTankId=@playerId AND battleMode=@battleMode; ";
			DB.AddWithValue(ref sql, "@playerId", playerTankId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battleMode", battleMode.SqlName, DB.SqlDataType.VarChar);
			DataTable dt = await DB.FetchData(sql);
            result.Battles = 0;
			result.Xp = 0;
            result.Wins = 0;
			if (dt.Rows.Count > 0) // No battle recorded for this tank in this mode, create now and fetch once more
			{
                result.Battles = Convert.ToInt32(dt.Rows[0]["battles"]);
                result.Wins = Convert.ToInt32(dt.Rows[0]["wins"]);
                result.Xp = Convert.ToInt32(dt.Rows[0]["xp"]);
			}
			dt.Dispose();
			dt.Clear();
			return result;
		}
		

		private async static Task AddPlayerTankBattle(int playerTankId, string battleMode)
		{
			string sql = "INSERT INTO PlayerTankBattle (playerTankId, battleMode, battles) VALUES (@playerTankId, @battleMode, 0); ";
			DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
			DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
            await DB.ExecuteNonQuery(sql);
		}

		public async static Task<int> GetPlayerTankCount()
		{
			string sql = "SELECT count(id) AS count FROM playerTank WHERE playerId=@playerId; ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql);
			int count = 0;
			if (dt.Rows.Count > 0) count = Convert.ToInt32(dt.Rows[0]["count"]);
			dt.Dispose();
			dt.Clear();
			return count;
		}

		public async static Task<int> ConvertWs2TankId(int wsTankId, int wsCountryId)
		{
			string sql = "SELECT tankId FROM wsTankId WHERE wsTankId=@wsTankId AND wsCountryId=@wsCountryId; ";
			DB.AddWithValue(ref sql, "@wsTankId", wsTankId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@wsCountryId", wsCountryId, DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql);
			int lookupTankId = 0;
			if (dt.Rows.Count > 0) lookupTankId = Convert.ToInt32(dt.Rows[0]["tankId"]);
			dt.Dispose();
			dt.Clear();
			return lookupTankId;
		}


		public async static Task<int> GetPlayerTankId(int tankId)
		{
			string sql = "SELECT playerTank.id " +
						 "FROM playerTank INNER JOIN tank ON playerTank.tankid = tank.id " +
						 "WHERE tank.id=@id and playerTank.playerId=@playerId; ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@id", tankId, DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql);
			int lookupTankId = 0;
			if (dt.Rows.Count > 0) lookupTankId = Convert.ToInt32(dt.Rows[0][0]);
			dt.Dispose();
			dt.Clear();
			return lookupTankId;
		}

		public async static Task<int> GetPlayerTankId(string tankName)
		{
			string sql = "SELECT playerTank.id " +
						 "FROM playerTank INNER JOIN tank ON playerTank.tankid = tank.id " +
						 "WHERE tank.name=@name and playerTank.playerId=@playerId; ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@name", tankName, DB.SqlDataType.VarChar);
			DataTable dt = await DB.FetchData(sql);
			int lookupTankId = 0;
			if (dt.Rows.Count > 0) lookupTankId = Convert.ToInt32(dt.Rows[0][0]);
			dt.Dispose();
			dt.Clear();
			return lookupTankId;
		}

		public async static Task<DataTable> GetBattle(int battleId)
		{
			string sql = "SELECT * FROM battle WHERE id=@id; ";
			DB.AddWithValue(ref sql, "@id", battleId, DB.SqlDataType.Int);
			return await DB.FetchData(sql);
		}

		public async static Task<int> GetBattleIdForImportedWsBattle(int wsId)
		{
			string sql = "SELECT Id FROM battle WHERE wsId=@wsId; ";
			DB.AddWithValue(ref sql, "@wsId", wsId, DB.SqlDataType.Int); 
			DataTable dt = await DB.FetchData(sql);
			int lookupBattle = 0;
			if (dt.Rows.Count > 0) lookupBattle = Convert.ToInt32(dt.Rows[0]["Id"]);
			dt.Dispose();
			dt.Clear();
			return (lookupBattle);
		}

		public static DataTable json2dbMapping = new DataTable();
		
		public async static Task GetJson2dbMappingFromDB()
		{
			json2dbMapping = await DB.FetchData("SELECT * FROM json2dbMapping ORDER BY jsonMainSubProperty; ");
		}

        public async static Task<DataTable> GetTankData2BattleMapping(BattleMode.TypeEnum dossierBattleMode)
		{
            BattleMode.Item battleMode = BattleMode.GetItemFromType(dossierBattleMode);
			string sql =
				"SELECT  dbDataType, dbPlayerTank, dbPlayerTankMode, dbBattle " +
				"FROM    json2dbMapping " +
				"WHERE   (dbBattle IS NOT NULL) AND (dbPlayerTankMode IS NULL OR dbPlayerTankMode=@dbPlayerTankMode) " +
				"GROUP BY dbDataType, dbPlayerTank, dbBattle, dbPlayerTankMode ";
			DB.AddWithValue(ref sql, "@dbPlayerTankMode", battleMode.SqlName, DB.SqlDataType.VarChar);
			return await DB.FetchData(sql);
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

        public static string GetTankName(int TankId, bool short_name = false)
		{
            DataRow[] dr = tankList.Select("id = " + TankId.ToString());
            if (dr.Length == 0)
                return "";
            if (short_name)
                return dr[0]["short_name"].ToString();
            else
                return dr[0]["name"].ToString();
		}

        public async static Task<DataRow> GetTankInfo(int TankId)
        {
            string sql = @"
                select t.*, c.name as countryName, tt.name as tankTypeName
                from tank t 
                    left join country c on t.countryId = c.id 
                    left join tankType tt on t.tankTypeId = tt.id
                where t.id = " + TankId.ToString();
            DataTable dt = await DB.FetchData(sql);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
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

        public static bool HasCustomTankInfo(int tankId)
        {
            bool hasCustomTankInfo = false;
            string expression = "id = " + tankId ;
            DataRow[] foundRows = tankList.Select(expression);
            if (foundRows.Length > 0) // If tank exist in Tank table 
            {
                hasCustomTankInfo = Convert.ToBoolean(foundRows[0]["customTankInfo"]);
            }
            return hasCustomTankInfo;
        }

        public async static Task<int> GetTankTier(string TankName)
		{
			int tankTier = 0;
			string sql = "SELECT tier FROM tank WHERE name=@name; ";
			DB.AddWithValue(ref sql, "@name", TankName, DB.SqlDataType.VarChar);
			DataTable dt = await DB.FetchData(sql);
			if (dt.Rows.Count > 0) tankTier = Convert.ToInt32(dt.Rows[0]["tier"]);
			dt.Dispose();
			dt.Clear();
			return tankTier;
		}

		public async static Task<int> GetTankTier(int TankId)
		{
			int tankTier = 0;
			string sql = "SELECT tier FROM tank WHERE id=@id; ";
			DB.AddWithValue(ref sql, "@id", TankId, DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql);
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

		public async static Task<int> GetTankID(int PlayerTankId)
		{
			string sql = "SELECT tank.id " +
						 "FROM playerTank INNER JOIN tank ON playerTank.tankid = tank.id " +
						 "WHERE playerTank.Id=@PlayerTankId AND playerTank.playerId=@playerId; ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@PlayerTankId", PlayerTankId, DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql);
			int tankId = 0;
			if (dt.Rows.Count > 0) tankId = Convert.ToInt32(dt.Rows[0]["id"]);
			dt.Dispose();
			dt.Clear();
			return tankId;
		}

		public async static Task<bool> GetAchievmentExist(string achName)
		{
			bool exists = false;
			string sql = "SELECT ach.id FROM ach WHERE name=@name; ";
			DB.AddWithValue(ref sql, "@name", achName, DB.SqlDataType.VarChar);
			DataTable dt = await DB.FetchData(sql);
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

		public async static Task<bool> PlayerTankExists(int tankId)
		{
			string sql = "SELECT id FROM playerTank WHERE tankId=@tankId; ";
			DB.AddWithValue(ref sql, "@tankId", tankId, DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql);
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
