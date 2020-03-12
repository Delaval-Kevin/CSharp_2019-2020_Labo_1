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

namespace PersonelMap_Manager
{
    public partial class MainWindow : Window
    {
        IPageChange currentControl;


        public MainWindow()
        {
            InitializeComponent();
            loadPageLogin();
        }

        private void MainWindow_OnPageChange(int obj)
        {
            switch (obj)
            {
                case 1: loadPagePrincipal(); break; // si on vient de la page 1 on va vers la page 2
                case 2: loadPageLogin(); break; // si on vient de la page 2 on va vers la page 1
            }
        }

        public void loadPagePrincipal()
        {
            currentControl = new PagePrincipal();
            this.MonControle.Content = currentControl;

            currentControl.pageChange += MainWindow_OnPageChange;
        }

        public void loadPageLogin()
        {
            currentControl = new PageLogin();
            this.MonControle.Content = currentControl;

            currentControl.pageChange += MainWindow_OnPageChange;
        }
    }
}
