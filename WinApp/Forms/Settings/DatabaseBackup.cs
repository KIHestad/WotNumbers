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
                // Get initial text and check if 12 hours since last backup
                bool lastBackupWithin12H = true;
                DateTime lastBackup = new DateTime();
                if (Config.Settings.databaseBackupLastPerformed == null)
                {
                    lblProgressStatus.Text = "No backup performed previously";
                    lastBackupWithin12H = false;
                }
                else
                {
                    lastBackup = Convert.ToDateTime(Config.Settings.databaseBackupLastPerformed);
                    lblProgressStatus.Text = "Last backup run: " + lastBackup.ToString();
                    DateTime time12HoursAgo = DateTime.Now.AddHours(-12);
                    lastBackupWithin12H = (lastBackup.CompareTo(time12HoursAgo) > 0);
                }
                // If auto, check for minimum 12 hours since last backup
                if (_autoRun && lastBackupWithin12H)
                {
                    _autoRun = false;
                    this.Close();
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
        
        private void UpdateFromApi_Shown(object sender, EventArgs e)
		{
			if (_autoRun)
				RunNow();
		}

		private void UpdateProgressBar(int value, string statusText)
		{
			lblProgressStatus.Text = statusText;
    		badProgressBar.Value = value;
			Refresh();
			Application.DoEvents();
		}

		private void RunNow()
		{
			// Init
            this.Cursor = Cursors.WaitCursor;
			FormTheme.Cursor = Cursors.WaitCursor;
			badProgressBar.Value = 0;
			badProgressBar.Visible = true;
            // Perform backup
            DateTime backupTime = DateTime.Now;
            
            // Copy Database
            bool ok = Copy(backupTime);

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

		private void btnStart_Click(object sender, EventArgs e)
		{
			if (btnStart.Text == "Start")
            {
                btnStart.Text = "Cancel";
                RunNow();
            }
            else
            {
                cancelFlag = true;
                btnStart.Text = "Start";
            }
            
		}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancelFlag = true;
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

        private bool Copy(DateTime backupTime)
        {
            try
            {
                byte[] buffer = new byte[1024 * 512]; // 0.5MB buffer
                string backupFile = Config.Settings.databaseBackupFilePath;
                if (backupFile.Substring(backupFile.Length - 1, 1) != "\\")
                    backupFile += "\\";
                backupFile += Path.GetFileNameWithoutExtension(Config.Settings.databaseFileName) + "_";
                backupFile += string.Format("{0:yyyy-MM-dd_HH-mm-ss}", backupTime);
                backupFile += Path.GetExtension(Config.Settings.databaseFileName);

                using (FileStream source = new FileStream(Config.Settings.databaseFileName, FileMode.Open, FileAccess.Read))
                {
                    long fileLength = source.Length;
                    using (FileStream dest = new FileStream(backupFile, FileMode.CreateNew, FileAccess.Write))
                    {
                        long totalBytes = 0;
                        int currentBlockSize = 0;

                        while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            totalBytes += currentBlockSize;
                            double persentage = (double)totalBytes * 100.0 / fileLength;
                            dest.Write(buffer, 0, currentBlockSize);
                            UpdateProgressBar(Convert.ToInt32(persentage), "File copy in progress...");
                            if (cancelFlag)
                                break;
                        }
                    }
                    // Delete dest file here if cancelled
                    if (cancelFlag)
                        File.Delete(backupFile);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex, "Database Backup failed");
                if (!_autoRun)
                    MsgBox.Show("Error performing database backup: " + ex.Message, "Database Backup Error");
                return false;
            }
        }

		
	}
}
