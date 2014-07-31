using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinApp.Forms
{
	public partial class InGarageApi : Form
	{
		public InGarageApi()
		{
			InitializeComponent();
		}

		private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			// Look for:
			// http://wotnum.hestad.no/Api/WotAuthenticate.aspx?authenticationresult=1&status=ok&access_token=ba1ca887d157e6842507b715ba3f0eaee69f1789&nickname=BadButton&account_id=500700596&expires_at=1407971037
			string result = e.Url.AbsoluteUri.ToString();
			string resultOk = "http://wotnum.hestad.no/Api/WotAuthenticate.aspx?authenticationresult=1&";
			if (result.Length > resultOk.Length && result.Substring(0, resultOk.Length) == resultOk)
			{
				// Get Values
				// status=ok
				// access_token=ba1ca887d157e6842507b715ba3f0eaee69f1789
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
	}
}
