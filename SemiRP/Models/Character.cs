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

        private int id;
        private Account account;
        private string name;
        private uint age;
        private int level;
        private List<GroupRank> groupRanks;
        private List<Group> groupOwner;
        private List<Building> buildingOwner;
        private Inventory inventory;

        public Character()
        {

        }

        public Character(int id, Account account, string name, uint age, int level, List<GroupRank> groupRanks, List<Group> groupOwner, List<Building> buildingOwner, Inventory inventory)
        {
            this.Id = id;
            this.Account = account;
            this.Name = name;
            this.Age = age;
            this.GroupRanks = groupRanks;
            this.GroupOwner = groupOwner;
            this.BuildingOwner = buildingOwner;
            this.Inventory = inventory;
            this.Level = level;
        }
        public IList<Permission> GetPerms()
        {
            return PermsSet.PermissionsSetPermission.Select(p => p.Permission).ToList();
        }

        [Key]
        public int Id { get => id; set => id = value; }
        public virtual Account Account { get => account; set => account = value; }
        public string Name { get => name; set => name = value; }
        public uint Age { get => age; set => age = value; }
        public uint Skin { get; set; }
        public virtual List<GroupRank> GroupRanks { get => groupRanks; set => groupRanks = value; }
        public virtual List<Group> GroupOwner { get => groupOwner; set => groupOwner = value; }
        public virtual List<Building> BuildingOwner { get => buildingOwner; set => buildingOwner = value; }
        public virtual Inventory Inventory { get => inventory; set => inventory = value; }
        public CharSex Sex { get; set; }
        public int Level { get => level; set => level = value; }
        public virtual SpawnLocation SpawnLocation { get; set; }
        [ForeignKey("PermissionSet")]
        public virtual PermissionSet PermsSet { get; set; }
    }
}
