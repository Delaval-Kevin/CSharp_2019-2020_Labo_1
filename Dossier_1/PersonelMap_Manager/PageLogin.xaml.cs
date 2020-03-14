using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyCartographyObjects;

namespace PersonelMap_Manager
{
    public partial class PageLogin : UserControl, IPageChange
    {
        #region EVENEMENTS
        public event Action<int, MyPersonalMapData> pageChange;
        #endregion

        #region VARIABLES
        private bool _errorFN = false;
        private bool _errorLN = false;
        #endregion

        #region CONSTRUCTEURS
        public PageLogin()
        {
            InitializeComponent();
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
                    _errorFN = true;
                } 
                if(LastNameBox.Text.Length < 1)
                {
                    LastNameBox.Text = "Please enter your last name";
                    LastNameBox.SetCurrentValue(ForegroundProperty, Brushes.Red);
                    _errorLN = true;
                }
            }
            else
            {
                if(_errorFN == false && _errorLN == false)
                {
                    MyPersonalMapData user = new MyPersonalMapData(FirstNameBox.Text, LastNameBox.Text);

                    try
                    {
                        user.Load();
                        pageChange?.Invoke(2, user);
                    }
                    catch(Exception)
                    {
                        MessageBoxResult ret = MessageBox.Show("This profile does not exist yet, do you want to create a new one?",
                        "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if (ret == MessageBoxResult.Yes)
                        {
                            pageChange?.Invoke(2, user);
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

            if (_errorFN == true && tmp == FirstNameBox)
            {
                tmp.Text = "";
                tmp.SetCurrentValue(ForegroundProperty, Brushes.WhiteSmoke);
                _errorFN = false;
            }
            if (_errorLN == true && tmp == LastNameBox)
            {
                tmp.Text = "";
                tmp.SetCurrentValue(ForegroundProperty, Brushes.WhiteSmoke);
                _errorLN = false;
            }
        }
        #endregion
    }
}
