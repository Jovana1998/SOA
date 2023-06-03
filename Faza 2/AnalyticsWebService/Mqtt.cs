﻿using MQTTnet;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
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
                factory = new ConnectionFactory() { HostName = "host.docker.internal", Port = 5673, UserName ="joka", Password="joka"};
            
                conn = factory.CreateConnection();
                channel = conn.CreateModel();

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

        public void Subscribe(string topic, EventHandler<BasicDeliverEventArgs> callback)
        {
            try
            {
                //channel.ExchangeDeclare(topic, ExchangeType.Direct);
                channel.QueueDeclare(queue: topic,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
                //channel.QueueBind(topic, topic, topic, null);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += callback;
                channel.BasicConsume(queue: topic,
                                     autoAck: true,
                                     consumer: consumer);

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
