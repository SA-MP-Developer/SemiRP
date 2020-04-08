using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Models
{
    class Building
    {
        private int id;
        private Character owner;
        private Container container;
        private String name;

        public Building()
        {
        }

        public Building(int id, Character owner, Container container, string name)
        {
            this.Id = id;
            this.Owner = owner;
            this.Container = container;
            this.Name = name;
        }

        public int Id { get => id; set => id = value; }
        public Character Owner { get => owner; set => owner = value; }
        public string Name { get => name; set => name = value; }
        internal Container Container { get => container; set => container = value; }
    }
}
