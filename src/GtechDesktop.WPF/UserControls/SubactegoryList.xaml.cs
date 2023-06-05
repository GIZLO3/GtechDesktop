using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using GtechDesktop.WPF.Windows;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;


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
            var products = ProductRepository.GetProductFromSubactegory(subcategory.Id);                
            ProductBox.ItemsSource = products;
        }

        private void MoreDetailsButtonClicked(object sender, RoutedEventArgs e)
        {
            var product = ((Button)sender).CommandParameter as Product;
            var productDetailsWindow = new ProductDetailsWindow(product);
            productDetailsWindow.Show();
        }
    }
}
