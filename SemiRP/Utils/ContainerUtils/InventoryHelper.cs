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
        public static void AddItemToCharacter(Character character, Item item)
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            
            if (character.Inventory == null)
                throw new Exception("Le joueur n'a pas d'inventaire.");

            if(character.ItemInHand == null)
            {
                character.ItemInHand = item;
                dbContext.SaveChanges();
            }
            else if(character.Inventory.ListItems.Count() < character.Inventory.MaxSpace)
            {
                character.Inventory.ListItems.Add(item);
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("L'inventaire est complet et la main n'est pas vide.");
            }
        }
    }
}
