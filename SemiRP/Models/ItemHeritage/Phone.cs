using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SemiRP.Models.ItemHeritage
{
    public class Phone : Item
    {
        private string number;
        private bool defaultPhone;
        private bool isRinging;
        public Phone(string number, bool defaultPhone, bool isRinging)
        {
            this.Number = number;
            this.DefaultPhone = defaultPhone;
            this.IsRinging = isRinging;
        }

        public string Number { get => number; set => number = value; }
        public bool DefaultPhone { get => defaultPhone; set => defaultPhone = value; }
        [NotMapped]
        public bool IsRinging { get => isRinging; set => isRinging = value; }
    }
}
