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
            jsonProperty.MainSection mainSection = new jsonProperty.MainSection();
            jsonProperty.Item currentItem = new jsonProperty.Item();
            
            // Loop through json file
            while (reader.Read())
            {
                if (reader.Depth <= 1) // main level ( 0 or 1)
                {
                    if (reader.Value != null) // ********************************************  found main level - get section type  ************************************************************
                    {
                        string currentSectionType = reader.Value.ToString();

                        if (currentSectionType == mainSection.header) currentItem.mainSection = mainSection.header;
                        if (currentSectionType == mainSection.tanks) currentItem.mainSection = mainSection.tanks;
                        if (currentSectionType == mainSection.tanks_v2) currentItem.mainSection = mainSection.tanks_v2;
                        log.Add("\nMain section: " + currentItem.mainSection + "(Line: " + reader.LineNumber + ")");
                    }
                }

                if (currentItem.mainSection == mainSection.tanks || currentItem.mainSection == mainSection.tanks_v2) // Only get data from tank or tank_v2 sections, skpi header for now....
                {
                    if (reader.Depth == 2) // ********************************************  found second level = tank level  ************************************************************
                    {
                        if (reader.Value != null) // found new tank
                        {
                            if (tank.tankName != "") // Tank data exist, save and log
                            {
                                log.Add("  > READY TO SAVE TO DB - Tank: '" + tank.tankName + "' | battleCount:" + tank.battleCount + "\n");
                            }
                            // Reset all values
                            tank.Clear();
                            // Get new tank name
                            currentItem.tank = reader.Value.ToString(); // add to current item
                            tank.tankName = reader.Value.ToString(); // add to current tank
                        }
                    }
                    else
                    {
                        if (reader.Depth == 3) // ********************************************  found third level = subsection  ************************************************************
                        {
                            if (reader.Value != null)
                            {
                                currentItem.subSection = reader.Value.ToString();
                                currentItem.property = ""; // reset property for reading next
                            }
                        }
                        else // ********************************************  found fourth level = property and value  ************************************************************
                        {
                            if (currentItem.subSection != "rawdata") // skip these subsections
                            {
                                if (reader.TokenType == JsonToken.PropertyName)
                                {
                                    // Property
                                    currentItem.property = reader.Value.ToString();
                                }
                                else
                                {
                                    if (reader.Value != null)
                                    {
                                        // Value
                                        currentItem.value = reader.Value;

                                        // Check data
                                        

                                        // Check data here - make separate class for this later
                                        //
                                        if (currentItem.mainSection == mainSection.tanks)
                                        {
                                            if (currentItem.subSection == "tankdata" && currentItem.property == "battlesCount") tank.battleCount = Convert.ToInt32(currentItem.value);
                                        }
                                        else if (currentItem.mainSection == mainSection.tanks_v2)
                                        {
                                            if (currentItem.subSection == "a15x15" && currentItem.property == "battlesCount") tank.battleCount = Convert.ToInt32(currentItem.value);
                                        }

                                        // Temp log all data
                                        log.Add("  " + currentItem.mainSection + "." + currentItem.tank + "." + currentItem.subSection + "." + currentItem.property + ":" + currentItem.value);

                                    }
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
