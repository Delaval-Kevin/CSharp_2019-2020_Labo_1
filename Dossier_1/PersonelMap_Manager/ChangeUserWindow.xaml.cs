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
using System.Windows.Shapes;
using MyCartographyObjects;

namespace PersonelMap_Manager
{
    public partial class ChangeUserWindow : Window
    {
        #region VARIABLES
        MyPersonalMapData _user;
        #endregion

        #region PROPRIETES
        public MyPersonalMapData User
        {
            get { return _user; }
            set { _user = value; }
        }
        #endregion

        #region CONSTRUCTEURS
        public ChangeUserWindow(MyPersonalMapData user)
        {
            InitializeComponent();
            User = user;
        }
        #endregion

        #region BOUTONS
        private void Cancel_Button(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OK_Button(object sender, RoutedEventArgs e)
        {
            MyPersonalMapData tmpUser = new MyPersonalMapData(lastName.Text, firstName.Text);
            
            try
            {
                tmpUser.Load(); //Test s'il existe

                User.Nom = lastName.Text;
                User.Prenom = firstName.Text;
                User.Load();
                this.Close();
            }
            catch(Exception)
            {
                Window error = new ErrorWindow("An error occurred while loading the profile");
                error.ShowDialog(); //bloque la page 
            }
        }
        #endregion
    }
}
