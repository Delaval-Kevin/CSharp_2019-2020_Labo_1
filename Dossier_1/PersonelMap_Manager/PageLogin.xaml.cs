using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using MyCartographyObjects;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace PersonelMap_Manager
{
    public partial class PageLogin : UserControl, IPageChange
    {
        #region EVENEMENTS
        public event Action<int, MyPersonalMapData> PageChange;
        #endregion

        #region VARIABLES
        private bool _errorFN = false;
        private bool _errorLN = false;
        private MyPersonalMapData _user;
        #endregion

        #region PROPRIETES
        public MyPersonalMapData User
        {
            get { return _user; }
            set { _user = value; }
        }

        public bool ErrorFN
        {
            get { return _errorFN; }
            set { _errorFN = value; }
        }

        public bool ErrorLN
        {
            get { return _errorLN; }
            set { _errorLN = value; }
        }
        #endregion

        #region CONSTRUCTEURS
        public PageLogin()
        {
            InitializeComponent();
            User = new MyPersonalMapData("Delaval", "Kevin");
            CurrentStack.DataContext = User;
        }
        #endregion

        #region BOUTONS
        //Fonction qui permet d'appeler la page 2 "Principal"
        private void Submit_Button(object sender, RoutedEventArgs e)
        {
            //Vefification si la TextBox est vide 
            if (FirstNameBox.Text.Length < 1 || LastNameBox.Text.Length < 1)
            {
                if (FirstNameBox.Text.Length < 1)
                {
                    FirstNameBox.Text = "Please enter your first name";
                    FirstNameBox.SetCurrentValue(ForegroundProperty, Brushes.Red);
                    ErrorFN = true;
                } 
                if(LastNameBox.Text.Length < 1)
                {
                    LastNameBox.Text = "Please enter your last name";
                    LastNameBox.SetCurrentValue(ForegroundProperty, Brushes.Red);
                    ErrorLN = true;
                }
            }
            else
            {
                if(ErrorFN == false && ErrorLN == false)
                {
                    try
                    {
                        User.Load();
                        PageChange?.Invoke(2, User);
                    }
                    catch(Exception)
                    {
                        MessageBoxResult ret = MessageBox.Show("This profile does not exist yet, do you want to create a new one?",
                        "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if (ret == MessageBoxResult.Yes)
                        {
                            User.Save();
                            PageChange?.Invoke(2, User);
                        }
                    }
                }
            }
        }
        #endregion

        #region EVENTS
        //Evenement pour effacer le contenu si erreur
        private void MouseEnter_Event(object sender, MouseEventArgs e)
        {
            TextBox tmp = sender as TextBox;

            if (ErrorFN == true && tmp == FirstNameBox)
            {
                tmp.Text = "";
                tmp.SetCurrentValue(ForegroundProperty, Brushes.WhiteSmoke);
                ErrorFN = false;
            }
            if (ErrorLN == true && tmp == LastNameBox)
            {
                tmp.Text = "";
                tmp.SetCurrentValue(ForegroundProperty, Brushes.WhiteSmoke);
                ErrorLN = false;
            }
        }
        #endregion
    }
}
