using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class Item
    {
        private int id;
        private String name;
        private int quantity;
        private Container currentContainer;
        private SpawnLocation spawnLocation;

        public Item()
        {
        }

        public Item(int id, string name, int quantity, Container currentContainer, SpawnLocation spawnLocation)
        {
            this.Id = id;
            this.Name = name;
            this.Quantity = quantity;
            this.currentContainer = currentContainer;
            this.spawnLocation = spawnLocation;
        }

        [Key]
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public virtual Container CurrentContainer { get => currentContainer; set => currentContainer = value; }
        public virtual SpawnLocation SpawnLocation { get => spawnLocation; set => spawnLocation = value; }
    }
}
