using Microsoft.EntityFrameworkCore.Query.Internal;
using SampSharp.GameMode;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SemiRP.Models;
using SemiRP.Models.ItemHeritage;
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
            if (!sender.AccountData.HavePerm("admin.cmd.help"))
                return;

            Utils.Chat.AdminChat(sender, "/help /goto /gethere /slap /pm /perm /give");
            
        }

        [Command("goto", "gt")]
        private static void Goto(Player sender, Player target)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.goto"))
                return;

            var tppos = target.Position;
            if (sender.InAnyVehicle)
                sender.Vehicle.Position = new Vector3(tppos.X + 2.5f, tppos.Y, tppos.Z);
            else
                sender.Position = new Vector3(tppos.X + 1f, tppos.Y, tppos.Z);

            Utils.Chat.AdminChat(sender, "Vous vous êtes téléporté à " + Color.Red + target.Name + Color.White + " (" + target.Id + ").");
            Utils.Chat.AdminChat(target, Color.Red + sender.AccountData.Nickname + Color.White + " s'est téléporté à vous.");
        }

        [Command("gethere", "gh")]
        private static void Gethere(Player sender, Player target)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.gethere"))
                return;

            var tppos = sender.Position;
            if (target.InAnyVehicle)
                target.Vehicle.Position = new Vector3(tppos.X + 2.5f, tppos.Y, tppos.Z);
            else
                target.Position = new Vector3(tppos.X + 1f, tppos.Y, tppos.Z);

            Utils.Chat.AdminChat(sender, "Vous avez téléporté " + Color.Red + target.Name + Color.White + " (" + target.Id + ") à vous.");
            Utils.Chat.AdminChat(target, "Vous avez été téléporté par " + Color.Red + sender.AccountData.Nickname + Color.White + ".");
        }

        [Command("slap")]
        private static void Slap(Player sender, Player target)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.slap"))
                return;

            target.Position = new Vector3(target.Position.X, target.Position.Y, target.Position.Z + 10f);

            Utils.Chat.AdminChat(sender, "Vous avez slapé " + Color.Red + target.Name + Color.White + " (" + target.Id + ").");
            Utils.Chat.AdminChat(target, "Vous avez été slapé par " + Color.Red  + sender.AccountData.Nickname + Color.White + ".");
        }

        [Command("freeze")]
        private static void Freeze(Player sender, Player target)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.freeze"))
                return;

            target.ToggleControllable(false);

            Utils.Chat.AdminChat(sender, "Vous avez freeze " + Color.Red + target.Name + Color.White + " (" + target.Id + ").");
            Utils.Chat.AdminChat(target, "Vous avez été freeze par " + Color.Red + sender.AccountData.Nickname + Color.White + ".");
        }

        [Command("unfreeze")]
        private static void UnFreeze(Player sender, Player target)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.freeze"))
                return;

            target.ToggleControllable(false);

            Utils.Chat.AdminChat(sender, "Vous avez freeze " + Color.Red + target.Name + Color.White + " (" + target.Id + ").");
            Utils.Chat.AdminChat(target, "Vous avez été freeze par " + Color.Red + sender.AccountData.Nickname + Color.White + ".");
        }

        [Command("pm", "mp")]
        private static void PrivateMessage(Player sender, Player target, string message)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.pm"))
                return;

            Utils.Chat.AdminChat(sender, Color.Yellow + "[PM] " + Color.White + sender.AccountData.Nickname + " : " + message);
            Utils.Chat.AdminChat(target, Color.Yellow + "[PM] " + Color.White + sender.AccountData.Nickname + " : " + message);
        }

        [CommandGroup("permission", "perm")]
        class Permissions
        {
            [Command("add", "a")]
            private static void Add(Player sender, Player target, string perm)
            {
                //if (!sender.AccountData.HavePerm("admin.cmds.perms.add"))
                //    return;

                var ret = Utils.Permissions.AddPerm(target.AccountData.PermsSet, perm);
                if (ret == 1)
                {
                    Utils.Chat.AdminChat(sender, "La permission \"" + perm + "\" n'éxiste pas.");
                    return;
                }
                else if (ret == 2)
                {
                    Utils.Chat.AdminChat(sender, "La permission \"" + perm + "\" est déjà attribuée à " + Color.Red + target.Name + Color.White + " (" + target.Id + ").");
                    return;
                }

                Utils.Chat.AdminChat(sender, "Vous avez ajouté la permission \"" + perm + "\" à " + Color.Red + target.Name + Color.White + " (" + target.Id + ").");
                Utils.Chat.AdminChat(target, Color.Red + sender.AccountData.Nickname + Color.White + " vous a ajouté la permission \"" + perm + "\".");
            }

            [Command("remove", "rm")]
            private static void Remove(Player sender, Player target, string perm)
            {
                if (!sender.AccountData.HavePerm("admin.cmds.perms.remove"))
                    return;

                var ret = Utils.Permissions.RemovePerm(target.AccountData.PermsSet, perm);
                if (ret == 1)
                {
                    Utils.Chat.AdminChat(sender, "La permission \"" + perm + "\" n'éxiste pas.");
                    return;
                }
                else if(ret == 2)
                {
                    Utils.Chat.AdminChat(sender, "La permission \"" + perm + "\" n'est pas attribuée à " + Color.Red + target.Name + Color.White + " (" + target.Id + ").");
                    return;
                }

                Utils.Chat.AdminChat(sender, "Vous avez enlevé la permission \"" + perm + "\" à " + Color.Red + target.Name + Color.White + " (" + target.Id + ").");
                Utils.Chat.AdminChat(target, Color.Red + sender.AccountData.Nickname + Color.White + " vous a enlevé la permission \"" + perm + "\".");
            }

            [Command("list", "ls")]
            private static void Show(Player sender, Player target, string perm = "")
            {
                if (!sender.AccountData.HavePerm("admin.cmds.perms.list"))
                    return;

                List<string> permsStrings;

                if (perm == "")
                {
                    permsStrings = Utils.Permissions.ListAccountPerms(target.AccountData);
                    Utils.Chat.AdminChat(sender, "Permissions de " + Color.Red + target.Name + Color.White + " (" + target.Id + ") :");
                }
                else
                {
                    permsStrings = Utils.Permissions.ListAccountPermChildren(target.AccountData, perm);
                    Utils.Chat.AdminChat(sender, "Permissions de " + Color.Red + target.Name + Color.White + " (" + target.Id + ") à partir de \"" + perm + "\" :");
                }

                foreach (string permname in permsStrings)
                {
                    sender.SendClientMessage(permname);
                }
            }
        }

        [CommandGroup("set")]
        class Set
        {
            [Command("skin")]
            private static void SetSkin(Player sender, Player target, int skinid)
            {
                if (!sender.AccountData.HavePerm("admin.cmds.set.skin"))
                    return;

                if (skinid < 0 || 311 < skinid)
                {
                    Utils.Chat.AdminChat(sender, "Le skin ID " + skinid + " n'est pas valide, l'id d'un skin doit être entre 0 et 311.");
                }

                target.ActiveCharacter.Skin = (uint)skinid;
                target.Skin = skinid;

                Utils.Chat.AdminChat(sender, "Vous avez mis le skin " + skinid + " à " + Color.Red + target.Name + Color.White + " (" + target.Id + ").");
                Utils.Chat.AdminChat(target, Color.Red + sender.AccountData.Nickname + Color.White + " vous a mis le skin " + skinid + ".");
            }
        }

        [CommandGroup("give")]
        class GiveItems
        {
            [Command("phone")]
            private static void GiveObject(Player sender, Player target)
            {
                if (!sender.AccountData.HavePerm("admin.cmds.give.phone"))
                    return;
                Phone phone = PhoneHelper.CreatePhone();
                InventoryHelper.AddItemToCharacter(target.ActiveCharacter, phone);
                
            }
        }
    }
}
