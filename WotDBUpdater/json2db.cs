using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;

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

                                        // Check data by getting jsonUserTank Mapping
                                        string expression = "jsonMainSubProperty='" + currentItem.mainSection + "." + currentItem.subSection + "." + currentItem.property + "'";
                                        DataRow[] foundRows = tankData.jsonUserTankTable.Select(expression);

                                        // IF mapping found add currentItem into NewUserTankRow
                                        if (foundRows.Length != 0)
                                        {
                                            // Add now
                                            string dataType = foundRows[0]["dataType"].ToString();
                                            string dbField = foundRows[0]["dbField"].ToString();
                                            switch (dataType)
                                            {
                                                case "String": NewUserTankRow[dbField] = currentItem.value.ToString(); ; break;
                                                case "DateTime": NewUserTankRow[dbField] = ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); ; break;
                                                case "Int": NewUserTankRow[dbField] = Convert.ToInt32(currentItem.value); ; break;
                                            }
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

        static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
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

        private static void dossier2db_ver2(string filename, bool ForceUpdate = false)
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
            
            JToken rootToken;
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            JObject fileContent = JObject.Parse(json);
            rootToken = fileContent.First;
            if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
            {
                rootToken = rootToken.Next.Next;
                rootToken = rootToken.Next;
                List<string> logtxt = new List<string>();
                JToken turrets = rootToken.Children().First();
                foreach (JProperty turretItem in turrets.Children())
                {
                    JToken t = turretItem.First();
                    String result = t["name"].ToString();
                    result += " | " + t["nation_i18n"].ToString();
                    result += " | " + t["armor_fedd"].ToString();
                    result += " | " + t["circular_vision_radius"].ToString();
                    result += " | " + t["weight"].ToString();
                    result += " | " + t["name"].ToString();
                    JArray tanksArray = (JArray)t["tanks"];
                    result += " | " + tanksArray[0].ToString();
                    logtxt.Add(result);
                }
                Log.LogToFile(logtxt);

            }
        }

    }
}
