using GraphQLService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLService.IService
{
    public interface ITemperatureService
    {
      public List<TempModel> GetAllData();
    }
}
