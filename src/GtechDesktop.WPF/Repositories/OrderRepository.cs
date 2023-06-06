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
