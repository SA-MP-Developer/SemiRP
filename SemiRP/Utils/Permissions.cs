using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using SampSharp.GameMode.SAMP;
using SemiRP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;

namespace SemiRP.Utils
{
    class Permissions
    {
        public static short AddPerm(PermissionSet permSet, string permname)
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;

            dbContext.PermissionSets.Attach(permSet);

            if (!dbContext.Permissions.Any(p => p.Name == permname))
                return 1;

            if (permSet.PermissionsSetPermission.IsNullOrEmpty() && permSet.PermissionsSetPermission.Select(p => p.Permission).Any(p => p.Name == permname))
                return 2;

            var perm = dbContext.Permissions.Single(p => p.Name == permname);

            if (perm.ChildPermissions == null || perm.ChildPermissions.Count == 0)
            {
                permSet.PermissionsSetPermission.Add(new PermissionSetPermission(permSet, perm));
            }
            else
            {
                AddPerm_Rec(permSet, perm);
            }
            dbContext.SaveChanges();
            return 0;
        }

        private static void AddPerm_Rec(PermissionSet permSet, Permission perm)
        {
            if (perm.ChildPermissions == null || perm.ChildPermissions.Count == 0)
            {
                if (!permSet.PermissionsSetPermission.Select(p => p.Permission).Any(p => p.Name == perm.Name))
                    permSet.PermissionsSetPermission.Add(new PermissionSetPermission(permSet, perm));
            }
            else
            {
                foreach (Permission child in perm.ChildPermissions)
                {
                    AddPerm_Rec(permSet, child);
                }
            }
        }

        public static short RemovePerm(PermissionSet permSet, string permname)
        {
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;

            dbContext.PermissionSets.Attach(permSet);

            if (!dbContext.Permissions.Any(p => p.Name == permname))
                return 1;

            var perm = dbContext.Permissions.Single(p => p.Name == permname);

            if (perm.ChildPermissions == null || perm.ChildPermissions.Count == 0)
            {
                if (!permSet.PermissionsSetPermission.Select(p => p.Permission).Any(p => p.Name == permname))
                    return 2;
                var permSetPerm = dbContext.PermissionSetPermissions.Where(p => p.PermissionId == perm.Id && p.PermissionSetId == permSet.Id).SingleOrDefault();
                permSet.PermissionsSetPermission.Remove(permSetPerm);
            }
            else
            {
                RemovePerm_Rec(permSet, perm, dbContext);
            }
            dbContext.SaveChanges();
            return 0;
        }

        private static void RemovePerm_Rec(PermissionSet permSet, Permission perm, ServerDbContext db)
        {
            if (perm.ChildPermissions == null || perm.ChildPermissions.Count == 0)
            {
                if (!permSet.PermissionsSetPermission.Select(p => p.Permission).Any(p => p.Id == perm.Id))
                    return;

                var permSetPerm = db.PermissionSetPermissions.Where(p => p.PermissionId == perm.Id && p.PermissionSetId == permSet.Id).SingleOrDefault();
                permSet.PermissionsSetPermission.Remove(permSetPerm);
            }
            else
            {
                foreach (Permission child in perm.ChildPermissions)
                {
                    RemovePerm_Rec(permSet, child, db);
                }
            }
        }

        public static List<string> ListAccountPerms(Account account)
        {
            List<string> result = new List<string>();
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;

            dbContext.Accounts.Attach(account);

            foreach (Permission perm in account.GetPerms())
            {
                result.AddRange(ListAccountPermChildren(account, perm.Name));
            }

            return result;
        }

        public static List<string> ListAccountPermChildren(Account account, string permname)
        {
            List<string> result = new List<string>();
            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;

            dbContext.Accounts.Attach(account);

            if (!account.GetPerms().Any(p => p.Name == permname))
                return result;

            var perm = account.GetPerms().Where(p => p.Name == permname).First();

            result.Add(permname);

            if (perm.ChildPermissions != null)
            {
                foreach (Permission child in perm.ChildPermissions)
                {
                    result.AddRange(ListAccountPermChildren(account, child.Name));
                }
            }

            return result;
        }
    }
}
