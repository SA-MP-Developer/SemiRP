using SemiRP.Models;
using SemiRP.Models.ContainerHeritage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemiRP.Utils.ContainerUtils
{
    public class InventoryHelper
    {
        public static Inventory GetInventory(Character character)
        {
            using(var db = new ServerDbContext())
            {
                return db.Characters.Select(x => x).Where(x => x == character).FirstOrDefault().Inventory;
            }
        }
        
    }
}
