using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using WinApp.Code;

namespace WinApp.Services
{
    public class AppStartupModel
    {
        public AppStartupModel()
        {
            Success = false;
        }
        public bool Success { get; set; }                       // Overall result
        public string Message { get; set; }                     // Message for connection result and more
        public int PlayerId { get; set; }                       // Player Id for later authentication for posting data
        public Guid PlayerToken { get; set; }                   // Player token for later authentication for posting data
        public string DownloadUrl { get; set; }                 // URL for goto download page
        public AppStartupDownloadVersionModel ReleaseVersion { get; set; }
        public AppStartupDownloadVersionModel PilotVersion { get; set; }
    }

    public class AppStartupDownloadVersionModel
    {
        public string AppVersion { get; set; }                  // Latest version available for user in format X.X.X
        public DateTime? MessageDate { get; set; }               // New message posted at date to be validated in client for showing automatically
        public string Message { get; set; }                     // Message result to be displayed if not success or if message date > last message displayed
        public bool MaintenanceMode { get; set; }               // Downloading not available, download in maintenance mode
        public DateTime? RunWotApi { get; set; }                // Run action
        public DateTime? RunForceDossierFileCheck { get; set; } // Run action
    }

    public class Request
    {
        public string Token { get; set; }
    }

    public class AppStartup
	{
		public async Task<AppStartupModel> Run(bool refreshPlayerApiToken)
		{
            // Body data to send
            string token = Config.Settings.playerName + '&' + Config.Settings.playerServer + '&' + AppVersion.AssemblyVersion;
            token = UtilSecurity.Encrypt(token);
            //token = HttpUtility.UrlEncode(token);
            Request request = new Request()
            {
                Token = token
            };
            // Call Wot Numbers Web service
            try
			{
                // Log app start and request data form wot num website
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsJsonAsync($"{Constants.WotNumWebUrl()}/Api/AppStartup", request);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                AppStartupModel appStartupModel = JsonConvert.DeserializeObject<AppStartupModel>(result);
                // Check result and store player id + token
                if (refreshPlayerApiToken && appStartupModel.Success)
                {
                    string sql = "UPDATE player SET playerApiId=@playerId, playerApiToken=@playerToken WHERE playerName=@playerName AND playerServer=@playerServer;";
                    DB.AddWithValue(ref sql, "@playerId", appStartupModel.PlayerId, DB.SqlDataType.Int);
                    DB.AddWithValue(ref sql, "@playerToken", appStartupModel.PlayerToken, DB.SqlDataType.VarChar);
                    DB.AddWithValue(ref sql, "@playerName", Config.Settings.playerName, DB.SqlDataType.VarChar);
                    DB.AddWithValue(ref sql, "@playerServer", Config.Settings.playerServer, DB.SqlDataType.VarChar);
                    DB.ExecuteNonQuery(sql);
                }
                return appStartupModel;
			}
			catch (Exception ex)
			{
                return new AppStartupModel()
                {
                    Success = false,
                    Message = ex.Message
                };
			}
		}
        
	}


}
