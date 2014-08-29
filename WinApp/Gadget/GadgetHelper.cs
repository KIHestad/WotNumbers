using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinApp.Gadget
{
	class GadgetHelper
	{
		public enum TimeRange
		{
			Total = 0,
			Num1000 = 1,
			TimeMonth = 2,
			TimeWeek = 3,
			TimeToday = 4,
			Num5000 = 5,
		}

		public static TimeRange SelectedTimeRangeEFF = TimeRange.Total;
		public static TimeRange SelectedTimeRangeWN7 = TimeRange.Total;
		public static TimeRange SelectedTimeRangeWN8 = TimeRange.Total;
		public static TimeRange SelectedTimeRangeWR = TimeRange.Total;
	}
}
