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
using MyCartographyObjects;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace PersonelMap_Manager
{
    public partial class OptPOI : Window
    {
        #region VARIABLES
        private POI _poi;
        #endregion

        #region PROPRIETES
        public POI Poi
        {
            get { return _poi; }
            set { _poi = value; }
        }
        #endregion

        #region CONSTRUCTEURS
        public OptPOI(POI poi)
        {
            InitializeComponent();
            Poi = poi;
            opt.DataContext = Poi;
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
