using SemiRP.Models.ContainerHeritage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SemiRP.Models
{
    public class Character
    {
        public enum CharSex
        {
            NOT_SET = -1,
            MAN = 0,
            WOMAN = 1
        }

        public Character()
        {
            this.GroupOwner = new List<Group>();
            this.GroupRanks = new List<GroupRank>();
        }

        public Character(int id, Account account, string name, uint age, int level, List<GroupRank> groupRanks, List<Group> groupOwner, Inventory inventory, Item itemInHand)
        {
            this.Id = id;
            this.Account = account;
            this.Name = name;
            this.Age = age;
            this.GroupRanks = groupRanks;
            this.GroupOwner = groupOwner;
            this.Inventory = inventory;
            this.Level = level;
            this.ItemInHand = itemInHand;

            this.GroupOwner = new List<Group>();
            this.GroupRanks = new List<GroupRank>();
        }
        public IList<Permission> GetPerms()
        {
            return PermsSet.PermissionsSetPermission.Select(p => p.Permission).ToList();
        }

        [Key]
        public int Id { get; set; }
        public virtual Account Account { get; set; }
        public string Name { get; set; }
        public uint Age { get; set; }
        public uint Skin { get; set; }
        public virtual List<GroupRank> GroupRanks { get; set; }
        public virtual List<Group> GroupOwner { get; set; }
        public virtual Inventory Inventory { get; set; }
        public CharSex Sex { get; set; }
        public int Level { get; set; }
        public virtual SpawnLocation SpawnLocation { get; set; }
        [ForeignKey("PermissionSet")]
        public virtual PermissionSet PermsSet { get; set; }
        public virtual Item ItemInHand { get; set; }
    }
}
