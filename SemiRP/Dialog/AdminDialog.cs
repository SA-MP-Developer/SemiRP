using SampSharp.GameMode.Display;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Dialog
{
    public class AdminDialog
    {
        private static TablistDialog listAdmin;
        private static TablistDialog listModeration;
        private static TablistDialog listGive;
        private static TablistDialog listVehicle;
        private static TablistDialog listPerms;

        private static InputDialog responseDialog;

        public static void ShowAdminDialog(Player player)
        {
            listAdmin = new TablistDialog("Administration",4, "Sélectionner", "Quitter");
            listAdmin.Add(new[]
            {
                "Modération",
                "Donner",
                "Véhicule",
                "Permissions"
            });
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
                            ShowModerationDialog(player);
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
        private static void ShowModerationDialog(Player player)
        {
            listModeration = new TablistDialog("Administration - Modération", 4, "Sélectionner", "Quitter");
            listModeration.Add(new[]
            {
                "Se téléporter à",
                "Téléporter à sois",
                "Slap",
                "Freeze",
                "Unfreeze",
                "Activer/Désactiver PM"
            });
            listModeration.Show(player);
            listModeration.Response += (sender, EventArgs) =>
            {
                switch (EventArgs.ListItem)
                {
                    case 0:
                        {
                            responseDialog = new InputDialog("Se téléporter à","Indiquez l'ID du joueur auquel se téléporter :",false, "Sélectionner", "Quitter");
                            responseDialog.Response += (sender, EventArgs) =>
                            {
                                try
                                {

                                }
                                catch(Exception e)
                                {
                                    Utils.Chat.ErrorChat(player, "Une erreur est survenue, impossible de se téléporter au joueur.");
                                }
                            };
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
                                    Utils.Chat.ErrorChat(player, "Une erreur est survenue, impossible de téléporter le joueur.");
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
                                    Utils.Chat.ErrorChat(player, "Une erreur est survenue, impossible de slap le joueur.");
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
                                    Utils.Chat.ErrorChat(player, "Une erreur est survenue, impossible de freeze le joueur.");
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
                                    Utils.Chat.ErrorChat(player, "Une erreur est survenue, impossible d'unfreeze le joueur.");
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
                                Utils.Chat.ErrorChat(player, "Une erreur est survenue, impossible d'unfreeze le joueur.");
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
