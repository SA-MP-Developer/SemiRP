using SampSharp.GameMode.Display;
using SemiRP.Utils;
using SemiRP.Utils.PlayerUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Dialog
{
    public class AdminDialog
    {
        private static ListDialog listAdmin;
        private static ListDialog listModeration;
        private static ListDialog listGive;
        private static ListDialog listVehicle;
        private static ListDialog listPerms;

        private static InputDialog responseDialog;

        public static void ShowAdminDialog(Player player, Player receiver = null)
        {
            listAdmin = new ListDialog("Administration", "Sélectionner", "Quitter");
            listAdmin.AddItem("Modération");
            listAdmin.AddItem("Donner");
            listAdmin.AddItem("Véhicule");
            listAdmin.AddItem("Permissions");
            listAdmin.Show(player);
            listAdmin.Response += (sender, EventArgs) =>
            {
                if(EventArgs.DialogButton == SampSharp.GameMode.Definitions.DialogButton.Right)
                {
                    return;
                }

                switch (EventArgs.ListItem)
                {
                    case 0:
                        {
                            ShowModerationDialog(player, receiver);
                            break;
                        }
                    case 1:
                        {
                            ShowGiveDialog(player);
                            break;
                        }
                    case 2:
                        {
                            ShowVehicleDialog(player);
                            break;
                        }
                    case 3:
                        {
                            ShowPermsDialog(player);
                            break;
                        }
                    default:return;
                }
            };
        }
        private static void ShowModerationDialog(Player player, Player receiver)
        {
            listModeration = new ListDialog("Administration - Modération", "Sélectionner", "Quitter");
            listModeration.AddItem("Se téléporter à");
            listModeration.AddItem("Téléporter à sois");
            listModeration.AddItem("Slap");
            listModeration.AddItem("Freeze");
            listModeration.AddItem("Unfreeze");
            listModeration.AddItem("Activer/Désactiver PM");
            listModeration.Show(player);
            listModeration.Response += (sender, EventArgs) =>
            {
                switch (EventArgs.ListItem)
                {
                    case 0:
                        {
                            if(receiver != null)
                            {
                                responseDialog = new InputDialog("Se téléporter à", "Indiquez l'ID du joueur auquel se téléporter :", false, "Sélectionner", "Quitter");
                                responseDialog.Response += (sender, EventArgs) =>
                                {
                                    try
                                    {
                                        AdminHelper.Goto(player, receiver);
                                    }
                                    catch (Exception e)
                                    {
                                        Utils.Chat.ErrorChat(player, "Une erreur est survenue, impossible de se téléporter au joueur." + e.Message);
                                    }
                                };
                            }
                            else
                            {
                                responseDialog = new InputDialog("Se téléporter à", "Indiquez l'ID du joueur auquel se téléporter :", false, "Sélectionner", "Quitter");
                                responseDialog.Response += (sender, EventArgs) =>
                                {
                                    try
                                    {
                                        receiver = PlayerHelper.AskId(player);
                                        AdminHelper.Goto(player, receiver);
                                    }
                                    catch (Exception e)
                                    {
                                        Utils.Chat.ErrorChat(player, "Une erreur est survenue, impossible de se téléporter au joueur." + e.Message);
                                    }
                                };
                            }
                            break;
                        }
                    case 1:
                        {
                            responseDialog = new InputDialog("Téléporter à sois", "Indiquez l'ID du joueur que vous voulez téléporter :", false, "Sélectionner", "Quitter");
                            responseDialog.Response += (sender, EventArgs) =>
                            {
                                try
                                {

                                }
                                catch (Exception e)
                                {
                                    Utils.Chat.ErrorChat(player, "Une erreur est survenue, impossible de téléporter le joueur." + e.Message);
                                }
                            };
                            break;
                        }
                    case 2:
                        {
                            responseDialog = new InputDialog("Slap", "Indiquez l'ID du joueur que vous voulez slap :", false, "Sélectionner", "Quitter");
                            responseDialog.Response += (sender, EventArgs) =>
                            {
                                try
                                {

                                }
                                catch (Exception e)
                                {
                                    Utils.Chat.ErrorChat(player, "Une erreur est survenue, impossible de slap le joueur." + e.Message);
                                }
                            };
                            break;
                        }
                    case 3:
                        {
                            responseDialog = new InputDialog("Freeze", "Indiquez l'ID du joueur que vous voulez freeze :", false, "Sélectionner", "Quitter");
                            responseDialog.Response += (sender, EventArgs) =>
                            {
                                try
                                {

                                }
                                catch (Exception e)
                                {
                                    Utils.Chat.ErrorChat(player, "Une erreur est survenue, impossible de freeze le joueur." + e.Message);
                                }
                            };
                            break;
                        }
                    case 4:
                        {
                            responseDialog = new InputDialog("Unfreeze", "Indiquez l'ID du joueur que vous voulez unfreeze :", false, "Sélectionner", "Quitter");
                            responseDialog.Response += (sender, EventArgs) =>
                            {
                                try
                                {

                                }
                                catch (Exception e)
                                {
                                    Utils.Chat.ErrorChat(player, "Une erreur est survenue, impossible d'unfreeze le joueur." + e.Message);
                                }
                            };
                            break;
                        }
                    case 5:
                        {
                            try
                            {

                            }
                            catch (Exception e)
                            {
                                Utils.Chat.ErrorChat(player, "Une erreur est survenue, impossible d'unfreeze le joueur." + e.Message);
                            }
                            break;
                        }
                }
            };
        }
        private static void ShowGiveDialog(Player player)
        {

        }
        private static void ShowVehicleDialog(Player player)
        {

        }
        private static void ShowPermsDialog(Player player)
        {

        }

    }
}
