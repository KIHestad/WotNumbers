using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
    public class DossierHelper
    {
        public class DossierFileInfo
        {
            public string ServerUrl { get; set; }
            public string ServerRealmName { get; set; }
            public string PlayerWargamingId { get; set; }
            public string PlayerName { get; set; }
            public string AccountType { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        // Gets the names of the player from name of dossier file
        public static DossierFileInfo GetDossierFileInfo(string dossierFileName)
        {
            DossierFileInfo dfi = new DossierFileInfo
            {
                Success = true
            };
            try
            {
                // First decode filename
                FileInfo fi = new FileInfo(dossierFileName);
                string str = fi.Name.Replace(fi.Extension, string.Empty);
                byte[] decodedFileNameBytes = Base32.Base32Encoder.Decode(str.ToLowerInvariant());
                string decodedFileName = Encoding.UTF8.GetString(decodedFileNameBytes);

                // Returns:
                // EU login2.p2.worldoftanks.eu:20016;BadButton;PlayerAccount
                // ASIA wotasia1-2.login.wargaming.net:20016;leafcap;PlayerAccount
                // NA wotna3.login.wargaming.net:20016; kihestad; PlayerAccount
                // RU login2.p2.tanki.su:20014;LoGGaN;PlayerAccount
                // CT login-ct.worldoftanks.net:20015;BadButton_EU;PlayerAccount

                // Split decoded file name
                string[] splitDecodedFilename = decodedFileName.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                if (splitDecodedFilename.Length < 3)
                {
                    dfi.Success = false;
                    dfi.Message = "Error decoding dossier file name: " + decodedFileName;
                    Log.AddToLogBuffer(dfi.Message, true);
                    return dfi;
                }
                dfi.ServerUrl = splitDecodedFilename[0];
                dfi.PlayerName = splitDecodedFilename[1];
                dfi.AccountType = splitDecodedFilename[2];
                // Find server realm from server url
                string[] splitServerUrl = dfi.ServerUrl.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                if (splitServerUrl.Length < 3)
                {
                    dfi.Success = false;
                    dfi.Message = $"Error decoding server realm name from dossier file name: " + decodedFileName;
                    Log.AddToLogBuffer(dfi.Message, true);
                    return dfi;
                }
                string url1 = splitServerUrl[0].ToLower();
                string url2 = splitServerUrl[1].ToLower();
                string url3 = splitServerUrl[2].ToLower();
                string url4 = "";
                if (splitServerUrl.Length > 3)
                    url4 = splitServerUrl[3].ToLower();
                if (url4.StartsWith("eu"))
                    dfi.ServerRealmName = "EU";
                else if (url1.StartsWith("wotna"))
                    dfi.ServerRealmName = "NA";
                else if (url1.StartsWith("wotasia"))
                    dfi.ServerRealmName = "ASIA";
                else if (url1.StartsWith("login") && url4.StartsWith("su"))
                    dfi.ServerRealmName = "RU";
                else if (url1.Contains("-ct"))
                    dfi.ServerRealmName = "CT";
                else
                {
                    dfi.Success = false;
                    dfi.Message = $"Error getting server realm name from dossier file name: " + decodedFileName;
                    Log.AddToLogBuffer(dfi.Message, true);
                    return dfi;
                }
            }
            catch (Exception ex)
            {
                Log.AddToLogBuffer("Error getting player name from dossier file name.", true);
                dfi.Success = false;
                dfi.Message = "Error decoding dossier file name. " + ex.Message;
            }
            return dfi;
        }
        
    }
}
