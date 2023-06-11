using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using GtechDesktop.WPF.Windows;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace GtechDesktop.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for UserDetails.xaml
    /// </summary>
    public partial class UserDetails : UserControl
    {
        public UserDetails(User user)
        {
            InitializeComponent();
            loginLabel.Content = user.Login;
            usernameLabel.Content = user.Username;
            emailLabel.Content = user.Email;
            OrdersListView.ItemsSource = OrderRepository.GetUserOrders(user.Id);
        }

        private void LogOutButtonClick(object sender, RoutedEventArgs e)//obsługa wylogowania się
        {
            App.LoggedUser = null;//ustawienie pola zalogowanego użytkowanika na null
            if(App.adminMainWindow != null)//jeżeli okno admina jest otwarte, należy je zamknąć
                App.adminMainWindow.Close();

            File.Delete(App.GtechLoggedUserJsonFilePath);//usunięcie pliku json z informacjami o użytkowniku
            App.NavigateToHomeWindow();//powrót do strony głównej
        }

        private void EditAccountButtonClick(object sender, RoutedEventArgs e)//otwarcie okna edycji użytkownika
        {
            if(App.LoggedUser != null)
            {
                var editUserWindow = new EditUser(App.LoggedUser);
                editUserWindow.ShowDialog();
            }    
        }
    }
}
