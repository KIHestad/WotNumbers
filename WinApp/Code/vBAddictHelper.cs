using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

// API reference:
// http://www.vbaddict.net/threads/5743-vBAddict-API-Uploading-Dossier-Battle-Results-and-Replays-to-vBAddict-net


namespace WinApp.Code
{
	class vBAddictHelper
	{
		private static int timeout = 15000; // milliseconds

        public static SettingsItem Settings = new SettingsItem(); 

        public class SettingsItem
        {
            public string Token { get; set; }
            public bool UploadActive { get; set; }
            public bool UploadReplayActive { get; set; }
        }

        public async static Task GetSettings()
        {
            string sql = "SELECT * FROM player WHERE id=@id";
            DB.AddWithValue(ref sql, "@id", Config.Settings.playerId, DB.SqlDataType.Int);
            DataTable dt = await DB.FetchData(sql);
            string token = "";
            bool uploadActive = false;
            bool uploadReplayActive = false;
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                if (dr["vbaddictToken"] != DBNull.Value)
                    token = dr["vbaddictToken"].ToString();
                uploadActive = Convert.ToBoolean(dr["vbaddictUploadActive"]);
                uploadReplayActive = Convert.ToBoolean(dr["vbaddictUploadReplayActive"]);
            }
            Settings.Token = token;
            Settings.UploadActive = uploadActive;
            Settings.UploadReplayActive = uploadReplayActive;
        }

        public async static Task SaveSettings()
        {
            string sql =
                "UPDATE player " +
                "SET vbaddictToken=@vbaddictToken, vbaddictUploadActive=@vbaddictUploadActive, vbaddictUploadReplayActive=@vbaddictUploadReplayActive " +
                "WHERE id=@id; ";
            DB.AddWithValue(ref sql, "@id", Config.Settings.playerId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@vbaddictToken", Settings.Token, DB.SqlDataType.VarChar);
            DB.AddWithValue(ref sql, "@vbaddictUploadActive", Settings.UploadActive, DB.SqlDataType.Boolean);
            DB.AddWithValue(ref sql, "@vbaddictUploadReplayActive", Settings.UploadReplayActive, DB.SqlDataType.Boolean);
            await DB.ExecuteNonQuery(sql);
        }

		public async static Task<string> TestConnection()
		{
			try
			{
				string url = "http://carius.vbaddict.net:82/upload_check/xml/";
                HttpClient client = new HttpClient()
                {
                    Timeout = new TimeSpan(0, 0, 10) // 10 seconds
                };
                client.DefaultRequestHeaders.Add("User-Agent", "Wot Numbers " + AppVersion.AssemblyVersion);
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string xmlResult = await response.Content.ReadAsStringAsync();

                XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(xmlResult); // Load string into xml doc
				string result = XmlHelper.XmlToString(xmlDoc);
				
				XmlNode node = xmlDoc.GetElementsByTagName("status").Item(0);  // get status node from xml
				string responseCode = node.FirstChild.Value.ToString();
				node = xmlDoc.GetElementsByTagName("message").Item(0);   // get message node from xml
				string responseText = node.FirstChild.Value.ToString();
				
				//return result + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
				
				if (responseCode == "0")
					return "Connection test successfully completed!";
				else
					return "The connection could not be established. " + Environment.NewLine + Environment.NewLine
						 + responseText + Environment.NewLine 
						 + "Error code: " + responseCode + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
			}
			catch (Exception ex)
			{
				return "Error connecting to vBAddict. Error message:" + Environment.NewLine + Environment.NewLine + ex.Message;
			}
		}
		
		public static bool UploadDossier(string dossierFile, string playerName, string playerServer, string playerToken, out string msg, bool showResultAsXML = true)
		{
			msg = "Starting upload dossier file to vBAddict...";
			bool result = true;
			try
			{
				string url = "http://carius.vbaddict.net:82/upload_file/dossier/@SERVER/@USERNAME/@TOKEN/xml/";
				url = url.Replace("@USERNAME", playerName);
				url = url.Replace("@SERVER", playerServer);
				if (playerToken == "")
					playerToken = "-";
				url = url.Replace("@TOKEN", playerToken);
				HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
				httpRequest.Timeout = timeout; 
				httpRequest.UserAgent = "Wot Numbers " + AppVersion.AssemblyVersion;
				httpRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
				// Get ready to pu file with request
				httpRequest.Method = "PUT";
				httpRequest.KeepAlive = false;
				httpRequest.SendChunked = false;
				httpRequest.AllowWriteStreamBuffering = true;
				// Read file into stream
				Stream reqStream = httpRequest.GetRequestStream();
				string localFile = dossierFile;
				FileStream rdr = new FileStream(localFile, FileMode.Open, FileAccess.Read);
				byte[] inData = new byte[4096];
				int bytesRead = rdr.Read(inData, 0, inData.Length);
				while (bytesRead > 0)
				{
					reqStream.Write(inData, 0, bytesRead);
					bytesRead = rdr.Read(inData, 0, inData.Length);
				}
				reqStream.Close();
				rdr.Close();
				// Perform the web request
				HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
				// Get result
				StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());
				string xmlResult = responseStream.ReadToEnd(); // Read result into string
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(xmlResult); // Load string into xml doc
				msg = XmlHelper.XmlToString(xmlDoc) + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
				// Check result
				XmlNodeList response = xmlDoc.GetElementsByTagName("response");
				string status = "";
				foreach (XmlNode item in response[0].ChildNodes)  // get status code
				{
					if (item.Name == "status") status = item.InnerText;
				}
				result = (status == "0");
				string message = "";
				foreach (XmlNode item in response[0].ChildNodes)  // get response message 
				{
					if (item.Name == "message") message = item.InnerText;
				}
				if (status == "0")
					msg = "Upload dossier file successfully completed!";
				else
					if (!showResultAsXML)
					{
						msg = "Error during dossier file upload." + Environment.NewLine + Environment.NewLine
							+ message + Environment.NewLine
							+ "Error code: " + status + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
					}
			}
			catch (Exception ex)
			{
				msg = "Error uploading dossier file. Error message:" + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine;
				result = false;
			}
			return result;
		}

