using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP;
using SemiRP.Data;
using SemiRP.Models;
using SemiRP.Models.ItemHeritage;
using SemiRP.Utils.ItemUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Dialog
{
    public class InventoryDialog
    {
        private static TablistDialog listInventory;
        public static void ShowPlayerInventory(Player player)
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            listInventory = new TablistDialog("Inventaire", new[] { "Nom", "Quantité" }, "Sélectionner", "Quitter");
            int maxSpaceContainer = player.ActiveCharacter.Inventory.MaxSpace;
            List<Item> listItemsContainer = player.ActiveCharacter.Inventory.ListItems;
            int i = 0;
            foreach (Item item in listItemsContainer)
            {
                listInventory.Add(new[]
                {
                    item.Name,
                    item.Quantity.ToString()
                });
                i++;
            }
            if (i < maxSpaceContainer)
            {
                for (int a = 0; a < (maxSpaceContainer - i); a++)
                {
                    listInventory.Add(new[]
                    {
                        Color.Green+"Vide"+Color.White,
                        "0"
                    });
                }
            }
            listInventory.Show(player);

            listInventory.Response += (playerReponse, EventArgs) =>
            {
                if(EventArgs.DialogButton == SampSharp.GameMode.Definitions.DialogButton.Right)
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
                            player.SetAmmo(player.Weapon, 0); // Remove gun from player
                            if(player.ActiveCharacter.ItemInHand is Gun)
                            {
                                player.ActiveCharacter.ItemInHand.Quantity = player.WeaponAmmo;
                            }
                            player.ActiveCharacter.Inventory.ListItems.Add(player.ActiveCharacter.ItemInHand); // Add item to inventory
                            ItemHelper.RemoveItemFromPlayerHand(player);
                            dbContext.SaveChanges();
                        }
                        catch(Exception e)
                        {
                            Utils.Chat.ErrorChat(player, "Impossible de prendre l'objet.");
                        }
                    }
                }
                else // Take item from inventory
                {
                    if(player.ActiveCharacter.Inventory.ListItems[EventArgs.ListItem] is Gun)
                    {
                        try
                        {
                            ItemHelper.PutItemInPlayerHand(player, player.ActiveCharacter.Inventory.ListItems[EventArgs.ListItem]);
                            player.GiveWeapon((SampSharp.GameMode.Definitions.Weapon)((Gun)player.ActiveCharacter.Inventory.ListItems[EventArgs.ListItem]).idWeapon, ((Gun)player.ActiveCharacter.Inventory.ListItems[EventArgs.ListItem]).Quantity); // Give player weapon
                            player.ActiveCharacter.Inventory.ListItems.RemoveAt(EventArgs.ListItem); // Remove weapon from inventory
                            dbContext.SaveChanges();
                        }
                        catch(Exception e)
                        {
                            Utils.Chat.ErrorChat(player, "Impossible de prendre l'objet : "+e.Message);
                        }
                        
                    }
                    else
                    {
                        try
                        {
                            player.ActiveCharacter.ItemInHand = player.ActiveCharacter.Inventory.ListItems[EventArgs.ListItem]; // put in hand of player
                            player.ActiveCharacter.Inventory.ListItems.RemoveAt(EventArgs.ListItem); // Remove item from inventory
                            dbContext.SaveChanges();
                        }
                        catch(Exception e)
                        {
                            Utils.Chat.ErrorChat(player, "Impossible de prendre l'objet.");
                        }
                    }
                }
            };
        }
    }
}
