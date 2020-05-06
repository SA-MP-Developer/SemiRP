using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SemiRP.Dialog;
using SemiRP.Models.ItemHeritage;
using SemiRP.Utils;
using SemiRP.Utils.ItemUtils;
using SemiRP.Utils.Vehicles;
using System;

namespace SemiRP.Commands
{
    [CommandGroup("admin", "a")]
    class AdminCommands
    {
        [Command("help", "aide", "h", "a")]
        private static void Help(Player sender)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.help"))
                return;

            Chat.AdminChat(sender, "help, goto, gethere, slap, freeze, unfreeze, pm, kick, perm, v, set, give, delete");
        }

        [Command("dialog", "d")]
        private static void Dialog(Player sender)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.dialog"))
                return;

            AdminDialog.ShowAdminDialog(sender);
        }

        [Command("goto", "gt")]
        private static void Goto(Player sender, Player target)
        {
            try
            {
                AdminHelper.Goto(sender, target);
                Chat.AdminChat(sender, "Vous vous êtes téléporté à " + Constants.Chat.USERNAME + target.Name + Color.White + " (" + target.Id + ").");
                Chat.ClientChat(target, Constants.Chat.USERNAME + sender.AccountData.Nickname + Color.White + " s'est téléporté à vous.");
            }
            catch(Exception e)
            {
                Chat.ErrorChat(sender, e.Message);
            }
        }

        [Command("gethere", "gh")]
        private static void Gethere(Player sender, Player target)
        {
            try
            {
                AdminHelper.Gethere(sender, target);
                Chat.AdminChat(sender, "Vous avez téléporté " + Constants.Chat.USERNAME + target.Name + Color.White + " (" + target.Id + ") à vous.");
                Chat.ClientChat(target, "Vous avez été téléporté par " + Constants.Chat.USERNAME + sender.AccountData.Nickname + Color.White + ".");
            }
            catch(Exception e)
            {
                Chat.ErrorChat(sender, e.Message);
            }
        }

        [Command("slap")]
        private static void Slap(Player sender, Player target)
        {
            try
            {
                AdminHelper.Slap(sender, target);
                Chat.AdminChat(sender, "Vous avez slapé " + Constants.Chat.USERNAME + target.Name + Color.White + " (" + target.Id + ").");
                Chat.ClientChat(target, "Vous avez été slapé par " + Constants.Chat.USERNAME + sender.AccountData.Nickname + Color.White + ".");
            }
            catch(Exception e)
            {
                Chat.ErrorChat(sender, e.Message);
            }
        }

        [Command("freeze")]
        private static void Freeze(Player sender, Player target)
        {
            try
            {
                AdminHelper.Freeze(sender, target);
                Chat.AdminChat(sender, "Vous avez freeze " + Constants.Chat.USERNAME + target.Name + Color.White + " (" + target.Id + ").");
                Chat.ClientChat(target, "Vous avez été freeze par " + Constants.Chat.USERNAME + sender.AccountData.Nickname + Color.White + ".");
            }
            catch(Exception e)
            {
                Chat.ErrorChat(sender, e.Message);
            }
        }

        [Command("unfreeze")]
        private static void UnFreeze(Player sender, Player target)
        {
            try
            {
                AdminHelper.UnFreeze(sender, target);
                Chat.AdminChat(sender, "Vous avez défreeze " + Constants.Chat.USERNAME + target.Name + Color.White + " (" + target.Id + ").");
                Chat.ClientChat(target, "Vous avez été défreeze par " + Constants.Chat.USERNAME + sender.AccountData.Nickname + Color.White + ".");
            }
            catch(Exception e)
            {
                Chat.ErrorChat(sender, e.Message);
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
                Chat.ErrorChat(sender, e.Message);
            }
        }

        [Command("kick", "k")]
        private static void Kick(Player sender, Player target, string message)
        {
            try
            {
                AdminHelper.Kick(sender, target, message);
            }
            catch(Exception e){
                Chat.ErrorChat(sender, e.Message);
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
                    Chat.AdminChat(sender, "Vous avez ajouté la permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\" à " + Constants.Chat.USERNAME + target.Name + Color.White + " (" + target.Id + ").");
                    Chat.ClientChat(target, Constants.Chat.USERNAME + sender.AccountData.Nickname + Color.White + " vous a ajouté la permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\".");
                }
                catch(Exception e)
                {
                    Chat.ErrorChat(sender, e.Message);
                }
            }

            [Command("remove", "rm", "rem")]
            private static void Remove(Player sender, Player target, string perm)
            {
                try
                {
                    AdminHelper.PermissionRemove(sender, target, perm);
                    Chat.AdminChat(sender, "Vous avez enlevé la permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\" à " + Constants.Chat.USERNAME + target.Name + Color.White + " (" + target.Id + ").");
                    Chat.ClientChat(target, Constants.Chat.USERNAME + sender.AccountData.Nickname + Color.White + " vous a enlevé la permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\".");
                }
                catch(Exception e)
                {
                    Chat.ErrorChat(sender, e.Message);
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
                    Chat.ErrorChat(sender, e.Message);
                }
            }
        }

        [CommandGroup("vehicule", "v")]
        class Vehicule
        {
            [Command("create", "c")]
            private static void Create(Player sender, Player target, VehicleModelType vehicle)
            {
                try
                {
                    AdminHelper.VehicleCreate(sender, target, vehicle);
                    Chat.AdminChat(sender, "Véhicule " + Constants.Chat.HIGHLIGHT + vehicle + Color.White + " crée pour " + Constants.Chat.USERNAME + target.Name + Color.White + " (" + target.Id + ").");
                    Chat.ClientChat(target, Constants.Chat.USERNAME + sender.AccountData.Nickname + Color.White + " vous a créé le véhicule " + Constants.Chat.HIGHLIGHT + vehicle + Color.White + ".");
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, e.Message);
                }
            }

            [Command("spawntmp", "tmp")]
            private static void SpawnTmp(Player sender, VehicleModelType vehicle)
            {
                try
                {
                    AdminHelper.VehicleSpawnTmp(sender, vehicle);
                    Chat.AdminChat(sender, "Véhicule temporaire " + Constants.Chat.HIGHLIGHT + vehicle + Color.White + " crée.");
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, e.Message);
                }
            }

            [Command("destroy", "del", "rm", "d")]
            private static void Destroy(Player sender, int id = -1)
            {
                try
                {
                    AdminHelper.VehicleDestroy(sender, id);
                    Chat.AdminChat(sender, "Véhicule (id: " + Constants.Chat.HIGHLIGHT + id + Color.White + ") supprimé.");
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, e.Message);
                }
            }

            [Command("tpnearest", "tpn")]
            private static void TpNearest(Player sender)
            {
                try
                {
                    Vehicle vehicle = AdminHelper.VehicleTpNearest(sender);
                    Chat.AdminChat(sender, "Vous vous êtes téléporté au véhicule id " + Constants.Chat.HIGHLIGHT + vehicle.Id + Color.White + " (bdd: " + Constants.Chat.HIGHLIGHT + vehicle.Data.Id + Color.White + ").");
                }
                catch(Exception e)
                {
                    Chat.ErrorChat(sender, e.Message);
                }
            }
            
            [Command("tp", "goto")]
            private static void TpId(Player sender, int id)
            {
                try
                {
                    Vehicle vehicle = AdminHelper.VehicleTpId(sender, id);
                    Chat.AdminChat(sender, "Vous vous êtes téléporté au véhicule id " + Constants.Chat.HIGHLIGHT + vehicle.Id + Color.White + " (bdd: " + Constants.Chat.HIGHLIGHT + vehicle.Data.Id + Color.White + ").");
                }
                catch(Exception e)
                {
                    Chat.ErrorChat(sender, e.Message);
                }
            }

            [Command("heal", "reparer", "repair")]
            private static void Heal(Player sender, int id = -1)
            {
                try
                {
                    Vehicle vehicle = AdminHelper.VehicleHeal(sender, id);
                    Chat.AdminChat(sender, "Véhicule id " + Constants.Chat.HIGHLIGHT + vehicle.Id + Color.White + " (bdd: " + Constants.Chat.HIGHLIGHT + vehicle.Data.Id + Color.White + ") réparé.");
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, e.Message);
                }
            }

            [Command("fill", "remplir")]
            private static void Fill(Player sender, int id = -1)
            {
                try
                {
                    Vehicle vehicle = AdminHelper.VehicleFill(sender, id);
                    Chat.AdminChat(sender, "Véhicule id " + Constants.Chat.HIGHLIGHT + vehicle.Id + Color.White + " (bdd: " + Constants.Chat.HIGHLIGHT + vehicle.Data.Id + Color.White + ") rempli.");
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, e.Message);
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
                    Chat.ErrorChat(sender, e.Message);
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
                    Chat.AdminChat(sender, "Vous avez mis le skin " + Constants.Chat.HIGHLIGHT + skinid + Color.White + " à " + Constants.Chat.USERNAME + target.Name + Color.White + " (" + target.Id + ").");
                    Chat.ClientChat(target, Constants.Chat.USERNAME + sender.AccountData.Nickname + Color.White + " vous a mis le skin " + Constants.Chat.HIGHLIGHT + skinid + Color.White + ".");
                }
                catch(Exception e)
                {
                    Chat.ErrorChat(sender, e.Message);
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
                    Chat.AdminChat(sender, "Le téléphone \"" + phone.Number + "\" bien été ajouté à " + Constants.Chat.USERNAME + target.ActiveCharacter.Name + Color.White + " (" + target.Id + ").");
                    Chat.ClientChat(target, "L'administrateur " + Constants.Chat.USERNAME + sender.AccountData.Nickname + Color.White + " vous a ajouté un téléphone : " + phone.Number + ".");
                }
                catch(Exception e)
                {
                    Chat.AdminChat(sender, e.Message);
                }
                
            }
            [Command("gun", "weapon")]
            private static void GiveGun(Player sender, Player target, Weapon gun)
            {
                try
                {
                    AdminHelper.GiveGun(sender, target, gun);
                    Chat.AdminChat(sender, "L'arme " + Constants.Chat.HIGHLIGHT + gun + Color.White + " a bien été ajouté au joueur " + Constants.Chat.USERNAME + target.ActiveCharacter.Name + Color.White + " (" + target.Id + ").");
                    Chat.ClientChat(target, Constants.Chat.USERNAME + sender.AccountData.Nickname + Color.White + " vous a donné une arme : " + Constants.Chat.HIGHLIGHT + gun + Color.White + ".");
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, e.Message);
                }
            }

            [Command("gun", "weapon")]
            private static void GiveGun(Player sender, Player target, Weapon gun, int ammo)
            {
                try
                {
                    AdminHelper.GiveGun(sender, target, gun, ammo);
                    Chat.AdminChat(sender, "L'arme " + Constants.Chat.HIGHLIGHT + gun + Color.White + " a bien été ajouté au joueur " + Constants.Chat.USERNAME + target.ActiveCharacter.Name + Color.White + " (" + target.Id + ").");
                    Chat.ClientChat(target, Constants.Chat.USERNAME + sender.AccountData.Nickname + Color.White + " vous a donné une arme : " + Constants.Chat.HIGHLIGHT + gun + Color.White + ".");
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, e.Message);
                }
            }
        }

        [CommandGroup("delete")]
        class DeleteItems
        {
            [Command("hand")]
            private static void DeleteItemInHand(Player sender, Player target)
            {
                try
                {
                    if(target.ActiveCharacter.ItemInHand == null)
                    {
                        throw new Exception("Le joueur n'a pas d'objet en main.");
                    }
                    ItemHelper.DeleteItem(target.ActiveCharacter.ItemInHand);
                    Chat.AdminChat(sender, "L'objet de la main du joueur " + Constants.Chat.USERNAME + target.ActiveCharacter.Name + Color.White + " a bien été supprimé");
                    Chat.ClientChat(target, Constants.Chat.USERNAME + sender.AccountData.Nickname + Color.White + " a supprimé l'objet que vous aviez dans la main.");
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, e.Message);
                }
            }
        }
    }
}
