using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinApp.Code
{
	class PlayerHelper
	{
		public static string GetPlayerNameFromNameAndServer(string playerNameAndServer)
		{
			string playerName = "";
			int pos = playerNameAndServer.IndexOf(" ");
			if (pos > 0)
				playerName = playerNameAndServer.Substring(0, pos);
			return playerName;
		}
	}
}
