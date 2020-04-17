using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class Group
    {
        private int id;
        private Character owner;
        private string name;
        private DateTime dateCreation;
        List<GroupRank> listRank;

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
        public int Id { get => id; set => id = value; }
        public virtual Character Owner { get => owner; set => owner = value; }
        public string Name { get => name; set => name = value; }
        public DateTime DateCreation { get => dateCreation; set => dateCreation = value; }
        public virtual List<GroupRank> ListRank { get => listRank; set => listRank = value; }
    }
}
