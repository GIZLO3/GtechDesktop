using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using System.Windows;
using System.Windows.Controls;
using GtechDesktop.WPF.Windows;

namespace GtechDesktop.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();

            ProductsBox.ItemsSource = ProductRepository.GetRandomPercentOfProducts();
        }

        private void MoreDetailsButtonClicked(object sender, RoutedEventArgs e)//obsluga kliknięcia przyciku szczegółów produktu
        {
            var product = ((Button)sender).CommandParameter as Product;
            if (product != null)
            {
                var productDetailsWindow = new ProductDetailsWindow(product);
                productDetailsWindow.Show();
            }  
        }
    }
}
