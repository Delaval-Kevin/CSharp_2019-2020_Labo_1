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
    public partial class MainWindow : Window
    {
        IPageChange currentControl;

        #region CONSTRUCTEURS
        public MainWindow()
        {
            InitializeComponent();
            loadPageLogin();
        }
        #endregion

        #region METHODES
        //Fonction pour switcher entre les pages
        private void MainWindow_OnPageChange(int obj, MyPersonalMapData user)
        {
            switch (obj)
            {
                case 1: loadPageLogin(); break; // si on vient de la page "Principal" on va vers la "page Login"
                case 2: loadPagePrincipal(user); break; // si on vient de la "Login" on va vers la page "Principal"
            }
        }

        //Fonction de chargement pour la page "Login"
        public void loadPageLogin()
        {
            currentControl = new PageLogin();
            this.MonControle.Content = currentControl; //Affiche la nouvelle page 'Login'

            currentControl.pageChange += MainWindow_OnPageChange; //On s'abonne à l'évènement 'pageChange'
        }

        //Fonction de chargement pour la page "Principal"
        public void loadPagePrincipal(MyPersonalMapData user)
        {
            currentControl = new PagePrincipal(user);
            this.MonControle.Content = currentControl; //Affiche la nouvelle page 'Principal'

            currentControl.pageChange += MainWindow_OnPageChange; //On s'abonne à l'évènement 'pageChange'
        }
        #endregion
    }
}
