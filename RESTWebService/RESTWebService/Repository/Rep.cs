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
        public async Task<DataModel> GetDataByIdAsync(string id)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            return await coll.Find(x => x.ID == id).FirstOrDefaultAsync();
        }
        public async Task<IList<DataModel>> GetDataByTempValueAsync(string value)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            return await coll.Find(x => x.Temperature_c == value).ToListAsync();
        }
        public async Task<IList<DataModel>> GetDataByHumidityAsync(string humidity)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            return await coll.Find(x => x.Humidity_p == humidity).ToListAsync();
        }
        public async Task RemoveDataByIdAsync(string id)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            await coll.DeleteOneAsync(x => x.ID == id);
        }
        public async Task ModifyDataAsync(DataModel dm)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            await coll.ReplaceOneAsync(x => x.ID == dm.ID, dm);
        }
        public async Task ModifyTemperatureByIdAsync(string id, string tempValue)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            DataModel dm = await coll.Find(x => x.ID == id).FirstOrDefaultAsync();
            dm.Temperature_c = tempValue;
            await coll.ReplaceOneAsync(x => x.ID == id, dm);
        }
        public async Task ModifyHumidityByIdAsync(string id, string humidityValue)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            DataModel dm = await coll.Find(x => x.ID == id).FirstOrDefaultAsync();
            dm.Humidity_p = humidityValue;
            await coll.ReplaceOneAsync(x => x.ID == id, dm);
        }
    }
}
