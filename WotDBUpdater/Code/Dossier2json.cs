using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Scripting.Hosting;

namespace WotDBUpdater
{
    public static class dossier2json
    {
        public static FileSystemWatcher dossierFileWatcher = new FileSystemWatcher();

        private static string LogText(string logtext)
        {
            return DateTime.Now + " " + logtext;
        }

        public static string updateDossierFileWatcher()
        {
            string logtext = "Dossier file listener stopped";
            bool run = (Config.Settings.run == 1);
            if (run)
            {
                logtext = "Dossier file listener started";
                dossierFileWatcher.Path = Path.GetDirectoryName(Config.Settings.dossierFilePath + "\\");
                dossierFileWatcher.Filter = "*.dat";
                dossierFileWatcher.NotifyFilter = NotifyFilters.LastWrite;
                dossierFileWatcher.Changed += new FileSystemEventHandler(dossierFileChanged);
                dossierFileWatcher.EnableRaisingEvents = true;
            }
            dossierFileWatcher.EnableRaisingEvents = run;
            Log.LogToFile(logtext,true);
            return logtext;
        }

        public static string manualRun(bool TestRunPrevJsonFile = false, bool ForceUpdate = false)
        {
            string returval = "Manual dossier file check started...";
            Log.CheckLogFileSize();
            List<string> logtext = new List<string>();
            bool ok = true;
            String dossierfile = "";
            if (!TestRunPrevJsonFile)
            {
                // Dossier file manual handling - get all dossier files
                logtext.Add(LogText("Manual run, looking for new dossier file"));
                if (Directory.Exists(Config.Settings.dossierFilePath))
                {
                    string[] files = Directory.GetFiles(Config.Settings.dossierFilePath, "*.dat");
                    DateTime dossierfiledate = new DateTime(1970, 1, 1);
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
                        logtext.Add(LogText(" > No dossier file found"));
                        returval = "No dossier file found - check Application Settings";
                        ok = false;
                    }
                    else
                    {
                        logtext.Add(LogText(" > Dossier file found"));
                    }
                }
                else
	            {
                    logtext.Add(LogText(" > Inncorrect path to dossier file, check Application Settings."));
                    returval = "Inncorrect path to dossier file - check Application Settings";
                    ok = false;
	            }
            }
            else
            {
                logtext.Add(LogText("Test run, using latest converted json file"));
            }
            if (ok)
            {
                List<string> newlogtext = copyAndConvertFile(out returval, dossierfile, TestRunPrevJsonFile, ForceUpdate);
                foreach (string s in newlogtext)
                {
                    logtext.Add(s);
                }
            }
            Log.LogToFile(logtext);
            return returval;
        }

        private static void dossierFileChanged(object source, FileSystemEventArgs e)
        {
            Log.CheckLogFileSize();
            List<string> logtext = new List<string>();
            logtext.Add(LogText("Dossier file listener detected updated dossier file"));
            // Dossier file automatic handling
            // Stop listening to dossier file
            dossierFileWatcher.EnableRaisingEvents = false;
            //Log("Dossier file updated");
            // Get config data
            string dossierfile = e.FullPath;
            FileInfo file = new FileInfo(dossierfile);
            // Wait until file is ready to read, 
            List<string> logtextnew1 = WaitUntilFileReadyToRead(dossierfile, 4000);
            // Perform file conversion from picle til json
            string statusresult;
            List<string> logtextnew2 = copyAndConvertFile(out statusresult, dossierfile);
            // Add logtext
            foreach (string s in logtextnew1)
	        {
		        logtext.Add(s);
	        }
            foreach (string s in logtextnew2)
            {
                logtext.Add(s);
            }
            
            // Continue listening to dossier file
            dossierFileWatcher.EnableRaisingEvents = true;
            // Save log to textfile
            Log.LogToFile(logtext);
        }

