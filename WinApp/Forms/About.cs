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

		private string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." +
					Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString() + " (" +
					Assembly.GetExecutingAssembly().GetName().Version.MinorRevision.ToString() + ")";
			}
		}

		private void About_Load(object sender, EventArgs e)
		{
			string dbVersionComment = " (correct version)";
			if (DBVersion.ExpectedNumber != DBVersion.CurrentNumber())
				dbVersionComment = " (expected: " + DBVersion.ExpectedNumber.ToString("0000") + ")";
			lblAppVer.Text = AssemblyVersion;
			lblDBver.Text = DBVersion.CurrentNumber().ToString("0000");
			lblWN8ver.Text = DBVersion.WN8Version().ToString();
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
