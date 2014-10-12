using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinApp.Code
{
	public class BattleHelper
	{
		public enum MainBattleMode
		{
			ModeRandom_TC = 1,
			ModeTeam = 2,
			ModeHistorical = 3,
			ModeSkirmishes = 4,
		}

		public static string GetSQLMainBattleMode(MainBattleMode mainBattleMode)
		{
			switch (mainBattleMode)
			{
				case MainBattleMode.ModeRandom_TC:
					return "15";
				case MainBattleMode.ModeTeam:
					return "7";
				case MainBattleMode.ModeHistorical:
					return "Historical";
				case MainBattleMode.ModeSkirmishes:
					return "Skirmishes";
			}
			return "";
		}

	}
}
