using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IronPython.Hosting;

namespace WinApp.Code
{
	class Battle2json
	{
		private static List<string> battleResultFileRead = new List<string>(); // List for files read from wargaming battle folder, to avoid read several times
		
		public static void CheckBattleResultNewFiles()
		{
			List<string> battleResultNewFiles = new List<string>(); // List containing new files
			// Get current battle_result subfolder for getting dat-files
			DirectoryInfo di = new DirectoryInfo(Config.Settings.battleFilePath);
			DirectoryInfo[] folders = di.GetDirectories();
			// Loop through all battle result forlders
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
					// Success, read content find battle
				}
			}
		}

		private static bool ConvertBattleUsingPython(string filename)
		{
			// Change filename to fixed name
			if (!File.Exists(Path.GetDirectoryName(filename) + "/battle.dat"))
				File.Move(filename, Path.GetDirectoryName(filename) + "/battle.dat");
			// Locate Python script
			string appPath = Path.GetDirectoryName(Application.ExecutablePath); // path to app dir
			string battle2jsonScript = appPath + "/dossier2json/wotbr2j.py"; // python-script for converting dossier file
			// Use IronPython
			try
			{
				//var ipy = Python.CreateRuntime();
				//dynamic ipyrun = ipy.UseFile(dossier2jsonScript);
				//ipyrun.main();

				Microsoft.Scripting.Hosting.ScriptEngine py = Python.CreateEngine(); // allow us to run ironpython programs
				Microsoft.Scripting.Hosting.ScriptScope scope = py.ExecuteFile(battle2jsonScript); // this is your python program
				dynamic result = scope.GetVariable("main")();

			}
			catch (Exception ex)
			{
				Code.MsgBox.Show("Error running Python script converting battle file: " + ex.Message + Environment.NewLine + Environment.NewLine +
				"Inner Exception: " + ex.InnerException, "Error converting battle file to json");
				return false;
			}
			// Rename files back to "original" names
			File.Move(Path.GetDirectoryName(filename) + "/battle.dat", filename);
			File.Move(Path.GetDirectoryName(filename) + "/battle.json", Path.GetDirectoryName(filename) + "/" + Path.GetFileNameWithoutExtension(filename) + ".json");
			return true;
		}
	}
}
