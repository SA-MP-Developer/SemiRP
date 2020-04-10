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
        private ContainerType type;
        private List<Item> listItems;


        public Container()
        {
        }

        public Container(int id, ContainerType type, List<Item> items)
        {
            this.Id = id;
            this.Type = type;
            this.ListItems = items;
        }

        public Container(int id, string name, ContainerType type, List<Item> listItems)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.listItems = listItems;
        }

        [Key]
        public int Id { get => id; set => id = value; }
        public ContainerType Type { get => type; set => type = value; }
        public List<Item> ListItems { get => listItems; set => listItems = value; }
        public string Name { get => name; set => name = value; }
    }
}
