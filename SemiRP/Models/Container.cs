using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class Container
    {
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
            this.Id = id;
            this.Name = name;
            this.ListItems = listItems;
            this.MaxSpace = maxSpace;
        }

        [Key]
        public int Id { get ; set ; }
        public virtual List<Item> ListItems { get ; set ; }
        public string Name { get ; set ; }
        public int MaxSpace { get ; set ; }
    }
}
