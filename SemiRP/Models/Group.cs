using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class Group
    {

        public Group()
        {
        }

        public Group(int id, Character owner, string name, DateTime dateCreation, List<GroupRank> listRank)
        {
            this.Id = id;
            this.Owner = owner;
            this.Name = name;
            this.DateCreation = dateCreation;
            this.ListRank = listRank;
        }

        [Key]
        public int Id { get; set ; }
        public virtual Character Owner { get ; set ; }
        public string Name { get ; set ; }
        public DateTime DateCreation { get ; set ; }
        public virtual List<GroupRank> ListRank { get ; set ; }
    }
}
