using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class Vehicle
    {
        private int id;
        private Container container;
        private Character owner;
        private int idVehicle;

        public Vehicle()
        {
        }

        public Vehicle(int id, Container container, Character owner, int idVehicle)
        {
            this.Id = id;
            this.Container = container;
            this.Owner = owner;
            this.IdVehicle = idVehicle;
        }

        [Key]
        public int Id { get => id; set => id = value; }
        public virtual Character Owner { get => owner; set => owner = value; }
        public int IdVehicle { get => idVehicle; set => idVehicle = value; }
        public virtual Container Container { get => container; set => container = value; }
    }
}
