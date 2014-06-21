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

namespace WinApp.Code
{
	class Dossier2db
	{
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
			
			// Update base data
			TankData.GetPlayerTankAchList();
			TankData.GetPlayerTankFragList();

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
			DataTable NewPlayerTankTable = TankData.GetPlayerTank(-1); // Return no data, only empty database with structure
			DataTable NewPlayerTankBattleTable = TankData.GetPlayerTankBattle(-1, TankData.DossierBattleMode.Mode15, false); // Return no data, only empty database with structure
			DataRow NewPlayerTankRow = NewPlayerTankTable.NewRow();
			DataRow NewPlayerTankBattle15Row = NewPlayerTankBattleTable.NewRow();
			DataRow NewPlayerTankBattle7Row = NewPlayerTankBattleTable.NewRow();
			DataRow NewPlayerTankBattleHistoricalRow = NewPlayerTankBattleTable.NewRow();
			string tankName = "";
			JsonItem currentItem = new JsonItem();
			string fragList = "";
			List<AchItem> achList = new List<AchItem>();
			string[] achDossierSubLevel3 = { "", "Two", "Three" };
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
						DataRow[] foundRows = TankData.json2dbMapping.Select(expression);
						
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
										case "DateTime":
											// Fix for missing date on lastBattleTime
											if (currentItem.property == "lastBattleTime" && currentItem.value.ToString() == "0")
												NewPlayerTankRow[dbField] = DateTime.Now;
											else
												NewPlayerTankRow[dbField] = ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value));
											break;
										case "Int": NewPlayerTankRow[dbField] = Convert.ToInt32(currentItem.value); break;
									}
								}
								else if (dbPlayerTankMode.ToString() == "15")
								{
									// playerTankBattle mode 15x15
									switch (dataType)
									{
										case "String": NewPlayerTankBattle15Row[dbField] = currentItem.value.ToString(); break;
										case "DateTime": NewPlayerTankBattle15Row[dbField] = ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); break;
										case "Int": NewPlayerTankBattle15Row[dbField] = Convert.ToInt32(currentItem.value); break;
									}
								}
								else if (dbPlayerTankMode.ToString() == "7")
								{
									// playerTankBattle mode 7x7
									switch (dataType)
									{
										case "String": NewPlayerTankBattle7Row[dbField] = currentItem.value.ToString(); break;
										case "DateTime": NewPlayerTankBattle7Row[dbField] = ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); break;
										case "Int": NewPlayerTankBattle7Row[dbField] = Convert.ToInt32(currentItem.value); break;
									}
								}
								else if (dbPlayerTankMode.ToString() == "Historical")
								{
									// playerTankBattle mode Historical
									switch (dataType)
									{
										case "String": NewPlayerTankBattleHistoricalRow[dbField] = currentItem.value.ToString(); break;
										case "DateTime": NewPlayerTankBattleHistoricalRow[dbField] = ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); break;
										case "Int": NewPlayerTankBattleHistoricalRow[dbField] = Convert.ToInt32(currentItem.value); break;
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
						else if ( currentItem.subSection == "achievements7x7" || currentItem.subSection == "achievements") //  Found unmapped achievent in dedicated subsections
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
						else if (currentItem.subSection == "fragslist" || currentItem.subSection == "kills") // Check frags
							fragList += currentItem.value.ToString() + ";";
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
									if (CheckTankDataResult(tankName, NewPlayerTankRow, NewPlayerTankBattle15Row, NewPlayerTankBattle7Row, NewPlayerTankBattleHistoricalRow, fragList, achList, ForceUpdate, saveBattleResult))
										battleSaved = true; // result if battle was detected and saved
									// Reset all values
									NewPlayerTankTable.Clear();
									NewPlayerTankRow = NewPlayerTankTable.NewRow();
									NewPlayerTankBattle15Row = NewPlayerTankBattleTable.NewRow();
									NewPlayerTankBattle7Row = NewPlayerTankBattleTable.NewRow();
									NewPlayerTankBattleHistoricalRow = NewPlayerTankBattleTable.NewRow();
									// clear frags and achievments
									fragList = "";
									achList.Clear();
								}
								// Get new tank name
								if (currentItem.mainSection == "tanks_v2" || currentItem.mainSection == "tanks") // The only section containing tanks to be read
								{
									currentItem.tank = reader.Value.ToString(); // add to current item
									tankName = reader.Value.ToString(); // add to current tank
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
			if (CheckTankDataResult(tankName, NewPlayerTankRow, NewPlayerTankBattle15Row, NewPlayerTankBattle7Row, NewPlayerTankBattleHistoricalRow, fragList, achList, ForceUpdate, saveBattleResult)) 
				battleSaved = true; // result if battle was detected and saved
			// Done
			TankData.ClearPlayerTankAchList();
			TankData.ClearPlayerTankFragList();
			
			if (battleSaved) Log.BattleResultDoneLog();
			sw.Stop();
			TimeSpan ts = sw.Elapsed;
			// Log.LogToFile(log);
			return ("Dossier file succsessfully analyzed - time spent " + ts.Minutes + ":" + ts.Seconds + "." + ts.Milliseconds.ToString("000"));
		}

		static DateTime ConvertFromUnixTimestamp(double timestamp)
		{
			DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return origin.AddSeconds(timestamp);
		}

		// TODO: NOT IN USE - remove if OK (18.6.2014)
		//private static void UpdateNewPlayerTankRow(ref DataRow NewPlayerTankRow, JsonItem currentItem)
		//{
		//	JsonMainSection mainSection = new JsonMainSection(); 
		//	// OLD SECTION NO LONGER IN USE
		//	//if (currentItem.mainSection == mainSection.tanks)
		//	//{
		//	//	if (currentItem.subSection == "tankdata" && currentItem.property == "battlesCount") NewPlayerTankRow["battles15"] = Convert.ToInt32(currentItem.value);
		//	//	if (currentItem.subSection == "tankdata" && currentItem.property == "wins") NewPlayerTankRow["wins15"] = Convert.ToInt32(currentItem.value);
		//	//	if (currentItem.subSection == "tankdata" && currentItem.property == "wins") NewPlayerTankRow["wins15"] = Convert.ToInt32(currentItem.value);
		//	//}
		//	//else if (currentItem.mainSection == mainSection.tanks_v2)
		//	//{
		//		if (currentItem.subSection == "a15x15" && currentItem.property == "battlesCount") NewPlayerTankRow["battles15"] = Convert.ToInt32(currentItem.value);
		//		if (currentItem.subSection == "a15x15" && currentItem.property == "wins") NewPlayerTankRow["wins15"] = Convert.ToInt32(currentItem.value);
		//		if (currentItem.subSection == "a7x7" && currentItem.property == "battlesCount") NewPlayerTankRow["battles7"] = Convert.ToInt32(currentItem.value);
		//		if (currentItem.subSection == "a7x7" && currentItem.property == "wins") NewPlayerTankRow["wins7"] = Convert.ToInt32(currentItem.value);
		//	//}
		//}

		public static bool CheckTankDataResult(string tankName, 
												DataRow playerTankNewRow, 
												DataRow playerTankBattle15NewRow, 
												DataRow playerTankBattle7NewRow,
												DataRow playerTankBattleHistoricalNewRow, 
												string fragList, 
												List<AchItem> achList, 
												bool forceUpdate = false, 
												bool saveBattleResult = true )
		{
			// Get Tank ID
			bool battleSave = false; // Sets true if battle is saved, and is return value
			int tankId = TankData.GetTankID(tankName);
			if (tankId > 0) // when tankid=0 the tank is not found in tank table
			{
				// Get tank new battle count
				int playerTankNewRow_battles15 = 0;
				int playerTankNewRow_battles7 = 0;
				int playerTankNewRow_battlesHistorical = 0;
				if (playerTankBattle15NewRow["battles"] != DBNull.Value) playerTankNewRow_battles15 = Convert.ToInt32(playerTankBattle15NewRow["battles"]);
				if (playerTankBattle7NewRow["battles"] != DBNull.Value) playerTankNewRow_battles7 = Convert.ToInt32(playerTankBattle7NewRow["battles"]);
				if (playerTankBattleHistoricalNewRow["battles"] != DBNull.Value) playerTankNewRow_battlesHistorical = Convert.ToInt32(playerTankBattleHistoricalNewRow["battles"]);
				// Check if battle count has increased, get existing battle count
				DataTable playerTankOldTable = TankData.GetPlayerTank(tankId); // Return Existing Player Tank Data
				// Check if Player has this tank
				if (playerTankOldTable.Rows.Count == 0)
				{
					// New tank detected, this parts only run when new tank is detected
					SaveNewPlayerTank(tankId); // Save new tank
					playerTankOldTable = TankData.GetPlayerTank(tankId); // Get data into DataTable once more now after row is added
				}
				// Get the get existing (old) tank data row
				DataRow playerTankOldRow = playerTankOldTable.Rows[0];
				int playerTankId = Convert.ToInt32(playerTankOldTable.Rows[0]["id"]);
				// Get the old battle count
				int playerTankOldRow_wins15 = 0;
				int playerTankOldRow_wins7 = 0;
				int playerTankOldRow_winsHistorical = 0;
				int playerTankOldRow_xp15 = 0;
				int playerTankOldRow_xp7 = 0;
				int playerTankOldRow_xpHistorical = 0;
				int playerTankOldRow_battles15 = TankData.GetPlayerTankBattleCount(playerTankId, TankData.DossierBattleMode.Mode15, out playerTankOldRow_wins15, out playerTankOldRow_xp15);
				int playerTankOldRow_battles7 = TankData.GetPlayerTankBattleCount(playerTankId, TankData.DossierBattleMode.Mode7, out playerTankOldRow_wins7, out playerTankOldRow_xp7);
				int playerTankOldRow_battlesHistorical = TankData.GetPlayerTankBattleCount(playerTankId, TankData.DossierBattleMode.ModeHistorical, out playerTankOldRow_winsHistorical, out playerTankOldRow_xpHistorical); 
				
				// Calculate number of new battles 
				int battlesNew15 = playerTankNewRow_battles15 - playerTankOldRow_battles15;
				int battlesNew7 = playerTankNewRow_battles7 - playerTankOldRow_battles7;
				int battlesNewHistorical = playerTankNewRow_battlesHistorical - playerTankOldRow_battlesHistorical;
				// Check if new battle on this tank then do db update, if force do it anyway
				if (battlesNew15 != 0 || battlesNew7 != 0 || battlesNewHistorical != 0 ||
					(forceUpdate && (playerTankOldRow_battles15 != 0 || playerTankOldRow_battles7 != 0 || playerTankOldRow_battlesHistorical != 0)))
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
								case "String": sqlFields += "'" + playerTankNewRow[colName] + "'"; break;
								case "DateTime": sqlFields += "'" + Convert.ToDateTime(playerTankNewRow[colName]).ToString("yyyy-MM-dd HH:mm:ss") + "'"; break;
								default: sqlFields += playerTankNewRow[colName]; break;
							}
						}
					}
					// Check if new victory, then update playerTank.LastVictoryTime
					// Calculate number of new victories
					bool battleVictory = false;
					bool firstVictory = false;
					int victoryNew = 0;
					if (battlesNew15 > 0) victoryNew += Convert.ToInt32(playerTankBattle15NewRow["wins"]) - playerTankOldRow_wins15;
					if (battlesNew7 > 0) victoryNew += Convert.ToInt32(playerTankBattle7NewRow["wins"]) - playerTankOldRow_wins7;
					if (battlesNewHistorical > 0) victoryNew += Convert.ToInt32(playerTankBattleHistoricalNewRow["wins"]) - playerTankOldRow_winsHistorical;
					if (victoryNew > 0)
					{
						battleVictory = true;
						// Check if this is first victory by comparing to current value
						if (playerTankNewRow["lastVictoryTime"] == DBNull.Value)
							firstVictory = true; // first time last victory is recorded, assume first victory
						else
						{
							// Check if this victory is first one this day, first find date for last victory
							DateTime lastVictoryTime = Convert.ToDateTime(playerTankNewRow["lastVictoryTime"]);
							if (lastVictoryTime.Hour < 5) lastVictoryTime = lastVictoryTime.AddDays(-1); // correct date according to server reset 05:00
							lastVictoryTime = new DateTime(lastVictoryTime.Year, lastVictoryTime.Month, lastVictoryTime.Day, 0, 0, 0); // Remove time - just focus on date
							// Find date for this victory
							DateTime thisBattleTime = Convert.ToDateTime(playerTankNewRow["lastBattleTime"]);
							if (thisBattleTime.Hour < 5) thisBattleTime = thisBattleTime.AddDays(-1); // correct date according to server reset 05:00
							thisBattleTime = new DateTime(thisBattleTime.Year, thisBattleTime.Month, thisBattleTime.Day, 0, 0, 0); // Remove time - just focus on date
							// Now compare to se if this battle victory is not the same as last battle victory recorded - then it is first victory of today
							firstVictory = (lastVictoryTime != thisBattleTime);
						}
						if (firstVictory)
						{
							sqlFields += ", lastVictoryTime=";
							sqlFields += "'" + Convert.ToDateTime(playerTankNewRow["lastBattleTime"]).ToString("yyyy-MM-dd HH:mm:ss") + "'";
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
					
					// Check fraglist to update playertank frags
					List<FragItem> battleFragList = UpdatePlayerTankFrag(tankId, playerTankId, fragList);

					// Check if achivment exists
					List<AchItem> battleAchList = UpdatePlayerTankAch(tankId, playerTankId, achList);
									

					// If detected several battle modes, dont save fraglist and achivements to battle, as we don't know how to seperate them
					int severalModes = 0;
					if (battlesNew15 != 0) severalModes++;
					if (battlesNew7 != 0) severalModes++;
					if (battlesNewHistorical != 0) severalModes++;
					if (severalModes > 1)
					{
						battleFragList.Clear();
						battleAchList.Clear();
					}

					// Now update playerTank battle for different battle modes
					if (battlesNew15 > 0 || (forceUpdate && playerTankOldRow_battles15 != 0))
					{
						UpdatePlayerTankBattle(TankData.DossierBattleMode.Mode15, playerTankId, tankId, playerTankNewRow, playerTankOldRow, playerTankBattle15NewRow,
												playerTankNewRow_battles15, battlesNew15, battleFragList, battleAchList, saveBattleResult);
						battleSave = true;
					}
					if (battlesNew7 > 0 || (forceUpdate && playerTankOldRow_battles7 != 0))
					{
						UpdatePlayerTankBattle(TankData.DossierBattleMode.Mode7, playerTankId, tankId, playerTankNewRow, playerTankOldRow, playerTankBattle7NewRow,
												playerTankNewRow_battles7, battlesNew7, battleFragList, battleAchList, saveBattleResult);
						battleSave = true;
					}
					if (battlesNewHistorical > 0 || (forceUpdate && playerTankOldRow_battlesHistorical != 0))
					{
						UpdatePlayerTankBattle(TankData.DossierBattleMode.ModeHistorical, playerTankId, tankId, playerTankNewRow, playerTankOldRow, playerTankBattleHistoricalNewRow,
												playerTankNewRow_battlesHistorical, battlesNewHistorical, battleFragList, battleAchList, saveBattleResult);
						battleSave = true;
					}


					// Check if grinding
					if (Convert.ToInt32(playerTankOldRow["gGrindXP"]) > 0)
					{
						// Yes, apply grinding progress to playerTank now
						// Get grinding data
						string sql = "SELECT tank.name, gCurrentXP, gGrindXP, gGoalXP, gProgressXP, gBattlesDay, gComment, lastVictoryTime, " +
								"        SUM(playerTankBattle.battles) as battles, SUM(playerTankBattle.wins) as wins, " +
								"        MAX(playerTankBattle.maxXp) AS maxXP, SUM(playerTankBattle.xp) AS totalXP, " +
								"        SUM(playerTankBattle.xp / NULLIF(playerTankBattle.battles, 0) * playerTankBattle.battleOfTotal) AS avgXP " +
								"FROM    tank INNER JOIN " +
								"        playerTank ON tank.id = playerTank.tankId INNER JOIN " +
								"        playerTankBattle ON playerTank.id = playerTankBattle.playerTankId " +
								"WHERE  (playerTank.id = @playerTankId) " +
								"GROUP BY tank.name, gCurrentXP, gGrindXP, gGoalXP, gProgressXP, gBattlesDay, gComment, lastVictoryTime ";
						DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
						DataRow grinding = DB.FetchData(sql).Rows[0];
						// Calc new values
						// Get base XP for this battle
						int XP = 0;
						if (battlesNew15 > 0) XP += Convert.ToInt32(playerTankBattle15NewRow["xp"]) - playerTankOldRow_xp15;
						if (battlesNew7 > 0) XP += Convert.ToInt32(playerTankBattle7NewRow["xp"]) - playerTankOldRow_xp7;
						if (battlesNewHistorical > 0) XP += Convert.ToInt32(playerTankBattleHistoricalNewRow["xp"]) - playerTankOldRow_xpHistorical;
						if (battleVictory) // If victory, check if first victory this day, or if every victory has bonus
						{
							if (Code.GrindingHelper.Settings.EveryVictoryFactor > 0)
								XP = XP * Code.GrindingHelper.Settings.EveryVictoryFactor;
							else if (firstVictory)
								XP = XP * Code.GrindingHelper.Settings.FirstVictoryFactor;
						}
						// Get parameters for grinding calc
						int progress = Convert.ToInt32(grinding["gProgressXP"]) + XP; // Added XP to previous progress
						int grind = Convert.ToInt32(grinding["gGrindXP"]);
						int btlPerDay = Convert.ToInt32(grinding["gBattlesDay"]);
						// Calc values according to increased XP (progress)
						int progressPercent = GrindingHelper.CalcProgressPercent(grind, progress);
						int restXP = GrindingHelper.CalcProgressRestXP(grind, progress);
						int realAvgXP = GrindingHelper.CalcRealAvgXP(grinding["battles"].ToString(), grinding["wins"].ToString(), grinding["totalXP"].ToString(),
																	 grinding["avgXP"].ToString(), btlPerDay.ToString());
						int restBattles = GrindingHelper.CalcRestBattles(restXP, realAvgXP);
						int restDays = GrindingHelper.CalcRestDays(restXP, realAvgXP, btlPerDay);
						// Save to playerTank
						sql = "UPDATE playerTank SET gProgressXP=@ProgressXP, gRestXP=@RestXP, gProgressPercent=@ProgressPercent, " +
							  "					     gRestBattles=@RestBattles, gRestDays=@RestDays  " +
							  "WHERE id=@id; ";
						DB.AddWithValue(ref sql, "@ProgressXP", progress, DB.SqlDataType.Int);
						DB.AddWithValue(ref sql, "@RestXP", restXP, DB.SqlDataType.Int);
						DB.AddWithValue(ref sql, "@ProgressPercent", progressPercent, DB.SqlDataType.Int);
						DB.AddWithValue(ref sql, "@RestBattles", restBattles, DB.SqlDataType.Int);
						DB.AddWithValue(ref sql, "@RestDays", restDays, DB.SqlDataType.Int);
						DB.AddWithValue(ref sql, "@id", playerTankId, DB.SqlDataType.Int);
						DB.ExecuteNonQuery(sql);
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
						DataRow[] lookupAch = TankData.achList.Select(expression);
						
						//string sql = "SELECT id FROM ach WHERE name=@achName; ";
						//DB.AddWithValue(ref sql, "@achName", newAch.achName, DB.SqlDataType.VarChar);
						//DataTable lookupAch = DB.FetchData(sql);
						if (lookupAch.Length > 0)
						{
							// Found ach, get id now
							int achId = Convert.ToInt32(lookupAch[0]["id"]);
							// Find the current achievent
							expression = "playerTankId=" + playerTankId.ToString() + " AND achId=" + achId.ToString();
							DataRow[] lookupPlayerTankAch = TankData.playerTankAchList.Select(expression);
						
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

		private static void UpdatePlayerTankBattle(TankData.DossierBattleMode battleMode, 
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
			DataTable playerTankBattleOld = TankData.GetPlayerTankBattle(playerTankId, battleMode, true); 
			// Update playerTankBattle
			string sqlFields = "";
			// Calculate WN8
			sqlFields += "wn8=" + Rating.CalculatePlayerTankWn8(tankId, playerTankNewRow_battles, playerTankBattleNewRow);
			// Calculate Eff
			sqlFields += ", eff=" + Rating.CalculatePlayerTankEff(tankId, playerTankNewRow_battles, playerTankBattleNewRow);
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
						case "String": sqlFields += "'" + playerTankBattleNewRow[colName] + "'"; break;
						case "DateTime": sqlFields += "'" + Convert.ToDateTime(playerTankBattleNewRow[colName]).ToString("yyyy-MM-dd HH:mm:ss") + "'"; break;
						default: sqlFields += playerTankBattleNewRow[colName]; break;
					}
				}
			}
			// Update database
			if (sqlFields.Length > 0)
			{
				string sql = "UPDATE playerTankBattle SET " + sqlFields + " WHERE playerTankId=@playerTankId AND battleMode=@battleMode; ";
				DB.AddWithValue(ref sql, "@playerTankId", playerTankId, DB.SqlDataType.Int);
				DB.AddWithValue(ref sql, "@battleMode", TankData.DbBattleMode(battleMode), DB.SqlDataType.VarChar);
				DB.ExecuteNonQuery(sql);
			}
			// Add battle, if any and not first run - then avoid
			if (saveBattleResult && battlesNew > 0)
				AddBattle(playerTankNewRow, playerTankOldRow, playerTankBattleNewRow, playerTankBattleOld.Rows[0], battleMode, tankId, playerTankId, battlesNew, battleFragList, battleAchList);
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
				// Get frags for this tank

				string expression = "PlayerTankTankId=" + tankId.ToString();
				DataRow[] lookupPlayerFrag = TankData.playerTankFragList.Select(expression);

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
			//}
			//catch (Exception ex)
			//{
			//	string s = ex.Message;
			//	throw;
			//}
			
			return battleFrag;
		}

		private static void AddBattle(DataRow playerTankNewRow, 
									     DataRow playerTankOldRow, 
										 DataRow playerTankBattleNewRow, 
										 DataRow playerTankBattleOldRow,
										 TankData.DossierBattleMode battleMode, 
										 int tankId, 
										 int playerTankId, 
										 int battlesCount, 
										 List<FragItem> battleFragList, 
										 List<AchItem> battleAchList)
		{
			//try
			//{
				// Create datarow to put calculated battle data
				DataTable battleTableNew = TankData.GetBattle(-1); // Return no data, only empty database with structure
				DataRow battleNewRow = battleTableNew.NewRow();
				foreach (DataRow dr in TankData.GetTankData2BattleMapping(battleMode).Rows)
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
							case "String": sqlValues += ", '" + battleNewRow[colName] + "'"; break;
							case "DateTime": sqlValues += ", '" + Convert.ToDateTime(battleNewRow[colName]).ToString("yyyy-MM-dd HH:mm:ss") + "'"; break;
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
				// Calculate WN8
				sqlFields += ", wn8";
				sqlValues += ", " + Rating.CalculateBattleWn8(tankId, battlesCount, battleNewRow);
				// Calc Eff
				sqlFields += ", eff";
				sqlValues += ", " + Rating.CalculateBattleEff(tankId, battlesCount, battleNewRow);
				// Add battle mode
				sqlFields += ", battleMode";
				sqlValues += ", @battleMode";
				DB.AddWithValue(ref sqlValues, "@battleMode", TankData.DbBattleMode(battleMode), DB.SqlDataType.VarChar); 
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
