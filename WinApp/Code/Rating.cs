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
		private static int ConvertDbVal2Int32(object dbValue)
		{
			int value = 0;
			if (dbValue != DBNull.Value)
			{
				value = Convert.ToInt32(dbValue);
			}
			return value;
		}

		public static double ConvertDbVal2Double(object dbValue)
		{
			double value = 0;
			if (dbValue != DBNull.Value)
			{
				value = Convert.ToDouble(dbValue);
			}
			return value;
		}

		public static double CalculatePlayerTankWn8(int tankId, int tankBattleCount, DataRow playerTankBattle)
		{
			Double WN8 = 0;
			// get tankdata for current tank
			DataRow tankInfo = TankData.TankInfo(tankId);
			if (tankInfo != null && tankBattleCount > 0 && tankInfo["expDmg"] != DBNull.Value)
			{
				// get tank average values per battle
				double avgDmg = ConvertDbVal2Double(playerTankBattle["dmg"]) / tankBattleCount;
				double avgSpot = ConvertDbVal2Double(playerTankBattle["spot"]) / tankBattleCount;
				double avgFrag = ConvertDbVal2Double(playerTankBattle["frags"]) / tankBattleCount;
				double avgDef = ConvertDbVal2Double(playerTankBattle["def"]) / tankBattleCount;
				double avgWinRate = ConvertDbVal2Double(playerTankBattle["wins"]) / tankBattleCount * 100;
				// get wn8 exp values for tank
				double expDmg = Convert.ToDouble(tankInfo["expDmg"]);
				double expSpot = Convert.ToDouble(tankInfo["expSpot"]);
				double expFrag = Convert.ToDouble(tankInfo["expFrags"]);
				double expDef = Convert.ToDouble(tankInfo["expDef"]);
				double expWinRate = Convert.ToDouble(tankInfo["expWR"]);
				// Step 1
				double rDAMAGE = avgDmg / expDmg;
				double rSPOT = avgSpot / expSpot;
				double rFRAG = avgFrag / expFrag;
				double rDEF = avgDef / expDef;
				double rWIN = avgWinRate / expWinRate;
				// Step 2
				double rWINc = Math.Max(0, (rWIN - 0.71) / (1 - 0.71));
				double rDAMAGEc = Math.Max(0, (rDAMAGE - 0.22) / (1 - 0.22));
				double rFRAGc = Math.Max(0, Math.Min(rDAMAGEc + 0.2, (rFRAG - 0.12) / (1 - 0.12)));
				double rSPOTc = Math.Max(0, Math.Min(rDAMAGEc + 0.1, (rSPOT - 0.38) / (1 - 0.38)));
				double rDEFc = Math.Max(0, Math.Min(rDAMAGEc + 0.1, (rDEF - 0.10) / (1 - 0.10)));
				// Step 3
				WN8 = (980 * rDAMAGEc) + (210 * rDAMAGEc * rFRAGc) + (155 * rFRAGc * rSPOTc) + (75 * rDEFc * rFRAGc) + (145 * Math.Min(1.8, rWINc));
				// Return value
			}
			return Convert.ToInt32(WN8);
		}

		public static double CalculatePlayerTotalWn8()
		{
			double WN8 = 0;
			// Get player totals from db
			string sql = "select sum(battles) as battles, sum(dmg) as dmg, sum(spot) as spot, sum(frags) as frags, sum(def) as def, sum(wins) as wins " +
				"from playerTankBattle ptb left join " +
				"  playerTank pt on ptb.playerTankId=pt.id left join " +
				"  tank t on pt.tankId = t.id " +
				"where t.expDmg is not null and pt.playerId=@playerId ";
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DataTable playerTotalsTable = DB.FetchData(sql);
			if (playerTotalsTable.Rows.Count > 0)
			{
				// Get player totals
				DataRow playerTotals = playerTotalsTable.Rows[0];
				double totalBattles = Convert.ToDouble(playerTotals["battles"]);
				double Dmg = Convert.ToDouble(playerTotals["dmg"]);
				double Spot = Convert.ToDouble(playerTotals["spot"]);
				double Frag = Convert.ToDouble(playerTotals["frags"]);
				double Def = Convert.ToDouble(playerTotals["def"]);
				double Wins = Convert.ToDouble(playerTotals["wins"]);
				double WinRate = Wins / totalBattles * 100;
				// Get tanks with battle count per tank and expected values from db
				sql =
					"select sum(ptb.battles) as battles, t.id, t.expDmg, t.expSpot, t.expFrags, t.expDef, t.expWR " +
					"from playerTankBattle ptb left join " +
					"  playerTank pt on ptb.playerTankId=pt.id left join " +
					"  tank t on pt.tankId = t.id " +
					"where t.expDmg is not null and pt.playerId=@playerId " +
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
				// Step 1
				double rDAMAGE = Dmg / expDmg ;
				double rSPOT = Spot / expSpot;
				double rFRAG = Frag / expFrag;
				double rDEF = Def / expDef;
				double rWIN = WinRate / (expWinRate / totalBattles);
				// Step 2
				double rWINc = Math.Max(0, (rWIN - 0.71) / (1 - 0.71));
				double rDAMAGEc = Math.Max(0, (rDAMAGE - 0.22) / (1 - 0.22));
				double rFRAGc = Math.Max(0, Math.Min(rDAMAGEc + 0.2, (rFRAG - 0.12) / (1 - 0.12)));
				double rSPOTc = Math.Max(0, Math.Min(rDAMAGEc + 0.1, (rSPOT - 0.38) / (1 - 0.38)));
				double rDEFc = Math.Max(0, Math.Min(rDAMAGEc + 0.1, (rDEF - 0.10) / (1 - 0.10)));
				// Step 3
				WN8 = ((980 * rDAMAGEc) + (210 * rDAMAGEc * rFRAGc) + (155 * rFRAGc * rSPOTc) + (75 * rDEFc * rFRAGc) + (145 * Math.Min(1.8, rWINc)));
				// Return value
			}
			return WN8;
		}

		public static double CalculatePlayerTotalWn8(DataTable playerTankBattle)
		{
			double WN8 = 0;
			// Get player totals from datatable
			if (playerTankBattle.Rows.Count > 0)
			{
				// Get player totals
				double totalBattles = Convert.ToDouble(playerTankBattle.Compute("SUM([battles])",""));
				double Dmg = Convert.ToDouble(playerTankBattle.Compute("SUM([dmg])",""));
				double Spot = Convert.ToDouble(playerTankBattle.Compute("SUM([spot])",""));
				double Frag = Convert.ToDouble(playerTankBattle.Compute("SUM([frags])",""));
				double Def = Convert.ToDouble(playerTankBattle.Compute("SUM([def])",""));
				double Wins = Convert.ToDouble(playerTankBattle.Compute("SUM([wins])",""));
				double WinRate = Wins / totalBattles * 100;
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
				// Step 1
				double rDAMAGE = Dmg / expDmg;
				double rSPOT = Spot / expSpot;
				double rFRAG = Frag / expFrag;
				double rDEF = Def / expDef;
				double rWIN = WinRate / (expWinRate / totalBattles);
				// Step 2
				double rWINc = Math.Max(0, (rWIN - 0.71) / (1 - 0.71));
				double rDAMAGEc = Math.Max(0, (rDAMAGE - 0.22) / (1 - 0.22));
				double rFRAGc = Math.Max(0, Math.Min(rDAMAGEc + 0.2, (rFRAG - 0.12) / (1 - 0.12)));
				double rSPOTc = Math.Max(0, Math.Min(rDAMAGEc + 0.1, (rSPOT - 0.38) / (1 - 0.38)));
				double rDEFc = Math.Max(0, Math.Min(rDAMAGEc + 0.1, (rDEF - 0.10) / (1 - 0.10)));
				// Step 3
				WN8 = ((980 * rDAMAGEc) + (210 * rDAMAGEc * rFRAGc) + (155 * rFRAGc * rSPOTc) + (75 * rDEFc * rFRAGc) + (145 * Math.Min(1.8, rWINc)));
				// Return value
			}
			return WN8;
		}


		public static double CalculatePlayerEFFforChart(double sumBattleCount, double sumDAMAGE, double sumSPOT, double sumFRAGS, double sumDEF, double sumCAP, double TIER = 0)
		{
			Double EFF = 0;
			if (sumBattleCount > 0)
			{
				double DAMAGE = sumDAMAGE / sumBattleCount;
				double SPOT = sumSPOT / sumBattleCount;
				double FRAGS = sumFRAGS / sumBattleCount;
				double DEF = sumDEF / sumBattleCount;
				double CAP = sumCAP / sumBattleCount;
				// CALC
				if (TIER == 0)
					EFF =
						DAMAGE + 
						FRAGS * 250 +
						SPOT * 150 +
						Math.Log(CAP + 1, 1.732) * 150 +
						DEF * 150;
				else
					EFF =
						DAMAGE * (10 / (TIER + 2)) * (0.23 + 2 * TIER / 100) +
						FRAGS * 250 +
						SPOT * 150 +
						Math.Log(CAP + 1, 1.732) * 150 +
						DEF * 150;
			}
			// Return value
			return EFF;
		}

		public static double CalculatePlayerTankEff(int tankId, int totalBattleCount, DataRow playerTankData)
		{
			Double EFF = 0;
			// get tankdata for current tank
			DataRow tankInfo = TankData.TankInfo(tankId);
			int tankTier = Convert.ToInt32(tankInfo["tier"]);
			if (tankInfo != null && totalBattleCount > 0)
			{
				double TIER = tankTier;
				double DAMAGE = 0;
				double SPOT = 0;
				double FRAGS = 0;
				double DEF = 0;
				double CAP = 0;
				DAMAGE = ConvertDbVal2Double(playerTankData["dmg"]) / totalBattleCount;
				SPOT = ConvertDbVal2Double(playerTankData["spot"]) / totalBattleCount;
				FRAGS = ConvertDbVal2Double(playerTankData["frags"]) / totalBattleCount;
				DEF = ConvertDbVal2Double(playerTankData["def"]) / totalBattleCount;
				CAP = ConvertDbVal2Double(playerTankData["cap"]) / totalBattleCount;
				// CALC
					EFF =
						DAMAGE * (10 / (TIER + 2)) * (0.23 + 2 * TIER / 100) +
						FRAGS * 250 +
						SPOT * 150 +
						Math.Log(CAP + 1, 1.732) * 150 +
						DEF * 150;
			}
			return Convert.ToInt32(EFF);
		}


		public static int CalculateBattleWn8(int tankId, int battleCount, DataRow battleData)
		{
			int dmg = ConvertDbVal2Int32(battleData["dmg"]) ;
			int spotted = ConvertDbVal2Int32(battleData["spotted"]);
			int frags = ConvertDbVal2Int32(battleData["frags"]);
			int def = ConvertDbVal2Int32(battleData["def"]);
			return CalculateBattleWn8(tankId, battleCount, dmg, spotted, frags, def);
		}

		public static int CalculateBattleWn8(int tankId, int battleCount, int dmg, int spotted, int frags, int def)
		{
			Double WN8 = 0;
			// get tankdata for current tank
			DataRow tankInfo = TankData.TankInfo(tankId);
			if (tankInfo != null && battleCount > 0 && tankInfo["expDmg"] != DBNull.Value)
			{
				double avgDmg = dmg / battleCount;
				double avgSpot = spotted / battleCount;
				double avgFrag = frags / battleCount;
				double avgDef = def / battleCount;
				//double avgWinRate = (ConvertDbVal2Double(battleData["victory"])) / battleCount * 100;
				// WN8 WRx = Winrate is fixed to the expected winRate 
				double avgWinRate = Convert.ToDouble(tankInfo["expWR"]);
				// get wn8 exp values for tank
				double expDmg = Convert.ToDouble(tankInfo["expDmg"]);
				double expSpot = Convert.ToDouble(tankInfo["expSpot"]);
				double expFrag = Convert.ToDouble(tankInfo["expFrags"]);
				double expDef = Convert.ToDouble(tankInfo["expDef"]);
				double expWinRate = Convert.ToDouble(tankInfo["expWR"]);
				// Step 1
				double rDAMAGE = avgDmg / expDmg;
				double rSPOT = avgSpot / expSpot;
				double rFRAG = avgFrag / expFrag;
				double rDEF = avgDef / expDef;
				double rWIN = avgWinRate / expWinRate;
				// Step 2
				double rWINc = Math.Max(0, (rWIN - 0.71) / (1 - 0.71));
				double rDAMAGEc = Math.Max(0, (rDAMAGE - 0.22) / (1 - 0.22));
				double rFRAGc = Math.Max(0, Math.Min(rDAMAGEc + 0.2, (rFRAG - 0.12) / (1 - 0.12)));
				double rSPOTc = Math.Max(0, Math.Min(rDAMAGEc + 0.1, (rSPOT - 0.38) / (1 - 0.38)));
				double rDEFc = Math.Max(0, Math.Min(rDAMAGEc + 0.1, (rDEF - 0.10) / (1 - 0.10)));
				// Step 3
				WN8 = (980 * rDAMAGEc) + (210 * rDAMAGEc * rFRAGc) + (155 * rFRAGc * rSPOTc) + (75 * rDEFc * rFRAGc) + (145 * Math.Min(1.8, rWINc));
				// Return value
			}
			return Convert.ToInt32(WN8);
		}

		public static int CalculateBattleEff(int tankId, int battleCount, DataRow battleData)
		{
			int DAMAGE = ConvertDbVal2Int32(battleData["dmg"]);
			int SPOT = ConvertDbVal2Int32(battleData["spotted"]);
			int FRAGS = ConvertDbVal2Int32(battleData["frags"]);
			int DEF = ConvertDbVal2Int32(battleData["def"]);
			int CAP = ConvertDbVal2Int32(battleData["cap"]);
			return CalculateBattleEff(tankId, battleCount, DAMAGE, SPOT, FRAGS, DEF, CAP);
		}

		public static int CalculateBattleEff(int tankId, int battleCount, int dmg, int spotted, int frags, int def, int cap)
		{
			Double EFF = 0;
			// get tankdata for current tank
			DataRow tankInfo = TankData.TankInfo(tankId);
			int tankTier = Convert.ToInt32(tankInfo["tier"]);
			if (tankInfo != null && battleCount > 0 && tankInfo["expDmg"] != DBNull.Value)
			{
				double TIER = tankTier;
				double DAMAGE = dmg / battleCount;
				double SPOT = spotted / battleCount;
				double FRAGS = frags / battleCount;
				double DEF = def / battleCount;
				double CAP = cap / battleCount;
				// CALC
				EFF =
						DAMAGE * (10 / (TIER + 2)) * (0.23 + 2 * TIER / 100) +
						FRAGS * 250 +
						SPOT * 150 +
						Math.Log(CAP + 1, 1.732) * 150 +
						DEF * 150;
			}
			// Return value
			return Convert.ToInt32(EFF);
		}

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
			if (eff > 1774) effRatingColor = ColorTheme.Rating_uniqe;
			else if (eff > 1474) effRatingColor = ColorTheme.Rating_very_good;
			else if (eff > 1144) effRatingColor = ColorTheme.Rating_good;
			else if (eff > 849) effRatingColor = ColorTheme.Rating_normal;
			else if (eff > 609) effRatingColor = ColorTheme.Rating_bad;
			return effRatingColor;
		}

		public static Color WN8color(double wn8)
		{
			// Dynamic color by WN8 rating
			//	{ "value": 310,  "color": ${"def.colorRating.very_bad" } },  //    0 - 309  - very bad   (20% of players)
			//	{ "value": 750,  "color": ${"def.colorRating.bad"      } },  //  310 - 749  - bad        (better then 20% of players)
			//	{ "value": 1310, "color": ${"def.colorRating.normal"   } },  //  750 - 1309 - normal     (better then 60% of players)
			//	{ "value": 1965, "color": ${"def.colorRating.good"     } },  // 1310 - 1964 - good       (better then 90% of players)
			//	{ "value": 2540, "color": ${"def.colorRating.very_good"} },  // 1965 - 2539 - very good  (better then 99% of players)
			//	{ "value": 9999, "color": ${"def.colorRating.unique"   } }   // 2540 - *    - unique     (better then 99.9% of players)
			Color wn8RatingColor = ColorTheme.Rating_very_bad;
			if (wn8 > 2539) wn8RatingColor = ColorTheme.Rating_uniqe;
			else if (wn8 > 1964) wn8RatingColor = ColorTheme.Rating_very_good;
			else if (wn8 > 1309) wn8RatingColor = ColorTheme.Rating_good;
			else if (wn8 > 749) wn8RatingColor = ColorTheme.Rating_normal;
			else if (wn8 > 309) wn8RatingColor = ColorTheme.Rating_bad;
			return wn8RatingColor;
		}

		public static Color WinRateColor(double wr)
		{
			//// Dynamic color by win percent
			//  { "value": 46.5, "color": ${"def.colorRating.very_bad" } },   //  0   - 46.5  - very bad   (20% of players)
			//  { "value": 48.5, "color": ${"def.colorRating.bad"      } },   // 46.5 - 48.5  - bad        (better then 20% of players)
			//  { "value": 51.5, "color": ${"def.colorRating.normal"   } },   // 48.5 - 51.5  - normal     (better then 60% of players)
			//  { "value": 56.5, "color": ${"def.colorRating.good"     } },   // 51.5 - 56.5  - good       (better then 90% of players)
			//  { "value": 64.5, "color": ${"def.colorRating.very_good"} },   // 56.5 - 64.5  - very good  (better then 99% of players)
			//  { "value": 101,  "color": ${"def.colorRating.unique"   } }    // 64.5 - 100   - unique     (better then 99.9% of players)
			Color wrRatingColor = ColorTheme.Rating_very_bad;
			if (wr > 64.5) wrRatingColor = ColorTheme.Rating_uniqe;
			else if (wr > 56.5) wrRatingColor = ColorTheme.Rating_very_good;
			else if (wr > 51.5) wrRatingColor = ColorTheme.Rating_good;
			else if (wr > 48.5) wrRatingColor = ColorTheme.Rating_normal;
			else if (wr > 46.5) wrRatingColor = ColorTheme.Rating_bad;
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
			if (kBattles > 20) battleCountRatingColor = ColorTheme.Rating_uniqe;
			else if (kBattles > 14) battleCountRatingColor = ColorTheme.Rating_very_good;
			else if (kBattles > 9) battleCountRatingColor = ColorTheme.Rating_good;
			else if (kBattles > 5) battleCountRatingColor = ColorTheme.Rating_normal;
			else if (kBattles > 2) battleCountRatingColor = ColorTheme.Rating_bad;
			return battleCountRatingColor;
		}

	}
}
