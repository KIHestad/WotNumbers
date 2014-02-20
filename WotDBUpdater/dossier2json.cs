using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater
{
    class dossier2json
    {

        public static string ConvertDossierUsingPython(string dossier2jsonfile, string dossierfile, string jsonfile)
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
                return(ex.Message);
            }
            String s = json2db.readJson(jsonfile);
            

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
            return s;
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
    
    }
}
