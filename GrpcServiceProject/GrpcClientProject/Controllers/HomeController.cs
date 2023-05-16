using Grpc.Net.Client;
using GrpcClient;
using GrpcService;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback =
    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var channel = GrpcChannel.ForAddress("http://localhost:5001",
    new GrpcChannelOptions { HttpHandler = httpHandler });
            var client = new Temperature.TemperatureClient(channel);
            var request = new HelloRequest { Name = "Joka" };
            var reply = await client.SayHelloAsync(request);
            return Ok();
        }

       

     
    }
}