using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Services.Models
{
    public class AppBattleUploadModels
    {
        public class Request
        {
            public int PlayerId { get; set; }
            public Guid PlayerToken { get; set; }
            public List<AppBattle> Battles { get; set; }
        }

        public class Result
        {
            public Result()
            {
                Success = false;
            }
            public bool Success { get; set; }                       // Overall result
            public string Message { get; set; }                     // Message for connection result and more
            public List<int> BattleIdSuccessTransfer { get; set; }  // List of Battle id's successfully transferred to web

        }

        public class AppBattle
        {
            public int PlayerId { get; set; }
            public int PlayerBattleAppId { get; set; }
            public DateTime BattleTime { get; set; }
            public byte BattleModeId { get; set; }
            public byte BattleResultId { get; set; }
            public byte BattleSurviveId { get; set; }
            public int TankId { get; set; }
            public byte Frags { get; set; }
            public short Dmg { get; set; }
            public short DmgReceived { get; set; }
            public byte Cap { get; set; }
            public byte Def { get; set; }
            public short Shots { get; set; }
            public short Hits { get; set; }
            public byte Spotted { get; set; }
            public short Xp { get; set; }
            public short XpOriginal { get; set; }
            public short Wn8 { get; set; }
            public long? ArenaUniqueId { get; set; }
            public int? XpReal { get; set; }
            public int? Credits { get; set; }
            public byte? MarkOfMastery { get; set; }
            public byte? FragsTeam { get; set; }
            public byte? FragsEnemy { get; set; }
            public short? DmgRating { get; set; }
            public short? DmgRatingTotal { get; set; }
            public short? MapId { get; set; }
            public short BattlesCount { get; set; }
        }
    }

}
