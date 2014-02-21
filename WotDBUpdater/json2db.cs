using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WotDBUpdater
{
    class json2db
    {
        private static class SectionType
        {
            public static string header = "header";
            public static string tanks = "tanks";
            public static string tanks_v2 = "tanks_v2";
        }

        private class TankData
        {
            public string tankName = "";
            public int battleCount = 0;

            public void Clear()
            {
                tankName = "";
                battleCount = 0;
            }
        }
        
        public static String readJson(string filename)
        {
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(filename))
            {
                String line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);
                }
            }

            string json = sb.ToString();

            //try
            //{
            //    ConfigData conf = new ConfigData();
            //    conf = Config.GetConfig();
            //    SqlConnection con = new SqlConnection(conf.DatabaseConn);
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand("INSERT INTO json (jsonId, jsonString) values (1, @json)", con);
            //    cmd.Parameters.AddWithValue("@json", json);
            //    cmd.ExecuteNonQuery();
            //    con.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Database error: " + ex.Message);
            //}

            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            // read json string
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
                        
            // logging
            List<string> log = new List<string>();

            // Declare
            TankData tank = new TankData();
            string currentsection1 = "";
            string currentsection3 = "";
            string currentproperty = "";
            object currentvalue = null;
            
            // Loop through json file
            while (reader.Read())
            {
                if (reader.Depth <= 1) // main level ( 0 or 1)
                {
                    if (reader.Value != null) // ********************************************  found main level - get section type  ************************************************************
                    {
                        string mainlevel = reader.Value.ToString();
                        if (mainlevel == SectionType.header) currentsection1 = SectionType.header;
                        if (mainlevel == SectionType.tanks) currentsection1 = SectionType.tanks;
                        if (mainlevel == SectionType.tanks_v2) currentsection1 = SectionType.tanks_v2;
                        log.Add("\nMain section: " + currentsection1 + "(Line: " + reader.LineNumber + ")");
                    }
                }

                if (currentsection1 == SectionType.tanks || currentsection1 == SectionType.tanks_v2) // Only get data from tank or tank_v2 sections, skpi header for now....
                {
                    if (reader.Depth == 2) // ********************************************  found second level = tank level  ************************************************************
                    {
                        if (reader.Value != null) // found new tank
                        {
                            if (tank.tankName != "") // Tank data exist, save and log
                            {
                                log.Add("  Tank: '" + tank.tankName + "' | battleCount:" + tank.battleCount);
                            }
                            // Reset all values
                            tank.Clear();
                            // Get new tank name
                            tank.tankName = reader.Value.ToString();
                        }
                    }
                    else
                    {
                        if (reader.Depth == 3) // ********************************************  found third level = datatype  ************************************************************
                        {
                            if (reader.Value != null)
                            {
                                currentsection3 = reader.Value.ToString();
                            }
                        }
                        else // ********************************************  found fourth level = property and value  ************************************************************
                        {
                            if (reader.TokenType == JsonToken.PropertyName)
                            {
                                // Property
                                currentproperty = reader.Value.ToString();
                            }
                            else
                            {
                                if (reader.Value != null)
                                {
                                    // Value
                                    currentvalue = reader.Value.ToString();

                                    // Check data here - make separate class for this later
                                    //
                                    if (currentsection1 == SectionType.tanks)
                                    {
                                        if (currentsection3 == "tankdata" && currentproperty == "battlesCount") tank.battleCount = Convert.ToInt32(currentvalue);
                                    }
                                    else if (currentsection1 == SectionType.tanks_v2)
                                    {
                                        if (currentsection3 == "a15x15" && currentproperty == "battlesCount") tank.battleCount = Convert.ToInt32(currentvalue);
                                    }

                                    // Temp log all data
                                    // log.Add("      > " + currentsection1 + "  |  " + tank.tankName + "  |  " + currentsection3 + "  |  " + currentproperty + ":" + currentvalue.ToString());
                                }
                            }
                        }
                    }
                }
            }
            reader.Close();
            
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            Log.LogToFile(log);


            return (" > Time spent analyzing file: " + ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds.ToString("000"));
        }
    }
}
