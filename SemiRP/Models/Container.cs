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
        private List<Object> listObjects;


        public Container()
        {
        }

        public Container(int id, ContainerType type, List<Object> objects)
        {
            this.Id = id;
            this.Type = type;
            this.ListObjects = objects;
        }

        public Container(int id, string name, ContainerType type, List<Object> listObjects)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.listObjects = listObjects;
        }

        [Key]
        public int Id { get => id; set => id = value; }
        public ContainerType Type { get => type; set => type = value; }
        public List<Object> ListObjects { get => listObjects; set => listObjects = value; }
        public string Name { get => name; set => name = value; }
    }
}
