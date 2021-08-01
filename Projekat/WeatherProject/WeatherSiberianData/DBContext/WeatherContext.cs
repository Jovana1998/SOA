using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.IO;

namespace WeatherSiberianData.DBContext
{
    public class WeatherContext :DbContext
    {
        private static IMongoDatabase _db=null;
        private static readonly object objLock = new object();

        public static IMongoDatabase GetInstance()
        {
            if (_db==null)
            {
                lock (objLock)
                {
                    if (_db==null)
                    {
                        _db = CreateDB();
                    }
                }  
              
            }
            return _db;
        }
        private static IMongoDatabase CreateDB()
        {
            //string connectionString = string.Empty;
            //using (var sr = new StreamReader("mongodb+srv://vida:sifra3000@cluster0.hhqm6.mongodb.net/Cluster0?retryWrites=true&w=majority"))
            //{
            //    ///*mongodb+srv://Jovana:vidosava98@cluster0.4ukyt.mongodb.net/Cluster0?retryWrites=true&w=majority*/
            //    connectionString = sr.ReadToEnd(); 
            //}
            //var client = new MongoClient(connectionString);
            //var db=client.GetDatabase("SOA");
            //return db;



            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://vida:sifra3000@cluster0.hhqm6.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("WeatherSiberianData");
            return database;
        }
    }
}
