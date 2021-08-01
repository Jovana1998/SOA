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
    
    //[Route("[controller]")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private IWeatherRepository _repository;
        public DataController()
        {
            _repository = new WeatherRepository(WeatherContext.GetInstance());
        }
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

        [HttpGet]
        public async Task<IActionResult> GetData([Required] string id)
        {
            if(id== null)
            {
                return BadRequest();
            }
            //await _repository.GetDataAsync(id);
            return Ok(await _repository.GetDataAsync(id));// kad je unutar to znaci da se stampa na ekran
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveData()
        {
            await _repository.RemoveDataAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveOneData([Required] string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            await _repository.RemoveOneDataAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> ModifyData([Required, FromBody] DataModel dm)
        {
            if (dm == null)
            {
                return BadRequest();
            }
            await _repository.ModifyDataAsync(dm);
            return Ok();
        }
    }
}
