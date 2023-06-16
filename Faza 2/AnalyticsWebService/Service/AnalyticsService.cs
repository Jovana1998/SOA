
using MQTTnet;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System.Text;

namespace AnalyticsWebService.Services
{
    public class AnalyticsService
    {
        private readonly Mqtt _mqtt;
        private event EventHandler ServiceCreated;
        //private IHubContext<MessageHub> _hubContext;
        public AnalyticsService()
        {
            _mqtt = new Mqtt();
       
        }
    }
}
