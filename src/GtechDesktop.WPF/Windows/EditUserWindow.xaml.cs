using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using GtechDesktop.WPF.Services;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;

namespace GtechDesktop.WPF.Windows
{
    /// <summary>
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        private User User;
        private bool IsAdmin = false;
        public EditUser(User user)
        {
            InitializeComponent();

            User = user;
            UsernameTxt.Text = user.Username;
            EmailTxt.Text = user.Email;
        }

        public EditUser(User user, bool isAdmin)//konstruktor z edycją z poziomu admina (brak konieczności podawania hasła)
        {
            InitializeComponent();
            IsAdmin = isAdmin;

            PasswordTxt.Password = "-------------------";
            PasswordTxt.IsEnabled = false;
            User = user;
            UsernameTxt.Text = user.Username;
            EmailTxt.Text = user.Email;
        }

        private void EditAccountButtonClick(object sender, RoutedEventArgs e)
        {
            var success = true;

            if (string.IsNullOrWhiteSpace(UsernameTxt.Text) || string.IsNullOrWhiteSpace(EmailTxt.Text) || string.IsNullOrWhiteSpace(PasswordTxt.Password))
            {
                success = false;
                MessageBox.Show("Uzupełnij wymagane pola! \n *pola są wymagane", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (!UsernameTxt.Text.All(char.IsLetterOrDigit) || UsernameTxt.Text.Length < 3 || UsernameTxt.Text.Length > 50)
                {
                    success = false;
                    MessageBox.Show("Nazwa użytkownika musi mieć od 3 do 50 znaków i może zawierać tylko litery i liczby!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (!new EmailAddressAttribute().IsValid(EmailTxt.Text))
                {
                    success = false;
                    MessageBox.Show("Adres email jest niepoprawny!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if(!string.IsNullOrWhiteSpace(NewPasswordTxt.Password) && !string.IsNullOrWhiteSpace(NewPassword2Txt.Password))
                {
                    if (NewPasswordTxt.Password.Length < 8)
                    {
                        success = false;
                        MessageBox.Show("Hasło musi zawierać przynajmniej 8 znaków!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    if (NewPassword2Txt.Password != NewPasswordTxt.Password)
                    {
                        success = false;
                        MessageBox.Show("Nowe hasła się nie zgadzają!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                if(!PasswordService.VerifyPassword(PasswordTxt.Password, User.Password, User.Salt) && !IsAdmin)
                {
                    success = false;
                    MessageBox.Show("Aktulne hasło jest niepoprawne!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            if (success)//edycja uzytkownika w bazie
            {
                User.Username = UsernameTxt.Text;
                if (!string.IsNullOrWhiteSpace(NewPasswordTxt.Password) && !string.IsNullOrWhiteSpace(NewPassword2Txt.Password))
                { 
                    User.Password = PasswordService.HashPasword(NewPasswordTxt.Password, out var salt);
                    User.Salt = salt;
                }
                User.Email = EmailTxt.Text;
                UserRepository.UpdateUser(User);
                if(!IsAdmin)
                {
                    App.LoggedUser = User;
                    App.NavigateToUserDetailsWindow(); 
                }

                Close();
            }
        }
    }
}
