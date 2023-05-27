
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTWebService.Model;
using RESTWebService.DBContext;
using System.ComponentModel.DataAnnotations;
using RESTWebService.Repository;
using Microsoft.Extensions.Logging;

namespace RESTWebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private IRep _repository;
        public DataController()
        {
            _repository = new Rep(DBContext.DBContext.GetInstance());
        }
        [HttpPost]
        public async Task<IActionResult> AddData([FromBody, Required] DataModel dataModel)
        {

            if (dataModel == null)
            {
                return BadRequest();
            }
            await _repository.AddDataAsync(dataModel);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            return Ok(await _repository.GetAllDataAsync());
        }
        [HttpGet]
        public async Task<IActionResult> GetDataById([Required] string id)
        {
            return Ok(await _repository.GetDataByIdAsync(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetDataByTemperature([Required] string temperature)
        {
            return Ok(await _repository.GetDataByTempValueAsync(temperature));
        }
        [HttpGet]
        public async Task<IActionResult> GetDataByHumidity([Required] string humidity)
        {
            return Ok(await _repository.GetDataByHumidityAsync(humidity));
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveDataById([Required] string id)
        {
            await _repository.RemoveDataByIdAsync(id);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> ModifyData([FromBody] DataModel dm)
        {
            if (dm == null)
            {
                return BadRequest();
            }
            await _repository.ModifyDataAsync(dm);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> ModifyTemperatureById([Required] string id, [Required] string tempValue)
        {
            await _repository.ModifyTemperatureByIdAsync(id, tempValue);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> ModifyHumidityById([Required] string id, [Required] string humidityValue)
        {
            await _repository.ModifyHumidityByIdAsync(id, humidityValue);
            return Ok();
        }
    }
}
