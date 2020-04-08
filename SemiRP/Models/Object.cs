﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Models
{
    class Object
    {
        private int id;
        private String name;
        private double weight;
        private int quantity;

        public Object()
        {
        }

        public Object(int id, string name, double weight, int quantity)
        {
            this.Id = id;
            this.Name = name;
            this.Weight = weight;
            this.Quantity = quantity;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Weight { get => weight; set => weight = value; }
        public int Quantity { get => quantity; set => quantity = value; }
    }
}
