using System;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;
using Pizzas.API.Utils;
using Pizzas.API.Services;

namespace Pizzas.API.Helper
{
    public class SecurityHelper
    {
        public static bool isValidToken(string token)
        {
            try
            {
                Usuario user = UsuarioService.GetByToken(token);
                if (user == null) return false;
                return user.TokenExpirationDate > DateTime.Now;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}