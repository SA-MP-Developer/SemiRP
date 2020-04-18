using SampSharp.GameMode.Definitions;
using SemiRP.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Lists
{
    public static class VehicleModelList
    {
        public static List<VehicleModel> Models = new List<VehicleModel>
        {
            new VehicleModel(VehicleModelType.Landstalker, 25000, 50.0f, 2.0f, 10),
            new VehicleModel(VehicleModelType.Bravura, 10000, 35.0f, 1.5f, 6),
            new VehicleModel(VehicleModelType.Buffalo, 50000, 45.0f, 3.0f, 4),
            new VehicleModel(VehicleModelType.Linerunner,100000,100.0f,12.5f,8),
            new VehicleModel(VehicleModelType.Perenniel,17000,65.0f,6.0f,12),
            new VehicleModel(VehicleModelType.Sentinel,30000,50.0f,7.0f,6),
            new VehicleModel(VehicleModelType.Dumper,-1,300.0f,20.0f,0),
            new VehicleModel(VehicleModelType.Firetruck,-1,80.0f,8.0f,15),
            new VehicleModel(VehicleModelType.Trashmaster,-1,80.0f,8.0f,0),
            new VehicleModel(VehicleModelType.Stretch,120000,60.0f,9.0f,6),
            new VehicleModel(VehicleModelType.Manana,5000,30.0f,3.0f,3),
            new VehicleModel(VehicleModelType.Infernus,300000,50.0f,12.0f,4),
        };
    }
}