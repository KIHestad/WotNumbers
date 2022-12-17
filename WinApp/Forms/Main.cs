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
using WinApp.Code.FormLayout;
using WinApp.Code.FormView;
using System.Threading.Tasks;

namespace WinApp.Forms
{
	public partial class Main : Form
	{
		#region Init Content and Layout

		private bool Init = true;
		private ConfigData.Result LoadConfigDataResult = null;
		private ConfigData.PosSize mainFormPosSize = new ConfigData.PosSize();
		private int currentPlayerId = 0;
		private bool mainGridFormatting = false; // Controls if grid should be formattet or not
		private bool mainGridSaveColWidth = false; // Controls if change width on cols should be saved
		private FormWindowState mainFormWindowsState = FormWindowState.Normal;
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

		public Main()
		{
			InitializeComponent();
		}

		private async void Main_Load(object sender, EventArgs e)
		{
			// Get Config
			LoadConfigDataResult = await Config.GetConfig();
			currentPlayerId = Config.Settings.playerId;
			mainFormPosSize = Config.Settings.posSize;
			// Check if outside of visible area
			bool withinAnyScreen = false;
			foreach (var item in Screen.AllScreens)
			{
				if (item.WorkingArea.Top <= mainFormPosSize.Top &&
					item.WorkingArea.Left <= mainFormPosSize.Left &&
					item.WorkingArea.Top + item.WorkingArea.Height >= mainFormPosSize.Top + mainFormPosSize.Height &&
					item.WorkingArea.Left + item.WorkingArea.Width >= mainFormPosSize.Left + mainFormPosSize.Width)
				{
					withinAnyScreen = true;
				}
			}
			// Set PosSize
			if (mainFormPosSize != null && withinAnyScreen)
			{
				this.Top = mainFormPosSize.Top;
				this.Left = mainFormPosSize.Left;
				this.Width = mainFormPosSize.Width;
				this.Height = mainFormPosSize.Height;
			}
			// Make sure borderless form do not cover task bar when maximized
			Screen screen = Screen.FromControl(this);
			this.MaximumSize = screen.WorkingArea.Size;

			// Black Border on loading
			MainTheme.FormBorderColor = ColorTheme.FormBorderBlack;
			// Resize Form Theme Title Area to fit 125% or 150% font size in Win
			Graphics graphics = this.CreateGraphics();
			Double dpiFactor = graphics.DpiX / 96;
			if (dpiFactor != 1)
			{
				// Scale form according to scale factor
				MainTheme.TitleHeight = MainTheme.SystemExitImage.Height + toolMain.Height;
				// Move main toolbar to bottom of title height
				toolMain.Top = MainTheme.TitleHeight - toolMain.Height + MainTheme.FormMargin + 2;
			}
			this.ShowInTaskbar = !Config.Settings.notifyIconUse;
			Refresh();
			// Maximize now if last settings
			if (mainFormPosSize.WindowState == FormWindowState.Maximized)
			{
				this.WindowState = FormWindowState.Maximized;
				mainFormWindowsState = FormWindowState.Maximized;
			}
			this.Opacity = 100;
		}

