using System;
using System.Collections.Generic;
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
