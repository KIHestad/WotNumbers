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
	public partial class Download : Form
	{
		public Download()
		{
			InitializeComponent();
		}

		private void Download_Load(object sender, EventArgs e)
		{
			DownloadNow();
		}

		private static WebClient webClient = null;
		private void DownloadNow()
		{
			// Start download now
			VersionInfo vi = CheckForNewVersion.versionInfo;
			IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
			defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;
			webClient = new WebClient();
			webClient.Proxy = defaultWebProxy;
			webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadNewVersionDone);
			webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadNewVersionInProgress);
			string filename = Config.AppDataDownloadFolder + vi.downloadFile;
			if (File.Exists(filename))
				File.Delete(filename);
			try
			{
                string downloadURL = UtilSecurity.Decrypt(HttpUtility.UrlDecode(vi.id));
                webClient.DownloadFileAsync(new Uri(downloadURL), filename);
			}
			catch (Exception ex)
			{
				Code.MsgBox.Show("The download prosess was stopped due to an error, or it was terminated.","Download process stopped", this);
				Log.LogToFile(ex);
				this.Close();
			}
			
		}

		private void DownloadNewVersionInProgress(object sender, DownloadProgressChangedEventArgs e)
		{
			progressDownload.Value = e.ProgressPercentage;
			Application.DoEvents();
		}

		private void DownloadNewVersionDone(object sender, AsyncCompletedEventArgs e)
		{
			Application.DoEvents();
			VersionInfo vi = CheckForNewVersion.versionInfo;
			string filename = Config.AppDataDownloadFolder + vi.downloadFile;
			if (e.Cancelled)
			{
				Code.MsgBox.Show("Download has been canceled","Download canceled", this);
				this.Close();
			}
			else
			{
				string msg = "Wot Numbers version " + vi.version + " is downloaded. The downloaded file is located here:" + Environment.NewLine + Environment.NewLine +
					filename + Environment.NewLine + Environment.NewLine +
					"Press 'OK' to close Wot Numbers and start the installation." + Environment.NewLine + Environment.NewLine;
                Code.MsgBox.Button answer = Code.MsgBox.Show(msg, "Start installation now", MsgBox.Type.OKCancel, this);
				if (answer == MsgBox.Button.OK)
				{
					Process.Start(filename);
					Application.Exit();
				}
				else
				{
					this.Close();
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			try
			{
				webClient.CancelAsync();
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				this.Close();
			}
			
		}

	}
}
