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
using System.Net.Cache;
using System.Data;

namespace WinApp.Code
{
	public static class ImportWN8Api2DB
	{

		private static string LogText(string logtext)
		{
			return DateTime.Now + " " + logtext;
		}

        public static WebResponse GetResponseNoCache(Uri uri)
        {
            // Set a default policy level for the "http:" and "https" schemes.
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Default);
            HttpWebRequest.DefaultCachePolicy = policy;
            // Create the request.
            WebRequest request = WebRequest.Create(uri);
            // Define a cache policy for this request only. 
            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            request.CachePolicy = noCachePolicy;
            WebResponse response = request.GetResponse();
            Console.WriteLine("IsFromCache? {0}", response.IsFromCache);
            return response;
        }
        
        // updates all WN8 expected values, or only for one tank if added
        public static String UpdateWN8(Form parentForm, int updateOnlyTankId = 0)
		{
			string sql = "";
			int tankId = 0;
			double expFrags = 0;
			double expDmg = 0;
			double expSpot = 0;
			double expDef = 0;
			double expWR = 0;
			int updateCount = 0;
            // Get WN8 from API
            string urlBase = "https://static.modxvm.com/wn8-data-exp/json/wn8exp.json";
            try
            {
                // First reset flag for fetched from api
                DB.ExecuteNonQuery("UPDATE tank SET wn8ExpApiFetch=0;");
                // Get data from API
                string url = urlBase + "?GUID=" + Convert.ToBase64String(Guid.NewGuid().ToByteArray()); 
                // Set a default policy level for the "http:" and "https" schemes.
                HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Default);
                HttpWebRequest.DefaultCachePolicy = policy;
                // Create request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				request.Timeout = 10000;     // 10 secs
                request.KeepAlive = true;
                // Set security
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                // Define a cache policy for this request only. 
                HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                request.CachePolicy = noCachePolicy;
                WebResponse webResponse = request.GetResponse();
				StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());
				string json = responseStream.ReadToEnd();
				responseStream.Close();
				// Get ready to parse through WN8 exp values
				JObject allTokens = JObject.Parse(json);
			
				//JToken headerToken = allTokens.First;
				//JToken versionToken = headerToken.First;
				//WN8Version = (int)versionToken.First;

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
								case "IDNum": tankId = (int)((JProperty)jtoken).Value; break;
								case "expFrag": expFrags = (double)((JProperty)jtoken).Value; break;
								case "expDamage": expDmg = (double)((JProperty)jtoken).Value; break;
								case "expSpot": expSpot = (double)((JProperty)jtoken).Value; break;
								case "expDef": expDef = (double)((JProperty)jtoken).Value; break;
								case "expWinRate": expWR = (double)((JProperty)jtoken).Value; break;
							}
						}
						jtoken = jtoken.Next;
					}

                    if (updateOnlyTankId == 0 || updateOnlyTankId == tankId)
                    {
                        string newsql = "update tank set expDmg = @expDmg, expWR = @expWR, expSpot = @expSpot, expFrags = @expFrags, expDef = @expDef, wn8ExpApiFetch=1 where id = @id;";
                        DB.AddWithValue(ref newsql, "@expDmg", expDmg, DB.SqlDataType.Float);
                        DB.AddWithValue(ref newsql, "@expWR", expWR, DB.SqlDataType.Float);
                        DB.AddWithValue(ref newsql, "@expSpot", expSpot, DB.SqlDataType.Float);
                        DB.AddWithValue(ref newsql, "@expFrags", expFrags, DB.SqlDataType.Float);
                        DB.AddWithValue(ref newsql, "@expDef", expDef, DB.SqlDataType.Float);
                        DB.AddWithValue(ref newsql, "@id", tankId, DB.SqlDataType.Int);
                        sql += newsql;
                        updateCount++;
                    }
				}

			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				string msg = 
					"Could not connect to " + urlBase + ", please check your Internet access." + Environment.NewLine + Environment.NewLine +
					ex.Message + Environment.NewLine +
					ex.InnerException + Environment.NewLine + Environment.NewLine;
				MsgBox.Show(msg, "Problem connecting to http://www.wnefficiency.net", parentForm);
				return "";
			}

			// Execute update statements
			try
			{
				DB.ExecuteNonQuery(sql, true, true);
                int wn8LastUpdated = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
				sql = "update _version_ set version=" + wn8LastUpdated + " where id=2;";
				DB.ExecuteNonQuery(sql, true, true);
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				MsgBox.Show(ex.Message, "Error occured", parentForm);
                return "";
            }

            if (updateCount == 0)
                return ("Did not find WN8 expected values for tank");
            else if (updateCount == 1)
                return ("WN8 expected values updated for tank");
            else
                return ("WN8 expected values updated for " + updateCount.ToString() + " tanks");

        }

        public static String FixMissingWN8(Form parentForm)
        {
            DataTable dtMissing = DB.FetchData(@"
                SELECT tier, tankTypeId
                FROM tank
                WHERE wn8ExpApiFetch=0 and tier > 0
                GROUP BY tier, tankTypeId");
            foreach (DataRow drMissing in dtMissing.Rows)
            {
                string updateSQL = "";
                string sql = @"
                    SELECT expDmg, expWR, expSpot, expFrags, expDef
                    FROM tank
                    WHERE wn8ExpApiFetch=1 and tier = @tier AND tankTypeId = @tankTypeId 
                    GROUP BY expDmg, expWR, expSpot, expFrags, expDef
                    ORDER BY count(id) DESC";
                DB.AddWithValue(ref sql, "@tier", Convert.ToInt32(drMissing["tier"]), DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@tankTypeId", Convert.ToInt32(drMissing["tankTypeId"]), DB.SqlDataType.Int);
                DataTable dtExp = DB.FetchData(sql);
                if (dtExp.Rows.Count > 0)
                {
                    string newsql = @"
                        update tank 
                        set expDmg = @expDmg, expWR = @expWR, expSpot = @expSpot, expFrags = @expFrags, expDef = @expDef 
                        where wn8ExpApiFetch=0 AND tier = @tier AND tankTypeId = @tankTypeId;";
                    DataRow dr = dtExp.Rows[0];
                    DB.AddWithValue(ref newsql, "@expDmg", dr["expDmg"], DB.SqlDataType.Float);
                    DB.AddWithValue(ref newsql, "@expWR", dr["expWR"], DB.SqlDataType.Float);
                    DB.AddWithValue(ref newsql, "@expSpot", dr["expSpot"], DB.SqlDataType.Float);
                    DB.AddWithValue(ref newsql, "@expFrags", dr["expFrags"], DB.SqlDataType.Float);
                    DB.AddWithValue(ref newsql, "@expDef", dr["expDef"], DB.SqlDataType.Float);
                    DB.AddWithValue(ref newsql, "@tier", drMissing["tier"], DB.SqlDataType.Int);
                    DB.AddWithValue(ref newsql, "@tankTypeId", drMissing["tankTypeId"], DB.SqlDataType.Int);
                    updateSQL += newsql;
                }
                DB.ExecuteNonQuery(updateSQL, true, true);
            }
            return "Done adding WN8 exp values for tanks with missing values";
        }

    }
}
