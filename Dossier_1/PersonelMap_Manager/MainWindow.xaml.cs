using System;
using System.Windows;
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
            LoadPageLogin();
        }
        #endregion

        #region METHODES
        //Fonction pour switcher entre les pages
        private void MainWindow_OnPageChange(int obj, MyPersonalMapData user)
        {
            switch (obj)
            {
                case 1: LoadPageLogin(); break; // si on vient de la page "Principal" on va vers la "page Login"
                case 2: LoadPagePrincipal(user); break; // si on vient de la "Login" on va vers la page "Principal"
            }
        }

        //Fonction de chargement pour la page "Login"
        public void LoadPageLogin()
        {
            currentControl = new PageLogin();
            this.MonControle.Content = currentControl; //Affiche la nouvelle page 'Login'

            currentControl.PageChange += MainWindow_OnPageChange; //On s'abonne à l'évènement 'pageChange'
        }

        //Fonction de chargement pour la page "Principal"
        public void LoadPagePrincipal(MyPersonalMapData user)
        {
            currentControl = new PagePrincipal(user);
            this.MonControle.Content = currentControl; //Affiche la nouvelle page 'Principal'

            currentControl.PageChange += MainWindow_OnPageChange; //On s'abonne à l'évènement 'pageChange'
        }
        #endregion
    }
}
