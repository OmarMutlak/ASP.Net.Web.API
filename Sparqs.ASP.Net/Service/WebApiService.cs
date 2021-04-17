using MySql.Data.MySqlClient;
using Sparqs.ASP.Net.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Sparqs.ASP.Net
{
    public class WebApiService
    {
        private static readonly string connectionString = ConfigurationSettings.AppSettings["myConnectionString"];

        private static readonly int commandTimeOut = 60;

        private MySqlConnection myConnection;

        public WebApiService()
        {
            myConnection = new MySqlConnection(connectionString);
        }

        internal List<Restaurant> GetRestaurants()
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
                        var newRes = new Restaurant(Convert.ToInt32(myReader.GetString("id")), myReader.GetString("title"),
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

        internal Restaurant GetRestaurantById(int id)
        {
            var result = new Restaurant();
            var restaurant = GetRestaurants().Find(res => res.id.Equals(id));
            if (restaurant == null)
            {
                throw new Exception("Not Available");
            }
            var query = "SELECT * FROM restaurants WHERE id == " + id;
            var command = createCommand(query);
            try
            {
                myConnection.Open();
                MySqlDataReader myReader = command.ExecuteReader();
                if (myReader.HasRows)
                {
                    result.id = Convert.ToInt32(myReader.GetString("id");
                    result.title = myReader.GetString("title");
                    result.street = myReader.GetString("street");
                    result.cityCode = myReader.GetString("cityCode");
                    result.city = myReader.GetString("city");
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

        internal void SaveRestaurant(Restaurant restaurant)
        {
            var existingRestaurants = GetRestaurants();
            var restaurantExists = existingRestaurants.Exists(
                                                            res =>
                                                                res.id.Equals(restaurant.id));
            if (restaurantExists)
            {
                throw new Exception("Already exists");
            }

            var query = "INSERT INTO restaurants VALUES (" + restaurant.id +
                                                "," + restaurant.title +
                                                "," + restaurant.street +
                                                "," + restaurant.cityCode +
                                                "," + restaurant.city + ")";
            myConnection.Open();
            var command = createCommand(query);
            command.ExecuteNonQuery();
            myConnection.Close();
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
