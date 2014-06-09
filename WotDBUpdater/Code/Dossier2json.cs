using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using IronPython.Hosting;

namespace WinApp.Code
{
	public static class dossier2json
	{
		public static FileSystemWatcher dossierFileWatcher = new FileSystemWatcher();

		private static string LogText(string logtext)
		{
			return DateTime.Now + " " + logtext;
		}

		public static string UpdateDossierFileWatcher()
		{
			string logtext = "Dossier file listener stopped";
			bool run = (Config.Settings.dossierFileWathcherRun == 1);
			if (run)
			{
				try
				{
					logtext = "Dossier file listener started";
					dossierFileWatcher.Path = Path.GetDirectoryName(Config.Settings.dossierFilePath + "\\");
					dossierFileWatcher.Filter = "*.dat";
					dossierFileWatcher.NotifyFilter = NotifyFilters.LastWrite;
					dossierFileWatcher.Changed += new FileSystemEventHandler(DossierFileChanged);
					dossierFileWatcher.EnableRaisingEvents = true;
				}
				catch (Exception)
				{
					Code.MsgBox.Show("Error in dossier file path, please check your application settings", "Error in dossier file path");
					run = false;
				}
			}
			dossierFileWatcher.EnableRaisingEvents = run;
			Log.LogToFile(logtext,true);
			return logtext;
		}

		public static string ManualRun(bool TestRunPrevJsonFile = false, bool ForceUpdate = false)
		{
			string returVal = "Manual dossier file check started...";
			Log.CheckLogFileSize();
			List<string> logText = new List<string>();
			bool ok = true;
			String dossierFile = "";
			if (!TestRunPrevJsonFile)
			{
				// Dossier file manual handling - get all dossier files
				logText.Add(LogText("Manual run, looking for new dossier file"));
				if (Directory.Exists(Config.Settings.dossierFilePath))
				{
					string[] files = Directory.GetFiles(Config.Settings.dossierFilePath, "*.dat");
					DateTime dossierFileDate = new DateTime(1970, 1, 1);
					foreach (string file in files)
					{
						FileInfo checkFile = new FileInfo(file);
						if (checkFile.LastWriteTime > dossierFileDate)
						{
							dossierFile = checkFile.FullName;
							dossierFileDate = checkFile.LastWriteTime;
						}
					}
					if (dossierFile == "")
					{
						logText.Add(LogText(" > No dossier file found"));
						returVal = "No dossier file found - check Application Settings";
						ok = false;
					}
					else
					{
						logText.Add(LogText(" > Dossier file found"));
					}
				}
				else
				{
					logText.Add(LogText(" > Inncorrect path to dossier file, check Application Settings."));
					returVal = "Inncorrect path to dossier file - check Application Settings";
					ok = false;
				}
			}
			else
			{
				logText.Add(LogText("Test run, using latest converted json file"));
			}
			if (ok)
			{
				List<string> newLogText = CopyAndConvertFile(out returVal, dossierFile, TestRunPrevJsonFile, ForceUpdate);
				foreach (string s in newLogText)
				{
					logText.Add(s);
				}
			}
			Log.LogToFile(logText);
			return returVal;
		}

		private static void DossierFileChanged(object source, FileSystemEventArgs e)
		{
			Log.CheckLogFileSize();
			List<string> logText = new List<string>();
			logText.Add(LogText("Dossier file listener detected updated dossier file"));
			// Dossier file automatic handling
			// Stop listening to dossier file
			dossierFileWatcher.EnableRaisingEvents = false;
			//Log("Dossier file updated");
			// Get config data
			string dossierFile = e.FullPath;
			FileInfo file = new FileInfo(dossierFile);
			// Wait until file is ready to read, 
			List<string> logtextnew1 = WaitUntilFileReadyToRead(dossierFile, 4000);
			// Perform file conversion from picle til json
			string statusResult;
			List<string> logTextNew2 = CopyAndConvertFile(out statusResult, dossierFile);
			// Add logtext
			foreach (string s in logtextnew1)
			{
				logText.Add(s);
			}
			foreach (string s in logTextNew2)
			{
				logText.Add(s);
			}
			
			// Continue listening to dossier file
			dossierFileWatcher.EnableRaisingEvents = true;
			// Save log to textfile
			Log.LogToFile(logText);
		}

		private static List<string> WaitUntilFileReadyToRead(string filePath, int maxWaitTime)
		{
			// Checks file is readable
			List<string> logtext = new List<string>();
			bool fileOK = false;
			int waitInterval = 100; // time to wait in ms per read operation to check filesize
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			while (stopWatch.ElapsedMilliseconds < maxWaitTime && !fileOK)
			{
				try
				{
					using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
					{
						fileOK = true;
						TimeSpan ts = stopWatch.Elapsed;
						logtext.Add(LogText(String.Format(" > Dossierfile read successful (waited: {0:0000}ms)", stopWatch.ElapsedMilliseconds.ToString())));
					}
				}
				catch
				{
					// could not read file
					logtext.Add(LogText(String.Format(" > Dossierfile not ready yet (waited: {0:0000}ms)", stopWatch.ElapsedMilliseconds.ToString())));
					System.Threading.Thread.Sleep(waitInterval);
				}
			}
			stopWatch.Stop();
			return logtext;
		}

