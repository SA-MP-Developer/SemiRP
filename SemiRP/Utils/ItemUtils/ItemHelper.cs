using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SemiRP.Models;
using SemiRP.Models.ItemHeritage;

namespace SemiRP.Utils.ItemUtils
{
    public class ItemHelper
    {
        public static List<Item> GetAllItem()
        {
            return ((GameMode)GameMode.Instance).DbContext.Items.ToList();
        }
        public static List<Item> GetItemByName(string name)
        {
            return ((GameMode)GameMode.Instance).DbContext.Items.Select(x=>x).Where(w=>w.Name == name).ToList();
        }
    }
}
