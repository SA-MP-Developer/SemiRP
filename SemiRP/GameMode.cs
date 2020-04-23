﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SemiRP.Models;

namespace SemiRP
{
    public class GameMode : BaseMode
    {
        #region Overrides of BaseMode

        private ServerDbContext dbContext;
        public ServerDbContext DbContext { get { return dbContext; } }

        private Timer vehicleTimer;

        protected override void OnExited(EventArgs e)
        {
            base.OnExited(e);

            vehicleTimer.Dispose();

            dbContext.Dispose();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            this.DisableInteriorEnterExits();
            this.EnableStuntBonusForAll(false);
            this.AllowInteriorWeapons(true);
            this.ManualVehicleEngineAndLights();
            this.ShowPlayerMarkers(SampSharp.GameMode.Definitions.PlayerMarkersMode.Off);
            this.ShowNameTags(false);

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

            Console.WriteLine("[VehicleModels] Begining checking...");
            InitializeVehicleModel();
            Console.WriteLine("[VehicleModels] Done.");

            Console.WriteLine("[Vehicles] Begining loading...");
            List<VehicleData> vehList = dbContext.Vehicles.ToList();

            foreach (VehicleData vData in vehList)
            {
                try
                {
                    Utils.Vehicles.Helper.CreateFromData(vData);
                }
                catch (Exception)
                {
                    Console.WriteLine("Can't spawn vehicle id " + vData.Id + ".");
                }
            }

            Console.WriteLine("[Vehicles] Done.");

            Console.WriteLine("[Timers] Setup timers...");
            vehicleTimer = new Timer(Constants.Vehicle.MS500_TIMER, true);
            vehicleTimer.Tick += Vehicle.GlobalTimer;
            Console.WriteLine("Vehicle timer");

            Console.WriteLine("[Timers] Done.");

        }

        private void InitializeVehicleModel()
        {
            foreach (VehicleModel model in Lists.VehicleModelList.Models)
            {
                if (!dbContext.VehicleModels.Any(m => m.Model == model.Model))
                {
                    Console.WriteLine("Adding " + nameof(model.Model));
                    dbContext.VehicleModels.Add(model);
                }
                else
                {
                    var tmpModel = dbContext.VehicleModels.First(m => m.Model == model.Model);
                    model.FuelConsumption = tmpModel.FuelConsumption;
                    model.BasePrice = tmpModel.BasePrice;
                    model.MaxFuel = tmpModel.MaxFuel;
                    model.ContainerSize = tmpModel.ContainerSize;
                    Console.WriteLine("Model : " + model.Model + " | Fuel : " + model.MaxFuel + " | Cons. : " + model.FuelConsumption);
                }
            }
            dbContext.SaveChanges();
        }

        #endregion
    }
}