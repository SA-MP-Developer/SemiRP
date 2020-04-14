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
        public static bool AddAccountPerm(Account account, string permname)
        {
            using (var db = new ServerDbContext())
            {
                db.Accounts.Attach(account);

                if (account.Perms.Any(p => p.Name == permname))
                    return false; ;

                account.Perms.Add(new Permission(permname));

                db.SaveChanges();
            }

            return true;
        }

        public static bool RemoveAccountPerm(Account account, string permname, bool first=true)
        {
            using (var db = new ServerDbContext())
            {
                db.Accounts.Attach(account);

                if (!account.Perms.Any(p => p.Name == permname))
                    return false;

                var perm = account.Perms.Where(p => p.Name == permname).First();

                if (perm.ChildPermissions != null)
                {
                    foreach (Permission child in perm.ChildPermissions)
                    {
                        RemoveAccountPerm(account, child.Name, false);
                    }
                }

                account.Perms.Remove(perm);
                db.Permissions.Remove(perm);

                if (first)
                    db.SaveChanges();
            }

            return true;
        }

        public static List<string> ListAccountPerms(Account account)
        {
            List<string> result = new List<string>();
            using (var db = new ServerDbContext())
            {
                db.Accounts.Attach(account);

                foreach (Permission perm in account.Perms)
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

                if (!account.Perms.Any(p => p.Name == permname))
                    return result;

                var perm = account.Perms.Where(p => p.Name == permname).First();

                result.Add(perm.ParentPermission == null ? permname : perm.ParentPermission.Name + "/" + permname);

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
