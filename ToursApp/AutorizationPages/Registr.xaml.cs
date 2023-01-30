using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
        private Client _currentClient = new Client();
        AutorizationWindow window = new AutorizationWindow();

        public Registr()
        {
            InitializeComponent();
            DataContext = _currentClient;

        }

        private void CheckPass_Click(object sender, RoutedEventArgs e)
        {//Показать скрыть пароль


            if (CheckbPass.IsChecked.Value)
            {
                PasscText.Text = Passc.Password; // скопируем в TextBox из PasswordBox
                PasscText.Visibility = Visibility.Visible; // TextBox - отобразить
                Passc.Visibility = Visibility.Hidden; // PasswordBox - скрыть


                EnPasscText.Text = EnPassc.Password; // скопируем в TextBox из PasswordBox
                EnPasscText.Visibility = Visibility.Visible; // TextBox - отобразить
                EnPassc.Visibility = Visibility.Hidden; // PasswordBox - скрыть

            }
            else
            {
                Passc.Password = PasscText.Text; // скопируем в PasswordBox из TextBox 
                PasscText.Visibility = Visibility.Hidden; // TextBox - скрыть
                Passc.Visibility = Visibility.Visible; // PasswordBox - отобразить

                EnPassc.Password = EnPasscText.Text; // скопируем в PasswordBox из TextBox 
                EnPasscText.Visibility = Visibility.Hidden; // TextBox - скрыть
                EnPassc.Visibility = Visibility.Visible; // PasswordBox - отобразить
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {//Процесс проверки и регистрации

            StringBuilder errors = new StringBuilder();

            var currentClient = TourBaseEntities.GetContext().Clients.ToList();
            //Проверка логина
            currentClient = currentClient.Where(p => p.Login.ToLower().Contains(Loginc.Text.ToLower())).ToList();
            if (currentClient.Count >= 1)
            {
                errors.AppendLine("Аккаунт с таким логином уже существует");
            }
            else
            {//Проверка пароля
                if (CheckPass(Passc.Password))
                {//Проверка на структуру пароля
                    if (Passc.Password == EnPassc.Password)
                    {//Проверка на подтверждение пароля
                        if (Namec.Text != null && Surnamec.Text != null && Loginc.Text != null)
                        {
                            PasscText.Text = Passc.Password;

                            TourBaseEntities.GetContext().Clients.Add(_currentClient);

                            try
                            {
                                TourBaseEntities.GetContext().SaveChanges();
                                MessageBox.Show("Вы зарегестрированы!");
                                window.Close();
                                new MainWindow(_currentClient.Name_Client, _currentClient.Surname_Client, _currentClient.Login, _currentClient.Admin_Right).ShowDialog();
                                
                            }
                            catch (DbEntityValidationException ex)
                            {
                                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                                {
                                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                                    {
                                        MessageBox.Show("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                                    }
                                }
                            }
                        }
                        else
                        {
                            errors.AppendLine("Заполните все поля");
                        }
                    }
                    else
                    {
                        errors.AppendLine("Пароли не совпадают");
                    }
                }
                else
                {
                    errors.AppendLine("длина пароля должна быть не менее 6 символов, обязательное наличие цифр, знаков препинания, строчных и прописных букв");
                }
            }



            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

        }
        static bool CheckPass(string pass) => pass.Length >= 6
        && pass.Any(char.IsLetter)
        && pass.Any(char.IsDigit)
        && pass.Any(char.IsPunctuation)
        && pass.Any(char.IsLower)
        && pass.Any(char.IsUpper);
    }
}
