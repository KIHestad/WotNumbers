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
using System.Net;
using System.Diagnostics;
using WinApp.Gadget;

namespace WinApp.Forms
{
	public partial class Main : Form
	{
		#region Init Content and Layout

		private bool Init = true;
		private bool LoadConfigOK = true;
		private string LoadConfigMsg = "";
		private ConfigData.PosSize mainFormPosSize = new ConfigData.PosSize();
		private int currentPlayerId = 0;
		private bool mainGridFormatting = false; // Controls if grid should be formattet or not
		private bool mainGridSaveColWidth = false; // Controls if change width on cols should be saved
		private FormWindowState mainFormWindowsState = FormWindowState.Normal;

		public Main()
		{
			InitializeComponent();
			// Get Config
			LoadConfigOK = Config.GetConfig(out LoadConfigMsg);
			currentPlayerId = Config.Settings.playerId;
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
			// Hide info panel at startup
			panelInfo.Height = 0;
			// Log startup
			Log.AddToLogBuffer("", false);
			Log.AddToLogBuffer("********* Application startup *********", true);
			Log.AddToLogBuffer("", false);
			// Make sure borderless form do not cover task bar when maximized
			Screen screen = Screen.FromControl(this);
			this.MaximumSize = screen.WorkingArea.Size;
			// Maximize now if last settings
			if (mainFormPosSize.WindowState == FormWindowState.Maximized)
			{
				this.WindowState = FormWindowState.Maximized;
				mainFormWindowsState = FormWindowState.Maximized;
			}
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
			toolMain.ShowItemToolTips = true;
			mBattles.Visible = false;
			mTankFilter.Visible = false;
			mRefreshSeparator.Visible = true;
			mColumnSelect.Visible = false;
			mMode.Visible = false;
			mGadget.Visible = true;
			mHomeEdit.Visible = true;
			mBattleGroup.Visible = false;
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
			dataGridMain.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			dataGridMain.ColumnHeadersDefaultCellStyle.BackColor = ColorTheme.GridBorders;
			dataGridMain.ColumnHeadersDefaultCellStyle.ForeColor = ColorTheme.ControlFont;
			dataGridMain.ColumnHeadersDefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
			dataGridMain.ColumnHeadersDefaultCellStyle.SelectionBackColor = ColorTheme.GridSelectedHeaderColor;
			dataGridMain.DefaultCellStyle.BackColor = ColorTheme.FormBack;
			dataGridMain.DefaultCellStyle.ForeColor = ColorTheme.ControlFont;
			dataGridMain.DefaultCellStyle.SelectionForeColor = ColorTheme.ControlFont;
			dataGridMain.DefaultCellStyle.SelectionBackColor = ColorTheme.GridSelectedCellColor;
			dataGridMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridMain.ColumnHeadersDefaultCellStyle.Padding = new Padding(2, 4, 0, 4);
			dataGridMain.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
			// Set font size
			int fontSize = Config.Settings.gridFontSize;
			if (fontSize < 6) fontSize = 8;
			dataGridMain.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", fontSize);
			dataGridMain.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", fontSize);
			dataGridMain.RowHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", fontSize);
		}


		private ContextMenu notifyIconContextMenu;
		private MenuItem notifyIconMenuItem1;
		private void CreateNotifyIconContextMenu()
		{
			notifyIconContextMenu = new System.Windows.Forms.ContextMenu();
			notifyIconMenuItem1 = new System.Windows.Forms.MenuItem();
			// Initialize notifyIconContextMenu 
			notifyIconContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { notifyIconMenuItem1 });
			// Initialize notifyIconMenuItem1 
			notifyIconMenuItem1.Index = 0;
			notifyIconMenuItem1.Text = "Exit";
			notifyIconMenuItem1.Click += new System.EventHandler(notifyIconMenuItem1_Click);
		}

		private void notifyIconMenuItem1_Click(object Sender, EventArgs e)
		{
			// Close the form, which closes the application. 
			this.Close();
		}

