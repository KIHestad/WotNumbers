using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
	public class BattleMode
	{
        public enum TypeEnum
        {
            AllModes = 0,
            ModeRandom_TC = 1,
            ModeHistorical = 3,
            ModeTeam = 2,
            ModeTeamRanked = 7,
            ModeGlobalMap = 8,
            ModeSkirmishes = 4,
            ModeStronghold = 6,
            ModeSpecial = 5,
        }

        public class Item
        {
            public TypeEnum Type { get; set; }
            public string Name { get; set; }
            public string SqlName { get; set; }
        }

        public static List<Item> GetMainBattleModes(bool includeAllModes = false)
        {
            List<Item> manBattleModes = new List<Item>();
            manBattleModes.Add(new Item() { 
                Type = TypeEnum.ModeRandom_TC, 
                Name = "Random",
                SqlName = "15"
            });
            manBattleModes.Add(new Item() { 
                Type = TypeEnum.ModeHistorical, 
                Name = "Historical",
                SqlName = "Historical"
            });
            manBattleModes.Add(new Item() { 
                Type = TypeEnum.ModeTeam, 
                Name = "Team: Unranked",
                SqlName = "7"
            });
            manBattleModes.Add(new Item() { 
                Type = TypeEnum.ModeTeamRanked,
                Name = "Team: Ranked",
                SqlName = "7Ranked"
            });
            manBattleModes.Add(new Item() { 
                Type = TypeEnum.ModeGlobalMap,
                Name = "Global Map",
                SqlName = "GlobalMap" 
            });
            manBattleModes.Add(new Item() { 
                Type = TypeEnum.ModeSkirmishes,
                Name = "Skirmishes",
                SqlName = "Skirmishes" 
            });
            manBattleModes.Add(new Item() { 
                Type = TypeEnum.ModeStronghold,
                Name = "Stronghold",
                SqlName = "Stronghold" 
            });
            manBattleModes.Add(new Item() { 
                Type = TypeEnum.ModeSpecial,
                Name = "Special Events",
                SqlName = "Special" 
            });
            if (includeAllModes)
            {
                manBattleModes.Add(new Item()
                {
                    Type = TypeEnum.AllModes,
                    Name = "All Modes",
                    SqlName = ""
                });
            }
            return manBattleModes;
        }

        public static Item GetItemFromType(TypeEnum type)
        {
            Item item = null;
            List<Item> mainBattleModes = GetMainBattleModes(true);
            if (mainBattleModes.Where(b => b.Type == type).Count() > 0)
                item = mainBattleModes.Where(b => b.Type == type).First();
            return item;
        }

        public static Item GetItemFromName(string name)
        {
            Item item = null;
            List<Item> mainBattleModes = GetMainBattleModes(true);
            if (mainBattleModes.Where(b => b.Name == name).Count() > 0)
                item = mainBattleModes.Where(b => b.Name == name).First();
            return item;
        }

        public static Item GetItemFromSqlName(string sqlName)
        {
            Item item = null;
            List<Item> mainBattleModes = GetMainBattleModes(true);
            if (mainBattleModes.Where(b => b.SqlName == sqlName).Count() > 0)
                item = mainBattleModes.Where(b => b.SqlName == sqlName).First();
            return item;
        }

        public static string GetDropDownList(bool includeAllMoldes)
        {
            string list = "";
            foreach (Item item in GetMainBattleModes(includeAllMoldes))
            {
                list += item.Name + ",";
            }
            list = list.Substring(0, list.Length - 1); // Remove last comma
            return list;
        }

        public static string GetShortmenuName(string menuName)
		{
			string shortMenuname = menuName;
			if (menuName.IndexOf("(") > 0) // remove text in paranthes
			{
				shortMenuname = menuName.Substring(0, menuName.IndexOf("(")).Trim();
			}
			return shortMenuname;
		}
	}
}
