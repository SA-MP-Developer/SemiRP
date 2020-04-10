using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class Building
    {
        private int id;
        private Character owner;
        private Group ownerGroup;
        private Container container;
        private String name;
        private bool onSale;
        private bool salable;

        public Building()
        {
        }

        public Building(int id, Group ownerGroup, Container container, string name, bool onSale, bool salable)
        {
            this.id = id;
            this.OwnerGroup = ownerGroup;
            this.container = container;
            this.name = name;
            this.OnSale = onSale;
            this.Salable = salable;
        }

        public Building(int id, Character owner, Container container, string name, bool onSale, bool salable)
        {
            this.Id = id;
            this.Owner = owner;
            this.Container = container;
            this.Name = name;
            this.OnSale = onSale;
            this.Salable = salable;
        }

        [Key]
        public int Id { get => id; set => id = value; }
        public Character Owner { get => owner; set => owner = value; }
        public string Name { get => name; set => name = value; }
        public Container Container { get => container; set => container = value; }
        public Group OwnerGroup { get => ownerGroup; set => ownerGroup = value; }
        public bool OnSale { get => onSale; set => onSale = value; }
        public bool Salable { get => salable; set => salable = value; }
    }
}
