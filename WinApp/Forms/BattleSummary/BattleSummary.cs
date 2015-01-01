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
	public partial class BattleSummary : Form
	{
		private string battleTimeFilter = "";
		private string battleModeFilter = "";
		private string battleMode = "";
		private string tankFilter = "";
		private string tankJoin = "";
		private int wn8 = 0;
		private int wn8avg = 0;
		private int wn7 = 0;
		private int wn7avg = 0;
		private int eff = 0;
		private int effavg = 0;

		public BattleSummary(string currentBattleTimeFilter, string currentBattleModeFilter,string currentBattleMode, string currentTankFilter, string currentTankJoin)
		{
			InitializeComponent();
			battleModeFilter = currentBattleModeFilter;
			battleMode = currentBattleMode;
			if (battleMode == "%")
				battleMode = "";
			battleTimeFilter = currentBattleTimeFilter;
			tankFilter = currentTankFilter;
			tankJoin = currentTankJoin;
		}

		private void BattleSummary_Load(object sender, EventArgs e)
		{
			// Create SQL
			string sql =
				"SELECT sum(battle.battlescount) " +
				"FROM    battle INNER JOIN " +
				"        playerTank ON battle.playerTankId = playerTank.id INNER JOIN " +
				"        tank ON playerTank.tankId = tank.id INNER JOIN " +
				"        tankType ON tank.tankTypeId = tankType.Id INNER JOIN " +
				"        country ON tank.countryId = country.Id INNER JOIN " +
				"        battleResult ON battle.battleResultId = battleResult.id LEFT JOIN " +
				"        map on battle.mapId = map.id INNER JOIN " +
				"        battleSurvive ON battle.battleSurviveId = battleSurvive.id " + tankJoin +
				"WHERE   playerTank.playerId=@playerid " + battleTimeFilter + battleModeFilter + tankFilter;
			DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
			DataTable dt = new DataTable();
			dt = DB.FetchData(sql, Config.Settings.showDBErrors);
			DataRow dr = null;
			int battlesCount = 0;
			if (dt.Rows.Count > 0)
			{
				dr = dt.Rows[0];
				battlesCount = Convert.ToInt32(dr[0]);
			}
			if (battlesCount == 0)
			{
				MsgBox.Show("No battles", "Battles summary not available");
				this.Close();
			}
			else
			{
				// Show battle count in form title
				if (battlesCount == 1)
					BattleSummaryTheme.Text = "Battles Summary for one battle";
				else
					BattleSummaryTheme.Text = "Battles Summary for " + battlesCount.ToString() + " battles";
				// Show battle mode
				BattleSummaryTheme.Text += " - Average values based on Battle Mode: " + BattleHelper.GetBattleModeReadableName(battleMode);
				// Get battle data
				GetStandardGridDetails();
				GetWN8Details();
			}
		}

		private void GetStandardGridDetails()
		{
			// This battle result
			string sql =
				"SELECT sum(battlesCount) as battlesCount, sum(dmg) as dmg, sum(assistSpot) as assistSpot, sum(assistTrack) as assistTrack, sum(dmgBlocked) as dmgBlocked, sum(potentialDmgReceived) as potentialDmgReceived, sum(dmgReceived) as dmgReceived,  " +
				"  sum(shots) as shots, sum(hits) as hits, sum(pierced) as pierced, sum(heHits) as heHits, sum(piercedReceived) as piercedReceived, sum(shotsReceived) as shotsReceived, sum(heHitsReceived) as heHitsReceived, sum(noDmgShotsReceived) as noDmgShotsReceived, " +
				"  sum(frags) as frags, sum(spotted) as spot, sum(cap) as cap, sum(def) as def, sum(battle.mileage) as mileage, sum(battle.treesCut) as treesCut, " +
				"  sum(credits) as credits, sum(creditsPenalty) as creditsPenalty, sum(creditsContributionIn) as creditsContributionIn, sum(creditsContributionOut) as creditsContributionOut, sum(creditsToDraw) as creditsToDraw, sum(autoRepairCost) as autoRepairCost, sum(autoLoadCost) as autoLoadCost, sum(autoEquipCost) as autoEquipCost, " +
				"    sum(eventCredits) as eventCredits , sum(originalCredits) as originalCredits, sum(creditsNet) as creditsNet, sum(achievementCredits) as achievementCredits, sum(premiumCreditsFactor10) as premiumCreditsFactor10, " +
				"  sum(real_xp) as real_xp, sum(xpPenalty) * -1 as xpPenalty, sum(freeXP) as freeXP, sum(dailyXPFactor10) as dailyXPFactor10, sum(premiumXPFactor10) as premiumXPFactor10, sum(eventXP) as eventXP, sum(eventFreeXP) as eventFreeXP, sum(eventTMenXP) as eventTMenXP, sum(achievementXP) as achievementXP, sum(achievementFreeXP) as achievementFreeXP " +
				"FROM    battle INNER JOIN " +
				"        playerTank ON battle.playerTankId = playerTank.id INNER JOIN " +
				"        tank ON playerTank.tankId = tank.id INNER JOIN " +
				"        tankType ON tank.tankTypeId = tankType.Id INNER JOIN " +
				"        country ON tank.countryId = country.Id INNER JOIN " +
				"        battleResult ON battle.battleResultId = battleResult.id LEFT JOIN " +
				"        map on battle.mapId = map.id INNER JOIN " +
				"        battleSurvive ON battle.battleSurviveId = battleSurvive.id " + tankJoin +
				"WHERE   playerTank.playerId=@playerid " + battleTimeFilter + battleModeFilter + tankFilter;
			DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
			DataTable dtRes = DB.FetchData(sql);
			// Total result
			string battleModeAvgFilter = "";
			if (battleMode != "")
				battleModeAvgFilter = " AND battleMode = @battleMode";
			sql =
				"SELECT sum(battles) as battles, sum(dmg) as dmg, sum(dmgReceived) as dmgReceived, sum(assistSpot) as assistSpot, sum(assistTrack) as assistTrack, sum(dmgBlocked) as dmgBlocked, sum(potentialDmgReceived) as potentialDmgReceived, " +
				"  sum(shots) as shots, sum(hits) as hits, sum(pierced) as pierced, sum(heHits) as heHits, sum(piercedReceived) as piercedReceived, sum(shotsReceived) as shotsReceived, sum(heHitsReceived) as heHitsReceived, sum(noDmgShotsReceived) as noDmgShotsReceived, " +
				"  sum(frags) as frags, sum(spot) as spot, sum(cap) as cap, sum(def) as def, sum(mileage) as mileage, sum(treesCut) as treesCut " +
				"FROM playerTank INNER JOIN playerTankBattle ON playerTank.id = playerTankBattle.playerTankId " +
				"WHERE playerTank.playerId=@playerId " + battleModeAvgFilter;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
			DataTable dtAvg = DB.FetchData(sql);
			if (dtRes.Rows.Count > 0)
			{
				DataRow drVal = dtRes.Rows[0];
				DataRow drAvg = null;
				bool avgOK = (dtAvg.Rows.Count > 0);
				int avgBattleCount = 0;
				if (avgOK)
				{
					drAvg = dtAvg.Rows[0];
					if (drAvg["battles"] != DBNull.Value)
						avgBattleCount = Convert.ToInt32(drAvg["battles"]);
					avgOK = (avgBattleCount > 0);
				}
				// Create new datatable to show result
				DataTable dt = new DataTable();
				dt.Columns.Add("Parameter", typeof(string));
				dt.Columns.Add("Image", typeof(Image));
				dt.Columns.Add("Result", typeof(string));
				dt.Columns.Add("Average", typeof(string));

				// Add Rows to damage grid
				DataTable dtDamage = dt.Clone();
				dtDamage.Rows.Add(GetValues(dtDamage, drVal, drAvg, "Damage Done", "dmg", avgBattleCount));
				dtDamage.Rows.Add(GetValues(dtDamage, drVal, drAvg, "      by Spotting", "assistSpot", avgBattleCount));
				dtDamage.Rows.Add(GetValues(dtDamage, drVal, drAvg, "      by Tracking", "assistTrack", avgBattleCount));
				dtDamage.Rows.Add(GetValues(dtDamage, drVal, drAvg, "      by Blocking", "dmgBlocked", avgBattleCount));
				dtDamage.Rows.Add(GetValues(dtDamage, drVal, drAvg, "Damage Received", "dmgReceived", avgBattleCount, false));
				dtDamage.Rows.Add(GetValues(dtDamage, drVal, drAvg, "      Potential", "potentialDmgReceived", avgBattleCount, false));
				// Damage done/received
				string dmgDoneReceived = "INF";
				double dmgDR = Convert.ToDouble(drVal["dmgReceived"]);
				Image dmgDRImage = new Bitmap(1, 1);
				bool getDmgDRImage = true;
				if (dmgDR > 0)
				{
					dmgDR = Convert.ToDouble(drVal["dmg"]) / dmgDR;
					dmgDoneReceived = Math.Round(dmgDR, 1).ToString("# ### ##0.0");
				}
				else
					getDmgDRImage = false;
				string avgDmgDoneReceived = "INF";
				double avgDmgDR = Convert.ToDouble(drAvg["dmgReceived"]);
				if (avgDmgDR > 0)
				{
					avgDmgDR = (Convert.ToDouble(drAvg["dmg"]) / avgDmgDR);
					avgDmgDoneReceived = Math.Round(avgDmgDR, 1).ToString("# ### ##0.0");
				}
				else
					getDmgDRImage = false;
				if (getDmgDRImage) dmgDRImage = GetIndicator(dmgDR, avgDmgDR);
				dtDamage.Rows.Add(GetValues(dtDamage, dmgDoneReceived, avgDmgDoneReceived, "Dmg Done/Received", dmgDRImage));

				// Done;
				dtDamage.AcceptChanges();
				dgvDamage.DataSource = dtDamage;
				FormatStandardDataGrid(dgvDamage, "");

				// Add Rows to shooting grid
				DataTable dtShooting = dt.Clone();
				dtShooting.Rows.Add(GetValues(dtShooting, drVal, drAvg, "Shots Fired", "shots", avgBattleCount, decimals: 1));
				dtShooting.Rows.Add(GetValues(dtShooting, drVal, drAvg, "      Hits", "hits", avgBattleCount, decimals: 1));
				dtShooting.Rows.Add(GetValues(dtShooting, drVal, drAvg, "      Pierced", "pierced", avgBattleCount, decimals: 1));
				dtShooting.Rows.Add(GetValues(dtShooting, drVal, drAvg, "      He Hits", "heHits", avgBattleCount, decimals: 1));
				dtShooting.Rows.Add(GetValues(dtShooting, drVal, drAvg, "Shots Received", "shotsReceived", avgBattleCount, false, decimals: 1));
				dtShooting.Rows.Add(GetValues(dtShooting, drVal, drAvg, "      Pierced", "piercedReceived", avgBattleCount, false, decimals: 1));
				dtShooting.Rows.Add(GetValues(dtShooting, drVal, drAvg, "      He Hits", "heHitsReceived", avgBattleCount, false, decimals: 1));
				dtShooting.Rows.Add(GetValues(dtShooting, drVal, drAvg, "      No Damage", "noDmgShotsReceived", avgBattleCount, false, decimals: 1));

				// Done;
				dtShooting.AcceptChanges();
				dgvShooting.DataSource = dtShooting;
				FormatStandardDataGrid(dgvShooting, "");

				// Add Rows to shooting grid
				DataTable dtPerformance = dt.Clone();
				dtPerformance.Rows.Add(GetValues(dtPerformance, drVal, drAvg, "Frags", "frags", avgBattleCount, decimals: 1));
				dtPerformance.Rows.Add(GetValues(dtPerformance, drVal, drAvg, "Spot", "spot", avgBattleCount, decimals: 1));
				dtPerformance.Rows.Add(GetValues(dtPerformance, drVal, drAvg, "Cap", "cap", avgBattleCount, decimals: 1));
				dtPerformance.Rows.Add(GetValues(dtPerformance, drVal, drAvg, "Defence", "def", avgBattleCount, decimals: 1));
				// Done;
				dtPerformance.AcceptChanges();
				dgvPerformance.DataSource = dtPerformance;
				FormatStandardDataGrid(dgvPerformance, "");

				// Add Rows to other result grid
				DataTable dtOther = dt.Clone();
				dtOther.Rows.Add(GetValues(dtOther, drVal, drAvg, "Drive distance (m)", "mileage", avgBattleCount));
				dtOther.Rows.Add(GetValues(dtOther, drVal, drAvg, "Trees Cut", "treesCut", avgBattleCount, decimals: 1));
				// Done;
				dtOther.AcceptChanges();
				dgvOther.DataSource = dtOther;
				FormatStandardDataGrid(dgvOther, "");

				// Calculate avg ratings
				wn8avg = Convert.ToInt32(Rating.CalculatePlayerTotalWN8(battleMode));
				wn7avg = Convert.ToInt32(Rating.CalcTotalWN7(battleMode));
				effavg = Convert.ToInt32(Rating.CalcTotalEFF(battleMode));
				// Calg current battle ratings
				wn8 = Convert.ToInt32(Rating.CalcAvgBattleWN8(battleTimeFilter, 0, battleMode, tankFilter, battleModeFilter));
				wn7 = Convert.ToInt32(Rating.CalcBattleWN7(battleTimeFilter, 0, battleMode, tankFilter, battleModeFilter));
				eff = Convert.ToInt32(Rating.CalcBattleEFF(battleTimeFilter, 0, battleMode, tankFilter, battleModeFilter));
				// Add rows to Ratings grid
				DataTable dtRating = dt.Clone();
				dtRating.Rows.Add(GetValues(dtRating, wn8, wn8avg, "WN8"));
				dtRating.Rows.Add(GetValues(dtRating, wn7, wn7avg, "WN7"));
				dtRating.Rows.Add(GetValues(dtRating, eff, effavg, "EFF"));

				// Done;
				dtRating.AcceptChanges();
				dgvRating.DataSource = dtRating;
				FormatStandardDataGrid(dgvRating, "");


				//lblWN8.ForeColor = Rating.WN8color(wn8);
				//lblWN7.ForeColor = Rating.WN7color(wn7);
				//lblEFF.ForeColor = Rating.EffColor(eff);

				// Enhanced battle result
				// Create SQL for getting rows with enhanced battle data
				// This battle result
				sql =
					"SELECT sum(battlesCount) as battlesCount, sum(dmg) as dmg, sum(assistSpot) as assistSpot, sum(assistTrack) as assistTrack, sum(dmgBlocked) as dmgBlocked, sum(potentialDmgReceived) as potentialDmgReceived, sum(dmgReceived) as dmgReceived,  " +
					"  sum(shots) as shots, sum(hits) as hits, sum(pierced) as pierced, sum(heHits) as heHits, sum(piercedReceived) as piercedReceived, sum(shotsReceived) as shotsReceived, sum(heHitsReceived) as heHitsReceived, sum(noDmgShotsReceived) as noDmgShotsReceived, " +
					"  sum(frags) as frags, sum(spotted) as spot, sum(cap) as cap, sum(def) as def, sum(battle.mileage) as mileage, sum(battle.treesCut) as treesCut, " +
					"  sum(credits) as credits, sum(creditsPenalty) as creditsPenalty, sum(creditsContributionIn) as creditsContributionIn, sum(creditsContributionOut) as creditsContributionOut, sum(creditsToDraw) as creditsToDraw, sum(autoRepairCost) as autoRepairCost, sum(autoLoadCost) as autoLoadCost, sum(autoEquipCost) as autoEquipCost, " +
					"    sum(eventCredits) as eventCredits , sum(originalCredits) as originalCredits, sum(creditsNet) as creditsNet, sum(achievementCredits) as achievementCredits, sum(premiumCreditsFactor10) as premiumCreditsFactor10, " +
					"  sum(real_xp) as real_xp, sum(xpPenalty) * -1 as xpPenalty, sum(freeXP) as freeXP, sum(dailyXPFactor10) as dailyXPFactor10, sum(premiumXPFactor10) as premiumXPFactor10, sum(eventXP) as eventXP, sum(eventFreeXP) as eventFreeXP, sum(eventTMenXP) as eventTMenXP, sum(achievementXP) as achievementXP, sum(achievementFreeXP) as achievementFreeXP " +
					"FROM    battle INNER JOIN " +
					"        playerTank ON battle.playerTankId = playerTank.id INNER JOIN " +
					"        tank ON playerTank.tankId = tank.id INNER JOIN " +
					"        tankType ON tank.tankTypeId = tankType.Id INNER JOIN " +
					"        country ON tank.countryId = country.Id INNER JOIN " +
					"        battleResult ON battle.battleResultId = battleResult.id LEFT JOIN " +
					"        map on battle.mapId = map.id INNER JOIN " +
					"        battleSurvive ON battle.battleSurviveId = battleSurvive.id " + tankJoin +
					"WHERE   arenaUniqueID is not null AND playerTank.playerId=@playerid " + battleTimeFilter + battleModeFilter + tankFilter;
				DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
				dtRes = DB.FetchData(sql);
				if (dtRes.Rows.Count == 0)
				{
					// not fetched battle result
					pictureBoxCredits.Visible = false;
					pictureBoxCreditsBack.Visible = false;
					lblCredits.Visible = false;
					pictureBoxXP.Visible = false;
					pictureBoxXPBack.Visible = false;
					lblXP.Visible = false;
				}
				else
				{
					// All battle results 
					sql =
						"SELECT SUM(battlesCount) as battles, SUM(credits) as credits, SUM(creditsNet) as creditsNet, " +
						"   SUM(autoRepairCost) as autoRepairCost, SUM(autoLoadCost) as autoLoadCost, SUM(autoEquipCost) as autoEquipCost, SUM(creditsContributionOut) as creditsContributionOut, " +
						"  SUM(real_xp) as real_xp, SUM(xpPenalty) * -1 as xpPenalty, SUM(freeXP) as freeXP, SUM(eventXP) as eventXP, SUM(eventFreeXP) as eventFreeXP, SUM(eventTMenXP) as eventTMenXP, " +
						"  SUM(achievementXP) as achievementXP, SUM(achievementFreeXP) as achievementFreeXP " +
						"FROM battle INNER JOIN playerTank ON battle.playerTankId = playerTank.Id " +
						"WHERE arenaUniqueID is not null AND playerTank.playerId=@playerId " + battleModeFilter; 
					DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
					DataTable dtTotBattle = DB.FetchData(sql);
					DataRow drTotBattle = dtTotBattle.Rows[0];
					double totBattleCount = Convert.ToInt32(drTotBattle["battles"]);

					// Add Rows to credit grid
					DataTable dtCredit = dt.Clone();
					dtCredit.Rows.Add(GetValues(dtCredit, drVal, drTotBattle, "Income", "credits", Convert.ToInt32(totBattleCount)));
					double totalCost =
						Convert.ToInt32(drVal["autoRepairCost"]) +
						Convert.ToInt32(drVal["autoLoadCost"]) +
						Convert.ToInt32(drVal["autoEquipCost"]) +
						Convert.ToInt32(drVal["creditsContributionOut"]);
					double avgCost =
						Convert.ToInt32(drTotBattle["autoRepairCost"]) +
						Convert.ToInt32(drTotBattle["autoLoadCost"]) +
						Convert.ToInt32(drTotBattle["autoEquipCost"]) +
						Convert.ToInt32(drTotBattle["creditsContributionOut"]);
					avgCost = avgCost / totBattleCount;
					Image totalCostImg = GetIndicator(totalCost, avgCost, higherIsBest: false);
					dtCredit.Rows.Add(GetValues(dtCredit, "-" + totalCost.ToString("# ### ###"), "-" + Math.Round(avgCost, 0).ToString("# ### ###"), "Cost", totalCostImg));
					dtCredit.Rows.Add(GetValues(dtCredit, drVal, drTotBattle, "Result", "creditsNet", Convert.ToInt32(totBattleCount)));
					// Done;
					dtCredit.AcceptChanges();
					dgvCredit.DataSource = dtCredit;
					FormatStandardDataGrid(dgvCredit, "");

					// Show multiplier
					//string multiplier = drVal["dailyXPFactorTxt"].ToString().Replace(" ", "");
					//if (multiplier != "1X")
					//	lblXP.Text = "XP (" + multiplier + ")";
					
					//// Add Rows to XP grid
					DataTable dtXP = dt.Clone();
					dtXP.Rows.Add(GetValues(dtXP, drVal, drTotBattle, "Total XP", "real_xp", Convert.ToInt32(totBattleCount)));
					dtXP.Rows.Add(GetValues(dtXP, drVal, drTotBattle, "    Due to mission", "eventXP", Convert.ToInt32(totBattleCount)));
					dtXP.Rows.Add(GetValues(dtXP, drVal, drTotBattle, "    Fine for causing dmg", "xpPenalty", Convert.ToInt32(totBattleCount)));
					dtXP.Rows.Add(GetValues(dtXP, drVal, drTotBattle, "Free XP", "freeXP", Convert.ToInt32(totBattleCount)));
					// Done;
					dtXP.AcceptChanges();
					dgvXP.DataSource = dtXP;
					FormatStandardDataGrid(dgvXP, "");
				}

			}
		}

		private void GetWN8Details()
		{
			// Format
			GridHelper.StyleDataGrid(dgvWN8, DataGridViewSelectionMode.CellSelect);
			dgvWN8.GridColor = ColorTheme.GridHeaderBackLight;
			dgvWN8.AllowUserToResizeColumns = false;
			// Get data
			string sql =
				"SELECT playerTank.tankId, battle.battlesCount, battle.dmg, battle.spotted, battle.frags, battle.def, " +
				"      tank.expDmg, tank.expSpot, tank.expFrags, tank.expDef, tank.expWR " +
				"FROM    battle INNER JOIN " +
				"        playerTank ON battle.playerTankId = playerTank.id INNER JOIN " +
				"        tank ON playerTank.tankId = tank.id INNER JOIN " +
				"        tankType ON tank.tankTypeId = tankType.Id INNER JOIN " +
				"        country ON tank.countryId = country.Id INNER JOIN " +
				"        battleResult ON battle.battleResultId = battleResult.id LEFT JOIN " +
				"        map on battle.mapId = map.id INNER JOIN " +
				"        battleSurvive ON battle.battleSurviveId = battleSurvive.id " + tankJoin +
				"WHERE   playerTank.playerId=@playerid " + battleTimeFilter + battleModeFilter + tankFilter;
			DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				// Calc total WN 8 parameter values
				double dmg = 0;
				double spotted = 0;
				double frags = 0;
				double def = 0;
				double exp_dmg = 0;
				double exp_spotted = 0;
				double exp_frags = 0;
				double exp_def = 0;
				double exp_wr = 0;
				foreach (DataRow dr in dt.Rows)
				{
					dmg += Convert.ToDouble(dr["dmg"]);
					spotted += Convert.ToDouble(dr["spotted"]);
					frags += Convert.ToDouble(dr["frags"]);
					def += Convert.ToDouble(dr["def"]);
					exp_dmg += DbConvert.ToDouble(dr["expDmg"]);
					exp_spotted += DbConvert.ToDouble(dr["expSpot"]);
					exp_frags += DbConvert.ToDouble(dr["expFrags"]);
					exp_def += DbConvert.ToDouble(dr["expDef"]);
					exp_wr += DbConvert.ToDouble(dr["expWR"]);
				}
				// Get result and total expected values
				double rWINc;
				double rDAMAGEc;
				double rFRAGSc;
				double rSPOTc;
				double rDEFc;
				Rating.UseWN8FormulaReturnResult(
						dmg, spotted, frags, def, exp_wr,
						exp_dmg, exp_spotted, exp_frags, exp_def, exp_wr,
						out rWINc, out rDAMAGEc, out rFRAGSc, out rSPOTc, out rDEFc);
				
				// Create table for showing WN8 details
				DataTable dtWN8 = new DataTable();
				dtWN8.Columns.Add("Parameter", typeof(string));
				dtWN8.Columns.Add("Image", typeof(Image));
				dtWN8.Columns.Add("Result", typeof(string));
				dtWN8.Columns.Add("Exp", typeof(string));
				dtWN8.Columns.Add("Value", typeof(string));

				// Damage
				DataRow drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "Damage";
				drWN8["Image"] = GetIndicator(dmg, exp_dmg);
				drWN8["Result"] = dmg.ToString("N0");
				drWN8["Exp"] = exp_dmg.ToString("N0");
				drWN8["Value"] = Math.Round(rDAMAGEc, 0).ToString("N0");
				dtWN8.Rows.Add(drWN8);
				// Frags
				drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "Frags";
				drWN8["Image"] = GetIndicator(frags, exp_frags);
				drWN8["Result"] = frags.ToString("N0");
				drWN8["Exp"] = exp_frags.ToString("N0");
				drWN8["Value"] = Math.Round(rFRAGSc, 0).ToString("N0");
				dtWN8.Rows.Add(drWN8);
				// Spot
				drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "Spot";
				drWN8["Image"] = GetIndicator(spotted, exp_spotted);
				drWN8["Result"] = spotted.ToString("N0");
				drWN8["Exp"] = exp_spotted.ToString("N0");
				drWN8["Value"] = Math.Round(rSPOTc, 0).ToString("N0");
				dtWN8.Rows.Add(drWN8);
				// Defence
				drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "Defence";
				drWN8["Image"] = GetIndicator(def, exp_def);
				drWN8["Result"] = def.ToString("N0");
				drWN8["Exp"] = exp_def.ToString("N0");
				drWN8["Value"] = Math.Round(rDEFc, 1).ToString("N0");
				dtWN8.Rows.Add(drWN8);
				// Win Rate
				drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "Win Rate";
				drWN8["Image"] = imgIndicators.Images[1];
				drWN8["Result"] = "Fixed";
				drWN8["Exp"] = exp_wr.ToString() + "%";
				drWN8["Value"] = Math.Round(rWINc, 1).ToString("N0");
				dtWN8.Rows.Add(drWN8);
				// Total
				drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "WN8";
				drWN8["Image"] = new Bitmap(1, 1);
				drWN8["Result"] = "";
				drWN8["Exp"] = "";
				drWN8["Value"] = wn8.ToString("N0");
				dtWN8.Rows.Add(drWN8);
				// Done;
				dtWN8.AcceptChanges();
				dgvWN8.DataSource = dtWN8;
				// Format dgv
				dgvWN8.Columns[0].HeaderText = "";
				dgvWN8.Columns[1].HeaderText = "";
				// Total width = 220
				dgvWN8.Columns[0].Width = 70;
				dgvWN8.Columns[1].Width = 20;
				dgvWN8.Columns[2].Width = 50;
				dgvWN8.Columns[3].Width = 50;
				dgvWN8.Columns[4].Width = 50;
				dgvWN8.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
				dgvWN8.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				dgvWN8.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				dgvWN8.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				dgvWN8.Width = dgvWN8.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + 2;
				dgvWN8.Height = dgvWN8.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + dgvWN8.ColumnHeadersHeight + 2;
				foreach (DataGridViewColumn dgvc in dgvWN8.Columns)
				{
					dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
				}
				dgvWN8.Visible = true;
				dgvWN8.ClearSelection();
			}
		}

		private DataRow GetValues(DataTable dt, DataRow drVal, DataRow drAvg, string rowHeader, string sqlField, int avgBattleCount, bool higherIsBest = true, int decimals = 0)
		{
			int battleCount = Convert.ToInt32(drVal["battlesCount"]);
			DataRow drNew = dt.NewRow();
			drNew["Parameter"] = rowHeader;
			double val = Convert.ToDouble(drVal[sqlField]) / battleCount;
			double avg = 0;
			if (avgBattleCount > 0) avg = Convert.ToDouble(drAvg[sqlField]) / avgBattleCount;
			drNew["Image"] = GetIndicator(val, avg, (avgBattleCount > 0), higherIsBest: higherIsBest);
			string numFormat = "# ### ##0";
			if (decimals > 0)
				numFormat += ".0";
			drNew["Result"] = Math.Round(val, decimals).ToString(numFormat);
			drNew["Average"] = Math.Round(avg, decimals).ToString(numFormat);
			return drNew;
		}

		private DataRow GetValues(DataTable dt, string val, string avg, string rowHeader, Image image)
		{
			// Image number:
			// 0 = higher (green)
			// 1 = equal (orange)
			// 2 = lower (red)
			DataRow drNew = dt.NewRow();
			drNew["Parameter"] = rowHeader;
			drNew["Image"] = image;
			drNew["Result"] = val;
			drNew["Average"] = avg;
			return drNew;
		}

		private DataRow GetValues(DataTable dt, int val, int avg, string rowHeader)
		{
			// Image number:
			// 0 = higher (green)
			// 1 = equal (orange)
			// 2 = lower (red)
			DataRow drNew = dt.NewRow();
			drNew["Parameter"] = rowHeader;
			drNew["Image"] = GetIndicator(val, avg);
			drNew["Result"] = val.ToString();
			drNew["Average"] = avg.ToString();
			return drNew;
		}

		private Image GetIndicator(double value, double compareTo, bool showIcon = true, bool higherIsBest = true)
		{
			if (!showIcon)
				return new Bitmap(1, 1);
			else
			{
				int indicator = 1; // neutral
				if (higherIsBest)
				{
					// Normal - highet var = best
					if (value > compareTo * 1.025)
						indicator = 0; // up
					else if (value < compareTo * 0.975)
						indicator = 2; // down
				}
				else
				{
					// Reverse mode - lowest value = best
					if (value > compareTo * 1.025)
						indicator = 2; // down
					else if (value < compareTo * 0.975)
						indicator = 0; // up
				}
				return imgIndicators.Images[indicator];
			}
		}

		private void FormatStandardDataGrid(DataGridView dgv, string headerText)
		{
			// Format dgv
			GridHelper.StyleDataGrid(dgv, DataGridViewSelectionMode.CellSelect);
			dgv.GridColor = ColorTheme.GridHeaderBackLight;
			dgv.AllowUserToResizeColumns = false;
			// Headers
			dgv.Columns[0].HeaderText = headerText;
			dgv.Columns[1].HeaderText = "";
			// Total width = 230
			dgv.Columns[0].Width = 120;
			dgv.Columns[1].Width = 20;
			dgv.Columns[2].Width = 50;
			dgv.Columns[3].Width = 50;
			// Align
			dgv.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dgv.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			dgv.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			// Size
			dgv.Width = dgv.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + 2;
			dgv.Height = dgv.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + dgv.ColumnHeadersHeight + 2;
			// Format
			dgv.Columns[2].DefaultCellStyle.Format = "N0";
			dgv.Columns[3].DefaultCellStyle.Format = "N0";
			// No sorting
			foreach (DataGridViewColumn dgvc in dgv.Columns)
			{
				dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
			}
			dgv.Visible = true;
			dgv.ClearSelection();
		}

		private void dgvRating_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			// rating color
			dgvRating.Rows[0].Cells["Result"].Style.ForeColor = Rating.WN8color(wn8);
			dgvRating.Rows[0].Cells["Average"].Style.ForeColor = Rating.WN8color(wn8avg);
			dgvRating.Rows[1].Cells["Result"].Style.ForeColor = Rating.WN7color(wn7);
			dgvRating.Rows[1].Cells["Average"].Style.ForeColor = Rating.WN7color(wn7avg);
			dgvRating.Rows[2].Cells["Result"].Style.ForeColor = Rating.EffColor(eff);
			dgvRating.Rows[2].Cells["Average"].Style.ForeColor = Rating.EffColor(effavg);
		}

		private void dgvWN8_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			// rating color
			dgvWN8.Rows[5].Cells["Value"].Style.ForeColor = Rating.WN8color(wn8);
		}


	}
}
