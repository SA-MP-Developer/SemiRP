using SemiRP.Models;
using SemiRP.Models.ItemHeritage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemiRP.Utils.ItemUtils
{
    public class PhoneHelper
    {
        public static List<Phone> GetAllPhone()
        {

            using (var db = new ServerDbContext())
            {
                return db.Phones.ToList();
            }
        }
        public static Phone GetPhoneByNumber(string number)
        {
            using (var db = new ServerDbContext())
            {
                return db.Phones.Select(x => x).Where(w => w.Number == number).FirstOrDefault();
            }
        }
        public static Character GetPhoneOwner(Phone phone)
        {
            using (var db = new ServerDbContext())
            {
                Container container = db.Containers.Select(x => x).Where(w => w == phone.CurrentContainer).FirstOrDefault();
                if(container != null)
                {
                    Character character = db.Characters.Select(x => x).Where(w => w.Inventory == container).FirstOrDefault();
                    return character;
                }
                return null;
            }
        }
    }
}
