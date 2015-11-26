using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinApp.Code;

namespace WinApp.Code
{
	class Rating
	{

		#region Convert DB Value to Double

		public static double ConvertDbVal2Double(object dbValue)
		{
			double value = 0;
			if (dbValue != DBNull.Value)
			{
				value = Convert.ToDouble(dbValue);
			}
			return value;
		}

		#endregion

		#region WN8

		public static double CalcAvgBattleWN8(string battleTimeFilter, int battleCount = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
		{
			double WN8 = 0;
			if (battleMode == "")
				battleMode = "%";
			// Create an empty datatable with all tanks, no values
			string sql =
				"select tank.id as tankId, 0 as battles, 0 as dmg, 0 as spot, 0 as frags, " +
				"  0 as def, 0 as cap, 0 as wins " +
				"from playerTankBattle ptb left join " +
				"  playerTank pt on ptb.playerTankId=pt.id and pt.playerId=@playerId and ptb.battleMode like @battleMode left join " +
				"  tank on pt.tankId = tank.id " +
                tankJoin + " " +
				"where tank.expDmg is not null and ptb.battleMode like @battleMode " +
				"group by tank.id ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);

			DataTable ptb = DB.FetchData(sql);
			// Get all battles
			sql =
				"select battlesCount as battles, dmg, spotted as spot, frags, " +
				"  def, cap, tank.tier as tier , victory as wins, tank.id as tankId " +
				"from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id left join " +
				"  tank on playerTank.tankId = tank.id " +
				"where playerId=@playerId and battleMode like @battleMode " + battleTimeFilter + " " + tankFilter + " " + battleModeFilter + " order by battleTime DESC";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
			DataTable dtBattles = DB.FetchData(sql);
			if (dtBattles.Rows.Count > 0)
			{
				if (battleCount == 0) battleCount = dtBattles.Rows.Count;
				int count = 0;
				foreach (DataRow stats in dtBattles.Rows)
				{
					double btl = Rating.ConvertDbVal2Double(stats["battles"]);
					// add to datatable
					string tankId = stats["tankId"].ToString();
					DataRow[] ptbRow = ptb.Select("tankId = " + tankId);
					if (ptbRow.Length > 0)
					{
						ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) + Convert.ToInt32(stats["battles"]);
						ptbRow[0]["dmg"] = Convert.ToInt32(ptbRow[0]["dmg"]) + Convert.ToInt32(stats["dmg"]) * btl;
						ptbRow[0]["spot"] = Convert.ToInt32(ptbRow[0]["spot"]) + Convert.ToInt32(stats["spot"]) * btl;
						ptbRow[0]["frags"] = Convert.ToInt32(ptbRow[0]["frags"]) + Convert.ToInt32(stats["frags"]) * btl;
						ptbRow[0]["def"] = Convert.ToInt32(ptbRow[0]["def"]) + Convert.ToInt32(stats["def"]) * btl;
						ptbRow[0]["cap"] = Convert.ToInt32(ptbRow[0]["cap"]) + Convert.ToInt32(stats["cap"]) * btl;
						ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) + Convert.ToInt32(stats["wins"]) * btl;
					}
					else
					{
						if (Config.Settings.showDBErrors)
							Log.LogToFile("*** Could not find playerTank for battle mode '" + battleMode + "' for tank: " + tankId + " ***");
					}
					count++;
					if (count > battleCount) break;
				}
				// Check for null values
				if (ptb.Rows.Count > 0)
					WN8 = Code.Rating.CalculatePlayerTankTotalWN8(ptb);
			}
			return WN8;
		}
				
