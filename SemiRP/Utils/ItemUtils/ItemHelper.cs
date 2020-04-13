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
            
            using (var db = new ServerDbContext())
            {
                return db.Items.ToList();
            }
        }
        public static List<Item> GetItemByName(string name)
        {
            using (var db = new ServerDbContext())
            {
                return db.Items.Select(x=>x).Where(w=>w.Name == name).ToList();
            }
        }

        



    }
}
