using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WinApp.Code;
using WinApp.Code.Rating;
using WinApp.Code.FormLayout;
using System.Threading.Tasks;

namespace WinApp.Forms
{
	public partial class BattleSummary : FormCloseOnEsc
    {
		#region init
		
		private string battleTimeFilter = "";
        private bool battleCountFilter = false;
        private string battleModeFilter = "";
		private string battleMode = "";
		private string tankFilter = "";
		private string tankJoin = "";
        private string sqlBattleWhere = "";
        private int wn9 = 0;
        private int wn9avg = 0;
        private int wn8 = 0;
        private int wn8avg = 0;
        private int wn7 = 0;
		private int wn7avg = 0;
		private int eff = 0;
		private int effavg = 0;
        private double wr = 0;
        private double wravg = 0;
		// team datagrid and scrollbar and params
		private DataGridView dgvTeam1 = new DataGridView();
		private BadScrollBar scroll = new BadScrollBar();
		private int playerTeam = 0; // 1=My team | 0=Enemy team
		private string lastOrderHeaderText = "Player";
		private bool? lastOrderAscending = true;

		public BattleSummary(string currentBattleTimeFilter, bool currentBattleCountFilter, string currentBattleModeFilter,string currentBattleMode, string currentTankFilter, string currentTankJoin)
		{
			InitializeComponent();
			battleModeFilter = currentBattleModeFilter;
			battleMode = currentBattleMode;
			if (battleMode == "%")
				battleMode = "";
			battleTimeFilter = currentBattleTimeFilter;
            battleCountFilter = currentBattleCountFilter;
			tankFilter = currentTankFilter;
			tankJoin = currentTankJoin;
		}

		private async void BattleSummary_Load(object sender, EventArgs e)
		{
			// Reset list of valid vBAddict players, forces lookup
            ExternalPlayerProfile.vBAddictPlayers = null;
            ExternalPlayerProfile.vBAddictPlayersManualLookup = true;

            // Create battle filter
            if (battleCountFilter)
                sqlBattleWhere = " AND battle.battlesCountTotal = 1 ";
            else
                sqlBattleWhere = battleTimeFilter + battleModeFilter + tankFilter;

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
                "WHERE   playerTank.playerId=@playerid " + sqlBattleWhere;
            DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
			DataTable dt = new DataTable();
			dt = await DB.FetchData(sql, Config.Settings.showDBErrors);
			DataRow dr = null;
			int battlesCount = 0;
			if (dt.Rows.Count > 0)
			{
				dr = dt.Rows[0];
				battlesCount = Convert.ToInt32(dr[0]);
				InitTeamsTabs();
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
					BattleSummaryTheme.Text = "Summary of Battle";
				else
					BattleSummaryTheme.Text = "Summary of " + battlesCount.ToString() + " Battles";
				// Show battle mode
                BattleSummaryTheme.Text += " - Average values based on Battle Mode: " + BattleMode.GetItemFromSqlName(battleMode).Name;
                // Get battle data
                await GetStandardGridDetails();
                await GetWN8Details();
			}
			
		}

		private void InitTeamsTabs()
		{
			// Show team tabs and make ready datagrids
			btnOurTeam.Visible = true;
			btnEnemyTeam.Visible = true;
			dgvTeam1.Visible = false;
			GridHelper.StyleDataGrid(dgvTeam1);
			dgvTeam1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dgvTeam1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			dgvTeam1.ColumnHeadersHeight = 42;
			dgvTeam1.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvCellFormatting);
			dgvTeam1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(dgvColumnHeaderMouseClick);
			dgvTeam1.ColumnWidthChanged += new DataGridViewColumnEventHandler(dgvColumnWidthChanged);
			dgvTeam1.Top = grpMain.Top + 8;
			dgvTeam1.Left = grpMain.Left + 1;
			this.Controls.Add(dgvTeam1);
			dgvTeam1.RowTemplate.Height = 26;
			// Add scrollbar
			scroll.ScrollOrientation = ScrollOrientation.VerticalScroll;
			scroll.Name = "scroll";
			scroll.Width = 17;
			scroll.Top = dgvTeam1.Top;
			scroll.Visible = false;
			this.Controls.Add(scroll);
			// Add scrollbar events
			scroll.MouseDown += new MouseEventHandler(scroll_MouseDown);
			scroll.MouseUp += new MouseEventHandler(scroll_MouseUp);
			scroll.MouseMove += new MouseEventHandler(scroll_MouseMove);
			// Init placement
			PlaceControl(dgvTeam1, GridLocation.Whole);
            // Add right click menu to grid
            dgvTeam1.CellMouseDown += new DataGridViewCellMouseEventHandler(DgvTeam_CellMouseDown);
            CreateDataGridContextMenu();
		}

