using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class GroupRank
    {
        private int id;
        private String name;
        private Group parentGroup;

        public GroupRank()
        {
        }

        public GroupRank(int id, string name, Group parentGroup)
        {
            this.Id = id;
            this.Name = name;
            this.ParentGroup = parentGroup;
        }
        [Key]
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public Group ParentGroup { get => parentGroup; set => parentGroup = value; }
    }
}
