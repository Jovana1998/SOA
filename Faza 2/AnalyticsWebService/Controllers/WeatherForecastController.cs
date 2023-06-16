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
        [HttpGet("Subscribe")]
        public async Task<IActionResult> Subscribe()
        {
            return Ok();
        }

        [HttpGet("Get")]
        public IEnumerable<WeatherForecast> Get()
        {
            string porukaPayload = "Cao Joko";
            var mqttFactory = new MqttFactory();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = ""
            })
            .ToArray();
        }
        [HttpGet("Index")]
        public void Index()
        {
            string porukaPayload = "Cao Joko";
            var mqttFactory = new MqttFactory();
           var factory = new ConnectionFactory
            {
                UserName = "guest",
                Password = "guest",
                Port = 15672,
                HostName = "rabbitmq",
                VirtualHost = "/",
                DispatchConsumersAsync = false
            };
            var endpoints = new System.Collections.Generic.List<AmqpTcpEndpoint> {
                      new AmqpTcpEndpoint("hostname"),
                      new AmqpTcpEndpoint("rabbitmq")
                };
            var conn = factory.CreateConnection(endpoints);
            var topic = "JokaVida";
           var  channel = conn.CreateModel();
            //channel.ExchangeDeclare(topic, ExchangeType.Direct);
            channel.QueueDeclare(queue: topic,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
            //channel.QueueBind(topic, topic, topic, null);
            var consumer = new EventingBasicConsumer(channel);
            
            channel.BasicConsume(queue: topic,
                                 autoAck: true,
                                 consumer: consumer);


        }
    }
}