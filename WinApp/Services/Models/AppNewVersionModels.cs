using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Services.Models
{
    public class AppNewVersionDownloadSettings
    {
        public bool Active { get; set; }
        public string Version { get; set; }
        public string Date { get; set; }
        public string InactiveMessage { get; set; }
    }

    public class AppNewVersionApiResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public AppNewVersionDownloadSettings DownloadSettings { get; set; }
    }
}
