using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WotDBUpdater.Code;

namespace WotDBUpdater.Forms.Test
{
    public partial class TestProgressBar : Form
    {

        private readonly BackgroundWorker _bw = new BackgroundWorker();

        public TestProgressBar()
        {
            InitializeComponent();

            progressBar.MarqueeAnimationSpeed = 20;
            progressBar.Visible = false;

            _bw.DoWork += Import;
            _bw.RunWorkerCompleted += BwRunWorkerCompleted;
        }

        private void BwRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // hide the progress bar when the long running process finishes
            progressBar.Hide();
        }

        private static void Import(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            ImportMisc2DB.UpdateWN8();
            Code.MsgBox.Show("Import status","Done");
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            progressBar.Show();
            _bw.RunWorkerAsync();
        }
    }
}
