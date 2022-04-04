using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;

namespace Pizzas.API.Utils
{
    public class BD {
        public static SqlConnection GetConnection()
        {
            string CONNECTION_STRING = @"Persist Security Info=False;User ID=Pizzas;password=Pizzas;Initial Catalog=DAI-Pizzas;Data Source=.;";
            return new SqlConnection(CONNECTION_STRING);
        }
    }
}