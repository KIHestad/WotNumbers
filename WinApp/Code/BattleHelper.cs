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
			ModeSpecial = 5,
			ModeStronghold = 6,
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
				case MainBattleMode.ModeSpecial:
					return "Special";
				case MainBattleMode.ModeStronghold:
					return "Stronghold";

			}
			return "";
		}

		public static string GetBattleModeReadableName(string battleMode)
		{
			string battleModeReadableName = "";
			switch (battleMode)
			{
				case "15": battleModeReadableName = "Random / TC"; break;
				case "7": battleModeReadableName = "Team"; break;
				case "Historical": battleModeReadableName = "Historical"; break;
				case "Skirmishes": battleModeReadableName = "Skirmishes"; break;
				case "Special": battleModeReadableName = "Special"; break;
				case "Stronghold": battleModeReadableName = "Stronghold"; break;
				case "": battleModeReadableName = "All Modes"; break;
			}
			return battleModeReadableName;
		}
	}
}
