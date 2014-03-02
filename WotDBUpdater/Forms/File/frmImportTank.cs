using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotDBUpdater.Forms
{
    public partial class frmImportTank : Form
    {
        public frmImportTank()
        {
            InitializeComponent();
        }

        void Log(string logtext, bool addTime = false)
        {
            // log to ListBox and scroll to bottom
            string timestamp = "";
            if (addTime) timestamp = DateTime.Now.ToString() + " ";
            listBoxLog.Items.Add(timestamp + logtext);
            listBoxLog.TopIndex = listBoxLog.Items.Count - 1;
        }

        void Log(List<string> logtext)
        {
            foreach (string s in logtext)
            {
                Log(s);
            }
        }

        private void btnImportTanks_Click(object sender, EventArgs e)
        {
            //List<string> result = importTanks2DB.importTanks();
            //Log(result);
            //importTanks2DB.fetchTanks();
            List<string> log = importTanks2DB.string2json();
            foreach (string item in log)
            {
                listBoxLog.Items.Add(item);
            }
            tankData.GetTankListFromDB();

        }

        private void btnUpdateWN8_Click(object sender, EventArgs e)
        {
            importTanks2DB.UpdateWN8();
            MessageBox.Show("Update complete");
        }
    }
}
