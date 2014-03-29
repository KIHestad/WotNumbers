using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotDBUpdater
{
	public class Log
	{
		private static string path = Path.GetDirectoryName(Application.ExecutablePath); // Log path
		private static string filename = "/log.txt"; // Log filename

		public static void CheckLogFileSize()
		{
			if (File.Exists(path + filename))
			{
				FileInfo file = new FileInfo(path + filename);
				if (file.Length > 1024*1024*20) // max 20 MB
				{
					string movefilename = "/log_" + DateTime.Now.ToString("yyyy-MM-dd_HHmm") + ".txt";
					file.CopyTo(path + movefilename);
					file.Delete();
					CreateFileIfNotExist();
				}
			}
		}

		public static void LogToFile(string logtext, bool addDateTime = false)
		{
			// Add list og Strings
			CreateFileIfNotExist();
			using (StreamWriter sw = File.AppendText(path + filename))
			{
				sw.WriteLine(logtext);
			}
		}

		public static void LogToFile(List<string> logtext, bool addDateTime = false)
		{
			// Add list og Strings
			CreateFileIfNotExist();
			using (StreamWriter sw = File.AppendText(path + filename))
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
			if (!File.Exists(path + filename))
			{
				// Create a file to write to. 
				using (StreamWriter sw = File.CreateText(path + filename))
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

		private static string battleResultDoneFile = "/Dossier2json/LastBattle.txt";

		public static void BattleResultDoneLog()
		{
			using (StreamWriter sw = File.CreateText(path + battleResultDoneFile))
			{
				sw.WriteLine("last battle: " + DateTime.Now.ToString());
				sw.Close();
			}
		}

		public static string BattleResultDoneLogFileName()
		{
			if (!File.Exists(path + battleResultDoneFile))
			using (StreamWriter sw = File.CreateText(path + "/Dossier2json/LastBattle.txt"))
			{
				sw.WriteLine("no battles logged");
				sw.Close();
			}
			return path + battleResultDoneFile;
		}



	}
}
