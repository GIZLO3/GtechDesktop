using GtechDesktop.WPF.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtechDesktop.WPF.Repositories
{
    public class ProducerRepository
    {
        public static List<Producer> GetAllProducers()
        {
            App.Connection.Open();
            var producers = new List<Producer>();
            var getCommand = new SqlCommand("SELECT * FROM [gtech].[dbo].[producer]", App.Connection);

            using (var dataReader = getCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var producer = new Producer();
                    producer.Id = dataReader.GetInt32(0);
                    producer.Name = dataReader.GetString(1);
                    producers.Add(producer);
                }
            }
            App.Connection.Close();
            return producers;
        }

        public static int InsertProducer(Producer producer)
        {
            App.Connection.Open();
            var insertCommand = new SqlCommand("INSERT INTO [gtech].[dbo].[producer] VALUES(@Name)", App.Connection);
            insertCommand.Parameters.AddWithValue("@Name", producer.Name);
            var id = insertCommand.ExecuteNonQuery();
            App.Connection.Close();
            return id;
        }
    }
}

