﻿using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using GtechDesktop.WPF.Windows;
using GtechDesktop.WPF.Windows.AdminWindows;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GtechDesktop.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for Navbar.xaml
    /// </summary>
    public partial class Navbar : UserControl
    {
        public Navbar()
        {
            InitializeComponent();
            UpdateLoggedUser();
            GetCategories();
        }

        private void GetCategories()//tworzenie menu z kategoriami i dodawanie ich do grida
        {
            var categories = CategoryRepository.GetAllCategories();
            for (int i = 0; i < categories.Count(); i++)
            {
                var columnDefinition = new ColumnDefinition();//tworzenie nowego columnDefinition
                columnDefinition.MaxWidth = 200;
                categoriesBar.ColumnDefinitions.Add(columnDefinition);
                var menu = new Menu();
                menu.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f8f9fa"));
                menu.SetValue(Grid.ColumnProperty, i);
                categoriesBar.Children.Add(menu);//dodanie menu do grida
                var categoryMenuItem = new MenuItem();
                categoryMenuItem.Header = categories[i].Name;
                categoryMenuItem.FontSize = 14;
                categoryMenuItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f8f9fa"));

                var subcategories = SubcategoryRepository.GetSubcategoriesByCategoryId(categories[i].Id);
                foreach (var subcategory in subcategories)//tworzenie MenuItem dla każdej podkategorii w aktualnej kategorii
                {
                    var subcategoryMenuItem = new MenuItem();
                    subcategoryMenuItem.Header = subcategory.Name;
                    subcategoryMenuItem.CommandParameter = subcategory;
                    subcategoryMenuItem.Click += NavigateToSubacetgory;//dodanie event handler kliknięcia
                    categoryMenuItem.Items.Add(subcategoryMenuItem);//dodanie do MenuItema z kategorią MenuItem z podkategorią
                }

                menu.Items.Add(categoryMenuItem);//dodanie do menu MenuItema z kategorią
            }
        }

        private void UpdateLoggedUser()
        {
            if (App.LoggedUser == null)//jeżeli użytkownik nie jest zalogowany
            {
                userButton.Content = "Zaloguj się";
            }
            else//jeżeli użytkownik jest zalogowany
            {
                userButton.Content = App.LoggedUser.Login;

                if (App.LoggedUser.IsAdmin)//jeżeli jest adminem utworzenie przyciku do przejścia do widoku admina
                { 
                    var button = new Button();
                    button.SetValue(Grid.RowProperty, 2);
                    button.Content = ">>> Przejdź do widoku admina <<<";
                    button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e63946"));
                    button.Foreground = new SolidColorBrush(Colors.White);
                    button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e63946"));
                    button.Click += OpenAdminPanel;
                    NavbarGrid.RowDefinitions.Add(new RowDefinition());
                    NavbarGrid.Children.Add(button);
                }
            }
        }

        private void OpenAdminPanel(object sender, RoutedEventArgs e)
        {
            App.adminMainWindow = new AdminMainWindow();
            App.adminMainWindow.Show();
        }

        private void NavigateToSubacetgory(object sender, RoutedEventArgs e)
        {
            var subcategory = ((MenuItem)sender).CommandParameter as Subcategory;
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.MainContent.Content = new SubactegoryList(subcategory);
        }   

        private void NavigateToHomeWindow(object sender, MouseButtonEventArgs e)
        {
            App.NavigateToHomeWindow();
        }

        private void UserButtonClick(object sender, RoutedEventArgs e)
        {
            if (App.LoggedUser == null)//jeżeli użytkownik nie jest zalogowany - otworzenie okna logowania
            {
                var loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            }
            else//jeżeli użytkownik jest zalogowany - zmiana contentu MainWindow na szczegóły konta
            {
                App.NavigateToUserDetailsWindow();
            }
        }

        private void CartButtonClick(object sender, RoutedEventArgs e)//otwarcie okna koszyka
        {
            var cartWindow = new CartWindow();
            cartWindow.ShowDialog();
        }
    }
}
