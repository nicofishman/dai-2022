using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;
using Pizzas.API.Utils;
using Pizzas.API.Helper;

namespace Pizzas.API.Services
{
    public static class PizzaService
    {
        public static List<Pizza> GetAll()
        {
            string sqlQuery = $"SELECT Id, Nombre, LibreGluten, Importe, Descripcion FROM Pizzas";
            List<Pizza> returnList;

            returnList = new List<Pizza>();
            using (SqlConnection db = BD.GetConnection())
            {
                returnList = db.Query<Pizza>(sqlQuery).ToList();
            }

            return returnList;
        }
        public static Pizza GetById(int id)
        {
            string sqlQuery = $"SELECT Id, Nombre, LibreGluten, Importe, Descripcion FROM Pizzas WHERE Id = {id}";
            Pizza returnEntity = null;
            using (SqlConnection db = BD.GetConnection())
            {
                returnEntity = db.QueryFirstOrDefault<Pizza>(sqlQuery);
            }
            return returnEntity;
        }
        public static int Add(Pizza pizza, string token)
        {
            string sqlQuery = $"INSERT INTO Pizzas (Nombre, LibreGluten, Importe, Descripcion) VALUES (@nombre, @libreGluten, @importe, @descripcion) ";
            int intRowsAffected = 0;
            if (SecurityHelper.isValidToken(token)){
                using (SqlConnection db = BD.GetConnection())
                {
                    intRowsAffected = db.Execute(sqlQuery, new {
                        nombre= pizza.Nombre,
                        libreGluten= pizza.LibreGluten,
                        importe= pizza.Importe,
                        descripcion= pizza.Descripcion
                    });
                }
            } else {
                intRowsAffected = -1;
            }
            return intRowsAffected;
        }

        public static int UpdateById(Pizza pizza, string token)
        {
            int intRowsAffected = 0;
            string sqlQuery = $"UPDATE Pizzas SET Nombre = @nombre, LibreGluten = @libreGluten, Importe = @importe, Descripcion = @descripcion WHERE Id = @pizzaId";
            if (SecurityHelper.isValidToken(token)){
                using (SqlConnection db = BD.GetConnection())
                {
                    intRowsAffected = db.Execute(sqlQuery, new {
                        pizzaId = pizza.Id,
                        nombre= pizza.Nombre,
                        libreGluten= pizza.LibreGluten,
                        importe= pizza.Importe,
                        descripcion= pizza.Descripcion
                    });
                }
            } else {
                intRowsAffected = -1;
            }
            return intRowsAffected;
        }
        public static int DeleteById(int id, string token)
        {
            string sqlQuery = $"DELETE FROM Pizzas WHERE Id = {id}";
            int intRowsAffected = 0;
            if (SecurityHelper.isValidToken(token)){
                using (SqlConnection db = BD.GetConnection())
                {
                    intRowsAffected = db.Execute(sqlQuery);
                }
            } else {
                intRowsAffected = -1;
            }
            return intRowsAffected;
        }
    }
}