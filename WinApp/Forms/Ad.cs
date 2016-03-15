using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;
using WinApp.Code.CefBrowser;

namespace WinApp.Forms
{
    public partial class Ad : Form
    {
        private ChromiumWebBrowser browser { get; set; }
        private const string adPage = "http://wotnumbers.com/AdSense.aspx";

        public Ad()
        {
            InitializeComponent();
        }

        private void Ad_Load(object sender, EventArgs e)
        {
            // Create browser go to default page
            browser = new ChromiumWebBrowser(adPage);
            // browser.LoadingStateChanged += OnLoadingStateChanged;
            browser.AddressChanged += OnAddressChanged;
            browser.Dock = DockStyle.Fill;
            browser.BackColor = ColorTheme.FormBack;
            pnlBrowser.Controls.Add(browser);
            pnlBrowser.Visible = true;
        }

        private void OnLoadingStateChanged(Object sender, LoadingStateChangedEventArgs args)
        {
            
        }

        // When browser address has changed, check if another than ad page is show to enable goto-button
        private void OnAddressChanged(Object sender, AddressChangedEventArgs args)
        {
            this.Invoke((MethodInvoker)delegate
            {
                btnGoTo.Enabled = (browser.Address != adPage);
            });
            
        }

        // SHow next ad
        private void btnNextAd_Click(object sender, EventArgs e)
        {
            // Refresh browser by requesting same page
            browser.Load(adPage);
        }

        // Open default web browser at current page
        private void btnGoTo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(browser.Address);
        }

        // Close form
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
