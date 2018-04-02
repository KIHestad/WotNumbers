using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code.Rating
{
    public class WN9
    {
        #region parameteres

        public class TierAvg
        {
            public double Win { get; set; }
            public double Dmg { get; set; }
            public double Frag { get; set; }
            public double Spot { get; set; }
            public double Def { get; set; }
            public double Cap { get; set; }
            public double Weight { get; set; }
        }

        public static List<TierAvg> tierAvg = new List<TierAvg>();

        public static void SetTierAvgList()
        {
            // from 150816 EU avgs exc scout/arty
            string tierAvgJSON =
                "[ " +
                "{ win:0.477, dmg:88.9, frag:0.68, spot:0.90, def:0.53, cap:1.0, weight:0.40 }," +
                "{ win:0.490, dmg:118.2, frag:0.66, spot:0.85, def:0.65, cap:1.0, weight:0.41 }," +
                "{ win:0.495, dmg:145.1, frag:0.59, spot:1.05, def:0.51, cap:1.0, weight:0.44 }," +
                "{ win:0.492, dmg:214.0, frag:0.60, spot:0.81, def:0.55, cap:1.0, weight:0.44 }," +
                "{ win:0.495, dmg:388.3, frag:0.75, spot:0.93, def:0.63, cap:1.0, weight:0.60 }," +
                "{ win:0.497, dmg:578.7, frag:0.74, spot:0.93, def:0.52, cap:1.0, weight:0.70 }," +
                "{ win:0.498, dmg:791.1, frag:0.76, spot:0.87, def:0.58, cap:1.0, weight:0.82 }," +
                "{ win:0.497, dmg:1098.7, frag:0.79, spot:0.87, def:0.58, cap:1.0, weight:1.00 }," +
                "{ win:0.498, dmg:1443.2, frag:0.86, spot:0.94, def:0.56, cap:1.0, weight:1.23 }," +
                "{ win:0.498, dmg:1963.8, frag:1.04, spot:1.08, def:0.61, cap:1.0, weight:1.60 }]";
            tierAvg = JsonConvert.DeserializeObject<List<TierAvg>>(tierAvgJSON);
        }

        public class RatingParametersWN9
        {
            public RatingParametersWN9()
            {
                RP = new WNHelper.RatingParameters();
                Tier = 0;
                MmRange = 0;
                WN9exp = 0;
                WN9nerf = 0;
                WN9scale = 0;
            }
            public WNHelper.RatingParameters RP { get; set; }
            public int Tier { get; set; }
            public int MmRange { get; set; }
            public double WN9exp { get; set; }
            public double WN9scale { get; set; }
            public double WN9nerf { get; set; }
        }

        //public static RatingParametersWN9 GetParamForPlayerTotal(string battleMode)
        //{
        //    RatingParametersWN9 rpWN = new RatingParametersWN9();
        //    // Get player totals from db
        //    rpWN.rp = WNHelper.GetParamForPlayerTankBattle(battleMode, excludeIfWN9ExpValIsNull: true);
        //    if (rpWN.rp == null)
        //        return null;
        //    // Get tanks with battle count per tank and expected values from db
        //    string battleModeWhere = "";
        //    if (battleMode != "")
        //    {
        //        battleModeWhere = " and ptb.battleMode=@battleMode ";
        //        DB.AddWithValue(ref battleModeWhere, "@battleMode", battleMode, DB.SqlDataType.VarChar);
        //    }
        //    string sql =
        //        "select sum(ptb.battles) as battles, t.id, t.mmrange, t.wn9exp, t. wn9scale, t.wn9nerf " +
        //        "from playerTankBattle ptb left join " +
        //        "  playerTank pt on ptb.playerTankId=pt.id left join " +
        //        "  tank t on pt.tankId = t.id " +
        //        "where pt.playerId=@playerId " + battleModeWhere + // TODO: OK removing? t.expDmg is not null and 
        //        "group by t.id, t.mmrange, t.wn9exp, t. wn9scale, t.wn9nerf ";
        //    DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
        //    DataTable expectedTable = await DB.FetchData(sql);
        //    foreach (DataRow expected in expectedTable.Rows)
        //    {
        //        // Get tanks with battle count per tank and expected values
        //        double battlecount = Convert.ToDouble(expected["battles"]);
        //        if (battlecount > 0 && expected["wn9exp"] != DBNull.Value)
        //        {
        //            rpWN.mmrange += Convert.ToInt32(expected["mmrange"]);
        //            rpWN.wn9exp += Convert.ToDouble(expected["wn9exp"]) * battlecount;
        //            rpWN.wn9nerf += Convert.ToDouble(expected["wn9nerf"]) * battlecount;
        //            rpWN.wn9scale += Convert.ToDouble(expected["wn9scale"]) * battlecount;
        //            rpWN.tier += Convert.ToInt32(expected["tier"]); // Correct??
        //        }
        //    }
        //    return rpWN;
        //}

        public static RatingParametersWN9 GetParamForPlayerTankBattle(DataTable playerTankBattle)
        {
            RatingParametersWN9 rpWN = null;
            // Get player totals from datatable
            if (playerTankBattle != null && playerTankBattle.Rows.Count > 0)
            {
                // Get player totals
                rpWN = new RatingParametersWN9();
                rpWN.RP.BATTLES = Convert.ToDouble(playerTankBattle.Compute("SUM([battles])", ""));
                rpWN.RP.DAMAGE = Convert.ToDouble(playerTankBattle.Compute("SUM([dmg])", ""));
                rpWN.RP.SPOT = Convert.ToDouble(playerTankBattle.Compute("SUM([spot])", ""));
                rpWN.RP.FRAGS = Convert.ToDouble(playerTankBattle.Compute("SUM([frags])", ""));
                rpWN.RP.DEF = Convert.ToDouble(playerTankBattle.Compute("SUM([def])", ""));
                rpWN.RP.WINS = Convert.ToDouble(playerTankBattle.Compute("SUM([wins])", ""));
                // Get tanks with battle count per tank and expected values from db
                double totalBattleCount = 0;
                foreach (DataRow ptbRow in playerTankBattle.Rows)
                {
                    // Get tanks with battle count per tank and expected values
                    int tankId = Convert.ToInt32(ptbRow["tankId"]);
                    double battlecount = Convert.ToDouble(ptbRow["battles"]);
                    DataRow expected = TankHelper.TankInfo(tankId);
                    if (battlecount > 0 && expected != null && expected["wn9exp"] != DBNull.Value)
                    {
                        rpWN.MmRange += Convert.ToInt32(expected["mmrange"]) * Convert.ToInt32(battlecount);
                        rpWN.WN9exp += Convert.ToDouble(expected["wn9exp"]) * battlecount;
                        rpWN.WN9nerf += Convert.ToDouble(expected["wn9nerf"]) * battlecount;
                        rpWN.WN9scale += Convert.ToDouble(expected["wn9scale"]) * battlecount;
                        rpWN.Tier += Convert.ToInt32(expected["tier"]) * Convert.ToInt32(battlecount);
                        totalBattleCount += battlecount;
                    }
                }
                // Get average values
                if (totalBattleCount > 0)
                {
                    double avg_mmrange = rpWN.MmRange / totalBattleCount;
                    rpWN.MmRange = Convert.ToInt32(Math.Round(avg_mmrange, 0));
                    rpWN.WN9exp = rpWN.WN9exp / totalBattleCount;
                    rpWN.WN9nerf = rpWN.WN9nerf / totalBattleCount;
                    rpWN.WN9scale = rpWN.WN9scale / totalBattleCount;
                    double avg_tier = rpWN.Tier / totalBattleCount;
                    rpWN.Tier = Convert.ToInt32(Math.Max(1, Math.Round(avg_tier, 0)));
                }
            }
            return rpWN;
        }

        #endregion

        #region calc

        private class TankListWn9
        {
            public double WN9 { get; set; }
            public double Weight { get; set; }
        }

        public async static Task<DataTable> GetPlayerTotalWN9TankStats(string battleMode = "")
        {
            // Get tanks by WN9 
            string battleModeWhere = "";
            if (battleMode != "")
                battleModeWhere = " AND ptb.battleMode = '" + battleMode + "' ";
            string sql =
                "SELECT ptb.playerTankId, SUM(ptb.battles) as battles, ISNULL(SUM(ptb.wn9 * ptb.battles) / NULLIF(SUM(ptb.battles), 0),0) as wn9, " + 
                "ISNULL(t.wn9exp,0) as wn9exp, t.tier as tier, ISNULL(t.wn9nerf,0) as wn9nerf " +
                "FROM playerTankBattle ptb INNER JOIN playerTank pt ON ptb.playerTankId = pt.id INNER JOIN tank t ON pt.tankId = t.Id " +
                "WHERE pt.playerId = @playerId AND t.wn9exp is not null AND " +
                "ISNULL(ptb.wn9,0) <> 0 AND t.tankTypeId <> 5 " + // don't use tanks with no expected values // don't use SPGs & missing tanks
                battleModeWhere +
                "GROUP BY playerTankId, ptb.wn9maxhist, t.wn9exp, t.tier, t.wn9nerf " +
                "HAVING SUM(ptb.battles) > 0 " +
                "ORDER BY ptb.wn9maxhist DESC "; // sort tanks by WN9 decreasing
            if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
                sql = sql.Replace("ISNULL(", "IFNULL(");                                                                     
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            return await DB.FetchData(sql);
        }

        public async static Task<double> CalcPlayerTotal(string battleMode = "", DataTable tankStats = null)
        {
            // compile list of valid tanks with battles & WN9
            List<TankListWn9> tanklist = new List<TankListWn9>();
            double totbat = 0;
            // Replace loop in original formula, calling method to get player total tank stats if not provided
            if (tankStats == null)
                tankStats = await GetPlayerTotalWN9TankStats(battleMode);
            // Calc WN9 if any tankstats found
            if (tankStats != null & tankStats.Rows.Count > 0)
            {
                // cap tank weight according to tier, total battles & nerf status
                double totweight = 0;
                foreach (DataRow dr in tankStats.Rows)
                {
                    double exp = Convert.ToDouble(dr["wn9exp"]);
                    double tier = Convert.ToDouble(dr["tier"]);
                    double batcap = tier * (40 + tier * totbat / 2000);
                    double nerf = Convert.ToDouble(dr["wn9nerf"]);
                    if (nerf > 0) batcap /= 2;
                    double wn9 = Convert.ToDouble(dr["wn9"]);
                    double weight = Math.Min(Convert.ToDouble(dr["battles"]), batcap);
                    tanklist.Add(new TankListWn9() { WN9 = wn9, Weight = weight });
                    totweight += weight;
                }

                // add up account WN9 over top 65% of capped battles
                totweight *= 0.65;
                double wn9tot = 0; double usedweight = 0; int i = 0;
                for (; usedweight + tanklist[i].Weight <= totweight; i++)
                {
                    wn9tot += tanklist[i].WN9 * tanklist[i].Weight;
                    usedweight += tanklist[i].Weight;
                }
                // last tank before cutoff uses remaining weight, not its battle count
                wn9tot += tanklist[i].WN9 * (totweight - usedweight);
                return wn9tot / totweight;
            }
            else
            {
                return 0;
            }
        }

        public async static Task<Wn9Result> CalcBattle(int tankId, WNHelper.RatingParameters ratingParameters)
        {
            WNHelper.RatingParameters rp = new WNHelper.RatingParameters(ratingParameters); // clone it to not affect input class
            rp.CAP = rp.CAP * rp.BATTLES;
            rp.DAMAGE = rp.DAMAGE * rp.BATTLES;
            rp.DEF = rp.DEF * rp.BATTLES;
            rp.FRAGS = rp.FRAGS * rp.BATTLES;
            rp.SPOT = rp.SPOT * rp.BATTLES;
            rp.WINS = rp.WINS ;
            return await CalcTank(tankId, rp);
        }

        public async static Task<Wn9Result> CalcTank(int tankId, WNHelper.RatingParameters ratingParameters)
        {
            Wn9Result result = new Wn9Result();
            WNHelper.RatingParameters rp = new WNHelper.RatingParameters(ratingParameters); // clone it to not affect input class
            RatingParametersWN9 rpWN = new RatingParametersWN9
            {
                RP = rp
            };
            // get tankdata for current tank
            DataRow tankInfo = TankHelper.TankInfo(tankId);
            if (tankInfo != null && rp.BATTLES > 0 && tankInfo["wn9exp"] != DBNull.Value)
            {
                // get wn9 exp values for tank
                rpWN.MmRange = Convert.ToInt32(tankInfo["mmrange"]);
                rpWN.WN9exp = Convert.ToDouble(tankInfo["wn9exp"]);
                rpWN.WN9nerf = Convert.ToDouble(tankInfo["wn9nerf"]);
                rpWN.WN9scale = Convert.ToDouble(tankInfo["wn9scale"]);
                rpWN.Tier = Convert.ToInt32(tankInfo["tier"]);
                // Use WN8 formula to calculate result

                result = await UseFormula(rpWN);
            }
            return result;
        }


        public async static Task<Wn9Result> CalcBattleRange(string battleTimeFilter, int maxBattles = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
        {
            DataTable ptb = await WNHelper.GetDataForBattleRange(battleTimeFilter, maxBattles, battleMode, tankFilter, battleModeFilter, tankJoin);
            return await CalcPlayerTankBattle(ptb);
        }

        public async static Task<double> CalcBattleRangeReverse(string battleTimeFilter, int battleCount = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
        {
            // get battle result for battle range to reverse
            DataTable ptb = await WNHelper.GetDataForBattleRange(battleTimeFilter, battleCount, battleMode, tankFilter, battleModeFilter, tankJoin);
            // if any battles played calculate reverse
            if (ptb != null && ptb.Rows.Count > 0 && Convert.ToInt32(ptb.Compute("SUM([battles])", "")) > 0)
            {
                // get players current total wn9 stats
                DataTable playerTotalWN9TankStats = await GetPlayerTotalWN9TankStats(battleMode);

                // loop throgh battle range tank by tank and adjust the players current total wn9 stats
                foreach (DataRow dr in ptb.Rows)
                {
                    // Get the wn9 stats for the tank to reverse stats
                    int tankId = Convert.ToInt32(dr["tankId"]);
                    DataRow[] drToAdjust = playerTotalWN9TankStats.Select("playerTankId = " + await TankHelper.GetPlayerTankId(tankId));
                    // If tank is found continue to adjust (spgs are excluded)
                    if (drToAdjust.Length > 0)
                    {
                        // Check if battles is played for tank
                        if (Convert.ToInt32(dr["battles"]) > 0)
                        {
                            // Get total stats for tank
                            DataTable playerTotalTankStats = await WNHelper.GetTotalTankStatsForPlayerTank(battleMode, tankJoin, tankId);
                            RatingParametersWN9 rpWN9 = GetParamForPlayerTankBattle(playerTotalTankStats);
                            // Subtract stats from range to find older totals = reversing the stats
                            rpWN9.RP.BATTLES -= Convert.ToInt32(dr["battles"]);
                            rpWN9.RP.CAP -= Convert.ToInt32(dr["cap"]);
                            rpWN9.RP.DAMAGE -= Convert.ToInt32(dr["dmg"]);
                            rpWN9.RP.DEF -= Convert.ToInt32(dr["def"]);
                            rpWN9.RP.FRAGS -= Convert.ToInt32(dr["frags"]);
                            rpWN9.RP.SPOT -= Convert.ToInt32(dr["spot"]);
                            rpWN9.RP.WINS -= Convert.ToInt32(dr["wins"]);
                            // Calc new WN9 for tank with adjusted stats
                            Wn9Result adjResult = await CalcTank(tankId, rpWN9.RP);
                            // Adjust now
                            drToAdjust[0]["battles"] = rpWN9.RP.BATTLES;
                            drToAdjust[0]["wn9"] = adjResult.WN9;
                        }
                    }
                }
                // calc player overall wn with adjusted player total wn9 stats
                return await CalcPlayerTotal(battleMode, playerTotalWN9TankStats);
            }
            else
            {
                // No battles found to reverse, return total player wn9
                return await CalcPlayerTotal(battleMode);
            }
        }
        public class Wn9Result
        {
            public Wn9Result()
            {
                WN9 = 0;
                WN9maxhist = 0;
            }
            public double WN9 { get; set; }
            public double WN9maxhist { get; set; }
        }

        public async static Task<Wn9Result> CalcPlayerTankBattle(DataTable playerTankBattle)
        {
            // Get WN rating parameters using datatable containing playerTankBattle
            RatingParametersWN9 rpWN = GetParamForPlayerTankBattle(playerTankBattle);
            // Use WN9 formula to calculate result
            return await UseFormula(rpWN);
        }
        
        private async static Task<Wn9Result> UseFormula(RatingParametersWN9 rpWN)
        {
            // inputs:
            // tank is object containing tank_id variable & "random" object
            //     "random" object contains battles, damage_dealt, frags, spotted and dropped_capture_points
            // expvals is array containing wn9exp/wn9scale/tier/mmrange for each tank, indexed by tank_id
            // maxhist should be false for current values and true for maximum historical values
            Wn9Result result = new Wn9Result();
            try
            {
                if (rpWN != null && rpWN.RP.BATTLES > 0)
                {
                    // Using same variable name as in formula description explained at http://jaj22.org.uk/wn9implement.html
                    WNHelper.RatingParameters tank = rpWN.RP;
                    RatingParametersWN9 exp = rpWN;
                    // Array [0-9] corresponds with tier [1-10], subtract 1 from exp.tier to locate correct avg values from array
                    int tierInArray = (exp.Tier - 1);
                    // Select tier average from table, adding +1 to tier if tank has +3 tier MM.
                    TierAvg avg = tierAvg[exp.MmRange >= 3 ? tierInArray + 1 : tierInArray];
                    double rdmg = tank.DAMAGE / (tank.BATTLES * avg.Dmg);
                    double rfrag = tank.FRAGS / (tank.BATTLES * avg.Frag);
                    double rspot = tank.SPOT / (tank.BATTLES * avg.Spot);
                    double rdef = tank.DEF / (tank.BATTLES * avg.Def);

                    // Calculate raw winrate-correlated wn9base
                    // Use different formula for low battle counts
                    double wn9base = 0.7 * rdmg;
                    if (tank.BATTLES < 5)
                        wn9base += 0.14 * rfrag + 0.13 * Math.Sqrt(rspot) + 0.03 * Math.Sqrt(rdef);
                    else
                        wn9base += 0.25 * Math.Sqrt(rfrag * rspot) + 0.05 * Math.Sqrt(rfrag * Math.Sqrt(rdef));

                    // Calc with maxhist, Adjust expected value when generating maximum historical value
                    double wn9expMaxhist = exp.WN9exp * (1 + exp.WN9nerf);
                    result.WN9maxhist = 666 * Math.Max(0, 1 + (wn9base / wn9expMaxhist - 1) / exp.WN9scale);

                    // Calculate final WN9 based on tank expected value & skill scaling 
                    result.WN9 = 666 * Math.Max(0, 1 + (wn9base / exp.WN9exp - 1) / exp.WN9scale);
                }
            }
            catch (Exception ex)
            {
                await Log.LogToFile(ex, 
                    "Error calculationg WN9, rating params: {" +
                    " Battles:" + rpWN.RP.BATTLES +
                    " Cap:" + rpWN.RP.CAP +
                    " Dmg:" + rpWN.RP.DAMAGE +
                    " Def:" + rpWN.RP.DEF +
                    " Frags:" + rpWN.RP.FRAGS +
                    " Spot:" + rpWN.RP.SPOT +
                    " Tier:" + rpWN.RP.TIER +
                    " Wins:" + rpWN.RP.WINS +
                    " }"
                );
            }           
            // Return value
            return result;
        }

    }

    #endregion
}
