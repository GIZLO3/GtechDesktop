using GtechDesktop.WPF.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GtechDesktop.WPF.Repositories
{
    public class UserRepository
    {
        public static User GetUser(string login)
        {
            App.Connection.Open();
            var getCommand = new SqlCommand("SELECT * FROM [gtech].[dbo].[user] WHERE Login=@Login", App.Connection);
            getCommand.Parameters.AddWithValue("@Login", login);
            var user = new User();
            using ( var dataReader = getCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    user.Id = dataReader.GetInt32(0);
                    user.Login = Regex.Replace(dataReader.GetString(1), @"\s+", "");
                    user.Username = Regex.Replace(dataReader.GetString(2), @"\s+", "");
                    user.Password = Regex.Replace(dataReader.GetString(3), @"\s+", ""); 
                    user.Salt = (byte[])dataReader["Salt"];
                    user.Email = Regex.Replace(dataReader.GetString(5), @"\s+", "");
                    user.IsAdmin = dataReader.GetBoolean(6);
                }
            }  
            App.Connection.Close();

            return user;
        }

        public static User GetUser(int Id)
        {
            App.Connection.Open();
            var getCommand = new SqlCommand("SELECT * FROM [gtech].[dbo].[user] WHERE UserId=@UserId", App.Connection);
            getCommand.Parameters.AddWithValue("@UserId", Id);
            var user = new User();
            using (var dataReader = getCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    user.Id = dataReader.GetInt32(0);
                    user.Login = Regex.Replace(dataReader.GetString(1), @"\s+", "");
                    user.Username = Regex.Replace(dataReader.GetString(2), @"\s+", "");
                    user.Password = Regex.Replace(dataReader.GetString(3), @"\s+", "");
                    user.Salt = (byte[])dataReader["Salt"];
                    user.Email = Regex.Replace(dataReader.GetString(5), @"\s+", "");
                    user.IsAdmin = dataReader.GetBoolean(6);
                }
            }
            App.Connection.Close();

            return user;
        }

        public static List<User> GetUsers()
        {
            App.Connection.Open();
            var users = new List<User>();
            var getCommand = new SqlCommand("SELECT * FROM [gtech].[dbo].[user] WHERE IsAdmin=0", App.Connection);
            
            using (var dataReader = getCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var user = new User();
                    user.Id = dataReader.GetInt32(0);
                    user.Login = Regex.Replace(dataReader.GetString(1), @"\s+", "");
                    user.Username = Regex.Replace(dataReader.GetString(2), @"\s+", "");
                    user.Password = Regex.Replace(dataReader.GetString(3), @"\s+", "");
                    user.Salt = (byte[])dataReader["Salt"];
                    user.Email = Regex.Replace(dataReader.GetString(5), @"\s+", "");
                    user.IsAdmin = dataReader.GetBoolean(6);
                    users.Add(user);
                }
            }
            App.Connection.Close();

            return users;
        }

        public static int InsertUser(User user)
        {
            App.Connection.Open();
            var insertCommand = new SqlCommand("INSERT INTO [gtech].[dbo].[user] VALUES(@Login, @Username, @Password, @Salt, @Email, @IsAdmin)", App.Connection);
            insertCommand.Parameters.AddWithValue("@Login", user.Login);
            insertCommand.Parameters.AddWithValue("@Username", user.Username);
            insertCommand.Parameters.AddWithValue("@Password", user.Password);
            insertCommand.Parameters.AddWithValue("@Salt", user.Salt);
            insertCommand.Parameters.AddWithValue("@Email", user.Email);
            insertCommand.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
            var id = insertCommand.ExecuteNonQuery();
            App.Connection.Close();

            return id;
        }

        public static void UpdateUser(User user)
        {
            App.Connection.Open();
            var updateCommand = new SqlCommand("UPDATE [gtech].[dbo].[user] SET Username=@Username, Password=@Password, Email=@Email, IsAdmin=@IsAdmin WHERE UserId=@UserId", App.Connection);
            updateCommand.Parameters.AddWithValue("@Username", user.Username);
            updateCommand.Parameters.AddWithValue("@Password", user.Password);
            updateCommand.Parameters.AddWithValue("@Email", user.Email);
            updateCommand.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
            updateCommand.Parameters.AddWithValue("@UserId", user.Id);
            updateCommand.ExecuteNonQuery();
            App.Connection.Close();
        }

        public static void DeleteUser(User user)
        {
            App.Connection.Open();
            var deleteCommand = new SqlCommand("DELETE FROM [gtech].[dbo].[user] WHERE UserId=@UserId", App.Connection);
            deleteCommand.Parameters.AddWithValue("@UserId", user.Id);
            deleteCommand.ExecuteNonQuery();
            App.Connection.Close();
        }
    }
}
