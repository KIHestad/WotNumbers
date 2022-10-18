using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WinApp.Code;

namespace WinApp.Services
{
    public class AppNewVersion
    {
        public static Models.AppNewVersionApiResult Check()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString($"{Constants.WotNumWebUrl()}/VersionSettings.json");
            try
            {
                return new Models.AppNewVersionApiResult
                {
                    Success = true,
                    DownloadSettings = JsonConvert.DeserializeObject<Models.AppNewVersionDownloadSettings>(json)
                };
            }
            catch (Exception ex)
            {
                return new Models.AppNewVersionApiResult()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
