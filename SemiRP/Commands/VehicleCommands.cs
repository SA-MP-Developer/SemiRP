using System;
using System.Collections.Generic;
using System.Linq;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;
using SemiRP.Utils;
using SemiRP.Utils.Vehicles;

namespace SemiRP.Commands
{
    [CommandGroup("vehicle", "veh", "v")]
    public class VehicleCommands
    {
        [Command("lock", "verouiller")]
        private static void Lock(Player sender)
        {
            var nearestVeh = Helper.GetNearestVehicle(sender, Constants.Vehicle.LOCK_RANGE);

            if (nearestVeh != null && Helper.IsBorrowerOrOwner(sender, nearestVeh))
            {
                Chat.SendMeChat(sender, nearestVeh.Locked ? "déverouille son véhicule." : "vérouille son véhicule.");
                nearestVeh.Locked = !nearestVeh.Locked;
                return;
            }

            if (!Helper.CommandCheckForVehicle(sender))
                return;

            var vehicle = ((Vehicle) sender.Vehicle);

            if (Helper.IsBorrowerOrOwner(sender, vehicle))
            {
                Chat.SendMeChat(sender, vehicle.Locked ? "dévérouille son véhicule." : "vérouille son véhicule.");
                vehicle.Locked = !vehicle.Locked;
            }
            else
            {
                Chat.ErrorChat(sender, "Vous n'êtes pas propriétaire de ce véhicule.");
            }
        }

        [Command("garer", "park")]
        private static void Park(Player sender)
        {
            if (!Helper.CommandCheckForVehicle(sender))
                return;

            var vehicle = ((Vehicle) sender.Vehicle);

            if (Helper.IsOwner(sender, vehicle))
            {
                Chat.InfoChat(sender, "Vous avez garé votre véhicule, il réapparaitra désormais ici.");
                vehicle.Data.SpawnLocation.Position =
                    new Vector3(vehicle.Position.X, vehicle.Position.Y, vehicle.Position.Z + 0.5f);
                vehicle.Data.SpawnLocation.RotZ = vehicle.Angle;

                var vData = vehicle.Data;

                ServerDbContext dbContext = ((GameMode) GameMode.Instance).DbContext;
                dbContext.Update(vehicle.Data);
                dbContext.SaveChanges();

                List<BasePlayer> passengers = vehicle.Passengers.ToList();

                vehicle.Dispose();
                vehicle = Helper.CreateFromData(vData);

                sender.PutInVehicle(vehicle);

                for (int i = 1, j = 0; j < passengers.Count() && i <= vehicle.ModelInfo.SeatCount; i++, j++)
                {
                    passengers[j].PutInVehicle(vehicle, i);
                }
            }
            else
            {
                Chat.ErrorChat(sender, "Vous n'êtes pas propriétaire de ce véhicule.");
            }
        }

        [Command("phare", "lights")]
        private static void Lights(Player sender)
        {
            if (!Helper.CommandCheckForVehicle(sender))
                return;

            var vehicle = ((Vehicle) sender.Vehicle);

            if (vehicle.ModelInfo.Category == VehicleCategory.Bike)
            {
                Chat.ErrorChat(sender, "Ce véhicule n'a pas de phares.");
            }

            Chat.SendMeChat(sender,
                vehicle.Lights ? "éteint les phares de son véhicule." : "allume les phares de son véhicule.");
            vehicle.Lights = !vehicle.Lights;
        }

        [Command("capot", "hood")]
        private static void Hood(Player sender)
        {
            var nearestVeh = Helper.GetNearestVehicle(sender, Constants.Vehicle.HOOD_RANGE);

            Vehicle vehicle = null;

            if (nearestVeh != null)
                vehicle = nearestVeh;
            else
            {
                if (!Helper.CommandCheckForVehicle(sender))
                    return;

                vehicle = ((Vehicle) sender.Vehicle);
            }

            if (vehicle.ModelInfo.Category == VehicleCategory.Bike
                || vehicle.ModelInfo.Category == VehicleCategory.Airplane
                || vehicle.ModelInfo.Category == VehicleCategory.Boat
                || vehicle.ModelInfo.Category == VehicleCategory.Bike
                || vehicle.ModelInfo.Category == VehicleCategory.Helicopter)
            {
                Chat.ErrorChat(sender, "Ce véhicule n'a pas de capot.");
            }

            Chat.SendMeChat(sender,
                vehicle.Bonnet ? "ferme le capot de son véhicule." : "ouvre le capot de son véhicule.");
            vehicle.Bonnet = !vehicle.Bonnet;
        }

        [Command("moteur", "engine")]
        private static void Engine(Player sender)
        {
            if (!Helper.CommandCheckForVehicle(sender))
                return;

            var vehicle = ((Vehicle) sender.Vehicle);

            if (ModelHelper.IsBicycle(vehicle))
            {
                Chat.ErrorChat(sender, "Ce véhicule n'a pas de moteur.");
            }

            if (!Helper.IsBorrowerOrOwner(sender, vehicle))
            {
                Chat.ErrorChat(sender, "Vous n'avez pas les clefs de ce véhicule.");
                return;
            }

            if (vehicle.Data.Fuel == 0)
            {
                Chat.ErrorChat(sender, "Ce véhicule n'a plus d'essence.");
            }

            if (vehicle.Engine)
                Chat.SendMeChat(sender, "éteint le moteur de son véhicule.");
            else
                Chat.SendMeChat(sender, "allume le moteur de son véhicule.");
            vehicle.Engine = !vehicle.Engine;
        }

        [Command("liste", "list")]
        private static void List(Player sender)
        {
            if (!Vehicle.All.Any(v => ((Vehicle) v).Data.Owner == sender.ActiveCharacter))
            {
                Chat.ErrorChat(sender, "Vous n'avez pas de véhicules.");
                return;
            }

            Chat.ClientChat(sender, "===== Vos véhicules =====");
            foreach (string line in CmdHelper.ListPlayerVehicles(sender))
            {
                Chat.ClientChat(sender, line);
            }
        }

        [Command("preter", "lend")]
        private static void Lend(Player sender, Player target, int vehicle)
        {
            try
            {
                Vehicle veh = CmdHelper.GetCurrentVehicleOrID(sender, vehicle);

                if (!Helper.IsOwner(sender, veh))
                    throw new Exception("Vous n'êtes pas propriétaire de ce véhicule.");

                if (veh.Data.Borrowers.Select(b => b.Borrower).Any(b => b == target.ActiveCharacter))
                    throw new Exception("Vous prêtez déjà votre véhicule à ce joueur.");

                Helper.BorrowVehicle(veh, target);
                Chat.InfoChat(sender, "Vous avez prêté votre véhicule " + Constants.Chat.HIGHLIGHT +
                                      VehicleModelInfo.ForVehicle(veh).Name + Color.White
                                      + " à " + Constants.Chat.HIGHLIGHT + target.Name + Color.White + ".");

                ServerDbContext dbContext = ((GameMode) GameMode.Instance).DbContext;
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Chat.ErrorChat(sender, e.Message);
            }
        }

        [Command("coffre", "trunk")]
        private static void Trunk(Player sender)
        {
        }
    }
}
