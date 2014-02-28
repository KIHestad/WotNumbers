using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WotDBUpdater
{
    class modules2DB
    {

        #region fetchFromAPI

        public static String fetchFromAPI()
        {
            Log.LogToFile("test");

            string url = "https://api.worldoftanks.eu/wot/encyclopedia/tankturrets/?application_id=0a7f2eb79dce0dd45df9b8fedfed7530";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Timeout = 10000;     // 10 secs
            httpRequest.UserAgent = "Code Sample Web Client";

            HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
            StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());

            return responseStream.ReadToEnd();
        }

        #endregion



        #region test

        public static String importTurrets()
        {
            string json = fetchFromAPI();
            int moduleCount;
            JToken rootToken;
            JToken moduleToken;
            string sql = "";

            JObject allTokens = JObject.Parse(json);
            rootToken = allTokens.First;

            if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
            {
                rootToken = rootToken.Next;
                moduleCount = (int)((JProperty)rootToken).Value;  // returns count

                rootToken = rootToken.Next;  // start reading modules

                JToken turrets = rootToken.Children().First();  // read all tokens 
                
                foreach (JProperty turret in turrets)
                { 
                    moduleToken = turret.First();
                    
                    int turretId = Int32.Parse(((JProperty)moduleToken.Parent).Name);
                    JArray tanksArray = (JArray)moduleToken["tanks"];
                    int tankId = Int32.Parse(tanksArray[0].ToString());
                    string name = moduleToken["name_i18n"].ToString();
                    int tier = Int32.Parse(moduleToken["level"].ToString());
                    int viewRange = Int32.Parse(moduleToken["circular_vision_radius"].ToString());
                    int armorFront = Int32.Parse(moduleToken["armor_forehead"].ToString());
                    int armorSides = Int32.Parse(moduleToken["armor_board"].ToString());
                    int armorRear = Int32.Parse(moduleToken["armor_fedd"].ToString());
                    
                    //string weight = moduleToken["weight"].ToString();
                    //string s = moduleToken["name"].ToString();

                    sql = sql + "insert into turret (turretId, tankId, name, tier, viewRange, armorFront, armorSides, armorRear) values"
                              + "('" + turretId + "', '" + tankId + "', '" + name + "', '" + tier + "', '" + viewRange + "', '" + armorFront 
                              + "', '" + armorSides + "', '" + armorRear +"'); ";
                }
                write2DB(sql);
            }

            return ("Import Complete");
        }


        public static void write2DB(string sql)
        {
            try
            {
                SqlConnection con = new SqlConnection(Config.Settings.DatabaseConn);
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion
    }
}
