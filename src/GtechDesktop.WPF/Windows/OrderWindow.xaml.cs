using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using GtechDesktop.WPF.Services;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace GtechDesktop.WPF.Windows
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private readonly decimal DeliveryPrice = 8.99m; //cena dostawy jest stała
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
            OrderTotalPrice = productsTotalPrice + DeliveryPrice;

            ProductsTotalPriceLabel.Content = productsTotalPrice + "zł";
            DeliveryPriceLabel.Content = DeliveryPrice + "zł";
            OrderTotalPriceLabel.Content = OrderTotalPrice + "zł";
        }

        private bool Validete()//validacja danych
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

        private void MakeOrderButtonClick(object sender, RoutedEventArgs e)//składanie zamówienia
        {
            if (Validete() && App.LoggedUser != null)
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
                foreach( var key in keys)//zmiana ilości produktu w bazie 
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
