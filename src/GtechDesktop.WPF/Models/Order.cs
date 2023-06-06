using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtechDesktop.WPF.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal OrderTotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public Dictionary<int, CartProduct>? Products { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
