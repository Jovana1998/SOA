using Microsoft.AspNetCore.Mvc;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;

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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            string porukaPayload = "Cao Joko";
            var mqttFactory = new MqttFactory();

           

            var options = new MqttClientOptionsBuilder()
                .WithClientId("131df074d5c8")
                .WithCredentials(string.Empty, string.Empty)
                .WithTcpServer("172.28.80.1", 1883)
                //.WithCleanSession()
                .Build();

            IMqttClient client = mqttFactory.CreateMqttClient();

            //var conn = client.ConnectAsync(options).Result;

            //var message = new MqttApplicationMessageBuilder()
            //    .WithTopic("JokaVida")
            //    .WithPayload(porukaPayload)
            //    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
            //    .Build();
            //client.PublishAsync(message);
            //client.DisconnectAsync();

            var topicFilter = new MqttTopicFilterBuilder()
                .WithTopic("JokaVida")
                .Build();
            var conn1 = client.ConnectAsync(options).Result;
            var sub = client.SubscribeAsync(topicFilter);
            if (client.IsConnected)
            {
                Console.WriteLine("Konektovano je na topik:" + sub.Result);
            }
            ///var han = client.UseApplicationMessageReceivedHandlerAsync(e => {
            //Console.WriteLine(e)});
            //string receiveMsg = x.ApplicationMessage.ConvertPayloadToString();
            client.ApplicationMessageReceivedAsync += e =>
            {
                Console.WriteLine(e.ApplicationMessage.ConvertPayloadToString());
                return Task.CompletedTask;
            };
            //client.DisconnectAsync();
        }
        //private Task Client_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs x)
        //{
        //    string topic = x.ApplicationMessage.Topic;
        //    string receiveMsg = x.ApplicationMessage.ConvertPayloadToString();
        //    //Home home = _uow.HomeRepository.Get(h => h.Name == topic);
        //    Console.WriteLine("Uso sam" + receiveMsg);
        //    return Task.CompletedTask;
        //}
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}