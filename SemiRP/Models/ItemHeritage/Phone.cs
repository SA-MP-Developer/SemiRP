using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Models.ItemHeritage
{
    public class Phone : Item
    {
        private string number;
        private bool defaultPhone;
        public Phone(string number, bool defaultPhone)
        {
            this.Number = number;
            this.DefaultPhone = defaultPhone;
        }

        public string Number { get => number; set => number = value; }
        public bool DefaultPhone { get => defaultPhone; set => defaultPhone = value; }
    }
}
