using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
	class GrindingHelper
	{
		public static class Settings
		{
			public static int FirstVictoryFactor = 2;
			public static int EveryVictoryFactor = 0;
		}

        public class Progress
        {
            // Tank stats
            public int Battles { get; set; }
            public int Wins { get; set; }
            public int TotalXP { get; set; }
            public int AvgXP { get; set; }
            public int RealAvgXP { get; set; } // Need no input, just calc output
            // Grinding setup
            public int TargetXP { get; set; }
            public int ProgressXP { get; set; }
            public int RestXP { get; set; } // Need no input, just calc output
            public int ProgressPercent { get; set; } // Need no input, just calc output
            // Grinding progress
            public int ProgressGoal { get; set; }
            public DateTime? CompleationDate { get; set; }
            public int BtlPerDay { get; set; }
            public int RestBattles { get; set; } // Need no input, just calc output
            public int RestDays { get; set; } // Need no input, just calc output
        }

        public static Progress CalcProgress(Progress progress)
        {
            // Calc remaining xp used in sub calculations
            progress.RestXP = CalcProgressRestXP(progress.TargetXP, progress.ProgressXP);
            // Calc progress percent
            progress.ProgressPercent = GrindingHelper.CalcProgressPercent(progress.TargetXP, progress.ProgressXP);
            // If grinding progress is dependent of completion calculate battles per day to reach goal
            if (progress.ProgressGoal == 1)
            {
                // Calc max days to complete before grinding progress goal
                if (progress.CompleationDate != null)
                {
                    DateTime getComplDate = Convert.ToDateTime(progress.CompleationDate);
                    // Get max rest days
                    int maxRestDays = (getComplDate - DateTime.Now).Days + 1;
                    if (maxRestDays < 1)
                        maxRestDays = 1;
                    // Run a loop testing number of battles per day until goal is reached
                    int testBtlPerDay = 1;
                    int testRealAvgXP = CalcRealAvgXP(progress.Battles.ToString(), progress.Wins.ToString(), progress.TotalXP.ToString(), progress.AvgXP.ToString(), testBtlPerDay.ToString());
                    int testRestBattles = CalcRestBattles(progress.RestXP, testRealAvgXP);
                    while (CalcRestDays(progress.RestXP, testRealAvgXP, testBtlPerDay) > maxRestDays)
                    {
                        testBtlPerDay++;
                        testRealAvgXP = CalcRealAvgXP(progress.Battles.ToString(), progress.Wins.ToString(), progress.TotalXP.ToString(), progress.AvgXP.ToString(), testBtlPerDay.ToString());
                        testRestBattles = CalcRestBattles(progress.RestXP, testRealAvgXP);
                    }
                    progress.BtlPerDay = testBtlPerDay;
                }
            }
            // Calc values dependent of battles per day
            progress.RealAvgXP = CalcRealAvgXP(progress.Battles.ToString(), progress.Wins.ToString(), progress.TotalXP.ToString(), progress.AvgXP.ToString(), progress.BtlPerDay.ToString());
            progress.RestBattles = CalcRestBattles(progress.RestXP, progress.RealAvgXP);
            // Calc completion date and rest days according to progress type
            progress.RestDays = CalcRestDays(progress.RestXP, progress.RealAvgXP, progress.BtlPerDay);
            DateTime newCompleationDate = DateTime.Now.AddDays(progress.RestDays);
            progress.CompleationDate = new DateTime(newCompleationDate.Year, newCompleationDate.Month, newCompleationDate.Day);
            return progress;
        }

        public async static Task<bool> CheckForDailyRecalculateGrindingProgress()
        {
            bool grindingRecalcPerformed = false;
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            if (today != Config.Settings.lastGrindingProgressRecalc)
            {
                await RecalculateGrindingProgress();
                Config.Settings.lastGrindingProgressRecalc = today;
                string msg = "";
                Config.SaveConfig(out msg);
                grindingRecalcPerformed = true;
            }
            return grindingRecalcPerformed;
        }

        public async static Task RecalculateGrindingProgress()
        {
            // Get grinding data
            string sql =
                "SELECT playerTank.id as playerTankId, tank.name, gCurrentXP, gGrindXP, gGoalXP, gProgressXP, gBattlesDay, gComment, lastVictoryTime, " +
                "        gCompleationDate, gProgressGoal, " +
                "        SUM(playerTankBattle.battles) as battles, SUM(playerTankBattle.wins) as wins, " +
                "        MAX(playerTankBattle.maxXp) AS maxXP, SUM(playerTankBattle.xp) AS totalXP, " +
                "        SUM(playerTankBattle.xp) / SUM(playerTankBattle.battles) AS avgXP " +
                "FROM    tank INNER JOIN " +
                "        playerTank ON tank.id = playerTank.tankId INNER JOIN " +
                "        playerTankBattle ON playerTank.id = playerTankBattle.playerTankId " +
                "WHERE  playerTank.playerid = @playerId AND gGrindXP > 0 " +
                "GROUP BY playerTank.id, tank.name, gCurrentXP, gGrindXP, gGoalXP, gProgressXP, gBattlesDay, gComment, lastVictoryTime, gCompleationDate, gProgressGoal ";
            DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DataTable dt = DB.FetchData(sql);
            sql = "";
            foreach (DataRow grinding in dt.Rows)
            {
                // Get parameters for grinding calc
                Progress progress = new Progress();
                progress.ProgressXP = Convert.ToInt32(grinding["gProgressXP"]); // Added XP to previous progress
                progress.TargetXP = Convert.ToInt32(grinding["gGrindXP"]);
                progress.Battles = Convert.ToInt32(grinding["battles"]);
                progress.Wins = Convert.ToInt32(grinding["wins"]);
                progress.TotalXP = Convert.ToInt32(grinding["totalXP"]);
                progress.AvgXP = Convert.ToInt32(grinding["avgXP"]);
                // Set current progress
                progress.ProgressGoal = Convert.ToInt32(grinding["gProgressGoal"]);
                progress.CompleationDate = null;
                if (grinding["gCompleationDate"] != DBNull.Value)
                    progress.CompleationDate = Convert.ToDateTime(grinding["gCompleationDate"]);
                progress.BtlPerDay = Convert.ToInt32(grinding["gBattlesDay"]);
                // Calc new progress
                progress = CalcProgress(progress);
                // Save to playerTank
                sql +=
                    "UPDATE playerTank SET gProgressXP=@ProgressXP, gRestXP=@RestXP, gProgressPercent=@ProgressPercent, " +
                    "					   gRestBattles=@RestBattles, gRestDays=@RestDays, gCompleationDate=@CompleationDate, gBattlesDay=@BattlesDay " + Environment.NewLine + 
                    "WHERE id=@id; " + Environment.NewLine;
                DB.AddWithValue(ref sql, "@ProgressXP", progress.ProgressXP, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@RestXP", progress.RestXP, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@ProgressPercent", progress.ProgressPercent, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@RestBattles", progress.RestBattles, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@RestDays", progress.RestDays, DB.SqlDataType.Int);
                DateTime date = Convert.ToDateTime(progress.CompleationDate);
                DB.AddWithValue(ref sql, "@CompleationDate", new DateTime(date.Year, date.Month, date.Day), DB.SqlDataType.DateTime);
                DB.AddWithValue(ref sql, "@BattlesDay", progress.BtlPerDay, DB.SqlDataType.Int);
                int playerTankId = Convert.ToInt32(grinding["playerTankId"]);
                DB.AddWithValue(ref sql, "@id", playerTankId, DB.SqlDataType.Int);
            }
            await DB.ExecuteNonQueryAsync(sql, RunInBatch: true);
        }

        public static int CalcRealAvgXP(string Battles, string Wins, string TotalXP, string AvgXP, string BtlDay)
		{
			int b = 0;
			if (!Int32.TryParse(Battles, out b))
				return 0;
			else
			{
				if (b > 0)
				{
					double winRate = Convert.ToDouble(Wins) / Convert.ToDouble(Battles);
					if (winRate < 0.3) winRate = 0.3;
					if (winRate > 0.7) winRate = 0.7;
					double avgXP = Convert.ToDouble(AvgXP); // avg base XP
					double battles = Convert.ToDouble(Battles); // total battes played
					double btlDay = Convert.ToDouble(BtlDay); // battles per day
					if (btlDay == 0) btlDay = 1;
					double totXP = Convert.ToDouble(TotalXP); // total base XP earned
					double calc2XbattlesTotXP = avgXP * battles / btlDay * winRate; // calculated number of 2X battles played, assuming at least one victory every day played
					double calcExtraBonusTotXP = avgXP * 0.0 * battles * winRate; // caclulated an average of 0% extra for bonuses (3x/5x/2x every wins), apply only for wins
					double calcTotXP = totXP + calc2XbattlesTotXP + calcExtraBonusTotXP;
					return Convert.ToInt32(calcTotXP / battles);
				}
				else
					return 0;
			}
		}

		public static int CalcProgressPercent(int GrindXP, int ProgressXP)
		{
			int progressPercent = 0;
			if (GrindXP > 0)
				progressPercent = (ProgressXP * 100) / GrindXP;
			if (progressPercent > 100)
				progressPercent = 100;
			return progressPercent;	
		}

		public static int CalcProgressRestXP(int GrindXP, int ProgressXP)
		{
			int progressRest = GrindXP - ProgressXP;
			if (progressRest < 0)
				progressRest = 0;
			return progressRest;
		}

		public static int CalcRestBattles(int ProgressRestXp, int RealAvgXP)
		{
			if (RealAvgXP == 0)
				return 0;
			else
			{
				double d = Convert.ToDouble(ProgressRestXp) / RealAvgXP;
				return Convert.ToInt32(Math.Round(d.RoundUp(0), 0));
			}
		}

		public static int CalcRestDays(int ProgressRestXp, int RealAvgXP, int BattlesPerDay)
		{
			if (BattlesPerDay == 0) BattlesPerDay = 1;
			if (RealAvgXP > 0)
			{
				double d = Convert.ToDouble(ProgressRestXp) / (RealAvgXP * BattlesPerDay);
				return Convert.ToInt32(Math.Round(d.RoundUp(0), 0));
			}
			else
				return 0;
		}
	}
}
