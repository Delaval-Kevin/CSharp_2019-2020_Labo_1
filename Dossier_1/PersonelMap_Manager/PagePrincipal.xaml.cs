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
    public partial class PagePrincipal : UserControl, IPageChange
    {
        #region EVENEMENTS
        public event Action<int, MyPersonalMapData> pageChange;
        #endregion

        #region VARIABLES
        private MyPersonalMapData _user;
        #endregion

        #region CONSTRUCTEURS
        public PagePrincipal(MyPersonalMapData user)
        {
            InitializeComponent();
           
            _user = user; 
            StatBar.Text = "User connected : " + _user.Nom + " " + _user.Prenom; //Affichage dans la StatusBar
        }
        #endregion

        #region BOUTONS
        //Fonction d'ouverture pour la fenêtre AboutBox
        private void About_Box_Button(object sender, RoutedEventArgs e)
        {
            Window about = new AboutBox();
            about.ShowDialog(); //bloque la page 
        }

        //Fonction d'ouverture pour la fenêtre Option
        private void Option_Button(object sender, RoutedEventArgs e)
        {
            Window option = new Option();
            option.Show(); //n'empêche pas la page principale de fonctionner
        }

        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            pageChange?.Invoke(1, null);
        }

        private void Open_Button(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Button(object sender, RoutedEventArgs e)
        {

        }

        private void POI_Import_Button(object sender, RoutedEventArgs e)
        {

        }

        private void POI_Export_Button(object sender, RoutedEventArgs e)
        {

        }

        private void Trajet_Import_Button(object sender, RoutedEventArgs e)
        {

        }

        private void Trajet_Export_Button(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
