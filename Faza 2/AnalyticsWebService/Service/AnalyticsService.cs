
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
            //_hubContext = null;
            ServiceCreated += OnServiceCreated;
            ServiceCreated?.Invoke(this, EventArgs.Empty);
        }

        private void OnServiceCreated(object sender, EventArgs args)
        {
            try
            {
                //if (_mqtt.IsConnected())
                //{
                    _mqtt.Subscribe("sensordata", OnDataReceived);
                    Console.WriteLine("subscribed");
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void OnDataReceived(object sender, BasicDeliverEventArgs arg)
        {
            try
            {
                var data = Encoding.UTF8.GetString(arg.Body.ToArray());
                var json = JsonConvert.DeserializeObject(data);
                _mqtt.Publish(data, "amq.topic");
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

     

        private async void SendEventToWebDashboard(string eventVal)
        {
            //await _hubContext.Clients.All.SendAsync("SendEvent", eventVal);
        }
    }
}
