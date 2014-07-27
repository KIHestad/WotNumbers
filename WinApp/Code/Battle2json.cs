using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WinApp.Code
{
	class Battle2json
	{
		private static List<string> battleFileRead = new List<string>(); // List for files read from wargaming battle folder
		
		//public static List<string> CopyBattleFiles()
		//{
		//	// Get current battle_result subfolder for getting dat-files
		//	DirectoryInfo di = new DirectoryInfo(Config.Settings.battleFilePath);
		//	DirectoryInfo[] folders = di.GetDirectories();
		//	// Loop through all battle result forlders
		//	foreach (DirectoryInfo folder in folders)
		//	{
		//		string[] files = Directory.GetFiles(folder.FullName, "*.dat");
		//		foreach (string file in files)
		//		{
		//			FileInfo checkFile = new FileInfo(file);
		//			//if (checkFile.LastWriteTime > dossierFileDate)
		//			//{
		//			//	dossierFile = checkFile.FullName;
		//			//	dossierFileDate = checkFile.LastWriteTime;
		//			//}
		//		}

		//	}
				
		//	return foldername;
		//}

		public static void CopyNewBattleFiles()
		{
			// Copy battle files for processing
						
		}
	}
}
