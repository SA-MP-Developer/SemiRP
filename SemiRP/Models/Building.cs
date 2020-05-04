using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class Building
    {

        public Building()
        {
        }

        public Building(int id, Group ownerGroup, Container container, string name, bool onSale, bool salable)
        {
            this.Id = id;
            this.OwnerGroup = ownerGroup;
            this.Container = container;
            this.Name = name;
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
        public int Id { get ; set ; }
        public virtual Character Owner { get ; set ; }
        public string Name { get ; set ; }
        public virtual Container Container { get ; set ; }
        public virtual Group OwnerGroup { get ; set ; }
        public bool OnSale { get ; set ; }
        public bool Salable { get ; set ; }
    }
}
