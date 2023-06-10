using GtechDesktop.WPF.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GtechDesktop.WPF.Repositories
{
    public class OrderRepository
    {   
        public static List<Order> GetUserOrders(int userId)
        {
            App.Connection.Open();
            var orders = new List<Order>();
            var getCommand = new SqlCommand("SELECT * FROM [gtech].[dbo].[order] WHERE UserId=@UserId", App.Connection);
            getCommand.Parameters.AddWithValue("@UserId", userId);

            using (var dataReader = getCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var order = new Order();
                    order.Id = dataReader.GetInt32(0);
                    order.SubmissionDate = dataReader.GetDateTime(1);
                    order.DeliveryDate = dataReader.GetDateTime(2);
                    order.OrderTotalPrice = dataReader.GetDecimal(3);
                    order.Status = Enum.Parse<OrderStatus>(dataReader.GetString(4));
                    order.Products = JsonSerializer.Deserialize<Dictionary<int, CartProduct>>(dataReader.GetString(5));
                    order.UserId = dataReader.GetInt32(6);
                    order.Name = dataReader.GetString(7);
                    order.Surname = dataReader.GetString(8);
                    order.Address = dataReader.GetString(9);
                    order.PostalCode = dataReader.GetString(10);
                    order.City = dataReader.GetString(11);
                    order.PhoneNumber = dataReader.GetString(12);
                    orders.Add(order);
                }
            }
            App.Connection.Close();
            return orders;
        }

        public static int InsertOrder(Order order)
        {
            App.Connection.Open();
            var insertCommand = new SqlCommand("INSERT INTO [gtech].[dbo].[order] VALUES(@SubmissionDate, @DeliveryDate, @OrderTotalPrice, @Status, @Products, @UserId, @Name, @Surname, @Address, @PostalCode, @City, @PhoneNumber)", App.Connection);
            insertCommand.Parameters.AddWithValue("@SubmissionDate", order.SubmissionDate);
            insertCommand.Parameters.AddWithValue("@DeliveryDate", order.DeliveryDate);
            insertCommand.Parameters.AddWithValue("@OrderTotalPrice", order.OrderTotalPrice);
            insertCommand.Parameters.AddWithValue("@Status", order.Status.ToString());
            insertCommand.Parameters.AddWithValue("@Products", JsonSerializer.Serialize(order.Products));
            insertCommand.Parameters.AddWithValue("@UserId", order.UserId);
            insertCommand.Parameters.AddWithValue("@Name", order.Name);
            insertCommand.Parameters.AddWithValue("@Surname", order.Surname);
            insertCommand.Parameters.AddWithValue("@Address", order.Address);
            insertCommand.Parameters.AddWithValue("@PostalCode", order.PostalCode); ;
            insertCommand.Parameters.AddWithValue("@City", order.City);
            insertCommand.Parameters.AddWithValue("@PhoneNumber", order.PhoneNumber);
            var id = insertCommand.ExecuteNonQuery();
            App.Connection.Close();
            return id;
        }
    }
}
