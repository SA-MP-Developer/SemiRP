using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SemiRP.Models;
using SemiRP.Models.ItemHeritage;
using SemiRP.Utils.PlayerUtils;
using SampSharp.Streamer.World;

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
            player.ActiveCharacter.ItemInHand = item;
        }
        public static void PutItemOnGround(Player player)
        {
            if (player.ActiveCharacter.ItemInHand == null)
                throw new Exception("Aucun objet en main.");
            if (player.ActiveCharacter.ItemInHand.ModelId == -1)
                throw new Exception("Cet objet n'est pas posable.");
            Item item = player.ActiveCharacter.ItemInHand;
            item.SpawnLocation = new SpawnLocation(player.Position, player.Rotation.Z,player.Interior, player.VirtualWorld);
            DynamicObject itemOnGround = new DynamicObject(item.ModelId, item.SpawnLocation.Position, item.SpawnLocation.Rotation, item.SpawnLocation.VirtualWorld, item.SpawnLocation.Interior);
            RemoveItemFromPlayerHand(player);
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            dbContext.SaveChanges();
        }

    }
}
