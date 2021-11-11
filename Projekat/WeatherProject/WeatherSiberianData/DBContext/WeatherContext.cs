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
            //var settings = MongoClientSettings.FromConnectionString("mongodb+srv://vida:sifra3000@cluster0.hhqm6.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            //var client = new MongoClient(settings);
            //var database = client.GetDatabase("WeatherSiberianData");
            //return database;


           // var settings = MongoClientSettings.FromConnectionString("mongodb+srv://vida:sifra3000@cluster0.hhqm6.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            //var client = new MongoClient(settings);
            //var database = client.GetDatabase("WeatherSiberianData");
           // return database;



            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://soa:soa12345@cluster0.xnw0z.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("test");
            return database;





        }
    }
}
