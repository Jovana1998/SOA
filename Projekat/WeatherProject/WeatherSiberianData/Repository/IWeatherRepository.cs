using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherSiberianData.Model;

namespace WeatherSiberianData.Repository
{
    interface IWeatherRepository
    {
        Task AddDataAsync(DataModel dm);
        Task<IList<DataModel>> GetAllDataAsync();

        Task<IList<DataModel>> GetDataAsync(string id);
        Task<IList<DataModel>> GetAllDataTypeAsync(string type);

        Task<IList<DataModel>> GetAllDataValueAsync(string value);
        Task RemoveDataAsync();
        Task RemoveOneDataAsync(string id);
        Task ModifyDataAsync(DataModel dm);

        Task ModifyDataValueAsync(string id, string value);

    }
}
