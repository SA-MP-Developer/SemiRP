using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Models
{
    class Vehicle
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

        public int Id { get => id; set => id = value; }
        public Character Owner { get => owner; set => owner = value; }
        public int IdVehicle { get => idVehicle; set => idVehicle = value; }
        internal Container Container { get => container; set => container = value; }
    }
}
