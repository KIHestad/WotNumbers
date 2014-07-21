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

		#region importTanks

		//public static List<string> UpdateTanks()
		//{
		//	string appPath = Path.GetDirectoryName(Application.ExecutablePath);
		//	string jsonfile = appPath + "/Dossier2json/tanks.json";
		//	StringBuilder sb = new StringBuilder();
		//	using (StreamReader sr = new StreamReader(jsonfile))
		//	{
		//		String line;
		//		// Read and display lines from the file until the end of 
		//		// the file is reached.
		//		while ((line = sr.ReadLine()) != null)
		//		{
		//			sb.AppendLine(line);
		//		}
		//	}
		//	string json = sb.ToString();
		//	String s = "{items:" + json + "}";
		//	int jsonCompDescr = 0;
		//	int jsonType = 0;
		//	int jsonCountryid = 0;
		//	string jsonTitle = "";
		//	int jsonTier = 0;
		//	int jsonPremium = 0;

		//	Log.CheckLogFileSize();
		//	List<string> log = new List<string>();
		//	log.Add("Start checking tanks (" + DateTime.Now.ToString() + ")");

		//	try
		//	{
		//		JObject root = JObject.Parse(s);
		//		JArray items = (JArray)root["items"];
		//		JObject item;
		//		JToken jtoken;
		//		bool ok = true;
		//		for (int i = 0; i < items.Count; i++) //loop through rows
		//		{
		//			item = (JObject)items[i];
		//			jtoken = item.First;
		//			string tokenValue;
		//			bool tankExists = false;
		//			while (jtoken != null) //loop through columns
		//			{
		//				tokenValue = (((JProperty)jtoken).Name.ToString() + " : " + ((JProperty)jtoken).Value.ToString() + "<br />");
		//				jtoken = jtoken.Next;
						

		//				if (jtoken != null)
		//				{
		//					string tokenName = (string)((JProperty)jtoken).Name.ToString();
		//					switch (tokenName)
		//					{
		//						case "countryid" : jsonCountryid = (int)((JProperty)jtoken).Value; break;
		//						case "type" : jsonType = (int)((JProperty)jtoken).Value; break;
		//						case "tier" : jsonTier = (int)((JProperty)jtoken).Value; break;
		//						case "premium" : jsonPremium = (int)((JProperty)jtoken).Value; break;
		//						case "title" : jsonTitle = (string)((JProperty)jtoken).Value.ToString(); break;
		//						case "compDescr" : jsonCompDescr = (int)((JProperty)jtoken).Value; break; // Check if tank exsits
		//					}

		//				}
		//			}
		//			tankExists = TankData.TankExist(jsonCompDescr);
		//			string sql = "INSERT INTO tank (id, tankTypeId, countryId, name, tier, premium) VALUES (@id, @tankTypeId, @countryId, @name, @tier, @premium)";
		//			if (!tankExists) // Only run if Tank does not exists in table
		//			{
		//				DB.AddWithValue(ref sql, "@id", jsonCompDescr, DB.SqlDataType.Int);
		//				DB.AddWithValue(ref sql, "@tankTypeId", jsonType, DB.SqlDataType.Int);
		//				DB.AddWithValue(ref sql, "@countryId", jsonCountryid, DB.SqlDataType.Int);
		//				DB.AddWithValue(ref sql, "@name", jsonTitle, DB.SqlDataType.VarChar);
		//				DB.AddWithValue(ref sql, "@tier", jsonTier, DB.SqlDataType.Int);
		//				DB.AddWithValue(ref sql, "@premium", jsonPremium, DB.SqlDataType.Int);
		//				ok = DB.ExecuteNonQuery(sql);
		//				log.Add("  Added new tank: " + jsonTitle + "(" + jsonCompDescr + ")");
		//			}
		//			else
		//			{
		//				log.Add("  Check completed, tank exsits: " + jsonTitle + "(" + jsonCompDescr + ")");
		//			}
		//			if (!ok)
		//			{
		//				log.Add("ERROR - Import incomplete! (" + DateTime.Now.ToString() + ")");
		//				log.Add("ERROR - SQL:");
		//				log.Add(sql);
		//				return log;
		//			}
		//		}
		//		log.Add("Import complete! (" + DateTime.Now.ToString() + ")");
		//		Log.LogToFile(log);
		//		return log;
		//	}

		//	catch (Exception ex)
		//	{
		//		log.Add(ex.Message + " (" + DateTime.Now.ToString() + ")");
		//		return log;
		//	}
		//}

		#endregion



		#region updateWN8


		public static String UpdateWN8()
		{
			string sql = "";
			string tankId = "";
			string expFrags = "";
			string expDmg = "";
			string expSpot = "";
			string expDef = "";
			string expWR = "";
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

			}
			catch (Exception ex)
			{
				Code.MsgBox.Show("Could not connect to http://www.wnefficiency.net, please check your Internet access." + Environment.NewLine + Environment.NewLine +
					ex.Message + Environment.NewLine +
					ex.InnerException + Environment.NewLine + Environment.NewLine, "Problem connecting to http://www.wnefficiency.net");
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
				Code.MsgBox.Show(ex.Message, "Error occured");
			}

			return ("Import Complete");
		}

		#endregion
	}
}
