using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class PermissionSet
    {
        public PermissionSet()
        {
            PermissionsSetPermission = new List<PermissionSetPermission>();
        }

        [Key]
        public int Id { get; set; }
        public virtual IList<PermissionSetPermission> PermissionsSetPermission { get; set; }
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
        public virtual PermissionSet PermissionSet { get; set; }

        public int PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
