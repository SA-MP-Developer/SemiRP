using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP;
using SemiRP.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.PlayerSystems
{
    class PlayerInventory
    {
        private readonly Player player;

        #region Dialogs

        private TablistDialog listInventory;

        #endregion

        public PlayerInventory(Player player)
        {
            this.player = player;
            BuildInventoryDialog(player);
            
        }

        private void BuildInventoryDialog(Player player)
        {
            listInventory = new TablistDialog("Inventaire", new[] { "Nom", "Quantité" }, "Sélectionner", "Quitter");
            int maxSpaceContainer = player.ActiveCharacter.Inventory.MaxSpace;
            List<Item> listItemsContainer = player.ActiveCharacter.Inventory.ListItems;
            int i = 0;
            foreach(Item item in listItemsContainer)
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
                for(int a=0;a<(maxSpaceContainer - i); a++)
                {
                    listInventory.Add(new[]
                    {
                        Color.Green+"Vide"+Color.White,
                        "0"
                    });
                }
            }
            listInventory.Show(player);
            
        }
    }
}
