using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class Permission
    {
        private int id;
        private String name;
        private Permission parentPermission;
        private List<Permission> childPermissions;

        public Permission()
        {
        }

        public Permission(int id, string name, List<Permission> childPermissions)
        {
            this.id = id;
            this.name = name;
            this.childPermissions = childPermissions;
        }

        public Permission(int id, string name, Permission parentPermission)
        {
            this.id = id;
            this.name = name;
            this.parentPermission = parentPermission;
        }

        public Permission(int id, string name, Permission parentPermission, List<Permission> childPermissions)
        {
            this.Id = id;
            this.name = name;
            this.ParentPermission = parentPermission;
            this.ChildPermissions = childPermissions;
        }
        [Key]
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public Permission ParentPermission { get => parentPermission; set => parentPermission = value; }
        public List<Permission> ChildPermissions { get => childPermissions; set => childPermissions = value; }
    }
}
