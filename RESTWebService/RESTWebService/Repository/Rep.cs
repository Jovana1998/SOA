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
        public async Task<DataModel> GetDataByValueAsync(string value)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            return await coll.Find(x => x.Value == value).FirstOrDefaultAsync();
        }
        public async Task<DataModel> GetDataByTypeAsync(string type)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            return await coll.Find(x => x.Type == type).FirstOrDefaultAsync();
        }
        public async Task RemoveDataAsync()
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            string id = "5ca4bbcea2dd94ee58162a65";
            await coll.DeleteOneAsync(x => x.ID == id);
        }
         public async Task RemoveDataByIdAsync(string id)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            await coll.DeleteOneAsync(x => x.ID == id);
        }
        public async Task RemoveDataByValueAsync(string value)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            await coll.DeleteOneAsync(x => x.Value == value);
        }
        public async Task ModifyDataAsync(DataModel dm)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            await coll.ReplaceOneAsync(x => x.ID == dm.ID, dm);
        }
        public async Task ModifyDataByIdAsync(string id, string value)
        {
            var coll = _dbContext.GetCollection<DataModel>("DATA");
            DataModel dm = await coll.Find(x => x.ID == id).FirstOrDefaultAsync();
            dm.Value = value;
            await coll.ReplaceOneAsync(x => x.ID == id, dm);
        }
    }
}
