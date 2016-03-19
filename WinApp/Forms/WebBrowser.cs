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
    public partial class WebBrowser : Form
    {
        private ChromiumWebBrowser browser { get; set; }
        private string _address = "http://wotnumbers.com/Forum.aspx?menu=6&_Forum";

        public WebBrowser(string address = "")
        {
            InitializeComponent();
            if (address != "")
                _address = address;
        }

        private void WebBrowser_Load(object sender, EventArgs e)
        {
            // Position browser window panel
            pnlBrowser.Top = WebBrowserTheme.TitleHeight;
            pnlBrowser.Left = 1;
            pnlBrowser.Width = WebBrowserTheme.Width - 2;
            pnlBrowser.Height = WebBrowserTheme.Height - WebBrowserTheme.TitleHeight - WebBrowserTheme.FormFooterHeight;
            
            // Create browser go to default page
            browser = new ChromiumWebBrowser(_address);
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
                //btnGoTo.Enabled = (browser.Address != adPage);
            });
            
        }



    }
}