		public static double CalculateTankWN8(int tankId, double battleCount, double dmg, double spotted, double frags, double def, double wins, bool WN8WRx = false)
		{
			Double WN8 = 0;
			// get tankdata for current tank
			DataRow tankInfo = TankHelper.TankInfo(tankId);
			if (tankInfo != null && battleCount > 0 && tankInfo["expDmg"] != DBNull.Value)
			{
				// WN8 = Winrate for tank(s)
				double avgWinRate = wins / battleCount * 100;
				// WN8 WRx = Winrate is fixed to the expected winRate 
				if (WN8WRx)
					avgWinRate = Convert.ToDouble(tankInfo["expWR"]);
				// get wn8 exp values for tank
				double expDmg = Convert.ToDouble(tankInfo["expDmg"]) * battleCount;
				double expSpot = Convert.ToDouble(tankInfo["expSpot"]) * battleCount;
				double expFrag = Convert.ToDouble(tankInfo["expFrags"]) * battleCount;
				double expDef = Convert.ToDouble(tankInfo["expDef"]) * battleCount;
				double expWinRate = Convert.ToDouble(tankInfo["expWR"]);
				// Use WN8 formula to calculate result
				WN8 = UseWN8Formula(dmg ,spotted ,frags ,def ,avgWinRate, expDmg, expSpot, expFrag, expDef, expWinRate);
			}
			return WN8;
		}

		public static double CalculatePlayerTankTotalWN8(DataTable playerTankBattle)
		{
			double WN8 = 0;
			// Get player totals from datatable
			if (playerTankBattle.Rows.Count > 0)
			{
				// Get player totals
				double totalBattles = Convert.ToDouble(playerTankBattle.Compute("SUM([battles])", ""));
				double dmg = Convert.ToDouble(playerTankBattle.Compute("SUM([dmg])", ""));
				double spotted = Convert.ToDouble(playerTankBattle.Compute("SUM([spot])", ""));
				double frags = Convert.ToDouble(playerTankBattle.Compute("SUM([frags])", ""));
				double def = Convert.ToDouble(playerTankBattle.Compute("SUM([def])", ""));
				double Wins = Convert.ToDouble(playerTankBattle.Compute("SUM([wins])", ""));
				double avgWinRate = Wins / totalBattles * 100;
				// Get tanks with battle count per tank and expected values from db
				double expDmg = 0;
				double expSpot = 0;
				double expFrag = 0;
				double expDef = 0;
				double expWinRate = 0;
				foreach (DataRow ptbRow in playerTankBattle.Rows)
				{
					// Get tanks with battle count per tank and expected values
					int tankId = Convert.ToInt32(ptbRow["tankId"]);
					double battlecount = Convert.ToDouble(ptbRow["battles"]);
					DataRow expected = TankHelper.TankInfo(tankId);
					if (battlecount > 0 && expected != null && expected["expDmg"] != DBNull.Value)
					{
						expDmg += Convert.ToDouble(expected["expDmg"]) * battlecount;
						expSpot += Convert.ToDouble(expected["expSpot"]) * battlecount;
						expFrag += Convert.ToDouble(expected["expFrags"]) * battlecount;
						expDef += Convert.ToDouble(expected["expDef"]) * battlecount;
						expWinRate += Convert.ToDouble(expected["expWR"]) * battlecount;
					}
				}
				// Use WN8 formula to calculate result
				if (totalBattles > 0)
					WN8 = UseWN8Formula(dmg, spotted, frags, def, avgWinRate, expDmg, expSpot, expFrag, expDef, (expWinRate / totalBattles));
			}
			return WN8;
		}
				
