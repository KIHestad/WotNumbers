using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WinApp.Code.Rating
{
    public class WN8
    {

        #region params

        public class RatingParametersWN8
        {
            public RatingParametersWN8()
            {
                rp = new WNHelper.RatingParameters();
                expDmg = 0;
                expSpot = 0;
                expFrag = 0;
                expDef = 0;
                expWinRate = 0;
            }
            public WNHelper.RatingParameters rp { get; set; }
            public double expWinRate { get; set; }
            public double expDmg { get; set; }
            public double expFrag { get; set; }
            public double expSpot { get; set; }
            public double expDef { get; set; }
        }



        public static RatingParametersWN8 GetParamForPlayerTankBattle(DataTable playerTankBattle)
        {
            RatingParametersWN8 rpWN = null;
            // Get player totals from datatable
            if (playerTankBattle.Rows.Count > 0)
            {
                // Get player totals
                rpWN = new RatingParametersWN8();
                rpWN.rp.BATTLES = Convert.ToDouble(playerTankBattle.Compute("SUM([battles])", ""));
                rpWN.rp.DAMAGE = Convert.ToDouble(playerTankBattle.Compute("SUM([dmg])", ""));
                rpWN.rp.SPOT = Convert.ToDouble(playerTankBattle.Compute("SUM([spot])", ""));
                rpWN.rp.FRAGS = Convert.ToDouble(playerTankBattle.Compute("SUM([frags])", ""));
                rpWN.rp.DEF = Convert.ToDouble(playerTankBattle.Compute("SUM([def])", ""));
                rpWN.rp.WINS = Convert.ToDouble(playerTankBattle.Compute("SUM([wins])", ""));
                // Get tanks with battle count per tank and expected values from db
                foreach (DataRow ptbRow in playerTankBattle.Rows)
                {
                    // Get tanks with battle count per tank and expected values
                    int tankId = Convert.ToInt32(ptbRow["tankId"]);
                    double battlecount = Convert.ToDouble(ptbRow["battles"]);
                    DataRow expected = TankHelper.TankInfo(tankId);
                    if (battlecount > 0 && expected != null && expected["expDmg"] != DBNull.Value)
                    {
                        rpWN.expDmg += Convert.ToDouble(expected["expDmg"]) * battlecount;
                        rpWN.expSpot += Convert.ToDouble(expected["expSpot"]) * battlecount;
                        rpWN.expFrag += Convert.ToDouble(expected["expFrags"]) * battlecount;
                        rpWN.expDef += Convert.ToDouble(expected["expDef"]) * battlecount;
                        rpWN.expWinRate += Convert.ToDouble(expected["expWR"]) * battlecount;
                    }
                }
            }
            return rpWN;
        }

        public static RatingParametersWN8 GetParamForPlayerTotal(string battleMode)
        {
            RatingParametersWN8 rpWN = new RatingParametersWN8();
            // Get player totals from db
            rpWN.rp = WNHelper.GetParamForPlayerTankBattle(battleMode, excludeIfWN8ExpDmgIsNull: true);
            if (rpWN.rp == null)
                return null;
            // Get tanks with battle count per tank and expected values from db
            string battleModeWhere = "";
            if (battleMode != "")
            {
                battleModeWhere = " and ptb.battleMode=@battleMode ";
                DB.AddWithValue(ref battleModeWhere, "@battleMode", battleMode, DB.SqlDataType.VarChar);
            }
            string sql =
                "select sum(ptb.battles) as battles, t.id, t.expDmg, t.expSpot, t.expFrags, t.expDef, t.expWR " +
                "from playerTankBattle ptb left join " +
                "  playerTank pt on ptb.playerTankId=pt.id left join " +
                "  tank t on pt.tankId = t.id " +
                "where t.expDmg is not null and pt.playerId=@playerId " + battleModeWhere +
                "group by t.id, t.expDmg, t.expSpot, t.expFrags, t.expDef, t.expWR  ";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DataTable expectedTable = DB.FetchData(sql);
            foreach (DataRow expected in expectedTable.Rows)
            {
                // Get tanks with battle count per tank and expected values
                double battlecount = Convert.ToDouble(expected["battles"]);
                if (battlecount > 0 && expected["expDmg"] != DBNull.Value)
                {
                    rpWN.expDmg += Convert.ToDouble(expected["expDmg"]) * battlecount;
                    rpWN.expSpot += Convert.ToDouble(expected["expSpot"]) * battlecount;
                    rpWN.expFrag += Convert.ToDouble(expected["expFrags"]) * battlecount;
                    rpWN.expDef += Convert.ToDouble(expected["expDef"]) * battlecount;
                    rpWN.expWinRate += Convert.ToDouble(expected["expWR"]) * battlecount;
                }
            }
            return rpWN;
        }

        #endregion

        #region calc

        public static double CalcPlayerTotal(string battleMode = "")
        {
            double WN8 = 0;
            RatingParametersWN8 rpWN = GetParamForPlayerTotal(battleMode);
            // Use WN8 formula to calculate result
            WN8 = UseFormula(rpWN);
            return WN8;
        }

        public static double CalcBattle(int tankId, WNHelper.RatingParameters ratingParameters)
        {
            WNHelper.RatingParameters rp = new WNHelper.RatingParameters(ratingParameters); // clone it to not affect input class
            bool WN8WRx = false;
            if (rp.BATTLES == 1)
                WN8WRx = true;
            else
            {
                rp.CAP = rp.CAP * rp.BATTLES;
                rp.DAMAGE = rp.DAMAGE * rp.BATTLES;
                rp.DEF = rp.DEF * rp.BATTLES;
                rp.FRAGS = rp.FRAGS * rp.BATTLES;
                rp.SPOT = rp.SPOT * rp.BATTLES;
                rp.WINS = rp.WINS * rp.BATTLES;
            }
            return CalcTank(tankId, rp, WN8WRx);
        }

        public static double CalcTank(int tankId, WNHelper.RatingParameters ratingParameters, bool WN8WRx = false)
        {
            Double WN8 = 0;
            WNHelper.RatingParameters rp = new WNHelper.RatingParameters(ratingParameters); // clone it to not affect input class
            RatingParametersWN8 rpWN = new RatingParametersWN8();
            rpWN.rp = rp;
            // get tankdata for current tank
            DataRow tankInfo = TankHelper.TankInfo(tankId);
            if (tankInfo != null && rp.BATTLES > 0 && tankInfo["expDmg"] != DBNull.Value)
            {
                // WN8 WRx = Winrate is fixed to the expected winRate 
                if (WN8WRx)
                    rp.WINS = Convert.ToDouble(tankInfo["expWR"]) / 100 * rp.BATTLES;
                // get wn8 exp values for tank
                rpWN.expDmg = Convert.ToDouble(tankInfo["expDmg"]) * rp.BATTLES;
                rpWN.expSpot = Convert.ToDouble(tankInfo["expSpot"]) * rp.BATTLES;
                rpWN.expFrag = Convert.ToDouble(tankInfo["expFrags"]) * rp.BATTLES;
                rpWN.expDef = Convert.ToDouble(tankInfo["expDef"]) * rp.BATTLES;
                rpWN.expWinRate = Convert.ToDouble(tankInfo["expWR"]) * rp.BATTLES;
                // Use WN8 formula to calculate result
                WN8 = UseFormula(rpWN);
            }
            return WN8;
        }


        public static double CalcBattleRange(string battleTimeFilter, int maxBattles = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
        {
            DataTable ptb = WNHelper.GetDataForBattleRange(battleTimeFilter, maxBattles, battleMode, tankFilter, battleModeFilter, tankJoin);
            return CalcPlayerTankBattle(ptb);
        }

        public static double CalcBattleRangeReverse(string battleTimeFilter, int battleCount = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
        {
            DataTable ptb = WNHelper.GetDataForPlayerTankBattleReverse(battleTimeFilter, battleCount, battleMode, tankFilter, battleModeFilter, tankJoin);
            return CalcPlayerTankBattle(ptb);
        }


        public static double CalcPlayerTankBattle(DataTable playerTankBattle)
        {
            // Get WN rating parameters using datatable containing playerTankBattle
            RatingParametersWN8 rpWN = GetParamForPlayerTankBattle(playerTankBattle);
            // Use WN8 formula to calculate result
            return UseFormula(rpWN);
        }

        private static double UseFormula(RatingParametersWN8 rpWN)
        {
            double WN8 = 0;
            if (rpWN != null && rpWN.rp.BATTLES > 0)
            {
                // Step 1
                double rDAMAGE = rpWN.rp.DAMAGE / rpWN.expDmg;
                double rSPOT = rpWN.rp.SPOT / rpWN.expSpot;
                double rFRAG = rpWN.rp.FRAGS / rpWN.expFrag;
                double rDEF = rpWN.rp.DEF / rpWN.expDef;
                double rWIN = (rpWN.rp.WINS / rpWN.rp.BATTLES * 100) / (rpWN.expWinRate / rpWN.rp.BATTLES);
                // Step 2
                double rWINc = Math.Max(0, (rWIN - 0.71) / (1 - 0.71));
                double rDAMAGEc = Math.Max(0, (rDAMAGE - 0.22) / (1 - 0.22));
                double rFRAGc = Math.Max(0, Math.Min(rDAMAGEc + 0.2, (rFRAG - 0.12) / (1 - 0.12)));
                double rSPOTc = Math.Max(0, Math.Min(rDAMAGEc + 0.1, (rSPOT - 0.38) / (1 - 0.38)));
                double rDEFc = Math.Max(0, Math.Min(rDAMAGEc + 0.1, (rDEF - 0.10) / (1 - 0.10)));
                // Step 3
                WN8 = (980 * rDAMAGEc) + (210 * rDAMAGEc * rFRAGc) + (155 * rFRAGc * rSPOTc) + (75 * rDEFc * rFRAGc) + (145 * Math.Min(1.8, rWINc));
            }
            // Return value
            return WN8;
        }

        public static void UseFormulaReturnResult(
            WNHelper.RatingParameters rp, double avgWinRate, double expDmg, double expSpot, double expFrag, double expDef, double expWinRate,
            out double rWINc, out double rDAMAGEc, out double rFRAGc, out double rSPOTc, out double rDEFc)
        {
            // Step 1
            double rDAMAGE = rp.DAMAGE / expDmg;
            double rSPOT = rp.SPOT / expSpot;
            double rFRAG = rp.FRAGS / expFrag;
            double rDEF = rp.DEF / expDef;
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
    }

    #endregion
}
