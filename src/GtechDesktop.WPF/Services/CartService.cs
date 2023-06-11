using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace GtechDesktop.WPF.Services
{
    public class CartService
    {
        public static void AddToCart(Product product, int amount) //dodanie produktów do koszyka
        {
            var currentProductAmount = ProductRepository.GetCurrentProductAmount(product.Id); //pobranie aktualnej ilości produktu z bazy danych

            var append = false;
            var cartProduct = new CartProduct();
            if (App.Cart.ContainsKey(product.Id))//sprawdzenie czy w koszyku jest już przechwywany taki produkt
            {
                cartProduct = App.Cart[product.Id];//ustawienie cartProduct na dane znajdujące się już w koszyku
                amount += App.Cart[product.Id].Amount;
                append = true;//ustawienie flagi append na true
            }

            if (currentProductAmount >= amount)//jeżeli w sklepie jest dostępna żądana ilość produktu, dodanie produktu do koszyka
            {
                var totalPrice = amount * product.Price;

                cartProduct.Amount = amount;
                cartProduct.TotalPrice = totalPrice;

                if (!append)//jeżeli flaga append jest false, to do koszyka dodawany jest nowy produkt
                {
                    App.Cart.Add(product.Id, cartProduct);
                }

                JsonService.WriteFile(App.Cart, App.GtechCartFilePath);//zapisanie koszyka w pliku na komputerze
                MessageBox.Show("Dodano produkt do koszyka!", "Gtech", MessageBoxButton.OK);
            }
        }
        public static void DeleteFromCart(int productId)
        {
            App.Cart.Remove(productId);
            JsonService.WriteFile(App.Cart, App.GtechCartFilePath);
        }

        public static void ClearCart()
        {
            App.Cart = new Dictionary<int, CartProduct>();
            File.Delete(App.GtechCartFilePath);
        }
    }
}
