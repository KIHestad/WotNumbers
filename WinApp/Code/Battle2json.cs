using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using Newtonsoft.Json.Linq;

namespace WinApp.Code
{
	class Battle2json
	{
		private static List<string> battleResultFileRead = new List<string>(); // List of dat-files read from wargaming battle folder, to avoid read several times
		private static List<string> battleResultFileExists = new List<string>(); // List of json-files already existing in battle folder, to avoid read several times
		public static FileSystemWatcher battleResultFileWatcher = new FileSystemWatcher();
		public static bool battleResultReadRunning = false;
		public static bool battleResultReadRunOnceMore = false;
		public static bool battleResultWaiting = false;

		private class BattleValues
		{
			public string colname;
			public int value;
		}

		public static void StartBattleResultFileWatcher()
		{
			battleResultFileWatcher.Path = Path.GetDirectoryName(Config.Settings.battleFilePath);
			battleResultFileWatcher.Filter = "*.dat";
			battleResultFileWatcher.IncludeSubdirectories = true;
			battleResultFileWatcher.NotifyFilter = NotifyFilters.LastWrite;
			battleResultFileWatcher.Changed += new FileSystemEventHandler(BattleResultFileChanged);
			battleResultFileWatcher.EnableRaisingEvents = true;
		}

		private static void BattleResultFileChanged(object source, FileSystemEventArgs e)
		{
			if (!battleResultReadRunning)
			{
				GetAndConvertBattleFiles();
			}
			else
			{
				battleResultReadRunOnceMore = true;
			}				
		}

		public static void GetExistingBattleFiles()
		{
			// Get existing json files
			string[] filesJson = Directory.GetFiles(Config.AppDataBattleResultFolder, "*.json");
			foreach (string file in filesJson)
			{
				battleResultFileExists.Add(Path.GetFileNameWithoutExtension(file).ToString()); // Remove file extension
			}
		}

		public static void GetAndConvertBattleFiles()
		{
			if (!battleResultReadRunning)
			{
				battleResultReadRunning = true;
				List<string> battleResultNewFiles = new List<string>(); // List containing new files
				// Get WoT top level battle_result folder for getting dat-files
				DirectoryInfo di = new DirectoryInfo(Config.Settings.battleFilePath);
				DirectoryInfo[] folders = di.GetDirectories();
				// testing one file
				foreach (DirectoryInfo folder in folders)
				{
					string[] filesDat = Directory.GetFiles(folder.FullName, "*.dat");
					foreach (string file in filesDat)
					{
						string filenameWihoutExt = Path.GetFileNameWithoutExtension(file).ToString();
						if (!battleResultFileRead.Exists(x => x == file) && !battleResultFileExists.Exists(x => x == filenameWihoutExt))
						{
							// New file found, copy it and remember to avoid copy twice
							battleResultFileRead.Add(file);
							// Copy
							FileInfo fileBattleOriginal = new FileInfo(file); // the original dossier file
							string filename = Path.GetFileName(file);
							fileBattleOriginal.CopyTo(Config.AppDataBattleResultFolder + filename, true); // copy original dossier fil and rename it for analyze
							battleResultNewFiles.Add(Config.AppDataBattleResultFolder + filename);
						}
					}
				}

				// Loop through new files
				foreach (string file in battleResultNewFiles)
				{
					// Convert file to json
					if (ConvertBattleUsingPython(file))
					{
						// Success, clean up delete dat file
						FileInfo fileBattleOriginal = new FileInfo(file); // the original dossier file
						string filename = Path.GetFileName(file);
						fileBattleOriginal.Delete(); // delete original DAT file
					}
				}
				battleResultReadRunning = false;
				// check if run once more, if new file is found
				if (battleResultReadRunOnceMore)
				{
					battleResultReadRunOnceMore = false;
					GetAndConvertBattleFiles();
				}
				// Update into database now, if not dossier file is running
				// TODO: CheckBattleResultNewFiles();
			}
		}

