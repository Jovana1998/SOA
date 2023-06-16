using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AnalyticsWebService
{
    public class Mqtt
    {
        private readonly ConnectionFactory factory;
        private static IConnection conn;
        private static IModel channel;

        public Mqtt()
        {
            try
            {
                var mqttFactory = new MqttFactory();
                var client = mqttFactory.CreateMqttClient();
                var mqttClientOptions = new MqttClientOptionsBuilder()
                  .WithTcpServer("test.mosquitto.org")
                  .Build();

                client.ApplicationMessageReceivedAsync += e =>
                {
                    var data = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    Console.WriteLine(data);
                    //posalji na drugi topic 
                    return Task.CompletedTask;
                };

                client.ConnectAsync(mqttClientOptions, CancellationToken.None).Wait();

                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                 .WithTopicFilter(f => { f.WithTopic("sensordata"); })
                 .Build();

                client.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None).Wait();
                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        public void Discoonnect()
        {
            try
            {
                if(channel.IsOpen)
                    channel.Close();
                if(conn.IsOpen)
                    conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.InnerException);
            }
        }

        public bool IsConnected()
        {
            return conn.IsOpen;
        }

        public void Publish(object data, string topic)
        {
            try
            {
                channel.ExchangeDeclare(topic, ExchangeType.Direct);
                channel.QueueDeclare(queue: topic,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                channel.QueueBind(topic, topic, topic, null);
                channel.BasicPublish(exchange: topic,
                                     routingKey: topic,
                                     basicProperties: null,
                                     body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));
            }
            catch (Exception e)
            {
                Console.WriteLine("Publish failed: " + e.Message);
            }
        }

        public async Task Subscribe(string topic, EventHandler<BasicDeliverEventArgs> callback)
        {
            try
            {
                var options = new MqttClientOptionsBuilder()
                    .WithClientId(Guid.NewGuid().ToString())
                    .WithTcpServer("host.docker.internal", 1883)
                    .WithCleanSession()
                    .Build();
         
                Console.WriteLine(" Press [enter] to exit.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Subscribe failed: " + e.Message);
            }
        }

        public void UnSubscribe(string topic)
        {
            try
            {
                //channel.ExchangeDeclare(topic, ExchangeType.Direct);
                channel.QueueDeclare(queue: topic,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
                //channel.QueueBind(topic, topic, topic, null);
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicCancel(consumer.ConsumerTags[0]);

                Console.WriteLine(" Press [enter] to exit.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Subscribe failed: " + e.Message);
            }
        }
    }
}
