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

        private void LogOutButtonClick(object sender, RoutedEventArgs e)
        {
            App.LoggedUser = null;
            if(App.adminMainWindow != null)
                App.adminMainWindow.Close();

            File.Delete(App.GtechLoggedUserJsonFilePath);
            App.NavigateToHomeWindow();
        }

        private void EditAccountButtonClick(object sender, RoutedEventArgs e)
        {
            var editUserWindow = new EditUser(App.LoggedUser);
            editUserWindow.ShowDialog();
        }
    }
}
