using SemiRP.Exceptions;
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
        public static Item AddItemToCharacter(Character character, Item item)
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            
            if (character.Inventory == null)
                throw new InventoryAddingExceptions("Le joueur n'a pas d'inventaire.");

            if(character.ItemInHand == null)
            {
                character.ItemInHand = item;
                dbContext.SaveChanges();
                return item;
            }
            else if(character.Inventory.ListItems.Count() < character.Inventory.MaxSpace)
            {
                character.Inventory.ListItems.Add(item);
                dbContext.SaveChanges();
                return item;
            }
            else
            {
                throw new InventoryAddingExceptions("L'inventaire est complet et la main n'est pas vide.");
            }
        }
        public static Item RemoveItemFromCharacter(Character character, Item item)
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            if (character.Inventory == null)
                throw new InventoryRemovingExceptions("Le joueur n'a pas d'inventaire.");
            if (item == null)
                throw new InventoryRemovingExceptions("Un problème est survenu avec l'objet.");

            if(character.ItemInHand == item)
            {
                character.ItemInHand = null;
                dbContext.SaveChanges();
                return item;
            }
            else if (character.Inventory.ListItems.Remove(item))
            {
                dbContext.SaveChanges();
                return item;
            }
            else
            {
                throw new InventoryRemovingExceptions("L'objet n'existe pas dans l'inventaire ou la main du joueur.");
            }
        }
    }
}
