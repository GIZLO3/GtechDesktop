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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private decimal OrderTotalPrice = 0;
        public OrderWindow()
        {
            InitializeComponent();

            var keys = App.Cart.Keys.ToArray();
            decimal productsTotalPrice = 0;
            foreach (var key in keys)
            {
                productsTotalPrice += App.Cart[key].TotalPrice;
            }
            OrderTotalPrice = productsTotalPrice + (decimal)8.99;

            ProductsTotalPriceLabel.Content = productsTotalPrice + "zł";
            DeliveryPriceLabel.Content = 8.99 + "zł";
            OrderTotalPriceLabel.Content = OrderTotalPrice + "zł";
        }

        private bool Validete()
        {
            if (App.LoggedUser == null)
                return false;

            var emptyRegex = new Regex(@"^.{0}$");
            if(emptyRegex.IsMatch(NameTxt.Text) || emptyRegex.IsMatch(SurnameTxt.Text) || emptyRegex.IsMatch(AddressTxt.Text) || emptyRegex.IsMatch(PostalCodeTxt.Text) || emptyRegex.IsMatch(CityTxt.Text) || emptyRegex.IsMatch(PhoneNumberTxt.Text))
                return false;

            var postalCodeRegex = new Regex(@"^\d{2}-\d{3}$");
            if (!postalCodeRegex.IsMatch(PostalCodeTxt.Text))
                return false;

            var phoneRegex = new Regex(@"^^\d{9}$$");
            if (!phoneRegex.IsMatch(PhoneNumberTxt.Text))
                return false;

            var keys = App.Cart.Keys.ToArray(); 
            foreach(var key in keys) //sprawdzanie czy w sklepie jest wystarczająca ilość produktów 
            {
                var product = ProductRepository.GetProduct(key);
                if(product.Amount < App.Cart[key].Amount)
                {
                    MessageBox.Show("W magazynie nie ma wystarczającej ilości produktu : " + product.Name, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }       
            }

            return true;
        }

        private void MakeOrderButtonClick(object sender, RoutedEventArgs e)
        {
            if (Validete())
            {
                var order = new Order();
                order.SubmissionDate = DateTime.Now;
                order.DeliveryDate = DateTime.Now.AddDays(3);
                order.OrderTotalPrice = OrderTotalPrice;
                order.Status = OrderStatus.Złożone;
                order.Products = App.Cart;
                order.UserId = App.LoggedUser.Id;
                order.Name = NameTxt.Text;
                order.Surname = SurnameTxt.Text;
                order.Address = AddressTxt.Text;
                order.PostalCode = PostalCodeTxt.Text;
                order.City = CityTxt.Text;
                order.PhoneNumber = PhoneNumberTxt.Text;
                OrderRepository.InsertOrder(order);
                CartService.ClearCart();

                var keys = order.Products.Keys.ToArray();
                foreach( var key in keys)
                {
                    var product = ProductRepository.GetProduct(key);
                    product.Amount -= order.Products[key].Amount;
                    ProductRepository.UpdateProduct(product);
                }
                Close();
            }
            else
                MessageBox.Show("Podaj prawidłowe dane!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
