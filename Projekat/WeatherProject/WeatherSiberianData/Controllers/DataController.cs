using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherSiberianData.Model;
using WeatherSiberianData.DBContext;
using System.ComponentModel.DataAnnotations;
using WeatherSiberianData.Repository;

namespace WeatherSiberianData.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataController : Controller
    {
        private IWeatherRepository _repository;
        public DataController()
        {
            _repository = new WeatherRepository(WeatherContext.GetInstance());
        }
    }
}
