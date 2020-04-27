using Microsoft.EntityFrameworkCore.Internal;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;
using SemiRP.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;

namespace SemiRP.Utils.Vehicles
{
    public class Helper
    {
        public static Vehicle CreateVehicle(Character owner, VehicleModel model, Vector3 position, float rotation, VehicleColor color1 = VehicleColor.White, VehicleColor color2 = VehicleColor.White, bool temp = false)
        {
            try
            {
                ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;

                var dataVeh = new Models.VehicleData();

                dataVeh.Type = model;
                dataVeh.Color1 = color1;
                dataVeh.Color2 = color2;
                dataVeh.SpawnLocation = new SpawnLocation(position, rotation);

                dataVeh.MaxFuel = model.MaxFuel;
                dataVeh.FuelConsumption = model.FuelConsumption;
                dataVeh.Fuel = model.MaxFuel;
                dataVeh.Dammages = 1000f;

                dataVeh.Owner = owner;
                dataVeh.Container = new Container(10);

                dataVeh.Temporary = temp;

                if (!temp)
                {
                    dbContext.Add(dataVeh);
                    dbContext.SaveChanges();
                }
                Vehicle veh = (Vehicle)Vehicle.Create(model.Model, position, rotation, (int)color1, (int)color2);
                veh.Data = dataVeh;

                return veh;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void DestroyVehicle(int vehicle)
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;

            if (!dbContext.Vehicles.Any(v => v.Id == vehicle))
                throw new Exception("La destruction du véhicule ID " + vehicle + " a échouée.");

            var veh = dbContext.Vehicles.Single(v => v.Id == vehicle);
            dbContext.Vehicles.Remove(veh);
            dbContext.SaveChanges();
        }

        public static void DestroyVehicle(Vehicle vehicle)
        {
            vehicle.Dispose();

            if (!vehicle.Data.Temporary)
                Utils.Vehicles.Helper.DestroyVehicle(vehicle.Data.Id);
        }

        public static Vehicle CreateFromData(VehicleData data)
        {
            Vehicle veh = (Vehicle)Vehicle.Create(data.Type.Model, data.SpawnLocation.Position, data.SpawnLocation.RotZ, (int)data.Color1, (int)data.Color2);
            veh.Health = data.Dammages;
            veh.Locked = true;
            veh.Data = data;
            return veh;
        }

        public static bool CommandCheckForVehicle(Player player)
        {
            if (!player.InAnyVehicle)
            {
                Utils.Chat.ErrorChat(player, "Vous n'êtes pas dans un véhicule !");
                return false;
            }

            if (player.Vehicle == null)
            {
                Utils.Chat.ErrorChat(player, "Ce véhicule n'est pas géré par le GameMode !");
                return false;
            }

            return true;
        }

        public static Vehicle GetNearestVehicle(Player player)
        {
            return (Vehicle)Vehicle.All.OrderBy(v => player.GetDistanceFromPoint(v.Position)).First();
        }

        public static Vehicle GetNearestVehicle(Player player, float range)
        {
            var vehicle = (Vehicle)Vehicle.All.OrderBy(v => player.GetDistanceFromPoint(v.Position)).First();
            if (vehicle == null)
                return null;

            if (player.IsInRangeOfPoint(range, vehicle.Position))
                return vehicle;
            return null;
        }

        public static bool IsOwner(Player sender, Vehicle vehicle)
        {
            return vehicle.Data.Owner == sender.ActiveCharacter;
        }

        public static bool IsBorrowerOrOwner(Player sender, Vehicle vehicle)
        {
            return IsOwner(sender, vehicle) || vehicle.Data.Borrowers.Select(b => b.Borrower).Any(b => b == sender.ActiveCharacter);
        }


        public static void BorrowVehicle(Vehicle vehicle, Player borrower)
        {
            if (vehicle.Data.Borrowers.Select(b => b.Borrower).Any(b => b == borrower.ActiveCharacter))
                throw new Exception("Ce joueur a déjà emprunté ce véhicule.");

            vehicle.Data.Borrowers.Add(new VehicleDataBorrower(vehicle.Data, borrower.ActiveCharacter));
            
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            dbContext.SaveChanges();
        }

        public static void UnBorrowVehicle(Vehicle vehicle, Player borrower)
        {
            if (!vehicle.Data.Borrowers.Select(b => b.Borrower).Any(b => b == borrower.ActiveCharacter))
                throw new Exception("Ce joueur n'emprunte pas ce véhicule.");

            var vDataBorrower = vehicle.Data.Borrowers.Where(b => b.Borrower == borrower.ActiveCharacter).First();
            vehicle.Data.Borrowers.Remove(vDataBorrower);

            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
            dbContext.SaveChanges();
        }
    }
}
