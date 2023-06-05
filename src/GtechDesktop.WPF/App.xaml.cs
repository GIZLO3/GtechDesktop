using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text.Json;
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
        public static string GtechLoggedUserJsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "gtechLoggedUser.json");
        
        public static Dictionary<int, CartProduct> Cart = new Dictionary<int, CartProduct>(); 
        public static string GtechCartFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "gtechCart.json");

        public static AdminMainWindow? adminMainWindow;
        private static MainWindow? mainWindow;

        private void App_Startup(object sender, StartupEventArgs e)
        {
            if (File.Exists(GtechLoggedUserJsonFilePath))
            {
                using StreamReader streamReader = new(GtechLoggedUserJsonFilePath);
                var json = streamReader.ReadToEnd();
                LoggedUser = JsonSerializer.Deserialize<User>(json);
            }

            if (File.Exists(GtechCartFilePath))
            {
                using StreamReader streamReader = new(GtechCartFilePath);
                var json = streamReader.ReadToEnd();
                Cart = JsonSerializer.Deserialize<Dictionary<int, CartProduct>>(json);
            }

            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
            mainWindow = (MainWindow)Application.Current.MainWindow;
        }

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
