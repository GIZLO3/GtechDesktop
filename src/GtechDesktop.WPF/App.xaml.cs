using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using GtechDesktop.WPF.UserControls;
using GtechDesktop.WPF.Windows.AdminWindows;
using Microsoft.Data.SqlClient;

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
        public static MainWindow? mainWindow;

        private void App_Startup(object sender, StartupEventArgs e)//metoda wywoływana podczas włączania aplikacji, ustawia się to w App.xaml (Startup="App_Startup")
        {
            if (File.Exists(GtechLoggedUserJsonFilePath))//pobranie danych z pliku o zalogowanym używkowniku
            {
                using StreamReader streamReader = new(GtechLoggedUserJsonFilePath);
                var json = streamReader.ReadToEnd();
                var localUser = JsonSerializer.Deserialize<User>(json);
                if (localUser != null)
                {
                    var storedUser = UserRepository.GetUser(localUser.Id);//pobranie z bazy uzytkownika 
                    if(storedUser != null)
                    {
                        if (localUser.Password == storedUser.Password)//sprawdzenie czy hasła się zgadzają
                            LoggedUser = storedUser;
                        else
                            File.Delete(GtechLoggedUserJsonFilePath);
                    }
                }
                
            }

            if (File.Exists(GtechCartFilePath))//pobranie koszyka z pliku
            {
                using StreamReader streamReader = new(GtechCartFilePath);
                var json = streamReader.ReadToEnd();
                Cart = JsonSerializer.Deserialize<Dictionary<int, CartProduct>>(json);
            }

            //otworzenie MainWindow
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
            mainWindow = (MainWindow)Application.Current.MainWindow;
        }

        public static void NavigateToHomeWindow()//ustawienie contentu mainWindow na Home
        {  
            if(mainWindow != null)
            {
                mainWindow.MainContent.Content = new Home();
                mainWindow.Title = "G-tech Desktop";
            }       
        }
      
        public static void NavigateToUserDetailsWindow()//ustawienie contentu mainWindow na userDetails
        {
            if (mainWindow != null && LoggedUser != null)
            {
                mainWindow.MainContent.Content = new UserDetails(LoggedUser);
                mainWindow.Title = "Moje konto";
            }
        }
    }
}
