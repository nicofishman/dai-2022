using System;
using System.Reflection;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;
using Pizzas.API.Utils;

namespace Pizzas.API.Services
{
    public class UsuarioService
    {
        public static Usuario Login(string user, string pwd)
        {
            Usuario returnEntity = null;
            try
            {
                returnEntity = GetByUserNamePassword(user, pwd);

                if (returnEntity != null)
                {
                    string token = RefreshToken(returnEntity.Id);
                    if (token != null)
                    {
                        returnEntity = GetByUserNamePassword(user, pwd);
                    }
                }
                if (returnEntity == null)
                {
                    return null;
                }
                returnEntity.Password = new string('*', returnEntity.Password.Length);
                return returnEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Usuario GetByUserNamePassword(string user, string pwd)
        {
            string sqlQuery = $"SELECT * FROM Usuarios WHERE UserName = @userName AND Password = @password";
            Usuario returnEntity = null;
            try
            {
                using (SqlConnection db = BD.GetConnection())
                {
                    returnEntity = db.QueryFirstOrDefault<Usuario>(sqlQuery, new
                    {
                        userName = user,
                        password = pwd
                    });
                }
                return returnEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Usuario GetById(int id)
        {
            string sqlQuery = $"SELECT * FROM Usuarios WHERE Id = @id";
            Usuario returnEntity = null;
            try
            {
                using (SqlConnection db = BD.GetConnection())
                {
                    returnEntity = db.QueryFirstOrDefault<Usuario>(sqlQuery, new
                    {
                        id = id
                    });
                }
                return returnEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string RefreshToken(int id)
        {
            string sqlQuery = $"UPDATE Usuarios SET Token = @token, TokenExpirationDate = DATEADD(minute, 15, GETDATE()) WHERE Id = @id";
            int intRowsAffected = 0;
            try
            {
                using (SqlConnection db = BD.GetConnection())
                {
                    intRowsAffected = db.Execute(sqlQuery, new
                    {
                        id = id,
                        token = Guid.NewGuid().ToString()
                    });
                }
                Usuario newUser = GetById(id);
                return newUser.Token;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Usuario GetByToken(string token)
        {
            string sqlQuery = $"SELECT * FROM Usuarios WHERE Token = @token";
            Usuario returnEntity = null;
            try
            {
                using (SqlConnection db = BD.GetConnection())
                {
                    returnEntity = db.QueryFirstOrDefault<Usuario>(sqlQuery, new
                    {
                        token = token
                    });
                }
                return returnEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}