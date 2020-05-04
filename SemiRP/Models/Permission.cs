using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class Permission
    {
        public Permission() { }

        public Permission(string name)
        {
            this.Name = name;
        }

        public Permission(int id, string name, List<Permission> childPermissions)
        {
            this.Id = id;
            this.Name = name;
            this.ChildPermissions = childPermissions;
        }

        public Permission(int id, string name, Permission parentPermission)
        {
            this.Id = id;
            this.Name = name;
            this.ParentPermission = parentPermission;
        }

        public Permission(int id, string name, Permission parentPermission, List<Permission> childPermissions)
        {
            this.Id = id;
            this.Name = name;
            this.ParentPermission = parentPermission;
            this.ChildPermissions = childPermissions;
        }
        [Key]
        public int Id { get ; set ; }
        public string Name { get; set; }
        public virtual Permission ParentPermission { get ; set ; }
        public virtual List<Permission> ChildPermissions { get ; set ; }

        public virtual IList<PermissionSetPermission> PermissionsSetPermission { get; set; }
    }
}
