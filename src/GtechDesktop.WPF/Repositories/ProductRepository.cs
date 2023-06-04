using GtechDesktop.WPF.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GtechDesktop.WPF.Repositories
{
    public class ProductRepository
    {
        public static Product GetProduct(int productId)
        {
            App.Connection.Open();
            var product = new Product();
            var getCommand = new SqlCommand("SELECT * FROM [gtech].[dbo].[product] WHERE ProductId=@ProductId", App.Connection);
            getCommand.Parameters.AddWithValue("@ProductId", productId);
            using (var dataReader = getCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    product.Id = dataReader.GetInt32(0);
                    product.Name = dataReader.GetString(1);
                    product.Price = dataReader.GetDecimal(2);
                    product.Amount = dataReader.GetInt32(3);
                    product.Image = Regex.Replace(dataReader.GetString(4), @"\s+", "");
                    product.Propeties = JsonSerializer.Deserialize<Dictionary<string, string>>(dataReader.GetString(5));
                    product.ProducerId = dataReader.GetInt32(6);
                    product.SubcategoryId = dataReader.GetInt32(7);
                }
            }
            App.Connection.Close();
            return product;
        }

        public static List<Product> GetProductFromSubactegory(int subcategoryId)
        {
            App.Connection.Open();
            var products = new List<Product>();
            var getCommand = new SqlCommand("SELECT * FROM [gtech].[dbo].[product] WHERE SubcategoryId=@SubcategoryId", App.Connection);
            getCommand.Parameters.AddWithValue("@SubcategoryId", subcategoryId);

            using (var dataReader = getCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var product = new Product();
                    product.Id = dataReader.GetInt32(0);
                    product.Name = dataReader.GetString(1);
                    product.Price = dataReader.GetDecimal(2);
                    product.Amount = dataReader.GetInt32(3);
                    product.Image = Regex.Replace(dataReader.GetString(4), @"\s+", "");
                    product.Propeties = JsonSerializer.Deserialize<Dictionary<string, string>>(dataReader.GetString(5));
                    product.ProducerId = dataReader.GetInt32(6);
                    product.SubcategoryId = dataReader.GetInt32(7);
                    products.Add(product);
                }
            }
            App.Connection.Close();

            return products;
        }
        public static List<Product> GetRandomPercentOfProducts(int percent)
        {
            App.Connection.Open();
            var products = new List<Product>();
            var getCommand = new SqlCommand("SELECT * FROM [gtech].[dbo].[product] WHERE ProductId IN (SELECT TOP @percent PERCENT ProductId FROM [gtech].[dbo].[product] ORDER BY newid())", App.Connection);
            getCommand.Parameters.AddWithValue("@percent", percent);

            using (var dataReader = getCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var product = new Product();
                    product.Id = dataReader.GetInt32(0);
                    product.Name = dataReader.GetString(1);
                    product.Price = dataReader.GetDecimal(2);
                    product.Amount = dataReader.GetInt32(3);
                    product.Image = Regex.Replace(dataReader.GetString(4), @"\s+", "");
                    product.Propeties = JsonSerializer.Deserialize<Dictionary<string, string>>(dataReader.GetString(5));
                    product.ProducerId = dataReader.GetInt32(6);
                    product.SubcategoryId = dataReader.GetInt32(7);
                    products.Add(product);
                }
            }
            App.Connection.Close();

            return products;
        }

        public static int InsertProduct(Product product)
        {
            App.Connection.Open();
            var insertCommand = new SqlCommand("INSERT INTO [gtech].[dbo].[product] VALUES(@Name, @Price, @Amount, @Image, @Properties, @ProducerId, @SubcategoryId)", App.Connection);
            insertCommand.Parameters.AddWithValue("@Name", product.Name);
            insertCommand.Parameters.AddWithValue("@Price", product.Price);
            insertCommand.Parameters.AddWithValue("@Amount", product.Amount);
            insertCommand.Parameters.AddWithValue("@Image", product.Image);
            insertCommand.Parameters.AddWithValue("@Properties", JsonSerializer.Serialize(product.Propeties));
            insertCommand.Parameters.AddWithValue("@ProducerId", product.ProducerId);
            insertCommand.Parameters.AddWithValue("@SubcategoryId", product.SubcategoryId);
            var id = insertCommand.ExecuteNonQuery();
            App.Connection.Close();
            return id;
        }
    }
}
