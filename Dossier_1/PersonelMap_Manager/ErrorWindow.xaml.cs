using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace PersonelMap_Manager
{
    public partial class ErrorWindow : Window
    {
        #region CONTRUCTEURS
        public ErrorWindow(string message)
        {
            InitializeComponent();
            Mes_Text.Text = message;
        }
        #endregion

        #region BOUTONS
        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
