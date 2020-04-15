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

        

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Console.WriteLine("\n--------------------------------------------");
            Console.WriteLine(" SemiRP originally made by VickeS and Papawy");
            Console.WriteLine("---------------------------------------------\n");

            Console.WriteLine("[Perms] Checking and adding missing permissions...");
            using (var db = new ServerDbContext())
            {
                List<Permission> dbPerms = db.Permissions.ToList();
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
;                       if (!dbPerms.Any(p => p.Name == tmpPath))
                        {
                            perm = new Permission(tmpPath);
                            if (prevPerm != null)
                                perm.ParentPermission = prevPerm;
                            db.Permissions.Add(perm);
                            Console.WriteLine("Added " + tmpPath);
                        }
                        else
                        {
                            perm = dbPerms.Single(p => p.Name == tmpPath);
                            if (prevPerm != null && perm.ParentPermission != prevPerm)
                            {
                                perm.ParentPermission = prevPerm;
                                db.Entry(perm).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                        db.SaveChanges();      
                        prevPerm = perm;
                    }
                }
            }
            Console.WriteLine("[Perms] Done.");

        }

        #endregion
    }
}