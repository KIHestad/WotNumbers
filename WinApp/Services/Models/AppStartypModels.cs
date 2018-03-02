using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Services.Models
{
    public class AppStartupModels
    {
        public class Request
        {
            public string Token { get; set; }
        }

        public class Result
        {
            public Result()
            {
                Success = false;
            }
            public bool Success { get; set; }                       // Overall result
            public string Message { get; set; }                     // Message for connection result and more
            public int PlayerId { get; set; }                       // Player Id for later authentication for posting data
            public Guid PlayerToken { get; set; }                   // Player token for later authentication for posting data
            public string DownloadUrl { get; set; }                 // URL for goto download page
            public DownloadVersion ReleaseVersion { get; set; }
            public DownloadVersion PilotVersion { get; set; }
        }

        public class DownloadVersion
        {
            public string AppVersion { get; set; }                  // Latest version available for user in format X.X.X
            public DateTime? MessageDate { get; set; }               // New message posted at date to be validated in client for showing automatically
            public string Message { get; set; }                     // Message result to be displayed if not success or if message date > last message displayed
            public bool MaintenanceMode { get; set; }               // Downloading not available, download in maintenance mode
            public DateTime? RunWotApi { get; set; }                // Run action
            public DateTime? RunForceDossierFileCheck { get; set; } // Run action
        }
    }

    
}
