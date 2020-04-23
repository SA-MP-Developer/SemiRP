using SampSharp.GameMode.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System.Text;

namespace SemiRP.Models
{
    public class VehicleModel
    {
        public VehicleModel()
        {

        }

        public VehicleModel(VehicleModelType model, int basePrice, float maxFuel, float fuelCons, int containerSize)
        {
            Model = model;
            BasePrice = basePrice;
            MaxFuel = maxFuel;
            FuelConsumption = fuelCons;
            ContainerSize = containerSize;
        }

        [Key]
        public int Id { get; set; }

        public VehicleModelType Model { get; set; }
        public int BasePrice { get; set; }
        public float MaxFuel { get; set; }
        public float FuelConsumption { get; set; }
        public int ContainerSize { get; set; }
    }
}
