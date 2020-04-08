using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Models
{
    class Container
    {
        private int id;

        public Container()
        {
        }

        public Container(int id)
        {
            this.Id = id;
        }

        public int Id { get => id; set => id = value; }
    }
}
