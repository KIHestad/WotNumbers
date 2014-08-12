using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;

namespace WinApp.Code
{
	class Battle2json
	{
		private static List<string> battleResultFileRead = new List<string>(); // List for files read from wargaming battle folder, to avoid read several times
		
		public static void CheckBattleResultNewFiles()
		{
			List<string> battleResultNewFiles = new List<string>(); // List containing new files
			// Get WoT top level battle_result folder for getting dat-files
			DirectoryInfo di = new DirectoryInfo(Config.Settings.battleFilePath);
			DirectoryInfo[] folders = di.GetDirectories();
			// testing one file
			foreach (DirectoryInfo folder in folders)
			{
				string[] files = Directory.GetFiles(folder.FullName, "*.dat");
				foreach (string file in files)
				{
					if (!battleResultFileRead.Exists(x => x == file))
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
		}

		private static void test()
		{
			string[] files = Directory.GetFiles(Config.AppDataBattleResultFolder, "*.dat");
			foreach (string file in files)
			{
				ConvertBattleUsingPython(file);	
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