		private void chkAvg_Click(object sender, EventArgs e)
		{
			chkAvg.Checked = true;
			chkSum.Checked = false;
			ShowTeam();
		}

		private void chkSum_Click(object sender, EventArgs e)
		{
			chkAvg.Checked = false;
			chkSum.Checked = true;
			ShowTeam();
		}

		private void btnTab_Click(object sender, EventArgs e)
		{
			// deselect tabs
			btnEnemyTeam.Checked = false;
			btnOurTeam.Checked = false;
			btnPersonal.Checked = false;
			// hide my result
			panelMyResult.Visible = false;
			// hide grids
			dgvTeam1.Visible = false;
			// hide scroll
			scroll.Visible = false;
			// select tab
			BadButton btn = (BadButton)sender;
			btn.Checked = true;
			string selectedTab = btn.Name;
			switch (selectedTab)
			{
				case "btnPersonal":
					chkSum.Visible = false;
					chkAvg.Visible = false;
					dgvCredit.ClearSelection();
					dgvDamage.ClearSelection();
					dgvOther.ClearSelection();
					dgvPerformance.ClearSelection();
					dgvRating.ClearSelection();
					dgvShooting.ClearSelection();
					dgvWN8.ClearSelection();
					dgvXP.ClearSelection();
					panelMyResult.Visible = true;
					break;
				case "btnOurTeam":
					chkSum.Visible = true;
					chkAvg.Visible = true;
					playerTeam = 1; // My team
					ShowTeam();
					break;
				case "btnEnemyTeam":
					chkSum.Visible = true;
					chkAvg.Visible = true;
					playerTeam = 0; // Enemy team
					ShowTeam();
					break;
			}
		}

		#endregion

        #region Grid Right Click

        private void CreateDataGridContextMenu()
        {
            dgvTeam1.ContextMenuStrip = ExternalPlayerProfile.MenuItems();
        }

