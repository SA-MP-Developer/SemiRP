using System;
using System.Collections.Generic;
using System.Configuration;
using SampSharp.GameMode;

namespace SemiRP
{
    public class GameMode : BaseMode
    {
        #region Overrides of BaseMode

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Console.WriteLine("\n------------------------------------------");
            Console.WriteLine(" SemiRP originally made by VickeS and Papawy");
            Console.WriteLine("-------------------------------------------\n");

        }

        #endregion
    }
}