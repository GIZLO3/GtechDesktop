using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
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
using System.Windows.Shapes;

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

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            var user = ((Button)sender).CommandParameter as User;
            var editUserWindow = new EditUser(user, true);
            editUserWindow.ShowDialog();
            UsersListView.ItemsSource = UserRepository.GetUsers();
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
