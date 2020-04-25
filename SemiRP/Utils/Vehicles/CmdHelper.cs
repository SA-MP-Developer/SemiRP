using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SemiRP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SemiRP.Utils.Vehicles
{
    public static class CmdHelper
    {
        public static Vehicle GetCurrentVehicleOrID(Player player, int id = -1)
        {
            if (player.Vehicle == null && id == -1)
            {
                throw new Exception("Vous n'avez pas choisi de véhicule.");
            }

            Vehicle vehicle = null;

            if (id != -1)
            {
                if (!Vehicle.All.Any(v => v.Id == id))
                {
                    throw new Exception("Ce véhicule n'éxiste pas.");
                }
                vehicle = (Vehicle)Vehicle.All.Where(v => v.Id == id).First();
            }
            else
                vehicle = (Vehicle)player.Vehicle;

            return vehicle;
        }

        public static List<string> DisplayAdminInfos(Vehicle vehicle)
        {
            List<string> ret = new List<string>();
            ret.Add("ID BDD : " + Constants.Chat.HIGHLIGHT + vehicle.Id + Color.White + " | ID SAMP : " + Constants.Chat.HIGHLIGHT + vehicle.Id);
            ret.Add("Position (x,y,z) : ("
                + Constants.Chat.HIGHLIGHT + vehicle.Position.X + Color.White + ","
                + Constants.Chat.HIGHLIGHT + vehicle.Position.Y + Color.White + ","
                + Constants.Chat.HIGHLIGHT + vehicle.Position.Z + Color.White + ")");

            ret.Add("Model : " + Constants.Chat.HIGHLIGHT + vehicle.ModelInfo.Name);
            ret.Add("Color 1 : " + Constants.Chat.HIGHLIGHT + vehicle.Data.Color1 + Color.White + ", Color 2 : " + Constants.Chat.HIGHLIGHT + vehicle.Data.Color2);

            var ownerPlayer = Utils.PlayerUtils.PlayerHelper.SearchCharacter(vehicle.Data.Owner);
            if (ownerPlayer == null)
                ret.Add("Owner : " + Constants.Chat.HIGHLIGHT + vehicle.Data.Owner.Name + " (Compte : " + vehicle.Data.Owner.Account.Id + ")");
            else
                ret.Add("Owner : " + Constants.Chat.HIGHLIGHT + vehicle.Data.Owner.Name + " (Compte : " + vehicle.Data.Owner.Account.Id + ") (connecté, id : " + ownerPlayer.Id + ")");

            foreach (Character borrower in vehicle.Data.Borrowers.Select(b => b.Borrower).ToList())
            {
                var borrowerPlayer = Utils.PlayerUtils.PlayerHelper.SearchCharacter(borrower);
                if (borrowerPlayer == null)
                    ret.Add("Borrower : " + Constants.Chat.HIGHLIGHT + borrower.Name + " (Compte : " + borrower.Account.Id + ")");
                else
                    ret.Add("Borrower : " + Constants.Chat.HIGHLIGHT + borrower.Name + " (Compte : " + borrower.Account.Id + ") (connecté, id : " + borrowerPlayer.Id + ")");
            }

            ret.Add("Health : " + Constants.Chat.HIGHLIGHT + vehicle.Health);
            ret.Add("Fuel : " + Constants.Chat.HIGHLIGHT + vehicle.Data.Fuel.ToString("0.00") + Color.White + " | Fuel Tank : " + Constants.Chat.HIGHLIGHT + vehicle.Data.MaxFuel.ToString("0.0") + Color.White + " | Fuel Cons. : " + Constants.Chat.HIGHLIGHT + vehicle.Data.FuelConsumption.ToString("0.0"));
            ret.Add("Mileage : " + Constants.Chat.HIGHLIGHT + vehicle.Data.Mileage.ToString("0.000"));
            ret.Add("Current Speed : " + Constants.Chat.HIGHLIGHT + vehicle.Speed);
            ret.Add("Locked : " + Constants.Chat.HIGHLIGHT + (vehicle.Locked ? "YES" : "NO"));

            return ret;
        }

        public static void CreateTmpVehicleForPlayer(Player sender, VehicleModelType vehicle)
        {
            var veh = Utils.Vehicles.Helper.CreateVehicle(sender.ActiveCharacter, Utils.Vehicles.ModelHelper.ModelForModelType(vehicle), sender.Position, sender.Angle, VehicleColor.BrighRed, VehicleColor.BrighRed, true);
            veh.Data.FuelConsumption = 0;
            veh.Health = 100000;
            sender.PutInVehicle(veh);
            veh.Engine = true;
        }

        public static void CreateVehicleForPlayer(Player sender, VehicleModelType vehicle)
        {
            var veh = Utils.Vehicles.Helper.CreateVehicle(sender.ActiveCharacter, Utils.Vehicles.ModelHelper.ModelForModelType(vehicle), sender.Position, sender.Angle);
        }
    }
}