		public static double CalculatePlayerTotalWN8(string battleMode = "")
		{
			double WN8 = 0;
			// Get player totals from db
			string battleModeWhere = "";
			if (battleMode != "")
			{
				battleModeWhere += " and ptb.battleMode=@battleMode ";
				DB.AddWithValue(ref battleModeWhere, "@battleMode", battleMode, DB.SqlDataType.VarChar);
			}
			string sql = "select sum(battles) as battles, sum(dmg) as dmg, sum(spot) as spot, sum(frags) as frags, sum(def) as def, sum(wins) as wins " +
				"from playerTankBattle ptb left join " +
				"  playerTank pt on ptb.playerTankId=pt.id left join " +
				"  tank t on pt.tankId = t.id " +
				"where t.expDmg is not null and pt.playerId=@playerId " + battleModeWhere;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable playerTotalsTable = DB.FetchData(sql);
			if (playerTotalsTable.Rows.Count > 0)
			{
				// Get player totals
				DataRow playerTotals = playerTotalsTable.Rows[0];
				double totalBattles = Convert.ToDouble(playerTotals["battles"]);
				double dmg = Convert.ToDouble(playerTotals["dmg"]);
				double spotted = Convert.ToDouble(playerTotals["spot"]);
				double frags = Convert.ToDouble(playerTotals["frags"]);
				double def = Convert.ToDouble(playerTotals["def"]);
				double Wins = Convert.ToDouble(playerTotals["wins"]);
				double avgWinRate = Wins / totalBattles * 100;
				// Get tanks with battle count per tank and expected values from db
				sql =
					"select sum(ptb.battles) as battles, t.id, t.expDmg, t.expSpot, t.expFrags, t.expDef, t.expWR " +
					"from playerTankBattle ptb left join " +
					"  playerTank pt on ptb.playerTankId=pt.id left join " +
					"  tank t on pt.tankId = t.id " +
					"where t.expDmg is not null and pt.playerId=@playerId " + battleModeWhere +
					"group by t.id, t.expDmg, t.expSpot, t.expFrags, t.expDef, t.expWR  ";
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				DataTable expectedTable = DB.FetchData(sql);
				double expDmg = 0;
				double expSpot = 0;
				double expFrag = 0;
				double expDef = 0;
				double expWinRate = 0;
				foreach (DataRow expected in expectedTable.Rows)
				{
					// Get tanks with battle count per tank and expected values
					double battlecount = Convert.ToDouble(expected["battles"]);
					if (battlecount > 0 && expected["expDmg"] != DBNull.Value)
					{
						expDmg += Convert.ToDouble(expected["expDmg"]) * battlecount;
						expSpot += Convert.ToDouble(expected["expSpot"]) * battlecount;
						expFrag += Convert.ToDouble(expected["expFrags"]) * battlecount;
						expDef += Convert.ToDouble(expected["expDef"]) * battlecount;
						expWinRate += Convert.ToDouble(expected["expWR"]) * battlecount;
					}
				}
				// Use WN8 formula to calculate result
				WN8 = UseWN8Formula(dmg, spotted, frags, def, avgWinRate, expDmg, expSpot, expFrag, expDef, (expWinRate / totalBattles));
			}
			return WN8;
		}

		private static double UseWN8Formula(double dmg, double spotted, double frags, double def, double avgWinRate, double expDmg, double expSpot, double expFrag, double expDef, double expWinRate)
		{
			// Step 1
			double rDAMAGE = dmg / expDmg;
			double rSPOT = spotted / expSpot;
			double rFRAG = frags / expFrag;
			double rDEF = def / expDef;
			double rWIN = avgWinRate / expWinRate;
			// Step 2
			double rWINc = Math.Max(0, (rWIN - 0.71) / (1 - 0.71));
			double rDAMAGEc = Math.Max(0, (rDAMAGE - 0.22) / (1 - 0.22));
			double rFRAGc = Math.Max(0, Math.Min(rDAMAGEc + 0.2, (rFRAG - 0.12) / (1 - 0.12)));
			double rSPOTc = Math.Max(0, Math.Min(rDAMAGEc + 0.1, (rSPOT - 0.38) / (1 - 0.38)));
			double rDEFc = Math.Max(0, Math.Min(rDAMAGEc + 0.1, (rDEF - 0.10) / (1 - 0.10)));
			// Step 3
			double WN8 = (980 * rDAMAGEc) + (210 * rDAMAGEc * rFRAGc) + (155 * rFRAGc * rSPOTc) + (75 * rDEFc * rFRAGc) + (145 * Math.Min(1.8, rWINc));
			// Return value
			return WN8;
		}

