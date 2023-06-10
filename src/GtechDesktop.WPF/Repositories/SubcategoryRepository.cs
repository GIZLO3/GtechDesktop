using GtechDesktop.WPF.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GtechDesktop.WPF.Repositories
{
    public class SubcategoryRepository
    {
        public static List<Subcategory> GetSubcategories()
        {
            App.Connection.Open();
            var getCommand = new SqlCommand("SELECT * FROM [gtech].[dbo].[subcategory]", App.Connection);
            var subcategories = new List<Subcategory>();
            using (var dataReader = getCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var subcategory = new Subcategory();
                    subcategory.Id = dataReader.GetInt32(0);
                    subcategory.Name = Regex.Replace(dataReader.GetString(1), @"\s+", "");
                    subcategory.ParametersPattern = JsonSerializer.Deserialize<List<string>>(dataReader.GetString(2));
                    subcategory.CategoryId = dataReader.GetInt32(3);
                    subcategories.Add(subcategory);
                }
            }
            App.Connection.Close();

            return subcategories;
        }

        public static Subcategory GetSubcategory(int Id)
        {
            App.Connection.Open();
            var getCommand = new SqlCommand("SELECT * FROM [gtech].[dbo].[subcategory] WHERE SubcategoryId=@SubcategoryId", App.Connection);
            getCommand.Parameters.AddWithValue("@SubcategoryId", Id);
            var subcategory = new Subcategory();
            using (var dataReader = getCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    subcategory.Id = dataReader.GetInt32(0);
                    subcategory.Name = Regex.Replace(dataReader.GetString(1), @"\s+", "");
                    subcategory.ParametersPattern = JsonSerializer.Deserialize<List<string>>(dataReader.GetString(2));
                    subcategory.CategoryId = dataReader.GetInt32(3);
                }
            }
            App.Connection.Close();

            return subcategory;
        }

        public static List<Subcategory> GetSubcategoriesByCategoryId(int CategoryId)
        {
            App.Connection.Open();
            var getCommand = new SqlCommand("SELECT * FROM [gtech].[dbo].[subcategory] WHERE CategoryId=@CategoryId", App.Connection);
            getCommand.Parameters.AddWithValue("@CategoryId", CategoryId);
            var subcategories = new List<Subcategory>();
            using (var dataReader = getCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var subcategory = new Subcategory();
                    subcategory.Id = dataReader.GetInt32(0);
                    subcategory.Name = Regex.Replace(dataReader.GetString(1), @"\s+", "");
                    subcategory.ParametersPattern = JsonSerializer.Deserialize<List<string>>(dataReader.GetString(2));
                    subcategory.CategoryId = dataReader.GetInt32(3);
                    subcategories.Add(subcategory);
                }
            }
            App.Connection.Close();

            return subcategories;
        }

        public static int InsertSubcategory(Subcategory subcategory)
        {
            App.Connection.Open();
            var insertCommand = new SqlCommand("INSERT INTO [gtech].[dbo].[subcategory] VALUES(@Subcategory, @ParametersPattern, @CategoryId)", App.Connection);
            insertCommand.Parameters.AddWithValue("@Subcategory", subcategory.Name);
            insertCommand.Parameters.AddWithValue("@ParametersPattern", JsonSerializer.Serialize(subcategory.ParametersPattern));
            insertCommand.Parameters.AddWithValue("@CategoryId", subcategory.CategoryId);
            var id = insertCommand.ExecuteNonQuery();
            App.Connection.Close();
            return id;
        }

        public static void UpdateSubcategory(Subcategory subcategory)
        {
            App.Connection.Open();
            var updateCommand = new SqlCommand("UPDATE [gtech].[dbo].[subcategory] SET Subcategory=@Subcategory, ParametersPattern=@ParametersPattern, CategoryId=@CategoryId WHERE SubcategoryId=@SubcategoryId", App.Connection);
            updateCommand.Parameters.AddWithValue("@Subcategory", subcategory.Name);
            updateCommand.Parameters.AddWithValue("@ParametersPattern", JsonSerializer.Serialize(subcategory.ParametersPattern));
            updateCommand.Parameters.AddWithValue("@CategoryId", subcategory.CategoryId);
            updateCommand.Parameters.AddWithValue("@SubcategoryId", subcategory.Id);
            updateCommand.ExecuteNonQuery();
            App.Connection.Close();
        }

        public static void DeleteSubcategory(Subcategory subcategory)
        {
            App.Connection.Open();
            var deleteCommand = new SqlCommand("DELETE FROM [gtech].[dbo].[subcategory] WHERE SubcategoryId=@SubcategoryId", App.Connection);
            deleteCommand.Parameters.AddWithValue("@SubcategoryId", subcategory.Id);
            deleteCommand.ExecuteNonQuery();
            App.Connection.Close();
        }
    }
}
