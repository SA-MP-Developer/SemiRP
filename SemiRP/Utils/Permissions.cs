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
            using (var db = new ServerDbContext())
            {
                db.PermissionSets.Attach(permSet);

                if (!db.Permissions.Any(p => p.Name == permname))
                    return 1;

                if (permSet.PermissionsSetPermission.Select(p => p.Permission).Any(p => p.Name == permname))
                    return 2;

                var perm = db.Permissions.Single(p => p.Name == permname);
                db.Permissions.Load();

                if (perm.ChildPermissions == null || perm.ChildPermissions.Count == 0)
                {
                    permSet.PermissionsSetPermission.Add(new PermissionSetPermission(permSet, perm));
                }
                else
                {
                    AddPerm_Rec(permSet, perm);
                }
                db.Entry(permSet).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
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
                foreach(Permission child in perm.ChildPermissions)
                {
                    AddPerm_Rec(permSet, child);
                }
            }
        }

        public static short RemovePerm(PermissionSet permSet, string permname)
        {
            using (var db = new ServerDbContext())
            {
                db.PermissionSets.Attach(permSet);

                if (!db.Permissions.Any(p => p.Name == permname))
                    return 1;

                var perm = db.Permissions.Single(p => p.Name == permname);
                db.Permissions.Load();
                db.PermissionSetPermissions.Where(ps => ps.PermissionSetId == permSet.Id).Load();

                if (perm.ChildPermissions == null || perm.ChildPermissions.Count == 0)
                {
                    if (!permSet.PermissionsSetPermission.Select(p => p.Permission).Any(p => p.Name == permname))
                        return 2;
                    var permSetPerm = db.PermissionSetPermissions.Where(p => p.PermissionId == perm.Id && p.PermissionSetId == permSet.Id).SingleOrDefault();
                    permSet.PermissionsSetPermission.Remove(permSetPerm);
                }
                else
                {
                    RemovePerm_Rec(permSet, perm, db);
                }
                db.Entry(permSet).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
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
            using (var db = new ServerDbContext())
            {
                db.Accounts.Attach(account);

                foreach (Permission perm in account.GetPerms())
                {
                    result.AddRange(ListAccountPermChildren(account, perm.Name));
                }
            }

            return result;
        }

        public static List<string> ListAccountPermChildren(Account account, string permname)
        {
            List<string> result = new List<string>();
            using (var db = new ServerDbContext())
            {
                db.Accounts.Attach(account);

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
            }

            return result;
        }
    }
}
