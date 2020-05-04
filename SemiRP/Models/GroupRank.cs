using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class GroupRank
    {
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
        public int Id { get ; set ; }
        public string Name { get ; set ; }
        public virtual Group ParentGroup { get ; set ; }
    }
}
