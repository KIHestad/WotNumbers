using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace WinApp.Code
{
	class BattleResultRetriever
	{
		public static string BrrFile
		{
			get {return modsFolder + "BRR.pyc";}
			set {}
		}

		public static string InitFile
		{
			get {return modsFolder + "__init__.pyc";}
			set { }
		}

		public static string CameraNodeFile
		{
			get { return clientFolder + "CameraNode.pyc"; }
			set { }
		}

		public static string clientFolder
		{
			get
			{
				string wotFolder = Config.Settings.wotGameFolder;
				if (wotFolder != "" && wotFolder.Substring(wotFolder.Length - 1, 1) != "\\")
					wotFolder += "\\";
				return wotFolder += "res_mods\\0.9.7\\scripts\\client\\";
			}
			set { }
		}

		public static string modsFolder
		{
			get {return clientFolder += "mods\\";}
			set {}
		}

		public static bool IsWoTGameFolderOK()
		{
			string msg = "";
			bool woTGameFolderOK = false;
			if (Config.Settings.wotGameFolder == "")
			{
				if (Directory.Exists("C:\\Games\\World_of_Tanks"))
				{
					Config.Settings.wotGameFolder = "C:\\Games\\World_of_Tanks";
					Config.SaveConfig(out msg);
					woTGameFolderOK = true;
				}
				else if (Directory.Exists("D:\\Games\\World_of_Tanks"))
				{
					Config.Settings.wotGameFolder = "D:\\Games\\World_of_Tanks";
					Config.SaveConfig(out msg);
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
				if (File.Exists(BattleResultRetriever.BrrFile))
					files++;
				if (File.Exists(BattleResultRetriever.InitFile))
					files++;
				if (File.Exists(BattleResultRetriever.CameraNodeFile))
					files++;
				return (files == 3);
			}
			set {}
		}


		public static bool Install(out string msg)
		{
			bool ok = true;
			msg = "";
			try
			{
				if (!Directory.Exists(modsFolder))
				{
					Directory.CreateDirectory(modsFolder);
				}
				string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
				if (!File.Exists(BrrFile))
				{
					string fileToCopy = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\BRR.pyc";
					File.Copy(fileToCopy, BrrFile);
				}
				if (!File.Exists(InitFile))
				{
					string fileToCopy = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\__init__.pyc";
					File.Copy(fileToCopy, InitFile);
				}
				if (!File.Exists(CameraNodeFile))
				{
					string fileToCopy = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\CameraNode.pyc";
					File.Copy(fileToCopy, CameraNodeFile);
				}
				// Add the access control entry to the files
				AddFileSecurity(BrrFile, userName, FileSystemRights.FullControl, AccessControlType.Allow);
				AddFileSecurity(CameraNodeFile, userName, FileSystemRights.FullControl, AccessControlType.Allow);
				AddFileSecurity(InitFile, userName, FileSystemRights.FullControl, AccessControlType.Allow);
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
				File.Delete(BrrFile);
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
