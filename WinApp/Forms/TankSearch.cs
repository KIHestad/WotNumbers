using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
    public partial class TankSearch : Form
    {
        public TankSearch()
        {
            InitializeComponent();
        }

        private void TankSearch_Load(object sender, EventArgs e)
        {
            // Style toolbar
            toolStripMain.Renderer = new StripRenderer();
        }
    }
}
