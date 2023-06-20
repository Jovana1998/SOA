using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using AnalyticsWebService.Services;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Channels;

namespace AnalyticsWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        private AnalyticsService _analyticsService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
           
        }
        [HttpPost]
        public IActionResult CreateAnalyticsService()
        {
            if (_analyticsService == null)
                _analyticsService = new AnalyticsService();
            return Ok();
        }
    }
}