		public static void UseWN8FormulaReturnResult(
			double dmg, double spotted, double frags, double def, double avgWinRate, 
			double expDmg, double expSpot, double expFrag, double expDef, double expWinRate,
			out double rWINc, out double rDAMAGEc, out double rFRAGc, out double rSPOTc, out double rDEFc)
		{
			// Step 1
			double rDAMAGE = dmg / expDmg;
			double rSPOT = spotted / expSpot;
			double rFRAG = frags / expFrag;
			double rDEF = def / expDef;
			double rWIN = avgWinRate / expWinRate;
			// Step 2
			double rWIN2 = Math.Max(0, (rWIN - 0.71) / (1 - 0.71));
			double rDAMAGE2 = Math.Max(0, (rDAMAGE - 0.22) / (1 - 0.22));
			double rFRAG2 = Math.Max(0, Math.Min(rDAMAGE2 + 0.2, (rFRAG - 0.12) / (1 - 0.12)));
			double rSPOT2 = Math.Max(0, Math.Min(rDAMAGE2 + 0.1, (rSPOT - 0.38) / (1 - 0.38)));
			double rDEF2 = Math.Max(0, Math.Min(rDAMAGE2 + 0.1, (rDEF - 0.10) / (1 - 0.10)));
			// Step 3
			rDAMAGEc = (980 * rDAMAGE2);
			rFRAGc = (210 * rDAMAGE2 * rFRAG2);
			rSPOTc = (155 * rFRAG2 * rSPOT2);
			rDEFc = (75 * rDEF2 * rFRAG2);
			rWINc = (145 * Math.Min(1.8, rWIN2));
			
		}
		

		#endregion

		#region WN7


		public static double CalcTotalWN7(string battleMode = "15")
		{
			string battleModeLike = battleMode;
			if (battleModeLike == "")
				battleModeLike = "%";
			string sql =
					"select sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
					"  sum (ptb.def) as def, sum (cap) as cap, sum(t.tier * ptb.battles) as tier, sum(ptb.wins) as wins " +
					"from playerTankBattle ptb left join " +
					"  playerTank pt on ptb.playerTankId=pt.id left join " +
					"  tank t on pt.tankId = t.id " +
					"where pt.playerId=@playerId and ptb.battleMode like @battleMode";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battleMode", battleModeLike, DB.SqlDataType.VarChar);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count == 0) 
				return 0;
			DataRow stats = dt.Rows[0];
			double BATTLES = Rating.ConvertDbVal2Double(stats["battles"]);
			double DAMAGE = Rating.ConvertDbVal2Double(stats["dmg"]);
			double SPOT = Rating.ConvertDbVal2Double(stats["spot"]);
			double FRAGS = Rating.ConvertDbVal2Double(stats["frags"]);
			double DEF = Rating.ConvertDbVal2Double(stats["def"]);
			double CAP = Rating.ConvertDbVal2Double(stats["cap"]);
			double WINS = Rating.ConvertDbVal2Double(stats["wins"]);
			double TIER = 0;
			if (BATTLES > 0)
				TIER = Rating.ConvertDbVal2Double(stats["tier"]) / BATTLES;
			return CalculateWN7(BATTLES, DAMAGE, SPOT, FRAGS, DEF, CAP, WINS, Rating.GetAverageBattleTier(battleMode));
		}

