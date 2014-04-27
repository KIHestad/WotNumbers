using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using WotDBUpdater.Forms;
using System.Net;
using System.Reflection;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using WotDBUpdater.Code;


//using IronPython.Hosting;
//using Microsoft.Scripting.Hosting;
//using IronPython.Runtime;

namespace WotDBUpdater.Forms
{
	public partial class Main : Form
	{

		#region Init 

		public Main()
		{
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e)
		{
			// Hide content until ready
			MainTheme.Visible = true;
			// Black Border on loading
			MainTheme.FormBorderColor = ColorTheme.FormBorderBlack;
			// Style toolbar
			toolMain.Renderer = new StripRenderer();
			toolMain.BackColor = ColorTheme.FormBackTitle;
			toolMain.ShowItemToolTips = false;
			toolItemBattles.Visible = false;
			toolItemTankFilter.Visible = false;
			toolItemRefreshSeparator.Visible = false;
			toolItemColumnSelect.Visible = false;
			// Mouse scrolling for datagrid
			dataGridMain.MouseWheel += new MouseEventHandler(dataGridMain_MouseWheel);
			// Main panel covering whole content area - contains (optional) infopanel at top, grid and scrollbars at bottom
			panelMainArea.Left = MainTheme.MainArea.Left;
			panelMainArea.Top = MainTheme.MainArea.Top;
			// Hide scrollbar initially, set fixed init placement
			scrollX.ScrollElementsTotals = 0;
			scrollY.ScrollElementsTotals = 0;
			scrollX.Left = 0;
			// Grid init placement
			int gridAreaTop = panelInfo.Height; // Start below info panel
			int gridAreaHeight = panelMainArea.Height - panelInfo.Height; // Grid height
			dataGridMain.Top = gridAreaTop;
			dataGridMain.Left = 0;
			dataGridMain.Width = panelMainArea.Width - scrollY.Width;
			dataGridMain.Height = gridAreaHeight - scrollX.Height;
			// Style datagrid
			dataGridMain.BorderStyle = BorderStyle.None;
			dataGridMain.BackgroundColor = ColorTheme.FormBack;
			dataGridMain.GridColor = ColorTheme.GridBorders;
			dataGridMain.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			dataGridMain.ColumnHeadersDefaultCellStyle.BackColor = ColorTheme.GridBorders;
			dataGridMain.ColumnHeadersDefaultCellStyle.ForeColor = ColorTheme.ControlFont;
			dataGridMain.ColumnHeadersDefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
			dataGridMain.ColumnHeadersDefaultCellStyle.SelectionBackColor = ColorTheme.GridSelectedHeaderColor;
			dataGridMain.DefaultCellStyle.BackColor = ColorTheme.FormBack;
			dataGridMain.DefaultCellStyle.ForeColor = ColorTheme.ControlFont;
			dataGridMain.DefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
			dataGridMain.DefaultCellStyle.SelectionBackColor = ColorTheme.GridSelectedCellColor;
			// Draw form 
			lblStatus1.Text = "";
			lblStatus2.Text = "Application init...";
			lblStatusRowCount.Text = "";
			// Show content now
			MainTheme.Visible = true;
		}

		class StripRenderer : ToolStripProfessionalRenderer
		{
			public StripRenderer()
				: base(new Code.StripLayout())
			{
				this.RoundedEdges = false;
			}

			protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
			{
				base.OnRenderItemText(e);
				e.Item.ForeColor = ColorTheme.ToolWhiteToolStrip;
			}
		}

		private void Main_Shown(object sender, EventArgs e)
		{
			// Startup settings
			string msg = "";
			bool ok = Config.GetConfig(out msg);
			if (!ok)
			{
				Code.MsgBox.Show(msg, "Could not load config data");
				lblOverView.Text = "";
				Config.Settings.dossierFileWathcherRun = 0;
				SetListener();
				Form frm = new Forms.File.ApplicationSetting();
				frm.ShowDialog();
			}
			if (DB.CheckConnection())
			{
				// Init
				TankData.GetTankListFromDB();
				TankData.GetJson2dbMappingFromDB();
				TankData.GetTankData2BattleMappingFromDB();
			}
			string result = dossier2json.UpdateDossierFileWatcher();
			// Check DB Version
			bool versionOK = DBVersion.CheckForDbUpgrade();
			SetFormTitle();
			GetFavList();
			ShowContent();
			SetListener();
			// Battle result file watcher
			fileSystemWatcherNewBattle.Path = Path.GetDirectoryName(Log.BattleResultDoneLogFileName());
			fileSystemWatcherNewBattle.Filter = Path.GetFileName(Log.BattleResultDoneLogFileName());
			fileSystemWatcherNewBattle.NotifyFilter = NotifyFilters.LastWrite;
			fileSystemWatcherNewBattle.Changed += new FileSystemEventHandler(NewBattleFileChanged);
			fileSystemWatcherNewBattle.EnableRaisingEvents = false;
			// Display form and status message 
			SetStatus2("Application started");
			MainTheme.Cursor = Cursors.Default;
		}

		#endregion

		#region Common Events

		private int status2DefaultColor = 200;
		private int status2fadeColor = 200;

		private void NewBattleFileChanged(object source, FileSystemEventArgs e)
		{
			if (toolItemViewBattles.Checked)
			{
				GridShowBattle("New battle result detected, grid refreshed");
			}
		}

		private void timerStatus2_Tick(object sender, EventArgs e)
		{
			if (timerStatus2.Interval > 100)
			{
				// Change to fadeout
				timerStatus2.Interval = 20;
				status2fadeColor = status2DefaultColor;
			}
			else
			{
				status2fadeColor = status2fadeColor - 2;
				if (status2fadeColor >= 2)
				{
					lblStatus2.ForeColor = Color.FromArgb(255, status2fadeColor, status2fadeColor, status2fadeColor); // Fading
					Application.DoEvents();
				}
				else
				{
					timerStatus2.Enabled = false;
					lblStatus2.Text = "";
					Application.DoEvents();
				}
			}
		}

		private void SetStatus2(string txt)
		{
			timerStatus2.Enabled = false;
			Application.DoEvents();
			Thread.Sleep(20);
			timerStatus2.Interval = 6000;
			lblStatus2.ForeColor = Color.FromArgb(255, status2DefaultColor, status2DefaultColor, status2DefaultColor); // White color, not faded
			lblStatus2.Text = txt;
			Application.DoEvents();
			Thread.Sleep(20);
			timerStatus2.Enabled = true;
		}

		private void SetFormTitle()
		{
			// Check / show logged in player
			if (Config.Settings.playerName == "")
			{
				MainTheme.Text = "Argus - World of Tanks Statistics - NO PLAYER SELECTED";
			}
			else
			{
				MainTheme.Text = "Argus - World of Tanks Statistics - " + Config.Settings.playerName;
			}
			Refresh();
		}

		private void GetFavList()
		{
			// Remove favlist from menu
			toolItemTankFilter_FavSeparator.Visible = false;
			for (int i = 1; i <= 10; i++)
			{
				ToolStripMenuItem menuItem = toolItemTankFilter.DropDownItems["toolItemTankFilter_Fav" + i.ToString("00")] as ToolStripMenuItem;
				menuItem.Visible = false;
			}
			// Add favlist to menu
			string sql = "select * from favList where position > 0 order by position";
			DataTable dt = DB.FetchData(sql);
			if (dt.Rows.Count > 0)
			{
				toolItemTankFilter_FavSeparator.Visible = true;
				foreach (DataRow dr in dt.Rows)
				{
					ToolStripItem menuItem = toolItemTankFilter.DropDownItems["toolItemTankFilter_Fav" + Convert.ToInt32(dr["position"]).ToString("00")];
					menuItem.Text = dr["name"].ToString();
					menuItem.Visible = true;
				}
			}
		}


