using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;
using SemiRP.Models;
using SemiRP.Utils;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace SemiRP.PlayerSystems
{
    class PlayerRegistration
    {
        private Player player;

        private string password;
        private string email;

        #region Dialogs

        private ListDialog registrationMenu;
        private InputDialog passwordDialog;
        private InputDialog emailDialog;

        #endregion

        public PlayerRegistration(Player player)
        {
            this.player = player;

            registrationMenu = new ListDialog("Inscription", "Valider", "");
            registrationMenu.AddItem("[" + Color.Red + "KO" + Color.White + "] Mot de passe");
            registrationMenu.AddItem("[" + Color.Red + "KO" + Color.White + "] eMail");
            registrationMenu.AddItem("Continuer");
            registrationMenu.Response += MenuDialog;

            passwordDialog = new InputDialog("Inscription / Mot de passe",
                                    "Entrez le mot de passe que vous utiliserez pour vous connecter.\nIl doit être superieur ou égal à 8 caractères.",
                                    true, "Valider", "Retour");
            passwordDialog.Response += PasswordDialog;

            emailDialog = new InputDialog("Inscription / eMail",
                                    "Entrez votre adresse eMail.", false, "Valider", "Retour");
            emailDialog.Response += MailDialog;

            email = "";
            password = "";
        }

        public void Begin()
        {
            registrationMenu.Show(player);
        }

        private bool RegisterPlayer()
        {
            if (password.Length == 0 || email.Length == 0)
                return false;

            using (var db = new ServerDbContext())
            {
                Account acc = new Account();
                acc.Email = email;
                acc.Username = player.Name;
                acc.Nickname = player.Name;
                acc.Password = password;
                acc.LastConnectionIP = player.IP;
                acc.LastConnectionTime = DateTime.Now;

                db.Add(acc);
                db.SaveChanges();
            }

            return true;
        }

        private void BuildAndShowMenuDialog()
        {
            registrationMenu.Items.Clear();

            if (password.Length != 0)
                registrationMenu.AddItem("[" + Color.Green + "OK" + Color.White + "] Mot de passe");
            else
                registrationMenu.AddItem("[" + Color.Red + "KO" + Color.White + "] Mot de passe");

            if (email.Length != 0)
                registrationMenu.AddItem("[" + Color.Green + "OK" + Color.White + "] eMail");
            else
                registrationMenu.AddItem("[" + Color.Red + "KO" + Color.White + "] eMail");

            registrationMenu.AddItem("Continuer");

            registrationMenu.Show(player);
        }

        private void MenuDialog(object sender, DialogResponseEventArgs e)
        {
            switch (e.ListItem)
            {
                case 0:
                    passwordDialog.Show(player);
                    break;
                case 1:
                    emailDialog.Show(player);
                    break;
                case 2:
                    {
                        if (RegisterPlayer())
                            return;
                        else
                            BuildAndShowMenuDialog();
                        break;
                    }
                default:
                    BuildAndShowMenuDialog();
                    break;
            }
        }

        private void PasswordDialog(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton == DialogButton.Right)
            {
                BuildAndShowMenuDialog();
                return;
            }

            if (e.InputText.Length < 8)
            {
                passwordDialog.Show(player);
                return;
            }

            password = PasswordHasher.Hash(e.InputText);
            BuildAndShowMenuDialog();
        }

        private void MailDialog(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton == DialogButton.Right)
            {
                BuildAndShowMenuDialog();
                return;
            }

            try
            {
                MailAddress m = new MailAddress(e.InputText);

                email = e.InputText;
                BuildAndShowMenuDialog();
            }
            catch (FormatException)
            {
                emailDialog.Message = Color.Yellow + "L'eMail que vous avez entré n'est pas valide !\n\n" + Color.White + "Entrez votre adresse eMail.";
                emailDialog.Show(player);
            }
        }
    }
}
