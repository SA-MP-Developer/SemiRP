using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;
using SemiRP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace SemiRP.Utils.Vehicles
{
    public static class ModelHelper
    {
        public static bool IsBicycle(VehicleModel vdata)
        {
            switch (vdata.Model)
            {
                case VehicleModelType.BMX:
                case VehicleModelType.MountainBike:
                case VehicleModelType.Bike:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsBicycle(Vehicle veh)
        {
            return IsBicycle(veh.Data.Type);
        }

        public static bool HasHood(VehicleModel vdata)
        {
            switch (VehicleModelInfo.ForVehicle(vdata.Model).Category)
            {
                case VehicleCategory.Airplane:
                case VehicleCategory.Bike:
                case VehicleCategory.Boat:
                case VehicleCategory.Trailer:
                case VehicleCategory.TrainTrailer:
                case VehicleCategory.Helicopter:
                case VehicleCategory.RemoteControl:
                    return false;
                default:
                    break;
            }

            switch (vdata.Model)
            {
                case VehicleModelType.Dumper:
                case VehicleModelType.Firetruck:
                case VehicleModelType.BFInjection:
                case VehicleModelType.Rhino:
                case VehicleModelType.Hotknife:
                case VehicleModelType.Caddy:
                case VehicleModelType.Quad:
                case VehicleModelType.Dozer:
                case VehicleModelType.Forklift:
                case VehicleModelType.Vortex:
                case VehicleModelType.FiretruckLA:
                case VehicleModelType.Bandito:
                case VehicleModelType.Kart:
                case VehicleModelType.Mower:
                case VehicleModelType.Dune:
                case VehicleModelType.DFT30:
                case VehicleModelType.Tug:
                case VehicleModelType.SWAT:
                case VehicleModelType.Boxville:
                case VehicleModelType.Boxville2:
                    return false;
                default:
                    return true;
            }
        }

        public static bool HasHood(Vehicle veh)
        {
            return HasHood(veh.Data.Type);
        }

        public static VehicleModel ModelForModelType(VehicleModelType type)
        {
            return Lists.VehicleModelList.Models.First(m => m.Model == type);
        }
    }
}
