using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WinApp.Code
{
	class DateTimeHelper
	{
		public static int ServerTimeReset = 7;

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

		// Returns a date time for when current day started according to server time reset
		public static DateTime GetTodayDateTimeStart()
		{
			DateTime basedate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ServerTimeReset, 0, 0); // base date = current date + server time reset
			if (DateTime.Now.Hour + (DateTime.Now.Minute/60) < ServerTimeReset) // if hours+minues from current time is before server time reset
				basedate = basedate.AddDays(-1); // current day = previous 
			return basedate;
		}
	}
}
