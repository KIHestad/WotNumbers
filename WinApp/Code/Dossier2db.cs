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
using System.Windows.Forms;

namespace WinApp.Code
{
	public class Dossier2db
	{
		public static bool Running = false; // Flag to avoid running several dossier actions at the same time
		public static bool battleSaved = false; // If new battle is saved
		
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

		private static bool newTank = false;

		public static String ReadJson(string filename, bool ForceUpdate = false)
		{
			//try
			{
				// Read file into string
				StreamReader sr = new StreamReader(filename, Encoding.UTF8);
				string json = sr.ReadToEnd();
				sr.Close();
						
				Stopwatch sw = new Stopwatch();
				sw.Start();
			
				// Update base data
				TankHelper.GetPlayerTankAchList();
				TankHelper.GetPlayerTankFragList();

				// read json string
				JsonTextReader reader = new JsonTextReader(new StringReader(json));
						
				// logging
				Log.CheckLogFileSize();
								
				// Check for first run (if player tank = 0), then dont get battle result but force update
				bool saveBattleResult = true;
				if (TankHelper.GetPlayerTankCount() == 0)
				{
					saveBattleResult = false;
					ForceUpdate = true;
				}

				// Reset saved variable, sets to true if any new battle is found
				battleSaved = false;

				// Declare
				DataTable NewPlayerTankTable = TankHelper.GetPlayerTank(-1); // Return no data, only empty database with structure
                DataTable NewPlayerTankBattleTable = TankHelper.GetPlayerTankBattle(-1, BattleMode.TypeEnum.ModeRandom_TC, false); // Return no data, only empty database with structure
				DataRow NewPlayerTankRow = NewPlayerTankTable.NewRow();
				DataRow NewPlayerTankBattle15Row = NewPlayerTankBattleTable.NewRow();
				DataRow NewPlayerTankBattle7Row = NewPlayerTankBattleTable.NewRow();
				DataRow NewPlayerTankBattle7RankedRow = NewPlayerTankBattleTable.NewRow();
				DataRow NewPlayerTankBattleHistoricalRow = NewPlayerTankBattleTable.NewRow();
				DataRow NewPlayerTankBattleSkirmishesRow = NewPlayerTankBattleTable.NewRow();
				DataRow NewPlayerTankBattleStrongholdRow = NewPlayerTankBattleTable.NewRow();
				DataRow NewPlayerTankBattleGlobalMapRow = NewPlayerTankBattleTable.NewRow();
				string tankName = "";
				JsonItem currentItem = new JsonItem();
				string fragList = "";
				List<AchItem> achList = new List<AchItem>();
				// Loop through json file
				while (reader.Read())
				{
					if (reader.Depth > 3) // ********************************************  found fourth level = property and value  ************************************************************
					{
						// Found property, check if new property og new value
						if (reader.TokenType == JsonToken.PropertyName)
						{
							// New Property
							currentItem.property = reader.Value.ToString();
						}
						else if (reader.Value != null)
						{
							// Value exsists
							currentItem.value = reader.Value;

							// Check data by getting jsonPlayerTank Mapping
							string expression = "jsonMainSubProperty='" + currentItem.mainSection + "." + currentItem.subSection + "." + currentItem.property + "'";
							DataRow[] foundRows = TankHelper.json2dbMapping.Select(expression);
						
							//var result = from myRow in TankData.json2dbMapping.AsEnumerable()
							//			 where myRow.Field<string>("jsonMainSubProperty") == (currentItem.mainSection + "." + currentItem.subSection + "." + currentItem.property).ToString()
							//			 select myRow;
							//DataView foundRows = result.AsDataView();
						
							// IF mapping found add currentItem into NewPlayerTankRow
							if (foundRows.Length != 0)
							{
								if (foundRows[0]["dbPlayerTank"] != DBNull.Value) // Found mapping to PlayerTank
								{
									string dataType = foundRows[0]["dbDataType"].ToString();
									string dbField = foundRows[0]["dbPlayerTank"].ToString();
									var dbPlayerTankMode = foundRows[0]["dbPlayerTankMode"];
									if (dbPlayerTankMode == DBNull.Value)
									{
										// Default playerTank value
										switch (dataType)
										{
											case "String": NewPlayerTankRow[dbField] = currentItem.value.ToString(); break;
											case "DateTime": NewPlayerTankRow[dbField] = DateTimeHelper.ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); break;
											case "Int":
                                                int i = 0;
                                                if (currentItem.value != null)
                                                    Int32.TryParse(currentItem.value.ToString(), out i);
                                                NewPlayerTankRow[dbField] = i;
                                                break;
											case "BigInt": NewPlayerTankRow[dbField] = Convert.ToInt64(currentItem.value); break;
										}
									}
									else if (dbPlayerTankMode.ToString() == "15")
									{
										// playerTankBattle mode 15x15
										switch (dataType)
										{
											case "String": NewPlayerTankBattle15Row[dbField] = currentItem.value.ToString(); break;
											case "DateTime": NewPlayerTankBattle15Row[dbField] = DateTimeHelper.ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); break;
											case "Int": NewPlayerTankBattle15Row[dbField] = Convert.ToInt32(currentItem.value); break;
											case "BigInt": NewPlayerTankBattle15Row[dbField] = Convert.ToInt64(currentItem.value); break;
										}
									}
									else if (dbPlayerTankMode.ToString() == "7")
									{
										// playerTankBattle mode 7x7
										switch (dataType)
										{
											case "String": NewPlayerTankBattle7Row[dbField] = currentItem.value.ToString(); break;
											case "DateTime": NewPlayerTankBattle7Row[dbField] = DateTimeHelper.ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); break;
											case "Int": NewPlayerTankBattle7Row[dbField] = Convert.ToInt32(currentItem.value); break;
											case "BigInt": NewPlayerTankBattle7Row[dbField] = Convert.ToInt64(currentItem.value); break;
										}
									}
									else if (dbPlayerTankMode.ToString() == "7Ranked")
									{
										// playerTankBattle mode 7x7
										switch (dataType)
										{
											case "String": NewPlayerTankBattle7RankedRow[dbField] = currentItem.value.ToString(); break;
											case "DateTime": NewPlayerTankBattle7RankedRow[dbField] = DateTimeHelper.ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); break;
											case "Int": NewPlayerTankBattle7RankedRow[dbField] = Convert.ToInt32(currentItem.value); break;
											case "BigInt": NewPlayerTankBattle7RankedRow[dbField] = Convert.ToInt64(currentItem.value); break;
										}
									}
									else if (dbPlayerTankMode.ToString() == "Historical")
									{
										// playerTankBattle mode Historical
										switch (dataType)
										{
											case "String": NewPlayerTankBattleHistoricalRow[dbField] = currentItem.value.ToString(); break;
											case "DateTime": NewPlayerTankBattleHistoricalRow[dbField] = DateTimeHelper.ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); break;
											case "Int": NewPlayerTankBattleHistoricalRow[dbField] = Convert.ToInt32(currentItem.value); break;
											case "BigInt": NewPlayerTankBattleHistoricalRow[dbField] = Convert.ToInt64(currentItem.value); break;
										}
									}
									else if (dbPlayerTankMode.ToString() == "Skirmishes")
									{
										// playerTankBattle mode Historical
										switch (dataType)
										{
											case "String": NewPlayerTankBattleSkirmishesRow[dbField] = currentItem.value.ToString(); break;
											case "DateTime": NewPlayerTankBattleSkirmishesRow[dbField] = DateTimeHelper.ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); break;
											case "Int": NewPlayerTankBattleSkirmishesRow[dbField] = Convert.ToInt32(currentItem.value); break;
											case "BigInt": NewPlayerTankBattleSkirmishesRow[dbField] = Convert.ToInt64(currentItem.value); break;
										}
									}
									else if (dbPlayerTankMode.ToString() == "Stronghold")
									{
										// playerTankBattle mode Historical
										switch (dataType)
										{
											case "String": NewPlayerTankBattleStrongholdRow[dbField] = currentItem.value.ToString(); break;
											case "DateTime": NewPlayerTankBattleStrongholdRow[dbField] = DateTimeHelper.ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); break;
											case "Int": NewPlayerTankBattleStrongholdRow[dbField] = Convert.ToInt32(currentItem.value); break;
											case "BigInt": NewPlayerTankBattleStrongholdRow[dbField] = Convert.ToInt64(currentItem.value); break;
										}
									}
									else if (dbPlayerTankMode.ToString() == "GlobalMap")
									{
										// playerTankBattle mode Historical
										switch (dataType)
										{
											case "String": NewPlayerTankBattleGlobalMapRow[dbField] = currentItem.value.ToString(); break;
											case "DateTime": NewPlayerTankBattleGlobalMapRow[dbField] = DateTimeHelper.ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); break;
											case "Int": NewPlayerTankBattleGlobalMapRow[dbField] = Convert.ToInt32(currentItem.value); break;
											case "BigInt": NewPlayerTankBattleGlobalMapRow[dbField] = Convert.ToInt64(currentItem.value); break;
										}
									}
								}
								else if (foundRows[0]["dbAch"] != DBNull.Value) // Found mapped achievement
								{
									int count = Convert.ToInt32(currentItem.value);
									if (count > 0)
									{
										AchItem ach = new AchItem();
										ach.achName = currentItem.property.ToString();
										ach.count = count;
										achList.Add(ach);
									}
								}
							}
							//  Found unmapped achievent in dedicated subsections
							else if (currentItem.subSection == "achievements7x7" || currentItem.subSection == "achievements" || currentItem.subSection == "fortAchievements") 
							{
								int count = Convert.ToInt32(currentItem.value);
								if (count > 0)
								{
									AchItem ach = new AchItem();
									ach.achName = currentItem.property.ToString();
									ach.count = count;
									achList.Add(ach);
								}
							}

							// ****
							// Fraglist for tanks is not used
							// This method uses tank names, since IS-2 tank name is used for two tanks it cannot be used, have to use VBaddict tank ID to make perfect match
							// ****

							//else if (currentItem.subSection == "fragslist" || currentItem.subSection == "kills") // Check frags
							//{
							//	string newFraggedTank = currentItem.value.ToString() + ";";
							//	// avoid duplicates, else add frag
							//	if (!fragList.Contains(newFraggedTank))
							//		fragList += newFraggedTank ;
							//}

							// Temp log all data
							// log.Add("  " + currentItem.mainSection + "." + currentItem.tank + "." + currentItem.subSection + "." + currentItem.property + ":" + currentItem.value);
							// log.Add(currentItem.mainSection + ";" + currentItem.subSection + ";" + currentItem.property );
						}
					}
					else
					{
						if (reader.Depth == 3)
						{
							if (reader.Value != null) // ***************  found third level = subsection  **************************************
							{
								// Get subsection, set property blank
								currentItem.subSection = reader.Value.ToString();
								currentItem.property = ""; // reset property for reading next
							}
						}
						else
						{
							if (reader.Depth == 2)
							{
								if (reader.Value != null) // ****************   found second level = tank level  *****************************************
								{
									// Tank data exist, save data found and log
									if (tankName != "")
									{
										// log.Add("  > Check for DB update - Tank: '" + tankName );
										// TODO 7Ranked
										if (CheckTankDataResult(tankName, NewPlayerTankRow, NewPlayerTankBattle15Row, NewPlayerTankBattle7Row, NewPlayerTankBattle7RankedRow, NewPlayerTankBattleHistoricalRow, NewPlayerTankBattleSkirmishesRow, NewPlayerTankBattleStrongholdRow, NewPlayerTankBattleGlobalMapRow, fragList, achList, ForceUpdate, saveBattleResult))
											battleSaved = true; // result if battle was detected and saved
										// Reset all values
										NewPlayerTankTable.Clear();
										NewPlayerTankRow = NewPlayerTankTable.NewRow();
										NewPlayerTankBattle15Row = NewPlayerTankBattleTable.NewRow();
										NewPlayerTankBattle7Row = NewPlayerTankBattleTable.NewRow();
										NewPlayerTankBattle7RankedRow = NewPlayerTankBattleTable.NewRow();
										NewPlayerTankBattleHistoricalRow = NewPlayerTankBattleTable.NewRow();
										NewPlayerTankBattleSkirmishesRow = NewPlayerTankBattleTable.NewRow();
										NewPlayerTankBattleStrongholdRow = NewPlayerTankBattleTable.NewRow();
										NewPlayerTankBattleGlobalMapRow = NewPlayerTankBattleTable.NewRow();
										// clear frags and Achievements
										fragList = "";
										achList.Clear();
									}
									// Get new tank name
									if (currentItem.mainSection == "tanks_v2" || currentItem.mainSection == "tanks") // The only section containing tanks to be read
									{
										currentItem.tank = reader.Value.ToString(); // add to current item
										tankName = reader.Value.ToString(); // add to current tank
										string s = tankName;
									}
								}
							}
							else if (reader.Value != null) // main level ( 0 or 1)
							{
								// ***************************************************************  found main level - get mainSection name  *************************************
								currentItem.mainSection = reader.Value.ToString();
								//log.Add("\nMain section: " + currentItem.mainSection + "(Line: " + reader.LineNumber + ")");
							}
						}
					}
				}
				reader.Close();
				// Also write last tank found
				// log.Add("  > Check for DB update - Tank: '" + tankName );
				// TODO 7Ranked
				if (CheckTankDataResult(tankName, NewPlayerTankRow, NewPlayerTankBattle15Row, NewPlayerTankBattle7Row, NewPlayerTankBattle7RankedRow, NewPlayerTankBattleHistoricalRow, NewPlayerTankBattleSkirmishesRow, NewPlayerTankBattleStrongholdRow, NewPlayerTankBattleGlobalMapRow, fragList, achList, ForceUpdate, saveBattleResult)) 
					battleSaved = true; // result if battle was detected and saved
				// Done
				TankHelper.ClearPlayerTankAchList();
				TankHelper.ClearPlayerTankFragList();
			
				sw.Stop();
				TimeSpan ts = sw.Elapsed;
				// Log.LogToFile(log);
				NewPlayerTankTable.Dispose();
				NewPlayerTankTable.Clear();
				NewPlayerTankBattleTable.Dispose();
				NewPlayerTankBattleTable.Clear();
				// Check for new tanks, then load images
				if (newTank)
					ImageHelper.LoadTankImages(); // Load new image by reloading
				return "Battle fetch performed successfully"; // - time spent " + ts.Minutes + ":" + ts.Seconds + "." + ts.Milliseconds.ToString("000"));
			}
			/*catch (Exception ex)
			{
				Log.LogToFile(ex);
				return ("An error occured performing battle fetch, please check the log file");
			}
			 * */
			
		}

		public static bool CheckTankDataResult(string tankName, 
												DataRow playerTankNewRow, 
												DataRow playerTankBattle15NewRow,
												DataRow playerTankBattle7NewRow,
												DataRow playerTankBattle7RankedNewRow,
												DataRow playerTankBattleHistoricalNewRow, 
												DataRow PlayerTankBattleSkirmishesNewRow,
												DataRow PlayerTankBattleStrongholdNewRow,
												DataRow PlayerTankBattleGlobalMapNewRow,
												string fragList, 
												List<AchItem> achList, 
												bool forceUpdate = false, 
												bool saveBattleResult = true )
		{
			// Get Tank ID
			bool battleSave = false; // Sets true if battle is saved, and is return value
			// int tankId = TankData.GetTankID(tankName); old code - get from name
			if (playerTankNewRow["compactDescr"] == DBNull.Value)
			{
				Log.LogToFile("### Tank result terminated ### Did not find compactDescr in dossier file for tank: " + tankName,true);
				return false;
			}
			int tankId = Convert.ToInt32(playerTankNewRow["compactDescr"]);
			// Detect special battle mode for some tanks
			List<int> specialTanks = new List<int>( new[] {64801,64769,65089}); // Mammoth, Arctic Fox, Polar Bear
			bool specialTankFound = (specialTanks.Contains(tankId));
			// Get tank new battle count
			int playerTankNewRow_battles15 = 0;
			int playerTankNewRow_battles7 = 0;
			int playerTankNewRow_battles7Ranked = 0;
			int playerTankNewRow_battlesHistorical = 0;
			int playerTankNewRow_battlesSkirmishes = 0;
			int playerTankNewRow_battlesStronghold = 0;
			int playerTankNewRow_battlesGlobalMap = 0;
			if (playerTankBattle15NewRow["battles"] != DBNull.Value) playerTankNewRow_battles15 = Convert.ToInt32(playerTankBattle15NewRow["battles"]);
			if (playerTankBattle7NewRow["battles"] != DBNull.Value) playerTankNewRow_battles7 = Convert.ToInt32(playerTankBattle7NewRow["battles"]);
			if (playerTankBattle7RankedNewRow["battles"] != DBNull.Value) playerTankNewRow_battles7Ranked = Convert.ToInt32(playerTankBattle7RankedNewRow["battles"]);
			if (playerTankBattleHistoricalNewRow["battles"] != DBNull.Value) playerTankNewRow_battlesHistorical = Convert.ToInt32(playerTankBattleHistoricalNewRow["battles"]);
			if (PlayerTankBattleSkirmishesNewRow["battles"] != DBNull.Value) playerTankNewRow_battlesSkirmishes = Convert.ToInt32(PlayerTankBattleSkirmishesNewRow["battles"]);
			if (PlayerTankBattleStrongholdNewRow["battles"] != DBNull.Value) playerTankNewRow_battlesStronghold = Convert.ToInt32(PlayerTankBattleStrongholdNewRow["battles"]);
			if (PlayerTankBattleGlobalMapNewRow["battles"] != DBNull.Value) playerTankNewRow_battlesGlobalMap = Convert.ToInt32(PlayerTankBattleGlobalMapNewRow["battles"]);
			// Check if battle count has increased, get existing battle count
			DataTable playerTankOldTable = TankHelper.GetPlayerTank(tankId); // Return Existing Player Tank Data
			// Check if Player has this tank
			if (playerTankOldTable.Rows.Count == 0)
			{
				// New tank detected, this parts only run when new tank is detected
				SaveNewPlayerTank(tankId, tankName); // Save new tank
				playerTankOldTable = TankHelper.GetPlayerTank(tankId); // Get data into DataTable once more now after row is added
			}
			// Get the get existing (old) tank data row
			DataRow playerTankOldRow = playerTankOldTable.Rows[0];
			int playerTankId = Convert.ToInt32(playerTankOldTable.Rows[0]["id"]);
			// Get the old battle count
			int playerTankOldRow_wins15 = 0;
			int playerTankOldRow_wins7 = 0;
			int playerTankOldRow_wins7Ranked = 0;
			int playerTankOldRow_winsHistorical = 0;
			int playerTankOldRow_winsSkirmishes = 0;
			int playerTankOldRow_winsStronghold = 0;
			int playerTankOldRow_winsGlobalMap = 0;
			int playerTankOldRow_xp15 = 0;
			int playerTankOldRow_xp7 = 0;
			int playerTankOldRow_xp7Ranked = 0;
			int playerTankOldRow_xpHistorical = 0;
			int playerTankOldRow_xpSkirmishes = 0;
			int playerTankOldRow_xpStronghold = 0;
			int playerTankOldRow_xpGlobalMap = 0;
            int playerTankOldRow_battles15 = TankHelper.GetPlayerTankBattleCount(playerTankId, BattleMode.TypeEnum.ModeRandom_TC, out playerTankOldRow_wins15, out playerTankOldRow_xp15);
            int playerTankOldRow_battles7 = TankHelper.GetPlayerTankBattleCount(playerTankId, BattleMode.TypeEnum.ModeTeam, out playerTankOldRow_wins7, out playerTankOldRow_xp7);
            int playerTankOldRow_battles7Ranked = TankHelper.GetPlayerTankBattleCount(playerTankId, BattleMode.TypeEnum.ModeTeamRanked, out playerTankOldRow_wins7Ranked, out playerTankOldRow_xp7Ranked);
            int playerTankOldRow_battlesHistorical = TankHelper.GetPlayerTankBattleCount(playerTankId, BattleMode.TypeEnum.ModeHistorical, out playerTankOldRow_winsHistorical, out playerTankOldRow_xpHistorical);
            int playerTankOldRow_battlesSkirmishes = TankHelper.GetPlayerTankBattleCount(playerTankId, BattleMode.TypeEnum.ModeSkirmishes, out playerTankOldRow_winsSkirmishes, out playerTankOldRow_xpSkirmishes);
            int playerTankOldRow_battlesStronghold = TankHelper.GetPlayerTankBattleCount(playerTankId, BattleMode.TypeEnum.ModeStronghold, out playerTankOldRow_winsStronghold, out playerTankOldRow_xpStronghold);
            int playerTankOldRow_battlesGlobalMap = TankHelper.GetPlayerTankBattleCount(playerTankId, BattleMode.TypeEnum.ModeGlobalMap, out playerTankOldRow_winsGlobalMap, out playerTankOldRow_xpGlobalMap); 
				
			// Calculate number of new battles 
			int battlesNew15 = playerTankNewRow_battles15 - playerTankOldRow_battles15;
			int battlesNew7 = playerTankNewRow_battles7 - playerTankOldRow_battles7;
			int battlesNew7Ranked = playerTankNewRow_battles7Ranked - playerTankOldRow_battles7Ranked;
			int battlesNewHistorical = playerTankNewRow_battlesHistorical - playerTankOldRow_battlesHistorical;
			int battlesNewSkirmishes = playerTankNewRow_battlesSkirmishes - playerTankOldRow_battlesSkirmishes;
			int battlesNewStronghold = playerTankNewRow_battlesStronghold - playerTankOldRow_battlesStronghold;
			int battlesNewGlobalMap = playerTankNewRow_battlesGlobalMap - playerTankOldRow_battlesGlobalMap;

			// Check if new battle on this tank then do db update, if force do it anyway
			if (battlesNew15 > 0 || battlesNew7 > 0 || battlesNew7Ranked > 0 || battlesNewHistorical > 0 || battlesNewSkirmishes > 0 || battlesNewStronghold > 0 || battlesNewGlobalMap > 0 || specialTankFound ||
				(forceUpdate && (playerTankOldRow_battles15 > 0 || playerTankOldRow_battles7 > 0 || playerTankOldRow_battles7Ranked > 0 || playerTankOldRow_battlesHistorical > 0 || playerTankOldRow_battlesSkirmishes > 0 || playerTankOldRow_battlesStronghold > 0 || playerTankOldRow_battlesGlobalMap > 0 || specialTankFound)))
			{  
				// Update playerTank
				string sqlFields = "";
				foreach (DataColumn column in playerTankOldTable.Columns)
				{
					// Get columns and values from NewPlayerTankRow direct
					if (column.ColumnName != "Id" && playerTankNewRow[column.ColumnName] != DBNull.Value) // avoid the PK and if new data is NULL 
					{
						string colName = column.ColumnName;
						string colType = column.DataType.Name;
						sqlFields += ", " + colName + "=";
						switch (colType)
						{
							case "String": 
								sqlFields += "'" + playerTankNewRow[colName] + "'"; 
								break;
							case "DateTime": 
								DateTime dateTime = DateTimeHelper.AdjustForTimeZone(Convert.ToDateTime(playerTankNewRow[colName]));
								if (dateTime.Year == 1970)
									sqlFields += "NULL";
								else
                                    sqlFields += "'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) + "'"; 
								break;
							default: 
								sqlFields += playerTankNewRow[colName]; 
								break;
						}
					}
				}
				// Update database
				if (sqlFields.Length > 0 )
				{
					sqlFields = sqlFields.Substring(1); // Remove first comma
					string sql = "UPDATE playerTank SET " + sqlFields + " WHERE Id=@Id ";
					DB.AddWithValue(ref sql, "@Id", playerTankOldTable.Rows[0]["id"], DB.SqlDataType.Int);
					DB.ExecuteNonQuery(sql);
				}
					
				// No longer in us
				// Check fraglist to update playertank frags
				List<FragItem> battleFragList = new List<FragItem>(); //  UpdatePlayerTankFrag(tankId, playerTankId, fragList);

				// Check if achivment exists
				List<AchItem> battleAchList = UpdatePlayerTankAch(tankId, playerTankId, achList);
									

				// If detected several battle modes, dont save fraglist and achivements to battle, as we don't know how to seperate them
				int severalModes = 0;
				if (battlesNew15 > 0) severalModes++;
				if (battlesNew7 > 0) severalModes++;
				if (battlesNew7Ranked > 0) severalModes++;
				if (battlesNewHistorical > 0) severalModes++;
				if (battlesNewSkirmishes > 0) severalModes++;
				if (battlesNewStronghold > 0) severalModes++;
				if (battlesNewGlobalMap > 0) severalModes++;
				if (severalModes > 1)
				{
					battleFragList.Clear();
					battleAchList.Clear();
				}

				// Now update playerTank battle for different battle modes
				if (battlesNew15 > 0 || (forceUpdate && playerTankOldRow_battles15 != 0))
				{
                    UpdatePlayerTankBattle(BattleMode.TypeEnum.ModeRandom_TC, playerTankId, tankId, playerTankNewRow, playerTankOldRow, playerTankBattle15NewRow,
											playerTankNewRow_battles15, battlesNew15, battleFragList, battleAchList, saveBattleResult);
					battleSave = true;
				}
				if (battlesNew7 > 0 || (forceUpdate && playerTankOldRow_battles7 != 0))
				{
                    UpdatePlayerTankBattle(BattleMode.TypeEnum.ModeTeam, playerTankId, tankId, playerTankNewRow, playerTankOldRow, playerTankBattle7NewRow,
											playerTankNewRow_battles7, battlesNew7, battleFragList, battleAchList, saveBattleResult);
					battleSave = true;
				}
				if (battlesNew7Ranked > 0 || (forceUpdate && playerTankOldRow_battles7Ranked != 0))
				{
                    UpdatePlayerTankBattle(BattleMode.TypeEnum.ModeTeamRanked, playerTankId, tankId, playerTankNewRow, playerTankOldRow, playerTankBattle7RankedNewRow,
											playerTankNewRow_battles7Ranked, battlesNew7Ranked, battleFragList, battleAchList, saveBattleResult);
					battleSave = true;
				}
				if (battlesNewHistorical > 0 || (forceUpdate && playerTankOldRow_battlesHistorical != 0))
				{
                    UpdatePlayerTankBattle(BattleMode.TypeEnum.ModeHistorical, playerTankId, tankId, playerTankNewRow, playerTankOldRow, playerTankBattleHistoricalNewRow,
											playerTankNewRow_battlesHistorical, battlesNewHistorical, battleFragList, battleAchList, saveBattleResult);
					battleSave = true;
				}
				if (battlesNewSkirmishes > 0 || (forceUpdate && playerTankOldRow_battlesSkirmishes != 0))
				{
                    UpdatePlayerTankBattle(BattleMode.TypeEnum.ModeSkirmishes, playerTankId, tankId, playerTankNewRow, playerTankOldRow, PlayerTankBattleSkirmishesNewRow,
											playerTankNewRow_battlesSkirmishes, battlesNewSkirmishes, battleFragList, battleAchList, saveBattleResult);
					battleSave = true;
				}
				if (battlesNewStronghold > 0 || (forceUpdate && playerTankOldRow_battlesStronghold != 0))
				{
                    UpdatePlayerTankBattle(BattleMode.TypeEnum.ModeStronghold, playerTankId, tankId, playerTankNewRow, playerTankOldRow, PlayerTankBattleStrongholdNewRow,
											playerTankNewRow_battlesStronghold, battlesNewStronghold, battleFragList, battleAchList, saveBattleResult);
					battleSave = true;
				} 
				if (battlesNewGlobalMap > 0 || (forceUpdate && playerTankOldRow_battlesGlobalMap != 0))
				{
                    UpdatePlayerTankBattle(BattleMode.TypeEnum.ModeGlobalMap, playerTankId, tankId, playerTankNewRow, playerTankOldRow, PlayerTankBattleGlobalMapNewRow,
											playerTankNewRow_battlesGlobalMap, battlesNewGlobalMap, battleFragList, battleAchList, saveBattleResult);
					battleSave = true;
				}
				if (specialTankFound)
				{
					// for special tanks no stats are reported from dossier, make sure playerTankBattle exists anyway for battle mode "Special"
					// Get or create playerTank BattleResult
                    DataTable playerTankBattleOld = TankHelper.GetPlayerTankBattle(playerTankId, BattleMode.TypeEnum.ModeSpecial, true);
				}
			}
			playerTankOldTable.Dispose();
			playerTankOldTable.Clear();
			
			return battleSave;
		}

		private static void SaveNewPlayerTank(int tankId, string tankName)
		{
			// Check if this tank exists
			if (!TankHelper.TankExists(tankId))
                TankHelper.CreateUnknownTank(tankId, tankName);

			// Add to database
			string sql = "INSERT INTO PlayerTank (tankId, playerId) VALUES (@tankId, @playerId); ";
			DB.AddWithValue(ref sql, "@tankId", tankId, DB.SqlDataType.Int);
			DB.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, DB.SqlDataType.Int);
			DB.ExecuteNonQuery(sql);
			newTank = true;
		}

		private static List<AchItem> UpdatePlayerTankAch(int tankId, int playerTankId, List<AchItem> achList)
		{
			List<AchItem> battleAchList = new List<AchItem>();
			if (achList.Count > 0) // new ach detected
			{
				string sqlTotal = "";
				// Loop through all achivements
				foreach (AchItem newAch in achList)
				{
					if (newAch.count > 0) // Find the ones achieved
					{
						// Find ach ID
						string expression = "name=@achName";
						DB.AddWithValue(ref expression, "@achName", newAch.achName, DB.SqlDataType.VarChar);
						DataRow[] lookupAch = TankHelper.achList.Select(expression);
						
						//string sql = "SELECT id FROM ach WHERE name=@achName; ";
						//DB.AddWithValue(ref sql, "@achName", newAch.achName, DB.SqlDataType.VarChar);
						//DataTable lookupAch = DB.FetchData(sql);
						if (lookupAch.Length > 0)
						{
							// Found ach, get id now
							int achId = Convert.ToInt32(lookupAch[0]["id"]);
							// Find the current achievent
							expression = "playerTankId=" + playerTankId.ToString() + " AND achId=" + achId.ToString();
							DataRow[] lookupPlayerTankAch = TankHelper.playerTankAchList.Select(expression);
						
							//string sql = "SELECT * FROM playerTankAch WHERE playerTankId=@playerTankId AND achId=@achId; ";
							//DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
							//DB.AddWithValue(ref sql, "@achId", achId, DB.SqlDataType.Int);
							//DataTable currentAch = DB.FetchData(sql);
							if (lookupPlayerTankAch.Length == 0) // new achievment
							{
								// Insert new acheivement
								string sql = "INSERT INTO playerTankAch (achCount, playerTankId, achId) " +
									  "VALUES (@achCount, @playerTankId, @achId); ";
								DB.AddWithValue(ref sql, "@achCount", newAch.count, DB.SqlDataType.Int);
								DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
								DB.AddWithValue(ref sql, "@achId", achId, DB.SqlDataType.Int);
								//DB.ExecuteNonQuery(sql);
								sqlTotal += sql + Environment.NewLine;
								// Add to battle achievment
								AchItem ach = new AchItem();
								ach.achId = achId;
								ach.count = newAch.count;
								battleAchList.Add(ach);
							}
							else // achievent found, check count
							{
								int oldCount = Convert.ToInt32(lookupPlayerTankAch[0]["achCount"]);
								if (newAch.count > oldCount)
								{
									// Update achievment increased count
									string sql = "UPDATE playerTankAch SET achCount=@achCount " +
										  "WHERE playerTankId=@playerTankId AND achId=@achId; ";
									DB.AddWithValue(ref sql, "@achCount", newAch.count, DB.SqlDataType.Int);
									DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
									DB.AddWithValue(ref sql, "@achId", achId, DB.SqlDataType.Int);
									//DB.ExecuteNonQuery(sql);
									sqlTotal += sql + Environment.NewLine;
									// Add to battle achievment
									AchItem ach = new AchItem();
									ach.achId = achId;
									ach.count = newAch.count - oldCount;
									battleAchList.Add(ach);
								}
							}
						}
					}
				}
				DB.ExecuteNonQuery(sqlTotal, true, true);
			}
			return battleAchList;
		}

        private static void UpdatePlayerTankBattle(BattleMode.TypeEnum battleMode, 
													int playerTankId, 
													int tankId,
													DataRow playerTankNewRow, DataRow playerTankOldRow,
													DataRow playerTankBattleNewRow,
													int playerTankNewRow_battles,
													int battlesNew, 
													List<FragItem> battleFragList, 
													List<AchItem> battleAchList,
													bool saveBattleResult)
		{
			// Get or create playerTank BattleResult
			DataTable playerTankBattleOld = TankHelper.GetPlayerTankBattle(playerTankId, battleMode, true); 
			// Update playerTankBattle
			string sqlFields = "";
			// Get rating parameters
            Code.Rating.WNHelper.RatingParameters rp = new Code.Rating.WNHelper.RatingParameters();
            rp.DAMAGE = Code.Rating.WNHelper.ConvertDbVal2Double(playerTankBattleNewRow["dmg"]);
            rp.SPOT = Code.Rating.WNHelper.ConvertDbVal2Double(playerTankBattleNewRow["spot"]);
            rp.FRAGS = Code.Rating.WNHelper.ConvertDbVal2Double(playerTankBattleNewRow["frags"]);
            rp.DEF = Code.Rating.WNHelper.ConvertDbVal2Double(playerTankBattleNewRow["def"]);
            rp.WINS = Code.Rating.WNHelper.ConvertDbVal2Double(playerTankBattleNewRow["wins"]);
            rp.CAP = Code.Rating.WNHelper.ConvertDbVal2Double(playerTankBattleNewRow["cap"]);
            rp.BATTLES = playerTankNewRow_battles;
            // Calculate WN9
            double wn9maxhist = 0;
            sqlFields += " wn9=" + Math.Round(Code.Rating.WN9.CalcTank(tankId, rp, out wn9maxhist), 0).ToString();
            sqlFields += ", wn9maxhist=@wn9maxhist";
            DB.AddWithValue(ref sqlFields, "@wn9maxhist", wn9maxhist, DB.SqlDataType.Float);
            // Calculate WN8
            sqlFields += ", wn8=" + Math.Round(Code.Rating.WN8.CalcTank(tankId, rp), 0).ToString();
			// Calculate Eff
            sqlFields += ", eff=" + Math.Round(Code.Rating.EFF.EffTank(tankId, rp), 0).ToString();
            // Calculate WN7 - use special tier
            rp.TIER = TankHelper.GetTankTier(tankId);
            sqlFields += ", wn7=" + Math.Round(Code.Rating.WN7.WN7tank(rp), 0).ToString();
			// Calculate RWR
            sqlFields += ", rwr=" + Code.Rating.RWR.RWRtank(tankId, rp);
			foreach (DataColumn column in playerTankBattleOld.Columns)
			{
				// Get columns and values from NewPlayerTankRow direct
				if (column.ColumnName != "Id" && playerTankBattleNewRow[column.ColumnName] != DBNull.Value) // avoid the PK and if new data is NULL 
				{
					string colName = column.ColumnName;
					string colType = column.DataType.Name;
					sqlFields += ", " + colName + "=";
					switch (colType)
					{
						case "String": 
							sqlFields += "'" + playerTankBattleNewRow[colName] + "'"; 
							break;
						case "DateTime":
							DateTime dateTime = DateTimeHelper.AdjustForTimeZone(Convert.ToDateTime(playerTankBattleNewRow[colName]));
							if (dateTime.Year == 1970)
								sqlFields += "NULL";
							else
                                sqlFields += "'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) + "'"; 
							break;
						default: 
							sqlFields += playerTankBattleNewRow[colName]; 
							break;
					}
				}
			}
			// Update database
			if (sqlFields.Length > 0)
			{
				string sql = "UPDATE playerTankBattle SET " + sqlFields + " WHERE playerTankId=@playerTankId AND battleMode=@battleMode; ";
				DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
                DB.AddWithValue(ref sql, "@battleMode", BattleMode.GetItemFromType(battleMode).SqlName, DB.SqlDataType.VarChar);
				DB.ExecuteNonQuery(sql);
			}
			// Add battle, if any and not first run - then avoid
			if (saveBattleResult && battlesNew > 0)
				AddBattle(playerTankNewRow, playerTankOldRow, playerTankBattleNewRow, playerTankBattleOld.Rows[0], battleMode, tankId, playerTankId, battlesNew, battleFragList, battleAchList);
			playerTankBattleOld.Dispose();
			playerTankBattleOld.Clear();
		}

		private static List<FragItem> UpdatePlayerTankFrag(int tankId, int playerTankId, string fraglist)
		{
			List<FragItem> battleFrag = new List<FragItem>();
			try
			{
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
					FragItem.tankId = TankHelper.GetTankID(FragItem.tankName);
					newFrag.Add(FragItem);
				}
				// Check newFrag compared to existing frags for this tank
				List<FragItem> oldFrag = new List<FragItem>();
				// Get frags for this tank

				string expression = "PlayerTankTankId=" + tankId.ToString();
				DataRow[] lookupPlayerFrag = TankHelper.playerTankFragList.Select(expression);

				//string sql =
				//	"SELECT playerTank.id AS playerTankId, playerTankFrag.* " +
				//	"FROM playerTank INNER JOIN playerTankFrag ON playerTank.id=playerTankFrag.playerTankId " +
				//	"WHERE playerTank.tankId=@tankId; ";
				//DB.AddWithValue(ref sql, "@tankId", tankId, DB.SqlDataType.Int);
				//DataTable dt = DB.FetchData(sql);
				
				// If no frags exists for this tank get playerTankId separately
				foreach (DataRow reader in lookupPlayerFrag)
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
												" WHERE playerTankId=" + oldFrag[i].playerTankId +
												" AND fraggedTankId=" + newFragItem.tankId + "; ";
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
					DB.ExecuteNonQuery(playerTankFragSQL, true, true);
				}
			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
			}
			
			return battleFrag;
		}

		private static void AddBattle(DataRow playerTankNewRow, 
										 DataRow playerTankOldRow, 
										 DataRow playerTankBattleNewRow, 
										 DataRow playerTankBattleOldRow,
                                         BattleMode.TypeEnum battleMode, 
										 int tankId, 
										 int playerTankId, 
										 int battlesCount, 
										 List<FragItem> battleFragList, 
										 List<AchItem> battleAchList)
		{
			try
			{
				// Create datarow to put calculated battle data
				DataTable battleTableNew = TankHelper.GetBattle(-1); // Return no data, only empty database with structure
				DataRow battleNewRow = battleTableNew.NewRow();
				foreach (DataRow dr in TankHelper.GetTankData2BattleMapping(battleMode).Rows)
				{
					// Mapping fields
					string battleField = dr["dbBattle"].ToString();
					string playerTankField = dr["dbPlayerTank"].ToString();
					if (dr["dbPlayerTankMode"] == DBNull.Value) // Default player tank data
					{
						// Check datatype and calculate value
						if (dr["dbDataType"].ToString() == "DateTime") // For DateTime get the new value
						{
							battleNewRow[battleField] = playerTankNewRow[playerTankField];
						}
						else // For integers calculate new value as diff between new and old value
						{
							// Calculate difference from old to new Playertank result
							if (battleNewRow[battleField] == DBNull.Value) battleNewRow[battleField] = 0;
							int oldvalue = 0;
							int newvalue = 0;
							if (playerTankNewRow[playerTankField] != DBNull.Value) newvalue = Convert.ToInt32(playerTankNewRow[playerTankField]);
							if (playerTankOldRow[playerTankField] != DBNull.Value) oldvalue = Convert.ToInt32(playerTankOldRow[playerTankField]);
							battleNewRow[battleField] = (Convert.ToInt32(battleNewRow[battleField]) + newvalue - oldvalue);
						}
					}
					else // Battle Mode data
					{
						// Check datatype and calculate value
						if (dr["dbDataType"].ToString() == "DateTime") // For DateTime get the new value
						{
							battleNewRow[battleField] = playerTankBattleNewRow[playerTankField];
						}
						else // For integers calculate new value as diff between new and old value
						{
							// Calculate difference from old to new Playertank result
							if (battleNewRow[battleField] == DBNull.Value) battleNewRow[battleField] = 0;
							int oldvalue = 0;
							int newvalue = 0;
							if (playerTankBattleNewRow[playerTankField] != DBNull.Value) newvalue = Convert.ToInt32(playerTankBattleNewRow[playerTankField]);
							if (playerTankBattleOldRow[playerTankField] != DBNull.Value) oldvalue = Convert.ToInt32(playerTankBattleOldRow[playerTankField]);
							battleNewRow[battleField] = (Convert.ToInt32(battleNewRow[battleField]) + newvalue - oldvalue);
							// Debug.WriteLine("{1} : {2} -> {3}", battleField, oldvalue, newvalue);
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
				foreach (DataColumn column in battleTableNew.Columns)
				{
					if (column.ColumnName != "Id" && column.ColumnName != "playerTankID" && battleNewRow[column.ColumnName] != DBNull.Value) // avoid the PK and if new data is NULL 
					{
						string colName = column.ColumnName;
						string colType = column.DataType.Name;
						sqlFields += ", " + colName;
						switch (colType)
						{
							case "String": 
								sqlValues += ", '" + battleNewRow[colName] + "'"; 
								break;
							case "DateTime":
								DateTime dateTime = DateTimeHelper.AdjustForTimeZone(Convert.ToDateTime(battleNewRow[colName]));
                                sqlValues += ", '" + dateTime.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) + "'"; 
								break;
							default:
								{
									int value = Convert.ToInt32(battleNewRow[colName]);
									if (battlesCount > 1 && avgCols.Contains(colName)) value = value / battlesCount; // Calc average values
									sqlValues += ", " + value.ToString(); 
									break;
								}
						}
					}
				}
				// Get rating parameters
                Code.Rating.WNHelper.RatingParameters rp = new Code.Rating.WNHelper.RatingParameters();
                rp.DAMAGE = Code.Rating.WNHelper.ConvertDbVal2Double(battleNewRow["dmg"]);
                rp.SPOT = Code.Rating.WNHelper.ConvertDbVal2Double(battleNewRow["spotted"]);
                rp.FRAGS = Code.Rating.WNHelper.ConvertDbVal2Double(battleNewRow["frags"]);
                rp.DEF = Code.Rating.WNHelper.ConvertDbVal2Double(battleNewRow["def"]);
                rp.CAP = Code.Rating.WNHelper.ConvertDbVal2Double(battleNewRow["cap"]);
                rp.WINS = Code.Rating.WNHelper.ConvertDbVal2Double(battleNewRow["victory"]);
                rp.BATTLES = battlesCount;
                // Calculate WN9
                double wn9maxhist = 0; // Not in use for battle
                sqlFields += ", wn9";
                sqlValues += ", " + Math.Round(Code.Rating.WN9.CalcBattle(tankId, rp, out wn9maxhist), 0).ToString();
                
                // Calculate WN8
                sqlFields += ", wn8";
                sqlValues += ", " + Math.Round(Code.Rating.WN8.CalcBattle(tankId, rp), 0).ToString();
				// Calc Eff
				sqlFields += ", eff";
                sqlValues += ", " + Math.Round(Code.Rating.EFF.EffBattle(tankId, rp), 0).ToString();
                // Calculate WN7
                // Special tier calc
                sqlFields += ", wn7";
                rp.TIER = Code.Rating.WNHelper.GetAverageTier(BattleMode.GetItemFromType(battleMode).SqlName);
                sqlValues += ", " + Math.Round(Code.Rating.WN7.WN7battle(rp, true), 0).ToString();
				
				// Add battle mode
				sqlFields += ", battleMode";
				sqlValues += ", @battleMode";
                DB.AddWithValue(ref sqlValues, "@battleMode", BattleMode.GetItemFromType(battleMode).SqlName, DB.SqlDataType.VarChar); 
				// Calculate battle result
				int victorycount = Convert.ToInt32(battleNewRow["victory"]);
				int defeatcount = Convert.ToInt32(battleNewRow["defeat"]);
				int drawcount = battlesCount - victorycount - defeatcount;
				battleNewRow["draw"] = battlesCount - victorycount - defeatcount;
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
				sqlFields += ", battleResultId "; 
				sqlValues += ", " + battleResult.ToString();
				sqlFields += ", draw "; 
				sqlValues += ", " + drawcount.ToString();
				// Calculate battle survive
				int survivecount = Convert.ToInt32(battleNewRow["survived"]);
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
				sqlFields += ", battleSurviveId "; 
				sqlValues += ", " + battleSurvive.ToString();
				sqlFields += ", killed "; 
				sqlValues += ", " + killedcount.ToString();
                // Rank by avg damage and progress (delta value), only for random battles
                if (battleMode == BattleMode.TypeEnum.ModeRandom_TC)
                {
                    // Total
                    double rankDmg = Convert.ToDouble(playerTankBattleNewRow["damageRating"]);
                    sqlFields += ", damageRatingTotal ";
                    sqlValues += ", " + rankDmg.ToString().Replace(",", ".");
                    // Progress
                    rankDmg -= Convert.ToDouble(playerTankBattleOldRow["damageRating"]);
                    sqlFields += ", damageRating ";
                    sqlValues += ", " + rankDmg.ToString().Replace(",",".");
                }
                // Calc battle start time
                sqlFields += ", battleTimeStart ";
                // Get the battle end time, subtract lifetime to estimate start time - will be overwritten with actual start time from battle result
                DateTime battleDateTime = DateTimeHelper.AdjustForTimeZone(Convert.ToDateTime(battleNewRow["battletime"]));
                int lifetime = Convert.ToInt32(battleNewRow["battleLifeTime"]);
                if (lifetime > 180)
                    lifetime -= 120; // Normally lifetime is more than actually lifetime, probably because of loading time is included?
                sqlValues += ", '" + battleDateTime.AddSeconds(-lifetime).ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) + "'";
                
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
					
					// No longer in use
					// Insert Battle Frags
					//if (battleFragList.Count > 0)
					//{
					//	// Loop through new frags
					//	string battleFragSQL = "";
					//	foreach (var newFragItem in battleFragList)
					//	{
					//		battleFragSQL += "INSERT INTO battleFrag (battleId, fraggedTankId, fragCount) " +
					//						 "VALUES (" + battleId + ", " + newFragItem.tankId + ", " + newFragItem.fragCount.ToString() + "); ";
					//	}
					//	// Add to database
					//	DB.ExecuteNonQuery(battleFragSQL);
					//}
					// Insert battle Achievements
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
					dt.Dispose();
					dt.Clear();
				}
				battleTableNew.Dispose();
				battleTableNew.Clear();

			}
			catch (Exception ex)
			{
				Log.LogToFile(ex);
			}
			
		}
	}
}
