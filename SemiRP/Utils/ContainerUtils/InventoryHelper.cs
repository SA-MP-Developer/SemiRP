using SemiRP.Exceptions;
using SemiRP.Models;
using SemiRP.Models.ContainerHeritage;
using SemiRP.Models.ItemHeritage;
using SemiRP.Utils.ItemUtils;
using SemiRP.Utils.PlayerUtils;
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
            if(item is Phone)
            {
                if (((Phone)item).DefaultPhone)
                {
                    if (PhoneHelper.GetDefaultPhone(character) != null)
                    {
                        ((Phone)item).DefaultPhone = false;
                    }
                }
                
            }
            if(character.ItemInHand == null)
            {
                character.ItemInHand = item;
                dbContext.SaveChanges();
                if(item is Gun)
                {
                    Player player = PlayerHelper.SearchCharacter(character);
                    player.GiveWeapon(((Gun)item).idWeapon, ((Gun)item).Quantity);
                }
                return item;
            }
            else if(character.Inventory.ListItems.Count() < character.Inventory.MaxSpace)
            {
                item.CurrentContainer = character.Inventory;
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
                if (item is Gun)
                {
                    Player player = PlayerHelper.SearchCharacter(character);
                    player.ResetWeapons();
                }
                return item;
            }
            else if (character.Inventory.ListItems.Remove(item))
            {
                item.CurrentContainer = null;
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
