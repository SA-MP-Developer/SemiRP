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
                    if (player.Weapon != 0)
                    {
                        Gun weapon = new Gun(); // Create Gun
                        weapon.idWeapon = (int) player.Weapon;
                        weapon.Quantity = player.WeaponAmmo;
                        weapon.CurrentContainer = player.ActiveCharacter.Inventory;
                        weapon.SpawnLocation = null;
                        weapon.Name = Weapons.WeaponsDictionnary.GetValueOrDefault((int)player.Weapon);
                        player.SetAmmo(player.Weapon, 0); // Remove gun from player
                        player.ActiveCharacter.Inventory.ListItems.Add(weapon); // Add gun to inventory

                        
                        dbContext.SaveChanges();

                    }
                    else if(player.ActiveCharacter.ItemInHand != null)
                    {
                        player.ActiveCharacter.Inventory.ListItems.Add(player.ActiveCharacter.ItemInHand);
                        ItemHelper.RemoveItemFromPlayerHand(player);
                        dbContext.SaveChanges();
                    }
                    
                }
                else // Take item from inventory
                {
                    if(player.ActiveCharacter.Inventory.ListItems[EventArgs.ListItem] is Gun)
                    {
                        player.GiveWeapon((SampSharp.GameMode.Definitions.Weapon)((Gun)player.ActiveCharacter.Inventory.ListItems[EventArgs.ListItem]).idWeapon, ((Gun)player.ActiveCharacter.Inventory.ListItems[EventArgs.ListItem]).Quantity); // Give player weapon
                        player.ActiveCharacter.Inventory.ListItems.RemoveAt(EventArgs.ListItem); // Remove weapon from inventory
                    }
                    else
                    {
                        player.ActiveCharacter.ItemInHand = player.ActiveCharacter.Inventory.ListItems[EventArgs.ListItem]; // put in hand of player
                        player.ActiveCharacter.Inventory.ListItems.RemoveAt(EventArgs.ListItem); // Remove item from inventory
                    }
                }
            };
        }
    }
}
