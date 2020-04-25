using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;
using SemiRP.Dialog;
using SemiRP.Models;
using SemiRP.Models.ItemHeritage;
using SemiRP.Utils;
using SemiRP.Utils.ContainerUtils;
using SemiRP.Utils.ItemUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemiRP.Commands
{
    [CommandGroup("a", "admin")]
    class AdminCommands
    {
        [Command("help", "aide", "h", "a")]
        private static void Help(Player sender)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.help"))
                return;

            Utils.Chat.AdminChat(sender, "help, goto, gethere, slap, pm, perm, give, set, freeze");
            
        }

        [Command("dialog", "d")]
        private static void Dialog(Player sender)
        {
            if (!sender.AccountData.HavePerm("admin"))
                return;

            AdminDialog.ShowAdminDialog(sender);

        }

        [Command("goto", "gt")]
        private static void Goto(Player sender, Player target)
        {
            try
            {
                AdminHelper.Goto(sender, target);
                Utils.Chat.AdminChat(sender, "Vous vous êtes téléporté à " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                Utils.Chat.AdminChat(target, Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " s'est téléporté à vous.");
            }
            catch(Exception e)
            {
                Utils.Chat.ErrorChat(sender, "Erreur lors de la téléportation."+e.Message);
            }

            
        }

        [Command("gethere", "gh")]
        private static void Gethere(Player sender, Player target)
        {
            try
            {
                AdminHelper.Gethere(sender, target);
                Utils.Chat.AdminChat(sender, "Vous avez téléporté " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ") à vous.");
                Utils.Chat.AdminChat(target, "Vous avez été téléporté par " + Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + ".");
            }
            catch(Exception e)
            {
                Utils.Chat.ErrorChat(sender, "Erreur lors de la téléportation." + e.Message);
            }

            
        }

        [Command("slap")]
        private static void Slap(Player sender, Player target)
        {
            try
            {
                AdminHelper.Slap(sender, target);
                Utils.Chat.AdminChat(sender, "Vous avez slapé " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                Utils.Chat.AdminChat(target, "Vous avez été slapé par " + Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + ".");
            }
            catch(Exception e)
            {
                Utils.Chat.ErrorChat(sender, "Erreur lors du slap du joueur." + e.Message);
            }

            
        }

        [Command("freeze")]
        private static void Freeze(Player sender, Player target)
        {
            try
            {
                AdminHelper.Freeze(sender, target);
                Utils.Chat.AdminChat(sender, "Vous avez freeze " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                Utils.Chat.AdminChat(target, "Vous avez été freeze par " + Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + ".");
            }
            catch(Exception e)
            {
                Utils.Chat.ErrorChat(sender, "Erreur lors du freeze du joueur." + e.Message);
            }
        }

        [Command("unfreeze")]
        private static void UnFreeze(Player sender, Player target)
        {
            try
            {
                AdminHelper.UnFreeze(sender, target);
                Utils.Chat.AdminChat(sender, "Vous avez défreeze " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                Utils.Chat.AdminChat(target, "Vous avez été défreeze par " + Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + ".");
            }
            catch(Exception e)
            {
                Utils.Chat.ErrorChat(sender, "Erreur lors du unfreeze du joueur." + e.Message);
            }
        }

        [Command("pm", "mp")]
        private static void PrivateMessage(Player sender, Player target, string message)
        {
            try
            {
                AdminHelper.PrivateMessage(sender, target, message);
            }
            catch(Exception e){
                Utils.Chat.ErrorChat(sender, "Erreur lors de l'envoie du PM." + e.Message);
            }
        }

        [CommandGroup("permission", "perm")]
        class Permissions
        {
            [Command("add", "a")]
            private static void Add(Player sender, Player target, string perm)
            {
                try
                {
                    AdminHelper.PermissionAdd(sender, target, perm);
                    Utils.Chat.AdminChat(sender, "Vous avez ajouté la permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\" à " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                    Utils.Chat.AdminChat(target, Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " vous a ajouté la permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\".");
                }
                catch(Exception e)
                {
                    Utils.Chat.ErrorChat(sender, "Erreur lors de l'ajout de la permission." + e.Message);
                }
            }

            [Command("remove", "rm")]
            private static void Remove(Player sender, Player target, string perm)
            {
                try
                {
                    AdminHelper.PermissionRemove(sender, target, perm);
                    Utils.Chat.AdminChat(sender, "Vous avez enlevé la permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\" à " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                    Utils.Chat.AdminChat(target, Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " vous a enlevé la permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\".");
                }
                catch(Exception e)
                {
                    Utils.Chat.ErrorChat(sender, "Erreur lors de la suppression de la permission." + e.Message);
                }
            }

            [Command("list", "ls")]
            private static void Show(Player sender, Player target, string perm = "")
            {
                try
                {
                    AdminHelper.PermissionsShow(sender, target, perm);
                }
                catch(Exception e)
                {
                    Utils.Chat.ErrorChat(sender, "Erreur lors de l'affichage des permissions du joueur." + e.Message);
                }
            }
        }

        [CommandGroup("vehicule", "v")]
        class Vehicule
        {
            [Command("create", "c")]
            private static void Create(Player sender, Player target, VehicleModelType vehicle)
            {
<<<<<<< HEAD
                try
                {
                    int veh = AdminHelper.VehicleCreate(sender, target, vehicle);
                    Utils.Chat.AdminChat(sender, "Vous avez créé un véhicule (" + Constants.Chat.HIGHLIGHT + vehicle + Color.White + ") pour " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                    Utils.Chat.AdminChat(target, Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " vous a créé le véhicule " + Constants.Chat.HIGHLIGHT + vehicle + Color.White + " (id: " + veh + ").");
                }
                catch(Exception e)
                {
                    Utils.Chat.ErrorChat(sender, "Erreur lors de la création du véhicule." + e.Message);
                }
=======
                if (!sender.AccountData.HavePerm("admin.cmds.vehicule.create"))
                    return;

                Utils.Vehicles.CmdHelper.CreateVehicleForPlayer(target, vehicle);
                Utils.Chat.AdminChat(sender, "Vous avez créé un véhicule (" + Constants.Chat.HIGHLIGHT + vehicle + Color.White + ") pour " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                Utils.Chat.AdminChat(target, Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " vous a créé le véhicule " + Constants.Chat.HIGHLIGHT + vehicle + ".");
>>>>>>> vehicles: fix borrowers relationship, new speed calculation
            }

            [Command("spawntmp", "tmp")]
            private static void SpawnTmp(Player sender, VehicleModelType vehicle)
            {
<<<<<<< HEAD
                try
                {
                    AdminHelper.VehicleSpawnTmp(sender, vehicle);
                    Utils.Chat.AdminChat(sender, "Vous avez créé un véhicule temporaire (" + Constants.Chat.HIGHLIGHT + vehicle + Color.White + ").");
                }
                catch(Exception e)
                {
                    Utils.Chat.ErrorChat(sender, "Erreur lors de la création du véhicule temporaire." + e.Message);
                }
=======
                if (!sender.AccountData.HavePerm("admin.cmds.vehicule.spawntmp"))
                    return;

                Utils.Vehicles.CmdHelper.CreateTmpVehicleForPlayer(sender, vehicle);
                Utils.Chat.AdminChat(sender, "Vous avez créé un véhicule temporaire (" + Constants.Chat.HIGHLIGHT + vehicle + Color.White + ").");
>>>>>>> vehicles: fix borrowers relationship, new speed calculation
            }

            [Command("destroy", "del", "rm", "d")]
            private static void Destroy(Player sender, int id = -1)
            {
                try
                {
                    AdminHelper.VehicleDestroy(sender, id);
                    Utils.Chat.AdminChat(sender, "Vous avez détruit le véhicule ID (bdd) " + Constants.Chat.HIGHLIGHT + id + Color.White + ".");
                }
                catch (Exception e)
                {
                    Utils.Chat.AdminChat(sender, "Erreur lors de la suppression du véhicule."+e.Message);
                }
            }

            [Command("tpnearest", "tpn")]
            private static void TpTo(Player sender)
            {
                try
                {
                    Vehicle nearestVeh = AdminHelper.VehicleTpTo(sender);
                    Utils.Chat.AdminChat(sender, "Vous vous êtes téléporté au véhicule ID " + Constants.Chat.HIGHLIGHT + nearestVeh.Id + Color.White + " (bdd : " + nearestVeh.Data.Id + ").");
                }
                catch(Exception e)
                {
                    Utils.Chat.ErrorChat(sender, "Erreur lors de la téléportation du véhicule." + e.Message);
                }
            }

            [Command("heal", "reparer")]
            private static void Heal(Player sender, int id = -1)
            {
                try
                {
                    Vehicle vehicle = AdminHelper.VehicleHeal(sender, id);
                    Utils.Chat.AdminChat(sender, "Vous venez de réparer le véhicule ID " + Constants.Chat.HIGHLIGHT + vehicle.Id + Color.White + " (bdd : " + vehicle.Data.Id + ").");
                }
                catch (Exception e)
                {
                    Utils.Chat.AdminChat(sender, "Erreur lors de la réparation du véhicule."+e.Message);
                }
            }

            [Command("fill", "remplir")]
            private static void Fill(Player sender, int id = -1)
            {
                try
                {
                    Vehicle vehicle = AdminHelper.VehicleFill(sender, id);
                    Utils.Chat.AdminChat(sender, "Vous venez de remplir le véhicule ID " + Constants.Chat.HIGHLIGHT + vehicle.Id + Color.White + " (bdd : " + vehicle.Data.Id + ").");
                }
                catch (Exception e)
                {
                    Utils.Chat.AdminChat(sender, "Erreur lors du remplissage du véhicule." + e.Message);
                }
            }

            [Command("infos", "info", "i")]
            private static void Infos(Player sender, int id = -1)
            {
                try
                {
                    AdminHelper.VehicleInfos(sender, id);
                }
                catch (Exception e)
                {
                    Utils.Chat.AdminChat(sender, "Erreur lors de l'affichage des infos du véhicule." +e.Message);
                }
            }
        }

        [CommandGroup("set")]
        class Set
        {
            [Command("skin")]
            private static void SetSkin(Player sender, Player target, int skinid)
            {
                try
                {
                    AdminHelper.SetSkin(sender, target, skinid);
                    Utils.Chat.AdminChat(sender, "Vous avez mis le skin " + Constants.Chat.HIGHLIGHT + skinid + Color.White + " à " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                    Utils.Chat.AdminChat(target, Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " vous a mis le skin " + Constants.Chat.HIGHLIGHT + skinid + Color.White + ".");
                }
                catch(Exception e)
                {
                    Utils.Chat.AdminChat(sender, "Erreur lors du changement de skin." + e.Message);
                }
            }
        }

        [CommandGroup("give")]
        class GiveItems
        {
            [Command("phone")]
            private static void GivePhone(Player sender, Player target)
            {
                try
                {
                    Phone phone = AdminHelper.GivePhone(sender, target);
                    Chat.AdminChat(sender, "Le téléphone \"" + phone.Number + "\" bien été ajouté à " + target.ActiveCharacter.Name);
                    Chat.InfoChat(target, "L'administrateur " + Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " vous a ajouté un téléphone :\"" + phone.Number + "\".");
                }
                catch(Exception e)
                {
                    Utils.Chat.AdminChat(sender, "Erreur lors de donation du téléphone." + e.Message);
                }
                
            }
            [Command("gun")]
            private static void GiveGun(Player sender, Player target, Weapon gun)
            {

                try
                {
                    AdminHelper.GiveGun(sender, target, gun);
                    Chat.AdminChat(sender, "L'arme " + Color.Aqua + gun + Color.White + " a bien été ajouté au joueur " + target.ActiveCharacter.Name + ".");
                    Chat.InfoChat(target, "L'administrateur " + Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " vous a donné une arme : " + Color.Aqua + gun + Color.White + ".");
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, "L'arme n'a pas pu être donné au joueur." + e.Message);
                }
            }
            [Command("gun")]
            private static void GiveGun(Player sender, Player target, Weapon gun, int ammo)
            {
                try
                {
                    AdminHelper.GiveGun(sender, target, gun, ammo);
                    Chat.AdminChat(sender, "L'arme " + Color.Aqua + gun + Color.White + " a bien été ajouté au joueur " + target.ActiveCharacter.Name + ".");
                    Chat.InfoChat(target, "L'administrateur " + Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " vous a donné une arme : " + Color.Aqua + gun + Color.White + ".");
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, "L'arme n'a pas pu être donné au joueur." + e.Message);
                }
            }
        }
    }
}
