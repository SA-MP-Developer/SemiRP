using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;
using SemiRP.Models;
using SemiRP.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace SemiRP.PlayerSystems
{
    public class PlayerRegistration
    {
        MenuDialog menu;
        MenuDialogItem passwordItem;
        MenuDialogItem emailItem;

        public PlayerRegistration()
        {
            menu = new MenuDialog("Inscription", "Valider", continueName : "Continuer ...");

            passwordItem = new MenuDialogItem(Color.Red + "Mot de passe");
            passwordItem.Selected += PasswordSelect;

            emailItem = new MenuDialogItem(Color.Red + "Email");
            emailItem.Selected += EmailSelect;

            menu.AddItem(passwordItem);
            menu.AddItem(emailItem);
        }

        public void Show(BasePlayer player)
        {
            menu.Show(player);
        }

        public MenuDialog GetMenu()
        {
            return menu;
        }

        private void PasswordSelect(object sender, MenuDialogItemEventArgs e)
        {
            InputDialog passwordDialog = new InputDialog("Inscription / Mot de passe",
                                                "Veuillez entrer un mot de passe de plus de 7 caractères.",
                                                true, "Confirmer", "Retour");
            passwordDialog.Response += (sender, eventArg) =>
            {
                if (eventArg.DialogButton == DialogButton.Right)
                {
                    menu.Show(e.Player);
                    return;
                }

                if (eventArg.InputText.Length < 8)
                {
                    passwordDialog.Show(eventArg.Player);
                    return;
                }

                e.ParentData["password"] = PasswordHasher.Hash(eventArg.InputText);
                passwordItem.Name = Color.Green + "Mot de passe";
                menu.Show(e.Player);
            };

            passwordDialog.Show(e.Player);
        }

        private void EmailSelect(object sender, MenuDialogItemEventArgs e)
        {
            InputDialog emailDialog = new InputDialog("Inscription / Email",
                                                "Veuillez entrer un email pour vous contacter.",
                                                false, "Confirmer", "Retour");

            emailDialog.Response += (sender, eventArg) =>
            {
                if (eventArg.DialogButton == DialogButton.Right)
                {
                    menu.Show(e.Player);
                    return;
                }

                if (! new EmailAddressAttribute().IsValid(eventArg.InputText))
                {
                    emailDialog.Show(eventArg.Player);
                    return;
                }

                e.ParentData["email"] = eventArg.InputText;
                emailItem.Name = Color.Green + "Email";
                menu.Show(e.Player);
            };

            emailDialog.Show(e.Player);
        }
    }
}
