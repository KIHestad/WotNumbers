using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
    class BonusType
    {
        public int bonusType { get; set; }

        BonusType(int i_bonusType)
        {
            bonusType = i_bonusType;
        }

        public static implicit operator BonusType(int i_bonusType) => new BonusType(i_bonusType);
        public static implicit operator int(BonusType i_bonusType) => i_bonusType.bonusType;
        public static bool operator ==(BonusType a, BonusType b) => a.bonusType == b.bonusType;
        public static bool operator !=(BonusType a, BonusType b) => a.bonusType != b.bonusType;

        public bool ShouldGetPlatoon()
        {
            return bonusType == 1;
        }

        public bool ShouldGetFortResource()
        {
            return bonusType == 10;
        }

        public bool ShouldGetEnemyClan()
        {
            switch (bonusType)
            {
                case 3: // [fallthrough]
                case 4: // [fallthrough]
                case 5: // [fallthrough]
                case 10: // [fallthrough]
                case 11: // [fallthrough]
                case 12: // [fallthrough]
                case 13: return true;
            }
            return false;
        }

        public string GetBattleResultMode()
        {
            // Check class ARENA_BONUS_TYPE in constants.py
            switch (bonusType)
            {
                case 0: return "Unknown";
                case 1: return "Random";
                case 2: return "Trainig Room";
                case 3: return "Tank Company";
                case 4: return "Tournament";
                case 5: return "Clan War";
                case 6: return "Tutorial";
                case 7: return BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeTeam).Name;
                case 8: return BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeHistorical).Name;
                case 9: return BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeSpecial).Name;
                case 10: return BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeSkirmishes).Name;
                case 11: return BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeStronghold).Name;
                case 12: return BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeTeamRanked).Name;
                case 13: return BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeGlobalMap).Name;
                case 14: return "Tournament Regular";
                case 15: return "Tournament Clan";
                case 16: return "Rated Sandbox";
                case 17: return "Sandbox";
                case 18: return "Fallout";
                case 19: return "Fallout Multiteam";
                case 20: return "Sortie ";
                case 21: return "Fort battle";
                case 22: return "Ranked";
                case 23: return "Bootcamp";
                case 24: return BattleMode.GetItemFromType(BattleMode.TypeEnum.ModeGrand).Name;
                case 25: return "Epic random training";
                case 26: return "Event battles";
                case 27: return "Epic battle ";
                case 28: return "Epic battle training";
                case 29: return "Battle Royale solo";
                case 30: return "Battle Royale squad";
                case 31: return "Tournament event";
                case 32: return "Bob";
                case 33: return "Event random";
                case 34: return "Battle Royale Training Solo";
                case 35: return "Battle Royale Training Squad";
                case 36: return "Weekend brawl";
                case 37: return "Map Box";
                case 38: return "Map training";
                case 39: return "Real Time Strategy";
                case 40: return "Real Time Strategy 1x1";
                case 41: return "Real Time Strategy Bootcamp";
                case 42: return "Fun random";
                case 43: return "Competitive 7: Onslaught";
            }

            return "Unknown";
        }
        public BattleMode.TypeEnum GetBattleMode()
        {
            switch (bonusType)
            {
                case 1: return BattleMode.TypeEnum.ModeRandom_TC;
                case 7: return BattleMode.TypeEnum.ModeTeam;
                case 8: return BattleMode.TypeEnum.ModeHistorical;
                case 9: return BattleMode.TypeEnum.ModeSpecial;
                case 10: return BattleMode.TypeEnum.ModeSkirmishes;
                case 11: return BattleMode.TypeEnum.ModeStronghold;
                case 12: return BattleMode.TypeEnum.ModeTeamRanked;
                case 13: return BattleMode.TypeEnum.ModeGlobalMap;
                case 24: return BattleMode.TypeEnum.ModeGrand;
            }

            return BattleMode.TypeEnum.AllModes;
        }
    }
}
