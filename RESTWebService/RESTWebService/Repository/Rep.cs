using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using RESTWebService.Model;


namespace RESTWebService.Repository
{
    public class Rep : IRep
    {
        private readonly IMongoDatabase _dbContext;
        public Rep(IMongoDatabase db)
        {
            _dbContext = db;
        }
        //DataModel dd= new DataModel
        public async Task AddDataAsync(DataModel dm)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            await coll.InsertOneAsync(dm);
        }
    }
}
