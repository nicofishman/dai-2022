using System;
using System.Linq;
using System.Reflection;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;
using Pizzas.API.Utils;

namespace Pizzas.API.Services
{
    public class UsuarioService {
        public static Usuario Login(string user, string pwd){
            Usuario returnEntity = null;
            returnEntity = GetByUserNamePassword(user, pwd);

            if(returnEntity != null){
                string token = RefreshToken(returnEntity.Id);
                if (token != null) {
                    returnEntity = GetByUserNamePassword(user, pwd);
                }
            }
            if (returnEntity == null) {
                return null;
            }
            returnEntity.Password = new string('*', returnEntity.Password.Length);
            return returnEntity;
        }

        public static Usuario GetByUserNamePassword(string user, string pwd) {
            string sqlQuery = $"SELECT * FROM Usuarios WHERE UserName = @userName AND Password = @password";
            Usuario returnEntity = null;
            using (SqlConnection db = BD.GetConnection())
            {
                returnEntity = db.QueryFirstOrDefault<Usuario>(sqlQuery, new { 
                    userName = user, 
                    password = pwd 
                });
            }
            return returnEntity;
        }

        public static Usuario GetById(int id) {
            string sqlQuery = $"SELECT * FROM Usuarios WHERE Id = @id";
            Usuario returnEntity = null;
            using (SqlConnection db = BD.GetConnection())
            {
                returnEntity = db.QueryFirstOrDefault<Usuario>(sqlQuery, new { 
                    id = id 
                });
            }
            return returnEntity;
        }

        public static string RefreshToken(int id){
            string sqlQuery = $"UPDATE Usuarios SET Token = @token, TokenExpirationDate = DATEADD(minute, 15, GETDATE()) WHERE Id = @id";
            int intRowsAffected = 0;
            using (SqlConnection db = BD.GetConnection())
            {
                intRowsAffected = db.Execute(sqlQuery, new {
                    id= id,
                    token= Guid.NewGuid().ToString()
                });
            }
            Usuario newUser = GetById(id);
            return newUser.Token;
        }

        public static Usuario GetByToken(string token) {
            string sqlQuery = $"SELECT * FROM Usuarios WHERE Token = @token";
            Usuario returnEntity = null;
            try{
                using (SqlConnection db = BD.GetConnection())
                {
                    returnEntity = db.QueryFirstOrDefault<Usuario>(sqlQuery, new { 
                        token = token 
                    });
                }
            } catch (Exception ex) {
                MethodBase m = MethodBase.GetCurrentMethod();
                CustomLog.LogError(ex, m.ReflectedType.Name, m.Name, null);
            }
            return returnEntity;
        }
    }
}