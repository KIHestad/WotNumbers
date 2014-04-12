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
		public Main()
		{
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e)
		{
			// Hide form until ready
			MainTheme.Visible = false;
			// Style toolbar
			toolMain.Renderer = new StripRenderer();
			toolMain.BackColor = ColorTheme.FormBackTitle;
			toolMain.ShowItemToolTips = false;
			toolItemBattles.Visible = false;
			toolItemTankFilter.Visible = false;
			toolItemRefreshSeparator.Visible = false;
			// Style datagrid
			dataGridMain.BorderStyle = BorderStyle.None;
			dataGridMain.BackgroundColor = ColorTheme.FormBack;
			dataGridMain.GridColor = ColorTheme.GridBorders;
			dataGridMain.ColumnHeadersDefaultCellStyle.BackColor = ColorTheme.GridBorders;
			dataGridMain.ColumnHeadersDefaultCellStyle.ForeColor = ColorTheme.ControlFont;
			dataGridMain.ColumnHeadersDefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
			dataGridMain.ColumnHeadersDefaultCellStyle.SelectionBackColor = ColorTheme.GridSelectedHeaderColor;
			dataGridMain.DefaultCellStyle.BackColor = ColorTheme.FormBack;
			dataGridMain.DefaultCellStyle.ForeColor = ColorTheme.ControlFont;
			dataGridMain.DefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
			dataGridMain.DefaultCellStyle.SelectionBackColor = ColorTheme.GridSelectedCellColor;
			// Draw form 
			lblStatus1.Text = "Starting...";
			lblStatus2.Text = "";
			lblStatusRowCount.Text = "";
			RefreshFormAfterResize(true);
			InitForm();
			MainTheme.Visible = true;
			Application.DoEvents();
		}

		#region Layout

		class StripRenderer : ToolStripProfessionalRenderer
		{
			public StripRenderer() : base(new Code.StripLayout())
			{
				this.RoundedEdges = false;
			}

			protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
			{
				base.OnRenderItemText(e);
				e.Item.ForeColor = ColorTheme.ToolWhiteToolStrip;
			}
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
				MainTheme.Text = "WoT DBstats - NO PLAYER SELECTED";
			}
			else
			{
				MainTheme.Text = "WoT DBstats - " + Config.Settings.playerName;
			}
		}

		private void SetListener()
		{
			toolItemSettingsRun.Checked = (Config.Settings.run == 1);
			if (Config.Settings.run == 1)
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
				if (Config.Settings.run == 1)
					MainTheme.FormBorderColor = ColorTheme.FormBorderBlue;
				else
					MainTheme.FormBorderColor = ColorTheme.FormBorderRed;
			}
			Refresh();
		}
		#endregion

		#region Data Grid
		
		private enum DataGridType
		{
			None = 0,
			Overall = 1,
			Tank = 2,
			Battle = 3
		}

		private DataGridType DateGridSelected = DataGridType.None;
		
		private void GridShowOverall()
		{
			try
			{
				DateGridSelected = DataGridType.None;
				dataGridMain.DataSource = null;
				if (!Config.CheckDBConn()) return;
				string sql =
					"Select 'Tanks count' as Data, cast(count(id) as varchar) as Value from  dbo.playerTank where playerid=@playerid " +
					"UNION " +
					"SELECT 'Total battles' as Data ,cast( SUM(battles15) + SUM(battles7) as varchar) from dbo.playerTank where playerid=@playerid " +
					"UNION " +
					"SELECT 'Comment' as Data ,'This is an alpha version of a World of Tanks statistic tool - supposed to rule the World (of Tanks) :-)' ";
				sql = sql.Replace("@playerid", Config.Settings.playerId.ToString());
				dataGridMain.DataSource = db.FetchData(sql);
				DateGridSelected = DataGridType.Overall;
				// Text cols
				dataGridMain.Columns["Data"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
				dataGridMain.Columns["Data"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
				dataGridMain.Columns["Value"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
				dataGridMain.Columns["Value"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
				// Finish
				GridResizeOverall();
				GridScrollShowCurPos();
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
			dataGridMain.Columns[1].Width = 900;
		}

		private void GetTankfilter(out string whereSQL, out string Status2Message)
		{
			string tier = "";
			string tierId = "";
			string nation = "";
			string nationId = "";
			string type = "";
			string typeId = "";
			string message = "";
			string sql = "";
			// Calc filter nad set main menu title
			if (tankFilterItemCount == 0)
			{
				toolItemTankFilter.Text = "All Tanks";
				message = "All Tanks";
			}
			else 
			{
				if (toolItemTankFilter_Tier1.Checked)	{tier += "1, ";}
				if (toolItemTankFilter_Tier2.Checked)	{tier += "2, ";}
				if (toolItemTankFilter_Tier3.Checked)	{tier += "3, ";}
				if (toolItemTankFilter_Tier4.Checked)	{tier += "4, ";}
				if (toolItemTankFilter_Tier5.Checked)	{tier += "5, ";}
				if (toolItemTankFilter_Tier6.Checked)	{tier += "6, ";}
				if (toolItemTankFilter_Tier7.Checked)	{tier += "7, ";}
				if (toolItemTankFilter_Tier8.Checked)	{tier += "8, ";}
				if (toolItemTankFilter_Tier9.Checked)	{tier += "9, ";}
				if (toolItemTankFilter_Tier10.Checked)	{tier += "10, ";}
				if (toolItemTankFilter_CountryChina.Checked)	{nation += "China, "; nationId +="3, "; }
				if (toolItemTankFilter_CountryFrance.Checked)	{nation += "France, "; nationId +="4, "; }
				if (toolItemTankFilter_CountryGermany.Checked)	{nation += "Germany, "; nationId +="1, "; }
				if (toolItemTankFilter_CountryUK.Checked )		{nation += "UK, "; nationId +="5, "; }
				if (toolItemTankFilter_CountryUSA.Checked)		{nation += "USA, "; nationId +="2, "; }
				if (toolItemTankFilter_CountryUSSR.Checked )	{nation += "USSR, "; nationId +="0, "; }
				if (toolItemTankFilter_CountryJapan.Checked)    { nation += "Japan, "; nationId +="6, "; }
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
					sql = " tank.tier IN (" + tierId + ") ";
				}
				if (nation.Length > 0)
				{
					nation = "Nation: " + nation.Substring(0, nation.Length - 2) + "   ";
					nationId = nationId.Substring(0, nationId.Length - 2);
					if (sql != "") sql += " AND ";
					sql += " tank.countryId IN (" + nationId + ") ";
				}
				if (type.Length > 0)
				{
					typeId = typeId.Substring(0, typeId.Length - 2);
					type = "Type: " + type.Substring(0, type.Length - 2) + "   ";
					if (sql != "") sql += " AND ";
					sql += " tank.tankTypeId IN (" + typeId + ") ";
				}
				if (sql != "") sql = " AND (" + sql + ") ";
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
			whereSQL = sql;
			Status2Message = message;
		}

		private void GridShowTankInfo(string statusmessage = "")
		{
			DateGridSelected = DataGridType.None;
			dataGridMain.DataSource = null;
			if (!Config.CheckDBConn()) return;
			SqlConnection con = new SqlConnection(Config.DatabaseConnection());
			// Get Tank filter
			string message = "";
			string where = "";
			GetTankfilter(out where, out message);
			string sql =
				"SELECT   dbo.tank.tier AS Tier, dbo.tank.name AS Tank, dbo.tankType.name AS Tanktype, dbo.country.name AS Country, " +
				"         dbo.playerTank.battles15 AS [Battles15], dbo.playerTank.battles7 AS [Battles7], dbo.playerTank.wn8 as WN8, dbo.playerTank.eff as EFF " +
				"FROM    dbo.playerTank INNER JOIN " +
				"         dbo.player ON dbo.playerTank.playerId = dbo.player.id INNER JOIN " +
				"         dbo.tank ON dbo.playerTank.tankId = dbo.tank.id INNER JOIN " +
				"         dbo.tankType ON dbo.tank.tankTypeId = dbo.tankType.id INNER JOIN " +
				"         dbo.country ON dbo.tank.countryId = dbo.country.id " +
				"WHERE   dbo.player.id=@playerid " + where;
			sql = sql.Replace("@playerid", Config.Settings.playerId.ToString());
			dataGridMain.DataSource = db.FetchData(sql);
			DateGridSelected = DataGridType.Tank;
			// Text cols
			dataGridMain.Columns["Tank"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Tank"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Tanktype"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Tanktype"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Country"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Country"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
			// Finish
			GridResizeTankInfo();
			GridScrollShowCurPos();
			// Add status message
			if (statusmessage == "") statusmessage = message;
			SetStatus2(statusmessage);
			lblStatusRowCount.Text = "Rows " + dataGridMain.RowCount.ToString();
		}

		private void GridResizeTankInfo()
		{
			dataGridMain.Columns[0].Width = 35;
			dataGridMain.Columns[1].Width = 120;
			dataGridMain.Columns[2].Width = 100;
			for (int i = 3; i <= 7 ; i++)
			{
				dataGridMain.Columns[i].Width = 70;
			}
		}

		private void GridShowBattle(string overrideStatus2Message = "")
		{
			DateGridSelected = DataGridType.None;
			dataGridMain.DataSource = null;
			if (!Config.CheckDBConn()) return;
			SqlConnection con = new SqlConnection(Config.DatabaseConnection());
			// Create Battlefiler
			string battleFilter = "";
			if (!toolItemBattlesAll.Checked)
			{
				battleFilter = "AND battleTime>='@battleTime' ";
			}
			// Get Tank filter
			string tankFilterMessage = "";
			string tankFilter = "";
			GetTankfilter(out tankFilter, out tankFilterMessage);
			string sql =
				"SELECT CAST(tank.tier AS FLOAT) AS Tier, tank.name AS Tank, battleResult.name as Result, battleSurvive.name as Survived, " +
				"  battle.dmg AS [Damage Caused], battle.dmgReceived AS [Damage Received], CAST(battle.frags AS FLOAT) AS Kills, battle.xp AS XP, CAST(battle.spotted AS FLOAT) AS Detected, " +
				"  CAST(battle.cap AS FLOAT) AS [Capture Points], CAST(battle.def AS FLOAT) AS [Defense Points], CAST(battle.shots AS FLOAT) AS Shots, CAST(battle.hits AS FLOAT) AS Hits, battle.wn8 AS WN8, battle.eff AS EFF, " +
				"  battleResult.color as battleResultColor,  battleSurvive.color as battleSurviveColor, battlescount, battle.battleTime, battle.battleResultId, battle.battleSurviveId, " +
				"  battle.victory, battle.draw, battle.defeat, battle.survived as survivedcount, battle.killed as killedcount, 0 as footer " +
				"FROM    battle INNER JOIN " +
				"        playerTank ON battle.playerTankId = playerTank.id INNER JOIN " +
				"        tank ON playerTank.tankId = tank.id INNER JOIN " +
				"        battleResult ON battle.battleResultId = battleResult.id INNER JOIN " +
				"        battleSurvive ON battle.battleSurviveId = battleSurvive.id " +
				"WHERE   playerTank.playerId=@playerid " + battleFilter + tankFilter + 
				"ORDER BY battle.battleTime DESC ";
			sql = sql.Replace("@playerid", Config.Settings.playerId.ToString());
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
				sql = sql.Replace("@battleTime", dateFilter.ToString("yyyy-MM-dd"));
			}
			DataTable dt = new DataTable();
			dt = db.FetchData(sql);
			// Add footer
			if (dt.Rows.Count > 1)
			{
				sql =
					"SELECT  AVG(CAST(tank.tier AS FLOAT)) AS Tier, " +
					"        'Average on ' + CAST(SUM(battle.battlesCount) AS VARCHAR) + ' battles' AS Tank, " +
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
					"		 GETDATE() AS battleTime, " +
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
				sql = sql.Replace("@playerid", Config.Settings.playerId.ToString());
				if (!toolItemBattlesAll.Checked)
				{
					sql = sql.Replace("@battleTime", dateFilter.ToString("yyyy-MM-dd"));
				}
				dt.Merge(db.FetchData(sql));
				
			}
			// populate datagrid
			dataGridMain.DataSource = dt;
			DateGridSelected = DataGridType.Battle;
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
			// Text cols
			dataGridMain.Columns["Tank"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Result"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Survived"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Tank"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Result"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Survived"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
			
			// Finish up
			GridResizeBattle();
			GridScrollShowCurPos();
			toolItemBattles.Visible = true;
			if (overrideStatus2Message == "")
				SetStatus2(toolItemBattles.Text + "   " + tankFilterMessage);
			else
				SetStatus2(overrideStatus2Message);
			lblStatusRowCount.Text = "Rows " + dataGridMain.RowCount.ToString() ;
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
			if (DateGridSelected == DataGridType.Battle)
			{
				bool footer = (Convert.ToInt32(dataGridMain["footer", e.RowIndex].Value) == 1);
				string col = dataGridMain.Columns[e.ColumnIndex].Name;
				if (col.Equals("Tank"))
				{
				DataGridViewCell cell = dataGridMain[e.ColumnIndex, e.RowIndex];
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
					DataGridViewCell cell = dataGridMain[e.ColumnIndex, e.RowIndex];
					string battleResultColor = dataGridMain["battleResultColor", e.RowIndex].Value.ToString();
					cell.Style.ForeColor = System.Drawing.ColorTranslator.FromHtml(battleResultColor);
					int battlesCount = Convert.ToInt32(dataGridMain["battlescount", e.RowIndex].Value);
					if (battlesCount > 1)
				{
						cell.ToolTipText = "Victory: " + dataGridMain["victory", e.RowIndex].Value.ToString() + Environment.NewLine +
							"Draw: " + dataGridMain["draw", e.RowIndex].Value.ToString() + Environment.NewLine +
							"Defeat: " + dataGridMain["defeat", e.RowIndex].Value.ToString() ;
				}
			}
			// Survived color and formatting
				else if (col.Equals("Survived"))
			{
				DataGridViewCell cell = dataGridMain[e.ColumnIndex, e.RowIndex];
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
					DataGridViewCell cell = dataGridMain[e.ColumnIndex, e.RowIndex];
					if (col == "Tier" || col == "Kills" || col == "Detected" || col == "Shots" || col == "Hits" || col == "Capture Points" || col == "Defense Points")
				{
						cell.Style.Format = "n1";
				}

				}
			}
		}

		#endregion

		#region Scroll Grid

		private bool scrolling = false;
		private Point moveFromPoint;
		private int scrollY;

		private void GridScrollGoToPos()
		{
			try
			{
				// Calc position
				double scrollMax = panelScrollArea.Height - panelScrollbar.Height - 8;
				if (scrollMax > 0)
				{
					double scrollPos = panelScrollbar.Top - 4;
					// Move datagrid
					double rowcount = dataGridMain.RowCount - dataGridMain.DisplayedRowCount(false);
					// Move to position
					int pos = Convert.ToInt32(rowcount * (scrollPos / scrollMax));
					dataGridMain.FirstDisplayedScrollingRowIndex = pos;
				}
				else
				{ 
					scrolling = false; 
				}
			}
			catch (Exception ex)
			{
				Code.MsgBox.Show("Error when trying to scroll the grid, might be caused by empty datagrid (missing data connection)." + Environment.NewLine + Environment.NewLine + ex.Message, "Error scrolling");
			}

		}

		private void GridScrollShowCurPos()
		{
			// Pos in datagrid
			double rowcount = dataGridMain.RowCount;
			double gridrowcount = dataGridMain.DisplayedRowCount(false);
			double gridpos = dataGridMain.FirstDisplayedScrollingRowIndex / (rowcount - gridrowcount);
			int scrollMax = panelScrollArea.Height - 8; // Calc max height of scrollbar
			int scrollheight = 30; // Default height - minimum size
			// Calc scroll height
			if (rowcount <= gridrowcount) // Visible area > content = No scrolling
			{
				scrollheight = scrollMax;
			}
			else // Visible area to small for show all - calc scrollbar height now
			{
				double scrollheigthfactor = rowcount / gridrowcount;
				scrollheight = Convert.ToInt32(scrollMax / scrollheigthfactor);
				if (scrollheight < 30) scrollheight = 30;
			}
			// Calc scroll pos
			int scrollMaxArea = panelScrollArea.Height - scrollheight - 8; // Calc max height of scrollbar
			int scrollpos = 4; // Top
			if (rowcount > gridrowcount) // Visible area < content = scrolling)
			{
				scrollpos = Convert.ToInt32(gridpos * scrollMaxArea);
				if (scrollpos < 4) scrollpos = 4;
				if (scrollpos > scrollMaxArea + 4) scrollpos = Convert.ToInt32(scrollMaxArea + 4);
			}
			// Move to position and set height
			panelScrollbar.Top = scrollpos;
			panelScrollbar.Height = scrollheight;
		}

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
				GridScrollShowCurPos();                
			}
			catch (Exception)
			{
				// throw;
			}
			
		}

		private void pnlScrollbar_MouseHover(object sender, EventArgs e)
		{
			panelScrollbar.BackColor = ColorTheme.ToolGrayScrollbarHover; 
		}

		private void pnlScrollbar_MouseLeave(object sender, EventArgs e)
		{
			panelScrollbar.BackColor = ColorTheme.ToolGrayScrollbar;
		}

		private void pnlScrollbar_MouseDown(object sender, MouseEventArgs e)
		{
			panelScrollbar.BackColor = ColorTheme.ToolGrayCheckPressed;
			scrolling = true;
			moveFromPoint = Cursor.Position;
			scrollY = panelScrollbar.Top;
		}

		private void pnlScrollbar_MouseUp(object sender, MouseEventArgs e)
		{
			panelScrollbar.BackColor = ColorTheme.ToolGrayScrollbar;
			scrolling = false;
		}

		private void pnlScrollbar_MouseMove(object sender, MouseEventArgs e)
		{
			if (scrolling)
			{
				Point dif = Point.Subtract(Cursor.Position, new Size(moveFromPoint));
				int t = scrollY + dif.Y;
				if (t >= 4 && t <= panelScrollArea.Height - panelScrollbar.Height -4)
					panelScrollbar.Top = t;
				GridScrollGoToPos();
			}
		}

		#endregion

		#region Form Init and Form Resize

		private void InitForm()
		{
			panelMainArea.Left = MainTheme.MainArea.Left;
			panelMainArea.Top = MainTheme.MainArea.Top;
			panelGrid.Left = 0;
			panelGrid.Top = panelInfo.Height;
			dataGridMain.MouseWheel += new MouseEventHandler(dataGridMain_MouseWheel);
		}

		private void RefreshFormAfterResize(bool notrefreshgrid = false)
		{
			// Set Form border color
			SetFormBorder(); 
			// Set Main Area Panel
			panelMainArea.Width = MainTheme.MainArea.Width;
			panelMainArea.Height = MainTheme.MainArea.Height;
			// Set grid panel position and size
			panelGrid.Width = panelMainArea.Width;
			panelGrid.Height = panelMainArea.Height - panelInfo.Height;
			// Set grid width
			dataGridMain.Width = panelMainArea.Width - 20; // room for scrollbar
			// Grid
			if (!notrefreshgrid)
			{
				// Update grid scroll pos
				GridScrollShowCurPos(); 
			}
		}

		private void MainTheme_Resize(object sender, EventArgs e)
		{
			RefreshFormAfterResize();
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
			}
			else if (panelInfo.Height + infoPanelSlideSpeed > panelInfoMaxSize)
			{
				panelInfo.Height = panelInfoMaxSize;
				timerPanelSlide.Enabled = false;
			}
			else
			{
				panelInfo.Height += infoPanelSlideSpeed;
			}
				
			// Set grid panel height
			panelGrid.Top = panelInfo.Height;
			panelGrid.Height = panelMainArea.Height - panelInfo.Height;
			GridScrollShowCurPos();
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

		private void RefreshCurrentGrid()
		{
			if (toolItemViewOverall.Checked)
			{
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

		private void toolItemViewSelected_Click(object sender, EventArgs e)
		{
			ToolStripButton menuItem = (ToolStripButton)sender;
			if (!menuItem.Checked)
			{
				toolItemViewOverall.Checked = false;
				toolItemViewBattles.Checked = false;
				toolItemViewTankInfo.Checked = false;
				toolItemBattles.Visible = false;
				toolItemTankFilter.Visible = false;
				toolItemRefreshSeparator.Visible = true;
				menuItem.Checked = true;
				SetStatus2(menuItem.Text);
				if (toolItemViewOverall.Checked)
				{
					toolItemRefreshSeparator.Visible = false;
					InfoPanelSlideStart(true);
					GridShowOverall();
				}
				else if (toolItemViewTankInfo.Checked)
				{
					InfoPanelSlideStart(false);
					toolItemTankFilter.Visible = true;
					GridShowTankInfo();
				}
				else if (toolItemViewBattles.Checked)
				{
					InfoPanelSlideStart(false);
					toolItemBattles.Visible = true;
					toolItemTankFilter.Visible = true;
					GridShowBattle();
					fileSystemWatcherNewBattle.EnableRaisingEvents = true;
				}
			}
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

		private void toolItemTankFilter_Uncheck(bool tier, bool country, bool type)
		{
			if (tier)
			{
				toolItemTankFilter_All.Checked = true;
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
			toolItemTankFilter_All.Checked = (tankFilterItemCount == 0);
			// Reopen menu item
			this.toolItemTankFilter.ShowDropDown();
			// Refresh grid
			RefreshCurrentGrid();
		}

		private void toolItemTankFilter_All_Click(object sender, EventArgs e)
		{
			toolItemTankFilter_Uncheck(true, true, true);
		}

		private void toolItemTankFilterSelected(ToolStripMenuItem menuItem, ToolStripMenuItem parentMenuItem)
		{
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
			RefreshCurrentGrid();
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
			string message = "";
			GetTankfilter(out where, out message);
			if (toolItemViewBattles.Checked)
				SetStatus2(toolItemBattles.Text + "   " + message);
			else
				SetStatus2(message);
		}

		private void toolItemTankFilter_Country_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				toolItemTankFilter_Uncheck(false, true, false);
			}
		}

		private void toolItemTankFilter_Type_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				toolItemTankFilter_Uncheck(false, false, true);
			}
		}

		private void toolItemTankFilter_Tier_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				toolItemTankFilter_Uncheck(true, false, false);
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
		}

		private void toolItemSettingsDb_Click(object sender, EventArgs e)
		{
			
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
			string msg = "WoT DBstat version " + AssemblyVersion + Environment.NewLine + Environment.NewLine +
						 "Statistics Tool for World Of Tanks" + Environment.NewLine + Environment.NewLine +
						 "Created by: BadButton and cmdrTrinity";
			Code.MsgBox.Show(msg, "About WoT DBstat");
		}

		private void toolItemSettingsRun_Click(object sender, EventArgs e)
		{
			toolItemSettingsRun.Checked = !toolItemSettingsRun.Checked;
			// Set Start - Stop button properties
			if (toolItemSettingsRun.Checked)
			{
				Config.Settings.run = 1;
			}
			else
			{
				Config.Settings.run = 0;
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
			Form frm = new Forms.Reports.DBTable();
			frm.Show();
		}

		private void toolItemImportBattlesFromWotStat_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.File.ImportWotStat();
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

		private void SetDefaultCursor(object sender, MouseEventArgs e)
		{
			MainTheme.Cursor = Cursors.Default;
		}

		#endregion

		private void Main_Shown(object sender, EventArgs e)
		{
			// Startup settings
			string statusmsg = "Application started with issues...";
			string msg = "";
			bool ok = Config.GetConfig(out msg);
			if (!ok)
			{
				Code.MsgBox.Show(msg, "Could not load config data");
				lblOverView.Text = "";
				Config.Settings.run = 0;
				SetListener();
				Form frm = new Forms.File.ApplicationSetting();
				frm.ShowDialog();
			}
			else if (Config.CheckDBConn())
			{
				string result = dossier2json.UpdateDossierFileWatcher();
				SetListener();
				SetFormTitle();
				// Init
				TankData.GetTankListFromDB();
				TankData.GetJson2dbMappingViewFromDB();
				TankData.GettankData2BattleMappingViewFromDB();
				statusmsg = "Welcome back " + Config.Settings.playerName;
				// Show data
				lblOverView.Text = "Welcome back " + Config.Settings.playerName;
				GridShowOverall();
			}
			SetListener();
			// Battle result file watcher
			fileSystemWatcherNewBattle.Path = Path.GetDirectoryName(Log.BattleResultDoneLogFileName());
			fileSystemWatcherNewBattle.Filter = Path.GetFileName(Log.BattleResultDoneLogFileName());
			fileSystemWatcherNewBattle.NotifyFilter = NotifyFilters.LastWrite;
			fileSystemWatcherNewBattle.Changed += new FileSystemEventHandler(NewBattleFileChanged);
			fileSystemWatcherNewBattle.EnableRaisingEvents = false;
			// Display form and status message 
			SetStatus2(statusmsg);
		}


	}

	

}
