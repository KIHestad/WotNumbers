using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater
{
    class jsonProperty
    {
        public class MainSection
        {
            public string header = "header";
            public string tanks = "tanks";
            public string tanks_v2 = "tanks_v2";
        }
        
        public class Item
        {
            public string mainSection = "";
            public string tank = "";
            public string subSection = "";
            public string property = "";
            public object value = null;
        }

        public class ItemMapping
        {
            public string mainSection = "";
            public string subSection = "";
            public string property = "";
            public string dbtable = "";
            public string dbfield = "";
        }

        public List<ItemMapping> itemMappings = new List<ItemMapping>();

        public void initItemMappings(List<ItemMapping> itemMappings)
        {
            
        }


    }
}
