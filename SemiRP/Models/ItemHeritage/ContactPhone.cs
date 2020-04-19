using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemiRP.Models.ItemHeritage
{
    public class ContactPhone
    {
        public ContactPhone()
        {

        }

        public ContactPhone(string name, string number)
        {
            this.Name = name;
            this.Number = number;
        }

        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Number { get; set; }
    }
}
