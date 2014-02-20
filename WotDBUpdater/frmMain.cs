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
using System.Collections;

//using IronPython.Hosting;
//using Microsoft.Scripting.Hosting;
//using IronPython.Runtime;

namespace WotDBUpdater
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Startup settings
            ConfigData conf = new ConfigData();
            conf = Config.GetConfig();
            SetStartStopButton(conf);
        }

        private void SetStartStopButton(ConfigData conf)
        {
            // Set Start - Stop button properties, enable or disable file system watcher = listen to dossier file
            if (conf.Run == 1)
            {
                btnStartStop.Text = "Stop";
                lblStatus.Text = "RUNNING";
                pnlStatus.BackColor = System.Drawing.Color.ForestGreen; 
                Log("Started");
                fileSystemWatcherDossierFile.Path = Path.GetDirectoryName(conf.DossierFilePath + "\\");
                fileSystemWatcherDossierFile.Filter = "*.dat";
                fileSystemWatcherDossierFile.NotifyFilter = NotifyFilters.LastWrite;
                fileSystemWatcherDossierFile.Changed += new FileSystemEventHandler(DossierFileChanged);
                fileSystemWatcherDossierFile.EnableRaisingEvents = true;
            }
            else
            {
                btnStartStop.Text = "Start";
                lblStatus.Text = "STOPPED";
                pnlStatus.BackColor = System.Drawing.Color.Gray;
                Log("Stopped");
                fileSystemWatcherDossierFile.EnableRaisingEvents = false;
            }
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            // Start - Stop button event for listening to dossier file
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

        private void Log(string logtext)
        {
            // log to ListBox and scroll to bottom
            listBoxLog.Items.Add(DateTime.Now.ToString() + " " + logtext);
            listBoxLog.TopIndex = listBoxLog.Items.Count - 1;
        }

        private void DossierFileChanged(object source, FileSystemEventArgs e)
        {
            // Dossier file automatic handling
            // Stop listening to dossier file
            fileSystemWatcherDossierFile.EnableRaisingEvents = false;
            Log("Dossier file updated");
            // Get config data
            string dossierfile = e.FullPath;
            FileInfo file = new FileInfo(dossierfile);
            // Wait until file is ready to read, 
            WaitUntilFileReadyToRead(dossierfile, 4000);
            // Perform file conversion from picle til json
            CopyAndConvertFile(dossierfile);
            // Continue listening to dossier file, wait one sec first
            // System.Threading.Thread.Sleep(1000);
            fileSystemWatcherDossierFile.EnableRaisingEvents = true;
        }

        private void WaitUntilFileReadyToRead(string filePath, int maxWaitTime)
        {
            // Checks file is readable
            bool fileOK = false;
            int waitinterval = 100; // time to wait in ms per read operation to check filesize
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (stopWatch.ElapsedMilliseconds < maxWaitTime && !fileOK)
            {
                try
                {
                    using (FileStream stream = new FileStream(filePath,FileMode.Open,FileAccess.Read))
                    {
                        fileOK = true;
                        TimeSpan ts = stopWatch.Elapsed;
                        Log(String.Format(" > Dossierfile read successful (waited: {0:0000}ms)", stopWatch.ElapsedMilliseconds.ToString()));
                    }
                }
                catch
                {
                    // could not read file
                    Log(String.Format(" > Dossierfile not ready yet (waited: {0:0000}ms)",stopWatch.ElapsedMilliseconds.ToString()));
                    System.Threading.Thread.Sleep(waitinterval);
                }
            }
            stopWatch.Stop();
        }

        private void btnManualRun_Click(object sender, EventArgs e)
        {
            // Dossier file manual handling
            Log("Manual dossier file handling");
            // Get all dossier files
            ConfigData conf = new ConfigData();
            conf = Config.GetConfig();
            string[] files = Directory.GetFiles(conf.DossierFilePath, "*.dat");
            string dossierfile = "";
            DateTime dossierfiledate = new DateTime(1970,1,1);
            foreach (string file in files)
            {
                FileInfo checkfile = new FileInfo(file);
                if (checkfile.LastWriteTime > dossierfiledate)
                {
                    dossierfile = checkfile.FullName;
                    dossierfiledate = checkfile.LastWriteTime;
                }
            }
            if (dossierfile == "")
            {
                Log("No file found");
            }
            else
            {
                CopyAndConvertFile(dossierfile);
            }
        }

        private void CopyAndConvertFile(string dossierfile)
        {
            // Copy dossier file and perform file conversion til json format
            FileInfo file = new FileInfo(dossierfile); // the original dossier file
            string appPath = Path.GetDirectoryName(Application.ExecutablePath); // path to app dir
            string dossier2jsonfile = appPath + "/dossier2json/wotdc2j.py"; // python-script for converting dossier file
            string dossiernewfile = appPath + "/dossier_new.dat"; // new dossier file
            string dossierprevfile = appPath + "/dossier_prev.dat"; // previous dossier file
            string jsonfile = appPath + "/dossier_new.json"; // output file
            try
            {
                bool ok = true;
                file.CopyTo(dossiernewfile, true); // copy and rename dossier file
                FileInfo fileInfonew = new FileInfo(dossiernewfile); // the new dossier file
                FileInfo fileInfoprev = new FileInfo(dossierprevfile); // the previous dossier file
                // First check if same as previous uploaded
                if (File.Exists(dossierprevfile))
                {
                    if (FilesContentsAreEqual(fileInfonew, fileInfoprev))
                    {
                        // Files are identical, skip convert
                        Log(" > File skipped, same content as previos");
                        ok = false;
                    }
                }
                if (ok)
                {
                    // Convert file
                    Dossier2Jason(dossier2jsonfile, dossiernewfile, jsonfile); // convert to json
                    // Move new file as previos (copy and delete)
                    fileInfonew.CopyTo(dossierprevfile, true); // copy and rename dossier file
                    fileInfonew.Delete();
                    Log(" > File copied and converted");
                }
            }
            catch (Exception ex)
            {
                Log(" > File copy or conversion error: " + ex.Message);
            }
        }
                
        private static void Dossier2Jason(string dossier2jsonfile, string dossierfile, string jsonfile)
        {
            // Convert to json format using python conversion from cPicle stream format
            
            // Use ProcessStartInfo class to run python 
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "c:\\python27\\python.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Normal; //.Hidden;
            startInfo.Arguments = dossier2jsonfile + " " + dossierfile + " -f -r";
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
            String s = json2db.readJson(jsonfile);
            MessageBox.Show(s);

            // Alternative model - using IronPython
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

        }

        public static bool FilesContentsAreEqual(FileInfo fileInfo1, FileInfo fileInfo2)
        {
            bool result;
            if (fileInfo1.Length != fileInfo2.Length)
            {
                result = false;
            }
            else
            {
                using (var file1 = fileInfo1.OpenRead())
                {
                    using (var file2 = fileInfo2.OpenRead())
                    {
                        result = StreamsContentsAreEqual(file1, file2);
                    }
                }
            }
            return result;
        }

        private static bool StreamsContentsAreEqual(Stream stream1, Stream stream2)
        {
            const int bufferSize = 2048 * 2;
            var buffer1 = new byte[bufferSize];
            var buffer2 = new byte[bufferSize];

            while (true)
            {
                int count1 = stream1.Read(buffer1, 0, bufferSize);
                int count2 = stream2.Read(buffer2, 0, bufferSize);

                if (count1 != count2)
                {
                    return false;
                }

                if (count1 == 0)
                {
                    return true;
                }

                int iterations = (int)Math.Ceiling((double)count1 / sizeof(Int64));
                for (int i = 0; i < iterations; i++)
                {
                    if (BitConverter.ToInt64(buffer1, i * sizeof(Int64)) != BitConverter.ToInt64(buffer2, i * sizeof(Int64)))
                    {
                        return false;
                    }
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAbout();
            frm.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void selectDossierFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDossierFileSelect();
            frm.ShowDialog();
            ConfigData conf = new ConfigData();
            conf = Config.GetConfig();
            fileSystemWatcherDossierFile.Path = Path.GetDirectoryName(conf.DossierFilePath + "\\");
        }

        private void listBoxLog_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(listBoxLog.Items[listBoxLog.SelectedIndex].ToString(), "Log Details");
        }

        private void databaseSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDatabaseSetting();
            frm.ShowDialog();
        }

        private void showTankTableInGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmCountryInGrid();
            frm.ShowDialog();
        }

        private void addCountryToTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddCountryToTable();
            frm.ShowDialog();
        }
                
    }
}
