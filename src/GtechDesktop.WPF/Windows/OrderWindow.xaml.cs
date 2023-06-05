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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public OrderWindow()
        {
            InitializeComponent();

            var keys = App.Cart.Keys.ToArray();
            decimal productsTotalPrice = 0;
            foreach (var key in keys)
            {
                productsTotalPrice += App.Cart[key].TotalPrice;
            }
            var orderTotalPrice = productsTotalPrice + (decimal)8.99;

            ProductsTotalPrice.Content = productsTotalPrice + "zł";
            DeliveryPrice.Content = 8.99 + "zł";
            OrderTotalPrice.Content = orderTotalPrice + "zł";
        }
    }
}
