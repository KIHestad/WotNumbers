using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinApp.Code
{
	class RunBattleCheckHelper
	{
		public enum RunBattleCheckMode
		{
			Cancelled = 1,
			NormalMode = 2,
			ForceUpdateAll = 3,
		}

		public static RunBattleCheckMode CurrentBattleCheckMode = RunBattleCheckMode.Cancelled;
	}

}
