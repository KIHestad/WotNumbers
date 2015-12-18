using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
	public static class GridView
	{
		public static bool scheduleGridRefresh = false;
		public static bool refreshRunning = false;
		
		public enum Views
		{
			Overall = 0,
			Tank = 1,    // Same value used in database for colType in table columnList
			Battle = 2,  // Same value used in database for colType in table columnList
            Map = 3,
		}
	}
}
