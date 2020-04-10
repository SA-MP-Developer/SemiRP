using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class Permission
    {
        private int id;
        private String Name;
        private Permission parentPermission;
        private List<Permission> childPermissions;

        public Permission()
        {
        }

        public Permission(int id, string name, List<Permission> childPermissions)
        {
            this.id = id;
            Name = name;
            this.childPermissions = childPermissions;
        }

        public Permission(int id, string name, Permission parentPermission)
        {
            this.id = id;
            Name = name;
            this.parentPermission = parentPermission;
        }

        public Permission(int id, string name, Permission parentPermission, List<Permission> childPermissions)
        {
            this.Id = id;
            Name1 = name;
            this.ParentPermission = parentPermission;
            this.ChildPermissions = childPermissions;
        }

        public int Id { get => id; set => id = value; }
        public string Name1 { get => Name; set => Name = value; }
        public Permission ParentPermission { get => parentPermission; set => parentPermission = value; }
        public List<Permission> ChildPermissions { get => childPermissions; set => childPermissions = value; }
    }
}
