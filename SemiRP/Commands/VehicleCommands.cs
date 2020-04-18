using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemiRP.Commands
{
    [CommandGroup("vehicle", "veh", "v")]
    public class VehicleCommands
    {
        [Command("lock", "verouiller")]
        private static void Lock(Player sender)
        {
            var nearestVeh = Utils.Vehicles.Helper.GetNearestVehicle(sender, Constants.Vehicle.LOCK_RANGE);

            if (nearestVeh != null && (nearestVeh.Data.Owner == sender.ActiveCharacter || nearestVeh.Data.Borrowers.Contains(sender.ActiveCharacter)))
            {
                if (nearestVeh.Locked)
                    Utils.Chat.SendMeChat(sender, "dévérouille son véhicule.");
                else
                    Utils.Chat.SendMeChat(sender, "vérouille son véhicule.");
                    nearestVeh.Locked = !nearestVeh.Locked;
                return;
            }

            if (!Utils.Vehicles.Helper.CommandCheckForVehicle(sender))
                return;

            var vehicle = ((Vehicle)sender.Vehicle);

            if (vehicle.Data.Owner == sender.ActiveCharacter || vehicle.Data.Borrowers.Contains(sender.ActiveCharacter))
            {
                if (vehicle.Locked)
                    Utils.Chat.SendMeChat(sender, "dévérouille son véhicule.");
                else
                    Utils.Chat.SendMeChat(sender, "vérouille son véhicule.");
                vehicle.Locked = !vehicle.Locked;
            }
            else
            {
                Utils.Chat.ErrorChat(sender, "Vous n'êtes pas propriétaire de ce véhicule.");
            }
        }

        [Command("garer", "park")]
        private static void Park(Player sender)
        {
            if (!Utils.Vehicles.Helper.CommandCheckForVehicle(sender))
                return;

            var vehicle = ((Vehicle)sender.Vehicle);

            if (vehicle.Data.Owner == sender.ActiveCharacter)
            {
                Utils.Chat.InfoChat(sender, "Vous avez garé votre véhicule, il réapparaitra désormais ici.");
                vehicle.Data.SpawnLocation.Position = new SampSharp.GameMode.Vector3(vehicle.Position.X, vehicle.Position.Y, vehicle.Position.Z + 0.5f);
                vehicle.Data.SpawnLocation.RotZ = vehicle.Angle;

                var vData = vehicle.Data;

                ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;
                dbContext.Update(vehicle.Data);
                dbContext.SaveChanges();

                List<BasePlayer> passengers = vehicle.Passengers.ToList();

                vehicle.Dispose();
                vehicle = Utils.Vehicles.Helper.CreateFromData(vData);

                sender.PutInVehicle(vehicle);

                for (int i = 1, j = 0; j < passengers.Count() && i <= vehicle.ModelInfo.SeatCount; i++, j++)
                {
                    passengers[j].PutInVehicle(vehicle, i);
                }
            }
            else
            {
                Utils.Chat.ErrorChat(sender, "Vous n'êtes pas propriétaire de ce véhicule.");
            }
        }

        [Command("phare", "lights")]
        private static void Lights(Player sender)
        {
            if (!Utils.Vehicles.Helper.CommandCheckForVehicle(sender))
                return;

            var vehicle = ((Vehicle)sender.Vehicle);

            if (vehicle.ModelInfo.Category == SampSharp.GameMode.Definitions.VehicleCategory.Bike)
            {
                Utils.Chat.ErrorChat(sender, "Ce véhicule n'a pas de phares.");
            }

            if (vehicle.Lights)
                Utils.Chat.SendMeChat(sender, "éteint les phares de son véhicule.");
            else
                Utils.Chat.SendMeChat(sender, "allume les phares de son véhicule.");
            vehicle.Lights = !vehicle.Lights;
        }

        [Command("capot", "hood")]
        private static void Hood(Player sender)
        {
            var nearestVeh = Utils.Vehicles.Helper.GetNearestVehicle(sender, Constants.Vehicle.HOOD_RANGE);

            Vehicle vehicle = null;

            if (nearestVeh != null)
                vehicle = nearestVeh;
            else
            {
                if (!Utils.Vehicles.Helper.CommandCheckForVehicle(sender))
                    return;

                vehicle = ((Vehicle)sender.Vehicle);
            }

            if (vehicle.ModelInfo.Category == VehicleCategory.Bike
                || vehicle.ModelInfo.Category == VehicleCategory.Airplane
                || vehicle.ModelInfo.Category == VehicleCategory.Boat
                || vehicle.ModelInfo.Category == VehicleCategory.Bike
                || vehicle.ModelInfo.Category == VehicleCategory.Helicopter)
            {
                Utils.Chat.ErrorChat(sender, "Ce véhicule n'a pas de capot.");
            }

            if (vehicle.Bonnet)
                Utils.Chat.SendMeChat(sender, "ferme le capot de son véhicule.");
            else
                Utils.Chat.SendMeChat(sender, "ouvre le capot de son véhicule.");
            vehicle.Bonnet = !vehicle.Bonnet;
        }

        [Command("moteur", "engine")]
        private static void Engine(Player sender)
        {
            if (!Utils.Vehicles.Helper.CommandCheckForVehicle(sender))
                return;

            var vehicle = ((Vehicle)sender.Vehicle);

            if (Utils.Vehicles.ModelHelper.IsBicycle(vehicle))
            {
                Utils.Chat.ErrorChat(sender, "Ce véhicule n'a pas de moteur.");
            }

            if (!(vehicle.Data.Owner == sender.ActiveCharacter || vehicle.Data.Borrowers.Contains(sender.ActiveCharacter)))
            {
                Utils.Chat.ErrorChat(sender, "Vous n'avez pas les clefs de ce véhicule.");
                return;
            }

            if (vehicle.Data.Fuel == 0)
            {
                Utils.Chat.ErrorChat(sender, "Ce véhicule n'a plus d'essence.");
            }

            if (vehicle.Engine)
                Utils.Chat.SendMeChat(sender, "éteint le moteur de son véhicule.");
            else
                Utils.Chat.SendMeChat(sender, "allume le moteur de son véhicule.");
            vehicle.Engine = !vehicle.Engine;
        }
        [Command("coffre", "trunk")]
        private static void Trunk(Player sender)
        {

        }
    }
}
