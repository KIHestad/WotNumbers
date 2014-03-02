using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotDBUpdater.Forms.Test
{
    public partial class frmTestProgressBar : Form
    {

        private readonly BackgroundWorker _bw = new BackgroundWorker();

        public frmTestProgressBar()
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
            importTanks2DB.UpdateWN8();
            MessageBox.Show("Update complete");
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            progressBar.Show();
            _bw.RunWorkerAsync();
            
        }
    }
}
