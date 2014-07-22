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
		public UpdateFromApi()
		{
			InitializeComponent();
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

		private void btnStart_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			UpdateFromApiTheme.Cursor = Cursors.WaitCursor;
			btnStart.Enabled = false;
			badProgressBar.ValueMax = 6;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;

			// Get tanks, remember to init tankList first
			UpdateProgressBar("Retrieves tanks from Wargaming API");
			TankData.GetTankList();
			ImportWotApi2DB.ImportTanks();
			// Init after getting tanks and other basic data import
			TankData.GetTankList();
			TankData.GetJson2dbMappingFromDB();

			// Get turret
			UpdateProgressBar("Retrieves tank turrets from Wargaming API");
			ImportWotApi2DB.ImportTurrets();
			
			// Get guns
			UpdateProgressBar("Retrieves tank guns from Wargaming API");
			ImportWotApi2DB.ImportGuns();

			// Get radios
			UpdateProgressBar("Retrieves tank radios from Wargaming API");
			ImportWotApi2DB.ImportRadios();

			// Get achievements
			UpdateProgressBar("Retrieves achievements from Wargaming API");
			ImportWotApi2DB.ImportAchievements();
			TankData.GetAchList();

			// Get WN8 ratings
			UpdateProgressBar("Retrieves WN8 expected values from API");
			ImportWN8Api2DB.UpdateWN8();

			// Done
			UpdateProgressBar("");
			lblProgressStatus.Text = "Update finished: " + DateTime.Now.ToString();
			btnStart.Enabled = true;

			this.Cursor = Cursors.Default;
			this.Close();
		}
	}
}
