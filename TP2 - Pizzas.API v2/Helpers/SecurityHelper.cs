using System;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;
using Pizzas.API.Utils;

namespace Pizzas.API.Helper
{
    public class SecurityHelper
    {
        public static bool isValidToken(string token)
        {
            string sqlQuery = $"SELECT TokenExpirationDate FROM Usuarios WHERE Token = @token";
            using (SqlConnection db = BD.GetConnection())
            {
                DateTime? tokenExpirationDate = db.QueryFirstOrDefault<DateTime?>(sqlQuery, new
                {
                    token = token
                });
                if (tokenExpirationDate == null)
                {
                    return false;
                }
                return tokenExpirationDate > DateTime.Now;
            }
        }

    }
}