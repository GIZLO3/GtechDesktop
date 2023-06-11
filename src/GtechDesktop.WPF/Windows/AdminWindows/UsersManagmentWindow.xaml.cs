using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using System.Windows;
using System.Windows.Controls;

namespace GtechDesktop.WPF.Windows.AdminWindows
{
    /// <summary>
    /// Interaction logic for UsersManagmentWindow.xaml
    /// </summary>
    public partial class UsersManagmentWindow : Window
    {
        public UsersManagmentWindow()
        {
            InitializeComponent();

            UsersListView.ItemsSource = UserRepository.GetUsers(); 
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)//otwarcie okna do edycji użytkownika
        {
            var user = ((Button)sender).CommandParameter as User;
            if(user != null)
            {
                var editUserWindow = new EditUser(user, true);
                editUserWindow.ShowDialog();
                UsersListView.ItemsSource = UserRepository.GetUsers();
            }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)//usuwanie użytkownika
        {
            var user = ((Button)sender).CommandParameter as User;
            if(user != null)
            {
                if (MessageBox.Show("Czy na pewno chcesz usunąć użytkownika " + user.Login, "G-tech", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    UserRepository.DeleteUser(user);
                    UsersListView.ItemsSource = UserRepository.GetUsers();
                }
            }          
        }
    }
}
