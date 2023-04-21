using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLService.IService;
using GraphQLService.Models;

namespace GraphQLService.Service
{
    public class TemperatureService : ITemperatureService
    {
        public List<TempModel> GetAllData()
        {
            List<TempModel> listData = new List<TempModel>();
            for(int i = 0; i<9;i++)
            {
                listData.Add(new TempModel()
                {
                    id = "644283c8fd7ef303b8e99bf1",
                    value ="12",
                    recordTime = "10/01/2022",
                    type = "temp"
                });
            }
            return listData;
        }
    }
}
