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
    /// Логика взаимодействия для Vhod.xaml
    /// </summary>
    public partial class Vhod : Page
    {
        private Client _currentClient = new Client();
        private Window windowAuto;

        public Vhod(Window window1)
        {
            InitializeComponent();
            DataContext = _currentClient;
            windowAuto = window1;
        }
        private void CheckPass_Click(object sender, RoutedEventArgs e)
        {//Показать скрыть пароль
            if (CheckbPass.IsChecked.Value)
            {
                PasscText.Text = Passc.Password; // скопируем в TextBox из PasswordBox
                PasscText.Visibility = Visibility.Visible; // TextBox - отобразить
                Passc.Visibility = Visibility.Hidden; // PasswordBox - скрыть
            }
            else
            {
                Passc.Password = PasscText.Text; // скопируем в PasswordBox из TextBox 
                PasscText.Visibility = Visibility.Hidden; // TextBox - скрыть
                Passc.Visibility = Visibility.Visible; // PasswordBox - отобразить
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var currentClient = TourBaseEntities.GetContext().Clients.ToList();
            StringBuilder errors = new StringBuilder();
            
            currentClient = currentClient.Where(p => p.Login.ToLower() == Loginc.Text.ToLower()).ToList();

            

            if (currentClient.Count >= 1)
            {
                _currentClient = currentClient.Single();
                PasscText.Text = Passc.Password;
                if (PasscText.Text == _currentClient.Password)
                {
                    MessageBox.Show("Вы успешно вошли!");
                    windowAuto.Hide();
                    new MainWindow(_currentClient.Name_Client, _currentClient.Surname_Client, _currentClient.Login, _currentClient.Admin_Right).ShowDialog();
                }
                else
                {
                    errors.AppendLine("Неверный пароль");
                }
            }
            else
            {
                errors.AppendLine("Аккаунт с таким логином не существует");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Registr(windowAuto));
        }
    }
}
