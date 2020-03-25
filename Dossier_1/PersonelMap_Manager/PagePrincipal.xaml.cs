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
using MyCartographyObjects;
using Microsoft.Maps.MapControl.WPF;

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

        #region PROPRIETES
        public MyPersonalMapData User
        {
            get { return _user; }
            set { _user = value; }
        }
        #endregion

        #region CONSTRUCTEURS
        public PagePrincipal(MyPersonalMapData user)
        {
            InitializeComponent();
           
            User = user;
            ListDock.DataContext = User;
            ListeCoo.ItemsSource = User.Liste;
            AffichePin();
            StatBar.Text = "User connected : " + User.Nom + " " + User.Prenom; //Affichage dans la StatusBar
        }
        #endregion

        #region BOUTONS
        //Fonction d'ouverture pour la fenêtre AboutBox
        private void About_Box_Button(object sender, RoutedEventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog(); //bloque la page 
        }

        //Fonction d'ouverture pour la fenêtre Option
        private void Option_Button(object sender, RoutedEventArgs e)
        {
            Option option = new Option();
            option.colorChange += Option_colorChange; 
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
            ChangeUserWindow changeProf = new ChangeUserWindow(_user);
            changeProf.ShowDialog(); //bloque la page 
            StatBar.Text = "User " + User.Nom + " " + User.Prenom + " is loaded"; //Affichage dans la StatusBar
        }

        //Fonction pour sauvegarder le profil
        private void Save_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                _user.Save();
                StatBar.Text = "User " + User.Nom + " " + User.Prenom + " is saved"; //Affichage dans la StatusBar
            }
            catch(Exception exc)//"An error occurred while saving the profile"
            {
                ErrorWindow error = new ErrorWindow(exc.Message);
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

        //Fonction qui au double click sur la carte permet d'ajouter un point
        private void Map_Selection(object sender, MouseButtonEventArgs e)
        {
            //Désactive l'action par défaut du double_click
            e.Handled = true;

            //Avoir la coordonée de la souris
            Point point = e.GetPosition(map);

            //Conversion de la souris en coordonnées
            Location pinLocation = map.ViewportPointToLocation(point);
            

            if (Add.IsChecked == true)
            {
                AddItem(pinLocation);
            }

            if (Delete.IsChecked == true)
            {
                DeleteItem(pinLocation);
            }

            if (Select.IsChecked == true)
            {
                SelectItem(pinLocation);
            }

            //Recharge la liste
            reloadList();
        }
        #endregion

        #region METHODES
        private void AffichePin()
        {
            foreach (Coordonnees coo in User.Liste)
            {
                Location pinLocation = new Location(coo.Latitude, coo.Longitude);

                //Le pin a ajouter à la carte
                Pushpin pin = new Pushpin();
                pin.Location = pinLocation;

                //Ajoute le pin a la carte
                map.Children.Add(pin);
            }
        }

        private void reloadList()
        {
            ListeCoo.ItemsSource = null;
            ListeCoo.ItemsSource = User.Liste;
        }

        private void AddItem(Location pinLocation)
        {
            if(POI_Box.IsSelected == true)
            {
                //La position du pin sur la carte
                Pushpin pin = new Pushpin();
                pin.Location = pinLocation;

                POI TmpPoi = new POI(pinLocation.Latitude, pinLocation.Longitude, "");

                OptPOI text = new OptPOI(TmpPoi);
                text.ShowDialog(); //bloque la page 

                pin.Tag = TmpPoi;

                //Ajoute le pin a la carte
                map.Children.Add(pin);

                //Ajoute à la liste
                User.Liste.Add(TmpPoi);
            }

            if (Polyline_Box.IsSelected == true)
            {
                Polyline TmpPolyline = new Polyline(new Coordonnees(pinLocation.Latitude, pinLocation.Longitude));



                OptPolyline text = new OptPolyline(TmpPolyline);
                text.ShowDialog(); //bloque la page 

                //Ajoute à la liste
                User.Liste.Add(TmpPolyline);
            }
        }

        private void SelectItem(Location pinLocation)
        {
            foreach (Coordonnees coo in User.Liste)
            {
                if (coo.IsPointClose(pinLocation, 0.5))
                {
                    ErrorWindow error = new ErrorWindow("OK");
                    error.ShowDialog(); //bloque la page 
                }
            }
        }

        private void DeleteItem(Location pinLocation)
        {
            for (int i = 0; i < User.Liste.Count(); i++)
            {

                if (User.Liste[i] is IIsPointClose)
                {
                    IIsPointClose coo = User.Liste[i] as IIsPointClose;
                    if (coo.IsPointClose(pinLocation, 0.5))
                    {
                        ICartoObj cooTmp = coo as ICartoObj;
                        User.Liste.Remove(cooTmp);
                        map.Children.RemoveAt(i);
                    }
                }
            }
        }

        //Fonction appelée par l'évenement de la fenetre option
        private void Option_colorChange(Color text, Color back)
        {
            ListeCoo.Foreground = new SolidColorBrush(text);
            ListeCoo.Background = new SolidColorBrush(back);
        }
        #endregion
    }
}
