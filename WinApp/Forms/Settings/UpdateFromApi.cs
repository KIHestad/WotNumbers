using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class UpdateFromApi : Form
	{
		private static bool _autoRun = false;
		public UpdateFromApi(bool autoRun = false)
		{
			InitializeComponent();
			_autoRun = autoRun; 
		}

		private void UpdateFromApi_Shown(object sender, EventArgs e)
		{
			if (_autoRun)
				RunNow();
		}

		private void UpdateProgressBar(string statusText)
		{
			lblProgressStatus.Text = statusText;
			if (statusText == "")
				badProgressBar.Value = 0;
			else
				badProgressBar.Value++;
			Refresh();
			Application.DoEvents();
		}

		private void RunNow()
		{
			this.Cursor = Cursors.WaitCursor;
			UpdateFromApiTheme.Cursor = Cursors.WaitCursor;
			btnStart.Enabled = false;
			badProgressBar.ValueMax = 4;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;

			// Get tanks, remember to init tankList first
			UpdateProgressBar("Retrieves tanks from Wargaming API");
			TankHelper.GetTankList();
			ImportWotApi2DB.ImportTanks(this);
			// Init after getting tanks and other basic data import
			TankHelper.GetTankList();
			TankHelper.GetJson2dbMappingFromDB();

			// Get turret
			// UpdateProgressBar("Retrieves tank turrets from Wargaming API");
			// ImportWotApi2DB.ImportTurrets(this);

			// Get guns
			// UpdateProgressBar("Retrieves tank guns from Wargaming API");
			// ImportWotApi2DB.ImportGuns(this);

			// Get radios
			// UpdateProgressBar("Retrieves tank radios from Wargaming API");
			// ImportWotApi2DB.ImportRadios(this);

			// Get achievements
			UpdateProgressBar("Retrieves achievements from Wargaming API");
			ImportWotApi2DB.ImportAchievements(this);
			TankHelper.GetAchList();

			// Get achievements
			UpdateProgressBar("Retrieves maps from Wargaming API");
			ImportWotApi2DB.ImportMaps(this);
			
			// Get WN8 ratings
			UpdateProgressBar("Retrieves WN8 expected values from API");
			ImportWN8Api2DB.UpdateWN8(this);

			// Done
			DBVersion.RunWotApi = false;
			UpdateProgressBar("");
			lblProgressStatus.Text = "Update finished: " + DateTime.Now.ToString();
			btnStart.Enabled = true;

			// Save to settings
			Config.Settings.doneRunWotApi = DateTime.Now;
			string msg = "";
			Config.SaveConfig(out msg);

			// Done
			this.Cursor = Cursors.Default;
			this.Close();
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			RunNow();
		}

		
	}
}
