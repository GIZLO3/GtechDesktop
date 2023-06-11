using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using GtechDesktop.WPF.Services;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            ProductName.Content = Product.Name;
            Title = product.Name;

            Producer.Content = ProducerRepository.GetProducer(Product.Id).Name;
            Amount.Content = Product.Amount + " szt.";
            Price.Content = product.Price + "zł";

            if(Product.Properties != null)//wyświetlenie parametrów produktu
            {
                var keys = Product.Properties.Keys.ToList();

                foreach (var key in keys)
                {
                    var label = new Label();
                    label.Content = char.ToUpper(key[0]) + key.Substring(1) + ": " + Product.Properties[key];
                    label.FontSize = 16;
                    PropertiesStackPanel.Children.Add(label);
                }
            }
        }

        private void AddToCartButtonClick(object sender, RoutedEventArgs e)//dodanie produktu do koszyka z uzględnieniem ilości
        {
            if (AmountCounter.Text.All(char.IsDigit) && !string.IsNullOrEmpty(AmountCounter.Text))
            {
                var amount = int.Parse(AmountCounter.Text);
                CartService.AddToCart(Product, amount);
            }    
        }

        private void AmountNumberValidation(object sender, TextCompositionEventArgs e)//metoda zapobiegająca wpisaniu znaków do textboxa z ilością
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
