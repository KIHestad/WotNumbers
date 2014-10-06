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
		#region Init
		
		private int _battleId;
		private int team1 = 1;
		private int team2 = 2;
		private DataGridView dgvTeam1 = new DataGridView();
		private DataGridView dgvTeam2 = new DataGridView();
		private bool showFortResources = false;

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
			// hide my result
			panel1.Visible = false;
			// hide grids
			dgvTeam1.Visible = false;
			dgvTeam2.Visible = false;
			// select tab
			BadButton btn = (BadButton)sender;
			btn.Checked = true;
			string selectedTab = btn.Name;
			switch (selectedTab)
			{
				case "btnPersonal" :
					panel1.Visible = true;
					break;
				case "btnTeams" :
					ShowTeams();
					break;
				case "btnOurTeam" :
					ShowOwnTeam();
					break;
				case "btnEnemyTeam" :
					ShowEnemyTeam();
					break;
			}
		}

		private void BattleDetail_Load(object sender, EventArgs e)
		{
			GetMyPersonalInfo();
			string sql = "select id from battlePlayer where battleId=@battleId";
			DB.AddWithValue(ref sql, "@battleId", _battleId, DB.SqlDataType.Int);
			if (DB.FetchData(sql).Rows.Count > 0)
			{
				// Show team tabs and make ready datagrids
				btnTeams.Visible = true;
				btnOurTeam.Visible = true;
				btnEnemyTeam.Visible = true;
				dgvTeam1.Visible = false;
				dgvTeam2.Visible = false;
				GridHelper.StyleDataGrid(dgvTeam1);
				GridHelper.StyleDataGrid(dgvTeam2);
				this.Controls.Add(dgvTeam1);
				this.Controls.Add(dgvTeam2);
				// Find team 1 and 2
				sql = "select team from battlePlayer where battleId=@battleId and name=@name";
				DB.AddWithValue(ref sql, "@battleId", _battleId, DB.SqlDataType.Int);
				DB.AddWithValue(ref sql, "@name", Config.Settings.playerName, DB.SqlDataType.VarChar);
				DataTable dt = DB.FetchData(sql);
				if (dt.Rows.Count > 0)
				{
					team1 = Convert.ToInt32(dt.Rows[0][0]);
					team2 = 1;
					if (team1 == 1) team2 = 2;
				}
			}
		}

		#endregion

		#region My Result

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
				int masteryBadge = 0;
				if (dr["markOfMastery"] != DBNull.Value) masteryBadge = Convert.ToInt32(dr["markOfMastery"]);
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
				if (dr["deathReason"] != DBNull.Value && survival != "Yes") deathReason = " by: " + dr["deathReason"].ToString();
				Color battleSurviveColor = ColorTranslator.FromHtml(dr["battleSurviveColor"].ToString());
				switch (survival)
				{
					case "Yes": survival = "Survived"; break;
					case "No": survival = "Destroyed" + deathReason; break;
					case "Some": survival = "Battles survived: " + survivedCount.ToString(); break;
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
						battleMode = "Battles recorded: " + battleCount.ToString() ;
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
						case 10: battleMode = "Skimish Battle"; showFortResources = true; break;
					}
				}
				lblBattleMode.Text = battleMode;
				// Ratings
				double wn8 = Convert.ToDouble(dr["WN8"]);
				double wn7 = Convert.ToDouble(dr["WN7"]);
				double eff = Convert.ToDouble(dr["EFF"]);
				lblWN8.Text = Math.Round(wn8,0).ToString();
				lblWN7.Text = Math.Round(wn7,0).ToString();
				lblEFF.Text = Math.Round(eff,0).ToString();
				lblWN8.ForeColor = Rating.WN8color(wn8);
				lblWN7.ForeColor = Rating.WN8color(wn7);
				lblEFF.ForeColor = Rating.WN8color(eff);
				GetWN8Details();
			}
		}

		private void GetWN8Details()
		{
			string sql =
				"SELECT playerTank.tankId, battle.battlesCount, battle.dmg, battle.spotted, battle.frags, battle.def, " +
				"      tank.expDmg, tank.expSpot, tank.expFrags, tank.expDef, tank.expWR " +
				"FROM battle INNER JOIN playerTank ON battle.playerTankId = playerTank.id INNER JOIN tank ON playerTank.tankId = tank.id " +
				"WHERE battle.id = @battleId";
			DB.AddWithValue(ref sql, "@battleId", _battleId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				DataRow dr = dt.Rows[0];
				int tankId = Convert.ToInt32(dr["tankId"]);
				int battlesCount = Convert.ToInt32(dr["battlesCount"]);
				int dmg = Convert.ToInt32(dr["dmg"]);
				int spotted = Convert.ToInt32(dr["spotted"]);
				int frags = Convert.ToInt32(dr["frags"]);
				int def = Convert.ToInt32(dr["def"]);
				int exp_dmg = Convert.ToInt32(dr["expDmg"]);
				int exp_spotted = Convert.ToInt32(dr["expSpot"]) ;
				int exp_frags = Convert.ToInt32(dr["expFrags"]) ;
				int exp_def = Convert.ToInt32(dr["expDef"]);
				int exp_wr = Convert.ToInt32(dr["expWR"]);
				string wn8 = Math.Round(Rating.CalculateTankWN8(tankId, battlesCount, dmg, spotted, frags, def, 0, true), 0).ToString();
				double rWINc;
				double rDAMAGEc;
				double rFRAGSc;
				double rSPOTc;
				double rDEFc;
				Rating.UseWN8FormulaReturnResult(
					dmg, spotted, frags, def, exp_wr,
					exp_dmg, exp_spotted, exp_frags, exp_def, exp_wr,
					out rWINc, out rDAMAGEc, out rFRAGSc, out rSPOTc, out rDEFc);
				// Exp val
				txtRating_Exp_Dmg.Text = exp_dmg.ToString();
				txtRating_Exp_Frags.Text = exp_frags.ToString();
				txtRating_Exp_Spot.Text = exp_spotted.ToString();
				txtRating_Exp_Def.Text = exp_def.ToString();
				txtRating_Exp_WR.Text = exp_wr.ToString() + "%";
				// Result
				txtRating_Res_Dmg.Text = dmg.ToString();
				txtRating_Res_Frags.Text = frags.ToString();
				txtRating_Res_Spot.Text = spotted.ToString();
				txtRating_Res_Def.Text = def.ToString();
				// Values
				txtRating_Val_Dmg.Text = Math.Round(rDAMAGEc, 1).ToString();
				txtRating_Val_Frags.Text = Math.Round(rFRAGSc, 1).ToString();
				txtRating_Val_Spot.Text = Math.Round(rSPOTc, 1).ToString();
				txtRating_Val_Def.Text = Math.Round(rDEFc, 1).ToString();
				txtRating_Val_WR.Text = Math.Round(rWINc, 1).ToString();
				// Indicators
				picRatingWN8_Dmg.Image = imgIndicators.Images[GetIndicator(dmg, exp_dmg)];
				picRatingWN8_Frags.Image = imgIndicators.Images[GetIndicator(frags, exp_frags)];
				picRatingWN8_Spot.Image = imgIndicators.Images[GetIndicator(spotted, exp_spotted)];
				picRatingWN8_Def.Image = imgIndicators.Images[GetIndicator(def, exp_def)];
			}
		}

		private int GetIndicator(double value, double compareTo)
		{
			int indicator = 1; // neutral
			if (value > compareTo)
				indicator = 0; // up
			else if (value < compareTo)
				indicator = 2; // down
			return indicator;
		}

		#endregion

		#region Team Overview

		private void ShowTeams()
		{
			ResizeNow();
			dgvTeam1.DataSource = GetDataGridSource(team1);
			dgvTeam2.DataSource = GetDataGridSource(team2);
			AutoSizeColumns(dgvTeam1);
			AutoSizeColumns(dgvTeam2);
			dgvTeam1.BringToFront();
			dgvTeam2.BringToFront();
		}

		private void ShowOwnTeam()
		{
			ResizeNow();
			dgvTeam1.DataSource = GetDataGridSource(team1, true);
			AutoSizeColumns(dgvTeam1);
			dgvTeam1.BringToFront();
		}

		private void ShowEnemyTeam()
		{
			ResizeNow();
			dgvTeam2.DataSource = GetDataGridSource(team2, true);
			AutoSizeColumns(dgvTeam2);
			dgvTeam2.BringToFront();
		}

		private DataTable GetDataGridSource(int team, bool enhanced = false)
		{
			string fortResourcesFields = "";
			if (showFortResources) fortResourcesFields = ", fortResource as 'IR' ";
			string enhancedFields = "";
			if (enhanced)
				enhancedFields =
					", credits as 'Credits' " +
					", spotted as 'Spot' " +
					", kills as 'Frags' " +
					", tkills as 'Team kills' " +
					", shots as 'Shots' " +
					", hits as 'Hits' " +
					", directHits as 'Direct Hits' " +
					", capturePoints as 'Cap' " +
					", droppedCapturePoints as 'Dropped Cap' " +
					", damageReceived as 'Dmg Received' " +
					", shotsReceived as 'Shots Received' " +
					", directHitsReceived as 'Hits Received' " +
					", deathReason as 'Death Reason' ";
			string sql =
				"select tank.name as 'Tank', battlePlayer.name as 'Team Player', clanAbbrev as Clan, xp as 'XP', damageDealt as 'Dmg' " + 
				fortResourcesFields +
				enhancedFields +
				"from battlePlayer inner join " + 
				"     tank on battlePlayer.tankId = tank.id " +
				"where battleId=@battleId and team=@team";
			DB.AddWithValue(ref sql, "@battleId", _battleId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@team", team, DB.SqlDataType.Int);
			return DB.FetchData(sql);
		}
		
		private void AutoSizeColumns(DataGridView dgv)
		{
			dgv.Visible = true;
			dgv.AutoResizeColumns();
			int maxWidth = 80;
			foreach (DataGridViewColumn c in dgv.Columns)
			{
				if (c.Width > maxWidth)
				{
					c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
					c.Width = maxWidth;
				}
			}
		}

		#endregion

		#region resize

		private void BattleDetail_Resize(object sender, EventArgs e)
		{
			ResizeNow();
		}

		private void ResizeNow()
		{
			if (btnTeams.Checked)
			{
				PlaceGrid(dgvTeam1, GridLocation.Left);
				PlaceGrid(dgvTeam2, GridLocation.Right);
			}
			else if (btnOurTeam.Checked)
				PlaceGrid(dgvTeam1, GridLocation.Whole);
			else if (btnEnemyTeam.Checked)
				PlaceGrid(dgvTeam2, GridLocation.Whole);
		}

		private enum GridLocation
		{
			Left = 1,
			Right = 2,
			Whole = 3,
		}

		private void PlaceGrid(Control ctrl, GridLocation location)
		{
			switch (location)
			{
				case GridLocation.Left:
					ctrl.Left = grpMain.Left + 1;
					ctrl.Top = grpMain.Top + 8;
					ctrl.Width = grpMain.Width / 2 - 1;
					ctrl.Height = grpMain.Height - 9;
					break;
				case GridLocation.Right:
					ctrl.Left = grpMain.Left + (grpMain.Width / 2) + 1;
					ctrl.Top = grpMain.Top + 8;
					ctrl.Width = grpMain.Width / 2 - 1;
					ctrl.Height = grpMain.Height - 9;
					break;
				case GridLocation.Whole:
					ctrl.Left = grpMain.Left + 1;
					ctrl.Top = grpMain.Top + 8;
					ctrl.Width = grpMain.Width - 2;
					ctrl.Height = grpMain.Height - 9;
					break;
				default:
					break;
			}
		}

		#endregion

	}
}
