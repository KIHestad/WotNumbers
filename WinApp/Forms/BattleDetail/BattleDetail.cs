using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;
using WinApp.Code.FormLayout;

namespace WinApp.Forms
{
	public partial class BattleDetail : Form
	{
		#region Init
		
		private int battleId;
		private Form parentForm;
		private int team1 = 1;
		private int team2 = 2;
		private DataGridView dgvTeam1 = new DataGridView();
		private DataGridView dgvTeam2 = new DataGridView();
		private bool showFortResources = false;
		private bool showAllColumns = false;
		private Panel pnlBack = new Panel();
		private BadScrollBar scroll = new BadScrollBar();
		private BattleMode.TypeEnum mainBattleMode;
		private int tankId = 0;
		private int wn8 = 0;
		private int wn8avg = 0;
		private int wn7 = 0;
		private int wn7avg = 0;
		private int eff = 0;
		private int effavg = 0;
		// Map and Comment
		private bool fetchedMapAndComment = false;
		private Control controlBattleReview = null;
        
		public BattleDetail(int selectedBattleId, Form selectedParentForm)
		{
			InitializeComponent();
			battleId = selectedBattleId;
			parentForm = selectedParentForm;
		}

        private void BattleDetail_Shown(object sender, EventArgs e)
        {
            // Get vbAddict players
            ExternalPlayerProfile.GetvBAddictPlayers(battleId);
        }

