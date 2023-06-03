using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;
using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using GtechDesktop.WPF.UserControls;
using GtechDesktop.WPF.Windows.AdminWindows;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;

namespace GtechDesktop.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SqlConnection Connection = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=gtech;Integrated Security=True");
        public static User? LoggedUser;
        public static AdminMainWindow? adminMainWindow;
        private static MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public static void NavigateToHomeWindow()
        {  
            mainWindow.MainContent.Content = new Home();
        }
      
        public static void NavigateToUserDetailsWindow()
        {
            mainWindow.MainContent.Content = new UserDetails(LoggedUser);
        }
    }
}
