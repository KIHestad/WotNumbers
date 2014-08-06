using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WinApp.Code
{
	[Serializable()]
	public class VersionInfo
	{
		public bool      maintenanceMode { get; set; }				// maintenance mode - currently not operative
		public bool      error { get; set; }				    // true if fetch = OK
		public string    errorMsg { get; set; }				    // error message
		public string    version { get; set; }					// version in format X.X.X
		public string    downloadURL { get; set; }					// Full URL path for download file
		public string    downloadFile { get; set; }		        // File name for downloaded file to be created
		public string    message { get; set; }						// message to display
		public DateTime  messageDate	{ get; set; }			// message published date
		public DateTime  runWotApi { get; set; }				// force run wot api
		public DateTime  runForceDossierFileCheck { get; set; }	// force run full force dossier file check
	}
	
	public class CheckForNewVersion
	{
		public static VersionInfo versionInfo = new VersionInfo();
		
		public static void GetVersion(object sender, DoWorkEventArgs e)
		{
			// Get json data from Wot Numbers Web server API
			string result = "";
			string url = "http://wotnumbers.com/Api/CheckForNewVersion.aspx?player=@player&server=@server&version=@version";
			url = url.Replace("@player", Config.Settings.playerName);
			url = url.Replace("@server", Config.Settings.playerServer);
			url = url.Replace("@version", AppVersion.AssemblyVersion);
			Application.DoEvents(); // TODO: testing freeze-problem running API requests
			try
			{
				HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
				httpRequest.Timeout = 10000;     // milliseconds for wait for timeout
				httpRequest.UserAgent = "Wot Numbers " + AppVersion.AssemblyVersion;
				httpRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
				HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
				Application.DoEvents(); // TODO: testing freeze-problem running API requests
				StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());
				Application.DoEvents(); // TODO: testing freeze-problem running API requests
				result = responseStream.ReadToEnd();
				responseStream.Close();
				webResponse.Close();
				// Check data
				if (result != "")
				{
					versionInfo = JsonConvert.DeserializeObject<VersionInfo>(result);
					//int newVersion = MakeVersionToInt(versionInfo.version);
				}
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				versionInfo.error = true;
				versionInfo.errorMsg = ex.Message;
			}
		}

		public static double MakeVersionToDouble(string version)
		{
			double intVersion = 0;
			if (version != null)
			{
				const char separator = '.';
				string[] versionParts = version.Split(separator);
				double exp = Math.Pow(1000, versionParts.Length - 1);
				foreach (string versionPart in versionParts)
				{
					intVersion += Convert.ToInt32(versionPart) * exp;
					exp = exp / 1000;
				}
			}
			return intVersion;
		}
	}


}
