using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WinApp.Code.Rating
{
    public class PR
    {
        public class RatingParametersPR
        {
            public RatingParametersPR()
            {
                bc = 0;
                win = 0;
                surv = 0;
                dmg = 0;
                hit = 0;
                bc88 = 0;
                xp88 = 0;
                radio88 = 0;
                track88 = 0;
            }
            public double bc { get; set; } // ptb.battles          - battle.battlesCount 
            public double win { get; set; } // ptb.wins            - battle.victory 
            public double surv { get; set; } // ptb.survived       - battle.survived 
            public double dmg { get; set; } // ptb.dmg             - battle.dmg 
            public double hit { get; set; } // ptb.hits            - battle.hits 
            public double bc88 { get; set; } // ptb.battles8p      - battle.battlesCount 
            public double xp88 { get; set; } // ptb.xp8p           - battle.xp       
            public double radio88 { get; set; } // ptb.assistSpot  - battle.assistSpot 
            public double track88 { get; set; } // ptb.assistTrack - battle.assistTrack 
        }

        private static RatingParametersPR GetParamForPlayerTotal(string battleMode)
        {
            RatingParametersPR rpPR = new RatingParametersPR();
            // Get player totals from db
            string battleModeWhere = "";
            if (battleMode != "")
            {
                battleModeWhere = " and ptb.battleMode=@battleMode ";
                DB.AddWithValue(ref battleModeWhere, "@battleMode", battleMode, DB.SqlDataType.VarChar);
            }
            string sql =
                "select sum(ptb.battles) as bc, sum(ptb.wins) as win, sum(ptb.survived) as surv, sum(ptb.dmg) as dmg, sum(ptb.hits) as hit, " +
                "  sum (ptb.battles8p) as bc88, sum (ptb.xp8p) as xp88, sum(ptb.assistSpot) as radio88, sum (ptb.assistTrack) as track88 " +
                "from playerTankBattle ptb left join " +
                "  playerTank pt on ptb.playerTankId=pt.id " +
                "where pt.playerId=@playerId " + battleModeWhere ;
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DataTable playerTotalsTable = DB.FetchData(sql);
            if (playerTotalsTable.Rows.Count > 0)
            {
                DataRow stats = playerTotalsTable.Rows[0];
                rpPR.bc = WNHelper.ConvertDbVal2Double(stats["bc"]);
                rpPR.win = WNHelper.ConvertDbVal2Double(stats["win"]) / rpPR.bc;
                rpPR.surv = WNHelper.ConvertDbVal2Double(stats["surv"]) / rpPR.bc;
                rpPR.dmg = WNHelper.ConvertDbVal2Double(stats["dmg"]) / rpPR.bc;
                rpPR.hit = WNHelper.ConvertDbVal2Double(stats["hit"]) / rpPR.bc;
                rpPR.bc88 = WNHelper.ConvertDbVal2Double(stats["bc88"]);
                rpPR.xp88 = WNHelper.ConvertDbVal2Double(stats["xp88"]) / rpPR.bc88;
                rpPR.radio88 = WNHelper.ConvertDbVal2Double(stats["radio88"]) / rpPR.bc;
                rpPR.track88 = WNHelper.ConvertDbVal2Double(stats["track88"]) / rpPR.bc;
            }
            return rpPR;
        }

        public static double CalcPlayerTotal(string battleMode)
        {
            return UseFormula(GetParamForPlayerTotal(battleMode));
        }

        public static double CalcBattleRange(string battleMode, string battleTimeFilter, int maxBattles = 0)
        {
            return UseFormula(GetParamForPlayerTotal(battleMode));
        }


        private static double Asinh(double x)
        {
            return Math.Log(x + Math.Sqrt(x * x + 1));
        }

        private static double UseFormula(RatingParametersPR rp)
        {
            double PR = 0;
            if (rp != null && rp.bc > 0)
            {
                // from: http://wiki.wargaming.net/en/Player_Ratings_(WoT)
                // calculate inner parts
                double inner1 = 3500d / (1d + Math.Exp(16 - (31 * rp.win)));
                double inner2 = 1400d / (1d + Math.Exp(8 - (27 * rp.surv)));
                double inner3 = 3700d * Asinh(0.0006 * rp.dmg);
                double inner4 = Math.Tanh(0.002 * rp.bc88) * (3900 * Asinh(0.0015 * rp.xp88));
                double inner5 = 1.4 * rp.radio88;
                double inner6 = 1.1 * rp.track88;
                // calculate inner
                double inner = inner1 + inner2 + inner3 + inner4 + inner5 + inner6;
                // total formula using inner
                PR = 540d * Math.Pow(rp.bc, 0.37) * Math.Tanh(0.00163 * Math.Pow(rp.bc, -0.37) * (inner));

                ////from: http://ftr.wot-news.com/2013/09/12/new-wg-personal-rating-analysis/
                //// calculate inner parts
                //double inner1 = 3000d / (1d + Math.Exp((0.5 - rp.win) / 0.03));
                //double inner2 = 7000 * Math.Max(0, (rp.surv - 0.2));
                //double inner3 = 6000 * Math.Max(0, (rp.hit - 0.45));
                //double inner4 = 5 * ((2/(1+Math.Exp(-rp.bc88/500))) -1);
                //double inner5 = Math.Max(0,rp.xp88-160);
                //double inner6 = Math.Max(0,rp.dmg-170);
                //// total formula using inner
                //PR = ((2 / (1 + Math.Exp(-rp.bc / 4500))) - 1) * (inner1 + inner2 + inner3 + inner4 + inner5);


            }
            // Return value
            return PR;
        }
    }
}
