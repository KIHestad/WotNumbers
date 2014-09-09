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

		public static double CalcBattleEFF(string battleTimeFilter, int battleCount = 0, string battleMode = "15", string tankFilter = "")
		{
			double EFF = 0;
			string sql =
					"select battlesCount as battles, dmg, spotted as spot, frags, " +
					"  def, cap, tank.tier as tier , victory as wins " +
					"from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id left join " +
					"  tank on playerTank.tankId = tank.id " +
					"where playerId=@playerId and battleMode like @battleMode " + battleTimeFilter + " " + tankFilter + " order by battleTime DESC";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
			DataTable dtBattles = DB.FetchData(sql);
			if (dtBattles.Rows.Count > 0)
			{
				if (battleCount == 0) battleCount = dtBattles.Rows.Count;
				int count = 0;
				double BATTLES=0;
				double DAMAGE=0;
				double SPOT=0;
				double FRAGS=0;
				double DEF=0;
				double CAP=0;
				double WINS=0;
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

		public static double CalcBattleWN7(string battleTimeFilter, int battleCount = 0, string battleMode = "15", string tankFilter = "")
		{
			double WN7 = 0;
			string sql =
					"select battlesCount as battles, dmg, spotted as spot, frags, " +
					"  def, cap, tank.tier as tier , victory as wins " +
					"from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id left join " +
					"  tank on playerTank.tankId = tank.id " +
					"where playerId=@playerId and battleMode like @battleMode " + battleTimeFilter + " " + tankFilter + " order by battleTime DESC";
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

		public static double CalcBattleWN8(string battleTimeFilter, int battleCount = 0, string battleMode = "15", string tankFilter = "")
		{
			double WN8 = 0;
			// Create an empty datatable with all tanks, no values
			string sql =
				"select t.id as tankId, 0 as battles, 0 as dmg, 0 as spot, 0 as frags, " +
				"  0 as def, 0 as cap, 0 as wins " +
				"from playerTankBattle ptb left join " +
				"  playerTank pt on ptb.playerTankId=pt.id and pt.playerId=@playerId and ptb.battleMode like @battleMode left join " +
				"  tank t on pt.tankId = t.id " +
				"where t.expDmg is not null and ptb.battleMode like @battleMode " +
				"group by t.id ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);

			DataTable ptb = DB.FetchData(sql);
			// Get all battles
			sql =
				"select battlesCount as battles, dmg, spotted as spot, frags, " +
				"  def, cap, tank.tier as tier , victory as wins, tank.id as tankId " +
				"from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id left join " +
				"  tank on playerTank.tankId = tank.id " +
				"where playerId=@playerId and battleMode like @battleMode " + battleTimeFilter + " " + tankFilter + " order by battleTime DESC";
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
							Log.LogToFile("*** COULD NOT FIND TANK CALCULTATING WN8 RATING FOR GAUGE (" + tankId + ") ***");
					}
					count++;
					if (count > battleCount) break;
				}
				WN8 = Code.Rating.CalculatePlayerTankTotalWN8(ptb);
			}
			return WN8;
		}

		public static double CalculateTankWN8(int tankId, double battleCount, double dmg, double spotted, double frags, double def, double wins, bool WN8WRx = false)
		{
			Double WN8 = 0;
			// get tankdata for current tank
			DataRow tankInfo = TankData.TankInfo(tankId);
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
				WN8 = UseWN8Formula(dmg, spotted, frags, def, avgWinRate, expDmg, expSpot, expFrag, expDef, expWinRate);
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
					DataRow expected = TankData.TankInfo(tankId);
					if (battlecount > 0)
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
					if (battlecount > 0)
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
		
		#endregion

		#region WN7

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

		public static double CalculateTankEff(int tankId, double battleCount, double dmg, double spotted, double frags, double def, double cap)
		{
			// Get tankdata for current tank to get tier
			DataRow tankInfo = TankData.TankInfo(tankId);
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
		
		#endregion
					
		#region Color

		public static double[] rangeEFF = { 0, 305, 610, 850, 1145, 1300, 1475, 1775, 2000 };

		public static Color EffColor(double eff)
		{
			// Dynamic color by efficiency
			//  { "value": 610,  "color": ${"def.colorRating.very_bad" } },  //    0 - 609  - very bad   (20% of players)
			//  { "value": 850,  "color": ${"def.colorRating.bad"      } },  //  610 - 849  - bad        (better then 20% of players)
			//  { "value": 1145, "color": ${"def.colorRating.normal"   } },  //  850 - 1144 - normal     (better then 60% of players)
			//  { "value": 1475, "color": ${"def.colorRating.good"     } },  // 1145 - 1474 - good       (better then 90% of players)
			//  { "value": 1775, "color": ${"def.colorRating.very_good"} },  // 1475 - 1774 - very good  (better then 99% of players)
			//  { "value": 9999, "color": ${"def.colorRating.unique"   } }   // 1775 - *    - unique     (better then 99.9% of players)
			Color effRatingColor = ColorTheme.Rating_very_bad;
			if (eff >= rangeEFF[8]) effRatingColor = ColorTheme.Rating_super_uniqum;
			else if (eff >= rangeEFF[7]) effRatingColor = ColorTheme.Rating_uniqum;
			else if (eff >= rangeEFF[6]) effRatingColor = ColorTheme.Rating_great;
			else if (eff >= rangeEFF[5]) effRatingColor = ColorTheme.Rating_very_good;
			else if (eff >= rangeEFF[4]) effRatingColor = ColorTheme.Rating_good;
			else if (eff >= rangeEFF[3]) effRatingColor = ColorTheme.Rating_average;
			else if (eff >= rangeEFF[2]) effRatingColor = ColorTheme.Rating_below_average;
			else if (eff >= rangeEFF[1]) effRatingColor = ColorTheme.Rating_bad;
			return effRatingColor;
		}

		public static double[] rangeWN7 = { 0, 500, 700, 900, 1100, 1350, 1550, 1850, 2050 };

		public static Color WN7color(double wn7)
		{
			// http://wiki.wnefficiency.net/pages/Color_Scale
			Color wn7RatingColor = ColorTheme.Rating_very_bad;
			if (wn7 >= rangeWN7[8]) wn7RatingColor = ColorTheme.Rating_super_uniqum;
			else if (wn7 >= rangeWN7[7]) wn7RatingColor = ColorTheme.Rating_uniqum;
			else if (wn7 >= rangeWN7[6]) wn7RatingColor = ColorTheme.Rating_great;
			else if (wn7 >= rangeWN7[5]) wn7RatingColor = ColorTheme.Rating_very_good;
			else if (wn7 >= rangeWN7[4]) wn7RatingColor = ColorTheme.Rating_good;
			else if (wn7 >= rangeWN7[3]) wn7RatingColor = ColorTheme.Rating_average;
			else if (wn7 >= rangeWN7[2]) wn7RatingColor = ColorTheme.Rating_below_average;
			else if (wn7 >= rangeWN7[1]) wn7RatingColor = ColorTheme.Rating_bad;
			return wn7RatingColor;
		}

		public static double[] rangeWN8 = { 0, 300,600,900,1250,1600,1900,2350,2900 };

		public static Color WN8color(double wn8)
		{
			// http://wiki.wnefficiency.net/pages/Color_Scale
			Color wn8RatingColor = ColorTheme.Rating_very_bad;
			if (wn8 >= rangeWN8[8]) wn8RatingColor = ColorTheme.Rating_super_uniqum;
			else if (wn8 >= rangeWN8[7]) wn8RatingColor = ColorTheme.Rating_uniqum;
			else if (wn8 >= rangeWN8[6]) wn8RatingColor = ColorTheme.Rating_great;
			else if (wn8 >= rangeWN8[5]) wn8RatingColor = ColorTheme.Rating_very_good;
			else if (wn8 >= rangeWN8[4]) wn8RatingColor = ColorTheme.Rating_good;
			else if (wn8 >= rangeWN8[3]) wn8RatingColor = ColorTheme.Rating_average;
			else if (wn8 >= rangeWN8[2]) wn8RatingColor = ColorTheme.Rating_below_average;
			else if (wn8 >= rangeWN8[1]) wn8RatingColor = ColorTheme.Rating_bad;
			return wn8RatingColor;
		}

		public static double[] rangeWR = {0,45,47,49,52,54,56,60,65 };

		public static Color WinRateColor(double wr)
		{
			// http://wiki.wnefficiency.net/pages/Color_Scale
			Color wrRatingColor = ColorTheme.Rating_very_bad;
			if (wr >= rangeWR[8]) wrRatingColor = ColorTheme.Rating_super_uniqum;
			else if (wr >= rangeWR[7]) wrRatingColor = ColorTheme.Rating_uniqum;
			else if (wr >= rangeWR[6]) wrRatingColor = ColorTheme.Rating_great;
			else if (wr >= rangeWR[5]) wrRatingColor = ColorTheme.Rating_very_good;
			else if (wr >= rangeWR[4]) wrRatingColor = ColorTheme.Rating_good;
			else if (wr >= rangeWR[3]) wrRatingColor = ColorTheme.Rating_average;
			else if (wr >= rangeWR[2]) wrRatingColor = ColorTheme.Rating_below_average;
			else if (wr >= rangeWR[1]) wrRatingColor = ColorTheme.Rating_bad;
			return wrRatingColor;
		}

		public static Color BattleCountColor(int battleCount)
		{
			//// Dynamic color by kilo-battles
			//  { "value": 2,   "color": ${"def.colorRating.very_bad" } },   //  0 - 1
			//  { "value": 5,   "color": ${"def.colorRating.bad"      } },   //  2 - 4
			//  { "value": 9,   "color": ${"def.colorRating.normal"   } },   //  5 - 8
			//  { "value": 14,  "color": ${"def.colorRating.good"     } },   //  9 - 13
			//  { "value": 20,  "color": ${"def.colorRating.very_good"} },   // 14 - 19
			//  { "value": 999, "color": ${"def.colorRating.unique"   } }    // 20 - *
			double kBattles = Math.Round(Convert.ToDouble(battleCount / 1000), 0);
			Color battleCountRatingColor = ColorTheme.Rating_very_bad;
			if (kBattles > 20) battleCountRatingColor = ColorTheme.Rating_uniqum;
			else if (kBattles > 14) battleCountRatingColor = ColorTheme.Rating_great;
			else if (kBattles > 9) battleCountRatingColor = ColorTheme.Rating_good;
			else if (kBattles > 5) battleCountRatingColor = ColorTheme.Rating_below_average;
			else if (kBattles > 2) battleCountRatingColor = ColorTheme.Rating_bad;
			return battleCountRatingColor;
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
