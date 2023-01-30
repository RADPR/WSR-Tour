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
using ToursApp.AutorizationPages;

namespace ToursApp.AutorizationPages
{
    /// <summary>
    /// Логика взаимодействия для MenuAutorization.xaml
    /// </summary>
    public partial class MenuAutorization : Page
    {
        public MenuAutorization()
        {
            InitializeComponent();
        }

        private void Registr_Click(object sender, RoutedEventArgs e)
        {
            Manager2.MainFrame.Navigate(new Registr());
        }

        private void Vhod_Click(object sender, RoutedEventArgs e)
        {
            Manager2.MainFrame.Navigate(new Vhod());
        }
    }
}
