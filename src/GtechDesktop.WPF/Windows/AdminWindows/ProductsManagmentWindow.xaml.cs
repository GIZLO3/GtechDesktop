using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using System.Windows;
using System.Windows.Controls;

namespace GtechDesktop.WPF.Windows.AdminWindows
{
    /// <summary>
    /// Interaction logic for ProductsManagmentWindow.xaml
    /// </summary>
    public partial class ProductsManagmentWindow : Window
    {
        public ProductsManagmentWindow()
        {
            InitializeComponent();
            ProductsListView.ItemsSource = ProductRepository.GetProducts();
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)//brak edycji
        {
            var product = ((Button)sender).CommandParameter as Product;
            ProductsListView.ItemsSource = ProductRepository.GetProducts();
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)//usuwanie produktu
        {
            var product = ((Button)sender).CommandParameter as Product;
            if(product != null)
            {
                if (MessageBox.Show("Czy na pewno chcesz usunąć produkt " + product.Name, "G-tech", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    ProductRepository.DeleteProduct(product);
                    ProductsListView.ItemsSource = ProductRepository.GetProducts();
                }
            }
        }
    }
}
