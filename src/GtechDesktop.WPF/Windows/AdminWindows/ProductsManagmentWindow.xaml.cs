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
    /// Interaction logic for ProductsManagmentWindow.xaml
    /// </summary>
    public partial class ProductsManagmentWindow : Window
    {
        public ProductsManagmentWindow()
        {
            InitializeComponent();
            ProductsListView.ItemsSource = ProductRepository.GetProducts();
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            var product = ((Button)sender).CommandParameter as Product;
            ProductsListView.ItemsSource = ProductRepository.GetProducts();
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var product = ((Button)sender).CommandParameter as Product;
            if (MessageBox.Show("Czy na pewno chcesz usunąć produkt " + product.Name, "G-tech", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ProductRepository.DeleteProduct(product);
                ProductsListView.ItemsSource = ProductRepository.GetProducts();
            }
        }
    }
}
