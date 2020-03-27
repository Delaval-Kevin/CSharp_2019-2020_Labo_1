using System;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media;
using MyCartographyObjects;


namespace PersonelMap_Manager
{
    public partial class Option : Window
    {
        #region EVENEMENTS
        public event Action<Color, Color, String> ColorChange;
        #endregion

        #region CONSTRUCTEURS
        public Option(string pa)
        {
            InitializeComponent();
            path.Text = pa;
        }
        #endregion

        #region BOUTONS
        private void OK_Button(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Apply_Button(object sender, RoutedEventArgs e)
        {
            ColorChange?.Invoke(TextColor.Color, BackColor.Color, path.Text);
        }

        private void Cancel_Button(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
