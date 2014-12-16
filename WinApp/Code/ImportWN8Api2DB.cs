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
	public static class ImportWN8Api2DB
	{

		private static string LogText(string logtext)
		{
			return DateTime.Now + " " + logtext;
		}

		public static String UpdateWN8(Form parentForm)
		{
			string sql = "";
			int tankId = 0;
			double expFrags = 0;
			double expDmg = 0;
			double expSpot = 0;
			double expDef = 0;
			double expWR = 0;
			int WN8Version = 0;
			// Get WN8 from API
			try
			{
				string url = "http://www.wnefficiency.net/exp/expected_tank_values_latest.json";
				HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
				httpRequest.Timeout = 10000;     // 10 secs
				httpRequest.UserAgent = "Wot Numbers " + AppVersion.AssemblyVersion;
				httpRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
				HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
				StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());
				string json = responseStream.ReadToEnd();
				responseStream.Close();
				// Get ready to parse through WN8 exp values
				JObject allTokens = JObject.Parse(json);
			
				JToken headerToken = allTokens.First;
				JToken versionToken = headerToken.First;
				WN8Version = (int)versionToken.First;

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

					string newsql = "update tank set expDmg = @expDmg, expWR = @expWR, expSpot = @expSpot, expFrags = @expFrags, expDef = @expDef where id = @id;";
					DB.AddWithValue(ref newsql, "@expDmg", expDmg, DB.SqlDataType.Float);
					DB.AddWithValue(ref newsql, "@expWR", expWR, DB.SqlDataType.Float);
					DB.AddWithValue(ref newsql, "@expSpot", expSpot, DB.SqlDataType.Float);
					DB.AddWithValue(ref newsql, "@expFrags", expFrags, DB.SqlDataType.Float);
					DB.AddWithValue(ref newsql, "@expDef", expDef, DB.SqlDataType.Float);
					DB.AddWithValue(ref newsql, "@id", tankId, DB.SqlDataType.Int);
					sql += newsql;
				}

			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				string msg = 
					"Could not connect to http://www.wnefficiency.net, please check your Internet access." + Environment.NewLine + Environment.NewLine +
					ex.Message + Environment.NewLine +
					ex.InnerException + Environment.NewLine + Environment.NewLine;
				Code.MsgBox.Show(msg, "Problem connecting to http://www.wnefficiency.net", parentForm);
				return "";
			}

			// Execute update statements
			try
			{
				DB.ExecuteNonQuery(sql, true, true);
				sql = "update _version_ set version=" + WN8Version + " where id=2;";
				DB.ExecuteNonQuery(sql, true, true);
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				Code.MsgBox.Show(ex.Message, "Error occured", parentForm);
			}

			return ("Import Complete");
		}

	}
}
