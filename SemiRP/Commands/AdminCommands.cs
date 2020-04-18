﻿using Microsoft.EntityFrameworkCore.Query.Internal;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;
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

            Utils.Chat.AdminChat(sender, "Vous vous êtes téléporté à " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
            Utils.Chat.AdminChat(target, Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " s'est téléporté à vous.");
        }

        [Command("gethere", "gh")]
        private static void Gethere(Player sender, Player target)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.gethere"))
                return;

            var tppos = sender.Position;
            if (target.InAnyVehicle)
            {
                target.Vehicle.Position = new Vector3(tppos.X + 2.5f, tppos.Y, tppos.Z);
            }
            else
                target.Position = new Vector3(tppos.X + 1f, tppos.Y, tppos.Z);

            Utils.Chat.AdminChat(sender, "Vous avez téléporté " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ") à vous.");
            Utils.Chat.AdminChat(target, "Vous avez été téléporté par " + Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + ".");
        }

        [Command("slap")]
        private static void Slap(Player sender, Player target)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.slap"))
                return;

            target.Position = new Vector3(target.Position.X, target.Position.Y, target.Position.Z + 10f);

            Utils.Chat.AdminChat(sender, "Vous avez slapé " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
            Utils.Chat.AdminChat(target, "Vous avez été slapé par " + Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + ".");
        }

        [Command("freeze")]
        private static void Freeze(Player sender, Player target)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.freeze"))
                return;

            target.ToggleControllable(false);

            Utils.Chat.AdminChat(sender, "Vous avez freeze " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
            Utils.Chat.AdminChat(target, "Vous avez été freeze par " + Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + ".");
        }

        [Command("unfreeze")]
        private static void UnFreeze(Player sender, Player target)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.freeze"))
                return;

            target.ToggleControllable(true);

            Utils.Chat.AdminChat(sender, "Vous avez défreeze " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
            Utils.Chat.AdminChat(target, "Vous avez été défreeze par " + Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + ".");
        }

        [Command("pm", "mp")]
        private static void PrivateMessage(Player sender, Player target, string message)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.pm"))
                return;

            Utils.Chat.AdminChat(sender, Constants.Chat.ADMIN_PM + "[PM] "+ sender.AccountData.Nickname + " : " + message);
            Utils.Chat.AdminChat(target, Constants.Chat.ADMIN_PM + "[PM] " + sender.AccountData.Nickname + " : " + message);
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
                    Utils.Chat.AdminChat(sender, "La permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\" n'éxiste pas.");
                    return;
                }
                else if (ret == 2)
                {
                    Utils.Chat.AdminChat(sender, "La permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\" est déjà attribuée à " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                    return;
                }

                Utils.Chat.AdminChat(sender, "Vous avez ajouté la permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\" à " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                Utils.Chat.AdminChat(target, Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " vous a ajouté la permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\".");
            }

            [Command("remove", "rm")]
            private static void Remove(Player sender, Player target, string perm)
            {
                if (!sender.AccountData.HavePerm("admin.cmds.perms.remove"))
                    return;

                var ret = Utils.Permissions.RemovePerm(target.AccountData.PermsSet, perm);
                if (ret == 1)
                {
                    Utils.Chat.AdminChat(sender, "La permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\" n'éxiste pas.");
                    return;
                }
                else if(ret == 2)
                {
                    Utils.Chat.AdminChat(sender, "La permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\" n'est pas attribuée à " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                    return;
                }

                Utils.Chat.AdminChat(sender, "Vous avez enlevé la permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\" à " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                Utils.Chat.AdminChat(target, Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " vous a enlevé la permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\".");
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

        [CommandGroup("vehicule", "v")]
        class Vehicule
        {
            [Command("create")]
            private static void Create(Player sender, BaseVehicle vehicle)
            {
                if (!sender.AccountData.HavePerm("admin.cmds.vehicule.create"))
                    return;
            }

            [Command("spawntmp", "tmp")]
            private static void SpawnTmp(Player sender, VehicleModelType vehicule)
            {
                if (!sender.AccountData.HavePerm("admin.cmds.vehicule.spawntmp"))
                    return;

                var veh = BaseVehicle.Create(vehicule, sender.Position, 0.0f, 0, 0);
                sender.PutInVehicle(veh);
                Utils.Chat.AdminChat(sender, "Vous avez créé un véhicule temporaire (" + Constants.Chat.HIGHLIGHT + vehicule + Color.White + ").");
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
                    Utils.Chat.AdminChat(sender, "Le skin ID " + Constants.Chat.HIGHLIGHT + skinid + Color.White + " n'est pas valide, l'id d'un skin doit être entre 0 et 311.");
                }

                target.ActiveCharacter.Skin = (uint)skinid;
                target.Skin = skinid;

                Utils.Chat.AdminChat(sender, "Vous avez mis le skin " + Constants.Chat.HIGHLIGHT + skinid + Color.White + " à " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                Utils.Chat.AdminChat(target, Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " vous a mis le skin " + Constants.Chat.HIGHLIGHT + skinid + Color.White + ".");
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
                if(Utils.ItemUtils.PhoneHelper.GetDefaultPhone(target.ActiveCharacter) == null)
                {
                    phone.DefaultPhone = true;
                }
                
                if(InventoryHelper.AddItemToCharacter(target.ActiveCharacter, phone))
                {
                    Chat.AdminChat(sender, "Le téléphone \"" + phone.Number + "\" bien été ajouté à " + target.ActiveCharacter.Name);
                    Chat.InfoChat(target, "L'administrateur " + Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " vous a ajouté un téléphone :\"" + phone.Number + "\".");
                    ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
                    dbContext.SaveChanges();
                }
                else
                {
                    Chat.ErrorChat(sender, "Le téléphone n'a pas pu être ajouté à l'utilisateur.");
                    PhoneHelper.DeletePhone(phone);
                }
            }
        }
    }
}
