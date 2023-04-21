using GraphQLService.IService;
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
    }
}
