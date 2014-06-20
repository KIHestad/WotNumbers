using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApp.Code
{
	public class Log
	{
		private static string filename = "Log.txt"; // Log filename
		private static string battleResultDoneFile = "LastBattle.txt";

		public static void CheckLogFileSize()
		{
			if (File.Exists(Config.AppDataBaseLogFolder + filename))
			{
				FileInfo file = new FileInfo(Config.AppDataBaseLogFolder + filename);
				if (file.Length > 1024*1024*5) // max 5 MB
				{
					string movefilename = "Log_" + DateTime.Now.ToString("yyyy-MM-dd_HHmm") + ".txt";
					file.CopyTo(Config.AppDataBaseLogFolder + movefilename);
					file.Delete();
					CreateFileIfNotExist();
				}
			}
		}

		public static void LogToFile(string logtext, bool addDateTime = false)
		{
			// Add list og Strings
			CreateFileIfNotExist();
			using (StreamWriter sw = File.AppendText(Config.AppDataBaseLogFolder + filename))
			{
				sw.WriteLine(logtext);
			}
		}

		public static void LogToFile(List<string> logtext, bool addDateTime = false)
		{
			// Add list og Strings
			CreateFileIfNotExist();
			using (StreamWriter sw = File.AppendText(Config.AppDataBaseLogFolder + filename))
			{
				sw.WriteLine("");
				foreach (var s in logtext)
				{
					sw.WriteLine(s);
				}
			}
		}

		private static void CreateFileIfNotExist()
		{
			// This text is added only once to the file. 
			if (!File.Exists(Config.AppDataBaseLogFolder + filename))
			{
				// Create a file to write to. 
				using (StreamWriter sw = File.CreateText(Config.AppDataBaseLogFolder + filename))
				{
					sw.WriteLine("**************************************************");
					sw.WriteLine("Start logging: " + DateTime.Now.ToString());
					sw.WriteLine("**************************************************");
					sw.WriteLine("");
				}
			}
		}

		private static string AddDateTime(string logtext, bool addDateTime)
		{
			string s = logtext;
			if (addDateTime) s = DateTime.Now.ToString() + " " + s;
			return s;
		}

		
		public static void BattleResultDoneLog()
		{
			using (StreamWriter sw = File.CreateText(Config.AppDataBaseFolder + battleResultDoneFile))
			{
				sw.WriteLine("last battle: " + DateTime.Now.ToString());
				sw.Close();
			}
		}

		public static string BattleResultDoneLogFileName()
		{
			if (!File.Exists(Config.AppDataBaseFolder + battleResultDoneFile))
			{
				using (StreamWriter sw = File.CreateText(Config.AppDataBaseFolder + battleResultDoneFile))
				{
					sw.WriteLine("no battles logged");
					sw.Close();
				}
			}
			return Config.AppDataBaseFolder + battleResultDoneFile;
		}



	}
}
