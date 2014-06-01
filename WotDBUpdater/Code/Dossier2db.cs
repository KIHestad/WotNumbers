using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;
using System.Data.SqlClient;

namespace WotDBUpdater.Code
{
	class Dossier2db
	{
		public class JsonMainSection
		{
			public string header = "header";
			public string tanks = "tanks";
			public string tanks_v2 = "tanks_v2";
		}

		public class JsonItem
		{
			public string mainSection = "";
			public string tank = "";
			public string subSection = "";
			public string property = "";
			public object value = null;
		}

		public class FragItem
		{
			public int dossierNation = 0;
			public int dossierId = 0;
			public int fragCount = 0;
			public string tankName = "";
			public int tankId = 0;
			public int playerTankId = 0;
		}

		public class AchItem
		{
			public int achId = 0;
			public string achName = "";
			public int count = 0;
		}

		public static String ReadJson(string filename, bool ForceUpdate = false)
		{
			// Read file into string
			StreamReader sr = new StreamReader(filename, Encoding.UTF8);
			string json = sr.ReadToEnd();
			sr.Close();
						
			Stopwatch sw = new Stopwatch();
			sw.Start();
			
			// read json string
			JsonTextReader reader = new JsonTextReader(new StringReader(json));
						
			// logging
			List<string> log = new List<string>();
			Log.CheckLogFileSize();

			// If updating player tank detected new battle for any tank
			bool battleSaved = false;
			
			// Check for first run (if player tank = 0), then dont get battle result but force update
			bool saveBattleResult = true;
			if (TankData.GetPlayerTankCount() == 0)
			{
				saveBattleResult = false;
				ForceUpdate = true;
			}

			// Declare
			DataTable NewPlayerTankTable = TankData.GetPlayerTankFromDB(-1); // Return no data, only empty database with structure
			DataTable NewPlayerTankBattleTable = TankData.GetPlayerTankBattleFromDB(-1,""); // Return no data, only empty database with structure
			DataRow NewPlayerTankRow = NewPlayerTankTable.NewRow();
			DataRow NewPlayerTankBattle15Row = NewPlayerTankBattleTable.NewRow();
			DataRow NewPlayerTankBattle7Row = NewPlayerTankBattleTable.NewRow();
			string tankName = "";
			JsonMainSection mainSection = new JsonMainSection();
			JsonItem currentItem = new JsonItem();
			string fragList = "";
			List<AchItem> achList = new List<AchItem>();
			string[] achDossierSubLevel3 = { "", "Two", "Three" };
			// Loop through json file
			while (reader.Read())
			{
				if (reader.Depth <= 1) // main level ( 0 or 1)
				{
					if (reader.Value != null) // ********************************************  found main level - get section type  ************************************************************
					{
						string currentSectionType = reader.Value.ToString();

						if (currentSectionType == mainSection.header) currentItem.mainSection = mainSection.header;
						if (currentSectionType == mainSection.tanks) currentItem.mainSection = mainSection.tanks;
						if (currentSectionType == mainSection.tanks_v2) currentItem.mainSection = mainSection.tanks_v2;
						log.Add("\nMain section: " + currentItem.mainSection + "(Line: " + reader.LineNumber + ")");
					}
				}

				if (currentItem.mainSection == mainSection.tanks || currentItem.mainSection == mainSection.tanks_v2) // Only get data from tank or tank_v2 sections, skpi header for now....
				{
					if (reader.Depth == 2) // ********************************************  found second level = tank level  ************************************************************
					{
						if (reader.Value != null) // found new tank
						{
							// Tank data exist, save data found and log
							if  (tankName != "") 
							{
								log.Add("  > Check for DB update - Tank: '" + tankName );
								if (SaveTankDataResult(tankName, NewPlayerTankRow, NewPlayerTankBattle15Row, NewPlayerTankBattle7Row, fragList, achList, ForceUpdate, saveBattleResult))
									battleSaved = true; // result if battle was detected and saved
							}
							// Reset all values
							NewPlayerTankTable.Clear();
							NewPlayerTankRow = NewPlayerTankTable.NewRow();
							NewPlayerTankBattle15Row = NewPlayerTankBattleTable.NewRow();
							NewPlayerTankBattle7Row = NewPlayerTankBattleTable.NewRow();
							// Get new tank name
							currentItem.tank = reader.Value.ToString(); // add to current item
							tankName = reader.Value.ToString(); // add to current tank
							// clear frags and achievments
							fragList = "";
							achList.Clear();
						}
					}
					else
					{
						if (reader.Depth == 3) // ********************************************  found third level = subsection  ************************************************************
						{
							if (reader.Value != null)
							{
								currentItem.subSection = reader.Value.ToString();
								currentItem.property = ""; // reset property for reading next
							}
						}
						else // ********************************************  found fourth level = property and value  ************************************************************
						{
							if (currentItem.subSection != "rawdata") // skip these subsections
							{
								if (reader.TokenType == JsonToken.PropertyName)
								{
									// Property
									currentItem.property = reader.Value.ToString();
								}
								else
								{
									if (reader.Value != null)
									{
										// Value
										currentItem.value = reader.Value;

										// Check data by getting jsonPlayerTank Mapping
										string expression = "jsonMainSubProperty='" + currentItem.mainSection + "." + currentItem.subSection + "." + currentItem.property + "'";
										DataRow[] foundRows = TankData.json2dbMapping.Select(expression);

										// IF mapping found add currentItem into NewPlayerTankRow
										if (foundRows.Length != 0)
										{
											if (foundRows[0]["dbPlayerTank"] != DBNull.Value) // Found mapping to PlayerTank
											{
												string dataType = foundRows[0]["dbDataType"].ToString();
												string dbField = foundRows[0]["dbPlayerTank"].ToString();
												if (foundRows[0]["dbPlayerTankMode"] == DBNull.Value)
												{
													// Default playerTank value
													switch (dataType)
													{
														case "String": NewPlayerTankRow[dbField] = currentItem.value.ToString(); break;
														case "DateTime": NewPlayerTankRow[dbField] = ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); break;
														case "Int": NewPlayerTankRow[dbField] = Convert.ToInt32(currentItem.value); break;
													}
												}
												else if (foundRows[0]["dbPlayerTankMode"].ToString() == "15")
												{
													// playerTankBattle mode 15x15
													switch (dataType)
													{
														case "String": NewPlayerTankBattle15Row[dbField] = currentItem.value.ToString(); break;
														case "DateTime": NewPlayerTankBattle15Row[dbField] = ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); break;
														case "Int": NewPlayerTankBattle15Row[dbField] = Convert.ToInt32(currentItem.value); break;
													}
												}
												else if (foundRows[0]["dbPlayerTankMode"].ToString() == "7")
												{
													// playerTankBattle mode 7x7
													switch (dataType)
													{
														case "String": NewPlayerTankBattle7Row[dbField] = currentItem.value.ToString(); break;
														case "DateTime": NewPlayerTankBattle7Row[dbField] = ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); break;
														case "Int": NewPlayerTankBattle7Row[dbField] = Convert.ToInt32(currentItem.value); break;
													}
												}
											}
											else // Found mapping to Achievment
											{
												string dbField = foundRows[0]["dbAch"].ToString();
												AchItem ach = new AchItem();
												ach.achName = dbField;
												ach.count = Convert.ToInt32(currentItem.value);
												achList.Add(ach);
											}
										}
										// fraglist
										if (currentItem.subSection == "fragslist" || currentItem.subSection == "kills")
											fragList += currentItem.value.ToString() + ";";
										// Temp log all data
										log.Add("  " + currentItem.mainSection + "." + currentItem.tank + "." + currentItem.subSection + "." + currentItem.property + ":" + currentItem.value);
										//log.Add("  " + currentItem.mainSection + "." + currentItem.subSection + "." + currentItem.property );
									}
								}
							}
						}
					}
				}
			}
			reader.Close();
			// Also write last tank found
			log.Add("  > Check for DB update - Tank: '" + tankName );
			if (SaveTankDataResult(tankName, NewPlayerTankRow, NewPlayerTankBattle15Row, NewPlayerTankBattle7Row, fragList, achList, ForceUpdate, saveBattleResult)) 
				battleSaved = true; // result if battle was detected and saved
			// Done
			if (battleSaved) Log.BattleResultDoneLog();
			sw.Stop();
			TimeSpan ts = sw.Elapsed;
			Log.LogToFile(log);
			return ("Dossier file succsessfully analyzed - time spent " + ts.Minutes + ":" + ts.Seconds + "." + ts.Milliseconds.ToString("000"));
		}

		static DateTime ConvertFromUnixTimestamp(double timestamp)
		{
			DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return origin.AddSeconds(timestamp);
		}

		private static void UpdateNewPlayerTankRow(ref DataRow NewPlayerTankRow, JsonItem currentItem)
		{
			JsonMainSection mainSection = new JsonMainSection(); 
			if (currentItem.mainSection == mainSection.tanks)
			{
				if (currentItem.subSection == "tankdata" && currentItem.property == "battlesCount") NewPlayerTankRow["battles15"] = Convert.ToInt32(currentItem.value);
				if (currentItem.subSection == "tankdata" && currentItem.property == "wins") NewPlayerTankRow["wins15"] = Convert.ToInt32(currentItem.value);
				if (currentItem.subSection == "tankdata" && currentItem.property == "wins") NewPlayerTankRow["wins15"] = Convert.ToInt32(currentItem.value);
			}
			else if (currentItem.mainSection == mainSection.tanks_v2)
			{
				if (currentItem.subSection == "a15x15" && currentItem.property == "battlesCount") NewPlayerTankRow["battles15"] = Convert.ToInt32(currentItem.value);
				if (currentItem.subSection == "a15x15" && currentItem.property == "wins") NewPlayerTankRow["wins15"] = Convert.ToInt32(currentItem.value);
				if (currentItem.subSection == "a7x7" && currentItem.property == "battlesCount") NewPlayerTankRow["battles7"] = Convert.ToInt32(currentItem.value);
				if (currentItem.subSection == "a7x7" && currentItem.property == "wins") NewPlayerTankRow["wins7"] = Convert.ToInt32(currentItem.value);
			}
		}

		public static bool SaveTankDataResult(string tankName, DataRow NewPlayerTankRow, DataRow NewPlayerTankBattle15Row, DataRow NewPlayerTankBattle7Row,string fragList, List<AchItem> achList, bool ForceUpdate = false, bool saveBattleResult = true )
		{
			// Get Tank ID
			bool battleSave = false;
			int tankId = TankData.GetTankID(tankName);
			if (tankId > 0) // when tankid=0 the tank is not found in tank table
			{
				// Get tank new battle count
				int NewPlayerTankRow_battles15 = 0;
				int NewPlayerTankRow_battles7 = 0;
				if (NewPlayerTankBattle15Row["battles"] != DBNull.Value) NewPlayerTankRow_battles15 = Convert.ToInt32(NewPlayerTankBattle15Row["battles"]);
				if (NewPlayerTankBattle7Row["battles"] != DBNull.Value) NewPlayerTankRow_battles7 = Convert.ToInt32(NewPlayerTankBattle7Row["battles"]);
				// Check if battle count has increased, get existing battle count
				DataTable OldPlayerTankTable = TankData.GetPlayerTankFromDB(tankId); // Return Existing Player Tank Data
				// Check if Player has this tank
				int playerTankId = 0;
				if (OldPlayerTankTable.Rows.Count == 0)
				{
					// New tank detected, this parts only run when new tank is detected
					SaveNewPlayerTank(tankId); // Save new tank
					OldPlayerTankTable = TankData.GetPlayerTankFromDB(tankId); // Get data into DataTable once more now after row is added
					playerTankId = Convert.ToInt32(OldPlayerTankTable.Rows[0]["id"]); // Get id
					SaveNewPlayerTankBattle(playerTankId, NewPlayerTankRow_battles15, NewPlayerTankRow_battles7); // Save battles with battlecount
				}
				// Get the get existing (old) tank data row
				DataRow OldPlayerTankRow = OldPlayerTankTable.Rows[0];
				playerTankId = Convert.ToInt32(OldPlayerTankTable.Rows[0]["id"]);
				// Now get playerTank BattleResult
				DataRow OldPlayerTankBattle15Row = TankData.GetPlayerTankBattleFromDB(playerTankId, "15").Rows[0];
				DataRow OldPlayerTankBattle7Row = TankData.GetPlayerTankBattleFromDB(playerTankId, "7").Rows[0];
				// Calculate number of new battles 
				int battlessNew15 = NewPlayerTankRow_battles15 - Convert.ToInt32(OldPlayerTankBattle15Row["battles"]);
				int battlessNew7 = NewPlayerTankRow_battles7 - Convert.ToInt32(OldPlayerTankBattle7Row["battles"]);
				// Check if new battle on this tank then do db update, if force do it anyway
				if (battlessNew15 != 0 || battlessNew7 != 0 || ForceUpdate)
				{
					// Update playerTank
					string sqlFields = "";
					foreach (DataColumn column in OldPlayerTankTable.Columns)
					{
						// Get columns and values from NewPlayerTankRow direct
						if (column.ColumnName != "Id" && NewPlayerTankRow[column.ColumnName] != DBNull.Value) // avoid the PK and if new data is NULL 
						{
							string colName = column.ColumnName;
							string colType = column.DataType.Name;
							sqlFields += ", " + colName + "=";
							switch (colType)
							{
								case "String": sqlFields += "'" + NewPlayerTankRow[colName] + "'"; break;
								case "DateTime": sqlFields += "'" + Convert.ToDateTime(NewPlayerTankRow[colName]).ToString("yyyy-MM-dd HH:mm:ss") + "'"; break;
								default: sqlFields += NewPlayerTankRow[colName]; break;
							}
						}
					}
					// Update database
					if (sqlFields.Length > 0 )
					{
						sqlFields = sqlFields.Substring(1); // Remove first comma
						string sql = "UPDATE playerTank SET " + sqlFields + " WHERE Id=@Id ";
						DB.AddWithValue(ref sql, "@Id", OldPlayerTankTable.Rows[0]["id"], DB.SqlDataType.Int);
						DB.ExecuteNonQuery(sql);
					}
					// Now update playerTank battle result 15x15 and/or 7x7
					DataTable PlayerTankBattleTable = TankData.GetPlayerTankBattleFromDB(-1, ""); // Return empty data just to get column structure
					// Update playerTankBattle (15x15)
					if (battlessNew15 > 0 || ForceUpdate)
					{
						sqlFields = "";
						// Calculate WN8
						sqlFields += "wn8=" + Rating.CalculatePlayerTankWn8(tankId, NewPlayerTankRow_battles15, NewPlayerTankBattle15Row);
						// Calculate Eff
						sqlFields += ", eff=" + Rating.CalculatePlayerTankEff(tankId, NewPlayerTankRow_battles15, NewPlayerTankBattle15Row);
						foreach (DataColumn column in PlayerTankBattleTable.Columns)
						{
							// Get columns and values from NewPlayerTankRow direct
							if (column.ColumnName != "Id" && NewPlayerTankBattle15Row[column.ColumnName] != DBNull.Value) // avoid the PK and if new data is NULL 
							{
								string colName = column.ColumnName;
								string colType = column.DataType.Name;
								sqlFields += ", " + colName + "=";
								switch (colType)
								{
									case "String": sqlFields += "'" + NewPlayerTankBattle15Row[colName] + "'"; break;
									case "DateTime": sqlFields += "'" + Convert.ToDateTime(NewPlayerTankBattle15Row[colName]).ToString("yyyy-MM-dd HH:mm:ss") + "'"; break;
									default: sqlFields += NewPlayerTankBattle15Row[colName]; break;
								}
							}
						}
						// Calculate battleOfTotal = factor of how many of battles in this battlemode out of total battles
						string sql = "select SUM(battles) from playerTankBattle where playerTankId=@playerTankId;";
						DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
						int totalBattles = Convert.ToInt32(DB.FetchData(sql).Rows[0][0]) + battlessNew15;
						double battleOfTotal = 0;
						if (totalBattles > 0) battleOfTotal = NewPlayerTankRow_battles15 / Convert.ToDouble(totalBattles);
						sqlFields += ", battleOfTotal=" + battleOfTotal.ToString().Replace(",", ".");
						// Update database
						if (sqlFields.Length > 0)
						{
							sql = "UPDATE playerTankBattle SET " + sqlFields + " WHERE playerTankId=@playerTankId AND battleMode='15'; ";
							// Also update battleOfTotal for battlemode '7'
							if (totalBattles > 0)
							{
								battleOfTotal = (Convert.ToDouble(totalBattles) - NewPlayerTankRow_battles15) / Convert.ToDouble(totalBattles);
								sql += "UPDATE playerTankBattle SET battleOfTotal=" + battleOfTotal.ToString().Replace(",", ".") + " WHERE playerTankId=@playerTankId AND battleMode='7'; ";
							}
							DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
							DB.ExecuteNonQuery(sql);
						}
					}
					// Update playerTankBattle (7x7)
					if (battlessNew7 > 0 || ForceUpdate)
					{
						sqlFields = "";
						// Calculate WN8
						sqlFields += "wn8=" + Rating.CalculatePlayerTankWn8(tankId, NewPlayerTankRow_battles7, NewPlayerTankBattle7Row);
						// Calculate Eff
						sqlFields += ", eff=" + Rating.CalculatePlayerTankEff(tankId, NewPlayerTankRow_battles7, NewPlayerTankBattle7Row);
						foreach (DataColumn column in PlayerTankBattleTable.Columns)
						{
							// Get columns and values from NewPlayerTankRow direct
							if (column.ColumnName != "Id" && NewPlayerTankBattle7Row[column.ColumnName] != DBNull.Value) // avoid the PK and if new data is NULL 
							{
								string colName = column.ColumnName;
								string colType = column.DataType.Name;
								sqlFields += ", " + colName + "=";
								switch (colType)
								{
									case "String": sqlFields += "'" + NewPlayerTankBattle7Row[colName] + "'"; break;
									case "DateTime": sqlFields += "'" + Convert.ToDateTime(NewPlayerTankBattle7Row[colName]).ToString("yyyy-MM-dd HH:mm:ss") + "'"; break;
									default: sqlFields += NewPlayerTankBattle7Row[colName]; break;
								}
							}
						}
						// Calculate battleOfTotal = factor of how many of battles in this battlemode out of total battles
						string sql = "select SUM(battles) from playerTankBattle where playerTankId=@playerTankId;";
						DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
						int totalBattles = Convert.ToInt32(DB.FetchData(sql).Rows[0][0]) + battlessNew7;
						double battleOfTotal = 0;
						if (totalBattles > 0) battleOfTotal = NewPlayerTankRow_battles7 / Convert.ToDouble(totalBattles);
						sqlFields += ", battleOfTotal=" + battleOfTotal.ToString().Replace(",",".");
						// Update database
						if (sqlFields.Length > 0)
						{
							sql = "UPDATE playerTankBattle SET " + sqlFields + " WHERE playerTankId=@playerTankId AND battleMode='7'; ";
							// Also update battleOfTotal for battlemode '15'
							if (totalBattles > 0)
							{
								battleOfTotal = (Convert.ToDouble(totalBattles) - NewPlayerTankRow_battles7) / Convert.ToDouble(totalBattles);
								sql += "UPDATE playerTankBattle SET battleOfTotal=" + battleOfTotal.ToString().Replace(",", ".") + " WHERE playerTankId=@playerTankId AND battleMode='15'; ";
							}
							DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
							DB.ExecuteNonQuery(sql);
							
						}
					}
					// Check fraglist to update playertank frags
					List<FragItem> battleFragList = UpdatePlayerTankFrag(tankId, playerTankId, fragList);
					// Check if achivment exists
					List<AchItem> battleAchList = UpdatePlayerTankAch(tankId, playerTankId, achList);
					// If new battle on this tank also update battle table to store result of last battle(s)
					if (saveBattleResult)
					{
						if (battlessNew15 != 0 && battlessNew7 != 0)
						{
							// If detected both 15x15 and 7x7 recordings, dont save fraglist and achivements to battle, as we don't know how to seperate them
							battleFragList.Clear();
							battleAchList.Clear();
						}
						if (battlessNew15 != 0)
						{
							// New battle 15x15 is detected, update tankData in DB
							UpdateBattle(NewPlayerTankRow, OldPlayerTankRow, NewPlayerTankBattle15Row, OldPlayerTankBattle15Row, "15", tankId, playerTankId, battlessNew15, battleFragList, battleAchList);
							battleSave = true;
							
						}
						if (battlessNew7 != 0)
						{
							// New battle 7x7 is detected, update tankData in DB
							UpdateBattle(NewPlayerTankRow, OldPlayerTankRow, NewPlayerTankBattle7Row, OldPlayerTankBattle7Row, "7", tankId, playerTankId, battlessNew7, battleFragList, battleAchList);
							battleSave = true;
						}

					}
				}
			}
			return battleSave;
		}

		private static void SaveNewPlayerTank(int TankID)
		{
			// Add to database
			string sql = "INSERT INTO PlayerTank (tankId, playerId) VALUES (@tankId, @playerId); ";
			DB.AddWithValue(ref sql, "@tankId", TankID, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql);
		}

		public static void SaveNewPlayerTankBattle(int playerTankId, int battles15, int battles7)
		{
			// Add to database
			string sql = "INSERT INTO PlayerTankBattle (playerTankId, battleMode, battles) VALUES (@playerTankId, '15', @battles15); " +
						 "INSERT INTO PlayerTankBattle (playerTankId, battleMode, battles) VALUES (@playerTankId, '7', @battles7); ";
			DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battles15", battles15, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@battles7", battles7, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql);
		}

		private static List<AchItem> UpdatePlayerTankAch(int tankId, int playerTankId, List<AchItem> achList)
		{
			List<AchItem> battleAchList = new List<AchItem>();
			if (achList.Count > 0) // new ach detected
			{
				// Loop through all achivements
				foreach (AchItem newAch in achList)
				{
					if (newAch.count > 0) // Find the ones achieved
					{
						// Find the current achievent
						string sql = "SELECT achId, ach.name ,achCount " +
									"FROM playerTankAch INNER JOIN ach ON playerTankAch.achId = ach.Id " +
									"WHERE playerTankId=" + playerTankId + " AND ach.name='" + newAch.achName + "';";
						DataTable currentAch = DB.FetchData(sql);
						if (currentAch.Rows.Count == 0) // new achievment
						{
							// Get AchId 
							// TODO : improve
							sql = "SELECT id " +
									"FROM ach " +
									"WHERE name='" + newAch.achName + "'";
							DataTable lookupAch = DB.FetchData(sql);
							int achId = Convert.ToInt32(lookupAch.Rows[0]["id"]);
							// Insert new acheivement
							sql = "INSERT INTO playerTankAch (achCount, playerTankId, achId) " +
									"VALUES (@achCount, @playerTankId, @achId)";
							DB.AddWithValue(ref sql, "@achCount", newAch.count, DB.SqlDataType.Int);
							DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
							DB.AddWithValue(ref sql, "@achId", achId, DB.SqlDataType.Int);
							DB.ExecuteNonQuery(sql);
							// Add to battle achievment
							AchItem ach = new AchItem();
							ach.achId = achId;
							ach.count = (newAch.count);
							battleAchList.Add(ach);
						}
						else // achievent found, check count
						{
							int achId = Convert.ToInt32(currentAch.Rows[0]["achId"]);
							int oldCount = Convert.ToInt32(currentAch.Rows[0]["achCount"]);
							if (newAch.count > oldCount)
							{
								// Update achievment increased count
								sql = "UPDATE playerTankAch SET achCount=@achCount " +
										"WHERE playerTankId=@playerTankId AND achId=@achId;";
								DB.AddWithValue(ref sql, "@achCount", newAch.count, DB.SqlDataType.Int);
								DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
								DB.AddWithValue(ref sql, "@achId", achId, DB.SqlDataType.Int);
								DB.ExecuteNonQuery(sql);
								// Add to battle achievment
								AchItem ach = new AchItem();
								ach.achId = achId;
								ach.count = (newAch.count - oldCount);
								battleAchList.Add(ach);
							}
						}
					}
				}
			}
			return battleAchList;
		}

		private static List<FragItem> UpdatePlayerTankFrag(int tankId, int playerTankId, string fraglist)
		{
			List<FragItem> battleFrag = new List<FragItem>();
			//try
			//{
				// fraglist is semicolon separated string, each 4. element is one frag, split the string and create a list of FragItem
				List<FragItem> newFrag = new List<FragItem>();
				string[] stringSeparators = new string[] { ";" };
				string[] dossierFragItem = fraglist.Split(stringSeparators, StringSplitOptions.None);
				int fragcount = dossierFragItem.Count() / 4;
				for (int i = 0; i < fragcount; i++)
				{
					FragItem FragItem = new FragItem();
					FragItem.dossierNation = Convert.ToInt32(dossierFragItem[i * 4 + 0]);
					FragItem.dossierId = Convert.ToInt32(dossierFragItem[i * 4 + 1]);
					FragItem.fragCount = Convert.ToInt32(dossierFragItem[i * 4 + 2]);
					FragItem.tankName = dossierFragItem[i * 4 + 3].ToString();
					FragItem.tankId = TankData.GetTankID(FragItem.tankName);
					newFrag.Add(FragItem);
				}
				// Check newFrag compared to existing frags for this tank
				List<FragItem> oldFrag = new List<FragItem>();
				string sql =
					"SELECT playerTank.id AS playerTankId, playerTankFrag.* " +
					"FROM playerTank INNER JOIN playerTankFrag ON playerTank.id=playerTankFrag.playerTankId " +
					"WHERE playerTank.tankId=@tankId; ";
				DB.AddWithValue(ref sql, "@tankId", tankId, DB.SqlDataType.Int);
				DataTable dt = DB.FetchData(sql);
				// If no frags exists for this tank get playerTankId separately
				foreach (DataRow reader in dt.Rows)
				{
					FragItem FragItem = new FragItem();
					FragItem.tankId = Convert.ToInt32(reader["fraggedTankId"]);
					FragItem.playerTankId = Convert.ToInt32(reader["playerTankId"]);
					FragItem.fragCount = Convert.ToInt32(reader["fragCount"]);
					oldFrag.Add(FragItem);
				}
				// Now we got old and new frags, calculate update and inserts to playerTankFrag, and battleFrag
				string playerTankFragSQL = "";
				// Loop through new frags
				foreach (FragItem newFragItem in newFrag)
				{
					// Check if frags exists for this fragged tank
					int i = 0;
					bool foundFraggedTank = false;
					while (i < oldFrag.Count && !foundFraggedTank)
					{
						foundFraggedTank = (oldFrag[i].tankId == newFragItem.tankId);
						i++;
					}
					if (foundFraggedTank)
					{
						i--; // return to previous found item
						// fragged tank exsist, check if frag count has increased
						if (newFragItem.fragCount > oldFrag[i].fragCount)
						{
							// new frag on existing fragged tank, update
							playerTankFragSQL += "UPDATE playerTankFrag " +
												"SET fragCount = " + newFragItem.fragCount.ToString() +
												"WHERE playerTankId=" + oldFrag[i].playerTankId +
												"  AND fraggedTankId=" + newFragItem.tankId + "; ";
							// Add new frag count to battle Frag
							newFragItem.fragCount = newFragItem.fragCount - oldFrag[i].fragCount;
							battleFrag.Add(newFragItem);
						}
					}
					else
					{
						// new fragged tank, insert
						// Avoud if tank is unknown
						if (newFragItem.tankId != 0)
						{
							playerTankFragSQL += "INSERT INTO playerTankFrag (playerTankId, fraggedTankId, fragCount) " +
												"VALUES (" + playerTankId + ", " + newFragItem.tankId + ", " + newFragItem.fragCount.ToString() + "); ";
							// Add new frag count to battle Frag
							battleFrag.Add(newFragItem);
						}
					}
				}
				// Add to database
				if (playerTankFragSQL != "")
				{
					DB.ExecuteNonQuery(playerTankFragSQL);
				}
			//}
			//catch (Exception ex)
			//{
			//	string s = ex.Message;
			//	throw;
			//}
			
			return battleFrag;
		}

		private static void UpdateBattle(DataRow NewPlayerTankRow, DataRow OldPlayerTankRow, DataRow NewPlayerTankBattleRow, DataRow OldPlayerTankBattleRow, String BattleMode, int tankId, int playerTankId, int battlesCount, List<FragItem> battleFragList, List<AchItem> battleAchList)
		{
			//try
			//{
				// Create datarow to put calculated battle data
				DataTable NewBattleTable = TankData.GetBattleFromDB(-1); // Return no data, only empty database with structure
				DataRow NewbattleRow = NewBattleTable.NewRow();
				foreach (DataRow dr in TankData.GetTankData2BattleMappingFromDB(BattleMode).Rows)
				{
					// Mapping fields
					string battleField = dr["dbBattle"].ToString();
					string playerTankField = dr["dbPlayerTank"].ToString();
					if (dr["dbPlayerTankMode"] == DBNull.Value) // Default player tank data
					{
						// Check datatype and calculate value
						if (dr["dbDataType"].ToString() == "DateTime") // For DateTime get the new value
						{
							NewbattleRow[battleField] = NewPlayerTankRow[playerTankField];
						}
						else // For integers calculate new value as diff between new and old value
						{
							// Calculate difference from old to new Playertank result
							if (NewbattleRow[battleField] == DBNull.Value) NewbattleRow[battleField] = 0;
							int oldvalue = 0;
							int newvalue = 0;
							if (NewPlayerTankRow[playerTankField] != DBNull.Value) newvalue = Convert.ToInt32(NewPlayerTankRow[playerTankField]);
							if (OldPlayerTankRow[playerTankField] != DBNull.Value) oldvalue = Convert.ToInt32(OldPlayerTankRow[playerTankField]);
							NewbattleRow[battleField] = (Convert.ToInt32(NewbattleRow[battleField]) + newvalue - oldvalue);
						}
					}
					else // Battle Mode data
					{
						// Check datatype and calculate value
						if (dr["dbDataType"].ToString() == "DateTime") // For DateTime get the new value
						{
							NewbattleRow[battleField] = NewPlayerTankBattleRow[playerTankField];
						}
						else // For integers calculate new value as diff between new and old value
						{
							// Calculate difference from old to new Playertank result
							if (NewbattleRow[battleField] == DBNull.Value) NewbattleRow[battleField] = 0;
							int oldvalue = 0;
							int newvalue = 0;
							if (NewPlayerTankBattleRow[playerTankField] != DBNull.Value) newvalue = Convert.ToInt32(NewPlayerTankBattleRow[playerTankField]);
							if (OldPlayerTankBattleRow[playerTankField] != DBNull.Value) oldvalue = Convert.ToInt32(OldPlayerTankBattleRow[playerTankField]);
							NewbattleRow[battleField] = (Convert.ToInt32(NewbattleRow[battleField]) + newvalue - oldvalue);
						}
					}
				}
				// Create SQl to insert new battle row
				string sqlFields = ""; 
				string sqlValues = ""; 
				// Loop through mapping table to get all generate fields, check against column names if average values must be calculted when more than one battle is detected
				string[] avgCols = new string[] { "battleLifeTime", "killed", "frags", "dmg", "dmgReceived", "assistSpot", "assistTrack", 
					"cap", "def", "shots", "hits", "shotsReceived", "pierced", "piercedReceived", "spotted", "mileage", "treesCut", "xp" 
				}; 
				foreach (DataColumn column in NewBattleTable.Columns)
				{
					if (column.ColumnName != "Id" && column.ColumnName != "playerTankID" && NewbattleRow[column.ColumnName] != DBNull.Value) // avoid the PK and if new data is NULL 
					{
						string colName = column.ColumnName;
						string colType = column.DataType.Name;
						sqlFields += ", " + colName;
						switch (colType)
						{
							case "String": sqlValues += ", '" + NewbattleRow[colName] + "'"; break;
							case "DateTime": sqlValues += ", '" + Convert.ToDateTime(NewbattleRow[colName]).ToString("yyyy-MM-dd HH:mm:ss") + "'"; break;
							default:
								{
									int value = Convert.ToInt32(NewbattleRow[colName]);
									if (battlesCount > 1 && avgCols.Contains(colName)) value = value / battlesCount; // Calc average values
									sqlValues += ", " + value.ToString(); 
									break;
								}
						}
					}
				}
				// Calculate WN8
				sqlFields += ", wn8";
				sqlValues += ", " + Rating.CalculateBattleWn8(tankId, battlesCount, NewbattleRow);
				// Calc Eff
				sqlFields += ", eff";
				sqlValues += ", " + Rating.CalculateBattleEff(tankId, battlesCount, NewbattleRow);
				// Add battle mode
				sqlFields += ", battleMode"; sqlValues += ", " + BattleMode; 
				// Calculate battle result
				int victorycount = Convert.ToInt32(NewbattleRow["victory"]);
				int defeatcount = Convert.ToInt32(NewbattleRow["defeat"]);
				int drawcount = battlesCount - victorycount - defeatcount;
				NewbattleRow["draw"] = battlesCount - victorycount - defeatcount;
				int battleResult = 0;
				// (1, 'Victory', 1, '#00FF21')
				// (2, 'Draw', 1, '#FFFF00')
				// (3, 'Defeat', 1 ,'#FF0000')
				// (4, 'Several', '#0094FF')
				if (victorycount > 0 && victorycount == battlesCount)
					battleResult = 1;
				else if (defeatcount > 0 && defeatcount == battlesCount)
					battleResult = 3;
				else if ((victorycount + defeatcount) == 0)
					battleResult = 2;
				else
					battleResult = 4;
				sqlFields += ", battleResultId "; sqlValues += ", " + battleResult.ToString();
				sqlFields += ", draw "; sqlValues += ", " + drawcount.ToString();
				// Calculate battle survive
				int survivecount = Convert.ToInt32(NewbattleRow["survived"]);
				int killedcount = battlesCount - survivecount;
				int battleSurvive = 0;
				// (1, 'Yes', 1, '#00FF21')
				// (2, 'Some', '#0094FF')
				// (3, 'No', 1 ,'#FF0000')
				if (survivecount == 0)
					battleSurvive = 3;
				else if (survivecount == battlesCount)
					battleSurvive = 1;
				else
					battleSurvive = 2;
				sqlFields += ", battleSurviveId "; sqlValues += ", " + battleSurvive.ToString();
				sqlFields += ", killed "; sqlValues += ", " + killedcount.ToString();
				// Update database
				if (sqlFields.Length > 0)
				{
					// Insert Battle
					string sql = "INSERT INTO battle (playerTankId " + sqlFields + ") VALUES (@playerTankId " + sqlValues + "); ";
					DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
					DB.ExecuteNonQuery(sql);
					// Get the last battle id
					int battleId = 0;
					sql = "select max(id) as battleId from battle";
					DataTable dt = DB.FetchData(sql);
					if (dt.Rows.Count > 0)
						battleId = Convert.ToInt32(dt.Rows[0]["battleId"]);
					// Insert Battle Frags
					if (battleFragList.Count > 0)
					{
						// Loop through new frags
						string battleFragSQL = "";
						foreach (var newFragItem in battleFragList)
						{
							battleFragSQL += "INSERT INTO battleFrag (battleId, fraggedTankId, fragCount) " +
													"VALUES (" + battleId + ", " + newFragItem.tankId + ", " + newFragItem.fragCount.ToString() + "); ";
						}
						// Add to database
						DB.ExecuteNonQuery(battleFragSQL);
					}
					// Insert battle achievments
					if (battleAchList.Count > 0)
					{
						// Loop through new frags
						string battleAchSQL = "";
						foreach (var newAchItem in battleAchList)
						{
							battleAchSQL += "INSERT INTO battleAch (battleId, achId, achCount) " +
													"VALUES (" + battleId + ", " + newAchItem.achId.ToString() + ", " + newAchItem.count.ToString() + "); ";
						}
						// Add to database
						DB.ExecuteNonQuery(battleAchSQL);
					}
				}
			//}
			//catch (Exception ex)
			//{
			//	string s = ex.Message;
			//	throw;
			//}
			
		}
	}
}
