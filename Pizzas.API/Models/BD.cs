using System;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Pizzas.API.Models
{
    public class BD
    {
        private const string _ConnectionString = "Persist Security Info=False;User ID=Pizzas;password=Pizzas;Initial Catalog=DAI-Pizzas;Data Source=.;";
        //GetAll method that parses to pizza class
        public static IEnumerable<Pizza> GetAll()
        {
            List<Pizza> misPizzas = null;
            using (SqlConnection db = new SqlConnection(_ConnectionString))
            {
                misPizzas = db.QueryFirstOrDefault<PersonaMasEstablecimiento>(query);
            }
        }

    }
}