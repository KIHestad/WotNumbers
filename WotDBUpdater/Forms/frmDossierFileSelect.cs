using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotDBUpdater
{
    public partial class frmDossierFileSelect : Form
    {
        public frmDossierFileSelect()
        {
            InitializeComponent();
        }

        private void frmDossierFileSelect_Load(object sender, EventArgs e)
        {
            // Startup settings
            ConfigData conf = new ConfigData();
            conf = Config.GetConfig();
            txtDossierFilePath.Text = conf.DossierFilePath;
        }

        private void btnOpenDossierFile_Click(object sender, EventArgs e)
        {
            // Select dossier file
            openFileDialogDossierFile.FileName = "*.dat";
            if (txtDossierFilePath.Text == "")
            {
                openFileDialogDossierFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\wargaming.net\\WorldOfTanks\\dossier_cache";
            }
            else
            {
                openFileDialogDossierFile.InitialDirectory = txtDossierFilePath.Text;
            }
            openFileDialogDossierFile.ShowDialog();
            // If file selected save config with new values
            if (openFileDialogDossierFile.FileName != "")
            {
                txtDossierFilePath.Text = Path.GetDirectoryName(openFileDialogDossierFile.FileName);  
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ConfigData conf = new ConfigData();
            conf = Config.GetConfig();
            conf.DossierFilePath = txtDossierFilePath.Text;
            Config.SaveConfig(conf);
            Form.ActiveForm.Close();
        }

    }
}
