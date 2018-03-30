using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code.Rating
{
    public class WN7
    {
        public async static Task<double> WN7total(string battleMode = "15")
        {
            WNHelper.RatingParameters rp = await WNHelper.GetParamForPlayerTankBattle(battleMode);
            if (rp == null)
                return 0;
            rp.TIER = await WNHelper.GetAverageTier(battleMode);
            return WN7useFormula(rp);
        }

        public static double WN7tank(WNHelper.RatingParameters tankRP, bool calcForBattle = false)
        {
            WNHelper.RatingParameters rp = new WNHelper.RatingParameters(tankRP); // clone
            // Call method for calc rating
            return WN7useFormula(rp, calcForBattle);
        }

        public static double WN7battle(WNHelper.RatingParameters battleRP, bool calcForBattle = false)
        {
            WNHelper.RatingParameters rp = new WNHelper.RatingParameters(battleRP); // clone
            // If more than one battle recorded 
            if (rp.BATTLES > 1)
            {
                rp.CAP = rp.BATTLES * rp.CAP;
                rp.DAMAGE = rp.BATTLES * rp.DAMAGE;
                rp.DEF = rp.BATTLES * rp.DEF;
                rp.FRAGS = rp.BATTLES * rp.FRAGS;
                rp.SPOT = rp.BATTLES * rp.SPOT;
            }
            // Call method for calc rating
            return WN7useFormula(rp, calcForBattle);
        }

        public async static Task<double> WN7battle(string battleTimeFilter, int maxBattles = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
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
            DataTable dtBattles = await DB.FetchData(sql);
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
                    rp.TIER = (rp.TIER / rp.BATTLES);
                    WN7 = WN7useFormula(rp);
                }
            }
            return WN7;
        }

        public async static Task<double> WN7reverse(string battleTimeFilter, int battleCount = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
        {
            // Find current total EFF
            WNHelper.RatingParameters rp = await WNHelper.GetParamForPlayerTankBattle(battleMode);
            if (rp == null)
                return 0;
            rp.TIER = await WNHelper.GetAverageTier(battleMode) * rp.BATTLES;
            double totalWN7 = WN7useFormula(rp);

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
            DataTable dtBattles = await DB.FetchData(sql);
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
            rp.TIER = (rp.TIER / rp.BATTLES);
            return WN7useFormula(rp);
        }


        public static double WN7useFormula(WNHelper.RatingParameters rpInput, bool calcForBattle = false)
        {
            WNHelper.RatingParameters rp = new WNHelper.RatingParameters(rpInput);
            double WN7 = 0;
            if (rp.BATTLES > 0 && rp.TIER > 0)
            {
                // Calc average values
                double DAMAGE = rp.DAMAGE / rp.BATTLES;
                double SPOT = rp.SPOT / rp.BATTLES;
                double FRAGS = rp.FRAGS / rp.BATTLES;
                double DEF = rp.DEF / rp.BATTLES;
                double CAP = rp.CAP / rp.BATTLES;
                double WINRATE = rp.WINS / rp.BATTLES;
                double TIER = rp.TIER; // Override battle tier using special WN7 tier calculation
                // For battle calculations set WinRate to 50%
                if (rp.BATTLES == 1 || calcForBattle)
                    WINRATE = 0.5;
                // Calculate subvalues
                double WN7_Frags = (1240 - 1040 / Math.Pow(Math.Min(TIER, 6), 0.164)) * (FRAGS);
                double WN7_Damage = DAMAGE * 530 / (184 * Math.Exp(0.24 * TIER) + 130);
                double WN7_Spot = (SPOT) * 125 * (Math.Min(TIER, 3)) / 3;
                double WN7_Defense = Math.Min(DEF, 2.2) * 100;
                double WN7_Winrate = ((185 / (0.17 + Math.Exp(((WINRATE * 100) - 35) * -0.134))) - 500) * 0.45;
                double WN7_LowTierPenalty = ((5 - Math.Min(TIER, 5)) * 125) / (1 + Math.Exp(TIER - Math.Pow(rp.BATTLES / 220, 3 / TIER)) * 1.5);
                // Find result
                WN7 = WN7_Frags + WN7_Damage + WN7_Spot + WN7_Defense + WN7_Winrate - WN7_LowTierPenalty;
            }
            // Return value
            return WN7;
        }
    }
}
