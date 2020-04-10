using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using static SemiRP.Models.Character;

namespace SemiRP.PlayerSystems
{
    public class CharCreationDialogEndEventArgs : EventArgs
    {
        public string Name;
        public uint Age;
        public CharSex Sex;
    }

    class PlayerCharacterCreation
    {
        private readonly Player player;

        private string char_name;
        private uint char_age;
        private CharSex char_sex;

        #region Dialogs

        private readonly ListDialog creationMenu;
        private readonly InputDialog nameDialog;
        private readonly InputDialog ageDialog;
        private readonly MessageDialog sexDialog;

        #endregion

        public PlayerCharacterCreation(Player player)
        {
            this.player = player;

            char_name = "";
            char_age = 0;
            char_sex = CharSex.NOT_SET;

            creationMenu = new ListDialog("Personnage", "Valider", "");
            creationMenu.AddItem("[" + Color.Red + " " + Color.White + "] Nom");
            creationMenu.AddItem("[" + Color.Red + " " + Color.White + "] Age");
            creationMenu.AddItem("[" + Color.Red + " " + Color.White + "] Sexe");
            creationMenu.AddItem("Continuer");
            creationMenu.Response += MenuDialog;

            nameDialog = new InputDialog("Personnage / Nom", "Veuillez entrer le nom de votre personnage, sous la forme \"Prénom_Nom\"." , false, "Valider", "Retour");
            nameDialog.Response += NameDialog;

            ageDialog = new InputDialog("Personnage / Age", "Veuillez entrer l'age de votre personnage, entre 16 et 90 ans.", false, "Valider", "Retour");

            sexDialog = new MessageDialog("Personnage / Sexe", "Veuillez choisir le sexe de votre personnage.", "Homme", "Femme");

        }

        public void Begin()
        {
            creationMenu.Show(player);
        }

        protected virtual void OnDialogEnded(CharCreationDialogEndEventArgs e)
        {
            DialogEnded?.Invoke(this, e);
        }

        public event EventHandler<CharCreationDialogEndEventArgs> DialogEnded;

        private bool EndDialog()
        {
            if (char_name.Length == 0 || char_age == 0 || char_sex == CharSex.NOT_SET)
                return false;

            CharCreationDialogEndEventArgs e = new CharCreationDialogEndEventArgs
            {
                Name = char_name,
                Age = char_age,
                Sex = char_sex
            };

            OnDialogEnded(e);

            return true;
        }

        private void BuildAndShowMenuDialog()
        {
            creationMenu.Items.Clear();

            string name_item;
            if (char_name.Length != 0)
                name_item = "[" + Color.Green + "X" + Color.White + "] ";
            else
                name_item =  "[" + Color.Red + " " + Color.White + "] ";
            creationMenu.AddItem(name_item + "Nom");

            string age_item;
            if (char_age != 0)
                age_item = "[" + Color.Green + "X" + Color.White + "] ";
            else
                age_item = "[" + Color.Red + " " + Color.White + "] ";
            creationMenu.AddItem(age_item + "Age");

            string sex_item;
            if (char_sex != CharSex.NOT_SET)
                sex_item = "[" + Color.Green + "X" + Color.White + "] ";
            else
                sex_item = "[" + Color.Red + " " + Color.White + "] ";
            creationMenu.AddItem(sex_item + "Sexe");

            creationMenu.AddItem("Continuer");

            creationMenu.Show(player);
        }

        private void MenuDialog(object sender, DialogResponseEventArgs e)
        {
            switch (e.ListItem)
            {
                case 0:
                    nameDialog.Show(player);
                    break;
                case 1:
                    ageDialog.Show(player);
                    break;
                case 2:
                    sexDialog.Show(player);
                    break;
                case 4:
                    {
                        if (EndDialog())
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

        private void NameDialog(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton == DialogButton.Right)
            {
                BuildAndShowMenuDialog();
                return;
            }

            var regex = new Regex(@"[A-Z][a-z]+_[A-Z][a-z]+([A-Z][a-z]+)*");
            if (!regex.IsMatch(e.InputText))
            {
                nameDialog.Message = Color.Yellow + "Le nom entré n'est pas correct." + Color.White + "\n" + nameDialog.Message;
                BuildAndShowMenuDialog();
                return;
            }

            char_name = e.InputText;
            BuildAndShowMenuDialog();
        }

        private void AgeDialog(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton == DialogButton.Right)
            {
                BuildAndShowMenuDialog();
                return;
            }

            try
            {
                uint age = uint.Parse(e.InputText);
                if (age >= 16 || age <= 90)
                {
                    char_age = age;
                    BuildAndShowMenuDialog();
                    return;
                }

                ageDialog.Message = Color.Yellow + "L'age entré n'est pas correct." + Color.White + "\n" + nameDialog.Message;
                BuildAndShowMenuDialog();
                return;
            }
            catch (FormatException)
            {
                ageDialog.Message = Color.Yellow + "L'age entré n'est pas correct." + Color.White + "\n" + nameDialog.Message;
                BuildAndShowMenuDialog();
                return;
            }
        }

        private void SexDialog(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton == DialogButton.Right)
            {
                char_sex = CharSex.WOMAN;
                BuildAndShowMenuDialog();
                return;
            }
            char_sex = CharSex.MAN;
            BuildAndShowMenuDialog();
        }
    }
}
