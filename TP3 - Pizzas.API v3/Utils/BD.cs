using System;
using System.Reflection;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;
using Pizzas.API.Helper;

namespace Pizzas.API.Utils
{
    public class BD
    {
        public static SqlConnection GetConnection()
        {
            try
            {
                string CONNECTION_STRING = ConfigurationHelper.GetConfiguration()["DatabaseSettings:ConnectionString"];
                return new SqlConnection(CONNECTION_STRING);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}