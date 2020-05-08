using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SemiRP.Models.ItemHeritage
{
    public class Phone : Item
    {
        public Phone()
        {

        }

        public Phone(string number, bool defaultPhone, bool isRinging, string phoneNumberCaller, bool isCalling, int maxContact)
        {
            Number = number;
            DefaultPhone = defaultPhone;
            IsRinging = IsRinging;
            PhoneNumberCaller = phoneNumberCaller;
            IsCalling = isCalling;
            MaxContact = maxContact;
            PhoneBook = new List<ContactPhone>();
            this.Name = "Téléphone (" + number + ")";
            this.Quantity = 1;
            this.ModelId = 18872;
            this.Anonym = false;
        }


        public string Number { get; set; }
        public bool DefaultPhone { get; set; }
        [NotMapped]
        public bool IsRinging { get; set; }
        [NotMapped]
        public string PhoneNumberCaller { get; set; }
        [NotMapped]
        public bool IsCalling { get; set; }
        public virtual List<ContactPhone> PhoneBook { get; set; }
        public int MaxContact { get; set; }
        public bool Anonym { get; set; }
    }
}
