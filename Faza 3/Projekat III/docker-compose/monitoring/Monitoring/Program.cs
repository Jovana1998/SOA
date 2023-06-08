using MQTTnet.Client;
using MQTTnet;
using Newtonsoft.Json;

namespace Monitoring
{
    public static class Program
    {
        public static void Main(String[] args)
        {
            try
            {

                var mqttFactory = new MqttFactory();

                string? address = Environment.GetEnvironmentVariable("BROKER_ADDRESS"),
                    topic = Environment.GetEnvironmentVariable("BROKER_TOPIC"),
                    deviceAddress = Environment.GetEnvironmentVariable("DEVICE_ADDRESS");
                if (address == null)
                    address = "127.0.0.1";
                if (topic == null)
                    topic = "edgex-tutorial";
                if (deviceAddress == null)
                    deviceAddress = "http://127.0.0.1:48082/api/v1/device/name/SOAProjectIII/command/value";

                var mqttClient = mqttFactory.CreateMqttClient();

                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer(address)
                    .Build();

                mqttClient.ApplicationMessageReceivedAsync += e =>
                {
                    string s = System.Text.Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    SensorDataMessage m = JsonConvert.DeserializeObject<SensorDataMessage>(s);

                    string value = "";
                    if (m.Readings?.Count > 0)
                    {
                        value = Evaluate(m.Readings[0]);
                        Console.Write(value + "\t");
                        HttpClient client = new HttpClient();
                        //value = m.Readings[0].Value +" : "+ m.Readings[0].Name;
                        HttpResponseMessage rm = client.PutAsync(deviceAddress,
                                new StringContent(JsonConvert.SerializeObject(
                                    new
                                    {
                                        value
                                    }),
                                    System.Text.Encoding.UTF8,
                                    "application/json")).Result;
                        Console.WriteLine(rm.Content.ReadAsStringAsync().Result);
                    }
                    //}

                    return Task.CompletedTask;
                };
          
            mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None).Wait();

            Console.WriteLine("MQTT client connected!");

            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic(topic); })
                .Build();

            mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None).Wait();

            Console.WriteLine("MQTT client subscribed to topic!");
            Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public static string Evaluate(SensorDataReading r)
        {
            string ret = "";
            switch(r.Name)
            {
                case ("temperature"):
                    int t = Int16.Parse(r.Value);
                    if (t < 25)
                        ret = "off";
                    else 
                        ret = "on";
                    break;
                case ("humidity"):
                    int h = Int16.Parse(r.Value);
                    if (h > 70)
                        ret = "on";
                    else 
                        ret = "off";
                    break;
                case ("ping"):
                    float p = float.Parse(r.Value);
                    if (p > 20.0)
                        ret = "on";
                    else
                        ret = "off";
                    break;
                case ("time"):
                    DateTime time = DateTime.Parse(r.Value);
                    if (time.Day == DateTime.Now.Day)
                        ret = "on";
                    else
                        ret = "off";
                    break;
                default:
                    break;
            }
            return r.Name +" : "+ret;
        }
    }
}