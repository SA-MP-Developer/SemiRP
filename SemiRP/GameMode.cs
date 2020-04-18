using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using SampSharp.GameMode;
using SemiRP.Models;

namespace SemiRP
{
    public class GameMode : BaseMode
    {
        #region Overrides of BaseMode

        private ServerDbContext dbContext;

        public ServerDbContext DbContext { get { return dbContext; } }
        protected override void OnExited(EventArgs e)
        {
            base.OnExited(e);
            dbContext.Dispose();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            dbContext = new ServerDbContext();

            Console.WriteLine("\n--------------------------------------------");
            Console.WriteLine(" SemiRP originally made by VickeS and Papawy");
            Console.WriteLine("---------------------------------------------\n");

            Console.WriteLine("[Perms] Checking and adding missing permissions...");
            List<Permission> dbPerms = dbContext.Permissions.ToList();
            foreach (string permPath in PermissionList.Perms)
            {
                var perms = permPath.Split('.');

                Permission prevPerm = null;
                string tmpPath = "";

                for (uint i = 0; i < perms.Length; i++)
                {
                    if (tmpPath == "")
                        tmpPath += perms[i];
                    else
                        tmpPath += "." + perms[i];

                    Permission perm = null;
;                   if (!dbPerms.Any(p => p.Name == tmpPath))
                    {
                        perm = new Permission(tmpPath);
                        if (prevPerm != null)
                            perm.ParentPermission = prevPerm;

                        dbContext.Permissions.Add(perm);
                        dbPerms.Add(perm);
                        Console.WriteLine("Added " + tmpPath);
                    }
                    else
                    {
                        perm = dbPerms.Single(p => p.Name == tmpPath);
                        if (prevPerm != null && perm.ParentPermission != prevPerm)
                        {
                            perm.ParentPermission = prevPerm;
                            dbContext.Entry(perm).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }
                    dbContext.SaveChanges();
                    prevPerm = perm;
                }
            }
            Console.WriteLine("[Perms] Done.");

        }

        #endregion
    }
}