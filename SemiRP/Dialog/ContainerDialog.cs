using System;
using System.Collections.Generic;
using System.Text;
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
    public class ContainerDialog
    {
        private static TablistDialog listContainer;
        public static void ShowContainer(Player player, Container container)
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            int maxSpaceContainer = container.MaxSpace;
            listContainer = new TablistDialog(container.Name+" (" + container.ListItems.Count + "/" + maxSpaceContainer + ")", new[] { "Nom", "Quantité" }, "Sélectionner", "Quitter");

            List<Item> listItemsContainer = container.ListItems;
            int i = 0;
            foreach (Item item in listItemsContainer)
            {
                listContainer.Add(item.Name, item.Quantity.ToString());
                i++;
            }
            if (i < maxSpaceContainer)
            {
                for (int a = 0; a < (maxSpaceContainer - i); a++)
                {
                    listContainer.Add(Color.DarkGray + " Vide" + Color.White, Color.DarkGray + "0");
                }
            }
            listContainer.Show(player);

            listContainer.Response += (playerReponse, EventArgs) =>
            {
                if (EventArgs.DialogButton == DialogButton.Right)
                {
                    return;
                }
                if (container.ListItems.Count <= EventArgs.ListItem
                   || container.ListItems[EventArgs.ListItem] == null) //  Put item in container
                {
                    if (player.ActiveCharacter.ItemInHand != null)
                    {
                        try
                        {
                            if (player.ActiveCharacter.ItemInHand is Gun)
                            {
                                player.SetArmedWeapon(((Gun)player.ActiveCharacter.ItemInHand).idWeapon);
                                player.ActiveCharacter.ItemInHand.Quantity = player.WeaponAmmo;
                            }
                            container.ListItems.Add(player.ActiveCharacter.ItemInHand); // Add item to container
                            ItemHelper.RemoveItemFromPlayerHand(player);
                            dbContext.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            Chat.ErrorChat(player, "Impossible de prendre l'objet.");
                        }
                    }
                }
                else // Take item from container
                {
                    if (player.ActiveCharacter.ItemInHand == null)
                    {
                        try
                        {
                            //ItemHelper.PutItemInPlayerHand(player, container.ListItems[EventArgs.ListItem]);
                            
                            InventoryHelper.AddItemToCharacter(player.ActiveCharacter, container.ListItems[EventArgs.ListItem]);
                            container.ListItems.RemoveAt(EventArgs.ListItem);
                            
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
                            Item itemContainer = container.ListItems[EventArgs.ListItem];
                            Item itemInHand = player.ActiveCharacter.ItemInHand;
                            
                            container.ListItems.RemoveAt(EventArgs.ListItem);
                            InventoryHelper.RemoveItemFromCharacter(player.ActiveCharacter, player.ActiveCharacter.ItemInHand);

                            container.ListItems.Add(itemInHand);
                            InventoryHelper.AddItemToCharacter(player.ActiveCharacter, itemContainer);
                        }
                        catch (Exception e)
                        {
                            Chat.ErrorChat(player, "Impossible de prendre l'objet : " + e.Message);
                        }
                    }

                }
            };
        }
    }
}
