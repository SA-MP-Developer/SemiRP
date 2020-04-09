﻿using SampSharp.GameMode.Display;
using SampSharp.GameMode.World;
using SampSharp.GameMode.SAMP;
using System;
using System.Collections.Generic;
using System.Text;
using SampSharp.GameMode.Definitions;
using SemiRP.Utils;
using SampSharp.GameMode.Events;
using System.Runtime.CompilerServices;

namespace SemiRP.PlayerSystems
{
    class PlayerLogin
    {
        private Player player;
        private int maxAttemtps;

        private int attempts;
        private bool success;

        #region Dialogs

        private InputDialog passwordDialog;

        #endregion

        public PlayerLogin(Player player, int maxAttemtps = 3)
        {
            this.player = player;

            this.maxAttemtps = maxAttemtps;
            this.attempts = 0;

            this.success = false;

            string text = "Re-bonjour {0} !\nDernière connexion le {1} ({2}).\nVeuillez entrer votre mot de passe.";
            text = string.Format(text, player.Name, player.AccountData.LastConnectionTime.ToString(), player.AccountData.LastConnectionIP);
            passwordDialog = new InputDialog("Connexion", text, true, "Valider", "Quitter");
            passwordDialog.Response += LoginDialog;
        }

        public bool Begin()
        {
            passwordDialog.Show(player);

            return success;
        }

        private void LoginDialog(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton == DialogButton.Right)
            {
                success = false;
                return;
            }

            if (e.InputText.Length == 0)
            {
                passwordDialog.Show(player);
                return;
            }

            if (!PasswordHasher.Verify(e.InputText, player.AccountData.Password))
            {
                if (attempts < maxAttemtps)
                {
                    attempts++;
                    passwordDialog.Message = "Mauvais mot de passe !\nIl vous reste " + Color.DarkRed + (maxAttemtps - attempts) + Color.White + " essais.";
                    passwordDialog.Show(player);
                    return;
                }
                success = false;
                player.Kick();
                return;
            }

            success = true;
            return;
        }
    }
}