		public static void CheckBattleResultNewFiles()
		{
			bool refreshAfterUpdate = false;
			battleResultWaiting = false;
			// Look for new files
			GetAndConvertBattleFiles();
			// Get all json files
			string[] filesJson = Directory.GetFiles(Config.AppDataBattleResultFolder, "*.json");
			foreach (string file in filesJson)
			{
				// Read content
				StreamReader sr = new StreamReader(file, Encoding.UTF8);
				string json = sr.ReadToEnd();
				sr.Close();
				// Root token
				JToken token_root = JObject.Parse(json);
				// Common token
				JToken token_common = token_root["common"];
				string result = (string)token_common.SelectToken("result"); // Find unique id
				// Check if ok
				if (result == "ok")
				{
					double arenaUniqueID = (double)token_root.SelectToken("arenaUniqueID"); // Find unique id
					double arenaCreateTime = (double)token_common.SelectToken("arenaCreateTime"); // Arena create time
					double duration = (double)token_common.SelectToken("duration"); // Arena duration
					double battlefinishUnix = arenaCreateTime + duration; // Battle finish time
					DateTime battleTime = DateTimeHelper.AdjustForTimeZone(DateTimeHelper.ConvertFromUnixTimestamp(battlefinishUnix)).AddSeconds(45);
					// Personal token
					JToken token_personel = token_root["personal"];
					int tankId = (int)token_personel.SelectToken("typeCompDescr"); // tankId
					// Now find battle
					string sql =
						"select b.id as battleId " +
						"from battle b left join playerTank pt on b.playerTankId = pt.id " +
						"where pt.tankId=@tankId and b.battleTime>@battleTimeFrom and b.battleTime<@battleTimeTo and b.battlesCount=1;";
					DB.AddWithValue(ref sql, "@tankId", tankId, DB.SqlDataType.Int);
					DB.AddWithValue(ref sql, "@battleTimeFrom", battleTime.AddSeconds(-30).ToString("yyyy-MM-dd HH:mm:ss"), DB.SqlDataType.DateTime);
					DB.AddWithValue(ref sql, "@battleTimeTo", battleTime.AddSeconds(30).ToString("yyyy-MM-dd HH:mm:ss"), DB.SqlDataType.DateTime);
					DataTable dt = DB.FetchData(sql);
					if (dt.Rows.Count > 0)
					{
						// Battle found, get battleId
						int battleId = Convert.ToInt32(dt.Rows[0]["battleId"]);
						// Get values
						List<BattleValues> battleValues = new List<BattleValues>();
						battleValues.Add(new BattleValues() { colname = "credits", value = (int)token_personel.SelectToken("credits") });

						// insert data
						string fields = "";
						foreach (var battleValue in battleValues)
						{
							fields += battleValue.colname + " = " + battleValue.value.ToString() + ", ";
						}
						sql = "update battle set " + fields + " arenaUniqueID=@arenaUniqueID where id=@battleId";
						DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
						DB.AddWithValue(ref sql, "@arenaUniqueID", arenaUniqueID, DB.SqlDataType.Float);
						DB.ExecuteNonQuery(sql);
						refreshAfterUpdate = true;
						//Debug.WriteLine("OK " + battleTime + " - " + tankId);
					}
					else
					{
						// Battle do not exists, Delete if old file
						// Debug.WriteLine(" - " + battleTime + " - " + tankId);
						if (battleTime < DateTime.Now.AddDays(-3))
						{
							FileInfo fileBattleOriginal = new FileInfo(file); // the original dossier file
							string filename = Path.GetFileName(file);
							fileBattleOriginal.Delete(); // delete original DAT file
						}
						else
						{
							// new file not read, waiting for battle
							battleResultWaiting = true;
						}
					}
					// Done - should delete file here - include later after testing
				}
				else
				{
					// Error in json file, delete it
					FileInfo fileBattleOriginal = new FileInfo(file); // the original dossier file
					string filename = Path.GetFileName(file);
					fileBattleOriginal.Delete(); // delete original DAT file
				}
				// Create alert file if new battle result added 
				if (refreshAfterUpdate)
				{
					Log.BattleResultDoneLog();
				}
			}
		}

		private static bool ConvertBattleUsingPython(string filename)
		{
			// Locate Python script
			string appPath = Path.GetDirectoryName(Application.ExecutablePath); // path to app dir
			string battle2jsonScript = appPath + "/dossier2json/wotbr2j.py"; // python-script for converting dossier file
			// Use IronPython
			try
			{
				//var ipy = Python.CreateRuntime();
				//dynamic ipyrun = ipy.UseFile(dossier2jsonScript);
				//ipyrun.main();
				if (!PythonEngine.InUse)
				{
					PythonEngine.InUse = true;
					var argv = new List();
					argv.Add(battle2jsonScript); // Have to add filename to run as first arg
					argv.Add(filename);
					argv.Add("-f");
					PythonEngine.Engine.GetSysModule().SetVariable("argv", argv);
					Microsoft.Scripting.Hosting.ScriptScope scope = PythonEngine.Engine.ExecuteFile(battle2jsonScript); // this is your python program
					dynamic result = scope.GetVariable("main")();

					//ScriptRuntimeSetup setup = new ScriptRuntimeSetup();
					//setup.DebugMode = true;
					//setup.LanguageSetups.Add(Python.CreateLanguageSetup(null));
					//ScriptRuntime runtime = new ScriptRuntime(setup);
					//ScriptEngine engine = runtime.GetEngineByTypeName(typeof(PythonContext).AssemblyQualifiedName);
					//ScriptSource script = engine.CreateScriptSourceFromFile(battle2jsonScript);
					//CompiledCode code = script.Compile();
					//ScriptScope scope = engine.CreateScope();
					//script.Execute(scope);

					PythonEngine.InUse = false;
				}
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				Code.MsgBox.Show("Error running Python script converting battle file: " + ex.Message + Environment.NewLine + Environment.NewLine +
				"Inner Exception: " + ex.InnerException, "Error converting battle file to json");
				PythonEngine.InUse = false;
				return false;
			}
			return true;
		}
	}
}
