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

namespace WotDBUpdater
{
	class ImportWotApi2DB
	{
		/* 
		 * Import functions for data from WoT API tank modules and achivements
		 * Data is retrieved from Wargaming API
		 * Tables are emptied before import begins
		 */

		private enum WotApiType
		{
			Turret = 1,
			Gun = 2,
			Radio = 3,
			Achievement = 4
		}

		#region fetchFromAPI

		private static string FetchFromAPI(WotApiType WotAPi)
		{
			Log.LogToFile("Get data from WoT API:" + WotAPi.ToString());
			string url = "";
			if (WotAPi == WotApiType.Turret)
			{
				url = "https://api.worldoftanks.eu/wot/encyclopedia/tankturrets/?application_id=0a7f2eb79dce0dd45df9b8fedfed7530";
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

		#endregion

		#region importTurrets

		public static String ImportTurrets()
		{
			string json = FetchFromAPI(WotApiType.Turret);
			int moduleCount;
			JToken rootToken;
			JToken moduleToken;
			string sql = "";

			JObject allTokens = JObject.Parse(json);
			rootToken = allTokens.First;   // returns status token

			if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
			{
				rootToken = rootToken.Next;
				moduleCount = (int)((JProperty)rootToken).Value;   // returns count (not in use for now)

				rootToken = rootToken.Next;   // start reading modules
				JToken turrets = rootToken.Children().First();   // read all tokens in data token

				List<string> logtext = new List<string>();

				foreach (JProperty turret in turrets)   // turret = turretId + child tokens
				{
					moduleToken = turret.First();   // First() returns only child tokens of turret
					
					int id = Int32.Parse(((JProperty)moduleToken.Parent).Name);   // step back to parent to fetch the isolated turretId
					JArray tanksArray = (JArray)moduleToken["tanks"];
					int tankId = Int32.Parse(tanksArray[0].ToString());   // fetch only the first tank in the array for now (all turrets are related to one tank)
					string name = moduleToken["name_i18n"].ToString();
					int tier = Int32.Parse(moduleToken["level"].ToString());
					int viewRange = Int32.Parse(moduleToken["circular_vision_radius"].ToString());
					int armorFront = Int32.Parse(moduleToken["armor_forehead"].ToString());
					int armorSides = Int32.Parse(moduleToken["armor_board"].ToString());
					int armorRear = Int32.Parse(moduleToken["armor_fedd"].ToString());

					sql = sql + "insert into modTurret (id, tankId, name, tier, viewRange, armorFront, armorSides, armorRear) values "
							  + "(" + id + ", " + tankId + ", '" + name + "', " + tier + ", " + viewRange + ", " + armorFront 
							  + ", " + armorSides + ", " + armorRear +");";
					
					
				}
				logtext.Add(sql);
				Log.CheckLogFileSize();
				Log.LogToFile(logtext);
				// Execute delete and insert statements
				try
				{
					SqlConnection con = new SqlConnection(Config.DatabaseConnection());
					con.Open();
					SqlCommand delete = new SqlCommand("delete from modTurret", con);
					SqlCommand insert = new SqlCommand(sql, con);
					delete.ExecuteNonQuery();
					insert.ExecuteNonQuery();
					con.Close();
				}
				catch (Exception ex)
				{
					Code.Support.MessageDark.Show(ex.Message.ToString(), "Error occured");
				}
			}

			return ("Import Complete");
		}

		#endregion

		#region importGuns

		public static String ImportGuns()
		{
			string json = FetchFromAPI(WotApiType.Gun);
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
									+ "', '" + pen1 + "', '" + pen2 + "', '" + pen3 + "', '" + fireRate +"'); ";
					
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

					SqlConnection con = new SqlConnection(Config.DatabaseConnection());
					con.Open();
					SqlCommand delete = new SqlCommand("delete from modTurretGun; delete from modTankGun; delete from modGun", con);
					string inserts = gunSql + turretSql + tankSql;
					SqlCommand insert = new SqlCommand(inserts, con);
					delete.ExecuteNonQuery();
					insert.ExecuteNonQuery();
					con.Close();

					sw.Stop();
					TimeSpan ts = sw.Elapsed;
					string s = " > Time spent analyzing file: " + ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds.ToString("000");
				}
				catch (Exception ex)
				{
					Code.Support.MessageDark.Show(ex.Message, "Error occured");
				}
			}

			return ("Import Complete");
		}

		#endregion

		#region importRadios

