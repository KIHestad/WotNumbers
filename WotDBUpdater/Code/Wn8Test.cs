using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater
{
    class Wn8Test
    {
        private static double ConvertDbVal2Double(object dbValue)
        {
            double value = 0;
            if (dbValue != DBNull.Value)
            {
                value = Convert.ToDouble(dbValue);
            }
            return value;
        }
        
        public static double CalculatePlayerTankWn8(int tankId, int totalBattleCount, DataRow playerTankData)
        {
            Double WN8 = 0;
            // get tankdata for current tank
            DataRow tankInfo = TankData.TankInfo(tankId);
            if (tankInfo != null && totalBattleCount > 0 && tankInfo["expDmg"] != DBNull.Value)
            {
                double avgDmg = (ConvertDbVal2Double(playerTankData["dmg15"]) + ConvertDbVal2Double(playerTankData["dmg7"])) / totalBattleCount;
                double avgSpot = (ConvertDbVal2Double(playerTankData["spot15"]) + ConvertDbVal2Double(playerTankData["spot7"])) / totalBattleCount;
                double avgFrag = (ConvertDbVal2Double(playerTankData["frags15"]) + ConvertDbVal2Double(playerTankData["frags7"])) / totalBattleCount;
                double avgDef = (ConvertDbVal2Double(playerTankData["def15"]) + ConvertDbVal2Double(playerTankData["def7"])) / totalBattleCount;
                double avgWinRate = (ConvertDbVal2Double(playerTankData["wins15"]) + ConvertDbVal2Double(playerTankData["wins7"])) / totalBattleCount;
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
                WN8 = 980 * rDAMAGEc + 210 * rDAMAGEc * rFRAGc + 155 * rFRAGc * rSPOTc + 75 * rDEFc * rFRAGc + 145 * Math.Min(1.8, rWINc);
                // Return value
            }
            return Convert.ToInt32(WN8);
        }

        public static double CalculateBattleWn8(int tankId, int battleCount, DataRow battleData)
        {
            Double WN8 = 0;
            // get tankdata for current tank
            DataRow tankInfo = TankData.TankInfo(tankId);
            if (tankInfo != null && battleCount > 0 && tankInfo["expDmg"] != DBNull.Value)
            {
                double avgDmg = (ConvertDbVal2Double(battleData["dmg"])) / battleCount;
                double avgSpot = (ConvertDbVal2Double(battleData["spotted"])) / battleCount;
                double avgFrag = (ConvertDbVal2Double(battleData["frags"])) / battleCount;
                double avgDef = (ConvertDbVal2Double(battleData["def"])) / battleCount;
                double avgWinRate = (ConvertDbVal2Double(battleData["victory"])) / battleCount;
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
                WN8 = 980 * rDAMAGEc + 210 * rDAMAGEc * rFRAGc + 155 * rFRAGc * rSPOTc + 75 * rDEFc * rFRAGc + 145 * Math.Min(1.8, rWINc);
                // Return value
            }
            return Convert.ToInt32(WN8);
        }
    }
}
