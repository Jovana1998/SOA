using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using WeatherSiberianData.Model;


namespace WeatherSiberianData.Repository
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly IMongoDatabase _dbContext;
        public WeatherRepository (IMongoDatabase db)
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
