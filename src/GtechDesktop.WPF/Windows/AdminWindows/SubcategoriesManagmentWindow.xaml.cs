using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using System.Windows;
using System.Windows.Controls;

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

        private void EditButtonClick(object sender, RoutedEventArgs e)//otwarcie okna do edycji podkategorii
        {
            var subcategory = ((Button)sender).CommandParameter as Subcategory;
            if (subcategory != null ) 
            { 
                var addSubcategoryWindow = new AddSubcategoryWindow(subcategory);
                addSubcategoryWindow.ShowDialog();
                SubcategoriesListView.ItemsSource = SubcategoryRepository.GetSubcategories();
            }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)//usuwanie podkategroii
        {
            var subcategory = ((Button)sender).CommandParameter as Subcategory;
            if (subcategory != null )
            {
                if (MessageBox.Show("Czy na pewno chcesz usunąć użytkownika " + subcategory.Name, "G-tech", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    SubcategoryRepository.DeleteSubcategory(subcategory);
                    SubcategoriesListView.ItemsSource = SubcategoryRepository.GetSubcategories();
                }
            }
        }
    }
}
