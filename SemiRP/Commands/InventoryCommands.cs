using SampSharp.GameMode.SAMP.Commands;
using SemiRP.Dialog;
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

        [Command("ramasser")]
        private static void PutItemInPlayerHand (Player player)
        {
            try
            {
                ItemHelper.RemoveItemFromGround(player);
                Utils.Chat.InfoChat(player, "L'objet a été ramassé.");

            }
            catch(Exception e)
            {
                Utils.Chat.ErrorChat(player, "Impossible de ramasser l'objet : "+e.Message);
            }
        }

        [Command("poser")]
        private static void PutItemOnGround(Player player)
        {
            try
            {
                ItemHelper.PutItemOnGround(player);
                Utils.Chat.InfoChat(player, "L'objet a été posé au sol.");

            }
            catch (Exception e)
            {
                Utils.Chat.ErrorChat(player, "L'objet n'a pas pu être posé au sol : " + e.Message);
            }
        }

    }
    public class InventoryCommand
    {
        [Command("inventaire", "inv", "sac", "i")]
        private static void OpenInventory(Player player)
        {
            try
            {
                InventoryDialog.ShowPlayerInventory(player);
            }
            catch (Exception e)
            {
                Utils.Chat.ErrorChat(player, "Une erreur s'est produite avec l'inventaire : "+e.Message);
            }
        }
    }
    
}
