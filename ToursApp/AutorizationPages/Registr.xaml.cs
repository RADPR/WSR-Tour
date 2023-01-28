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

namespace ToursApp.AutorizationPages
{
    /// <summary>
    /// Логика взаимодействия для Registr.xaml
    /// </summary>
    public partial class Registr : Page
    {
        public Registr()
        {
            InitializeComponent();
        }

        private void CheckPass_Click(object sender, RoutedEventArgs e)
        {
            if (CheckPass.IsChecked == true)
            {
                Passc.PasswordChar = ;
            }
            else
            {
                Passc.PasswordChar = '*';
            }
        }
    }
}
