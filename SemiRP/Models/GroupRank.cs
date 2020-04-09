using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    class GroupRank
    {
        private int id;
        private String name;

        public GroupRank()
        {
        }

        public GroupRank(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        [Key]
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
