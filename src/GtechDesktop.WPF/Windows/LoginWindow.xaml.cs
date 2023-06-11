using GtechDesktop.WPF.Repositories;
using GtechDesktop.WPF.Services;
using System.Windows;

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

        private void OpenRegisterWindow(object sender, RoutedEventArgs e)//otwarcie okna rejestracji
        {
            var registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)//logowanie się
        {
            if (string.IsNullOrWhiteSpace(LoginTxt.Text) ||  string.IsNullOrWhiteSpace(PasswordTxt.Password))
            {
                MessageBox.Show("Uzupełnij wszytkie pola!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var storedUser = UserRepository.GetUser(LoginTxt.Text);//uzytkownik z tym samym loginem co podany
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
