using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using WinApp.Code;
using System.Text;

namespace WinApp.Services
{
    public class AppStartup
	{
		public async Task<Models.AppStartupModels.Result> Run(bool refreshPlayerApiToken)
		{
            // Body data to send
            string token = Config.Settings.playerName + '&' + Config.Settings.playerServer + '&' + AppVersion.AssemblyVersion;
            token = UtilSecurity.Encrypt(token);
            //token = HttpUtility.UrlEncode(token);
            Models.AppStartupModels.Request request = new Models.AppStartupModels.Request()
            {
                Token = token
            };
            // Call Wot Numbers Web service
            try
			{
                // Log app start and request data form wot num website
                HttpClient client = new HttpClient()
                {
                    Timeout = new TimeSpan(0, 0, 10), // 10 seconds
                };
                StringContent httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{Constants.WotNumWebUrl()}/Api/AppStartup", httpContent);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                Models.AppStartupModels.Result result = JsonConvert.DeserializeObject<Models.AppStartupModels.Result>(json);
                // Check result and store player id + token
                if (refreshPlayerApiToken && result.Success)
                {
                    string sql = "UPDATE player SET playerApiId=@playerId, playerApiToken=@playerToken WHERE playerName=@playerName AND playerServer=@playerServer;";
                    DB.AddWithValue(ref sql, "@playerId", result.PlayerId, DB.SqlDataType.Int);
                    DB.AddWithValue(ref sql, "@playerToken", result.PlayerToken, DB.SqlDataType.VarChar);
                    DB.AddWithValue(ref sql, "@playerName", Config.Settings.playerName, DB.SqlDataType.VarChar);
                    DB.AddWithValue(ref sql, "@playerServer", Config.Settings.playerServer, DB.SqlDataType.VarChar);
                    await DB.ExecuteNonQuery(sql);
                }
                return result;
			}
			catch (Exception ex)
			{
                return new Models.AppStartupModels.Result()
                {
                    Success = false,
                    Message = ex.Message
                };
			}
		}
        
	}


}
