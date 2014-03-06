using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Net;

namespace WotDBUpdater
{
    public static class ImportTanks2DB
    {

        private static string LogText(string logtext)
        {
            return DateTime.Now + " " + logtext;
        }

        
        
        public static String FetchDataFromFile(string type)
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            string jsonfile = "";
            string url = "";
            string json = "";
            if (type == "tanks")
            {
                jsonfile = appPath + "/Dossier2json/tanks.json";

                StringBuilder sb = new StringBuilder();
                using (StreamReader sr = new StreamReader(jsonfile))
                {
                    String line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.AppendLine(line);
                    }
                }
                json = sb.ToString();
            }

            else if (type == "WN8")
            {
                //jsonfile = appPath + "/Dossier2json/wn8.json";
                
                url = "http://www.wnefficiency.net/exp/expected_tank_values_latest.json";
                
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                
                httpRequest.Timeout = 10000;     // 10 secs
                httpRequest.UserAgent = "Code Sample Web Client";
                
                HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
                StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());
                
                json = responseStream.ReadToEnd();
            }
            
            return json;
        }


        #region importTanks

        public static List<string> UpdateTanks()
        {
            String s = "{items:" + FetchDataFromFile("tanks") + "}";
            SqlConnection con = new SqlConnection(Config.DatabaseConnection());
            con.Open();

            int jsonCompDescr = 0;
            int jsonType = 0;
            int jsonCountryid = 0;
            string jsonTitle = "";
            int jsonTier = 0;
            int jsonPremium = 0;

            Log.CheckLogFileSize();
            List<string> log = new List<string>();
            log.Add("Start checking tanks (" + DateTime.Now.ToString() + ")");

            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tank (id, tankTypeId, countryId, name, tier, premium) VALUES (@id, @tankTypeId, @countryId, @name, @tier, @premium)", con);

                JObject root = JObject.Parse(s);
                JArray items = (JArray)root["items"];
                JObject item;
                JToken jtoken;

                for (int i = 0; i < items.Count; i++) //loop through rows
                {
                    item = (JObject)items[i];
                    jtoken = item.First;
                    string tokenValue;
                    bool tankExists = false;
                    while (jtoken != null) //loop through columns
                    {
                        tokenValue = (((JProperty)jtoken).Name.ToString() + " : " + ((JProperty)jtoken).Value.ToString() + "<br />");
                        jtoken = jtoken.Next;
                        

                        if (jtoken != null)
                        {
                            string tokenName = (string)((JProperty)jtoken).Name.ToString();
                            switch (tokenName)
	                        {
		                        case "countryid" : jsonCountryid = (int)((JProperty)jtoken).Value; break;
                                case "type" : jsonType = (int)((JProperty)jtoken).Value; break;
                                case "tier" : jsonTier = (int)((JProperty)jtoken).Value; break;
                                case "premium" : jsonPremium = (int)((JProperty)jtoken).Value; break;
                                case "title" : jsonTitle = (string)((JProperty)jtoken).Value.ToString(); break;
                                case "compDescr" : jsonCompDescr = (int)((JProperty)jtoken).Value; tankExists = TankData.TankExist(jsonCompDescr); break; // Check if tank exsits
                            }
                        }
                    }

                    if (!tankExists) // Only run if Tank does not exists in table
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@id", jsonCompDescr);
                        cmd.Parameters.AddWithValue("@tankTypeId", jsonType);
                        cmd.Parameters.AddWithValue("@countryid", jsonCountryid);
                        cmd.Parameters.AddWithValue("@name", jsonTitle);
                        cmd.Parameters.AddWithValue("@tier", jsonTier);
                        cmd.Parameters.AddWithValue("@premium", jsonPremium);
                        cmd.ExecuteNonQuery();
                        log.Add("  Added new tank: " + jsonTitle + "(" + jsonCompDescr + ")");
                    }
                    else
                    {
                        log.Add("  Check completed, tank exsits: " + jsonTitle + "(" + jsonCompDescr + ")");
                    }
                }
                con.Close();
                log.Add("Import complete! (" + DateTime.Now.ToString() + ")");
                Log.LogToFile(log);
                return log;
            }

            catch (Exception ex)
            {
                log.Add(ex.Message + " (" + DateTime.Now.ToString() + ")");
                return log;
            }
        }

        #endregion


        #region updateWN8


        public static String UpdateWN8()
        {
            String json = FetchDataFromFile("WN8");
            string sql = "";
            string tankId = "";
            string expFrags = "";
            string expDmg = "";
            string expSpot = "";
            string expDef = "";
            string expWR = "";

            JObject allTokens = JObject.Parse(json);
            JArray items = (JArray)allTokens["data"];
            JObject item;
            JToken jtoken;
            for (int i = 0; i < items.Count; i++) //loop through tanks
            {
                item = (JObject)items[i];
                jtoken = item.First;
                string tokenValue;
                while (jtoken != null) //loop through values for each tank
                {
                    tokenValue = (((JProperty)jtoken).Name.ToString() + " : " + ((JProperty)jtoken).Value.ToString() + "<br />");

                    if (jtoken != null)
                    {
                        string tokenName = (string)((JProperty)jtoken).Name.ToString();
                        switch (tokenName)
                        {
                            case "IDNum": tankId = (string)((JProperty)jtoken).Value.ToString(); break;
                            case "expFrag": expFrags = (string)((JProperty)jtoken).Value.ToString(); break;
                            case "expDamage": expDmg = (string)((JProperty)jtoken).Value.ToString(); break;
                            case "expSpot": expSpot = (string)((JProperty)jtoken).Value.ToString(); break;
                            case "expDef": expDef = (string)((JProperty)jtoken).Value.ToString(); break;
                            case "expWinRate": expWR = (string)((JProperty)jtoken).Value.ToString(); break;
                        }
                    }
                    jtoken = jtoken.Next;
                }
                
                sql = sql + "update tank set expDmg = " + expDmg
                                        + ", expWR = " + expWR
                                        + ", expSpot = " + expSpot
                                        + ", expFrags = " + expFrags
                                        + ", expDef = " + expDef
                                        + " where id = " + tankId
                                        + "; ";
            }

            // Execute update statements
            try
            {
                SqlConnection con = new SqlConnection(Config.DatabaseConnection());
                con.Open();
                SqlCommand update = new SqlCommand(sql, con);
                update.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return ("Import Complete");
        }

        #endregion
    }
}
