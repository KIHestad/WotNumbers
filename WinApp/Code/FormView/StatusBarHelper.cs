using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinApp.Code
{
	public static class StatusBarHelper
	{
		public static string Message = "";
		public static bool ClearAfterNextShow = true;

		public static bool MessageExists
		{
			get { return (Message != ""); }
		}

		public static void CheckForClear()
		{
			if (ClearAfterNextShow)
				Message = "";
		}
	}
}
