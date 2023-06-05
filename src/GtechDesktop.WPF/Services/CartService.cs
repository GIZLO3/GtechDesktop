using GtechDesktop.WPF.Models;
using GtechDesktop.WPF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GtechDesktop.WPF.Services
{
    public class CartService
    {
        public static void AddToCart(Product product, int amount)
        {
            var currentProductAmount = ProductRepository.GetCurrentProductAmount(product.Id);

            var append = false;
            var cartProduct = new CartProduct();
            if (App.Cart.ContainsKey(product.Id))
            {
                cartProduct = App.Cart[product.Id];
                amount += App.Cart[product.Id].Amount;
                append = true;
            }

            if (currentProductAmount >= amount)
            {
                var totalPrice = amount * product.Price;

                cartProduct.Amount = amount;
                cartProduct.TotalPrice = totalPrice;

                if (!append)
                {
                    App.Cart.Add(product.Id, cartProduct);
                }

                JsonService.WriteFile(App.Cart, App.GtechCartFilePath);
                MessageBox.Show("Dodano produkt do koszyka!", "Gtech", MessageBoxButton.OK);
            }
        }
        public static void DeleteFromCart(int productId)
        {
            App.Cart.Remove(productId);
            JsonService.WriteFile(App.Cart, App.GtechCartFilePath);
        }
    }
}
