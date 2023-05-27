using GraphQLService.IService;
using GraphQLService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLService.Models
{
    public class Query
    {
        ITemperatureService _temperatureService = null;
        public Query(ITemperatureService temperatureService)
        {
            _temperatureService = temperatureService;
        }

        public List<TempModel> TempModels => _temperatureService.GetAllData();

        public TempModel TempModel => _temperatureService.GetDataById("6445743c1ea9933fcc9fb205");
    }
}
