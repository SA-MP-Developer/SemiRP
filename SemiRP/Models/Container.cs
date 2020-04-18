using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class Container
    {
        private int id;
        private String name;
        private List<Item> listItems;
        private int maxSpace;


        public Container()
        {
            ListItems = new List<Item>();
        }

        public Container(int maxSpace)
        {
            this.MaxSpace = maxSpace;
            ListItems = new List<Item>();
        }

        public Container(int id, List<Item> items, int maxSpace)
        {
            this.Id = id;
            this.ListItems = items;
            this.MaxSpace = maxSpace;
        }

        public Container(int id, string name, List<Item> listItems, int maxSpace)
        {
            this.id = id;
            this.name = name;
            this.listItems = listItems;
            this.MaxSpace = maxSpace;
        }

        [Key]
        public int Id { get => id; set => id = value; }
        public virtual List<Item> ListItems { get => listItems; set => listItems = value; }
        public string Name { get => name; set => name = value; }
        public int MaxSpace { get => maxSpace; set => maxSpace = value; }
    }
}
