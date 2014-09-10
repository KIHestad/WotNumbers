using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WinApp.Code
{
	class DateTimeHelper
	{
		public static DateTime ConvertFromUnixTimestamp(double timestamp)
		{
			DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return origin.AddSeconds(timestamp);
		}

		public static DateTime AdjustForTimeZone(DateTime timeToAdjust)
		{
			TimeZone currentTimeZone = TimeZone.CurrentTimeZone;
			TimeSpan offset = currentTimeZone.GetUtcOffset(DateTime.Now);
			return timeToAdjust.AddHours(offset.Hours);
		}
	}
}
