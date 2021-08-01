using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherSiberianData.Model;
using WeatherSiberianData.DBContext;
using System.ComponentModel.DataAnnotations;
using WeatherSiberianData.Repository;
using Microsoft.Extensions.Logging;

namespace WeatherSiberianData.Controllers
{
    
    [Route("[controller]")]
    //[Route("api/[controller]/[action]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private IWeatherRepository _repository;
        public DataController()
        {
            _repository = new WeatherRepository(WeatherContext.GetInstance());
        }
        //private readonly ILogger<DataController> _logger;
        //public DataController(ILogger<DataController> logger)
        //{
        //    _logger = logger;
        //}
        //[HttpGet]
        //public IEnumerable<DataModel> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new DataModel
        //    {
        //        ID = "5553a998e4b02cf7151190b8",
        //        Type = "1",
        //        Value = "2"
        //    })
        //    .ToArray();
        //}
        [HttpPost]
        public async Task<IActionResult> AddSensorData([FromBody, Required] DataModel sensorDataModel)
        {
            
            if (sensorDataModel == null)
            {
                return BadRequest();
            }
            await _repository.AddDataAsync(sensorDataModel);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            return Ok(await _repository.GetAllDataAsync());
        }
    }
}
