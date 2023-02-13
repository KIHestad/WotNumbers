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
	public partial class UpdateFromApi : FormCloseOnEsc
    {
		private static bool _autoRun = false;
		public UpdateFromApi(bool autoRun = false)
		{
			InitializeComponent();
			_autoRun = autoRun; 
		}

		private async void UpdateFromApi_Shown(object sender, EventArgs e)
		{
			if (_autoRun)
				await RunNow();
		}

		private void UpdateProgressBar(string statusText)
		{
			lblProgressStatus.Text = statusText;
			if (statusText == "")
				badProgressBar.Value = 0;
			else
				badProgressBar.Value++;
			Refresh();
		}

		private async Task RunNow()
		{
			this.Cursor = Cursors.WaitCursor;
			UpdateFromApiTheme.Cursor = Cursors.WaitCursor;
			btnStart.Enabled = false;
			badProgressBar.ValueMax = 5;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;
            bool overwriteCustom = (chkOverwriteCustom.Checked);

			// Get achievements
			UpdateProgressBar("Retrieves player account ids from Wargaming API");
			await ImportWotApi2DB.ImportPlayerAccountId(this, Config.Settings.playerServer);

			// Get tanks, remember to init tankList first
			UpdateProgressBar("Retrieves tanks from Wargaming API");

            // old method
            await TankHelper.GetTankList(); // Init after getting tanks before next tank list fetch
            await ImportWotApi2DB.ImportTanksOldAPI(this, overwriteCustom);

            // New method
            await TankHelper.GetTankList();
            await ImportWotApi2DB.ImportTanks(this, overwriteCustom);

            // Init after getting tanks and other basic data import
            await TankHelper.GetTankList();
            await TankHelper.GetJson2dbMappingFromDB();

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
			//UpdateProgressBar("Retrieves achievements from Wargaming API");
			//await ImportWotApi2DB.ImportAchievements(this);
            //await TankHelper.GetAchList();

			// Get achievements
			UpdateProgressBar("Retrieves maps from Wargaming API");
			await ImportWotApi2DB.ImportMaps(this);
			
			// Get WN8 ratings
			UpdateProgressBar("Retrieves WN8 expected values from API");
			await ImportWN8Api2DB.UpdateWN8(this);
            await ImportWN8Api2DB.FixMissingWN8(this);

            // Get WN9 ratings
            UpdateProgressBar("Retrieves WN9 expected values from API");
            await ImportWN9Api2DB.UpdateWN9(this);

            // New Init after upgrade db
            await TankHelper.GetAllLists();

            // Done
            DBVersion.RunDownloadAndUpdateTanks = false;
			UpdateProgressBar("");
			lblProgressStatus.Text = "Update finished: " + DateTime.Now.ToString();
			btnStart.Enabled = true;

			// Save to settings
			Config.Settings.doneRunWotApi = DateTime.Now;
            await Config.SaveConfig();

            // Done
            this.Cursor = Cursors.Default;
			this.Close();
		}

		private async void btnStart_Click(object sender, EventArgs e)
		{
            await RunNow();
		}

		
	}
}
