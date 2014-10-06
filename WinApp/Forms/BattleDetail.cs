using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class BattleDetail : Form
	{
		private int _battleId;
		
		public BattleDetail(int battleId)
		{
			InitializeComponent();
			_battleId = battleId;
		}

		private void btnTab_Click(object sender, EventArgs e)
		{
			// deselect tabs
			btnEnemyTeam.Checked = false;
			btnOurTeam.Checked = false;
			btnPersonal.Checked = false;
			btnTeams.Checked = false;
			// deselect panels
			panel1.Visible = false;
			// select tab
			BadButton btn = (BadButton)sender;
			btn.Checked = true;
			string selectedTab = btn.Name;
			switch (selectedTab)
			{
				case "btnPersonal" :
					panel1.Visible = true;
					break;
			}
		}

		private void BattleDetail_Load(object sender, EventArgs e)
		{
			GetMyPersonalInfo();
		}

		private void GetMyPersonalInfo()
		{
			string sql =
				"SELECT battle.*, tank.id as tankId, tank.name as tankName, map.name as mapName, " +
				"		battleResult.name as battleResultName, battleResult.color as battleResultColor, " + 
				"		battleSurvive.name as battleSurviveName, battleSurvive.color as battleSurviveColor " +
				"FROM   battle INNER JOIN " +
				"       playerTank ON battle.playerTankId = playerTank.id INNER JOIN " +
				"       tank ON playerTank.tankId = tank.id INNER JOIN " +
				"       tankType ON tank.tankTypeId = tankType.Id INNER JOIN " +
				"       country ON tank.countryId = country.Id INNER JOIN " +
				"       battleResult ON battle.battleResultId = battleResult.id LEFT JOIN " +
				"       map on battle.mapId = map.id INNER JOIN " +
				"       battleSurvive ON battle.battleSurviveId = battleSurvive.id " +
				"WHERE	battle.id=@battleId";
			DB.AddWithValue(ref sql, "@battleId", _battleId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				DataRow dr = dt.Rows[0];
				// Battle result
				lblResult.Text = dr["battleResultName"].ToString();
				Color battleResultColor = ColorTranslator.FromHtml(dr["battleResultColor"].ToString());
				lblResult.ForeColor = battleResultColor;
				// Tank img
				int tankId = Convert.ToInt32(dr["tankId"]);
				picTank.Image = ImageHelper.GetTankImage(tankId, ImageHelper.TankImageType.LargeImage);
				// Tank name
				lblTankName.Text = dr["tankName"].ToString();
				// Mastery Badge Image
				int masteryBadge = Convert.ToInt32(dr["markOfMastery"]);
				picMB.Image = ImageHelper.GetMasteryBadgeImage(masteryBadge, false);
				// Map name
				string mapName = "";
				if (dr["mapName"] != DBNull.Value) mapName = dr["mapName"].ToString();
				lblMap.Text = mapName;
				// Battle time
				DateTime finished = Convert.ToDateTime(dr["battleTime"]);
				int duration = Convert.ToInt32(dr["battleLifeTime"]);
				TimeSpan t = TimeSpan.FromSeconds(duration);
				lblDate.Text = finished.ToString("d");
				lblTime.Text = finished.ToString("t");
				lblDuration.Text = string.Format("{0:D0}:{1:D2}", t.Minutes, t.Seconds);
				// Battle count
				int battleCount = Convert.ToInt32(dr["battlesCount"]);
				int survivedCount = Convert.ToInt32(dr["survived"]);
				// Survival
				string survival = dr["battleSurviveName"].ToString();
				string deathReason = "";
				if (dr["deathReason"] != DBNull.Value && survival != "Yes") deathReason = " due to: " + dr["deathReason"].ToString();
				Color battleSurviveColor = ColorTranslator.FromHtml(dr["battleSurviveColor"].ToString());
				switch (survival)
				{
					case "Yes": survival = "Survived"; break;
					case "No": survival = "Killed" + deathReason; break;
					case "Some": survival = "Survived: " + survivedCount.ToString() + " / " + battleCount.ToString(); break;
				}
				lblSurvival.Text = survival;
				lblSurvival.ForeColor = battleSurviveColor;
				
				// Battle mode
				string battleMode = "";
				int bonusType = -1;
				if (dr["bonusType"] != DBNull.Value) bonusType = Convert.ToInt32(dr["bonusType"]);
				if (bonusType == -1)
				{
					if (battleCount > 1)
						battleMode = "Several battles recorded";
					else
						battleMode = "Battle result not fetched";
				}
				else
				{
					switch (bonusType)
					{
						case 0: battleMode = "Unknown Battle Mode"; break;
						case 1: battleMode = "Standard Battle"; break;
						case 2: battleMode = "Trainig Room Battle"; break;
						case 3: battleMode = "Tank Company Battle"; break;
						case 4: battleMode = "Clan War Battle"; break;
						case 5: battleMode = "Tutorial Battle"; break;
						case 10: battleMode = "Skimish Battle"; break;
					}
				}
				lblBattleMode.Text = battleMode;
			}
		}

		private void BattleDetail_Resize(object sender, EventArgs e)
		{
			//lblResult.Left = panel1.Width / 2 - lblResult.Width / 2;
			//picTank.Left = panel1.Width / 2 - picTank.Width / 2;
		}


	}
}
