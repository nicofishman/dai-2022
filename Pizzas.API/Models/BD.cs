using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;

using Pizzas.API.Models;

namespace Pizzas.API.Utils
{
    public static class BD
    {
        private static string CONNECTION_STRING = @"Persist Security Info=False;User ID=Pizzas;password=Pizzas;Initial Catalog=DAI-Pizzas;Data Source=.;";
        public static List<Pizza> GetAll()
        {
            string sqlQuery = $"SELECT Id, Nombre, LibreGluten, Importe, Descripcion FROM Pizzas";
            List<Pizza> returnList;

            returnList = new List<Pizza>();
            using (SqlConnection db = new SqlConnection(CONNECTION_STRING))
            {
                returnList = db.Query<Pizza>(sqlQuery).ToList();
            }

            return returnList;
        }
        public static Pizza GetById(int id)
        {
            string sqlQuery = $"SELECT Id, Nombre, LibreGluten, Importe, Descripcion FROM Pizzas WHERE Id = {id}";
            Pizza returnEntity = null;
            using (SqlConnection db = new SqlConnection(CONNECTION_STRING))
            {
                returnEntity = db.QueryFirstOrDefault<Pizza>(sqlQuery);
            }
            return returnEntity;
        }
        public static int Add(Pizza pizza)
        {
            string sqlQuery = $"INSERT INTO Pizzas (Nombre, LibreGluten, Importe, Descripcion) VALUES (@nombre, @libreGluten, @importe, @descripcion) ";
            int intRowsAffected = 0;
            using (SqlConnection db = new SqlConnection(CONNECTION_STRING))
            {
                intRowsAffected = db.Execute(sqlQuery, new {
                    nombre= pizza.Nombre,
                    libreGluten= pizza.LibreGluten,
                    importe= pizza.Importe,
                    descripcion= pizza.Descripcion
                });
            }
            return intRowsAffected;
        }

        public static int UpdateById(Pizza pizza)
        {
            int intRowsAffected = 0;
            string sqlQuery = $"UPDATE Pizzas SET Nombre = @nombre, LibreGluten = @libreGluten, Importe = @importe, Descripcion = @descripcion WHERE Id = @pizzaId";
            using (var db = new SqlConnection(CONNECTION_STRING))
            {
                intRowsAffected = db.Execute(sqlQuery, new {
                    pizzaId = pizza.Id,
                    nombre= pizza.Nombre,
                    libreGluten= pizza.LibreGluten,
                    importe= pizza.Importe,
                    descripcion= pizza.Descripcion
                });
            }
            return intRowsAffected;
        }
        public static int DeleteById(int id)
        {
            string sqlQuery = $"DELETE FROM Pizzas WHERE Id = {id}";
            int intRowsAffected = 0;
            using (SqlConnection db = new SqlConnection(CONNECTION_STRING))
            {
                intRowsAffected = db.Execute(sqlQuery);
            }
            return intRowsAffected;
        }
    }
}