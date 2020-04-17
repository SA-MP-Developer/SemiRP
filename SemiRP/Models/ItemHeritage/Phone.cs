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
        private string phoneNumberCaller;
        private bool isCalling;

        public Phone()
        {

        }

        public Phone(string number, bool defaultPhone, bool isRinging, string phoneNumberCaller, bool isCalling)
        {
            this.Number = number;
            this.DefaultPhone = defaultPhone;
            this.IsRinging = isRinging;
            this.PhoneNumberCaller = phoneNumberCaller;
            this.IsCalling = isCalling;
        }

        public string Number { get => number; set => number = value; }
        public bool DefaultPhone { get => defaultPhone; set => defaultPhone = value; }
        [NotMapped]
        public bool IsRinging { get => isRinging; set => isRinging = value; }
        [NotMapped]
        public string PhoneNumberCaller { get => phoneNumberCaller; set => phoneNumberCaller = value; }
        [NotMapped]
        public bool IsCalling { get => isCalling; set => isCalling = value; }
    }
}
