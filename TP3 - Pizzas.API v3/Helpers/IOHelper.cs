using System;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;
using Pizzas.API.Utils;
using Pizzas.API.Services;
using System.IO;

namespace Pizzas.API.Helper
{
    public class IOHelper
    {
        public static bool AppendInFile(string fullFileName, string data){
            try{
                using(StreamWriter w = File.AppendText(fullFileName)){
                    w.WriteLine(data);
                }
                return true;
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}