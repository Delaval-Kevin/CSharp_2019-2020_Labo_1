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
    public partial class PageLogin : UserControl, IPageChange
    {
        public PageLogin()
        {
            InitializeComponent();
        }

        public event Action<int> pageChange;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pageChange?.Invoke(1);
        }
    }
}
