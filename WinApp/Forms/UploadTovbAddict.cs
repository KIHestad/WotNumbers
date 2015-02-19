using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class UploadTovBAddict : Form
	{
		public UploadTovBAddict()
		{
			InitializeComponent();
		}

		private void btnTestConnection_Click(object sender, EventArgs e)
		{
			MsgBox.Show(vBAddict.TestConnection(), "vBAddict connection test result");
		}

		private void btnUploadDossier_Click(object sender, EventArgs e)
		{
			string dossierFile = Config.AppDataBaseFolder + "dossier_prev.dat";
			string token = txtToken.Text.Trim();
			string msg = "";
			bool result = vBAddict.UploadDossier(dossierFile, Config.Settings.playerName, Config.Settings.playerServer.ToLower(), token, out msg, false);
			string msgHeader = "Success uploading dossier file to vBAddict";
			if (!result)
				msgHeader = "Error uploading dossier file to vBAddict";
			MsgBox.Show(msg, msgHeader);
		}

		private void UploadTovBAddict_Load(object sender, EventArgs e)
		{
			chkActivateAutoUpload.Checked = Config.Settings.vBAddictUploadActive;
			txtToken.Text = Config.Settings.vBAddictPlayerToken;
		}

		private void btnSaveSettings_Click(object sender, EventArgs e)
		{
			Config.Settings.vBAddictUploadActive = chkActivateAutoUpload.Checked;
			Config.Settings.vBAddictPlayerToken = txtToken.Text;
			string msg = "";
			if (!Config.SaveConfig(out msg))
				MsgBox.Show("Error saving settings: " + msg, "Save result");
			else
				this.Close();
		}

		private void linkVbAddict_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.vbaddict.net/token.php");
		}
		
	}
}
