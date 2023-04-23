using Grpc.Net.Client;
using GrpcClient;
using GrpcService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GrpcClient.Controllers
{
    public class HomeController : Controller
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

        public async Task<IActionResult> Index()
        {
          
            var request = new HelloRequest { Name = "Joka"};
            var reply = await client.SayHelloAsync(request);
            return View();
        }
        public async Task<IActionResult> GetAllData()
        {

            var request = new HelloRequest { Name = "Joka" };
            var reply = await client.SayHelloAsync(request);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}