		private static List<string> CopyAndConvertFile(out string statusResult, string dossierFile, bool testRunPrevJsonFile = false, bool forceUpdate = false)
		{
			// Copy dossier file and perform file conversion til json format
			List<string> logText = new List<string>();
			string appPath = Path.GetDirectoryName(Application.ExecutablePath); // path to app dir
			string dossier2jsonScript = appPath + "/dossier2json/wotdc2j.py"; // python-script for converting dossier file
			string dossierDatNewFile = appPath + "/dossier2json/dossier.dat"; // new dossier file
			string dossierDatPrevFile = appPath + "/dossier2json/dossier_prev.dat"; // previous dossier file
			string dossierJsonFile = appPath + "/dossier2json/dossier.json"; // output file
			string returVal = "Starting file handling...";
			//try
			//{
				bool ok = true;
				if (!testRunPrevJsonFile)
				{
					FileInfo fileDossierOriginal = new FileInfo(dossierFile); // the original dossier file
					fileDossierOriginal.CopyTo(dossierDatNewFile, true); // copy original dossier fil and rename it for analyze
					if (File.Exists(dossierDatPrevFile)) // check if previous file exist, and new one is different (skip if testrun)
					{
						FileInfo fileInfonew = new FileInfo(dossierDatNewFile); // the new dossier file
						FileInfo fileInfoprev = new FileInfo(dossierDatPrevFile); // the previous dossier file
						if (dossier2json.FilesContentsAreEqual(fileInfonew, fileInfoprev))
						{
							// Files are identical, skip convert
							logText.Add(LogText(" > File skipped, same content as previos"));
							returVal = "Manual dossier file check skipped - same content as previous";
							fileInfonew.Delete();
							ok = false;
						}
					}
				}
				if (!testRunPrevJsonFile && ok) // Convert file to json (skip if testrun)
				{
					string result = dossier2json.ConvertDossierUsingPython(dossier2jsonScript, dossierDatNewFile); // convert to json
					if (result != "") // error occured
					{
						logText.Add(result);
						returVal = "Error converting dossier file to json - check log file";
						ok = false;
					}
					else
					{
						logText.Add(LogText(" > Successfully convertet dossier file to json"));
						// Move new file as previos (copy and delete)
						FileInfo fileInfonew = new FileInfo(dossierDatNewFile); // the new dossier file
						fileInfonew = new FileInfo(dossierDatNewFile); // the new dossier file
						fileInfonew.CopyTo(dossierDatPrevFile, true); // copy and rename dossier file
						fileInfonew.Delete();
						logText.Add(LogText(" > Renamed copied dossierfile as previous file"));
					}
				}
				if (ok) // Analyze json file and add to db
				{
					if (File.Exists(dossierJsonFile))
					{
						returVal = Dossier2db.ReadJson(dossierJsonFile, forceUpdate);
						logText.Add(LogText(" > " + returVal));
					}
					else
					{
						logText.Add(LogText(" > No json file found"));
						returVal = "No previous dossier file found - run manual check";
					}
				}
			//}
			//catch (Exception ex)
			//{
			//	logText.Add(LogText(" > General file copy or conversion error: " + ex.Message));
			//	returVal = "General file copy or conversion error - check log file";
			//}
			Log.LogToFile(logText);
			statusResult = returVal;
			return logText;
		}

		private static string ConvertDossierUsingPython(string dossier2jsonScript, string dossierDatFile)
		{
			// Convert to json format using python conversion from cPicle stream format
			
			// //Use ProcessStartInfo class to run python 
			//ProcessStartInfo startInfo = new ProcessStartInfo();
			//startInfo.CreateNoWindow = false;
			//startInfo.UseShellExecute = false;
			//startInfo.FileName = "c:\\python27\\python.exe";
			//startInfo.WindowStyle = ProcessWindowStyle.Normal;
			//startInfo.Arguments = dossier2jsonScript + " " + dossierDatFile + " -f -r";
			//try
			//{
			//	// Start the process with the info we specified.
			//	// Call WaitForExit and then the using statement will close.
			//	using (Process exeProcess = Process.Start(startInfo))
			//	{
			//		exeProcess.WaitForExit();
			//	}
			//}
			//catch (Exception ex)
			//{
			//	return LogText(ex.Message);
			//}

			// Use IronPython
			try
			{
				var ipy = Python.CreateRuntime();
				dynamic ipyrun = ipy.UseFile(dossier2jsonScript);
				ipyrun.main();
			}
			catch (Exception ex)
			{
				Code.MsgBox.Show("Error running Python script converting dossier file: " + ex.Message, "Error converting dossier file to json");
				return "Error converting dossier file to json";
			}
			return "";
		}

		private static bool FilesContentsAreEqual(FileInfo fileInfo1, FileInfo fileInfo2)
		{
			bool result;
			if (fileInfo1.Length != fileInfo2.Length)
			{
				result = false;
			}
			else
			{
				using (var file1 = fileInfo1.OpenRead())
				{
					using (var file2 = fileInfo2.OpenRead())
					{
						result = StreamsContentsAreEqual(file1, file2);
					}
				}
			}
			return result;
		}

		private static bool StreamsContentsAreEqual(Stream stream1, Stream stream2)
		{
			const int bufferSize = 2048 * 2;
			var buffer1 = new byte[bufferSize];
			var buffer2 = new byte[bufferSize];

			while (true)
			{
				int count1 = stream1.Read(buffer1, 0, bufferSize);
				int count2 = stream2.Read(buffer2, 0, bufferSize);

				if (count1 != count2)
				{
					return false;
				}

				if (count1 == 0)
				{
					return true;
				}

				int iterations = (int)Math.Ceiling((double)count1 / sizeof(Int64));
				for (int i = 0; i < iterations; i++)
				{
					if (BitConverter.ToInt64(buffer1, i * sizeof(Int64)) != BitConverter.ToInt64(buffer2, i * sizeof(Int64)))
					{
						return false;
					}
				}
			}
		}
	
	}
}
