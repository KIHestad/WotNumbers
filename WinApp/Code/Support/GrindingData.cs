using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
	class GrindingData
	{
		public static class Settings
		{
			public static int FirstVictoryFactor = 2;
			public static int EveryVictoryFactor = 0;
		}

		public static int CalcAvgXP(string Battles, string Wins, string TotalXP, string AvgXP, string BtlDay)
		{
			double winRate = Convert.ToDouble(Wins) / Convert.ToDouble(Battles);
			if (winRate < 0.3) winRate = 0.3;
			if (winRate > 0.7) winRate = 0.7;
			double avgXP = Convert.ToDouble(AvgXP); // avg base XP
			double battles = Convert.ToDouble(Battles); // total battes played
			double btlDay = Convert.ToDouble(BtlDay); // battles per day
			if (btlDay == 0) btlDay = 2;
			double totXP = Convert.ToDouble(TotalXP); // total base XP earned
			double calc2XbattlesTotXP = avgXP * battles / btlDay * winRate; // calculated number of 2X battles played, assuming at least one victory every day played
			double calcExtraBonusTotXP = avgXP * 0.2 * battles * winRate; // caclulated an average of 20% extra for bonuses (3x/5x/2x every wins), apply only for wins
			double calcTotXP = totXP + calc2XbattlesTotXP + calcExtraBonusTotXP;
			return Convert.ToInt32(calcTotXP / battles);
		}
	}
}
