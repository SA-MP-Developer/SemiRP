using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
                target.Position = new Vector3(tppos.X, tppos.Y, tppos.Z + 1f);

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
                if (!sender.AccountData.HavePerm("admin.cmds.perms.add"))
                    return;

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
            [Command("create", "c")]
            private static void Create(Player sender, Player target, VehicleModelType vehicle)
            {
                if (!sender.AccountData.HavePerm("admin.cmds.vehicule.create"))
                    return;

                var veh = Utils.Vehicles.Helper.CreateVehicle(target.ActiveCharacter, Utils.Vehicles.ModelHelper.ModelForModelType(vehicle), target.Position, target.Angle);
                Utils.Chat.AdminChat(sender, "Vous avez créé un véhicule (" + Constants.Chat.HIGHLIGHT + vehicle + Color.White + ") pour " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                Utils.Chat.AdminChat(target, Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " vous a créé le véhicule " + Constants.Chat.HIGHLIGHT + vehicle + Color.White + " (id: " + veh.Data.Id + ").");
            }

            [Command("spawntmp", "tmp")]
            private static void SpawnTmp(Player sender, VehicleModelType vehicle)
            {
                if (!sender.AccountData.HavePerm("admin.cmds.vehicule.spawntmp"))
                    return;

                var veh = Utils.Vehicles.Helper.CreateVehicle(sender.ActiveCharacter, Utils.Vehicles.ModelHelper.ModelForModelType(vehicle), sender.Position, sender.Angle, VehicleColor.BrighRed, VehicleColor.BrighRed, true);
                veh.Data.FuelConsumption = 0;
                sender.PutInVehicle(veh);
                veh.Engine = true;
                Utils.Chat.AdminChat(sender, "Vous avez créé un véhicule temporaire (" + Constants.Chat.HIGHLIGHT + vehicle + Color.White + ").");
            }

            [Command("destroy", "del", "rm", "d")]
            private static void Destroy(Player sender, int vehicle = -1)
            {
                if (!sender.AccountData.HavePerm("admin.cmds.vehicule.destroy"))
                    return;

                if (vehicle == -1)
                {
                    if (!sender.InAnyVehicle)
                    {
                        Utils.Chat.AdminChat(sender, "Vous n'êtes pas dans un véhicule !");
                        return;
                    }

                    if (sender.Vehicle == null)
                    {
                        Utils.Chat.AdminChat(sender, "Ce véhicule n'est pas géré par le GameMode.");
                        return;
                    }

                    Vehicle veh = (Vehicle)sender.Vehicle;

                    if (!Utils.Vehicles.Helper.DestroyVehicle(veh))
                    {
                        Utils.Chat.AdminChat(sender, "Un problème est survenu lors de la destruction du véhicule.");
                        return;
                    }

                    Utils.Chat.AdminChat(sender, "Vous avez détruit le véhicule.");
                    return;
                }

                ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;

                if (!dbContext.Vehicles.Any(v => v.Id == vehicle))
                {
                    Utils.Chat.AdminChat(sender, "Le vehicule ID " + Constants.Chat.HIGHLIGHT + vehicle + Color.White + " n'éxiste pas.");
                    return;
                }

                if (!Utils.Vehicles.Helper.DestroyVehicle(vehicle))
                {
                    Utils.Chat.AdminChat(sender, "Un problème est survenu lors de la destruction du véhicule ID (bdd) " + Constants.Chat.HIGHLIGHT + vehicle + Color.White + ".");
                    return;
                }

                Utils.Chat.AdminChat(sender, "Vous avez détruit le véhicule ID (bdd) " + Constants.Chat.HIGHLIGHT + vehicle + Color.White + ".");
            }

            [Command("tpnearest", "tpn")]
            private static void TpTo(Player sender)
            {
                if (!sender.AccountData.HavePerm("admin.cmds.vehicule.tp"))
                    return;

                var nearestVeh = Utils.Vehicles.Helper.GetNearestVehicle(sender);

                if (nearestVeh == null)
                    return;

                if (nearestVeh.Driver != null)
                    sender.Position = new Vector3(nearestVeh.Position.X, nearestVeh.Position.Y, nearestVeh.Position.Z + 1f);
                else
                    sender.PutInVehicle(nearestVeh);
                Utils.Chat.AdminChat(sender, "Vous vous êtes téléporté au véhicule ID " + Constants.Chat.HIGHLIGHT + nearestVeh.Id + Color.White + " (bdd : " + nearestVeh.Data.Id + ").");
            }

            [Command("infos", "info", "i")]
            private static void Infos(Player sender, int id = -1)
            {
                if (!sender.AccountData.HavePerm("admin.cmds.vehicule.infos"))
                    return;

                if (sender.Vehicle == null && id == -1)
                {
                    Utils.Chat.AdminChat(sender, "Vous n'avez pas choisi de véhicule.");
                    return;
                }

                Vehicle vehicle = null;

                if (id != -1)
                {
                    if (!Vehicle.All.Any(v => v.Id == id))
                    {
                        Utils.Chat.AdminChat(sender, "Ce véhicule n'éxiste pas.");
                        return;
                    }
                    vehicle = (Vehicle)Vehicle.All.Where(v => v.Id == id).First();
                }
                else
                {
                    if (sender.Vehicle == null)
                    {
                        Utils.Chat.AdminChat(sender, "Vous n'êtes pas dans un véhicule.");
                        return;
                    }
                    vehicle = (Vehicle)sender.Vehicle;
                }

                Utils.Chat.AdminChat(sender, "==== Informations du véhicule ID " + vehicle.Id + " ====");
                Utils.Chat.AdminChat(sender, "ID BDD : " + Constants.Chat.HIGHLIGHT + vehicle.Data.Id);
                Utils.Chat.AdminChat(sender, "Position (x,y,z) : ("
                    + Constants.Chat.HIGHLIGHT + vehicle.Position.X + Color.White + ","
                    + Constants.Chat.HIGHLIGHT + vehicle.Position.Y + Color.White + ","
                    + Constants.Chat.HIGHLIGHT + vehicle.Position.Z + Color.White + ")");

                Utils.Chat.AdminChat(sender, "Model : " + Constants.Chat.HIGHLIGHT + vehicle.ModelInfo.Name);
                Utils.Chat.AdminChat(sender, "Color 1 : " + Constants.Chat.HIGHLIGHT + vehicle.Data.Color1 + Color.White + ", Color 2 : " + Constants.Chat.HIGHLIGHT + vehicle.Data.Color2);

                var ownerPlayer = Utils.PlayerUtils.PlayerHelper.SearchCharacter(vehicle.Data.Owner);
                if (ownerPlayer == null)
                    Utils.Chat.AdminChat(sender, "Owner : " + Constants.Chat.HIGHLIGHT + vehicle.Data.Owner.Name + "(Compte : " + vehicle.Data.Owner.Account.Id + ")");
                else
                    Utils.Chat.AdminChat(sender, "Owner : " + Constants.Chat.HIGHLIGHT + vehicle.Data.Owner.Name + "(Compte : " + vehicle.Data.Owner.Account.Id + ") (connecté, id : " +  ownerPlayer.Id + ")");

                foreach (Character borrower in vehicle.Data.Borrowers)
                {
                    var borrowerPlayer = Utils.PlayerUtils.PlayerHelper.SearchCharacter(borrower);
                    if (borrowerPlayer == null)
                        Utils.Chat.AdminChat(sender, "Borrower : " + Constants.Chat.HIGHLIGHT + borrower.Name + "(Compte : " + borrower.Account.Id + ")");
                    else
                        Utils.Chat.AdminChat(sender, "Borrower : " + Constants.Chat.HIGHLIGHT + borrower.Name + "(Compte : " + borrower.Account.Id + ") (connecté, id : " + borrowerPlayer.Id + ")");
                }

                Utils.Chat.AdminChat(sender, "Health : " + Constants.Chat.HIGHLIGHT + vehicle.Health);
                Utils.Chat.AdminChat(sender, "Fuel : " + Constants.Chat.HIGHLIGHT + vehicle.Data.Fuel.ToString("0.00") + Color.White + " | Fuel Tank : " + Constants.Chat.HIGHLIGHT + vehicle.Data.MaxFuel.ToString("0.0") + Color.White + " | Fuel Cons. : " + Constants.Chat.HIGHLIGHT + vehicle.Data.FuelConsumption.ToString("0.0"));
                Utils.Chat.AdminChat(sender, "Mileage : " + Constants.Chat.HIGHLIGHT + vehicle.Data.Mileage.ToString("0.000"));
                Utils.Chat.AdminChat(sender, "Current Speed : " + Constants.Chat.HIGHLIGHT + vehicle.Speed);
                Utils.Chat.AdminChat(sender, "Locked : " + Constants.Chat.HIGHLIGHT + (vehicle.Locked ? "YES" : "NO"));
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
                    return;
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
            [Command("gun")]
            private static void GiveGun(Player sender, Player target, int idGun)
            {
                if (!sender.AccountData.HavePerm("admin.cmds.give.gun"))
                    return;

                try
                {
                    target.GiveWeapon((Weapon)idGun, 500);
                    Chat.AdminChat(sender, "L'arme " + Color.Aqua + Data.Weapons.WeaponsDictionnary.GetValueOrDefault(idGun) + Color.White + " a bien été ajouté au joueur " + target.ActiveCharacter.Name + ".");
                    Chat.InfoChat(target, "L'administrateur " + Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " vous a donné une arme : " + Color.Aqua + Data.Weapons.WeaponsDictionnary.GetValueOrDefault(idGun) + Color.White + ".");
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, "L'arme n'a pas pu être donné au joueur.");
                }
            }
            [Command("gun")]
            private static void GiveGun(Player sender, Player target, int idGun, int ammo)
            {
                if (!sender.AccountData.HavePerm("admin.cmds.give.gun"))
                    return;
                try
                {
                    target.GiveWeapon((Weapon)idGun, ammo);
                    Chat.AdminChat(sender, "L'arme "+Color.Aqua+Data.Weapons.WeaponsDictionnary.GetValueOrDefault(idGun)+Color.White+" avec "+ Color.Aqua + ammo + Color.White + " munitions a bien été ajouté au joueur "+target.ActiveCharacter.Name+".");
                    Chat.InfoChat(target, "L'administrateur " + Constants.Chat.HIGHLIGHT + sender.AccountData.Nickname + Color.White + " vous a donné une arme : " + Color.Aqua + Data.Weapons.WeaponsDictionnary.GetValueOrDefault(idGun) + Color.White + ".");
                }
                catch (Exception e)
                {
                    Chat.ErrorChat(sender, "L'arme n'a pas pu être donné au joueur.");
                }
                
            }
        }
    }
}
