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
using WinApp.Code;

namespace WinApp.Forms
{
	public partial class DatabaseBackup : FormCloseOnEsc
    {
        private bool cancelFlag = false;
        private static bool _autoRun = false;
        public DatabaseBackup(bool autoRun = false)
		{
			InitializeComponent();
			_autoRun = autoRun; 
		}

        private void DatabaseBackup_Load(object sender, EventArgs e)
        {
            // Check if database backup settings
            if (Config.Settings.databaseType == ConfigData.dbType.SQLite && Config.Settings.databaseBackupFilePath.Trim().Length > 0)
            {
                // Get initial text 
                if (Config.Settings.databaseBackupLastPerformed == null)
                {
                    lblProgressStatus.Text = "No backup performed previously";
                }
                else
                {
                    DateTime lastBackup = Convert.ToDateTime(Config.Settings.databaseBackupLastPerformed);
                    lblProgressStatus.Text = "Last backup run: " + lastBackup.ToString();
                }
            }
            else
            {
                // No backup path or not used SQLite database
                if (!_autoRun)
                    MsgBox.Show("Can only perform database backup if path is added and SQLite database is used", "Database Backup Terminated");
                _autoRun = false;
                this.Close();
            }
        }
        
        private async void UpdateFromApi_Shown(object sender, EventArgs e)
		{
			if (_autoRun)
            {
                btnStart.Text = "Cancel";
                await RunNow();
            }
				
		}

		private void UpdateProgressBar(int value, string statusText)
		{
			lblProgressStatus.Text = statusText;
    		badProgressBar.Value = value;
			Refresh();
		}

		private async Task RunNow()
		{
			// Init
            this.Cursor = Cursors.WaitCursor;
			FormTheme.Cursor = Cursors.WaitCursor;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;

            // Release database
            GC.Collect();

            // Perform backup
            DateTime backupTime = DateTime.Now;
            
            // Copy Database
            bool ok = await Copy(backupTime);

			// Done
			if (cancelFlag) 
                UpdateProgressBar(0, "File copy cancelled");
            else if (!ok)
                UpdateProgressBar(0, "File copy failed");
            else
            {
                UpdateProgressBar(100, "Deleting old backup files");
                CleanUp();
                UpdateProgressBar(100, "File copy finished");
                // Save backup time
                Config.Settings.databaseBackupLastPerformed = backupTime;
                string msg = "";
                Config.SaveConfig(out msg);
            }
			// Done
            btnStart.Text = "Start";
            cancelFlag = false;
			this.Cursor = Cursors.Default;
            FormTheme.Cursor = Cursors.Default;
            if (_autoRun)
                this.Close();
		}

		private async void btnStart_Click(object sender, EventArgs e)
		{
			if (btnStart.Text == "Start")
            {
                btnStart.Text = "Cancel";
                await RunNow();
            }
            else
            {
                cancelFlag = true;
                btnStart.Text = "Start";
            }
            
		}

        private class BackupFiles
        {
            public DateTime created { get; set; }
            public string fileName { get; set; }
        }

        private void CleanUp()
        {
            // Get existing json files
            string[] existingBackupFiles = Directory.GetFiles(
                Config.Settings.databaseBackupFilePath,
                Path.GetFileNameWithoutExtension(Config.Settings.databaseFileName) + "*" + Path.GetExtension(Config.Settings.databaseFileName), 
                SearchOption.TopDirectoryOnly);
            // If more than 10, delete the oldest
            int fileCount = existingBackupFiles.Length;
            if (fileCount > 10)
            {
                // Create list with created date
                List<BackupFiles> backupFile = new List<BackupFiles>();
                foreach (string file in existingBackupFiles)
                {
                    FileInfo fi = new FileInfo(file);
                    BackupFiles bf = new BackupFiles();
                    bf.fileName = fi.FullName;
                    bf.created = fi.CreationTime;
                    backupFile.Add(bf);
                }
                // Sort on created date
                List<BackupFiles> sortedBackupFiles = backupFile.OrderBy(b => b.created).ToList();
                // Delete oldests
                for (int i = 0; i < fileCount - 10; i++)
                {
                    File.Delete(sortedBackupFiles[i].fileName);
                }
            }

        }

        private async Task<bool> Copy(DateTime backupTime)
        {
            byte[] buffer = new byte[1024 * 512]; // 0.5MB buffer
            string backupFile = Config.Settings.databaseBackupFilePath;
            if (backupFile.Substring(backupFile.Length - 1, 1) != "\\")
                backupFile += "\\";
            backupFile += Path.GetFileNameWithoutExtension(Config.Settings.databaseFileName) + "_";
            backupFile += string.Format("{0:yyyy-MM-dd_HH-mm-ss}", backupTime);
            backupFile += Path.GetExtension(Config.Settings.databaseFileName);
            // Read database file
            FileStream source = null;
            int readTry = 1;
            bool readOK = false;
            while (!cancelFlag && !readOK && readTry < 30)
            {
                System.Threading.Thread.Sleep(1000);
                UpdateProgressBar(readTry, "Waiting for database read access...");
                try
                {
                    readTry++;
                    source = new FileStream(Config.Settings.databaseFileName, FileMode.Open, FileAccess.Read);
                    readOK = true;
                }
                catch (Exception)
                {
                    
                }
            }
            if (source == null)
            {
                if (!cancelFlag)
                    MsgBox.Show("Error performing database backup, could not get read access to database file.", "Database Backup Terminated");
                return false;
            }
            else
            {
                UpdateProgressBar(40, "Creating backup file...");
                // Create destination
                try
                {
                    long fileLength = source.Length;
                    FileStream dest = new FileStream(backupFile, FileMode.CreateNew, FileAccess.Write);
                    long totalBytes = 0;
                    int currentBlockSize = 0;

                    while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        totalBytes += currentBlockSize;
                        double persentage = (double)totalBytes * 100.0 / fileLength;
                        await dest.WriteAsync(buffer, 0, currentBlockSize);
                        UpdateProgressBar(Convert.ToInt32(persentage), "File copy in progress...");
                        if (cancelFlag)
                            break;
                    }
                    // Delete dest file here if cancelled
                    if (cancelFlag)
                        File.Delete(backupFile);
                    return true;
                }
                catch (Exception ex)
                {
                    Log.LogToFile(ex, "Database Backup failed");
                    MsgBox.Show("Error performing database backup: " + ex.Message + Environment.NewLine + Environment.NewLine, "Database Backup Error");
                    return false;
                }
            }
        }
	}
}
