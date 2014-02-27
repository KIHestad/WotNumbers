using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater
{
    class modules2DB
    {

        #region tankStats2DB

        public class JsonMainSection
        {
            public string status = "status";
            public string count = "count";
            public string data = "data";
        }

        public class JsonItem
        {
            public string mainSection = "";
            public string turret = "";
            public string subSection = "";
            public string property = "";
            public object value = null;
        }


        //private void testURLToolStripMenuItem_Click(object sender, EventArgs e)
        public static String fetchTankStats()
        {
            string url = "https://api.worldoftanks.eu/wot/encyclopedia/tankturrets/?application_id=2a8bf9a1ee36d6125058bf6efd006caf";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Timeout = 10000;     // 10 secs
            httpRequest.UserAgent = "Code Sample Web Client";

            HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
            StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());

            return responseStream.ReadToEnd();
        }

        public static String importTurrets()
        {
            String json = fetchTankStats();
            JsonTextReader reader = new JsonTextReader(new StringReader(json));

            // logging
            List<string> log = new List<string>();

            DataTable NewTurretTable = moduleData.GetTurretDataFromDB(-1); // Return no data, only empty database with structure
            DataRow NewTurretRow = NewTurretTable.NewRow();
            string turretHeader = "";

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

                        if (currentSectionType == mainSection.status) currentItem.mainSection = mainSection.status;
                        if (currentSectionType == mainSection.count) currentItem.mainSection = mainSection.count;
                        if (currentSectionType == mainSection.data) currentItem.mainSection = mainSection.data;
                        log.Add("\nMain section: " + currentItem.mainSection + "(Line: " + reader.LineNumber + ")");
                    }
                }

                if (currentItem.mainSection == mainSection.data) // Only read from data section
                {
                    if (reader.Depth == 2) // ********************************************  found second level = turret level  ************************************************************
                    {
                        if (reader.Value != null) // found new turret
                        {
                            // Turret data exists, save data found and log
                            if (turretHeader != "")
                            {
                                log.Add("  > Check for DB update - Turret: '" + turretHeader + " | name:" + NewTurretRow["name"]);
                                int turretId = Convert.ToInt32(turretHeader);
                                moduleData.saveTurret(turretId, NewTurretRow); //(removed ForceUpdate switch)                         <--------------
                            }
                            // Reset all values
                            NewTurretTable.Clear();
                            NewTurretRow = NewTurretTable.NewRow();
                            // Get new turret name
                            currentItem.turret = reader.Value.ToString(); // add to current item
                            turretHeader = reader.Value.ToString(); // add to current tank
                        }
                    }
                    else
                    {
                        if (reader.Depth == 3) // ********************************************  found third level = property and value  ************************************************************
                        {
                            //if (reader.Value != null)
                            //{
                            //    currentItem.subSection = reader.Value.ToString();
                            //    currentItem.property = ""; // reset property for reading next
                            //}

                            if (reader.TokenType == JsonToken.PropertyName)
                            {
                                // Property
                                currentItem.property = reader.Value.ToString();
                                string x = currentItem.property;
                            }

                            else
                            {
                                if (reader.Value != null)
                                {
                                    // Value
                                    currentItem.value = reader.Value;
                                    string x = currentItem.property;

                                    // Check data
                                    string expression = "jsonProperty='" + currentItem.property + "'";
                                    //DataRow[] foundRows = moduleData.jsonTurretTable.Select("jsonTurretId=1");
                                    DataRow[] foundRows = moduleData.jsonTurretTable.Select(expression);

                                    // Test using DataView
                                    //moduleData.jsonTurretView.RowFilter = expression;

                                    //if (moduleData.jsonTurretView.Count != 0)
                                    //{
                                    //    // test
                                    //    string dataType = moduleData.jsonTurretView[0]["dataType"].ToString();
                                    //    string dbField = moduleData.jsonTurretView[0]["dbField"].ToString();

                                    //    switch (dataType)
                                    //    {
                                    //        case "String": NewTurretRow[dbField] = currentItem.value.ToString(); ; break;
                                    //        //case "DateTime": NewTurretRow[dbField] = ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); ; break;
                                    //        case "Int": NewTurretRow[dbField] = Convert.ToInt32(currentItem.value); ; break;
                                    //    }
                                    //}

                                    if (foundRows.Length != 0)
                                    {
                                        // Add now
                                        string dataType = foundRows[0]["dataType"].ToString();
                                        string dbField = foundRows[0]["dbField"].ToString();
                                        switch (dataType)
                                        {
                                            case "String": NewTurretRow[dbField] = currentItem.value.ToString(); ; break;
                                            //case "DateTime": NewTurretRow[dbField] = ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); ; break;
                                            case "Int": NewTurretRow[dbField] = Convert.ToInt32(currentItem.value); ; break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            reader.Close();
            return json;
        }

        #endregion

        #region test

        public static void readModuleData()
        {
            string json = fetchTankStats();
            int moduleCount;
            string property;
            JToken rootToken;
            JToken moduleToken;
            JToken propertyToken;

            JsonTextReader reader = new JsonTextReader(new StringReader(json));

            //while (reader.Read())
            //{
            //    if (reader.Depth <= 1)
            //    {
            //        if (reader.Value != null)
            //        {
            //            if (reader.Value == "count")
            //            {

            //            }
            //            string f = reader.Value.ToString();

            //        }
            //    }

            //    else
            //    {
            //        string f = reader.Value.ToString();

            //    }
            //}


            //JObject fileContent = JObject.Parse(json);

            //rootToken = fileContent.First;

            //if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
            //{
            //    rootToken = rootToken.Next;
            //    moduleCount = (int)((JProperty)rootToken).Value;

            //    //data token starts here
            //    rootToken = rootToken.Next;




            //foreach (JObject modules in rootToken.Children<JObject>())
            //{
            //    //moduleToken = modules.First;
            //}
            //    foreach (JObject moduleProperties in moduleToken.Children<JObject>())
            //    {
            //        propertyToken = moduleProperties.First;
            //        property = ((JProperty)propertyToken).Value.ToString();
            //        propertyToken = moduleProperties.Next;
            //    }
            //}

            //JObject j = rootToken.Children<JObject>();
            //moduleToken = rootToken.Children().First();
            //int x = 0;
            //while (moduleToken != null)
            //{
            //    //x++;
            //    moduleToken = moduleToken.Ancestors().Children()
            //}


        }

        #endregion
    }
}
