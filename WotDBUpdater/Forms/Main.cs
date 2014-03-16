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
            menuMain.Renderer = new MyToolStripRenderer();
            menuMain.BackColor = Color.FromArgb(255, 45, 45, 45);
            panelTop.BackColor = Color.FromArgb(255, 45, 45, 45);
            // Size
            RefreshForm();
            // Startup settings
            string msg = Config.GetConfig();
            if (msg != "") 
            {
                MessageBoxEx.Show(this, msg,"Could not load config data");
            }
            else if (Config.CheckDBConn())
            {
                string result = dossier2json.updateDossierFileWatcher();
                SetStatus2(result);
                SetListener();
                SetFormTitle();
                // Init
                TankData.GetTankListFromDB();
                TankData.GetJson2dbMappingViewFromDB();
                TankData.GettankData2BattleMappingViewFromDB();
            }
            
            SetStatus2("Application started");
        }

        #region layout

        class MyToolStripRenderer : ToolStripProfessionalRenderer
        {
            public MyToolStripRenderer()
                : base(new Code.Support.MenuStripLayout())
            {
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                base.OnRenderItemText(e);
                e.Item.ForeColor = Color.FromArgb(255, 240, 240, 240);
            }
        }

        private void panelMaster_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panelMaster.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }
                
        #endregion

        #region mainEvents

        private int status2DefaultColor = 200;
        private int status2fadeColor = 200;

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
                    lblStatus2.ForeColor = Color.FromArgb(255, status2fadeColor, status2fadeColor, status2fadeColor);
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
            timerStatus2.Interval = 10000;
            lblStatus2.ForeColor = Color.FromArgb(255, status2DefaultColor, status2DefaultColor, status2DefaultColor);
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
            menuItemRunStopToggle.Checked = (Config.Settings.run == 1);
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
            string result = dossier2json.updateDossierFileWatcher();
            SetStatus2(result);
        }
        
        #endregion

        #region moveForm

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

        #region resizeForm

        private bool moving = false;
        private Point moveFromPoint;
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
                RefreshForm();
            }
        }

        private void picResize_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
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
            picClose.BackColor = Code.Support.MenuStripLayout.colorGrayHover;
        }

        private void picClose_MouseLeave(object sender, EventArgs e)
        {
            picClose.BackColor = Code.Support.MenuStripLayout.colorGrayMain;
        }

        private void picNormalize_MouseHover(object sender, EventArgs e)
        {
            picNormalize.BackColor = Code.Support.MenuStripLayout.colorGrayHover;
        }

        private void picNormalize_MouseLeave(object sender, EventArgs e)
        {
            picNormalize.BackColor = Code.Support.MenuStripLayout.colorGrayMain;
        }

        private void picMinimize_MouseHover(object sender, EventArgs e)
        {
            picMinimize.BackColor = Code.Support.MenuStripLayout.colorGrayHover;
        }

        private void picMinimize_MouseLeave(object sender, EventArgs e)
        {
            picMinimize.BackColor = Code.Support.MenuStripLayout.colorGrayMain;
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
            RefreshForm();
        }

        private void picMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void RefreshForm()
        {
            Refresh();
            // Title bar
            panelTop.Left = 1;
            panelTop.Top = 1;
            // Title bar form handling
            picClose.Left = panelMain.Width - picClose.Width;
            picNormalize.Left = picClose.Left - picNormalize.Width;
            picMinimize.Left = picNormalize.Left - picMinimize.Width;
            // Main Area including menu
            panelMain.Left = 1;
            panelMain.Top = panelTop.Height + 1;
            panelMain.Height = panelMaster.Height - panelTop.Height - 2;
            // Status bar
            panelStatus.Left = 1;
            panelStatus.Top = panelMaster.Height - panelStatus.Height - 1;
            panelStatus.Width = panelMaster.Width - 2;
            // Status bar resize handling
            picResize.Left = panelStatus.Width - picResize.Width;
            picResize.Visible = (this.WindowState != FormWindowState.Maximized);
            
        }

        #endregion

        #region menuItemAction

        private void menuItemAppSettings_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.File.ApplicationSetting();
            frm.ShowDialog();
            SetFormTitle();
        }

        private void menuItemDbSettings_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.File.DatabaseSetting();
            frm.ShowDialog();
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItemReportView_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.Reports.DBView();
            frm.Show();
        }

        private void menuItemReportTable_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.Reports.DBTable();
            frm.Show();
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.Help.About();
            frm.ShowDialog();
        }

        private void menuItemRunStopToggle_Click(object sender, EventArgs e)
        {
            menuItemRunStopToggle.Checked = !menuItemRunStopToggle.Checked;
            // Set Start - Stop button properties
            if (menuItemRunStopToggle.Checked)
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

        private void menuItemManualRun_Click(object sender, EventArgs e)
        {
            // Dossier file manual handling
            SetStatus2("Starting manual dossier check...");
            string result = dossier2json.manualRun();
            SetStatus2(result);
        }

        private void menuItemRunPrev_Click(object sender, EventArgs e)
        {
            // Test running previous dossier file
            SetStatus2("Starting check on previous dossier file...");
            string result = dossier2json.manualRun(true);
            SetStatus2(result);
        }

        private void menuItemRunPrevForceUpdate_Click(object sender, EventArgs e)
        {
            // Test running previous dossier file, force update - even if no more battles is detected
            SetStatus2("Starting check on previous dossier file with force update...");
            string result = dossier2json.manualRun(true, true);
            SetStatus2(result);
        }

        #endregion

        #region menuItem_TESTING

        private void menuItemTest_ShowCountry_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.Test.CountryInGrid();
            frm.ShowDialog();
        }

        private void menuItemTest_AddCountry_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.Test.AddCountryToTable();
            frm.ShowDialog();
        }

        private void menuItemTest_ListTanks_Click(object sender, EventArgs e)
        {
            string s = TankData.ListTanks();
            MessageBoxEx.Show(this, s);
        }

        private void menuItemTest_ImportTank_Wn8exp_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.File.ImportTank();
            frm.ShowDialog();
        }

        private void menuItemTest_ImportTurret_Click(object sender, EventArgs e)
        {
            string s = Modules2DB.ImportTurrets();
            MessageBoxEx.Show(this, s);
        }

        private void menuItemTest_ImportGun_Click(object sender, EventArgs e)
        {
            string s = Modules2DB.ImportGuns();
            MessageBoxEx.Show(this, s);
        }

        private void menuItemTest_ImportRadio_Click(object sender, EventArgs e)
        {
            string s = Modules2DB.ImportRadios();
            MessageBoxEx.Show(this, s);
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
            MessageBoxEx.Show(this, content);
        }

        private void menuItemTest_ProgressBar_Click(object sender, EventArgs e)
        {
            Form frm = new Forms.Test.TestProgressBar();
            frm.Show();
        }

        private void menuItemTest_ViewRange_Click(object sender, EventArgs e)
        {
            string vr = ViewRange.CalcViewRange().ToString();
            MessageBoxEx.Show(this,vr,"Test calc view range");
        }

        #endregion

    }

    

}
