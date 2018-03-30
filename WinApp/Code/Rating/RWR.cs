using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code.Rating
{
    public class RWR
    {
        public async static Task<double?> RWRtotal(string battleMode)
        {
            double? RWR = null;
            WN8.RatingParametersWN8 rpWN8 = await WN8.GetParamForPlayerTotal(battleMode);
            // Use WN8 formula to calculate result
            RWR = RWRuseFormula(rpWN8);
            return RWR;
        }

        public static string RWRtank(int tankId, WNHelper.RatingParameters rpBattle)
        {
            Double? RWR = null;
            WNHelper.RatingParameters rp = new WNHelper.RatingParameters(rpBattle); // clone it to not affect input class
            WN8.RatingParametersWN8 rpWN8 = new WN8.RatingParametersWN8();
            rpWN8.rp = rp;
            // get tankdata for current tank
            DataRow tankInfo = TankHelper.TankInfo(tankId);
            if (tankInfo != null && rp.BATTLES > 0 && tankInfo["expDmg"] != DBNull.Value)
            {
                // get wn8 exp values for tank
                rpWN8.expWinRate = Convert.ToDouble(tankInfo["expWR"]) * rp.BATTLES;
                // Use WN8 formula to calculate result
                RWR = RWRuseFormula(rpWN8);
            }
            if (RWR == null)
                return "NULL";
            else
            {
                Double RWRvalue = Convert.ToDouble(RWR);
                return Math.Round(RWRvalue, 2).ToString().Replace(",", ".");
            }
        }

        public async static Task<double?> RWRbattle(string battleTimeFilter, int maxBattles, string battleMode)
        {
            double? RWR = null;
            if (battleMode == "")
                battleMode = "%";
            // Create an empty datatable with all tanks, no values
            string sql =
                "select tank.id as tankId, 0 as battles, 0 as dmg, 0 as spot, 0 as frags, " +
                "  0 as def, 0 as cap, 0 as wins " +
                "from playerTankBattle ptb left join " +
                "  playerTank pt on ptb.playerTankId=pt.id and pt.playerId=@playerId and ptb.battleMode like @battleMode left join " +
                "  tank on pt.tankId = tank.id " +
                "where tank.expDmg is not null and ptb.battleMode like @battleMode " +
                "group by tank.id ";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);

            DataTable ptb = await DB.FetchData(sql);
            // Get all battles
            sql =
                "select battlesCount as battles, victory as wins, tank.id as tankId " +
                "from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id left join " +
                "  tank on playerTank.tankId = tank.id " +
                "where playerId=@playerId and battleMode like @battleMode " + battleTimeFilter + " order by battleTime DESC";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
            DataTable dtBattles = await DB.FetchData(sql);
            if (dtBattles.Rows.Count > 0)
            {
                int countBattles = 0;
                string error = "";
                foreach (DataRow stats in dtBattles.Rows)
                {
                    int btl = Convert.ToInt32(stats["battles"]);
                    countBattles += btl;
                    // add to datatable
                    string tankId = stats["tankId"].ToString();
                    DataRow[] ptbRow = ptb.Select("tankId = " + tankId);
                    if (ptbRow.Length > 0)
                    {
                        ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) + btl;
                        ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) + Convert.ToInt32(stats["wins"]) * btl;
                    }
                    else
                    {
                        error += tankId.ToString() + ",";
                    }
                    countBattles++;
                    if (maxBattles > 0 && countBattles > maxBattles) break;
                }
                // Check for null values
                if (ptb.Rows.Count > 0)
                    RWR = RWRplayerTankBattle(ptb);
                if (error != "" && Config.Settings.showDBErrors)
                    await Log.LogToFile("RWRbattle() - Could not find playerTank for battle mode '" + battleMode + "' for tank: " + error);
            }
            return RWR;
        }

        public async static Task<double?> RWRReverse(string battleTimeFilter, string battleMode)
        {
            if (battleMode == "")
                battleMode = "%";
            // Create an datatable with all tanks and total stats
            string sql =
                "select tank.id as tankId, SUM(battles) as battles, SUM(wins) as wins " +
                "from playerTankBattle ptb left join " +
                "  playerTank pt on ptb.playerTankId=pt.id and pt.playerId=@playerId and ptb.battleMode like @battleMode left join " +
                "  tank on pt.tankId = tank.id " +
                "where tank.expWR is not null and ptb.battleMode like @battleMode " +
                "group by tank.id ";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);

            DataTable ptb = await DB.FetchData(sql);
            // Get all battles and subtract from totals
            sql =
                "select battlesCount as battles, dmg, spotted as spot, frags, " +
                "  def, cap, tank.tier as tier , victory as wins, tank.id as tankId " +
                "from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id left join " +
                "  tank on playerTank.tankId = tank.id " +
                "where playerId=@playerId and battleMode like @battleMode " + battleTimeFilter + " order by battleTime DESC";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
            DataTable dtBattles = await DB.FetchData(sql);
            if (dtBattles.Rows.Count > 0)
            {
                string error = "";
                foreach (DataRow stats in dtBattles.Rows)
                {
                    double btl = WNHelper.ConvertDbVal2Double(stats["battles"]);
                    // add to datatable
                    string tankId = stats["tankId"].ToString();
                    DataRow[] ptbRow = ptb.Select("tankId = " + tankId);
                    if (ptbRow.Length > 0)
                    {
                        ptbRow[0]["battles"] = Convert.ToInt32(ptbRow[0]["battles"]) - Convert.ToInt32(stats["battles"]);
                        ptbRow[0]["wins"] = Convert.ToInt32(ptbRow[0]["wins"]) - Convert.ToInt32(stats["wins"]);
                    }
                    else
                    {
                        error += tankId.ToString() + ",";
                    }
                }
                if (error != "" && Config.Settings.showDBErrors)
                    await Log.LogToFile("RWRReverse() - Could not find playerTank for battle mode '" + battleMode + "' for tank: " + error);

            }
            return RWR.RWRplayerTankBattle(ptb);
        }

        public static double? RWRplayerTankBattle(DataTable playerTankBattle)
        {
            double? RWR = null;
            // Get player totals from datatable
            if (playerTankBattle.Rows.Count > 0)
            {
                // Get player totals
                WN8.RatingParametersWN8 rpWN8 = new WN8.RatingParametersWN8();
                rpWN8.rp.BATTLES = Convert.ToDouble(playerTankBattle.Compute("SUM([battles])", ""));
                rpWN8.rp.WINS = Convert.ToDouble(playerTankBattle.Compute("SUM([wins])", ""));
                // Get tanks with battle count per tank and expected values from db
                foreach (DataRow ptbRow in playerTankBattle.Rows)
                {
                    // Get tanks with battle count per tank and expected values
                    int tankId = Convert.ToInt32(ptbRow["tankId"]);
                    double battlecount = Convert.ToDouble(ptbRow["battles"]);
                    DataRow expected = TankHelper.TankInfo(tankId);
                    if (battlecount > 0 && expected != null && expected["expWR"] != DBNull.Value)
                    {
                        rpWN8.expWinRate += Convert.ToDouble(expected["expWR"]) * battlecount;
                    }
                }
                // Use RWR formula to calculate result
                if (rpWN8.rp.BATTLES > 0)
                {
                    RWR = RWRuseFormula(rpWN8);
                }

            }
            return RWR;
        }


        private static double? RWRuseFormula(WN8.RatingParametersWN8 rpWN8)
        {
            double? RWR = null;
            if (rpWN8.rp.BATTLES > 0)
                RWR = (((rpWN8.rp.WINS / rpWN8.rp.BATTLES * 100) / (rpWN8.expWinRate / rpWN8.rp.BATTLES)) - 1) * 100;
            return RWR;
        }

    }
}
