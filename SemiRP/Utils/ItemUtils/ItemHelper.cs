using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SemiRP.Models;
using SemiRP.Models.ItemHeritage;
using SemiRP.Utils.PlayerUtils;
using SampSharp.Streamer.World;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SemiRP.Utils.ContainerUtils;

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
        public static Item GetNearestItemOfCharacter(Character character)
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            Player player = PlayerHelper.SearchCharacter(character);
            Item item = dbContext.Items.Where(i => i.CurrentContainer == null && i.SpawnLocation != null).ToList().OrderBy(x => player.GetDistanceFromPoint(x.SpawnLocation.Position)).FirstOrDefault();
            if(item != null)
            {
                return item;
            }
            else
            {
                throw new Exception("Aucun objet.");
            }
            
        }
        public static void ItemIsCloseEnoughOfPlayer(Player player, Item item)
        {
            if (item.SpawnLocation.Position.DistanceTo(player.Position) > Constants.Item.PROXIMITY_RADIUS)
            {
                throw new Exception("Aucun objet à proximité.");
            }
        }
        public static void RemoveItemFromPlayerHand(Player player)
        {
            if(player.ActiveCharacter.ItemInHand == null)
            {
                throw new Exception("Aucun objet dans les mains.");
            }
            player.ActiveCharacter.ItemInHand = null;
            player.ResetWeapons();
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            dbContext.SaveChanges();
        }
        public static void DeleteItem(Item item)
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            dbContext.Items.Remove(item);
            dbContext.SaveChanges();
        }
        public static void PutItemInPlayerHand(Player player, Item item)
        {
            if(player.ActiveCharacter.ItemInHand != null)
            {
                throw new Exception("Le joueur a déjà un objet en main.");
            }
            item.SpawnLocation = null;
            item.DynamicObject = null;
            player.ActiveCharacter.ItemInHand = item;
            if (item is Gun)
            {
                player.GiveWeapon(((Gun)item).idWeapon, item.Quantity);
            }
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            dbContext.SaveChanges();
        }
        public static void PutItemOnGround(Player player)
        {
            if (player.ActiveCharacter.ItemInHand == null)
                throw new Exception("Aucun objet en main.");
            if (player.ActiveCharacter.ItemInHand.ModelId == -1)
                throw new Exception("Cet objet n'est pas posable.");
            Item item = player.ActiveCharacter.ItemInHand;
            if(item is Gun)
            {
                player.SetArmedWeapon(((Gun)item).idWeapon);
                item.Quantity = player.WeaponAmmo;
            }
            Vector3 position = new Vector3(player.Position.X, player.Position.Y, player.Position.Z-0.9);
            Vector3 rotation = new Vector3();
            if(item is Gun)
            {
                rotation = new Vector3(90, 0, 0);
            }
            else
            {
                rotation = new Vector3(0, 0, 0);
            }
            item.SpawnLocation = new SpawnLocation(position, rotation,player.Interior, player.VirtualWorld);
            item.DynamicObject = new DynamicObject(item.ModelId, item.SpawnLocation.Position, item.SpawnLocation.Rotation, item.SpawnLocation.VirtualWorld, item.SpawnLocation.Interior);
            RemoveItemFromPlayerHand(player);
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            dbContext.SaveChanges();
        }
        public static void RemoveItemFromGround(Player player)
        {
            Item item = GetNearestItemOfCharacter(player.ActiveCharacter);
            ItemIsCloseEnoughOfPlayer(player, item);
            if (player.ActiveCharacter.ItemInHand != null)
                throw new Exception("Vous avez déjà un objet en main.");
            item.SpawnLocation = null;
            item.DynamicObject.Dispose();
            item.DynamicObject = null;
            InventoryHelper.AddItemToCharacter(player.ActiveCharacter, item);
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            dbContext.SaveChanges();

        }

    }
}
