using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class InGarageApi : FormCloseOnEsc
    {
		public InGarageApi()
		{
            SetBrowserFeatureControl();
            InitializeComponent();
		}

		private void InGarageApi_Load(object sender, EventArgs e)
		{
			Uri url= new Uri("http://wotnumbers.com/Api/WotAuthenticate.aspx?uid=1&server=" + Config.Settings.playerServer);
			webBrowser1.ScriptErrorsSuppressed = true;
			webBrowser1.Url = url;
		}

		private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			// Look for:
			// http://wotnumbers.com/Api/WotAuthenticate.aspx?authenticationresult=1&status=ok&access_token=ba1ca887d157e6842507b715ba3f0eaee69f1789&nickname=BadButton&account_id=500700596&expires_at=1407971037
			string result = e.Url.AbsoluteUri.ToString();
			string resultOk = "http://wotnumbers.com/Api/WotAuthenticate.aspx?authenticationresult=1&";
			if (result.Length > resultOk.Length && result.Substring(0, resultOk.Length) == resultOk)
			{
				// Get Values
				// status=ok
				// access_token=zzzzzzzzzzzzzzzzzzzzzzzzzzzzz
				// nickname=BadButton
				// account_id=500700596
				// expires_at=1407971037
				InGarageApiResult.status = "";
				InGarageApiResult.access_token = "";
				InGarageApiResult.nickname = "";
				InGarageApiResult.account_id = "";
				InGarageApiResult.expires_at = "";
				InGarageApiResult.message = "";
				string data = result.Substring(resultOk.Length);
				string[] sectionSeparators = { "&" };
				string[] sections = data.Split(sectionSeparators, StringSplitOptions.RemoveEmptyEntries);
				foreach (string section in sections)
				{
					string[] valueSeparators = { "=" };
					string[] value = section.Split(valueSeparators, StringSplitOptions.RemoveEmptyEntries);
					if (value.Length == 2)
					{
						string val = value[1];
						switch (value[0])
						{
							case "status": InGarageApiResult.status = val; break;
							case "access_token": InGarageApiResult.access_token = val; break;
							case "nickname": InGarageApiResult.nickname = val; break;
							case "account_id": InGarageApiResult.account_id = val; break;
							case "expires_at": InGarageApiResult.expires_at = val; break;
							case "message": InGarageApiResult.message = val; break;
						}
					}
				}
				if (InGarageApiResult.status != "ok")
				{
					Code.MsgBox.Show("Login cancelled or error occured trying to authenticate using Wargaming API." + Environment.NewLine + Environment.NewLine +
						InGarageApiResult.message + Environment.NewLine + Environment.NewLine, "Terminated authentication using Watgaming API", this);
				}
				this.Close();
			}
		}

		private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
		{
			txtAddress.Text = e.Url.ToString();
			txtAddress.ToolTipText = txtAddress.Text;
		}

        private void SetBrowserFeatureControlKey(string feature, string appName, uint value)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(
                String.Concat(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\", feature),
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(appName, (UInt32)value, RegistryValueKind.DWord);
            }
        }

        private void SetBrowserFeatureControl()
        {
            // http://msdn.microsoft.com/en-us/library/ee330720(v=vs.85).aspx

            // FeatureControl settings are per-process
            var fileName = System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

            // make the control is not running inside Visual Studio Designer
            if (String.Compare(fileName, "devenv.exe", true) == 0 || String.Compare(fileName, "XDesProc.exe", true) == 0)
                return;

            SetBrowserFeatureControlKey("FEATURE_BROWSER_EMULATION", fileName, GetBrowserEmulationMode()); // Webpages containing standards-based !DOCTYPE directives are displayed in IE10 Standards mode.
            SetBrowserFeatureControlKey("FEATURE_AJAX_CONNECTIONEVENTS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_MANAGE_SCRIPT_CIRCULAR_REFS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_DOMSTORAGE ", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_GPU_RENDERING ", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_IVIEWOBJECTDRAW_DMLT9_WITH_GDI  ", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_DISABLE_LEGACY_COMPRESSION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_LOCALMACHINE_LOCKDOWN", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_OBJECT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_SCRIPT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_DISABLE_NAVIGATION_SOUNDS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_SCRIPTURL_MITIGATION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_SPELLCHECKING", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_STATUS_BAR_THROTTLING", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_TABBED_BROWSING", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_VALIDATE_NAVIGATE_URL", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_DOCUMENT_ZOOM", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_POPUPMANAGEMENT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_MOVESIZECHILD", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_ADDON_MANAGEMENT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_WEBSOCKET", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WINDOW_RESTRICTIONS ", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_XMLHTTP", fileName, 1);
        }

        private UInt32 GetBrowserEmulationMode()
        {
            int browserVersion = 7;
            using (var ieKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                System.Security.AccessControl.RegistryRights.QueryValues))
            {
                var version = ieKey.GetValue("svcVersion");
                if (null == version)
                {
                    version = ieKey.GetValue("Version");
                    if (null == version)
                        throw new ApplicationException("Microsoft Internet Explorer is required!");
                }
                int.TryParse(version.ToString().Split('.')[0], out browserVersion);
            }

            UInt32 mode = 11000; // Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode. Default value for Internet Explorer 11.
            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. Default value for applications hosting the WebBrowser Control.
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode. Default value for Internet Explorer 8
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode. Default value for Internet Explorer 9.
                    break;
                case 10:
                    mode = 10000; // Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE10 mode. Default value for Internet Explorer 10.
                    break;
                default:
                    // use IE11 mode by default
                    break;
            }

            return mode;
        }

	}
}
