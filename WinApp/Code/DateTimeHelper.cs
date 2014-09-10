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

		//public static DataRow AdjustForTimeZone(DataRow playerTankRow)
		//{
		//	TimeZone currentTimeZone = TimeZone.CurrentTimeZone;
		//	TimeSpan offset = currentTimeZone.GetUtcOffset(DateTime.Now);

		//	if (playerTankRow["creationTime"] != DBNull.Value)
		//		playerTankRow["creationTime"] = Convert.ToDateTime(playerTankRow["creationTime"]).AddHours(offset.Hours);
		//	if (playerTankRow["updatedTime"] != DBNull.Value)
		//		playerTankRow["updatedTime"] = Convert.ToDateTime(playerTankRow["updatedTime"]).AddHours(offset.Hours);
		//	if (playerTankRow["lastBattleTime"] != DBNull.Value)
		//		playerTankRow["lastBattleTime"] = Convert.ToDateTime(playerTankRow["lastBattleTime"]).AddHours(offset.Hours);

		//	return playerTankRow;
		//}

		public static DateTime AdjustForTimeZone(DateTime timeToAdjust)
		{
			TimeZone currentTimeZone = TimeZone.CurrentTimeZone;
			TimeSpan offset = currentTimeZone.GetUtcOffset(DateTime.Now);
			return timeToAdjust.AddHours(offset.Hours);
		}
	}
}
