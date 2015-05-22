using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinApp.Code
{
	class BattleResultRetriever
	{
		public static string BrrFile
		{
			get
			{
				string wotFile = Config.Settings.wotGameFolder;
				if (wotFile.Substring(wotFile.Length - 1, 1) != "\\")
					wotFile += "\\";
				return wotFile += "res_mods\\0.9.7\\scripts\\client\\mods\\BRR.pyc";
			}
			set {}
		}

		public static string BrrFolder
		{
			get
			{
				string wotFolder = Config.Settings.wotGameFolder;
				if (wotFolder.Substring(wotFolder.Length - 1, 1) != "\\")
					wotFolder += "\\";
				return wotFolder += "res_mods\\0.9.7\\scripts\\client\\mods\\";
			}
			set {}
		}

		public static bool Installed
		{
			get
			{
				return File.Exists(BattleResultRetriever.BrrFile);
			}
			set {}
		}


		public static bool Install(out string msg)
		{
			bool ok = true;
			msg = "";
			try
			{
				if (!Directory.Exists(BrrFolder))
					Directory.CreateDirectory(BrrFolder);
				string fileToCopy = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\BRR.pyc";
				File.Copy(fileToCopy, BrrFile);
			}
			catch (Exception ex)
			{
				msg = ex.Message;
				ok = false;
			}
			return ok;
		}

		public static bool Uninstall(out string msg)
		{
			bool ok = true;
			msg = "";
			try
			{
				File.Delete(BrrFile);
			}
			catch (Exception ex)
			{
				msg = ex.Message;
				ok = false;
			}
			return ok;
		}

		

	}
}
