﻿using System;
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

namespace PersonelMap_Manager
{
    public partial class Option : Window
    {
        #region EVENEMENTS
        public event Action<Color, Color> colorChange;
        #endregion

        #region CONSTRUCTEURS
        public Option()
        {
            InitializeComponent();
        }
        #endregion

        #region BOUTONS
        private void OK_Button(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Apply_Button(object sender, RoutedEventArgs e)
        {
            colorChange?.Invoke(TextColor.Color, BackColor.Color);
        }

        private void Cancel_Button(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
