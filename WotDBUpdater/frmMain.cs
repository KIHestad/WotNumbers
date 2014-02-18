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
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Collections;
//using IronPython.Runtime;

namespace WotDBUpdater
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        private void btnOpenDossierFile_Click(object sender, EventArgs e)
        {
            if (txtDossierFile.Text == "")
            {
                openFileDialogDossierFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\wargaming.net\\WorldOfTanks\\dossier_cache";
            }
            else
            {
                openFileDialogDossierFile.InitialDirectory = txtDossierFile.Text;
            }
            openFileDialogDossierFile.ShowDialog();
            txtDossierFile.Text = openFileDialogDossierFile.FileName;
            ConfigData conf = new ConfigData();
            conf = Config.GetConfig();
            conf.Filename = txtDossierFile.Text;
            Config.SaveConfig(conf);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ConfigData conf = new ConfigData();
            conf = Config.GetConfig();
            txtDossierFile.Text = conf.Filename;
            SetStartStopButton(conf);
        }

        private void SetStartStopButton(ConfigData conf)
        {
            if (conf.Run == 1)
            {
                btnStartStop.Text = "Started";
                Log(DateTime.Now.ToString() + " - Started");
                fileSystemWatcherDossierFile.Path = Path.GetDirectoryName(conf.Filename);
                fileSystemWatcherDossierFile.Filter = Path.GetFileName(conf.Filename);
                fileSystemWatcherDossierFile.NotifyFilter = NotifyFilters.LastWrite;
                fileSystemWatcherDossierFile.Changed += new FileSystemEventHandler(DossierFileChanged);
                fileSystemWatcherDossierFile.EnableRaisingEvents = true;
            }
            else
            {
                btnStartStop.Text = "Stopped";
                Log(DateTime.Now.ToString() + " - Stopped");
                fileSystemWatcherDossierFile.EnableRaisingEvents = false;
            }
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            ConfigData conf = new ConfigData();
            conf = Config.GetConfig();
            if (conf.Run == 1)
            {
                conf.Run = 0;
            }
            else
            {
                conf.Run = 1;
            }
            Config.SaveConfig(conf);
            SetStartStopButton(conf);
        }

        private void Log(string txt)
        {
            List<string> log = new List<string>();
            log.Add(txt);
            foreach (string item in listBoxLog.Items)
            {
                log.Add(item);
            }
            listBoxLog.Items.Clear();
            listBoxLog.Items.AddRange(log.ToArray());
        }

        private void DossierFileChanged(object source, FileSystemEventArgs e)
        {
            // Stop listening to dossier file
            fileSystemWatcherDossierFile.EnableRaisingEvents = false;
            // Get new dossier file
            ConfigData conf = new ConfigData();
            conf = Config.GetConfig();
            int i=0;
            FileInfo file = new FileInfo(conf.Filename);
            while (IsFileLocked(file) || i <= 8) 
            {
                Thread.Sleep(250);
                i += 1;
            }
            Log(DateTime.Now.ToString() + " - File updated");
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            string dossierfile = appPath + "/dossier.dat";
            string dossier2json = appPath + "/dossier2json/wotdc2j.py";
            try
            {
                file.CopyTo(dossierfile, true);
                Dossier2Jason(dossierfile, dossier2json);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"DossierFileChanged");
            }
            // Continue listening to dossier file
            fileSystemWatcherDossierFile.EnableRaisingEvents = true;
        }

        private void btnManualRun_Click(object sender, EventArgs e)
        {
            // Get new dossier file -
            ConfigData conf = new ConfigData();
            conf = Config.GetConfig();
            FileInfo file = new FileInfo(conf.Filename);
            Log(DateTime.Now.ToString() + " - Manual check");
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            string dossierfile = appPath + "/dossier.dat";
            string dossier2json = appPath + "/dossier2json/wotdc2j.py";
            try
            {
                file.CopyTo(dossierfile, true);
                Dossier2Jason(dossierfile, dossier2json);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "btnManualRun_Click");
            }
        }
        
        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }



        static void Dossier2Jason(string dossierfile, string dossier2json)
        {
            //try
            //{
            //    IDictionary<string, object> options = new Dictionary<string, object>();
            //    dossierfile = dossierfile.Replace("\\","/");
            //    options["Arguments"] = new[] { dossierfile, "-f", "-r" };
            //    ScriptEngine engine = Python.CreateEngine(options);
            //    dossier2json = dossier2json.Replace("\\", "/");
            //    engine.ExecuteFile(dossier2json);
            
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message,"Dossier2Jason");
            //}
            //engine.ExecuteFile(dossier2json + " " + dossierfile + " -f -r");


            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "c:\\python27\\python.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Normal; //.Hidden;
            startInfo.Arguments = dossier2json + " " + dossierfile + " -f -r";

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
                
    }
}
