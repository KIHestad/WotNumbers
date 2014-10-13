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

namespace WinApp.Forms
{
	public partial class BattleDetail : Form
	{
		#region Init
		
		private int battleId;
		private int team1 = 1;
		private int team2 = 2;
		private DataGridView dgvTeam1 = new DataGridView();
		private DataGridView dgvTeam2 = new DataGridView();
		private bool showFortResources = false;
		private bool showAllColumns = false;
		private Panel pnlBack = new Panel();
		private BadScrollBar scroll = new BadScrollBar();
		private BattleHelper.MainBattleMode mainBattleMode;
		private int tankId = 0;

		public BattleDetail(int selectedBattleId)
		{
			InitializeComponent();
			battleId = selectedBattleId;
		}

		private void BattleDetail_Load(object sender, EventArgs e)
		{
			// My result
			GridHelper.StyleDataGrid(dgvWN8, DataGridViewSelectionMode.CellSelect);
			dgvWN8.GridColor = ColorTheme.GridHeaderBackLight;
			dgvWN8.AllowUserToResizeColumns = false;
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
			}
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
					dgvWN8.ClearSelection();
					dgvDamage.ClearSelection();
					panel1.Visible = true;
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
				// Tank img
				tankId = Convert.ToInt32(dr["tankId"]);
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
						mainBattleMode = BattleHelper.MainBattleMode.ModeRandom_TC;
						battleMode = "Random Battle"; 
						break;
					case "7": 
						mainBattleMode = BattleHelper.MainBattleMode.ModeTeam;
						battleMode = "Team Battle";
						break;
					case "Historical": 
						mainBattleMode = BattleHelper.MainBattleMode.ModeHistorical;
						battleMode = "Historical Battle";
						break;
					case "Skirmishes": 
						mainBattleMode = BattleHelper.MainBattleMode.ModeSkirmishes;
						battleMode = "Skirmish Battle";
						showFortResources = true; 
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
				GetStandardGridDetails();
			}
		}

		private void GetWN8Details()
		{
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
				drWN8["Result"] = dmg.ToString();
				drWN8["Exp"] = exp_dmg.ToString();
				drWN8["Value"] = Math.Round(rDAMAGEc, 1).ToString();
				dtWN8.Rows.Add(drWN8);
				// Frags
				drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "Frags";
				drWN8["Image"] = GetIndicator(frags, exp_frags);
				drWN8["Result"] = frags.ToString();
				drWN8["Exp"] = exp_frags.ToString();
				drWN8["Value"] = Math.Round(rFRAGSc, 1).ToString();
				dtWN8.Rows.Add(drWN8);
				// Spot
				drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "Spot";
				drWN8["Image"] = GetIndicator(spotted, exp_spotted);
				drWN8["Result"] = spotted.ToString();
				drWN8["Exp"] = exp_spotted.ToString();
				drWN8["Value"] = Math.Round(rSPOTc, 1).ToString();
				dtWN8.Rows.Add(drWN8);
				// Defence
				drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "Defence";
				drWN8["Image"] = GetIndicator(def, exp_def);
				drWN8["Result"] = def.ToString();
				drWN8["Exp"] = exp_def.ToString();
				drWN8["Value"] = Math.Round(rDEFc, 1).ToString();
				dtWN8.Rows.Add(drWN8);
				// Win Rate
				drWN8 = dtWN8.NewRow();
				drWN8["Parameter"] = "Win Rate";
				drWN8["Image"] = imgIndicators.Images[1];
				drWN8["Result"] = "Fixed";
				drWN8["Exp"] = exp_wr.ToString() + "%";
				drWN8["Value"] = Math.Round(rWINc, 1).ToString();
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
				dgvWN8.Columns[2].Width = 40;
				dgvWN8.Columns[3].Width = 40;
				dgvWN8.Columns[4].Width = 50;
				dgvWN8.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
				dgvWN8.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				dgvWN8.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				dgvWN8.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				dgvWN8.Width = dgvWN8.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + 2;
				dgvWN8.Height = dgvWN8.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + dgvWN8.ColumnHeadersHeight + 2;
				dgvWN8.Columns[2].DefaultCellStyle.Format = "N0";
				dgvWN8.Columns[3].DefaultCellStyle.Format = "N0";
				dgvWN8.Columns[4].DefaultCellStyle.Format = "N1";
				foreach (DataGridViewColumn dgvc in dgvWN8.Columns)
				{
					dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;					
				}
				dgvWN8.Visible = true;
				dgvWN8.ClearSelection();
			}
		}

		private void GetStandardGridDetails()
		{
			string sql =
				"SELECT dmg, assistSpot, assistTrack, dmgBlocked, potentialDmgReceived, dmgReceived,  " +
				"  shots, hits, pierced, heHits, piercedReceived, shotsReceived, heHitsReceived, noDmgShotsReceived, " +
				"  frags, spotted as spot, cap, def " +
				"FROM battle " +
				"WHERE id = @battleId";
			DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
			DataTable dtRes = DB.FetchData(sql);
			sql =
				"SELECT battles, dmg, dmgReceived, assistSpot, assistTrack, dmgBlocked, potentialDmgReceived, " +
				"  shots, hits, pierced, heHits, piercedReceived, shotsReceived, heHitsReceived, noDmgShotsReceived, " +
				"  frags, spot, cap, def " +
				"FROM playerTank INNER JOIN playerTankBattle ON playerTank.id = playerTankBattle.playerTankId " +
				"WHERE playerTank.tankId = @tankId and playerTankBattle.battleMode=@battleMode";
			DB.AddWithValue(ref sql, "@tankId", tankId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battleMode", BattleHelper.GetSQLMainBattleMode(mainBattleMode), DB.SqlDataType.VarChar);
			DataTable dtAvg = DB.FetchData(sql);
			if (dtRes.Rows.Count > 0)
			{
				DataRow drVal = dtRes.Rows[0];
				DataRow drAvg = null;
				bool avgOK = (dtAvg.Rows.Count > 0);
				int battleCount = 0;
				if (avgOK)
				{
					drAvg = dtAvg.Rows[0];
					if (drAvg["battles"] != DBNull.Value)
						battleCount = Convert.ToInt32(drAvg["battles"]);
					avgOK = (battleCount > 0);
				}
				// Create new datatable to show result
				DataTable dt = new DataTable();
				dt.Columns.Add("Parameter", typeof(string));
				dt.Columns.Add("Image", typeof(Image));
				dt.Columns.Add("Result", typeof(string));
				dt.Columns.Add("Average", typeof(string));
				
				// Add Rows to damage grid
				DataTable dtDamage = dt.Clone();
				dtDamage.Rows.Add(GetValues(dtDamage, drVal, drAvg, "Damage Done", "dmg", battleCount));
				dtDamage.Rows.Add(GetValues(dtDamage, drVal, drAvg, "      by Spotting", "assistSpot", battleCount));
				dtDamage.Rows.Add(GetValues(dtDamage, drVal, drAvg, "      by Tracking", "assistTrack", battleCount));
				dtDamage.Rows.Add(GetValues(dtDamage, drVal, drAvg, "      by Blocking", "dmgBlocked", battleCount));
				dtDamage.Rows.Add(GetValues(dtDamage, drVal, drAvg, "Damage Received", "dmgReceived", battleCount, false));
				dtDamage.Rows.Add(GetValues(dtDamage, drVal, drAvg, "      Potential", "potentialDmgReceived", battleCount));
				// Done;
				dtDamage.AcceptChanges();
				dgvDamage.DataSource = dtDamage;
				FormatStandardDataGrid(dgvDamage, "");
				
				// Add Rows to shooting grid
				DataTable dtShooting = dt.Clone();
				dtShooting.Rows.Add(GetValues(dtShooting, drVal, drAvg, "Shots Fired", "shots", battleCount, decimals: 1));
				dtShooting.Rows.Add(GetValues(dtShooting, drVal, drAvg, "      Hits", "hits", battleCount, decimals: 1));
				dtShooting.Rows.Add(GetValues(dtShooting, drVal, drAvg, "      Pierced", "pierced", battleCount, decimals: 1));
				dtShooting.Rows.Add(GetValues(dtShooting, drVal, drAvg, "      He Hits", "heHits", battleCount, decimals: 1));
				dtShooting.Rows.Add(GetValues(dtShooting, drVal, drAvg, "Shots Received", "shotsReceived", battleCount, false, decimals: 1));
				dtShooting.Rows.Add(GetValues(dtShooting, drVal, drAvg, "      Pierced", "piercedReceived", battleCount, false, decimals: 1));
				dtShooting.Rows.Add(GetValues(dtShooting, drVal, drAvg, "      He Hits", "heHitsReceived", battleCount, false, decimals: 1));
				dtShooting.Rows.Add(GetValues(dtShooting, drVal, drAvg, "      No Damage", "noDmgShotsReceived", battleCount, decimals: 1));
				// Done;
				dtShooting.AcceptChanges();
				dgvShooting.DataSource = dtShooting;
				FormatStandardDataGrid(dgvShooting, "");

				// Add Rows to shooting grid
				DataTable dtPerformance = dt.Clone();
				dtPerformance.Rows.Add(GetValues(dtPerformance, drVal, drAvg, "Frags", "frags", battleCount, decimals: 1));
				dtPerformance.Rows.Add(GetValues(dtPerformance, drVal, drAvg, "Spot", "spot", battleCount, decimals: 1));
				dtPerformance.Rows.Add(GetValues(dtPerformance, drVal, drAvg, "Cap", "cap", battleCount, decimals: 1));
				dtPerformance.Rows.Add(GetValues(dtPerformance, drVal, drAvg, "Defence", "def", battleCount, decimals: 1));
				// Done;
				dtPerformance.AcceptChanges();
				dgvPerformance.DataSource = dtPerformance;
				FormatStandardDataGrid(dgvPerformance, "");


			}
		}

		private DataRow GetValues(DataTable dt, DataRow drVal, DataRow drAvg, string rowHeader, string sqlField, int battleCount, bool higherIsBest = true, int decimals = 0)
		{
			DataRow drNew = dt.NewRow();
			drNew["Parameter"] = rowHeader;
			int val = Convert.ToInt32(drVal[sqlField]);
			double avg = 0;
			if (battleCount > 0) avg = Convert.ToDouble(drAvg[sqlField]) / battleCount;
			if (higherIsBest)
				drNew["Image"] = GetIndicator(val, avg, (battleCount > 0));
			else
				drNew["Image"] = GetIndicator(avg, val, (battleCount > 0));
			drNew["Result"] = val.ToString();
			drNew["Average"] = Math.Round(avg, decimals).ToString();
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
			dgv.Columns[0].Width = 100;
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

		private Image GetIndicator(double value, double compareTo, bool showIcon = true)
		{
			if (!showIcon)
				return new Bitmap(1, 1);
			else
			{
				int indicator = 1; // neutral
				if (value > compareTo * 1.025)
					indicator = 0; // up
				else if (value < compareTo * 0.975)
					indicator = 2; // down
				return imgIndicators.Images[indicator];
			}
		}

		#endregion

		#region Grids

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

		private DataTable GetDataGridSource(int team, string orderBy = "xp DESC")
		{
			string fortResourcesFields = "";
			if (showFortResources) fortResourcesFields = ", fortResource as 'IR' ";
			string enhancedFields = "";
			if (showAllColumns)
				enhancedFields =
					", killerName as 'Killed By' " +
										
					", damageAssistedTrack as 'Dmg Track' " +
					", damageAssistedRadio as 'Dmg Spot' " +
					", sniperDamageDealt as 'Dmg Sniper' " +
					
					", damageReceived as 'Dmg Received' " +
					", damageBlockedByArmor as 'Dmg Blocked' " +
					
					", spotted as 'Spot' " +
					", capturePoints as 'Cap' " +
					", droppedCapturePoints as 'Decap' " +
					
					", shots as 'Shots' " +
					", hits as 'Hits' " +
					", pierced as 'Pierced Hits' " +
					", explosionHits as 'Explosion Hits' " +
					
					", directHitsReceived as 'Hits Received' " +
					", piercingsReceived as 'Piercings Received' " +
					", explosionHitsReceived as 'Expl Hits Received' " +
					", noDamageShotsReceived as 'No Dmg Hits Received' " +

					", mileage as 'Milage' " +
					", lifeTime as 'Life Time' " +

					", credits as 'Base Credit' " +
					", isPrematureLeave as 'Premature Leave' " +
					", isTeamKiller as 'Team Killer' " +
					", tkills as 'Team Kills' ";
		
			string sql =
				"select battlePlayer.name as 'Player', clanAbbrev as Clan, tank.name as 'Tank', damageDealt as 'Dmg', kills as 'Frags', xp as 'XP' " +
				fortResourcesFields +
				enhancedFields +
				", deathReason as 'Dead', tank.id as 'TankId', " + team + " as 'Team' " +
				"from battlePlayer inner join " +
				"     tank on battlePlayer.tankId = tank.id " +
				"where battleId=@battleId and team=@team " +
				"order by " + orderBy;
			DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@team", team, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			// Add image as first col
			dt.Columns.Add("TankImage", typeof(Image)).SetOrdinal(3);
			// Add Total Row
			DataRow totalRow = dt.NewRow();
			// Set 0 ad deafult
			foreach (DataColumn dc in dt.Columns)
				if (dc.DataType == System.Type.GetType("System.Int32"))
					totalRow[dc.ColumnName] = 0;
			totalRow["Player"] = "Total";
			Bitmap blankImg = new Bitmap(1, 1);
			totalRow["TankImage"] = (Image)blankImg;
			dt.Rows.Add(totalRow);
			int totRow = dt.Rows.Count -1;
			// Add images
			for (int i = 0; i < dt.Rows.Count - 1; i++)
			{
				DataRow dr = dt.Rows[i];
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
				foreach (DataColumn dc in dt.Columns)
				{
					if (dc.DataType == System.Type.GetType("System.Int32"))
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
			dgv.Columns["TankId"].Visible = false;
			dgv.Columns["Dead"].Visible = false;
			dgv.Columns["Team"].Visible = false;
			dgv.Columns["TankImage"].HeaderText = "";
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
				dgvc.Width = 50;
				// Sorting, set manual 
				dgvc.SortMode = DataGridViewColumnSortMode.Programmatic;
			}
			// col fixed width
			dgv.Columns["TankImage"].Width = 60;
			dgv.Columns["TankImage"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			// Calc width for rest of fields
			if (!showAllColumns)
			{
				// left part = player, clan, tank img, tank
				int w = dgv.Width - 2; // total available width without img col
				int left = Convert.ToInt32(w * 0.65);
				int leftFreeArea = left - 60 - 50;
				dgv.Columns["Player"].Width = leftFreeArea / 2;
				dgv.Columns["Tank"].Width = leftFreeArea - (leftFreeArea / 2);
				// right part = dmg, frags, xp, IR
				int right = w - left;
				int rightCols = 3;
				if (showFortResources) rightCols = 4;
				int i = right / rightCols;
				dgv.Columns["Frags"].Width = i;
				dgv.Columns["Dmg"].Width = i;
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
						dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.GridRowCurrentPlayerDead;
					else
						dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.GridRowPlayerDead;
				}
				else if (dead == -99)
					dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.GridTotalsRow;
				else if (dgv["Player", e.RowIndex].Value.ToString() == Config.Settings.playerName)
					dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.GridRowCurrentPlayerAlive;
			
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
			string sortDirection = "";
			if (column.HeaderCell.SortGlyphDirection == SortOrder.Descending)
				sortDirection = " DESC";
			int team = Convert.ToInt32(dgv.Rows[0].Cells["Team"].Value);
			dgv.DataSource = GetDataGridSource(team, "'" + columnName + "'" + sortDirection);
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
					if (posBefore != dgv.FirstDisplayedScrollingColumnIndex) Refresh();
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
