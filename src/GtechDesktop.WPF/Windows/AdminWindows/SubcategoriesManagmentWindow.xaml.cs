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
    /// Interaction logic for SubcategoriesManagmentWindow.xaml
    /// </summary>
    public partial class SubcategoriesManagmentWindow : Window
    {
        public SubcategoriesManagmentWindow()
        {
            InitializeComponent();
            SubcategoriesListView.ItemsSource = SubcategoryRepository.GetSubcategories();
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            var subcategory = ((Button)sender).CommandParameter as Subcategory;
            var addSubcategoryWindow = new AddSubcategoryWindow(subcategory);
            addSubcategoryWindow.ShowDialog();
            SubcategoriesListView.ItemsSource = SubcategoryRepository.GetSubcategories();
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var subcategory = ((Button)sender).CommandParameter as Subcategory;
            if (MessageBox.Show("Czy na pewno chcesz usunąć użytkownika " + subcategory.Name, "G-tech", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                SubcategoryRepository.DeleteSubcategory(subcategory);
                SubcategoriesListView.ItemsSource = SubcategoryRepository.GetSubcategories();
            }
        }
    }
}
