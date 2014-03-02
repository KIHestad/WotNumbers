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
    public partial class frmApplicationSetting : Form
    {
        public frmApplicationSetting()
        {
            InitializeComponent();
        }

        private void frmDossierFileSelect_Load(object sender, EventArgs e)
        {
            // Startup settings
            txtDossierFilePath.Text = Config.Settings.DossierFilePath;
            txtPlayerName.Text = Config.Settings.playerName;
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
            Config.Settings.DossierFilePath = txtDossierFilePath.Text;
            Config.Settings.playerName = txtPlayerName.Text;
            if (Config.SaveConfig(true,true)) // Try saving, returns error message and is false if failed
            {
                Form.ActiveForm.Close();
            }
        }

    }
}
