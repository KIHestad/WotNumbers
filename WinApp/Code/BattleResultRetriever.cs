using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApp.Code
{
	class BattleResultRetriever
	{
        private static string ClientGuiFolder
        {
            get
            {
                return Path.Combine(WoThelper.GetFullResModsPath(), "scripts", "client" , "gui");
            }
            set { }
        }
        
        private static string ModsFolder
        {
            get { return Path.Combine(ClientGuiFolder, "mods"); }
            set { }
        }
        
        private static string BrrFile
		{
			get {return "mod_BRR.pyc";}
			set {}
		}

        //public static string InitFile
        //{
        //    get {return modsFolder + "__init__.pyc";}
        //    set { }
        //}

        //public static string CameraNodeFile
        //{
        //    get { return clientFolder + "CameraNode.pyc"; }
        //    set { }
        //}

		

		

		public async static Task<bool> IsWoTGameFolderOK()
		{
			bool woTGameFolderOK = false;
			if (Config.Settings.wotGameFolder == "")
			{
				if (Directory.Exists("C:\\Games\\World_of_Tanks"))
				{
					Config.Settings.wotGameFolder = "C:\\Games\\World_of_Tanks";
                    await Config.SaveConfig();
                    await WoThelper.CheckForNewResModsFolder();
                    woTGameFolderOK = true;
				}
				else if (Directory.Exists("D:\\Games\\World_of_Tanks"))
				{
					Config.Settings.wotGameFolder = "D:\\Games\\World_of_Tanks";
                    await Config.SaveConfig();
                    await WoThelper.CheckForNewResModsFolder();
                    woTGameFolderOK = true;
				}
                else if (Directory.Exists("E:\\Games\\World_of_Tanks"))
                {
                    Config.Settings.wotGameFolder = "E:\\Games\\World_of_Tanks";
                    await Config.SaveConfig();
                    await WoThelper.CheckForNewResModsFolder();
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
				if (File.Exists(Path.Combine(ModsFolder, BrrFile)))
					files++;
                //if (File.Exists(BattleResultRetriever.InitFile))
                //    files++;
                //if (File.Exists(BattleResultRetriever.CameraNodeFile))
                //    files++;
                //return true; // Temp deactivated for WoT 9.15 - not working
				return (files == 1);
			}
			set {}
		}


		public static bool Install(out string msg)
		{
            //msg = "BRR mod is currently not working for WoT 9.15";
            //return false;
            
            
            bool ok = true;
			msg = "";
			try
			{
				if (!Directory.Exists(ModsFolder))
				{
					Directory.CreateDirectory(ModsFolder);
				}
				string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
				string fileToCopy = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Docs", BrrFile);
                string fileCopyToLocation = Path.Combine(ModsFolder, BrrFile);
                File.Copy(fileToCopy, fileCopyToLocation, true);
				// Add the access control entry to the files
                AddFileSecurity(fileCopyToLocation, userName, FileSystemRights.FullControl, AccessControlType.Allow);
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
                File.Delete(Path.Combine(ModsFolder, BrrFile));
			}
			catch (Exception ex)
			{
				msg = ex.Message;
				ok = false;
			}
			return ok;
		}

		// Adds an ACL entry on the specified file for the specified account. 
		public static void AddFileSecurity(string fileName, string account, FileSystemRights rights, AccessControlType controlType)
		{
			try
			{
				// Get a FileSecurity object that represents the 
				// current security settings.
				FileSecurity fSecurity = File.GetAccessControl(fileName);

				// Add the FileSystemAccessRule to the security settings.
				fSecurity.AddAccessRule(new FileSystemAccessRule(account, rights, controlType));

				// Set the new access settings.
				File.SetAccessControl(fileName, fSecurity);
			}
			catch (Exception)
			{
				
			}
			
		}

	}
}
