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
			get {return BrrFolder + "BRR.pyc";}
			set {}
		}

		public static string InitFile
		{
			get {return BrrFolder + "__init__.pyc";}
			set { }
		}

		public static string BrrFolder
		{
			get
			{
				string wotFolder = Config.Settings.wotGameFolder;
				if (wotFolder != "" && wotFolder.Substring(wotFolder.Length - 1, 1) != "\\")
					wotFolder += "\\";
				return wotFolder += "res_mods\\0.9.7\\scripts\\client\\mods\\";
			}
			set {}
		}

		public static bool IsWoTGameFolderOK()
		{
			string msg = "";
			bool woTGameFolderOK = false;
			if (Config.Settings.wotGameFolder == "")
			{
				if (Directory.Exists("C:\\Games\\World_of_Tanks"))
				{
					Config.Settings.wotGameFolder = "C:\\Games\\World_of_Tanks";
					Config.SaveConfig(out msg);
					woTGameFolderOK = true;
				}
				else if (Directory.Exists("D:\\Games\\World_of_Tanks"))
				{
					Config.Settings.wotGameFolder = "D:\\Games\\World_of_Tanks";
					Config.SaveConfig(out msg);
					woTGameFolderOK = true;
				}
			}
			if (!woTGameFolderOK)
				woTGameFolderOK = Directory.Exists(Config.Settings.wotGameFolder);
			return woTGameFolderOK;
		}

		public static bool Installed
		{
			get
			{
				int files = 0;
				if (File.Exists(BattleResultRetriever.BrrFile))
					files++;
				if (File.Exists(BattleResultRetriever.InitFile))
					files++;
				return (files == 2);
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
				if (!File.Exists(BrrFile))
				{
					string fileToCopy = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\BRR.pyc";
					File.Copy(fileToCopy, BrrFile);
				}
				if (!File.Exists(InitFile))
				{
					string fileToCopy = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\__init__.pyc";
					File.Copy(fileToCopy, InitFile);
				}
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
