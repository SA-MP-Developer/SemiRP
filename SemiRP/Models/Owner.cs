using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models
{
    public class Owner
    {
        public Owner()
        {
            Character = null;
            Group = null;
        }

        public Owner(Character chr)
        {
            Character = chr;
        }

        public Owner(Group grp)
        {
            Group = grp;
        }

        public bool IsOwner(Character chr)
        {
            return !(Character == null) || Character == chr;
        }

        public bool IsOwner(Group grp)
        {
            return !(Group == null) || Group == grp;
        }

        [Key]
        public int Id { get; set; }

        public virtual Character Character { get; set; }
        public virtual Group Group { get; set; }
    }
}
