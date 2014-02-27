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
            string url = "https://api.worldoftanks.eu/wot/encyclopedia/tankturrets/?application_id=2a8bf9a1ee36d6125058bf6efd006caf";
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

            JObject allTokens = JObject.Parse(json);
            rootToken = allTokens.First;

            if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
            {
                rootToken = rootToken.Next;
                moduleCount = (int)((JProperty)rootToken).Value;  // returns count

                rootToken = rootToken.Next;  // start reading modules

                JToken turrets = rootToken.Children().First();
                Stopwatch sw = new Stopwatch();
                sw.Start();
                foreach (JProperty turret in turrets)
                { 
                    moduleToken = turret.First();
                    string parent = ((JProperty)moduleToken.Parent).Name.ToString();
                    string nation_i18n = moduleToken["nation_i18n"].ToString();
                    string armor_fedd = moduleToken["armor_fedd"].ToString();
                    string circular_vision_radius = moduleToken["circular_vision_radius"].ToString();
                    //string weight = moduleToken["weight"].ToString();
                    //string s = moduleToken["name"].ToString();
                    JArray tanksArray = (JArray)moduleToken["tanks"];
                    string tanks = tanksArray[0].ToString();
                }
                sw.Stop();
                TimeSpan ts = sw.Elapsed;
            }

            return ("Import Complete");
        }


        //public static void write2DB()
        //{
        //    try
        //    {
        //        // Cet config data
        //        SqlConnection con = new SqlConnection(Config.Settings.DatabaseConn);
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("INSERT INTO country (countryid, name, shortname) VALUES (@countryid, @name, @shortname)", con);
        //        cmd.Parameters.AddWithValue("@countryid", txtid.Text);
        //        cmd.Parameters.AddWithValue("@name", txtName.Text);
        //        cmd.Parameters.AddWithValue("@shortname", txtShortName.Text);
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}


        #endregion
    }
}
