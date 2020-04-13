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
        private int id;

        private int interior;
        private int virtualworld;

        private float x;
        private float y;
        private float z;

        private float rx;
        private float ry;
        private float rz;

        public SpawnLocation()
        {
        }

        public SpawnLocation(int id, int interior, int virtualworld, float x, float y, float z, float rx, float ry, float rz)
        {
            this.Id = id;
            this.Interior = interior;
            this.VirtualWorld = virtualworld;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.RotX = rx;
            this.RotY = ry;
            this.RotZ = rz;
        }

        [Key]
        public int Id { get => id; set => id = value; }
        public int Interior { get => interior; set => interior = value; }
        public int VirtualWorld { get => virtualworld; set => virtualworld = value; }

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public float Z { get => z; set => z = value; }

        public float RotX { get => rx; set => rx = value; }
        public float RotY { get => ry; set => ry = value; }
        public float RotZ { get => rz; set => rz = value; }

        [NotMapped]
        public Vector3 Position { get => new Vector3(x, y, z); set { x = value.X; y = value.Y;  z = value.Z; } }
        [NotMapped]
        public Vector3 Rotation { get => new Vector3(rx, ry, rz); set { rx = value.X; ry = value.Y; rz = value.Z; } }


    }
}
