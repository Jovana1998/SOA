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
    }
}
