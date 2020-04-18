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
        public static Inventory GetAllItems(Character character)
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            return dbContext.Characters.Select(x => x).Where(x => x == character).FirstOrDefault().Inventory;
        }
        public static Item GetItemByName(Character character, String name)
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            return dbContext.Characters.Select(x => x).Where(x => x == character).FirstOrDefault().Inventory.ListItems.Select(x=>x).Where(x=>x.Name == name).FirstOrDefault();
        }
        public static bool AddItemToCharacter(Character character, Item item)
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            Inventory inv = dbContext.Characters.Select(x => x).Where(x => x == character).FirstOrDefault().Inventory;
            if (inv == null)
                return false;

            if(inv.ListItems.Count() < inv.MaxSpace)
            {
                inv.ListItems.Add(item);
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
