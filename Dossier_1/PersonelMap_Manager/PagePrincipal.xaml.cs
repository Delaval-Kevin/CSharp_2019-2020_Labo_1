using System;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Media;
using MyCartographyObjects;
using System.Windows.Controls;
using System.Collections.Generic;
using Microsoft.Maps.MapControl.WPF;
using System.Text.RegularExpressions;

namespace PersonelMap_Manager
{
    public partial class PagePrincipal : UserControl, IPageChange
    {
        #region EVENEMENTS
        public event Action<int, MyPersonalMapData> PageChange;
        #endregion


        #region VARIABLES
        private MyPersonalMapData _user;
        private List<Coordonnees> _listeTmp;
        private MapPolyline _polylineTpm;
        private MapPolygon _polygonTpm;
        #endregion


        #region PROPRIETES
        public MyPersonalMapData User
        {
            get { return _user; }
            set { _user = value; }
        }

        public List<Coordonnees> ListeTmp
        {
            get { return _listeTmp; }
            set { _listeTmp = value; }
        }

        public MapPolyline PolylineTpm
        {
            get { return _polylineTpm; }
            set { _polylineTpm = value; }
        }

        public MapPolygon PolygonTpm
        {
            get { return _polygonTpm; }
            set { _polygonTpm = value; }
        }
        #endregion


        #region CONSTRUCTEURS
        public PagePrincipal(MyPersonalMapData user)
        {
            InitializeComponent();
           
            User = user;
            ListeTmp = null;
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
            Option option = new Option(User.Path);
            option.ColorChange += Option_colorChange;
            option.Show(); //n'empêche pas la page principale de fonctionner
        }


        //Fonction pour quitter la page principale et revenir à la page login
        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            PageChange?.Invoke(1, null);
        }


        //Fonction pour charger un autre profil
        private void Open_Button(object sender, RoutedEventArgs e)
        {
            ChangeUserWindow changeProf = new ChangeUserWindow(_user);
            changeProf.ShowDialog(); //bloque la page 
            ReloadList();
            map.Children.Clear();
            AffichePin();
            StatBar.Text = "User " + User.Nom + " " + User.Prenom + " is loaded"; //Affichage dans la StatusBar
        }


