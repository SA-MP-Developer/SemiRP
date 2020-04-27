using SampSharp.GameMode;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SemiRP.Models
{
    public class SpawnLocation
    {
        public SpawnLocation()
        {
        }

        public SpawnLocation(Vector3 position, float rotation, int interior = 0, int virtualworld = 0)
        {
            Position = position;
            RotX = 0f;
            RotY = 0f;
            RotZ = rotation;

            Interior = interior;
            VirtualWorld = virtualworld;
        }


        [Key]
        public int Id { get; set; }
        public int Interior { get; set; }
        public int VirtualWorld { get; set; }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float RotX { get; set; }
        public float RotY { get; set; }
        public float RotZ { get; set; }

        [NotMapped]
        public Vector3 Position { get => new Vector3(X, Y, Z); set { X = value.X; Y = value.Y;  Z = value.Z; } }
        [NotMapped]
        public Vector3 Rotation { get => new Vector3(RotX, RotY, RotZ); set { RotX = value.X; RotY = value.Y; RotZ = value.Z; } }


    }
}
