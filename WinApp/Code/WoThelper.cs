using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WinApp.Code
{
    class WoThelper
    {
        // Get WoT install folder with end backslash
        public static string GetFullInstallPath
        {
            get
            {
                string folder = Config.Settings.wotGameFolder;
                if (folder != "" && folder.Substring(folder.Length - 1, 1) != "\\")
                    folder += "\\";
                return folder;
            }
            set { }
        }

        // Returns full path to res_mods folder + current active subfolder, or spesified subfolder
        public static string GetFullResModsPath(string subFolder = "")
        {
            if (subFolder == "")
                return GetFullInstallPath += "res_mods\\" + Config.Settings.res_mods_subfolder + "\\";
            else
                return GetFullInstallPath += "res_mods\\" + subFolder + "\\";
        }
        public static string GetResModsPathSubfolder(string subFolder = "")
        {
            if (subFolder == "")
                return "\\res_mods\\" + Config.Settings.res_mods_subfolder + "\\";
            else
                return "\\res_mods\\" + subFolder + "\\";
        }


        private static string GetHighestResModsFolder()
        {
            try
            {
                string resModsFullPath = GetFullInstallPath + "res_mods\\";
                DirectoryInfo directory = new DirectoryInfo(resModsFullPath);
                List<DirectoryInfo> subfolders = directory.GetDirectories().ToList();
                string highestResModsFolder = "";
                Int64 highestResModsFolderToNumber = 0;
                foreach (DirectoryInfo subfolder in subfolders)
                {
                    if (subfolder.Name.Contains("."))
                    {
                        List<string> subFolderPart = subfolder.Name.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        // res mods subfolder = WoT game version, can be 5 part number
                        Int64 factor = (Int64)1000 * (Int64)1000 * (Int64)1000 * (Int64)1000 * (Int64)1000;
                        Int64 currentSubfolderToNumber = 0;
                        bool convertValid = true;
                        foreach (string s in subFolderPart)
                        {
                            Int64 num = 0;
                            if (!Int64.TryParse(s, out num))
                                convertValid = false;
                            currentSubfolderToNumber += num * factor;
                            factor = factor / 1000; // for each part number, reduce factor with 100
                        }
                        // Check if highest number
                        if (convertValid && currentSubfolderToNumber > highestResModsFolderToNumber)
                        {
                            highestResModsFolderToNumber = currentSubfolderToNumber;
                            highestResModsFolder = subfolder.Name;
                        }
                    }
                }
                return highestResModsFolder;
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex, "Error getting highest res_mods subfolder");
                return "";
            }
            
        }

        public static void CheckForNewResModsFolder()
        {
            string highestResModsFolder = GetHighestResModsFolder();
            if (highestResModsFolder != "" && highestResModsFolder != Config.Settings.res_mods_subfolder)
            {
                string msg = "";
                Config.Settings.res_mods_subfolder = highestResModsFolder;
                Config.SaveConfig(out msg);
            }
        }

    }
}
