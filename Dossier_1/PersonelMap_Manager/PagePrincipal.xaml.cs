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
          //  _user.Liste.Add(new POI());
          //  _user.Liste.Add(new POI(12.21,125.21,"zefezf"));
          //  _user.Liste.Add(new POI(11,111,"test"));

            ListeCoo.ItemsSource = _user.Liste;
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

        //Fonction pour quitter la page principale et revenir à la page login
        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            pageChange?.Invoke(1, null);
        }

        //Fonction pour charger un autre profil
        private void Open_Button(object sender, RoutedEventArgs e)
        {
            Window changeProf = new ChangeUserWindow(_user);
            changeProf.ShowDialog(); //bloque la page 
            StatBar.Text = "User " + _user.Nom + " " + _user.Prenom + " is loaded"; //Affichage dans la StatusBar
        }

        //Fonction pour sauvegarder le profil
        private void Save_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                _user.Save();
                StatBar.Text = "User " + _user.Nom + " " + _user.Prenom + " is saved"; //Affichage dans la StatusBar
            }
            catch(Exception exc)//"An error occurred while saving the profile"
            {
                Window error = new ErrorWindow(exc.Message);
                error.ShowDialog(); //bloque la page 
            }
        }

        //Fonction pour importer les POI d'un ficher .CSV
        private void POI_Import_Button(object sender, RoutedEventArgs e)
        {

        }

        //Fonction pour exporter les POI d'un ficher .CSV
        private void POI_Export_Button(object sender, RoutedEventArgs e)
        {

        }

        //Fonction pour importer les Trajets d'un ficher .CSV
        private void Trajet_Import_Button(object sender, RoutedEventArgs e)
        {

        }

        //Fonction pour exporter les Trajets d'un ficher .CSV
        private void Trajet_Export_Button(object sender, RoutedEventArgs e)
        {

        }
        
        //Fonction qui permet de selectionner sur la carte quand 'select' est enclanché
        private void Select_Checked(object sender, RoutedEventArgs e)
        {

        }

        //Fonction qui permet de supprimer sur la carte quand 'delete' est enclanché
        private void Delete_Checked(object sender, RoutedEventArgs e)
        {

        }

        //Fonction qui permet d'ajouter sur la carte quand 'add' est enclanché
        private void Add_Checked(object sender, RoutedEventArgs e)
        {

        }
        
        //Fonction qui au double click sur la carte permet d'ajoutr un point
        private void Map_Selection(object sender, MouseButtonEventArgs e)
        {
            
        }
        #endregion
    }
}
