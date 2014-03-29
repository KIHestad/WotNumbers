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
			// Style
			toolMain.Renderer = new StripRenderer();
			toolMain.BackColor = Code.Support.StripLayout.colorGrayMain;
			toolBattle.Renderer = new StripRenderer();
			toolBattle.BackColor = Code.Support.StripLayout.colorGrayMain;
			menuMain.Renderer = new StripRenderer();
			menuMain.BackColor = Code.Support.StripLayout.colorGrayMain;

			panelTop.BackColor = Code.Support.StripLayout.colorGrayMain;
			toolMain.ShowItemToolTips = false;
			Config.Settings.run = 0;
			SetListener();
			// Size
			InitForm();
			RefreshFormAfterResize(true);
			// Startup settings
			string statusmsg = "Application started with issues...";
			string msg = Config.GetConfig();
			if (msg != "") 
			{
				Code.Support.Message.Show(msg,"Could not load config data");
				lblOverView.Text = "Please check app and db settings...";
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
			SetStatus2(statusmsg);
			// Battle result file watcher
			fileSystemWatcherNewBattle.Path = Path.GetDirectoryName(Log.BattleResultDoneLogFileName());
			fileSystemWatcherNewBattle.Filter = Path.GetFileName(Log.BattleResultDoneLogFileName());
			fileSystemWatcherNewBattle.NotifyFilter = NotifyFilters.LastWrite;
			fileSystemWatcherNewBattle.Changed += new FileSystemEventHandler(NewBattleFileChanged);
			fileSystemWatcherNewBattle.EnableRaisingEvents = false;
		}

		#region Layout

		class StripRenderer : ToolStripProfessionalRenderer
		{
			public StripRenderer() : base(new Code.Support.StripLayout())
			{
				this.RoundedEdges = false;
			}
			
			protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
			{
				base.OnRenderItemText(e);
				e.Item.ForeColor = Code.Support.StripLayout.colorWhiteToolStrip;
			}
		}

		private void panelMaster_Paint(object sender, PaintEventArgs e)
		{
			// Default Border Color
			Color borderColor = Color.Black;
			// Border color according to run state
			if (this.WindowState != FormWindowState.Maximized)
			{
				if (Config.Settings.run == 1)
					borderColor = Code.Support.StripLayout.colorBlueSelectedButton;
				else
					borderColor = System.Drawing.Color.DarkRed;
			}
			ControlPaint.DrawBorder(e.Graphics, this.panelMaster.ClientRectangle, borderColor, ButtonBorderStyle.Solid);
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
				lblTitle.Text = "WoT DBstats - NO PLAYER SELECTED";
			}
			else
			{
				lblTitle.Text = "WoT DBstats - " + Config.Settings.playerName;
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
			Refresh();
			SetStatus2(result);
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
			DateGridSelected = DataGridType.None;
			dataGridMain.DataSource = null;
			if (!Config.CheckDBConn()) return;
			SqlConnection con = new SqlConnection(Config.DatabaseConnection());
			string sql =
				"Select 'Tanks count' as Data, cast(count(id) as varchar) as Value from  dbo.playerTank where playerid=@playerid " +
				"UNION " +
				"SELECT 'Total battles' as Data ,cast( SUM(battles15) + SUM(battles7) as varchar) from dbo.playerTank where playerid=@playerid " +
				"UNION " +
				"SELECT 'Comment' as Data ,'This is an alpha version of a World of Tanks statistic tool - supposed to rule the World (of Tanks) :-)' ";
				
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.Parameters.AddWithValue("@playerid", Config.Settings.playerId);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			dataGridMain.DataSource = dt;
			DateGridSelected = DataGridType.Overall;
			// Text cols
			dataGridMain.Columns["Data"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Data"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Value"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridMain.Columns["Value"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
			// Finish
			GridResizeOverall();
			GridScrollShowCurPos();
		}

		private void GridResizeOverall()
		{
			dataGridMain.Columns[0].Width = 100;
			dataGridMain.Columns[1].Width = 900;
		}

		private void GridShowTankInfo()
		{
			DateGridSelected = DataGridType.None;
			dataGridMain.DataSource = null;
			if (!Config.CheckDBConn()) return;
			SqlConnection con = new SqlConnection(Config.DatabaseConnection());
			string sql =
				"SELECT   dbo.tank.tier AS Tier, dbo.tank.name AS Tank, dbo.tankType.name AS Tanktype, dbo.country.name AS Country, " +
				"         dbo.playerTank.battles15 AS [Battles15], dbo.playerTank.battles7 AS [Battles7], dbo.playerTank.wn8 as WN8, dbo.playerTank.eff as EFF " +
				"FROM    dbo.playerTank INNER JOIN " +
				"         dbo.player ON dbo.playerTank.playerId = dbo.player.id INNER JOIN " +
				"         dbo.tank ON dbo.playerTank.tankId = dbo.tank.id INNER JOIN " +
				"         dbo.tankType ON dbo.tank.tankTypeId = dbo.tankType.id INNER JOIN " +
				"         dbo.country ON dbo.tank.countryId = dbo.country.id " +
				"WHERE   dbo.player.id=@playerid ";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.Parameters.AddWithValue("@playerid", Config.Settings.playerId);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			dataGridMain.DataSource = dt;
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

		private void GridShowBattle(string statusmessage = "")
		{
			DateGridSelected = DataGridType.None;
			dataGridMain.DataSource = null;
			if (!Config.CheckDBConn()) return;
			SqlConnection con = new SqlConnection(Config.DatabaseConnection());
			string battleFilter = "";
			if (!toolBattleFilterAll.Checked)
			{
				battleFilter = "AND battleTime>=@battleTime ";
			}
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
				"WHERE   playerTank.playerId=@playerid " + battleFilter +
				"ORDER BY battle.battleTime DESC ";
				
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.Parameters.AddWithValue("@playerid", Config.Settings.playerId);
			if (!toolBattleFilterAll.Checked)
			{
				DateTime basedate = DateTime.Now;
				if (DateTime.Now.Hour < 5) basedate = DateTime.Now.AddDays(-1); // correct date according to server reset 05:00
				DateTime dateFilter = new DateTime(basedate.Year, basedate.Month, basedate.Day, 5, 0, 0); 
				// Adjust time scale according to selected filter
				if (toolBattleFilter3days.Checked) dateFilter = DateTime.Now.AddDays(-3);
				else if (toolBattleFilterWeek.Checked) dateFilter = DateTime.Now.AddDays(-7);
				else if (toolBattleFilterMonth.Checked) dateFilter = DateTime.Now.AddMonths(-1);
				else if (toolBattleFilterYear.Checked) dateFilter = DateTime.Now.AddYears(-1);
				cmd.Parameters.AddWithValue("@battleTime", dateFilter);
			}
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			// Add footer
			if (dt.Rows.Count > 1)
			{
				sql =
					"SELECT  AVG(CAST(tank.tier AS FLOAT)) AS Tier, " +
					"        'Average on ' + CAST(SUM(battle.battlesCount) AS VARCHAR) + ' battles' AS Tank, " +
					"        CAST(ROUND(SUM(CAST(battle.victory AS FLOAT)) / SUM(battle.battlesCount) * 100, 1) AS VARCHAR) + '%' AS Result, " +
					"        CAST(ROUND(SUM(CAST(battle.survived AS FLOAT)) / SUM(battle.battlesCount) * 100, 1) AS VARCHAR) + '%' AS Survived, " +
					"        AVG(CAST(battle.dmg AS FLOAT)) AS [Damage Caused], " +
					"        AVG(CAST(battle.dmgReceived AS FLOAT)) AS [Damage Received], " +
					"        AVG(CAST(battle.frags AS FLOAT)) AS Kills, " +
					"        AVG(CAST(battle.xp AS FLOAT)) AS XP, " +
					"        AVG(CAST(battle.spotted AS FLOAT)) AS Detected," +
					"		 AVG(CAST(battle.cap AS FLOAT)) AS [Capture Points], " +
					"		 AVG(CAST(battle.def AS FLOAT)) AS [Defense Points], " +
					"		 AVG(CAST(battle.shots AS FLOAT)) AS Shots, " +
					"		 AVG(CAST(battle.hits AS FLOAT)) AS Hits, " +
					"		 AVG(CAST(battle.wn8 AS FLOAT)) AS WN8, " +
					"		 AVG(CAST(battle.eff AS FLOAT)) AS EFF, " +
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
					"WHERE   playerTank.playerId=@playerid " + battleFilter;
				cmd = new SqlCommand(sql, con);
				cmd.Parameters.AddWithValue("@playerid", Config.Settings.playerId);
				if (!toolBattleFilterAll.Checked)
				{
					DateTime basedate = DateTime.Now;
					if (DateTime.Now.Hour < 5) basedate = DateTime.Now.AddDays(-1); // correct date according to server reset 05:00
					DateTime dateFilter = new DateTime(basedate.Year, basedate.Month, basedate.Day, 5, 0, 0);
					// Adjust time scale according to selected filter
					if (toolBattleFilter3days.Checked) dateFilter = DateTime.Now.AddDays(-3);
					else if (toolBattleFilterWeek.Checked) dateFilter = DateTime.Now.AddDays(-7);
					else if (toolBattleFilterMonth.Checked) dateFilter = DateTime.Now.AddMonths(-1);
					else if (toolBattleFilterYear.Checked) dateFilter = DateTime.Now.AddYears(-1);
					cmd.Parameters.AddWithValue("@battleTime", dateFilter);
				}
				cmd.CommandType = CommandType.Text;
				da = new SqlDataAdapter(cmd);
				da.Fill(dt);
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
			toolBattle.Visible = true;
			if (statusmessage == "") statusmessage = "Selected view: Battle - Filter: " + toolBattleFilter.Text;
			SetStatus2(statusmessage);
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
						dataGridMain.Rows[e.RowIndex].DefaultCellStyle.BackColor = Code.Support.StripLayout.colorGrayDropDownBack;
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

		#region Move Form

		private bool dragging = false;
		private Point dragCursorPoint;
		private Point dragFormPoint;

		private void panelTop_MouseDown(object sender, MouseEventArgs e)
		{
			dragging = true;
			dragCursorPoint = Cursor.Position;
			dragFormPoint = this.Location;
		}

		private void panelTop_MouseMove(object sender, MouseEventArgs e)
		{
			if (dragging)
			{
				Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
				this.Location = Point.Add(dragFormPoint, new Size(dif));
			}
		}

		private void panelTop_MouseUp(object sender, MouseEventArgs e)
		{
			dragging = false;
		}

		
		private void Main_Resize(object sender, EventArgs e)
		{
			panelTop.Width = panelMaster.Width - 2;
			panelMain.Width = panelMaster.Width - 2;
			panelMain.Height = panelMain.Height - panelTop.Height - 2;
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
				Code.Support.Message.Show("Error when trying to scroll the grid, might be caused by empty datagrid (missing data connection)." + Environment.NewLine + Environment.NewLine + ex.Message, "Error scrolling");
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
			panelScrollbar.BackColor = Code.Support.StripLayout.colorGrayScrollbarHover; 
		}

		private void pnlScrollbar_MouseLeave(object sender, EventArgs e)
		{
			panelScrollbar.BackColor = Code.Support.StripLayout.colorGrayScrollbar;
		}

		private void pnlScrollbar_MouseDown(object sender, MouseEventArgs e)
		{
			panelScrollbar.BackColor = Code.Support.StripLayout.colorGrayCheckPressed;
			scrolling = true;
			moveFromPoint = Cursor.Position;
			scrollY = panelScrollbar.Top;
		}

		private void pnlScrollbar_MouseUp(object sender, MouseEventArgs e)
		{
			panelScrollbar.BackColor = Code.Support.StripLayout.colorGrayScrollbar;
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

		#region Form Init and Resize

		private void InitForm()
		{
			// Title bar
			panelTop.Left = 1;
			panelTop.Top = 1;
			// Panel strip - holds menu / toolbar
			panelStrip.Left = 1;
			panelStrip.Top = 1 + panelTop.Height;
			panelStrip.Height = 26;
			// Main Toolbar
			toolMain.Top = 0;
			toolMain.Left = 12; // margin to left - center below icon
			// Battle Toolbar
			toolBattle.Top = 0;
			toolBattle.Left = toolMain.Left + toolMain.Width;
			toolBattle.Visible = false;
			// Panel info - showing picture / welcome
			panelInfo.Left = 1;
			panelInfo.Top = panelStrip.Top + panelStrip.Height;
			// Main Area including grid
			panelMain.Left = 1;
			panelMain.Top = panelInfo.Top + panelInfo.Height;
			// Status bar
			panelStatus.Left = 1;
			// Datagrid init size
			dataGridMain.Width = panelMain.Width - 20; // room for scrollbar
			// Mouse wheel handle grid
			dataGridMain.MouseWheel += new MouseEventHandler(dataGridMain_MouseWheel);
		}

		private void RefreshFormAfterResize(bool notrefreshgrid = false)
		{
			// Title bar form handling with min, max, close buttons
			int fullWidth = panelMaster.Width - 2; // remove for border on panelMaster
			panelTop.Width = fullWidth;
			picClose.Left = panelMain.Width - picClose.Width;
			picNormalize.Left = picClose.Left - picNormalize.Width;
			picMinimize.Left = picNormalize.Left - picMinimize.Width;
			// Strip panel holding toolbar/menu
			panelStrip.Width = fullWidth;
			// Info panel with picture
			panelInfo.Width = fullWidth;
			// Status panel at bottom, with resize button
			panelStatus.Top = panelMaster.Height - panelStatus.Height - 1;
			panelStatus.Width = fullWidth;
			picResize.Left = panelStatus.Width - picResize.Width;
			picResize.Visible = (this.WindowState != FormWindowState.Maximized);
			// Main panel with grid
			panelMain.Width = fullWidth;
			panelMain.Height = panelMaster.Height - panelMain.Top - panelStatus.Height - 2;
			// Grid
			if (!notrefreshgrid)
			{
				dataGridMain.Width = panelMain.Width - 20; // room for scrollbar
				// Update grid scroll pos
				GridScrollShowCurPos(); 
			}
			
		}

		private bool moving = false;
		private int formX;
		private int formY;
		
		private void picResize_MouseDown(object sender, MouseEventArgs e)
		{
			moving = true;
			moveFromPoint = Cursor.Position;
			formX = Main.ActiveForm.Width;
			formY = Main.ActiveForm.Height;
		}

		private void picResize_MouseMove(object sender, MouseEventArgs e)
		{
			if (moving)
			{
				Point dif = Point.Subtract(Cursor.Position, new Size(moveFromPoint));
				if (formX + dif.X > 300) Main.ActiveForm.Width = formX + dif.X;
				if (formY + dif.Y > 150) Main.ActiveForm.Height = formY + dif.Y;
				RefreshFormAfterResize(true);
			}
		}

		private void picResize_MouseUp(object sender, MouseEventArgs e)
		{
			moving = false;
			RefreshFormAfterResize();
		}

		private void picResize_MouseHover(object sender, EventArgs e)
		{
			this.Cursor = Cursors.SizeNWSE;
		}

		private void picResize_MouseLeave(object sender, EventArgs e)
		{
			this.Cursor = Cursors.Default;
		}

		private void picClose_MouseHover(object sender, EventArgs e)
		{
			picClose.BackColor = Code.Support.StripLayout.colorGrayHover;
		}

		private void picClose_MouseLeave(object sender, EventArgs e)
		{
			picClose.BackColor = Code.Support.StripLayout.colorGrayMain;
		}

		private void picNormalize_MouseHover(object sender, EventArgs e)
		{
			picNormalize.BackColor = Code.Support.StripLayout.colorGrayHover;
		}

		private void picNormalize_MouseLeave(object sender, EventArgs e)
		{
			picNormalize.BackColor = Code.Support.StripLayout.colorGrayMain;
		}

		private void picMinimize_MouseHover(object sender, EventArgs e)
		{
			picMinimize.BackColor = Code.Support.StripLayout.colorGrayHover;
		}

		private void picMinimize_MouseLeave(object sender, EventArgs e)
		{
			picMinimize.BackColor = Code.Support.StripLayout.colorGrayMain;
		}

		private void picClose_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void picNormalize_Click(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Normal)
			{
				this.WindowState = FormWindowState.Maximized;
			}
			else
			{
				this.WindowState = FormWindowState.Normal;
			}
			RefreshFormAfterResize();
			Refresh();
		}

		private void picMinimize_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
			Refresh();
		}

		#endregion
		
		#region Panel Info - Slider Events

		private int panelInfoSlideSpeed;
		
		private void panelInfoSlideStart(bool show)
		{
			if (show)
			{
				panelInfoSlideSpeed = 4;
				panelInfo.Visible = true;
				panelMain.Height = panelMaster.Height - panelInfo.Top - panelInfo.Height - panelStatus.Height - 2;
			}
			else if (!show)
			{
				panelInfoSlideSpeed = -4;
				panelMain.Height = panelMaster.Height - panelInfo.Top - panelStatus.Height - 2;        
			}
			timerPanelSlide.Enabled = true;
		}

		private void timerPanelSlide_Tick(object sender, EventArgs e)
		{
			// Expand or collapse panel
			int panelInfoMaxSize = 72;
			if (panelInfo.Height + panelInfoSlideSpeed < 0)
			{
				panelInfo.Height = 0;
				timerPanelSlide.Enabled=false;
			}
			else if (panelInfo.Height + panelInfoSlideSpeed > panelInfoMaxSize)
			{
				panelInfo.Height = panelInfoMaxSize;
				timerPanelSlide.Enabled = false;
			}
			else
				panelInfo.Height += panelInfoSlideSpeed;
			// Move sub leves togheter with panel
			panelMain.Top = panelInfo.Top + panelInfo.Height;
			panelMain.Height = panelMaster.Height - panelMain.Top - panelStatus.Height - 2;
			GridScrollShowCurPos();
		}

		#endregion       
	 
		#region Toolstrip Events
		
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

		private void toolItemViewOverall_Click(object sender, EventArgs e)
		{
			if (toolItemViewOverall.Checked) return; // quit if this view is already selected
			SetStatus2("Selected view: Overall");
			string s = Config.Settings.playerName;
			if (s == "") 
				s = "Check app settings, missing player name";
			else
				s = "Statistic Overview for " + s;
			lblOverView.Text = s;
			panelInfoSlideStart(true);
			toolItemViewTankInfo.Checked = false;
			toolItemViewBattles.Checked = false;
			toolItemViewOverall.Checked = true;
			fileSystemWatcherNewBattle.EnableRaisingEvents = false;
			GridShowOverall();
		}
		
		private void toolItemViewTankInfo_Click(object sender, EventArgs e)
		{
			if (toolItemViewTankInfo.Checked) return; // quit if this view is already selected
			SetStatus2("Selected view: Tanks");
			panelInfoSlideStart(false);
			toolItemViewOverall.Checked = false;
			toolItemViewBattles.Checked = false;
			toolItemViewTankInfo.Checked = true;
			fileSystemWatcherNewBattle.EnableRaisingEvents = false;
			GridShowTankInfo();
		}

		private void toolItenViewBattles_Click(object sender, EventArgs e)
		{
			if (toolItemViewBattles.Checked) return; // quit if this view is already selected
			panelInfoSlideStart(false);
			toolItemViewOverall.Checked = false;
			toolItemViewTankInfo.Checked = false;
			toolItemViewBattles.Checked = true;
			fileSystemWatcherNewBattle.EnableRaisingEvents = true;
			GridShowBattle();
		}
		
		private void toolItemSettingsApp_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.File.ApplicationSetting();
			frm.ShowDialog();
			SetFormTitle();
		}

		private void toolItemSettingsDb_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.File.DatabaseSetting();
			frm.ShowDialog();
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
						 "Tool for getting data from WoT dossier file to MS SQL Database" + Environment.NewLine + Environment.NewLine +
						 "Created by: BadButton and cmdrTrinity";
			Code.Support.Message.Show(msg, "About WoT DBstat");
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
			Config.SaveAppConfig(out msg);
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

		private void ToolBatteFilterClear()
		{
			toolBattleFilterToday.Checked = false;
			toolBattleFilter3days.Checked = false;
			toolBattleFilterWeek.Checked = false;
			toolBattleFilterMonth.Checked = false;
			toolBattleFilterYear.Checked = false;
			toolBattleFilterAll.Checked = false;
		}

		private void toolBattleFilterToday_Click(object sender, EventArgs e)
		{
			ToolBatteFilterClear();
			toolBattleFilterToday.Checked = true;
			toolBattleFilter.Text = toolBattleFilterToday.Text;
			GridShowBattle();
		}

		private void toolBattleFilter3days_Click(object sender, EventArgs e)
		{
			ToolBatteFilterClear();
			toolBattleFilter3days.Checked = true;
			toolBattleFilter.Text = toolBattleFilter3days.Text;
			GridShowBattle();
		}

		private void toolBattleFilterWeek_Click(object sender, EventArgs e)
		{
			ToolBatteFilterClear();
			toolBattleFilterWeek.Checked = true;
			toolBattleFilter.Text = toolBattleFilterWeek.Text;
			GridShowBattle();
		}

		private void toolBattleFilterMonth_Click(object sender, EventArgs e)
		{
			ToolBatteFilterClear();
			toolBattleFilterMonth.Checked = true;
			toolBattleFilter.Text = toolBattleFilterMonth.Text;
			GridShowBattle();
		}

		private void toolBattleFilterYear_Click(object sender, EventArgs e)
		{
			ToolBatteFilterClear();
			toolBattleFilterYear.Checked = true;
			toolBattleFilter.Text = toolBattleFilterYear.Text;
			GridShowBattle();
		}

		private void toolBattleFilterAll_Click(object sender, EventArgs e)
		{
			ToolBatteFilterClear();
			toolBattleFilterAll.Checked = true;
			toolBattleFilter.Text = toolBattleFilterAll.Text;
			GridShowBattle();
		}

		#endregion

		#region Menu Item -> TESTING

		
		private void menuItemTest_ImportTank_Wn8exp_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.File.ImportTank();
			frm.ShowDialog();
		}

		private void menuItemTest_ImportTurret_Click(object sender, EventArgs e)
		{
			string s = Modules2DB.ImportTurrets();
			Code.Support.Message.Show(s);
		}

		private void menuItemTest_ImportGun_Click(object sender, EventArgs e)
		{
			string s = Modules2DB.ImportGuns();
			Code.Support.Message.Show(s);
		}

		private void menuItemTest_ImportRadio_Click(object sender, EventArgs e)
		{
			string s = Modules2DB.ImportRadios();
			Code.Support.Message.Show(s);
		}

		private void menuItemTest_WotURL_Click(object sender, EventArgs e)
		{
			string lcUrl = "https://api.worldoftanks.eu/wot/encyclopedia/tankinfo/?application_id=2a70055c41b7a6fff1e35a3ba9cadbf1&tank_id=49";
			HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(lcUrl);

			httpRequest.Timeout = 10000;     // 10 secs
			httpRequest.UserAgent = "Code Sample Web Client";

			HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
			StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());

			string content = responseStream.ReadToEnd();
			Code.Support.Message.Show(content);
		}

		private void menuItemTest_ProgressBar_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.Test.TestProgressBar();
			frm.Show();
		}

		private void menuItemTest_ViewRange_Click(object sender, EventArgs e)
		{
			string vr = ViewRange.CalcViewRange().ToString();
			Code.Support.Message.Show(vr,"Test calc view range");
		}

		private void viewRangeFormToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.Test.ViewRange();
			frm.ShowDialog();
		}

		
		#endregion

		
	}

	

}
