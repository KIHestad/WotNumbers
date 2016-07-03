using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WinApp.Code.Rating
{
    public class WN9
    {
        #region parameteres

        public class TierAvg
        {
            public double win { get; set; }
            public double dmg { get; set; }
            public double frag { get; set; }
            public double spot { get; set; }
            public double def { get; set; }
            public double cap { get; set; }
            public double weight { get; set; }
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
                rp = new WNHelper.RatingParameters();
                tier = 0;
                mmrange = 0;
                wn9exp = 0;
                wn9nerf = 0;
                wn9scale = 0;
            }
            public WNHelper.RatingParameters rp { get; set; }
            public int tier { get; set; }
            public int mmrange { get; set; }
            public double wn9exp { get; set; }
            public double wn9scale { get; set; }
            public double wn9nerf { get; set; }
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
        //    DataTable expectedTable = DB.FetchData(sql);
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
            if (playerTankBattle.Rows.Count > 0)
            {
                // Get player totals
                rpWN = new RatingParametersWN9();
                rpWN.rp.BATTLES = Convert.ToDouble(playerTankBattle.Compute("SUM([battles])", ""));
                rpWN.rp.DAMAGE = Convert.ToDouble(playerTankBattle.Compute("SUM([dmg])", ""));
                rpWN.rp.SPOT = Convert.ToDouble(playerTankBattle.Compute("SUM([spot])", ""));
                rpWN.rp.FRAGS = Convert.ToDouble(playerTankBattle.Compute("SUM([frags])", ""));
                rpWN.rp.DEF = Convert.ToDouble(playerTankBattle.Compute("SUM([def])", ""));
                rpWN.rp.WINS = Convert.ToDouble(playerTankBattle.Compute("SUM([wins])", ""));
                // Get tanks with battle count per tank and expected values from db
                double totalBattleCount = 0;
                foreach (DataRow ptbRow in playerTankBattle.Rows)
                {
                    // Get tanks with battle count per tank and expected values
                    int tankId = Convert.ToInt32(ptbRow["tankId"]);
                    double battlecount = Convert.ToDouble(ptbRow["battles"]);
                    DataRow expected = TankHelper.TankInfo(tankId);
                    if (battlecount > 0 && expected["wn9exp"] != DBNull.Value)
                    {
                        rpWN.mmrange += Convert.ToInt32(expected["mmrange"]) * Convert.ToInt32(battlecount);
                        rpWN.wn9exp += Convert.ToDouble(expected["wn9exp"]) * battlecount;
                        rpWN.wn9nerf += Convert.ToDouble(expected["wn9nerf"]) * battlecount;
                        rpWN.wn9scale += Convert.ToDouble(expected["wn9scale"]) * battlecount;
                        rpWN.tier += Convert.ToInt32(expected["tier"]) * Convert.ToInt32(battlecount);
                        totalBattleCount += battlecount;
                    }
                }
                // Get average values
                if (totalBattleCount > 0)
                {
                    double avg_mmrange = rpWN.mmrange / totalBattleCount;
                    rpWN.mmrange = Convert.ToInt32(Math.Round(avg_mmrange, 0));
                    rpWN.wn9exp = rpWN.wn9exp / totalBattleCount;
                    rpWN.wn9nerf = rpWN.wn9nerf / totalBattleCount;
                    rpWN.wn9scale = rpWN.wn9scale / totalBattleCount;
                    double avg_tier = rpWN.tier / totalBattleCount;
                    rpWN.tier = Convert.ToInt32(Math.Max(1, Math.Round(avg_tier, 0)));
                }
            }
            return rpWN;
        }

        #endregion

        #region calc

        private class tankListWn9
        {
            public int wn9 { get; set; }
            public int weight { get; set; }
        }

        public static DataTable GetPlayerTotalWN9TankStats(string battleMode = "")
        {
            // Get tanks by WN9 
            string battleModeWhere = "";
            if (battleMode != "")
                battleModeWhere = " AND ptb.battleMode = '" + battleMode + "' ";
            string sql =
                "SELECT ptb.playerTankId, SUM(ptb.battles) as battles, SUM(ptb.wn9maxhist * ptb.battles) / NULLIF(SUM(ptb.battles), 0) as wn9 " +
                "FROM playerTankBattle ptb INNER JOIN playerTank pt ON ptb.playerTankId = pt.id " +
                "WHERE pt.playerId = @playerId AND " +
                "ptb.wn9maxhist IS NOT NULL " + // don't use tanks with no expected values
                battleModeWhere +
                "GROUP BY playerTankId " +
                "HAVING SUM(ptb.battles) > 0 " +
                "ORDER BY SUM(ptb.wn9maxhist * ptb.battles) / NULLIF(SUM(ptb.battles), 0) DESC "; // sort tanks by WN9 decreasing
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            return DB.FetchData(sql);
        }

        public static double CalcPlayerTotal(string battleMode = "", DataTable tankStats = null)
        {
            // Get player total tank stats if not provided
            if (tankStats == null)
                tankStats = GetPlayerTotalWN9TankStats(battleMode);

            // compile list of valid tanks with battles & WN9 
            int batcap = 200 + (Convert.ToInt32(tankStats.Compute("SUM([battles])","")) / 50);
            double totweight = 0;
            List<tankListWn9> tanklist = new List<tankListWn9>();
            foreach (DataRow dr in tankStats.Rows)
            {
                int wn9 = Convert.ToInt32(dr["wn9"]);
                int weight = Math.Min(Convert.ToInt32(dr["battles"]), batcap);
                tanklist.Add(new tankListWn9() { wn9 = wn9, weight = weight });
                totweight += weight;
            }

	        // add up account WN9 over top 65% of capped battles
	        totweight *= 0.65;
            double wn9tot = 0; double usedweight = 0; int i = 0;
            for (; usedweight+tanklist[i].weight <= totweight; i++)
	        {
		        wn9tot += tanklist[i].wn9* tanklist[i].weight;
		        usedweight += tanklist[i].weight;
	        }
            // last tank before cutoff uses remaining weight, not its battle count
            wn9tot += tanklist[i].wn9* (totweight - usedweight);
	        return wn9tot / totweight;
        }

        public static double CalcBattle(int tankId, WNHelper.RatingParameters ratingParameters, out double WN9maxhist)
        {
            WNHelper.RatingParameters rp = new WNHelper.RatingParameters(ratingParameters); // clone it to not affect input class
            rp.CAP = rp.CAP * rp.BATTLES;
            rp.DAMAGE = rp.DAMAGE * rp.BATTLES;
            rp.DEF = rp.DEF * rp.BATTLES;
            rp.FRAGS = rp.FRAGS * rp.BATTLES;
            rp.SPOT = rp.SPOT * rp.BATTLES;
            rp.WINS = rp.WINS * rp.BATTLES;
            return CalcTank(tankId, rp, out WN9maxhist);
        }

        public static double CalcTank(int tankId, WNHelper.RatingParameters ratingParameters, out double WN9maxhist)
        {
            double WN9 = 0;
            WN9maxhist = 0;
            WNHelper.RatingParameters rp = new WNHelper.RatingParameters(ratingParameters); // clone it to not affect input class
            RatingParametersWN9 rpWN = new RatingParametersWN9();
            rpWN.rp = rp;
            // get tankdata for current tank
            DataRow tankInfo = TankHelper.TankInfo(tankId);
            if (tankInfo != null && rp.BATTLES > 0 && tankInfo["wn9exp"] != DBNull.Value)
            {
                // get wn9 exp values for tank
                rpWN.mmrange = Convert.ToInt32(tankInfo["mmrange"]);
                rpWN.wn9exp = Convert.ToDouble(tankInfo["wn9exp"]);
                rpWN.wn9nerf = Convert.ToDouble(tankInfo["wn9nerf"]);
                rpWN.wn9scale = Convert.ToDouble(tankInfo["wn9scale"]);
                rpWN.tier = Convert.ToInt32(tankInfo["tier"]);
                // Use WN8 formula to calculate result

                WN9 = UseFormula(rpWN, out WN9maxhist);
            }
            return WN9;
        }


        public static double CalcBattleRange(string battleTimeFilter, int maxBattles = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
        {
            DataTable ptb = WNHelper.GetDataForBattleRange(battleTimeFilter, maxBattles, battleMode, tankFilter, battleModeFilter, tankJoin);
            double WN9maxhist = 0;
            return CalcPlayerTankBattle(ptb, out WN9maxhist);
        }

        public static double CalcBattleRangeReverse(string battleTimeFilter, int battleCount = 0, string battleMode = "15", string tankFilter = "", string battleModeFilter = "", string tankJoin = "")
        {
            double wn9 = 0;
            // get battle result for battle range to reverse
            DataTable ptb = WNHelper.GetDataForBattleRange(battleTimeFilter, battleCount, battleMode, tankFilter, battleModeFilter, tankJoin);
            // if any battles played calculate reverse
            int ptbBattlesCount = Convert.ToInt32(ptb.Compute("SUM([battles])", ""));
            if (ptbBattlesCount > 0)
            {
                // get players current total wn9 stats
                DataTable playerTotalWN9TankStats = GetPlayerTotalWN9TankStats(battleMode);

                // loop throgh battle range tank by tank and adjust the players current total wn9 stats
                foreach (DataRow dr in ptb.Rows)
                {
                    // Check if battles is played for tank
                    if (Convert.ToInt32(dr["battles"]) > 0)
                    {
                        // Get total stats for tank
                        int tankId = Convert.ToInt32(dr["tankId"]);
                        DataTable playerTotalTankStats = WNHelper.GetTotalTankStatsForPlayerTank(battleMode, tankJoin, tankId);
                        RatingParametersWN9 rpWN9 = GetParamForPlayerTankBattle(playerTotalTankStats);
                        // Subtract stats from range to find older totals = reversing the stats
                        rpWN9.rp.BATTLES -= Convert.ToInt32(dr["battles"]);
                        rpWN9.rp.CAP -= Convert.ToInt32(dr["cap"]);
                        rpWN9.rp.DAMAGE -= Convert.ToInt32(dr["dmg"]);
                        rpWN9.rp.DEF -= Convert.ToInt32(dr["def"]);
                        rpWN9.rp.FRAGS -= Convert.ToInt32(dr["frags"]);
                        rpWN9.rp.SPOT -= Convert.ToInt32(dr["spot"]);
                        rpWN9.rp.WINS -= Convert.ToInt32(dr["wins"]);
                        // Calc new WN9 for tank with adjusted stats
                        double WN9adjMaxhist = 0;
                        double WN9adj = CalcTank(tankId, rpWN9.rp, out WN9adjMaxhist);
                        // Now adjust the wn9 stats for the tank
                        DataRow[] drToAdjust = playerTotalWN9TankStats.Select("playerTankId = " + TankHelper.GetPlayerTankId(tankId));
                        drToAdjust[0]["battles"] = rpWN9.rp.BATTLES;
                        drToAdjust[0]["wn9"] = WN9adjMaxhist;
                    }
                }

                // calc player overall wn with adjusted pløayer total wn9 stats
                wn9 = CalcPlayerTotal(battleMode, playerTotalWN9TankStats);
            }
            else
            {
                // No battles found to reverse, return total player wn9
                wn9 = CalcPlayerTotal(battleMode);
            }


            return wn9;
        }


        public static double CalcPlayerTankBattle(DataTable playerTankBattle, out double WN9maxhist)
        {
            // Get WN rating parameters using datatable containing playerTankBattle
            RatingParametersWN9 rpWN = GetParamForPlayerTankBattle(playerTankBattle);
            // Use WN9 formula to calculate result
            return UseFormula(rpWN, out WN9maxhist);
        }

        private static double UseFormula(RatingParametersWN9 rpWN, out double WN9maxhist)
        {
            // inputs:
            // tank is object containing tank_id variable & "random" object
            //     "random" object contains battles, damage_dealt, frags, spotted and dropped_capture_points
            // expvals is array containing wn9exp/wn9scale/tier/mmrange for each tank, indexed by tank_id
            // maxhist should be false for current values and true for maximum historical values
            WNHelper.RatingParameters tank = rpWN.rp; // Using same variable name as in formula description explained at http://jaj22.org.uk/wn9implement.html
            RatingParametersWN9 exp = rpWN; // Using same variable name as in formula description explained at http://jaj22.org.uk/wn9implement.html

            double WN9 = 0;
            WN9maxhist = 0;
            if (rpWN != null && rpWN.rp.BATTLES > 0)
            {
                // Array [0-9] corresponds with tier [1-10], subtract 1 from exp.tier to locate correct avg values from array
                int tierInArray = (exp.tier - 1);
                // Select tier average from table, adding +1 to tier if tank has +3 tier MM.
                TierAvg avg = tierAvg[exp.mmrange >= 3 ? tierInArray + 1 : tierInArray];  
                double rdmg = tank.DAMAGE / (tank.BATTLES * avg.dmg);
                double rfrag = tank.FRAGS / (tank.BATTLES * avg.frag);
                double rspot = tank.SPOT / (tank.BATTLES * avg.spot);
                double rdef = tank.DEF / (tank.BATTLES * avg.def);

                // Calculate raw winrate-correlated wn9base
                // Use different formula for low battle counts
                double wn9base = 0.7 * rdmg;
                if (tank.BATTLES < 5)
                    wn9base += 0.14 * rfrag + 0.13 * Math.Sqrt(rspot) + 0.03 * Math.Sqrt(rdef);
                else
                    wn9base += 0.25 * Math.Sqrt(rfrag * rspot) + 0.05 * Math.Sqrt(rfrag * Math.Sqrt(rdef));

                // Calc with maxhist, Adjust expected value when generating maximum historical value
                double wn9expMaxhist = exp.wn9exp * (1 + exp.wn9nerf);
                WN9maxhist = 666 * Math.Max(0, 1 + (wn9base / wn9expMaxhist - 1) / exp.wn9scale);

                // Calculate final WN9 based on tank expected value & skill scaling 
                WN9 = 666 * Math.Max(0, 1 + (wn9base / exp.wn9exp - 1) / exp.wn9scale);
            }
            // Return value
            return WN9;
        }

    }

    #endregion
}
