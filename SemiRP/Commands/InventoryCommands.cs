using SampSharp.GameMode.SAMP.Commands;
using SemiRP.Dialog;
using SemiRP.Exceptions;
using SemiRP.Models;
using SemiRP.Utils;
using SemiRP.Utils.ContainerUtils;
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
                Item itemFromGround = ItemHelper.RemoveItemFromGround(player);
                InventoryHelper.AddItemToCharacter(player.ActiveCharacter, itemFromGround);
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
                ItemHelper.RemoveItemFromPlayerHand(player);
                Chat.InfoChat(player, "L'objet a été posé au sol.");
            }
            catch (Exception e)
            {
                Chat.ErrorChat(player, e.Message);
            }
        }
        [Command("donner", "don")]
        private static void GiveItemToCharacter(Player player, Player receiver)
        {
            Item itemToGive = player.ActiveCharacter.ItemInHand;
            try
            {
                InventoryHelper.RemoveItemFromCharacter(player.ActiveCharacter, player.ActiveCharacter.ItemInHand);
                InventoryHelper.AddItemToCharacter(receiver.ActiveCharacter, itemToGive);
                
                Chat.InfoChat(player, "L'objet à été donné à "+receiver.ActiveCharacter.Name+".");
            }
            catch (InventoryAddingExceptions e)
            {
                InventoryHelper.AddItemToCharacter(player.ActiveCharacter, itemToGive);
                Chat.ErrorChat(player, e.Message);
            }
            catch(InventoryRemovingExceptions e)
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