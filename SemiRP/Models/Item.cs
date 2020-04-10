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
        private double weight;
        private int quantity;

        public Item()
        {
        }

        public Item(int id, string name, double weight, int quantity)
        {
            this.Id = id;
            this.Name = name;
            this.Weight = weight;
            this.Quantity = quantity;
        }

        [Key]
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Weight { get => weight; set => weight = value; }
        public int Quantity { get => quantity; set => quantity = value; }
    }
}
