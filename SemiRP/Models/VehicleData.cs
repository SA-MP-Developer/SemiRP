using SampSharp.GameMode.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SemiRP.Models
{
    public class VehicleData
    {
        public VehicleData()
        {
            Borrowers = new List<Owner>();
            Temporary = false;
        }

        [Key]
        public int Id { get; set; }
        public virtual VehicleModel Type { get; set; }
        public VehicleColor Color1 { get; set; }
        public VehicleColor Color2 { get; set; }
        public virtual SpawnLocation SpawnLocation { get; set; }
        public float MaxFuel { get; set; }
        public float Fuel { get; set; }
        public float FuelConsumption { get; set; }
        public float Dammages { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual List<Owner> Borrowers { get; set; }
        public virtual Container Container { get; set; }

        public float Mileage { get; set; }

        [NotMapped]
        public bool Temporary { get; set; }

    }
}