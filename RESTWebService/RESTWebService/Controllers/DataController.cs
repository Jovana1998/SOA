
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

    //[Route("[controller]")]
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
        public async Task<IActionResult> AddData([FromBody, Required] DataModel sensorDataModel)
        {

            if (sensorDataModel == null)
            {
                return BadRequest();
            }
            await _repository.AddDataAsync(sensorDataModel);
            return Ok();
        }
    }
}