		public static double CalcBattleWN7(string battleTimeFilter, int battleCount = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
		{
			double WN7 = 0;
			if (battleMode == "")
				battleMode = "%";
			string sql =
					"select battlesCount as battles, dmg, spotted as spot, frags, " +
					"  def, cap, tank.tier as tier , victory as wins " +
					"from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id left join " +
					"  tank on playerTank.tankId = tank.id " +
                    tankJoin + " " +
					"where playerId=@playerId and battleMode like @battleMode " + battleTimeFilter + " " + tankFilter + " " + battleModeFilter + " order by battleTime DESC";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
			DataTable dtBattles = DB.FetchData(sql);
			if (dtBattles.Rows.Count > 0)
			{
				if (battleCount == 0) battleCount = dtBattles.Rows.Count;
				int count = 0;
				double BATTLES = 0;
				double DAMAGE = 0;
				double SPOT = 0;
				double FRAGS = 0;
				double DEF = 0;
				double CAP = 0;
				double WINS = 0;
				double TIER = 0;
				foreach (DataRow stats in dtBattles.Rows)
				{
					double btl = Rating.ConvertDbVal2Double(stats["battles"]);
					BATTLES += btl;
					DAMAGE += Rating.ConvertDbVal2Double(stats["dmg"]) * btl;
					SPOT += Rating.ConvertDbVal2Double(stats["spot"]) * btl;
					FRAGS += Rating.ConvertDbVal2Double(stats["frags"]) * btl;
					DEF += Rating.ConvertDbVal2Double(stats["def"]) * btl;
					CAP += Rating.ConvertDbVal2Double(stats["cap"]) * btl;
					WINS += Rating.ConvertDbVal2Double(stats["wins"]) * btl;
					TIER += Rating.ConvertDbVal2Double(stats["tier"]) * btl;
					count++;
					if (count > battleCount) break;
				}
				if (BATTLES > 0)
					WN7 = Code.Rating.CalculateWN7(BATTLES, DAMAGE, SPOT, FRAGS, DEF, CAP, WINS, (TIER / BATTLES));
			}
			return WN7;
		}

		public static double CalculateWN7(double battleCount, double dmg, double spotted, double frags, double def, double cap, double wins, double TIER, bool calcForBattle = false)
		{
			double WN7 = 0;
			if (battleCount > 0 && TIER > 0)
			{
				// Calc average values
				double DAMAGE = dmg / battleCount;
				double SPOT = spotted / battleCount;
				double FRAGS = frags / battleCount;
				double DEF = def / battleCount;
				double CAP = cap / battleCount;
				double WINRATE = wins / battleCount;
				// For battle calculations set WinRate to 50%
				if (battleCount == 1 || calcForBattle)
					WINRATE = 0.5; 
				// Calculate subvalues
				double WN7_Frags = (1240 - 1040 / Math.Pow(Math.Min(TIER, 6), 0.164)) * (FRAGS);
				double WN7_Damage = DAMAGE * 530 / (184 * Math.Exp(0.24 * TIER) + 130);
				double WN7_Spot = (SPOT) * 125 * (Math.Min(TIER, 3)) / 3;
				double WN7_Defense = Math.Min(DEF, 2.2) * 100;
				double WN7_Winrate = ((185 / (0.17 + Math.Exp(((WINRATE*100) - 35) * -0.134))) - 500) * 0.45;
				double WN7_LowTierPenalty = ((5 - Math.Min(TIER, 5)) * 125) / (1 + Math.Exp(TIER - Math.Pow(battleCount / 220, 3 / TIER)) * 1.5);
				// Find result
				WN7 = WN7_Frags + WN7_Damage + WN7_Spot + WN7_Defense + WN7_Winrate - WN7_LowTierPenalty;
			}
			// Return value
			return WN7;
		}

		#endregion

		#region EFF

		public static double CalcTotalEFF(string battleMode = "15")
		{
			if (battleMode == "")
				battleMode = "%";
			string sql =
					"select sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
					"  sum (ptb.def) as def, sum (cap) as cap, sum(t.tier * ptb.battles) as tier, sum(ptb.wins) as wins " +
					"from playerTankBattle ptb left join " +
					"  playerTank pt on ptb.playerTankId=pt.id left join " +
					"  tank t on pt.tankId = t.id " +
					"where pt.playerId=@playerId and ptb.battleMode like @battleMode";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count == 0) 
				return 0;
			DataRow stats = dt.Rows[0];
			double BATTLES = Rating.ConvertDbVal2Double(stats["battles"]);
			double DAMAGE = Rating.ConvertDbVal2Double(stats["dmg"]);
			double SPOT = Rating.ConvertDbVal2Double(stats["spot"]);
			double FRAGS = Rating.ConvertDbVal2Double(stats["frags"]);
			double DEF = Rating.ConvertDbVal2Double(stats["def"]);
			double CAP = Rating.ConvertDbVal2Double(stats["cap"]);
			double WINS = Rating.ConvertDbVal2Double(stats["wins"]);
			double TIER = 0;
			if (BATTLES > 0)
				TIER = Rating.ConvertDbVal2Double(stats["tier"]) / BATTLES;
			return CalculateEFF(BATTLES, DAMAGE, SPOT, FRAGS, DEF, CAP, TIER);
		}

