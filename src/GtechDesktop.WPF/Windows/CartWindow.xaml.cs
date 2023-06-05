using GtechDesktop.WPF.Repositories;
using GtechDesktop.WPF.Services;
using GtechDesktop.WPF.UserControls;
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

namespace GtechDesktop.WPF.Windows
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public CartWindow()
        {
            InitializeComponent();

            if(App.LoggedUser == null)
            {
                Button.IsEnabled = false;
                Button.FontSize = 14;
                Button.Foreground = new SolidColorBrush(Colors.Black);
                Button.Content = "Zaloguj się, aby zrobić zamówienie";
            }

            if(App.Cart.Count == 0)
                Button.IsEnabled = false; 
            else
                GetCart();  
        }

        private void GetCart()
        {
            var keys = App.Cart.Keys;
            var productIndex = 0;
            CartGrid.Children.Clear();
            foreach (var key in keys)
            {
                var product = ProductRepository.GetProduct(key);
                if (product != null)
                {
                    var rowDef = new RowDefinition();
                    rowDef.Height = GridLength.Auto;
                    CartGrid.RowDefinitions.Add(rowDef);
                    var mainstackPanel = new StackPanel();
                    mainstackPanel.Orientation = Orientation.Horizontal;
                    mainstackPanel.Height = 80;
                    mainstackPanel.MaxWidth = 300;
                    var image = new Image();
                    image.Source = product.BitmapImage;
                    image.MaxWidth = 80;
                    mainstackPanel.Children.Add(image);

                    var stackPanel = new StackPanel();
                    stackPanel.VerticalAlignment = VerticalAlignment.Center;
                    var nameTxt = new TextBlock();
                    nameTxt.TextWrapping = TextWrapping.WrapWithOverflow;
                    nameTxt.Text = product.Name;
                    stackPanel.Children.Add(nameTxt);

                    var horizontalStackPanel = new StackPanel();
                    horizontalStackPanel.Orientation = Orientation.Horizontal;
                    var amountTxt = new TextBlock();
                    amountTxt.Text = "Ilość: " + App.Cart[key].Amount;
                    amountTxt.FontSize = 14;
                    horizontalStackPanel.Children.Add(amountTxt);
                    var totalPriceTxt = new TextBlock();
                    totalPriceTxt.Text = "  " + App.Cart[key].TotalPrice + "zł  ";
                    totalPriceTxt.FontSize = 14;
                    totalPriceTxt.FontWeight = FontWeights.Bold;
                    horizontalStackPanel.Children.Add(totalPriceTxt);

                    var deleteButton = new Button();
                    deleteButton.Content = "Usuń";
                    deleteButton.Foreground = new SolidColorBrush(Colors.White);
                    deleteButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e63946"));
                    deleteButton.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e63946"));
                    deleteButton.CommandParameter = product.Id;
                    deleteButton.Click += DeleteFromCartButtonClick;
                    horizontalStackPanel.Children.Add(deleteButton);

                    stackPanel.Children.Add(horizontalStackPanel);
                    mainstackPanel.Children.Add(stackPanel);
                    mainstackPanel.SetValue(Grid.RowProperty, productIndex++);
                    CartGrid.Children.Add(mainstackPanel);
                }
            }
        }


        private void MakeOrderButtonClick(object sender, RoutedEventArgs e)
        {
            var orderWindow = new OrderWindow();
            orderWindow.ShowDialog();
        }

        private void DeleteFromCartButtonClick(object sender, RoutedEventArgs e)
        {
            var productId = (int)((Button)sender).CommandParameter;
            CartService.DeleteFromCart(productId);

            GetCart();
        }
    }
}
