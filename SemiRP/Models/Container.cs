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


        public Container()
        {
        }

        public Container(int id, List<Item> items)
        {
            this.Id = id;
            this.ListItems = items;
        }

        public Container(int id, string name, List<Item> listItems)
        {
            this.id = id;
            this.name = name;
            this.listItems = listItems;
        }

        [Key]
        public int Id { get => id; set => id = value; }
        public List<Item> ListItems { get => listItems; set => listItems = value; }
        public string Name { get => name; set => name = value; }
    }
}
