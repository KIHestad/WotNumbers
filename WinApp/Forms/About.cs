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
	public partial class About : Form
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
			lblWN8ver.Text = DBVersion.GetWN8Version().ToString();
		}

		private void linkWotNumbers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://wotnumbers.com/");
		}

		private void linkVbAddict_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.vbaddict.net/");
		}

	}
}