        //Fonction pour sauvegarder le profil
        private void Save_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                User.Save();
                StatBar.Text = "User " + User.Nom + " " + User.Prenom + " is saved"; //Affichage dans la StatusBar
            }
            catch(Exception)//"An error occurred while saving the profile"
            {
                ErrorWindow error = new ErrorWindow("The profile could not be loaded");
                error.ShowDialog(); //bloque la page 
            }
        }


        //Fonction pour importer les POI d'un ficher .CSV
        private void POI_Import_Button(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "csv files (*.csv)|*.csv"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                    {
                        string line = sr.ReadLine();
                        string[] list = Regex.Split(line, @";+");

                        Pushpin pin = new Pushpin();
                        Location pinLocation = new Location();

                        POI TmpPoi = new POI(Convert.ToDouble(list[0]) , Convert.ToDouble(list[1]), list[2]);

                        pin.Tag = TmpPoi;
                        pinLocation.Latitude = TmpPoi.Latitude;
                        pinLocation.Longitude = TmpPoi.Longitude;

                        pin.Location = pinLocation;

                        //Ajoute le pin a la carte
                        map.Children.Add(pin);

                        //Ajoute à la liste
                        User.Liste.Add(TmpPoi);

                        StatBar.Text = "POI imported"; //Affichage dans la StatusBar
                    }
                }
                catch (Exception)
                {
                    ErrorWindow error = new ErrorWindow("The file could not be read");
                    error.ShowDialog(); //bloque la page
                }
            }
        }


        //Fonction pour exporter les POI d'un ficher .CSV
        private void POI_Export_Button(object sender, RoutedEventArgs e)
        {
            if(ListeCoo.SelectedItem is POI poi)
            {
                using (StreamWriter sw = new StreamWriter(User.Path + poi.Description + ".csv"))
                {
                    sw.WriteLine(poi.Latitude.ToString() + ";" + poi.Longitude.ToString() + ";" + poi.Description);
                }
                StatBar.Text = "POI exported"; //Affichage dans la StatusBar
            }
            else
            {
                ErrorWindow error = new ErrorWindow("The file could not be export");
                error.ShowDialog(); //bloque la page
            }
        }


        //Fonction pour importer les Trajets d'un ficher .CSV
        private void Polyline_Import_Button(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "csv files (*.csv)|*.csv"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                    {
                        string line = "";
                       
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] list = Regex.Split(line, @";+");
                            //C'est que c'est un POI donc ajout du POI
                            if(list[2] != String.Empty)
                            {
                                Pushpin pin = new Pushpin();
                                Location pinLocation = new Location();

                                POI TmpPoi = new POI(Convert.ToDouble(list[0]), Convert.ToDouble(list[1]), list[2]);

                                pin.Tag = TmpPoi;
                                pinLocation.Latitude = TmpPoi.Latitude;
                                pinLocation.Longitude = TmpPoi.Longitude;

                                pin.Location = pinLocation;

                                //Ajoute le pin a la carte
                                map.Children.Add(pin);

                                //Ajoute à la liste
                                User.Liste.Add(TmpPoi);
                            }

                            if (ListeTmp == null)
                            {
                                ListeTmp = new List<Coordonnees>();
                                PolylineTpm = new MapPolyline
                                {
                                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue),
                                    StrokeThickness = 3,
                                    Opacity = 1,
                                    Locations = new LocationCollection()
                                };
                            }
                            ListeTmp.Add(new Coordonnees(Convert.ToDouble(list[0]), Convert.ToDouble(list[1])));

                            PolylineTpm.Locations.Add(new Location (Convert.ToDouble(list[0]), Convert.ToDouble(list[1])));
                        }

                        Polyline TmpPolyline = new Polyline
                        {
                            Coordonnees = ListeTmp
                        };
                        TmpPolyline.Description = openFileDialog.SafeFileName;

                        //Ajoute à la liste
                        User.Liste.Add(TmpPolyline);
                        map.Children.Add(PolylineTpm);

                        PolylineTpm = null;
                        ListeTmp = null;
                    }
                    StatBar.Text = "Path imported"; //Affichage dans la StatusBar                    
                }
                catch (Exception)
                {
                    ErrorWindow error = new ErrorWindow("The file could not be read");
                    error.ShowDialog(); //bloque la page
                }
            }
        }


        //Fonction pour exporter les Trajets d'un ficher .CSV
        private void Polyline_Export_Button(object sender, RoutedEventArgs e)
        {
            if (ListeCoo.SelectedItem is Polyline poly)
            {
                using (StreamWriter sw = new StreamWriter(User.Path + poly.Description + ".csv"))
                {
                    foreach(Coordonnees coo in poly.Coordonnees)
                    {
                        sw.WriteLine(coo.Latitude.ToString() + ";" + coo.Longitude.ToString() + ";");
                    }
                }

                StatBar.Text = "Path exported"; //Affichage dans la StatusBar
            }
            else
            {
                ErrorWindow error = new ErrorWindow("The file could not be export");
                error.ShowDialog(); //bloque la page
            }
        }


        //Fonction pour importer les Polygons d'un ficher .CSV
        private void Polygon_Import_Button(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "csv files (*.csv)|*.csv"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                    {
                        string line = "";

                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] list = Regex.Split(line, @";+");
                            //C'est que c'est un POI donc ajout du POI
                            if (list[2] != String.Empty)
                            {
                                Pushpin pin = new Pushpin();
                                Location pinLocation = new Location();

                                POI TmpPoi = new POI(Convert.ToDouble(list[0]), Convert.ToDouble(list[1]), list[2]);

                                pin.Tag = TmpPoi;
                                pinLocation.Latitude = TmpPoi.Latitude;
                                pinLocation.Longitude = TmpPoi.Longitude;

                                pin.Location = pinLocation;

                                //Ajoute le pin a la carte
                                map.Children.Add(pin);

                                //Ajoute à la liste
                                User.Liste.Add(TmpPoi);
                            }

                            if (ListeTmp == null)
                            {
                                ListeTmp = new List<Coordonnees>();
                                PolygonTpm = new MapPolygon
                                {
                                    Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightBlue),
                                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue),
                                    StrokeThickness = 3,
                                    Opacity = 0.6,
                                    Locations = new LocationCollection()
                                };
                            }
                            ListeTmp.Add(new Coordonnees(Convert.ToDouble(list[0]), Convert.ToDouble(list[1])));

                            PolygonTpm.Locations.Add(new Location(Convert.ToDouble(list[0]), Convert.ToDouble(list[1])));
                        }

                        Polygon TmpPolygon = new Polygon
                        {
                            Coordonnees = ListeTmp
                        };
                        TmpPolygon.Description = openFileDialog.SafeFileName;

                        //Ajoute à la liste
                        User.Liste.Add(TmpPolygon);
                        map.Children.Add(PolygonTpm);

                        PolygonTpm = null;
                        ListeTmp = null;
                    }
                    StatBar.Text = "Path imported"; //Affichage dans la StatusBar                    
                }
                catch (Exception)
                {
                    ErrorWindow error = new ErrorWindow("The file could not be read");
                    error.ShowDialog(); //bloque la page
                }
            }
        }


        //Fonction pour exporter les Polygons d'un ficher .CSV
        private void Polygon_Export_Button(object sender, RoutedEventArgs e)
        {
            if (ListeCoo.SelectedItem is Polygon poly)
            {
                using (StreamWriter sw = new StreamWriter(User.Path + poly.Description + ".csv"))
                {
                    foreach (Coordonnees coo in poly.Coordonnees)
                    {
                        sw.WriteLine(coo.Latitude.ToString() + ";" + coo.Longitude.ToString() + ";");
                    }
                }

                StatBar.Text = "Polygon exported"; //Affichage dans la StatusBar
            }
            else
            {
                ErrorWindow error = new ErrorWindow("The file could not be export");
                error.ShowDialog(); //bloque la page
            }
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
            ReloadList();
        }


        //ce centrer sur l'objet selectionné
        private void ListeCoo_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            StatBar.Text = ListeCoo.SelectedItem.ToString(); //Affichage dans la StatusBar
            if (ListeCoo.SelectedItem is POI poi)
            {
                map.Center = new Location(poi.Latitude,poi.Longitude);
            }
            else if (ListeCoo.SelectedItem is Polyline polyl)
            {
                map.Center = new Location(polyl.Coordonnees[0].Latitude, polyl.Coordonnees[0].Longitude);
            }
            else if (ListeCoo.SelectedItem is Polygon polyg)
            {
                map.Center = new Location(polyg.Coordonnees[0].Latitude, polyg.Coordonnees[0].Longitude);
            }
            else
            {
                ErrorWindow error = new ErrorWindow("An error has occurred");
                error.ShowDialog(); //bloque la page
            }
            map.ZoomLevel = 6;
        }


        //efface la liste temporaire si on decide de modifier le choix de Polyline, Polygon ou POI
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListeTmp != null)
            {
                if(PolylineTpm != null)
                {
                    map.Children.Remove(PolylineTpm);
                    PolylineTpm = null;

                }
                else if(PolygonTpm != null)
                {
                    map.Children.Remove(PolygonTpm);
                    PolygonTpm = null;
                }
                ListeTmp = null;
            }
        }


        //Efface la liste temporaire si arrete l'ajout de Polyline, Polygon ou POI
        private void Add_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ListeTmp != null)
            {
                if (PolylineTpm != null)
                {
                    map.Children.Remove(PolylineTpm);
                    PolylineTpm = null;

                }
                else if (PolygonTpm != null)
                {
                    map.Children.Remove(PolygonTpm);
                    PolygonTpm = null;
                }
                ListeTmp = null;
            }
        }
        #endregion


        #region METHODES
        //affiche les objets sur la carte
        private void AffichePin()
        {
            for (int i = 0; i < User.Liste.Count(); i++)
            {
                if (User.Liste[i] is Coordonnees)
                {
                    Coordonnees coo = User.Liste[i] as Coordonnees;
                    Location pinLocation = new Location(coo.Latitude, coo.Longitude);

                    //Le pin a ajouter à la carte
                    Pushpin pin = new Pushpin
                    {
                        Location = pinLocation
                    };

                    //Ajoute le pin a la carte
                    map.Children.Add(pin);
                }
                else if (User.Liste[i] is Polyline)
                {
                    Polyline poly = User.Liste[i] as Polyline;
                    PolylineTpm = new MapPolyline
                    {
                        Stroke = new System.Windows.Media.SolidColorBrush(poly.Couleur),
                        StrokeThickness = poly.Epaisseur,
                        Opacity = 1,
                        Locations = new LocationCollection()
                    };

                    foreach (Coordonnees coo in poly.Coordonnees)
                    {
                        PolylineTpm.Locations.Add(new Location(coo.Latitude, coo.Longitude));
                    }

                    map.Children.Add(PolylineTpm);
                }
                else if (User.Liste[i] is Polygon)
                {
                    Polygon poly = User.Liste[i] as Polygon;
                    PolygonTpm = new MapPolygon
                    {
                        Stroke = new System.Windows.Media.SolidColorBrush(poly.CouleurContour),
                        Fill = new System.Windows.Media.SolidColorBrush(poly.CouleurRemplissage),
                        Opacity = poly.Opacite,
                        Locations = new LocationCollection()
                    };

                    foreach (Coordonnees coo in poly.Coordonnees)
                    {
                        PolygonTpm.Locations.Add(new Location(coo.Latitude, coo.Longitude));
                    }

                    map.Children.Add(PolygonTpm);
                }
            }
        }


        //Permet de remettre la liste à jour
        private void ReloadList()
        {
            ListeCoo.ItemsSource = null;
            ListeCoo.ItemsSource = User.Liste;
        }


        //Ajouter un item
        private void AddItem(Location pinLocation)
        {
            if(POI_Box.IsSelected == true)
            {
                //La position du pin sur la carte
                Pushpin pin = new Pushpin();

                POI TmpPoi = new POI(pinLocation.Latitude, pinLocation.Longitude, "");

                OptPOI text = new OptPOI(TmpPoi);
                text.ShowDialog(); //bloque la page 

                pin.Tag = TmpPoi;
                pinLocation.Latitude = TmpPoi.Latitude;
                pinLocation.Longitude = TmpPoi.Longitude;

                pin.Location = pinLocation;

                //Ajoute le pin a la carte
                map.Children.Add(pin);

                //Ajoute à la liste
                User.Liste.Add(TmpPoi);
                StatBar.Text = "POI added"; //Affichage dans la StatusBar
            }

            if (Polyline_Box.IsSelected == true)
            {
                if(ListeTmp == null)
                {
                    ListeTmp = new List<Coordonnees>();
                    PolylineTpm = new MapPolyline
                    {
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue),
                        StrokeThickness = 3,
                        Opacity = 1,
                        Locations = new LocationCollection()
                    };
                    map.Children.Add(PolylineTpm);
                }

                ListeTmp.Add(new Coordonnees(pinLocation.Latitude, pinLocation.Longitude));

                PolylineTpm.Locations.Add(new Location(pinLocation.Latitude, pinLocation.Longitude));

                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    Polyline TmpPolyline = new Polyline
                    {
                        Coordonnees = ListeTmp
                    };
                    OptPolyline text = new OptPolyline(TmpPolyline);
                    text.ShowDialog(); //bloque la page 

                    //Ajoute à la liste
                    PolylineTpm.StrokeThickness = TmpPolyline.Epaisseur;
                    User.Liste.Add(TmpPolyline);
                    map.Children.Remove(PolylineTpm);
                    PolylineTpm.Stroke = new System.Windows.Media.SolidColorBrush(TmpPolyline.Couleur);
                    map.Children.Add(PolylineTpm);

                    PolylineTpm = null;
                    ListeTmp = null;
                }
                StatBar.Text = "Polyline added"; //Affichage dans la StatusBar
            }

            if (Polygone_Box.IsSelected == true)
            {
                if (ListeTmp == null)
                {
                    ListeTmp = new List<Coordonnees>();
                    PolygonTpm = new MapPolygon
                    {
                        Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightBlue),
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue),
                        StrokeThickness = 3,
                        Opacity = 0.6,
                        Locations = new LocationCollection()
                    };
                    map.Children.Add(PolygonTpm);
                }

                ListeTmp.Add(new Coordonnees(pinLocation.Latitude, pinLocation.Longitude));

                PolygonTpm.Locations.Add(new Location(pinLocation.Latitude, pinLocation.Longitude));

                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    Polygon TmpPolygon = new Polygon
                    {
                        Coordonnees = ListeTmp
                    };
                    OptPolygon text = new OptPolygon(TmpPolygon);
                    text.ShowDialog(); //bloque la page 

                    //Ajoute à la liste
                    User.Liste.Add(TmpPolygon);
                    map.Children.Remove(PolygonTpm);
                    PolygonTpm.Fill = new System.Windows.Media.SolidColorBrush(TmpPolygon.CouleurRemplissage);
                    PolygonTpm.Stroke = new System.Windows.Media.SolidColorBrush(TmpPolygon.CouleurContour);
                    PolygonTpm.Opacity = TmpPolygon.Opacite;
                    map.Children.Add(PolygonTpm);

                    PolygonTpm = null;
                    ListeTmp = null;
                }
                StatBar.Text = "Polygon added"; //Affichage dans la StatusBar
            }
        }


        //Ouvrir le menu correspondant à la selection
        private void SelectItem(Location pinLocation)
        {
            for (int i = 0; i < User.Liste.Count(); i++)
            {

                if (User.Liste[i] is IIsPointClose)
                {
                    IIsPointClose coo = User.Liste[i] as IIsPointClose;
                    if (coo.IsPointClose(pinLocation, 0.5))
                    {
                        if(coo is POI poi)
                        {
                            OptPOI text = new OptPOI(poi);
                            text.ShowDialog(); //bloque la page

                            Pushpin pin = map.Children[i] as Pushpin;
                            pin.Location = new Location(poi.Latitude, poi.Longitude);
                            StatBar.Text = "POI updated"; //Affichage dans la StatusBar
                        }
                        else if(coo is Polyline polyl)
                        {
                            OptPolyline text = new OptPolyline(polyl);
                            text.ShowDialog(); //bloque la page 
                            
                            MapPolyline tmpPolyl = map.Children[i] as MapPolyline;
                            tmpPolyl.Stroke = new System.Windows.Media.SolidColorBrush(polyl.Couleur);
                            tmpPolyl.StrokeThickness = polyl.Epaisseur;
                            StatBar.Text = "Polyline updated"; //Affichage dans la StatusBar
                        }
                        else if(coo is Polygon polyg)
                        {
                            OptPolygon text = new OptPolygon(polyg);
                            text.ShowDialog(); //bloque la page

                            MapPolygon tmpPolyg = map.Children[i] as MapPolygon;
                            tmpPolyg.Stroke = new System.Windows.Media.SolidColorBrush(polyg.CouleurContour);
                            tmpPolyg.Fill = new System.Windows.Media.SolidColorBrush(polyg.CouleurRemplissage);
                            tmpPolyg.Opacity = polyg.Opacite;
                            StatBar.Text = "Polygon updated"; //Affichage dans la StatusBar
                        }
                        else
                        {
                            ErrorWindow error = new ErrorWindow("An error has occurred");
                            error.ShowDialog(); //bloque la page 
                        }
                        ReloadList();
                    }
                }
            }
        }


        //Supprimer un item
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
                        StatBar.Text = "Item deleted"; //Affichage dans la StatusBar
                    }
                }
            }
        }


        //Fonction appelée par l'évenement de la fenetre option
        private void Option_colorChange(Color text, Color back, String path)
        {
            ListeCoo.Foreground = new SolidColorBrush(text);
            ListeCoo.Background = new SolidColorBrush(back);
            User.Path = path;
            StatBar.Text = "ListBox color changed"; //Affichage dans la StatusBar
        }
        #endregion
    }
}
