using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Key]
        public int Id { get => id; set => id = value; }
    }
}
