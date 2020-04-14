using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SemiRP.Models.ContainerHeritage
{
    public class Inventory : Container
    {
        [Required]
        public Character Owner { get; set; }
    }
}
