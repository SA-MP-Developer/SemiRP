using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SemiRP.Dialog
{
    public class FormDialogEventArgs
    {
        public Dictionary<string, object> Data { get; set; }
        public Player Player { get; set; }
        public bool Exited { get; set; }
    }

    public class FormDialog
    {
        public event EventHandler<FormDialogEventArgs> Response;

        interface Field
        {
            string Title { get; set; }
            string Dataname { get; set; }
            Type Type { get; }

            bool Hidden { get; set; }
        }

        #region fields

        struct FieldNumber : Field
        {
            public string Title { get; set; }
            public string Dataname { get; set; }
            public Type Type => typeof(int);

            public Predicate<int> Condition;
            public bool Hidden { get; set; }
        }

        struct FieldFloat : Field
        {
            public string Title { get; set; }
            public string Dataname { get; set; }
            public Type Type => typeof(float);

            public Predicate<float> Condition;
            public bool Hidden { get; set; }
        }

        struct FieldString : Field
        {
            public string Title { get; set; }
            public string Dataname { get; set; }
            public Type Type => typeof(string);

            public Predicate<string> Condition;
            public bool Hidden { get; set; }
        }

        struct FieldBool : Field
        {
            public string Title { get; set; }
            public string Dataname { get; set; }
            public Type Type => typeof(bool);
            public string ChoiceLeft { get; set; }
            public string ChoiceRight { get; set; }

            public Predicate<bool> Condition;
            public bool Hidden { get; set; }
        }

        #endregion

        public string Title { get; private set; }
        public string ContinueButton { get; private set; }
        public bool CanQuit { get; private set; }

        private List<Field> fields;

        private Dictionary<string, object> datas;

        public FormDialog(string title, string continueBtn = "Continuer", bool canQuit = true)
        {
            Title = title;
            ContinueButton = continueBtn;
            CanQuit = canQuit;

            fields = new List<Field>();
            datas = new Dictionary<string, object>();
        }

        #region fieldsFunctions

        public FormDialog AddFieldNumber(string title, string dataname, Predicate<int> condition, bool hide = false)
        {
            FieldNumber field = new FieldNumber();
            field.Title = title;
            field.Dataname = dataname;
            field.Condition = condition;
            field.Hidden = hide;

            fields.Add(field);

            return this;
        }

        public FormDialog AddFieldNumber(string title, string dataname, bool hide = false)
        {
            AddFieldNumber(title, dataname, i => true, hide);
            return this;
        }

        public FormDialog AddFieldFloat(string title, string dataname, Predicate<float> condition, bool hide = false)
        {
            FieldFloat field = new FieldFloat();
            field.Title = title;
            field.Dataname = dataname;
            field.Condition = condition;
            field.Hidden = hide;

            fields.Add(field);

            return this;
        }

        public FormDialog AddFieldFloat(string title, string dataname, bool hide = false)
        {
            AddFieldFloat(title, dataname, i => true, hide);
            return this;
        }

        public FormDialog AddFieldString(string title, string dataname, Predicate<string> condition, bool hide = false)
        {
            FieldString field = new FieldString();
            field.Title = title;
            field.Dataname = dataname;
            field.Condition = condition;
            field.Hidden = hide;

            fields.Add(field);

            return this;
        }

        public FormDialog AddFieldString(string title, string dataname, bool hide = false)
        {
            AddFieldString(title, dataname, i => true, hide);
            return this;
        }

        public FormDialog AddFieldBool(string title, string choiceLeft, string choiceRight, string dataname, Predicate<bool> condition)
        {
            FieldBool field = new FieldBool();
            field.Title = title;
            field.Dataname = dataname;
            field.Condition = condition;
            field.ChoiceLeft = choiceLeft;
            field.ChoiceRight = choiceRight;

            fields.Add(field);

            return this;
        }

        public FormDialog AddFieldBool(string title, string choiceLeft, string choiceRight, string dataname)
        {
            AddFieldBool(title, choiceLeft, choiceRight, dataname, i => true);
            return this;
        }

        #endregion

        public void Show(Player player)
        {
            ListDialog dialog = new ListDialog(Title, "Sélectionner", CanQuit ? "Quitter" : null);

            foreach (Field f in fields)
            {
                dialog.AddItem(f.Title);
            }

            dialog.AddItem(ContinueButton);
            dialog.Response += DialogResponse;
            dialog.Show(player);
        }

        private void DialogResponse(object sender, DialogResponseEventArgs e)
        {
            if (CanQuit && e.DialogButton == SampSharp.GameMode.Definitions.DialogButton.Right)
            {
                FormDialogEventArgs retArgs = new FormDialogEventArgs();
                retArgs.Player = (Player)e.Player;
                retArgs.Data = datas;
                retArgs.Exited = true;
                Response?.Invoke(this, retArgs);
                return;
            }

            if (e.ListItem > fields.Count)
            {
                Show((Player)e.Player);
                return;
            }

            if (e.ListItem == fields.Count) // It's over the count so it's after the last field
            {
                FormDialogEventArgs retArgs = new FormDialogEventArgs();
                retArgs.Player = (Player)e.Player;
                retArgs.Data = datas;
                retArgs.Exited = false;
                Response?.Invoke(this, retArgs);
                return;
            }

            if (fields[e.ListItem] is FieldBool)
            {
                FieldBool field = (FieldBool)fields[e.ListItem];
                MessageDialog dialog = new MessageDialog(Title, field.Title, field.ChoiceLeft, field.ChoiceRight);
                dialog.Response += (sender, e) =>
                {
                    if (!field.Condition(e.DialogButton == DialogButton.Left))
                    {
                        dialog.Show(e.Player);
                        return;
                    }

                    datas[field.Dataname] = e.DialogButton == DialogButton.Left;
                    Show((Player)e.Player);
                };
                dialog.Show(e.Player);
                return;
            }
            else
            {
                Field field = fields[e.ListItem];
                InputDialog dialog = new InputDialog(Title, field.Title, field.Hidden, "Continuer", "Retour");
                switch (field)
                {
                    case FieldNumber fn:
                        dialog.Response += (sender, e) =>
                        {
                            int res = 0;


                            if (!int.TryParse(e.InputText, out res) || !fn.Condition(res))
                            {
                                dialog.Show(e.Player);
                                return;
                            }

                            datas[field.Dataname] = res;
                            Show((Player)e.Player);
                        };
                        break;
                    case FieldFloat ff:
                        dialog.Response += (sender, e) =>
                        {
                            float res = 0;


                            if (!float.TryParse(e.InputText, out res) || !ff.Condition(res))
                            {
                                dialog.Show(e.Player);
                                return;
                            }

                            datas[field.Dataname] = res;
                            Show((Player)e.Player);
                        };
                        break;
                    case FieldString fs:
                        dialog.Response += (sender, e) =>
                        {
                            if (!fs.Condition(e.InputText))
                            {
                                dialog.Show(e.Player);
                                return;
                            }

                            datas[field.Dataname] = e.InputText;
                            Show((Player)e.Player);
                        };
                        break;

                };
                
                dialog.Show(e.Player);
                return;
            }
        }

    }
}
