using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace GrpcClient.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Temperature.TemperatureClient client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var channel = GrpcChannel.ForAddress("http://localhost:5001",
            new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new Temperature.TemperatureClient(channel);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            var reply = client.GetAllData(new Empty());
            return Ok(reply);
        }
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery]GetByIdRequest request)
        {
            var reply = client.GetById(request);
            return Ok(reply);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRequest request)
        {
            var reply = client.Create(request);
            return Ok(reply);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateRequest request)
        {
            var reply = client.Update(request);
            return Ok(reply);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteRequest request)
        {
            var reply = client.Delete(request);
            return Ok(reply);
        }




    }
}