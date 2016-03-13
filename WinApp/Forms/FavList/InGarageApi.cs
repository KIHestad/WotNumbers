using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;
using CefSharp.WinForms;
using CefSharp;
using System.Threading.Tasks;
using WinApp.Code.CefBrowser;

namespace WinApp.Forms
{
	public partial class InGarageApi : Form
	{
        private ChromiumWebBrowser browser { get; set; }

        public InGarageApi()
		{
			InitializeComponent();
		}

		private void InGarageApi_Load(object sender, EventArgs e)
		{
            // Create browser go to default page
            string address = "http://wotnumbers.com/Api/WotAuthenticate.aspx?uid=1&server=" + Config.Settings.playerServer;
            browser = new ChromiumWebBrowser(address);
            browser.AddressChanged += OnBrowserAddressChanged;
            browser.Dock = DockStyle.Fill;
            pnlBrowser.Controls.Add(browser);
		}

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            bool ok = false;
            Form frm = this;
            // get new browser url address
            this.InvokeOnUiThreadIfRequired(() => txtAddress.Text = args.Address);
            txtAddress.ToolTipText = txtAddress.Text;
            // check if received result
            string url = txtAddress.Text; // args.Browser.MainFrame.Url;
            string urlOk = "http://wotnumbers.com/Api/WotAuthenticate.aspx?authenticationresult=1&";
            if (url.Length > urlOk.Length && url.Substring(0, urlOk.Length) == urlOk)
            {
                string data = url.Substring(urlOk.Length);
                ok = CheckWebResult(data, this);
                if (ok)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        // close the form on the forms thread
                        this.Close();
                    });
                }
            }
        }
       
        private bool CheckWebResult(string data, Form parentForm)
		{
            bool ok = true;
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
                ok = false;
			}
            return ok;
		}

	
	}
}