		public static String ImportRadios()
		{
			string json = FetchFromAPI(WotApiType.Radio);
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

					SqlConnection con = new SqlConnection(Config.DatabaseConnection());
					con.Open();
					SqlCommand delete = new SqlCommand("delete from modTankRadio; delete from modRadio;", con);
					string inserts = radioSql + tankSql;
					SqlCommand insert = new SqlCommand(inserts, con);
					delete.ExecuteNonQuery();
					insert.ExecuteNonQuery();
					con.Close();

					sw.Stop();
					TimeSpan ts = sw.Elapsed;
					string s = " > Time spent analyzing file: " + ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds.ToString("000");
				}
				catch (Exception ex)
				{
					Code.Support.MessageDark.Show(ex.Message, "Error occured");
				}

			}

			return ("Import Complete");
		}


		#endregion

		#region importAchievements

		public static void ImportAchievements()
		{
			string json = FetchFromAPI(WotApiType.Achievement);
			JObject allTokens = JObject.Parse(json);
			JToken rootToken = allTokens.First;
			if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
			{
				rootToken = rootToken.Next;
				int achCount = (int)((JProperty)rootToken).Value;
				rootToken = rootToken.Next;
				JToken achList = rootToken.Children().First();
				SqlConnection con = new SqlConnection(Config.DatabaseConnection());
				con.Open();
				foreach (JProperty ach in achList)
				{
					JToken medalToken = ach.First();
					// Check if ach already exists
					if (!TankData.GetAchievmentExist(medalToken["name"].ToString()))
					{
						string sql = "insert into ach (name, section, options, section_order, image, name_i18n, type ,ordernum, description, " +
									"  image1 ,image2 ,image3 ,image4 ,name_i18n1 ,name_i18n2 ,name_i18n3 ,name_i18n4) " +
									"values (@name, @section, @options, @section_order, @image, @name_i18n, @type , @ordernum, @description, " +
									"  @image1, @image2, @image3, @image4, @name_i18n1, @name_i18n2, @name_i18n3, @name_i18n4) ";
						// Get data from json token and insert to query
						SqlCommand cmd = new SqlCommand(sql, con);
						// string tokenName = ((JProperty)moduleToken.Parent).Name.ToString()); // Not in use
						cmd.Parameters.AddWithValue("@name", medalToken["name"].ToString());
						cmd.Parameters.AddWithValue("@section", medalToken["section"].ToString());
						cmd.Parameters.AddWithValue("@section_order", medalToken["section_order"].ToString());
						cmd.Parameters.AddWithValue("@type", medalToken["type"].ToString());
						cmd.Parameters.AddWithValue("@ordernum", medalToken["order"].ToString());
						cmd.Parameters.AddWithValue("@description", medalToken["description"].ToString());
						// Check if several medal alternatives, and get images and names, set NULL as default value
						string options = medalToken["options"].ToString();
						if (options == "") // no options, get default medal image and name
						{
							cmd.Parameters.AddWithValue("@image", medalToken["image"].ToString());
							cmd.Parameters.AddWithValue("@name_i18n", medalToken["name_i18n"].ToString());
							cmd.Parameters.AddWithValue("@options", DBNull.Value);
							cmd.Parameters.AddWithValue("@image1", DBNull.Value);
							cmd.Parameters.AddWithValue("@image2", DBNull.Value);
							cmd.Parameters.AddWithValue("@image3", DBNull.Value);
							cmd.Parameters.AddWithValue("@image4", DBNull.Value);
							cmd.Parameters.AddWithValue("@name_i18n1", DBNull.Value);
							cmd.Parameters.AddWithValue("@name_i18n2", DBNull.Value);
							cmd.Parameters.AddWithValue("@name_i18n3", DBNull.Value);
							cmd.Parameters.AddWithValue("@name_i18n4", DBNull.Value);
						}
						else // get medal optional images and names
						{
							cmd.Parameters.AddWithValue("@image", DBNull.Value);
							cmd.Parameters.AddWithValue("@name_i18n", DBNull.Value);
							cmd.Parameters.AddWithValue("@options", options);
							// Get the medal options from array
							JArray medalArray = (JArray)medalToken["options"];
							int num = medalArray.Count;
							if (num > 4) num = 4;
							for (int i = 1; i <= num; i++)
							{
								cmd.Parameters.AddWithValue("@image" + i.ToString(), medalArray[i - 1]["image"].ToString());
								cmd.Parameters.AddWithValue("@name_i18n" + i.ToString(), medalArray[i - 1]["name_i18n"].ToString());
							}
							// If not 4, put null in rest
							for (int i = num + 1; i <= 4; i++)
							{
								cmd.Parameters.AddWithValue("@image" + i.ToString(), DBNull.Value);
								cmd.Parameters.AddWithValue("@name_i18n" + i.ToString(), DBNull.Value);
							}

						}

						// Insert to db now
						cmd.ExecuteNonQuery();
					}
				}
				con.Close();
			}
		}


		#endregion
	}
}
