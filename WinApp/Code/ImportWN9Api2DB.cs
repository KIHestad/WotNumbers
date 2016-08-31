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

namespace WinApp.Code
{
	public static class ImportWN9Api2DB
	{
        private class TankData
        {
            public int id { get; set; }
            public int mmrange { get; set; }
            public double wn9exp { get; set; }
            public double wn9scale { get; set; }
            public double wn9nerf { get; set; }
        }


        private static string LogText(string logtext)
		{
			return DateTime.Now + " " + logtext;
		}

		public static String UpdateWN9(Form parentForm, int updateOnlyTankId = 0)
        {
            string sql = "";
            double WN9Version = 0;
            int updateCount = 0;
            // Get WN8 from API
            try
			{
				string url = "http://jaj22.org.uk/tankdata/exp_wn9.json";
				HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
				httpRequest.Timeout = 10000;     // 10 secs
				httpRequest.UserAgent = "Wot Numbers " + AppVersion.AssemblyVersion;
				httpRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
				HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
				StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());
				string json = responseStream.ReadToEnd();
				responseStream.Close();
				// Get ready to parse through WN9 exp values
				JObject allTokens = JObject.Parse(json);
                JToken headerToken = allTokens.First;
                // Get version
                JToken versionToken = headerToken.First;
                WN9Version = (double)versionToken.First;
                // get tank data
                List<TankData> tankData = JsonConvert.DeserializeObject<List<TankData>>(allTokens["data"].ToString());
                foreach (TankData item in tankData)
                {
                    if (updateOnlyTankId == 0 || updateOnlyTankId == item.id)
                    {
                        string newsql = "update tank set mmrange = @mmrange, wn9exp = @wn9exp, wn9scale = @wn9scale, wn9nerf = @wn9nerf where id = @id;";
                        DB.AddWithValue(ref newsql, "@mmrange", item.mmrange, DB.SqlDataType.Int);
                        DB.AddWithValue(ref newsql, "@wn9exp", item.wn9exp, DB.SqlDataType.Float);
                        DB.AddWithValue(ref newsql, "@wn9scale", item.wn9scale, DB.SqlDataType.Float);
                        DB.AddWithValue(ref newsql, "@wn9nerf", item.wn9nerf, DB.SqlDataType.Float);
                        DB.AddWithValue(ref newsql, "@id", item.id, DB.SqlDataType.Int);
                        sql += newsql;
                        updateCount++;
                    }
				}
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				string msg =
                    "Could not connect to http://jaj22.org.uk, please check your Internet access." + Environment.NewLine + Environment.NewLine +
					ex.Message + Environment.NewLine +
					ex.InnerException + Environment.NewLine + Environment.NewLine;
				MsgBox.Show(msg, "Problem connecting to http://jaj22.org.uk", parentForm);
				return "";
			}

			// Execute update statements
			try
			{
				DB.ExecuteNonQuery(sql, true, true);
				sql = "update _version_ set version=@version where id=3;";
                DB.AddWithValue(ref sql, "@version", WN9Version * 100, DB.SqlDataType.Float);
				DB.ExecuteNonQuery(sql, true, true);
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				MsgBox.Show(ex.Message, "Error occured", parentForm);
                return "";
            }

            if (updateCount == 0)
                return ("Did not find WN9 expected values for tank");
            else if (updateCount == 1)
                return ("WN9 expected values updated for tank");
            else
                return ("WN9 expected values updated for " + updateCount.ToString() + " tanks");
        }

	}
}
