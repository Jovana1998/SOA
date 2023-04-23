using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLService.IService;
using GraphQLService.Models;
using MongoDB.Driver;

namespace GraphQLService.Service
{
    public class TemperatureService : ITemperatureService
    {
        private readonly IMongoDatabase _dbContext;
        public TemperatureService()
        {
            _dbContext = DBContext.DBContext.GetInstance();
        }
        public List<TempModel> GetAllData()
        {
            var coll = _dbContext.GetCollection<TempModel>("DATA");
            List<TempModel> listData = new List<TempModel>();
            listData =  coll.Find(x => true).ToList();
            return listData;
        }
        public TempModel GetDataById(string id)
        {
            var coll = _dbContext.GetCollection<TempModel>("DATA");
            TempModel data = new TempModel();
            data = coll.Find(x => x.Id == id).FirstOrDefault();
            return data;
        }
    }
}
