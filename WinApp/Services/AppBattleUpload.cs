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
            string debug = "";
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
                    debug = $"BattleId={dr["id"].ToString()}";
                    // Get 
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
                    b.PlayerId = playerId;
                    b.PlayerBattleAppId = Convert.ToInt32(dr["id"]);
                    b.BattleTime = Convert.ToDateTime(dr["battleTime"]);
                    b.BattleModeId = battleModeId;
                    b.BattleResultId = Convert.ToByte(dr["battleResultId"]);
                    b.BattleSurviveId = Convert.ToByte(dr["battleSurviveId"]);
                    b.TankId = Convert.ToInt32(dr["tankId"]);
                    b.Frags = Convert.ToByte(dr["frags"]);
                    b.Dmg = Convert.ToInt16(dr["dmg"]);
                    b.DmgReceived = Convert.ToInt16(dr["dmgReceived"]);
                    b.Cap = Convert.ToByte(dr["cap"]);
                    b.Def = Convert.ToByte(dr["def"]);
                    b.Shots = Convert.ToInt16(dr["shots"]);
                    b.Hits = Convert.ToInt16(dr["hits"]);
                    b.Spotted = Convert.ToByte(dr["spotted"]);
                    b.Xp = Convert.ToInt16(dr["xp"]);
                    b.XpOriginal = Convert.ToInt16(dr["xpOriginal"]);
                    b.Wn8 = Convert.ToInt16(dr["wn8"]);
                    b.ArenaUniqueId = dr["arenaUniqueID"] == DBNull.Value ? (long?)null : Convert.ToInt64(dr["arenaUniqueID"]);
                    if (b.ArenaUniqueId != null)
                    {
                        b.XpReal = dr["real_xp"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["real_xp"]);
                        b.Credits = dr["creditsNet"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["creditsNet"]);
                        b.MarkOfMastery = dr["markOfMastery"] == DBNull.Value ? (byte?)null : Convert.ToByte(dr["markOfMastery"]);
                        b.FragsTeam = dr["fragsteam"] == DBNull.Value ? (byte?)null : Convert.ToByte(dr["fragsteam"]);
                        b.FragsEnemy = dr["fragsenemy"] == DBNull.Value ? (byte?)null : Convert.ToByte(dr["fragsenemy"]);
                        b.DmgRating = dr["damageRating"] == DBNull.Value ? (Int16?)null : Convert.ToInt16(dr["damageRating"]);
                        b.DmgRatingTotal = dr["damageRatingTotal"] == DBNull.Value ? (Int16?)null : Convert.ToInt16(dr["damageRatingTotal"]);
                    }
                    battles.Add(b);
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
                return ex.Message + Environment.NewLine + Environment.NewLine +
                    "Debug: " + debug + Environment.NewLine + Environment.NewLine +
                    "Trace: " + ex.StackTrace + Environment.NewLine + Environment.NewLine;
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
