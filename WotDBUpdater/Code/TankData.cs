using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater.Code
{
	public static class TankData
	{
		#region DatabaseLookup

		public static DataTable tankList = new DataTable();

		public static void GetTankListFromDB()
		{
			tankList.Clear();
			tankList = db.FetchData("SELECT * FROM tank");
		}

		public static DataTable GetPlayerTankFromDB(int tankId)
		{
			using (SqlConnection conn = new SqlConnection(Config.DatabaseConnection()))
			{
				DataTable dt = new DataTable();
				conn.Open();
				SqlCommand command = new SqlCommand("SELECT * FROM playerTank WHERE playerId = " + Config.Settings.playerId + " AND tankId=" + tankId.ToString(), conn);
				SqlDataAdapter adapter = new SqlDataAdapter(command);
				adapter.Fill(dt);
				conn.Close();
				return dt;
			}
		}

		public static int GetPlayerTankCount()
		{
			using (SqlConnection conn = new SqlConnection(Config.DatabaseConnection()))
			{
				conn.Open();
				SqlCommand command = new SqlCommand("SELECT count(id) AS count FROM playerTank WHERE playerId = " + Config.Settings.playerId, conn);
				SqlDataReader myReader = command.ExecuteReader();
				int count = 0;
				while (myReader.Read())
				{
					count = Convert.ToInt32(myReader["count"]);
				}
				conn.Close();
				return count;
			}
		}

		public static int ConvertWs2TankId(int wsTankId, int wsCountryId)
		{
			using (SqlConnection conn = new SqlConnection(Config.DatabaseConnection()))
			{
				conn.Open();
				SqlCommand command = new SqlCommand("SELECT tankId " +
													"FROM wsTankId " +
													"WHERE wsTankId = " + wsTankId.ToString() + " AND wsCountryId = " + wsCountryId.ToString(), conn);
				SqlDataReader myReader = command.ExecuteReader();
				int lookupTankId = 0;
				while (myReader.Read())
				{
					lookupTankId = Convert.ToInt32(myReader["tankId"]);
				}
				conn.Close();
				return lookupTankId;
			}
		}


		public static int GetPlayerTankId(int tankId)
		{
			using (SqlConnection conn = new SqlConnection(Config.DatabaseConnection()))
			{
				conn.Open();
				SqlCommand command = new SqlCommand("SELECT playerTank.id AS playerTankId " +
													"FROM playerTank INNER JOIN tank ON playerTank.tankid = tank.id " + 
												    "WHERE tank.id = " + tankId, conn);
				SqlDataReader myReader = command.ExecuteReader();
				int lookupTankId = 0;
				while (myReader.Read())
				{
					lookupTankId = Convert.ToInt32(myReader["playerTankId"]);
				}
				conn.Close();
				return lookupTankId;
			}
		}

		public static DataTable GetBattleFromDB(int battleId)
		{
			using (SqlConnection conn = new SqlConnection(Config.DatabaseConnection()))
			{
				DataTable dt = new DataTable();
				conn.Open();
				SqlCommand command = new SqlCommand("SELECT * FROM battle WHERE id=" + battleId.ToString(), conn);
				SqlDataAdapter adapter = new SqlDataAdapter(command);
				adapter.Fill(dt);
				conn.Close();
				return dt;
			}
		}

		public static int GetBattleIdForImportedWsBattleFromDB(int wsId)
		{
			using (SqlConnection conn = new SqlConnection(Config.DatabaseConnection()))
			{
				conn.Open();
				SqlCommand command = new SqlCommand("SELECT Id FROM battle WHERE wsId=" + wsId.ToString(), conn);
				SqlDataReader myReader = command.ExecuteReader();
				int lookupBattle = 0;
				while (myReader.Read())
				{
					lookupBattle = Convert.ToInt32(myReader["Id"]);
				}
				conn.Close();
				return (lookupBattle);
			}
		}

		public static DataTable json2dbMappingView = new DataTable();
		
		public static void GetJson2dbMappingViewFromDB()
		{
			json2dbMappingView.Clear();
			json2dbMappingView = db.FetchData("SELECT * FROM json2dbMapping ORDER BY jsonMainSubProperty");
			try
			{
				json2dbMappingView.PrimaryKey = new DataColumn[] { json2dbMappingView.Columns["jsonMainSubProperty"] };
			}
			catch (Exception)
			{
				//throw;
			}
			
		}

		public static DataTable tankData2BattleMappingView = new DataTable();

		public static void GettankData2BattleMappingViewFromDB()
		{
			tankData2BattleMappingView.Clear();
			tankData2BattleMappingView = db.FetchData("SELECT * FROM tankData2BattleMappingView");
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

		public static bool TankExist(int tankID)
		{
			string expression = "id = " + tankID.ToString();
			DataRow[] foundRows = tankList.Select(expression);
			return (foundRows.Length > 0);
		}

		public static DataRow TankInfo(int tankID)
		{
			string expression = "id = " + tankID.ToString();
			DataRow[] foundRows = tankList.Select(expression);
			if (foundRows.Length > 0)
				return foundRows[0];
			else
				return null;
		}

		public static void SetPlayerTankAllAch()
		{
			// This makes sure all player tanks has all achievmenets - default value count=0
			string sql = "insert into playerTankAch (playerTankId, achId, achCount) " +
						"select playerTankAchAllView.playerTankId, playerTankAchAllView.achId, 0 from playerTankAchAllView left join " +
						"playerTankAch on playerTankAchAllView.playerTankId = playerTankAch.playerTankId and playerTankAchAllView.achId = playerTankAch.achId " +
						"where playerTankAch.playerTankId is null";
			SqlConnection con = new SqlConnection(Config.DatabaseConnection());
			con.Open();
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.ExecuteNonQuery();
			con.Close();
		}

		public static void SetPlayerTankAllAch(int playerTankId)
		{
			// This makes sure this player tanks has all achievmenets - default value count=0
			string sql = "insert into playerTankAch (playerTankId, achId, achCount) " +
						"select " + playerTankId.ToString() + ", ach.id, 0 from ach left join " +
						"playerTankAch on ach.id = playerTankAch.achId and playerTankAch.playerTankId = " + playerTankId.ToString() + " " +
						"where playerTankAch.playerTankId is null";
			db.ExecuteNonQuery(sql);
		}


		public static bool GetAchievmentExist(string achName)
		{
			bool exists = false;
			string sql = "SELECT ach.id " +
							"FROM ach  " +
							"WHERE name = '" + achName + "'";
			DataTable dt = db.FetchData(sql);
			exists = (dt.Rows.Count > 0);
			return exists;
		}

		
		#endregion

	   
	}
}
