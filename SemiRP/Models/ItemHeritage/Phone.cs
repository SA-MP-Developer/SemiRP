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
        private List<ContactPhone> phoneBook;
        private int maxContact;

        public Phone()
        {

        }

        public Phone(string number, bool defaultPhone, bool isRinging, string phoneNumberCaller, bool isCalling, int maxContact)
        {
            this.number = number;
            this.defaultPhone = defaultPhone;
            this.isRinging = isRinging;
            this.phoneNumberCaller = phoneNumberCaller;
            this.isCalling = isCalling;
            this.MaxContact = maxContact;
            this.phoneBook = new List<ContactPhone>();
        }


        public string Number { get => number; set => number = value; }
        public bool DefaultPhone { get => defaultPhone; set => defaultPhone = value; }
        [NotMapped]
        public bool IsRinging { get => isRinging; set => isRinging = value; }
        [NotMapped]
        public string PhoneNumberCaller { get => phoneNumberCaller; set => phoneNumberCaller = value; }
        [NotMapped]
        public bool IsCalling { get => isCalling; set => isCalling = value; }
        public List<ContactPhone> PhoneBook { get; set; }
        public int MaxContact { get => maxContact; set => maxContact = value; }
    }
}
