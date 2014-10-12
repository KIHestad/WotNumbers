using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class WoTGameClientSettings : Form
	{
		public WoTGameClientSettings()
		{
			InitializeComponent();
		}

		private void WoTGameClientSettings_Load(object sender, EventArgs e)
		{
		
			//Find num of cores
			int coreCount = Environment.ProcessorCount;
			if (coreCount > 0) chkCore0.Visible = true;
			if (coreCount > 1) chkCore1.Visible = true;
			if (coreCount > 2) chkCore2.Visible = true;
			if (coreCount > 3) chkCore3.Visible = true;
			if (coreCount > 4) chkCore4.Visible = true;
			if (coreCount > 5) chkCore5.Visible = true;
			if (coreCount > 6) chkCore6.Visible = true;
			if (coreCount > 7) chkCore7.Visible = true;
			// Get saved data
			if (Config.Settings.wotGameStartType == ConfigData.WoTGameStartType.Game)
				ddStartApp.Text = "Wot Game";
			else if (Config.Settings.wotGameStartType == ConfigData.WoTGameStartType.Launcher)
				ddStartApp.Text = "WoT Launcher";
			txtFolder.Text = Config.Settings.wotGameFolder;
			if (txtFolder.Text == "")
			{
				if (Directory.Exists("C:\\Games\\World_of_Tanks"))
					txtFolder.Text = "C:\\Games\\World_of_Tanks";
				else if (Directory.Exists("D:\\Games\\World_of_Tanks"))
					txtFolder.Text = "D:\\Games\\World_of_Tanks";
			}
			txtBatchFile.Text = Config.Settings.wotGameRunBatchFile;
			chkAutoRun.Checked = Config.Settings.wotGameAutoStart;
			if (Config.Settings.wotGameAffinity > 0)
			{
				chkOptimizeOn.Checked = true;
				UpdateCoreCheckBoxes();
			}
			string wotGameAffinity = Config.Settings.wotGameAffinity.ToBinary();
			int core = 0;
			for (int i = wotGameAffinity.Length; i > 0 ; i--)
			{
				string val = wotGameAffinity.Substring(i - 1, 1);
				if (val == "1")
				{
					switch (core)
					{
						case 0: chkCore0.Checked = true; break;
						case 1: chkCore1.Checked = true; break;
						case 2: chkCore2.Checked = true; break;
						case 3: chkCore3.Checked = true; break;
						case 4: chkCore4.Checked = true; break;
						case 5: chkCore5.Checked = true; break;
						case 6: chkCore6.Checked = true; break;
						case 7: chkCore7.Checked = true; break;
					}
				}
				core++;
			}
		}

		private void ddStartApp_Click(object sender, EventArgs e)
		{
			Code.DropDownGrid.Show(ddStartApp, Code.DropDownGrid.DropDownGridType.List, "Do not start WoT,WoT Launcher,Wot Game");
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			ConfigData.WoTGameStartType wotGameStartType = ConfigData.WoTGameStartType.None;
			if (ddStartApp.Text == "Wot Game")
				wotGameStartType = ConfigData.WoTGameStartType.Game;
			if (ddStartApp.Text == "WoT Launcher")
				wotGameStartType = ConfigData.WoTGameStartType.Launcher;
			Config.Settings.wotGameStartType = wotGameStartType;
			Config.Settings.wotGameFolder = txtFolder.Text;
			Config.Settings.wotGameRunBatchFile = txtBatchFile.Text;
			Config.Settings.wotGameAutoStart = chkAutoRun.Checked;
			long wotGameAffinity = 0;
			if (chkOptimizeOn.Checked)
			{
				if (chkCore0.Checked) wotGameAffinity += 1;
				if (chkCore1.Checked) wotGameAffinity += 2;
				if (chkCore2.Checked) wotGameAffinity += 4;
				if (chkCore3.Checked) wotGameAffinity += 8;
				if (chkCore4.Checked) wotGameAffinity += 16;
				if (chkCore5.Checked) wotGameAffinity += 32;
				if (chkCore6.Checked) wotGameAffinity += 64;
				if (chkCore7.Checked) wotGameAffinity += 128;
			}
			Config.Settings.wotGameAffinity = wotGameAffinity;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void chkOptimizeOn_Click(object sender, EventArgs e)
		{
			UpdateCoreCheckBoxes();
		}

		private void UpdateCoreCheckBoxes()
		{
			chkCore0.Enabled = chkOptimizeOn.Checked;
			chkCore1.Enabled = chkOptimizeOn.Checked;
			chkCore2.Enabled = chkOptimizeOn.Checked;
			chkCore3.Enabled = chkOptimizeOn.Checked;
			chkCore4.Enabled = chkOptimizeOn.Checked;
			chkCore5.Enabled = chkOptimizeOn.Checked;
			chkCore6.Enabled = chkOptimizeOn.Checked;
			chkCore7.Enabled = chkOptimizeOn.Checked;
		}

		private void btnFolder_Click(object sender, EventArgs e)
		{
			// Select dossier file
			folderBrowserDialog1.ShowNewFolderButton = false;
			if (txtFolder.Text != "")
			{
				folderBrowserDialog1.SelectedPath = txtFolder.Text;
			}
			folderBrowserDialog1.ShowDialog();
			// If file selected save config with new values
			if (folderBrowserDialog1.SelectedPath != "")
			{
				txtFolder.Text = folderBrowserDialog1.SelectedPath;
			}
		}

		private void btnFile_Click(object sender, EventArgs e)
		{
			openFileDialog1.FileName = "*.bat";
			openFileDialog1.ShowDialog();
			if (openFileDialog1.FileName != "*.bat" && openFileDialog1.FileName != "")
			{
				txtBatchFile.Text = openFileDialog1.FileName;
			}
		}
	}
}