		private void BattleDetail_Load(object sender, EventArgs e)
		{
            // BattleResult
			panelBattleReview.Visible = false;
			panelBattleReview.Top = panelMyResult.Top;
			panelBattleReview.Left = panelMyResult.Left;
			panelBattleReview.Height = panelMyResult.Height;
			panelBattleReview.Width = panelMyResult.Width;
			panelBattleReview.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
			// My result
			GetMyPersonalInfo();
			// Teams
			string sql = "select id from battlePlayer where battleId=@battleId";
			DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
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
				dgvTeam1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
				dgvTeam2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
				dgvTeam1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				dgvTeam2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				dgvTeam1.ColumnHeadersHeight = 42;
				dgvTeam2.ColumnHeadersHeight = 42;
				dgvTeam1.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvCellFormatting);
				dgvTeam2.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvCellFormatting);
				dgvTeam1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(dgvColumnHeaderMouseClick);
				dgvTeam2.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(dgvColumnHeaderMouseClick);
				dgvTeam1.ColumnWidthChanged += new DataGridViewColumnEventHandler(dgvColumnWidthChanged);
				dgvTeam2.ColumnWidthChanged += new DataGridViewColumnEventHandler(dgvColumnWidthChanged);
				this.Controls.Add(dgvTeam1);
				this.Controls.Add(dgvTeam2);
				dgvTeam1.RowTemplate.Height = 26;
				dgvTeam2.RowTemplate.Height = 26;
				// Add panel as background
				pnlBack.BackColor = ColorTheme.ControlSeparatorGroupBoxBorder;
				pnlBack.Visible = false;
				this.Controls.Add(pnlBack);
				// Add scrollbar
				scroll.ScrollOrientation = ScrollOrientation.HorizontalScroll;
				scroll.Name = "scroll";
				scroll.Height = 17;
				scroll.Left = grpMain.Left + 1;
				scroll.Visible = false;
				this.Controls.Add(scroll);
				// Add scrollbar events
				scroll.MouseDown += new MouseEventHandler(scroll_MouseDown);
				scroll.MouseUp += new MouseEventHandler(scroll_MouseUp);
				scroll.MouseMove += new MouseEventHandler(scroll_MouseMove);
				// Find team 1 and 2
				sql = "select team from battlePlayer where battleId=@battleId and name=@name";
				DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
				DB.AddWithValue(ref sql, "@name", Config.Settings.playerName, DB.SqlDataType.VarChar);
				DataTable dt = DB.FetchData(sql);
				if (dt.Rows.Count > 0)
				{
					team1 = Convert.ToInt32(dt.Rows[0][0]);
					team2 = 1;
					if (team1 == 1) team2 = 2;
				}
                // Add right click menu to grid
                dgvTeam1.CellMouseDown += new DataGridViewCellMouseEventHandler(DgvTeam_CellMouseDown);
                dgvTeam2.CellMouseDown += new DataGridViewCellMouseEventHandler(DgvTeam_CellMouseDown);
                CreateDataGridContextMenu();
			}
		}

		private void btnTab_Click(object sender, EventArgs e)
		{
			// deselect tabs
			btnEnemyTeam.Checked = false;
			btnOurTeam.Checked = false;
			btnPersonal.Checked = false;
			btnTeams.Checked = false;
			btnBattleReview.Checked = false;
			// hide my result
			panelMyResult.Visible = false;
			// hide map and comment
			panelBattleReview.Visible = false;
			// hide grids
			dgvTeam1.Visible = false;
			dgvTeam2.Visible = false;
			// hide grid background
			pnlBack.Visible = false;
			// hide scroll
			scroll.Visible = false;
			// select tab
			BadButton btn = (BadButton)sender;
			btn.Checked = true;
			string selectedTab = btn.Name;
			switch (selectedTab)
			{
				case "btnPersonal":
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
				case "btnTeams":
					ShowTeams();
					break;
				case "btnOurTeam":
					ShowOwnTeam();
					break;
				case "btnEnemyTeam":
					ShowEnemyTeam();
					break;
				case "btnBattleReview":
					ShowBattleReview();
					break;
			}
		}

		#endregion

        #region Grid Right Click

        private void CreateDataGridContextMenu()
        {
            dgvTeam1.ContextMenuStrip = ExternalPlayerProfile.MenuItems();
            dgvTeam2.ContextMenuStrip = ExternalPlayerProfile.MenuItems();
        }

		private void DgvTeam_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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
				Log.LogToFile(ex);
				if (Config.Settings.showDBErrors)
					MsgBox.Show("Error on grid mouse down event, see log file", "Grid error", this);
			}
		}

        #endregion

		#region My Result

		private void GetMyPersonalInfo()
		{
			string sql =
				"SELECT battle.*, tank.id as tankId, tank.name as tankName, map.name as mapName, map.arena_id as arena_id, " +
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
			DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				DataRow dr = dt.Rows[0];
				// Battle result
				string battleResult = dr["battleResultName"].ToString();
				
				Color battleResultColor = ColorTranslator.FromHtml(dr["battleResultColor"].ToString());
				lblResult.ForeColor = battleResultColor;
				if (dr["enemyClanAbbrev"] != DBNull.Value)
				{
					switch (battleResult)
					{
						case "Victory": battleResult += " over " + dr["enemyClanAbbrev"].ToString(); break;
						case "Draw": battleResult += " against " + dr["enemyClanAbbrev"].ToString(); break;
						case "Defeat": battleResult = "Defeated by " + dr["enemyClanAbbrev"].ToString(); break;
					}
				}	
				lblResult.Text = battleResult;
				// Tank name
				lblTankName.Text = dr["tankName"].ToString();
				// Mastery Badge Image
				int masteryBadge = 0;
				if (dr["markOfMastery"] != DBNull.Value) masteryBadge = Convert.ToInt32(dr["markOfMastery"]);
				picMB.Image = ImageHelper.GetMasteryBadgeImage(masteryBadge, false);
				// Header picture /tank & Map), and map info
				Image imgHeader = new Bitmap(picHeader.Width, picHeader.Height);
				tankId = Convert.ToInt32(dr["tankId"]);
				picHeader.Image = ImageHelper.GetTankImage(tankId, ImageHelper.TankImageType.LargeImage);
				string mapName = "";
				if (dr["mapName"] != DBNull.Value)
					mapName = dr["mapName"].ToString();
				else
					btnBattleReview.Text = "Comment";
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
				Color battleSurviveColor = ColorTranslator.FromHtml(dr["battleSurviveColor"].ToString());
				switch (survival)
				{
					case "Yes":
						survival = "Survived"; 
						if (battleCount > 1)
							survival = "Battles: " + battleCount.ToString() + " - Survived all"; 
						break;
					case "Some": 
						survival = "Battles: " + battleCount.ToString() + " - Survived: " + survivedCount.ToString(); 
						break;
					case "No":
						survival = "Destroyed";
						if (battleCount > 1)
							survival = "Battles: " + battleCount.ToString() + " - Destroyed in all"; 
						string deathReason = "Destroyed";
						if (dr["deathReason"] != DBNull.Value) 
							deathReason = dr["deathReason"].ToString();
						string killedPrefix = " caused by ";
						switch (deathReason)
						{
							case "Shot": survival = "Killed by a shot"; killedPrefix=" from"; break;
							case "Burned": survival = "Put on fire"; killedPrefix = " by"; break;
							case "Rammed": survival = "Rammed"; killedPrefix=" by"; break;
							case "Chrashed": survival = "Vehicle crashed"; break;
							case "Death zone": survival = "Destroyed in a death zone"; break;
							case "Drowned": survival = "Vehicle drowned"; break;
						}
						if (dr["killedByPlayerName"] != DBNull.Value)
							survival += killedPrefix + " " + dr["killedByPlayerName"].ToString();
						break;
					
				}
				lblSurvival.Text = survival;
				lblSurvival.ForeColor = battleSurviveColor;
				// Main Battle Mode
				string mainMode = dr["battleMode"].ToString();
				string battleMode = "";
				switch (mainMode)
				{
					case "15":
                        mainBattleMode = BattleMode.TypeEnum.ModeRandom_TC;
						battleMode = "Random Battle"; 
						break;
					case "7":
                        mainBattleMode = BattleMode.TypeEnum.ModeTeam;
						battleMode = "Team: Unranked Battle";
						break;
					case "7Ranked":
                        mainBattleMode = BattleMode.TypeEnum.ModeTeamRanked;
						battleMode = "Team: Ranked Battle";
						break;
					case "Historical":
                        mainBattleMode = BattleMode.TypeEnum.ModeHistorical;
						battleMode = "Historical Battle";
						break;
					case "Skirmishes":
                        mainBattleMode = BattleMode.TypeEnum.ModeSkirmishes;
						battleMode = "Skirmish Battle";
						showFortResources = true; 
						break;
					case "Stronghold":
                        mainBattleMode = BattleMode.TypeEnum.ModeStronghold;
						battleMode = "Battle for Stronghold";
						showFortResources = true;
						break;
					case "Special":
                        mainBattleMode = BattleMode.TypeEnum.ModeSpecial;
						battleMode = "Special Event Battle";
						showFortResources = false; 
						break;
					case "GlobalMap":
                        mainBattleMode = BattleMode.TypeEnum.ModeGlobalMap;
						battleMode = "Global Map";
						showFortResources = false;
						break;
				}
				// Battle mode text
				if (dr["battleResultMode"] != DBNull.Value)
					battleMode = dr["battleResultMode"].ToString() + " Battle";
				else
				{
					if (Convert.ToInt32(dr["modeClan"]) > 0)
						battleMode = "Clan War Battle";
					else if (Convert.ToInt32(dr["modeCompany"]) > 0)
						battleMode = "Tank Company Battle";
				}
				// get WN8 to use as total in wn8 detail grid
				wn8 = Convert.ToInt32(dr["wn8"]);
				wn7 = Convert.ToInt32(dr["wn7"]);
				eff = Convert.ToInt32(dr["eff"]);
				lblBattleMode.Text = battleMode;
				GetWN8Details();
				GetStandardGridDetails();
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
				"FROM battle INNER JOIN playerTank ON battle.playerTankId = playerTank.id INNER JOIN tank ON playerTank.tankId = tank.id " +
				"WHERE battle.id = @battleId";
			DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				DataRow dr = dt.Rows[0];
				//int tankId = Convert.ToInt32(dr["tankId"]);
				//int battlesCount = Convert.ToInt32(dr["battlesCount"]);
                Rating.RatingParameters rp = new Rating.RatingParameters();
				rp.DAMAGE = Convert.ToDouble(dr["dmg"]);
				rp.SPOT = Convert.ToDouble(dr["spotted"]);
				rp.FRAGS = Convert.ToDouble(dr["frags"]);
				rp.DEF = Convert.ToDouble(dr["def"]);
				double exp_dmg = DbConvert.ToDouble(dr["expDmg"]);
				double exp_spotted = DbConvert.ToDouble(dr["expSpot"]);
				double exp_frags = DbConvert.ToDouble(dr["expFrags"]);
				double exp_def = DbConvert.ToDouble(dr["expDef"]);
				double exp_wr = DbConvert.ToDouble(dr["expWR"]);
				//string wn8 = Math.Round(Rating.CalculateTankWN8(tankId, battlesCount, dmg, spotted, frags, def, 0, true), 0).ToString();
				double rWINc;
				double rDAMAGEc;
				double rFRAGSc;
				double rSPOTc;
				double rDEFc;
				Rating.WN8useFormulaReturnResult(
					rp, exp_wr,
					exp_dmg, exp_spotted, exp_frags, exp_def, exp_wr,
					out rWINc, out rDAMAGEc, out rFRAGSc, out rSPOTc, out rDEFc);
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
				drWN8["Exp"] = exp_dmg.ToString("N1");
				drWN8["Value"] = Math.Round(rDAMAGEc, 0).ToString("N0");
				dtWN8.Rows.Add(drWN8);
				// Frags
				drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "Frags";
				drWN8["Image"] = GetIndicator(rp.FRAGS, exp_frags);
				drWN8["Result"] = rp.FRAGS.ToString("N0");
				drWN8["Exp"] = exp_frags.ToString("N1");
				drWN8["Value"] = Math.Round(rFRAGSc, 0).ToString("N0");
				dtWN8.Rows.Add(drWN8);
				// Spot
				drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "Spot";
				drWN8["Image"] = GetIndicator(rp.SPOT, exp_spotted);
				drWN8["Result"] = rp.SPOT.ToString("N0");
				drWN8["Exp"] = exp_spotted.ToString("N1");
				drWN8["Value"] = Math.Round(rSPOTc, 0).ToString("N0");
				dtWN8.Rows.Add(drWN8);
				// Defence
				drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "Defence";
				drWN8["Image"] = GetIndicator(rp.DEF, exp_def);
				drWN8["Result"] = rp.DEF.ToString("N0");
				drWN8["Exp"] = exp_def.ToString("N1");
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
				drWN8["Image"] = new Bitmap(1,1);
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

		private void dgvWN8_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			// rating color
            dgvWN8.Rows[5].Cells["Value"].Style.ForeColor = ColorRangeScheme.WN8color(wn8);
		}

		private void GetStandardGridDetails()
		{
			// This battle result
			string sql =
				"SELECT battlesCount, dmg, assistSpot, assistTrack, dmgBlocked, potentialDmgReceived, dmgReceived,  " +
				"  shots, hits, pierced, heHits, piercedReceived, shotsReceived, heHitsReceived, noDmgShotsReceived, " +
				"  frags, spotted as spot, cap, def, arenaUniqueID, mileage, treesCut, eff, wn7, wn8, " +
				"  credits, creditsPenalty, creditsContributionIn, creditsContributionOut, creditsToDraw, autoRepairCost, autoLoadCost, autoEquipCost, " +
				"    eventCredits , originalCredits, creditsNet, achievementCredits, premiumCreditsFactor10, dailyXPFactorTxt, " +
				"  real_xp, (xpPenalty * -1) as xpPenalty, freeXP, dailyXPFactor10, premiumXPFactor10, eventXP, eventFreeXP, eventTMenXP, achievementXP, achievementFreeXP " +
				"FROM battle " +
				"WHERE id = @battleId";
			DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
			DataTable dtRes = DB.FetchData(sql);
			// Tank total result
			sql =
				"SELECT battles, dmg, dmgReceived, assistSpot, assistTrack, dmgBlocked, potentialDmgReceived, " +
				"  shots, hits, pierced, heHits, piercedReceived, shotsReceived, heHitsReceived, noDmgShotsReceived, " +
				"  frags, spot, cap, def, mileage, treesCut, eff, wn7, wn8 " +
				"FROM playerTank INNER JOIN playerTankBattle ON playerTank.id = playerTankBattle.playerTankId " +
				"WHERE playerTank.tankId = @tankId and playerTankBattle.battleMode=@battleMode and playerTank.playerId=@playerId ";
			DB.AddWithValue(ref sql, "@tankId", tankId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
            DB.AddWithValue(ref sql, "@battleMode", BattleMode.GetItemFromType(mainBattleMode).SqlName, DB.SqlDataType.VarChar);
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
					dmgDoneReceived = Math.Round(dmgDR,1).ToString("# ### ##0.0");
				}
				else
					getDmgDRImage = false;
				string avgDmgDoneReceived = "INF";
				double avgDmgDR = Convert.ToDouble(drAvg["dmgReceived"]);
				if (avgDmgDR > 0) 
				{
					avgDmgDR = (Convert.ToDouble(drAvg["dmg"]) / avgDmgDR);
					avgDmgDoneReceived = Math.Round(avgDmgDR,1).ToString("# ### ##0.0");
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

				// Add rows to Ratings grid
				DataTable dtRating = dt.Clone();
				dtRating.Rows.Add(GetValues(dtRating, drVal, drAvg, "WN8", "wn8", 1));
				dtRating.Rows.Add(GetValues(dtRating, drVal, drAvg, "WN7", "wn7", 1));
				dtRating.Rows.Add(GetValues(dtRating, drVal, drAvg, "EFF", "eff", 1));
				wn8avg = Convert.ToInt32(drAvg["wn8"]);
				wn7avg = Convert.ToInt32(drAvg["wn7"]);
				effavg = Convert.ToInt32(drAvg["eff"]);

				// Done;
				dtRating.AcceptChanges();
				dgvRating.DataSource = dtRating;
				FormatStandardDataGrid(dgvRating, "");


				//lblWN8.ForeColor = Rating.WN8color(wn8);
				//lblWN7.ForeColor = Rating.WN7color(wn7);
				//lblEFF.ForeColor = Rating.EffColor(eff);

				// Enhanced battle result
				if (drVal["arenaUniqueID"] == DBNull.Value)
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
					// All battle results for this tank
					sql =
						"SELECT SUM(battlesCount) as battles, SUM(credits) as credits, SUM(creditsNet) as creditsNet, " +
						"   SUM(autoRepairCost) as autoRepairCost, SUM(autoLoadCost) as autoLoadCost, SUM(autoEquipCost) as autoEquipCost, SUM(creditsContributionOut) as creditsContributionOut, " +
						"  SUM(real_xp) as real_xp, SUM(xpPenalty) * -1 as xpPenalty, SUM(freeXP) as freeXP, SUM(eventXP) as eventXP, SUM(eventFreeXP) as eventFreeXP, SUM(eventTMenXP) as eventTMenXP, " +
						"  SUM(achievementXP) as achievementXP, SUM(achievementFreeXP) as achievementFreeXP " +
						"FROM battle INNER JOIN playerTank ON battle.playerTankId = playerTank.Id " +
						"WHERE arenaUniqueID is not null AND playerTank.tankId = @tankId and battle.battleMode=@battleMode and playerTank.playerId=@playerId ";
					DB.AddWithValue(ref sql, "@tankId", tankId, DB.SqlDataType.Int);
					DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
                    DB.AddWithValue(ref sql, "@battleMode", BattleMode.GetItemFromType(mainBattleMode).SqlName, DB.SqlDataType.VarChar);
					DataTable dtTotBattle = DB.FetchData(sql);
					DataRow drTotBattle = dtTotBattle.Rows[0];
					double totBattleCount = Convert.ToInt32(drTotBattle["battles"]);
				
					// Add Rows to credit grid
					DataTable dtCredit = dt.Clone();
					dtCredit.Rows.Add(GetValues(dtCredit, drVal, drTotBattle, "Income", "credits", Convert.ToInt32(totBattleCount)));
					//dtCredit.Rows.Add(GetValues(dtCredit, drVal, drVal, "   for Mission", "eventCredits", avgBattleCount));
					//dtCredit.Rows.Add(GetValues(dtCredit, drVal, drVal, "   for Achievement", "achievementCredits", avgBattleCount));
					//dtCredit.Rows.Add(GetValues(dtCredit, drVal, drVal, "   Dmg Compensation", "creditsToDraw", avgBattleCount));
					//dtCredit.Rows.Add(GetValues(dtCredit, drVal, drVal, "creditsContributionIn", "creditsContributionIn", avgBattleCount));
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
					//dtCredit.Rows.Add(GetValues(dtCredit, drVal, drVal, "   for Repair", "autoRepairCost", avgBattleCount));
					//dtCredit.Rows.Add(GetValues(dtCredit, drVal, drVal, "   for Ammo", "autoLoadCost", avgBattleCount));
					//dtCredit.Rows.Add(GetValues(dtCredit, drVal, drVal, "   for Consumables", "autoLoadCost", avgBattleCount));
					//dtCredit.Rows.Add(GetValues(dtCredit, drVal, drVal, "   Dmg Compensation", "autoEquipCost", avgBattleCount));
					//dtCredit.Rows.Add(GetValues(dtCredit, drVal, drVal, "   creditsContributionOut", "creditsContributionOut", avgBattleCount));
					dtCredit.Rows.Add(GetValues(dtCredit, drVal, drTotBattle, "Result", "creditsNet", Convert.ToInt32(totBattleCount)));
					//dtCredit.Rows.Add(GetValues(dtCredit, drVal, drVal, "originalCredits", "originalCredits", avgBattleCount));
					//dtCredit.Rows.Add(GetValues(dtCredit, drVal, drVal, "premiumCreditsFactor10", "premiumCreditsFactor10", avgBattleCount));
					// Done;
					dtCredit.AcceptChanges();
					dgvCredit.DataSource = dtCredit;
					FormatStandardDataGrid(dgvCredit, "");

					// Show multiplier
					string multiplier = drVal["dailyXPFactorTxt"].ToString().Replace(" ", "");
					if (multiplier != "1X")
						lblXP.Text = "XP (" + multiplier + ")";
					// Add Rows to XP grid
					DataTable dtXP = dt.Clone();
					dtXP.Rows.Add(GetValues(dtXP, drVal, drTotBattle, "Total XP", "real_xp", Convert.ToInt32(totBattleCount)));
					dtXP.Rows.Add(GetValues(dtXP, drVal, drTotBattle, "    Due to mission", "eventXP", Convert.ToInt32(totBattleCount)));
					dtXP.Rows.Add(GetValues(dtXP, drVal, drTotBattle, "    Fine for causing dmg", "xpPenalty", Convert.ToInt32(totBattleCount)));
					dtXP.Rows.Add(GetValues(dtXP, drVal, drTotBattle, "Free XP", "freeXP", Convert.ToInt32(totBattleCount)));
					//dtXP.Rows.Add(GetValues(dtXP, drVal, drTotBattle, "    Due to mission", "eventFreeXP", Convert.ToInt32(totBattleCount)));
					// Done;
					dtXP.AcceptChanges();
					dgvXP.DataSource = dtXP;
					FormatStandardDataGrid(dgvXP, "");
				}
				
			}
		}

		private void dgvRating_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			// rating color
            dgvRating.Rows[0].Cells["Result"].Style.ForeColor = ColorRangeScheme.WN8color(wn8);
            dgvRating.Rows[0].Cells["Average"].Style.ForeColor = ColorRangeScheme.WN8color(wn8avg);
            dgvRating.Rows[1].Cells["Result"].Style.ForeColor = ColorRangeScheme.WN7color(wn7);
            dgvRating.Rows[1].Cells["Average"].Style.ForeColor = ColorRangeScheme.WN7color(wn7avg);
            dgvRating.Rows[2].Cells["Result"].Style.ForeColor = ColorRangeScheme.EffColor(eff);
            dgvRating.Rows[2].Cells["Average"].Style.ForeColor = ColorRangeScheme.EffColor(effavg);
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

		#endregion

		#region Teams

		private void ShowTeams()
		{
			showAllColumns = false;
			dgvTeam1.DataSource = GetDataGridSource(team1);
			dgvTeam2.DataSource = GetDataGridSource(team2);
			ResizeNow();
			FormatDataGrid(dgvTeam1);
			FormatDataGrid(dgvTeam2);
			pnlBack.BringToFront();
			dgvTeam1.BringToFront();
			dgvTeam2.BringToFront();
			pnlBack.Visible = true;
		}

		private void ShowOwnTeam()
		{
			showAllColumns = true;
			dgvTeam1.DataSource = GetDataGridSource(team1);
			ResizeNow();
			scroll.ScrollPosition = 0;
			FormatDataGrid(dgvTeam1);
			scroll.BringToFront();
			dgvTeam1.BringToFront();
			RefreshScrollbars(dgvTeam1);
			MoveScrollBar();
		}

		private void ShowEnemyTeam()
		{
			showAllColumns = true;
			dgvTeam2.DataSource = GetDataGridSource(team2);
			ResizeNow();
			scroll.ScrollPosition = 0;
			FormatDataGrid(dgvTeam2);
			scroll.BringToFront();
			dgvTeam2.BringToFront();
			RefreshScrollbars(dgvTeam2);
			MoveScrollBar();
		}

		private void ShowBattleReview()
		{
			if (!fetchedMapAndComment)
			{
				controlBattleReview = new Forms.BattleReview(battleId, parentForm);
				controlBattleReview.Name = "controlBattleReview";
				panelBattleReview.Controls.Add(controlBattleReview);
				controlBattleReview.Dock = DockStyle.Fill;
				Control[] c = panelBattleReview.Controls.Find("controlBattleReview", false);
				c[0].BringToFront();
			}
			panelBattleReview.Visible = true;
			panelBattleReview.BringToFront();
		}

		private string GetSortField(string headerText)
		{
			string sortField = "";
			switch (headerText)
			{
				case "IR": sortField = "fortResource"; break;
				case "Player": sortField = "battlePlayer.name"; break;
				case "Clan": sortField = "clanAbbrev"; break;
				case "Tank": sortField = "tank.name"; break;
				case "Dmg": sortField = "damageDealt"; break;
				case "Frags": sortField = "kills"; break;
				case "XP": sortField = "xp"; break;
				case "Killed By": sortField = "killerName"; break;
				case "Dmg Track": sortField = "damageAssistedTrack"; break;
				case "Dmg Spot": sortField = "damageAssistedRadio"; break;
				case "Dmg Sniper": sortField = "sniperDamageDealt"; break;
				case "Dmg Received": sortField = "damageReceived"; break;
				case "Dmg Blocked": sortField = "damageBlockedByArmor"; break;
				case "Spot": sortField = "spotted"; break;
				case "Cap": sortField = "capturePoints"; break;
				case "Decap": sortField = "droppedCapturePoints"; break;
				case "Shots": sortField = "shots"; break;
				case "Hits": sortField = "hits"; break;
				case "Pierced Hits": sortField = "pierced"; break;
				case "Explosion Hits": sortField = "explosionHits"; break;
				case "Hits Received": sortField = "directHitsReceived"; break;
				case "Piercings Received": sortField = "piercingsReceived"; break;
				case "Expl Hits Received": sortField = "explosionHitsReceived"; break;
				case "No Dmg Hits Received": sortField = "noDamageShotsReceived"; break;
				case "Milage": sortField = "mileage"; break;
				case "Life Time": sortField = "lifeTime"; break;
				case "Base Credit": sortField = "credits"; break;
				case "Premature Leave": sortField = "isPrematureLeave"; break;
				case "Team Killer": sortField = "isTeamKiller"; break;
				case "Team Kills": sortField = "tkills"; break;

			}
			return sortField;
		}

		private DataTable GetDataGridSource(int team, string orderHeaderText = "XP", bool orderAscending = false)
		{
			string fortResourcesFields = "";
			if (showFortResources) fortResourcesFields = ", fortResource as 'IR' ";
			string enhancedFields = "";
			string orderBy = GetSortField(orderHeaderText);
			if (orderBy != "")
			{
				orderBy = " ORDER BY " + orderBy;
				if (!orderAscending)
					orderBy += " DESC ";
			}
			if (showAllColumns)
				enhancedFields =
					", '' as separator1 " +
					", killerName as 'Killed By' " +

					", '' as separator2 " +
					", damageAssistedTrack as 'Dmg Track' " +
					", damageAssistedRadio as 'Dmg Spot' " +
					", sniperDamageDealt as 'Dmg Sniper' " +

					", '' as separator8 " +
					", damageReceived as 'Dmg Received' " +
					", damageBlockedByArmor as 'Dmg Blocked' " +

					", '' as separator3 " +
					", spotted as 'Spot' " +
					", capturePoints as 'Cap' " +
					", droppedCapturePoints as 'Decap' " +

					", '' as separator4 " +
					", shots as 'Shots' " +
					", hits as 'Hits' " +
					", pierced as 'Pierced Hits' " +
					", explosionHits as 'Explosion Hits' " +

					", '' as separator5 " +
					", directHitsReceived as 'Hits Received' " +
					", piercingsReceived as 'Piercings Received' " +
					", explosionHitsReceived as 'Expl Hits Received' " +
					", noDamageShotsReceived as 'No Dmg Hits Received' " +

					", '' as separator6 " + 
					", mileage as 'Milage' " +
					", lifeTime as 'Life Time' " +

					", '' as separator7 " + 
					", credits as 'Base Credit' " +

					", '' as separator9 " + 
					", isPrematureLeave as 'Premature Leave' " +
					", isTeamKiller as 'Team Killer' " +
					", tkills as 'Team Kills' ";

			string sql =
                "select battlePlayer.accountId, '' as 'vBAddict', battlePlayer.name as 'Player', clanAbbrev as Clan, tank.name as 'Tank', damageDealt as 'Dmg', kills as 'Frags', xp as 'XP' " +
				fortResourcesFields +
				enhancedFields +
				", deathReason as 'Dead', tank.id as 'TankId', " + team + " as 'Team' " +
				"from battlePlayer inner join " +
				"     tank on battlePlayer.tankId = tank.id " +
				"where battleId=@battleId and team=@team " +
				orderBy;
			DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@team", team, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			// Add image as first col
			dt.Columns.Add("TankImage", typeof(Image)).SetOrdinal(3);
			// Add Total Row
			DataRow totalRow = dt.NewRow();
			// Set 0 ad deafult
			foreach (DataColumn dc in dt.Columns)
				if (dc.DataType == System.Type.GetType("System.Int32") || dc.DataType == System.Type.GetType("System.Int64"))
					totalRow[dc.ColumnName] = 0;
			totalRow["Player"] = "Total";
			Bitmap blankImg = new Bitmap(1, 1);
			totalRow["TankImage"] = (Image)blankImg;
			dt.Rows.Add(totalRow);
			int totRow = dt.Rows.Count -1;
			// Add images and calc total and more
			for (int i = 0; i < dt.Rows.Count - 1; i++)
			{
                DataRow dr = dt.Rows[i];
                // Tank img
				int tankId = Convert.ToInt32(dr["TankId"]);
				Image img = ImageHelper.GetTankImage(tankId, ImageHelper.TankImageType.SmallImage);
				Bitmap newImage = new Bitmap(92,24);
				using (Graphics gr = Graphics.FromImage((Image)newImage))
				{
					//gr.SmoothingMode = SmoothingMode.HighQuality;
					gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
					//gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
					gr.DrawImage(img, 0, 0, 92, 24);
				}
				dr["TankImage"] = (Image)newImage;
                // vbAddict
                if (ExternalPlayerProfile.vBAddictPlayers.Contains(dr["accountId"].ToString()))
                    dr["vBAddict"] = "*";
                // Totals
				foreach (DataColumn dc in dt.Columns)
				{
					if (dc.DataType == System.Type.GetType("System.Int32") || dc.DataType == System.Type.GetType("System.Int64"))
					{
						if (dc.ColumnName != "Dead" && dr[dc.ColumnName] != DBNull.Value && Convert.ToInt32(dr[dc.ColumnName]) == 0) 
							dr[dc.ColumnName] = DBNull.Value;
						else
						{
							int total = Convert.ToInt32(dt.Rows[totRow][dc.ColumnName]);
							int addvalue = 0;
							if (dr[dc.ColumnName] != DBNull.Value) addvalue = Convert.ToInt32(dr[dc.ColumnName]);
							dt.Rows[totRow][dc.ColumnName] = total + addvalue;
						}
					}
				}
			}
			// Change total row type
			dt.Rows[totRow]["Dead"] = -99; // Indicates total row, dark background
			dt.AcceptChanges();
			return dt;
		}

        
		private void FormatDataGrid(DataGridView dgv)
		{
			// Deselect
			dgv.ClearSelection();
			// Freeze first column
			dgv.Columns[0].Frozen = true;
			// Hide
            dgv.Columns["AccountId"].Visible = false;
			dgv.Columns["TankId"].Visible = false;
			dgv.Columns["Dead"].Visible = false;
			dgv.Columns["Team"].Visible = false;
			dgv.Columns["TankImage"].HeaderText = "";
            dgv.Columns["vBAddict"].HeaderText = "";
			// Left align text col
			dgv.Columns["Tank"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
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
			// col fixed width
			dgv.Columns["TankImage"].Width = 60;
			dgv.Columns["TankImage"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns["vBAddict"].Width = 12;
			// Calc width for rest of fields
			if (!showAllColumns)
			{
				// left part = player, clan, tank img, tank
				int w = dgv.Width - 2; // total available width without img col
				int left = Convert.ToInt32(w * 0.6);
				int leftFreeArea = left - 60 - 12 - 50;
				dgv.Columns["Player"].Width = leftFreeArea / 2;
				dgv.Columns["Tank"].Width = leftFreeArea - (leftFreeArea / 2);
				// right part = dmg, frags, xp, IR
				int right = w - left;
				int rightCols = 3;
				if (showFortResources) rightCols = 4;
				int i = right / rightCols;
				dgv.Columns["Dmg"].Width = i + 5;
				dgv.Columns["Frags"].Width = i - 5;
				if (showFortResources) dgv.Columns["IR"].Width = i;
				dgv.Columns["XP"].Width = right - (i * (rightCols - 1));
			}
			else
			{
				dgv.Columns["Player"].Width = 100;
				dgv.Columns["Tank"].Width = 100;
				dgv.Columns["Killed By"].Width = 80;
				dgv.Columns["Killed By"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
				dgv.Columns["Base Credit"].Width = 65;
				dgv.Columns["Dmg Received"].Width = 65;
				dgv.Columns["Base Credit"].DefaultCellStyle.Format = "N0";
				dgv.Columns["Dmg Received"].DefaultCellStyle.Format = "N0";
				dgv.Columns["Milage"].DefaultCellStyle.Format = "N0";
				dgv.Columns["Explosion Hits"].Width = 55;
				dgv.Columns["Expl Hits Received"].Width = 60;
				dgv.Columns["No Dmg Hits Received"].Width = 80;
			}
			// Show
			dgv.Visible = true;
			
		}

		private void dgvCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			DataGridView dgv = (DataGridView)sender;
			string col = dgv.Columns[e.ColumnIndex].Name;
			if (col.Equals("Player"))
			{
				int dead = Convert.ToInt32(dgv["Dead", e.RowIndex].Value);
				if (dead >= 0)
				{
					dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = ColorTheme.ControlDarkFont;
					if (dgv["Player", e.RowIndex].Value.ToString() == Config.Settings.playerName)
					{
						dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.GridRowCurrentPlayerDead;
						dgv.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorTheme.GridRowCurrentPlayerDeadeSelected;
					}
					else
					{
						dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.GridRowPlayerDead;
						dgv.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorTheme.GridRowPlayerDeadeSelected;
					}
				}
				else if (dead == -99)
					dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.GridTotalsRow;
				else if (dgv["Player", e.RowIndex].Value.ToString() == Config.Settings.playerName)
				{
					dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.GridRowCurrentPlayerAlive;
					dgv.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorTheme.GridRowCurrentPlayerAliveSelected;
				}
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
			int team = Convert.ToInt32(dgv.Rows[0].Cells["Team"].Value);
			dgv.DataSource = GetDataGridSource(team, columnName, sortDirection);
			if ((btnEnemyTeam.Checked || btnOurTeam.Checked) && scroll.ScrollPosition > 0)
			{
				Refresh();
				dgv.FirstDisplayedScrollingColumnIndex = scroll.ScrollPosition;
			}
			dgv.ClearSelection();			
		}

		private void dgvColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
		{
			if (scroll.ScrollPosition > 0 && (btnEnemyTeam.Checked || btnOurTeam.Checked))
			{
				DataGridView dgv = (DataGridView)sender;
				dgv.FirstDisplayedScrollingColumnIndex = scroll.ScrollPosition;
			}
		}

		#endregion

		#region GridScrollbar

		bool scrollingX = false;
		private void scroll_MouseDown(object sender, MouseEventArgs e)
		{
			scrollingX = true;
			ScrollX();
		}

		private void scroll_MouseUp(object sender, MouseEventArgs e)
		{
			scrollingX = false;
		}

		private void scroll_MouseMove(object sender, MouseEventArgs e)
		{
			if (scrollingX) ScrollX();
		}

		private void ScrollX()
		{
			try
			{
				DataGridView dgv = null;
				if (btnOurTeam.Checked)
					dgv = dgvTeam1;
				else if (btnEnemyTeam.Checked)
					dgv = dgvTeam2;
				if (dgv != null)
				{
					int posBefore = dgv.FirstDisplayedScrollingColumnIndex;
					dgv.FirstDisplayedScrollingColumnIndex = scroll.ScrollPosition + 1; // adjust for frozen row
					//if (posBefore != dgv.FirstDisplayedScrollingColumnIndex) Refresh();
				}
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				// throw;
			}
		}

		private void dataGridMain_SelectionChanged(object sender, EventArgs e)
		{
			MoveScrollBar(); // Move scrollbar according to grid movements
		}

		private void RefreshScrollbars(DataGridView dgv)
		{
			// Set scrollbar properties according to grid content
			int XVisible = 0;
			int XTotal = 0;
			// Calc scroll boundarys
			if (dgv.RowCount > 0)
			{
				foreach (DataGridViewColumn col in dgv.Columns)
				{
					if (col.Visible) XTotal++;
				}
				XVisible = dgv.DisplayedColumnCount(false);
			}
			// Scroll init
			scroll.ScrollElementsVisible = XVisible;
			scroll.ScrollElementsTotals = XTotal - 1; // subtract one for frozen row
			scroll.Visible = scroll.ScrollNecessary;
		}

		private void MoveScrollBar()
		{
			try
			{
				if (btnOurTeam.Checked)
					scroll.ScrollPosition = dgvTeam1.FirstDisplayedScrollingColumnIndex - 1; // adjust for frozen row
				else if (btnEnemyTeam.Checked)
					scroll.ScrollPosition = dgvTeam2.FirstDisplayedScrollingColumnIndex - 1; // adjust for frozen row
			}
			catch (Exception)
			{
				// ignore errors, only affect scrollbar position
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
				PlaceControl(pnlBack, GridLocation.Whole);
				PlaceControl(dgvTeam1, GridLocation.Left);
				PlaceControl(dgvTeam2, GridLocation.Right);
				FormatDataGrid(dgvTeam1);
				FormatDataGrid(dgvTeam2);
			}
			else if (btnOurTeam.Checked)
			{
				PlaceControl(dgvTeam1, GridLocation.Both);
				FormatDataGrid(dgvTeam1);
				RefreshScrollbars(dgvTeam1);
				PlaceScroll(dgvTeam1);
			}
			else if (btnEnemyTeam.Checked)
			{
				PlaceControl(dgvTeam2, GridLocation.Both);
				FormatDataGrid(dgvTeam2);
				RefreshScrollbars(dgvTeam2);
				PlaceScroll(dgvTeam2);
			}
			else if (btnBattleReview.Checked)
			{
				Control[] c = panelBattleReview.Controls.Find("controlBattleReview", false);
				c[0].Refresh();
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
				case GridLocation.Left:
					ctrl.Left = grpMain.Left + 1;
					ctrl.Top = grpMain.Top + 8;
					ctrl.Width = grpMain.Width / 2 - 3;
					ctrl.Height = grpMain.Height - 9;
					break;
				case GridLocation.Right:
					ctrl.Left = grpMain.Left + (grpMain.Width / 2) + 3;
					ctrl.Top = grpMain.Top + 8;
					ctrl.Width = grpMain.Width / 2 - 4;
					ctrl.Height = grpMain.Height - 9;
					break;
				case GridLocation.Both:
					ctrl.Left = grpMain.Left + 1;
					ctrl.Top = grpMain.Top + 8;
					ctrl.Width = grpMain.Width - 2;
					int scrollHeight = 0;
					if (scroll.ScrollNecessary)
						scrollHeight = scroll.Height;
					ctrl.Height = grpMain.Height - 9 - scrollHeight;
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

		private void PlaceScroll(Control grid)
		{
			scroll.Top = grid.Top + grid.Height;
			scroll.Width = grid.Width;
		}

		#endregion

	}
}
