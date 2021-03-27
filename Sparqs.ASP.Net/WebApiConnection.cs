using MySql.Data.MySqlClient;
using Sparqs.ASP.Net.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Sparqs.ASP.Net
{
    public class WebApiConnection
    {
        private static readonly string connectionString = ConfigurationSettings.AppSettings["myConnectionString"];

        private static readonly int commandTimeOut = 60;

        private MySqlConnection myConnection;

        public WebApiConnection()
        {
            myConnection = new MySqlConnection(connectionString);
        }

        internal IEnumerable<Restaurant> readRestaurants()
        {
            var result = new List<Restaurant>();
            var query = "SELECT * FROM restaurants";
            var command = createCommand(query);
            try
            {
                myConnection.Open();
                MySqlDataReader myReader = command.ExecuteReader();
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        var newRes = new Restaurant(myReader.GetString("id"), myReader.GetString("title"),
                            myReader.GetString("street"), myReader.GetString("cityCode"), myReader.GetString("city"));
                        result.Add(newRes);
                    }
                }
                if (myReader != null && !myReader.IsClosed)
                {
                    myReader.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (myConnection != null && ConnectionState.Open == myConnection.State)
                {
                    myConnection.Close();
                }
            }
            return result;
        }

        private MySqlCommand createCommand(string query)
        {
            var command = new MySqlCommand(query, myConnection);
            command.CommandType = CommandType.Text;
            command.Connection = myConnection;
            command.CommandTimeout = commandTimeOut;
            return command;
        }
    }
}
