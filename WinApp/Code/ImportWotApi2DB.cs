using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApp.Code
{
	class ImportWotApi2DB
	{
		/* 
		 * Import functions for data from WoT API (tanks, modules and achivements)
		 * New items will be added
		 * Existing items will be updated
		 */

		private class LogItems
		{
			public string Type; // Name of the logging = what API is logged
			public string Inserted; // name of items inserted = new items
			public int InsertedCount;
			public string Updated; // name of items updated = existing items
			public int UpdatedCount;
		}

		private enum WotApiType
		{
			Tank = 1,
			Turret = 2,
			Gun = 3,
			Radio = 4,
			Achievement = 5,
			TankDetails = 6,
			PlayersInGarageVehicles = 7,
			Maps = 8,
		}

		#region fetchFromAPI

		private static string WotServerApiUrl()
		{
			string serverURL = "";
			string server = Config.Settings.playerServer;
			// override to EU server for not supported regions
			if (server == "" || server == "ASIA" || server == "KR")
				server = "EU";
			switch (server)
			{
				case "EU": serverURL = "http://api.worldoftanks.eu";  break;
				case "COM": serverURL = "http://api.worldoftanks.com"; break;
				case "NET": serverURL = "http://api.worldoftanks.ru"; break;
				case "ASIA": serverURL = "http://api.worldoftanks-sea.com"; break;
				case "KR": serverURL = "http://api.worldoftanks.kr"; break;
			}
			return serverURL;
		}

		private static string WotApplicationId()
		{
			string applicationId = "";
			string server = Config.Settings.playerServer;
			// override to EU server for not supported regions or if missing
			if (server == "" || server == "ASIA" || server == "KR")
				server = "EU";
			switch (server)
			{
				case "EU": applicationId = "2a70055c41b7a6fff1e35a3ba9cadbf1"; break;
				case "COM": applicationId = "417860beae5ef8a03e11520aaacbf123"; break;
				case "NET": applicationId = "f53b88fef36646161ddfa4418fc5209c"; break;
				case "ASIA": applicationId = ""; break;
				case "KR": applicationId = ""; break;
			}
			return applicationId;
		}

		private static string FetchFromAPI(WotApiType WotAPi, int tankId, Form parentForm)
		{
			try
			{
				Log.CheckLogFileSize();
				Log.AddToLogBuffer(Environment.NewLine + "Get data from WoT API: " + WotAPi.ToString());
				string url = WotServerApiUrl();
				string applicationId = WotApplicationId();
				if (WotAPi == WotApiType.Tank)
				{
					url += "/wot/encyclopedia/tanks/?application_id=" + applicationId;
				}
				if (WotAPi == WotApiType.Turret)
				{
					url += "/wot/encyclopedia/tankturrets/?application_id=" + applicationId;
				}
				else if (WotAPi == WotApiType.Gun)
				{
					url += "/wot/encyclopedia/tankguns/?application_id=" + applicationId;
				}
				else if (WotAPi == WotApiType.Radio)
				{
					url += "/wot/encyclopedia/tankradios/?application_id=" + applicationId;
				}
				else if (WotAPi == WotApiType.Achievement)
				{
					url += "/wot/encyclopedia/achievements/?application_id=" + applicationId;
				}
				else if (WotAPi == WotApiType.Maps)
				{
					url += "/wot/encyclopedia/arenas/?application_id=" + applicationId;
				}
				else if (WotAPi == WotApiType.TankDetails)
				{
					url += "/wot/encyclopedia/tankinfo/?application_id=" + applicationId + "&tank_id=" + tankId;
				}
				else if (WotAPi == WotApiType.PlayersInGarageVehicles)
				{
					url += "/wot/tanks/stats/?application_id=" + applicationId + "&access_token=" + Forms.InGarageApiResult.access_token + "&account_id=" + Forms.InGarageApiResult.account_id + "&in_garage=1";
				}
				Application.DoEvents(); // TODO: testing freeze-problem running API requests
				HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
				httpRequest.Timeout = 10000;     // 10 secs
				httpRequest.UserAgent = "Wot Numbers " + AppVersion.AssemblyVersion;
				httpRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
				HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
				Application.DoEvents(); // TODO: testing freeze-problem running API requests
				StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());
				Application.DoEvents(); // TODO: testing freeze-problem running API requests
				string s = responseStream.ReadToEnd();
				responseStream.Close();
				webResponse.Close();
				Log.WriteLogBuffer();
				return s;
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
				string msg = 
					"Could not connect to WoT API, please check your Internet access." + Environment.NewLine + Environment.NewLine +
					ex.Message + Environment.NewLine +
					ex.InnerException + Environment.NewLine + Environment.NewLine;
				Code.MsgBox.Show(msg, "Problem connecting to WoT API", parentForm);
				return "";
			}
			
		}

		#endregion

		#region update log file

		private static void WriteApiLog(LogItems logItems)
		{
			// Update log after import
			Log.AddToLogBuffer("Import complete: (" + DateTime.Now.ToString() + ")");
			if (logItems.Inserted != null)
			{
				logItems.Inserted = logItems.Inserted.Substring(0, logItems.Inserted.Length - 2);
				Log.AddToLogBuffer("  Added " + logItems.InsertedCount + " new " + logItems.Type + ":");
				Log.AddToLogBuffer("  " + logItems.Inserted);
			}
			else
			{
				Log.AddToLogBuffer("  No new " + logItems.Type + " added");
			}
			if (logItems.Updated != null)
			{
				logItems.Updated = logItems.Updated.Substring(0, logItems.Updated.Length - 2);
				Log.AddToLogBuffer("  Updated data on " + logItems.UpdatedCount + " existing " + logItems.Type + ":");
				Log.AddToLogBuffer("  " + logItems.Updated);
			}
			Log.WriteLogBuffer();
		}

		#endregion

		#region importTanks

		public static String ImportTanks(Form parentForm)
		{
			string json = FetchFromAPI(WotApiType.Tank, 0, parentForm);
			if (json == "")
			{
				return "No data imported.";
			}
			else
			{
				int tankTypeId = 0;
				int countryId = 0;
				int premium = 0;
				bool tankExists = false;

				Log.AddToLogBuffer("Start checking tanks (" + DateTime.Now.ToString() + ")");

				try
				{
					JObject allTokens = JObject.Parse(json);
					JToken rootToken = allTokens.First;   // returns status token

					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						int itemCount = (int)((JProperty)rootToken).Value;   // returns count (not in use for now)

						rootToken = rootToken.Next.Next;   // start reading tanks
						JToken tanks = rootToken.Children().First();   // read all tokens in data token

						LogItems logItems = new LogItems(); // Gather info of result, logged after runned
						string sqlTotal = "";
						foreach (JProperty tank in tanks)   // tank = tankId + child tokens
						{
							Application.DoEvents(); // TODO: testing freeze-problem running API requests
							JToken itemToken = tank.First();   // First() returns only child tokens of tank

							int itemId = Int32.Parse(((JProperty)itemToken.Parent).Name);   // step back to parent to fetch the isolated tankId
							string type = itemToken["type"].ToString();
							switch (type)
							{
								case "lightTank": tankTypeId = 1; break;
								case "mediumTank": tankTypeId = 2; break;
								case "heavyTank": tankTypeId = 3; break;
								case "AT-SPG": tankTypeId = 4; break;
								case "SPG": tankTypeId = 5; break;
							}
							string country = itemToken["nation"].ToString();
							switch (country)
							{
								case "ussr": countryId = 0; break;
								case "germany": countryId = 1; break;
								case "usa": countryId = 2; break;
								case "china": countryId = 3; break;
								case "france": countryId = 4; break;
								case "uk": countryId = 5; break;
								case "japan": countryId = 6; break;
							}
							string name = itemToken["name_i18n"].ToString();
							int tier = Int32.Parse(itemToken["level"].ToString());
							bool isPremium = Convert.ToBoolean(itemToken["is_premium"]);
							premium = 0;
							if (isPremium) premium = 1;

							// Write to db
							tankExists = TankHelper.TankExists(itemId);
							string insertSql = "INSERT INTO tank (id, tankTypeId, countryId, name, tier, premium) VALUES (@id, @tankTypeId, @countryId, @name, @tier, @premium); ";
							string updateSql = "UPDATE tank set tankTypeId=@tankTypeId, countryId=@countryId, name=@name, tier=@tier, premium=@premium WHERE id=@id; " ;

							// insert if tank does not exist
							if (!tankExists)
							{
								DB.AddWithValue(ref insertSql, "@id", itemId, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@tankTypeId", tankTypeId, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@countryId", countryId, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@name", name, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref insertSql, "@tier", tier, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@premium", premium, DB.SqlDataType.Int);
								// ok = DB.ExecuteNonQuery(insertSql);  
								sqlTotal += insertSql + Environment.NewLine;
								logItems.Inserted += name + ", ";
								logItems.InsertedCount++;
							}

							// update if tank exists
							else
							{
								DB.AddWithValue(ref updateSql, "@id", itemId, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@tankTypeId", tankTypeId, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@countryId", countryId, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@name", name, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref updateSql, "@tier", tier, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@premium", premium, DB.SqlDataType.Int);
								// ok = DB.ExecuteNonQuery(updateSql);  
								sqlTotal += updateSql + Environment.NewLine; 
								logItems.Updated += name + ", ";
								logItems.UpdatedCount++;
							}
						}
						DB.ExecuteNonQuery(sqlTotal, true, true); // Run all SQL in batch
						// Update log file after import
						WriteApiLog(logItems);
					}

					//Code.MsgBox.Show("Tank import complete");
					return ("Import Complete");
				}

				catch (Exception ex)
				{
					Log.LogToFile(ex);
					Code.MsgBox.Show(ex.Message, "Error fetching tanks from WoT API", parentForm);
					return ("ERROR - Import incomplete!" + Environment.NewLine + Environment.NewLine + ex);
				}
			}
		}

		#endregion

		#region importTurrets

		public static String ImportTurrets(Form parentForm)
		{
			string json = FetchFromAPI(WotApiType.Turret, 0, parentForm);
			if (json == "")
			{
				return "No data imported.";
			}
			else
			{
				Log.AddToLogBuffer("Start checking turrets (" + DateTime.Now.ToString() + ")");
				string sqlTotal = "";
				try
				{
					JObject allTokens = JObject.Parse(json);
					JToken rootToken = allTokens.First;   // returns status token

					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						int itemCount = (int)((JProperty)rootToken).Value;   // returns count (not in use for now)

						rootToken = rootToken.Next.Next;   // start reading modules
						JToken turrets = rootToken.Children().First();   // read all tokens in data token
						DataTable itemsInDB = DB.FetchData("select id from modTurret");   // Fetch id of turrets already existing in db
						LogItems logItems = new LogItems(); // Gather info of result, logged after runned
						foreach (JProperty turret in turrets)   // turret = turretId + child tokens
						{
							Application.DoEvents(); // TODO: testing freeze-problem running API requests
							JToken itemToken = turret.First();   // First() returns only child tokens of turret

							int itemId = Int32.Parse(((JProperty)itemToken.Parent).Name);   // step back to parent to fetch the isolated turretId
							JArray tanksArray = (JArray)itemToken["tanks"];
							int tankId = Int32.Parse(tanksArray[0].ToString());   // fetch only the first tank in the array for now (all turrets are related to one tank)
							string name = itemToken["name_i18n"].ToString();
							int tier = Int32.Parse(itemToken["level"].ToString());
							int viewRange = Int32.Parse(itemToken["circular_vision_radius"].ToString());
							int armorFront = Int32.Parse(itemToken["armor_forehead"].ToString());
							int armorSides = Int32.Parse(itemToken["armor_board"].ToString());
							int armorRear = Int32.Parse(itemToken["armor_fedd"].ToString());

							DataRow[] moduleExists = itemsInDB.Select("id = '" + itemId + "'");
							string insertSql = 
								"INSERT INTO modTurret (id, tankId, name, tier, viewRange, armorFront, armorSides, armorRear) VALUES "
								+ "(@id, @tankId, @name, @tier, @viewRange, @armorFront, @armorSides, @armorRear); ";
							string updateSql = 
								"UPDATE modTurret set tankId=@tankId, name=@name, tier=@tier, viewRange=@viewRange, armorFront=@armorFront, armorSides=@armorSides, armorRear=@armorRear WHERE id=@id; ";

							if (moduleExists.Length == 0)
							{
								DB.AddWithValue(ref insertSql, "@id", itemId, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@tankId", tankId, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@name", name, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref insertSql, "@tier", tier, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@viewRange", viewRange, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@armorFront", armorFront, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@armorSides", armorSides, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@armorRear", armorRear, DB.SqlDataType.Int);
								// ok = DB.ExecuteNonQuery(insertSql);
								sqlTotal += insertSql + Environment.NewLine;
								logItems.Inserted += name + ", ";
								logItems.InsertedCount++;
							}

							else
							{
								DB.AddWithValue(ref updateSql, "@id", itemId, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@tankId", tankId, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@name", name, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref updateSql, "@tier", tier, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@viewRange", viewRange, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@armorFront", armorFront, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@armorSides", armorSides, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@armorRear", armorRear, DB.SqlDataType.Int);
								// ok = DB.ExecuteNonQuery(updateSql);
								sqlTotal += updateSql + Environment.NewLine;
								logItems.Updated += name + ", ";
								logItems.UpdatedCount++;
							}
						}
						DB.ExecuteNonQuery(sqlTotal, true, true);
						// Update log file after import
						WriteApiLog(logItems);
					}

					//Code.MsgBox.Show("Turret import complete");
					return ("Import Complete");
					
				}
				
				catch (Exception ex)
				{
					Log.LogToFile(ex);
					Code.MsgBox.Show(ex.Message, "Error fetching turrets from WoT API", parentForm);
					return ("ERROR - Import incomplete!" + Environment.NewLine + Environment.NewLine + ex);
				}
			}
		}

		#endregion

		#region importGuns

		public static String ImportGuns(Form parentForm)
		{
			string json = FetchFromAPI(WotApiType.Gun, 0, parentForm);
			if (json == "")
			{
				return "No data imported.";
			}
			else
			{
				Log.AddToLogBuffer("Start checking guns (" + DateTime.Now.ToString() + ")");

				try
				{
					JObject allTokens = JObject.Parse(json);
					JToken rootToken = allTokens.First;
					string sqlTotal = "";
					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						int itemCount = (int)((JProperty)rootToken).Value;

						rootToken = rootToken.Next.Next;
						JToken guns = rootToken.Children().First();

						// Drop relations to turret and tank before import (new relations will be added)
						string sql = "DELETE FROM modTankGun; DELETE FROM modTurretGun; ";
						DB.ExecuteNonQuery(sql);

						DataTable itemsInDB = DB.FetchData("select id from modGun");   // Fetch id of guns already existing in db
						LogItems logItems = new LogItems(); // Gather info of result, logged after runned
						foreach (JProperty gun in guns)
						{
							Application.DoEvents(); // TODO: testing freeze-problem running API requests
							JToken itemToken = gun.First();

							int itemId = Int32.Parse(((JProperty)itemToken.Parent).Name);
							string name = itemToken["name_i18n"].ToString();
							int tier = Int32.Parse(itemToken["level"].ToString());
							JArray dmgArray = (JArray)itemToken["damage"];
							int dmg1 = Int32.Parse(dmgArray[0].ToString());
							int dmg2 = 0;                                                           // guns have 1, 2 or 3 types of ammo
							if (dmgArray.Count > 1) dmg2 = Int32.Parse(dmgArray[1].ToString());     // fetch damage and penetration if available
							int dmg3 = 0;
							if (dmgArray.Count > 2) dmg3 = Int32.Parse(dmgArray[2].ToString());
							JArray penArray = (JArray)itemToken["piercing_power"];
							int pen1 = Int32.Parse(penArray[0].ToString());
							int pen2 = 0;
							if (penArray.Count > 1) pen2 = Int32.Parse(penArray[1].ToString());
							int pen3 = 0;
							if (penArray.Count > 2) pen3 = Int32.Parse(penArray[2].ToString());
							string fireRate = ((itemToken["rate"].ToString())).Replace(",", ".");

							DataRow[] moduleExists = itemsInDB.Select("id = '" + itemId + "'");

							string insertSql = 
								"INSERT INTO modGun (id, name, tier, dmg1, dmg2, dmg3, pen1, pen2, pen3, fireRate) VALUES "
								+ "(@id, @name, @tier, @dmg1, @dmg2, @dmg3, @pen1, @pen2, @pen3, @fireRate); ";
							string updateSql = 
								"UPDATE modGun SET name=@name, tier=@tier, dmg1=@dmg1, dmg2=@dmg2, dmg3=@dmg3, pen1=@pen1, pen2=@pen2, pen3=@pen3, fireRate=@fireRate WHERE id=@id; ";

							if (moduleExists.Length == 0)
							{
								DB.AddWithValue(ref insertSql, "@id", itemId, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@name", name, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref insertSql, "@tier", tier, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@dmg1", dmg1, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@dmg2", dmg2, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@dmg3", dmg3, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@pen1", pen1, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@pen2", pen2, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@pen3", pen3, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@fireRate", fireRate, DB.SqlDataType.Int);
								//ok = DB.ExecuteNonQuery(insertSql);
								sqlTotal += insertSql + Environment.NewLine;
								logItems.Inserted += name + ", ";
								logItems.InsertedCount++;
							}

							else
							{
								DB.AddWithValue(ref updateSql, "@id", itemId, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@name", name, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref updateSql, "@tier", tier, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@dmg1", dmg1, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@dmg2", dmg2, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@dmg3", dmg3, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@pen1", pen1, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@pen2", pen2, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@pen3", pen3, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@fireRate", fireRate, DB.SqlDataType.Int);
								// ok = DB.ExecuteNonQuery(updateSql);
								sqlTotal += updateSql + Environment.NewLine;
								logItems.Updated += name + ", ";
								logItems.UpdatedCount++;
							}

							// Create relation to turret if possible (not all tanks have a turret)
							JArray turretArray = (JArray)itemToken["turrets"];
							if (turretArray.Count > 0)
							{
								for (int i = 0; i < turretArray.Count; i++)
								{
									int turretId = Int32.Parse(turretArray[i].ToString());
									insertSql = "INSERT INTO modTurretGun (turretId, gunId) VALUES ( " + turretId + ", " + itemId + "); ";
									//DB.ExecuteNonQuery(insertSql);
									sqlTotal += insertSql + Environment.NewLine;
								}
							}

							// Create relation to tank
							JArray tankArray = (JArray)itemToken["tanks"];
							if (tankArray.Count > 0)
							{
								for (int i = 0; i < tankArray.Count; i++)
								{
									int tankId = Int32.Parse(tankArray[i].ToString());
									insertSql = "INSERT INTO modTankGun (tankId, gunId) VALUES ( " + tankId + ", " + itemId + "); ";
									//DB.ExecuteNonQuery(insertSql);
									sqlTotal += insertSql + Environment.NewLine;
								}
							}
						}

						// Update log file after import
						WriteApiLog(logItems);

					}
					DB.ExecuteNonQuery(sqlTotal, true, true);
					//Code.MsgBox.Show("Gun import complete");
					return ("Gun import Complete");
				}

				catch (Exception ex)
				{
					Log.LogToFile(ex);
					Code.MsgBox.Show(ex.Message, "Error fetching guns from WoT API", parentForm);
					return ("ERROR - Import incomplete!" + Environment.NewLine + Environment.NewLine + ex);
				}
			}
		}

		#endregion

		#region importRadios

		public static String ImportRadios(Form parentForm)
		{
			string json = FetchFromAPI(WotApiType.Radio, 0, parentForm);
			if (json == "")
			{
				return "No data imported.";
			}
			else
			{
				Log.AddToLogBuffer("Start checking radios (" + DateTime.Now.ToString() + ")");

				try
				{
					JObject allTokens = JObject.Parse(json);
					JToken rootToken = allTokens.First;
					string sqlTotal = "";
					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						int itemCount = (int)((JProperty)rootToken).Value;

						rootToken = rootToken.Next.Next;
						JToken radios = rootToken.Children().First();

						// Drop relations to tank before import (new relations will be added)
						string sql = "DELETE FROM modTankRadio;";
						DB.ExecuteNonQuery(sql);

						DataTable itemsInDB = DB.FetchData("select id from modRadio");   // Fetch id of radios already existing in db
						LogItems logItems = new LogItems(); // Gather info of result, logged after runned

						foreach (JProperty radio in radios)
						{
							Application.DoEvents(); // TODO: testing freeze-problem running API requests
							JToken itemToken = radio.First();

							int itemId = Int32.Parse(((JProperty)itemToken.Parent).Name);
							string name = itemToken["name_i18n"].ToString();
							int tier = Int32.Parse(itemToken["level"].ToString());
							int signalRange = Int32.Parse(itemToken["distance"].ToString());

							DataRow[] moduleExists = itemsInDB.Select("id = '" + itemId + "'");
							string insertSql = "INSERT INTO modRadio (id, name, tier, signalRange) VALUES (@id, @name, @tier, @signalRange); ";
							string updateSql = "UPDATE modRadio SET name=@name, tier=@tier, signalRange=@signalRange WHERE id=@id; ";

							if (moduleExists.Length == 0)
							{
								DB.AddWithValue(ref insertSql, "@id", itemId, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@name", name, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref insertSql, "@tier", tier, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@signalRange", signalRange, DB.SqlDataType.Int);
								// ok = DB.ExecuteNonQuery(insertSql);
								sqlTotal += insertSql + Environment.NewLine;
								logItems.Inserted += name + ", ";
								logItems.InsertedCount++;
							}

							else
							{
								DB.AddWithValue(ref updateSql, "@id", itemId, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@name", name, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref updateSql, "@tier", tier, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@signalRange", signalRange, DB.SqlDataType.Int);
								// ok = DB.ExecuteNonQuery(updateSql);
								sqlTotal += updateSql + Environment.NewLine;
								logItems.Updated += name + ", ";
								logItems.UpdatedCount++;
							}

							// Create relation to tank
							JArray tankArray = (JArray)itemToken["tanks"];
							if (tankArray.Count > 0)
							{
								for (int i = 0; i < tankArray.Count; i++)
								{
									int tankId = Int32.Parse(tankArray[i].ToString());
									insertSql = "INSERT INTO modTankRadio (tankId, radioId) VALUES ( " + tankId + ", " + itemId + "); ";
									// DB.ExecuteNonQuery(insertSql);
									sqlTotal += insertSql + Environment.NewLine;
								}
							}
						}

						// Update log file after import
						WriteApiLog(logItems);
					}
					DB.ExecuteNonQuery(sqlTotal, true, true);
					//Code.MsgBox.Show("Radio import complete");
					return ("Import Complete");
				}

				catch (Exception ex)
				{
					Log.LogToFile(ex);
					Code.MsgBox.Show(ex.Message, "Error fetching radios from WoT API", parentForm);
					return ("ERROR - Import incomplete!" + Environment.NewLine + Environment.NewLine + ex);
				}
			}
		}

		#endregion

		#region importMaps

		public static String ImportMaps(Form parentForm)
		{
			string json = FetchFromAPI(WotApiType.Maps, 0, parentForm);
			if (json == "")
			{
				return "No data imported.";
			}
			else
			{
				Log.AddToLogBuffer("Start checking maps (" + DateTime.Now.ToString() + ")");

				try
				{
					JObject allTokens = JObject.Parse(json);
					JToken rootToken = allTokens.First;
					LogItems logItems = new LogItems(); // Gather info of result, logged after runned
					string sqlTotal = "";
					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						int itemCount = (int)((JProperty)rootToken).Value;
						rootToken = rootToken.Next.Next;
						JToken maps = rootToken.Children().First();
						foreach (JProperty map in maps)
						{
							Application.DoEvents(); // TODO: testing freeze-problem running API requests
							JToken itemToken = map.First();
							string name = itemToken["name_i18n"].ToString();
							name = name.Replace("'","");
							string description = itemToken["description"].ToString();
							string arena_id = itemToken["arena_id"].ToString();
							string updateSql = "UPDATE map SET description=@description, arena_id=@arena_id WHERE name=@name; " ;
							DB.AddWithValue(ref updateSql, "@name", name, DB.SqlDataType.VarChar);
							DB.AddWithValue(ref updateSql, "@description", description, DB.SqlDataType.VarChar);
							DB.AddWithValue(ref updateSql, "@arena_id", arena_id, DB.SqlDataType.VarChar);
							sqlTotal += updateSql + "\n" + Environment.NewLine;
							logItems.Inserted += name + ", ";
							logItems.InsertedCount++;
						}

						// Update log file after import
						WriteApiLog(logItems);
					}
					DB.ExecuteNonQuery(sqlTotal, true, true);
					return ("Import Complete");
				}

				catch (Exception ex)
				{
					Log.LogToFile(ex);
					return ("ERROR - Import incomplete!" + Environment.NewLine + Environment.NewLine + ex);
				}
			}
		}

		#endregion


		#region importAchievements

		public static void ImportAchievements(Form parentForm)
		{
			string json = FetchFromAPI(WotApiType.Achievement, 0, parentForm);
			if (json == "")
			{
				// no action, no data found
			}
			else
			{
				Log.AddToLogBuffer("Start checking achievements (" + DateTime.Now.ToString() + ")");

				try
				{
					JObject allTokens = JObject.Parse(json);
					JToken rootToken = allTokens.First;
					string sqlTotal = "";
					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						int achCount = (int)((JProperty)rootToken).Value;

						rootToken = rootToken.Next.Next;
						JToken achList = rootToken.Children().First();
						LogItems logItems = new LogItems(); // Gather info of result, logged after runned
						foreach (JProperty ach in achList)
						{
							Application.DoEvents(); // TODO: testing freeze-problem running API requests
							JToken itemToken = ach.First();

							// Check if ach already exists
							if (!TankHelper.GetAchievmentExist(itemToken["name"].ToString()))
							{
								string sql = "INSERT INTO ACH (name, section, section_order, name_i18n, type, ordernum, description) " +
											"VALUES (@name, @section, 0, @name_i18n, @type, @ordernum, @description); ";
								// Get data from json token and insert to query
								// string tokenName = ((JProperty)moduleToken.Parent).Name.ToString()); // Not in use
								DB.AddWithValue(ref sql, "@name", itemToken["name"].ToString(), DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@section", itemToken["section"].ToString(), DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@section_order", itemToken["section_order"].ToString(), DB.SqlDataType.Int);
								DB.AddWithValue(ref sql, "@type", itemToken["type"].ToString(), DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@ordernum", itemToken["order"].ToString(), DB.SqlDataType.Int);
								DB.AddWithValue(ref sql, "@description", itemToken["description"].ToString(), DB.SqlDataType.VarChar);
								// Check if several medal alternatives, and get images and names, set NULL as default value
								string options = itemToken["options"].ToString();
								if (options == "") // no options, get default medal image and name
								{
									DB.AddWithValue(ref sql, "@imgPath", itemToken["image"].ToString(), DB.SqlDataType.VarChar);
									// insert img...
									DB.AddWithValue(ref sql, "@name_i18n", itemToken["name_i18n"].ToString(), DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@options", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@img1Path", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@img2Path", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@img3Path", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@img4Path", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@name_i18n1", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@name_i18n2", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@name_i18n3", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@name_i18n4", DBNull.Value, DB.SqlDataType.VarChar);
								}
								else // get medal optional images and names
								{
									DB.AddWithValue(ref sql, "@imgPath", DBNull.Value, DB.SqlDataType.VarChar);
									 // insert img...
									DB.AddWithValue(ref sql, "@name_i18n", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@options", options, DB.SqlDataType.VarChar);
									// Get the medal options from array
									JArray medalArray = (JArray)itemToken["options"];
									int num = medalArray.Count;
									if (num > 4) num = 4;
									for (int i = 1; i <= num; i++)
									{
										DB.AddWithValue(ref sql, "@img" + i.ToString() + "Path", medalArray[i - 1]["image"].ToString(), DB.SqlDataType.VarChar);
										DB.AddWithValue(ref sql, "@name_i18n" + i.ToString(), medalArray[i - 1]["name_i18n"].ToString(), DB.SqlDataType.VarChar);
										// insert img...
									}
									// If not 4, put null in rest
									for (int i = num + 1; i <= 4; i++)
									{
										DB.AddWithValue(ref sql, "@img" + i.ToString() +"Path", DBNull.Value, DB.SqlDataType.VarChar);
										DB.AddWithValue(ref sql, "@name_i18n" + i.ToString(), DBNull.Value, DB.SqlDataType.VarChar);
										// insert img...
									}

								}
								// Insert to db now
								sqlTotal += sql + Environment.NewLine;
								logItems.Inserted += itemToken["name"].ToString() + ", ";
								logItems.InsertedCount++;
							}
							else
							{
								//string sql = "UPDATE ach SET section=@section, options=@options, section_order=@section_order, imgPath=@imgPath, name_i18n=@name_i18n, "
								//           + "type=@type, ordernum=@ordernum, description=@description, img1Path=@img1Path, img2Path=@img2Path, img3Path=@img3Path, img4Path=@img4Path, "
								//           + "name_i18n1=@name_i18n1, name_i18n2=@name_i18n2, name_i18n3=@name_i18n3, name_i18n4=@name_i18n4 WHERE name=@name";
								string sql = "UPDATE ach SET section=@section, name_i18n=@name_i18n, "
										   + "type=@type, ordernum=@ordernum, description=@description WHERE name=@name; ";

								// Get data from json token and insert to query
								// string tokenName = ((JProperty)moduleToken.Parent).Name.ToString()); // Not in use
								DB.AddWithValue(ref sql, "@name", itemToken["name"].ToString(), DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@section", itemToken["section"].ToString(), DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@section_order", itemToken["section_order"].ToString(), DB.SqlDataType.Int);
								DB.AddWithValue(ref sql, "@type", itemToken["type"].ToString(), DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@ordernum", itemToken["order"].ToString(), DB.SqlDataType.Int);
								DB.AddWithValue(ref sql, "@description", itemToken["description"].ToString(), DB.SqlDataType.VarChar);
								// Check if several medal alternatives, and get images and names, set NULL as default value
								string options = itemToken["options"].ToString();
								if (options == "") // no options, get default medal image and name
								{
									DB.AddWithValue(ref sql, "@imgPath", itemToken["image"].ToString(), DB.SqlDataType.VarChar);
									// insert img...
									DB.AddWithValue(ref sql, "@name_i18n", itemToken["name_i18n"].ToString(), DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@options", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@img1Path", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@img2Path", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@img3Path", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@img4Path", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@name_i18n1", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@name_i18n2", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@name_i18n3", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@name_i18n4", DBNull.Value, DB.SqlDataType.VarChar);
								}
								else // get medal optional images and names
								{
									DB.AddWithValue(ref sql, "@imgPath", DBNull.Value, DB.SqlDataType.VarChar);
									// insert img...
									DB.AddWithValue(ref sql, "@name_i18n", DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@options", options, DB.SqlDataType.VarChar);
									// Get the medal options from array
									JArray medalArray = (JArray)itemToken["options"];
									int num = medalArray.Count;
									if (num > 4) num = 4;
									for (int i = 1; i <= num; i++)
									{
										DB.AddWithValue(ref sql, "@img" + i.ToString() + "Path", medalArray[i - 1]["image"].ToString(), DB.SqlDataType.VarChar);
										DB.AddWithValue(ref sql, "@name_i18n" + i.ToString(), medalArray[i - 1]["name_i18n"].ToString(), DB.SqlDataType.VarChar);
										// insert img...
									}
									// If not 4, put null in rest
									for (int i = num + 1; i <= 4; i++)
									{
										DB.AddWithValue(ref sql, "@img" + i.ToString() + "Path", DBNull.Value, DB.SqlDataType.VarChar);
										DB.AddWithValue(ref sql, "@name_i18n" + i.ToString(), DBNull.Value, DB.SqlDataType.VarChar);
										 // insert img...
									}
								}

								// Update db now
								// if (!DB.ExecuteNonQuery(sql)) return;
								sqlTotal = sql + Environment.NewLine;
								logItems.Updated += itemToken["name"].ToString() + ", ";
								logItems.UpdatedCount++;
							}
						}
						DB.ExecuteNonQuery(sqlTotal, true, true);
						// Update log file after import
						WriteApiLog(logItems);
					}

					//Code.MsgBox.Show("Achievement import complete");
				}

				catch (Exception ex)
				{
					Log.LogToFile(ex);
					Code.MsgBox.Show(ex.Message, "Error fetching acheivments from WoT API", parentForm);
					//return ("ERROR - Import incomplete!" + Environment.NewLine + Environment.NewLine + ex);
				}
			}
		}

		#endregion

		#region importPlayersInGarageVehicles

		public static List<int> ImportPlayersInGarageVehicles(Form parentForm)
		{
			List<int> tanksInGarage = new List<int>();
			string json = FetchFromAPI(WotApiType.PlayersInGarageVehicles, 0, parentForm);
			if (json != "")
			{
				Log.AddToLogBuffer("Start checking players tanks in garage (" + DateTime.Now.ToString() + ")");
				try
				{
					JObject allTokens = JObject.Parse(json);
					JToken rootToken = allTokens.First;   // returns status token

					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						int itemCount = (int)((JProperty)rootToken).Value;   // returns count (not in use for now)
						rootToken = rootToken.Next;   // set root to data element
						JToken player = rootToken.Children().First();   // get player element
						string jsonPlayerTanks = player.ToString();
						JObject o = JObject.Parse(jsonPlayerTanks);
						JArray arr = (JArray) o.SelectToken(Forms.InGarageApiResult.account_id);
						foreach (JToken tank in arr)
						{
							int tankId = Convert.ToInt32(tank["tank_id"]);
							tanksInGarage.Add(tankId);
						}
						Log.AddToLogBuffer("Found " + tanksInGarage.Count + " tanks in garage");
						Log.WriteLogBuffer();
					}
				}

				catch (Exception ex)
				{
					Log.LogToFile(ex);
					Code.MsgBox.Show(ex.Message, "Error fetching tanks in garage from WoT API", parentForm);
				}
			}
			return tanksInGarage;
		}

		#endregion

		
		
	}
}
