using GtechDesktop.WPF.Repositories;
using GtechDesktop.WPF.Services;
using GtechDesktop.WPF.UserControls;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.IdentityModel.Logging;

namespace GtechDesktop.WPF.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OpenRegisterWindow(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTxt.Text) ||  string.IsNullOrWhiteSpace(PasswordTxt.Password))
            {
                MessageBox.Show("Uzupełnij wszytkie pola!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var storedUser = UserRepository.GetUser(LoginTxt.Text);
                if (storedUser.Login != null && PasswordService.VerifyPassword(PasswordTxt.Password, storedUser.Password, storedUser.Salt))
                {
                    App.LoggedUser = storedUser;
                    JsonService.WriteFile(App.LoggedUser, App.GtechLoggedUserJsonFilePath);
                    App.NavigateToHomeWindow();
                    Close();
                }
                else
                    MessageBox.Show("Login lub hasło jest niepoprawne!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
