using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
	class BattleModeHelper
	{
		public static string GetShortmenuName(string menuName)
		{
			string shortMenuname = menuName;
			if (menuName.IndexOf("(") > 0) // remove text in paranthes
			{
				shortMenuname = menuName.Substring(0, menuName.IndexOf("(")).Trim();
			}
			return shortMenuname;
		}
	}
}
