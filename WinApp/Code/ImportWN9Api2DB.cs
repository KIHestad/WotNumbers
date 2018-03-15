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
using System.Net.Http;

namespace WinApp.Code
{
	public static class ImportWN9Api2DB
	{
        private class TankData
        {
            public int Id { get; set; }
            public int MMrange { get; set; }
            public double WN9exp { get; set; }
            public double WN9scale { get; set; }
            public double WN9nerf { get; set; }
        }


        private static string LogText(string logtext)
		{
			return DateTime.Now + " " + logtext;
		}

		public async static Task<String> UpdateWN9(Form parentForm, int updateOnlyTankId = 0)
        {
            string sql = "";
            double WN9Version = 0;
            int updateCount = 0;
            string url = "http://jaj22.org.uk/tankdata/exp_wn9.json";
            // Get WN8 from API
            try
			{
                HttpClient client = new HttpClient()
                {
                    Timeout = new TimeSpan(0, 0, 10) // 10 seconds
                };
                client.DefaultRequestHeaders.Add("User-Agent", "Wot Numbers " + AppVersion.AssemblyVersion);
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();

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
                    if (updateOnlyTankId == 0 || updateOnlyTankId == item.Id)
                    {
                        string newsql = "update tank set mmrange = @mmrange, wn9exp = @wn9exp, wn9scale = @wn9scale, wn9nerf = @wn9nerf where id = @id;";
                        DB.AddWithValue(ref newsql, "@mmrange", item.MMrange, DB.SqlDataType.Int);
                        DB.AddWithValue(ref newsql, "@wn9exp", item.WN9exp, DB.SqlDataType.Float);
                        DB.AddWithValue(ref newsql, "@wn9scale", item.WN9scale, DB.SqlDataType.Float);
                        DB.AddWithValue(ref newsql, "@wn9nerf", item.WN9nerf, DB.SqlDataType.Float);
                        DB.AddWithValue(ref newsql, "@id", item.Id, DB.SqlDataType.Int);
                        sql += newsql;
                        updateCount++;
                    }
				}
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				string msg =
                    "Could not connect to " + url + ", please check your Internet access." + Environment.NewLine + Environment.NewLine +
					ex.Message + Environment.NewLine +
					ex.InnerException + Environment.NewLine + Environment.NewLine;
				MsgBox.Show(msg, "Problem connecting to http://jaj22.org.uk", parentForm);
				return "";
			}

			// Execute update statements
			try
			{
                await DB.ExecuteNonQueryAsync(sql, true, true);
				sql = "update _version_ set version=@version where id=3;";
                DB.AddWithValue(ref sql, "@version", WN9Version * 100, DB.SqlDataType.Float);
                await DB.ExecuteNonQueryAsync(sql, true, true);
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
