using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
		
		public static double CalculatePlayerTotalWn8()
		{
<<<<<<< HEAD
			string sql = "SELECT sum(ptb.battles * ptb.wn8) / sum(ptb.battles) " +
						 "FROM playerTankBattle ptb " +
						 "join playerTank pt on pt.id = ptb.playerTankId " +
						 "join tank t on t.id = pt.tankId";
			DataTable dtTotalWN8 = new DataTable();
			dtTotalWN8 = DB.FetchData(sql);
			int totalWN8 = (int)dtTotalWN8.Rows[0][0];

			return totalWN8;
=======
            string sql = "SELECT sum(battles * wn8) / sum(battles) FROM playerTankBattle";
            DataTable dtTotalWN8 = new DataTable();
            dtTotalWN8 = DB.FetchData(sql);
            int totalWN8 = (int)dtTotalWN8.Rows[0][0];
            return totalWN8;
>>>>>>> 5e59b28775c68799368df20cf861fbdc48724a0d
		}

		public static double CalculatePlayerTankWn8(int tankId, int totalBattleCount, DataRow playerTankBattle)
		{
			Double WN8 = 0;
			// get tankdata for current tank
			DataRow tankInfo = TankData.TankInfo(tankId);
			if (tankInfo != null && totalBattleCount > 0 && tankInfo["expDmg"] != DBNull.Value)
			{
				double avgDmg = 0;
				double avgSpot = 0;
				double avgFrag = 0;
				double avgDef = 0;
				double avgWinRate = 0;
				avgDmg = ConvertDbVal2Double(playerTankBattle["dmg"]) / totalBattleCount;
				avgSpot = ConvertDbVal2Double(playerTankBattle["spot"]) / totalBattleCount;
				avgFrag = ConvertDbVal2Double(playerTankBattle["frags"]) / totalBattleCount;
				avgDef = ConvertDbVal2Double(playerTankBattle["def"]) / totalBattleCount;
				avgWinRate = ConvertDbVal2Double(playerTankBattle["wins"]) / totalBattleCount * 100;
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

		public static double CalculatePlayerTotalEFF()
		{
<<<<<<< HEAD
			string sql = "SELECT sum(ptb.battles * ptb.eff) / sum(ptb.battles) " +
						 "FROM playerTankBattle ptb " +
						 "join playerTank pt on pt.id = ptb.playerTankId " +
						 "join tank t on t.id = pt.tankId";
			DataTable dtTotalEff = new DataTable();
			dtTotalEff = DB.FetchData(sql);
			int totalEff = (int)dtTotalEff.Rows[0][0];

			return totalEff;
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
=======
            string sql = "SELECT sum(battles * eff) / sum(battles) FROM playerTankBattle";
            DataTable dtTotalEff = new DataTable();
            dtTotalEff = DB.FetchData(sql);
            int totalEff = (int)dtTotalEff.Rows[0][0];
            return totalEff;
>>>>>>> 5e59b28775c68799368df20cf861fbdc48724a0d
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

					//EFF = FRAGS * (350.0 - TIER * 20.0)
					//    + DAMAGE * (0.2 + 1.5 / TIER)
					//    + 200.0 * SPOT
					//    + 15.0 * CAP
					//    + 15.0 * DEF;
			}
			// Return value
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

	}
}
