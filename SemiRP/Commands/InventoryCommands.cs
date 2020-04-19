using SampSharp.GameMode.SAMP.Commands;
using SemiRP.Models;
using SemiRP.Utils.ContainerUtils;
using SemiRP.Utils.ItemUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Commands
{
    [CommandGroup("inventaire", "inv", "sac", "i")]
    public class InventoryCommands
    {

        [Command("mettre")]
        private static void PutItemInInventory (Player player){
            
            try
            {
                Item item = ItemHelper.GetNearestItemOfCharacter(player.ActiveCharacter);
                ItemHelper.ItemIsCloseEnoughOfPlayer(player, item);
                InventoryHelper.AddItemToCharacter(player.ActiveCharacter, item);
                Utils.Chat.InfoChat(player, "L'objet a été ajouté à l'inventaire.");
            }
            catch(Exception e)
            {
                Utils.Chat.ErrorChat(player, "L'ajout de l'objet dans l'inventaire à échoué : "+e.Message);
            }
        }

        [Command("prendre")]
        private static void GetItemFromInventory(Player player, int slotNumber)
        {
            try
            {

            }
            catch(Exception e)
            {
                Utils.Chat.ErrorChat(player, "Impossible de prendre l'objet depuis l'inventaire : " + e.Message);
            }
        }
    }
}
