using SampSharp.Streamer.World;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SemiRP.Models
{
    public class Item
    {
        public Item()
        {
        }

        public Item(int id, string name, int quantity, Container currentContainer, SpawnLocation spawnLocation, int modelId = -1, DynamicObject dynamicObject = null)
        {
            this.Id = id;
            this.Name = name;
            this.Quantity = quantity;
            this.CurrentContainer = currentContainer;
            this.SpawnLocation = spawnLocation;
            this.ModelId = modelId;
            this.DynamicObject = dynamicObject;
        }

        [Key]
        public int Id { get ; set ; }
        public string Name { get ; set ; }
        public int Quantity { get ; set ; }
        public virtual Container CurrentContainer { get ; set ; }
        public virtual SpawnLocation SpawnLocation { get ; set ; }
        public int ModelId { get ; set ; }
        [NotMapped]
        public DynamicObject DynamicObject { get; set; }
    }
}
