using System;
using System.Windows;
using MyCartographyObjects;

namespace PersonelMap_Manager
{

    public partial class OptPolygon : Window
    {
        #region VARIABLES
        private Polygon _polygon;
        #endregion

        #region PROPRIETES
        public Polygon Poly
        {
            get { return _polygon; }
            set { _polygon = value; }
        }
        #endregion

        #region CONSTRUCTEURS
        public OptPolygon(Polygon tmpPoly)
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
            this.Close();
        }
        #endregion
    }
}
