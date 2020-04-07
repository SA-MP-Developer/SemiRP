using SampSharp.GameMode.World;
using SampSharp.GameMode.SAMP;
using System;
using System.Collections.Generic;
using System.Text;
using SampSharp.GameMode.Display;

namespace SemiRP
{
    partial class Player : BasePlayer
    {
        private void RegisterPlayer()
        {
            string text = "Bonjour {0}, vous n'avez pas de compte et vous allez donc entrer dans la création de compte.";
            text = string.Format(text, this.Name);
            var welcomeDialog = new MessageDialog("Bienvenu !", text, "Continuer", "Quitter");
        }
    }
}