		public static bool UploadBattle(string battleFile, string playerName, string playerServer, string playerToken, out string msg)
		{
			msg = "Starting upload battle file to vBAddict...";
			bool result = true;
			try
			{
				string url = "http://carius.vbaddict.net:82/upload_file/battleresult/@SERVER/@USERNAME/@TOKEN/xml/";
				url = url.Replace("@USERNAME", playerName);
				url = url.Replace("@SERVER", playerServer);
				if (playerToken == "")
					playerToken = "-";
				playerToken = playerToken.Trim();
				url = url.Replace("@TOKEN", playerToken);
				HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
				httpRequest.Timeout = timeout; 
				httpRequest.UserAgent = "Wot Numbers " + AppVersion.AssemblyVersion;
				httpRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
				// Get ready to pu file with request
				httpRequest.Method = "PUT";
				httpRequest.KeepAlive = false;
				httpRequest.SendChunked = false;
				httpRequest.AllowWriteStreamBuffering = true;
				// Read file into stream
				Stream reqStream = httpRequest.GetRequestStream();
				string localFile = battleFile;
				FileStream rdr = new FileStream(localFile, FileMode.Open, FileAccess.Read);
				byte[] inData = new byte[4096];
				int bytesRead = rdr.Read(inData, 0, inData.Length);
				while (bytesRead > 0)
				{
					reqStream.Write(inData, 0, bytesRead);
					bytesRead = rdr.Read(inData, 0, inData.Length);
				}
				reqStream.Close();
				rdr.Close();
				// Perform the web request
				HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
				// Get result
				StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());
				string xmlResult = responseStream.ReadToEnd(); // Read result into string
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(xmlResult); // Load string into xml doc
				msg = XmlHelper.XmlToString(xmlDoc);
				// Check result
				XmlNodeList response = xmlDoc.GetElementsByTagName("response");
				string status = "";
				foreach (XmlNode item in response[0].ChildNodes)
				{
					if (item.Name == "status") status = item.InnerText;
				}
				result = (status == "0");
			}
			catch (Exception ex)
			{
				result = false;
				Log.LogToFile(ex, "Error uploading battle file to vBAddict.").ConfigureAwait(false);
			}
			return result;
		}

        public class UploadReplayresult
        {
            public UploadReplayresult()
            {
                Success = true;
                Message = "";
            }
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        public async static Task<UploadReplayresult> UploadReplay(int battleId, string replayFile, string playerName, string playerServer, string playerToken)
        {
            UploadReplayresult result = new UploadReplayresult();
            try
            {
                string url = "http://carius.vbaddict.net:82/upload_file/replay/@SERVER/@USERNAME/@TOKEN/xml/";
                url = url.Replace("@USERNAME", playerName);
                url = url.Replace("@SERVER", playerServer);
                if (playerToken == "")
                    playerToken = "-";
                playerToken = playerToken.Trim();
                url = url.Replace("@TOKEN", playerToken);
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Timeout = timeout;
                httpRequest.UserAgent = "Wot Numbers " + AppVersion.AssemblyVersion;
                httpRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
                // Get ready to pu file with request
                httpRequest.Method = "PUT";
                httpRequest.KeepAlive = false;
                httpRequest.SendChunked = false;
                httpRequest.AllowWriteStreamBuffering = true;
                // Read file into stream
                Stream reqStream = httpRequest.GetRequestStream();
                string localFile = replayFile;
                FileStream rdr = new FileStream(localFile, FileMode.Open, FileAccess.Read);
                byte[] inData = new byte[4096];
                int bytesRead = rdr.Read(inData, 0, inData.Length);
                while (bytesRead > 0)
                {
                    reqStream.Write(inData, 0, bytesRead);
                    bytesRead = rdr.Read(inData, 0, inData.Length);
                }
                reqStream.Close();
                rdr.Close();
                // Perform the web request
                HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
                // Get result
                StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());
                string xmlResult = responseStream.ReadToEnd(); // Read result into string
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlResult); // Load string into xml doc
                // Check result
                XmlNodeList response = xmlDoc.GetElementsByTagName("response");
                string status = "";
                foreach (XmlNode item in response[0].ChildNodes)
                {
                    if (item.Name == "status") status = item.InnerText;
                    if (item.Name == "message") result.Message = item.InnerText;
                }
                result.Success = (status == "0");
                if (result.Success)
                    await UpdateInfoUploadedvBAddict(battleId);
            }
            catch (Exception ex)
            {
                result.Success = false;
                await Log.LogToFile(ex, "Error uploading replay file to vBAddict.").ConfigureAwait(false);
                result.Message = ex.Message;
            }
            return result;
        }

        public class SearchForuserResult
        {
            public List<string> Users { get; set; }
            public string ErrorMessage { get; set; }
        }

        public async static Task<SearchForuserResult> SearchForUser(string AccountId)
        {
            List<string> AccountIds = new List<string>
            {
                AccountId
            };
            return await SearchForUser(AccountIds);
        }

        public async static Task<SearchForuserResult> SearchForUser(List<string> AccountIds)
        {
            // Default
            SearchForuserResult result = new SearchForuserResult()
            {
                Users = new List<string>(),
                ErrorMessage = ""
            };
            // Terminate if no data
            if (AccountIds.Count == 0)
            {
                return null;
            }
            // Get API URL
            string accountIDList = "";
            foreach (string accountId in AccountIds)
            {
                accountIDList += accountId + ",";
            }
            accountIDList = accountIDList.Substring(0, accountIDList.Length -1); // remove last comma
            string url = string.Format("http://carius.vbaddict.net:82/player_info/search_accountid/{0}/{1}/xml", ExternalPlayerProfile.GetServer, accountIDList);
            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Timeout = timeout;
                httpRequest.UserAgent = "Wot Numbers " + AppVersion.AssemblyVersion;
                httpRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
                WebResponse webResponse = await httpRequest.GetResponseAsync();
                StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());
                string xmlResult = responseStream.ReadToEnd(); // Read result into string
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlResult); // Load string into xml doc
                // Get status and message
                XmlNode node = xmlDoc.GetElementsByTagName("status").Item(0);  // get status node from xml
                string responseStatus = node.FirstChild.Value.ToString();
                node = xmlDoc.GetElementsByTagName("message").Item(0);   // get message node from xml
                string responseMessage = node.FirstChild.Value.ToString();
                // Check if valid
                if (responseStatus == "0" && responseMessage == "Success")
                {
                    // Read data
                    int counter = 0;
                    node = xmlDoc.GetElementsByTagName("item" + counter).Item(0);
                    while (node != null)
                    {
                        result.Users.Add(node.ChildNodes[0].InnerText);
                        counter++;
                        node = xmlDoc.GetElementsByTagName("item" + counter).Item(0);
                    }
                }
                else
                {
                    result.ErrorMessage = "Returned message: " + responseMessage + " [Status:" + responseStatus + "] when searching for users at vBAddict";
                    await Log.LogToFile(result.ErrorMessage);
                }
                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Error running vBAddict API: " + url;
                await Log.LogToFile(ex, result.ErrorMessage);
                return result;
            }
        }

        public async static Task<string> GetInfoUploadedvBAddict(int battleId)
        {
            string sql = "select uploadedvBAddict from battle where id=@id";
            DB.AddWithValue(ref sql, "@id", battleId, DB.SqlDataType.Int);
            DataTable dt = await DB.FetchData(sql);
            string result = "";
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                if (dr[0] != DBNull.Value)
                {
                    DateTime uploadTime = Convert.ToDateTime(dr[0]);
                    result = "Uploaded: " + uploadTime.ToShortDateString() + " " + uploadTime.ToShortTimeString();
                }
            }
            return result;
        }

        public async static Task<string> UpdateInfoUploadedvBAddict(int battleId)
        {
            string sql = "update battle set uploadedvBAddict=@uploadedvBAddict where id=@id";
            DB.AddWithValue(ref sql, "@id", battleId, DB.SqlDataType.Int);
            DateTime uploadTime = DateTime.Now;
            DB.AddWithValue(ref sql, "@uploadedvBAddict", uploadTime, DB.SqlDataType.DateTime);
            await DB.ExecuteNonQuery(sql);
            return await GetInfoUploadedvBAddict(battleId);
        }

        public async static Task<string> GetReplayURLInfo(int battleId)
        {
            // return: map - nation - tankname - battleid
            string replayURLInfo = "";
            // Get Battle info
            string sql =
                "select map.name as mapName, country.vBAddictName as nationName, tank.name as tankName, battle.arenauniqueid " +
                "from battle " +
                "  inner join playerTank pt on battle.playerTankId = pt.id " + 
                "  inner join tank on pt.tankId = tank.Id " + 
                "  left join map on battle.mapId = map.id  " +
                "  left join country on tank.countryId = country.id " +
                "where battle.id=" + battleId.ToString();
            DataTable dtBattle = await DB.FetchData(sql);
            if (dtBattle.Rows.Count > 0)
            {
                DataRow drBattle = dtBattle.Rows[0];
                string mapName = drBattle["mapName"].ToString().ToLower();
                string nation = drBattle["nationName"].ToString().ToLower();
                string tankName = drBattle["tankName"].ToString().ToLower();
                mapName = Regex.Replace(mapName, "[^a-z0-9]+", "_");
                nation = Regex.Replace(nation, "[^a-z0-9]+", "_");
                tankName = Regex.Replace(tankName, "[^a-z0-9]+", "_");
                string arenauniqueid = drBattle["arenauniqueid"].ToString().ToLower();
                replayURLInfo = 
                    mapName + "-" +
                    nation + "-" +
                    tankName + "-" +
                    arenauniqueid;
            }
            return replayURLInfo;
        }
	}
}
