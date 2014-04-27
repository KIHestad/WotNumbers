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

namespace WotDBUpdater.Code
{
	class ImportWotApi2DB
	{
		/* 
		 * Import functions for data from WoT API (tanks, modules and achivements)
         * New items will be added
         * Existing items will be updated
		 */

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


		private enum WotApiType
		{
			Tank = 1,
            Turret = 2,
			Gun = 3,
			Radio = 4,
			Achievement = 5
		}

		#region fetchFromAPI

		private static string FetchFromAPI(WotApiType WotAPi)
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
				}
				else if (WotAPi == WotApiType.Radio)
				{
					url = "https://api.worldoftanks.eu/wot/encyclopedia/tankradios/?application_id=0a7f2eb79dce0dd45df9b8fedfed7530";
				}
				else if (WotAPi == WotApiType.Achievement)
				{
					url = "https://api.worldoftanks.eu/wot/encyclopedia/achievements/?application_id=0a7f2eb79dce0dd45df9b8fedfed7530";
				}
				HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
				httpRequest.Timeout = 10000;     // 10 secs
				httpRequest.UserAgent = "Code Sample Web Client";
				HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
				StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());
				string s = responseStream.ReadToEnd();
				responseStream.Close();
				webResponse.Close();
				return s;
			}
			catch (Exception ex)
			{
				Code.MsgBox.Show("Could not connect to WoT API, please check your Internet access." + Environment.NewLine + Environment.NewLine +
					ex.Message, "Problem connecting to WoT API");
				return "";
			}
			
		}

		#endregion

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
            string json = FetchFromAPI(WotApiType.Tank);
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

                        List<string> logtext = new List<string>();

                        foreach (JProperty tank in tanks)   // tank = tankId + child tokens
                        {
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
                            insertSql = "INSERT INTO tank (id, tankTypeId, countryId, name, tier, premium) VALUES (@id, @tankTypeId, @countryId, @name, @tier, @premium)";
                            updateSql = "UPDATE tank set tankTypeId=@tankTypeId, countryId=@countryId, name=@name, tier=@tier, premium=@premium where id=@id";

                            // insert if tank does not exist
                            if (!tankExists)
                            {
                                DB.AddWithValue(ref insertSql, "@id", itemId, DB.SqlDataType.Int);
                                DB.AddWithValue(ref insertSql, "@tankTypeId", tankTypeId, DB.SqlDataType.Int);
                                DB.AddWithValue(ref insertSql, "@countryId", countryId, DB.SqlDataType.Int);
                                DB.AddWithValue(ref insertSql, "@name", name, DB.SqlDataType.VarChar);
                                DB.AddWithValue(ref insertSql, "@tier", tier, DB.SqlDataType.Int);
                                DB.AddWithValue(ref insertSql, "@premium", premium, DB.SqlDataType.Int);
                                ok = DB.ExecuteNonQuery(insertSql);  
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
                                ok = DB.ExecuteNonQuery(updateSql);  
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

                        // Update log file after import
                        updateLog("tanks");
                    }

                    MessageBox.Show("Tank import complete");
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
            httpRequest.UserAgent = "Code Sample Web Client";
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
			string json = FetchFromAPI(WotApiType.Turret);
			if (json == "")
			{
				return "No data imported.";
			}
			else
			{
                log.Add("Start checking turrets (" + DateTime.Now.ToString() + ")");

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

                        List<string> logtext = new List<string>();

                        //DataTable itemsInDB = DB.FetchData("select id from turret");   // Fetch id of turrets already existing in db

                        foreach (JProperty turret in turrets)   // turret = turretId + child tokens
                        {
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
                                      + "(@id, @tankId, @name, @tier, @viewRange, @armorFront, @armorSides, @armorRear)";
                            updateSql = "UPDATE modTurret set tankId=@tankId, name=@name, tier=@tier, viewRange=@viewRange, armorFront=@armorFront, armorSides=@armorSides, armorRear=@armorRear where id=@id";

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
                                ok = DB.ExecuteNonQuery(insertSql);
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
                                ok = DB.ExecuteNonQuery(updateSql);
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

                        // Update log file after import
                        updateLog("turrets");
                    }

                    MessageBox.Show("Turret import complete");
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
			string json = FetchFromAPI(WotApiType.Gun);
			if (json == "")
			{
				return "No data imported.";
			}
			else
			{
				int moduleCount;
				JToken rootToken;
				JToken moduleToken;
				string gunSql = "";
				string turretSql = "";
				string tankSql = "";

				JObject allTokens = JObject.Parse(json);
				rootToken = allTokens.First;

				if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
				{
					rootToken = rootToken.Next;
					moduleCount = (int)((JProperty)rootToken).Value;

					rootToken = rootToken.Next;
					JToken guns = rootToken.Children().First();

					foreach (JProperty gun in guns)
					{
						moduleToken = gun.First();

						int id = Int32.Parse(((JProperty)moduleToken.Parent).Name);
						string name = moduleToken["name_i18n"].ToString();
						int tier = Int32.Parse(moduleToken["level"].ToString());
						JArray dmgArray = (JArray)moduleToken["damage"];
						int dmg1 = Int32.Parse(dmgArray[0].ToString());
						int dmg2 = 0;                                                           // guns have 1, 2 or 3 types of ammo
						if (dmgArray.Count > 1) { dmg2 = Int32.Parse(dmgArray[1].ToString()); } // fetch damage and penetration if available
						int dmg3 = 0;
						if (dmgArray.Count > 2) { dmg3 = Int32.Parse(dmgArray[2].ToString()); }
						JArray penArray = (JArray)moduleToken["piercing_power"];
						int pen1 = Int32.Parse(penArray[0].ToString());
						int pen2 = 0;
						if (penArray.Count > 1) { pen2 = Int32.Parse(penArray[1].ToString()); }
						int pen3 = 0;
						if (penArray.Count > 2) { pen3 = Int32.Parse(penArray[2].ToString()); }
						string fireRate = ((moduleToken["rate"].ToString())).Replace(",", ".");

						gunSql = gunSql + "insert into modGun (id, name, tier, dmg1, dmg2, dmg3, pen1, pen2, pen3, fireRate) values "
										+ "('" + id + "', '" + name + "', '" + tier + "', '" + dmg1 + "', '" + dmg2 + "', '" + dmg3
										+ "', '" + pen1 + "', '" + pen2 + "', '" + pen3 + "', '" + fireRate + "'); ";

						// Create relation to turret if possible
						JArray turretArray = (JArray)moduleToken["turrets"];
						if (turretArray.Count > 0)
						{
							for (int i = 0; i < turretArray.Count; i++)
							{
								turretSql = turretSql + "insert into modTurretGun (turretId, gunId) values (";
								turretSql = turretSql + Int32.Parse(turretArray[i].ToString()) + ", " + id;
								turretSql = turretSql + "); ";
							}
						}

						// Create relation to tank
						JArray tankArray = (JArray)moduleToken["tanks"];
						if (tankArray.Count > 0)
						{
							for (int i = 0; i < tankArray.Count; i++)
							{
								tankSql = tankSql + "insert into modTankGun (tankId, gunId) values (";
								tankSql = tankSql + Int32.Parse(tankArray[i].ToString()) + ", " + id;
								tankSql = tankSql + "); ";
							}
						}
					}

					try
					{
						Stopwatch sw = new Stopwatch();
						sw.Start();
						DB.ExecuteNonQuery("delete from modTurretGun; delete from modTankGun; delete from modGun;");
						DB.ExecuteNonQuery(gunSql + turretSql + tankSql);
						sw.Stop();
						TimeSpan ts = sw.Elapsed;
						string s = " > Time spent analyzing file: " + ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds.ToString("000");
					}
					catch (Exception ex)
					{
						Code.MsgBox.Show(ex.Message, "Error occured");
					}
				}

				return ("Import Complete");
			}
		}

		#endregion

		#region importRadios

		public static String ImportRadios()
		{
			string json = FetchFromAPI(WotApiType.Radio);
			if (json == "")
			{
				return "No data imported.";
			}
			else
			{
				int moduleCount;
				JToken rootToken;
				JToken moduleToken;
				string radioSql = "";
				string tankSql = "";

				JObject allTokens = JObject.Parse(json);
				rootToken = allTokens.First;

				if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
				{
					rootToken = rootToken.Next;
					moduleCount = (int)((JProperty)rootToken).Value;

					rootToken = rootToken.Next;
					JToken guns = rootToken.Children().First();

					foreach (JProperty gun in guns)
					{
						moduleToken = gun.First();

						int id = Int32.Parse(((JProperty)moduleToken.Parent).Name);
						string name = moduleToken["name_i18n"].ToString();
						int tier = Int32.Parse(moduleToken["level"].ToString());
						int signalRange = Int32.Parse(moduleToken["distance"].ToString());

						radioSql = radioSql + "insert into modRadio (id, name, tier, signalRange) values "
										+ "('" + id + "', '" + name + "', '" + tier + "', '" + signalRange + "'); ";

						// Create relation to tank
						JArray tankArray = (JArray)moduleToken["tanks"];
						if (tankArray.Count > 0)
						{
							for (int i = 0; i < tankArray.Count; i++)
							{
								tankSql = tankSql + "insert into modTankRadio (tankId, radioId) values (";
								tankSql = tankSql + Int32.Parse(tankArray[i].ToString()) + ", " + id;
								tankSql = tankSql + "); ";
							}
						}
					}

					try
					{
						Stopwatch sw = new Stopwatch();
						sw.Start();
						DB.ExecuteNonQuery("delete from modTankRadio; delete from modRadio;");
						DB.ExecuteNonQuery(radioSql + tankSql);
						sw.Stop();
						TimeSpan ts = sw.Elapsed;
						string s = " > Time spent analyzing file: " + ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds.ToString("000");
					}
					catch (Exception ex)
					{
						Code.MsgBox.Show(ex.Message, "Error occured");
					}

				}

				return ("Import Complete");
			}
		}


		#endregion

		#region importAchievements

		public static void ImportAchievements()
		{
			string json = FetchFromAPI(WotApiType.Achievement);
			if (json == "")
			{
				// no action, no data found
			}
			else
			{
				JObject allTokens = JObject.Parse(json);
				JToken rootToken = allTokens.First;
				if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
				{
					rootToken = rootToken.Next;
					int achCount = (int)((JProperty)rootToken).Value;
					rootToken = rootToken.Next;
					JToken achList = rootToken.Children().First();
					foreach (JProperty ach in achList)
					{
						JToken medalToken = ach.First();
						// Check if ach already exists
						if (!TankData.GetAchievmentExist(medalToken["name"].ToString()))
						{
							string sql = "insert into ach (name, section, options, section_order, image, name_i18n, type, ordernum, description, " +
										"  image1, image2, image3, image4, name_i18n1, name_i18n2, name_i18n3, name_i18n4) " +
										"values (@name, @section, @options, @section_order, @image, @name_i18n, @type, @ordernum, @description, " +
										"  @image1, @image2, @image3, @image4, @name_i18n1, @name_i18n2, @name_i18n3, @name_i18n4) ";
							// Get data from json token and insert to query
							// string tokenName = ((JProperty)moduleToken.Parent).Name.ToString()); // Not in use
							DB.AddWithValue(ref sql, "@name", medalToken["name"].ToString(), DB.SqlDataType.VarChar);
							DB.AddWithValue(ref sql, "@section", medalToken["section"].ToString(), DB.SqlDataType.VarChar);
							DB.AddWithValue(ref sql, "@section_order", medalToken["section_order"].ToString(), DB.SqlDataType.Int);
							DB.AddWithValue(ref sql, "@type", medalToken["type"].ToString(), DB.SqlDataType.VarChar);
							DB.AddWithValue(ref sql, "@ordernum", medalToken["order"].ToString(), DB.SqlDataType.Int);
							DB.AddWithValue(ref sql, "@description", medalToken["description"].ToString(), DB.SqlDataType.VarChar);
							// Check if several medal alternatives, and get images and names, set NULL as default value
							string options = medalToken["options"].ToString();
							if (options == "") // no options, get default medal image and name
							{
								DB.AddWithValue(ref sql, "@image", medalToken["image"].ToString(), DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@name_i18n", medalToken["name_i18n"].ToString(), DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@options", DBNull.Value, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@image1", DBNull.Value, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@image2", DBNull.Value, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@image3", DBNull.Value, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@image4", DBNull.Value, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@name_i18n1", DBNull.Value, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@name_i18n2", DBNull.Value, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@name_i18n3", DBNull.Value, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@name_i18n4", DBNull.Value, DB.SqlDataType.VarChar);
							}
							else // get medal optional images and names
							{
								DB.AddWithValue(ref sql, "@image", DBNull.Value, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@name_i18n", DBNull.Value, DB.SqlDataType.VarChar);
								DB.AddWithValue(ref sql, "@options", options, DB.SqlDataType.VarChar);
								// Get the medal options from array
								JArray medalArray = (JArray)medalToken["options"];
								int num = medalArray.Count;
								if (num > 4) num = 4;
								for (int i = 1; i <= num; i++)
								{
									DB.AddWithValue(ref sql, "@image" + i.ToString(), medalArray[i - 1]["image"].ToString(), DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@name_i18n" + i.ToString(), medalArray[i - 1]["name_i18n"].ToString(), DB.SqlDataType.VarChar);
								}
								// If not 4, put null in rest
								for (int i = num + 1; i <= 4; i++)
								{
									DB.AddWithValue(ref sql, "@image" + i.ToString(), DBNull.Value, DB.SqlDataType.VarChar);
									DB.AddWithValue(ref sql, "@name_i18n" + i.ToString(), DBNull.Value, DB.SqlDataType.VarChar);
								}

							}

							// Insert to db now
							if (!DB.ExecuteNonQuery(sql)) return;
						}
					}
				}
			}
		}


		#endregion
	}
}
