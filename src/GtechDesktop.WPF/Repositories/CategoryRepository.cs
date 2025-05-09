﻿using GtechDesktop.WPF.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace GtechDesktop.WPF.Repositories
{
    public class CategoryRepository
    {
        public static Category GetCategory(int Id)
        {
            App.Connection.Open();
            var getCommand = new SqlCommand("SELECT * FROM [gtech].[dbo].[category] WHERE CategoryId=@CategoryId", App.Connection);
            getCommand.Parameters.AddWithValue("@CategoryId", Id);
            var category = new Category();
            using (var dataReader = getCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    category.Id = dataReader.GetInt32(0);
                    category.Name = dataReader.GetString(1);
                }
            }
            App.Connection.Close();

            return category;
        }

        public static List<Category> GetAllCategories()
        {
            App.Connection.Open();
            var categories = new List<Category>();
            var getCommand = new SqlCommand("SELECT * FROM [gtech].[dbo].[category]", App.Connection);
            
            using (var dataReader = getCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var category = new Category();
                    category.Id = dataReader.GetInt32(0);
                    category.Name = dataReader.GetString(1);
                    categories.Add(category);
                }
            }
            App.Connection.Close();
            return categories;
        }
        public static int InsertCategory(Category category)
        {
            App.Connection.Open();
            var insertCommand = new SqlCommand("INSERT INTO [gtech].[dbo].[category] VALUES(@Name)", App.Connection);
            insertCommand.Parameters.AddWithValue("@Name", category.Name);
            var id = insertCommand.ExecuteNonQuery();
            App.Connection.Close();
            return id;
        }
    }
}
