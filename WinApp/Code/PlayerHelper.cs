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

		public static string GetServerFromNameAndServer(string playerNameAndServer)
		{
			string server = "";
			int pos = playerNameAndServer.IndexOf(" (");
			if (pos > 0)
				server = playerNameAndServer.Substring(pos +1);
			pos = playerNameAndServer.IndexOf(")");
			if (pos > 0)
				server = playerNameAndServer.Substring(0, pos);
			return server;
		}
	}
}
