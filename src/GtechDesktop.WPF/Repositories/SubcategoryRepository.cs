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
                    subcategory.ParametersPatten = JsonSerializer.Deserialize<List<string>>(dataReader.GetString(2));
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
                    subcategory.ParametersPatten = JsonSerializer.Deserialize<List<string>>(dataReader.GetString(2));
                    subcategory.CategoryId = dataReader.GetInt32(3);
                    subcategories.Add(subcategory);
                }
            }
            App.Connection.Close();

            return subcategories;
        }
    }
}
