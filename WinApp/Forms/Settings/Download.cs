using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class Download : FormCloseOnEsc
    {
		private Services.AppStartupModel _appStartupModel; 
        
        public Download(Services.AppStartupModel appStartupModel)
		{
			InitializeComponent();
            _appStartupModel = appStartupModel;
		}

		private void Download_Load(object sender, EventArgs e)
		{
            lblCurrVer.Text = AppVersion.AssemblyVersion;
            lblNewVer.Text = _appStartupModel.LatestAppVersion;
        }
        

        private void btnGoToDownloadPage_Click(object sender, EventArgs e)
        {
            Process.Start(_appStartupModel.DownloadUrl);
            this.Close();
            Application.Exit();
        }
    }
}
