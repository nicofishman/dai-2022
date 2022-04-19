using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
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
            ConfigurationHelper.SetConfiguration();
            string sqlQuery = $"SELECT Id, Nombre, LibreGluten, Importe, Descripcion FROM Pizzas";
            List<Pizza> returnList;

            returnList = new List<Pizza>();
            try
            {
                using (SqlConnection db = BD.GetConnection())
                {
                    returnList = db.Query<Pizza>(sqlQuery).ToList();
                }
                return returnList;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static Pizza GetById(int id)
        {
            string sqlQuery = $"SELECT Id, Nombre, LibreGluten, Importe, Descripcion FROM Pizzas WHERE Id = {id}";
            Pizza returnEntity = null;
            try
            {
                using (SqlConnection db = BD.GetConnection())
                {
                    returnEntity = db.QueryFirstOrDefault<Pizza>(sqlQuery);
                }
                return returnEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Add(Pizza pizza, string token)
        {
            string sqlQuery = $"INSERT INTO Pizzas (Nombre, LibreGluten, Importe, Descripcion) VALUES (@nombre, @libreGluten, @importe, @descripcion) ";
            int intRowsAffected = 0;
            try
            {
                if (SecurityHelper.isValidToken(token))
                {
                    using (SqlConnection db = BD.GetConnection())
                    {
                        intRowsAffected = db.Execute(sqlQuery, new
                        {
                            nombre = pizza.Nombre,
                            libreGluten = pizza.LibreGluten,
                            importe = pizza.Importe,
                            descripcion = pizza.Descripcion
                        });
                    }
                }
                else
                {
                    intRowsAffected = -1;
                }
                return intRowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int UpdateById(Pizza pizza, string token)
        {
            int intRowsAffected = 0;
            string sqlQuery = $"UPDATE Pizzas SET Nombre = @nombre, LibreGluten = @libreGluten, Importe = @importe, Descripcion = @descripcion WHERE Id = @pizzaId";
            try
            {
                if (SecurityHelper.isValidToken(token))
                {
                    using (SqlConnection db = BD.GetConnection())
                    {
                        intRowsAffected = db.Execute(sqlQuery, new
                        {
                            pizzaId = pizza.Id,
                            nombre = pizza.Nombre,
                            libreGluten = pizza.LibreGluten,
                            importe = pizza.Importe,
                            descripcion = pizza.Descripcion
                        });
                    }
                }
                else
                {
                    intRowsAffected = -1;
                }
                return intRowsAffected;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static int DeleteById(int id, string token)
        {
            string sqlQuery = $"DELETE FROM Pizzas WHERE Id = {id}";
            int intRowsAffected = 0;
            try
            {
                if (SecurityHelper.isValidToken(token))
                {
                    using (SqlConnection db = BD.GetConnection())
                    {
                        intRowsAffected = db.Execute(sqlQuery);
                    }
                }
                else
                {
                    intRowsAffected = -1;
                }
                return intRowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}