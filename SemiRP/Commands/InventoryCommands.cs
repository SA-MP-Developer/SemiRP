using SampSharp.GameMode.SAMP.Commands;
using SemiRP.Dialog;
using SemiRP.Utils;
using SemiRP.Utils.ItemUtils;
using System;

namespace SemiRP.Commands
{
    [CommandGroup("inventaire", "inv", "sac", "i")]
    public class InventoryCommands
    {
        [Command("ramasser", "prendre", "recuperer")]
        private static void PutItemInPlayerHand(Player player)
        {
            try
            {
                ItemHelper.RemoveItemFromGround(player);
                Chat.InfoChat(player, "L'objet a été ramassé.");
            }
            catch (Exception e)
            {
                Chat.ErrorChat(player, e.Message);
            }
        }

        [Command("poser", "mettre")]
        private static void PutItemOnGround(Player player)
        {
            try
            {
                ItemHelper.PutItemOnGround(player);
                Chat.InfoChat(player, "L'objet a été posé au sol.");
            }
            catch (Exception e)
            {
                Chat.ErrorChat(player, e.Message);
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
                Chat.ErrorChat(player, e.Message);
            }
        }
    }
}