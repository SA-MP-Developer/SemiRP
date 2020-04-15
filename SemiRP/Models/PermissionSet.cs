using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class PermissionSet
    {
        [Key]
        public int Id { get; set; }
        public IList<PermissionSetPermission> PermissionsSetPermission { get; set; }
    }

    public class PermissionSetPermission
    {
        public PermissionSetPermission()
        {

        }

        public PermissionSetPermission(PermissionSet permissionSet, Permission permission)
        {
            PermissionId = permission.Id;
            Permission = permission;
            PermissionSetId = permissionSet.Id;
            PermissionSet = permissionSet;
        }

        public int PermissionSetId { get; set; }
        public PermissionSet PermissionSet { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
