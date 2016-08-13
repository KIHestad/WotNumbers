using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class About : FormCloseOnEsc
    {
		public About()
		{
			InitializeComponent();
		}

		private void About_Load(object sender, EventArgs e)
		{
			string dbVersionComment = " (correct version)";
			if (DBVersion.ExpectedNumber != DBVersion.GetDBVersion())
				dbVersionComment = " (expected: " + DBVersion.ExpectedNumber.ToString("0000") + ")";
			lblAppVer.Text = AppVersion.AssemblyVersion + " " + AppVersion.BuildVersion;
			lblDBver.Text = DBVersion.GetDBVersion().ToString("0000");
			lblWN8ver.Text = DBVersion.GetWNVersion(8).ToString();
            lblWN9ver.Text = DBVersion.GetWNVersion(9).ToString();
        }

		private void linkWotNumbers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://wotnumbers.com/");
		}

		private void linkVbAddict_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.vbaddict.net/");
		}

		private void btnIronPythonEnvironment_Click(object sender, EventArgs e)
		{
			// Use IronPython
			try
			{
				//if (!PythonEngine.InUse)
				//{
				string msg =
					"Runtime.IO.InputEncoding: " + Environment.NewLine +
					"   " + PythonEngine.Engine.Runtime.IO.InputEncoding.EncodingName + Environment.NewLine +
					"   " + PythonEngine.Engine.Runtime.IO.InputEncoding.HeaderName + " / " + PythonEngine.Engine.Runtime.IO.InputEncoding.BodyName + Environment.NewLine +
					Environment.NewLine +
					"Runtime.IO.OutputEncoding: " + Environment.NewLine +
					"   " + PythonEngine.Engine.Runtime.IO.OutputEncoding.EncodingName + Environment.NewLine +
					"   " + PythonEngine.Engine.Runtime.IO.OutputEncoding.HeaderName + " / " + PythonEngine.Engine.Runtime.IO.OutputEncoding.BodyName + Environment.NewLine +
					Environment.NewLine + Environment.NewLine;
				Code.MsgBox.Show(msg, "IronPython Environment");
				//}
				//else
				//	Code.MsgBox.Show("Python Engine is in use, cannot check default encoding now. Try again in a few sec", "IronPython Busy");

			}
			catch (Exception ex)
			{
				Log.LogToFile(ex, "CheckIronPythonDefaultEncoding exception");
				Code.MsgBox.Show("Error: " + ex.Message + Environment.NewLine + Environment.NewLine +
					"Inner Exception: " + ex.InnerException, "Error running IronPython Environment Check");
			}
		}

	}
}
