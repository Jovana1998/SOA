using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTWebService.Model;

namespace RESTWebService.Repository
{
    interface IRep
    {
        Task AddDataAsync(DataModel dm);
        Task<IList<DataModel>> GetAllDataAsync();
        Task<DataModel> GetDataByIdAsync(string id);
        Task<IList<DataModel>> GetDataByTempValueAsync(string value);
        Task<IList<DataModel>> GetDataByHumidityAsync(string humidity);
        Task RemoveDataByIdAsync(string id);
        Task ModifyDataAsync(DataModel dm);
        Task ModifyTemperatureByIdAsync(string id, string tempValue);
        Task ModifyHumidityByIdAsync(string id, string humidityValue);
    }
}