		private void CreateDataGridContextMenu()
		{
			// Datagrid context menu (Right click on Grid)
			ContextMenuStrip dataGridMainPopup = new ContextMenuStrip();
			dataGridMainPopup.Renderer = new StripRenderer();
			dataGridMainPopup.BackColor = ColorTheme.ToolGrayMainBack;
			ToolStripSeparator dataGridMainPopup_Separator1 = new ToolStripSeparator();
			ToolStripSeparator dataGridMainPopup_Separator2 = new ToolStripSeparator();
			ToolStripSeparator dataGridMainPopup_Separator3 = new ToolStripSeparator();
			ToolStripSeparator dataGridMainPopup_Separator4 = new ToolStripSeparator();
			
			ToolStripMenuItem dataGridMainPopup_TankDetails = new ToolStripMenuItem("Tank Details");
			dataGridMainPopup_TankDetails.Image = imageListToolStrip.Images[1];
			dataGridMainPopup_TankDetails.Click += new EventHandler(dataGridMainPopup_TankDetails_Click);
			
			ToolStripMenuItem dataGridMainPopup_BattleChart = new ToolStripMenuItem("Chart (Selected Tank)");
			dataGridMainPopup_BattleChart.Image = imageListToolStrip.Images[2];
			dataGridMainPopup_BattleChart.Click += new EventHandler(dataGridMainPopup_BattleChart_Click);
			
			ToolStripMenuItem dataGridMainPopup_GrindingSetup = new ToolStripMenuItem("Grinding Setup");
			dataGridMainPopup_GrindingSetup.Image = imageListToolStrip.Images[3];
			dataGridMainPopup_GrindingSetup.Click += new EventHandler(dataGridMainPopup_GrindingSetup_Click);
			
			ToolStripMenuItem dataGridMainPopup_FilterOnTank = new ToolStripMenuItem("Filter on this tank");
			dataGridMainPopup_FilterOnTank.Image = imageListToolStrip.Images[4];
			dataGridMainPopup_FilterOnTank.Click += new EventHandler(dataGridMainPopup_FilterOnTank_Click);
			
			ToolStripMenuItem dataGridMainPopup_FavListAddTank = new ToolStripMenuItem("Add tank to favourite tank list");
			dataGridMainPopup_FavListAddTank.Image = imageListToolStrip.Images[5];
			dataGridMainPopup_FavListAddTank.Click += new EventHandler(dataGridMainPopup_FavListAddTank_Click);
			
			ToolStripMenuItem dataGridMainPopup_FavListRemoveTank = new ToolStripMenuItem("Remove tank from favourite tank list");
			dataGridMainPopup_FavListRemoveTank.Image = imageListToolStrip.Images[6];
			dataGridMainPopup_FavListRemoveTank.Click += new EventHandler(dataGridMainPopup_FavListRemoveTank_Click);
			
			ToolStripMenuItem dataGridMainPopup_FavListCreateNew = new ToolStripMenuItem("Create new favourite tank list");
			dataGridMainPopup_FavListCreateNew.Image = imageListToolStrip.Images[7];
			dataGridMainPopup_FavListCreateNew.Click += new EventHandler(dataGridMainPopup_FavListCreateNew_Click);
			
			ToolStripMenuItem dataGridMainPopup_DeleteBattle = new ToolStripMenuItem("Delete this battle");
			dataGridMainPopup_DeleteBattle.Image = imageListToolStrip.Images[8];
			dataGridMainPopup_DeleteBattle.Click += new EventHandler(dataGridMainPopup_DeleteBattle_Click);

			ToolStripMenuItem dataGridMainPopup_BattleDetails = new ToolStripMenuItem("Battle Details");
			dataGridMainPopup_BattleDetails.Image = imageListToolStrip.Images[9];
			dataGridMainPopup_BattleDetails.Click += new EventHandler(dataGridMainPopup_BattleDetails_Click);

			ToolStripMenuItem dataGridMainPopup_TankWN8 = new ToolStripMenuItem("WN8 Tank Details");
			dataGridMainPopup_TankWN8.Image = imageListToolStrip.Images[10];
			dataGridMainPopup_TankWN8.Click += new EventHandler(dataGridMainPopup_TankWN8_Click);

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
						dataGridMainPopup_TankDetails,
						dataGridMainPopup_TankWN8,
						dataGridMainPopup_Separator2,
						dataGridMainPopup_BattleChart, 
						dataGridMainPopup_GrindingSetup, 
						dataGridMainPopup_Separator1,
						dataGridMainPopup_FavListAddTank,
						dataGridMainPopup_FavListRemoveTank,
						dataGridMainPopup_FavListCreateNew
					});
					break;
				case GridView.Views.Battle:
					dataGridMainPopup.Items.AddRange(new ToolStripItem[] 
					{ 
						//TODO: Temp remove features
						dataGridMainPopup_BattleDetails,
						dataGridMainPopup_TankDetails, 
						dataGridMainPopup_Separator4,
						dataGridMainPopup_BattleChart, 
						dataGridMainPopup_GrindingSetup,
						dataGridMainPopup_Separator1,
						dataGridMainPopup_FilterOnTank,
						dataGridMainPopup_Separator2,
						dataGridMainPopup_FavListAddTank,
						dataGridMainPopup_FavListRemoveTank,
						dataGridMainPopup_FavListCreateNew,
						dataGridMainPopup_Separator3,
						dataGridMainPopup_DeleteBattle
					});
					break;
				default:
					break;
			}
			
			//Assign to datagridview
			dataGridMain.ContextMenuStrip = dataGridMainPopup;
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
			try
			{
				// Systray icon with context menu
				CreateNotifyIconContextMenu();
				notifyIcon.ContextMenu = notifyIconContextMenu;
				notifyIcon.Visible = Config.Settings.notifyIconUse;
				this.ShowInTaskbar = !Config.Settings.notifyIconUse;
				MainTheme.FormExitAsMinimize = Config.Settings.notifyIconFormExitToMinimize;

				// Log startup to file now
				Log.WriteLogBuffer();
				// Start WoT if autoRun is enabled
				if (Config.Settings.wotGameAutoStart)
					StartWoTGame();
				// Ready to draw form
				Init = false;
				// Create IronPython Engine
				PythonEngine.CreateEngine();

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
					TankHelper.GetAllLists();
				}
				// Update file watchers
				string result = dossier2json.UpdateDossierFileWatcher(this);
				Battle2json.UpdateBattleResultFileWatcher();
				// Check DB Version an dupgrade if needed
				bool versionOK = DBVersion.CheckForDbUpgrade(this);
				// Add init items to Form
				SetFormTitle();
				SetFavListMenu();
				SetListener(false);
				// Get Images
				ImageHelper.CreateTankImageTable();
				ImageHelper.LoadTankImages();
				ImageHelper.CreateMasteryBageImageTable();
				ImageHelper.CreateTankTypeImageTable();
				ImageHelper.CreateNationImageTable();
				// Show view
				ChangeView(GridView.Views.Overall, true);
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
				// Check for new version
				RunCheckForNewVersion();
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				MsgBox.Show("Error occured initializing application:" + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine, "Startup error", this);
			}
			
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

		#region Check For New Verion and Download

		private BackgroundWorker bwCheckForNewVersion;
		private bool _onlyCheckVersionWithMessage = false;
		private void RunCheckForNewVersion(bool onlyCheckVersionWithMessage = false)
		{
			_onlyCheckVersionWithMessage = onlyCheckVersionWithMessage;
			bwCheckForNewVersion = new BackgroundWorker();
			bwCheckForNewVersion.WorkerSupportsCancellation = false;
			bwCheckForNewVersion.WorkerReportsProgress = false;
			bwCheckForNewVersion.DoWork += new DoWorkEventHandler(CheckForNewVersion.GetVersion);
			bwCheckForNewVersion.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PerformCheckForNewVersion);
			//bwCheckForNewVersion.RunWorkerCompleted += 
			if (bwCheckForNewVersion.IsBusy != true)
			{
				bwCheckForNewVersion.RunWorkerAsync();
			}
		}
		
		private void RunInitialDossierFileCheck(string message)
		{
			if (DBVersion.RunWotApi)
				RunWotApi(true);
			if (DBVersion.RunRecalcBattleWN8)
				RunRecalcBattleWN8(true);
			// Check for dossier update
			RunDossierFileCheck(message, DBVersion.RunDossierFileCheckWithForceUpdate);
		}

		private void PerformCheckForNewVersion(object sender, RunWorkerCompletedEventArgs e)
		{
			VersionInfo vi = CheckForNewVersion.versionInfo;
			if (vi.error)
			{
				// Could not check for new version, run normal dossier file check
				if (!_onlyCheckVersionWithMessage)
				{
					RunInitialDossierFileCheck("Could not check for new version, no connection to Wot Numbers website");
				}
				// Message if debug mode or manual version check
				if (Config.Settings.showDBErrors || _onlyCheckVersionWithMessage)
				{
					Code.MsgBox.Show("Could not check for new version, could be that you have no Internet access." + Environment.NewLine + Environment.NewLine +
										vi.errorMsg + Environment.NewLine + Environment.NewLine + Environment.NewLine,
										"Version check failed",
										this);
				}
			}
			else if (vi.maintenanceMode)
			{
				// Web is currently in maintance mode, just skip version check - perform normal startup
				if (!_onlyCheckVersionWithMessage)
				{
					RunInitialDossierFileCheck("Wot Numbers website in maintenance mode, version check skipped");
				}
				else
				{
					// Message if debug mode
					if (Config.Settings.showDBErrors || _onlyCheckVersionWithMessage)
						Code.MsgBox.Show("Could not check for new version, Wot Numbers web server is currently in maintenance mode." + Environment.NewLine + Environment.NewLine,
											"Version check terminated", this);
				}
			}
			else
			{
				// version found, check result
				double currentVersion = CheckForNewVersion.MakeVersionToDouble(AppVersion.AssemblyVersion);
				double newVersion = newVersion = CheckForNewVersion.MakeVersionToDouble(vi.version);
				if (currentVersion >= newVersion)
				{
					if (_onlyCheckVersionWithMessage)
					{
						// Message if debug mode
						Code.MsgBox.Show("You are running the latest version: " + Environment.NewLine + Environment.NewLine +
										"Wot Numbers " + AppVersion.AssemblyVersion + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine,
										"Version checked", this);
					}
					else
					{
						// Normal startup, no new version
						if (vi.message != "" && vi.messageDate <= DateTime.Now)
						{
							if (Config.Settings.readMessage == null || Config.Settings.readMessage < vi.messageDate)
							{
								// Display message from Wot Numbers API
								MsgBox.Show(vi.message + Environment.NewLine + Environment.NewLine, "Message published " + vi.messageDate.ToString("dd.MM.yyyy"), this);
								// Save read message
								Config.Settings.readMessage = DateTime.Now;
								string msg = "";
								Config.SaveConfig(out msg);
							}
						}
						// Force dossier file check 
						if (vi.runWotApi <= DateTime.Now) // Avoid running future planned updates
						{
							if (Config.Settings.doneRunWotApi == null || Config.Settings.doneRunWotApi < vi.runWotApi)
								DBVersion.RunWotApi = true;
						}
						// Force dossier file check or just normal - replaces RunInitialDossierFileCheck();
						if (vi.runForceDossierFileCheck <= DateTime.Now) // Avoid running future planned updates
						{
							if (Config.Settings.doneRunForceDossierFileCheck == null || Config.Settings.doneRunForceDossierFileCheck < vi.runForceDossierFileCheck)
								DBVersion.RunDossierFileCheckWithForceUpdate = true;
						}
						RunInitialDossierFileCheck("You are running the latest version (Wot Numbers " + AppVersion.AssemblyVersion + ")");
					}
				}
				else
				{
					// New version avaliable
					string msg = "Wot Numbers version " + vi.version + " is available for download." + Environment.NewLine + Environment.NewLine +
						"You are currently running version: " + AppVersion.AssemblyVersion + "." + Environment.NewLine + Environment.NewLine +
						"Press 'OK' to download the new version now." + Environment.NewLine + Environment.NewLine;
					Code.MsgBox.Button answer = Code.MsgBox.Show(msg, "New version avaliable for download", MsgBoxType.OKCancel, this);
					if (answer == MsgBox.Button.OKButton)
					{
						Form frm = new Forms.Download();
						frm.ShowDialog();
						RunInitialDossierFileCheck("New version downloaded (Wot Numbers " + vi.version + "), running installed version (Wot Numbers " + AppVersion.AssemblyVersion + ")");
					}
					else
					{
						if (!_onlyCheckVersionWithMessage)
							RunInitialDossierFileCheck("New version found (Wot Numbers " + vi.version + "), running installed version (Wot Numbers " + AppVersion.AssemblyVersion + ")");
					}
				}
				// Enable Settings menues
				mSettingsRun.Enabled = true;
				mSettingsRunBattleCheck.Enabled = true;
				mUpdateDataFromAPI.Enabled = true;
				mRecalcBattleWN8.Enabled = true;
				mImportBattlesFromWotStat.Enabled = true;
				mSettingsAppLayout.Enabled = true;
				mSettingsApp.Enabled = true;
			}
		}

		#endregion

		#region Common Events

		private int status2DefaultColor = 200;
		private int status2fadeColor = 200;

		private void NewBattleFileChanged(object source, FileSystemEventArgs e)
		{
			// New battle saved
			ShowView("New battle data fetched, view refreshed");
			if (notifyIcon.Visible)
				notifyIcon.ShowBalloonTip(1000);
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

		private bool Status2AutoEnabled = true;
		private void SetStatus2(string txt = "")
		{
			if (Status2AutoEnabled)
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
				notifyIcon.Text = "Wot Numbers - Running";
				lblStatus1.ForeColor = System.Drawing.Color.ForestGreen;
			}
			else
			{
				lblStatus1.Text = "Stopped";
				notifyIcon.Text = "Wot Numbers - Stopped";
				lblStatus1.ForeColor = System.Drawing.Color.DarkRed;
				
			}
			string result = dossier2json.UpdateDossierFileWatcher(this);
			Battle2json.UpdateBattleResultFileWatcher();
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
			notifyIcon.Visible = false;
			// Save config to save current screen pos and size
			Config.Settings.posSize.WindowState = this.WindowState;
			string msg = "";
			Config.SaveConfig(out msg);
			// Log exit
			Log.AddToLogBuffer("Application Exit", true);
			Log.WriteLogBuffer();
		}

		private void Main_Resize(object sender, EventArgs e)
		{
			if (!Init)
			{
				ResizeNow();
				// Remember if new viewable size is normal or maximized
				if (this.WindowState != FormWindowState.Minimized)
					mainFormWindowsState = this.WindowState;
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
			toolMain.Width = panelMainArea.Width - toolMain.Left;
			if (MainSettings.View != GridView.Views.Overall)
			{
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
			SetFormTitle();
			SetStatus2("Refreshing view...");
			ShowView("View refreshed");
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
				// Default settings
				dataGridMain.ColumnHeadersVisible = true;
				dataGridMain.RowHeadersVisible = true;
				scrollCorner.BringToFront();
				scrollX.BringToFront();
				scrollY.BringToFront();
				dataGridMain.BringToFront();
				// Remove home view edit mode if enabled
				if (mHomeEdit.Checked)
				{
					mHomeEdit.Checked = false;
					GadgetEditModeChange();
				}
				// Set new values according to new selected view
				switch (MainSettings.View)
				{
					case GridView.Views.Overall:
						// Select view
						mViewOverall.Checked = true;
						dataGridMain.Visible = false;
						scrollX.Visible = false;
						scrollY.Visible = false;
						scrollCorner.Visible = false;
						panelInfo.Height = 0;
						// Show/Hide Tool Items
						mBattles.Visible = true;
						mGadget.Visible = true;
						mHomeEdit.Visible = true;
						mBattles.Visible = false;
						mTankFilter.Visible = false;
						mColumnSelect.Visible = false;
						mMode.Visible = false;
						mBattleGroup.Visible = false;
						mRefreshSeparator.Visible = true;
						break;
					case GridView.Views.Tank:
						// Select view
						mViewTankInfo.Checked = true;
						// Show grid
						dataGridMain.Visible = true;
						scrollX.Visible = true;
						scrollY.Visible = true;
						scrollCorner.Visible = true;
						dataGridMain.RowHeadersWidth = Config.Settings.mainGridTankRowWidht;
						// Show/Hide Tool Items
						mBattles.Visible = false;
						mTankFilter.Visible = true;
						mColumnSelect.Visible = true;
						mMode.Visible = true;
						mModeClan.Visible = false;
						mModeCompany.Visible = false;
						mModeRandom.Visible = false;
						mModeRandomTankCompany.Visible = true;
						mModeRandomSoloPlatoon.Visible = false;
						mGadget.Visible = false;
						mHomeEdit.Visible = false;
						mBattleGroup.Visible = false;
						mRefreshSeparator.Visible = true;
						mColumnSelect_Edit.Text = "Edit Tank View...";
						mColumnSelect.ToolTipText = "Select Tank View";
						// Get Column Setup List - also finds correct tank filter/fav list
						SetColListMenu();
						// Get Battle mode
						SetBattleModeMenu();
						// Add datagrid context menu (right click on datagrid)
						CreateDataGridContextMenu();
						// Info slider hide
						InfoPanelSlideStart(false);
						this.ActiveControl = dataGridMain;
						break;
					case GridView.Views.Battle:
						// Select view
						mViewBattles.Checked = true;
						// Show grid
						dataGridMain.Visible = true;
						scrollX.Visible = true;
						scrollY.Visible = true;
						scrollCorner.Visible = true;
						dataGridMain.RowHeadersWidth = Config.Settings.mainGridBattleRowWidht;
						// Show/Hide Tool Items
						mBattles.Visible = false;
						mTankFilter.Visible = true;
						mColumnSelect.Visible = true;
						mMode.Visible = true;
						mModeClan.Visible = true;
						mModeCompany.Visible = true;
						mModeRandom.Visible = true;
						mModeRandomTankCompany.Visible = false;
						mModeRandomSoloPlatoon.Visible = true;
						mBattleGroup.Visible = true;
						mGadget.Visible = false;
						mHomeEdit.Visible = false;
						mRefreshSeparator.Visible = true;
						mColumnSelect_Edit.Text = "Edit Battle View...";
						mColumnSelect.ToolTipText = "Select Battle View";
						// Get Column Setup List  - also finds correct tank filter/fav list
						SetColListMenu();
						// Get Battle mode
						SetBattleModeMenu();
						// Add datagrid context menu (right click on datagrid)
						CreateDataGridContextMenu();
						// Info slider hide
						InfoPanelSlideStart(false);
						this.ActiveControl = dataGridMain;
						break;
				}
				toolMain.Renderer = new StripRenderer();
				ShowView(); // Changed view, no status message applied, sets in GridShow
			}
		}

		private bool homeViewCreated = false;
		private void ShowView(string Status2Message = "", bool ShowDefaultStatus2Message = true)
		{
			try
			{
				if (Config.Settings.playerId == 0)
				{
					SetStatus2("No player selected, please check application settings");
				}
				else
				{
					if (currentPlayerId != Config.Settings.playerId)
					{
						// Stop file watchers if running
						int runState = Config.Settings.dossierFileWathcherRun;
						if (runState == 1)
						{
							Config.Settings.dossierFileWathcherRun = 0;
							SetListener();
							Application.DoEvents();
						}
						currentPlayerId = Config.Settings.playerId;
						SetFormTitle();
						MsgBox.Show("Current player is changed because new player data is fetched." +
							Environment.NewLine + Environment.NewLine + "Player changed to: " + Config.Settings.playerNameAndServer +
							Environment.NewLine + Environment.NewLine, "Current player chenged");
						// Return to prev file watcher state
						if (runState != Config.Settings.dossierFileWathcherRun)
						{
							Config.Settings.dossierFileWathcherRun = runState;
							SetListener();
							Application.DoEvents();
						}
					}
					switch (MainSettings.View)
					{
						case GridView.Views.Overall:
							// New home view
							if (!homeViewCreated)
							{
								HomeViewCreate("Creating Home View...");
								homeViewCreated = true;
							}
							if (Status2Message == "" && ShowDefaultStatus2Message) Status2Message = "Home view selected";
							HomeViewRefresh(Status2Message);
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
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
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
				ShowView("Selected column setup: " + selectedMenu.Text);
			}
		}

		private void toolItemColumnSelect_Edit_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.ColList();
			frm.ShowDialog();
			SetColListMenu(); // Refresh column setup list now
			ShowView("Refreshed grid after column setup change"); // Refresh grid now
		}

		#endregion

		#region Menu Items: Tank Filter / Fav List 

		private string tankFilterFavListName = "";
		private string tankFilterManualFilter = "";
		private void SetTankFilterMenuName()
		{
			string s = tankFilterFavListName;
			GridFilter.FavListShowType favListShowType = MainSettings.GetCurrentGridFilter().FavListShow;
			if (s == "")
			{
				if (favListShowType == GridFilter.FavListShowType.MyTanks)
					s = "My Tanks";
				else if (favListShowType == GridFilter.FavListShowType.AllTanksNotOwned)
					s = "All Tanks";
			}
			if (tankFilterManualFilter != "")
				s += " - " + tankFilterManualFilter;
			mTankFilter.Text = s;
		}
		
		private void SetFavListMenu()
		{
			// Uncheck favlist from menu
			FavListMenuUncheck();
			// Hide all favlist
			for (int i = 1; i <= 10; i++)
			{
				ToolStripMenuItem menuItem = mTankFilter.DropDownItems["mTankFilter_Fav" + i.ToString("00")] as ToolStripMenuItem;
				menuItem.Visible = false;
			}
			// Add favlist to menu
			GridFilter.FavListShowType newShowType = GridFilter.FavListShowType.MyTanks;
			string sql = "select * from favList where position is not null and name is not null order by position";
			DataTable dt = DB.FetchData(sql, Config.Settings.showDBErrors);
			if (dt.Rows.Count > 0)
			{
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
							tankFilterFavListName = menuItem.Text;
							newShowType = GridFilter.FavListShowType.FavList;
						}
					}
				}
			}
			// If no faclist is visible, select all tanks
			MainSettings.GetCurrentGridFilter().FavListShow = newShowType;
			if (newShowType == GridFilter.FavListShowType.MyTanks)
				mTankFilter_All.Checked = true;
			// Set menu name
			SetTankFilterMenuName();
		}

		private void FavListMenuUncheck()
		{
			// Deselect all tanks
			mTankFilter_All.Checked = false;
			mTankFilter_All_NotOwned.Checked = false;
			// Deselect all favlist
			for (int i = 1; i <= 10; i++)
			{
				ToolStripMenuItem menuItem = mTankFilter.DropDownItems["mTankFilter_Fav" + i.ToString("00")] as ToolStripMenuItem;
				menuItem.Checked = false;
			}

			// Remove menu name
			tankFilterFavListName = "";
		}

		private void toolItemTankFilter_All_Click(object sender, EventArgs e)
		{
			// Changed FavList
			GridFilter.Settings gf = MainSettings.GetCurrentGridFilter();
			gf.FavListShow = GridFilter.FavListShowType.MyTanks;
			MainSettings.UpdateCurrentGridFilter(gf);
			// check fav list menu select
			FavListMenuUncheck();
			mTankFilter_All.Checked = true;
			SetTankFilterMenuName();
			// Set menu item and show grid
			ShowView("Selected all tanks owned");
		}

		private void mTankFilter_All_NotOwned_Click(object sender, EventArgs e)
		{
			// Changed FavList
			GridFilter.Settings gf = MainSettings.GetCurrentGridFilter();
			gf.FavListShow = GridFilter.FavListShowType.AllTanksNotOwned;
			gf.BattleMode = GridFilter.BattleModeType.All;
			MainSettings.UpdateCurrentGridFilter(gf);
			// check fav list menu select
			FavListMenuUncheck();
			mTankFilter_All_NotOwned.Checked = true;
			SetTankFilterMenuName();
			SetBattleModeMenu();
			// Set menu item and show grid
			ShowView("Selected all tanks");
		}

		private void toolItem_Fav_Clicked(object sender, EventArgs e)
		{
			// Selected favList from toolbar
			ToolStripMenuItem selectedMenu = (ToolStripMenuItem)sender;
			// Get favListId for selected favList
			int newFavListId = FavListHelper.GetId(selectedMenu.Text);
			// Changed FavList
			GridFilter.Settings gf = MainSettings.GetCurrentGridFilter();
			gf.FavListId = newFavListId;
			gf.FavListName = selectedMenu.Text;
			gf.FavListShow = GridFilter.FavListShowType.FavList;
			MainSettings.UpdateCurrentGridFilter(gf);
			// check fav list menu select
			FavListMenuUncheck();
			selectedMenu.Checked = true;
			tankFilterFavListName = selectedMenu.Text;
			SetTankFilterMenuName();
			// Set menu item and show grid
			ShowView("Selected favourite tank list: " + selectedMenu.Text);
		}

		private void SelectFavMenuItem()
		{
			// Programatically select all tanks or favourite tank list
			switch (MainSettings.GetCurrentGridFilter().FavListShow)
			{
				case GridFilter.FavListShowType.UseCurrent:
					// No action, use previous selected tanks filter
					break;
				case GridFilter.FavListShowType.MyTanks:
					// Remove all filters, select All Tanks
					FavListMenuUncheck();
					mTankFilter_All.Checked = true;
					SetTankFilterMenuName();
					break;
				case GridFilter.FavListShowType.FavList:
					FavListMenuUncheck();
					// Find favlist in menu and put on checkbox
					for (int i = 1; i <= 10; i++)
					{
						ToolStripMenuItem menuItem = mTankFilter.DropDownItems["mTankFilter_Fav" + i.ToString("00")] as ToolStripMenuItem;
						if (menuItem.Text == MainSettings.GetCurrentGridFilter().FavListName)
						{
							menuItem.Checked = true;
							tankFilterFavListName = MainSettings.GetCurrentGridFilter().FavListName;
						}
					}
					SetTankFilterMenuName();
					break;
			}
		}

		private void TankFilterMenuUncheck(bool tier, bool country, bool type, bool reopenMenu = true)
		{
			tankFilterNation = 0;
			tankFilterTier = 0;
			tankFilterType = 0;
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
				mTankFilter_Tier.Text = "Tier";
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
				mTankFilter_Country.Text = "Nation";
			}
			if (type)
			{
				mTankFilter_TypeHT.Checked = false;
				mTankFilter_TypeLT.Checked = false;
				mTankFilter_TypeMT.Checked = false;
				mTankFilter_TypeSPG.Checked = false;
				mTankFilter_TypeTD.Checked = false;
				mTankFilter_Type.Text = "Tank Type";
			}
			// Count selected menu items
			if (mTankFilter_CountryChina.Checked) tankFilterNation++;
			if (mTankFilter_CountryFrance.Checked) tankFilterNation++;
			if (mTankFilter_CountryGermany.Checked) tankFilterNation++;
			if (mTankFilter_CountryUK.Checked) tankFilterNation++;
			if (mTankFilter_CountryUSA.Checked) tankFilterNation++;
			if (mTankFilter_CountryUSSR.Checked) tankFilterNation++;
			if (mTankFilter_CountryJapan.Checked) tankFilterNation++;

			if (mTankFilter_TypeLT.Checked) tankFilterType++;
			if (mTankFilter_TypeMT.Checked) tankFilterType++;
			if (mTankFilter_TypeHT.Checked) tankFilterType++;
			if (mTankFilter_TypeTD.Checked) tankFilterType++;
			if (mTankFilter_TypeSPG.Checked) tankFilterType++;
			
			if (mTankFilter_Tier1.Checked) tankFilterTier++;
			if (mTankFilter_Tier2.Checked) tankFilterTier++;
			if (mTankFilter_Tier3.Checked) tankFilterTier++;
			if (mTankFilter_Tier4.Checked) tankFilterTier++;
			if (mTankFilter_Tier5.Checked) tankFilterTier++;
			if (mTankFilter_Tier6.Checked) tankFilterTier++;
			if (mTankFilter_Tier7.Checked) tankFilterTier++;
			if (mTankFilter_Tier8.Checked) tankFilterTier++;
			if (mTankFilter_Tier9.Checked) tankFilterTier++;
			if (mTankFilter_Tier10.Checked) tankFilterTier++;

			// Add text for manual filters
			if (tankFilterNation > 0)
				mTankFilter_Country.Text = "Nation (filtered: " + tankFilterNation + ")";
			if (tankFilterType > 0)
				mTankFilter_Type.Text = "Tank Type (filtered: " + tankFilterType + ")";
			if (tankFilterTier > 0)
				mTankFilter_Tier.Text = "Tier (filtered: " + tankFilterTier + ")";

			// Reopen menu item exept for "all tanks"
			if (reopenMenu) this.mTankFilter.ShowDropDown();
		}

		private void TankFilterMenuSelect(ToolStripMenuItem menuItem, ToolStripMenuItem parentMenuItem)
		{
			string status2message = "";
			// Update selected tankfilter type
			GridFilter.Settings gf = MainSettings.GetCurrentGridFilter();
			gf.TankId = -1; // Remove tank filter
			MainSettings.UpdateCurrentGridFilter(gf);
			// Get Tank filter
			string tankFilterMessage = "";
			string tankJoin = "";
			string whereSQL = "";
			Tankfilter(out whereSQL, out tankJoin, out tankFilterMessage);
			SetTankFilterMenuName();
			status2message = "Selected tank filter: " + tankFilterMessage;
			mTankFilter.ShowDropDown();
			parentMenuItem.ShowDropDown();
			// Done
			ShowView(status2message);
		}
		
		private void mTankFilter_Clear_Click(object sender, EventArgs e)
		{
			TankFilterMenuUncheck(true, true, true, false);
			// Update selected tankfilter 
			GridFilter.Settings gf = MainSettings.GetCurrentGridFilter();
			gf.TankId = -1; // Remove tank filter
			MainSettings.UpdateCurrentGridFilter(gf);
			// Done
			ShowView("Tank filter cleared");
		}

		private void toolItemTankFilter_Tier_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
			// Update menu item
			menuItem.Checked = !menuItem.Checked;
			if (menuItem.Checked)
				tankFilterTier++;
			else
				tankFilterTier--;
			if (tankFilterTier > 0)
				mTankFilter_Tier.Text = "Tier (filtered: " + tankFilterTier + ")";
			else
				mTankFilter_Tier.Text = "Tier";
			TankFilterMenuSelect(menuItem, mTankFilter_Tier);
		}

		private void toolItemTankFilter_Type_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
			// Update menu item
			menuItem.Checked = !menuItem.Checked;
			if (menuItem.Checked)
				tankFilterType++;
			else
				tankFilterType--;
			if (tankFilterType > 0)
				mTankFilter_Type.Text = "Tank Type (filtered: " + tankFilterType + ")";
			else
				mTankFilter_Type.Text = "Tank Type";
			TankFilterMenuSelect(menuItem, mTankFilter_Type);
		}

		private void toolItemTankFilter_Country_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
			// Update menu item
			menuItem.Checked = !menuItem.Checked;
			if (menuItem.Checked)
				tankFilterNation++;
			else
				tankFilterNation--;
			if (tankFilterNation > 0)
				mTankFilter_Country.Text = "Nation (filtered: " + tankFilterNation + ")";
			else
				mTankFilter_Country.Text = "Nation";
			TankFilterMenuSelect(menuItem, mTankFilter_Country);
		}

		private void toolItemTankFilter_Country_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				TankFilterMenuUncheck(false, true, false, false);
				ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
				TankFilterMenuSelect(menuItem, mTankFilter_Country);
			}
		}

		private void toolItemTankFilter_Type_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				TankFilterMenuUncheck(false, false, true, false);
				ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
				TankFilterMenuSelect(menuItem, mTankFilter_Type);
			}
		}

		private void toolItemTankFilter_Tier_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				TankFilterMenuUncheck(true, false, false, false);
				ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
				TankFilterMenuSelect(menuItem, mTankFilter_Tier);
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
			Form frm = new Forms.FavList();
			frm.ShowDialog();
			// After fav list changes reload menu
			SetFavListMenu(); // Reload fav list items
			ShowView("Refreshed grid after fovourite tank list change"); // Refresh grid now
		}

		#endregion

		#region Battle Grouping

		private void toolItemGroupingSelected_Click(object sender, EventArgs e)
		{
			mBattleGroup_No.Checked = false;
			mBattleGroup_TankAverage.Checked = false;
			mBattleGroup_TankSum.Checked = false;
			ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
			menuItem.Checked = true;
			mBattleGroup.Text = menuItem.Text;
			GridShowBattle("Selected grouping: " + menuItem.Text);
		}

		#endregion

		#region Menu Items: Battle Time

		private void mBattleTime_Click(object sender, EventArgs e)
		{
			SelectBattleTimeMenu((ToolStripMenuItem)sender);
		}

		private void mBattleTimeCustomChange_Click(object sender, EventArgs e)
		{
			ShowCustomBattleTimeFilter();
		}

		private void ShowCustomBattleTimeFilter()
		{
			Form frm = new Forms.BattleTimeFilterCustom();
			frm.ShowDialog();
		}

		private void mBattles_MouseDown(object sender, MouseEventArgs e)
		{
			// On right mouse click just display status message for current filter
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				ShowCustomBattleTimeFilter();
				if (GridFilter.BattleTimeFilterCustomApply)
				{
					SelectBattleTimeMenu(mBattlesCustomUse);
				}
			}
		}

		private void SelectBattleTimeMenu(ToolStripMenuItem menuItem)
		{
			mBattles1d.Checked = false;
			mBattles2d.Checked = false;
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
			mBattlesCustomUse.Checked = false;
			menuItem.Checked = true;
			if (menuItem == mBattlesCustomUse)
				mBattles.Text = menuItem.Tag.ToString(); // Different name on main tool item then menu name for battle time filter button
			else
				mBattles.Text = menuItem.Text; // Use menu name as main tool item name for battle time filter button
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
			if (gf.FavListShow == GridFilter.FavListShowType.AllTanksNotOwned)
			{
				gf.FavListShow = GridFilter.FavListShowType.MyTanks;
				SetFavListMenu();
			}
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
			foreach (var dropDownItem in mModeRandomSoloPlatoon.DropDownItems)
			{
				if (dropDownItem is ToolStripMenuItem)
				{
					ToolStripMenuItem menuItem = (ToolStripMenuItem)dropDownItem;
					menuItem.Checked = false;
				}
			}

			// check battle mode list menu select
			selectedMenu.Checked = true;
			string mainToolItemMenuText = selectedMenu.Text;
			
			mMode.Text = mainToolItemMenuText;
			// Set menu item and show grid
			ShowView("Selected battle mode: " + selectedMenu.Text);
		}

		private void SetBattleModeMenu()
		{
			bool done = false;
			foreach (var dropDownItem in mMode.DropDownItems)
			{
				string battleMode = MainSettings.GetCurrentGridFilter().BattleMode.ToString();
				if (dropDownItem is ToolStripMenuItem)
				{
					ToolStripMenuItem menuItem = (ToolStripMenuItem)dropDownItem;
					if (menuItem.Tag != null && battleMode == menuItem.Tag.ToString())
					{
						menuItem.Checked = true;
						mMode.Text = menuItem.Text;
						done = true;
					}
					else
					{
						menuItem.Checked = false;
					}
				}
			}
			if (!done)
			{
				foreach (var dropDownItem in mModeRandomSoloPlatoon.DropDownItems)
				{
					string battleMode = MainSettings.GetCurrentGridFilter().BattleMode.ToString();
					if (dropDownItem is ToolStripMenuItem)
					{
						ToolStripMenuItem menuItem = (ToolStripMenuItem)dropDownItem;
						if (menuItem.Tag != null && battleMode == menuItem.Tag.ToString())
						{
							menuItem.Checked = true;
							mMode.Text = menuItem.Text;
						}
						else
						{
							menuItem.Checked = false;
						}
					}
				}
			}
		}


		#endregion
					
		#region Fav List and Tank Filter 

		private int tankFilterNation = 0;
		private int tankFilterType = 0;
		private int tankFilterTier = 0;

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
			string tankOwnedWhereSQL = "";
			// Check favlist
			if (MainSettings.GetCurrentGridFilter().FavListShow == GridFilter.FavListShowType.FavList)
			{
				message = tankFilterFavListName;
				newJoinSQL = " INNER JOIN favListTank ON tank.id=favListTank.tankId AND favListTank.favListId=@favListId ";
				DB.AddWithValue(ref newJoinSQL, "@favListId", MainSettings.GetCurrentGridFilter().FavListId, DB.SqlDataType.Int);
			}
			else
			{
				if (MainSettings.GetCurrentGridFilter().FavListShow == GridFilter.FavListShowType.AllTanksNotOwned)
				{
					message = "All tanks";
				}
				else
				if (MainSettings.GetCurrentGridFilter().FavListShow == GridFilter.FavListShowType.MyTanks)
				{
					message = "All tanks owned";
					tankOwnedWhereSQL += " AND playerTank.id is not null ";
				}
			}
			// Check if spesific tank is filtered
			if (MainSettings.GetCurrentGridFilter().TankId != -1)
			{
				int tankId = MainSettings.GetCurrentGridFilter().TankId;
				string tankName = TankHelper.GetTankName(tankId);
				tankFilterManualFilter = tankName;
				message += " - Filtered on tank: " + tankName;
				newWhereSQL = " AND tank.id=@tankId ";
				DB.AddWithValue(ref newWhereSQL, "@tankId", tankId, DB.SqlDataType.Int);
			}
			else
			{
				// Check manual filters
				int manualFilterCount = 0;
				if (mTankFilter_Tier1.Checked) { tier += "1,"; manualFilterCount++; }
				if (mTankFilter_Tier2.Checked) { tier += "2,"; manualFilterCount++; }
				if (mTankFilter_Tier3.Checked) { tier += "3,"; manualFilterCount++; }
				if (mTankFilter_Tier4.Checked) { tier += "4,"; manualFilterCount++; }
				if (mTankFilter_Tier5.Checked) { tier += "5,"; manualFilterCount++; }
				if (mTankFilter_Tier6.Checked) { tier += "6,"; manualFilterCount++; }
				if (mTankFilter_Tier7.Checked) { tier += "7,"; manualFilterCount++; }
				if (mTankFilter_Tier8.Checked) { tier += "8,"; manualFilterCount++; }
				if (mTankFilter_Tier9.Checked) { tier += "9,"; manualFilterCount++; }
				if (mTankFilter_Tier10.Checked) { tier += "10,"; manualFilterCount++; }

				if (mTankFilter_CountryChina.Checked) { nation += "China,"; nationId += "3,"; manualFilterCount++; }
				if (mTankFilter_CountryFrance.Checked) { nation += "France,"; nationId += "4,"; manualFilterCount++; }
				if (mTankFilter_CountryGermany.Checked) { nation += "Germany,"; nationId += "1,"; manualFilterCount++; }
				if (mTankFilter_CountryUK.Checked) { nation += "UK,"; nationId += "5,"; manualFilterCount++; }
				if (mTankFilter_CountryUSA.Checked) { nation += "USA,"; nationId += "2,"; manualFilterCount++; }
				if (mTankFilter_CountryUSSR.Checked) { nation += "USSR,"; nationId += "0,"; manualFilterCount++; }
				if (mTankFilter_CountryJapan.Checked) { nation += "Japan,"; nationId += "6,"; manualFilterCount++; }

				if (mTankFilter_TypeLT.Checked) { type += "Light,"; typeId += "1,"; manualFilterCount++; }
				if (mTankFilter_TypeMT.Checked) { type += "Medium,"; typeId += "2,"; manualFilterCount++; }
				if (mTankFilter_TypeHT.Checked) { type += "Heavy,"; typeId += "3,"; manualFilterCount++; }
				if (mTankFilter_TypeTD.Checked) { type += "TD,"; typeId += "4,"; manualFilterCount++; }
				if (mTankFilter_TypeSPG.Checked) { type += "SPG,"; typeId += "5,"; manualFilterCount++; }
				// Compose status message
				if (tier.Length > 0)
				{
					string tierId = tier;
					tier = tier.Substring(0, tier.Length - 1);
					if (newWhereSQL != "") newWhereSQL += " AND ";
					newWhereSQL += " tank.tier IN (" + tierId.Substring(0, tierId.Length - 1) + ") ";
				}
				if (nation.Length > 0)
				{
					nation = nation.Substring(0, nation.Length - 1);
					if (newWhereSQL != "") newWhereSQL += " AND ";
					newWhereSQL += " tank.countryId IN (" + nationId.Substring(0, nationId.Length - 1) + ") ";
				}
				if (type.Length > 0)
				{
					type = type.Substring(0, type.Length - 1);
					if (newWhereSQL != "") newWhereSQL += " AND ";
					newWhereSQL += " tank.tankTypeId IN (" + typeId.Substring(0, typeId.Length - 1) + ") ";
				}
				if (newWhereSQL != "") newWhereSQL = " AND (" + newWhereSQL + ") ";
				// Check if manual filter is selected, show in statusbar and as menu name
				if (manualFilterCount > 0)
				{
					// Add correct main menu name
					if (manualFilterCount == 1)
						if (tier.Length > 0)
							tankFilterManualFilter = "Tier " + tier;
						else
							tankFilterManualFilter = tier + nation + type;
					else
						tankFilterManualFilter = "Filtered";
					// Statusbar text
					if (tier.Length > 0)
						message += " - Tier: " + tier;
					if (type.Length > 0)
						message += " - Type: " + type;
					if (nation.Length > 0)
						message += " - Nation: " + nation;
				}
				else
				{
					tankFilterManualFilter = "";
				}
			}
			// Show filtername in menu
			SetTankFilterMenuName();
			whereSQL = tankOwnedWhereSQL + newWhereSQL;
			joinSQL = newJoinSQL;
			Status2Message = message;
		}

		#endregion
		
		#region Data Grid - TANK VIEW                                      ***********************************************************************

		private void GridShowTank(string Status2Message)
		{
			// Grid init placement
			int gridAreaTop = panelInfo.Height; // Start below info panel
			dataGridMain.Top = gridAreaTop;
			dataGridMain.Left = 0;
			// Init
			mainGridSaveColWidth = false; // Do not save changing of colWidth when loading grid
			mainGridFormatting = false;
			if (!DB.CheckConnection(false)) return;
			// Get Columns
			string select = "";
			List<ColListHelper.ColListClass> colList = new List<ColListHelper.ColListClass>();
			int img;
			int smallimg;
			int contourimg;
			int masterybadgeimg;
			ColListHelper.GetSelectedColumnList(out select, out colList, out img, out smallimg, out contourimg, out masterybadgeimg);
			// Get Tank filter
			string message = "";
			string tankFilter = "";
			string join = "";
			Tankfilter(out tankFilter, out join, out message);
			// Create Battle mode filter
			string battleModeFilter = "";
			switch (MainSettings.GridFilterTank.BattleMode)
			{
				case GridFilter.BattleModeType.RandomAndTankCompany:
					battleModeFilter = " AND (playerTankBattle.battleMode = '15') ";
					break;
				case GridFilter.BattleModeType.Team:
					battleModeFilter = " AND (playerTankBattle.battleMode = '7') ";
					break;
				case GridFilter.BattleModeType.Random:
					battleModeFilter = " AND (playerTankBattle.battleMode = '15' AND playerTank.hasClan = 0 AND playerTank.hasCompany = 0) ";
					break;
				case GridFilter.BattleModeType.ClanWar:
					battleModeFilter = " AND (playerTank.hasClan = 1) ";
					break;
				case GridFilter.BattleModeType.TankCompany:
					battleModeFilter = " AND (playerTank.hasCompany = 1) ";
					break;
				case GridFilter.BattleModeType.Historical:
					battleModeFilter = " AND (playerTankBattle.battleMode = 'Historical') ";
					break;
				case GridFilter.BattleModeType.Skirmishes:
					battleModeFilter = " AND (playerTankBattle.battleMode = 'Skirmishes') ";
					break;
				default:
					break;
			}
			// Get soring
			GridSortingHelper.Sorting sorting = GridSortingHelper.GetSorting(MainSettings.GetCurrentGridFilter());
			// Default values for painting glyph as sort order direction on column header
			if (sorting.lastSortColumn == "")
			{
				sorting.lastSortColumn = "playerTank.lastBattleTime";
				sorting.lastColumn = "";
			}
			// Create sort order
			string sortOrder = sorting.lastSortColumn + " ";
			currentSortColName = sorting.lastColumn;
			if (sorting.lastSortDirectionAsc)
			{
				sortOrder += " ASC ";
				currentSortDirection = SortOrder.Ascending;
			}
			else
			{
				sortOrder += " DESC ";
				currentSortDirection = SortOrder.Descending;
			}

			// Create the SQL
			string sql = "";
			if (MainSettings.GridFilterTank.BattleMode == GridFilter.BattleModeType.All)
			{
				// Use playerTankBattleTotalsView in stead of playerTankBattle to show totals
				select = select.Replace("playerTankBattle", "playerTankBattleTotalsView");
				sortOrder = sortOrder.Replace("playerTankBattle", "playerTankBattleTotalsView");
				// Get SUM for playerTankBattle as several battleModes might appear
				sql =
					"SELECT   " + select + " playerTank.Id as player_Tank_Id, tank.id as tank_id, tank.name as tank_name, playerTank.markOfMastery as mb_id " + Environment.NewLine +
					"FROM     tank LEFT JOIN " + Environment.NewLine +
					"         playerTank ON tank.id = playerTank.tankId AND playerTank.playerId=@playerid INNER JOIN " + Environment.NewLine +
					"         tankType ON tank.tankTypeId = tankType.id INNER JOIN " + Environment.NewLine +
					"         country ON tank.countryId = country.id LEFT OUTER JOIN " + Environment.NewLine +
					"         playerTankBattleTotalsView ON playerTankBattleTotalsView.playerTankId = playerTank.id LEFT OUTER JOIN " + Environment.NewLine +
					"         modTurret ON playerTank.modTurretId = modTurret.id LEFT OUTER JOIN " + Environment.NewLine +
					"         modRadio ON modRadio.id = playerTank.modRadioId LEFT OUTER JOIN " + Environment.NewLine +
					"         modGun ON playerTank.modGunId = modGun.id " + join + Environment.NewLine +
					"WHERE    tank.id > 0 " + tankFilter + " " + Environment.NewLine +
					"ORDER BY " + sortOrder;
			}
			else
			{
				// Only gets one row from playerTankBattle for an explisit battleMode
				sql =
					"SELECT   " + select + " playerTank.Id as player_Tank_Id, tank.id as tank_id, tank.name as tank_name, playerTank.markOfMastery as mb_id " + Environment.NewLine +
					"FROM     tank LEFT JOIN " + Environment.NewLine +
					"         playerTank ON tank.id = playerTank.tankId AND playerTank.playerId=@playerid INNER JOIN " + Environment.NewLine +
					"         tankType ON tank.tankTypeId = tankType.id INNER JOIN " + Environment.NewLine +
					"         country ON tank.countryId = country.id LEFT OUTER JOIN " + Environment.NewLine +
					"         playerTankBattle ON playerTankBattle.playerTankId = playerTank.id LEFT OUTER JOIN " + Environment.NewLine +
					"         modTurret ON playerTank.modTurretId = modTurret.id LEFT OUTER JOIN " + Environment.NewLine +
					"         modRadio ON modRadio.id = playerTank.modRadioId LEFT OUTER JOIN " + Environment.NewLine +
					"         modGun ON playerTank.modGunId = modGun.id " + join + Environment.NewLine +
					"WHERE    tank.id > 0 " + tankFilter + battleModeFilter + " " + Environment.NewLine +
					"ORDER BY " + sortOrder;
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
			// If tank images add cols in datatable containing the image
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
			// If Mastery badge image add it
			if (masterybadgeimg > -1)
			{
				// Use ImageHelper to add columns in use
				List<ImageHelper.ImgColumns> imgPosition = new List<ImageHelper.ImgColumns>();
				imgPosition.Add(new ImageHelper.ImgColumns("Mastery Badge", masterybadgeimg));
				// Sort images to get right position
				imgPosition.Sort();
				// Add column
				dtTankData.Columns.Add(imgPosition[0].colName, typeof(Image)).SetOrdinal(imgPosition[0].colPosition);
				// Fill with images
				// Fill with images
				foreach (DataRow dr in dtTankData.Rows)
				{
					int mb_id = DbConvert.ToInt32(dr["mb_id"]);
					dr["Mastery Badge"] = ImageHelper.GetMasteryBadgeImage(mb_id);
				}
			}
			// Assign datatable to grid
			dataGridMain.Columns.Clear();
			mainGridFormatting = true;
			dataGridMain.DataSource = dtTankData;
			frozenRows = 0;
			// Unfocus
			dataGridMain.ClearSelection();
			//  Hide system cols
			dataGridMain.Columns["player_Tank_Id"].Visible = false;
			dataGridMain.Columns["tank_Id"].Visible = false;
			dataGridMain.Columns["tank_name"].Visible = false;
			dataGridMain.Columns["mb_id"].Visible = false;
			// Grid col size and formatting
			int colNum = 0;
			foreach (ColListHelper.ColListClass colListItem in colList)
			{
				// Minimum widht and separator back color
				if (colListItem.name.Length > 13 && colListItem.name.Substring(0, 13) == " - Separator ")
				{
					dataGridMain.Columns[colListItem.name].DefaultCellStyle.BackColor = ColorTheme.GridColumnSeparator;
					dataGridMain.Columns[colListItem.name].Resizable = DataGridViewTriState.False;
					dataGridMain.Columns[colListItem.name].MinimumWidth = 2;
					dataGridMain.Columns[colListItem.name].HeaderText = "";
				}
				else
					dataGridMain.Columns[colListItem.name].MinimumWidth = 25;
				// Sortmode - width
				dataGridMain.Columns[colListItem.name].SortMode = DataGridViewColumnSortMode.Programmatic;
				dataGridMain.Columns[colListItem.name].Width = colListItem.colWidth;
				// Format cells
				if (colListItem.colType == "Int")
				{
					dataGridMain.Columns[colListItem.name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
					dataGridMain.Columns[colListItem.name].DefaultCellStyle.Format = "N0";
				}
				else if (colListItem.colType == "Float")
				{
					dataGridMain.Columns[colListItem.name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
					dataGridMain.Columns[colListItem.name].DefaultCellStyle.Format = "N1";
				}
				else if (colListItem.colType == "Image" && colListItem.name == "Tank Image")
				{
					dataGridMain.Columns[colListItem.name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
				}
				else if (colListItem.colType == "Image" && colListItem.name == "Tank Image Large")
				{
					dataGridMain.Columns[colListItem.name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				}
				colNum++;
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
				// Grid init placement
				int gridAreaTop = panelInfo.Height; // Start below info panel
				dataGridMain.Top = gridAreaTop;
				dataGridMain.Left = 0;
				// Init
				mainGridSaveColWidth = false; // Do not save changing of colWidth when loading grid
				mainGridFormatting = false;
				dataGridMain.DataSource = null;
				int rowTotalsIndex = 0;
				int rowAverageIndex = 0;
				if (!DB.CheckConnection(false)) return;
				// Find if grouping 
				bool groupingActive = (!mBattleGroup_No.Checked);
				bool groupingSum = (mBattleGroup_TankSum.Checked);
				// Get Columns
				string select = "";
				List<ColListHelper.ColListClass> colList = new List<ColListHelper.ColListClass>();
				int img;
				int smallimg;
				int contourimg;
				int masterybadgeimg;
				ColListHelper.GetSelectedColumnList(out select, out colList, out img, out smallimg, out contourimg, out masterybadgeimg, groupingActive, groupingSum);
				// Get soring
				GridSortingHelper.Sorting sorting = GridSortingHelper.GetSorting(MainSettings.GetCurrentGridFilter());
				// Default values for painting glyph as sort order direction on column header
				if (sorting.lastColumn == "")
				{
					sorting.lastColumn = "battle.battleTime";
					sorting.lastSortColumn = sorting.lastColumn;
				}
				// Create sort order if no grouping 
				string sortDirection = "";
				currentSortColName = sorting.lastColumn;
				if (sorting.lastSortDirectionAsc)
				{
					sortDirection += " ASC ";
					currentSortDirection = SortOrder.Ascending;
				}
				else
				{
					sortDirection += " DESC ";
					currentSortDirection = SortOrder.Descending;
				}
				// Create Grouping or normal sql parts
				string selectFixed = "";
				string groupBy = "";
				string sortOrder = "";

				if (groupingActive)
				{
					// grouping
					selectFixed =
						"  '#30A8FF' as battleResultColor,  '#30A8FF' as battleSurviveColor, " +
						"  NULL as battleTimeToolTip, SUM(battle.battlesCount) as battlesCountToolTip, " +
						"  SUM(battle.victory) as victoryToolTip, SUM(battle.draw) as drawToolTip, SUM(battle.defeat) as defeatToolTip, " +
						"  SUM(battle.survived) as survivedCountToolTip, SUM(battle.killed) as killedCountToolTip, tank.id as tank_id, tank.name as tank_name, 0 as arenaUniqueID," +
						"  0 as footer, playerTank.Id as player_Tank_Id, 0 as mb_id, 0 as battle_Id ";
					groupBy = "GROUP BY tank.id, tank.Name, playerTank.Id ";
					// Get sorting be finding calcultated fiels
					foreach (ColListHelper.ColListClass col in colList)
					{
						if (col.colName == sorting.lastSortColumn)
						{
							if (col.colNameSelect != "''" && col.colNameSelect != "NULL")
								sortOrder = "ORDER BY " + col.colNameSelect + " " + sortDirection + " ";
						}
					}
				}
				else
				{
					// no grouping
					selectFixed =
						"  battleResult.color as battleResultColor,  battleSurvive.color as battleSurviveColor, " +
						"  CAST(battle.battleTime AS DATETIME) as battleTimeToolTip, battle.battlesCount as battlesCountToolTip, " +
						"  battle.victory as victoryToolTip, battle.draw as drawToolTip, battle.defeat as defeatToolTip, " +
						"  battle.survived as survivedCountToolTip, battle.killed as killedCountToolTip, tank.id as tank_id, tank.name as tank_name, " +
						"  battle.markOfMastery as mb_id, battle.arenaUniqueID," +
						"  0 as footer, playerTank.Id as player_Tank_Id, battle.id as battle_Id ";
					sortOrder = "ORDER BY " + sorting.lastSortColumn + " " + sortDirection + " ";
				}
				
				// Create Battle Time filer
				string battleTimeFilter = "";
				if (!mBattlesAll.Checked)
				{
					DateTime dateFilter = new DateTime();
					if (!mBattlesCustomUse.Checked)
					{
						// Normal predefined battle time filters
						battleTimeFilter = " AND battleTime>=@battleTime ";
						DateTime basedate = DateTime.Now; // current time
						if (DateTime.Now.Hour < 7) basedate = DateTime.Now.AddDays(-1); // correct date according to server reset 07:00 AM
						dateFilter = new DateTime(basedate.Year, basedate.Month, basedate.Day, 7, 0, 0); // datefilter = today
						// Adjust time scale according to selected filter
						if (mBattles3d.Checked) dateFilter = dateFilter.AddDays(-3);
						else if (mBattles2d.Checked) dateFilter = dateFilter.AddDays(-2);
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
							battleTimeFilter = " AND battleTime>=@battleTime AND battleTime<=@battleFromTime ";
							DB.AddWithValue(ref battleTimeFilter, "@battleFromTime", dateFromYesterdayFilter.ToString("yyyy-MM-dd HH:mm"), DB.SqlDataType.DateTime);
						}
						DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter.ToString("yyyy-MM-dd HH:mm"), DB.SqlDataType.DateTime);
					}
					else
					{
						// Custom battle time filter
						if (Config.Settings.customBattleTimeFilter.from != null)
						{
							battleTimeFilter = " AND battleTime>=@battleTime ";
							dateFilter = Convert.ToDateTime(Config.Settings.customBattleTimeFilter.from);
							DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter.ToString("yyyy-MM-dd HH:mm"), DB.SqlDataType.DateTime);
						}
						if (Config.Settings.customBattleTimeFilter.to != null)
						{
							battleTimeFilter += " AND battleTime<=@battleTime ";
							dateFilter = Convert.ToDateTime(Config.Settings.customBattleTimeFilter.to);
							DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter.ToString("yyyy-MM-dd HH:mm"), DB.SqlDataType.DateTime);
						}
					}
				}

				// Create Battle mode filter
				string battleModeFilter = "";
				string battleMode = "%";
				if (MainSettings.GridFilterBattle.BattleMode != GridFilter.BattleModeType.All)
				{
					switch (MainSettings.GridFilterBattle.BattleMode)
					{
						case GridFilter.BattleModeType.RandomAndTankCompany:
							battleModeFilter = " AND (battleMode = '15') ";
							battleMode = "15";
							break;
						case GridFilter.BattleModeType.Team:
							battleModeFilter = " AND (battleMode = '7') ";
							battleMode = "7";
							break;
						case GridFilter.BattleModeType.Random:
							battleModeFilter = " AND (battleMode = '15' AND modeClan = 0 AND modeCompany = 0) ";
							battleMode = "15";
							break;
						case GridFilter.BattleModeType.ClanWar:
							battleModeFilter = " AND (battleMode = '15' AND modeClan > 0) ";
							battleMode = "15";
							break;
						case GridFilter.BattleModeType.TankCompany:
							battleModeFilter = " AND (battleMode = '15' AND modeCompany > 0) ";
							battleMode = "15";
							break;
						case GridFilter.BattleModeType.Historical:
							battleModeFilter = " AND (battleMode = 'Historical') ";
							battleMode = "Historical";
							break;
						case GridFilter.BattleModeType.Skirmishes:
							battleModeFilter = " AND (battleMode = 'Skirmishes') ";
							battleMode = "Skirmishes";
							break;
						case GridFilter.BattleModeType.RandomPlatoon:
							battleModeFilter = " AND (battleMode = '15' AND platoonParticipants > 0) ";
							battleMode = "15";
							break;
						case GridFilter.BattleModeType.RandomPlatoon2:
							battleModeFilter = " AND (battleMode = '15' AND platoonParticipants = 2) ";
							battleMode = "15";
							break;
						case GridFilter.BattleModeType.RandomPlatoon3:
							battleModeFilter = " AND (battleMode = '15' AND platoonParticipants = 3) ";
							battleMode = "15";
							break;
						case GridFilter.BattleModeType.RandomSolo:
							battleModeFilter = " AND (battleMode = '15' AND platoonParticipants = 0) ";
							battleMode = "15";
							break;
						
					}
				}
											
				
				// Get Tank filter
				string tankFilterMessage = "";
				string tankFilter = "";
				string tankJoin = "";
				Tankfilter(out tankFilter, out tankJoin, out tankFilterMessage);
				
				// Create SQL
				string sql =
					"SELECT " + select + " " + selectFixed + " " +
					"FROM    battle INNER JOIN " +
					"        playerTank ON battle.playerTankId = playerTank.id INNER JOIN " +
					"        tank ON playerTank.tankId = tank.id INNER JOIN " +
					"        tankType ON tank.tankTypeId = tankType.Id INNER JOIN " +
					"        country ON tank.countryId = country.Id INNER JOIN " +
					"        battleResult ON battle.battleResultId = battleResult.id LEFT JOIN " +
					"        map on battle.mapId = map.id INNER JOIN " +
					"        battleSurvive ON battle.battleSurviveId = battleSurvive.id " + tankJoin +
					"WHERE   playerTank.playerId=@playerid " + battleTimeFilter + battleModeFilter + tankFilter +
					groupBy + " " +
					sortOrder;
				DB.AddWithValue(ref sql, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
				
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
				// If Mastery badge image add it
				if (masterybadgeimg > -1)
				{
					// Use ImageHelper to add columns in use
					List<ImageHelper.ImgColumns> imgPosition = new List<ImageHelper.ImgColumns>();
					imgPosition.Add(new ImageHelper.ImgColumns("Mastery Badge", masterybadgeimg));
					// Sort images to get right position
					imgPosition.Sort();
					// Add column
					dt.Columns.Add(imgPosition[0].colName, typeof(Image)).SetOrdinal(imgPosition[0].colPosition);
					// Fill with images
					// Fill with images
					foreach (DataRow dr in dt.Rows)
					{
						int mb_id = 0;
						if (dr["mb_id"] != DBNull.Value)
							mb_id = Convert.ToInt32(dr["mb_id"]);
						dr["Mastery Badge"] = ImageHelper.GetMasteryBadgeImage(mb_id);
					}
				}
				int rowcount = dt.Rows.Count;
				// Add footer
				int totalBattleCount = 0;
				double totalWinRate = 0;
				double totalSurvivedRate = 0;
				// Add footer now, if any rows an no grouping ************************ AVG **********************
				if (rowcount > 0 && !groupingActive)
				{
					// Create blank image in case of image in footer
					Image blankImage = new Bitmap(1, 1);
					// totals
					totalBattleCount = Convert.ToInt32(dt.Compute("Sum(battlesCountToolTip)", ""));
					totalWinRate = Convert.ToDouble(dt.Compute("Sum(victoryToolTip)", "")) * 100 / totalBattleCount;
					totalSurvivedRate = Convert.ToDouble(dt.Compute("Sum(survivedCountToolTip)", "")) * 100 / totalBattleCount;
					// the footer row #1 - average
					DataRow rowAverage = dt.NewRow();
					rowAverage["footer"] = 2;
					rowAverage["battleResultColor"] = "";
					rowAverage["battleSurviveColor"] = "";
					rowAverage["battleTimeToolTip"] = DBNull.Value;
					rowAverage["battlesCountToolTip"] = 0;
					rowAverage["victoryToolTip"] = 0;
					rowAverage["drawToolTip"] = 0;
					rowAverage["defeatToolTip"] = 0;
					rowAverage["survivedCountToolTip"] = 0;
					rowAverage["killedCountToolTip"] = 0;
					IEnumerable<string> nonAvgCols = new List<string> 
					{ 
						"ID", "Premium", "Mastery Badge", "Mastery Badge ID", "Battle Count" , "Platoon", "Killed By Player ID", "Enemy Clan ID"
					};
					foreach (ColListHelper.ColListClass colListItem in colList)
					{
						if (colListItem.colType == "Int" || colListItem.colType == "Float")
						{
							
							if (!nonAvgCols.Contains(colListItem.name))
							{
								int count = 0;
								double sum = 0;
								foreach (DataRow dr in dt.Rows)
								{
									if (dr[colListItem.name] != DBNull.Value)
									{
										count += Convert.ToInt32(dr["battlesCountToolTip"]);
										sum += Convert.ToInt32(dr[colListItem.name]) * Convert.ToInt32(dr["battlesCountToolTip"]);
									}
								}
								if (count > 0)
									if (count > 1 && colListItem.name == "WN8") // Special calculation for WN8
										rowAverage[colListItem.name] = Rating.CalcAvgBattleWN8(battleTimeFilter, 0, battleMode, tankFilter, battleModeFilter);
									else if (count > 1 && colListItem.name == "WN7") // Special calculation for WN7
										rowAverage[colListItem.name] = Rating.CalcBattleWN7(battleTimeFilter, 0, battleMode, tankFilter, battleModeFilter);
									else if (count > 1 && colListItem.name == "EFF") // Special calculation for EFF
										rowAverage[colListItem.name] = Rating.CalcBattleEFF(battleTimeFilter, 0, battleMode, tankFilter, battleModeFilter);
									else
										rowAverage[colListItem.name] = sum / count;
								else
									rowAverage[colListItem.name] = 0;
							}
							else
							{
								rowAverage[colListItem.name] = DBNull.Value;
							}
						}
						else if (colListItem.colType == "DateTime")
						{
							rowAverage[colListItem.name] = DBNull.Value;
						}
						else if (colListItem.colType == "Image")
						{
							rowAverage[colListItem.name] = blankImage;
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
							rowAverage[colListItem.name] = s;
						}
					}
					// the footer row #2 ************************** TOTALS **************************
					DataRow rowTotals = dt.NewRow();
					rowTotals["footer"] = 1;
					rowTotals["battleResultColor"] = "";
					rowTotals["battleSurviveColor"] = "";
					rowTotals["battleTimeToolTip"] = DBNull.Value;
					rowTotals["battlesCountToolTip"] = 0;
					rowTotals["victoryToolTip"] = 0;
					rowTotals["drawToolTip"] = 0;
					rowTotals["defeatToolTip"] = 0;
					rowTotals["survivedCountToolTip"] = 0;
					rowTotals["killedCountToolTip"] = 0;
					IEnumerable<string> nonTotalsCols = new List<string> 
					{ 
						"Tier", "Premium", "ID", "Mastery Badge ID", "EFF", "WN7", "WN8", "Hit Rate",  
						"Pierced Shots%", "Pierced Hits%", "HE Shots %", "HE Hts %", "Platoon", "Killed By Player ID", "Enemy Clan ID"
					};
					IEnumerable<string> countCols = new List<string> 
					{ 
						"Killed Count", "Victory" ,"Draw","Defeat","Survival Count","Clan","Company","Battle Count"
					};

					foreach (ColListHelper.ColListClass colListItem in colList)
					{
						// Format column
						if (colListItem.colType == "Int" || colListItem.colType == "Float")
						{
							
							if (!nonTotalsCols.Contains(colListItem.name)) // Avoid calculate total EFF/WN8
							{
								// looping through datatable for every row per column and multiply with battlesCountToolTip to get correct sum when several battles recorded on one row
								int sum = 0;
								if (!countCols.Contains(colListItem.name))
								{
									foreach (DataRow dr in dt.Rows)
									{

										if (dr[colListItem.name] != DBNull.Value)
										{
											sum += Convert.ToInt32(dr[colListItem.name]) * Convert.ToInt32(dr["battlesCountToolTip"]);
										}
									}
								}
								else
								{
									foreach (DataRow dr in dt.Rows)
									{
										if (dr[colListItem.name] != DBNull.Value)
										{
											sum += Convert.ToInt32(dr[colListItem.name]);
										}
									}
								}
								rowTotals[colListItem.name] = sum;
							}
							else
								rowTotals[colListItem.name] = DBNull.Value;
						}
						else if (colListItem.colType == "DateTime")
						{
							rowTotals[colListItem.name] = DBNull.Value;
						}
						else if (colListItem.colType == "Image")
						{
							rowTotals[colListItem.name] = blankImage;
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
							rowTotals[colListItem.name] = s;
						}
					}
					// Add rows
					if (Config.Settings.gridBattlesTotalsTop)
					{
						// as header
						dt.Rows.InsertAt(rowTotals, 0);
						dt.Rows.InsertAt(rowAverage, 0);
						rowTotalsIndex = 1;
						rowAverageIndex = 0;
					}
					else
					{
						// as footer
						dt.Rows.Add(rowTotals);
						rowTotalsIndex = dt.Rows.Count -1;
						dt.Rows.Add(rowAverage);
						rowAverageIndex = dt.Rows.Count -1;
					}
				}
				// Set row height in template before rendering to fit images
				dataGridMain.RowTemplate.Height = 23;
				if (smallimg >= 0)
					dataGridMain.RowTemplate.Height = 31;
				if (img >= 0)
					dataGridMain.RowTemplate.Height = 60;
				// populate datagrid 
				dataGridMain.Columns.Clear();
				mainGridFormatting = true;
				dataGridMain.DataSource = dt;
				frozenRows = 0;
				// If totals/average on top make frozen
				if (rowcount > 0 && !groupingActive && Config.Settings.gridBattlesTotalsTop)
				{
					// As frozen top rows
					dataGridMain.Rows[rowTotalsIndex].Frozen = true;
					dataGridMain.Rows[rowAverageIndex].Frozen = true;
					frozenRows = 2;
				}
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
				dataGridMain.Columns["player_Tank_Id"].Visible = false;
				dataGridMain.Columns["battle_Id"].Visible = false;
				dataGridMain.Columns["tank_Id"].Visible = false;
				dataGridMain.Columns["tank_name"].Visible = false;
				dataGridMain.Columns["arenaUniqueID"].Visible = false;
				dataGridMain.Columns["mb_id"].Visible = false;
				// Format grid 
				foreach (ColListHelper.ColListClass colListItem in colList)
				{
					// MInimum width and separator back color
					if (colListItem.name.Length > 13 && colListItem.name.Substring(0, 13) == " - Separator ")
					{
						dataGridMain.Columns[colListItem.name].DefaultCellStyle.BackColor = ColorTheme.GridColumnSeparator;
						dataGridMain.Columns[colListItem.name].Resizable = DataGridViewTriState.False;
						dataGridMain.Columns[colListItem.name].MinimumWidth = 2;
						dataGridMain.Columns[colListItem.name].HeaderText = "";
						// avg and totals darker separator colors
						if (rowcount > 0 && !groupingActive)
						{
							dataGridMain.Rows[rowAverageIndex].Cells[colListItem.name].Style.BackColor = ColorTheme.GridColumnHeaderSeparator;
							dataGridMain.Rows[rowTotalsIndex].Cells[colListItem.name].Style.BackColor = ColorTheme.GridColumnHeaderSeparator;
						}
					}
					else
						dataGridMain.Columns[colListItem.name].MinimumWidth = 25;
					// Width and sorting
					dataGridMain.Columns[colListItem.name].Width = colListItem.colWidth;
					dataGridMain.Columns[colListItem.name].SortMode = DataGridViewColumnSortMode.Programmatic;
					// Format cells
					if (colListItem.colType == "Int")
					{
						dataGridMain.Columns[colListItem.name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
						dataGridMain.Columns[colListItem.name].DefaultCellStyle.Format = "N0";
					}
					else if (colListItem.colType == "Float")
					{
						dataGridMain.Columns[colListItem.name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
						IEnumerable<string> showFloatValues = new List<string> 
						{ 
							"Exp Dmg","Exp Win Rate","Exp Spot","Exp Frags","Exp Def"
						};
						if (!showFloatValues.Contains(colListItem.name)) // Avoid calculate total EFF/WN8
							dataGridMain.Columns[colListItem.name].DefaultCellStyle.Format = "N0";
						else
							dataGridMain.Columns[colListItem.name].DefaultCellStyle.Format = "N1";
						if (rowcount > 0 && !groupingActive) // Special format in average row for floating values
							dataGridMain.Rows[rowAverageIndex].Cells[colListItem.name].Style.Format = "N1";
					}
					else if (colListItem.colType == "Image" && colListItem.name == "Tank Image")
					{
						dataGridMain.Columns[colListItem.name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
					}
					else if (colListItem.colType == "Image" && colListItem.name == "Tank Image Large")
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
									dataGridMain.Rows[rowAverageIndex].Cells["Tank"].ToolTipText = "Average based on " + totalBattleCount.ToString() + " battles";
									dataGridMain.Rows[rowTotalsIndex].Cells["Tank"].ToolTipText = "Totals based on " + totalBattleCount.ToString() + " battles";
									break;
							}
						}
					}
				}
				// Finish up
				ResizeNow();
				mainGridSaveColWidth = true;
				mBattles.Visible = true;
				SetStatus2(Status2Message);
				lblStatusRowCount.Text = "Rows " + rowcount.ToString();
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				//throw;
			}
			
		}

		#endregion

		#region Grid sorting and paint events

		private string currentSortColName = "";
		private SortOrder currentSortDirection = SortOrder.Descending;

		private void dataGridMain_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			// Add battle count number on battle view
			if (MainSettings.View == GridView.Views.Battle && e.ColumnIndex == -1)
			{
				// Frozen rows at top
				int offset = 0;
				if (!mBattleGroup_No.Checked && e.RowIndex > 0)
					offset = 1;
				else if (Config.Settings.gridBattlesTotalsTop && e.RowIndex > 1)
					offset = -1;
				else if (!Config.Settings.gridBattlesTotalsTop && e.RowIndex < dataGridMain.Rows.Count - 2 && e.RowIndex > -1)
					offset = 1;
				if (offset != 0)
				{
					e.PaintBackground(e.CellBounds, true);
					Color battleCountColor = ColorTheme.ControlDarkRed;
					if (dataGridMain.Rows[e.RowIndex].Cells["arenaUniqueID"].Value != DBNull.Value)
					{
						if (dataGridMain.Rows[e.RowIndex].Cells["arenaUniqueID"].Value.ToString() != "0")
							battleCountColor = ColorTheme.FormBorderBlue;
						else
							battleCountColor = ColorTheme.ControlDimmedFont;
					}
					using (SolidBrush br = new SolidBrush(battleCountColor))
					{
						StringFormat sf = new StringFormat();
						sf.Alignment = StringAlignment.Center;
						sf.LineAlignment = StringAlignment.Center;
						e.Graphics.DrawString((e.RowIndex + offset).ToString(), e.CellStyle.Font, br, e.CellBounds, sf);
					}
					e.Handled = true;
				}
			}
			// Add glyph to column headers
			else if (MainSettings.View != GridView.Views.Overall && e.RowIndex < 0 && e.ColumnIndex > -1 && dataGridMain.Columns[e.ColumnIndex].HeaderText == currentSortColName) 
			{
				dataGridMain.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = currentSortDirection;
			}
			// Remove arrow on row headers
			else if (e.ColumnIndex == -1 && e.RowIndex > -1) 
			{
				e.PaintBackground(e.CellBounds, true);
				using (SolidBrush br = new SolidBrush(ColorTheme.ControlDimmedFont))
				{
					StringFormat sf = new StringFormat();
					sf.Alignment = StringAlignment.Center;
					sf.LineAlignment = StringAlignment.Center;
					e.Graphics.DrawString("", e.CellStyle.Font, br, e.CellBounds, sf);
				}
				e.Handled = true;
			}
		}

		private void dataGridMain_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				// Avoid headerRow and Image columns
				if (e.ColumnIndex < 0)
					return;
				// Manual Sort for battle and tanks view
				if (MainSettings.View != GridView.Views.Overall)
				{
					// Find colName

					ColListHelper.ColListClass clc = ColListHelper.GetColListItem(dataGridMain.Columns[e.ColumnIndex].Name, MainSettings.View);
					// Find current sort
					GridSortingHelper.Sorting sorting = GridSortingHelper.GetSorting(MainSettings.GetCurrentGridFilter());
					// Check if this is first sorting
					if (sorting == null)
						sorting = new GridSortingHelper.Sorting();
					// Check if same same column as last
					if (clc.name == sorting.lastColumn)
					{
						// same as last, reverse sort direction
						sorting.lastSortDirectionAsc = !sorting.lastSortDirectionAsc;
					}
					else
					{
						// new column, get default sort direction
						sorting.lastColumn = clc.name; // column name in header
						sorting.lastSortColumn = clc.colName; // database field to sort on
						bool sortDirectionAsc = false;
						if (dataGridMain.Columns[e.ColumnIndex].ValueType == typeof(string))
							sortDirectionAsc = true;
						sorting.lastSortDirectionAsc = sortDirectionAsc;
					}
					// Save new sorting
					GridSortingHelper.SaveSorting(MainSettings.GetCurrentGridFilter().ColListId, sorting);
					// Show grid
					ShowView("Datagrid refreshed after sorting");
				}
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				if (Config.Settings.showDBErrors)
					MsgBox.Show("Error on grid header mouse click event, see log file", "Grid error", this);

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
						cell.Style.SelectionForeColor = cell.Style.ForeColor;
					}
				}
				else if (col.Equals("WN8"))
				{
					if (dataGridMain["WN8", e.RowIndex].Value != DBNull.Value)
					{
						int wn8 = Convert.ToInt32(dataGridMain["WN8", e.RowIndex].Value);
						cell.Style.ForeColor = Rating.WN8color(wn8);
						cell.Style.SelectionForeColor = cell.Style.ForeColor;
					}
				}
				else if (col.Equals("WN7"))
				{
					if (dataGridMain["WN7", e.RowIndex].Value != DBNull.Value)
					{
						int wn7 = Convert.ToInt32(dataGridMain["WN7", e.RowIndex].Value);
						cell.Style.ForeColor = Rating.WN7color(wn7);
						cell.Style.SelectionForeColor = cell.Style.ForeColor;
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
							cell.Style.SelectionForeColor = cell.Style.ForeColor;
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
							cell.Style.SelectionForeColor = cell.Style.ForeColor;
						}
					}
				}
				else if (MainSettings.View == GridView.Views.Battle)
				{
					bool footer = (Convert.ToInt32(dataGridMain["footer", e.RowIndex].Value) > 0);
					if (footer)
						dataGridMain.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.GridTotalsRow;
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
						cell.Style.SelectionForeColor = cell.Style.ForeColor;
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
						cell.Style.SelectionForeColor = cell.Style.ForeColor;
						int battlesCount = Convert.ToInt32(dataGridMain["battlesCountToolTip", e.RowIndex].Value);
						if (battlesCount > 1)
						{
							cell.ToolTipText = "Survived: " + dataGridMain["survivedCountToolTip", e.RowIndex].Value.ToString() + Environment.NewLine +
								"Killed: " + dataGridMain["killedcountToolTip", e.RowIndex].Value.ToString();
						}
					}
				}
				else if (MainSettings.View == GridView.Views.Tank)
				{
					if (col.Equals("Win Rate"))
					{
						if (dataGridMain["Win Rate", e.RowIndex].Value != DBNull.Value)
						{
							double wr = Convert.ToDouble(dataGridMain["Win Rate", e.RowIndex].Value);
							cell.Style.ForeColor = Rating.WinRateColor(wr);
							cell.Style.SelectionForeColor = cell.Style.ForeColor;
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
			try
			{
				if (e.RowIndex != -1 && e.ColumnIndex != -1)
				{
					dataGridRightClickRow = e.RowIndex;
					dataGridRightClickCol = e.ColumnIndex;
					dataGridMain.CurrentCell = dataGridMain.Rows[dataGridRightClickRow].Cells[dataGridRightClickCol];
				}
				// Check if footer
				if (dataGridRightClickRow != -1 && dataGridRightClickCol != -1 && MainSettings.View == GridView.Views.Battle)
				{
					if (Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["footer"].Value) > 0)
					{
						dataGridRightClickCol = -1;
						dataGridRightClickRow = -1;
					}
				}

			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				if (Config.Settings.showDBErrors)
					MsgBox.Show("Error on grid mouse down event, see log file", "Grid error", this);
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
			if (dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value != DBNull.Value)
			{
				int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
				Form frm = new Forms.GrindingSetup(playerTankId);
				frm.ShowDialog();
				if (MainSettings.View == GridView.Views.Tank)
					ShowView("Refreshed grid");
			}
		}

		private void dataGridMainPopup_BattleChart_Click(object sender, EventArgs e)
		{
			if (dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value != DBNull.Value)
			{
				int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
				Form frm = new Forms.BattleChartTier(playerTankId);
				FormHelper.OpenFormToRightOfParent(this, frm);
			}
		}

		private void dataGridMainPopup_BattleDetails_Click(object sender, EventArgs e)
		{
			int battleId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["battle_Id"].Value);
			Form frm = new Forms.BattleDetail(battleId, this);
			FormHelper.OpenFormCenterOfParent(this, frm);
		}

		private void dataGridMainPopup_TankDetails_Click(object sender, EventArgs e)
		{
			int playerTankId = DbConvert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
			int tankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["tank_Id"].Value);
			Form frm = new Forms.PlayerTankDetail(playerTankId, tankId);
			FormHelper.OpenFormToRightOfParent(this, frm);
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
					ShowView("Deleted battle, grid refreshed");
				}
			}
		}
				
		private void dataGridMainPopup_TankWN8_Click(object sender, EventArgs e)
		{
			if (dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value != DBNull.Value)
			{
				int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
				string sql =
					"select t.id as tankId, ptb.battles as battles, ptb.dmg as dmg, ptb.spot as spot, ptb.frags as frags, " +
					"  ptb.def as def, ptb.cap as cap, wins as wins, " +
					"  t.expDmg as expDmg, t.expSpot as expSpot, t.expFrags as expFrags, t.expDef as expDef, t.expWR as expWR " +
					"from playerTankBattle ptb left join " +
					"  playerTank pt on ptb.playerTankId=pt.id and pt.playerId=@playerId and ptb.battleMode='15' left join " +
					"  tank t on pt.tankId = t.id " +
					"where t.expDmg is not null and ptb.battleMode='15' and pt.id=@playerTankId ";
				DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
				DataTable dt = DB.FetchData(sql);
				if (dt.Rows.Count > 0)
				{
					DataRow dr = dt.Rows[0];
					int tankId = Convert.ToInt32(dr["tankId"]);
					double battlesCount = Convert.ToInt32(dr["battles"]);
					double dmg = Convert.ToDouble(dr["dmg"]);
					double spotted = Convert.ToDouble(dr["spot"]);
					double frags = Convert.ToDouble(dr["frags"]);
					double def = Convert.ToDouble(dr["def"]);
					double wins = Convert.ToDouble(dr["Wins"]);
					string wn8 = Math.Round(Rating.CalculateTankWN8(tankId, battlesCount, dmg, spotted, frags, def, wins), 0).ToString();
					double rWINc;
					double rDAMAGEc;
					double rFRAGSc;
					double rSPOTc;
					double rDEFc;
					dmg = dmg / battlesCount;
					spotted = spotted / battlesCount;
					frags = frags / battlesCount;
					def = def / battlesCount;
					double wr = wins / battlesCount * 100;
					double exp_dmg = Convert.ToDouble(dr["expDmg"]);
					double exp_spotted = Convert.ToDouble(dr["expSpot"]);
					double exp_frags = Convert.ToDouble(dr["expFrags"]);
					double exp_def = Convert.ToDouble(dr["expDef"]);
					double exp_wr = Convert.ToDouble(dr["expWR"]);
					Rating.UseWN8FormulaReturnResult(
						dmg, spotted, frags, def, wr,
						exp_dmg, exp_spotted, exp_frags, exp_def, exp_wr,
						out rWINc, out rDAMAGEc, out rFRAGSc, out rSPOTc, out rDEFc);
					string message = "WN8 Rating for this tank in Random/TC: ";
					message += wn8 + Environment.NewLine + Environment.NewLine;
					message += "Value" + "\t  " + "Result" + "\t" + "Expected" + "\t " + "WN8 result" + Environment.NewLine;
					message += "-------------" + "\t  " + "----------" + "\t" + "------------" + "\t " + "----------------" + Environment.NewLine;
					message += "Damage:" + "\t  " + Math.Round(dmg, 1).ToString() + "\t" + Math.Round(exp_dmg, 1).ToString() + "\t " + Math.Round(rDAMAGEc, 2) + Environment.NewLine;
					message += "Frags:" + "\t  " + Math.Round(frags, 1).ToString() + "\t" + Math.Round(exp_frags, 1).ToString() + "\t " + Math.Round(rFRAGSc, 2) + Environment.NewLine;
					message += "Spot:" + "\t  " + Math.Round(spotted, 1).ToString() + "\t" + Math.Round(exp_spotted, 1).ToString() + "\t " + Math.Round(rSPOTc, 2) + Environment.NewLine;
					message += "Defence:" + "\t  " + Math.Round(def, 1).ToString() + "\t" + Math.Round(exp_def, 1).ToString() + "\t " + Math.Round(rDEFc, 2) + Environment.NewLine;
					message += "Win rate:" + "\t  " + Math.Round(wr, 1).ToString() + "%" + "\t" + Math.Round(exp_wr, 1).ToString() + "%" + "\t " + Math.Round(rWINc, 2) + Environment.NewLine;
					message += Environment.NewLine;
					MsgBox.Show(message, "WN8 Tank Details");
				}
			}
		}

		private void dataGridMainPopup_FilterOnTank_Click(object sender, EventArgs e)
		{
			int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
			int tankId = TankHelper.GetTankID(playerTankId);
			if (tankId != 0)
			{
				TankFilterMenuUncheck(true, true, true, false);
				MainSettings.GetCurrentGridFilter().TankId = tankId;
				ShowView("Filtered on tank: " + TankHelper.GetTankName(tankId));
			}
		}

		private void dataGridMainPopup_FavListCreateNew_Click(object sender, EventArgs e)
		{
			if (dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value != DBNull.Value)
			{
				int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
				int tankId = TankHelper.GetTankID(playerTankId);
				Form frm = new Forms.FavListNewEdit(0, "", tankId);
				frm.ShowDialog();
				// After fav list changes reload menu
				SetFavListMenu(); // Reload fav list items
			}
		}

		private void dataGridMainPopup_FavListAddTank_Click(object sender, EventArgs e)
		{
			if (dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value != DBNull.Value)
			{
				int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
				int tankId = TankHelper.GetTankID(playerTankId);
				if (tankId != 0 && FavListHelper.CheckIfAnyFavList(this, tankId, true))
				{
					Form frm = new Forms.FavListAddRemoveTank(this, tankId, true);
					frm.ShowDialog();
				}
			}
		}

		private void dataGridMainPopup_FavListRemoveTank_Click(object sender, EventArgs e)
		{
			if (dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value != DBNull.Value)
			{
				int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
				int tankId = TankHelper.GetTankID(playerTankId);
				if (tankId != 0 && FavListHelper.CheckIfAnyFavList(this, tankId, false))
				{
					Form frm = new Forms.FavListAddRemoveTank(this, tankId, false);
					frm.ShowDialog();
					// refresh if tank removed
					if (FavListHelper.refreshGridAfterAddRemove)
					{
						try
						{
							int pos = dataGridMain.FirstDisplayedScrollingRowIndex;
							dataGridMain.Visible = false;
							ShowView("Refresh after removed tank from favourite tank list");
							dataGridMain.FirstDisplayedScrollingRowIndex = pos;
							MoveScrollBar();
							dataGridMain.Visible = true;
							dataGridMain.Focus();
							FavListHelper.refreshGridAfterAddRemove = false;
						}
						catch (Exception)
						{
							// Do nothing, just optional scrolling and refresh event
						}

					}
				}
			}
		}


		#endregion

		#region Grid Col Widht Changed

		private void dataGridMain_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
		{
			string colName = "";
			try
			{
				if (mainGridSaveColWidth && MainSettings.View != GridView.Views.Overall)
				{
					colName = dataGridMain.Columns[e.Column.HeaderText].HeaderText;
					int newWidth = e.Column.Width;
					dataGridMain.Columns[e.Column.HeaderText].Width = newWidth;
					ColListHelper.SaveColWidth(e.Column.HeaderText, newWidth);
				}
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex, "Error saving column resize: [" + colName +  "]");
				if (Config.Settings.showDBErrors)
					MsgBox.Show("Error occured saving resized column, see log file.", "Error resizing column", this);
			}
			// Move back to scrollbar position
			dataGridMain.FirstDisplayedScrollingColumnIndex = scrollX.ScrollPosition;
		}

		private void dataGridMain_RowHeadersWidthChanged(object sender, EventArgs e)
		{
			int rowHeaderWidth = dataGridMain.RowHeadersWidth;
			switch (MainSettings.View)
			{
				case GridView.Views.Overall:
					break;
				case GridView.Views.Tank:
					Config.Settings.mainGridTankRowWidht = rowHeaderWidth; 
					break;
				case GridView.Views.Battle:
					Config.Settings.mainGridBattleRowWidht = rowHeaderWidth; 
					break;
				default:
					break;
			}
		}

		#endregion

		#region Grid Scroll Handling

		private bool scrollingY = false;
		private bool scrollingX = false;

		private int frozenRows = 0;
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

		private void scrollY_MouseUp(object sender, MouseEventArgs e)
		{
			scrollingY = false;
		}

		private void ScrollY()
		{
			try
			{
				int posBefore = dataGridMain.FirstDisplayedScrollingRowIndex - frozenRows;
				dataGridMain.FirstDisplayedScrollingRowIndex = scrollY.ScrollPosition + frozenRows;
				if (posBefore != dataGridMain.FirstDisplayedScrollingRowIndex) Refresh();
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				// throw;
			}
			
		}

		private void dataGridMain_MouseWheel(object sender, MouseEventArgs e)
		{
			// Enable mouse wheel scrolling for datagrid
			try
			{
				// scroll in grid from mouse wheel
				if (scrollY.ScrollNecessary)
				{
					int currentIndex = this.dataGridMain.FirstDisplayedScrollingRowIndex - frozenRows;
					int scrollLines = SystemInformation.MouseWheelScrollLines;
					if (e.Delta > 0)
					{
						this.dataGridMain.FirstDisplayedScrollingRowIndex = Math.Max(0, currentIndex - scrollLines) + frozenRows;
					}
					else if (e.Delta < 0)
					{
						this.dataGridMain.FirstDisplayedScrollingRowIndex = currentIndex + scrollLines + frozenRows;
					}
				}
				else if (scrollX.ScrollNecessary)
				{
					int currentIndex = this.dataGridMain.FirstDisplayedScrollingColumnIndex;
					int scrollColumns = 1; //SystemInformation.MouseWheelScrollLines;
					if (e.Delta > 0)
					{
						this.dataGridMain.FirstDisplayedScrollingColumnIndex = Math.Max(0, currentIndex - scrollColumns);
					}
					else if (e.Delta < 0)
					{
						this.dataGridMain.FirstDisplayedScrollingColumnIndex = currentIndex + scrollColumns;
					}
				}
				// move scrollbar
				MoveScrollBar();
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				// throw;
			}
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

		private void MoveScrollBar()
		{
			try
			{
				scrollX.ScrollPosition = dataGridMain.FirstDisplayedScrollingColumnIndex;
				scrollY.ScrollPosition = dataGridMain.FirstDisplayedScrollingRowIndex - frozenRows;
			}
			catch (Exception)
			{
				// ignore errors, only affect scrollbar position
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
				try
				{
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
				catch (Exception ex)
				{
					Log.LogToFile(ex);
					// throw;
				}
				
			}
			
		}

		#endregion       
		
		#region App, DB and other Settings + Help/About + Chart

		private void toolItemViewChart_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.BattleChartTier(0);
			FormHelper.OpenFormToRightOfParent(this, frm);
		}
	
		private void toolItemSettingsApp_Click(object sender, EventArgs e)
		{
			// Stop file watchers if running
			int runState = Config.Settings.dossierFileWathcherRun;
			if (runState == 1)
			{
				Config.Settings.dossierFileWathcherRun = 0;
				SetListener();
			}
			
			Form frm = new Forms.ApplicationSetting();
			frm.ShowDialog();

			// Return to prev file watcher state
			if (runState != Config.Settings.dossierFileWathcherRun)
			{
				Config.Settings.dossierFileWathcherRun = runState;
				SetListener();
			}

			// Update main form
			currentPlayerId = Config.Settings.playerId;
			SetFormTitle();
			SetFavListMenu(); // Reload fav list items
			SetColListMenu(); // Refresh column setup list now
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
			int WN8versionCurrent = DBVersion.GetWN8Version();
			Form frm = new Forms.UpdateFromApi(autoRun);
			frm.ShowDialog();
			int WN8versionNew = DBVersion.GetWN8Version();
			if (WN8versionNew > WN8versionCurrent)
				RunRecalcBattleWN8(true);
		}

		private void mRecalcBattleWN8_Click(object sender, EventArgs e)
		{
			// Stop file watchers if running
			int runState = Config.Settings.dossierFileWathcherRun;
			if (runState == 1)
			{
				Config.Settings.dossierFileWathcherRun = 0;
				SetListener();
			}
			// Show dialog
			Form frm = new Forms.RecalcBattleWN8();
			frm.ShowDialog();
			// Return to prev file watcher state
			if (runState != Config.Settings.dossierFileWathcherRun)
			{
				Config.Settings.dossierFileWathcherRun = runState;
				SetListener();
			}
		}

		private void RunRecalcBattleWN8(bool autoRun = false)
		{
			Form frm = new Forms.RecalcBattleWN8(autoRun);
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

		private void mSettingsRunBattleCheck_Click(object sender, EventArgs e)
		{
			if (Dossier2db.dossierRunning)
				MsgBox.Show("Battle check is already running, cannot run twice at the same time.", "Battle Check Already Running", this);
			else
			{
				// Stop file watchers if running
				int runState = Config.Settings.dossierFileWathcherRun;
				if (runState == 1)
				{
					Config.Settings.dossierFileWathcherRun = 0;
					SetListener();
				}
				// Whow dialog and run battle check if selected
				RunBattleCheckHelper.CurrentBattleCheckMode = RunBattleCheckHelper.RunBattleCheckMode.Cancelled;
				Form frm = new Forms.ManualCheckNewBattles();
				frm.ShowDialog();
				if (RunBattleCheckHelper.CurrentBattleCheckMode != RunBattleCheckHelper.RunBattleCheckMode.Cancelled)
				{
					bool forceUpdate = (RunBattleCheckHelper.CurrentBattleCheckMode == RunBattleCheckHelper.RunBattleCheckMode.ForceUpdateAll);
					string message = "Running battle check...";
					if (forceUpdate) message = "Running battle check with force update...";
					RunDossierFileCheck(message, forceUpdate);
				}
				// Return to prev file watcher state
				if (runState != Config.Settings.dossierFileWathcherRun)
				{
					Config.Settings.dossierFileWathcherRun = runState;
					SetListener();
				}
			}				
		}

		private void RunDossierFileCheck(string message, bool forceUpdate)
		{
			if (Dossier2db.dossierRunning)
				SetStatus2("Terminated new battle check, already running...");
			else
			{
				SetStatus2(message);
				Log.AddToLogBuffer("", false);
				Log.AddToLogBuffer(message, true);
				dossier2json d2j = new dossier2json();
				d2j.ManualRunInBackground(message, forceUpdate);
				Log.WriteLogBuffer();
			}
		}

		private void toolItemShowDbTables_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.DatabaseTable();
			FormHelper.OpenFormCenterOfParent(this, frm);
		}

		private void toolItemImportBattlesFromWotStat_Click(object sender, EventArgs e)
		{
			if (Dossier2db.dossierRunning)
				MsgBox.Show("Battle check is running, cannot start import at the same time. Please wait some seconds until battle check is done." +
							Environment.NewLine + Environment.NewLine, "Cannot start import yet", this);
			else
			{
				// Stop file watchers if running
				int runState = Config.Settings.dossierFileWathcherRun;
				if (runState == 1)
				{
					Config.Settings.dossierFileWathcherRun = 0;
					SetListener();
					Application.DoEvents();
				}
				// Show import form
				Form frm = new Forms.ImportWotStat();
				frm.ShowDialog();
				// Return to prev file watcher state
				if (runState != Config.Settings.dossierFileWathcherRun)
				{
					Config.Settings.dossierFileWathcherRun = runState;
					SetListener();
					Application.DoEvents();
				}
			}
			
		}

		private void mHelpAbout_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.About();
			frm.ShowDialog();
		}

		private void mHelpCheckVersion_Click(object sender, EventArgs e)
		{
			RunCheckForNewVersion(true);
		}

		private void mHelpMessage_Click(object sender, EventArgs e)
		{
			// Message
			VersionInfo vi = CheckForNewVersion.versionInfo;
			if (vi.message == null)
				MsgBox.Show("Message not fetched yet, try again in some seconds...", "Message not retrieved yet", this);
			else if (vi.message != "")
				MsgBox.Show(vi.message + Environment.NewLine + Environment.NewLine, "Message published " + vi.messageDate.ToString("dd.MM.yyyy"), this);
			else
				MsgBox.Show("No message is currently published", "No message published", this);
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
					ShowView("Refreshed grid after 'In Garage' tank list updated"); // Refresh grid now
				}
			}
		}

		private void mSettingsAppLayout_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.ApplicationLayout();
			frm.ShowDialog();
			dataGridMain.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", Config.Settings.gridFontSize);
			dataGridMain.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", Config.Settings.gridFontSize);
			dataGridMain.RowHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", Config.Settings.gridFontSize);
			ChangeView(MainSettings.View, true);
		}

		private void mExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void mWoT_Click(object sender, EventArgs e)
		{
			StartWoTGame();
		}


		private void StartWoTGame()
		{
			string err = "";
			string msg = "";
			try
			{
				if (Config.Settings.wotGameStartType == ConfigData.WoTGameStartType.NotConfigured)
				{
					Form frm = new Forms.WoTGameClientSettings();
					frm.ShowDialog();
				}
				else
				{
					if (Config.Settings.wotGameStartType != ConfigData.WoTGameStartType.None)
					{
						// Start WoT
						err = "Error trying to start World of Tanks, check WoT game start settings.";
						string workingDir = Config.Settings.wotGameFolder;
						string lastchar = workingDir.Substring(workingDir.Length - 1, 1);
						if (lastchar == "/" || lastchar == "\\")
							workingDir = workingDir.Substring(0, workingDir.Length - 1);
						string filename = "";
						switch (Config.Settings.wotGameStartType)
						{
							case ConfigData.WoTGameStartType.Launcher:
								filename = "WOTLauncher.exe";
								break;
							case ConfigData.WoTGameStartType.Game:
								filename = "WorldOfTanks.exe";
								break;
						}
						// Check if running
						// Find WoT process
						Process[] wotProcess = Process.GetProcessesByName("WorldOfTanks");
						if (wotProcess.Length > 0)
						{
							msg = "World of Tanks is already running";
						}
						else
						{
							msg = "Starting World of Tanks";
							//Create process
							System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
							pProcess.StartInfo.FileName = workingDir + "/" + filename;
							pProcess.StartInfo.WorkingDirectory = workingDir;
							pProcess.StartInfo.UseShellExecute = false;
							pProcess.StartInfo.RedirectStandardOutput = true;
							//Start the process
							pProcess.Start();
						}
						//Start timer for setting affnity after started if optimized settings used
						if (Config.Settings.wotGameAffinity != 0)
						{
							timerWoTAffnity.Enabled = true;
							timerWotAffnityCount = 0;
						}
					}
					// Run batch file
					if (Config.Settings.wotGameRunBatchFile != "")
					{
						if (msg == "")
							msg = "Starting additional programs";
						else
							msg += ", starting additional programs";
						err = "Error trying to start additional programs, check WoT game start settings.";
						//Create process
						ProcessStartInfo psi = new ProcessStartInfo();
						psi.RedirectStandardInput = true;
						psi.RedirectStandardOutput = true;
						psi.UseShellExecute = false;
						psi.WindowStyle = ProcessWindowStyle.Hidden;
						psi.CreateNoWindow = true;
						psi.FileName = Config.Settings.wotGameRunBatchFile;
						//Start the process
						Process p = Process.Start(psi);
					}
					// No action message
					if (Config.Settings.wotGameStartType == ConfigData.WoTGameStartType.None && Config.Settings.wotGameRunBatchFile == "")
					{
						msg = "No action is configured, check your startup settings";
					}
					if (msg != "")
						SetStatus2(msg);
				}
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				MsgBox.Show(err + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine, "Error running programs", this);
			}
		}

		private int timerWotAffnityCount = 0;
		private void timerWoTAffnity_Tick(object sender, EventArgs e)
		{
			try
			{
				// Number of tics before timeout
				timerWotAffnityCount++;
				if (timerWotAffnityCount > 30) timerWoTAffnity.Enabled = false; // 10 = 1 minute before timeout
				// Find WoT process
				Process[] wotProcess = Process.GetProcessesByName("WorldOfTanks");
				if (wotProcess.Length > 0)
				{
					foreach (Process p in wotProcess)
					{
						long AffinityMask = (long)p.ProcessorAffinity;
						long newAffinityMask = Config.Settings.wotGameAffinity;
						if (AffinityMask != newAffinityMask || p.PriorityClass != ProcessPriorityClass.High)
						{
							string s = "Changed WoT game client process to optimized CPU settings. From: " + AffinityMask.ToBinary();
							s += " (" + p.PriorityClass.ToString() + " Priority)";
							s += " - To: " + newAffinityMask.ToBinary() + " (High Priority)";
							SetStatus2(s);
							p.ProcessorAffinity = (IntPtr)newAffinityMask;
							p.PriorityClass = ProcessPriorityClass.High;
						}
						else
						{
							string s = "WoT game client process uses optimized CPU settings: " + AffinityMask.ToBinary();
							s += " (" + p.PriorityClass.ToString() + " Priority)";
							SetStatus2(s);
						}
					}
					timerWoTAffnity.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				timerWoTAffnity.Enabled = false;
				SetStatus2("Could not change WoT using optimization settings, check log file for details");
				Log.LogToFile(ex);
			}

		}

		private void mWoTStartGameSettings_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.WoTGameClientSettings();
			frm.ShowDialog();
		}

		private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (this.WindowState == FormWindowState.Minimized)
				{
					this.WindowState = mainFormWindowsState;
				}
				else
				{
					this.WindowState = FormWindowState.Minimized;
				}
			}
		}

		#endregion

		#region Testing
		
		private void ViewRangeTesting()
		{
			Form frm = new Forms.ViewRange();
			frm.ShowDialog();
		}

		

		#endregion

		#region Gadgets

		private void HomeViewCreate(string Status2Message)
		{
			ResizeNow();
			// First remove current controls
			List<Control> controlsRemove = new List<Control>();
			foreach (Control c in panelMainArea.Controls)
			{
				if (c.Name.Length > 1 && c.Name.Substring(0, 2) == "uc")
				{
					controlsRemove.Add(c);
				}
			}
			foreach (Control c in controlsRemove)
			{
				panelMainArea.Controls.Remove(c);
			}
			// Add grid image
			Image image = imageGrid.Images[0];
			Bitmap bm = new Bitmap(image.Width * 40, image.Height * 30);
			Graphics gp = Graphics.FromImage(bm);
			for (int x = 0; x < 40; x++)
			{
				for (int y = 0; y < 30; y++)
				{
					gp.DrawImage(image, new Point(x * 50, y * 50));
				}
			}
			Image result = bm;
			PictureBox picGrid = new PictureBox();
			picGrid.Width = 2000;
			picGrid.Height = 1500;
			picGrid.Top = 2;
			picGrid.Left = 2;
			picGrid.Image = result;
			picGrid.Name = "ucPicGrid";
			picGrid.Visible = false;
			panelMainArea.Controls.Add(picGrid);
			
			// Add current gadgets
			GadgetHelper.GetGadgets();
			foreach (GadgetHelper.GadgetItem gadget in GadgetHelper.gadgets)
			{
				panelMainArea.Controls.Add(gadget.control);
				Control[] c = panelMainArea.Controls.Find(gadget.control.Name, false);
				c[0].Height = gadget.height;
				c[0].Width = gadget.width;
				c[0].BringToFront();
			}
		}

		private void HomeViewRefresh(string Status2Message)
		{
			List<Control> controls = new List<Control>();
			foreach (Control c in panelMainArea.Controls)
			{
				if (c.Name.Substring(0, 2) == "uc")
				{
					c.Invalidate();
				}
			}
			SetStatus2(Status2Message);
		}

		private void mGadgetAdd(object sender, EventArgs e)
		{
			// Enable edit mode if not
			if (!mHomeEdit.Checked)
			{
				mHomeEdit.Checked = true;
				GadgetEditModeChange();
			}
			// Get gadget
			ToolStripMenuItem menu = (ToolStripMenuItem)sender;
			string controlName = menu.Tag.ToString();
			// Find popup form for controls with parameters
			ShowGadgetParameter(controlName);
			if (GadgetHelper.newParametersOK)
			{
				// Create new control
				Control controlAdd = GadgetHelper.GetGadgetControl(menu.Tag.ToString(), GadgetHelper.newParameters);
				// Save new control
				GadgetHelper.GadgetItem newGadget = new GadgetHelper.GadgetItem();
				newGadget.control = controlAdd;
				newGadget.controlName = controlName;
				newGadget.height = controlAdd.Height;
				newGadget.width = controlAdd.Width;
				newGadget.posX = 2;
				newGadget.posY = 2;
				newGadget.control.Top = newGadget.posX;
				newGadget.control.Left = newGadget.posY;
				newGadget.sortorder = -1;
				newGadget.visible = true;
				newGadget.resizable = GadgetHelper.IsGadgetRezisable(controlName);
				// Special gadgets customization
				switch (controlName)
				{
					case "ucBattleListLargeImages":
						newGadget.width = (Convert.ToInt32(GadgetHelper.newParameters[0]) * 170) - 10;
						newGadget.height = (Convert.ToInt32(GadgetHelper.newParameters[1]) * 120) - 10;
						break;
				}
				newGadget.name = GadgetHelper.GetGadgetName(controlName);
				int gadgetId = GadgetHelper.InsertNewGadget(newGadget);
				newGadget.id = gadgetId;
				// Add to panel and resize
				controlAdd.Name = "uc" + gadgetId.ToString();
				panelMainArea.Controls.Add(controlAdd);
				Control[] c = panelMainArea.Controls.Find(controlAdd.Name, false);
				c[0].Height = newGadget.height;
				c[0].Width = newGadget.width;
				c[0].BringToFront();
				c[0].Enabled = false;
			}
		}

		private void mHomeEdit_Click(object sender, EventArgs e)
		{
			mHomeEdit.Checked = !mHomeEdit.Checked;
			GadgetEditModeChange();
		}

		private void GadgetEditModeChange()
		{
			if (mHomeEdit.Checked)
			{
				// Enable edit style
				Status2AutoEnabled = false;
				timerStatus2.Enabled = false;
				Application.DoEvents();
				lblStatus2.ForeColor = ColorTheme.FormBorderBlue;
				lblStatus2.Text = "Enabled Home View Edit Mode";
				// Add mouse move event for main panel
				panelMainArea.MouseMove += new MouseEventHandler(panelEditor_MouseMove);
				panelMainArea.MouseDown += new MouseEventHandler(panelEditor_MouseDown);
				panelMainArea.MouseUp += new MouseEventHandler(panelEditor_MouseUp);
				
				// Disable all gadgets
				foreach (Control c in panelMainArea.Controls)
				{
					if (c.Name.Substring(0, 2) == "uc")
					{
						c.Enabled = false;
					}
				}
				// Show grid
				Control[] ucPicGrid = panelMainArea.Controls.Find("ucPicGrid", false);
				ucPicGrid[0].Visible = true;
			}
			else
			{
				// RemoveOwnedForm Grid
				Control[] ucPicGrid = panelMainArea.Controls.Find("ucPicGrid", false);
				ucPicGrid[0].Visible = false;
				// Remove mouse move event for main panel
				panelMainArea.MouseMove -= panelEditor_MouseMove;
				panelMainArea.MouseDown -= panelEditor_MouseDown;
				panelMainArea.MouseUp -= panelEditor_MouseUp;
				panelMainArea.ContextMenuStrip = null;
				panelMainArea.Refresh(); // force paint event
				// Enable all gadgets
				foreach (Control c in panelMainArea.Controls)
				{
					if (c.Name.Substring(0, 2) == "uc")
					{
						c.Enabled = true;
					}
				}
				// Enable default style
				Status2AutoEnabled = true;
				SetStatus2("Disabled Home View Edit Mode");
				GadgetHelper.SortGadgets();
			}
		}

		//private GadgetHelper.GadgetItem lastSelectedGadget = null;
		private GadgetHelper.GadgetItem selectedGadget = null; // current selected gadget
		private bool moveOrRezizeMode = false;
		
		private void panelEditor_MouseMove(object sender, MouseEventArgs e)
		{
			string actionText = "";
			// Check if move or resize mode
			if (!moveOrRezizeMode)
			{
				// Not moving or resizing , just selecting area
				GadgetHelper.GadgetItem newSelectedGadget = GadgetHelper.FindGadgetFromLocation(e.X, e.Y);
				// Check if pointing on gadget 
				if (newSelectedGadget != null)
				{
					// Yes, poining on gadget - check if pointing on new gadget
					if (newSelectedGadget != selectedGadget)
					{
						// New gadget selected
						if (selectedGadget != null)
						{
							selectedGadget.control.BackColor = ColorTheme.FormBack; // Remove selected border from previous selected gadget
						}
						selectedGadget = newSelectedGadget;
						selectedGadget.control.BackColor = ColorTheme.FormBackSelectedGadget;
						bool hasParam = GadgetHelper.HasGadetParameter(selectedGadget);
						CreateGadgetContextMenu(hasParam);
						panelMainArea.Refresh(); // force paint event
						actionText = " - Selected gadget: " + selectedGadget.name.ToString();
					}
					else
					{
						// pointing on same gadget
						actionText = " - Selected gadget: " + selectedGadget.name.ToString();
					}
					// Check if resizable
					if (selectedGadget.resizable)
					{
						if (e.X > selectedGadget.control.Left + selectedGadget.control.Width - 20 && e.Y > selectedGadget.control.Top + selectedGadget.control.Height - 20)
							Cursor = Cursors.SizeNWSE;
						else if (e.X > selectedGadget.control.Left + selectedGadget.control.Width - 10)
							Cursor = Cursors.SizeWE;
						else if (e.Y > selectedGadget.control.Top + selectedGadget.control.Height - 10)
							Cursor = Cursors.SizeNS;
					}
				}
				else
				{
					// No, poining on background
					if (selectedGadget != null)
					{
						selectedGadget.control.BackColor = ColorTheme.FormBack;
						selectedGadget = null;
						panelMainArea.ContextMenuStrip = null;
						panelMainArea.Refresh(); // force paint event
					}
					// Return to default mouse cursor
					if (Cursor == Cursors.Help)
						Cursor = Cursors.Default;
				}
			}
			else
			{
				// Moving selected gadget
				int gridSize = 10;
				if (Cursor == Cursors.SizeAll)
				{
					selectedGadget.control.Top = selectedGadgetTop + (Convert.ToInt32((Cursor.Position.Y - mouseDownY) / gridSize) * gridSize);
					selectedGadget.control.Left = selectedGadgetLeft + (Convert.ToInt32((Cursor.Position.X - mouseDownX) / gridSize) * gridSize);
					actionText = " - Moving gadget: " + selectedGadget.name.ToString();
				}
				else if (Cursor == Cursors.SizeWE)
				{
					actionText = " - Resize horizontal gadget: " + selectedGadget.name.ToString();
					selectedGadget.control.BackColor = ColorTheme.FormBack;
					int newSize = selectedGadget.width + (Convert.ToInt32((Cursor.Position.X - mouseDownX) / gridSize) * gridSize);
					if (newSize > 100) selectedGadget.control.Width = newSize;
					selectedGadget.control.BackColor = ColorTheme.FormBackSelectedGadget;
				}
				else if (Cursor == Cursors.SizeNS)
				{
					actionText = " - Resize vertical gadget: " + selectedGadget.name.ToString();
					selectedGadget.control.BackColor = ColorTheme.FormBack;
					int newSize = selectedGadget.height + (Convert.ToInt32((Cursor.Position.Y - mouseDownY) / gridSize) * gridSize);
					if (newSize > 100) selectedGadget.control.Height = newSize;
					selectedGadget.control.BackColor = ColorTheme.FormBackSelectedGadget;
				}
				else if (Cursor == Cursors.SizeNWSE)
				{
					actionText = " - Resize gadget: " + selectedGadget.name.ToString();
					selectedGadget.control.BackColor = ColorTheme.FormBack;
					int newSize = selectedGadget.height + (Convert.ToInt32((Cursor.Position.Y - mouseDownY) / gridSize) * gridSize);
					if (newSize > 100) selectedGadget.control.Height = newSize;
					newSize = selectedGadget.width + (Convert.ToInt32((Cursor.Position.X - mouseDownX) / gridSize) * gridSize);
					if (newSize > 100) selectedGadget.control.Width = newSize;
					selectedGadget.control.BackColor = ColorTheme.FormBackSelectedGadget;
				}
			}
			// Show mouse position
			lblStatus2.Text = "Position: " + e.X + " x " + e.Y + actionText;
		}

		private int mouseDownX = 0;
		private int mouseDownY = 0;
		private int selectedGadgetTop = 0;
		private int selectedGadgetLeft = 0;
		
		private void panelEditor_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left && selectedGadget != null)
			{
				// move or resize gadget mode
				moveOrRezizeMode = true;
				if (selectedGadget.resizable)
				{
					if (e.X > (selectedGadget.control.Left + selectedGadget.control.Width - 20) && e.Y > (selectedGadget.control.Top + selectedGadget.control.Height - 20))
						Cursor = Cursors.SizeNWSE;
					else if (e.X > (selectedGadget.control.Left + selectedGadget.control.Width - 10))
						Cursor = Cursors.SizeWE;
					else if (e.Y > (selectedGadget.control.Top + selectedGadget.control.Height - 10))
						Cursor = Cursors.SizeNS;
					else
						Cursor = Cursors.SizeAll;
				}
				else
				{
					Cursor = Cursors.SizeAll;
				}
				// Remeber position
				mouseDownX = Cursor.Position.X;
				mouseDownY = Cursor.Position.Y;
				selectedGadgetTop = selectedGadget.control.Top;
				selectedGadgetLeft = selectedGadget.control.Left;
				selectedGadget.control.BringToFront();
			}
			if (e.Button == System.Windows.Forms.MouseButtons.Right && selectedGadget != null)
			{
				// move mode off
				moveOrRezizeMode = false;
				Cursor = Cursors.Default;
			}
		}

		private void panelEditor_MouseUp(object sender, MouseEventArgs e)
		{
			if (selectedGadget != null)
			{
				// Save new location if moved 
				if (selectedGadget.posY != selectedGadget.control.Top || selectedGadget.posX != selectedGadget.control.Left)
				{
					GadgetHelper.SaveGadgetPosition(selectedGadget.id, selectedGadget.control.Left, selectedGadget.control.Top);
					selectedGadget.posY = selectedGadget.control.Top;
					selectedGadget.posX = selectedGadget.control.Left;
				}
				// Save new size if resized
				if (selectedGadget.width != selectedGadget.control.Width || selectedGadget.height != selectedGadget.control.Height)
				{
					selectedGadget.height = selectedGadget.control.Height;
					selectedGadget.width = selectedGadget.control.Width;
					GadgetHelper.SaveGadgetSize(selectedGadget);
				}
			}
			// move mode off
			moveOrRezizeMode = false;
		}
		

		private void mGadgetRedraw_Click(object sender, EventArgs e)
		{
			HomeViewCreate("Redrawn gadgets");
			GadgetEditModeChange();
		}

		private void CreateGadgetContextMenu(bool customizeAvailable)
		{
			// Datagrid context menu (Right click on Grid)
			ContextMenuStrip gadgetMainPopup = new ContextMenuStrip();
			gadgetMainPopup.Renderer = new StripRenderer();
			gadgetMainPopup.BackColor = ColorTheme.ToolGrayMainBack;
			// Items
			ToolStripSeparator gadgetMainPopup_Separator1 = new ToolStripSeparator();
			ToolStripMenuItem gadgetMainPopup_Remove = new ToolStripMenuItem("Remove Gadget");
			gadgetMainPopup_Remove.Click += new EventHandler(gadgetMainPopup_Remove_Click);
			ToolStripMenuItem gadgetMainPopup_Customize = new ToolStripMenuItem("Customize Gadget");
			gadgetMainPopup_Customize.Click += new EventHandler(gadgetMainPopup_Customize_Click);
			// Add events
			gadgetMainPopup.Opening += new System.ComponentModel.CancelEventHandler(gadgetMainPopup_Opening);
			
			// Add context menu
			if (customizeAvailable)
			{
				gadgetMainPopup.Items.AddRange(new ToolStripItem[] 
				{ 
					gadgetMainPopup_Customize, 
					gadgetMainPopup_Separator1,
					gadgetMainPopup_Remove
				});
			}
			else
			{
				gadgetMainPopup.Items.AddRange(new ToolStripItem[] 
				{ 
					gadgetMainPopup_Remove
				});
			}
			//Assign to panel
			panelMainArea.ContextMenuStrip = gadgetMainPopup;
		}

		private void gadgetMainPopup_Opening(object sender, CancelEventArgs e)
		{
			if (selectedGadget == null)
			{
				e.Cancel = true; // Close if validation fails
			}
		}

		private void gadgetMainPopup_Remove_Click(object sender, EventArgs e)
		{
			if (selectedGadget != null)
			{
				GadgetHelper.RemoveGadget(selectedGadget);
				panelMainArea.Controls.Remove(selectedGadget.control);
			}
		}

		private void gadgetMainPopup_Customize_Click(object sender, EventArgs e)
		{
			GadgetHelper.GadgetItem gadget = selectedGadget;
			ShowGadgetParameter(gadget.controlName, gadget.id);
			if (GadgetHelper.newParametersOK)
			{
				// Special gadgets customization
				switch (gadget.controlName)
				{
					case "ucBattleListLargeImages":
						gadget.width = (Convert.ToInt32(GadgetHelper.newParameters[0]) * 170) - 10;
						gadget.height = (Convert.ToInt32(GadgetHelper.newParameters[1]) * 120) - 10;
						GadgetHelper.SaveGadgetSize(gadget);
						break;
				}
				// save new settings
				GadgetHelper.SaveGadgetParameter(gadget);
				// remove it
				panelMainArea.Controls.Remove(gadget.control);
				// get updated control
				Control uc = GadgetHelper.GetGadgetControl(gadget.controlName, GadgetHelper.newParameters);
				uc.Name = "uc" + gadget.id.ToString();
				uc.Tag = gadget.name;
				uc.Top = gadget.posY;
				uc.Left = gadget.posX;
				uc.Height = gadget.height;
				uc.Width = gadget.width;
				gadget.control = uc;
				
				// Add to panel and resize
				panelMainArea.Controls.Add(gadget.control);
				Control[] c = panelMainArea.Controls.Find(gadget.control.Name, false);
				c[0].Height = gadget.height;
				c[0].Width = gadget.width;
				c[0].BringToFront();
				c[0].Enabled = false;
			}
		}

		private void ShowGadgetParameter(string controlName, int gadgetId = -1)
		{
			GadgetHelper.newParameters = new object[] { null, null, null, null, null };
			GadgetHelper.newParametersOK = true;
			Form frm = null;
			switch (controlName)
			{
				case "ucGaugeWinRate":
					frm = new Gadget.paramBattleMode(gadgetId);
					break;
				case "ucBattleListLargeImages":
					frm = new Gadget.paramColsRows(gadgetId);
					break;
				case "ucChartTier":
					frm = new Gadget.paramBM_Color(gadgetId);
					break;
				case "ucChartNation":
					frm = new Gadget.paramBM_Color(gadgetId);
					break;
				case "ucChartTankType":
					frm = new Gadget.paramBM_Color(gadgetId);
					break;
				case "ucGaugeKillDeath":
					frm = new Gadget.paramBattleMode(gadgetId);
					break;
				case "ucGaugeDmgCausedReceived":
					frm = new Gadget.paramBattleMode(gadgetId);
					break;
			}
			if (frm != null)
				frm.ShowDialog(this);
		}

		private void mGadgetRemoveAll_Click(object sender, EventArgs e)
		{
			MsgBox.Button answer = MsgBox.Show("This will remove all gadgets.", "Remove all gadgets", MsgBoxType.OKCancel, this);
			if (answer == MsgBox.Button.OKButton)
			{
				GadgetHelper.RemoveGadgetAll();
				HomeViewCreate("Removed all gadgets");
			}
		}

		private void mGadgetReset_Click(object sender, EventArgs e)
		{
			MsgBox.Button answer = MsgBox.Show("This will remove all current gadgets, and reset to default setup.", "Reset to default gadgets", MsgBoxType.OKCancel, this);
			if (answer == MsgBox.Button.OKButton)
			{
				GadgetHelper.RemoveGadgetAll();
				GadgetHelper.DefaultSetup();
				mHomeEdit.Checked = false;
				GadgetEditModeChange();
				HomeViewCreate("Reset to default gadgets");

			}
		}

		#endregion

	}
}
