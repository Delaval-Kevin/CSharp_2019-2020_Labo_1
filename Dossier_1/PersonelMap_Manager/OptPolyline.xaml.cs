﻿using System.Windows;
using MyCartographyObjects;


namespace PersonelMap_Manager
{
    public partial class OptPolyline : Window
    {
        #region VARIABLES
        private Polyline _polyline;
        #endregion

        #region PROPRIETES
        public Polyline Poly
        {
            get { return _polyline; }
            set { _polyline = value; }
        }
        #endregion

        #region CONSTRUCTEURS
        public OptPolyline(Polyline tmpPoly)
        {
            InitializeComponent();
            Poly = tmpPoly;
            opt.DataContext = Poly;
            ListeCoo.ItemsSource = Poly.Coordonnees;
        }
        #endregion

        #region BOUTONS
        private void OK_Button(object sender, RoutedEventArgs e)
        {
            Poly.Description = Desc.Text;
            Poly.Couleur = Col.Color;
            this.Close();
        }
        #endregion
    }
}