		private async void Main_Shown(object sender, EventArgs e)
		{
			// Style toolbar
			//toolMain.Renderer = new StripRenderer();
			//toolMain.ShowItemToolTips = true;
			mBattles.Visible = false;
			mTankFilter.Visible = false;
			mRefreshSeparator.Visible = true;
			mColumnSelect.Visible = false;
			mMode.Visible = false;
			mHomeView.Visible = true;
			mHomeViewEditMode.Visible = true;
			mBattleGroup.Visible = false;
			// Check config
			if (await DB.CheckConnection(false))
			{
				await TankHelper.GetAllLists();
				// Check DB Version an dupgrade if needed
				bool versionOK = await DBVersion.CheckForDbUpgrade(this);
			}
			// Check for missing or new admin db
			if (DBVersion.CopyAdminDB || !File.Exists(Config.AppDataBaseFolder + "Admin.db"))
			{
				string adminDB = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\Database\\Admin.db";
				File.Copy(adminDB, Config.AppDataBaseFolder + "Admin.db", true);
			}

			// Statusbar text
			lblStatus1.Text = "";
			lblStatus2.Text = "Application starting...";
			lblStatusRowCount.Text = "";
			// Log startup
			string startupMessage = "********* Application startup - Wot Numbers " + AppVersion.AssemblyVersion + " " + AppVersion.BuildVersion + " *********";
			string startupLine = ("").PadLeft(startupMessage.Length, '*');
			string startupSpace = ("").PadLeft(20, ' ');
			Log.AddToLogBuffer("", false);
			Log.AddToLogBuffer("", false);
			Log.AddToLogBuffer($"{startupSpace}'{startupLine}'", false);
			Log.AddToLogBuffer($"{startupSpace}'{startupMessage}'", false);
			Log.AddToLogBuffer($"{startupSpace}'{startupLine}'", false);
			// Write log
			await Log.WriteLogBuffer(true);


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
			int gridAreaTop = 0; // Start below info panel
			int gridAreaHeight = panelMainArea.Height; // Grid height
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
			// Set Rating colors
			ColorRangeScheme.SetRatingColors();

			try
			{
				// Systray icon with context menu
				CreateNotifyIconContextMenu();
				notifyIcon.ContextMenu = notifyIconContextMenu;
				notifyIcon.Visible = Config.Settings.notifyIconUse;
				MainTheme.FormExitAsMinimize = Config.Settings.notifyIconFormExitToMinimize;

				// Start WoT if autoRun is enabled
				if (Config.Settings.wotGameAutoStart)
					await StartWoTGame();
				// Ready to draw form
				Init = false;

				// Show vbAddict Player Profil toolbar if upload activated
				// mVBaddict.Visible = (Config.Settings.vBAddictShowToolBarMenu);

				// Create IronPython Engine
				PythonEngine.CreateEngine();

				// Startup settings
				if (!LoadConfigDataResult.Success)
				{
					Log.AddToLogBuffer(" > No config MsgBox", true);
					MsgBox.Button answer = Code.MsgBox.Show(
						"Press 'OK' to create new SQLite database." +
						Environment.NewLine + Environment.NewLine +
						"Press 'Cancel' for advanced setup to relocate previously used database or create MSSQL database." +
						Environment.NewLine + Environment.NewLine,
						"Welcome to Wot Numbers", MsgBox.Type.OKCancel, this);
					if (answer == MsgBox.Button.OK)
						await AutoSetup();
					if (!LoadConfigDataResult.Success)
					{
						Config.Settings.dossierFileWathcherRun = 0;
						await SetListener(false);
						MsgBox.Show(LoadConfigDataResult.Message, "Could not load config data", this);
						Form frm = new Forms.Settings.AppSettings(AppSettingsHelper.Tabs.Main);
						frm.ShowDialog(this);
					}
				}

				// Init form
				SetFormTitle();
				await SetListener(false);
				await MainSettings.SetDefaultGridFilters();
				BattleChartHelper.Settings = new BattleChartHelper.BattleChartSettings(); // Set default battle chart settings
																						  // TODO: Set new blank current view, add feature for getting latest used favourite from config settings
				BattleChartHelper.CurrentChartView = new List<BattleChartHelper.BattleChartItem>();
				Code.Rating.WN9.SetTierAvgList();
				if (await DB.CheckConnection(true, this))
				{
					// Moved to Page Load - to run this and make sure db upgrades are done before app starts
					// TankHelper.GetAllLists();
					// Check DB Version an dupgrade if needed
					// bool versionOK = DBVersion.CheckForDbUpgrade(this);

					// Add init items to Form
					await SetFavListMenu();
					// Check for res_mods folder
					await WoThelper.CheckForNewResModsFolder();
					// Get vBAddict settings
					// await vBAddictHelper.GetSettings();
					// Get Images
					ImageHelper.CreateTankImageTable();
					await ImageHelper.LoadTankImages();
					ImageHelper.CreateMasteryBageImageTable();
					ImageHelper.CreateTankTypeImageTable();
					ImageHelper.CreateNationImageTable();
					ImageHelper.CheckedMenuIcon = imageListToolStrip.Images[0];
					// Home view recent list
					await GetHomeViewRecentList();
					mHomeView.Text = Config.Settings.currentHomeView;
					// Battle Count Filter init 
					await BattleCountFilterSet();
					// Check if current Home View has gadgets, if not use default
					DataTable dtHomeView = await DB.FetchData("select * from gadget");
					if (dtHomeView == null || dtHomeView.Rows.Count == 0)
					{
						string file = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\New_Default_Setup.json";
						bool ok = await GadgetHelper.HomeViewLoadFromFile(file, false);
						if (ok)
						{
							mHomeView.Text = "Default";
							Config.Settings.currentHomeView = mHomeView.Text;
							await Config.SaveConfig();
						}
						else
						{
							await GadgetHelper.RemoveGadgetAll();
							await HomeViewCreate("Could not load Home View");
							await HomeViewRefresh("Refresh Home View");
						}
					}
					// Set current submenu checked
					CheckCurrentHomeViewSubMenu();

					// Show view
					await ChangeView(GridView.Views.Overall, true);
					// Check BRR
					if (Config.Settings.CheckForBrrOnStartup)
					{
						bool brrOK = true;
						if (String.IsNullOrEmpty(Config.Settings.wotGameFolder))
						{
							brrOK = await BattleResultRetriever.IsWoTGameFolderOK();
							if (!brrOK)
							{
								MsgBox.Show(
									"Check for WoT Battle Result Retriever is by default on. Please locate WoT game folder, or turn of check for Battle Result Retriever on startup." + Environment.NewLine + Environment.NewLine,
									"Battle Result Retriver Settings",
									this
								);
								Form frm = new Forms.Settings.AppSettings(AppSettingsHelper.Tabs.WoTGameClient);
								frm.ShowDialog(this);
							}
						}

						if (brrOK && (!BattleResultRetriever.Installed || DBVersion.RunInstallNewBrrVersion))
						{
							string s = "WoT Battle Result Retriever mod is not installed, install now? ";
							if (DBVersion.RunInstallNewBrrVersion)
								s = "New version of WoT Battle Result Retriever mod needs to be installed, install now? ";
							MsgBox.Button result = MsgBox.Show(
								s + Environment.NewLine + Environment.NewLine +
								"To avoid this check on startup go to Settings + WoT Game Settings" + Environment.NewLine + Environment.NewLine,
								"Install Battle Result Retriever",
								MsgBox.Type.OKCancel, this
							);
							if (result == MsgBox.Button.OK)
							{
								if (!BattleResultRetriever.Install(out string msg))
								{
									MsgBox.Show(msg, "Error installing BRR");
								}
							}
						}
					}
					// Check for recalc grinding progress
					await GrindingHelper.CheckForDailyRecalculateGrindingProgress();

					// Run app statup webservice api, check for new version and more
					await RunAppStartupAPI(false);

				}
				else
				{
					Config.Settings.dossierFileWathcherRun = 0;
					await SetListener(false);
					mAppSettings.Enabled = true;
					mShowDbTables.Enabled = false;
					StatusBarHelper.Message = "Database connection failed";
					StatusBarHelper.ClearAfterNextShow = false;
					SetStatus2("Application started with errors");
				}

				// Set external player icon image
				// ExternalPlayerProfile.image_vBAddict = imageListToolStrip.Images[15];
				ExternalPlayerProfile.image_Wargaming = imageListToolStrip.Images[16];

				// Ready 
				MainTheme.Cursor = Cursors.Default;
				// Write log
				await Log.WriteLogBuffer();
			}
			catch (Exception ex)
			{
				await Log.LogToFile(ex);
				MsgBox.Show("Error occured initializing application:" + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine, "Startup error", this);
			}
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
			ContextMenuStrip dataGridMainPopup = new ContextMenuStrip
			{
				Renderer = new StripRenderer(),
				BackColor = ColorTheme.ToolGrayMainBack
			};
			ToolStripSeparator dataGridMainPopup_Separator = new ToolStripSeparator();

			ToolStripMenuItem dataGridMainPopup_TankDetails = new ToolStripMenuItem("Tank Details")
			{
				Image = imageListToolStrip.Images[1]
			};
			dataGridMainPopup_TankDetails.Click += new EventHandler(dataGridMainPopup_TankDetails_Click);

			ToolStripMenuItem dataGridMainPopup_EditTankInfo = new ToolStripMenuItem("Edit Tank Info");
			dataGridMainPopup_EditTankInfo.Click += new EventHandler(dataGridMainPopup_EditTankInfo_Click);

			ToolStripMenuItem dataGridMainPopup_BattleChart = new ToolStripMenuItem("Chart")
			{
				Image = imageListToolStrip.Images[2]
			};
			dataGridMainPopup_BattleChart.Click += new EventHandler(dataGridMainPopup_BattleChart_Click);

			ToolStripMenuItem dataGridMainPopup_GrindingSetup = new ToolStripMenuItem("Grinding Setup")
			{
				Image = imageListToolStrip.Images[3]
			};
			dataGridMainPopup_GrindingSetup.Click += new EventHandler(dataGridMainPopup_GrindingSetup_Click);

			ToolStripMenuItem dataGridMainPopup_GrindingSetupRecalculate = new ToolStripMenuItem("Recalculate Grinding Progress");
			dataGridMainPopup_GrindingSetupRecalculate.Click += new EventHandler(dataGridMainPopup_GrindingSetupRecalculate_Click);

			ToolStripMenuItem dataGridMainPopup_FilterOnTank = new ToolStripMenuItem("Filter on this tank")
			{
				Image = imageListToolStrip.Images[17]
			};
			dataGridMainPopup_FilterOnTank.Click += new EventHandler(dataGridMainPopup_FilterOnTank_Click);

			ToolStripMenuItem dataGridMainPopup_FilterOnTankSearch = new ToolStripMenuItem("Search for tank...")
			{
				Image = imageListToolStrip.Images[4]
			};
			dataGridMainPopup_FilterOnTankSearch.Click += new EventHandler(dataGridMainPopup_FilterOnTankSearch_Click);

			ToolStripMenuItem dataGridMainPopup_FilterOnTankClear = new ToolStripMenuItem("Remove filter on tank")
			{
				Image = imageListToolStrip.Images[11]
			};
			dataGridMainPopup_FilterOnTankClear.Click += new EventHandler(dataGridMainPopup_FilterOnTankClear_Click);

			ToolStripMenuItem dataGridMainPopup_FavListAddTank = new ToolStripMenuItem("Add tank to favourite tank list")
			{
				Image = imageListToolStrip.Images[5]
			};
			dataGridMainPopup_FavListAddTank.Click += new EventHandler(dataGridMainPopup_FavListAddTank_Click);

			ToolStripMenuItem dataGridMainPopup_FavListRemoveTank = new ToolStripMenuItem("Remove tank from favourite tank list")
			{
				Image = imageListToolStrip.Images[6]
			};
			dataGridMainPopup_FavListRemoveTank.Click += new EventHandler(dataGridMainPopup_FavListRemoveTank_Click);

			ToolStripMenuItem dataGridMainPopup_FavListCreateNew = new ToolStripMenuItem("Create new favourite tank list")
			{
				Image = imageListToolStrip.Images[7]
			};
			dataGridMainPopup_FavListCreateNew.Click += new EventHandler(dataGridMainPopup_FavListCreateNew_Click);

			ToolStripMenuItem dataGridMainPopup_DeleteBattle = new ToolStripMenuItem("Delete this battle")
			{
				Image = imageListToolStrip.Images[8]
			};
			dataGridMainPopup_DeleteBattle.Click += new EventHandler(dataGridMainPopup_DeleteBattle_Click);

			ToolStripMenuItem dataGridMainPopup_BattleDetails = new ToolStripMenuItem("Battle Details")
			{
				Image = imageListToolStrip.Images[9]
			};
			dataGridMainPopup_BattleDetails.Click += new EventHandler(dataGridMainPopup_BattleDetails_Click);

			ToolStripMenuItem dataGridMainPopup_BattleSummary = new ToolStripMenuItem("Summary of Battles")
			{
				Image = imageListToolStrip.Images[12]
			};
			dataGridMainPopup_BattleSummary.Click += new EventHandler(dataGridMainPopup_BattleSummary_Click);

			ToolStripMenuItem dataGridMainPopup_TankWN8 = new ToolStripMenuItem("WN8 Tank Details")
			{
				Image = imageListToolStrip.Images[10]
			};
			dataGridMainPopup_TankWN8.Click += async (s, e) => await dataGridMainPopup_TankWN8_Click(s, e);

			ToolStripMenuItem dataGridMainPopup_CopyRowToClipboard = new ToolStripMenuItem("Copy Row to Clipboard")
			{
				Image = imageListToolStrip.Images[13]
			};
			dataGridMainPopup_CopyRowToClipboard.Click += new EventHandler(dataGridMainPopup_CopyRowToClipboard_Click);

			ToolStripMenuItem dataGridMainPopup_RecalculateTankCredit = new ToolStripMenuItem("Recalculate Tank Credits");
			dataGridMainPopup_RecalculateTankCredit.Click += new EventHandler(dataGridMainPopup_RecalculateTankCredit_Click);

			ToolStripMenuItem dataGridMainPopup_RecalculateTankRating = new ToolStripMenuItem("Recalculate Tank Ratings");
			dataGridMainPopup_RecalculateTankRating.Click += new EventHandler(dataGridMainPopup_RecalculateTankRating_Click);

			ToolStripMenuItem dataGridMainPopup_RecalculateBattleRating = new ToolStripMenuItem("Recalculate Battle Rating");
			dataGridMainPopup_RecalculateBattleRating.Click += new EventHandler(dataGridMainPopup_RecalculateBattleRating_Click);

			ToolStripMenuItem dataGridMainPopup_Replay = new ToolStripMenuItem("Search for Replay")
			{
				Image = imageListToolStrip.Images[14]
			};
			dataGridMainPopup_Replay.Click += new EventHandler(dataGridMainPopup_Replay_Click);


			// Add events
			dataGridMainPopup.Opening += new CancelEventHandler(dataGridMainPopup_Opening);
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
						dataGridMainPopup_EditTankInfo,
						dataGridMainPopup_TankWN8,
						new ToolStripSeparator(),
						dataGridMainPopup_BattleChart,
						dataGridMainPopup_GrindingSetup,
						dataGridMainPopup_GrindingSetupRecalculate,
						new ToolStripSeparator(),
						dataGridMainPopup_FavListAddTank,
						dataGridMainPopup_FavListRemoveTank,
						dataGridMainPopup_FavListCreateNew,
						new ToolStripSeparator(),
						dataGridMainPopup_RecalculateTankCredit,
						dataGridMainPopup_RecalculateTankRating,
						new ToolStripSeparator(),
						dataGridMainPopup_CopyRowToClipboard
					});
					break;
				case GridView.Views.Battle:
					dataGridMainPopup.Items.AddRange(new ToolStripItem[]
					{
						dataGridMainPopup_BattleDetails,
						dataGridMainPopup_BattleSummary,
						dataGridMainPopup_TankDetails,
						new ToolStripSeparator(),
						dataGridMainPopup_BattleChart,
						dataGridMainPopup_GrindingSetup,
						dataGridMainPopup_Replay,
						new ToolStripSeparator()
					});
					if (MainSettings.GetCurrentGridFilter().TankId == -1)
					{
						dataGridMainPopup.Items.Add(dataGridMainPopup_FilterOnTank);
						dataGridMainPopup.Items.Add(dataGridMainPopup_FilterOnTankSearch);
					}
					else
					{
						dataGridMainPopup.Items.Add(dataGridMainPopup_FilterOnTankClear);
					}
					dataGridMainPopup.Items.AddRange(new ToolStripItem[]
					{
						new ToolStripSeparator(),
						dataGridMainPopup_FavListAddTank,
						dataGridMainPopup_FavListRemoveTank,
						dataGridMainPopup_FavListCreateNew,
						new ToolStripSeparator(),
						dataGridMainPopup_CopyRowToClipboard,
						new ToolStripSeparator(),
						dataGridMainPopup_RecalculateBattleRating,
						dataGridMainPopup_DeleteBattle
					});
					break;
				default:
					break;
			}

			//Assign to datagridview
			dataGridMain.ContextMenuStrip = dataGridMainPopup;
		}

		private async Task AutoSetup()
		{
			// TODO:
			// Autodetect dossier file from default location, save to config
			string dossierFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\wargaming.net\\WorldOfTanks\\dossier_cache";
			if (Directory.Exists(dossierFolder))
			{
				// Autocreate new database
				Config.Settings.dossierFilePath = dossierFolder;
				Form frm = new Forms.DatabaseNew(true);
				frm.ShowDialog(this);
				LoadConfigDataResult.Success = AutoSetupHelper.AutoSetupCompleteOK;
				if (LoadConfigDataResult.Success)
				{
					Config.Settings.dossierFileWathcherRun = 1;
					await Config.SaveConfig();
				}
			}
			else
			{
				LoadConfigDataResult.Message = "Could not locate dossier file path, please select manually from Application Settings.";
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

		#region Check For New Version and Download

		private async Task RunAppStartupAPI(bool manualVersionCheck)
		{
			// Run appstart procedure, webservice to wot numbers website, log app usage + get playerId/token for posting data to website + check for new version
			// Services.AppStartup appStartup = new Services.AppStartup();
			// Services.Models.AppStartupModels.Result appStartupResult = await appStartup.Run(!manualVersionCheck);

			// New method: check for new version at hestad.no web server
			var websiteVersionCheck = Services.AppNewVersion.Check();

			// Not success getting website download settings
			if (!websiteVersionCheck.Success)
			{
				Log.AddToLogBuffer("Unsuccessful check for new version on website. " + websiteVersionCheck.Message);
				await Log.WriteLogBuffer(true);
				// Manual check for new version, or debug mode, show message in message box
				if (manualVersionCheck)
				{
					MsgBox.Show(
						"Unsuccessful check for new version." + Environment.NewLine + Environment.NewLine +
						websiteVersionCheck.Message + Environment.NewLine + Environment.NewLine + Environment.NewLine,
						"Website version check",
						this);
				}
				// Normal startup, continue with startup actions, show notification in status bar
				if (!manualVersionCheck)
				{
					await RunAppStartupActions("Error occured checking website for new version.");
				}
			}
			// Success getting appstart data
			else
			{
				// Check if download is available
				if (!websiteVersionCheck.DownloadSettings.Active)
				{
					// Not available
					Log.AddToLogBuffer("Website providing new version for download is in maintenace mode.");
					Log.AddToLogBuffer("Message returned from website: " + websiteVersionCheck.DownloadSettings.InactiveMessage);
					await Log.WriteLogBuffer(true);
					if (manualVersionCheck)
					{
						Log.AddToLogBuffer("Wot Numbers website download is currently not available. " + websiteVersionCheck.DownloadSettings.InactiveMessage);
						await Log.WriteLogBuffer(true);
						if (Config.Settings.showDBErrors || manualVersionCheck)
							MsgBox.Show(
								"Wot Numbers download is currently not available." + Environment.NewLine + Environment.NewLine +
								websiteVersionCheck.DownloadSettings.InactiveMessage + Environment.NewLine + Environment.NewLine,
								"Website version check", this);
					}
					if (!manualVersionCheck)
					{
						await RunAppStartupActions(websiteVersionCheck.DownloadSettings.InactiveMessage);
					}
				}
				// Download exists
				else
				{
					// Get versions
					double currentVersion = AppVersion.AssemblyVersion.ToVersionNumber();
					double latestVersion = websiteVersionCheck.DownloadSettings.Version.ToVersionNumber();
					// Check if new version found
					bool newVersionAvailable = (latestVersion > currentVersion);
					if (newVersionAvailable)
					{
						// New version found
						Log.AddToLogBuffer($"New version found online: {websiteVersionCheck.DownloadSettings.Version}");
						await Log.WriteLogBuffer(true);
						string msg =
							"New version is available: " + websiteVersionCheck.DownloadSettings.Version + Environment.NewLine + Environment.NewLine +
							"	Do you want to go to the download site?" + Environment.NewLine + Environment.NewLine;

						MsgBox.Button answer = MsgBox.Show(msg, "Website version check", MsgBox.Type.YesNo, this);
						if (answer == MsgBox.Button.Yes)
						{
							Process.Start(Constants.WotNumDownloadUrl());
							this.Close();
							Application.Exit();
						}
					}
					else
					{
						// Not found new version
						Log.AddToLogBuffer("Successfully checked for new version online, current version is latest. Your are ready to go. ");
						await Log.WriteLogBuffer(true);
						if (manualVersionCheck)
						{
							string msg =
								"You are running the latest version: " + AppVersion.AssemblyVersion + Environment.NewLine + Environment.NewLine +
								"Do you want to go to Wot Numbers website download page anyway?" + Environment.NewLine + Environment.NewLine;
							MsgBox.Button answer = MsgBox.Show(msg, "Website version check", MsgBox.Type.YesNo, this);
							if (answer == MsgBox.Button.Yes)
							{
								Process.Start(Constants.WotNumDownloadUrl());
							}
						}
						else
						{
							// Check for triggered actions
							await RunAppStartupActions("You are running the latest version (Wot Numbers " + AppVersion.AssemblyVersion + ")");
						}
					}
				}
			}
			// Enable Settings menues
			mSettingsRun.Enabled = true;
			mSettingsRunBattleCheck.Enabled = true;
			mRecalcTankStatistics.Enabled = true;
			mUpdateDataFromAPI.Enabled = true;
			mRecalcBattleRatings.Enabled = true;
			mRecalcBattleWN9.Enabled = true;
			mRecalcBattleWN8.Enabled = true;
			mRecalcBattleWN7.Enabled = true;
			mRecalcBattleEFF.Enabled = true;
			mRecalcBattlePos.Enabled = true;
			mRecalcBattleAllRatings.Enabled = true;
			mRecalcBattleCreditsPerTank.Enabled = true;
			mRecalcBattleStats.Enabled = true;
			mRecalcBattleMinTier.Enabled = true;
			mRecalcBattleMaxTier.Enabled = true;
			mAppSettings.Enabled = true;
		}

		private async Task RunAppStartupActions(string message)
		{
			// Debug option - avoid init dossier file check after startup
			// if (false)
			{
				if (DBVersion.RunDownloadAndUpdateTanks)
					await RunWotApi(true);
				if (DBVersion.RunRecalcBattleWN8 || DBVersion.RunRecalcBattleWN9 || DBVersion.RunRecalcBattlePos)
					await RunRecalcBattleRating(true, DBVersion.RunRecalcBattleWN8, DBVersion.RunRecalcBattleWN9, DBVersion.RunRecalcBattlePos);
				if (DBVersion.RunRecalcBattleCreditPerTank)
					await RunRecalcBattleCreditsPerTank(true);
				if (DBVersion.RunRecalcBattleKDratioCRdmg)
					await RunRecalcBattleKDratioCRdmg(true);
				if (DBVersion.RunRecalcBattleMinTier)
					await RunRecalcBattleMinTier();
				if (DBVersion.RunRecalcBattleMaxTier)
					await RunRecalcBattleMaxTier();

				// Check for dossier update
				StatusBarHelper.Message = message;
				if (Config.Settings.dossierFileWathcherRun == 1 || DBVersion.RunDossierFileCheckWithForceUpdate)
				{
					string msg = "Running initial battle fetch...";
					if (DBVersion.RunDossierFileCheckWithForceUpdate)
						msg = "Running initial battle fetch with force update all data...";
					await RunDossierFileCheck(msg, DBVersion.RunDossierFileCheckWithForceUpdate);
				}

				// Discontinued
				// Upload battles to website
				//string result = await new Services.AppBattleUpload().Run(DBVersion.RunUploadAllToWotNumWeb);
				//if (DBVersion.RunUploadAllToWotNumWeb)
				//    await Log.LogToFile($" > > Battle upload to Wot Numbers website for all battles done. Status: {result}");
				//else
				//    await Log.LogToFile($" > > Battle upload to Wot Numbers website for new battles done. status: {result}");

			}
		}

		#endregion

		#region Common Events

		private int status2DefaultColor = 200;
		private int status2fadeColor = 200;

		public async Task GridViewRefresh(string message = null)
		{
			if (!GridView.refreshRunning)
			{
				GridView.refreshRunning = true;
				if (message == null)
					message = "View refreshed";
				await ShowView(message);
				if (notifyIcon.Visible)
					notifyIcon.ShowBalloonTip(1000);
				GridView.refreshRunning = false;
			}
		}

		// TODO - Remove
		private async void NewBattleFileChanged(object source, FileSystemEventArgs e)
		{
			await GridViewRefresh();
		}

		[DebuggerNonUserCode]
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
				}
				else
				{
					timerStatus2.Enabled = false;
					lblStatus2.Text = "";
					if (StatusBarHelper.MessageExists)
					{
						SetStatus2();
						StatusBarHelper.CheckForClear();
					}
				}
			}
		}

		private bool Status2AutoEnabled = true;
		public void SetStatus2(string txt = "")
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
					timerStatus2.Interval = 3000;
					lblStatus2.ForeColor = Color.FromArgb(255, status2DefaultColor, status2DefaultColor, status2DefaultColor); // White color, not faded
					lblStatus2.Text = msg;
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

		private async Task SetListener(bool showStatus2Message = true)
		{
			mSettingsRun.Checked = (Config.Settings.dossierFileWathcherRun == 1);
			if (Config.Settings.dossierFileWathcherRun == 1)
			{
				lblStatus1.Text = "Running";
				notifyIcon.Text = "Wot Numbers - Running";
				lblStatus1.ForeColor = Color.ForestGreen;
			}
			else
			{
				lblStatus1.Text = "Stopped";
				notifyIcon.Text = "Wot Numbers - Stopped";
				lblStatus1.ForeColor = Color.DarkRed;

			}
			string result = await UpdateDossierAndBattleFileWatcher();
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

		private async void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Check for running backup if the database backup if SQLite db, period is greater than zero and file path added
			if (Config.Settings.databaseBackupPeriod > 0 &&
				Config.Settings.databaseType == ConfigData.dbType.SQLite &&
				Config.Settings.databaseBackupFilePath.Trim().Length > 0
			)
			{
                // If a database backup has not occured yet, set the last backup date to be 10 days
                // before today so that the backup is guaranteed to happen (because max period is 7 days).
                DateTime lastBackup = Config.Settings.databaseBackupLastPerformed ?? (DateTime.Now.AddDays(-10));
				if (lastBackup.AddDays(Config.Settings.databaseBackupPeriod).CompareTo(DateTime.Now) < 0)
				{
					Form frm = new DatabaseBackup(true);
					frm.ShowDialog(this);
				}
			}
			// Hide systray icon
			notifyIcon.Visible = false;
			// Save config to save current screen pos and size
			Config.Settings.posSize.WindowState = this.WindowState;
			await Config.SaveConfig();
			// Log exit
			Log.AddToLogBuffer("// Application Exit", true);
			await Log.WriteLogBuffer();
		}

		private void Main_Resize(object sender, EventArgs e)
		{
			if (!Init)
			{
				bool formNormalOrMax = this.WindowState != FormWindowState.Minimized;
				if (formNormalOrMax)
				{
					// Form visible, perform normal resize event
					ResizeNow();
					mainFormWindowsState = this.WindowState;
					// Remember new size for saving on form close
					if (this.WindowState == FormWindowState.Normal)
					{
						Config.Settings.posSize.Width = this.Width;
						Config.Settings.posSize.Height = this.Height;
					}
					this.Opacity = 100;
				}
				else
				{
					// Form minimized, hide
					this.Opacity = 0;
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
				int gridAreaTop = 0; // Start below info panel
				int gridAreaHeight = panelMainArea.Height; // Grid height
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

		private async void toolItemRefresh_Click(object sender, EventArgs e)
		{
			if (!GridView.refreshRunning)
			{
				GridView.refreshRunning = true;
				SetFormTitle();
				SetStatus2("Refreshing view...");
				await ShowView("View refreshed");
				GridView.refreshRunning = false;
			}
		}

		private async void mViewOverall_Click(object sender, EventArgs e)
		{
			await ChangeView(GridView.Views.Overall);
		}

		private async void mViewTankInfo_Click(object sender, EventArgs e)
		{
			await ChangeView(GridView.Views.Tank);
		}

		private async void mViewBattles_Click(object sender, EventArgs e)
		{
			await ChangeView(GridView.Views.Battle);
		}

		private async void mViewMaps_Click(object sender, EventArgs e)
		{
			await ChangeView(GridView.Views.Map);
		}

		private async Task ChangeView(GridView.Views newGridView, bool forceUpdate = false)
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
					case GridView.Views.Map:
						mViewMaps.Checked = false;
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
				if (mHomeViewEditMode.Checked)
				{
					mHomeViewEditMode.Checked = false;
					await GadgetEditModeChange();
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
						lblStatusRowCount.Visible = false;
						// Show/Hide Tool Items
						mBattles.Visible = true;
						mHomeView.Visible = true;
						mHomeViewEdit.Visible = true;
						mBattles.Visible = false;
						mMapViewType.Visible = false;
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
						lblStatusRowCount.Visible = true;
						// Show/Hide Tool Items
						mBattles.Visible = false;
						mMapViewType.Visible = false;
						mTankFilter.Visible = true;
						mColumnSelect.Visible = true;
						mMode.Visible = true;
						mModeClan.Visible = false;
						mModeCompany.Visible = false;
						mModeRandom.Visible = false;
						mModeRandomTankCompany.Visible = true;
						mModeRandomSoloPlatoon.Visible = false;
						toolStripSeparatorForBattleView.Visible = false;
						mHomeView.Visible = false;
						mHomeViewEdit.Visible = false;
						mBattleGroup.Visible = false;
						mRefreshSeparator.Visible = true;
						mColumnSelect_Edit.Text = "Edit Tank View...";
						mColumnSelect.ToolTipText = "Select Tank View";
						// Get Column Setup List - also finds correct tank filter/fav list
						await SetColListMenu();
						// Get Battle mode
						SetBattleModeMenu();
						// Add datagrid context menu (right click on datagrid)
						CreateDataGridContextMenu();
						// Info slider hide
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
						lblStatusRowCount.Visible = true;
						// Show/Hide Tool Items
						mBattles.Visible = false;
						mMapViewType.Visible = false;
						mTankFilter.Visible = true;
						mColumnSelect.Visible = true;
						mMode.Visible = true;
						mModeClan.Visible = true;
						mModeCompany.Visible = true;
						mModeRandom.Visible = true;
						mModeRandomTankCompany.Visible = false;
						mModeRandomSoloPlatoon.Visible = true;
						toolStripSeparatorForBattleView.Visible = true;
						mBattleGroup.Visible = true;
						mHomeView.Visible = false;
						mHomeViewEdit.Visible = false;
						mRefreshSeparator.Visible = true;
						mColumnSelect_Edit.Text = "Edit Battle View...";
						mColumnSelect.ToolTipText = "Select Battle View";
						// Get Column Setup List  - also finds correct tank filter/fav list
						await SetColListMenu();
						// Get Battle mode
						SetBattleModeMenu();
						// Add datagrid context menu (right click on datagrid)
						CreateDataGridContextMenu();
						// Default control
						this.ActiveControl = dataGridMain;
						break;
					case GridView.Views.Map:
						// Select view
						mViewMaps.Checked = true;
						// Show grid
						dataGridMain.Visible = true;
						scrollX.Visible = true;
						scrollY.Visible = true;
						scrollCorner.Visible = true;
						dataGridMain.RowHeadersWidth = Config.Settings.mainGridBattleRowWidht;
						lblStatusRowCount.Visible = true;
						// Show/Hide Tool Items
						mMapViewType.Visible = true;
						mBattles.Visible = false;
						mTankFilter.Visible = true;
						mColumnSelect.Visible = false;
						mMode.Visible = true;
						mModeClan.Visible = true;
						mModeCompany.Visible = true;
						mModeRandom.Visible = true;
						mModeRandomTankCompany.Visible = false;
						mModeRandomSoloPlatoon.Visible = true;
						toolStripSeparatorForBattleView.Visible = true;
						mBattleGroup.Visible = false;
						mHomeView.Visible = false;
						mHomeViewEditMode.Visible = false;
						mRefreshSeparator.Visible = true;
						mColumnSelect_Edit.Text = "Edit Map View...";
						mColumnSelect.ToolTipText = "Select Map View";
						// Get Column Setup List  - also finds correct tank filter/fav list
						await SetColListMenu();
						// Get Battle mode
						SetBattleModeMenu();
						// Add datagrid context menu (right click on datagrid)
						CreateDataGridContextMenu();
						// Default control
						this.ActiveControl = dataGridMain;
						break;
				}
				toolMain.Renderer = new StripRenderer();
				await ShowView(); // Changed view, no status message applied, sets in GridShow
			}
		}

		private bool homeViewCreated = false;
		private async Task ShowView(string Status2Message = "", bool ShowDefaultStatus2Message = true)
		{
			try
			{
				if (Config.Settings.playerId == 0)
				{
					SetStatus2("No player selected, please check application settings");
					panelMainArea.Visible = false;
				}
				else
				{
					if (panelMainArea.Visible != true)
						panelMainArea.Visible = true;
					if (currentPlayerId != Config.Settings.playerId)
					{
						// Stop file watchers if running
						int runState = Config.Settings.dossierFileWathcherRun;
						if (runState == 1)
						{
							Config.Settings.dossierFileWathcherRun = 0;
							await SetListener();
						}
						currentPlayerId = Config.Settings.playerId;
						SetFormTitle();
						MsgBox.Show($"Current player is changed to {Config.Settings.playerNameAndServer} because new player data is fetched." + Environment.NewLine + Environment.NewLine, "Current player changed", this);
						// Return to prev file watcher state
						if (runState != Config.Settings.dossierFileWathcherRun)
						{
							Config.Settings.dossierFileWathcherRun = runState;
							await SetListener();
						}
					}
					switch (MainSettings.View)
					{
						case GridView.Views.Overall:
							// New home view
							if (!homeViewCreated)
							{
								await HomeViewCreate("Creating Home View...");
								homeViewCreated = true;
							}
							if (Status2Message == "" && ShowDefaultStatus2Message) Status2Message = "Home view selected";
							await HomeViewRefresh(Status2Message);
							break;
						case GridView.Views.Tank:
							if (Status2Message == "" && ShowDefaultStatus2Message) Status2Message = "Tank view selected";
							await GridShowTank(Status2Message);
							break;
						case GridView.Views.Battle:
							if (Status2Message == "" && ShowDefaultStatus2Message) Status2Message = "Battle view selected";
							await GridShowBattle(Status2Message);
							break;
						case GridView.Views.Map:
							if (Status2Message == "" && ShowDefaultStatus2Message) Status2Message = "Map view selected";
							await GridShowMap(Status2Message);
							break;
						default:
							break;
					}
				}
			}
			catch (Exception ex)
			{
				await Log.LogToFile(ex);
				if (Config.Settings.showDBErrors)
					MsgBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + ex.InnerException, "Error initializing view", this);
			}

		}

		#endregion

		#region Dossier and Battle File Listeners

		public async Task<string> UpdateDossierAndBattleFileWatcher()
		{
			string logText = "Automatically fetch new battles stopped";
			bool run = (Config.Settings.dossierFileWathcherRun == 1);
			if (run)
			{
				try
				{
					logText = "Automatically fetch new battles started";
					SetStatus2(logText);
					// Dossier
					fswDossier.Path = Path.GetDirectoryName(Config.Settings.dossierFilePath + "\\");
					fswDossier.Filter = "*.dat";
					fswDossier.NotifyFilter = NotifyFilters.LastWrite;
					fswDossier.EnableRaisingEvents = true;
					// Battle
					fswBattle.Path = Path.GetDirectoryName(Config.Settings.battleFilePath);
					fswBattle.Filter = "*.dat";
					fswBattle.IncludeSubdirectories = true;
					fswBattle.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
					fswBattle.EnableRaisingEvents = true;
				}
				catch (Exception ex)
				{
					await Log.LogToFile(ex, "Error on: " + logText);
					MsgBox.Show(
						"Error activation dossier or battle listners, please check your application settings." + Environment.NewLine + Environment.NewLine +
						ex.Message,
						"Error in dossier file path",
						this);
					run = false;
				}
			}
			// Set active / deactive now
			fswDossier.EnableRaisingEvents = run;
			fswBattle.EnableRaisingEvents = run;
			await Log.LogToFile("// " + logText, true);
			return logText;
		}

		private async void fswDossier_Changed(object sender, FileSystemEventArgs e)
		{
			// Stop listening to dossier file
			fswDossier.EnableRaisingEvents = false;
			// Logging
			await Log.CheckLogFileSize();
			await Log.LogToFile("// Dossier file listener detected updated dossier file");
			// Dossier file automatic handling
			// Get config data
			string dossierFile = e.FullPath;
			FileInfo file = new FileInfo(dossierFile);
			// Wait until file is ready to read, 
			bool fileOk = await Dossier2json.WaitUntilFileReadyToRead(dossierFile, 4);
			// Perform file conversion from picle to json
			if (fileOk)
			{
				var result = await Dossier2json.RunDossierRead(dossierFile);
				// Refresh and display result
				if (result.Success && result.NewBattlesCount > 0)
					await GridViewRefresh(result.Message);
				else
					SetStatus2(result.Message);
				// Check for recalc grinding progress
				await GrindingHelper.CheckForDailyRecalculateGrindingProgress();
			}
			// Continue listening to dossier file
			fswDossier.EnableRaisingEvents = true;
		}

		private async void fswBattle_Created(object sender, FileSystemEventArgs e)
		{
			if (!Dossier2db.Running)
			{
				fswBattle.EnableRaisingEvents = false;
				await Log.LogToFile("// Battle file listener detected new battle file");
				int battlesUpdated = await Battle2json.RunBattleResultRead();
				// Refresh and display result
				if (battlesUpdated > 0)
					await GridViewRefresh("Battle files updated");
				fswBattle.EnableRaisingEvents = true;
			}
			else
				await Log.LogToFile("// New battle file detected, reading is terminated due to dossier file process is running");
		}





		#endregion

		#region Menu Items: Col List

		private async Task SetColListMenu()
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
			DataTable dt = await DB.FetchData(sql, Config.Settings.showDBErrors);
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
					if (menuItemNum > 15) break;
				}
			}
			SelectFavMenuItem();
		}

		private async void toolItemColumnSelect_Click(object sender, EventArgs e)
		{
			// Selected a colList from toolbar
			ToolStripMenuItem selectedMenu = (ToolStripMenuItem)sender;
			// Get colListId for the selected colList
			int newColListId = await ColListHelper.GetColListId(selectedMenu.Text);
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
				GridFilter.Settings newColListSettings = await ColListHelper.GetSettingsForColList(newColListId);
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
				CreateDataGridContextMenu(); // Recreate context menu
				SelectFavMenuItem();
				// Show grid
				await ShowView("Selected column setup: " + selectedMenu.Text);
			}
		}

		private void toolItemColumnSelect_Edit_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.ColList(this);
			frm.ShowDialog();
		}

		public async Task ReturnFromColListFrom()
		{
			await SetColListMenu(); // Refresh column setup list now
			await ShowView("Refreshed grid after column setup change"); // Refresh grid now
		}

		#endregion

		#region Menu Items: Map View Alternatives

		private async void mMapViewType_Click(object sender, EventArgs e)
		{
			// Selected a colList from toolbar
			ToolStripMenuItem selectedMenu = (ToolStripMenuItem)sender;
			// if not already selcted change
			if (!selectedMenu.Checked)
			{
				// uncheck
				mMapDefault.Checked = false;
				mMapDescr.Checked = false;
				mMapDescrLarge.Checked = false;
				// select and set main menu title
				selectedMenu.Checked = true;
				mMapViewType.Text = selectedMenu.Text;
				// Reset sorting
				mapSorting.ColumnName = "Map";
				mapSorting.ColumnHeader = "Map";
				mapSorting.SortDirectionAsc = true;
				// Show grid
				await ShowView("Selected map view: " + selectedMenu.Text);
			}

		}

		private async void mMapShowAll_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem selectedMenu = (ToolStripMenuItem)sender;
			// Toggle
			selectedMenu.Checked = !selectedMenu.Checked;
			string s = "only maps with recorded battles";
			if (selectedMenu.Checked)
				s = "maps without recorded battles";
			await ShowView("Changed map view to show " + s);
		}

		private async void mMapShowOld_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem selectedMenu = (ToolStripMenuItem)sender;
			// Toggle
			selectedMenu.Checked = !selectedMenu.Checked;
			string s = "only maps currently in play";
			if (selectedMenu.Checked)
				s = "maps that are old / obsolete and no longer in play";
			await ShowView("Changed map view to show " + s);
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

		private async Task SetFavListMenu()
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
			DataTable dt = await DB.FetchData(sql, Config.Settings.showDBErrors);
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
			try
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
			catch (Exception)
			{

			}

		}

		private async void toolItemTankFilter_All_Click(object sender, EventArgs e)
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
			await ShowView("Selected all tanks owned");
		}

		private async void mTankFilter_All_NotOwned_Click(object sender, EventArgs e)
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
			await ShowView("Selected all tanks");
		}

		private async void toolItem_Fav_Clicked(object sender, EventArgs e)
		{
			// Selected favList from toolbar
			ToolStripMenuItem selectedMenu = (ToolStripMenuItem)sender;
			// Get favListId for selected favList
			int newFavListId = await FavListHelper.GetId(selectedMenu.Text);
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
			await ShowView("Selected favourite tank list: " + selectedMenu.Text);
		}

		private void SelectFavMenuItem()
		{
			// Programatically select all tanks or favourite tank list
			switch (MainSettings.GetCurrentGridFilter().FavListShow)
			{
				case GridFilter.FavListShowType.UseCurrent:
					// No action, use previous selected tanks filter
					SetTankFilterMenuName();
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
				mTankFilter_CountryCzechoslovakia.Checked = false;
				mTankFilter_CountrySweden.Checked = false;
				mTankFilter_CountryPoland.Checked = false;
				mTankFilter_CountryItaly.Checked = false;
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
			if (mTankFilter_CountryCzechoslovakia.Checked) tankFilterNation++;
			if (mTankFilter_CountrySweden.Checked) tankFilterNation++;
			if (mTankFilter_CountryPoland.Checked) tankFilterNation++;
			if (mTankFilter_CountryItaly.Checked) tankFilterNation++;

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

		private async Task TankFilterMenuSelect(ToolStripMenuItem menuItem, ToolStripMenuItem parentMenuItem)
		{
			string status2message = "";
			// Update selected tankfilter type
			GridFilter.Settings gf = MainSettings.GetCurrentGridFilter();
			gf.TankId = -1; // Remove tank filter
			MainSettings.UpdateCurrentGridFilter(gf);
			// Get Tank filter
			Tankfilter(out string whereSQL, out string tankJoin, out string tankFilterMessage);
			SetTankFilterMenuName();
			status2message = "Selected tank filter: " + tankFilterMessage;
			mTankFilter.ShowDropDown();
			parentMenuItem.ShowDropDown();
			// Done
			await ShowView(status2message);
		}

		private async void mTankFilter_Clear_Click(object sender, EventArgs e)
		{
			TankFilterMenuUncheck(true, true, true, false);
			// Update selected tankfilter 
			GridFilter.Settings gf = MainSettings.GetCurrentGridFilter();
			gf.TankId = -1; // Remove tank filter
			MainSettings.UpdateCurrentGridFilter(gf);
			// Done
			await ShowView("Tank filter cleared");
			CreateDataGridContextMenu(); // Recreate context menu
		}

		private async void toolItemTankFilter_Tier_Click(object sender, EventArgs e)
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
			await TankFilterMenuSelect(menuItem, mTankFilter_Tier);
		}

		private async void toolItemTankFilter_Type_Click(object sender, EventArgs e)
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
			await TankFilterMenuSelect(menuItem, mTankFilter_Type);
		}

		private async void toolItemTankFilter_Country_Click(object sender, EventArgs e)
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
			await TankFilterMenuSelect(menuItem, mTankFilter_Country);
		}

		private async void toolItemTankFilter_Country_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				TankFilterMenuUncheck(false, true, false, false);
				ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
				await TankFilterMenuSelect(menuItem, mTankFilter_Country);
			}
		}

		private async void toolItemTankFilter_Type_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				TankFilterMenuUncheck(false, false, true, false);
				ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
				await TankFilterMenuSelect(menuItem, mTankFilter_Type);
			}
		}

		private async void toolItemTankFilter_Tier_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				TankFilterMenuUncheck(true, false, false, false);
				ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
				await TankFilterMenuSelect(menuItem, mTankFilter_Tier);
			}
		}

		private void toolItemTankFilter_MouseDown(object sender, MouseEventArgs e)
		{
			// On right mouse click just display status message for current filter
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				Tankfilter(out string where, out string join, out string message);
				SetStatus2("Current selected tank filter: " + message);
			}
		}

		private async void toolItemTankFilter_EditFavList_Click(object sender, EventArgs e)
		{
			// Show fal list editor
			Form frm = new Forms.FavList();
			frm.ShowDialog(this);
			// After fav list changes reload menu
			await SetFavListMenu(); // Reload fav list items
			await ShowView("Refreshed grid after fovourite tank list change"); // Refresh grid now
		}

		#endregion

		#region Menu Items: Home View / Recent List

		private async Task GetHomeViewRecentList()
		{
			string sql = "SELECT * FROM homeViewRecent ORDER BY id DESC;";
			DataTable dt = await DB.FetchData(sql);
			int recentItemsCount = dt.Rows.Count;
			mHomeViewRecent1.Visible = (recentItemsCount > 0);
			mHomeViewRecent2.Visible = (recentItemsCount > 1);
			mHomeViewRecent3.Visible = (recentItemsCount > 2);
			mHomeViewRecent4.Visible = (recentItemsCount > 3);
			mHomeViewRecent5.Visible = (recentItemsCount > 4);
			mHomeViewRecentSeparator.Visible = (recentItemsCount > 0);
			if (recentItemsCount > 0)
			{
				int i = 1;
				foreach (DataRow dr in dt.Rows)
				{
					if ((ToolStripMenuItem)mHomeView.DropDownItems["mHomeViewRecent" + i.ToString()] is ToolStripMenuItem m)
					{
						string fileName = dr["fileName"].ToString();
						string folder = dr["folder"].ToString();
						m.Text = fileName.Replace(".json", ""); // remove file extension for menu name
						m.Tag = folder + "\\" + fileName;
						i++;
					}
				}
			}
		}

		#endregion

		#region Menu Items: Battle Count Filter
		private void mBattlesCountSelect_Click(object sender, EventArgs e)
		{
			// Set new battle count filter as selected
			ToolStripMenuItem menu = (ToolStripMenuItem)sender;
			mBattlesCountSelected.Text = menu.Text;
			mBattlesCountSelected.Tag = menu.Tag;
			mBattlesCountSelected.Visible = true;
			// Select the new battle count filter
			BattleCountSelectMenu();
		}

		private void mBattlesCountSelected_Click(object sender, EventArgs e)
		{
			// Select current battle count filter
			BattleCountSelectMenu();
		}

		private async void BattleCountSelectMenu()
		{
			BattleTimeMenuReset();
			mBattlesCountSelected.Checked = true;
			mBattles.Text = mBattlesCountSelected.Text;
			await ShowView("Selected battle count filter: " + mBattlesCountSelected.Text);

		}

		private async void mBattlesCountSelectEdit_Click(object sender, EventArgs e)
		{
			string id = mBattlesCountSelected.Tag.ToString();
			if (id == "0")
			{
				MsgBox.Show("Please select a battle count filter first.");
				return;
			}
			DataRow dr = await BattleCountFilterHelper.Get(id);
			string count = dr["count"].ToString();
			InputBox.ResultClass answer = InputBox.Show("Select battle count:", "Edit Battle Count Filter", count, this);
			if (answer.Button == InputBox.InputButton.OK)
			{
				if (Int32.TryParse(answer.InputText, out int newCount))
				{
					await BattleCountFilterHelper.Save(id, newCount);
					await BattleCountFilterSet(id);
					await ShowView("Edited battle count filter: " + mBattlesCountSelected.Text);
				}
				else
				{
					MsgBox.Show("Illegal numeric value");
				}
			}
		}

		private async Task BattleCountFilterSet(string id = "0")
		{
			DataTable dt = await BattleCountFilterHelper.Get();
			int menuId = 0;
			foreach (DataRow dr in dt.Rows)
			{
				menuId++;
				ToolStripItem[] menuItemFind = mBattlesCountSelect.DropDownItems.Find("mBattlesCountSelect" + menuId.ToString(), true);
				ToolStripMenuItem menu = (ToolStripMenuItem)menuItemFind[0];
				menu.Text = "Last " + dr["count"].ToString() + " Battles";
				menu.Tag = dr["id"].ToString();
				if (id == menu.Tag.ToString())
				{
					mBattlesCountSelected.Text = menu.Text;
					mBattlesCountSelected.Tag = menu.Tag;
					BattleCountSelectMenu();
				}
			}
		}

		private void BattleCountMenuReset()
		{
			mBattlesCountSelected.Checked = false;
		}

		#endregion

		#region Menu Items: Battle Grouping

		private async void toolItemGroupingSelected_Click(object sender, EventArgs e)
		{
			mBattleGroup_No.Checked = false;
			mBattleGroup_TankAverage.Checked = false;
			mBattleGroup_TankSum.Checked = false;
			ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
			menuItem.Checked = true;
			mBattleGroup.Text = menuItem.Text;
			await GridShowBattle("Selected grouping: " + menuItem.Text);
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
			SelectBattleTimeMenu(mBattlesCustomUse);
		}

		private void ShowCustomBattleTimeFilter()
		{
			Form frm = new Forms.BattleTimeFilterCustom();
			frm.ShowDialog(this);
		}

		private void mBattles_MouseDown(object sender, MouseEventArgs e)
		{
			// On right mouse click just display status message for current filter
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				string message = mBattles.Text; // Selected menu item
												// Get Battle Time filer
				BattleTimeAndCountFilter(out string battleTimeFilter, out string battleTimeReadable, out bool battleCountFilter);
				// Show info
				SetStatus2("Current battle time filter: " + message + battleTimeReadable);
			}
		}

		private async void SelectBattleTimeMenu(ToolStripMenuItem menuItem)
		{
			BattleTimeMenuReset();
			BattleCountMenuReset();
			menuItem.Checked = true;
			if (menuItem == mBattlesCustomUse)
				mBattles.Text = menuItem.Tag.ToString(); // Different name on main tool item then menu name for battle time filter button
			else
				mBattles.Text = menuItem.Text; // Use menu name as main tool item name for battle time filter button
			await ShowView("Selected battle time: " + menuItem.Text);
		}

		private void BattleTimeMenuReset()
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
		}

		#endregion

		#region Menu Items: Battle Mode

		private async void toolItemMode_Click(object sender, EventArgs e)
		{
			// Selected battle mode from toolbar
			ToolStripMenuItem selectedMenu = (ToolStripMenuItem)sender;
			// Get new battle type for selected favList
			GridFilter.BattleModeType selectedMode = (GridFilter.BattleModeType)Enum.Parse(typeof(GridFilter.BattleModeType), selectedMenu.Tag.ToString());
			// Changed gridFilter
			GridFilter.Settings gf = MainSettings.GetCurrentGridFilter();
			gf.BattleMode = selectedMode;
			//if (gf.FavListShow == GridFilter.FavListShowType.AllTanksNotOwned)
			//{
			//	gf.FavListShow = GridFilter.FavListShowType.MyTanks;
			//	SetFavListMenu();
			//}
			MainSettings.UpdateCurrentGridFilter(gf);
			// Remove current menu checked
			foreach (var dropDownItem in mMode.DropDownItems)
			{
				if (dropDownItem is ToolStripMenuItem menuItem)
				{
					menuItem.Checked = false;
				}
			}
			foreach (var dropDownItem in mModeRandomSoloPlatoon.DropDownItems)
			{
				if (dropDownItem is ToolStripMenuItem menuItem)
				{
					menuItem.Checked = false;
				}
			}

			// check battle mode list menu select
			selectedMenu.Checked = true;
			string mainToolItemMenuText = selectedMenu.Text;

			mMode.Text = mainToolItemMenuText;
			// Set menu item and show grid
			await ShowView("Selected battle mode: " + selectedMenu.Text);
		}

		private void SetBattleModeMenu()
		{
			bool done = false;
			foreach (var dropDownItem in mMode.DropDownItems)
			{
				string battleMode = MainSettings.GetCurrentGridFilter().BattleMode.ToString();
				if (dropDownItem is ToolStripMenuItem menuItem)
				{
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
					if (dropDownItem is ToolStripMenuItem menuItem)
					{
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

		#region Menu Item: Admin Tools

		private void mAdminToolsUploadBattlesNew_Click(object sender, EventArgs e)
		{
			//Services.AppBattleUpload appBattleUpload = new Services.AppBattleUpload();
			//string result = await appBattleUpload.Run(false);
			//MsgBox.Show(result, "Upload new battles to Wot Numbers website", this);
			MsgBox.Show("Message", "This feature is discontinued", this);
		}

		private void mAdminToolsUploadBattlesAll_Click(object sender, EventArgs e)
		{
			//MsgBox.Button answer = MsgBox.Show(
			//    "This operation will transfer all battles even if they have been transferred previously, it might take a while. Are you sure you want to run the job?" + Environment.NewLine + Environment.NewLine,
			//    "Warning", MsgBox.Type.YesNo, this);
			//if (answer == MsgBox.Button.Yes)
			//{
			//    Services.AppBattleUpload appBattleUpload = new Services.AppBattleUpload();
			//    string result = await appBattleUpload.Run(true);
			//    MsgBox.Show(result, "Upload all battles to Wot Numbers website", this);
			//}
			MsgBox.Show("Message", "This feature is discontinued", this);
		}

		#endregion


		#region Filters 

		private int tankFilterNation = 0;
		private int tankFilterType = 0;
		private int tankFilterTier = 0;

		private void Tankfilter(out string whereSQL, out string joinSQL, out string status2Message, bool onlyTankFilter = false, bool onlyPlayerTankFilter = false)
		{
			string tier = "";
			string nation = "";
			string nationId = "";
			string type = "";
			string typeId = "";
			string message = "";
			string newJoinSQL = "";
			string tankWhereSQL = "";
			string playerTankWhereSQL_Owned = "";
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
				else if (MainSettings.GetCurrentGridFilter().FavListShow == GridFilter.FavListShowType.MyTanks)
				{
					message = "All tanks owned";
					if (!onlyTankFilter)
						playerTankWhereSQL_Owned += " AND playerTank.id is not null ";
				}
			}
			// Check if spesific tank is filtered
			if (MainSettings.GetCurrentGridFilter().TankId != -1)
			{
				int tankId = MainSettings.GetCurrentGridFilter().TankId;
				string tankName = TankHelper.GetTankName(tankId);
				tankFilterManualFilter = tankName;
				message += " - Filtered on tank: " + tankName;
				tankWhereSQL = " AND tank.id=@tankId ";
				DB.AddWithValue(ref tankWhereSQL, "@tankId", tankId, DB.SqlDataType.Int);
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
				if (mTankFilter_CountryCzechoslovakia.Checked) { nation += "Czechoslovakia,"; nationId += "7,"; manualFilterCount++; }
				if (mTankFilter_CountrySweden.Checked) { nation += "Sweden,"; nationId += "8,"; manualFilterCount++; }
				if (mTankFilter_CountryPoland.Checked) { nation += "Poland,"; nationId += "9,"; manualFilterCount++; }
				if (mTankFilter_CountryItaly.Checked) { nation += "Italy,"; nationId += "10,"; manualFilterCount++; }

				if (mTankFilter_TypeLT.Checked) { type += "Light,"; typeId += "1,"; manualFilterCount++; }
				if (mTankFilter_TypeMT.Checked) { type += "Medium,"; typeId += "2,"; manualFilterCount++; }
				if (mTankFilter_TypeHT.Checked) { type += "Heavy,"; typeId += "3,"; manualFilterCount++; }
				if (mTankFilter_TypeTD.Checked) { type += "TD,"; typeId += "4,"; manualFilterCount++; }
				if (mTankFilter_TypeSPG.Checked) { type += "SPG,"; typeId += "5,"; manualFilterCount++; }
				// create tank filter
				if (tier.Length > 0)
				{
					string tierId = tier;
					tier = tier.Substring(0, tier.Length - 1);
					if (tankWhereSQL != "") tankWhereSQL += " AND ";
					tankWhereSQL += " tank.tier IN (" + tierId.Substring(0, tierId.Length - 1) + ") ";
				}
				if (nation.Length > 0)
				{
					nation = nation.Substring(0, nation.Length - 1);
					if (tankWhereSQL != "") tankWhereSQL += " AND ";
					tankWhereSQL += " tank.countryId IN (" + nationId.Substring(0, nationId.Length - 1) + ") ";
				}
				if (type.Length > 0)
				{
					type = type.Substring(0, type.Length - 1);
					if (tankWhereSQL != "") tankWhereSQL += " AND ";
					tankWhereSQL += " tank.tankTypeId IN (" + typeId.Substring(0, typeId.Length - 1) + ") ";
				}
				if (tankWhereSQL != "") tankWhereSQL = " AND (" + tankWhereSQL + ") ";
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
			// Return correct where sql part
			if (onlyPlayerTankFilter)
				whereSQL = playerTankWhereSQL_Owned;
			else if (onlyTankFilter)
				whereSQL = tankWhereSQL;
			else
				whereSQL = playerTankWhereSQL_Owned + tankWhereSQL;
			joinSQL = newJoinSQL;
			status2Message = message;
		}

		private void BattleModeFilter(out string battleModeFilter, out string battleMode)
		{
			battleModeFilter = "";
			battleMode = "%";
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
					case GridFilter.BattleModeType.TeamRanked:
						battleModeFilter = " AND (battleMode = '7Ranked') ";
						battleMode = "7Ranked";
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
					case GridFilter.BattleModeType.Stronghold:
						battleModeFilter = " AND (battleMode = 'Stronghold') ";
						battleMode = "Stronghold";
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
					case GridFilter.BattleModeType.Special:
						battleModeFilter = " AND (battleMode = 'Special') ";
						battleMode = "Special";
						break;
					case GridFilter.BattleModeType.GlobalMap:
						battleModeFilter = " AND (battleMode = 'GlobalMap') ";
						battleMode = "GlobalMap";
						break;
					case GridFilter.BattleModeType.Grand:
						battleModeFilter = " AND (battleMode = 'Grand') ";
						battleMode = "Grand";
						break;
				}
			}
		}

		private void BattleTimeAndCountFilter(out string battleTimeFilter, out string battleTimeReadable, out bool battleCountFilter)
		{
			battleTimeFilter = "";
			battleTimeReadable = "";
			battleCountFilter = false;
			// Check filters
			if (mBattlesAll.Checked)
			{
				// All battles, no filter at all
				return;
			}
			else if (mBattlesCountSelected.Checked)
			{
				// Battle count filter
				battleCountFilter = true;
			}
			else
			{
                // Battle time filter
                DateTime dateFilter = new DateTime();
				if (!mBattlesCustomUse.Checked)
				{
					// Normal predefined battle time filters
					battleTimeFilter = " AND battleTime>=@battleTime ";
					battleTimeReadable = "@battleTime ->";
					dateFilter = DateTimeHelper.GetTodayDateTimeStart();
					// Adjust time scale according to selected filter
					if (mBattles3d.Checked) dateFilter = dateFilter.AddDays(-2);
					else if (mBattles2d.Checked) dateFilter = dateFilter.AddDays(-1);
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
						battleTimeReadable = "@battleTime  -> <- " + dateFromYesterdayFilter.ToString();
						DB.AddWithValue(ref battleTimeFilter, "@battleFromTime", dateFromYesterdayFilter, DB.SqlDataType.DateTime);
					}
					DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter, DB.SqlDataType.DateTime);
					battleTimeReadable = battleTimeReadable.Replace("@battleTime", dateFilter.ToString());
				}
				else
				{
					// Custom battle time filter
					if (Config.Settings.customBattleTimeFilter.from != null)
					{
						battleTimeFilter = " AND battleTime>=@battleTime ";
						dateFilter = Convert.ToDateTime(Config.Settings.customBattleTimeFilter.from);
						DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter, DB.SqlDataType.DateTime);
					}
					if (Config.Settings.customBattleTimeFilter.to != null)
					{
						battleTimeFilter += " AND battleTime<=@battleTime ";
						dateFilter = Convert.ToDateTime(Config.Settings.customBattleTimeFilter.to);
						DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter, DB.SqlDataType.DateTime);
					}
					if (Config.Settings.customBattleTimeFilter.from != null)
						battleTimeReadable = Config.Settings.customBattleTimeFilter.from + " -> ";
					if (Config.Settings.customBattleTimeFilter.to != null)
						battleTimeReadable += " <- " + Config.Settings.customBattleTimeFilter.to;
				}
				battleTimeReadable = " (" + battleTimeReadable + ")";
			}
		}

		#endregion

		#region Data Grid - TANK VIEW                                      ***********************************************************************

		private void GetTankBattleMode(out string battleModeSQL, out string battleModeFilter)
		{
			// Default values are Random/TC
			battleModeSQL = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeRandom_TC).SqlName;
			battleModeFilter = " AND (playerTankBattle.battleMode = '" + battleModeSQL + "') ";
			switch (MainSettings.GridFilterTank.BattleMode)
			{
				case GridFilter.BattleModeType.RandomAndTankCompany:
					battleModeSQL = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeRandom_TC).SqlName;
					battleModeFilter = " AND (playerTankBattle.battleMode = '" + battleModeSQL + "') ";
					break;
				case GridFilter.BattleModeType.Team:
					battleModeSQL = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeTeam).SqlName;
					battleModeFilter = " AND (playerTankBattle.battleMode = '" + battleModeSQL + "') ";
					break;
				case GridFilter.BattleModeType.TeamRanked:
					battleModeSQL = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeTeamRanked).SqlName;
					battleModeFilter = " AND (playerTankBattle.battleMode = '" + battleModeSQL + "') ";
					break;
				case GridFilter.BattleModeType.Random:
					battleModeSQL = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeRandom_TC).SqlName;
					battleModeFilter = " AND (playerTankBattle.battleMode = '" + battleModeSQL + "') AND playerTank.hasClan = 0 AND playerTank.hasCompany = 0) ";
					break;
				case GridFilter.BattleModeType.ClanWar:
					battleModeSQL = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeRandom_TC).SqlName;
					battleModeFilter = " AND (playerTank.hasClan = 1) ";
					break;
				case GridFilter.BattleModeType.TankCompany:
					battleModeSQL = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeRandom_TC).SqlName;
					battleModeFilter = " AND (playerTank.hasCompany = 1) ";
					break;
				case GridFilter.BattleModeType.Historical:
					battleModeSQL = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeHistorical).SqlName;
					battleModeFilter = " AND (playerTankBattle.battleMode = '" + battleModeSQL + "') ";
					break;
				case GridFilter.BattleModeType.Skirmishes:
					battleModeSQL = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeSkirmishes).SqlName;
					battleModeFilter = " AND (playerTankBattle.battleMode = '" + battleModeSQL + "') ";
					break;
				case GridFilter.BattleModeType.Stronghold:
					battleModeSQL = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeStronghold).SqlName;
					battleModeFilter = " AND (playerTankBattle.battleMode = '" + battleModeSQL + "') ";
					break;
				case GridFilter.BattleModeType.Special:
					battleModeSQL = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeSpecial).SqlName;
					battleModeFilter = " AND (playerTankBattle.battleMode = '" + battleModeSQL + "') ";
					break;
				case GridFilter.BattleModeType.GlobalMap:
					battleModeSQL = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeGlobalMap).SqlName;
					battleModeFilter = " AND (playerTankBattle.battleMode = '" + battleModeSQL + "') ";
					break;
				case GridFilter.BattleModeType.Grand:
					battleModeSQL = BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeGrand).SqlName;
					battleModeFilter = " AND (playerTankBattle.battleMode = '" + battleModeSQL + "') ";
					break;
				default:
					break;
			}
		}

		private async Task GridShowTank(string Status2Message)
		{
			// Grid init placement
			int gridAreaTop = 0; // Start below info panel
			dataGridMain.Top = gridAreaTop;
			dataGridMain.Left = 0;
			// Init
			mainGridSaveColWidth = false; // Do not save changing of colWidth when loading grid
			mainGridFormatting = false;
			if (!await DB.CheckConnection(false)) return;
			// Get Columns
			var selectedColList = await ColListHelper.GetSelectedColumnList();
			// Get Tank filter
			Tankfilter(out string tankFilter, out string join, out string message);
			// Create Battle mode filter
			GetTankBattleMode(out string battleModeSQL, out string battleModeFilter);
			// Get soring
			GridSortingHelper.Sorting sorting = await GridSortingHelper.GetSorting(MainSettings.GetCurrentGridFilter());
			// Default values for painting glyph as sort order direction on column header
			if (sorting.ColumnName == "")
			{
				sorting.ColumnName = "playerTank.lastBattleTime";
				sorting.ColumnHeader = "";
			}
			// Create sort order
			string sortOrder = sorting.ColumnName + " ";
			currentSortColName = sorting.ColumnHeader;
			if (sorting.SortDirectionAsc)
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
				selectedColList.ColListItems.Select = selectedColList.ColListItems.Select.Replace("playerTankBattle", "playerTankBattleTotalsView");
				sortOrder = sortOrder.Replace("playerTankBattle", "playerTankBattleTotalsView");
				// Get SUM for playerTankBattle as several battleModes might appear
				sql =
					"SELECT   " + selectedColList.ColListItems.Select + " playerTank.Id as player_Tank_Id, tank.id as tank_id, tank.name as tank_name, playerTank.markOfMastery as mb_id " + Environment.NewLine +
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
					"SELECT   " + selectedColList.ColListItems.Select + " playerTank.Id as player_Tank_Id, tank.id as tank_id, tank.name as tank_name, playerTank.markOfMastery as mb_id " + Environment.NewLine +
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
			if (selectedColList.Smallimg >= 0)
				dataGridMain.RowTemplate.Height = 31;
			if (selectedColList.Img >= 0)
				dataGridMain.RowTemplate.Height = 60;
			DataSet ds = new DataSet("DataSet");
			DataTable dtTankData = new DataTable("TankData");
			dtTankData = await DB.FetchData(sql, Config.Settings.showDBErrors);
			// If tank images add cols in datatable containing the image
			if (selectedColList.Contourimg + selectedColList.Smallimg + selectedColList.Img > -3)
			{
				// Use ImageHelper to add columns in use
				List<ImageHelper.ImgColumns> imgPosition = new List<ImageHelper.ImgColumns>();
				if (selectedColList.Contourimg >= 0)
					imgPosition.Add(new ImageHelper.ImgColumns("Tank Icon", selectedColList.Contourimg));
				if (selectedColList.Smallimg >= 0)
					imgPosition.Add(new ImageHelper.ImgColumns("Tank Image", selectedColList.Smallimg));
				if (selectedColList.Img >= 0)
					imgPosition.Add(new ImageHelper.ImgColumns("Tank Image Large", selectedColList.Img));
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
					if (selectedColList.Contourimg >= 0)
						dr["Tank Icon"] = ImageHelper.GetTankImage(tankId, "contourimg");
					if (selectedColList.Smallimg >= 0)
						dr["Tank Image"] = ImageHelper.GetTankImage(tankId, "smallimg");
					if (selectedColList.Img >= 0)
						dr["Tank Image Large"] = ImageHelper.GetTankImage(tankId, "img");
				}
			}
			// If Mastery badge image add it
			if (selectedColList.Masterybadgeimg > -1)
			{
				// Use ImageHelper to add columns in use
				List<ImageHelper.ImgColumns> imgPosition = new List<ImageHelper.ImgColumns>
				{
					new ImageHelper.ImgColumns("Mastery Badge", selectedColList.Masterybadgeimg)
				};
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
			// If column "Battles Today" is added, calc values now
			for (int i = 0; i < dtTankData.Columns.Count - 1; i++)
			{
				string colName = dtTankData.Columns[i].ColumnName;
				if (colName == "Battles Day")
				{
					// Create battle time filter for today
					string battleTimeFilter = " AND battleTime>=@battleTime ";
                    DateTime dateFilter = DateTimeHelper.GetTodayDateTimeStart();
					DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter, DB.SqlDataType.DateTime);
					// Calc here
					foreach (DataRow dr in dtTankData.Rows)
					{
						if (dr["player_Tank_Id"] != DBNull.Value)
						{
							int playerTankId = Convert.ToInt32(dr["player_Tank_Id"]);
							dr["Battles Today"] = await BattleHelper.GetTankBattleCount(playerTankId, battleTimeFilter);
						}
					}
					dtTankData.AcceptChanges();
					continue;
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
			foreach (ColListHelper.ColListItem colListItem in selectedColList.ColListItems.ColListItemList)
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
				mainGridSaveColWidth = false;
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

		private async Task GridShowBattle(string Status2Message)
		{
			try
			{
				// Grid init placement
				int gridAreaTop = 0; // Start below info panel
				dataGridMain.Top = gridAreaTop;
				dataGridMain.Left = 0;
				// Init
				mainGridSaveColWidth = false; // Do not save changing of colWidth when loading grid
				mainGridFormatting = false;
				dataGridMain.DataSource = null;
				int rowTotalsIndex = 0;
				int rowAverageIndex = 0;
				if (!await DB.CheckConnection(false)) return;
				// Find if grouping 
				bool groupingActive = (!mBattleGroup_No.Checked);
				bool groupingSum = (mBattleGroup_TankSum.Checked);
				// Get Columns
				var selectedColList = await ColListHelper.GetSelectedColumnList(groupingActive, groupingSum);
				// Get soring
				GridSortingHelper.Sorting sorting = await GridSortingHelper.GetSorting(MainSettings.GetCurrentGridFilter());
				// Default values for painting glyph as sort order direction on column header
				if (sorting.ColumnHeader == "")
				{
					sorting.ColumnHeader = "battle.battleTime";
					if (groupingActive)
						sorting.ColumnHeader = "tank_name";
					sorting.ColumnName = sorting.ColumnHeader;
				}
				// Create sort order if no grouping 
				string sortDirection = "";
				currentSortColName = sorting.ColumnHeader;
				if (sorting.SortDirectionAsc)
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
					// Non allowed sorting
					string groupBySort = sorting.ColumnHeader;
					if (groupBySort == "Tank Image") groupBySort = "tank_name";
					if (groupBySort == "Mastery Badge") groupBySort = "mb_id";
					// grouping
					selectFixed =
						"  '#30A8FF' as battleResultColor,  '#30A8FF' as battleSurviveColor, " +
						"  NULL as battleTimeToolTip, SUM(battle.battlesCount) as battlesCountToolTip, " +
						"  SUM(battle.victory) as victoryToolTip, SUM(battle.draw) as drawToolTip, SUM(battle.defeat) as defeatToolTip, " +
						"  SUM(battle.survived) as survivedCountToolTip, SUM(battle.killed) as killedCountToolTip, tank.id as tank_id, tank.name as tank_name, 0 as arenaUniqueID," +
						"  0 as footer, playerTank.Id as player_Tank_Id, 0 as mb_id, 0 as battle_Id ";
					groupBy = "GROUP BY tank.id, tank.Name, tank.short_name, playerTank.Id ";
					sortOrder = "ORDER BY [" + groupBySort + "] " + sortDirection + " ";
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
					sortOrder = "ORDER BY " + sorting.ColumnName + " " + sortDirection + " ";
				}

				// Get Battle Time filer or battle count filter
				BattleTimeAndCountFilter(out string battleTimeFilter, out string battleTimeReadable, out bool battleCountFilter);

				// Get Battle mode filter
				BattleModeFilter(out string battleModeFilter, out string battleMode);

				// Get Tank filter
				Tankfilter(out string tankFilter, out string tankJoin, out string tankFilterMessage);

				// Create where part, and check for battle count filter
				string from = "FROM (((((((battle INNER JOIN playerTank ON battle.playerTankId = playerTank.id) " +
						"INNER JOIN tank ON playerTank.tankId = tank.id) " +
                        "INNER JOIN tankType ON tank.tankTypeId = tankType.Id) " +
                        "INNER JOIN country ON tank.countryId = country.Id) " +
                        "INNER JOIN battleResult ON battle.battleResultId = battleResult.id) " +
                        "LEFT JOIN map on battle.mapId = map.id) " +
                        "INNER JOIN battleSurvive ON battle.battleSurviveId = battleSurvive.id) SB " +
						"INNER JOIN (select *, PL.id as PID, BP.accountId as AId, BP.team as SP from battlePlayer BP " +
                        "INNER JOIN player PL on AId = PL.accountId) P on battle.id = P.battleId " + tankJoin + " ";
                string where = "WHERE playerTank.playerId=@playerid " + battleTimeFilter + battleModeFilter + tankFilter + " ";
				DB.AddWithValue(ref where, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
				if (battleCountFilter)
				{
					await BattleCountFilterHelper.SetBattleFilter(from, where, await BattleCountFilterHelper.GetBattleLimitFromid(mBattlesCountSelected.Tag.ToString()));
					where = " WHERE battlesCountTotal = 1 ";
				}

				// Create SQL
				string sql =
					"SELECT " + selectedColList.ColListItems.Select + " " + selectFixed + " " +
					from +
					where +
					groupBy + " " +
					sortOrder;

				// Get data
				DataTable dt = new DataTable();
				dt = await DB.FetchData(sql, Config.Settings.showDBErrors);



				// If images add cols in datatable containing the image
				if (selectedColList.Contourimg + selectedColList.Smallimg + selectedColList.Img > -3)
				{
					// Use ImageHelper to add columns in use
					List<ImageHelper.ImgColumns> imgPosition = new List<ImageHelper.ImgColumns>();
					if (selectedColList.Contourimg >= 0)
						imgPosition.Add(new ImageHelper.ImgColumns("Tank Icon", selectedColList.Contourimg));
					if (selectedColList.Smallimg >= 0)
						imgPosition.Add(new ImageHelper.ImgColumns("Tank Image", selectedColList.Smallimg));
					if (selectedColList.Img >= 0)
						imgPosition.Add(new ImageHelper.ImgColumns("Tank Image Large", selectedColList.Img));
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
						if (selectedColList.Contourimg >= 0)
							dr["Tank Icon"] = ImageHelper.GetTankImage(tankId, "contourimg");
						if (selectedColList.Smallimg >= 0)
							dr["Tank Image"] = ImageHelper.GetTankImage(tankId, "smallimg");
						if (selectedColList.Img >= 0)
							dr["Tank Image Large"] = ImageHelper.GetTankImage(tankId, "img");
					}
				}
				// If Mastery badge image add it
				if (selectedColList.Masterybadgeimg > -1)
				{
					// Use ImageHelper to add columns in use
					List<ImageHelper.ImgColumns> imgPosition = new List<ImageHelper.ImgColumns>
					{
						new ImageHelper.ImgColumns("Mastery Badge", selectedColList.Masterybadgeimg)
					};
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

				// Rrow count
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
						"ID", "Premium", "Mastery Badge", "Mastery Badge ID", "Battle Count" , "Platoon", "Killed By Player ID", "Enemy Clan ID", "SP", "Team"
					};
					foreach (ColListHelper.ColListItem colListItem in selectedColList.ColListItems.ColListItemList)
					{
						if (colListItem.colType == "Int" || colListItem.colType == "Float")
						{

							if (!nonAvgCols.Contains(colListItem.name))
							{
								double count = 0;
								double sum = 0;
								foreach (DataRow dr in dt.Rows)
								{
									if (dr[colListItem.name] != DBNull.Value)
									{
										count += Convert.ToDouble(dr["battlesCountToolTip"]);
										sum += Convert.ToDouble(dr[colListItem.name]) * Convert.ToDouble(dr["battlesCountToolTip"]);
									}
								}
								if (count > 0)
									if (count > 1 && colListItem.name == "WN9") // Special calculation for WN8
										rowAverage[colListItem.name] = (await Code.Rating.WN9.CalcBattleRange(battleTimeFilter, 0, battleMode, tankFilter, battleModeFilter, tankJoin)).WN9;
									else if (count > 1 && colListItem.name == "WN8") // Special calculation for WN8
										rowAverage[colListItem.name] = await Code.Rating.WN8.CalcBattleRange(battleTimeFilter, 0, battleMode, tankFilter, battleModeFilter, tankJoin);
									else if (count > 1 && colListItem.name == "WN7") // Special calculation for WN7
										rowAverage[colListItem.name] = await Code.Rating.WN7.WN7battle(battleTimeFilter, 0, battleMode, tankFilter, battleModeFilter, tankJoin);
									else if (count > 1 && colListItem.name == "EFF") // Special calculation for EFF
										rowAverage[colListItem.name] = await Code.Rating.EFF.EffBattle(battleTimeFilter, 0, battleMode, tankFilter, battleModeFilter, tankJoin);
									else if (count > 1 && colListItem.name == "Dmg C/R") // Special calculation Dmg C/R
										rowAverage[colListItem.name] = Math.Round(await CalcAvgDmgCR(battleTimeFilter, battleMode, tankFilter, battleModeFilter, tankJoin), 1);
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
								case "SP": s = ""; break;
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
						"Tier", "Premium", "ID", "Mastery Badge ID", "EFF", "WN7", "WN8", "WN9", "Hit Rate", "Min Tier", "Max Tier", "Dmg Rank",
						"Pierced Shots%", "Pierced Hits%", "HE Shots %", "HE Hts %", "Platoon", "Killed By Player ID", "Enemy Clan ID", "Dmg C/R", "SP", "Team"
                    };
					IEnumerable<string> countCols = new List<string>
					{
						"Killed Count", "Victory" ,"Draw","Defeat","Survival Count","Clan","Company","Battle Count"
					};

					foreach (ColListHelper.ColListItem colListItem in selectedColList.ColListItems.ColListItemList)
					{
						// Format column
						if (colListItem.colType == "Int" || colListItem.colType == "Float")
						{

							if (!nonTotalsCols.Contains(colListItem.name)) // Avoid calculate total EFF/WN8
							{
								// looping through datatable for every row per column and multiply with battlesCountToolTip to get correct sum when several battles recorded on one row
								double sum = 0;
								if (!countCols.Contains(colListItem.name))
								{
									foreach (DataRow dr in dt.Rows)
									{

										if (dr[colListItem.name] != DBNull.Value)
										{
											sum += Convert.ToDouble(dr[colListItem.name]) * Convert.ToDouble(dr["battlesCountToolTip"]);
										}
									}
								}
								else
								{
									foreach (DataRow dr in dt.Rows)
									{
										if (dr[colListItem.name] != DBNull.Value)
										{
											sum += Convert.ToDouble(dr[colListItem.name]);
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
						rowTotalsIndex = dt.Rows.Count - 1;
						dt.Rows.Add(rowAverage);
						rowAverageIndex = dt.Rows.Count - 1;
					}
				}
				// Set row height in template before rendering to fit images
				dataGridMain.RowTemplate.Height = 23;
				if (selectedColList.Smallimg >= 0)
					dataGridMain.RowTemplate.Height = 31;
				if (selectedColList.Img >= 0)
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
				foreach (ColListHelper.ColListItem colListItem in selectedColList.ColListItems.ColListItemList)
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
						List<string> showFloatValues = new List<string>
						{
							"Exp Dmg","Exp Win Rate","Exp Spot","Exp Frags","Exp Def", "Dmg C/R", "Dmg Rank Progress", "Dmg Rank"
						};
						if (groupingActive)
						{
							showFloatValues.Add("Min Tier");
							showFloatValues.Add("Max Tier");
							showFloatValues.Add("Frags");
							showFloatValues.Add("Spot");
						}
						if (!showFloatValues.Contains(colListItem.name)) // Decimals
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
                                case "SP":
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
				await Log.LogToFile(ex);
				//throw;
			}

		}

		private async Task<double> CalcAvgDmgCR(string battleTimeFilter, string battleMode, string tankFilter, string battleModeFilter, string tankJoin = "")
		{
			double result = 0;
			if (battleMode == "")
				battleMode = "%";
			string sql =
					"select SUM(dmg) as dmg, SUM(dmgReceived) as dmgReceived " +
					"from battle INNER JOIN playerTank ON battle.playerTankId=playerTank.Id INNER JOIN tank on playerTank.tankId = tank.id " + tankJoin + " " +
					"where playerId=@playerId and battleMode like @battleMode " + battleTimeFilter + " " + tankFilter + " " + battleModeFilter;
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battleMode", battleMode, DB.SqlDataType.VarChar);
			DataTable dtBattles = await DB.FetchData(sql);
			if (dtBattles.Rows.Count > 0)
			{
				double dmg = Convert.ToDouble(dtBattles.Rows[0]["dmg"]);
				double dmgReceived = Convert.ToDouble(dtBattles.Rows[0]["dmgReceived"]);
				if (dmgReceived > 0)
					result = dmg / dmgReceived;
			}
			return result;
		}

		#endregion

		#region Data Grid - MAP VIEW                                       ***********************************************************************
		private static GridSortingHelper.Sorting mapSorting = new GridSortingHelper.Sorting
		{
			ColumnHeader = "Map",
			ColumnName = "Map",
			SortDirectionAsc = true
		};
		private async Task GridShowMap(string Status2Message)
		{
			try
			{
				// Grid init placement
				int gridAreaTop = 0; // Start below info panel
				dataGridMain.Top = gridAreaTop;
				dataGridMain.Left = 0;
				// Init
				mainGridSaveColWidth = false; // Do not save changing of colWidth when loading grid
				mainGridFormatting = false;
				dataGridMain.DataSource = null;
				// Check
				if (!await DB.CheckConnection(false)) return;

				// Show old maps?
				string sqlWhereOldMaps = "";
				string sqlAndOldMaps = "";
				if (!mMapShowOld.Checked)
				{
					sqlWhereOldMaps = " WHERE map.active = 1 ";
					sqlAndOldMaps = " AND map.active = 1 ";
				}

				// Get Battle Time filer
				BattleTimeAndCountFilter(out string sqlBattleTimeFilter, out string battleTimeReadable, out bool battleCountFilter);

				// Get Battle mode filter
				BattleModeFilter(out string sqlBattleModeFilter, out string battleMode);

				// Get sorting
				string sortDirection = "";
				if (mapSorting.SortDirectionAsc)
					sortDirection += " ASC ";
				else
					sortDirection += " DESC ";
				if (mapSorting.ColumnName == "Image")
				{
					mapSorting.ColumnName = "Map";
					mapSorting.ColumnHeader = "Map";
				}
				string sqlOrderBy = "ORDER BY [" + mapSorting.ColumnName + "] " + sortDirection + " ";

				// Get Tank filter
				Tankfilter(out string sqlTankFilter, out string tankJoin, out string tankFilterMessage, onlyTankFilter: true);
				Tankfilter(out string sqlPlayerTankFilter, out tankJoin, out tankFilterMessage, onlyPlayerTankFilter: true);

				// Get cols - Default
				string sqlSelect =
					"map.name AS 'Map', " +
					"MAX(battle.battleTime) AS 'Last Battle', " +
					"SUM(battle.battlesCount) AS 'Battles', " +
					"CAST(SUM(battle.battlesCount) AS FLOAT) AS 'Frequency', " +
					"SUM(battle.victory) * 100 / CAST(NULLIF(SUM(battle.battlesCount),0) AS FLOAT) AS 'Win Rate', " +
					"AVG(CAST(tank.tier AS FLOAT)) AS 'Avg Tier', " +
					"AVG(CAST(battle.maxBattleTier AS FLOAT)) AS 'Avg Max Tier', " +
					"map.id AS 'Map_ID' ";
				string sqlSelectRemainingMaps =
					"map.name AS 'Map', " +
					"CAST(NULL AS DATETIME) AS 'Last Battle', " +
					"0 AS 'Battles', " +
					"CAST(0 AS FLOAT) AS 'Frequency', " +
					"CAST(NULL AS FLOAT) AS 'Win Rate', " +
					"CAST(NULL AS FLOAT) AS 'Avg Tier', " +
					"CAST(NULL AS FLOAT) As 'Avg Max Tier', " +
					"map.id AS 'Map_ID' ";
				string sqlGroupBy =
					"map.id, map.name ";
				// Other views
				if (mMapDescr.Checked || mMapDescrLarge.Checked)
				{
					sqlSelect =
						"map.name AS 'Map', " +
						"map.description AS 'Description', " +
						"map.id AS 'Map_ID' ";
					sqlSelectRemainingMaps = sqlSelect;
					sqlGroupBy =
						"map.id, map.name, map.description ";
				}

				// Create SQL
				string where = "";
				string from =
					" FROM    map " +
					"  INNER JOIN battle ON map.id = battle.mapId " + sqlBattleTimeFilter + sqlBattleModeFilter +
					"  INNER JOIN playerTank ON battle.playerTankId = playerTank.id AND playerTank.playerId=@playerid " + sqlPlayerTankFilter +
					"  INNER JOIN tank ON playerTank.tankId = tank.id " + sqlTankFilter +
					"  INNER JOIN tankType ON tank.tankTypeId = tankType.Id " +
					"  INNER JOIN country ON tank.countryId = country.Id " +
					"  INNER JOIN battleResult ON battle.battleResultId = battleResult.id " +
					"  INNER JOIN battleSurvive ON battle.battleSurviveId = battleSurvive.id " + tankJoin.Replace("INNER JOIN", "LEFT JOIN") + " ";
				DB.AddWithValue(ref from, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
				if (battleCountFilter)
				{
					await BattleCountFilterHelper.SetBattleFilter(from, where, await BattleCountFilterHelper.GetBattleLimitFromid(mBattlesCountSelected.Tag.ToString()));
					from =
					" FROM    map " +
					"  INNER JOIN battle ON map.id = battle.mapId AND battle.battlesCountTotal = 1 " +
					"  INNER JOIN playerTank ON battle.playerTankId = playerTank.id AND playerTank.playerId=@playerid " + sqlPlayerTankFilter +
					"  INNER JOIN tank ON playerTank.tankId = tank.id " + sqlTankFilter +
					"  INNER JOIN tankType ON tank.tankTypeId = tankType.Id " +
					"  INNER JOIN country ON tank.countryId = country.Id " +
					"  INNER JOIN battleResult ON battle.battleResultId = battleResult.id " +
					"  INNER JOIN battleSurvive ON battle.battleSurviveId = battleSurvive.id " + tankJoin.Replace("INNER JOIN", "LEFT JOIN") + " ";
					DB.AddWithValue(ref from, "@playerid", Config.Settings.playerId.ToString(), DB.SqlDataType.Int);
				}

				// Todo: add battle count filter
				string sql =
					"SELECT " + sqlSelect +
					from +
					sqlWhereOldMaps +
					"GROUP BY " + sqlGroupBy +
					sqlOrderBy;

				DataTable dt = new DataTable();
				dt = await DB.FetchData(sql, Config.Settings.showDBErrors);

				// Row count, battle count and calc frequency
				int rowcount = dt.Rows.Count;
				int totalBattleCount = 0;
				if (rowcount > 0 && mMapDefault.Checked)
				{
					totalBattleCount = Convert.ToInt32(dt.Compute("Sum(Battles)", ""));
					foreach (DataRow dr in dt.Rows)
					{
						dr["Frequency"] = Math.Round((Convert.ToDouble(dr["Battles"]) / totalBattleCount * 100), 2);
					}
					dt.AcceptChanges();
				}

				// Add all maps and frequency
				if (mMapShowAll.Checked)
				{
					string existingMapID = "";
					string sqlWhereRemainingMaps = "";
					if (dt.Rows.Count > 0)
					{
						foreach (DataRow dr in dt.Rows)
						{
							existingMapID += dr["Map_ID"].ToString() + ",";
						}
						existingMapID = existingMapID.Substring(0, existingMapID.Length - 1);
						sqlWhereRemainingMaps = "WHERE id NOT IN (" + existingMapID + ") " + sqlAndOldMaps;
					}
					else
					{
						sqlWhereRemainingMaps = sqlWhereOldMaps;
					}
					string sqlRemainingMaps =
						"SELECT " + sqlSelectRemainingMaps +
						"FROM map " +
						sqlWhereRemainingMaps +
						sqlOrderBy;
					DataTable dtRest = await DB.FetchData(sqlRemainingMaps);
					if (dtRest.Rows.Count > 0)
					{
						if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
							dt.Merge(dtRest);
						else
						{
							foreach (DataRow dr in dtRest.Rows)
							{
								DataRow newRow = dt.NewRow();
								for (int i = 0; i < 8; i++) { newRow[i] = dr[i]; }
								dt.Rows.Add(newRow);
							}
						}
					}
				}

				// Set row height and image size before data is added
				bool imageIllustration = false;
				int imgWidth = 40;
				int imageHeight = 0;
				if (mMapDefault.Checked)
				{
					imageHeight = 30; // Resize to smaller image
					dataGridMain.RowTemplate.Height = 32;
					imageIllustration = true;
				}
				else if (mMapDescr.Checked)
				{
					imageHeight = 100; // Resize to smaller image
					dataGridMain.RowTemplate.Height = 100;
				}
				else
				{
					imageHeight = 0; // use deafult size - no resizing = 300
					dataGridMain.RowTemplate.Height = 300;
				}

				dataGridMain.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

				// Add map images
				dt.Columns.Add("Image", typeof(Image)).SetOrdinal(0);
				// Fill with images
				foreach (DataRow dr in dt.Rows)
				{
					int map_id = Convert.ToInt32(dr["Map_ID"]);
					Image img = await ImageHelper.GetMap(map_id, imageIllustration, imageHeight);
					dr["Image"] = img;
					imgWidth = img.Width;
				}

				// Add footer now
				int rowTotalsIndex = 0;
				if (rowcount > 0 && mMapDefault.Checked)
				{
					DataRow rowTotals = dt.NewRow();
					rowTotals["Map"] = "Totals";
					rowTotals["Image"] = new Bitmap(1, 1);
					rowTotals["Battles"] = totalBattleCount;
					rowTotals["Frequency"] = 100;
					// Add row
					if (Config.Settings.gridBattlesTotalsTop)
					{
						// as header
						dt.Rows.InsertAt(rowTotals, 0);
						rowTotalsIndex = 1;
					}
					else
					{
						// as footer
						dt.Rows.Add(rowTotals);
						rowTotalsIndex = dt.Rows.Count - 1;
					}
				}

				// populate datagrid
				dataGridMain.Columns.Clear();
				mainGridFormatting = true;
				frozenRows = 0;

				dataGridMain.DataSource = dt;
				// Unfocus
				dataGridMain.ClearSelection();
				// Hide sys cols
				dataGridMain.Columns["Map_ID"].Visible = false;
				// Cell Formatting
				dataGridMain.Columns["Map"].Width = 70 + (Config.Settings.gridFontSize * 8);
				dataGridMain.Columns["Image"].Width = imgWidth;
				if (mMapDefault.Checked)
				{
					dataGridMain.Columns["Battles"].Width = 10 + (Config.Settings.gridFontSize * 6);
					dataGridMain.Columns["Battles"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
					dataGridMain.Columns["Frequency"].Width = 20 + (Config.Settings.gridFontSize * 8);
					dataGridMain.Columns["Frequency"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
					dataGridMain.Columns["Frequency"].DefaultCellStyle.Format = "N2";
					dataGridMain.Columns["Last Battle"].Width = 40 + (Config.Settings.gridFontSize * 10);
					dataGridMain.Columns["Win Rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
					dataGridMain.Columns["Win Rate"].DefaultCellStyle.Format = "N1";
					dataGridMain.Columns["Win Rate"].Width = 75;
					dataGridMain.Columns["Avg Tier"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
					dataGridMain.Columns["Avg Tier"].DefaultCellStyle.Format = "N1";
					dataGridMain.Columns["Avg Tier"].Width = 75;
					dataGridMain.Columns["Avg Max Tier"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
					dataGridMain.Columns["Avg Max Tier"].DefaultCellStyle.Format = "N1";
					dataGridMain.Columns["Avg Max Tier"].Width = 75;
				}
				else if (mMapDescr.Checked)
				{
					dataGridMain.Columns["Image"].Width = 100;
					dataGridMain.Columns["Description"].Width = 400;
				}
				else if (mMapDescrLarge.Checked)
				{
					dataGridMain.Columns["Image"].Width = 300;
					dataGridMain.Columns["Description"].Width = 300;
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
				await Log.LogToFile(ex);
				MsgBox.Show(ex.Message, "Error on Map beta feature");
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
						StringFormat sf = new StringFormat
						{
							Alignment = StringAlignment.Center,
							LineAlignment = StringAlignment.Center
						};
						e.Graphics.DrawString((e.RowIndex + offset).ToString(), e.CellStyle.Font, br, e.CellBounds, sf);
					}
					e.Handled = true;
				}
			}
			// Add glyph to column headers for tank and battle view
			else if ((MainSettings.View == GridView.Views.Tank || MainSettings.View == GridView.Views.Battle) &&
						e.RowIndex < 0 && e.ColumnIndex > -1 && dataGridMain.Columns[e.ColumnIndex].HeaderText == currentSortColName)
			{
				dataGridMain.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = currentSortDirection;
			}
			// Add glyph to column headers for map view
			else if (MainSettings.View == GridView.Views.Map &&
						e.RowIndex < 0 && e.ColumnIndex > -1 && dataGridMain.Columns[e.ColumnIndex].HeaderText == mapSorting.ColumnHeader)
			{
				SortOrder mapSortDirection = SortOrder.Descending;
				if (mapSorting.SortDirectionAsc)
					mapSortDirection = SortOrder.Ascending;
				dataGridMain.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = mapSortDirection;
			}
			// Remove arrow on row headers
			else if (e.ColumnIndex == -1 && e.RowIndex > -1)
			{
				e.PaintBackground(e.CellBounds, true);
				using (SolidBrush br = new SolidBrush(ColorTheme.ControlDimmedFont))
				{
					StringFormat sf = new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					};
					e.Graphics.DrawString("", e.CellStyle.Font, br, e.CellBounds, sf);
				}
				e.Handled = true;
			}
		}

		private async void dataGridMain_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				// Avoid headerRow and Image columns
				if (e.ColumnIndex < 0)
					return;
				// Manual Sort for battle and tanks view
				if (MainSettings.View != GridView.Views.Overall && MainSettings.View != GridView.Views.Map)
				{
					// Find colName
					ColListHelper.ColListItem clc = await ColListHelper.GetColListItem(dataGridMain.Columns[e.ColumnIndex].Name, MainSettings.View);
					// Find current sort
					GridSortingHelper.Sorting sorting = await GridSortingHelper.GetSorting(MainSettings.GetCurrentGridFilter());
					// Check if this is first sorting
					if (sorting == null)
						sorting = new GridSortingHelper.Sorting();
					// Check if same same column as last
					if (clc.name == sorting.ColumnHeader)
					{
						// same as last, reverse sort direction
						sorting.SortDirectionAsc = !sorting.SortDirectionAsc;
					}
					else
					{
						// new column, get default sort direction
						sorting.ColumnHeader = clc.name; // column name in header
						sorting.ColumnName = clc.colName; // database field to sort on
						bool sortDirectionAsc = false;
						if (dataGridMain.Columns[e.ColumnIndex].ValueType == typeof(string))
							sortDirectionAsc = true;
						sorting.SortDirectionAsc = sortDirectionAsc;
					}
					// Save new sorting
					await GridSortingHelper.SaveSorting(MainSettings.GetCurrentGridFilter().ColListId, sorting);
					// Show grid
					await ShowView("Datagrid refreshed after sorting");
				}
				// Special sorting for map, just remember latest
				else if (MainSettings.View == GridView.Views.Map)
				{
					// Find colName
					string col = dataGridMain.Columns[e.ColumnIndex].Name;
					// check if same sort col, toggle dir
					if (col == mapSorting.ColumnHeader)
						mapSorting.SortDirectionAsc = !mapSorting.SortDirectionAsc;
					else
					{
						mapSorting.ColumnHeader = col;
						mapSorting.ColumnName = col;
						if (col == "Map" || col == "Description")
							mapSorting.SortDirectionAsc = true;
						else
							mapSorting.SortDirectionAsc = false;
					}
					// Show grid
					await ShowView("Datagrid refreshed after sorting");
				}
			}
			catch (Exception ex)
			{
				await Log.LogToFile(ex);
				if (Config.Settings.showDBErrors)
					MsgBox.Show("Error on grid header mouse click event, see log file", "Grid error", this);

			}

		}

		#endregion

		#region Grid Formatting

		private async void dataGridMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			try
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
							cell.Style.ForeColor = ColorRangeScheme.EffColor(eff);
							cell.Style.SelectionForeColor = cell.Style.ForeColor;
						}
					}
					else if (col.Equals("WN9") || col.Equals("WN9 Max Hist"))
					{
						if (dataGridMain["WN9", e.RowIndex].Value != DBNull.Value)
						{
							int wn9 = Convert.ToInt32(dataGridMain["WN9", e.RowIndex].Value);
							cell.Style.ForeColor = ColorRangeScheme.WN9color(wn9);
							cell.Style.SelectionForeColor = cell.Style.ForeColor;
						}
					}
					else if (col.Equals("WN8"))
					{
						if (dataGridMain["WN8", e.RowIndex].Value != DBNull.Value)
						{
							int wn8 = Convert.ToInt32(dataGridMain["WN8", e.RowIndex].Value);
							cell.Style.ForeColor = ColorRangeScheme.WN8color(wn8);
							cell.Style.SelectionForeColor = cell.Style.ForeColor;
						}
					}
					else if (col.Equals("WN7"))
					{
						if (dataGridMain["WN7", e.RowIndex].Value != DBNull.Value)
						{
							int wn7 = Convert.ToInt32(dataGridMain["WN7", e.RowIndex].Value);
							cell.Style.ForeColor = ColorRangeScheme.WN7color(wn7);
							cell.Style.SelectionForeColor = cell.Style.ForeColor;
						}
					}
					else if (col.Equals("Dmg Rank"))
					{
						if (dataGridMain["Dmg Rank", e.RowIndex].Value != DBNull.Value)
						{
							double dmgRank = Convert.ToDouble(dataGridMain["Dmg Rank", e.RowIndex].Value);
							cell.Style.ForeColor = ColorRangeScheme.DmgRankColor(dmgRank);
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
								else if (percentage >= 90) color = ColorTheme.Rating_very_great;
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
					else if (MainSettings.View == GridView.Views.Tank)
					{
						if (col.Equals("Remaining XP"))
						{
							if (dataGridMain[col, e.RowIndex].Value != DBNull.Value)
							{
								int val = Convert.ToInt32(dataGridMain[col, e.RowIndex].Value);
								if (val > 0)
								{
									Color color = ColorTheme.Rating_very_bad;
									if (val <= 5000) color = ColorTheme.Rating_super_uniqum;
									else if (val <= 10000) color = ColorTheme.Rating_uniqum;
									else if (val <= 25000) color = ColorTheme.Rating_very_great;
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
						else if (col.Equals("Battles Today"))
						{
							int today = Convert.ToInt32(dataGridMain[col, e.RowIndex].Value);
							if (dataGridMain["Battles Day", e.RowIndex].Value != DBNull.Value)
							{
								int target = Convert.ToInt32(dataGridMain["Battles Day", e.RowIndex].Value);
								int diff = today - target;
								Color color = ColorTheme.Rating_very_good;
								if (diff < -3)
									color = ColorTheme.Rating_very_bad;
								else if (diff < 0)
									color = ColorTheme.Rating_bad;
								else if (diff < 2)
									color = ColorTheme.Rating_good;
								cell.Style.ForeColor = color;
								cell.Style.SelectionForeColor = cell.Style.ForeColor;
								// Add background color if victory
								// Create battle time filter for today
								string battleTimeFilter = " AND battleTime>=@battleTime ";
                                DateTime dateFilter = DateTimeHelper.GetTodayDateTimeStart();
								DB.AddWithValue(ref battleTimeFilter, "@battleTime", dateFilter, DB.SqlDataType.DateTime);
								// Get values
								int playerTankId = Convert.ToInt32(dataGridMain["player_Tank_Id", e.RowIndex].Value);
								int victoryCount = await BattleHelper.GetTankBattleVictoryCount(playerTankId, battleTimeFilter);
								// add back color
								if (victoryCount > 0)
								{
									cell.Style.BackColor = ColorTheme.GridRowCurrentPlayerAlive;
									cell.Style.SelectionBackColor = ColorTheme.GridRowCurrentPlayerAliveSelected;
								}
							}
						}
						if (col.Equals("Win Rate"))
						{
							if (dataGridMain["Win Rate", e.RowIndex].Value != DBNull.Value)
							{
								double wr = Convert.ToDouble(dataGridMain["Win Rate", e.RowIndex].Value);
								cell.Style.ForeColor = ColorRangeScheme.WinRateColor(wr);
								cell.Style.SelectionForeColor = cell.Style.ForeColor;
							}
						}
						if (col.Equals("RWR"))
						{
							if (dataGridMain["RWR", e.RowIndex].Value != DBNull.Value)
							{
								double RWR = Convert.ToDouble(dataGridMain["RWR", e.RowIndex].Value);
								cell.Style.ForeColor = ColorRangeScheme.RWRcolor(RWR);
								cell.Style.SelectionForeColor = cell.Style.ForeColor;
							}
						}
					}
					else if (MainSettings.View == GridView.Views.Battle)
					{
						bool footer = false;
						if (dataGridMain["footer", e.RowIndex] != null)
							footer = (Convert.ToInt32(dataGridMain["footer", e.RowIndex].Value) > 0);
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
					else if (MainSettings.View == GridView.Views.Map)
					{
						bool footer = (dataGridMain["Map", e.RowIndex].Value.ToString() == "Totals");
						if (footer)
							dataGridMain.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTheme.GridTotalsRow;
						if (col.Equals("Win Rate"))
						{
							if (dataGridMain["Win Rate", e.RowIndex].Value != DBNull.Value)
							{
								double wr = Convert.ToDouble(dataGridMain["Win Rate", e.RowIndex].Value);
								cell.Style.ForeColor = ColorRangeScheme.WinRateColor(wr);
								cell.Style.SelectionForeColor = cell.Style.ForeColor;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				await Log.LogToFile(ex, "Error on datagrid paint event");
			}

		}

		#endregion

		#region Grid Right Click

		private int dataGridRightClickCol = -1;
		private int dataGridRightClickRow = -1;
		private async void dataGridMain_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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
					if (dataGridMain.Rows[dataGridRightClickRow].Cells["footer"].Value != null)
					{
						string s = dataGridMain.Rows[dataGridRightClickRow].Cells["footer"].Value.ToString();
						Int32.TryParse(s, out int i);
						if (i > 0)
						{
							dataGridRightClickCol = -1;
							dataGridRightClickRow = -1;
						}
					}
				}

			}
			catch (Exception ex)
			{
				await Log.LogToFile(ex, "Error on grid mouse down event");
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


		private async void dataGridMainPopup_GrindingSetup_Click(object sender, EventArgs e)
		{
			if (dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value != DBNull.Value)
			{
				int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
				Form frm = new Forms.GrindingSetup(playerTankId);
				frm.ShowDialog(this);
				if (MainSettings.View == GridView.Views.Tank)
					await ShowView("Refreshed grid");
			}
		}

		private async void dataGridMainPopup_GrindingSetupRecalculate_Click(object sender, EventArgs e)
		{
			await GrindingHelper.RecalculateGrindingProgress();
			if (MainSettings.View == GridView.Views.Tank)
				await ShowView("Recalculated Grinding Progress, refreshed grid");
			else
				SetStatus2("Recalculated Grinding Progress");
		}


		private void dataGridMainPopup_BattleChart_Click(object sender, EventArgs e)
		{
			if (dataGridMain.Rows[dataGridRightClickRow].Cells["tank_Id"].Value != DBNull.Value)
			{
				int tankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["tank_Id"].Value);
				Form frm = new Forms.BattleChart(tankId);
				FormHelper.OpenFormToRightOfParent(this, frm);
			}
		}

		private async void dataGridMainPopup_RecalculateBattleRating_Click(object sender, EventArgs e)
		{
			int battleId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["battle_Id"].Value);
			Form frm = new Forms.RecalcBattleRating(true, true, true, true, true, true, battleId);
			frm.ShowDialog(this);
			await ShowView("Refreshed grid");
		}

		private void dataGridMainPopup_BattleDetails_Click(object sender, EventArgs e)
		{
			int battleId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["battle_Id"].Value);
			Form frm = new Forms.BattleDetail(battleId, this);
			FormHelper.OpenFormCenterOfParent(this, frm);
		}

		private void dataGridMainPopup_BattleSummary_Click(object sender, EventArgs e)
		{
			// Get Battle Time filer
			BattleTimeAndCountFilter(out string battleTimeFilter, out string battleTimeReadable, out bool battleCountFilter);

			// Get Battle mode filter
			BattleModeFilter(out string battleModeFilter, out string battleMode);

			// Get Tank filter
			Tankfilter(out string tankFilter, out string tankJoin, out string tankFilterMessage);

			// Show form
			Form frm = new BattleSummary(battleTimeFilter, battleCountFilter, battleModeFilter, battleMode, tankFilter, tankJoin); // TODO: add battle count filter
			FormHelper.OpenFormCenterOfParent(this, frm);
		}

		private void dataGridMainPopup_TankDetails_Click(object sender, EventArgs e)
		{
			int playerTankId = DbConvert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
			int tankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["tank_Id"].Value);
			Form frm = new PlayerTankDetail(playerTankId, tankId);
			FormHelper.OpenFormToRightOfParent(this, frm);
		}

		private async void dataGridMainPopup_EditTankInfo_Click(object sender, EventArgs e)
		{
			int tankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["tank_Id"].Value);
			Form frm = new Forms.TankInfoEdit(tankId);
			frm.ShowDialog(this);
			await ShowView("Refresh view");
		}


		private async void dataGridMainPopup_DeleteBattle_Click(object sender, EventArgs e)
		{
			int battleId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["battle_Id"].Value);
			string sql = "select battle.battleTime, tank.name " +
				"from battle inner join playerTank on battle.playerTankId=playerTank.id inner join tank on playerTank.tankId=tank.Id " +
				"where battle.Id=@id ";
			DB.AddWithValue(ref sql, "@id", battleId, DB.SqlDataType.Int);
			DataTable dt = await DB.FetchData(sql, Config.Settings.showDBErrors);
			if (dt.Rows.Count > 0)
			{
                DateTime battleTime = Convert.ToDateTime(dt.Rows[0]["battleTime"]);
				string tankName = dt.Rows[0]["name"].ToString();
				Code.MsgBox.Button answer = Code.MsgBox.Show("Do you really want to delete this battle:" + Environment.NewLine + Environment.NewLine +
					"  Battle: " + battleTime + Environment.NewLine +
					"  Tank:   " + tankName, "Delete battle", MsgBox.Type.OKCancel, this);
				if (answer == MsgBox.Button.OK)
				{

					sql =
						"delete from battleAch where battleId=@battleId; " +
						"delete from battleFrag where battleId=@battleId; " +
						"delete from battleplayer where battleId=@battleId; " +
						"delete from battle where id=@battleId; ";
					DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.VarChar);
					await DB.ExecuteNonQuery(sql);
					await ShowView("Deleted battle, grid refreshed");
				}
			}
		}

		private void dataGridMainPopup_CopyRowToClipboard_Click(object sender, EventArgs e)
		{
			string s = "";
			foreach (DataGridViewColumn col in dataGridMain.Columns)
			{
				if (col.Visible && col.ValueType.Name != "Image")
				{
					if (col.Name.Length > 12 && col.Name.Substring(0, 13) == " - Separator ")
						s += Environment.NewLine;
					else
						s += col.Name + ": " + dataGridMain.Rows[dataGridRightClickRow].Cells[col.Name].Value + Environment.NewLine;
				}
			}
			System.Windows.Forms.Clipboard.SetText(s);
		}

		private async void dataGridMainPopup_RecalculateTankCredit_Click(object sender, EventArgs e)
		{
			int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
			await TankCreditCalculation.RecalculateForTank(playerTankId);
			await ShowView("Refreshed grid");
		}

		private async void dataGridMainPopup_RecalculateTankRating_Click(object sender, EventArgs e)
		{
			int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
			GetTankBattleMode(out string battleMode, out string temp);
			await Code.Rating.WNHelper.RecalculateRatingForTank(playerTankId, battleMode);
			await ShowView("Refreshed grid");
		}

		private void dataGridMainPopup_Replay_Click(object sender, EventArgs e)
		{
			int battleId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["battle_Id"].Value);
			Form frm = new Forms.Replay(battleId);
			frm.ShowDialog(this);
		}


		private async Task dataGridMainPopup_TankWN8_Click(object sender, EventArgs e)
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
				DataTable dt = await DB.FetchData(sql);
				if (dt.Rows.Count > 0)
				{
					DataRow dr = dt.Rows[0];
					int tankId = Convert.ToInt32(dr["tankId"]);
					Code.Rating.WNHelper.RatingParameters rp = new Code.Rating.WNHelper.RatingParameters
					{
						BATTLES = Convert.ToInt32(dr["battles"]),
						DAMAGE = Convert.ToDouble(dr["dmg"]),
						SPOT = Convert.ToDouble(dr["spot"]),
						FRAGS = Convert.ToDouble(dr["frags"]),
						DEF = Convert.ToDouble(dr["def"]),
						WINS = Convert.ToDouble(dr["Wins"])
					};
					// Calc WN8 using total rating params
					double wn8 = Math.Round(Code.Rating.WN8.CalcTank(tankId, rp), 0);

					rp.DAMAGE = rp.DAMAGE / rp.BATTLES;
					rp.SPOT = rp.SPOT / rp.BATTLES;
					rp.FRAGS = rp.FRAGS / rp.BATTLES;
					rp.DEF = rp.DEF / rp.BATTLES;

					double wr = rp.WINS / rp.BATTLES * 100;
					double exp_dmg = Convert.ToDouble(dr["expDmg"]);
					double exp_spotted = Convert.ToDouble(dr["expSpot"]);
					double exp_frags = Convert.ToDouble(dr["expFrags"]);
					double exp_def = Convert.ToDouble(dr["expDef"]);
					double exp_wr = Convert.ToDouble(dr["expWR"]);
					Code.Rating.WN8.UseFormulaReturnResult(rp, wr, exp_dmg, exp_spotted, exp_frags, exp_def, exp_wr, out double rWINc, out double rDAMAGEc, out double rFRAGSc, out double rSPOTc, out double rDEFc);


					string message = "WN8 Rating for this tank (battle mode: Random/TC): ";

					message += wn8 + Environment.NewLine + Environment.NewLine;
					message += "Value" + "\t  " + "Result" + "\t" + "Expected" + "\t " + "WN8" + "\t " + "%" + Environment.NewLine;
					message += "-------------" + "\t  " + "----------" + "\t" + "------------" + "\t " + "------------" + "\t " + "-------" + Environment.NewLine;
					message += "Damage:" + "\t  " + Math.Round(rp.DAMAGE, 1).ToString() + "\t" + Math.Round(exp_dmg, 1).ToString() + "\t " + Math.Round(rDAMAGEc, 2) + "\t " + Math.Round(rDAMAGEc / wn8 * 100, 1) + Environment.NewLine;
					message += "Frags:" + "\t  " + Math.Round(rp.FRAGS, 2).ToString() + "\t" + Math.Round(exp_frags, 2).ToString() + "\t " + Math.Round(rFRAGSc, 2) + "\t " + Math.Round(rFRAGSc / wn8 * 100, 1) + Environment.NewLine;
					message += "Spot:" + "\t  " + Math.Round(rp.SPOT, 2).ToString() + "\t" + Math.Round(exp_spotted, 2).ToString() + "\t " + Math.Round(rSPOTc, 2) + "\t " + Math.Round(rSPOTc / wn8 * 100, 1) + Environment.NewLine;
					message += "Defence:" + "\t  " + Math.Round(rp.DEF, 2).ToString() + "\t" + Math.Round(exp_def, 2).ToString() + "\t " + Math.Round(rDEFc, 2) + "\t " + Math.Round(rDEFc / wn8 * 100, 1) + Environment.NewLine;
					message += "Win rate:" + "\t  " + Math.Round(wr, 2).ToString() + "%" + "\t" + Math.Round(exp_wr, 2).ToString() + "%" + "\t " + Math.Round(rWINc, 2) + "\t " + Math.Round(rWINc / wn8 * 100, 1) + Environment.NewLine;
					MsgBox.Show(message, "WN8 Tank Details");
				}
			}
			else
			{
				MsgBox.Show("This option is only available for tanks you own or has owned, with at least one battle played.", "WN8 Tank Details");
			}
		}

		private async void dataGridMainPopup_FilterOnTank_Click(object sender, EventArgs e)
		{
			int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
			int tankId = await TankHelper.GetTankID(playerTankId);
			if (tankId != 0)
			{
				//TankFilterMenuUncheck(true, true, true, false);
				MainSettings.GetCurrentGridFilter().TankId = tankId;
				await ShowView("Filtered on tank: " + TankHelper.GetTankName(tankId));
				CreateDataGridContextMenu(); // Recreate context menu
			}
		}

		private async void dataGridMainPopup_FilterOnTankSearch_Click(object sender, EventArgs e)
		{
			await SearchForTank();
		}

		private async void mTankFilter_Search_Click(object sender, EventArgs e)
		{
			await SearchForTank();
		}

		private async Task SearchForTank()
		{
			TankSearchHelper.OpenTankSearch(this);
			if (TankSearchHelper.Result == MsgBox.Button.OK && TankSearchHelper.SelectedTankId > 0)
			{
				MainSettings.GetCurrentGridFilter().TankId = TankSearchHelper.SelectedTankId;
				await ShowView("Filtered on tank: " + TankHelper.GetTankName(MainSettings.GetCurrentGridFilter().TankId));
				CreateDataGridContextMenu(); // Recreate context menu
			}
		}

		private async void dataGridMainPopup_FilterOnTankClear_Click(object sender, EventArgs e)
		{
			MainSettings.GetCurrentGridFilter().TankId = -1;
			await ShowView("Filtered on tank removed");
			CreateDataGridContextMenu(); // Recreate context menu
		}

		private async void dataGridMainPopup_FavListCreateNew_Click(object sender, EventArgs e)
		{
			if (dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value != DBNull.Value)
			{
				int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
				int tankId = await TankHelper.GetTankID(playerTankId);
				Form frm = new Forms.FavListNewEdit(0, "", tankId);
				frm.ShowDialog(this);
				// After fav list changes reload menu
				await SetFavListMenu(); // Reload fav list items
			}
		}

		private async void dataGridMainPopup_FavListAddTank_Click(object sender, EventArgs e)
		{
			if (dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value != DBNull.Value)
			{
				int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
				int tankId = await TankHelper.GetTankID(playerTankId);
				if (tankId != 0 && await FavListHelper.CheckIfAnyFavList(this, tankId, true))
				{
					Form frm = new Forms.FavListAddRemoveTank(tankId, true);
					frm.ShowDialog(this);
				}
			}
		}

		private async void dataGridMainPopup_FavListRemoveTank_Click(object sender, EventArgs e)
		{
			if (dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value != DBNull.Value)
			{
				int playerTankId = Convert.ToInt32(dataGridMain.Rows[dataGridRightClickRow].Cells["player_Tank_Id"].Value);
				int tankId = await TankHelper.GetTankID(playerTankId);
				if (tankId != 0 && await FavListHelper.CheckIfAnyFavList(this, tankId, false))
				{
					Form frm = new FavListAddRemoveTank(tankId, false);
					frm.ShowDialog(this);
					// refresh if tank removed
					if (FavListHelper.refreshGridAfterAddRemove)
					{
						try
						{
							int pos = dataGridMain.FirstDisplayedScrollingRowIndex;
							dataGridMain.Visible = false;
							await ShowView("Refresh after removed tank from favourite tank list");
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

		private async void dataGridMain_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
		{
			string colName = "";
			try
			{
				if (mainGridSaveColWidth && e.Column.HeaderText != "" && MainSettings.View != GridView.Views.Overall)
				{
					colName = dataGridMain.Columns[e.Column.HeaderText].HeaderText;
					int newWidth = e.Column.Width;
					dataGridMain.Columns[e.Column.HeaderText].Width = newWidth;
					await ColListHelper.SaveColWidth(e.Column.HeaderText, newWidth);
				}
			}
			catch (Exception ex)
			{
				await Log.LogToFile(ex, "Error saving column resize: [" + colName + "]");
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

		private async void scrollY_MouseDown(object sender, MouseEventArgs e)
		{
			scrollingY = true;
			await ScrollY();
		}

		private async void scrollY_MouseMove(object sender, MouseEventArgs e)
		{
			if (scrollingY) await ScrollY();
		}

		private void scrollY_MouseUp(object sender, MouseEventArgs e)
		{
			scrollingY = false;
		}

		private async Task ScrollY()
		{
			try
			{
				int posBefore = dataGridMain.FirstDisplayedScrollingRowIndex - frozenRows;
				dataGridMain.FirstDisplayedScrollingRowIndex = scrollY.ScrollPosition + frozenRows;
				if (posBefore != dataGridMain.FirstDisplayedScrollingRowIndex) Refresh();
			}
			catch (Exception ex)
			{
				await Log.LogToFile(ex);
				// throw;
			}

		}

		private async void dataGridMain_MouseWheel(object sender, MouseEventArgs e)
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
				await Log.LogToFile(ex);
				// throw;
			}
		}

		private async void scrollX_MouseDown(object sender, MouseEventArgs e)
		{
			scrollingX = true;
			await ScrollX();
		}

		private void scrollX_MouseUp(object sender, MouseEventArgs e)
		{
			scrollingX = false;
		}

		private async void scrollX_MouseMove(object sender, MouseEventArgs e)
		{
			if (scrollingX) await ScrollX();
		}

		private async Task ScrollX()
		{
			try
			{
				int posBefore = dataGridMain.FirstDisplayedScrollingColumnIndex;
				dataGridMain.FirstDisplayedScrollingColumnIndex = scrollX.ScrollPosition;
				if (posBefore != dataGridMain.FirstDisplayedScrollingColumnIndex) Refresh();
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

		#region App, DB and other Settings + Help/About + Chart

		private async void mAppSettings_Click(object sender, EventArgs e)
		{
			// Stop file watchers if running
			int runState = Config.Settings.dossierFileWathcherRun;
			if (runState == 1)
			{
				Config.Settings.dossierFileWathcherRun = 0;
				await SetListener();
			}

			string databaseFilename = Config.Settings.databaseFileName;
			string databaseName = Config.Settings.databaseName;
			ConfigData.dbType databateType = Config.Settings.databaseType;

			// Show dialog
			Form frm = new Settings.AppSettings(AppSettingsHelper.Tabs.Main);
			frm.ShowDialog(this);

			// Update main form title
			currentPlayerId = Config.Settings.playerId;
			SetFormTitle();

			// Check for api update
			if (DBVersion.RunDownloadAndUpdateTanks)
				await RunWotApi(true);

			// Check if new database is created, database should be present but no player should exist
			if (await DB.CheckConnection(true, this))
			{
				bool runDossier = false;
				// If no player selected, or changed db type run dosser check
				runDossier = (Config.Settings.playerId == 0 || databateType != Config.Settings.databaseType || DBVersion.RunDossierFileCheckWithForceUpdate);
				if (!runDossier)
				{
					// check if changed db according to dbtype
					if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
						runDossier = (databaseFilename != Config.Settings.databaseFileName);
					else
						runDossier = (databaseName != Config.Settings.databaseName);
				}
				if (runDossier)
				{
					MsgBox.Button result = MsgBox.Show("A new database is selected, perform initial battle fetch now?", "Start initial battle fetch", MsgBox.Type.OKCancel, this);
					if (result == MsgBox.Button.OK)
					{
						await RunAppStartupActions("Running initial battle fetch for new database...");
					}
				}
			}

			// Set new Layout if changed
			dataGridMain.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", Config.Settings.gridFontSize);
			dataGridMain.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", Config.Settings.gridFontSize);
			dataGridMain.RowHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", Config.Settings.gridFontSize);
			ColorRangeScheme.SetRatingColors();

			// Return to prev file watcher state
			if (runState != Config.Settings.dossierFileWathcherRun)
			{
				Config.Settings.dossierFileWathcherRun = runState;
			}
			// Update main form
			await SetListener();
			currentPlayerId = Config.Settings.playerId;
			SetFormTitle();
			await SetFavListMenu(); // Reload fav list items
			await SetColListMenu(); // Refresh column setup list now
									// mVBaddict.Visible = (Config.Settings.vBAddictShowToolBarMenu); // Show vbAddict Player Profil toolbar if upload activated

			// Upload battles to wotnumweb
			// await new Services.AppBattleUpload().Run(false);

			// Refresh data
			SetStatus2("Refreshed grid");
			await ChangeView(MainSettings.View, true);
		}

		private void toolItemViewChart_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.BattleChart(0);
			FormHelper.OpenFormToRightOfParent(this, frm);
		}

		private async void toolItemUpdateDataFromAPI_Click(object sender, EventArgs e)
		{
			await RunWotApi();
		}

		private async Task RunWotApi(bool autoRun = false)
		{
			double WNcurrentVer8 = await DBVersion.GetWNVersion(8);
			double WNcurrentVer9 = await DBVersion.GetWNVersion(9);
			Form frm = new Forms.UpdateFromApi(autoRun);
			frm.ShowDialog(this);
			// Disabled auto update WN8 because new version is set per day and WN9 is no longer relevant or gets updated
			//bool WNnewVer8 = (DBVersion.GetWNVersion(8) > WNcurrentVer8);
			//bool WNnewVer9 = (await DBVersion.GetWNVersion(9) > WNcurrentVer9);
			//if (WNnewVer9) //
			//{
			//    await RunRecalcBattleRating(true, WNnewVer8, WNnewVer9); 
			//    DBVersion.RunDossierFileCheckWithForceUpdate = true;
			//}
			await ShowView("Refreshed view");
		}

		private async void mRecalcBattleRatings_Click(object sender, EventArgs e)
		{
			// Stop file watchers if running
			int runState = Config.Settings.dossierFileWathcherRun;
			if (runState == 1)
			{
				Config.Settings.dossierFileWathcherRun = 0;
				await SetListener();
			}
			// Get What rating to recalc
			ToolStripMenuItem menu = (ToolStripMenuItem)sender;
			bool WN9 = (menu.Tag.ToString() == "WN9" || menu.Tag.ToString() == "ALL");
			bool WN8 = (menu.Tag.ToString() == "WN8" || menu.Tag.ToString() == "ALL");
			bool WN7 = (menu.Tag.ToString() == "WN7" || menu.Tag.ToString() == "ALL");
			bool EFF = (menu.Tag.ToString() == "EFF" || menu.Tag.ToString() == "ALL");
			bool POS = (menu.Tag.ToString() == "POS" || menu.Tag.ToString() == "ALL");
			// Show dialog
			Form frm = new Forms.RecalcBattleRating(false, WN9, WN8, WN7, EFF, POS);
			frm.ShowDialog(this);
			// Return to prev file watcher state
			if (runState != Config.Settings.dossierFileWathcherRun)
			{
				Config.Settings.dossierFileWathcherRun = runState;
				await SetListener();
			}
			await ShowView("Refreshed grid");
		}

		private async void mRecalcBattleCreditsPerTank_Click(object sender, EventArgs e)
		{
			// Stop file watchers if running
			int runState = Config.Settings.dossierFileWathcherRun;
			if (runState == 1)
			{
				Config.Settings.dossierFileWathcherRun = 0;
				await SetListener();
			}
			// Show dialog
			Form frm = new Forms.RecalcBattleCreditPerTank();
			frm.ShowDialog(this);
			// Return to prev file watcher state
			if (runState != Config.Settings.dossierFileWathcherRun)
			{
				Config.Settings.dossierFileWathcherRun = runState;
				await SetListener();
			}
			await ShowView("Refreshed grid");
		}

		private async Task RunRecalcBattleRating(bool autoRun, bool wn8, bool wn9, bool pos)
		{
			Form frm = new Forms.RecalcBattleRating(autoRun, wn9, wn8, false, false, pos);
			frm.ShowDialog(this);
			await ShowView("Refreshed grid");
		}

		private async Task RunRecalcBattleCreditsPerTank(bool autoRun = false)
		{
			Form frm = new Forms.RecalcBattleCreditPerTank(autoRun);
			frm.ShowDialog(this);
			await ShowView("Refreshed grid");
		}

		private async Task RunRecalcBattleKDratioCRdmg(bool autoRun = false)
		{
			Form frm = new Forms.RecalcBattleKDratioCRdmg(autoRun);
			frm.ShowDialog(this);
			await ShowView("Refreshed grid");
		}

		private async Task RunRecalcBattleMinTier()
		{
			Form frm = new Forms.RecalcBattleMinTier(true);
			frm.ShowDialog(this);
			await ShowView("Refreshed grid");
		}

		private async Task RunRecalcBattleMaxTier()
		{
			Form frm = new Forms.RecalcBattleMaxTier(true);
			frm.ShowDialog(this);
			await ShowView("Refreshed grid");
		}


		private async void toolItemSettingsRun_Click(object sender, EventArgs e)
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
			await Config.SaveConfig();
			await SetListener();
		}

		private async void mSettingsRunBattleCheck_Click(object sender, EventArgs e)
		{
			// Immediate run dossier file check
			await RunDossierFileCheck("Check for new battle...", false);
		}

		private async void mRecalcTankStatistics_Click(object sender, EventArgs e)
		{
			// Immediate run dossier file check with force update
			await RunDossierFileCheck("Recalculate Tank Stats...", true);
		}

		private async void mRecalcMinTierValues_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.RecalcBattleMinTier(false, true);
			frm.ShowDialog(this);
			await ShowView("Refreshed grid");
		}

		private async void mRecalcMaxTierValues_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.RecalcBattleMaxTier(false, true);
			frm.ShowDialog(this);
			await ShowView("Refreshed grid");
		}

		private async Task RunDossierFileCheck(string message, bool forceUpdate)
		{
			if (Dossier2db.Running)
				SetStatus2("Terminated new battle check, already running...");
			else
			{
				SetStatus2(message);
				Log.AddToLogBuffer("// " + message, true);
				await Dossier2json.ManualRunInBackground(message, forceUpdate, this);
				await Log.WriteLogBuffer();
			}
		}

		private void toolItemShowDbTables_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.DatabaseTable();
			FormHelper.OpenFormCenterOfParent(this, frm);
		}


		private void mHelpAbout_Click(object sender, EventArgs e)
		{
			Form frm = new Forms.About();
			frm.ShowDialog(this);
		}

		private async void mHelpCheckVersion_Click(object sender, EventArgs e)
		{
			await RunAppStartupAPI(true);
		}

		private async void mTankFilter_GetInGarage_Click(object sender, EventArgs e)
		{
			InGarageApiResult.status = "";
			Form frm = new Forms.InGarageApi();
			frm.ShowDialog(this);
			if (InGarageApiResult.status == "ok")
			{
				frm = new Forms.InGarageProcessData();
				frm.ShowDialog(this);
				if (InGarageApiResult.changeFavList)
				{
					// After fav list changes reload menu
					await SetFavListMenu(); // Reload fav list items
					await ShowView("Refreshed grid after 'In Garage' tank list updated"); // Refresh grid now
				}
			}
		}

		private void mSettingsAppLayout_Click(object sender, EventArgs e)
		{
			//Form frm = new Forms.ApplicationLayout();
			//frm.ShowDialog(this);
			//dataGridMain.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", Config.Settings.gridFontSize);
			//dataGridMain.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", Config.Settings.gridFontSize);
			//dataGridMain.RowHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", Config.Settings.gridFontSize);
			//ChangeView(MainSettings.View, true);
		}

		private void mExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private async void mWoT_Click(object sender, EventArgs e)
		{
			await StartWoTGame();
		}


		private async Task StartWoTGame()
		{
			string err = "";
			string msg = "";
			try
			{
				if (Config.Settings.wotGameStartType == ConfigData.WoTGameStartType.NotConfigured)
				{
					Form frm = new Forms.Settings.AppSettings(AppSettingsHelper.Tabs.WoTGameClient);
					frm.ShowDialog(this);
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
								filename = "wgc_api.exe";
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
							pProcess.StartInfo.FileName = workingDir + "\\" + filename;
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
						ProcessStartInfo psi = new ProcessStartInfo
						{
							RedirectStandardInput = true,
							RedirectStandardOutput = true,
							UseShellExecute = false,
							WindowStyle = ProcessWindowStyle.Hidden,
							CreateNoWindow = true,
							FileName = Config.Settings.wotGameRunBatchFile
						};
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
				await Log.LogToFile(ex);
				MsgBox.Show(err + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine, "Error running programs", this);
			}
		}

		private int timerWotAffnityCount = 0;
		private async void timerWoTAffnity_Tick(object sender, EventArgs e)
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
				await Log.LogToFile(ex);
			}

		}

		private void mWoTStartGameSettings_Click(object sender, EventArgs e)
		{
			//Form frm = new Forms.WoTGameClientSettings();
			//frm.ShowDialog(this);
		}

		private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (this.WindowState == FormWindowState.Minimized)
				{
					this.WindowState = mainFormWindowsState;
					this.Opacity = 100;
				}
				else
				{
					this.WindowState = FormWindowState.Minimized;
					this.Opacity = 0;
				}
			}
		}

		private void mSettingsShowLogFiles_Click(object sender, EventArgs e)
		{
			// opens the folder in explorer
			Process.Start("explorer.exe", Config.AppDataLogFolder);
		}

		private void mSettingsShowAppData_Click(object sender, EventArgs e)
		{
			// opens the folder in explorer
			Process.Start("explorer.exe", Config.AppDataBaseFolder);
		}

		private void mWotNumWebForum_Click(object sender, EventArgs e)
		{
			Process.Start($"{Constants.WotNumWebUrl()}");
		}



		#endregion

		#region Gadgets

		private async Task HomeViewCreate(string Status2Message)
		{
			ResizeNow();
			// First remove current controls
			List<Control> controlsRemove = new List<Control>();
			foreach (Control c in panelMainArea.Controls)
			{
				if (c.Name.Length > 1 && c.Name.Substring(0, 2) == "uc" || c.Name == "userControlPicGrid")
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
			PictureBox picGrid = new PictureBox
			{
				Width = 2000,
				Height = 1500,
				Top = 2,
				Left = 2,
				Image = result,
				Name = "userControlPicGrid",
				Visible = false
			};
			panelMainArea.Controls.Add(picGrid);

			// Add current gadgets
			await GadgetHelper.GetGadgets();
			foreach (GadgetHelper.GadgetItem gadget in GadgetHelper.gadgets)
			{
				panelMainArea.Controls.Add(gadget.control);
				Control[] c = panelMainArea.Controls.Find(gadget.control.Name, false);
				c[0].Height = gadget.height;
				c[0].Width = gadget.width;
				c[0].BringToFront();
			}
		}

		private async Task HomeViewRefresh(string Status2Message)
		{
			List<Control> controls = new List<Control>();
			foreach (Control c in panelMainArea.Controls)
			{
				if (c.Name.Substring(0, 2) == "uc")
				{
					await GadgetHelper.ControlDataBind(c);
				}
			}
			SetStatus2(Status2Message);
		}

		private async void mGadgetAdd(object sender, EventArgs e)
		{
			// Enable edit mode if not
			if (!mHomeViewEditMode.Checked)
			{
				mHomeViewEditMode.Checked = true;
				await GadgetEditModeChange();
			}
			// Get gadget
			ToolStripMenuItem menu = (ToolStripMenuItem)sender;
			string controlName = menu.Tag.ToString();
			// Create new gadget
			await GadgetCreateOrEdit(controlName);
		}

		private async void mHomeEdit_Click(object sender, EventArgs e)
		{
			mHomeViewEditMode.Checked = !mHomeViewEditMode.Checked;
			await GadgetEditModeChange();
		}

		private async Task GadgetEditModeChange()
		{
			if (mHomeViewEditMode.Checked)
			{
				// Enable edit style
				Status2AutoEnabled = false;
				timerStatus2.Enabled = false;
				lblStatus2.ForeColor = ColorTheme.FormBorderBlue;
				lblStatus2.Text = "Enabled Home View Edit Mode";
				// Add mouse move event for main panel
				panelMainArea.MouseMove += new MouseEventHandler(panelEditor_MouseMove);
				panelMainArea.MouseDown += new MouseEventHandler(panelEditor_MouseDown);
				panelMainArea.MouseUp += new MouseEventHandler(panelEditor_MouseUp);

				// Disable all gadgets
				foreach (Control c in panelMainArea.Controls)
				{
					if (c.Name.Substring(0, 2) == "uc" || c.Name == "userControlPicGrid")
					{
						c.Enabled = false;
					}
				}
				// Show grid
				Control[] userControlPicGrid = panelMainArea.Controls.Find("userControlPicGrid", false);
				userControlPicGrid[0].Visible = true;
			}
			else
			{
				// RemoveOwnedForm Grid
				Control[] userControlPicGrid = panelMainArea.Controls.Find("userControlPicGrid", false);
				userControlPicGrid[0].Visible = false;
				// Remove mouse move event for main panel
				panelMainArea.MouseMove -= panelEditor_MouseMove;
				panelMainArea.MouseDown -= panelEditor_MouseDown;
				panelMainArea.MouseUp -= panelEditor_MouseUp;
				panelMainArea.ContextMenuStrip = null;
				panelMainArea.Refresh(); // force paint event
										 // Enable all gadgets
				foreach (Control c in panelMainArea.Controls)
				{
					if (c.Name.Substring(0, 2) == "uc" || c.Name == "userControlPicGrid")
					{
						c.Enabled = true;
					}
				}
				// Enable default style
				Status2AutoEnabled = true;
				SetStatus2("Disabled Home View Edit Mode");
				await GadgetHelper.SortGadgets();
			}
		}

		//private GadgetHelper.GadgetItem lastSelectedGadget = null;
		private GadgetHelper.GadgetItem gadgetPontedAt = null; // current gadget mouse pointer at
		private GadgetHelper.GadgetItem gadgetSelected = null; // current gadget selected with mouse click
		private bool gadgetMoveOrRezizeMode = false;
		private bool gadgetLockMode = false;

		private async void panelEditor_MouseMove(object sender, MouseEventArgs e)
		{
			string actionText = "";
			// Check if move or resize mode
			if (!gadgetMoveOrRezizeMode && !gadgetLockMode)
			{
				// Not moving or resizing , just selecting area
				GadgetHelper.GadgetItem newSelectedGadget = GadgetHelper.FindGadgetFromLocation(e.X, e.Y);
				// Check if pointing on gadget 
				if (newSelectedGadget != null)
				{
					// Yes, poining on gadget - check if pointing on new gadget
					if (newSelectedGadget != gadgetPontedAt)
					{
						// New gadget selected
						if (gadgetPontedAt != null)
						{
							gadgetPontedAt.control.BackColor = ColorTheme.FormBack; // Remove selected border from previous selected gadget
						}
						gadgetPontedAt = newSelectedGadget;
						gadgetPontedAt.control.BackColor = ColorTheme.FormBackSelectedGadget;
						bool hasParam = await GadgetHelper.HasGadetParameter(gadgetPontedAt);
						CreateGadgetContextMenu(hasParam);
						panelMainArea.Refresh(); // force paint event
						actionText = " - Selected gadget: " + gadgetPontedAt.name.ToString();
					}
					else
					{
						// pointing on same gadget
						actionText = " - Selected gadget: " + gadgetPontedAt.name.ToString();
					}
					// Check if resizable to show correct cursor
					if (gadgetPontedAt.resizable)
					{
						if (e.X > gadgetPontedAt.control.Left + gadgetPontedAt.control.Width - 20 && e.Y > gadgetPontedAt.control.Top + gadgetPontedAt.control.Height - 20)
							Cursor = Cursors.SizeNWSE;
						else if (e.X > gadgetPontedAt.control.Left + gadgetPontedAt.control.Width - 10)
							Cursor = Cursors.SizeWE;
						else if (e.Y > gadgetPontedAt.control.Top + gadgetPontedAt.control.Height - 10)
							Cursor = Cursors.SizeNS;
					}
				}
				else
				{
					// No, poining on background
					if (gadgetPontedAt != null)
					{
						gadgetPontedAt.control.BackColor = ColorTheme.FormBack;
						gadgetPontedAt = null;
						panelMainArea.ContextMenuStrip = null;
						panelMainArea.Refresh(); // force paint event
					}
					// Return to default mouse cursor
					if (Cursor == Cursors.Help)
						Cursor = Cursors.Default;
				}
			}
			else if (!gadgetLockMode)
			{
				// Moving or rezising selected gadget
				int gridSize = 10;
				if (Cursor == Cursors.SizeAll)
				{
					gadgetPontedAt.control.Top = selectedGadgetTop + (Convert.ToInt32((Cursor.Position.Y - mouseDownY) / gridSize) * gridSize);
					gadgetPontedAt.control.Left = selectedGadgetLeft + (Convert.ToInt32((Cursor.Position.X - mouseDownX) / gridSize) * gridSize);
					actionText = " - Moving gadget: " + gadgetPontedAt.name.ToString();
				}
				else if (Cursor == Cursors.SizeWE)
				{
					actionText = " - Resize horizontal gadget: " + gadgetPontedAt.name.ToString();
					gadgetPontedAt.control.BackColor = ColorTheme.FormBack;
					int newSize = gadgetPontedAt.width + (Convert.ToInt32((Cursor.Position.X - mouseDownX) / gridSize) * gridSize);
					gadgetPontedAt.control.Width = newSize;
					gadgetPontedAt.control.BackColor = ColorTheme.FormBackSelectedGadget;
				}
				else if (Cursor == Cursors.SizeNS)
				{
					actionText = " - Resize vertical gadget: " + gadgetPontedAt.name.ToString();
					gadgetPontedAt.control.BackColor = ColorTheme.FormBack;
					int newSize = gadgetPontedAt.height + (Convert.ToInt32((Cursor.Position.Y - mouseDownY) / gridSize) * gridSize);
					gadgetPontedAt.control.Height = newSize;
					gadgetPontedAt.control.BackColor = ColorTheme.FormBackSelectedGadget;
				}
				else if (Cursor == Cursors.SizeNWSE)
				{
					actionText = " - Resize gadget: " + gadgetPontedAt.name.ToString();
					gadgetPontedAt.control.BackColor = ColorTheme.FormBack;
					int newSize = gadgetPontedAt.height + (Convert.ToInt32((Cursor.Position.Y - mouseDownY) / gridSize) * gridSize);
					gadgetPontedAt.control.Height = newSize;
					newSize = gadgetPontedAt.width + (Convert.ToInt32((Cursor.Position.X - mouseDownX) / gridSize) * gridSize);
					gadgetPontedAt.control.Width = newSize;
					gadgetPontedAt.control.BackColor = ColorTheme.FormBackSelectedGadget;
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
			if (e.Button == System.Windows.Forms.MouseButtons.Left && gadgetPontedAt != null)
			{
				// move or resize gadget mode
				gadgetMoveOrRezizeMode = true;
				if (gadgetPontedAt.resizable)
				{
					if (e.X > (gadgetPontedAt.control.Left + gadgetPontedAt.control.Width - 20) && e.Y > (gadgetPontedAt.control.Top + gadgetPontedAt.control.Height - 20))
						Cursor = Cursors.SizeNWSE;
					else if (e.X > (gadgetPontedAt.control.Left + gadgetPontedAt.control.Width - 10))
						Cursor = Cursors.SizeWE;
					else if (e.Y > (gadgetPontedAt.control.Top + gadgetPontedAt.control.Height - 10))
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
				selectedGadgetTop = gadgetPontedAt.control.Top;
				selectedGadgetLeft = gadgetPontedAt.control.Left;
				gadgetPontedAt.control.BringToFront();
			}
			if (e.Button == System.Windows.Forms.MouseButtons.Right && gadgetPontedAt != null)
			{
				// move mode off
				gadgetMoveOrRezizeMode = false;
				Cursor = Cursors.Default;
			}
		}

		private async void panelEditor_MouseUp(object sender, MouseEventArgs e)
		{
			if (gadgetPontedAt != null)
			{
				// Save new location if moved 
				if (gadgetPontedAt.posY != gadgetPontedAt.control.Top || gadgetPontedAt.posX != gadgetPontedAt.control.Left)
				{
					await GadgetHelper.SaveGadgetPosition(gadgetPontedAt.id, gadgetPontedAt.control.Left, gadgetPontedAt.control.Top);
					gadgetPontedAt.posY = gadgetPontedAt.control.Top;
					gadgetPontedAt.posX = gadgetPontedAt.control.Left;
				}
				// Save new size if resized
				if (gadgetPontedAt.width != gadgetPontedAt.control.Width || gadgetPontedAt.height != gadgetPontedAt.control.Height)
				{
					gadgetPontedAt.height = gadgetPontedAt.control.Height;
					gadgetPontedAt.width = gadgetPontedAt.control.Width;
					await GadgetHelper.SaveGadgetSize(gadgetPontedAt);
				}
			}
			// move mode off
			gadgetMoveOrRezizeMode = false;
		}


		private async void mHomeViewRefresh_Click(object sender, EventArgs e)
		{
			await HomeViewCreate("Redrawn gadgets");
			await HomeViewRefresh("Refresh redrawn gadgets");
			await GadgetEditModeChange();
		}

		private void CreateGadgetContextMenu(bool customizeAvailable)
		{
			// Datagrid context menu (Right click on Grid)
			ContextMenuStrip gadgetMainPopup = new ContextMenuStrip
			{
				Renderer = new StripRenderer(),
				BackColor = ColorTheme.ToolGrayMainBack
			};
			// Items
			ToolStripSeparator gadgetMainPopup_Separator1 = new ToolStripSeparator();
			ToolStripMenuItem gadgetMainPopup_Remove = new ToolStripMenuItem("Remove Gadget");
			gadgetMainPopup_Remove.Click += new EventHandler(gadgetMainPopup_Remove_Click);
			ToolStripMenuItem gadgetMainPopup_Customize = new ToolStripMenuItem("Customize Gadget");
			gadgetMainPopup_Customize.Click += new EventHandler(gadgetMainPopup_Customize_Click);
			// Add events
			gadgetMainPopup.Opening += new System.ComponentModel.CancelEventHandler(gadgetMainPopup_Opening);
			gadgetMainPopup.Closed += new ToolStripDropDownClosedEventHandler(gadgetMainPopup_Closing);

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
			if (gadgetPontedAt == null)
			{
				e.Cancel = true; // Close if validation fails
			}
			else
			{
				gadgetLockMode = true; // lock selected gadget after popup opens
				gadgetSelected = gadgetPontedAt;
			}
		}

		private void gadgetMainPopup_Closing(object sender, ToolStripDropDownClosedEventArgs e)
		{
			gadgetLockMode = false; // free selected gadget after popup closed
		}

		private async void gadgetMainPopup_Remove_Click(object sender, EventArgs e)
		{
			try
			{
				if (gadgetSelected != null)
				{
					await GadgetHelper.RemoveGadget(gadgetSelected);
					panelMainArea.Controls.Remove(gadgetSelected.control);
					gadgetSelected = null;
				}
			}
			catch (Exception)
			{
				// throw;
			}

		}

		private async void gadgetMainPopup_Customize_Click(object sender, EventArgs e)
		{
			if (gadgetSelected != null)
			{
				// Edit gadget
				await GadgetCreateOrEdit(gadgetSelected.controlName, gadgetSelected.id);
			}
		}

		private async Task GadgetCreateOrEdit(string controlName, int gadgetId = -1) // if gadgetId == -1 then create new
		{
			GadgetHelper.newParameters = new object[20];
			GadgetHelper.newParametersOK = true;
			// Get form to customize parameters
			Form frm = null;
			switch (controlName)
			{
				case "ucGaugePR":
					frm = new Gadget.paramTimeSpan(gadgetId);
					break;
				case "ucGaugeWN9":
					frm = new Gadget.paramTimeSpan(gadgetId);
					break;
				case "ucGaugeWN8":
					frm = new Gadget.paramTimeSpan(gadgetId);
					break;
				case "ucGaugeWN7":
					frm = new Gadget.paramTimeSpan(gadgetId);
					break;
				case "ucGaugeEFF":
					frm = new Gadget.paramTimeSpan(gadgetId);
					break;
				case "ucGaugeWinRate":
					frm = new Gadget.paramBattleMode(gadgetId);
					break;
				case "ucGaugeRWR":
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
				case "ucTotalTanks":
					frm = new Gadget.paramBattleModeOnly(gadgetId);
					break;
				case "ucHeading":
					frm = new Gadget.paramText(gadgetId);
					break;
				case "ucTotalStats":
					frm = new Gadget.paramTotalStats(gadgetId);
					break;
				case "ucTotalStatsDefault":
					controlName = "ucTotalStats";
					frm = new Gadget.paramTotalStats(gadgetId, true);
					break;
			}
			if (frm != null)
				frm.ShowDialog(this);
			// Check gadget (after params are added)
			if (GadgetHelper.newParametersOK)
			{
				GadgetHelper.GadgetItem gadget = null;
				Control gadgetControl = null;
				// Create new gadget
				if (gadgetId == -1)
				{
					gadgetControl = await GadgetHelper.GetGadgetControl(controlName, GadgetHelper.newParameters);
					gadget = new GadgetHelper.GadgetItem
					{
						control = gadgetControl,
						controlName = controlName,
						height = gadgetControl.Height,
						width = gadgetControl.Width,
						posX = 2,
						posY = 2
					};
					gadget.control.Top = gadget.posX;
					gadget.control.Left = gadget.posY;
					gadget.sortorder = -1;
					gadget.visible = true;
					gadget.resizable = GadgetHelper.IsGadgetRezisable(controlName);
					gadget.name = GadgetHelper.GetGadgetName(controlName);
					gadgetId = await GadgetHelper.InsertNewGadget(gadget);
					gadget.id = gadgetId;
					gadgetControl.Name = "uc" + gadgetId.ToString();
				}
				// Edit gadget
				else
				{
					// Get selected gadget for saving
					gadget = gadgetSelected;
					// Remove Current added instance of gadget
					panelMainArea.Controls.Remove(gadget.control);
					// get updated control
					gadgetControl = await GadgetHelper.GetGadgetControl(gadget.controlName, GadgetHelper.newParameters);
					gadgetControl.Name = "uc" + gadget.id.ToString();
					gadgetControl.Tag = gadget.name;
					gadgetControl.Top = gadget.posY;
					gadgetControl.Left = gadget.posX;
					gadgetControl.Height = gadget.height;
					gadgetControl.Width = gadget.width;
					gadget.control = gadgetControl;
				}
				// Save gadget parameters
				await GadgetHelper.SaveGadgetParameter(gadget);
				// Special gadgets customization
				switch (gadget.controlName)
				{
					case "ucBattleListLargeImages":
						gadget.width = (Convert.ToInt32(GadgetHelper.newParameters[0]) * 170) - 10;
						gadget.height = (Convert.ToInt32(GadgetHelper.newParameters[1]) * 120) - 10;
						await GadgetHelper.SaveGadgetSize(gadget);
						break;
					case "ucTotalStats":
						// calc new width = each col = 200px + separators = 40 if more than one col
						int newWidth = (Convert.ToInt32(GadgetHelper.newParameters[2]) * 200) + (Convert.ToInt32(GadgetHelper.newParameters[2]) - 1) * 20;
						// calc new height = 22px per row * num of grid lines = params - fixed params, 70px is header + gridheader + footer
						int newHeight = (22 * (GadgetHelper.newParameters.Length - 6)) + 75;
						bool save = false;
						if (newWidth > gadget.width)
						{
							gadget.width = newWidth;
							save = true;
						}
						if (newHeight > gadget.height)
						{
							gadget.height = newHeight;
							save = true;
						}
						// Check that height is aligned to grid (10)
						int addHeight = 10 - (gadget.height % 10);
						if (addHeight > 0 && addHeight < 10)
						{
							gadget.height += addHeight;
							save = true;
						}
						// Save
						if (save)
							await GadgetHelper.SaveGadgetSize(gadget);
						break;
				}
				// Add to panel and resize
				panelMainArea.Controls.Add(gadget.control);
				Control[] c = panelMainArea.Controls.Find(gadget.control.Name, false);
				Control panelControl = c[0];
				panelControl.Height = gadget.height;
				panelControl.Width = gadget.width;
				panelControl.BringToFront();
				panelControl.Enabled = false;
				panelControl.Tag = gadget.controlName;
				// DataBind
				await GadgetHelper.ControlDataBind(panelControl);
			}
			gadgetSelected = null;
		}

		private async void mGadgetRemoveAll_Click(object sender, EventArgs e)
		{
			MsgBox.Button answer = MsgBox.Show("This will remove all gadgets.", "Remove all gadgets", MsgBox.Type.OKCancel, this);
			if (answer == MsgBox.Button.OK)
			{
				await GadgetHelper.RemoveGadgetAll();
				await HomeViewCreate("Removed all gadgets");
				await HomeViewRefresh("Refresh Home View");
			}
		}

		private async void mHomeViewDefaults_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;
			string file = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\" + menuitem.Tag.ToString();
			if (File.Exists(file))
			{
				await LoadHomeViewFile(menuitem, file);
			}
			else
			{
				MsgBox.Show("Cannot locate file: " + file + Environment.NewLine + Environment.NewLine + "Please reinstall Wot Numbers" + Environment.NewLine + Environment.NewLine, "Missing file", this);
			}
		}

		private async void mHomeViewRecent_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;
			string file = menuitem.Tag.ToString();
			if (File.Exists(file))
			{
				await LoadHomeViewFile(menuitem, file);
			}
			else
			{
				MsgBox.Show("Cannot locate file: " + file + Environment.NewLine + Environment.NewLine + "The menu item will be removed." + Environment.NewLine + Environment.NewLine, "Missing file", this);
				await GadgetHelper.RemoveRecentHomeView(file);
				await GetHomeViewRecentList();
			}
		}

		private async Task LoadHomeViewFile(ToolStripMenuItem menuitem, string file)
		{
			bool changeHomeView = true;
			if (!GadgetHelper.HomeViewSaved)
			{
				MsgBox.Button answer = MsgBox.Show("Du you want to save current home view before changing to: '" + menuitem.Text + "' home view?", "Save home view", MsgBox.Type.YesNo, this);
				if (answer == MsgBox.Button.Yes)
				{
					changeHomeView = await SaveHomeView();
				}
			}
			if (changeHomeView)
			{
				if (await GadgetHelper.HomeViewLoadFromFile(file, true))
				{
					mHomeViewEditMode.Checked = false;
					await GadgetEditModeChange();
					RemoveHomeViewSelectedMenuItems();
					menuitem.Checked = true;
					mHomeView.Text = menuitem.Text;
					Config.Settings.currentHomeView = mHomeView.Text;
					await Config.SaveConfig();
					await HomeViewCreate("Change to: '" + menuitem.Text + "' home view");
					await HomeViewRefresh("Refreshed home view");
				}

			}
		}

		private async void mHomeViewFileSave_Click(object sender, EventArgs e)
		{
			await SaveHomeView();
		}

		private async Task<bool> SaveHomeView()
		{

			saveFileDialog1.FileName = "";
			saveFileDialog1.DefaultExt = "*.json";
			saveFileDialog1.Filter = "Wot Numbers Home View files (*.json)|*.json|All files (*.*)|*.*";
			saveFileDialog1.InitialDirectory = Config.AppDataHomeViewFolder;
			DialogResult result = saveFileDialog1.ShowDialog(); // dialogbox checks for overwrite
			if (result == DialogResult.OK)
			{
				string fileName = Path.GetFileName(saveFileDialog1.FileName);
				await GadgetHelper.HomeViewSaveToFile(saveFileDialog1.FileName);
				bool updatedRecentList = await GadgetHelper.UpdateRecentHomeView(saveFileDialog1.FileName);
				if (updatedRecentList)
					await GetHomeViewRecentList();
				mHomeView.Text = fileName.Replace(".json", "");
				Config.Settings.currentHomeView = mHomeView.Text;
				CheckCurrentHomeViewSubMenu();
				await Config.SaveConfig();
				return true;
			}
			return false;
		}

		private async void mHomeViewFileLoad_Click(object sender, EventArgs e)
		{
			bool changeHomeView = true;
			if (!GadgetHelper.HomeViewSaved)
			{
				MsgBox.Button answer = MsgBox.Show("Du you want to save current home view loading new home view from file?", "Save home view", MsgBox.Type.YesNo, this);
				if (answer == MsgBox.Button.Yes)
				{
					changeHomeView = await SaveHomeView();
				}
			}
			if (changeHomeView)
			{
				openFileDialog1.FileName = "*.json";
				openFileDialog1.Filter = "Wot Numbers Home View files (*.json)|*.json|All files (*.*)|*.*";
				openFileDialog1.InitialDirectory = Config.AppDataHomeViewFolder;
				DialogResult result = openFileDialog1.ShowDialog();
				if (result == DialogResult.OK)
				{
					await GadgetHelper.HomeViewLoadFromFile(openFileDialog1.FileName, true);
					bool updatedRecentList = await GadgetHelper.UpdateRecentHomeView(openFileDialog1.FileName);
					if (updatedRecentList)
						await GetHomeViewRecentList();
					string fileName = Path.GetFileName(openFileDialog1.FileName);
					mHomeView.Text = fileName.Replace(".json", "");
					Config.Settings.currentHomeView = mHomeView.Text;
					CheckCurrentHomeViewSubMenu();
					await Config.SaveConfig();
					await HomeViewCreate("Redraw loaded Home View from file");
					await HomeViewRefresh("Refresh loaded Home View from file");
				}
			}

		}

		private async void mHomeViewClearRecentList_Click(object sender, EventArgs e)
		{
			await GadgetHelper.RemoveRecentHomeView("");
			await GetHomeViewRecentList();
		}

		private void RemoveHomeViewSelectedMenuItems()
		{
			mHomeViewDefault.Checked = false;
			mHomeViewClassic.Checked = false;
			mHomeViewRecent1.Checked = false;
			mHomeViewRecent2.Checked = false;
			mHomeViewRecent3.Checked = false;
			mHomeViewRecent4.Checked = false;
			mHomeViewRecent5.Checked = false;
		}

		private void CheckCurrentHomeViewSubMenu()
		{
			RemoveHomeViewSelectedMenuItems();
			if (mHomeView.Text == mHomeViewDefault.Text) mHomeViewDefault.Checked = true;
			else if (mHomeView.Text == mHomeViewClassic.Text) mHomeViewClassic.Checked = true;
			else if (mHomeView.Text == mHomeViewRecent1.Text) mHomeViewRecent1.Checked = true;
			else if (mHomeView.Text == mHomeViewRecent2.Text) mHomeViewRecent2.Checked = true;
			else if (mHomeView.Text == mHomeViewRecent3.Text) mHomeViewRecent3.Checked = true;
			else if (mHomeView.Text == mHomeViewRecent4.Text) mHomeViewRecent4.Checked = true;
			else if (mHomeView.Text == mHomeViewRecent5.Text) mHomeViewRecent5.Checked = true;
		}

		private void mHomeViewFileShowFolder_Click(object sender, EventArgs e)
		{
			// opens the folder in explorer
			Process.Start("explorer.exe", Config.AppDataHomeViewFolder);
		}
		#endregion

	}
}
