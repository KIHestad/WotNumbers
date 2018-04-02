using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code.Rating
{
    public class PR
    {
        public class RatingParametersPR
        {
            public RatingParametersPR()
            {
                BC = 0;
                Win = 0;
                Surv = 0;
                Dmg = 0;
                Hit = 0;
                BC88 = 0;
                Xp88 = 0;
                Radio88 = 0;
                Track88 = 0;
            }
            public double BC { get; set; } // ptb.battles          - battle.battlesCount 
            public double Win { get; set; } // ptb.wins            - battle.victory 
            public double Surv { get; set; } // ptb.survived       - battle.survived 
            public double Dmg { get; set; } // ptb.dmg             - battle.dmg 
            public double Hit { get; set; } // ptb.hits            - battle.hits 
            public double BC88 { get; set; } // ptb.battles8p      - battle.battlesCount 
            public double Xp88 { get; set; } // ptb.xp8p           - battle.xp       
            public double Radio88 { get; set; } // ptb.assistSpot  - battle.assistSpot 
            public double Track88 { get; set; } // ptb.assistTrack - battle.assistTrack 
        }

        private async static Task<RatingParametersPR> GetParamForPlayerTotal(string battleMode)
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
            DataTable playerTotalsTable = await DB.FetchData(sql);
            if (playerTotalsTable.Rows.Count > 0)
            {
                DataRow stats = playerTotalsTable.Rows[0];
                rpPR.BC = WNHelper.ConvertDbVal2Double(stats["bc"]);
                rpPR.Win = WNHelper.ConvertDbVal2Double(stats["win"]) / rpPR.BC;
                rpPR.Surv = WNHelper.ConvertDbVal2Double(stats["surv"]) / rpPR.BC;
                rpPR.Dmg = WNHelper.ConvertDbVal2Double(stats["dmg"]) / rpPR.BC;
                rpPR.Hit = WNHelper.ConvertDbVal2Double(stats["hit"]) / rpPR.BC;
                rpPR.BC88 = WNHelper.ConvertDbVal2Double(stats["bc88"]);
                rpPR.Xp88 = WNHelper.ConvertDbVal2Double(stats["xp88"]) / rpPR.BC88;
                rpPR.Radio88 = WNHelper.ConvertDbVal2Double(stats["radio88"]) / rpPR.BC;
                rpPR.Track88 = WNHelper.ConvertDbVal2Double(stats["track88"]) / rpPR.BC;
            }
            return rpPR;
        }

        public async static Task<double> CalcPlayerTotal(string battleMode)
        {
            return UseFormula(await GetParamForPlayerTotal(battleMode));
        }

        public async static Task<double> CalcBattleRange(string battleMode, string battleTimeFilter, int maxBattles = 0)
        {
            return UseFormula(await GetParamForPlayerTotal(battleMode));
        }


        private static double Asinh(double x)
        {
            return Math.Log(x + Math.Sqrt(x * x + 1));
        }

        private static double UseFormula(RatingParametersPR rp)
        {
            double PR = 0;
            if (rp != null && rp.BC > 0)
            {
                // from: http://wiki.wargaming.net/en/Player_Ratings_(WoT)
                // calculate inner parts
                double inner1 = 3500d / (1d + Math.Exp(16 - (31 * rp.Win)));
                double inner2 = 1400d / (1d + Math.Exp(8 - (27 * rp.Surv)));
                double inner3 = 3700d * Asinh(0.0006 * rp.Dmg);
                double inner4 = Math.Tanh(0.002 * rp.BC88) * (3900 * Asinh(0.0015 * rp.Xp88));
                double inner5 = 1.4 * rp.Radio88;
                double inner6 = 1.1 * rp.Track88;
                // calculate inner
                double inner = inner1 + inner2 + inner3 + inner4 + inner5 + inner6;
                // total formula using inner
                PR = 540d * Math.Pow(rp.BC, 0.37) * Math.Tanh(0.00163 * Math.Pow(rp.BC, -0.37) * (inner));

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
