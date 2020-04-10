using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class ContainerType
    {
        private int id;
        private String name;
        private int maxSpace;

        public ContainerType()
        {
        }

        public ContainerType(int id, string name, int maxSpace)
        {
            this.Id = id;
            this.Name = name;
            this.MaxSpace = maxSpace;
        }
        [Key]
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int MaxSpace { get => maxSpace; set => maxSpace = value; }
    }
}
