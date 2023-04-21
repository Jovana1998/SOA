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
            //for(int i = 0; i<9;i++)
            //{
            //    listData.Add(new TempModel()
            //    {
            //        id = "644283c8fd7ef303b8e99bf1",
            //        value ="12",
            //        recordTime = "10/01/2022",
            //        type = "temp"
            //    });
            //}
            return listData;
        }
    }
}