		private void SetListener()
		{
			toolItemSettingsRun.Checked = (Config.Settings.dossierFileWathcherRun == 1);
			if (Config.Settings.dossierFileWathcherRun == 1)
			{
				lblStatus1.Text = "Running";
				lblStatus1.ForeColor = System.Drawing.Color.ForestGreen;
			}
			else
			{
				lblStatus1.Text = "Stopped";
				lblStatus1.ForeColor = System.Drawing.Color.DarkRed;
				
			}
			string result = dossier2json.UpdateDossierFileWatcher();
			SetFormBorder();
			SetStatus2(result);
		}

		private void SetFormBorder()
		{
			if (this.WindowState == FormWindowState.Maximized)
				MainTheme.FormBorderColor = ColorTheme.FormBorderBlack;
			else
			{
				if (Config.Settings.dossierFileWathcherRun == 1)
					MainTheme.FormBorderColor = ColorTheme.FormBorderBlue;
				else
					MainTheme.FormBorderColor = ColorTheme.FormBorderRed;
			}
			Refresh();
		}

		#endregion

		#region Data Grid

		private bool mainGridFormatting = false;

		private void GridShowOverall()
		{
			try
			{
				mainGridFormatting = false;
				dataGridMain.DataSource = null;
				if (!DB.CheckConnection()) return;
				string sql =
					"Select 'Tanks count' as Data, cast(count(id) as varchar) as Value from playerTank where playerid=@playerid " +
					"UNION " +
					"SELECT 'Total battles' as Data, cast( SUM(battles15) + SUM(battles7) as varchar) from playerTank where playerid=@playerid " +
					"UNION " +
					"SELECT 'Comment' as Data ,'This is an alpha version of a World of Tanks statistic tool - supposed to rule the World (of Tanks) :-)' ";
				DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.VarChar);
				dataGridMain.DataSource = DB.FetchData(sql);
				// Text cols
				dataGridMain.Columns["Data"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
				dataGridMain.Columns["Data"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
				dataGridMain.Columns["Value"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
				dataGridMain.Columns["Value"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
				// Finish
				GridResizeOverall();
				ResizeNow();
				lblStatusRowCount.Text = "Rows " + dataGridMain.RowCount.ToString();
			}
			catch (Exception)
			{
				
				//throw;
			}
			
		}

		private void GridResizeOverall()
		{
			dataGridMain.Columns[0].Width = 100;
			dataGridMain.Columns[1].Width = 500;
		}

		private void GetTankfilter(out string whereSQL, out string joinSQL, out string Status2Message)
		{
			string tier = "";
			string tierId = "";
			string nation = "";
			string nationId = "";
			string type = "";
			string typeId = "";
			string message = "";
			string newWhereSQL = "";
			string newJoinSQL = "";
			// Calc filter and set main menu title
			if (tankFavListSelected != "")
			{
				toolItemTankFilter.Text = tankFavListSelected;
				message = "Favourite list: " + tankFavListSelected;
				string sql = "select id from favList where name=@name;";
				DB.AddWithValue(ref sql, "@name", tankFavListSelected, DB.SqlDataType.VarChar);
				DataTable dt = DB.FetchData(sql);
				int favListId = Convert.ToInt32(dt.Rows[0][0]);
				newJoinSQL = " INNER JOIN favListTank ON tank.id=favListTank.tankId AND favListTank.favListId=@favListId ";
				DB.AddWithValue(ref newJoinSQL, "@favListId", favListId, DB.SqlDataType.Int);
			}
			else if (tankFilterItemCount == 0)
			{
				toolItemTankFilter.Text = "All Tanks";
				message = "All Tanks";
			}
			else 
			{
				if (toolItemTankFilter_Tier1.Checked) { tier += "1, "; }
				if (toolItemTankFilter_Tier2.Checked) { tier += "2, "; }
				if (toolItemTankFilter_Tier3.Checked) { tier += "3, "; }
				if (toolItemTankFilter_Tier4.Checked) { tier += "4, "; }
				if (toolItemTankFilter_Tier5.Checked) { tier += "5, "; }
				if (toolItemTankFilter_Tier6.Checked) { tier += "6, "; }
				if (toolItemTankFilter_Tier7.Checked) { tier += "7, "; }
				if (toolItemTankFilter_Tier8.Checked) { tier += "8, "; }
				if (toolItemTankFilter_Tier9.Checked) { tier += "9, "; }
				if (toolItemTankFilter_Tier10.Checked) { tier += "10, "; }
				if (toolItemTankFilter_CountryChina.Checked) { nation += "China, "; nationId += "3, "; }
				if (toolItemTankFilter_CountryFrance.Checked) { nation += "France, "; nationId += "4, "; }
				if (toolItemTankFilter_CountryGermany.Checked) { nation += "Germany, "; nationId += "1, "; }
				if (toolItemTankFilter_CountryUK.Checked) { nation += "UK, "; nationId += "5, "; }
				if (toolItemTankFilter_CountryUSA.Checked) { nation += "USA, "; nationId += "2, "; }
				if (toolItemTankFilter_CountryUSSR.Checked) { nation += "USSR, "; nationId += "0, "; }
				if (toolItemTankFilter_CountryJapan.Checked) { nation += "Japan, "; nationId += "6, "; }
				if (toolItemTankFilter_TypeLT.Checked) { type += "Light, "; typeId += "1, "; }
				if (toolItemTankFilter_TypeMT.Checked) { type += "Medium, "; typeId += "2, "; }
				if (toolItemTankFilter_TypeHT.Checked) { type += "Heavy, "; typeId += "3, "; }
				if (toolItemTankFilter_TypeTD.Checked) { type += "TD, "; typeId += "4, "; }
				if (toolItemTankFilter_TypeSPG.Checked) { type += "SPG, "; typeId += "5, "; }
				// Compose status message
				if (tier.Length > 0)
				{
					tierId = tier.Substring(0, tier.Length - 2);
					tier = "Tier: " + tier.Substring(0, tier.Length - 2) + "   ";
					newWhereSQL = " tank.tier IN (" + tierId + ") ";
				}
				if (nation.Length > 0)
				{
					nation = "Nation: " + nation.Substring(0, nation.Length - 2) + "   ";
					nationId = nationId.Substring(0, nationId.Length - 2);
					if (newWhereSQL != "") newWhereSQL += " AND ";
					newWhereSQL += " tank.countryId IN (" + nationId + ") ";
				}
				if (type.Length > 0)
				{
					typeId = typeId.Substring(0, typeId.Length - 2);
					type = "Type: " + type.Substring(0, type.Length - 2) + "   ";
					if (newWhereSQL != "") newWhereSQL += " AND ";
					newWhereSQL += " tank.tankTypeId IN (" + typeId + ") ";
				}
				if (newWhereSQL != "") newWhereSQL = " AND (" + newWhereSQL + ") ";
				message = nation + type + tier;
				if (message.Length > 0) message = message.Substring(0, message.Length - 3);
				// Add correct mein menu name
				if (tankFilterItemCount == 1)
				{
					toolItemTankFilter.Text = message;
				}
				else
				{
					toolItemTankFilter.Text = "Tank filter";
				}
			}
			whereSQL = newWhereSQL;
			joinSQL = newJoinSQL;
			Status2Message = message;
		}

		private void GetSelectedColumnList(out string Select, out List<int> columnWidth)
		{
			string sql = "SELECT columnListSelection.sortorder, columnSelection.colName, columnSelection.name, columnSelection.colWidth " +
						 "FROM   columnListSelection INNER JOIN " +
						 "		 columnSelection ON columnListSelection.columnSelectionId = columnSelection.id " +
						 "WHERE        (columnListSelection.columnListId = @columnListId) " +
						 "ORDER BY columnListSelection.sortorder; ";
			DB.AddWithValue(ref sql, "@columnListId", columnListSelectedId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql);
			Select = "";
			List<int> selectColumnWidth = new List<int>();
			if (dt.Rows.Count == 0)
			{
				Select = "'No columns defined in Column Selection List' As 'Error', ";
				selectColumnWidth.Add(300);
			}
			else
			{
				foreach (DataRow dr in dt.Rows)
				{
					Select += dr["colname"].ToString() + " as '" + dr["name"].ToString() + "', ";
					selectColumnWidth.Add(Convert.ToInt32(dr["colWidth"]));
				}
			}
			columnWidth = selectColumnWidth;
		}

		private void GridShowTankInfo(string statusmessage = "")
		{
			mainGridFormatting = false;
			dataGridMain.DataSource = null;
			if (!DB.CheckConnection()) return;
			// Get Columns
			string select = "";
			List<int> columnWidth = new List<int>();
			GetSelectedColumnList(out select, out columnWidth);
			// Get Tank filter
			string message = "";
			string where = "";
			string join = "";
			GetTankfilter(out where, out join, out message);
			string sortordercol = "0 as sortorder ";
			if (join != "") sortordercol = "favListTank.sortorder as sortorder ";
			//tank.tier AS Tier, tank.name AS Tank, tankType.name AS Tanktype, country.name AS Country, " +
			//			 playerTank.battles15 AS [Battles15], playerTank.battles7 AS [Battles7], playerTank.wn8 as WN8, playerTank.eff as EFF, "
			string sql =
				"SELECT   " + select + sortordercol +
				"FROM     playerTank INNER JOIN " +
				"         player ON playerTank.playerId = player.id INNER JOIN " +
				"         tank ON playerTank.tankId = tank.id INNER JOIN " +
				"         tankType ON tank.tankTypeId = tankType.id INNER JOIN " +
				"         country ON tank.countryId = country.id " + join +
				"WHERE    player.id=@playerid " + where + " " +
				"ORDER BY sortorder";
			DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
			mainGridFormatting = true;
			dataGridMain.DataSource = DB.FetchData(sql);
			// Text cols
			dataGridMain.Columns["sortorder"].Visible = false;
			// Grid col size
			int colnum = 0;
			foreach (int cw in columnWidth)
			{
				dataGridMain.Columns[colnum].Width = cw;
				dataGridMain.Columns[colnum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
				dataGridMain.Columns[colnum].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
				colnum++;
			}
			ResizeNow();
			// Add status message
			if (statusmessage == "") statusmessage = message;
			SetStatus2(statusmessage);
			lblStatusRowCount.Text = "Rows " + dataGridMain.RowCount.ToString();
		}

		private void GridShowBattle(string overrideStatus2Message = "")
		{
			mainGridFormatting = false;
			dataGridMain.DataSource = null;
			if (!DB.CheckConnection()) return;
			// Create Battlefiler
			string battleFilter = "";
			if (!toolItemBattlesAll.Checked)
			{
				battleFilter = "AND battleTime>=@battleTime ";
			}
			// Get Tank filter
			string tankFilterMessage = "";
			string tankFilter = "";
			string tankJoin = "";
			GetTankfilter(out tankFilter, out tankJoin, out tankFilterMessage);
			string sortordercol = "0 as sortorder ";
			if (tankJoin != "") sortordercol = "favListTank.sortorder as sortorder ";
			string sql =
				"SELECT CAST(tank.tier AS FLOAT) AS Tier, tank.name AS Tank, CAST(battle.battleTime AS DATETIME) AS 'Battle Time', battleResult.name as Result, battleSurvive.name as Survived, " +
				"  battle.dmg AS [Damage Caused], battle.dmgReceived AS [Damage Received], CAST(battle.frags AS FLOAT) AS Kills, battle.xp AS XP, CAST(battle.spotted AS FLOAT) AS Detected, " +
				"  CAST(battle.cap AS FLOAT) AS [Capture Points], CAST(battle.def AS FLOAT) AS [Defense Points], CAST(battle.shots AS FLOAT) AS Shots, CAST(battle.hits AS FLOAT) AS Hits, battle.wn8 AS WN8, battle.eff AS EFF, " +
				"  battleResult.color as battleResultColor,  battleSurvive.color as battleSurviveColor, battlescount, CAST(battle.battleTime AS DATETIME) AS battleTime, battle.battleResultId, battle.battleSurviveId, " +
				"  battle.victory, battle.draw, battle.defeat, battle.survived as survivedcount, battle.killed as killedcount, 0 as footer, " + sortordercol + 
				"FROM    battle INNER JOIN " +
				"        playerTank ON battle.playerTankId = playerTank.id INNER JOIN " +
				"        tank ON playerTank.tankId = tank.id INNER JOIN " +
				"        battleResult ON battle.battleResultId = battleResult.id INNER JOIN " +
				"        battleSurvive ON battle.battleSurviveId = battleSurvive.id " + tankJoin +
				"WHERE   playerTank.playerId=@playerid " + battleFilter + tankFilter + 
				"ORDER BY sortorder, battle.battleTime DESC ";
			DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
			DateTime dateFilter = new DateTime();
			if (!toolItemBattlesAll.Checked)
			{
				DateTime basedate = DateTime.Now;
				if (DateTime.Now.Hour < 5) basedate = DateTime.Now.AddDays(-1); // correct date according to server reset 05:00
				dateFilter = new DateTime(basedate.Year, basedate.Month, basedate.Day, 5, 0, 0); 
				// Adjust time scale according to selected filter
				if (toolItemBattles3d.Checked) dateFilter = DateTime.Now.AddDays(-3);
				else if (toolItemBattles1w.Checked) dateFilter = DateTime.Now.AddDays(-7);
				else if (toolItemBattles1m.Checked) dateFilter = DateTime.Now.AddMonths(-1);
				else if (toolItemBattles1y.Checked) dateFilter = DateTime.Now.AddYears(-1);
				DB.AddWithValue(ref sql, "@battleTime", dateFilter.ToString("yyyy-MM-dd HH:mm"), DB.SqlDataType.DateTime);
			}
			DataTable dt = new DataTable();
			dt = DB.FetchData(sql);
			int rowcount = dt.Rows.Count;
			// Add footer
			if (dt.Rows.Count > 1)
			{
				sql =
					"SELECT  AVG(CAST(tank.tier AS FLOAT)) AS Tier, " +
					"        'Average  on ' + CAST(SUM(battle.battlesCount) AS VARCHAR) + ' battles' AS Tank, " +
					"        CAST(NULL AS DATETIME) as 'Battle Time', " +
					"        CAST(ROUND(SUM(CAST(battle.victory AS FLOAT)) / SUM(battle.battlesCount) * 100, 1) AS VARCHAR) + '%' AS Result, " +
					"        CAST(ROUND(SUM(CAST(battle.survived AS FLOAT)) / SUM(battle.battlesCount) * 100, 1) AS VARCHAR) + '%' AS Survived, " +
					"        CAST(AVG(CAST(battle.dmg AS FLOAT)) AS INT) AS [Damage Caused], " +
					"        CAST(AVG(CAST(battle.dmgReceived AS FLOAT)) AS INT) AS [Damage Received], " +
					"        AVG(CAST(battle.frags AS FLOAT)) AS Kills, " +
					"        CAST(AVG(CAST(battle.xp AS FLOAT)) AS INT) AS XP, " +
					"        AVG(CAST(battle.spotted AS FLOAT))  AS Detected," +
					"		 AVG(CAST(battle.cap AS FLOAT))  AS [Capture Points], " +
					"		 AVG(CAST(battle.def AS FLOAT)) AS [Defense Points], " +
					"		 AVG(CAST(battle.shots AS FLOAT))  AS Shots, " +
					"		 AVG(CAST(battle.hits AS FLOAT)) AS Hits, " +
					"		 CAST(AVG(CAST(battle.wn8 AS FLOAT)) AS INT) AS WN8, " +
					"		 CAST(AVG(CAST(battle.eff AS FLOAT)) AS INT) AS EFF, " +
					"		 '#F0F0F0' as battleResultColor, " +
					"		 '#F0F0F0' as battleSurviveColor, " +
					"		 SUM(battlescount) AS battlescount, " +
					"		 CAST(@getdate AS DATETIME) AS battleTime, " +
					"		 4 AS battleResultId, " +
					"		 2 AS battleSurviveId," +
					"		 SUM (battle.victory) AS victory, " +
					"		 SUM (battle.draw) AS draw, " +
					"		 SUM (battle.defeat) AS defeat, " +
					"		 SUM (battle.survived) as survivedcount, " +
					"		 SUM (battle.killed) as killedcount, " +
					"        1 as footer " +
					"FROM    battle INNER JOIN " +
					"        playerTank ON battle.playerTankId = playerTank.id INNER JOIN " +
					"        tank ON playerTank.tankId = tank.id " +
					"WHERE   playerTank.playerId=@playerid " + battleFilter + tankFilter;
				if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
					sql = sql.Replace("+", "||"); // For SQLite support use || instead of +
				DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
				DB.AddWithValue(ref sql, "@getdate", DateTime.Now.ToString("yyyy-MM-dd HH:mm"), DB.SqlDataType.DateTime);
				if (!toolItemBattlesAll.Checked)
				{
					DB.AddWithValue(ref sql, "@battleTime", dateFilter.ToString("yyyy-MM-dd HH:mm"), DB.SqlDataType.DateTime);
				}
				dt.Merge(DB.FetchData(sql));
				
			}
			// populate datagrid
			mainGridFormatting = true;
			dataGridMain.DataSource = dt;
			// Hide cols
			dataGridMain.Columns["battleResultColor"].Visible = false;
			dataGridMain.Columns["battleSurviveColor"].Visible = false;
			dataGridMain.Columns["battleTime"].Visible = false;
			dataGridMain.Columns["battlescount"].Visible = false;
			dataGridMain.Columns["battleResultId"].Visible = false;
			dataGridMain.Columns["battleSurviveId"].Visible = false;
			dataGridMain.Columns["victory"].Visible = false;
			dataGridMain.Columns["draw"].Visible = false;
			dataGridMain.Columns["defeat"].Visible = false;
			dataGridMain.Columns["survivedcount"].Visible = false;
			dataGridMain.Columns["killedcount"].Visible = false;
			dataGridMain.Columns["footer"].Visible = false;
			dataGridMain.Columns["sortorder"].Visible = false;
			// Text cols
			dataGridMain.Columns["Tank"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Result"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Survived"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Tank"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Result"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Survived"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
			
			// Finish up
			GridResizeBattle();
			ResizeNow();
			toolItemBattles.Visible = true;
			if (overrideStatus2Message == "")
				SetStatus2(toolItemBattles.Text + "   " + tankFilterMessage);
			else
				SetStatus2(overrideStatus2Message);
			lblStatusRowCount.Text = "Rows " + rowcount.ToString();
		}

		private void GridResizeBattle()
		{
			dataGridMain.Columns[0].Width = 35;
			dataGridMain.Columns[1].Width = 120;
			for (int i = 2; i <= 15; i++)
			{
				dataGridMain.Columns[i].Width = 60;
			}
			dataGridMain.Columns[6].Width = 50;
		}
		
		private void dataGridMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (mainGridFormatting)
			{
				string col = dataGridMain.Columns[e.ColumnIndex].Name;
				DataGridViewCell cell = dataGridMain[e.ColumnIndex, e.RowIndex];
				if (col.Equals("EFF"))
				{
					// Dynamic color by efficiency
					//"eff": [
					//  { "value": 610,  "color": ${"def.colorRating.very_bad" } },  //    0 - 609  - very bad   (20% of players)
					//  { "value": 850,  "color": ${"def.colorRating.bad"      } },  //  610 - 849  - bad        (better then 20% of players)
					//  { "value": 1145, "color": ${"def.colorRating.normal"   } },  //  850 - 1144 - normal     (better then 60% of players)
					//  { "value": 1475, "color": ${"def.colorRating.good"     } },  // 1145 - 1474 - good       (better then 90% of players)
					//  { "value": 1775, "color": ${"def.colorRating.very_good"} },  // 1475 - 1774 - very good  (better then 99% of players)
					//  { "value": 9999, "color": ${"def.colorRating.unique"   } }   // 1775 - *    - unique     (better then 99.9% of players)
					//]
					int eff = Convert.ToInt32(dataGridMain["EFF", e.RowIndex].Value);
					Color effRatingColor = ColorTheme.Rating_very_bad;
					if (eff > 1774) effRatingColor = ColorTheme.Rating_uniqe;
					else if (eff > 1474) effRatingColor = ColorTheme.Rating_very_good;
					else if (eff > 1144) effRatingColor = ColorTheme.Rating_good;
					else if (eff > 849) effRatingColor = ColorTheme.Rating_normal;
					else if (eff > 609) effRatingColor = ColorTheme.Rating_bad;
					cell.Style.ForeColor = effRatingColor;
				}
				else if (col.Equals("WN8"))
				{
					// Dynamic color by WN8 rating
					//"wn8": [
					//	{ "value": 310,  "color": ${"def.colorRating.very_bad" } },  //    0 - 309  - very bad   (20% of players)
					//	{ "value": 750,  "color": ${"def.colorRating.bad"      } },  //  310 - 749  - bad        (better then 20% of players)
					//	{ "value": 1310, "color": ${"def.colorRating.normal"   } },  //  750 - 1309 - normal     (better then 60% of players)
					//	{ "value": 1965, "color": ${"def.colorRating.good"     } },  // 1310 - 1964 - good       (better then 90% of players)
					//	{ "value": 2540, "color": ${"def.colorRating.very_good"} },  // 1965 - 2539 - very good  (better then 99% of players)
					//	{ "value": 9999, "color": ${"def.colorRating.unique"   } }   // 2540 - *    - unique     (better then 99.9% of players)
					//]
					int wn8 = Convert.ToInt32(dataGridMain["WN8", e.RowIndex].Value);
					Color wn8RatingColor = ColorTheme.Rating_very_bad;
					if (wn8 > 2539) wn8RatingColor = ColorTheme.Rating_uniqe;
					else if (wn8 > 1964) wn8RatingColor = ColorTheme.Rating_very_good;
					else if (wn8 > 1309) wn8RatingColor = ColorTheme.Rating_good;
					else if (wn8 > 749) wn8RatingColor = ColorTheme.Rating_normal;
					else if (wn8 > 309) wn8RatingColor = ColorTheme.Rating_bad;
					cell.Style.ForeColor = wn8RatingColor;
				}

				else if (toolItemViewBattles.Checked)
				{
					bool footer = (Convert.ToInt32(dataGridMain["footer", e.RowIndex].Value) == 1);
					if (col.Equals("Tank"))
					{
						string battleTime = dataGridMain["battleTime", e.RowIndex].Value.ToString();
						int battlesCount = Convert.ToInt32(dataGridMain["battlescount", e.RowIndex].Value);
						// Check if this row is normal or footer
						if (!footer) // normal line
						{
							cell.ToolTipText = "Battle result based on " + battlesCount.ToString() + " battle(s)" + Environment.NewLine + "Battle time: " + battleTime;
						}
						else // footer
						{
							cell.ToolTipText = "Average calculations based on " + battlesCount.ToString() + " battles";
							dataGridMain.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.ToolGrayMainBack;
						}
					}
					// Battle Result color color
					else if (col.Equals("Result"))
					{
						string battleResultColor = dataGridMain["battleResultColor", e.RowIndex].Value.ToString();
						cell.Style.ForeColor = System.Drawing.ColorTranslator.FromHtml(battleResultColor);
						int battlesCount = Convert.ToInt32(dataGridMain["battlescount", e.RowIndex].Value);
						if (battlesCount > 1)
						{
							cell.ToolTipText = "Victory: " + dataGridMain["victory", e.RowIndex].Value.ToString() + Environment.NewLine +
								"Draw: " + dataGridMain["draw", e.RowIndex].Value.ToString() + Environment.NewLine +
								"Defeat: " + dataGridMain["defeat", e.RowIndex].Value.ToString();
						}
					}
					// Survived color and formatting
					else if (col.Equals("Survived"))
					{
						string battleResultColor = dataGridMain["battleSurviveColor", e.RowIndex].Value.ToString();
						cell.Style.ForeColor = System.Drawing.ColorTranslator.FromHtml(battleResultColor);
						int battlesCount = Convert.ToInt32(dataGridMain["battlescount", e.RowIndex].Value);
						if (battlesCount > 1)
						{
							cell.ToolTipText = "Survived: " + dataGridMain["survivedcount", e.RowIndex].Value.ToString() + Environment.NewLine +
								"Killed: " + dataGridMain["killedcount", e.RowIndex].Value.ToString();
						}
					}
					// Foter desimal
					if (footer)
					{
						if (col == "Tier" || col == "Kills" || col == "Detected" || col == "Shots" || col == "Hits" || col == "Capture Points" || col == "Defense Points")
						{
							cell.Style.Format = "n1";
						}
					}
				}
				
			}
		}

		#endregion
		
		#region Resize

		private void Main_Resize(object sender, EventArgs e)
		{
			ResizeNow();
		}

		private void Main_ResizeEnd(object sender, EventArgs e)
		{
			ResizeNow();
		}

		private void ResizeNow()
		{
			// Set Form border color
			SetFormBorder(); 
			// Set Main Area Panel
			panelMainArea.Width = MainTheme.MainArea.Width;
			panelMainArea.Height = MainTheme.MainArea.Height;
			// Set scrollbars, size differs according to scrollbar visibility (ScrollNecessary)
			RefreshScrollbars();
			// Scroll and grid size
			int gridAreaTop = panelInfo.Height; // Start below info panel
			int gridAreaHeight = panelMainArea.Height - panelInfo.Height; // Grid height
			dataGridMain.Top = gridAreaTop;
			scrollCorner.Left = panelMainArea.Width - scrollCorner.Width;
			scrollCorner.Top = panelMainArea.Height - scrollCorner.Height;
			scrollY.Top = gridAreaTop;
			scrollY.Left = panelMainArea.Width - scrollY.Width;
			scrollX.Top = panelMainArea.Height - scrollX.Height;
			// check if scrollbar is visible to determine width / height
			int scrollYWidth = 0;
			int scrollXHeight = 0;
			if (scrollY.ScrollNecessary) scrollYWidth = scrollY.Width;
			if (scrollX.ScrollNecessary) scrollXHeight = scrollX.Height;
			dataGridMain.Width = panelMainArea.Width - scrollYWidth;
			dataGridMain.Height = gridAreaHeight - scrollXHeight; 
			scrollY.Height = dataGridMain.Height;
			scrollX.Width = dataGridMain.Width;
		}

		#endregion

		#region Grid and Scroll Handling

		// Set scrollbar properties according to grid content
		private void RefreshScrollbars()
		{
			int XVisible = 0;
			int XTotal = 0;
			int YVisible = 0;
			int YTotal = 0;
			// Calc scroll boundarys
			if (dataGridMain.RowCount > 0)
			{
				YTotal = dataGridMain.RowCount;
				YVisible = dataGridMain.DisplayedRowCount(false);
				//XTotal = dataGridMain.ColumnCount;
				foreach (DataGridViewColumn col in dataGridMain.Columns)
				{
					if (col.Visible) XTotal++;
				}
				XVisible = dataGridMain.DisplayedColumnCount(false);
			}
			// Scroll init
			scrollX.ScrollElementsVisible = XVisible;
			scrollX.ScrollElementsTotals = XTotal;
			scrollY.ScrollElementsVisible = YVisible;
			scrollY.ScrollElementsTotals = YTotal;
			// Scroll corner
			scrollCorner.Visible = (scrollX.ScrollNecessary && scrollY.ScrollNecessary);
		}

		// Scrolling Y
		private bool scrollingY = false;
		private void scrollY_MouseDown(object sender, MouseEventArgs e)
		{
			scrollingY = true;
			ScrollY();
		}

		private void scrollY_MouseMove(object sender, MouseEventArgs e)
		{
			if (scrollingY) ScrollY();
		}

		private void ScrollY()
		{
			int posBefore = dataGridMain.FirstDisplayedScrollingRowIndex;
			dataGridMain.FirstDisplayedScrollingRowIndex = scrollY.ScrollPosition;
			if (posBefore != dataGridMain.FirstDisplayedScrollingRowIndex) Refresh();
		}

		private void scrollY_MouseUp(object sender, MouseEventArgs e)
		{
			scrollingY = false;
		}


		// Scrolling X
		private bool scrollingX = false;
		private void scrollX_MouseDown(object sender, MouseEventArgs e)
		{
			scrollingX = true;
			ScrollX();
		}

		private void scrollX_MouseUp(object sender, MouseEventArgs e)
		{
			scrollingX = false;
		}

		private void scrollX_MouseMove(object sender, MouseEventArgs e)
		{
			if (scrollingX) ScrollX();
		}

		private void ScrollX()
		{
			int posBefore = dataGridMain.FirstDisplayedScrollingColumnIndex;
			dataGridMain.FirstDisplayedScrollingColumnIndex = scrollX.ScrollPosition;
			if (posBefore != dataGridMain.FirstDisplayedScrollingColumnIndex) Refresh();
		}

		// Move scrollbar according to grid movements
		private void dataGridMain_SelectionChanged(object sender, EventArgs e)
		{
			MoveScrollBar();
		}

		private void MoveScrollBar()
		{
			scrollX.ScrollPosition = dataGridMain.FirstDisplayedScrollingColumnIndex;
			scrollY.ScrollPosition = dataGridMain.FirstDisplayedScrollingRowIndex;
		}

		// Enable mouse wheel scrolling for datagrid
		private void dataGridMain_MouseWheel(object sender, MouseEventArgs e)
		{
			try
			{
				// scroll in grid from mouse wheel
				int currentIndex = this.dataGridMain.FirstDisplayedScrollingRowIndex;
				int scrollLines = SystemInformation.MouseWheelScrollLines;
				if (e.Delta > 0)
				{
					this.dataGridMain.FirstDisplayedScrollingRowIndex = Math.Max(0, currentIndex - scrollLines);
				}
				else if (e.Delta < 0)
				{
					this.dataGridMain.FirstDisplayedScrollingRowIndex = currentIndex + scrollLines;
				}
				// move scrollbar
				MoveScrollBar();
			}
			catch (Exception)
			{
				// throw;
			}
		}

		#endregion

		#region Panel Info - Slider Events

		private int infoPanelSlideSpeed;
		
		private void InfoPanelSlideStart(bool show)
		{
			if (show)
			{
				infoPanelSlideSpeed = 4;
				panelInfo.Visible = true;
			}
			else if (!show)
			{
				infoPanelSlideSpeed = -4;
			}
			timerPanelSlide.Enabled = true;
		}

		private void timerPanelSlide_Tick(object sender, EventArgs e)
		{
			// Expand or collapse panel
			int panelInfoMaxSize = 72;
			// Change InfoPanel Height if within boundary
			if (panelInfo.Height + infoPanelSlideSpeed < 0)
			{
				panelInfo.Height = 0;
				timerPanelSlide.Enabled = false;
				ResizeNow();
			}
			else if (panelInfo.Height + infoPanelSlideSpeed > panelInfoMaxSize)
			{
				panelInfo.Height = panelInfoMaxSize;
				timerPanelSlide.Enabled = false;
				ResizeNow();
			}
			else
			{
				panelInfo.Height += infoPanelSlideSpeed;
				// Simple form resize, only focus on height
				// Set Main Area Panel
				panelMainArea.Height = MainTheme.MainArea.Height;
				// Scroll and grid size
				int gridAreaTop = panelInfo.Height; // Start below info panel
				int gridAreaHeight = panelMainArea.Height - panelInfo.Height; // Grid height
				dataGridMain.Top = gridAreaTop;
				scrollCorner.Top = panelMainArea.Height - scrollCorner.Height;
				scrollY.Top = gridAreaTop;
				scrollX.Top = panelMainArea.Height - scrollX.Height;
				// check if scrollbar is visible to determine width / height
				int scrollXHeight = 0;
				if (scrollX.ScrollNecessary) scrollXHeight = scrollX.Height;
				dataGridMain.Height = gridAreaHeight - scrollXHeight;
				scrollY.Height = dataGridMain.Height;
			}
			
		}

		#endregion       
	 
		#region Toolstrip Events

		private void toolItem_Checked_paint(object sender, PaintEventArgs e)
		{
			ToolStripMenuItem menu = (ToolStripMenuItem)sender;
			if (menu.Checked)
			{
				if (menu.Image == null)
				{
					// Default checkbox
					e.Graphics.DrawImage(imageListToolStrip.Images[0], 5, 3);
					e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, ColorTheme.ToolGrayCheckBorder)), 4, 2, 17, 17);
					e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, ColorTheme.ToolGrayCheckBorder)), 5, 3, 15, 15);
				}
				else
				{
					// Border around picture
					e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, ColorTheme.ToolGrayCheckBorder)), 3, 1, 19, 19);
					e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, ColorTheme.ToolGrayCheckBorder)), 4, 2, 17, 17);
				}

			}
		}

		private void ShowContent()
		{
			if (toolItemViewOverall.Checked)
			{
				lblOverView.Text = "Welcome " + Config.Settings.playerName;
				GridShowOverall();
			}
			else if (toolItemViewTankInfo.Checked)
			{
				GridShowTankInfo();
			}
			else if (toolItemViewBattles.Checked)
			{
				GridShowBattle();
			}
		}

		private void SetTankFilterCheckedElements(string FavList)
		{
			toolItemTankFilter_Uncheck(true, true, true, true, false, false);
			tankFavListSelected = FavList;
			if (FavList != "") // Selected Favlist
			{
				// Check current favlist
				for (int i = 1; i <= 10; i++)
				{
					ToolStripMenuItem menuItem = toolItemTankFilter.DropDownItems["toolItemTankFilter_Fav" + i.ToString("00")] as ToolStripMenuItem;
					if (menuItem.Text == FavList) menuItem.Checked = true;
				}
				// Uncheck all tanks
				toolItemTankFilter_All.Checked = false;
			}
		}

		private void toolItemViewSelected_Click(object sender, EventArgs e)
		{
			// First remember current Tank Filter selection
			if (toolItemViewTankInfo.Checked)
			{
				tankFavListTankView = tankFavListSelected;
			}
			else if (toolItemViewBattles.Checked)
			{
				tankFavListBattleView = tankFavListSelected;
			}
			ToolStripButton menuItem = (ToolStripButton)sender;
			if (!menuItem.Checked)
			{
				toolItemViewOverall.Checked = false;
				toolItemViewBattles.Checked = false;
				toolItemViewTankInfo.Checked = false;
				toolItemBattles.Visible = false;
				toolItemTankFilter.Visible = false;
				toolItemColumnSelect.Visible = false;
				toolItemRefreshSeparator.Visible = true;
				menuItem.Checked = true;
				SetStatus2(menuItem.Text);
				if (toolItemViewOverall.Checked)
				{
					toolItemRefreshSeparator.Visible = false;
					InfoPanelSlideStart(true);
				}
				else if (toolItemViewTankInfo.Checked)
				{
					InfoPanelSlideStart(false);
					// Apply last selected Tank Filter
					SetTankFilterCheckedElements(tankFavListTankView);
					toolItemTankFilter.Visible = true;
					// Get Column Setup List
					GetColumnSetupList();
					toolItemColumnSelect.Visible = true;
					
				}
				else if (toolItemViewBattles.Checked)
				{
					InfoPanelSlideStart(false);
					toolItemBattles.Visible = true;
					// Apply last selected Tank Filter
					SetTankFilterCheckedElements(tankFavListBattleView);
					toolItemTankFilter.Visible = true;
					// Get Column Setup List
					GetColumnSetupList();
					toolItemColumnSelect.Visible = true;
					// Start file watcher to detect new battles
					fileSystemWatcherNewBattle.EnableRaisingEvents = true;
				}
			}
			ShowContent();
		}

		private void toolItemRefresh_Click(object sender, EventArgs e)
		{
			SetStatus2("Refreshing grid...");
			if (toolItemViewBattles.Checked)
				GridShowBattle();
			else if (toolItemViewTankInfo.Checked)
				GridShowTankInfo();
			else if (toolItemViewOverall.Checked)
				GridShowOverall();
			SetStatus2("Grid refreshed");
		}

		private void toolItemBattlesSelected_Click(object sender, EventArgs e)
		{
			toolItemBattles1d.Checked = false;
			toolItemBattles3d.Checked = false;
			toolItemBattles1w.Checked = false;
			toolItemBattles1m.Checked = false;
			toolItemBattles1y.Checked = false;
			toolItemBattlesAll.Checked = false;
			ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
			menuItem.Checked = true;
			toolItemBattles.Text = menuItem.Text;
			GridShowBattle();
		}

		private int tankFilterItemCount = 0; // To keep track on how manny tank filter itmes selected
		private string tankFavListSelected = ""; // To keep track on fav list selected for tank view
		private string tankFavListTankView = ""; // Remember fav list for tank view, "" == All tanks
		private string tankFavListBattleView = ""; // Remember fav list for battle view, "" == All tanks
		
		private void toolItemTankFilter_Uncheck(bool tier, bool country, bool type, bool favList, bool reopenMenu = true, bool autoRefreshGrid = true)
		{
			if (favList)
			{
				toolItemTankFavList_Uncheck();
			}
			if (tier)
			{
				toolItemTankFilter_Tier1.Checked = false;
				toolItemTankFilter_Tier2.Checked = false;
				toolItemTankFilter_Tier3.Checked = false;
				toolItemTankFilter_Tier4.Checked = false;
				toolItemTankFilter_Tier5.Checked = false;
				toolItemTankFilter_Tier6.Checked = false;
				toolItemTankFilter_Tier7.Checked = false;
				toolItemTankFilter_Tier8.Checked = false;
				toolItemTankFilter_Tier9.Checked = false;
				toolItemTankFilter_Tier10.Checked = false;
			}
			if (country)
			{
				toolItemTankFilter_CountryChina.Checked = false;
				toolItemTankFilter_CountryFrance.Checked = false;
				toolItemTankFilter_CountryGermany.Checked = false;
				toolItemTankFilter_CountryJapan.Checked = false;
				toolItemTankFilter_CountryUK.Checked = false;
				toolItemTankFilter_CountryUSA.Checked = false;
				toolItemTankFilter_CountryUSSR.Checked = false;
			}
			if (type)
			{
				toolItemTankFilter_TypeHT.Checked = false;
				toolItemTankFilter_TypeLT.Checked = false;
				toolItemTankFilter_TypeMT.Checked = false;
				toolItemTankFilter_TypeSPG.Checked = false;
				toolItemTankFilter_TypeTD.Checked = false;
			}
			// Count selected menu items
			tankFilterItemCount = 0;
			if (toolItemTankFilter_CountryChina.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_CountryFrance.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_CountryGermany.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_CountryUK.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_CountryUSA.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_CountryUSSR.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_CountryJapan.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_TypeLT.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_TypeMT.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_TypeHT.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_TypeTD.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_TypeSPG.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_Tier1.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_Tier2.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_Tier3.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_Tier4.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_Tier5.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_Tier6.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_Tier7.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_Tier8.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_Tier9.Checked) tankFilterItemCount++;
			if (toolItemTankFilter_Tier10.Checked) tankFilterItemCount++;
			toolItemTankFilter_All.Checked = (tankFilterItemCount == 0 && tankFavListSelected == "");
			// Reopen menu item exept for "all tanks"
			if (reopenMenu) this.toolItemTankFilter.ShowDropDown();
			// Refresh grid
			if (autoRefreshGrid) ShowContent();
		}

		private void toolItemTankFavList_Uncheck()
		{
			// Deselect all favlist
			for (int i = 1; i <= 10; i++)
			{
				ToolStripMenuItem menuItem = toolItemTankFilter.DropDownItems["toolItemTankFilter_Fav" + i.ToString("00")] as ToolStripMenuItem;
				menuItem.Checked = false;
			}
			tankFavListSelected = "";
		}

		private void toolItem_Fav_Clicked(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
			if (!menuItem.Checked)
			{
				toolItemTankFavList_Uncheck(); // Uncheck previous fav list selection
				menuItem.Checked = true; // check fav list menu select
				tankFavListSelected = menuItem.Text; // set current fav list selected
				toolItemTankFilter_Uncheck(true, true, true, false, false); // Unchek all other tank filter, no auto refresh grid
				ShowContent();
			}
		}

		private void toolItemTankFilter_All_Click(object sender, EventArgs e)
		{
			toolItemTankFilter_Uncheck(true, true, true, true, false);
		}

		private void toolItemTankFilterSelected(ToolStripMenuItem menuItem, ToolStripMenuItem parentMenuItem)
		{
			// Remove favlist
			toolItemTankFavList_Uncheck();	
			// Update menu tank filter checked elements
			menuItem.Checked = !menuItem.Checked;
			if (menuItem.Checked)
				tankFilterItemCount++;
			else
				tankFilterItemCount--;
			toolItemTankFilter_All.Checked = (tankFilterItemCount == 0);
			toolItemTankFilter.ShowDropDown();
			parentMenuItem.ShowDropDown();
			// Refresh grid
			ShowContent();
		}

		private void toolItemTankFilter_EditFavList_Click(object sender, EventArgs e)
		{
			// Show fal list editor
			Form frm = new Forms.File.FavTanks();
			frm.ShowDialog();
			// After fav list changes reload menu
			toolItemTankFilter_Uncheck(true, true, true, true, false); // Set select All tanks
			GetFavList(); // Reload fav list items

		}

		private void toolItemTankFilter_Tier_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
			toolItemTankFilterSelected(menuItem, toolItemTankFilter_Tier);
		}

		private void toolItemTankFilter_Type_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
			toolItemTankFilterSelected(menuItem, toolItemTankFilter_Type);
		}

		private void toolItemTankFilter_Country_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
			toolItemTankFilterSelected(menuItem, toolItemTankFilter_Country);
		}

		private void ShowTankFilterStatus()
		{
			string where = "";
			string join = "";
			string message = "";
			GetTankfilter(out where, out join, out message);
			if (toolItemViewBattles.Checked)
				SetStatus2(toolItemBattles.Text + "   " + message);
			else
				SetStatus2(message);
		}

		private void toolItemTankFilter_Country_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				toolItemTankFilter_Uncheck(false, true, false, false);
			}
		}

		private void toolItemTankFilter_Type_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				toolItemTankFilter_Uncheck(false, false, true, false);
			}
		}

		private void toolItemTankFilter_Tier_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				toolItemTankFilter_Uncheck(true, false, false, false);
			}
		}

		private void toolItemTankFilter_MouseDown(object sender, MouseEventArgs e)
		{
			// On right mouse click just display status message for current filter
			if (e.Button == System.Windows.Forms.MouseButtons.Right) ShowTankFilterStatus();
		}

		private void toolItemBattleFilter_MouseDown(object sender, MouseEventArgs e)
		{
			// On right mouse click just display status message for current filter
			if (e.Button == System.Windows.Forms.MouseButtons.Right) ShowTankFilterStatus();
		
		}

		private void toolItemSettingsApp_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.File.ApplicationSetting();
			frm.ShowDialog();
			SetFormTitle();
			// After settings changed, go to all tanks
			toolItemTankFilter_Uncheck(true, true, true, true, false); // Set select All tanks
			GetFavList();
			ShowContent();
			SetListener();
		}

		private string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." +
					Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString() + " (" +
					Assembly.GetExecutingAssembly().GetName().Version.MinorRevision.ToString() + ")";
			}
		}

		private void toolItemHelp_Click(object sender, EventArgs e)
		{
			//Form frm = new Forms.Help.About();
			//frm.ShowDialog();
			string dbVersionComment = " (correct version)";
			if (DBVersion.ExpectedNumber != DBVersion.CurrentNumber())
				dbVersionComment = " (expected: " + DBVersion.ExpectedNumber.ToString("0000") + ")"; 
			string msg = "Argus - World of Tanks Statistics" + Environment.NewLine + Environment.NewLine +
						 "Application version: " + AssemblyVersion + Environment.NewLine +
						 "Database version: " + DBVersion.CurrentNumber().ToString("0000") + dbVersionComment + Environment.NewLine + Environment.NewLine +
						 "Created by: BadButton and cmdrTrinity";
			Code.MsgBox.Show(msg, "About Argus");
		}

		private void toolItemSettingsRun_Click(object sender, EventArgs e)
		{
			toolItemSettingsRun.Checked = !toolItemSettingsRun.Checked;
			// Set Start - Stop button properties
			if (toolItemSettingsRun.Checked)
			{
				Config.Settings.dossierFileWathcherRun = 1;
			}
			else
			{
				Config.Settings.dossierFileWathcherRun = 0;
			}
			string msg = "";
			Config.SaveConfig(out msg);
			SetListener();
		}

		private void toolItemSettingsRunManual_Click(object sender, EventArgs e)
		{
			// Dossier file manual handling
			SetStatus2("Starting manual dossier check...");
			string result = dossier2json.ManualRun();
			SetStatus2(result);
		}

		private void toolItemSettingsUpdateFromPrev_Click(object sender, EventArgs e)
		{
			// Test running previous dossier file
			SetStatus2("Starting check on previous dossier file...");
			string result = dossier2json.ManualRun(true);
			SetStatus2(result);
		}

		private void toolItemSettingsForceUpdateFromPrev_Click(object sender, EventArgs e)
		{
			// Test running previous dossier file, force update - even if no more battles is detected
			SetStatus2("Starting check on previous dossier file with force update...");
			string result = dossier2json.ManualRun(true, true);
			SetStatus2(result);
		}

		private void toolItemShowDbTables_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.Reports.DatabaseTable();
			frm.Show();
		}

		private void toolItemImportBattlesFromWotStat_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.File.ImportWotStat();
			frm.ShowDialog();
		}

		private int columnListcolType = 0; // To keep track on current column list type
		private int columnListSelectedId = 0; // To keep track on current selected column list
		private int columnListSelectedTankView = 0; // Remember last selected column list for tank view, 0 == Use the default one
		private int columnListSelectedBattleView = 0; // Remember last selected column list for battle view, 0 == Use the default one
		
		private void GetColumnSetupList()
		{
			if (toolItemViewTankInfo.Checked) columnListcolType = 1;
			if (toolItemViewBattles.Checked) columnListcolType = 2;
			if (columnListcolType != 0)
			{
				// Hide all colum setup list menu items
				for (int i = 1; i <= 13; i++)
				{
					ToolStripMenuItem menuItem = toolItemColumnSelect.DropDownItems["toolItemColumnSelect_" + i.ToString("00")] as ToolStripMenuItem;
					menuItem.Visible = false;
					menuItem.Checked = false;
				}
				bool separatorVisible = false;
				// Add colum lists
				string sql = "select name, position, colDefault from columnList where colType=@colType and position is not null order by position; ";
				DB.AddWithValue(ref sql, "@colType", columnListcolType, DB.SqlDataType.Int);
				DataTable dt = DB.FetchData(sql);
				int colDefault = 1; // If no default is set, use first menu item
				if (dt.Rows.Count > 0)
				{
					foreach (DataRow dr in dt.Rows)
					{
						if (Convert.ToInt32(dr["position"]) > 3) separatorVisible = true;
						ToolStripMenuItem menuItem = toolItemColumnSelect.DropDownItems["toolItemColumnSelect_" + Convert.ToInt32(dr["position"]).ToString("00")] as ToolStripMenuItem;
						menuItem.Text = dr["name"].ToString();
						menuItem.Visible = true;
						if (Convert.ToBoolean(dr["colDefault"])) colDefault = Convert.ToInt32(dr["position"]); // Set default
					}
				}
				toolItemColumnSelectSep.Visible = separatorVisible;
				// Set checked menu item, use previus selected or use default
				if (columnListcolType == 1)
					if (columnListSelectedTankView != 0) colDefault = columnListSelectedTankView;
				else if (columnListcolType == 2)
					if (columnListSelectedBattleView != 0) colDefault = columnListSelectedBattleView;
				ToolStripMenuItem checkedMenuItem = toolItemColumnSelect.DropDownItems["toolItemColumnSelect_" + Convert.ToInt32(colDefault).ToString("00")] as ToolStripMenuItem;
				checkedMenuItem.Checked = true;
				columnListSelectedId = GetSelectedColumnListId(checkedMenuItem.Text);
			}
		}

		private void toolItemColumnSelect_Click(object sender, EventArgs e)
		{
			// Hide all colum setup list menu items
			for (int i = 1; i <= 13; i++)
			{
				ToolStripMenuItem menuItem = toolItemColumnSelect.DropDownItems["toolItemColumnSelect_" + i.ToString("00")] as ToolStripMenuItem;
				menuItem.Checked = false;
			}
			ToolStripMenuItem selectedMenu = (ToolStripMenuItem)sender;
			selectedMenu.Checked = true;
			// Get and remember selected column setup list
			columnListSelectedId = GetSelectedColumnListId(selectedMenu.Text);
			int colSelected = Convert.ToInt32(selectedMenu.Name.Substring(selectedMenu.Name.Length - 2, 2));
			if (columnListcolType == 1)
				columnListSelectedTankView = colSelected;
			else if (columnListcolType == 2)
				columnListSelectedBattleView = colSelected;
			ShowContent();
		}

		private int GetSelectedColumnListId(string ColumnListName)
		{
			string sql = "select id from columnList where colType=@colType and name=@name";
			DB.AddWithValue(ref sql, "@colType", columnListcolType, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@name", ColumnListName, DB.SqlDataType.VarChar);
			DataTable dt = DB.FetchData(sql);
			int i = 0;
			if (dt.Rows.Count > 0) i = Convert.ToInt32(dt.Rows[0][0]);
			return i;
		}

		private void toolItemColumnSelect_Edit_Click(object sender, EventArgs e)
		{
			File.ColumnSetup.ColumnSetupType colSetupType = new File.ColumnSetup.ColumnSetupType();
			if (toolItemViewBattles.Checked)
				colSetupType = File.ColumnSetup.ColumnSetupType.BattleView;
			else if (toolItemViewTankInfo.Checked)
				colSetupType = File.ColumnSetup.ColumnSetupType.TankView;
			Form frm = new Forms.File.ColumnSetup(colSetupType);
			frm.ShowDialog();
		}

		
		#endregion

		#region Toolstrip Events - TESTING
	
		private void toolItemTest_ImportTankWn8_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.File.ImportTank();
			frm.ShowDialog();
		}

		private void toolItemTest_ProgressBar_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.Test.TestProgressBar();
			frm.Show();
		}

		private void toolItemTest_ViewRange_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.Test.ViewRange();
			frm.ShowDialog();
		}

		private void toolItemTest_ScrollBar_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.Test.ScrollbarTest();
			frm.ShowDialog();
		}

		private void importDossierHistoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Code.ImportWotDossier2DB.importWotDossierHistory();
		}

		private void importWsDossierHistoryToDbToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Code.ImportWotDossier2DB.importWotDossierHistory2Battle();
		}

		private void testNewTankImportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Code.ImportWotApi2DB.ImportTanks();
		}

        private void testNewTurretImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Code.ImportWotApi2DB.ImportTurrets();
        }




		#endregion

		

		
		
	}
}
