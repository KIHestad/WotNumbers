using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using WinApp.Code;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace WinApp.Services
{
    public class AppBattleUpload
	{
        public async Task<string> RunForBattles(List<int> battleList)
        {
            if (battleList.Count > 0)
            {
                string battleIds = string.Join(",", battleList.Select(n => n.ToString()).ToArray());
                return await Run(true, battleIds);
            }
            else
            {
                return "No battles for upload";
            }
        }

        public async Task<string> Run(bool updateExisting, string battleIds = null)
        {
            try
            {
                // Get player
                DataTable dtPlayer = DB.FetchData($"SELECT playerApiId, playerApiToken FROM player WHERE Id={Config.Settings.playerId};");
                int playerId = Convert.ToInt32(dtPlayer.Rows[0]["playerApiId"]);
                Guid playerToken = Guid.Parse(dtPlayer.Rows[0]["playerApiToken"].ToString());
                // Get battles for transfer
                List<Models.AppBattleUploadModels.AppBattle> battles = new List<Models.AppBattleUploadModels.AppBattle>();
                string sql = "";
                if (updateExisting) // taks longer time, updates all battles even if they have been transferred previously
                    sql = @"
                        SELECT b.*, pt.tankId
                        FROM battle b INNER JOIN playerTank pt ON b.playerTankId = pt.Id
                        WHERE pt.playerId = " + Config.Settings.playerId + @"
                        ORDER BY b.id DESC; ";
                else // normal method, transfer all non-transferred
                    sql = @"
                        SELECT b.*, pt.tankId
                        FROM battle b INNER JOIN playerTank pt ON b.playerTankId = pt.Id
                        WHERE transferred = 0 AND pt.playerId = " + Config.Settings.playerId + @"
                        ORDER BY b.id DESC; ";
                if (battleIds != null) // special for transferring spesific battles, run from battle fetch to add details info
                {
                    sql = @"
                        SELECT b.*, pt.tankId
                        FROM battle b INNER JOIN playerTank pt ON b.playerTankId = pt.Id
                        WHERE b.Id IN (" + battleIds + @")
                        ORDER BY b.id DESC; ";
                }
                DataTable dtBattle = DB.FetchData(sql);
                // Loop through, find result
                int uploadSuccess = 0;
                int uploadTotal = 0;
                foreach (DataRow dr in dtBattle.Rows)
                {
                    int battleId = Convert.ToInt32(dr["id"]);
                    string debugField = "";
                    try
                    {
                        // Get
                        debugField = "battlemode (convert to byte)";
                        byte battleModeId = 15;
                        switch (dr["battleMode"].ToString())
                        {
                            case "15": battleModeId = 1; break;
                            case "7": battleModeId = 2; break;
                            case "7Ranked": battleModeId = 3; break;
                            case "GlobalMap": battleModeId = 4; break;
                            case "Special": battleModeId = 5; break;
                            case "Historical": battleModeId = 6; break;
                            case "Grand": battleModeId = 7; break;
                            case "Stronghold": battleModeId = 8; break;
                            case "Skirmishes": battleModeId = 9; break;
                        }
                        // Create battle dataset
                        Models.AppBattleUploadModels.AppBattle b = new Models.AppBattleUploadModels.AppBattle();
                        debugField = "playerId";
                        b.PlayerId = playerId;
                        debugField = "id";
                        b.PlayerBattleAppId = battleId;
                        debugField = "battleTime";
                        b.BattleTime = Convert.ToDateTime(dr["battleTime"]);
                        debugField = "battlemode";
                        b.BattleModeId = battleModeId;
                        debugField = "battleResultId";
                        b.BattleResultId = Convert.ToByte(dr["battleResultId"]);
                        debugField = "battleSurviveId";
                        b.BattleSurviveId = Convert.ToByte(dr["battleSurviveId"]);
                        debugField = "tankId";
                        b.TankId = Convert.ToInt32(dr["tankId"]);
                        debugField = "frags";
                        b.Frags = Convert.ToByte(dr["frags"]);
                        debugField = "dmg";
                        b.Dmg = Convert.ToInt16(dr["dmg"]);
                        debugField = "dmgReceived";
                        b.DmgReceived = Convert.ToInt16(dr["dmgReceived"]);
                        debugField = "cap";
                        b.Cap = Convert.ToByte(dr["cap"]);
                        debugField = "def";
                        b.Def = Convert.ToByte(dr["def"]);
                        debugField = "shots";
                        b.Shots = Convert.ToInt16(dr["shots"]);
                        debugField = "hits";
                        b.Hits = Convert.ToInt16(dr["hits"]);
                        debugField = "spotted";
                        b.Spotted = Convert.ToByte(dr["spotted"]);
                        debugField = "xp";
                        b.Xp = Convert.ToInt16(dr["xp"]);
                        debugField = "xpOriginal";
                        b.XpOriginal = Convert.ToInt16(dr["xpOriginal"]);
                        debugField = "wn8";
                        b.Wn8 = Convert.ToInt16(dr["wn8"]);
                        debugField = "arenaUniqueID";
                        b.ArenaUniqueId = dr["arenaUniqueID"] == DBNull.Value ? (long?)null : Convert.ToInt64(dr["arenaUniqueID"]);
                        if (b.ArenaUniqueId != null)
                        {
                            debugField = "real_xp";
                            b.XpReal = dr["real_xp"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["real_xp"]);
                            debugField = "creditsNet";
                            b.Credits = dr["creditsNet"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["creditsNet"]);
                            debugField = "markOfMastery";
                            b.MarkOfMastery = dr["markOfMastery"] == DBNull.Value ? (byte?)null : Convert.ToByte(dr["markOfMastery"]);
                            debugField = "fragsteam";
                            b.FragsTeam = dr["fragsteam"] == DBNull.Value ? (byte?)null : Convert.ToByte(dr["fragsteam"]);
                            debugField = "fragsenemy";
                            b.FragsEnemy = dr["fragsenemy"] == DBNull.Value ? (byte?)null : Convert.ToByte(dr["fragsenemy"]);
                            debugField = "damageRating";
                            b.DmgRating = dr["damageRating"] == DBNull.Value ? (Int16?)null : Convert.ToInt16(dr["damageRating"]);
                            debugField = "damageRatingTotal";
                            b.DmgRatingTotal = dr["damageRatingTotal"] == DBNull.Value ? (Int16?)null : Convert.ToInt16(dr["damageRatingTotal"]);
                            debugField = "mapId";
                            b.MapId = dr["mapId"] == DBNull.Value ? (Int16?)null : Convert.ToInt16(dr["mapId"]);
                        }
                        battles.Add(b);
                    }
                    catch (Exception ex)
                    {
                        var value = dr[debugField];
                        if (value == DBNull.Value)
                            value = "NULL";
                        Log.LogToFile(ex, " ### Error reading battle: " + battleId.ToString() + " field: " + debugField + " with value: " + value.ToString() + " for upload to web.");
                        DB.ExecuteNonQuery($"UPDATE battle SET transferred=1 WHERE id={battleId}");
                    }
                    
                    // Upload for each 1000 battles
                    if (battles.Count > 999)
                    {
                        uploadTotal += battles.Count;
                        uploadSuccess += await Upload(playerId, playerToken, battles);
                        battles = new List<Models.AppBattleUploadModels.AppBattle>();
                    }
                }
                // Upload rest
                if (battles.Count > 0)
                {
                    uploadTotal += battles.Count;
                    uploadSuccess += await Upload(playerId, playerToken, battles);
                }
                // Done
                if (uploadTotal == 0)
                    return "No battles for upload.";
                else if (uploadTotal == uploadSuccess)
                    return $"Uploaded {uploadSuccess} battles successfully";
                else
                    return $"Uploaded {uploadSuccess} of {uploadTotal}";
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex, " ### Error uploading battles to web");
                return $"Error uploading battles to web. Please check log file. Error: {ex.Message}" + Environment.NewLine + Environment.NewLine; 
            }
        }

        private async Task<int> Upload(int playerId, Guid playerToken, List<Models.AppBattleUploadModels.AppBattle> battles)
        {
            Models.AppBattleUploadModels.Request request = new Models.AppBattleUploadModels.Request()
            {
                PlayerId = playerId,
                PlayerToken = playerToken,
                Battles = battles
            };
            // Call Wot Numbers Web service, log app start and request data 
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync($"{Constants.WotNumWebUrl()}/Api/AppBattle", request);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            Models.AppBattleUploadModels.Result result = JsonConvert.DeserializeObject<Models.AppBattleUploadModels.Result>(json);
            // update battles as transferred
            string sql = "";
            if (result.Success)
            {
                foreach (int item in result.BattleIdSuccessTransfer)
                {
                    sql += $"UPDATE battle SET transferred=1 WHERE id={item.ToString()}; ";
                }
            }
            DB.ExecuteNonQuery(sql, false, true);
            return result.BattleIdSuccessTransfer.Count;
        }

    }


}
