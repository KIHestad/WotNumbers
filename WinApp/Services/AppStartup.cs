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
		public async Task<AppStartupModel> Run()
		{
            // Body data to send
            string token = Code.Config.Settings.playerName + '&' + Code.Config.Settings.playerServer + '&' + Code.AppVersion.AssemblyVersion;
            token = Code.UtilSecurity.Encrypt(token);
            //token = HttpUtility.UrlEncode(token);
            Request request = new Request()
            {
                Token = token
            };
            // Call Wot Numbers Web service
            try
			{
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsJsonAsync("http://wotnumbers2.com/Api/AppStartup", request);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                AppStartupModel appStartupModel = JsonConvert.DeserializeObject<AppStartupModel>(result);
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
