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