		public static double CalcBattleEFF(string battleTimeFilter, int battleCount = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
		{
			double EFF = 0;
			if (battleMode == "")
				battleMode = "%";
			string sql =
					"select battlesCount as battles, dmg, spotted as spot, frags, " +
					"  def, cap, tank.tier as tier , victory as wins " +
					"from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id left join " +
					"  tank on playerTank.tankId = tank.id " +
                    tankJoin + " " +
					"where playerId=@playerId and battleMode like @battleMode " + battleTimeFilter + " " + tankFilter + " " + battleModeFilter + " order by battleTime DESC";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
			DataTable dtBattles = DB.FetchData(sql);
			if (dtBattles.Rows.Count > 0)
			{
				if (battleCount == 0) battleCount = dtBattles.Rows.Count;
				int count = 0;
				double BATTLES = 0;
				double DAMAGE = 0;
				double SPOT = 0;
				double FRAGS = 0;
				double DEF = 0;
				double CAP = 0;
				double WINS = 0;
				double TIER = 0;
				foreach (DataRow stats in dtBattles.Rows)
				{
					double btl = Rating.ConvertDbVal2Double(stats["battles"]);
					BATTLES += btl;
					DAMAGE += Rating.ConvertDbVal2Double(stats["dmg"]) * btl;
					SPOT += Rating.ConvertDbVal2Double(stats["spot"]) * btl;
					FRAGS += Rating.ConvertDbVal2Double(stats["frags"]) * btl;
					DEF += Rating.ConvertDbVal2Double(stats["def"]) * btl;
					CAP += Rating.ConvertDbVal2Double(stats["cap"]) * btl;
					WINS += Rating.ConvertDbVal2Double(stats["wins"]) * btl;
					TIER += Rating.ConvertDbVal2Double(stats["tier"]) * btl;
					count++;
					if (count > battleCount) break;
				}
				if (BATTLES > 0)
				{
					EFF = Code.Rating.CalculateEFF(BATTLES, DAMAGE, SPOT, FRAGS, DEF, CAP, TIER / BATTLES);
				}
			}
			return EFF;
		}

		public static double CalculateTankEFF(int tankId, double battleCount, double dmg, double spotted, double frags, double def, double cap)
		{
			// Get tankdata for current tank to get tier
			DataRow tankInfo = TankHelper.TankInfo(tankId);
			double tier = 0;
			if (tankInfo != null)
			{
				tier = Convert.ToDouble(tankInfo["tier"]);
			}
			// Call method for calc EFF
			return CalculateEFF(battleCount, dmg, spotted, frags, def, cap, tier);
		}
		
		public static double CalculateEFF(double battleCount, double dmg, double spotted, double frags, double def, double cap, double tier)
		{
			double EFF = 0;
			if (battleCount > 0)
			{
				double DAMAGE = dmg / battleCount;
				double SPOT = spotted / battleCount;
				double FRAGS = frags / battleCount;
				double DEF = def / battleCount;
				double CAP = cap / battleCount;
				// CALC
				EFF =
					DAMAGE * (10 / (tier + 2)) * (0.23 + 2 * tier / 100) +
					FRAGS * 250 +
					SPOT * 150 +
					Math.Log(CAP + 1, 1.732) * 150 +
					DEF * 150;
			}
			// Return value
			return EFF;
		}

        public static double CalcBattleWR(string battleTimeFilter, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
        {
            double WR = 0;
            if (battleMode == "")
                battleMode = "%";
            string sql =
                "select battlesCount as battles, victory as wins " +
                "from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id left join " +
                "  tank on playerTank.tankId = tank.id " +
                tankJoin + " " +
                "where playerId=@playerId and battleMode like @battleMode " + battleTimeFilter + " " + tankFilter + " " + battleModeFilter + " order by battleTime DESC";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
            DataTable dtBattles = DB.FetchData(sql);
            if (dtBattles.Rows.Count > 0)
            {
                double BATTLES = 0;
                double WINS = 0;
                foreach (DataRow stats in dtBattles.Rows)
                {
                    BATTLES += Rating.ConvertDbVal2Double(stats["battles"]);
                    WINS += Rating.ConvertDbVal2Double(stats["wins"]);
                }
                if (BATTLES > 0)
                {
                    WR = Math.Round(WINS / BATTLES * 100, 2);
                }
            }
            return WR;
        }

        public static double CalcTankWR(string battleTimeFilter, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
        {
            double WR = 0;
            if (battleMode == "")
                battleMode = "%";
            string sql = 
                "select battles, wins " +
                "from playerTankBattle " +
                "where playerTankId IN " +
                "  (select distinct playerTank.id " +
                "  from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id left join " +
                "    tank on playerTank.tankId = tank.id " +
                "  " + tankJoin + " " +
                "  where playerId=@playerId and battleMode like @battleMode " + battleTimeFilter + " " + tankFilter + " " + battleModeFilter + ")";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
            DataTable dtBattles = DB.FetchData(sql);
            if (dtBattles.Rows.Count > 0)
            {
                double BATTLES = 0;
                double WINS = 0;
                foreach (DataRow stats in dtBattles.Rows)
                {
                    BATTLES += Rating.ConvertDbVal2Double(stats["battles"]);
                    WINS += Rating.ConvertDbVal2Double(stats["wins"]);
                }
                if (BATTLES > 0)
                {
                    WR = Math.Round(WINS / BATTLES * 100, 2);
                }
            }
            return WR;
        }
        
        #endregion
					
		
		#region Recalculate Battles

		public static void RecalcBattlesWN7()
		{
			string sql = "select battle.*, playerTank.tankId as tankId from battle inner join playerTank on battle.playerTankId=playerTank.Id ORDER BY battle.id DESC";
			DataTable dtBattles = DB.FetchData(sql);
			foreach (DataRow battle in dtBattles.Rows)
			{
				// Get rating parameters
				string battleId = Convert.ToInt32(battle["id"]).ToString();
				double dmg = Rating.ConvertDbVal2Double(battle["dmg"]);
				double spotted = Rating.ConvertDbVal2Double(battle["spotted"]);
				double frags = Rating.ConvertDbVal2Double(battle["frags"]);
				double def = Rating.ConvertDbVal2Double(battle["def"]);
				double cap = Rating.ConvertDbVal2Double(battle["cap"]);
				double wins = Rating.ConvertDbVal2Double(battle["victory"]);
				double battlesCount = Rating.ConvertDbVal2Double(battle["battlesCount"]);
				// Calculate WN7
				string wn7 = Convert.ToInt32(Math.Round(Rating.CalculateWN7(battlesCount, dmg, spotted, frags, def, cap, wins, Rating.GetAverageBattleTier(), true), 0)).ToString();
				// Generate SQL to update WN7
				sql = "UPDATE battle SET wn7=" + wn7 + " WHERE id = " + battleId;
				DB.ExecuteNonQuery(sql);
			}
			dtBattles.Dispose();
			dtBattles.Clear();
		}

		public static double GetAverageBattleTier(string battleMode = "")
		{
			double tier = 0;
			// Get average battle tier, used for total player WN7 and battle WN7
			string battleModeWhere = "";
			if (battleMode != "")
			{
				battleModeWhere += " and ptb.battleMode=@battleMode ";
				DB.AddWithValue(ref battleModeWhere, "@battleMode", battleMode, DB.SqlDataType.VarChar);
			}
			string sql =
				"select sum(ptb.battles) as battles, sum(t.tier * ptb.battles) as tier " +
				"from playerTankBattle ptb left join " +
				"  playerTank pt on ptb.playerTankId=pt.id left join " +
				"  tank t on pt.tankId = t.id " +
				"where pt.playerId=@playerId " + battleModeWhere;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
			{
				double totalBattles = Convert.ToDouble(dt.Rows[0][0]);
				if (totalBattles > 0)
				{
					tier = Convert.ToDouble(dt.Rows[0][1]) / totalBattles;
				}
			}
			return tier;
		}

		#endregion
	}
}
