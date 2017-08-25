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
            TankList = 9,
		}

		#region fetchFromAPI

		private static string WotServerApiUrl()
		{
            // Defaut use EU
            string serverURL = "http://api.worldoftanks.eu";
            // Get player server
            string server = Config.Settings.playerServer;
			// Change to correct server for NA and RU
			switch (server)
			{
				case "LOGIN": serverURL = "http://api.worldoftanks.com"; break;
				case "NET": serverURL = "http://api.worldoftanks.ru"; break;
			}
			return serverURL;
		}

		private static string WotApplicationId()
		{
            // Defaut use EU
            string applicationId = "2a70055c41b7a6fff1e35a3ba9cadbf1";
            // Get player server
            string server = Config.Settings.playerServer;
            // Change to correct server for NA and RU
            switch (server)
			{
				case "LOGIN": applicationId = "417860beae5ef8a03e11520aaacbf123"; break;
				case "NET": applicationId = "f53b88fef36646161ddfa4418fc5209c"; break;
			}
			return applicationId;
		}

		private static string FetchFromAPI(WotApiType WotAPi, int tankId, Form parentForm)
		{
			try
			{
				Log.CheckLogFileSize();
				Log.AddToLogBuffer("// Get data from WoT API: " + WotAPi.ToString());
				string url = WotServerApiUrl();
				string applicationId = WotApplicationId();
                switch (WotAPi)
                {
                    case WotApiType.Tank:
                        // NEW - get these fields: tank_id,name,short_name,is_premium_igr,type,nation,tier,description,price_credit,images
                        url += "/wot/encyclopedia/vehicles/?application_id=" + applicationId + "&fields=tank_id%2Cname%2Cshort_name%2Cis_premium%2Ctype%2Cnation%2Ctier%2Cdescription%2Cprice_credit%2Cimages%2Cdefault_profile.hp";
                        if (tankId != 0)
                            url += "&tank_id=" + tankId;
                        break;
                    case WotApiType.TankList:
                        // OLD
                        url += "/wot/encyclopedia/tanks/?application_id=" + applicationId; // old
                        break;
                    case WotApiType.Turret:
                        url += "/wot/encyclopedia/tankturrets/?application_id=" + applicationId;
                        break;
                    case WotApiType.Gun:
                        url += "/wot/encyclopedia/tankguns/?application_id=" + applicationId;
                        break;
                    case WotApiType.Radio:
                        url += "/wot/encyclopedia/tankradios/?application_id=" + applicationId;
                        break;
                    case WotApiType.Achievement:
                        url += "/wot/encyclopedia/achievements/?application_id=" + applicationId;
                        break;
                    case WotApiType.TankDetails:
                        url += "/wot/encyclopedia/tankinfo/?application_id=" + applicationId + "&tank_id=" + tankId;
                        break;
                    case WotApiType.PlayersInGarageVehicles:
                        url += "/wot/tanks/stats/?application_id=" + applicationId + "&access_token=" + Forms.InGarageApiResult.access_token + "&account_id=" + Forms.InGarageApiResult.account_id + "&in_garage=1";
                        break;
                    case WotApiType.Maps:
                        url += "/wot/encyclopedia/arenas/?application_id=" + applicationId;
                        break;
                    default:
                        break;
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

		private static void WriteApiLog(string apiType, LogItems logItems)
		{
			// Update log after import
			Log.AddToLogBuffer("// " + apiType + " import complete: (" + DateTime.Now.ToString() + ")");
			if (logItems.Inserted != null)
			{
				logItems.Inserted = logItems.Inserted.Substring(0, logItems.Inserted.Length - 2);
				Log.AddToLogBuffer(" > Added " + logItems.InsertedCount + " new items");
				Log.AddToLogBuffer(" > > " + logItems.Inserted);
			}
			else
			{
				Log.AddToLogBuffer(" > None added, no new items found");
			}
			if (logItems.Updated != null)
			{
				logItems.Updated = logItems.Updated.Substring(0, logItems.Updated.Length - 2);
				Log.AddToLogBuffer(" > Updated " + logItems.UpdatedCount + " existing items");
				Log.AddToLogBuffer(" > > " + logItems.Updated);
			}
            else
            {
                Log.AddToLogBuffer(" > None updated, no existing items found");
            }
            Log.WriteLogBuffer();
        }

        #endregion

        #region importTanks

        // New method: "Vehicles" gets enhanced tank data, problem not as updated as old "List of vehicles"
        public static String ImportTanks(Form parentForm, bool overwriteCustomTankDetails = false)
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

				Log.AddToLogBuffer("// Start checking tanks (" + DateTime.Now.ToString() + ")");

				try
				{
					JObject allTokens = JObject.Parse(json);
					JToken rootToken = allTokens.First;   // returns status token

					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						int itemCount = (int)((JProperty)rootToken.First.First).Value;   // returns count (not in use for now)

						rootToken = rootToken.Next;   // start reading tanks
						JToken tanks = rootToken.Children().First();   // read all tokens in data token

						LogItems logItems = new LogItems(); // Gather info of result, logged after runned
						string sqlTotal = "";
                        foreach (JProperty tank in tanks)   // tank = tankId + child tokens
                        {
                            Application.DoEvents(); // TODO: testing freeze-problem running API requests
                            JToken itemToken = tank.First();   // First() returns only child tokens of tank

                            // ID
                            int itemId = Int32.Parse(((JProperty)itemToken.Parent).Name);   // step back to parent to fetch the isolated tankId

                            // Check for custom tank info, skip update if exists unless spesified
                            if (!TankHelper.HasCustomTankInfo(itemId) || overwriteCustomTankDetails)
                            {

                                // Tank Type
                                string type = itemToken["type"].ToString();
                                switch (type)
                                {
                                    case "lightTank": tankTypeId = 1; break;
                                    case "mediumTank": tankTypeId = 2; break;
                                    case "heavyTank": tankTypeId = 3; break;
                                    case "AT-SPG": tankTypeId = 4; break;
                                    case "SPG": tankTypeId = 5; break;
                                }

                                // Nation
                                string country = itemToken["nation"].ToString();
                                countryId = -1;
                                switch (country)
                                {
                                    case "ussr": countryId = 0; break;
                                    case "germany": countryId = 1; break;
                                    case "usa": countryId = 2; break;
                                    case "china": countryId = 3; break;
                                    case "france": countryId = 4; break;
                                    case "uk": countryId = 5; break;
                                    case "japan": countryId = 6; break;
                                    case "czech": countryId = 7; break;
                                    case "sweden": countryId = 8; break;
                                    case "poland": countryId = 9; break;
                                }

                                // Tank name
                                string name = itemToken["name"].ToString();
                                string short_name = itemToken["short_name"].ToString();

                                // Image as sub level
                                JToken imageToken = itemToken["images"];
                                string imgPath = imageToken["small_icon"].ToString();

                                // Tier
                                int tier = Int32.Parse(itemToken["tier"].ToString());

                                // premium
                                bool isPremium = Convert.ToBoolean(itemToken["is_premium"]);
                                premium = 0;
                                if (isPremium) premium = 1;

                                // Price credits
                                string price_credit_str = itemToken["price_credit"].ToString();
                                double? price_credit = null;
                                double get_price_credit = 0;
                                if (Double.TryParse(price_credit_str, out get_price_credit))
                                    price_credit = get_price_credit;

                                // hp
                                int? hp = null;
                                int hp_int = 0;
                                JToken defaultProfile = itemToken["default_profile"];
                                string hp_str = defaultProfile["hp"].ToString();
                                if (Int32.TryParse(hp_str, out hp_int))
                                    hp = hp_int;

                                // Description
                                string description = itemToken["description"].ToString();

                                // Write to db
                                tankExists = TankHelper.TankExists(itemId);
                                string sql = "";

                                // insert if tank does not exist
                                if (!tankExists)
                                {
                                    sql =
                                        "INSERT INTO tank (id, tankTypeId, countryId, name, short_name, description, tier, premium, imgPath, price_credit, customTankInfo, hp) " +
                                        "VALUES (@id, @tankTypeId, @countryId, @name, @short_name, @description, @tier, @premium, @imgPath, @price_credit, 0, @hp); ";
                                    logItems.Inserted += name + ", ";
                                    logItems.InsertedCount++;
                                }
                                else
                                {
                                    sql =
                                        "UPDATE tank set tankTypeId=@tankTypeId, countryId=@countryId, name=@name, short_name=@short_name, description=@description, tier=@tier, " +
                                        "premium=@premium, imgPath=@imgPath, price_credit=@price_credit, customTankInfo=0, hp=@hp WHERE id=@id; ";
                                    logItems.Updated += name + ", ";
                                    logItems.UpdatedCount++;
                                }
                                // Add params    
                                DB.AddWithValue(ref sql, "@id", itemId, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sql, "@tankTypeId", tankTypeId, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sql, "@countryId", countryId, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sql, "@name", name, DB.SqlDataType.VarChar);
                                DB.AddWithValue(ref sql, "@short_name", short_name, DB.SqlDataType.VarChar);
                                DB.AddWithValue(ref sql, "@description", description, DB.SqlDataType.VarChar);
                                DB.AddWithValue(ref sql, "@tier", tier, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sql, "@premium", premium, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sql, "@imgPath", imgPath, DB.SqlDataType.VarChar);
                                DB.AddWithValue(ref sql, "@price_credit", price_credit, DB.SqlDataType.Float);
                                DB.AddWithValue(ref sql, "@hp", hp, DB.SqlDataType.Int);
                                sqlTotal += sql + Environment.NewLine;
                            }
                        }
                        DB.ExecuteNonQuery(sqlTotal, true, true); // Run all SQL in batch
                        // Update log file after import
                        WriteApiLog("Tanks", logItems);
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

        // Old method "List of vehicles", only get if tank is missing
        public static String ImportTankList(Form parentForm, bool overwriteCustomTankDetails = false)
        {
            string json = FetchFromAPI(WotApiType.TankList, 0, parentForm);
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

                Log.AddToLogBuffer("// Start checking tank list (" + DateTime.Now.ToString() + ")");

                try
                {
                    JObject allTokens = JObject.Parse(json);
                    JToken rootToken = allTokens.First;   // returns status token

                    if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
                    {
                        rootToken = rootToken.Next;
                        int itemCount = (int)((JProperty)rootToken.First.First).Value;   // returns count (not in use for now)

                        rootToken = rootToken.Next;   // start reading tanks
                        JToken tanks = rootToken.Children().First();   // read all tokens in data token

                        LogItems logItems = new LogItems(); // Gather info of result, logged after runned
                        string sqlTotal = "";
                        foreach (JProperty tank in tanks)   // tank = tankId + child tokens
                        {
                            Application.DoEvents(); // TODO: testing freeze-problem running API requests
                            JToken itemToken = tank.First();   // First() returns only child tokens of tank

                            // ID
                            int itemId = Int32.Parse(((JProperty)itemToken.Parent).Name);   // step back to parent to fetch the isolated tankId

                            // Check for custom tank info, skip update if exists unless specified
                            if (!TankHelper.HasCustomTankInfo(itemId) || overwriteCustomTankDetails)
                            {

                                // Tank Type
                                string type = itemToken["type"].ToString();
                                switch (type)
                                {
                                    case "lightTank": tankTypeId = 1; break;
                                    case "mediumTank": tankTypeId = 2; break;
                                    case "heavyTank": tankTypeId = 3; break;
                                    case "AT-SPG": tankTypeId = 4; break;
                                    case "SPG": tankTypeId = 5; break;
                                }

                                // Nation
                                string country = itemToken["nation"].ToString();
                                countryId = -1;
                                switch (country)
                                {
                                    case "ussr": countryId = 0; break;
                                    case "germany": countryId = 1; break;
                                    case "usa": countryId = 2; break;
                                    case "china": countryId = 3; break;
                                    case "france": countryId = 4; break;
                                    case "uk": countryId = 5; break;
                                    case "japan": countryId = 6; break;
                                    case "czech": countryId = 7; break;
                                    case "sweden": countryId = 8; break;
                                    case "poland": countryId = 9; break;
                                }

                                // Tank name
                                string name = itemToken["name_i18n"].ToString();
                                string short_name = itemToken["short_name_i18n"].ToString();
                                short_name = short_name.Replace(" short", ""); // fix for removing subfix added to short name

                                // Image 
                                string imgPath = itemToken["image"].ToString();

                                // Tier
                                int tier = Int32.Parse(itemToken["level"].ToString());

                                // premium
                                bool isPremium = Convert.ToBoolean(itemToken["is_premium"]);
                                premium = 0;
                                if (isPremium) premium = 1;

                                // Price credits (not available from API)
                                double? price_credit = null;

                                // Description (not available from API)
                                string description = "";

                                // Write to db
                                tankExists = TankHelper.TankExists(itemId);
                                string sql = "";

                                // insert if tank does not exist
                                if (!tankExists)
                                {
                                    sql =
                                        "INSERT INTO tank (id, tankTypeId, countryId, name, short_name, description, tier, premium, imgPath, price_credit, customTankInfo) " +
                                        "VALUES (@id, @tankTypeId, @countryId, @name, @short_name, @description, @tier, @premium, @imgPath, @price_credit, 0); ";
                                    logItems.Inserted += name + ", ";
                                    logItems.InsertedCount++;
                                }
                                else
                                {
                                    sql =
                                        "UPDATE tank set tankTypeId=@tankTypeId, countryId=@countryId, name=@name, short_name=@short_name, description=@description, tier=@tier, " +
                                        "premium=@premium, imgPath=@imgPath, price_credit=@price_credit, customTankInfo=0 WHERE id=@id; ";
                                    logItems.Updated += name + ", ";
                                    logItems.UpdatedCount++;
                                }
                                // Add params    
                                DB.AddWithValue(ref sql, "@id", itemId, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sql, "@tankTypeId", tankTypeId, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sql, "@countryId", countryId, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sql, "@name", name, DB.SqlDataType.VarChar);
                                DB.AddWithValue(ref sql, "@short_name", short_name, DB.SqlDataType.VarChar);
                                DB.AddWithValue(ref sql, "@description", description, DB.SqlDataType.VarChar);
                                DB.AddWithValue(ref sql, "@tier", tier, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sql, "@premium", premium, DB.SqlDataType.Int);
                                DB.AddWithValue(ref sql, "@imgPath", imgPath, DB.SqlDataType.VarChar);
                                DB.AddWithValue(ref sql, "@price_credit", price_credit, DB.SqlDataType.Float);
                                sqlTotal += sql + Environment.NewLine;
                            }
                        }
                        DB.ExecuteNonQuery(sqlTotal, true, true); // Run all SQL in batch
                        // Update log file after import
                        WriteApiLog("Tank list", logItems);
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

        // Tank Details, returned as TankHelper.BasicTankInfo
        public static TankHelper.BasicTankInfo ImportTanksDetails(Form parentForm, int tankId, out string message)
        {
            message = "";
            TankHelper.BasicTankInfo foundTankInfo = new TankHelper.BasicTankInfo();
            string json = FetchFromAPI(WotApiType.Tank, tankId, parentForm);
            if (json == "")
            {
                message =  "No data imported, no tank details found at Wargaming API for download for tank ID: " + tankId.ToString();
                return null;
            }
            else
            {
                try
                {
                    JObject allTokens = JObject.Parse(json);
                    JToken rootToken = allTokens.First;   // returns status token

                    if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
                    {
                        rootToken = rootToken.Next;
                        int itemCount = (int)((JProperty)rootToken.First.First).Value;   // returns count (not in use for now)

                        rootToken = rootToken.Next;   // start reading tanks
                        JToken tanks = rootToken.Children().First();   // read all tokens in data token

                        foreach (JProperty tank in tanks)   // tank = tankId + child tokens
                        {
                            Application.DoEvents(); // TODO: testing freeze-problem running API requests
                            JToken itemToken = tank.First();   // First() returns only child tokens of tank

                            if (itemToken.HasValues == false)
                            {
                                message = "No data found for this tank using Wargaming API";
                                return null;
                            }
                            else
                            {
                                // Tank Type
                                string type = itemToken["type"].ToString();
                                switch (type)
                                {
                                    case "lightTank": foundTankInfo.tankType = "Light tank"; break;
                                    case "mediumTank": foundTankInfo.tankType = "Medium tank"; break;
                                    case "heavyTank": foundTankInfo.tankType = "Heavy tank"; break;
                                    case "AT-SPG": foundTankInfo.tankType = "Tank destroyer"; break;
                                    case "SPG": foundTankInfo.tankType = "Self propelled gun"; break;
                                }

                                // Nation
                                string country = itemToken["nation"].ToString();
                                switch (country)
                                {
                                    case "ussr": foundTankInfo.nation = "USSR"; break;
                                    case "germany": foundTankInfo.nation = "Germany"; break;
                                    case "usa": foundTankInfo.nation = "USA"; break;
                                    case "china": foundTankInfo.nation = "China"; break;
                                    case "france": foundTankInfo.nation = "France"; break;
                                    case "uk": foundTankInfo.nation = "UK"; break;
                                    case "japan": foundTankInfo.nation = "Japan"; break;
                                    case "czech": foundTankInfo.nation = "Czechoslovakia"; break;
                                    case "sweden": foundTankInfo.nation = "Sweden"; break;
                                    case "poland": foundTankInfo.nation = "Poland"; break;
                                }

                                // Tank name
                                foundTankInfo.name = itemToken["name"].ToString();
                                foundTankInfo.short_name = itemToken["short_name"].ToString();

                                // Tier
                                foundTankInfo.tier = itemToken["tier"].ToString();

                                // Set as default stats
                                foundTankInfo.customTankInfo = false;
                            }
                        }
                    }
                    return foundTankInfo;
                }

                catch (Exception ex)
                {
                    Log.LogToFile(ex);
                    message = "Error in running Wargaming API for tank detals." + Environment.NewLine + Environment.NewLine + ex.Message;
                    return null; 
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
				Log.AddToLogBuffer("// Start checking turrets (" + DateTime.Now.ToString() + ")");
				string sqlTotal = "";
				try
				{
					JObject allTokens = JObject.Parse(json);
					JToken rootToken = allTokens.First;   // returns status token

					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						int itemCount = (int)((JProperty)rootToken.First.First).Value;   // returns count (not in use for now)

						rootToken = rootToken.Next;   // start reading modules
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
						WriteApiLog("Turrets", logItems);
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
				Log.AddToLogBuffer("// Start checking guns (" + DateTime.Now.ToString() + ")");

				try
				{
					JObject allTokens = JObject.Parse(json);
					JToken rootToken = allTokens.First;
					string sqlTotal = "";
					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						int itemCount = (int)((JProperty)rootToken.First.First).Value;

						rootToken = rootToken.Next;
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
						WriteApiLog("Guns", logItems);

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
				Log.AddToLogBuffer("// Start checking radios (" + DateTime.Now.ToString() + ")");

				try
				{
					JObject allTokens = JObject.Parse(json);
					JToken rootToken = allTokens.First;
					string sqlTotal = "";
					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						int itemCount = (int)((JProperty)rootToken.First.First).Value;

						rootToken = rootToken.Next;
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
						WriteApiLog("Radios", logItems);
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
				Log.AddToLogBuffer("// Start checking maps (" + DateTime.Now.ToString() + ")");

				try
				{
					JObject allTokens = JObject.Parse(json);
					JToken rootToken = allTokens.First;
					LogItems logItems = new LogItems(); // Gather info of result, logged after runned
					string sqlTotal = "";
                    List<string> newMaps = new List<string>();
                    if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						int itemCount = (int)((JProperty)rootToken.First.First).Value;
						rootToken = rootToken.Next;
						JToken maps = rootToken.Children().First();
                        // Get all current maps into list for checking if exits
                        DataTable currentMaps = DB.FetchData("SELECT * FROM map");
						foreach (JProperty map in maps)
						{
							Application.DoEvents(); // TODO: testing freeze-problem running API requests
							JToken itemToken = map.First();
							string name = itemToken["name_i18n"].ToString();
							name = name.Replace("'","");
							string description = itemToken["description"].ToString();
							string arena_id = itemToken["arena_id"].ToString();
                            if (name.Trim() == "")
                                name = arena_id;
                            string sql = "";
                            bool newMap = (currentMaps.Select("arena_id = '" + arena_id + "'").Count() == 0);
                            if (newMap)
                                sql = "INSERT INTO map (id, description, name, active, arena_id) VALUES (@id, @description, @name, 1, @arena_id); ";
                            else
                                sql = "UPDATE map SET description=@description, name=@name, active=1 WHERE arena_id=@arena_id; ";
                            DB.AddWithValue(ref sql, "@name", name, DB.SqlDataType.VarChar);
                            DB.AddWithValue(ref sql, "@description", description, DB.SqlDataType.VarChar);
                            DB.AddWithValue(ref sql, "@arena_id", arena_id, DB.SqlDataType.VarChar);
                            if (newMap)
                            {
                                newMaps.Add(sql);
                                logItems.Inserted += name + ", ";
                                logItems.InsertedCount++;
                            }
                            else
                            {
                                sqlTotal += sql + "\n" + Environment.NewLine;
                                logItems.Updated += name + ", ";
                                logItems.UpdatedCount++;
                            }
						}
						// Update log file after import
						WriteApiLog("Maps", logItems);
					}
                    // Update existing maps
                    if (sqlTotal.Length > 0)
                    {
                        sqlTotal = "UPDATE map SET active=0;" + sqlTotal; // Remove active flag before updates
                        DB.ExecuteNonQuery(sqlTotal, true, true);
                    }
                    // Add new maps
                    if (newMaps.Count > 0)
                    {
                        sqlTotal = "";
                        DataTable currentMaps = DB.FetchData("SELECT MAX(id) FROM map");
                        int newId = 0;
                        if (currentMaps.Rows.Count > 0)
                            newId = Convert.ToInt32(currentMaps.Rows[0][0]);
                        foreach (string sql  in newMaps)
                        {
                            newId++;
                            string newSQL = sql;
                            DB.AddWithValue(ref newSQL, "@id", newId , DB.SqlDataType.Int);
                            sqlTotal += newSQL + "\n" + Environment.NewLine;
                        }
                        if (sqlTotal.Length > 0)
                            DB.ExecuteNonQuery(sqlTotal, true, true);
                    }
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
				Log.AddToLogBuffer("// Start checking achievements (" + DateTime.Now.ToString() + ")");

				try
				{
					JObject allTokens = JObject.Parse(json);
					JToken rootToken = allTokens.First;
					string sqlTotal = "";
					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						int achCount = (int)((JProperty)rootToken.First.First).Value;

						rootToken = rootToken.Next;
						JToken achList = rootToken.Children().First();
						LogItems logItems = new LogItems(); // Gather info of result, logged after runned
						foreach (JProperty ach in achList)
						{
							Application.DoEvents(); // TODO: testing freeze-problem running API requests
							JToken itemToken = ach.First();
                            // Get description and crop at 255 chars
                            string description = itemToken["description"].ToString();
                            if (description.Length > 255)
                                description = description.Substring(0, 255);
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
								DB.AddWithValue(ref sql, "@description", description, DB.SqlDataType.VarChar);
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
								DB.AddWithValue(ref sql, "@description", description, DB.SqlDataType.VarChar);
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

								// Update log
								sqlTotal += sql + Environment.NewLine;
								logItems.Updated += itemToken["name"].ToString() + ", ";
								logItems.UpdatedCount++;
							}
						}
						DB.ExecuteNonQuery(sqlTotal, true, true);
						// Update log file after import
						WriteApiLog("Achievements", logItems);
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
				Log.AddToLogBuffer("// Start checking players tanks in garage (" + DateTime.Now.ToString() + ")");
				try
				{
					JObject allTokens = JObject.Parse(json);
					JToken rootToken = allTokens.First;   // returns status token

					if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
					{
						rootToken = rootToken.Next;
						int itemCount = (int)((JProperty)rootToken.First.First).Value;   // returns count (not in use for now)
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
						Log.AddToLogBuffer("// Found " + tanksInGarage.Count + " tanks in garage");
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
