using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;
using SemiRP.Models;
using SemiRP.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static SemiRP.Models.Character;

namespace SemiRP.PlayerSystems
{
    public class PlayerCharacterCreation
    {
        MenuDialog menu;
        MenuDialogItem nameItem;
        MenuDialogItem ageItem;
        MenuDialogItem sexItem;

        public PlayerCharacterCreation()
        {
            menu = new MenuDialog("Création de personnage", "Valider", continueName: "Continuer ...");

            nameItem = new MenuDialogItem(Color.Red + "Nom");
            nameItem.Selected += NameSelect;

            ageItem = new MenuDialogItem(Color.Red + "Age");
            ageItem.Selected += AgeSelect;

            sexItem = new MenuDialogItem(Color.Red + "Sexe");
            sexItem.Selected += SexSelect;

            menu.AddItem(nameItem);
            menu.AddItem(ageItem);
            menu.AddItem(sexItem);
        }

        public void Show(BasePlayer player)
        {
            menu.Show(player);
        }

        public MenuDialog GetMenu()
        {
            return menu;
        }

        private void NameSelect(object sender, MenuDialogItemEventArgs e)
        {
            InputDialog nameDialog = new InputDialog("Création de personnage / Nom",
                                                "Veuillez entrer un nom pour votre de personnage de la forme Prénom_Nom.",
                                                false, "Confirmer", "Retour");
            nameDialog.Response += (sender, eventArg) =>
            {
                if (eventArg.DialogButton == DialogButton.Right)
                {
                    menu.Show(e.Player);
                    return;
                }

                var regex = new Regex(@"[A-Z][a-z]+_[A-Z][a-z]+([A-Z][a-z]+)*");
                if (!regex.IsMatch(eventArg.InputText))
                {
                    nameDialog.Show(eventArg.Player);
                    return;
                }

                e.ParentData["name"] = eventArg.InputText;
                nameItem.Name = Color.Green + "Nom";
                menu.Show(e.Player);
            };

            nameDialog.Show(e.Player);
        }

        private void AgeSelect(object sender, MenuDialogItemEventArgs e)
        {
            InputDialog ageDialog = new InputDialog("Création de personnage / Age",
                                                "Veuillez entrer l'âge votre personnage.",
                                                false, "Confirmer", "Retour");

            ageDialog.Response += (sender, eventArg) =>
            {
                if (eventArg.DialogButton == DialogButton.Right)
                {
                    menu.Show(e.Player);
                    return;
                }

                uint age = 0;

                if (!(uint.TryParse(eventArg.InputText, out age)))
                {
                    ageDialog.Show(eventArg.Player);
                    return;
                }

                if (age < 16 || age > 90)
                {
                    ageDialog.Show(eventArg.Player);
                    return;
                }

                e.ParentData["age"] = age;
                ageItem.Name = Color.Green + "Age";
                menu.Show(e.Player);
            };

            ageDialog.Show(e.Player);
        }

        private void SexSelect(object sender, MenuDialogItemEventArgs e)
        {
            MessageDialog sexDialog = new MessageDialog("Création de personnage / Sexe",
                                                "Veuillez choisir le sexe de votre personnage.",
                                                "Homme", "Femme");

            sexDialog.Response += (sender, eventArg) =>
            {
                if (eventArg.DialogButton == DialogButton.Right)
                {
                    e.ParentData["sex"] = Character.CharSex.WOMAN;
                    sexItem.Name = Color.Green + "Sexe";
                }
                else
                {
                    e.ParentData["sex"] = Character.CharSex.MAN;
                    sexItem.Name = Color.Green + "Sexe";
                }
                menu.Show(e.Player);
            };

            sexDialog.Show(e.Player);
        }
    }
}
