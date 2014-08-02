using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using WinApp.Code;
using System.ComponentModel;

namespace WinApp.Forms
{
	public partial class Main : Form
	{
		#region Init Content and Layout

		private bool Init = true;
		private bool LoadConfigOK = true;
		private string LoadConfigMsg = "";
		private ConfigData.PosSize mainFormPosSize = new ConfigData.PosSize();
		
		public Main()
		{
			InitializeComponent();
			// Get Config
			LoadConfigOK = Config.GetConfig(out LoadConfigMsg);
			mainFormPosSize = Config.Settings.posSize;
		}

		// To be able to minimize from task bar
		const int WS_MINIMIZEBOX = 0x20000;
		const int CS_DBLCLKS = 0x8;
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.Style |= WS_MINIMIZEBOX;
				cp.ClassStyle |= CS_DBLCLKS;
				return cp;
			}
		}

		private void Main_Load(object sender, EventArgs e)
		{
			// Get PosSize
			if (mainFormPosSize != null)
			{
				this.Top = mainFormPosSize.Top;
				this.Left = mainFormPosSize.Left;
				this.Width = mainFormPosSize.Width;
				this.Height = mainFormPosSize.Height;
			}
			// Statusbar text
			lblStatus1.Text = "";
			lblStatus2.Text = "Application starting...";
			lblStatusRowCount.Text = "";
			// Log startup
			Code.Log.LogToFile("Application startup", true);
			// Make sure borderless form do not cover task bar when maximized
			Screen screen = Screen.FromControl(this);
			this.MaximumSize = screen.WorkingArea.Size;
			// Black Border on loading
			MainTheme.FormBorderColor = ColorTheme.FormBorderBlack;
			// Resize Form Theme Title Area to fit 125% or 150% font size in Win
			System.Drawing.Graphics graphics = this.CreateGraphics(); 
			Double dpiFactor = graphics.DpiX / 96;
			if (dpiFactor != 1)
			{
				// Scale form according to scale factor
				MainTheme.TitleHeight = MainTheme.SystemExitImage.Height + toolMain.Height;
				// Move main toolbar to bottom of title height
				toolMain.Top = MainTheme.TitleHeight - toolMain.Height + MainTheme.FormMargin + 2;
			}
			// Style toolbar
			toolMain.Renderer = new StripRenderer();
			toolMain.BackColor = ColorTheme.FormBackTitle;
			toolMain.ShowItemToolTips = false;
			mBattles.Visible = false;
			mTankFilter.Visible = false;
			mRefreshSeparator.Visible = false;
			mColumnSelect.Visible = false;
			mMode.Visible = false;
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
		}

		
		private void CreateDataGridContextMenu()
		{
			// Datagrid context menu (Right click on Grid)
			ContextMenuStrip dataGridMainPopup = new ContextMenuStrip();
			dataGridMainPopup.Renderer = new StripRenderer();
			dataGridMainPopup.BackColor = ColorTheme.ToolGrayMainBack;
			ToolStripSeparator dataGridMainPopup_Separator1 = new ToolStripSeparator();
			ToolStripSeparator dataGridMainPopup_Separator2 = new ToolStripSeparator();
			ToolStripMenuItem dataGridMainPopup_GrindingSetup = new ToolStripMenuItem("Grinding Setup");
			ToolStripMenuItem dataGridMainPopup_Chart = new ToolStripMenuItem("Battle Chart");
			ToolStripMenuItem dataGridMainPopup_Details = new ToolStripMenuItem("Tank Details");
			ToolStripMenuItem dataGridMainPopup_DeleteBattle = new ToolStripMenuItem("Delete this battle");
			ToolStripMenuItem dataGridMainPopup_FilterOnTank = new ToolStripMenuItem("Filter on this tank");
			//Assign event handlers
			dataGridMainPopup_GrindingSetup.Click += new EventHandler(dataGridMainPopup_GrindingSetup_Click);
			dataGridMainPopup_Chart.Click += new EventHandler(dataGridMainPopup_BattleChart_Click);
			dataGridMainPopup_Details.Click += new EventHandler(dataGridMainPopup_TankDetails_Click);
			dataGridMainPopup_DeleteBattle.Click += new EventHandler(dataGridMainPopup_DeleteBattle_Click);
			dataGridMainPopup_FilterOnTank.Click += new EventHandler(dataGridMainPopup_FilterOnTank_Click);
			// Add events
			dataGridMainPopup.Opening += new System.ComponentModel.CancelEventHandler(dataGridMainPopup_Opening);
			//Add to main context menu
			GridView.Views view = MainSettings.View;
			switch (view)
			{
				case GridView.Views.Overall:
					break;
				case GridView.Views.Tank:
					dataGridMainPopup.Items.AddRange(new ToolStripItem[] 
					{ 
						dataGridMainPopup_Details, 
						dataGridMainPopup_Chart, 
						dataGridMainPopup_GrindingSetup 
					});
					break;
				case GridView.Views.Battle:
					dataGridMainPopup.Items.AddRange(new ToolStripItem[] 
					{ 
						dataGridMainPopup_Details, 
						dataGridMainPopup_Chart, 
						dataGridMainPopup_GrindingSetup,
						dataGridMainPopup_Separator1,
						dataGridMainPopup_FilterOnTank,
						dataGridMainPopup_Separator2,
						dataGridMainPopup_DeleteBattle
					});
					break;
				default:
					break;
			}
			
			//Assign to datagridview
			dataGridMain.ContextMenuStrip = dataGridMainPopup;
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

		private void AutoSetup()
		{
			// TODO:
			// Autodetect dossier file from default location, save to config
			string dossierFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\wargaming.net\\WorldOfTanks\\dossier_cache";
			if (Directory.Exists(dossierFolder))
			{
				Config.Settings.dossierFilePath = dossierFolder;
				Form frm = new Forms.DatabaseNew(true);
				frm.ShowDialog();
				LoadConfigOK = AutoSetupHelper.AutoSetupCompleteOK;
				if (LoadConfigOK)
				{
					Config.Settings.dossierFileWathcherRun = 1;
					string msg = "";
					Config.SaveConfig(out msg);
				}
			}
			else
			{
				LoadConfigMsg = "Could not locate dossier file path, please select manually from Application Settings.";
			}
		}

		private void Main_Shown(object sender, EventArgs e)
		{
			// Ready to draw form
			Init = false;
			// Maximize now if last settings
			if (mainFormPosSize.WindowState == FormWindowState.Maximized)
			{
				this.WindowState = FormWindowState.Maximized;
			}
			// Startup settings
			if (!LoadConfigOK)
			{
				Code.Log.LogToFile("> No config MsgBox", true);
				MsgBox.Button answer = Code.MsgBox.Show(
					"Press 'OK' to create new SQLite database." +
					Environment.NewLine + Environment.NewLine +
					"Press 'Cancel' for advanced setup to relocate previously used database or create MSSQL database." +
					Environment.NewLine + Environment.NewLine,
					"Welcome to Wot Numbers", MsgBoxType.OKCancel, this);
				if (answer == MsgBox.Button.OKButton)
					AutoSetup();
				if (!LoadConfigOK)
				{
					Code.MsgBox.Show(LoadConfigMsg, "Could not load config data", this);
					lblOverView.Text = "";
					Config.Settings.dossierFileWathcherRun = 0;
					SetListener();
					Form frm = new Forms.ApplicationSetting();
					frm.ShowDialog();
				}
			}
			if (DB.CheckConnection())
			{
				TankData.GetAllLists();
			}
			// Show grid
			GridShow("", false);
			// Update dossier file watcher
			string result = dossier2json.UpdateDossierFileWatcher(this);
			// Check DB Version
			bool versionOK = DBVersion.CheckForDbUpgrade(this);
			// Add init items to Form
			SetFormTitle();
			SetFavListMenu();
			SetListener(false);
			ImageHelper.LoadTankImages();
			// Battle result file watcher
			fileSystemWatcherNewBattle.Path = Path.GetDirectoryName(Log.BattleResultDoneLogFileName());
			fileSystemWatcherNewBattle.Filter = Path.GetFileName(Log.BattleResultDoneLogFileName());
			fileSystemWatcherNewBattle.NotifyFilter = NotifyFilters.LastWrite;
			fileSystemWatcherNewBattle.Changed += new FileSystemEventHandler(NewBattleFileChanged);
			fileSystemWatcherNewBattle.EnableRaisingEvents = true;
			// Ready 
			MainTheme.Cursor = Cursors.Default;
			// Show status message
			SetStatus2("Application started");
			// Show Grinding Param Settings
			if (Config.Settings.grindParametersAutoStart)
			{
				Form frm = new Forms.GrindingParameter();
				frm.ShowDialog();
			}
			// Update Wot API
			if (DBVersion.RunWotApi)
				RunWotApi(true);
			// Check for dossier update
			if (DBVersion.RunDossierFileCheckWithForceUpdate)
				RunDossierFileCheckWithForceUpdate();
			else
				RunDossierFileCheck();
			
		}

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

		#endregion

		#region Common Events

		private int status2DefaultColor = 200;
		private int status2fadeColor = 200;

		private void NewBattleFileChanged(object source, FileSystemEventArgs e)
		{
			GridShow("Dossier file check finished successfully, grid refreshed");
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
					if (StatusBarHelper.MessageExists)
					{
						SetStatus2();
						StatusBarHelper.CheckForClear();
					}
				}
			}
		}

		private void SetStatus2(string txt = "")
		{
			string msg = txt; 
			if (StatusBarHelper.MessageExists && txt != "")
				msg += "   -   ";
			msg += StatusBarHelper.Message;
			if (msg != "")
			{
				timerStatus2.Enabled = false;
				Application.DoEvents();
				timerStatus2.Interval = 3000;
				lblStatus2.ForeColor = Color.FromArgb(255, status2DefaultColor, status2DefaultColor, status2DefaultColor); // White color, not faded
				lblStatus2.Text = msg;
				Application.DoEvents();
				timerStatus2.Enabled = true;
				if (StatusBarHelper.MessageExists)
					StatusBarHelper.CheckForClear();
			}
		}

		private void SetFormTitle()
		{
			// Check / show logged in player
			if (Config.Settings.playerName == "")
			{
				MainTheme.Text = "Wot Numbers - NO PLAYER SELECTED";
			}
			else
			{
				MainTheme.Text = "Wot Numbers - " + Config.Settings.playerNameAndServer;
			}
			Refresh();
		}

		private void SetListener(bool showStatus2Message = true)
		{
			mSettingsRun.Checked = (Config.Settings.dossierFileWathcherRun == 1);
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
			string result = dossier2json.UpdateDossierFileWatcher(this);
			SetFormBorder();
			if (showStatus2Message)
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

		#region Resize, Move or Close Form

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Save config to save current screen pos and size
			Config.Settings.posSize.WindowState = this.WindowState;
			string msg = "";
			Config.SaveConfig(out msg);
		}

		private void Main_Resize(object sender, EventArgs e)
		{
			if (!Init)
			{
				ResizeNow();
				// Remember new size for saving on form close
				if (this.WindowState == FormWindowState.Normal)
				{
					Config.Settings.posSize.Width = this.Width;
					Config.Settings.posSize.Height = this.Height;
				}
			}
		}

		private void Main_ResizeEnd(object sender, EventArgs e)
		{
			if (!Init) ResizeNow();
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

		private void Main_LocationChanged(object sender, EventArgs e)
		{
			if (!Init)
			{
				Screen screen = Screen.FromControl(this);
				this.MaximumSize = screen.WorkingArea.Size;
				// Remember new pos for saving on form close
				if (this.WindowState == FormWindowState.Normal)
				{
					Config.Settings.posSize.Top = this.Top;
					Config.Settings.posSize.Left = this.Left;
				}
			}
		}

		#endregion

		#region Main Navigation

		private void toolItemRefresh_Click(object sender, EventArgs e)
		{
			SetStatus2("Refreshing grid...");
			GridShow("Grid refreshed");
		}

		private void toolItemViewOverall_Click(object sender, EventArgs e)
		{
			ChangeView(GridView.Views.Overall);
		}

		private void toolItemViewTankInfo_Click(object sender, EventArgs e)
		{
			ChangeView(GridView.Views.Tank);
		}

		private void toolItemViewBattles_Click(object sender, EventArgs e)
		{
			ChangeView(GridView.Views.Battle);
		}

		private void ChangeView(GridView.Views newGridView, bool forceUpdate = false)
		{
			if (newGridView != MainSettings.View || forceUpdate) // Only do action if changed view or selected force update
			{
				// Uncheck previous selected view
				switch (MainSettings.View)
				{
					case GridView.Views.Overall:
						mViewOverall.Checked = false;
						break;
					case GridView.Views.Tank:
						mViewTankInfo.Checked = false;
						break;
					case GridView.Views.Battle:
						mViewBattles.Checked = false;
						break;
				}
				// Set new view as selected
				MainSettings.View = newGridView;
				// Set new values according to new selected view
				switch (MainSettings.View)
				{
					case GridView.Views.Overall:
						// Select view
						mViewOverall.Checked = true;
						// Show/Hide Tool Items
						mBattles.Visible = false;
						mTankFilter.Visible = false;
						mColumnSelect.Visible = false;
						mMode.Visible = false;
						mRefreshSeparator.Visible = true;
						mRefreshSeparator.Visible = false;
						// Remove datagrid context menu
						dataGridMain.ContextMenuStrip = null;
						// Start slider
						InfoPanelSlideStart(true);
						break;
					case GridView.Views.Tank:
						// Select view
						mViewTankInfo.Checked = true;
						// Show/Hide Tool Items
						mBattles.Visible = false;
						mTankFilter.Visible = true;
						mColumnSelect.Visible = true;
						mMode.Visible = true;
						mRefreshSeparator.Visible = true;
						// Get Column Setup List - also finds correct tank filter/fav list
						SetColListMenu();
						// Get Battle mode
						SetBattleModeMenu();
						// Add datagrid context menu (right click on datagrid)
						CreateDataGridContextMenu();
						// Info slider hide
						InfoPanelSlideStart(false);
						break;
					case GridView.Views.Battle:
						// Select view
						mViewBattles.Checked = true;
						// Show/Hide Tool Items
						mBattles.Visible = false;
						mTankFilter.Visible = true;
						mColumnSelect.Visible = true;
						mMode.Visible = true;
						mRefreshSeparator.Visible = true;
						// Get Column Setup List  - also finds correct tank filter/fav list
						SetColListMenu();
						// Get Battle mode
						SetBattleModeMenu();
						// Add datagrid context menu (right click on datagrid)
						CreateDataGridContextMenu();
						// Info slider hide
						InfoPanelSlideStart(false);
						break;
				}
				GridShow(); // Changed view, no status message applied, sets in GridShow
			}
		}

		private void GridShow(string Status2Message = "", bool ShowDefaultStatus2Message = true)
		{
			try
			{
				switch (MainSettings.View)
				{
					case GridView.Views.Overall:
						lblOverView.Text = "Welcome " + Config.Settings.playerName;
						if (Status2Message == "" && ShowDefaultStatus2Message) Status2Message = "Home view selected";
						GridShowOverall(Status2Message);
						break;
					case GridView.Views.Tank:
						if (Status2Message == "" && ShowDefaultStatus2Message) Status2Message = "Tank view selected";
						GridShowTank(Status2Message);
						break;
					case GridView.Views.Battle:
						if (Status2Message == "" && ShowDefaultStatus2Message) Status2Message = "Battle view selected";
						GridShowBattle(Status2Message);
						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				if (Config.Settings.showDBErrors)
					MsgBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + ex.InnerException, "Error initializing view", this);
			}
			
		}

		#endregion

		#region Menu Items: Col List

		private void SetColListMenu()
		{
			// Hide and uncheck all colum setup list menu items
			for (int i = 1; i <= 15; i++)
			{
				ToolStripMenuItem menuItem = mColumnSelect.DropDownItems["mColumnSelect_" + i.ToString("00")] as ToolStripMenuItem;
				menuItem.Visible = false;
				menuItem.Checked = false;
			}
			// Add colum lists according to database
			string sql = "select id, name, position from columnList where colType=@colType and position is not null order by position; ";
			DB.AddWithValue(ref sql, "@colType", (int)MainSettings.View, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql, Config.Settings.showDBErrors);
			if (dt.Rows.Count > 0)
			{
				int menuItemNum = 1;
				foreach (DataRow dr in dt.Rows)
				{
					ToolStripMenuItem menuItem = mColumnSelect.DropDownItems["mColumnSelect_" + menuItemNum.ToString("00")] as ToolStripMenuItem;
					string menuname = "(Missing name)";
					if (dr["name"] != DBNull.Value)
						menuname = dr["name"].ToString();
					menuItem.Text = menuname;
					menuItem.Visible = true;
					// check if selected
					if (MainSettings.GetCurrentGridFilter().ColListId == Convert.ToInt32(dr["id"]))
					{
						menuItem.Checked = true;
						mColumnSelect.Text = menuname;
					}
					// Stop after 15 menu items
					menuItemNum++;
					if (menuItemNum > 15) continue;
				}
			}
			SelectFavMenuItem();
		}

		private void toolItemColumnSelect_Click(object sender, EventArgs e)
		{
			// Selected a colList from toolbar
			ToolStripMenuItem selectedMenu = (ToolStripMenuItem)sender;
			// Get colListId for the selected colList
			int newColListId = ColListHelper.GetColListId(selectedMenu.Text);
			// Check if changed
			if (MainSettings.GetCurrentGridFilter().ColListId != newColListId)
			{
				// Changed colList
				// Uncheck colum setup list menu items
				for (int i = 1; i <= 13; i++)
				{
					ToolStripMenuItem menuItem = mColumnSelect.DropDownItems["mColumnSelect_" + i.ToString("00")] as ToolStripMenuItem;
					menuItem.Checked = false;
				}
				// Check and select new colList 
				selectedMenu.Checked = true;
				mColumnSelect.Text = selectedMenu.Text;
				// Get this colList to find what favList to show
				GridFilter.Settings newColListSettings = ColListHelper.GetSettingsForColList(newColListId);
				// Save selected column setup list to current
				GridFilter.Settings currentColListSettings = MainSettings.GetCurrentGridFilter();
				currentColListSettings.ColListId = newColListId;
				currentColListSettings.ColListName = selectedMenu.Text;
				if (newColListSettings.FavListShow != GridFilter.FavListShowType.UseCurrent)
				{
					currentColListSettings.TankId = -1; // Deselect tank filter
					currentColListSettings.FavListId = newColListSettings.FavListId;
					currentColListSettings.FavListName = newColListSettings.FavListName;
					currentColListSettings.FavListShow = newColListSettings.FavListShow;
				}
				MainSettings.UpdateCurrentGridFilter(currentColListSettings);
				Refresh();
				SelectFavMenuItem();
				Refresh();
				// Show grid
				GridShow("Selected column setup: " + selectedMenu.Text);
			}
		}

		private void toolItemColumnSelect_Edit_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.ColList();
			frm.ShowDialog();
			SetColListMenu(); // Refresh column setup list now
			GridShow("Refreshed grid after column setup change"); // Refresh grid now
		}

		#endregion

		#region Menu Items: Tank Filter / Fav List 

		private void SetFavListMenu()
		{
			// Hide and uncheck favlist from menu
			mTankFilter_FavSeparator.Visible = false;
			for (int i = 1; i <= 10; i++)
			{
				ToolStripMenuItem menuItem = mTankFilter.DropDownItems["mTankFilter_Fav" + i.ToString("00")] as ToolStripMenuItem;
				menuItem.Visible = false;
				menuItem.Checked = false;
			}
			// Add favlist to menu
			string sql = "select * from favList where position > 0 and name is not null order by position";
			DataTable dt = DB.FetchData(sql, Config.Settings.showDBErrors);
			if (dt.Rows.Count > 0)
			{
				mTankFilter_FavSeparator.Visible = true;
				foreach (DataRow dr in dt.Rows)
				{
					ToolStripMenuItem menuItem = (ToolStripMenuItem)mTankFilter.DropDownItems["mTankFilter_Fav" + Convert.ToInt32(dr["position"]).ToString("00")];
					if (menuItem != null)
					{
						menuItem.Text = dr["name"].ToString();
						menuItem.Visible = true;
						// check if selected
						if (MainSettings.GetCurrentGridFilter().FavListId == Convert.ToInt32(dr["id"]))
						{
							menuItem.Checked = true;
							mTankFilter.Text = menuItem.Text;
						}
					}
				}
			}
		}

		private void toolItem_Fav_Clicked(object sender, EventArgs e)
		{
			// Selected favList from toolbar
			ToolStripMenuItem selectedMenu = (ToolStripMenuItem)sender;
			// Get favListId for selected favList
			int newFavListId = FavListHelper.GetId(selectedMenu.Text);
			// Changed FavList
			GridFilter.Settings gf = MainSettings.GetCurrentGridFilter();
			gf.TankId = -1;
			gf.FavListId = newFavListId;
			gf.FavListName = selectedMenu.Text;
			gf.FavListShow = GridFilter.FavListShowType.FavList;
			MainSettings.UpdateCurrentGridFilter(gf);
			// Uncheck fav list menu items
			TankFilterMenuUncheck(true, true, true, true, false);
			// check fav list menu select
			selectedMenu.Checked = true;
			mTankFilter.Text = selectedMenu.Text;
			// Set menu item and show grid
			GridShow("Selected favourite tank list: " + selectedMenu.Text);
		}

		private void SelectFavMenuItem()
		{
			// Go to previous tank filter / fav list
			if (MainSettings.GetCurrentGridFilter().TankId != -1)
			{
				TankFilterMenuUncheck(true, true, true, true, false);
				mTankFilter_All.Checked = false;
			}
			else
			{
				switch (MainSettings.GetCurrentGridFilter().FavListShow)
				{
					case GridFilter.FavListShowType.UseCurrent:
						// No action, use previous selected tanks filter
						break;
					case GridFilter.FavListShowType.AllTanks:
						// Remove all filters, select All Tanks
						TankFilterMenuUncheck(true, true, true, true, false);
						break;
					case GridFilter.FavListShowType.FavList:
						// Go to 
						TankFilterMenuUncheck(true, true, true, true, false);
						mTankFilter_All.Checked = false;
						// Find favlist in menu and put on checkbox
						for (int i = 1; i <= 10; i++)
						{
							ToolStripMenuItem menuItem = mTankFilter.DropDownItems["mTankFilter_Fav" + i.ToString("00")] as ToolStripMenuItem;
							if (menuItem.Text == MainSettings.GetCurrentGridFilter().FavListName)
							{
								menuItem.Checked = true;
								mTankFilter.Text = MainSettings.GetCurrentGridFilter().FavListName;
							}
						}
						break;
					default:
						break;
				}
			}
		}

		private void TankFilterMenuUncheck(bool tier, bool country, bool type, bool favList, bool reopenMenu = true)
		{
			if (favList)
			{
				FavListMenuUncheck();
			}
			if (tier)
			{
				mTankFilter_Tier1.Checked = false;
				mTankFilter_Tier2.Checked = false;
				mTankFilter_Tier3.Checked = false;
				mTankFilter_Tier4.Checked = false;
				mTankFilter_Tier5.Checked = false;
				mTankFilter_Tier6.Checked = false;
				mTankFilter_Tier7.Checked = false;
				mTankFilter_Tier8.Checked = false;
				mTankFilter_Tier9.Checked = false;
				mTankFilter_Tier10.Checked = false;
			}
			if (country)
			{
				mTankFilter_CountryChina.Checked = false;
				mTankFilter_CountryFrance.Checked = false;
				mTankFilter_CountryGermany.Checked = false;
				mTankFilter_CountryJapan.Checked = false;
				mTankFilter_CountryUK.Checked = false;
				mTankFilter_CountryUSA.Checked = false;
				mTankFilter_CountryUSSR.Checked = false;
			}
			if (type)
			{
				mTankFilter_TypeHT.Checked = false;
				mTankFilter_TypeLT.Checked = false;
				mTankFilter_TypeMT.Checked = false;
				mTankFilter_TypeSPG.Checked = false;
				mTankFilter_TypeTD.Checked = false;
			}
			// Count selected menu items
			tankFilterItemCount = 0;
			if (mTankFilter_CountryChina.Checked) tankFilterItemCount++;
			if (mTankFilter_CountryFrance.Checked) tankFilterItemCount++;
			if (mTankFilter_CountryGermany.Checked) tankFilterItemCount++;
			if (mTankFilter_CountryUK.Checked) tankFilterItemCount++;
			if (mTankFilter_CountryUSA.Checked) tankFilterItemCount++;
			if (mTankFilter_CountryUSSR.Checked) tankFilterItemCount++;
			if (mTankFilter_CountryJapan.Checked) tankFilterItemCount++;
			if (mTankFilter_TypeLT.Checked) tankFilterItemCount++;
			if (mTankFilter_TypeMT.Checked) tankFilterItemCount++;
			if (mTankFilter_TypeHT.Checked) tankFilterItemCount++;
			if (mTankFilter_TypeTD.Checked) tankFilterItemCount++;
			if (mTankFilter_TypeSPG.Checked) tankFilterItemCount++;
			if (mTankFilter_Tier1.Checked) tankFilterItemCount++;
			if (mTankFilter_Tier2.Checked) tankFilterItemCount++;
			if (mTankFilter_Tier3.Checked) tankFilterItemCount++;
			if (mTankFilter_Tier4.Checked) tankFilterItemCount++;
			if (mTankFilter_Tier5.Checked) tankFilterItemCount++;
			if (mTankFilter_Tier6.Checked) tankFilterItemCount++;
			if (mTankFilter_Tier7.Checked) tankFilterItemCount++;
			if (mTankFilter_Tier8.Checked) tankFilterItemCount++;
			if (mTankFilter_Tier9.Checked) tankFilterItemCount++;
			if (mTankFilter_Tier10.Checked) tankFilterItemCount++;
			// Set "All Tanks" as checked if no tank filter is selected and no fav list is selected
			mTankFilter_All.Checked = (tankFilterItemCount == 0 && MainSettings.GetCurrentGridFilter().FavListShow != GridFilter.FavListShowType.FavList);
			// Reopen menu item exept for "all tanks"
			if (reopenMenu) this.mTankFilter.ShowDropDown();
		}

		private void FavListMenuUncheck()
		{
			// Deselect all favlist
			for (int i = 1; i <= 10; i++)
			{
				ToolStripMenuItem menuItem = mTankFilter.DropDownItems["mTankFilter_Fav" + i.ToString("00")] as ToolStripMenuItem;
				menuItem.Checked = false;
			}
		}

		private void TankFilterMenuSelect(ToolStripMenuItem menuItem, ToolStripMenuItem parentMenuItem, bool checkMenuItem = true)
		{
			string status2message = "";
			// Update selected tankfilter type
			GridFilter.Settings gf = MainSettings.GetCurrentGridFilter();
			gf.TankId = -1; // Remove tank filter
			gf.FavListShow = GridFilter.FavListShowType.AllTanks;
			MainSettings.UpdateCurrentGridFilter(gf);
			if (menuItem.Text == "All Tanks")
			{
				TankFilterMenuUncheck(true, true, true, true, false);
				status2message = "Selected all tanks";
			}
			else
			{
				// Remove favlist
				FavListMenuUncheck();
				// Update menu tank filter checked elements
				if (checkMenuItem)
				{
					menuItem.Checked = !menuItem.Checked;
					if (menuItem.Checked)
						tankFilterItemCount++;
					else
						tankFilterItemCount--;
				}
				mTankFilter_All.Checked = (tankFilterItemCount == 0);
				// Refresh grid
				// Get Tank filter
				string tankFilterMessage = "";
				string tankFilter = "";
				string tankJoin = "";
				Tankfilter(out tankFilter, out tankJoin, out tankFilterMessage);
				status2message = "Selected tank filter: " + tankFilterMessage;
				mTankFilter.ShowDropDown();
				parentMenuItem.ShowDropDown();
			}
			GridShow(status2message);
		}
		
		private void toolItemTankFilter_All_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
			TankFilterMenuSelect(menuItem, menuItem); // For all tanks, second param is not relevant - no parent menu item
		}

		private void toolItemTankFilter_Tier_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
			TankFilterMenuSelect(menuItem, mTankFilter_Tier);
		}

		private void toolItemTankFilter_Type_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
			TankFilterMenuSelect(menuItem, mTankFilter_Type);
		}

		private void toolItemTankFilter_Country_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
			TankFilterMenuSelect(menuItem, mTankFilter_Country);
		}

		private void toolItemTankFilter_Country_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				TankFilterMenuUncheck(false, true, false, false);
				ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
				TankFilterMenuSelect(menuItem, mTankFilter_Country, false);
			}
		}

		private void toolItemTankFilter_Type_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				TankFilterMenuUncheck(false, false, true, false);
				ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
				TankFilterMenuSelect(menuItem, mTankFilter_Type, false);
			}
		}

		private void toolItemTankFilter_Tier_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				TankFilterMenuUncheck(true, false, false, false);
				ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
				TankFilterMenuSelect(menuItem, mTankFilter_Tier, false);
			}
		}

		private void toolItemTankFilter_MouseDown(object sender, MouseEventArgs e)
		{
			// On right mouse click just display status message for current filter
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				string where = "";
				string join = "";
				string message = "";
				Tankfilter(out where, out join, out message);
				SetStatus2("Current selected tank filter: " + message);
			}
		}

		private void toolItemTankFilter_EditFavList_Click(object sender, EventArgs e)
		{
			// Show fal list editor
			Form frm = new Forms.FavList2(MainSettings.GetCurrentGridFilter().FavListId);
			frm.ShowDialog();
			// After fav list changes reload menu
			SetFavListMenu(); // Reload fav list items
			GridShow("Refreshed grid after fovourite tank list change"); // Refresh grid now
		}

		#endregion

		#region Menu Items: Battle Time
			
		private void toolItemBattlesSelected_Click(object sender, EventArgs e)
		{
			mBattles1d.Checked = false;
			mBattlesYesterday.Checked = false;
			mBattles3d.Checked = false;
			mBattles1w.Checked = false;
			mBattles2w.Checked = false;
			mBattles1m.Checked = false;
			mBattles3m.Checked = false;
			mBattles6m.Checked = false;
			mBattles1y.Checked = false;
			mBattles2y.Checked = false;
			mBattlesAll.Checked = false;
			ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
			menuItem.Checked = true;
			mBattles.Text = menuItem.Text;
			GridShowBattle("Selected battle time: " + menuItem.Text);
		}

		#endregion

		#region Menu Items: Battle Mode

		private void toolItemMode_Click(object sender, EventArgs e)
		{
			// Selected battle mode from toolbar
			ToolStripMenuItem selectedMenu = (ToolStripMenuItem)sender;
			// Get new battle type for selected favList
			GridFilter.BattleModeType selectedMode = (GridFilter.BattleModeType)Enum.Parse(typeof(GridFilter.BattleModeType), selectedMenu.Tag.ToString());
			// Changed gridFilter
			GridFilter.Settings gf = MainSettings.GetCurrentGridFilter();
			gf.BattleMode = selectedMode;
			MainSettings.UpdateCurrentGridFilter(gf);
			// Remove current menu checked
			foreach (var dropDownItem in mMode.DropDownItems)
			{
				if (dropDownItem is ToolStripMenuItem)
				{
					ToolStripMenuItem menuItem = (ToolStripMenuItem)dropDownItem;
					menuItem.Checked = false;
				}
			}
			// check battle mode list menu select
			selectedMenu.Checked = true;
			string mainToolItemMenuText = BattleModeHelper.GetShortmenuName(selectedMenu.Text);
			
			mMode.Text = mainToolItemMenuText;
			// Set menu item and show grid
			GridShow("Selected battle mode: " + selectedMenu.Text);
		}

		private void SetBattleModeMenu()
		{
			foreach (var dropDownItem in mMode.DropDownItems)
			{
				string battleMode = MainSettings.GetCurrentGridFilter().BattleMode.ToString();
				if (dropDownItem is ToolStripMenuItem)
				{
					ToolStripMenuItem menuItem = (ToolStripMenuItem)dropDownItem;
					if (menuItem.Tag != null && battleMode == menuItem.Tag.ToString())
					{
						menuItem.Checked = true;
						mMode.Text = BattleModeHelper.GetShortmenuName(menuItem.Text);
					}
					else
					{
						menuItem.Checked = false;
					}
				}
			}
		}

		private void toolItemModeSpecialInfo_Click(object sender, EventArgs e)
		{
			string s = "The tanks statistics are the same for Random, Company and Clan battles." +
						Environment.NewLine + Environment.NewLine +
						"For 'Tanks view' these filters only limit the tanks showing in grid, the stats will be the same." +
						Environment.NewLine + Environment.NewLine +
						"For 'battle view' the stats is calculated per battle and will be correct for any filter.";
			Code.MsgBox.Show(s, "Special Battle Filter Information", this);
		}


		#endregion

		#region Col List

		
		#endregion
					
		#region Fav List and Tank Filter 

		private int tankFilterItemCount = 0; // To keep track on how manny tank filter itmes selected
		
		private void Tankfilter(out string whereSQL, out string joinSQL, out string Status2Message)
		{
			string tier = "";
			string nation = "";
			string nationId = "";
			string type = "";
			string typeId = "";
			string message = "";
			string newWhereSQL = "";
			string newJoinSQL = "";
			// Calc filter and set statusbar message
			if (MainSettings.GetCurrentGridFilter().TankId != -1)
			{
				int tankId = MainSettings.GetCurrentGridFilter().TankId;
				string tankName = TankData.GetTankName(tankId);
				mTankFilter.Text = tankName;
				message = "Filtered on tank: " + tankName;
				newWhereSQL = " AND tank.id=@tankId ";
				DB.AddWithValue(ref newWhereSQL, "@tankId", tankId, DB.SqlDataType.Int);
			}
			else if (MainSettings.GetCurrentGridFilter().FavListShow == GridFilter.FavListShowType.FavList)
			{
				mTankFilter.Text = MainSettings.GetCurrentGridFilter().FavListName;
				message = "Favourite list: " + mTankFilter.Text;
				newJoinSQL = " INNER JOIN favListTank ON tank.id=favListTank.tankId AND favListTank.favListId=@favListId ";
				DB.AddWithValue(ref newJoinSQL, "@favListId", MainSettings.GetCurrentGridFilter().FavListId, DB.SqlDataType.Int);
			}
			else if (tankFilterItemCount == 0)
			{
				mTankFilter.Text = "All Tanks";
				message = "All Tanks";
			}
			else
			{
				if (mTankFilter_Tier1.Checked) { tier += "1,"; }
				if (mTankFilter_Tier2.Checked) { tier += "2,"; }
				if (mTankFilter_Tier3.Checked) { tier += "3,"; }
				if (mTankFilter_Tier4.Checked) { tier += "4,"; }
				if (mTankFilter_Tier5.Checked) { tier += "5,"; }
				if (mTankFilter_Tier6.Checked) { tier += "6,"; }
				if (mTankFilter_Tier7.Checked) { tier += "7,"; }
				if (mTankFilter_Tier8.Checked) { tier += "8,"; }
				if (mTankFilter_Tier9.Checked) { tier += "9,"; }
				if (mTankFilter_Tier10.Checked) { tier += "10,"; }
				if (mTankFilter_CountryChina.Checked) { nation += "China,"; nationId += "3,"; }
				if (mTankFilter_CountryFrance.Checked) { nation += "France,"; nationId += "4,"; }
				if (mTankFilter_CountryGermany.Checked) { nation += "Germany,"; nationId += "1,"; }
				if (mTankFilter_CountryUK.Checked) { nation += "UK,"; nationId += "5,"; }
				if (mTankFilter_CountryUSA.Checked) { nation += "USA,"; nationId += "2,"; }
				if (mTankFilter_CountryUSSR.Checked) { nation += "USSR,"; nationId += "0,"; }
				if (mTankFilter_CountryJapan.Checked) { nation += "Japan,"; nationId += "6,"; }
				if (mTankFilter_TypeLT.Checked) { type += "Light,"; typeId += "1,"; }
				if (mTankFilter_TypeMT.Checked) { type += "Medium,"; typeId += "2,"; }
				if (mTankFilter_TypeHT.Checked) { type += "Heavy,"; typeId += "3,"; }
				if (mTankFilter_TypeTD.Checked) { type += "TD,"; typeId += "4,"; }
				if (mTankFilter_TypeSPG.Checked) { type += "SPG,"; typeId += "5,"; }
				// Compose status message
				if (tier.Length > 0)
				{
					string tierId = tier;
					tier = " Tier=" + tier.Substring(0, tier.Length-1);
					newWhereSQL = " tank.tier IN (" + tierId.Substring(0, tierId.Length - 1) + ") ";
				}
				if (nation.Length > 0)
				{
					nation = " Nation=" + nation.Substring(0, nation.Length - 1);
					if (newWhereSQL != "") newWhereSQL += " AND ";
					newWhereSQL += " tank.countryId IN (" + nationId.Substring(0, nationId.Length - 1) + ") ";
				}
				if (type.Length > 0)
				{
					type = " Type=" + type.Substring(0, type.Length - 1);
					if (newWhereSQL != "") newWhereSQL += " AND ";
					newWhereSQL += " tank.tankTypeId IN (" + typeId.Substring(0, typeId.Length - 1) + ") ";
				}
				if (newWhereSQL != "") newWhereSQL = " AND (" + newWhereSQL + ") ";
				message = nation + type + tier;
				if (message.Length > 0) message = message.Substring(1);
				// Add correct mein menu name
				if (tankFilterItemCount == 1)
				{
					mTankFilter.Text = message;
				}
				else
				{
					mTankFilter.Text = "Tank filter";
				}
			}
			whereSQL = newWhereSQL;
			joinSQL = newJoinSQL;
			Status2Message = message;
		}

		#endregion
				
		#region Data Grid - OVERALL VIEW                                   ***********************************************************************

		private bool mainGridFormatting = false; // Controls if grid should be formattet or not
		private bool mainGridSaveColWidth = false; // Controls if change width on cols should be saved

		private void GridShowOverall(string Status2Message)
		{
			try
			{
				mainGridSaveColWidth = false; // Do not save changing of colWidth when loading grid
				mainGridFormatting = false;
				dataGridMain.DataSource = null;
				if (!DB.CheckConnection(false)) return;
				string sql =
					"Select 'Tanks count' as Data, cast(count(playerTank.tankId) as varchar) as Value " +
					"from playerTank " +
					"where playerid=@playerid and tankid in (select pt.tankId from playerTank pt inner join playerTankBattle ptb on pt.id = ptb.playerTankId) ";
				DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
				DataTable dt = DB.FetchData(sql, Config.Settings.showDBErrors);
				// Overall stats
				sql =
					"select sum(ptb.battles) as battles, sum(ptb.dmg) as dmg, sum (ptb.spot) as spot, sum (ptb.frags) as frags, " +
					"  sum (ptb.def) as def, sum (cap) as cap, sum(t.tier * ptb.battles) as tier, sum(ptb.wins) as wins " +
					"from playerTankBattle ptb left join " +
					"  playerTank pt on ptb.playerTankId=pt.id left join " +
					"  tank t on pt.tankId = t.id " +
					"where pt.playerId=@playerId ";
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				DataTable dtStats = DB.FetchData(sql);
				int battleCount = 0;
				double wr = 0;
				double wn8 = 0;
				double wn7 = 0;
				double eff = 0;
				bool applyColors = false;
				if (dtStats.Rows.Count > 0 && dtStats.Rows[0]["battles"] != DBNull.Value)
				{
					DataRow stats = dtStats.Rows[0];
					// Add Battle Count
					DataRow dr = dt.NewRow();
					dr["Data"] = "Total Battles";
					battleCount = Convert.ToInt32(stats["battles"]);
					dr["Value"] = battleCount.ToString();
					dt.Rows.Add(dr);
					// Add Winrate
					dr = dt.NewRow();
					dr["Data"] = "Win Rate";
					wr = (Convert.ToDouble(stats["wins"])/Convert.ToDouble(stats["battles"])*100);
					dr["Value"] = Math.Round(wr, 2).ToString() + " %";
					dt.Rows.Add(dr);
					// Rating parameters
					double BATTLES = Rating.ConvertDbVal2Double(stats["battles"]);
					double DAMAGE = Rating.ConvertDbVal2Double(stats["dmg"]);
					double SPOT = Rating.ConvertDbVal2Double(stats["spot"]);
					double FRAGS = Rating.ConvertDbVal2Double(stats["frags"]);
					double DEF = Rating.ConvertDbVal2Double(stats["def"]);
					double CAP = Rating.ConvertDbVal2Double(stats["cap"]);
					double WINS = Rating.ConvertDbVal2Double(stats["wins"]);
					double TIER = 0;
					if (BATTLES > 0)
						TIER = Rating.ConvertDbVal2Double(stats["tier"]) / BATTLES;
					
					// Add EFF
					dr = dt.NewRow();
					dr["Data"] = "EFF Total Rating";
					eff = Code.Rating.CalculateEFF(BATTLES, DAMAGE, SPOT, FRAGS, DEF, CAP, TIER);
					dr["Value"] = Math.Round(eff, 2).ToString();
					dt.Rows.Add(dr);

					// Add WN7
					dr = dt.NewRow();
					dr["Data"] = "WN7 Total Rating";
					wn7 = Code.Rating.CalculateWN7(BATTLES, DAMAGE, SPOT, FRAGS, DEF, CAP, WINS, Rating.GetAverageBattleTier());
					dr["Value"] = Math.Round(wn7, 2).ToString();
					dt.Rows.Add(dr);

					// Add WN8
					dr = dt.NewRow();
					dr["Data"] = "WN8 Total Rating";
					wn8 = Code.Rating.CalculatePlayerTotalWN8();
					dr["Value"] = Math.Round(wn8, 2).ToString();
					dt.Rows.Add(dr);

					// Ready to set colors
					applyColors = true;

				}
				// Set row height in template 
				dataGridMain.RowTemplate.Height = 23;
				// Populate grid
				dataGridMain.DataSource = dt;
				// Unfocus
				dataGridMain.ClearSelection();
				// Text cols
				dataGridMain.Columns["Data"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
				dataGridMain.Columns["Data"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
				dataGridMain.Columns["Value"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
				dataGridMain.Columns["Value"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
				// Colors
				if (applyColors)
				{
					dataGridMain.Rows[1].Cells[1].Style.ForeColor = Rating.BattleCountColor(battleCount);
					dataGridMain.Rows[2].Cells[1].Style.ForeColor = Rating.WinRateColor(wr);
					dataGridMain.Rows[3].Cells[1].Style.ForeColor = Rating.EffColor(eff);
					dataGridMain.Rows[4].Cells[1].Style.ForeColor = Rating.WN7color(wn7);
					dataGridMain.Rows[5].Cells[1].Style.ForeColor = Rating.WN8color(wn8);
					
				}
				// Finish
				dataGridMain.Columns[0].Width = 100;
				dataGridMain.Columns[1].Width = 500;
				ResizeNow();
				lblStatusRowCount.Text = "Rows " + dataGridMain.RowCount.ToString();
				// Status mesage
				SetStatus2(Status2Message);
			}
			catch (Exception ex)
			{
				string s = ex.Message;
				if (Config.Settings.showDBErrors)
					Code.MsgBox.Show(ex.Message, "Error showing overall stats", this);
				//throw;
			}
		}

		#endregion
		
		#region Data Grid - TANK VIEW                                      ***********************************************************************

		private void GridShowTank(string Status2Message)
		{
			mainGridSaveColWidth = false; // Do not save changing of colWidth when loading grid
			mainGridFormatting = false;
			dataGridMain.DataSource = null;
			if (!DB.CheckConnection(false)) return;
			// Get Columns
			string select = "";
			List<ColListHelper.ColListClass> colList = new List<ColListHelper.ColListClass>();
			int img;
			int smallimg;
			int contourimg;
			ColListHelper.GetSelectedColumnList(out select, out colList, out img, out smallimg, out contourimg);
			// Get Tank filter
			string message = "";
			string tankFilter = "";
			string join = "";
			Tankfilter(out tankFilter, out join, out message);
			// Create Battle mode filter
			string battleModeFilter = "";
			switch (MainSettings.GridFilterTank.BattleMode)
			{
				case GridFilter.BattleModeType.Mode15:
					battleModeFilter = " AND (playerTankBattle.battleMode = '15') ";
					break;
				case GridFilter.BattleModeType.Mode7:
					battleModeFilter = " AND (playerTankBattle.battleMode = '7') ";
					break;
				case GridFilter.BattleModeType.Random:
					battleModeFilter = " AND (playerTankBattle.battleMode = '15' AND playerTank.hasClan = 0 AND playerTank.hasCompany = 0) ";
					break;
				case GridFilter.BattleModeType.Clan:
					battleModeFilter = " AND (playerTank.hasClan = 1) ";
					break;
				case GridFilter.BattleModeType.Company:
					battleModeFilter = " AND (playerTank.hasCompany = 1) ";
					break;
				case GridFilter.BattleModeType.Historical:
					battleModeFilter = " AND (playerTankBattle.battleMode = 'Historical') ";
					break;
				default:
					break;
			}
			// Sort order
			string sortordercol = "0 as sortorder ";
			if (join != "")
			{
				sortordercol = "favListTank.sortorder as sortorder ";
			}
			// Create the SQL
			string sql = "";
			if (MainSettings.GridFilterTank.BattleMode == GridFilter.BattleModeType.All)
			{
				// Use playerTankBattleTotalsView in stead of playerTankBattle to show totals
				select = select.Replace("playerTankBattle", "playerTankBattleTotalsView");

				// Get SUM for playerTankBattle as several battleModes might appear
				sql =
					"SELECT   " + select + sortordercol + ", playerTank.Id as player_Tank_Id, tank.id as tank_id " + Environment.NewLine +
					"FROM     tank INNER JOIN " + Environment.NewLine +
					"         playerTank ON tank.id = playerTank.tankId INNER JOIN " + Environment.NewLine +
					"         tankType ON tank.tankTypeId = tankType.id INNER JOIN " + Environment.NewLine +
					"         country ON tank.countryId = country.id INNER JOIN " + Environment.NewLine +
					"         playerTankBattleTotalsView ON playerTankBattleTotalsView.playerTankId = playerTank.id LEFT OUTER JOIN " + Environment.NewLine +
					"         modTurret ON playerTank.modTurretId = modTurret.id LEFT OUTER JOIN " + Environment.NewLine +
					"         modRadio ON modRadio.id = playerTank.modRadioId LEFT OUTER JOIN " + Environment.NewLine +
					"         modGun ON playerTank.modGunId = modGun.id " + join + Environment.NewLine +
					"WHERE    playerTank.playerId=@playerid " + tankFilter + " " + Environment.NewLine +
					"ORDER BY playerTank.lastBattleTime DESC";
			}
			else
			{
				// Only gets one row from playerTankBattle for an explisit battleMode
				sql =
					"SELECT   " + select + sortordercol + ", playerTank.Id as player_Tank_Id, tank.id as tank_id " + Environment.NewLine +
					"FROM     tank INNER JOIN " + Environment.NewLine +
					"         playerTank ON tank.id = playerTank.tankId INNER JOIN " + Environment.NewLine +
					"         tankType ON tank.tankTypeId = tankType.id INNER JOIN " + Environment.NewLine +
					"         country ON tank.countryId = country.id INNER JOIN " + Environment.NewLine +
					"         playerTankBattle ON playerTankBattle.playerTankId = playerTank.id LEFT OUTER JOIN " + Environment.NewLine +
					"         modTurret ON playerTank.modTurretId = modTurret.id LEFT OUTER JOIN " + Environment.NewLine +
					"         modRadio ON modRadio.id = playerTank.modRadioId LEFT OUTER JOIN " + Environment.NewLine +
					"         modGun ON playerTank.modGunId = modGun.id " + join + Environment.NewLine +
					"WHERE    playerTank.playerId=@playerid " + tankFilter + battleModeFilter + " " + Environment.NewLine +
					"ORDER BY playerTank.lastBattleTime DESC";
			}
			// Code.MsgBox.Show(sql, "sql"); // FOR DEBUG
			DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
			// Set row height in template before rendering to fit images
			dataGridMain.RowTemplate.Height = 23;
			if (smallimg >= 0)
				dataGridMain.RowTemplate.Height = 31;
			if (img >= 0)
				dataGridMain.RowTemplate.Height = 60;
			DataSet ds = new DataSet("DataSet");
			DataTable dtTankData = new DataTable("TankData");
			dtTankData = DB.FetchData(sql, Config.Settings.showDBErrors);
			// If images add cols in datatable containing the image
			if (contourimg + smallimg + img > -3)
			{
				// Use ImageHelper to add columns in use
				List<ImageHelper.ImgColumns> imgPosition = new List<ImageHelper.ImgColumns>();
				if (contourimg >= 0)
					imgPosition.Add(new ImageHelper.ImgColumns("Tank Icon", contourimg));
				if (smallimg >= 0)
					imgPosition.Add(new ImageHelper.ImgColumns("Tank Image", smallimg));
				if (img >= 0)
					imgPosition.Add(new ImageHelper.ImgColumns("Tank Image Large", img));
				// Sort images to get right position
				imgPosition.Sort();
				// Add columns
				foreach (ImageHelper.ImgColumns imgItem in imgPosition)
				{
					dtTankData.Columns.Add(imgItem.colName, typeof(Image)).SetOrdinal(imgItem.colPosition);
				}
				// Fill with images
				foreach (DataRow dr in dtTankData.Rows)
				{
					int tankId = Convert.ToInt32(dr["tank_id"]);
					if (contourimg >= 0)
						dr["Tank Icon"] = ImageHelper.GetTankImage(tankId, "contourimg");
					if (smallimg >= 0)
						dr["Tank Image"] = ImageHelper.GetTankImage(tankId, "smallimg");
					if (img >= 0)
						dr["Tank Image Large"] = ImageHelper.GetTankImage(tankId, "img");
				}
			}
			// Assign datatable to grid
			mainGridFormatting = true;
			dataGridMain.DataSource = dtTankData;
			// Unfocus
			dataGridMain.ClearSelection();
			//  Hide system cols
			dataGridMain.Columns["sortorder"].Visible = false;
			dataGridMain.Columns["player_Tank_Id"].Visible = false;
			dataGridMain.Columns["tank_Id"].Visible = false;
			// Grid col size
			foreach (ColListHelper.ColListClass colListItem in colList)
			{
				dataGridMain.Columns[colListItem.name].Width = colListItem.width;
				if (colListItem.type == "Int")
				{
					dataGridMain.Columns[colListItem.name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
					dataGridMain.Columns[colListItem.name].DefaultCellStyle.Format = "N0";
				}
				else if (colListItem.type == "Float")
				{
					dataGridMain.Columns[colListItem.name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
					dataGridMain.Columns[colListItem.name].DefaultCellStyle.Format = "N1";
				}
				else if (colListItem.type == "Image" && colListItem.name == "Tank Image")
				{
					dataGridMain.Columns[colListItem.name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
				}
				else if (colListItem.type == "Image" && colListItem.name == "Tank Image Large")
				{
					dataGridMain.Columns[colListItem.name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				}
			}
			ResizeNow();
			mainGridSaveColWidth = true;
			// Add status message
			SetStatus2(Status2Message);
			lblStatusRowCount.Text = "Rows " + dataGridMain.RowCount.ToString();
		}

		#endregion
		
		#region Data Grid - BATTLE VIEW                                    ***********************************************************************

		private void GridShowBattle(string Status2Message)
		{
			try
			{
				mainGridSaveColWidth = false; // Do not save changing of colWidth when loading grid
				mainGridFormatting = false;
				dataGridMain.DataSource = null;
				if (!DB.CheckConnection(false)) return;
				// Get Columns
				string select = "";
				List<ColListHelper.ColListClass> colList = new List<ColListHelper.ColListClass>();
				int img;
				int smallimg;
				int contourimg;
				ColListHelper.GetSelectedColumnList(out select, out colList, out img, out smallimg, out contourimg);
				// Create Battle Time filer
				string battleTimeFilter = "";
				if (!mBattlesAll.Checked)
				{
					if (mBattlesYesterday.Checked)
						battleTimeFilter = " AND (battleTime>=@battleTime AND battleTime<=@battleFromTime) ";
					else
						battleTimeFilter = " AND battleTime>=@battleTime ";
				}
				// Create Battle mode filter
				string battleModeFilter = "";
				if (MainSettings.GridFilterBattle.BattleMode != GridFilter.BattleModeType.All)
				{
					switch (MainSettings.GridFilterBattle.BattleMode)
					{
						case GridFilter.BattleModeType.Mode15:
							battleModeFilter = " AND (battleMode = '15') ";
							break;
						case GridFilter.BattleModeType.Mode7:
							battleModeFilter = " AND (battleMode = '7') ";
							break;
						case GridFilter.BattleModeType.Random:
							battleModeFilter = " AND (battleMode = '15' AND modeClan = 0 AND modeCompany = 0) ";
							break;
						case GridFilter.BattleModeType.Clan:
							battleModeFilter = " AND (modeClan > 0) ";
							break;
						case GridFilter.BattleModeType.Company:
							battleModeFilter = " AND (modeCompany > 0) ";
							break;
						case GridFilter.BattleModeType.Historical:
							battleModeFilter = " AND (battleMode = 'Historical') ";
							break;
						default:
							break;
					}
				}

				// Get Tank filter
				string tankFilterMessage = "";
				string tankFilter = "";
				string tankJoin = "";
				Tankfilter(out tankFilter, out tankJoin, out tankFilterMessage);
				string sortordercol = "0 as sortorder ";
				string sql =
					"SELECT " + select +
					"  battleResult.color as battleResultColor,  battleSurvive.color as battleSurviveColor, " +
					"  CAST(battle.battleTime AS DATETIME) as battleTimeToolTip, battle.battlesCount as battlesCountToolTip, " +
					"  battle.victory as victoryToolTip, battle.draw as drawToolTip, battle.defeat as defeatToolTip, " +
					"  battle.survived as survivedCountToolTip, battle.killed as killedCountToolTip, tank.id as tank_id, " +
					"  0 as footer, playerTank.Id as player_Tank_Id, battle.id as battle_Id, " + sortordercol +
					"FROM    battle INNER JOIN " +
					"        playerTank ON battle.playerTankId = playerTank.id INNER JOIN " +
					"        tank ON playerTank.tankId = tank.id INNER JOIN " +
					"        tankType ON tank.tankTypeId = tankType.Id INNER JOIN " +
					"        country ON tank.countryId = country.Id INNER JOIN " +
					"        battleResult ON battle.battleResultId = battleResult.id INNER JOIN " +
					"        battleSurvive ON battle.battleSurviveId = battleSurvive.id " + tankJoin +
					"WHERE   playerTank.playerId=@playerid " + battleTimeFilter + battleModeFilter + tankFilter +
					"ORDER BY sortorder, battle.battleTime DESC ";
				DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
				DateTime dateFilter = new DateTime();
				if (!mBattlesAll.Checked)
				{
					DateTime basedate = DateTime.Now; // current time
					if (DateTime.Now.Hour < 5) basedate = DateTime.Now.AddDays(-1); // correct date according to server reset 05:00
					dateFilter = new DateTime(basedate.Year, basedate.Month, basedate.Day, 5, 0, 0); // datefilter = today
					// Adjust time scale according to selected filter
					if (mBattles3d.Checked) dateFilter = dateFilter.AddDays(-3);
					else if (mBattles1w.Checked) dateFilter = dateFilter.AddDays(-7);
					else if (mBattles2w.Checked) dateFilter = dateFilter.AddDays(-14);
					else if (mBattles1m.Checked) dateFilter = dateFilter.AddMonths(-1);
					else if (mBattles3m.Checked) dateFilter = dateFilter.AddMonths(-3);
					else if (mBattles6m.Checked) dateFilter = dateFilter.AddMonths(-6);
					else if (mBattles1y.Checked) dateFilter = dateFilter.AddYears(-1);
					else if (mBattles2y.Checked) dateFilter = dateFilter.AddYears(-2);
					else if (mBattlesYesterday.Checked)
					{
						DateTime dateFromYesterdayFilter = dateFilter;
						dateFilter = dateFilter.AddDays(-1);
						DB.AddWithValue(ref sql, "@battleFromTime", dateFromYesterdayFilter.ToString("yyyy-MM-dd HH:mm"), DB.SqlDataType.DateTime);
					}
					DB.AddWithValue(ref sql, "@battleTime", dateFilter.ToString("yyyy-MM-dd HH:mm"), DB.SqlDataType.DateTime);
				}
				DataTable dt = new DataTable();
				dt = DB.FetchData(sql, Config.Settings.showDBErrors);
				// If images add cols in datatable containing the image
				if (contourimg + smallimg + img > -3)
				{
					// Use ImageHelper to add columns in use
					List<ImageHelper.ImgColumns> imgPosition = new List<ImageHelper.ImgColumns>();
					if (contourimg >= 0)
						imgPosition.Add(new ImageHelper.ImgColumns("Tank Icon", contourimg));
					if (smallimg >= 0)
						imgPosition.Add(new ImageHelper.ImgColumns("Tank Image", smallimg));
					if (img >= 0)
						imgPosition.Add(new ImageHelper.ImgColumns("Tank Image Large", img));
					// Sort images to get right position
					imgPosition.Sort();
					// Add columns
					foreach (ImageHelper.ImgColumns imgItem in imgPosition)
					{
						dt.Columns.Add(imgItem.colName, typeof(Image)).SetOrdinal(imgItem.colPosition);
					}
					// Fill with images
					foreach (DataRow dr in dt.Rows)
					{
						int tankId = Convert.ToInt32(dr["tank_id"]);
						if (contourimg >= 0)
							dr["Tank Icon"] = ImageHelper.GetTankImage(tankId, "contourimg");
						if (smallimg >= 0)
							dr["Tank Image"] = ImageHelper.GetTankImage(tankId, "smallimg");
						if (img >= 0)
							dr["Tank Image Large"] = ImageHelper.GetTankImage(tankId, "img");
					}
				}
				int rowcount = dt.Rows.Count;
				// Add footer
				int totalBattleCount = 0;
				double totalWinRate = 0;
				double totalSurvivedRate = 0;
				// Add footer now, if ant rows
				if (rowcount > 0)
				{
					// Create blank image in case of image in footer
					Image blankImage = new Bitmap(1, 1);
					// totals
					totalBattleCount = Convert.ToInt32(dt.Compute("Sum(battlesCountToolTip)", ""));
					totalWinRate = Convert.ToDouble(dt.Compute("Sum(victoryToolTip)", "")) * 100 / totalBattleCount;
					totalSurvivedRate = Convert.ToDouble(dt.Compute("Sum(survivedCountToolTip)", "")) * 100 / totalBattleCount;
					// the footer row #1 - average
					DataRow footerRow1 = dt.NewRow();
					footerRow1["footer"] = 1;
					footerRow1["battleResultColor"] = "";
					footerRow1["battleSurviveColor"] = "";
					footerRow1["battleTimeToolTip"] = DBNull.Value;
					footerRow1["battlesCountToolTip"] = 0;
					footerRow1["victoryToolTip"] = 0;
					footerRow1["drawToolTip"] = 0;
					footerRow1["defeatToolTip"] = 0;
					footerRow1["survivedCountToolTip"] = 0;
					footerRow1["killedCountToolTip"] = 0;
					foreach (ColListHelper.ColListClass colListItem in colList)
					{
						if (colListItem.type == "Int")
						{
							footerRow1[colListItem.name] = Convert.ToInt32(dt.Compute("Sum([" + colListItem.name + "])", "")) / rowcount;
						}
						else if (colListItem.type == "Float")
						{
							footerRow1[colListItem.name] = Convert.ToDouble(dt.Compute("Sum([" + colListItem.name + "])", "")) / rowcount;
						}
						else if (colListItem.type == "DateTime")
						{
							footerRow1[colListItem.name] = DBNull.Value;
						}
						else if (colListItem.type == "Image")
						{
							footerRow1[colListItem.name] = blankImage;
						}
						else
						{
							string s = "";
							switch (colListItem.name)
							{
								case "Tank": s = "Average"; break;
								case "Result": s = Math.Round(totalWinRate, 1).ToString() + "%"; break;
								case "Survived": s = Math.Round(totalSurvivedRate, 1).ToString() + "%"; break;
							}
							footerRow1[colListItem.name] = s;
						}
					}
					// the footer row #2 - totals
					DataRow footerRow2 = dt.NewRow();
					footerRow2["footer"] = 2;
					footerRow2["battleResultColor"] = "";
					footerRow2["battleSurviveColor"] = "";
					footerRow2["battleTimeToolTip"] = DBNull.Value;
					footerRow2["battlesCountToolTip"] = 0;
					footerRow2["victoryToolTip"] = 0;
					footerRow2["drawToolTip"] = 0;
					footerRow2["defeatToolTip"] = 0;
					footerRow2["survivedCountToolTip"] = 0;
					footerRow2["killedCountToolTip"] = 0;
					foreach (ColListHelper.ColListClass colListItem in colList)
					{
						if (colListItem.type == "Int" || colListItem.type == "Float")
						{
							IEnumerable<string> nonTotalsCols = new List<string> { "EFF", "WN7", "WN8", "Hit Rate", "Tier", "ID", "Pierced Shots%", "Pierced Hits%", "HE Shots%" };
							if (!nonTotalsCols.Contains(colListItem.name)) // Avoid calculate total EFF/WN8
								// TODO: Must loop through datatable for every row per column and multiply with battlesCountToolTip to get correct sum when several battles recorded on one row
								footerRow2[colListItem.name] = Convert.ToInt32(dt.Compute("Sum([" + colListItem.name + "])", ""));
							else
								footerRow2[colListItem.name] = DBNull.Value;
						}
						else if (colListItem.type == "DateTime")
						{
							footerRow2[colListItem.name] = DBNull.Value;
						}
						else if (colListItem.type == "Image")
						{
							footerRow2[colListItem.name] = blankImage;
						}
						else
						{
							string s = "";
							switch (colListItem.name)
							{
								case "Tank": s = "Totals"; break;
								case "Result": s = ""; break;
								case "Survived": s = ""; break;
							}
							footerRow2[colListItem.name] = s;
						}
					}
					dt.Rows.Add(footerRow2);
					dt.Rows.Add(footerRow1);
				}
				// Set row height in template before rendering to fit images
				dataGridMain.RowTemplate.Height = 23;
				if (smallimg >= 0)
					dataGridMain.RowTemplate.Height = 31;
				if (img >= 0)
					dataGridMain.RowTemplate.Height = 60;
				// populate datagrid
				mainGridFormatting = true;
				dataGridMain.DataSource = dt;
				// Unfocus
				dataGridMain.ClearSelection();
				// Hide sys cols
				dataGridMain.Columns["battleResultColor"].Visible = false;
				dataGridMain.Columns["battleSurviveColor"].Visible = false;
				dataGridMain.Columns["battleTimeToolTip"].Visible = false;
				dataGridMain.Columns["battlesCountToolTip"].Visible = false;
				dataGridMain.Columns["victoryToolTip"].Visible = false;
				dataGridMain.Columns["drawToolTip"].Visible = false;
				dataGridMain.Columns["defeatToolTip"].Visible = false;
				dataGridMain.Columns["survivedCountToolTip"].Visible = false;
				dataGridMain.Columns["killedCountToolTip"].Visible = false;
				dataGridMain.Columns["footer"].Visible = false;
				dataGridMain.Columns["sortorder"].Visible = false;
				dataGridMain.Columns["player_Tank_Id"].Visible = false;
				dataGridMain.Columns["battle_Id"].Visible = false;
				dataGridMain.Columns["tank_Id"].Visible = false;
				// Format grid 
				foreach (ColListHelper.ColListClass colListItem in colList)
				{
					dataGridMain.Columns[colListItem.name].Width = colListItem.width;
					if (colListItem.type == "Int")
					{
						dataGridMain.Columns[colListItem.name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
						dataGridMain.Columns[colListItem.name].DefaultCellStyle.Format = "N0";
					}
					else if (colListItem.type == "Float")
					{
						dataGridMain.Columns[colListItem.name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
						dataGridMain.Columns[colListItem.name].DefaultCellStyle.Format = "N0";
						if (rowcount > 0) // Special format in footer for floating values
							dataGridMain.Rows[rowcount + 1].Cells[colListItem.name].Style.Format = "N1";
					}
					else if (colListItem.type == "Image" && colListItem.name == "Tank Image")
					{
						dataGridMain.Columns[colListItem.name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
					}
					else if (colListItem.type == "Image" && colListItem.name == "Tank Image Large")
					{
						dataGridMain.Columns[colListItem.name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
					}
					else // Format datetime/Varchar 
					{
						// Footer
						if (rowcount > 1)
						{
							switch (colListItem.name)
							{
								case "Tank":
									dataGridMain.Rows[rowcount].Cells["Tank"].ToolTipText = "Totals based on " + totalBattleCount.ToString() + " battles";
									dataGridMain.Rows[rowcount + 1].Cells["Tank"].ToolTipText = "Average based on " + totalBattleCount.ToString() + " battles";
									break;
							}
						}
					}
				}
				// Format grid footer
				//if (rowcount > 1)
				//{
				//	dataGridMain.Rows[rowcount].DefaultCellStyle.BackColor = ColorTheme.ToolGrayMainBack;
				//	dataGridMain.Rows[rowcount + 1].DefaultCellStyle.BackColor = ColorTheme.ToolGrayMainBack;
				//}
				// Finish up
				ResizeNow();
				mainGridSaveColWidth = true;
				mBattles.Visible = true;
				SetStatus2(Status2Message);
				lblStatusRowCount.Text = "Rows " + rowcount.ToString();
			}
			catch (Exception)
			{
				
				throw;
			}
			
		}

		#endregion

		#region Grid Formatting

		private void dataGridMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (mainGridFormatting)
			{
				string col = dataGridMain.Columns[e.ColumnIndex].Name;
				DataGridViewCell cell = dataGridMain[e.ColumnIndex, e.RowIndex];
				if (col.Equals("EFF"))
				{
					if (dataGridMain["EFF", e.RowIndex].Value != DBNull.Value)
					{
						int eff = Convert.ToInt32(dataGridMain["EFF", e.RowIndex].Value);
						cell.Style.ForeColor = Rating.EffColor(eff);
					}
				}
				else if (col.Equals("WN8"))
				{
					if (dataGridMain["WN8", e.RowIndex].Value != DBNull.Value)
					{
						int wn8 = Convert.ToInt32(dataGridMain["WN8", e.RowIndex].Value);
						cell.Style.ForeColor = Rating.WN8color(wn8);
					}
				}
				else if (col.Equals("WN7"))
				{
					if (dataGridMain["WN7", e.RowIndex].Value != DBNull.Value)
					{
						int wn7 = Convert.ToInt32(dataGridMain["WN7", e.RowIndex].Value);
						cell.Style.ForeColor = Rating.WN7color(wn7);
					}
				}
				else if (col.Contains("%"))
				{
					if (dataGridMain[col, e.RowIndex].Value != DBNull.Value)
					{
						int percentage = Convert.ToInt32(dataGridMain[col, e.RowIndex].Value);
						if (percentage > 0)
						{
							Color color = ColorTheme.Rating_very_bad;
							color = ColorTheme.Rating_very_bad;
							if (percentage >= 99) color = ColorTheme.Rating_super_uniqum;
							else if (percentage >= 95) color = ColorTheme.Rating_uniqum;
							else if (percentage >= 90) color = ColorTheme.Rating_great;
							else if (percentage >= 80) color = ColorTheme.Rating_very_good;
							else if (percentage >= 65) color = ColorTheme.Rating_good;
							else if (percentage >= 50) color = ColorTheme.Rating_average;
							else if (percentage >= 35) color = ColorTheme.Rating_below_average;
							else if (percentage >= 20) color = ColorTheme.Rating_bad;
							cell.Style.ForeColor = color;
						}
					}
				}
				else if (col.Equals("Remaining XP"))
				{
					if (dataGridMain[col, e.RowIndex].Value != DBNull.Value)
					{
						int val = Convert.ToInt32(dataGridMain[col, e.RowIndex].Value);
						if (val > 0)
						{
							Color color = ColorTheme.Rating_very_bad;
							if (val <= 5000) color = ColorTheme.Rating_super_uniqum;
							else if (val <= 10000) color = ColorTheme.Rating_uniqum;
							else if (val <= 25000) color = ColorTheme.Rating_great;
							else if (val <= 50000) color = ColorTheme.Rating_very_good;
							else if (val <= 75000) color = ColorTheme.Rating_good;
							else if (val <= 100000) color = ColorTheme.Rating_average;
							else if (val <= 125000) color = ColorTheme.Rating_below_average;
							else if (val <= 150000) color = ColorTheme.Rating_bad;
							cell.Style.ForeColor = color;
						}
					}
				}
				else if (mViewBattles.Checked)
				{
					bool footer = (Convert.ToInt32(dataGridMain["footer", e.RowIndex].Value) > 0);
					if (footer)
						dataGridMain.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.ToolGrayMainBack;
					if (col.Equals("Tank"))
					{
						string battleTime = dataGridMain["battleTimeToolTip", e.RowIndex].Value.ToString();
						int battlesCount = Convert.ToInt32(dataGridMain["battlesCountToolTip", e.RowIndex].Value);
						// Check if this row is normal or footer
						if (!footer) // normal line
						{
							cell.ToolTipText = "Battle result based on " + battlesCount.ToString() + " battle(s)" + Environment.NewLine + "Battle time: " + battleTime;
						}
					}
					// Battle Result color color
					else if (col.Equals("Result"))
					{
						string battleResultColor = dataGridMain["battleResultColor", e.RowIndex].Value.ToString();
						cell.Style.ForeColor = System.Drawing.ColorTranslator.FromHtml(battleResultColor);
						int battlesCount = Convert.ToInt32(dataGridMain["battlesCountToolTip", e.RowIndex].Value);
						if (battlesCount > 1)
						{
							cell.ToolTipText = "Victory: " + dataGridMain["victoryToolTip", e.RowIndex].Value.ToString() + Environment.NewLine +
								"Draw: " + dataGridMain["drawToolTip", e.RowIndex].Value.ToString() + Environment.NewLine +
								"Defeat: " + dataGridMain["defeatToolTip", e.RowIndex].Value.ToString();
						}
					}
					// Survived color and formatting
					else if (col.Equals("Survived"))
					{
						string battleResultColor = dataGridMain["battleSurviveColor", e.RowIndex].Value.ToString();
						cell.Style.ForeColor = System.Drawing.ColorTranslator.FromHtml(battleResultColor);
						int battlesCount = Convert.ToInt32(dataGridMain["battlesCountToolTip", e.RowIndex].Value);
						if (battlesCount > 1)
						{
							cell.ToolTipText = "Survived: " + dataGridMain["survivedCountToolTip", e.RowIndex].Value.ToString() + Environment.NewLine +
								"Killed: " + dataGridMain["killedcountToolTip", e.RowIndex].Value.ToString();
						}
					}
				}
				else if (mViewTankInfo.Checked)
				{
					if (col.Equals("Win Rate"))
					{
						if (dataGridMain["Win Rate", e.RowIndex].Value != DBNull.Value)
						{
							double wr = Convert.ToDouble(dataGridMain["Win Rate", e.RowIndex].Value);
							cell.Style.ForeColor = Rating.WinRateColor(wr);
						}
					}
					
				}
			}
		}

		#endregion
				
		#region Grid Right Click

		private int dataGridRightClickCol = -1;
		private int dataGridRightClickRow = -1;
		private void dataGridMain_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex != -1)
			{
				dataGridRightClickRow = e.RowIndex;
				dataGridRightClickCol = e.ColumnIndex;
				dataGridMain.CurrentCell = dataGridMain.Rows[dataGridRightClickRow].Cells[dataGridRightClickCol];
			}
			// Check if footer
			if (dataGridRightClickRow != -1 && MainSettings.View == GridView.Views.Battle)
			{
				if (Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["footer"].Value) > 0)
				{
					dataGridRightClickCol = -1;
					dataGridRightClickRow = -1;
				}
			}
		}

		private void dataGridMainPopup_Opening(object sender, CancelEventArgs e)
		{
			if (dataGridRightClickRow == -1)
			{
				e.Cancel = true; // Close if no valid cell is clicked
			}
				
		}
		

		private void dataGridMainPopup_GrindingSetup_Click(object sender, EventArgs e)
		{
			int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
			Form frm = new Forms.GrindingSetup(playerTankId);
			frm.ShowDialog();
			if (MainSettings.View == GridView.Views.Tank)
				GridShow("Refreshed grid");
		}

		private void dataGridMainPopup_BattleChart_Click(object sender, EventArgs e)
		{
			int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
			Form frm = new Forms.BattleChart(playerTankId);
			FormHelper.OpenForm(this, frm);
		}

		private void dataGridMainPopup_TankDetails_Click(object sender, EventArgs e)
		{
			int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
			Form frm = new Forms.PlayerTankDetail(playerTankId);
			FormHelper.OpenForm(this, frm);
		}

		private void dataGridMainPopup_DeleteBattle_Click(object sender, EventArgs e)
		{
			int battleId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["battle_Id"].Value);
			string sql = "select battle.battleTime, tank.name " +
				"from battle inner join playerTank on battle.playerTankId=playerTank.id inner join tank on playerTank.tankId=tank.Id " +
				"where battle.Id=@id ";
			DB.AddWithValue(ref sql, "@id", battleId, DB.SqlDataType.Int);
			DataTable dt = DB.FetchData(sql, Config.Settings.showDBErrors);
			if (dt.Rows.Count > 0)
			{
				DateTime battleTime = Convert.ToDateTime(dt.Rows[0]["battleTime"]);
				string tankName = dt.Rows[0]["name"].ToString();
				Code.MsgBox.Button answer = Code.MsgBox.Show("Do you really want to delete this battle:" + Environment.NewLine + Environment.NewLine +
					"  Battle: " + battleTime + Environment.NewLine +
					"  Tank:   " + tankName, "Delete battle", MsgBoxType.OKCancel, this);
				if (answer == MsgBox.Button.OKButton)
				{

					sql =
						"delete from battleAch where battleId=@battleId; " +
						"delete from battleFrag where battleId=@battleId; " +
						"delete from battle where id=@battleId; ";
					DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.VarChar);
					DB.ExecuteNonQuery(sql);
					GridShow("Deleted battle, grid refreshed");
				}
			}
		}

		private void dataGridMainPopup_FilterOnTank_Click(object sender, EventArgs e)
		{
			int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
			int tankId = TankData.GetTankID(playerTankId);
			if (tankId != 0)
			{
				TankFilterMenuUncheck(true, true, true, true, false);
				mTankFilter_All.Checked = false;
				MainSettings.GetCurrentGridFilter().TankId = tankId;
				GridShow("Filtered on tank: " + TankData.GetTankName(tankId));
			}
			
		}




		#endregion

		#region Grid Col Widht Changed

		private void dataGridMain_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
		{
			if (mainGridSaveColWidth && MainSettings.View != GridView.Views.Overall)
			{
				int newWidth = e.Column.Width;
				if (newWidth < 35)
				{
					newWidth = 35;
					dataGridMain.Columns[e.Column.HeaderText].Width = newWidth;
				}
				ColListHelper.SaveColWidth(e.Column.HeaderText, newWidth);
			}
		}

		#endregion

		#region Grid Scroll Handling

		private bool scrollingY = false;
		private bool scrollingX = false;

		private void RefreshScrollbars()
		{
			// Set scrollbar properties according to grid content
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
			try
			{
				int posBefore = dataGridMain.FirstDisplayedScrollingRowIndex;
				dataGridMain.FirstDisplayedScrollingRowIndex = scrollY.ScrollPosition;
				if (posBefore != dataGridMain.FirstDisplayedScrollingRowIndex) Refresh();
			}
			catch (Exception)
			{
				// throw;
			}
			
		}

		private void scrollY_MouseUp(object sender, MouseEventArgs e)
		{
			scrollingY = false;
		}

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
			try
			{
				int posBefore = dataGridMain.FirstDisplayedScrollingColumnIndex;
				dataGridMain.FirstDisplayedScrollingColumnIndex = scrollX.ScrollPosition;
				if (posBefore != dataGridMain.FirstDisplayedScrollingColumnIndex) Refresh();
			}
			catch (Exception)
			{
				// throw;
			}
		}

		private void dataGridMain_SelectionChanged(object sender, EventArgs e)
		{
			MoveScrollBar(); // Move scrollbar according to grid movements
		}

		private void MoveScrollBar()
		{
			scrollX.ScrollPosition = dataGridMain.FirstDisplayedScrollingColumnIndex;
			scrollY.ScrollPosition = dataGridMain.FirstDisplayedScrollingRowIndex;
		}

		private void dataGridMain_MouseWheel(object sender, MouseEventArgs e)
		{
			// Enable mouse wheel scrolling for datagrid
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
		
		#region App, DB and other Settings + Help/About + Chart

		private void toolItemViewChart_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.BattleChart(0);
			FormHelper.OpenForm(this, frm);
		}
	
		private void toolItemSettingsApp_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.ApplicationSetting();
			frm.ShowDialog();
			SetFormTitle();
			// After settings changed, go to overview
			ChangeView(GridView.Views.Overall, true);
			SetListener(false);
			SetStatus2("Refreshed grid");
		}

		private void toolItemUpdateDataFromAPI_Click(object sender, EventArgs e)
		{
			RunWotApi();
		}

		private void RunWotApi(bool autoRun = false)
		{
			Form frm = new Forms.UpdateFromApi(autoRun);
			frm.ShowDialog();
		}

		private void toolItemSettingsRun_Click(object sender, EventArgs e)
		{
			mSettingsRun.Checked = !mSettingsRun.Checked;
			// Set Start - Stop button properties
			if (mSettingsRun.Checked)
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
			RunDossierFileCheck();
		}

		private void RunDossierFileCheck()
		{
			// Dossier file manual handling
			SetStatus2("Starting manual dossier check...");
			dossier2json d2j = new dossier2json();
			d2j.ManualRunInBackground("Running manual dossier file check...", false);
			//SetFormTitle();
			//GridShow(result);
		}

		private void toolItemSettingsForceUpdateFromPrev_Click(object sender, EventArgs e)
		{
			RunDossierFileCheckWithForceUpdate();
		}

		private void RunDossierFileCheckWithForceUpdate()
		{
			SetStatus2("Starting dossier file check with force update...");
			dossier2json d2j = new dossier2json();
			d2j.ManualRunInBackground("Running dossier file check with force update...", true);
		
		}

		private void toolItemShowDbTables_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.DatabaseTable();
			FormHelper.OpenForm(this, frm);
		}

		private void toolItemImportBattlesFromWotStat_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.ImportWotStat();
			frm.ShowDialog();
		}

		private void toolItemHelp_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.About();
			frm.ShowDialog();
		}

		#endregion

		private void ViewRangeTesting()
		{
			Form frm = new Forms.ViewRange();
			frm.ShowDialog();
		}

		private void mSettingsTestAddBattleResult_Click(object sender, EventArgs e)
		{
			Battle2json.CheckBattleResultNewFiles();
		}

		private void mTankFilter_GetInGarage_Click(object sender, EventArgs e)
		{
			InGarageApiResult.status = "";
			Form frm = new Forms.InGarageApi();
			frm.ShowDialog();
			if (InGarageApiResult.status == "ok")
			{
				frm = new Forms.InGarageProcessData();
				frm.ShowDialog();
				if (InGarageApiResult.changeFavList)
				{
					// After fav list changes reload menu
					SetFavListMenu(); // Reload fav list items
					GridShow("Refreshed grid after 'In Garage' tank list updated"); // Refresh grid now
				}
			}
		}
	}
}
