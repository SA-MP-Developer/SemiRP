using System;
using System.Collections.Generic;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP;
using SemiRP.Models;
using SemiRP.Models.ItemHeritage;
using SemiRP.Utils;
using SemiRP.Utils.ContainerUtils;
using SemiRP.Utils.ItemUtils;

namespace SemiRP.Dialog
{
    public class InventoryDialog
    {
        private static TablistDialog listInventory;
        public static void ShowPlayerInventory(Player player)
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            int maxSpaceContainer = player.ActiveCharacter.Inventory.MaxSpace;
            listInventory = new TablistDialog("Inventaire ("+player.ActiveCharacter.Inventory.ListItems.Count+"/"+maxSpaceContainer+")", new[] { "Nom", "Quantité" }, "Sélectionner", "Quitter");
            
            List<Item> listItemsContainer = player.ActiveCharacter.Inventory.ListItems;
            int i = 0;
            foreach (Item item in listItemsContainer)
            {
                listInventory.Add(item.Name, item.Quantity.ToString());
                i++;
            }
            if (i < maxSpaceContainer)
            {
                for (int a = 0; a < (maxSpaceContainer - i); a++)
                {
                    listInventory.Add(Color.DarkGray + " Vide" + Color.White, Color.DarkGray + "0");
                }
            }
            listInventory.Show(player);

            listInventory.Response += (playerReponse, EventArgs) =>
            {
                if(EventArgs.DialogButton == DialogButton.Right)
                {
                    return;
                }
                if(player.ActiveCharacter.Inventory.ListItems.Count <= EventArgs.ListItem
                   || player.ActiveCharacter.Inventory.ListItems[EventArgs.ListItem] == null) //  Put item in inventory
                {
                    if (player.ActiveCharacter.ItemInHand != null)
                    {
                        try
                        {
                            if(player.ActiveCharacter.ItemInHand is Gun)
                            {
                                player.SetArmedWeapon(((Gun)player.ActiveCharacter.ItemInHand).idWeapon);
                                player.ActiveCharacter.ItemInHand.Quantity = player.WeaponAmmo;
                            }
                            player.ActiveCharacter.Inventory.ListItems.Add(player.ActiveCharacter.ItemInHand); // Add item to inventory
                            ItemHelper.RemoveItemFromPlayerHand(player);
                            dbContext.SaveChanges();
                        }
                        catch(Exception e)
                        {
                            Chat.ErrorChat(player, "Impossible de prendre l'objet.");
                        }
                    }
                }
                else // Take item from inventory
                {
                    if(player.ActiveCharacter.ItemInHand == null)
                    {
                        try
                        {
                            //ItemHelper.PutItemInPlayerHand(player, player.ActiveCharacter.Inventory.ListItems[EventArgs.ListItem]);
                            Item item = InventoryHelper.RemoveItemFromCharacter(player.ActiveCharacter, player.ActiveCharacter.Inventory.ListItems[EventArgs.ListItem]);
                            InventoryHelper.AddItemToCharacter(player.ActiveCharacter, item);
                            dbContext.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            Chat.ErrorChat(player, "Impossible de prendre l'objet : " + e.Message);
                        }
                    }
                    else
                    {
                        try
                        {
                            Item itemFromInventory = InventoryHelper.RemoveItemFromCharacter(player.ActiveCharacter, player.ActiveCharacter.Inventory.ListItems[EventArgs.ListItem]);
                            Item itemFromHand = InventoryHelper.RemoveItemFromCharacter(player.ActiveCharacter, player.ActiveCharacter.ItemInHand);
                            InventoryHelper.AddItemToCharacter(player.ActiveCharacter, itemFromInventory);
                            InventoryHelper.AddItemToCharacter(player.ActiveCharacter, itemFromHand);
                        }
                        catch(Exception e)
                        {
                            Chat.ErrorChat(player, "Impossible de prendre l'objet : " + e.Message);
                        }
                    }
                    
                }
            };
        }
    }
}
