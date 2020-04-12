using SemiRP.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Utils
{
    public static class SexUtils
    {
        public static string SexToString(Character.CharSex sex)
        {
            switch (sex)
            {
                case Character.CharSex.NOT_SET:
                    return "Non défini";
                case Character.CharSex.MAN:
                    return "Homme";
                case Character.CharSex.WOMAN:
                    return "Femme";
                default:
                    return "Non défini";
            }
        }
    }
}
