using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using GtechDesktop.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GtechDesktop.WPF.Windows
{
    /// <summary>
    /// Interaction logic for ProductDetailsWindow.xaml
    /// </summary>
    public partial class ProductDetailsWindow : Window
    {
        private Product Product;

        public ProductDetailsWindow(Product product)
        {
            InitializeComponent();

            Product = product;
            Image.Source = Product.BitmapImage;
            Name.Content = Product.Name;

            Producer.Content = ProducerRepository.GetProducer(Product.Id).Name;
            Amount.Content = Product.Amount + " szt.";
            Price.Content = product.Price + "zł";

            var keys = Product.Properties.Keys.ToList();

            foreach(var key in keys)
            {
                var label = new Label();
                label.Content = char.ToUpper(key[0]) + key.Substring(1) + ": " + Product.Properties[key];
                label.FontSize = 16;
                PropertiesStackPanel.Children.Add(label);
            }
        }

        private void AddToCartButtonClick(object sender, RoutedEventArgs e)
        {
            if (AmountCounter.Text.All(char.IsDigit) && !string.IsNullOrEmpty(AmountCounter.Text))
            {
                var amount = int.Parse(AmountCounter.Text);
                CartService.AddToCart(Product, amount);
            }    
        }

        private void AmountNumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
