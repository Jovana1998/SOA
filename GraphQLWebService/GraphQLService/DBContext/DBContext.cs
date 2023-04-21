using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
namespace GraphQLService.DBContext
{
    public class DBContext : DbContext
    {
        private static IMongoDatabase _db = null;
        private static readonly object objLock = new object();

        public static IMongoDatabase GetInstance()
        {
            if (_db == null)
            {
                lock (objLock)
                {
                    if (_db == null)
                    {
                        _db = CreateDB();
                    }
                }

            }
            return _db;
        }
        private static IMongoDatabase CreateDB()
        {

            var settings = MongoClientSettings.FromConnectionString("mongodb://soa:soa12345@cluster0-shard-00-00.xnw0z.mongodb.net:27017,cluster0-shard-00-01.xnw0z.mongodb.net:27017,cluster0-shard-00-02.xnw0z.mongodb.net:27017/myFirstDatabase?ssl=true&replicaSet=atlas-qmzfdd-shard-0&authSource=admin&retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("soa-2023");
            return database;

        }
    }
}
