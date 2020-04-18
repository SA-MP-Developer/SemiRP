using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Models.ItemHeritage
{
    public class ContactPhone
    {
        public ContactPhone(string name, string number)
        {
            this.name = name;
            this.number = number;
        }

        public String name { get; set; }
        public String number { get; set; }
    }
}