        private async void DgvTeam_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                // Connect the current selected datagrid 
                ExternalPlayerProfile.dataGridRightClick = (DataGridView)sender;
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    ExternalPlayerProfile.dataGridRightClickRow = e.RowIndex;
                    //ExternalPlayerProfile.dataGridRightClick.CurrentCell = ExternalPlayerProfile.dataGridRightClick.Rows[ExternalPlayerProfile.dataGridRightClickRow].Cells[e.ColumnIndex];
                }
            }
            catch (Exception ex)
            {
                await Log.LogToFile(ex);
                if (Config.Settings.showDBErrors)
                    MsgBox.Show("Error on grid mouse down event, see log file", "Grid error", this);
            }
        }

        
        #endregion

		#region My result

		private async Task GetStandardGridDetails()
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
				"WHERE   playerTank.playerId=@playerid " + sqlBattleWhere;
			DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
			DataTable dtRes = await DB.FetchData(sql);
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
			DataTable dtAvg = await DB.FetchData(sql);
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
                wn9avg = Convert.ToInt32(WN9.CalcPlayerTotal(battleMode));
                wn8avg = Convert.ToInt32(WN8.CalcPlayerTotal(battleMode));
				wn7avg = Convert.ToInt32(WN7.WN7total(battleMode));
				effavg = Convert.ToInt32(EFF.EffTotal(battleMode));
                wravg = await WR.WinrateTank(battleTimeFilter, battleMode, tankFilter, battleModeFilter);
                // Calc current battle ratings
                wn9 = Convert.ToInt32(WN9.CalcBattleRange(battleTimeFilter, 0, battleMode, tankFilter, battleModeFilter));
                wn8 = Convert.ToInt32(WN8.CalcBattleRange(battleTimeFilter, 0, battleMode, tankFilter, battleModeFilter));
                wn7 = Convert.ToInt32(WN7.WN7battle(battleTimeFilter, 0, battleMode, tankFilter, battleModeFilter));
                eff = Convert.ToInt32(EFF.EffBattle(battleTimeFilter, 0, battleMode, tankFilter, battleModeFilter));
                wr = await WR.WinrateBattle(battleTimeFilter, battleMode, tankFilter, battleModeFilter);
				// Add rows to Ratings grid
				DataTable dtRating = dt.Clone();
                dtRating.Rows.Add(GetValues(dtRating, wn9, wn9avg, "WN9"));
				dtRating.Rows.Add(GetValues(dtRating, wn8, wn8avg, "WN8"));
				dtRating.Rows.Add(GetValues(dtRating, wn7, wn7avg, "WN7"));
				dtRating.Rows.Add(GetValues(dtRating, eff, effavg, "EFF"));
                dtRating.Rows.Add(GetValues(dtRating, wr, wravg, "Win Rate"));

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
					"WHERE   arenaUniqueID is not null AND playerTank.playerId=@playerid " + sqlBattleWhere;
				DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
				dtRes = await DB.FetchData(sql);
				int battleResultRowCount = 0;
				if (dtRes.Rows.Count > 0 && dtRes.Rows[0][0] != DBNull.Value)
					battleResultRowCount = Convert.ToInt32(dtRes.Rows[0][0]);
				if (battleResultRowCount == 0)
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
					DataTable dtTotBattle = await DB.FetchData(sql);
					DataRow drTotBattle = dtTotBattle.Rows[0];
					double totBattleCount = Convert.ToInt32(drTotBattle["battles"]);
					double battleCount = Convert.ToInt32(dtRes.Rows[0]["battlesCount"]);
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
					totalCost = totalCost / battleCount;
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

		private async Task GetWN8Details()
		{
			// Format
			GridHelper.StyleDataGrid(dgvWN8, DataGridViewSelectionMode.CellSelect);
			dgvWN8.GridColor = ColorTheme.GridHeaderBackLight;
			dgvWN8.AllowUserToResizeColumns = false;
			// Get data
            // todo add battle count filter
			string sql =
				"SELECT playerTank.tankId, battle.battlesCount, battle.dmg, battle.spotted, battle.frags, battle.def, battle.victory, " +
				"      tank.expDmg, tank.expSpot, tank.expFrags, tank.expDef, tank.expWR " +
				"FROM    battle INNER JOIN " +
				"        playerTank ON battle.playerTankId = playerTank.id INNER JOIN " +
				"        tank ON playerTank.tankId = tank.id INNER JOIN " +
				"        tankType ON tank.tankTypeId = tankType.Id INNER JOIN " +
				"        country ON tank.countryId = country.Id INNER JOIN " +
				"        battleResult ON battle.battleResultId = battleResult.id LEFT JOIN " +
				"        map on battle.mapId = map.id INNER JOIN " +
				"        battleSurvive ON battle.battleSurviveId = battleSurvive.id " + tankJoin +
				"WHERE   playerTank.playerId=@playerid " + sqlBattleWhere;
			DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				// Calc total WN 8 parameter values
				//int battlesCount = 0;
                WNHelper.RatingParameters rp = new WNHelper.RatingParameters();
				double exp_dmg = 0;
				double exp_spotted = 0;
				double exp_frags = 0;
				double exp_def = 0;
				double exp_wr = 0;
				foreach (DataRow dr in dt.Rows)
				{
					//battlesCount = Convert.ToInt32(dr["battlesCount"]);
					rp.DAMAGE += Convert.ToDouble(dr["dmg"]);
					rp.SPOT += Convert.ToDouble(dr["spotted"]);
					rp.FRAGS += Convert.ToDouble(dr["frags"]);
					rp.DEF += Convert.ToDouble(dr["def"]);
					wr += Convert.ToDouble(dr["victory"]) / Convert.ToDouble(dr["battlesCount"]) * 100;
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
                WN8.UseFormulaReturnResult(
						rp, wr,
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
				drWN8["Image"] = GetIndicator(rp.DAMAGE, exp_dmg);
				drWN8["Result"] = rp.DAMAGE.ToString("N0");
				drWN8["Exp"] = exp_dmg.ToString("N0");
				drWN8["Value"] = Math.Round(rDAMAGEc, 0).ToString("N0");
				dtWN8.Rows.Add(drWN8);
				// Frags
				drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "Frags";
				drWN8["Image"] = GetIndicator(rp.FRAGS, exp_frags);
				drWN8["Result"] = rp.FRAGS.ToString("N1");
				drWN8["Exp"] = exp_frags.ToString("N1");
				drWN8["Value"] = Math.Round(rFRAGSc, 0).ToString("N0");
				dtWN8.Rows.Add(drWN8);
				// Spot
				drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "Spot";
				drWN8["Image"] = GetIndicator(rp.SPOT, exp_spotted);
				drWN8["Result"] = rp.SPOT.ToString("N1");
				drWN8["Exp"] = exp_spotted.ToString("N1");
				drWN8["Value"] = Math.Round(rSPOTc, 0).ToString("N0");
				dtWN8.Rows.Add(drWN8);
				// Defence
				drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "Defence";
				drWN8["Image"] = GetIndicator(rp.DEF, exp_def);
				drWN8["Result"] = rp.DEF.ToString("N1");
				drWN8["Exp"] = exp_def.ToString("N1");
				drWN8["Value"] = Math.Round(rDEFc, 1).ToString("N0");
				dtWN8.Rows.Add(drWN8);
				// Win Rate
				drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "Win Rate";
				drWN8["Image"] = imgIndicators.Images[1];
				drWN8["Result"] = wr.ToString("N0") + "%";
				drWN8["Exp"] = exp_wr.ToString("N0") + "%";
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

        private DataRow GetValues(DataTable dt, double val, double avg, string rowHeader)
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
            dgvRating.Rows[0].Cells["Result"].Style.ForeColor = ColorRangeScheme.WN9color(wn9);
            dgvRating.Rows[0].Cells["Average"].Style.ForeColor = ColorRangeScheme.WN9color(wn9avg);
            dgvRating.Rows[1].Cells["Result"].Style.ForeColor = ColorRangeScheme.WN8color(wn8);
            dgvRating.Rows[1].Cells["Average"].Style.ForeColor = ColorRangeScheme.WN8color(wn8avg);
            dgvRating.Rows[2].Cells["Result"].Style.ForeColor = ColorRangeScheme.WN7color(wn7);
            dgvRating.Rows[2].Cells["Average"].Style.ForeColor = ColorRangeScheme.WN7color(wn7avg);
            dgvRating.Rows[3].Cells["Result"].Style.ForeColor = ColorRangeScheme.EffColor(eff);
            dgvRating.Rows[3].Cells["Average"].Style.ForeColor = ColorRangeScheme.EffColor(effavg);
            dgvRating.Rows[4].Cells["Result"].Style.ForeColor = ColorRangeScheme.WinRateColor(wr);
            dgvRating.Rows[4].Cells["Average"].Style.ForeColor = ColorRangeScheme.WinRateColor(wravg);

		}

		private void dgvWN8_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			// rating color
            dgvWN8.Rows[5].Cells["Value"].Style.ForeColor = ColorRangeScheme.WN8color(wn8);
		}

		#endregion

		#region Teams

		private void ShowTeam()
		{
			dgvTeam1.DataSource = GetDataGridSource();
			ResizeNow();
			scroll.ScrollPosition = 0;
			FormatDataGrid(dgvTeam1);
			scroll.BringToFront();
			dgvTeam1.BringToFront();
			RefreshScrollbars(dgvTeam1);
			MoveScrollBar();
		}

		private void dgvCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			DataGridView dgv = (DataGridView)sender;
			string col = dgv.Columns[e.ColumnIndex].Name;
			if (col.Equals("Player"))
			{
				if (dgv["Player", e.RowIndex].Value.ToString() == Config.Settings.playerName)
				{
					dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.GridRowCurrentPlayerAlive;
					dgv.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorTheme.GridRowCurrentPlayerAliveSelected;
				}
				else if (e.RowIndex == dgvTeam1.RowCount - 1)
					dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.GridTotalsRow;
			}
			else if (col.Length > 9 && col.Substring(0, 9) == "separator")
			{
				e.CellStyle.BackColor = ColorTheme.GridHeaderBackLight;
				e.CellStyle.SelectionBackColor = ColorTheme.GridSelectedHeaderColor;
			}
		}

		private void dgvColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			// Sorting
			DataGridView dgv = (DataGridView)sender;
			DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
			string columnName = column.HeaderText;
			if (columnName == "") return; // No soring on image
			var sortGlyph = column.HeaderCell.SortGlyphDirection;
			if (columnName != "Tank" && columnName != "Clan" && columnName != "Player" && columnName != "Killed By")
			{
				// Descending sort
				switch (sortGlyph)
				{
					case SortOrder.None:
					case SortOrder.Ascending:
						column.HeaderCell.SortGlyphDirection = SortOrder.Descending;
						break;
					case SortOrder.Descending:
						column.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
						break;
				}
			}
			bool sortDirection = true;
			if (column.HeaderCell.SortGlyphDirection == SortOrder.Descending)
				sortDirection = false; ;
			dgv.DataSource = GetDataGridSource(columnName, sortDirection);
			if ((btnEnemyTeam.Checked || btnOurTeam.Checked) && scroll.ScrollPosition > 0)
			{
				Refresh();
				dgv.FirstDisplayedScrollingRowIndex = scroll.ScrollPosition;
			}
			dgv.ClearSelection();
		}

		private void dgvColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
		{
			if (scroll.ScrollPosition > 0 && (btnEnemyTeam.Checked || btnOurTeam.Checked))
			{
				DataGridView dgv = (DataGridView)sender;
				dgv.FirstDisplayedScrollingRowIndex = scroll.ScrollPosition;
			}
		}

		private string GetSortField(string headerText)
		{
			string sortField = "";
			switch (headerText)
			{
				case "Battles": sortField = "battlePlayer.accountId"; break;
				case "IR": sortField = "battlePlayer.fortResource"; break;
				case "Player": sortField = "battlePlayer.name"; break;
				case "Clan": sortField = "battlePlayer.clanAbbrev"; break;
				case "Dmg": sortField = "battlePlayer.damageDealt"; break;
				case "Frags": sortField = "battlePlayer.kills"; break;
				case "XP": sortField = "battlePlayer.xp"; break;
				case "Dmg Track": sortField = "battlePlayer.damageAssistedTrack"; break;
				case "Dmg Spot": sortField = "battlePlayer.damageAssistedRadio"; break;
				case "Dmg Sniper": sortField = "battlePlayer.sniperDamageDealt"; break;
				case "Dmg Received": sortField = "battlePlayer.damageReceived"; break;
				case "Dmg Blocked": sortField = "battlePlayer.damageBlockedByArmor"; break;
				case "Spot": sortField = "battlePlayer.spotted"; break;
				case "Cap": sortField = "battlePlayer.capturePoints"; break;
				case "Decap": sortField = "battlePlayer.droppedCapturePoints"; break;
				case "Shots": sortField = "battlePlayer.shots"; break;
				case "Hits": sortField = "battlePlayer.hits"; break;
				case "Pierced Hits": sortField = "battlePlayer.pierced"; break;
				case "Explosion Hits": sortField = "battlePlayer.explosionHits"; break;
				case "Hits Received": sortField = "battlePlayer.directHitsReceived"; break;
				case "Piercings Received": sortField = "battlePlayer.piercingsReceived"; break;
				case "Expl Hits Received": sortField = "battlePlayer.explosionHitsReceived"; break;
				case "No Dmg Hits Received": sortField = "battlePlayer.noDamageShotsReceived"; break;
				case "Milage": sortField = "battlePlayer.mileage"; break;
				case "Life Time": sortField = "battlePlayer.lifeTime"; break;
				case "Base Credit": sortField = "battlePlayer.credits"; break;
				case "Premature Leave": sortField = "battlePlayer.isPrematureLeave"; break;
				case "Team Killer": sortField = "battlePlayer.isTeamKiller"; break;
				case "Team Kills": sortField = "battlePlayer.tkills"; break;
			}
			return sortField;
		}


		private async Task<DataTable> GetDataGridSource(string orderHeaderText = "", bool? orderAscending = null)
		{
			if (orderHeaderText == "")
				orderHeaderText = lastOrderHeaderText;
			else
				lastOrderHeaderText = orderHeaderText;
			if (orderAscending == null)
				orderAscending = lastOrderAscending;
			else
				lastOrderAscending = orderAscending;

			string fortResourcesFields = "";
			if (true) fortResourcesFields = ", @CALC(battlePlayer.fortResource) as 'IR' ";
			string orderBy = GetSortField(orderHeaderText);
			if (orderBy != "")
			{
				if (orderBy != "battlePlayer.name" && orderBy != "battlePlayer.clanAbbrev")
				{
					if (orderBy == "battlePlayer.accountId")
						orderBy = "COUNT(" + orderBy + ")";
					else
						orderBy = "@CALC(" + orderBy + ")";
				}
				orderBy = " ORDER BY " + orderBy;
				if (orderAscending == false)
					orderBy += " DESC ";
			}
			string sql =
				"select " +
				"  battlePlayer.name as 'Player'" +
				", battlePlayer.clanAbbrev as Clan" +

				", '' as separator0 " +
				
				", COUNT(battlePlayer.accountId) as 'Battles'" +
				fortResourcesFields +

				", '' as separator1 " +

				", @CALC(CAST(battlePlayer.kills AS FLOAT)) as 'Frags'" +
				", @CALC(CAST(battlePlayer.spotted AS FLOAT)) as 'Spot' " +
				", @CALC(CAST(battlePlayer.capturePoints AS FLOAT)) as 'Cap' " +
				", @CALC(CAST(battlePlayer.droppedCapturePoints AS FLOAT)) as 'Decap' " +

				", '' as separator8 " +

                //", @CALC(tank.hp) as 'Tank HP'" +
                ", @CALC(battlePlayer.damageDealt) as 'Dmg'" +
				", @CALC(battlePlayer.damageReceived) as 'Dmg Received' " +
				", @CALC(battlePlayer.damageBlockedByArmor) as 'Dmg Blocked' " +


				", '' as separator4 " +
				", @CALC(CAST(battlePlayer.shots AS FLOAT)) as 'Shots' " +
				", @CALC(CAST(battlePlayer.hits AS FLOAT)) as 'Hits' " +
				", @CALC(CAST(battlePlayer.pierced AS FLOAT)) as 'Pierced Hits' " +

				", '' as separator6 " +
				", @CALC(battlePlayer.mileage) as 'Milage' " +
				", @CALC(battlePlayer.lifeTime) as 'Life Time' " +

				", '' as separator7 " +
				", @CALC(battlePlayer.xp) as 'XP' " +
				", @CALC(battlePlayer.credits) as 'Base Credit' " +
                ", battlePlayer.accountId " +
                
				"FROM    battle INNER JOIN " +
				"        battlePlayer ON battle.id = battlePlayer.battleId INNER JOIN " +	
				"        playerTank ON battle.playerTankId = playerTank.id INNER JOIN " +
				"        tank ON playerTank.tankId = tank.id INNER JOIN " +
				"        tankType ON tank.tankTypeId = tankType.Id INNER JOIN " +
				"        country ON tank.countryId = country.Id INNER JOIN " +
				"        battleResult ON battle.battleResultId = battleResult.id LEFT JOIN " +
				"        map on battle.mapId = map.id INNER JOIN " +
				"        battleSurvive ON battle.battleSurviveId = battleSurvive.id " + tankJoin +
				"WHERE   playerTank.playerId=@playerid AND battlePlayer.playerTeam=@playerTeam " + battleTimeFilter + battleModeFilter + tankFilter +
                "GROUP BY battlePlayer.name, battlePlayer.clanAbbrev, battlePlayer.accountId " + 
				orderBy;
			//TODO
			if (chkSum.Checked)
				sql = sql.Replace("@CALC(", "SUM(");
			else
				sql = sql.Replace("@CALC(", "AVG(");
			DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@playerTeam", playerTeam, DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql);
			// Add Total Row
			DataRow totalRow = dt.NewRow();
			// Set 0 ad deafult
			foreach (DataColumn dc in dt.Columns)
				if (dc.DataType != System.Type.GetType("System.String"))
					totalRow[dc.ColumnName] = 0;
			totalRow["Player"] = "Total";
			dt.Rows.Add(totalRow);
			// Set blank value instead of 0 and calc total
			int totRow = dt.Rows.Count - 1;
			for (int i = 0; i < totRow; i++)
			{
				DataRow dr = dt.Rows[i];
				foreach (DataColumn dc in dt.Columns)
				{
					if (dc.DataType != System.Type.GetType("System.String") && dc.ColumnName != "accountId")
					{
						if (dr[dc.ColumnName] != DBNull.Value && Convert.ToDouble(dr[dc.ColumnName]) == 0)
							dr[dc.ColumnName] = DBNull.Value;
						else
						{
							int total = Convert.ToInt32(dt.Rows[totRow][dc.ColumnName]);
							double addvalue = 0;
							if (dr[dc.ColumnName] != DBNull.Value) addvalue = Convert.ToDouble(dr[dc.ColumnName]);
							dt.Rows[totRow][dc.ColumnName] = total + addvalue;
						}
					}
				}
			}
			dt.AcceptChanges();
			return dt;
		}

		private void FormatDataGrid(DataGridView dgv)
		{
			// Deselect
			dgv.ClearSelection();
			// Freeze first column
			dgv.Columns[0].Frozen = true;
			// Left align text col
			dgv.Columns["Clan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dgv.Columns["Player"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			// Format
			dgv.Columns["Dmg"].DefaultCellStyle.Format = "N0";
			dgv.Columns["XP"].DefaultCellStyle.Format = "N0";
			// Default width and set sorting
			foreach (DataGridViewColumn dgvc in dgv.Columns)
			{
				if (dgvc.Name.Length > 9 && dgvc.Name.Substring(0, 9) == "separator")
				{
					dgvc.MinimumWidth = 2;
					dgvc.Width = 3;
					dgvc.HeaderText = "";
					dgvc.Resizable = DataGridViewTriState.False;
				}
				else
				{
					dgvc.Width = 50;
				}
				// Sorting, set manual 
				dgvc.SortMode = DataGridViewColumnSortMode.Programmatic;
			}
			// Calc width for rest of fields
			// left part = player, clan, tank img, tank
			int w = dgv.Width - 2; // total available width without img col
			dgv.Columns["Player"].Width = 100;
			if (true) dgv.Columns["IR"].Width = 40;
			dgv.Columns["Battles"].Width = 45;
			dgv.Columns["IR"].Width = 45;
			dgv.Columns["Frags"].Width = 40;
			dgv.Columns["Spot"].Width = 40;
			dgv.Columns["Cap"].Width = 38;
			dgv.Columns["Decap"].Width = 42;
			dgv.Columns["Shots"].Width = 40;
			dgv.Columns["Hits"].Width = 40;
			dgv.Columns["Pierced Hits"].Width = 40;
			
			dgv.Columns["XP"].Width = 55;
			dgv.Columns["Base Credit"].Width = 65;
			dgv.Columns["Dmg Received"].Width = 65;
			// Format
			dgv.Columns["Base Credit"].DefaultCellStyle.Format = "N0";
			dgv.Columns["Dmg Received"].DefaultCellStyle.Format = "N0";
			dgv.Columns["Milage"].DefaultCellStyle.Format = "N0";

			string format = "N0";
			if (chkAvg.Checked) format = "N1";

			dgv.Columns["Frags"].DefaultCellStyle.Format = format;
			dgv.Columns["Spot"].DefaultCellStyle.Format = format;
			dgv.Columns["Cap"].DefaultCellStyle.Format = format;
			dgv.Columns["Decap"].DefaultCellStyle.Format = format;

			dgv.Columns["Shots"].DefaultCellStyle.Format = format;
			dgv.Columns["Hits"].DefaultCellStyle.Format = format;
			dgv.Columns["Pierced Hits"].DefaultCellStyle.Format = format;
			// Show
			dgv.Visible = true;

		}

		#endregion

		#region GridScrollbar

		bool scrollingY = false;
		private async void scroll_MouseDown(object sender, MouseEventArgs e)
		{
			scrollingY = true;
            await ScrollY();
		}

		private void scroll_MouseUp(object sender, MouseEventArgs e)
		{
			scrollingY = false;
		}

		private async void scroll_MouseMove(object sender, MouseEventArgs e)
		{
			if (scrollingY) await ScrollY();
		}

		private async Task ScrollY()
		{
			try
			{
				int posBefore = dgvTeam1.FirstDisplayedScrollingRowIndex;
				dgvTeam1.FirstDisplayedScrollingRowIndex = scroll.ScrollPosition;
			}
			catch (Exception ex)
			{
				await Log.LogToFile(ex);
				// throw;
			}
		}

		private void dataGridMain_SelectionChanged(object sender, EventArgs e)
		{
			MoveScrollBar(); // Move scrollbar according to grid movements
		}

		private void RefreshScrollbars(DataGridView dgv)
		{
			// Scroll init
			scroll.ScrollElementsVisible = dgv.DisplayedRowCount(false);
			scroll.ScrollElementsTotals = dgv.RowCount; // subtract one for frozen row
			scroll.Visible = scroll.ScrollNecessary;
		}

		private void MoveScrollBar()
		{
			try
			{
				scroll.ScrollPosition = dgvTeam1.FirstDisplayedScrollingRowIndex; 
			}
			catch (Exception)
			{
				// ignore errors, only affect scrollbar position
			}
		}

		#endregion

		#region resize

		private void BattleSummary_Resize(object sender, EventArgs e)
		{
			ResizeNow();
		}

		private void ResizeNow()
		{
			if (btnOurTeam.Checked || btnEnemyTeam.Checked)
			{
				RefreshScrollbars(dgvTeam1);
				PlaceControl(dgvTeam1, GridLocation.Whole);
				FormatDataGrid(dgvTeam1);
				PlaceScroll(dgvTeam1);
			}
		}

		private enum GridLocation
		{
			Left = 1,
			Right = 2,
			Both = 3,
			Whole = 4,
		}

		private void PlaceControl(Control ctrl, GridLocation location)
		{
			switch (location)
			{
				case GridLocation.Whole:
					int w = grpMain.Width - 2;
					if (scroll.ScrollNecessary) w -= 17;
					ctrl.Width = w; 
					ctrl.Height = grpMain.Height - 9; // group height - top padding and borders
					break;
				default:
					break;
			}
		}

		private void PlaceScroll(Control grid)
		{
			scroll.Left = grid.Left + grid.Width;
			scroll.Height = grid.Height;
		}

		#endregion

		

	}
}
