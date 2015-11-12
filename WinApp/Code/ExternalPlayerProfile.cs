using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinApp.Code
{
    public class ExternalPlayerProfile
    {
        public static void Wargaming(string playerName, string playerAccountId)
        {
            try
            {
                string server = Config.Settings.playerServer.ToLower();
                if (server == "net")
                    server = "ru";
                string serverURL = string.Format("http://worldoftanks.{0}/community/accounts/{1}-{2}/", server, playerAccountId, playerName);
                System.Diagnostics.Process.Start(serverURL);
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex, "Error on showing player profile on Wargaming website.");
            }
        }

        public static void WotLabs(string playerName)
        {
            try
            {
                string server = Config.Settings.playerServer.ToLower();
                if (server == "net")
                    server = "ru"; 
                if (server == "asia")
                    server = "sea";
                string serverURL = string.Format("http://wotlabs.net/{0}/player/{1}", server, playerName);
                System.Diagnostics.Process.Start(serverURL);
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex, "Error on showing player profile on WotLabs website.");
            }
        }

        public static void vBAddict(string playerName)
        {
            try
            {
                string server = Config.Settings.playerServer.ToLower();
                if (server == "net")
                    server = "ru";
                string serverURL = string.Format("http://www.vbaddict.net/player/{0}-{1}", playerName.ToLower(), server);
                System.Diagnostics.Process.Start(serverURL);
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex, "Error on showing player profile on vBAddict website.");
            }
        }

        public static void Noobmeter(string playerName, string playerAccountId)
        {
            try
            {
                string server = Config.Settings.playerServer.ToLower();
                if (server == "net")
                    server = "ru";
                if (server == "asia")
                    server = "sea";
                string serverURL = string.Format("http://www.noobmeter.com/player/{0}/{1}/{2}/", server, playerName.ToLower(), playerAccountId);
                System.Diagnostics.Process.Start(serverURL);
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex, "Error on showing player profile on vBAddict website.");
            }
        }
    }
}
