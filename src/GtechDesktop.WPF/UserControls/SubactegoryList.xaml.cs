using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using GtechDesktop.WPF.Windows;
using System.Windows;
using System.Windows.Controls;

namespace GtechDesktop.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for SubactegoryList.xaml
    /// </summary>
    public partial class SubactegoryList : UserControl
    {
        public SubactegoryList(Subcategory subcategory)
        {
            InitializeComponent();

            SubacategoryName.Content = subcategory.Name;
            App.mainWindow.Title = subcategory.Name;
            var products = ProductRepository.GetProductFromSubactegory(subcategory.Id);//pobranie produktów z danej podkategorii z bazy danych             
            ProductBox.ItemsSource = products;
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
