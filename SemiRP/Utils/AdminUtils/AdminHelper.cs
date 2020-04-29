using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SemiRP.Data;
using SemiRP.Models;
using SemiRP.Models.ItemHeritage;
using SemiRP.Utils.ContainerUtils;
using SemiRP.Utils.ItemUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Utils
{
    public class AdminHelper
    {
        public static void Goto(Player sender, Player target)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.goto"))
                throw new Exception();

            var tppos = target.Position;
            if (sender.InAnyVehicle)
                sender.Vehicle.Position = new Vector3(tppos.X + 2.5f, tppos.Y, tppos.Z);
            else
                sender.Position = new Vector3(tppos.X + 1f, tppos.Y, tppos.Z);
        }

        public static void Gethere(Player sender, Player target)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.gethere"))
                throw new Exception();

            var tppos = sender.Position;
            if (target.InAnyVehicle)
            {
                target.Vehicle.Position = new Vector3(tppos.X + 2.5f, tppos.Y, tppos.Z);
            }
            else
                target.Position = new Vector3(tppos.X, tppos.Y, tppos.Z + 1f);
        }
        public static void Slap(Player sender, Player target)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.slap"))
                throw new Exception();

            target.Position = new Vector3(target.Position.X, target.Position.Y, target.Position.Z + 10f);
        }
        public static void Freeze(Player sender, Player target)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.freeze"))
                throw new Exception();

            target.ToggleControllable(false);
        }
        public static void UnFreeze(Player sender, Player target)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.freeze"))
                throw new Exception();

            target.ToggleControllable(true);
        }
        public static void PrivateMessage(Player sender, Player target, string message)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.pm"))
                throw new Exception();
            Utils.Chat.AdminChat(sender, Constants.Chat.ADMIN_PM + "[PM] " + sender.AccountData.Nickname + " : " + message);
            Utils.Chat.AdminChat(target, Constants.Chat.ADMIN_PM + "[PM] " + sender.AccountData.Nickname + " : " + message);
        }

        public static void PermissionAdd(Player sender, Player target, string perm)
        {
            //if (!sender.AccountData.HavePerm("admin.cmds.perms.add"))
            //    throw new Exception();

            var ret = Utils.Permissions.AddPerm(target.AccountData.PermsSet, perm);
            if (ret == 1)
            {
                Utils.Chat.AdminChat(sender, "La permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\" n'éxiste pas.");
                throw new Exception();
            }
            else if (ret == 2)
            {
                Utils.Chat.AdminChat(sender, "La permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\" est déjà attribuée à " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                throw new Exception();
            }
        }
        public static void PermissionRemove(Player sender, Player target, string perm)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.perms.remove"))
                throw new Exception();

            var ret = Utils.Permissions.RemovePerm(target.AccountData.PermsSet, perm);
            if (ret == 1)
            {
                Utils.Chat.AdminChat(sender, "La permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\" n'éxiste pas.");
                throw new Exception();
            }
            else if (ret == 2)
            {
                Utils.Chat.AdminChat(sender, "La permission \"" + Constants.Chat.HIGHLIGHT + perm + Color.White + "\" n'est pas attribuée à " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + " (" + target.Id + ").");
                throw new Exception();
            }
        }
        public static void PermissionsShow(Player sender, Player target, string perm = "")
        {
            if (!sender.AccountData.HavePerm("admin.cmds.perms.list"))
                throw new Exception();

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
        public static int VehicleCreate(Player sender, Player target, VehicleModelType vehicle)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.vehicule.create"))
                throw new Exception();

            var veh = Utils.Vehicles.Helper.CreateVehicle(target.ActiveCharacter, Utils.Vehicles.ModelHelper.ModelForModelType(vehicle), target.Position, target.Angle);
            return veh.Id;
        }
        public static void VehicleSpawnTmp(Player sender, VehicleModelType vehicle)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.vehicule.spawntmp"))
                throw new Exception();

            var veh = Utils.Vehicles.Helper.CreateVehicle(sender.ActiveCharacter, Utils.Vehicles.ModelHelper.ModelForModelType(vehicle), sender.Position, sender.Angle, VehicleColor.BrighRed, VehicleColor.BrighRed, true);
            veh.Data.FuelConsumption = 0;
            sender.PutInVehicle(veh);
            veh.Engine = true;
        }
        public static void VehicleDestroy(Player sender, int id = -1)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.vehicule.destroy"))
                throw new Exception();

            
            Vehicle vehicle = Utils.Vehicles.CmdHelper.GetCurrentVehicleOrID(sender, id);

            Utils.Vehicles.Helper.DestroyVehicle(vehicle);
        }
        public static Vehicle VehicleTpTo(Player sender)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.vehicule.tp"))
                throw new Exception();

            var nearestVeh = Utils.Vehicles.Helper.GetNearestVehicle(sender);

            if (nearestVeh == null)
                throw new Exception();

            if (nearestVeh.Driver != null)
                sender.Position = new Vector3(nearestVeh.Position.X, nearestVeh.Position.Y, nearestVeh.Position.Z + 1f);
            else
                sender.PutInVehicle(nearestVeh);

            return nearestVeh;
        }
        public static Vehicle VehicleHeal(Player sender, int id = -1)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.vehicule.heal"))
                throw new Exception();

            
            Vehicle vehicle = Utils.Vehicles.CmdHelper.GetCurrentVehicleOrID(sender, id);
            vehicle.Health = 1000;
            vehicle.Data.Dammages = 1000;

            return vehicle;
        }
        public static Vehicle VehicleFill(Player sender, int id = -1)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.vehicule.fill"))
                throw new Exception();
            Vehicle vehicle = Utils.Vehicles.CmdHelper.GetCurrentVehicleOrID(sender, id);
            vehicle.Data.Fuel = vehicle.Data.MaxFuel;
            return vehicle;
        }
        public static void VehicleInfos(Player sender, int id = -1)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.vehicule.infos"))
                throw new Exception();

            Vehicle veh = Utils.Vehicles.CmdHelper.GetCurrentVehicleOrID(sender, id);
            var strings = Utils.Vehicles.CmdHelper.DisplayAdminInfos(veh);

            Utils.Chat.AdminChat(sender, "======= Infos du véhicule =======");
            foreach (string line in strings)
            {
                Utils.Chat.AdminChat(sender, line);
            }
            
        }
        public static void SetSkin(Player sender, Player target, int skinid)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.set.skin"))
                throw new Exception();

            if (skinid < 0 || 311 < skinid)
            {
                throw new Exception("Le skin ID " + Constants.Chat.HIGHLIGHT + skinid + Color.White + " n'est pas valide, l'id d'un skin doit être entre 0 et 311.");

            }

            target.ActiveCharacter.Skin = (uint)skinid;
            target.Skin = skinid;

            
        }
        public static Phone GivePhone(Player sender, Player target)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.give.phone"))
                throw new Exception();
            Phone phone = PhoneHelper.CreatePhone();
            if (Utils.ItemUtils.PhoneHelper.GetDefaultPhone(target.ActiveCharacter) == null)
            {
                phone.DefaultPhone = true;
            }

            if (InventoryHelper.AddItemToCharacter(target.ActiveCharacter, phone))
            {
                ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
                dbContext.SaveChanges();
                return phone;
            }
            else
            {
                PhoneHelper.DeletePhone(phone);
                throw new Exception("Le téléphone n'a pas pu être ajouté à l'utilisateur.");
            }
        }
        public static void GiveGun(Player sender, Player target, Weapon gun)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.give.gun"))
                throw new Exception("Tu n'as pas la permission.");
            if (target.ActiveCharacter.ItemInHand != null)
                throw new Exception("La main du joueur doit être vide.");

            Gun weapon = new Gun(); // Create Gun
            weapon.idWeapon = gun;
            weapon.Quantity = 500;
            weapon.CurrentContainer = null;
            weapon.SpawnLocation = null;
            weapon.Name = Weapons.WeaponsDictionnary.GetValueOrDefault((int) gun);
            weapon.ModelId = WeaponsModelId.WeaponsModelIdDictionnary.GetValueOrDefault((int)gun);
            target.ActiveCharacter.ItemInHand = weapon;
            target.GiveWeapon(gun, 500);
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            dbContext.SaveChanges();
        }
        public static void GiveGun(Player sender, Player target, Weapon gun, int ammo)
        {
            if (!sender.AccountData.HavePerm("admin.cmds.give.gun"))
                throw new Exception();
            if (target.ActiveCharacter.ItemInHand != null)
                throw new Exception("La main du joueur doit être vide.");

            Gun weapon = new Gun(); // Create Gun
            weapon.idWeapon = gun;
            weapon.Quantity = ammo;
            weapon.CurrentContainer = null;
            weapon.SpawnLocation = null;
            weapon.Name = Weapons.WeaponsDictionnary.GetValueOrDefault((int)gun);
            weapon.ModelId = WeaponsModelId.WeaponsModelIdDictionnary.GetValueOrDefault((int)gun);
            target.ActiveCharacter.ItemInHand = weapon;
            target.GiveWeapon(gun, ammo);
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            dbContext.SaveChanges();
        }
    }
}
