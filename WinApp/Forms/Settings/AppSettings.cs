using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms.Settings
{
    public partial class AppSettings : Form
    {
        private static AppSettingsHelper.Tabs tab { get; set; }

        public AppSettings(AppSettingsHelper.Tabs showTab)
        {
            InitializeComponent();
            SelectTab(showTab);
        }

        private void AppSettings_Load(object sender, EventArgs e)
        {

        }

        private void SelectTab_Click(object sender, EventArgs e)
        {
            BadButton bb = (BadButton)sender;
            // Get Enum from value
            AppSettingsHelper.Tabs selectedTab = (AppSettingsHelper.Tabs)Enum.Parse(typeof(AppSettingsHelper.Tabs), bb.Tag.ToString());
            SelectTab(selectedTab);
        }

        private void SelectTab(AppSettingsHelper.Tabs showTab)
        {
            // Deselect all tabs
            btnTab1.Checked = false;
            btnTab2.Checked = false;
            btnTab3.Checked = false;
            btnTab4.Checked = false;
            btnTab5.Checked = false;
            btnTab6.Checked = false;
            // Remove current control
            List<Control> cList = pnlMain.Controls.Find("ctrl", false).ToList();
            foreach (Control c in cList)
            {
                pnlMain.Controls.Remove(c);
            }
            Control control = null;
            // Select tab and user control
            switch (showTab)
            {
                case AppSettingsHelper.Tabs.Main:
                    btnTab1.Checked = true;
                    control = new Forms.Settings.AppSettingsMain();
                    break;
                case AppSettingsHelper.Tabs.Layout:
                    btnTab2.Checked = true;
                    control = new Forms.Settings.AppSettingsLayout();
                    break;
                case AppSettingsHelper.Tabs.WoTGameClient:
                    btnTab3.Checked = true;
                    control = new Forms.Settings.AppSettingsWoT();
                    break;
                case AppSettingsHelper.Tabs.vBAddict:
                    btnTab4.Checked = true;
                    break;
                case AppSettingsHelper.Tabs.Import:
                    btnTab5.Checked = true;
                    control = new Forms.Settings.AppSettingsImport();
                    break;
                case AppSettingsHelper.Tabs.Replay:
                    btnTab6.Checked = true;
                    break;
            }
            // Load usercontorl = content if any defined
            if (control != null)
            {
                control.Name = "ctrl";
                pnlMain.Controls.Add(control);
                control.Dock = DockStyle.Fill;
                Control[] c = pnlMain.Controls.Find("ctrl", false);
                c[0].BringToFront();
                pnlMain.Visible = true;
            }
            else
            {
                pnlMain.Visible = false;
            }
        }

        
    }
}
