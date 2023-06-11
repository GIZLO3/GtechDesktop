using System.Linq;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using GtechDesktop.WPF.Repositories;
using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Services;

namespace GtechDesktop.WPF.Windows
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButtonClick(object sender, RoutedEventArgs e)//rejestracja
        {
            var success = true;
            
            if(string.IsNullOrWhiteSpace(LoginTxt.Text) || string.IsNullOrWhiteSpace(EmailTxt.Text) || string.IsNullOrWhiteSpace(PasswordTxt.Password) || string.IsNullOrWhiteSpace(Password2Txt.Password))
            {
                success = false;
                MessageBox.Show("Uzupełnij wszytkie pola!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if(!LoginTxt.Text.All(char.IsLetterOrDigit) || LoginTxt.Text.Length < 3 || LoginTxt.Text.Length > 50)
                {
                    success = false;
                    MessageBox.Show("Login musi mieć od 3 do 50 znaków i może zawierać tylko litery i liczby!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if(PasswordTxt.Password.Length < 8)
                {
                    success = false;
                    MessageBox.Show("Hasło musi zawierać przynajmniej 8 znaków!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if(Password2Txt.Password != PasswordTxt.Password)
                {
                    success = false;
                    MessageBox.Show("Hasła się nie zgadzają!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if(!new EmailAddressAttribute().IsValid(EmailTxt.Text))
                {
                    success = false;
                    MessageBox.Show("Adres email jest niepoprawny!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                var storedUser = UserRepository.GetUser(LoginTxt.Text);//sprawdzanie czy podany login już istnieje
                if(storedUser.Login != null)
                {
                    success = false;
                    MessageBox.Show("Użytkownik z takim login już istnieje!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            if(success)//dodanie do bazy uzytkownika i powrót do okna logowania
            {
                var user = new User();
                user.Login = LoginTxt.Text;
                user.Username = LoginTxt.Text;
                user.Password = PasswordService.HashPasword(PasswordTxt.Password, out var salt);
                user.Salt = salt;
                user.Email = EmailTxt.Text;
                UserRepository.InsertUser(user);
                Close();
            }
        }
    }
}
