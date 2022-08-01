using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;
using Pizzas.API.Utils;
using Pizzas.API.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace Pizzas.API.Helper
{
    public class ConfigurationHelper
    {
        public static IConfiguration GetConfiguration()
        {
            IConfiguration config;
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            config = builder.Build();
            return config;
        }
        public static void SetConfiguration()
        {
            string output;
            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                jsonObj["CustomLog"]["LogFolder"] = Directory.GetCurrentDirectory() + @"\Logs";
                output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            }
            File.WriteAllText("appsettings.json", output);
        }
    }
}