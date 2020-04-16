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
        private SpawnLocation spawnlocation;

        

        public Character()
        {

        }

        public Character(int id, Account account, string name, uint age, int level, List<GroupRank> groupRanks, List<Group> groupOwner, List<Building> buildingOwner, Inventory inventory, SpawnLocation spawnlocation)
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
            this.SpawnLocation = spawnlocation;
        }
        public IList<Permission> GetPerms()
        {
            return PermsSet.PermissionsSetPermission.Select(p => p.Permission).ToList();
        }

        [Key]
        public int Id { get => id; set => id = value; }
        public Account Account { get => account; set => account = value; }
        public string Name { get => name; set => name = value; }
        public uint Age { get => age; set => age = value; }
        public List<GroupRank> GroupRanks { get => groupRanks; set => groupRanks = value; }
        public List<Group> GroupOwner { get => groupOwner; set => groupOwner = value; }
        public List<Building> BuildingOwner { get => buildingOwner; set => buildingOwner = value; }
        public Inventory Inventory { get => inventory; set => inventory = value; }
        public CharSex Sex { get; set; }
        public int Level { get => level; set => level = value; }
        public SpawnLocation SpawnLocation { get => spawnlocation; set => spawnlocation = value; }
        [ForeignKey("PermissionSet")]
        public PermissionSet PermsSet { get; set; }
    }
}
