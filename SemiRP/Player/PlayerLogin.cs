using SampSharp.GameMode.Display;
using SampSharp.GameMode.World;
using SampSharp.GameMode.SAMP;
using System;
using System.Collections.Generic;
using System.Text;
using SampSharp.GameMode.Definitions;
using SemiRP.Utils;

namespace SemiRP
{
    partial class Player : BasePlayer
    {
        private void LogPlayer()
        {
            ShowLoginDialog(0);
        }

        private void ShowLoginDialog(int attempts)
        {
            string text;
            if (attempts == 0)
            {
                text = "Re-bonjour {0} !\nDernière connexion le {1} ({2}).\nVeuillez entrer votre mot de passe.";
                text = string.Format(text, this.Name, AccountData.LastConnectionTime.ToString(), AccountData.LastConnectionIP); ;
            }
            else if (attempts < PASSWORD_MAX_ATTEMPTS)
            {
                text = "Mauvais mot de passe !\nIl vous reste " + Color.DarkRed + (PASSWORD_MAX_ATTEMPTS - attempts) + Color.White + ".";
            }
            else
            {
                this.Kick();
                return;
            }

            var loginDialog = new InputDialog("Connexion", text, true, "Connexion", "Quitter");
            loginDialog.Response += (sender, e) => {
                if (e.DialogButton == DialogButton.Right)
                {
                    this.Kick();
                    return;
                }

                if (!PasswordHasher.Verify(e.InputText, AccountData.Password))
                {
                    ShowLoginDialog(attempts++);
                    return;
                }

                CharacterChoice();
            };
            loginDialog.Show(this);
        }
    }
}
