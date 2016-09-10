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
		public static List<string> logBuffer = new List<string>();

		#region logBuffer
		
		public static void WriteLogBuffer(bool forceLogging = false)
		{
			bool loggingOk = true;
			if (Config.Settings.showDBErrors || forceLogging)
			{
				Application.DoEvents();
				loggingOk = LogToFileLogBuffer(logBuffer, false);
			}
			if (loggingOk)
				logBuffer = new List<string>();
		}

		public static void AddToLogBuffer(string logtext, bool addDateTime = true)
		{
			if (addDateTime) logtext = DateTime.Now + "\t" + logtext;
			logBuffer.Add(logtext);
		}

		public static void AddIpyToLogBuffer(string logtext)
		{
			string ipyLog = "";
			ipyLog += DateTime.Now + "\t" + " > > IronPython output:" + Environment.NewLine ;
			ipyLog += logtext;
			logBuffer.Add(ipyLog);
		}

		private static string ReadFromStream(MemoryStream ms) {
			int length = (int)ms.Length;
			Byte[] bytes = new Byte[length];
 
			ms.Seek(0, SeekOrigin.Begin);
			ms.Read(bytes, 0, (int)ms.Length);
 
			return Encoding.GetEncoding("utf-8").GetString(bytes, 0, (int)ms.Length);
		}
   

		public static void AddToLogBuffer(List<string> logtext, bool addDateTime = true)
		{
			string d = "";
			if (addDateTime) d = DateTime.Now + "\t";
			foreach (string txt in logtext)
			{
				logBuffer.Add(d + txt);	
			}
		}

		#endregion

		#region Direct logging

		public static void LogToFile(Exception ex, string customErrorMsg = "")
		{
			// Add list og Strings
			if (CreateFileIfNotExist())
			{
				// Write current logbuffer first, force log logbuffer to include recent logging
				WriteLogBuffer(true);
				// Log exception
				using (StreamWriter sw = File.AppendText(Config.AppDataLogFolder + filename))
				{
					if (ex != null)
					{
						string logtext = Environment.NewLine;
						logtext += "{" + Environment.NewLine; 
						logtext += DateTime.Now + " ### EXCEPTION ###" + Environment.NewLine;
						logtext += "   Source:          " + ex.Source + Environment.NewLine;
						logtext += "   TargetSite:      " + ex.TargetSite + Environment.NewLine;
						logtext += "   Data:            " + ex.Data + Environment.NewLine;
						logtext += "   Message:         " + ex.Message + Environment.NewLine;
						if (ex.InnerException != null && ex.InnerException.ToString() != "")
							logtext += "   InnerException:  " + ex.InnerException + Environment.NewLine;
						logtext += "   Stack Trace: " + Environment.NewLine + ex.StackTrace + Environment.NewLine;
						if (customErrorMsg != "")
							logtext += "   Details: " + Environment.NewLine + "   " + customErrorMsg + Environment.NewLine;
						logtext += "}" + Environment.NewLine + Environment.NewLine; 
						sw.WriteLine(logtext);
					}
				}
			}
		}


		public static void LogToFile(string logtext, bool addDateTime = true)
		{
			if (Config.Settings.showDBErrors)
			{
				// Write current logbuffer first
				WriteLogBuffer();
				// Add list og Strings
				if (CreateFileIfNotExist())
				{
					using (StreamWriter sw = File.AppendText(Config.AppDataLogFolder + filename))
					{
						if (addDateTime) logtext = DateTime.Now + "\t" + logtext;
						sw.WriteLine(logtext);
					}
				}
			}
		}

		// Used to write logbuffer
		private static bool LogToFileLogBuffer(List<string> logtext, bool addDateTime = false)
		{
			bool loggingOK = true;
			try
			{
				if (Config.Settings.showDBErrors)
				{
					// Add list of Strings
					if (CreateFileIfNotExist())
					{
						using (StreamWriter sw = File.AppendText(Config.AppDataLogFolder + filename))
						{
							foreach (var s in logtext)
							{
								sw.WriteLine(s);
							}
						}
					}
				}
			}
			catch (Exception)
			{
				// Ignore error
				loggingOK = false;
			}
			return loggingOK;
		}

		#endregion

		public static void CheckLogFileSize()
		{
			if (File.Exists(Config.AppDataLogFolder + filename))
			{
				FileInfo file = new FileInfo(Config.AppDataLogFolder + filename);
				if (file.Length > 1024 * 1024 * 5) // max 5 MB
				{
					string movefilename = "Log_" + DateTime.Now.ToString("yyyy-MM-dd_HHmm") + ".txt";
					file.CopyTo(Config.AppDataLogFolder + movefilename);
					file.Delete();
					CreateFileIfNotExist();
				}
			}
			else
			{
				CreateFileIfNotExist();
			}
		}

		private static bool CreateFileIfNotExist()
		{
			bool ok = true;
			// This text is added only once to the file. 
			if (!File.Exists(Config.AppDataLogFolder + filename))
			{
				try
				{
					// Create a file to write to. 
					using (StreamWriter sw = File.CreateText(Config.AppDataLogFolder + filename))
					{
						sw.WriteLine("**************************************************");
						sw.WriteLine("Start logging: " + DateTime.Now.ToString());
						sw.WriteLine("**************************************************");
						sw.WriteLine("");
					}
				}
				catch (Exception ex)
				{
					Code.MsgBox.Show("Error creating log file: " + Config.AppDataLogFolder + filename + Environment.NewLine + Environment.NewLine +
						ex.Message + Environment.NewLine + Environment.NewLine, "Log file error");
					ok = false;
				}
			}
			return ok;
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
