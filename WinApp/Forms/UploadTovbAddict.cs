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
	public partial class UploadTovbAddict : Form
	{
		public UploadTovbAddict()
		{
			InitializeComponent();
		}

		private void btnTestConnection_Click(object sender, EventArgs e)
		{
			MsgBox.Show(vbAddict.TestConnection(), "vbAddict commection test result");
		}

		private void btnUploadDossier_Click(object sender, EventArgs e)
		{
			string dossierFile = Config.AppDataBaseFolder + "dossier_prev.dat";
			string token = token = txtToken.Text.Trim();
			string msg = "";
			bool result = vbAddict.UploadDossier(dossierFile, Config.Settings.playerName, Config.Settings.playerServer.ToLower(), token, out msg);
			string msgHeader = "Success uploading dossier file to vbAddict";
			if (!result)
				msgHeader = "Error uploading dossier file to vbAddict";
			MsgBox.Show(msg, msgHeader);
		}

		private void UploadTovbAddict_Load(object sender, EventArgs e)
		{
			chkActivateAutoUpload.Checked = Config.Settings.vbAddictUploadActive;
			txtToken.Text = Config.Settings.vbAddictPlayerToken;
		}

		private void btnSaveSettings_Click(object sender, EventArgs e)
		{
			Config.Settings.vbAddictUploadActive = chkActivateAutoUpload.Checked;
			Config.Settings.vbAddictPlayerToken = txtToken.Text;
			string msg = "";
			if (!Config.SaveConfig(out msg))
				MsgBox.Show("Error saving settings: " + msg, "Save result");
			else
				this.Close();
		}
		
	}
}
