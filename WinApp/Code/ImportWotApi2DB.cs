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

		#region variables

		private static int itemCount;
		private static JToken rootToken;
		private static JToken itemToken;
		private static int itemId;
		private static string insertSql;
		private static string updateSql;
		private static bool ok = true;
		private static DataTable itemsInDB;

		private static List<string> log = new List<string>();
		private static string logAddedItems;
		private static int logAddedItemsCount;
		private static string logItemExists;
		private static int logItemExistsCount;
		//private static string logTanksWithoutDetails; // some special tanks have lacking vehicle details in API
		//private static int logTanksWithoutDetailsCount;

		#endregion


		private enum WotApiType
		{
			Tank = 1,
			Turret = 2,
			Gun = 3,
			Radio = 4,
			Achievement = 5,
			TankDetails = 6
		}

		#region fetchFromAPI

		private static string FetchFromAPI(WotApiType WotAPi, int tankId)
		{
			try
			{
				Log.CheckLogFileSize();
				Log.LogToFile(Environment.NewLine + "Get data from WoT API: " + WotAPi.ToString());
				string url = "";
				if (WotAPi == WotApiType.Tank)
				{
					url = "https://api.worldoftanks.eu/wot/encyclopedia/tanks/?application_id=0a7f2eb79dce0dd45df9b8fedfed7530";
				}
				if (WotAPi == WotApiType.Turret)
				{
					url = "https://api.worldoftanks.eu/wot/encyclopedia/tankturrets/?application_id=0a7f2eb79dce0dd45df9b8fedfed7530";
					itemsInDB = DB.FetchData("select id from modTurret");   // Fetch id of turrets already existing in db
				}
				else if (WotAPi == WotApiType.Gun)
				{
					url = "https://api.worldoftanks.eu/wot/encyclopedia/tankguns/?application_id=0a7f2eb79dce0dd45df9b8fedfed7530";
					itemsInDB = DB.FetchData("select id from modGun");   // Fetch id of guns already existing in db
				}
				else if (WotAPi == WotApiType.Radio)
				{
					url = "https://api.worldoftanks.eu/wot/encyclopedia/tankradios/?application_id=0a7f2eb79dce0dd45df9b8fedfed7530";
					itemsInDB = DB.FetchData("select id from modRadio");   // Fetch id of radios already existing in db
				}
				else if (WotAPi == WotApiType.Achievement)
				{
					url = "https://api.worldoftanks.eu/wot/encyclopedia/achievements/?application_id=0a7f2eb79dce0dd45df9b8fedfed7530";
				}
				else if (WotAPi == WotApiType.TankDetails)
				{
					url = "https://api.worldoftanks.eu/wot/encyclopedia/tankinfo/?application_id=0a7f2eb79dce0dd45df9b8fedfed7530&tank_id=" + tankId;
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
				return s;
			}
			catch (Exception ex)
			{
				Code.MsgBox.Show("Could not connect to WoT API, please check your Internet access." + Environment.NewLine + Environment.NewLine +
					ex.Message + Environment.NewLine +
					ex.InnerException + Environment.NewLine + Environment.NewLine, "Problem connecting to WoT API");
				return "";
			}
			
		}

		#endregion

		//#region getImageFromAPI

		//public static byte[] getImageFromAPI(string url)
		//{
		//	byte[] imgArray;

		//	// Fetch image from url
		//	WebRequest req = WebRequest.Create(url);
		//	WebResponse response = req.GetResponse();
		//	Stream stream = response.GetResponseStream();

		//	// Read into memoryStream
		//	int dataLength = (int)response.ContentLength;
		//	byte[] buffer = new byte[1024];
		//	MemoryStream memStream = new MemoryStream();
		//	while (true)
		//	{
		//		int bytesRead = stream.Read(buffer, 0, buffer.Length);  //Try to read the data
		//		if (bytesRead == 0) break;
		//		memStream.Write(buffer, 0, bytesRead);  //Write the downloaded data
		//	}

		//	// Read into byte array
		//	Image image = Image.FromStream(memStream);
		//	imgArray = memStream.ToArray();

		//	return imgArray;
		//}

		//#endregion

		#region update log file

		private static void updateLog(string itemType)
		{
			// Update log after import
			log.Add("Import complete: (" + DateTime.Now.ToString() + ")");
			if (logAddedItems != null)
			{
				logAddedItems = logAddedItems.Substring(0, logAddedItems.Length - 2);
				log.Add("  Added " + logAddedItemsCount + " new " + itemType + ":");
				log.Add("  " + logAddedItems);
			}
			else
			{
				log.Add("  No new " + itemType + " added");
			}
			if (logItemExists != null)
			{
				logItemExists = logItemExists.Substring(0, logItemExists.Length - 2);
				log.Add("  Updated data on " + logItemExistsCount + " existing " + itemType + ":");
				log.Add("  " + logItemExists);
			}
			Log.LogToFile(log);

			log.Clear();
			logAddedItems = null;
			logAddedItemsCount = 0;
			logItemExists = null;
			logItemExistsCount = 0;
		}

		#endregion

		#region importTanks

		public static String ImportTanks()
		{
			string json = FetchFromAPI(WotApiType.Tank, 0);
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

				log.Add("Start checking tanks (" + DateTime.Now.ToString() + ")");

				try
				{
					JObject allTokens = JObject.Parse(json);
					rootToken = allTokens.First;   // returns status token

					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						itemCount = (int)((JProperty)rootToken).Value;   // returns count (not in use for now)

						rootToken = rootToken.Next;   // start reading tanks
						JToken tanks = rootToken.Children().First();   // read all tokens in data token

						//List<string> logtext = new List<string>();
						string sqlTotal = "";
						foreach (JProperty tank in tanks)   // tank = tankId + child tokens
						{
							Application.DoEvents(); // TODO: testing freeze-problem running API requests
							itemToken = tank.First();   // First() returns only child tokens of tank

							itemId = Int32.Parse(((JProperty)itemToken.Parent).Name);   // step back to parent to fetch the isolated tankId
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
							string isPremium = itemToken["is_premium"].ToString();
							switch (isPremium)
							{
								case "true": premium = 1; break;
								case "false": premium = 0; break;
							}

							// Write to db
							tankExists = TankData.TankExist(itemId);
							insertSql = "INSERT INTO tank (id, tankTypeId, countryId, name, tier, premium) VALUES (@id, @tankTypeId, @countryId, @name, @tier, @premium); ";
							updateSql = "UPDATE tank set tankTypeId=@tankTypeId, countryId=@countryId, name=@name, tier=@tier, premium=@premium WHERE id=@id; " ;

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
								logAddedItems = logAddedItems + name + ", ";
								logAddedItemsCount++;
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
								logItemExists = logItemExists + name + ", ";
								logItemExistsCount++;
							}

							if (!ok)
							{
								log.Add("ERROR - Import incomplete! (" + DateTime.Now.ToString() + ")");
								log.Add("ERROR - SQL:");
								log.Add(insertSql);
								return ("ERROR - Import incomplete!");
							}
						}
						DB.ExecuteNonQuery(sqlTotal, true, true); // Run all SQL in batch
						// Update log file after import
						updateLog("tanks");
					}

					//Code.MsgBox.Show("Tank import complete");
					return ("Import Complete");
				}

				catch (Exception ex)
				{
					log.Add(ex.Message + " (" + DateTime.Now.ToString() + ")");
					return ("ERROR - Import incomplete!" + Environment.NewLine + Environment.NewLine + ex);
				}
			}
		}

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

			// Get WN8 from API
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
			JArray items = (JArray)allTokens["data"];
			JObject item;
			JToken jtoken;
			for (int i = 0; i < items.Count; i++) //loop through tanks
			{
				Application.DoEvents(); // TODO: testing freeze-problem running API requests
				item = (JObject)items[i];
				jtoken = item.First;
				string tokenValue;
				while (jtoken != null) //loop through values for each tank
				{
					Application.DoEvents(); // TODO: testing freeze-problem running API requests
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
				DB.ExecuteNonQuery(sql);
			}
			catch (Exception ex)
			{
				Code.MsgBox.Show(ex.Message, "Error occured");
			}

			return ("Import Complete");
		}

		#endregion

		#region importTurrets

		public static String ImportTurrets()
		{
			string json = FetchFromAPI(WotApiType.Turret, 0);
			if (json == "")
			{
				return "No data imported.";
			}
			else
			{
				log.Add("Start checking turrets (" + DateTime.Now.ToString() + ")");
				string sqlTotal = "";
				try
				{
					JObject allTokens = JObject.Parse(json);
					rootToken = allTokens.First;   // returns status token

					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						itemCount = (int)((JProperty)rootToken).Value;   // returns count (not in use for now)

						rootToken = rootToken.Next;   // start reading modules
						JToken turrets = rootToken.Children().First();   // read all tokens in data token

						foreach (JProperty turret in turrets)   // turret = turretId + child tokens
						{
							Application.DoEvents(); // TODO: testing freeze-problem running API requests
							itemToken = turret.First();   // First() returns only child tokens of turret

							itemId = Int32.Parse(((JProperty)itemToken.Parent).Name);   // step back to parent to fetch the isolated turretId
							JArray tanksArray = (JArray)itemToken["tanks"];
							int tankId = Int32.Parse(tanksArray[0].ToString());   // fetch only the first tank in the array for now (all turrets are related to one tank)
							string name = itemToken["name_i18n"].ToString();
							int tier = Int32.Parse(itemToken["level"].ToString());
							int viewRange = Int32.Parse(itemToken["circular_vision_radius"].ToString());
							int armorFront = Int32.Parse(itemToken["armor_forehead"].ToString());
							int armorSides = Int32.Parse(itemToken["armor_board"].ToString());
							int armorRear = Int32.Parse(itemToken["armor_fedd"].ToString());

							var moduleExists = itemsInDB.Select("id = '" + itemId + "'");
							insertSql = "INSERT INTO modTurret (id, tankId, name, tier, viewRange, armorFront, armorSides, armorRear) VALUES "
									  + "(@id, @tankId, @name, @tier, @viewRange, @armorFront, @armorSides, @armorRear); ";
							updateSql = "UPDATE modTurret set tankId=@tankId, name=@name, tier=@tier, viewRange=@viewRange, armorFront=@armorFront, armorSides=@armorSides, armorRear=@armorRear WHERE id=@id; ";

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
								logAddedItems = logAddedItems + name + ", ";
								logAddedItemsCount++;
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
								logItemExists = logItemExists + name + ", ";
								logItemExistsCount++;
							}

							if (!ok)
							{
								log.Add("ERROR - Import incomplete! (" + DateTime.Now.ToString() + ")");
								log.Add("ERROR - SQL:");
								log.Add(insertSql);
								return ("ERROR - Import incomplete!");
							}
						}
						DB.ExecuteNonQuery(sqlTotal, true, true);
						// Update log file after import
						updateLog("turrets");
					}

					//Code.MsgBox.Show("Turret import complete");
					return ("Import Complete");
					
				}
				
				catch (Exception ex)
				{
					log.Add(ex.Message + " (" + DateTime.Now.ToString() + ")");
					return ("ERROR - Import incomplete!" + Environment.NewLine + Environment.NewLine + ex);
				}
			}
		}

		#endregion

		#region importGuns

		public static String ImportGuns()
		{
			string json = FetchFromAPI(WotApiType.Gun, 0);
			if (json == "")
			{
				return "No data imported.";
			}
			else
			{
				log.Add("Start checking guns (" + DateTime.Now.ToString() + ")");

				try
				{
					JObject allTokens = JObject.Parse(json);
					rootToken = allTokens.First;
					string sqlTotal = "";
					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						itemCount = (int)((JProperty)rootToken).Value;

						rootToken = rootToken.Next;
						JToken guns = rootToken.Children().First();

						// Drop relations to turret and tank before import (new relations will be added)
						string sql = "DELETE FROM modTankGun; DELETE FROM modTurretGun; ";
						DB.ExecuteNonQuery(sql);

						foreach (JProperty gun in guns)
						{
							Application.DoEvents(); // TODO: testing freeze-problem running API requests
							itemToken = gun.First();

							itemId = Int32.Parse(((JProperty)itemToken.Parent).Name);
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

							var moduleExists = itemsInDB.Select("id = '" + itemId + "'");

							insertSql = "INSERT INTO modGun (id, name, tier, dmg1, dmg2, dmg3, pen1, pen2, pen3, fireRate) VALUES "
									  + "(@id, @name, @tier, @dmg1, @dmg2, @dmg3, @pen1, @pen2, @pen3, @fireRate); ";
							updateSql = "UPDATE modGun SET name=@name, tier=@tier, dmg1=@dmg1, dmg2=@dmg2, dmg3=@dmg3, pen1=@pen1, pen2=@pen2, pen3=@pen3, fireRate=@fireRate WHERE id=@id; ";

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
								logAddedItems = logAddedItems + name + ", ";
								logAddedItemsCount++;
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
								logItemExists = logItemExists + name + ", ";
								logItemExistsCount++;
							}

							if (!ok)
							{
								log.Add("ERROR - Import incomplete! (" + DateTime.Now.ToString() + ")");
								log.Add("ERROR - SQL:");
								log.Add(insertSql);
								return ("ERROR - Import incomplete!");
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
						updateLog("guns");

					}
					DB.ExecuteNonQuery(sqlTotal, true, true);
					//Code.MsgBox.Show("Gun import complete");
					return ("Gun import Complete");
				}

				catch (Exception ex)
				{
					log.Add(ex.Message + " (" + DateTime.Now.ToString() + ")");
					return ("ERROR - Import incomplete!" + Environment.NewLine + Environment.NewLine + ex);
				}
			}
		}

		#endregion

		#region importRadios

		public static String ImportRadios()
		{
			string json = FetchFromAPI(WotApiType.Radio, 0);
			if (json == "")
			{
				return "No data imported.";
			}
			else
			{
				log.Add("Start checking radios (" + DateTime.Now.ToString() + ")");

				try
				{
					JObject allTokens = JObject.Parse(json);
					rootToken = allTokens.First;
					string sqlTotal = "";
					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						itemCount = (int)((JProperty)rootToken).Value;

						rootToken = rootToken.Next;
						JToken radios = rootToken.Children().First();

						// Drop relations to tank before import (new relations will be added)
						string sql = "DELETE FROM modTankRadio;";
						DB.ExecuteNonQuery(sql);

						foreach (JProperty radio in radios)
						{
							Application.DoEvents(); // TODO: testing freeze-problem running API requests
							itemToken = radio.First();

							itemId = Int32.Parse(((JProperty)itemToken.Parent).Name);
							string name = itemToken["name_i18n"].ToString();
							int tier = Int32.Parse(itemToken["level"].ToString());
							int signalRange = Int32.Parse(itemToken["distance"].ToString());

							var moduleExists = itemsInDB.Select("id = '" + itemId + "'");
							insertSql = "INSERT INTO modRadio (id, name, tier, signalRange) VALUES (@id, @name, @tier, @signalRange); ";
							updateSql = "UPDATE modRadio SET name=@name, tier=@tier, signalRange=@signalRange WHERE id=@id; ";

							if (moduleExists.Length == 0)
							{
								DB.AddWithValue(ref insertSql, "@id", itemId, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@name", name, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref insertSql, "@tier", tier, DB.SqlDataType.Int);
								DB.AddWithValue(ref insertSql, "@signalRange", signalRange, DB.SqlDataType.Int);
								// ok = DB.ExecuteNonQuery(insertSql);
								sqlTotal += insertSql + Environment.NewLine;
								logAddedItems = logAddedItems + name + ", ";
								logAddedItemsCount++;
							}

							else
							{
								DB.AddWithValue(ref updateSql, "@id", itemId, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@name", name, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref updateSql, "@tier", tier, DB.SqlDataType.Int);
								DB.AddWithValue(ref updateSql, "@signalRange", signalRange, DB.SqlDataType.Int);
								// ok = DB.ExecuteNonQuery(updateSql);
								sqlTotal += updateSql + Environment.NewLine;
								logItemExists = logItemExists + name + ", ";
								logItemExistsCount++;
							}

							if (!ok)
							{
								log.Add("ERROR - Import incomplete! (" + DateTime.Now.ToString() + ")");
								log.Add("ERROR - SQL:");
								log.Add(insertSql);
								return ("ERROR - Import incomplete!");
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
						updateLog("radios");
					}
					DB.ExecuteNonQuery(sqlTotal, true, true);
					//Code.MsgBox.Show("Radio import complete");
					return ("Import Complete");
				}

				catch (Exception ex)
				{
					log.Add(ex.Message + " (" + DateTime.Now.ToString() + ")");
					return ("ERROR - Import incomplete!" + Environment.NewLine + Environment.NewLine + ex);
				}
			}
		}

		#endregion

		#region importAchievements

		public static void ImportAchievements()
		{
			string json = FetchFromAPI(WotApiType.Achievement, 0);
			if (json == "")
			{
				// no action, no data found
			}
			else
			{
				log.Add("Start checking achievements (" + DateTime.Now.ToString() + ")");

				try
				{
					JObject allTokens = JObject.Parse(json);
					JToken rootToken = allTokens.First;
					string sqlTotal = "";
					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						int achCount = (int)((JProperty)rootToken).Value;

						rootToken = rootToken.Next;
						JToken achList = rootToken.Children().First();

						foreach (JProperty ach in achList)
						{
							Application.DoEvents(); // TODO: testing freeze-problem running API requests
							itemToken = ach.First();

							// Check if ach already exists
							if (!TankData.GetAchievmentExist(itemToken["name"].ToString()))
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
								try
								{
									//if (!DB.ExecuteNonQuery(sql)) return;
									sqlTotal += sql + Environment.NewLine;
									//Code.MsgBox.Show("x");
								}
								catch (Exception ex)
								{
									Code.MsgBox.Show(ex.Message);
								}

								logAddedItems = logAddedItems + itemToken["name"].ToString() + ", ";
								logAddedItemsCount++;
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
								logItemExists = logItemExists + itemToken["name"].ToString() + ", ";
								logItemExistsCount++;
							}
						}
						DB.ExecuteNonQuery(sqlTotal, true, true);
						// Update log file after import
						updateLog("achievements");
					}

					//Code.MsgBox.Show("Achievement import complete");
				}

				catch (Exception ex)
				{
					log.Add(ex.Message + " (" + DateTime.Now.ToString() + ")");
					Code.MsgBox.Show(ex.Message);
					//return ("ERROR - Import incomplete!" + Environment.NewLine + Environment.NewLine + ex);
				}
			}
		}

		#endregion



		
		
	}
}
