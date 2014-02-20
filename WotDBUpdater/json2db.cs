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

            try
            {
                ConfigData conf = new ConfigData();
                conf = Config.GetConfig();
                SqlConnection con = new SqlConnection(conf.DatabaseConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO json (jsonId, jsonString) values (1, @json)", con);
                cmd.Parameters.AddWithValue("@json", json);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }












            //json = json.Replace("-", "999");

            JsonTextReader reader = new JsonTextReader(new StringReader(json));



            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (reader.Read())
            {
                String s;
                if (reader.Value != null)
                    s = reader.TokenType.ToString();
                    //Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                else
                    s = reader.TokenType.ToString();
                    //Console.WriteLine("Token: {0}", reader.TokenType);
            }
            sw.Stop();
            TimeSpan ts = sw.Elapsed;

            //// Config data 
            //ConfigData conf = new ConfigData();
            //conf = Config.GetConfig();
            //// Connect to DB 
            //SqlConnection myConnection = new SqlConnection(conf.DatabaseConn);
            //SqlCommand myCommand = new SqlCommand();
            //SqlDataReader dr;
            //// Get all tanks 
            //string sql = "SELECT * FROM tanks";
            //myConnection.Open();
            //myCommand = new SqlCommand(sql, myConnection);
            //dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            //// Loop through datareader 
            //Stopwatch s = new Stopwatch();
            //s.Start();
            //while (dr.Read())
            //{
            //    string tankTitle = dr["tankTitle"].ToString();
            //    JObject dossier = JObject.Parse(json);
            //    int battleCount;
            //    try
            //    {
            //        battleCount = (int)dossier["tanks_v2"][tankTitle]["a15x15"]["battlesCount"];
            //    }
            //    catch (Exception)
            //    {
                    
            //    }
            //    try
            //    {
            //        battleCount = (int)dossier["tanks"][tankTitle]["tankdata"]["battlesCount"];
            //    }
            //    catch (Exception)
            //    {
            //        battleCount = 0;
            //    }
            //    //Console.WriteLine(tankTitle + " " + battleCount);
            //}
            //s.Stop();
            //TimeSpan ts = s.Elapsed;
            
            ////Console.WriteLine(ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds);
            //myConnection.Close();


            return (" > Time spent analyzing file: " + ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds);
        }
    }
}
