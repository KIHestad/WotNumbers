using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;


namespace WotDBUpdater
{
    class json2db
    {
        public class JsonMainSection
        {
            public string header = "header";
            public string tanks = "tanks";
            public string tanks_v2 = "tanks_v2";
        }

        public class JsonItem
        {
            public string mainSection = "";
            public string tank = "";
            public string subSection = "";
            public string property = "";
            public object value = null;
        }
        
        public static String readJson(string filename, bool ForceUpdate = false)
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

            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            // read json string
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
                        
            // logging
            List<string> log = new List<string>();

            // Declare
            DataTable NewUserTankTable = tankData.GetUserTankDataFromDB(-1); // Return no data, only empty database with structure
            DataRow NewUserTankRow = NewUserTankTable.NewRow();
            string tankName = "";
            //TankDataResult tdr = new TankDataResult();

            JsonMainSection mainSection = new JsonMainSection();
            JsonItem currentItem = new JsonItem();
            
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
                            // Tank data exist, save data found and log
                            if  (tankName != "") 
                            {
                                log.Add("  > Check for DB update - Tank: '" + tankName + " | battles15:" + NewUserTankRow["battles15"] + " | battles7:" + NewUserTankRow["battles7"]);
                                tankData.SaveTankDataResult(tankName, NewUserTankRow, ForceUpdate);
                            }
                            // Reset all values
                            NewUserTankTable.Clear();
                            NewUserTankRow = NewUserTankTable.NewRow();
                            // Get new tank name
                            currentItem.tank = reader.Value.ToString(); // add to current item
                            tankName = reader.Value.ToString(); // add to current tank
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
                                        string expression = "jsonMain='" + currentItem.mainSection + "' and jsonSub='" + currentItem.subSection + "' and jsonProperty='" + currentItem.property + "'";
                                        DataRow[] foundRows = tankData.jsonUserTankTable.Select(expression);
                                        if (foundRows.Length != 0)
                                        {
                                            string dbField = foundRows[0]["dbField"].ToString();
                                            NewUserTankRow[dbField] = currentItem.value;
                                        }

                                        // Temp log all data
                                        //log.Add("  " + currentItem.mainSection + "." + currentItem.tank + "." + currentItem.subSection + "." + currentItem.property + ":" + currentItem.value);
                                        //log.Add("  " + currentItem.mainSection + "." + currentItem.subSection + "." + currentItem.property );
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

        private static void UpdateNewUserTankRow(ref DataRow NewUserTankRow, JsonItem currentItem)
        {
            JsonMainSection mainSection = new JsonMainSection(); 
            if (currentItem.mainSection == mainSection.tanks)
            {
                if (currentItem.subSection == "tankdata" && currentItem.property == "battlesCount") NewUserTankRow["battles15"] = Convert.ToInt32(currentItem.value);
                if (currentItem.subSection == "tankdata" && currentItem.property == "wins") NewUserTankRow["wins15"] = Convert.ToInt32(currentItem.value);
                if (currentItem.subSection == "tankdata" && currentItem.property == "wins") NewUserTankRow["wins15"] = Convert.ToInt32(currentItem.value);
            }
            else if (currentItem.mainSection == mainSection.tanks_v2)
            {
                if (currentItem.subSection == "a15x15" && currentItem.property == "battlesCount") NewUserTankRow["battles15"] = Convert.ToInt32(currentItem.value);
                if (currentItem.subSection == "a15x15" && currentItem.property == "wins") NewUserTankRow["wins15"] = Convert.ToInt32(currentItem.value);
                if (currentItem.subSection == "a7x7" && currentItem.property == "battlesCount") NewUserTankRow["battles7"] = Convert.ToInt32(currentItem.value);
                if (currentItem.subSection == "a7x7" && currentItem.property == "wins") NewUserTankRow["wins7"] = Convert.ToInt32(currentItem.value);
            }
        }

     
    }
}
