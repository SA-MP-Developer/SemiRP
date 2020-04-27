using SampSharp.GameMode.Events;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World;
using SemiRP.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP
{
    [PooledType]
    public class Vehicle : BaseVehicle
    {
        public Models.VehicleData Data { get; set; }

        public bool Locked { get; set; }
        public float Speed { get {
                return (this.Velocity.Length * this.Velocity.Length) * Constants.Vehicle.SPEED_MAGIC;
            }
        }

        public override void OnSpawn(EventArgs e)
        {
            base.OnSpawn(e);

            Locked = true;
            this.Health = Data.Dammages;
            
        }

        public override void OnDeath(PlayerEventArgs e)
        {
            base.OnDeath(e);

            ServerDbContext dbContext = ((GameMode)GameMode.Instance).DbContext;

            Data.Dammages = 300;

            if (!Data.Temporary)
                dbContext.SaveChanges();
            else
                this.Dispose();
        }

        public override void OnDamageStatusUpdated(PlayerEventArgs e)
        {
            base.OnDamageStatusUpdated(e);
            if (Data.Temporary)
                this.Health = 1000;
        }

        public override void OnUnoccupiedUpdate(UnoccupiedVehicleEventArgs e)
        {
            base.OnUnoccupiedUpdate(e);

            if (Data.Temporary)
                this.Dispose();
        }

        public static void GlobalTimer(Object sender, EventArgs e)
        {
            foreach (Vehicle v in Vehicle.All)
            {
                float dist = v.Speed * (Constants.Vehicle.MS500_TIMER / (float)60000 / 30);
                v.Data.Mileage += dist;

                if (v.Data.Fuel < 0.02f)
                    v.Data.Fuel = 0f;

                if (v.Engine && v.Data.Fuel != 0f)
                {
                    if (v.Speed == 0)
                        v.Data.Fuel -= v.Data.FuelConsumption * Constants.Vehicle.STOPPED_CONSUMPTION_FACTOR;
                    else
                        v.Data.Fuel -= dist * (v.Data.FuelConsumption / 100);
                }
       
            }
        }
    }
}
