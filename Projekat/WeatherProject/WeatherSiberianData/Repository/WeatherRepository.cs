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
        public async Task<IList<DataModel>> GetAllDataAsync()
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            return await coll.Find(x => true).ToListAsync();

        }
        public async Task<IList<DataModel>> GetDataAsync(string id)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            return await coll.Find(x => x.ID == id).ToListAsync();
        }

        public async Task<IList<DataModel>> GetAllDataTypeAsync(string type)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            return await coll.Find(x => x.Type == type).ToListAsync();

        }
        public async Task<IList<DataModel>> GetAllDataValueAsync(string value)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            return await coll.Find(x => x.Value == value).ToListAsync();

        }
        public async Task RemoveDataAsync()
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            string st = "5ca4bbcea2dd94ee58162a65";
            await coll.DeleteOneAsync(x => x.ID ==st);
        }
        public async Task RemoveOneDataAsync(string id)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            await coll.DeleteManyAsync(x => x.ID == id);
        }
        public async Task ModifyDataAsync(DataModel dm)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            await coll.ReplaceOneAsync(x => x.ID == dm.ID, dm);
        }

        public async Task ModifyDataValueAsync(string id, string value)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            var a =  await coll.Find(x => x.ID == id).ToListAsync();
            DataModel b = a.First();
            b.Value = value;
            await coll.ReplaceOneAsync(x => x.ID == id, b);
        }
    }
}
