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
			bool battleSave = false;

			// Check for first run (if player tank = 0), then dont get battle result
			bool saveBattleResult = (TankData.GetPlayerTankCount() > 0);

			// Declare
			DataTable NewPlayerTankTable = TankData.GetPlayerTankFromDB(-1); // Return no data, only empty database with structure
			DataRow NewPlayerTankRow = NewPlayerTankTable.NewRow();
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
								log.Add("  > Check for DB update - Tank: '" + tankName + " | battles15:" + NewPlayerTankRow["battles15"] + " | battles7:" + NewPlayerTankRow["battles7"]);
								if (SaveTankDataResult(tankName, NewPlayerTankRow, fragList, achList, ForceUpdate, saveBattleResult)) battleSave = true;
							}
							// Reset all values
							NewPlayerTankTable.Clear();
							NewPlayerTankRow = NewPlayerTankTable.NewRow();
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
												switch (dataType)
												{
													case "String": NewPlayerTankRow[dbField] = currentItem.value.ToString(); break;
													case "DateTime": NewPlayerTankRow[dbField] = ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); break;
													case "Int": NewPlayerTankRow[dbField] = Convert.ToInt32(currentItem.value); break;
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
			log.Add("  > Check for DB update - Tank: '" + tankName + " | battles15:" + NewPlayerTankRow["battles15"] + " | battles7:" + NewPlayerTankRow["battles7"]);
			if (SaveTankDataResult(tankName, NewPlayerTankRow, fragList, achList, ForceUpdate, saveBattleResult)) battleSave = true;
			// Done
			if (battleSave) Log.BattleResultDoneLog();
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

		public static bool SaveTankDataResult(string tankName, DataRow NewPlayerTankRow, string fragList, List<AchItem> achList, bool ForceUpdate = false, bool saveBattleResult = true )
		{
			// Get Tank ID
			bool battleSave = false;
			int tankId = TankData.GetTankID(tankName);
			if (tankId > 0) // when tankid=0 the tank is not found in tank table
			{
				// Check if battle count has increased, first get existing battle count
				DataTable OldPlayerTankTable = TankData.GetPlayerTankFromDB(tankId); // Return Existing Player Tank Data
				// Check if Player has this tank
				if (OldPlayerTankTable.Rows.Count == 0)
				{
					SaveNewPlayerTank(tankId);
					OldPlayerTankTable = TankData.GetPlayerTankFromDB(tankId); // Return once more now after row is added
				}
				// Check if battle count has increased, first get existing (old) tank data
				DataRow OldPlayerTankRow = OldPlayerTankTable.Rows[0];
				int playerTankId = Convert.ToInt32(OldPlayerTankTable.Rows[0]["id"]);
				// Compare with last battle result
				int NewPlayerTankRow_battles15 = 0;
				int NewPlayerTankRow_battles7 = 0;
				if (NewPlayerTankRow["battles15"] != DBNull.Value) NewPlayerTankRow_battles15 = Convert.ToInt32(NewPlayerTankRow["battles15"]);
				if (NewPlayerTankRow["battles7"] != DBNull.Value) NewPlayerTankRow_battles7 = Convert.ToInt32(NewPlayerTankRow["battles7"]);
				int battlessNew15 = NewPlayerTankRow_battles15 - Convert.ToInt32(OldPlayerTankRow["battles15"]);
				int battlessNew7 = NewPlayerTankRow_battles7 - Convert.ToInt32(OldPlayerTankRow["battles7"]);
				// Check if new battle on this tank then do db update, if force do it anyway
				if (battlessNew15 != 0 || battlessNew7 != 0 || ForceUpdate)
				{
					// New battle detected, update tankData in DB
					UpdatePlayerTank(NewPlayerTankRow, OldPlayerTankTable, tankId, NewPlayerTankRow_battles15, NewPlayerTankRow_battles7);
					// Check fraglist to update playertank frags
					List<FragItem> battleFragList = UpdatePlayerTankFrag(tankId, playerTankId, fragList);
					// Check if achivment exists
					List<AchItem> battleAchList = UpdatePlayerTankAch(tankId, playerTankId, achList);
					// If new battle on this tank also update battle table to store result of last battle(s)
					if (saveBattleResult)
					{
						if (battlessNew15 != 0 || battlessNew7 != 0)
						{
							// New battle detected, update tankData in DB
							UpdateBattle(NewPlayerTankRow, OldPlayerTankTable, tankId, battlessNew15, battlessNew7, battleFragList, battleAchList);
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
			string sql = "INSERT INTO PlayerTank (tankId, playerId) VALUES (@tankId, @playerId)";
			db.AddWithValue(ref sql, "@tankId", TankID, db.SqlDataType.Int);
			db.AddWithValue(ref sql, "@playerId", Config.Settings.playerId, db.SqlDataType.Int);
			db.ExecuteNonQuery(sql);
		}

		private static void UpdatePlayerTank(DataRow NewPlayerTankRow, DataTable OldPlayerTankTable, int tankId, int battleCount15, int battleCount7)
		{
			// Get fields to update
			string sqlFields = "";
			// Calculate WN8
			sqlFields += "wn8=" + Rating.CalculatePlayerTankWn8(tankId, battleCount15 , Rating.BattleMode.Random15, NewPlayerTankRow);
			// Calculate Eff
			sqlFields += ", eff=" + Rating.CalculatePlayerTankEff(tankId, battleCount15 , Rating.BattleMode.Random15, NewPlayerTankRow);
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
			if (sqlFields.Length > 0)
			{
				string sql = "UPDATE playerTank SET " + sqlFields + " WHERE Id=@Id ";
				db.AddWithValue(ref sql, "@Id", OldPlayerTankTable.Rows[0]["id"], db.SqlDataType.Int);
				db.ExecuteNonQuery(sql);
			}
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
									"WHERE playerTankId=" + playerTankId + " AND ach.name='" + newAch.achName + "'";
						DataTable currentAch = db.FetchData(sql);
						if (currentAch.Rows.Count == 0) // new achievment
						{
							// Get AchId 
							// TODO : improve
							sql = "SELECT id " +
									"FROM ach " +
									"WHERE name='" + newAch.achName + "'";
							DataTable lookupAch = db.FetchData(sql);
							int achId = Convert.ToInt32(lookupAch.Rows[0]["id"]);
							// Insert new acheivement
							sql = "INSERT INTO playerTankAch (achCount, playerTankId, achId) " +
									"VALUES (@achCount, @playerTankId, @achId)";
							db.AddWithValue(ref sql, "@achCount", newAch.count, db.SqlDataType.Int);
							db.AddWithValue(ref sql, "@playerTankId", playerTankId, db.SqlDataType.Int);
							db.AddWithValue(ref sql, "@achId", achId, db.SqlDataType.Int);
							db.ExecuteNonQuery(sql);
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
										"WHERE playerTankId=@playerTankId AND achId=@achId";
								db.AddWithValue(ref sql, "@achCount", newAch.count, db.SqlDataType.Int);
								db.AddWithValue(ref sql, "@playerTankId", playerTankId, db.SqlDataType.Int);
								db.AddWithValue(ref sql, "@achId", achId, db.SqlDataType.Int);
								db.ExecuteNonQuery(sql);
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
					"WHERE playerTank.tankId=@tankId";
				db.AddWithValue(ref sql, "@tankId", tankId, db.SqlDataType.Int);
				DataTable dt = db.FetchData(sql);
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
				foreach (var newFragItem in newFrag)
				{
					// Check if frags exists for this fragged tank
					int i = -1;
					bool foundFraggedTank = false;
					while (i < oldFrag.Count - 1 && !foundFraggedTank)
					{
						i++;
						foundFraggedTank = (oldFrag[i].tankId == newFragItem.tankId);
					}
					if (foundFraggedTank)
					{
						// fragged tank exsist, check if frag count has increased
						if (newFragItem.fragCount > oldFrag[i].fragCount)
						{
							// new frag on existing fragged tank, update
							playerTankFragSQL += "UPDATE playerTankFrag " +
												"SET fragCount = " + newFragItem.fragCount.ToString() +
												"WHERE playerTankId=" + oldFrag[i].playerTankId +
												"  AND fraggedTankId=" + newFragItem.tankId + "; \n";
							// Add new frag count to battle Frag
							newFragItem.fragCount = newFragItem.fragCount - oldFrag[i].fragCount;
							battleFrag.Add(newFragItem);
						}
					}
					else
					{
						// new fragged tank, insert
						playerTankFragSQL += "INSERT INTO playerTankFrag (playerTankId, fraggedTankId, fragCount) " +
											"VALUES (" + playerTankId + ", " + newFragItem.tankId + ", " + newFragItem.fragCount.ToString() + "); \n";
						// Add new frag count to battle Frag
						battleFrag.Add(newFragItem);
					}
				}
				// Add to database
				if (playerTankFragSQL != "")
				{
					db.ExecuteNonQuery(playerTankFragSQL);
				}
			//}
			//catch (Exception ex)
			//{
			//	string s = ex.Message;
			//	throw;
			//}
			
			return battleFrag;
		}

		private static void UpdateBattle(DataRow NewPlayerTankRow, DataTable OldPlayerTankTable, int tankId, int battlessNew15, int battlessNew7, List<FragItem> battleFragList, List<AchItem> battleAchList)
		{
			//try
			//{
				// Create datarow to put calculated battle data
				DataTable NewBattleTable = TankData.GetBattleFromDB(-1); // Return no data, only empty database with structure
				DataRow NewbattleRow = NewBattleTable.NewRow();
				// Get fields to map playerTank data to Battle data
				bool modeCompany = false;
				bool modeClan = false;
				int battlesCount = (battlessNew15 + battlessNew7);
				foreach (DataRow dr in TankData.tankData2BattleMapping.Rows)
				{
					if (dr["dbBattle"] != DBNull.Value) // Skip reading value if fields not mapped 
					{
						// Get field to be checked
						string battleField = dr["dbBattle"].ToString();
						string playerTankField = dr["dbPlayerTank"].ToString();
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
							if (OldPlayerTankTable.Rows[0][playerTankField] != DBNull.Value) oldvalue = Convert.ToInt32(OldPlayerTankTable.Rows[0][playerTankField]);
							NewbattleRow[battleField] = (Convert.ToInt32(NewbattleRow[battleField]) + newvalue - oldvalue);
						}
					}
					else // Check in unmapped fields 
					{
						// Get field to be checked
						string playerTankField = dr["dbPlayerTank"].ToString();
						// Calculate clan and company battle mode
						if (playerTankField == "battlesClan" || playerTankField == "battlesCompany") // For DateTime get the new value
						{
							// Calculate difference from old to new playertank result
							int oldvalue = 0;
							int newvalue = 0;
							if (NewPlayerTankRow[playerTankField] != DBNull.Value) newvalue = Convert.ToInt32(NewPlayerTankRow[playerTankField]);
							if (OldPlayerTankTable.Rows[0][playerTankField] != DBNull.Value) oldvalue = Convert.ToInt32(OldPlayerTankTable.Rows[0][playerTankField]);
							if (newvalue > oldvalue)
							{
								modeClan = (playerTankField == "battlesClan");
								modeCompany = (playerTankField == "battlesCompany");
							}
						}
					}
				}
				// Get value to playerTankID, FK to parent table playerTank
				DataTable dt = TankData.GetPlayerTankFromDB(tankId);
				// Create SQl to insert new battle row
				// First add player id
				int playerTankId = Convert.ToInt32(dt.Rows[0]["Id"]);
				string sqlFields = ""; // string sqlFields = "playerTankId"; 
				string sqlValues = ""; // string sqlValues = dt.Rows[0]["Id"].ToString();
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
				if (battlessNew15 != 0) { sqlFields += ", mode15"; sqlValues += ", 1"; }
				if (battlessNew7 != 0) { sqlFields += ", mode7"; sqlValues += ", 1"; }
				if (modeCompany) { sqlFields += ", modeCompany"; sqlValues += ", 1"; }
				if (modeClan) { sqlFields += ", modeClan"; sqlValues += ", 1"; }
				
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
					db.AddWithValue(ref sql, "@playerTankId", playerTankId, db.SqlDataType.Int);
					db.ExecuteNonQuery(sql);
					// Get the last battle id
					int battleId = 0;
					sql = "select max(id) as battleId from battle";
					dt = db.FetchData(sql);
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
													"VALUES (" + battleId + ", " + newFragItem.tankId + ", " + newFragItem.fragCount.ToString() + "); \n";
						}
						// Add to database
						db.ExecuteNonQuery(battleFragSQL);
					}
					// Insert battle achievments
					if (battleAchList.Count > 0)
					{
						// Loop through new frags
						string battleAchSQL = "";
						foreach (var newAchItem in battleAchList)
						{
							battleAchSQL += "INSERT INTO battleAch (battleId, achId, achCount) " +
													"VALUES (" + battleId + ", " + newAchItem.achId.ToString() + ", " + newAchItem.count.ToString() + "); \n";
						}
						// Add to database
						db.ExecuteNonQuery(battleAchSQL);
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
