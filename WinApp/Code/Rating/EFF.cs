using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WinApp.Code.Rating
{
    public class EFF
    {
        public static double EffTotal(string battleMode = "15")
        {
            WNHelper.RatingParameters rp = WNHelper.GetParamForPlayerTankBattle(battleMode);
            if (rp == null)
                return 0;
            return EffUseFormula(rp);
        }

        public static double EffTank(int tankId, WNHelper.RatingParameters rpTank)
        {
            WNHelper.RatingParameters rp = new WNHelper.RatingParameters(rpTank); // clone it to not affect input class
            // Get tankdata for current tank to get tier
            DataRow tankInfo = TankHelper.TankInfo(tankId);
            if (tankInfo != null)
            {
                rp.TIER = Convert.ToDouble(tankInfo["tier"]) * rp.BATTLES;
            }
            // Call method for calc EFF
            return EffUseFormula(rp);
        }

        public static double EffBattle(int tankId, WNHelper.RatingParameters rpBattle)
        {
            WNHelper.RatingParameters rp = new WNHelper.RatingParameters(rpBattle); // clone it to not affect input class
            // Get tankdata for current tank to get tier
            DataRow tankInfo = TankHelper.TankInfo(tankId);
            double tier = 0;
            if (tankInfo != null)
            {
                tier = Convert.ToDouble(tankInfo["tier"]);
            }
            // If more than one battle recorded 
            if (rp.BATTLES > 1)
            {
                rp.CAP = rp.BATTLES * rp.CAP;
                rp.DAMAGE = rp.BATTLES * rp.DAMAGE;
                rp.DEF = rp.BATTLES * rp.DEF;
                rp.FRAGS = rp.BATTLES * rp.FRAGS;
                rp.SPOT = rp.BATTLES * rp.SPOT;
                rp.TIER = rp.BATTLES * tier;
            }
            // Call method for calc EFF
            return EffUseFormula(rp);
        }

        public static double EffBattle(string battleTimeFilter, int maxBattles = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
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
                WNHelper.RatingParameters rp = new WNHelper.RatingParameters();
                foreach (DataRow stats in dtBattles.Rows)
                {
                    double btl = WNHelper.ConvertDbVal2Double(stats["battles"]);
                    rp.BATTLES += btl;
                    rp.DAMAGE += WNHelper.ConvertDbVal2Double(stats["dmg"]) * btl;
                    rp.SPOT += WNHelper.ConvertDbVal2Double(stats["spot"]) * btl;
                    rp.FRAGS += WNHelper.ConvertDbVal2Double(stats["frags"]) * btl;
                    rp.DEF += WNHelper.ConvertDbVal2Double(stats["def"]) * btl;
                    rp.CAP += WNHelper.ConvertDbVal2Double(stats["cap"]) * btl;
                    rp.WINS += WNHelper.ConvertDbVal2Double(stats["wins"]) * btl;
                    rp.TIER += WNHelper.ConvertDbVal2Double(stats["tier"]) * btl;
                    if (maxBattles > 0 && rp.BATTLES > maxBattles) break;
                }
                if (rp.BATTLES > 0)
                {
                    EFF = EffUseFormula(rp);
                }
            }
            return EFF;
        }

        // Special calculation finds previous efficiency based on effiency parameters now - battle recorded parameters
        public static double EffReverse(string battleTimeFilter, int battleCount = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
        {
            // Find current total EFF
            WNHelper.RatingParameters rp = WNHelper.GetParamForPlayerTankBattle(battleMode);
            if (rp == null)
                return 0;
            double totalEff = EffUseFormula(rp);

            // Find changes and subtract
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
                foreach (DataRow stats in dtBattles.Rows)
                {
                    double btl = WNHelper.ConvertDbVal2Double(stats["battles"]);
                    rp.BATTLES -= btl;
                    rp.DAMAGE -= WNHelper.ConvertDbVal2Double(stats["dmg"]) * btl;
                    rp.SPOT -= WNHelper.ConvertDbVal2Double(stats["spot"]) * btl;
                    rp.FRAGS -= WNHelper.ConvertDbVal2Double(stats["frags"]) * btl;
                    rp.DEF -= WNHelper.ConvertDbVal2Double(stats["def"]) * btl;
                    rp.CAP -= WNHelper.ConvertDbVal2Double(stats["cap"]) * btl;
                    rp.WINS -= WNHelper.ConvertDbVal2Double(stats["wins"]) * btl;
                    rp.TIER -= WNHelper.ConvertDbVal2Double(stats["tier"]) * btl;
                    count++;
                    if (count > battleCount) break;
                }
            }
            return EffUseFormula(rp);
        }

        //public static double CalculateEFF(double battleCount, double dmg, double spotted, double frags, double def, double cap, double tier)
        public static double EffUseFormula(WNHelper.RatingParameters rpInput)
        {
            WNHelper.RatingParameters rp = new WNHelper.RatingParameters(rpInput);
            double EFF = 0;
            if (rp.BATTLES > 0)
            {
                double DAMAGE = rp.DAMAGE / rp.BATTLES;
                double SPOT = rp.SPOT / rp.BATTLES;
                double FRAGS = rp.FRAGS / rp.BATTLES;
                double DEF = rp.DEF / rp.BATTLES;
                double CAP = rp.CAP / rp.BATTLES;
                double TIER = rp.TIER / rp.BATTLES;
                // CALC
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
    }
}
