﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;
using System.Data.SqlClient;

namespace WotDBUpdater
{
    class dossier2db
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
        
        public static String readJson(string filename, bool ForceUpdate = false)
        {
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(filename))
            {
                String line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);
                }
            }

            string json = sb.ToString();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            // read json string
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
                        
            // logging
            List<string> log = new List<string>();

            // Declare
            DataTable NewUserTankTable = tankData.GetUserTankFromDB(-1); // Return no data, only empty database with structure
            DataRow NewUserTankRow = NewUserTankTable.NewRow();
            string tankName = "";
            //TankDataResult tdr = new TankDataResult();

            JsonMainSection mainSection = new JsonMainSection();
            JsonItem currentItem = new JsonItem();
            
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
                                log.Add("  > Check for DB update - Tank: '" + tankName + " | battles15:" + NewUserTankRow["battles15"] + " | battles7:" + NewUserTankRow["battles7"]);
                                SaveTankDataResult(tankName, NewUserTankRow, ForceUpdate);
                            }
                            // Reset all values
                            NewUserTankTable.Clear();
                            NewUserTankRow = NewUserTankTable.NewRow();
                            // Get new tank name
                            currentItem.tank = reader.Value.ToString(); // add to current item
                            tankName = reader.Value.ToString(); // add to current tank
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

                                        // Check data by getting jsonUserTank Mapping
                                        string expression = "jsonMainSubProperty='" + currentItem.mainSection + "." + currentItem.subSection + "." + currentItem.property + "'";
                                        DataRow[] foundRows = tankData.json2dbMappingView.Select(expression);

                                        // IF mapping found add currentItem into NewUserTankRow
                                        if (foundRows.Length != 0)
                                        {
                                            // Add now
                                            string dataType = foundRows[0]["dbDataType"].ToString();
                                            string dbField = foundRows[0]["dbUserTank"].ToString();
                                            switch (dataType)
                                            {
                                                case "String": NewUserTankRow[dbField] = currentItem.value.ToString(); ; break;
                                                case "DateTime": NewUserTankRow[dbField] = ConvertFromUnixTimestamp(Convert.ToDouble(currentItem.value)); ; break;
                                                case "Int": NewUserTankRow[dbField] = Convert.ToInt32(currentItem.value); ; break;
                                            }
                                        }

                                        // Temp log all data
                                        //log.Add("  " + currentItem.mainSection + "." + currentItem.tank + "." + currentItem.subSection + "." + currentItem.property + ":" + currentItem.value);
                                        //log.Add("  " + currentItem.mainSection + "." + currentItem.subSection + "." + currentItem.property );
                                    }
                                }
                            }
                        }
                    }
                }
            }
            reader.Close();
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            Log.LogToFile(log);
            return (" > Time spent analyzing file: " + ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds.ToString("000"));
        }

        static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }   

        private static void UpdateNewUserTankRow(ref DataRow NewUserTankRow, JsonItem currentItem)
        {
            JsonMainSection mainSection = new JsonMainSection(); 
            if (currentItem.mainSection == mainSection.tanks)
            {
                if (currentItem.subSection == "tankdata" && currentItem.property == "battlesCount") NewUserTankRow["battles15"] = Convert.ToInt32(currentItem.value);
                if (currentItem.subSection == "tankdata" && currentItem.property == "wins") NewUserTankRow["wins15"] = Convert.ToInt32(currentItem.value);
                if (currentItem.subSection == "tankdata" && currentItem.property == "wins") NewUserTankRow["wins15"] = Convert.ToInt32(currentItem.value);
            }
            else if (currentItem.mainSection == mainSection.tanks_v2)
            {
                if (currentItem.subSection == "a15x15" && currentItem.property == "battlesCount") NewUserTankRow["battles15"] = Convert.ToInt32(currentItem.value);
                if (currentItem.subSection == "a15x15" && currentItem.property == "wins") NewUserTankRow["wins15"] = Convert.ToInt32(currentItem.value);
                if (currentItem.subSection == "a7x7" && currentItem.property == "battlesCount") NewUserTankRow["battles7"] = Convert.ToInt32(currentItem.value);
                if (currentItem.subSection == "a7x7" && currentItem.property == "wins") NewUserTankRow["wins7"] = Convert.ToInt32(currentItem.value);
            }
        }

        // TODO: Check if using this model gives better perfomance and code than readJson
        private static void readJson_ver2(string filename, bool ForceUpdate = false)
        {
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(filename))
            {
                String line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);
                }
            }

            string json = sb.ToString();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            JToken rootToken;
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            JObject fileContent = JObject.Parse(json);
            rootToken = fileContent.First;
            if (((JProperty)rootToken).Name.ToString() == "status" && ((JProperty)rootToken).Value.ToString() == "ok")
            {
                rootToken = rootToken.Next.Next;
                rootToken = rootToken.Next;
                List<string> logtxt = new List<string>();
                JToken turrets = rootToken.Children().First();
                foreach (JProperty turretItem in turrets.Children())
                {
                    JToken t = turretItem.First();
                    String result = t["name"].ToString();
                    result += " | " + t["nation_i18n"].ToString();
                    result += " | " + t["armor_fedd"].ToString();
                    result += " | " + t["circular_vision_radius"].ToString();
                    result += " | " + t["weight"].ToString();
                    result += " | " + t["name"].ToString();
                    JArray tanksArray = (JArray)t["tanks"];
                    result += " | " + tanksArray[0].ToString();
                    logtxt.Add(result);
                }
                Log.LogToFile(logtxt);

            }
        }

        public static void SaveTankDataResult(string tankName, DataRow NewUserTankRow, bool ForceUpdate = false)
        {
            // Get Tank ID
            int tankID = tankData.GetTankID(tankName);
            if (tankID > 0) // when tankid=0 the tank is not found in tank table
            {
                // Check if battle count has increased, first get existing battle count
                DataTable OldUserTankTable = tankData.GetUserTankFromDB(tankID); // Return Existing User Tank Data
                // Check if user has this tank
                if (OldUserTankTable.Rows.Count == 0)
                {
                    SaveNewUserTank(tankID);
                    OldUserTankTable = tankData.GetUserTankFromDB(tankID); // Return once more now after row is added
                }
                // Check if battle count has increased, first get existing (old) tank data
                DataRow OldUserTankRow = OldUserTankTable.Rows[0];
                // Compare with last battle result
                int NewUserTankRow_battles15 = 0;
                int NewUserTankRow_battles7 = 0;
                if (NewUserTankRow["battles15"] != DBNull.Value) NewUserTankRow_battles15 = Convert.ToInt32(NewUserTankRow["battles15"]);
                if (NewUserTankRow["battles7"] != DBNull.Value) NewUserTankRow_battles7 = Convert.ToInt32(NewUserTankRow["battles7"]);
                int battlessNew15 = NewUserTankRow_battles15 - Convert.ToInt32(OldUserTankRow["battles15"]);
                int battlessNew7 = NewUserTankRow_battles7 - Convert.ToInt32(OldUserTankRow["battles7"]);
                // Check if new battle on this tank then do db update, if force do it anyway
                if (battlessNew15 != 0 || battlessNew7 != 0 || ForceUpdate)
                {
                    // New battle detected, update tankData in DB
                    UpdateUserTank(NewUserTankRow, OldUserTankTable);
                    // If new battle on this tank also update battle table to store result of last battle(s)
                    if (battlessNew15 != 0 || battlessNew7 != 0)
                    {
                        // New battle detected, update tankData in DB
                        UpdateBattle(NewUserTankRow, OldUserTankTable, tankID, battlessNew15, battlessNew7);
                    }
                }
            }
        }

        private static void SaveNewUserTank(int TankID)
        {
            // Add to database
            SqlConnection con = new SqlConnection(Config.Settings.DatabaseConn);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO userTank (tankId, wotUserId) VALUES (@tankId, @wotUserId)", con);
            cmd.Parameters.AddWithValue("@tankId", TankID);
            cmd.Parameters.AddWithValue("@wotUserId", Config.Settings.UserID);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private static void UpdateUserTank(DataRow NewUserTankRow, DataTable OldUserTankTable)
        {
            // Get fields to update
            string sqlFields = "";
            foreach (DataColumn column in OldUserTankTable.Columns)
            {
                if (column.ColumnName != "userTankId" && NewUserTankRow[column.ColumnName] != DBNull.Value) // avoid the PK and if new data is NULL 
                {
                    if (sqlFields.Length > 0) sqlFields += ", "; // Add comma exept for first element
                    string colName = column.ColumnName;
                    string colType = column.DataType.Name;
                    sqlFields += colName + "=";
                    switch (colType)
                    {
                        case "String": sqlFields += "'" + NewUserTankRow[colName] + "'"; break;
                        case "DateTime": sqlFields += "'" + Convert.ToDateTime(NewUserTankRow[colName]).ToString("yyyy-MM-dd HH:mm:ss") + "'"; break;
                        default: sqlFields += NewUserTankRow[colName]; break;
                    }
                }
            }
            // Update database
            if (sqlFields.Length > 0)
            {
                SqlConnection con = new SqlConnection(Config.Settings.DatabaseConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE userTank SET " + sqlFields + " WHERE userTankId=@userTankId ", con);
                cmd.Parameters.AddWithValue("@userTankId", OldUserTankTable.Rows[0]["userTankId"]);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private static void UpdateBattle(DataRow NewUserTankRow, DataTable OldUserTankTable, int tankID, int battlessNew15, int battlessNew7)
        {
            // Greate datarow to put calculated battle data
            DataTable NewBattleTable = tankData.GetBattleFromDB(-1); // Return no data, only empty database with structure
            DataRow NewbattleRow = NewBattleTable.NewRow();            
            // Get fields to map UserTank data to Battle data
            bool modeCompany = false;
            bool modeClan = false;
            foreach (DataRow dr in tankData.tankData2BattleMappingView.Rows)
            {
                if (dr["dbBattle"] != DBNull.Value) // Skip reading value if fields not mapped 
                {
                    // Get field to be checked
                    string battleField = dr["dbBattle"].ToString();
                    string userTankField = dr["dbUserTank"].ToString();
                    // Check datatype and calculate value
                    if (dr["dbDataType"].ToString() == "DateTime") // For DateTime get the new value
                    {
                        NewbattleRow[battleField] = NewUserTankRow[userTankField];
                    }
                    else // For integers calculate new value as diff between new and old value
                    {
                        // Calculate difference from old to new usertank result
                        if (NewbattleRow[battleField] == DBNull.Value) NewbattleRow[battleField] = 0;
                        int oldvalue = 0;
                        int newvalue = 0;
                        if (NewUserTankRow[userTankField] != DBNull.Value) newvalue = Convert.ToInt32(NewUserTankRow[userTankField]);
                        if (OldUserTankTable.Rows[0][userTankField] != DBNull.Value) oldvalue = Convert.ToInt32(OldUserTankTable.Rows[0][userTankField]);
                        NewbattleRow[battleField] = Convert.ToInt32(NewbattleRow[battleField]) + newvalue - oldvalue;
                    }
                }
                else // Check in unmapped fields for calculate clan and company battle mode
                {
                    // Get field to be checked
                    string userTankField = dr["dbUserTank"].ToString();
                    // Check datatype and calculate value
                    if (userTankField == "battlesClan" || userTankField == "battlesCompany") // For DateTime get the new value
                    {
                        // Calculate difference from old to new usertank result
                        int oldvalue = 0;
                        int newvalue = 0;
                        if (NewUserTankRow[userTankField] != DBNull.Value) newvalue = Convert.ToInt32(NewUserTankRow[userTankField]);
                        if (OldUserTankTable.Rows[0][userTankField] != DBNull.Value) oldvalue = Convert.ToInt32(OldUserTankTable.Rows[0][userTankField]);
                        if (newvalue > oldvalue)
                        {
                            modeClan = (userTankField == "battlesClan");
                            modeCompany = (userTankField == "battlesCompany");
                        }
                    }
                }
            }
            // Get value to userTankID, FK to parent table userTank
            DataTable dt = tankData.GetUserTankFromDB(tankID);
            string sqlFields = "userTankID";
            string sqlValues = dt.Rows[0]["userTankID"].ToString();
            // Get fields to update, loop through mapping table to get allgenerate SQL
            foreach (DataColumn column in NewBattleTable.Columns)
            {
                if (column.ColumnName != "battleId" && column.ColumnName != "wotUserId" && NewbattleRow[column.ColumnName] != DBNull.Value) // avoid the PK and if new data is NULL 
                {
                    string colName = column.ColumnName;
                    string colType = column.DataType.Name;
                    sqlFields += ", " + colName;
                    switch (colType)
                    {
                        case "String": sqlValues += ", '" + NewbattleRow[colName] + "'"; break;
                        case "DateTime": sqlValues += ", '" + Convert.ToDateTime(NewbattleRow[colName]).ToString("yyyy-MM-dd HH:mm:ss") + "'"; break;
                        default: sqlValues += ", " + NewbattleRow[colName]; break;
                    }
                }
            }
            // Add battle mode
            if (battlessNew15 != 0) { sqlFields += ", mode15"; sqlValues += ", 1"; }
            if (battlessNew7 != 0) { sqlFields += ", mode7"; sqlValues += ", 1"; }
            if (modeCompany) { sqlFields += ", modeCompany"; sqlValues += ", 1"; }
            if (modeClan) { sqlFields += ", modeClan"; sqlValues += ", 1"; }
            // Update database
            if (sqlFields.Length > 0)
            {
                SqlConnection con = new SqlConnection(Config.Settings.DatabaseConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO battle (" + sqlFields + ") VALUES (" + sqlValues + ")", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

    }
}