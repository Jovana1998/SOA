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
        private readonly IMongoCollection<DataModel> coll;
        public Rep(IMongoDatabase db)
        {
            _dbContext = db;
            coll = db.GetCollection<DataModel>("DATA");
        }
        public async Task AddDataAsync(DataModel dm)
        {
            await coll.InsertOneAsync(dm);
        }
        public async Task<IList<DataModel>> GetAllDataAsync()
        {
            return await coll.Find(x => true).ToListAsync();

        }
        public async Task<DataModel> GetDataByIdAsync(string id)
        {
            return await coll.Find(x => x.ID == id).FirstOrDefaultAsync();
        }
        public async Task<IList<DataModel>> GetDataByTempValueAsync(string value)
        {
            return await coll.Find(x => x.Temperature_c == value).ToListAsync();
        }
        public async Task<IList<DataModel>> GetDataByHumidityAsync(string humidity)
        {
            return await coll.Find(x => x.Humidity_p == humidity).ToListAsync();
        }
        public async Task RemoveDataByIdAsync(string id)
        {
            await coll.DeleteOneAsync(x => x.ID == id);
        }
        public async Task ModifyDataAsync(DataModel dm)
        {
            await coll.ReplaceOneAsync(x => x.ID == dm.ID, dm);
        }
        public async Task ModifyTemperatureByIdAsync(string id, string tempValue)
        {
            DataModel dm = await coll.Find(x => x.ID == id).FirstOrDefaultAsync();
            dm.Temperature_c = tempValue;
            await coll.ReplaceOneAsync(x => x.ID == id, dm);
        }
        public async Task ModifyHumidityByIdAsync(string id, string humidityValue)
        {
     
            DataModel dm = await coll.Find(x => x.ID == id).FirstOrDefaultAsync();
            dm.Humidity_p = humidityValue;
            await coll.ReplaceOneAsync(x => x.ID == id, dm);
        }
    }
}
