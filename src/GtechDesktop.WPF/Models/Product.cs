using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GtechDesktop.WPF.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string? Image { get; set; }
        public Dictionary<string, string>? Properties { get; set; }
        public int ProducerId { get; set; }
        public int SubcategoryId { get; set; }
        public BitmapImage? BitmapImage { get; set; }
    }
}
