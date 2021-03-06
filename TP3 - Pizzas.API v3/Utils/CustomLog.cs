using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;
using Pizzas.API.Helper;
using Newtonsoft.Json;

namespace Pizzas.API.Utils
{
    public class CustomLog
    {
        private static void CreateLogFolder()
        {
            string path = ConfigurationHelper.GetConfiguration()["CustomLog:LogFolder"];
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public static void LogError(string errorData)
        {
            LogError(errorData, null, null, null, null);
        }
        public static void LogError(Exception ex)
        {
            LogError(ex.Message, null, null, null, ex.StackTrace);
        }

        public static void LogError(string errorData, string errorMessage)
        {
            LogError(errorData, errorMessage, null, null, null);
        }
        public static void LogError(Exception ex, string className, string functionName)
        {
            LogError(ex.Message, className, functionName, null, ex.StackTrace);
        }

        public static void LogError(Exception ex, string className, string contexto, object datos)
        {
            LogError(ex.Message, className, contexto, datos, ex.StackTrace);
        }

        public static void LogError(string errorData, string className, string contexto, object datos, string stackTrace)
        {
            string customData, datosString = "";
            if (datos != null)
            {
                datosString = JsonConvert.SerializeObject(datos);
            }
            string[] newStackTrace = stackTrace.Split('\n');
            string pizzaStackTrace = "";
            foreach (var item in newStackTrace)
            {
                if (!item.TrimStart().StartsWith("at System."))
                {
                    Console.WriteLine(item.TrimStart());
                    pizzaStackTrace += item + "\n";
                }
            }

            customData = string.Format("{0} {1}{2,20}{3,10}{4}",
                DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"),
                errorData,
                (className != null) ? $"\n\tFile\t= {className}" : "",
                (contexto != null) ? $"\n\tContexto\t= {contexto}" : "",
                (stackTrace != null) ? $"\n\tStack\t = {pizzaStackTrace}" : "",
                (datos != null) ? $"\n\tDatos\t= {datos}" : ""
            );
            try
            {
                CreateLogFolder();
                string fullFileName = ConfigurationHelper.GetConfiguration()["CustomLog:LogFolder"] + @"\log.txt";
                IOHelper.AppendInFile(fullFileName, customData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}