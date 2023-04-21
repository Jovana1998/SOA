
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
        public async Task<IActionResult> GetDataByValue([Required] string value)
        {
            //provera za value
            return Ok(await _repository.GetDataByValueAsync(value));
        }
        [HttpGet]
        public async Task<IActionResult> GetDataByType([Required] string type)
        {
            return Ok(await _repository.GetDataByTypeAsync(type));
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveData()
        {
            await _repository.RemoveDataAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveDataById([Required] string id)
        {
            await _repository.RemoveDataByIdAsync(id);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveDataByValue([Required] string value)
        {
            await _repository.RemoveDataByValueAsync(value);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> ModifyData([/*Required,*/ FromBody] DataModel dm)
        {
            if (dm == null)
            {
                return BadRequest();
            }
            await _repository.ModifyDataAsync(dm);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> ModifyDataById([Required] string id, [Required] string value)
        {
            await _repository.ModifyDataByIdAsync(id, value);
            return Ok();
        }


    }
}