        private static List<string> WaitUntilFileReadyToRead(string filePath, int maxWaitTime)
        {
            // Checks file is readable
            List<string> logtext = new List<string>();
            bool fileOK = false;
            int waitinterval = 100; // time to wait in ms per read operation to check filesize
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (stopWatch.ElapsedMilliseconds < maxWaitTime && !fileOK)
            {
                try
                {
                    using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        fileOK = true;
                        TimeSpan ts = stopWatch.Elapsed;
                        logtext.Add(LogText(String.Format(" > Dossierfile read successful (waited: {0:0000}ms)", stopWatch.ElapsedMilliseconds.ToString())));
                    }
                }
                catch
                {
                    // could not read file
                    logtext.Add(LogText(String.Format(" > Dossierfile not ready yet (waited: {0:0000}ms)", stopWatch.ElapsedMilliseconds.ToString())));
                    System.Threading.Thread.Sleep(waitinterval);
                }
            }
            stopWatch.Stop();
            return logtext;
        }

        private static List<string> copyAndConvertFile(out string statusresult, string dossierfile, bool TestRunPrevJsonFile = false, bool ForceUpdate = false)
        {
            // Copy dossier file and perform file conversion til json format
            List<string> logtext = new List<string>();
            string appPath = Path.GetDirectoryName(Application.ExecutablePath); // path to app dir
            string dossier2jsonfile = appPath + "/dossier2json/wotdc2j.py"; // python-script for converting dossier file
            string dossiernewfile = appPath + "/dossier.dat"; // new dossier file
            string dossierprevfile = appPath + "/dossier_prev.dat"; // previous dossier file
            string jsonfile = appPath + "/dossier.json"; // output file
            string returval = "Starting file handling...";
            try
            {
                bool ok = true;
                if (!TestRunPrevJsonFile)
                {
                    FileInfo fileDossierOriginal = new FileInfo(dossierfile); // the original dossier file
                    fileDossierOriginal.CopyTo(dossiernewfile, true); // copy original dossier fil and rename it for analyze
                    if (File.Exists(dossierprevfile)) // check if previous file exist, and new one is different (skip if testrun)
                    {
                        FileInfo fileInfonew = new FileInfo(dossiernewfile); // the new dossier file
                        FileInfo fileInfoprev = new FileInfo(dossierprevfile); // the previous dossier file
                        if (dossier2json.FilesContentsAreEqual(fileInfonew, fileInfoprev))
                        {
                            // Files are identical, skip convert
                            logtext.Add(LogText(" > File skipped, same content as previos"));
                            returval = "Manual dossier file check skipped - same content as previous";
                            fileInfonew.Delete();
                            ok = false;
                        }
                    }
                }
                if (!TestRunPrevJsonFile && ok) // Convert file to json (skip if testrun)
                {
                    string result = dossier2json.ConvertDossierUsingPython(dossier2jsonfile, dossiernewfile); // convert to json
                    if (result != "") // error occured
                    {
                        logtext.Add(result);
                        returval = "Error converting dossier file to json - check log file";
                        ok = false;
                    }
                    else
                    {
                        logtext.Add(LogText(" > Successfully convertet dossier file to json"));
                        // Move new file as previos (copy and delete)
                        FileInfo fileInfonew = new FileInfo(dossiernewfile); // the new dossier file
                        fileInfonew = new FileInfo(dossiernewfile); // the new dossier file
                        fileInfonew.CopyTo(dossierprevfile, true); // copy and rename dossier file
                        fileInfonew.Delete();
                        logtext.Add(LogText(" > Renamed copied dossierfile as previous file"));
                    }
                }
                if (ok) // Analyze json file and add to db
                {
                    if (File.Exists(jsonfile))
                    {
                        returval = Dossier2db.ReadJson(jsonfile, ForceUpdate);
                        logtext.Add(LogText(" > " + returval));
                    }
                    else
                    {
                        logtext.Add(LogText(" > No json file found"));
                        returval = "No previous dossier file found - run manual check";
                    }
                }
            }
            catch (Exception ex)
            {
                logtext.Add(LogText(" > General file copy or conversion error: " + ex.Message));
                returval = "General file copy or conversion error - check log file";
            }
            Log.LogToFile(logtext);
            statusresult = returval;
            return logtext;
        }

        private static string ConvertDossierUsingPython(string dossier2jsonfile, string dossierfile)
        {
            // Convert to json format using python conversion from cPicle stream format
            // Use ProcessStartInfo class to run python 

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "c:\\python27\\python.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
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
                return LogText(ex.Message);
            }

            return "";
        }

        private static bool FilesContentsAreEqual(FileInfo fileInfo1, FileInfo fileInfo2)
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
    
    }
}
